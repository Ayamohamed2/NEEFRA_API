using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using NEEFRA.Domain.IReposatory;
using System.Collections.Concurrent;

namespace SignalIR_practice.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork unit;

        private static readonly ConcurrentDictionary<string, HashSet<string>> Connections
            = new ConcurrentDictionary<string, HashSet<string>>();
        private static readonly ConcurrentDictionary<string, HashSet<string>> UserGroups = new();
        private static readonly ConcurrentDictionary<string, HashSet<string>> GroupTypingUsers = new();
        private static readonly ConcurrentDictionary<string, (double lat, double lng)> GroupLocations = new();
        public ChatHub(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            if (string.IsNullOrEmpty(userId))
                return;

            var userConnections = Connections.GetOrAdd(userId, _ => new HashSet<string>());

            lock (userConnections)
            {
                userConnections.Add(connectionId);
            }


            var allusers = await unit.Users.GetAllAsync();
            var users=allusers.Select(u => new
            {
                UserId = u.Id,
                IsOnline = Connections.ContainsKey(u.Id),
                LastSeen = u.LastSeen
            }).ToList();

            await Clients.Caller.SendAsync("InitialUserStatuses", users);

            
            if (userConnections.Count == 1)
            {
                await Clients.Others.SendAsync("UserOnline", userId);
            }
            if (!UserGroups.ContainsKey(userId))
            {
                UserGroups[userId] = new HashSet<string>();
            }
            var groups =( await unit.GroupMember.GetAllAsync(g => g.UserId == userId)).Select(g => g.GroupId).ToList();

            foreach(var g in groups)
            {
                var groupName = "Group" + g;

                await Groups.AddToGroupAsync(connectionId, groupName);
                lock (UserGroups)
                {
                    UserGroups[userId].Add(g);
                }

            }

            await base.OnConnectedAsync();
        }

        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            if (string.IsNullOrEmpty(userId))
                return;

            if (Connections.TryGetValue(userId, out var userConnections))
            {
                lock (userConnections)
                {
                    userConnections.Remove(connectionId);
                }


                if (userConnections.Count == 0)
                {
                    Connections.TryRemove(userId, out _);

                    var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
                   

                    await Clients.All.SendAsync("Useroffline", userId);
                }
            }
            if (UserGroups.TryGetValue(userId, out var groups))
            {
                foreach (var groupId in groups)
                {
                    var groupName = "Group" + groupId;

                    if (GroupTypingUsers.TryGetValue(groupId, out var typingUsers))
                    {
                        lock (typingUsers)
                        {
                            typingUsers.Remove(userId);
                        }

                        await Clients.OthersInGroup(groupName).SendAsync("UserStoppedTyping", new
                        {
                            groupId = groupId,
                            userId = userId
                        });
                    }

                }

                UserGroups.Remove(userId,out _);
            }
          

            await base.OnDisconnectedAsync(exception);
        }

        


        public async Task JoinGroup(string groupId)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(userId))
                return;

            var ismemeber =(await unit.GroupMember.GetAllAsync(g => g.GroupId == groupId && g.UserId == userId)).Any();
            if (!ismemeber)
            {
                return;
            }

            var groupName = "Group" + groupId;
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            if (!UserGroups.ContainsKey(userId))
            {
                UserGroups[userId] = new HashSet<string>();
            }
            UserGroups[userId].Add(groupId);

            if (GroupLocations.TryGetValue(groupId, out var location))
            {
                await Clients.Caller.SendAsync("ReceiveLeaderLocation", new
                {
                    groupId = groupId,
                    latitude = location.lat,
                    longitude = location.lng
                });
            }
        }


        public async Task LeaveGroup(string groupId)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(userId))
                return;

            var groupName = "Group" + groupId;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            if (UserGroups.ContainsKey(userId))
            {
                UserGroups[userId].Remove(groupId);
            }

            if (GroupTypingUsers.TryGetValue(groupId, out var typingUsers))
            {
                lock (typingUsers)
                {
                    typingUsers.Remove(userId);
                }
            }


        }


        public async Task StartTyping(string groupId)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(userId))
                return;
            var ismemeber =( await unit.GroupMember.GetAllAsync(g => g.GroupId == groupId && g.UserId == userId)).Any();
            if (!ismemeber)
            {
                return;
            }

            var typingUsers = GroupTypingUsers.GetOrAdd(groupId, _ => new HashSet<string>());

            lock (typingUsers)
            {
                typingUsers.Add(userId);
            }
            var groupName = "Group" + groupId;

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            await Clients.OthersInGroup(groupName).SendAsync("UserStartedTyping", new
            {
                groupId = groupId,
                userId = userId,
                userName = user?.Name,
            });

        }

        public async Task GroupStopTyping(string groupId)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(userId))
                return;

            if (GroupTypingUsers.TryGetValue(groupId, out var typingUsers))
            {
                lock (typingUsers)
                {
                    typingUsers.Remove(userId);
                }
            }

            var user =await unit.Users.GetByFilterAsync(u => u.Id == userId);
            var groupName = "Group" + groupId;

            await Clients.OthersInGroup(groupName).SendAsync("UserStoppedTyping", new
            {
                groupId = groupId,
                userId = userId,
                userName = user?.Name
            });
        }



        public async Task<List<string>> GetTypingUsers(string groupId)
        {
            if (GroupTypingUsers.ContainsKey(groupId))
            {
                return GroupTypingUsers[groupId].ToList();
            }
            return new List<string>();
        }



        public async Task UpdateLeaderLocation(string groupId, double lat, double lng)
        {
           
            var groupName = "Group" + groupId;

            GroupLocations[groupId] = (lat, lng);

            await Clients.Group(groupName).SendAsync("ReceiveLeaderLocation", new
            {
                groupId = groupId,
                latitude = lat,
                longitude = lng
            });
        }
    }
}
