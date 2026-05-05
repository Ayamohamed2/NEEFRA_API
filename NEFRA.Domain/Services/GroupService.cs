using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Group;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.DTO.Service.Group;
using NEEFRA.Core.Entities.Group;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;
using Restaurant.Core.Interfaces.IService.Redis;

namespace NEEFRA.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger<GroupService> _logger;
        private readonly IRedisCacheService _cache;

        // ── Cache key templates ──────────────────────────────────────────────
        private const string GROUP_KEY = "group:{0}";                   // single group details
        private const string USER_GROUPS_KEY = "groups:user:{0}";             // list of groups for a user
        private const string GROUP_MEMBERS_KEY = "group:{0}:members";           // member list
        private const string GROUP_MESSAGES_KEY = "group:{0}:messages:user:{1}"; // per-user message view

        // ── TTLs ─────────────────────────────────────────────────────────────
        private static readonly TimeSpan GroupExpiry = TimeSpan.FromMinutes(15);
        private static readonly TimeSpan MembersExpiry = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan MessagesExpiry = TimeSpan.FromMinutes(2);  // short: changes often

        public GroupService(IUnitOfWork unit, ILogger<GroupService> logger, IRedisCacheService cache)
        {
            this.unit = unit;
            _logger = logger;
            _cache = cache;
        }

        // ════════════════════════════════════════════════════════════════════
        // Group CRUD
        // ════════════════════════════════════════════════════════════════════

        private string GenerateJoinCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private GroupMessage BuildMessage(string groupId, string senderId, MessageType type,
            string? text, IFormFile? file, IWebHostEnvironment env)
        {
            var message = new GroupMessage
            {
                GroupId = groupId,
                SenderId = senderId,
                Type = type,
                CreatedAt = DateTime.UtcNow
            };
            if (type == MessageType.Text)
                message.TextContent = text;
            else if (file != null)
                message.MediaUrl = unit.GroupMessages.GetImageURL(file, env, Type: type);
            return message;
        }

        public async Task<ServiceResult<CreateGroupResultDTO>> CreateGroupAsync(
            string userId, CreateGroupDTO dto, string baseUrl, IWebHostEnvironment env)
        {
            _logger.LogInformation("Creating group for userId: {UserId}, groupName: {GroupName}", userId, dto.Name);

            string? imgurl = null;
            if (dto.Image != null)
                imgurl = unit.Group.GetImageURL(dto.Image, userId, env);

            var joinCode = GenerateJoinCode();
            while (await unit.Group.GetByFilterAsync(j => j.JoinCode == joinCode) != null)
                joinCode = GenerateJoinCode();

            var group = new Group
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = imgurl,
                CreatorId = userId,
                JoinCode = joinCode,
                NumberOf_members = dto.NumberOf_members,
                tour_type = dto.tour_type,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await unit.Group.CreateAsync(group);
            await unit.GroupMember.CreateAsync(new GroupMember
            {
                GroupId = group.Id,
                UserId = userId,
                IsAdmin = true,
                JoinedAt = DateTime.UtcNow,
                IsActive = true
            });

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("Create group – user not found: {UserId}", userId);
                return new() { IsSuccess = false, Message = "User not found", ErrorType = "NotFound" };
            }

            // Bust the creator's group-list cache
            await _cache.RemoveAsync(string.Format(USER_GROUPS_KEY, userId));

            _logger.LogInformation("Group created – groupId: {GroupId}", group.Id);
            return new()
            {
                IsSuccess = true,
                Message = "Group created successfully",
                Data = new CreateGroupResultDTO
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    ImageUrl = string.IsNullOrEmpty(group.ImageUrl) ? null : $"{baseUrl}/{group.ImageUrl}",
                    JoinCode = group.JoinCode,
                    CreatorName = user.Name,
                    CreatedAt = group.CreatedAt
                }
            };
        }

        public async Task<ServiceResult<GroupDataDTO>> GetGroupByIdAsync(string userId, string groupId, string baseUrl)
        {
            _logger.LogInformation("Fetching group – groupId: {GroupId}", groupId);

            var cacheKey = string.Format(GROUP_KEY, groupId);
            var cached = await _cache.GetAsync<ServiceResult<GroupDataDTO>>(cacheKey);
            if (cached is not null)
            {
                _logger.LogDebug("Cache hit – group {GroupId}", groupId);
                return cached;
            }

            var group = await unit.Group.GetByFilterAsync(g => g.Id == groupId);
            if (group == null)
            {
                _logger.LogWarning("Get group failed – not found: {GroupId}", groupId);
                return new() { IsSuccess = false, Message = "Group not found", ErrorType = "NotFound" };
            }

            var membersCount = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.IsActive)).Count();

            var result = new ServiceResult<GroupDataDTO>
            {
                IsSuccess = true,
                Data = new GroupDataDTO
                {
                    GroupId = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    ImageUrl = string.IsNullOrEmpty(group.ImageUrl) ? null : baseUrl + group.ImageUrl,
                    joinCode = group.JoinCode,
                    TourType = group.tour_type,
                    Expexted_numberOfmembers = group.NumberOf_members,
                    createdAt = group.CreatedAt,
                    membersCount=membersCount
                }
            };

            await _cache.SetAsync(cacheKey, result, GroupExpiry);
            return result;
        }

        public async Task<ServiceResult<List<object>>> GetMyGroupsAsync(string userId, string baseUrl)
        {
            _logger.LogInformation("Fetching groups for userId: {UserId}", userId);

            var cacheKey = string.Format(USER_GROUPS_KEY, userId);
            var cached = await _cache.GetAsync<ServiceResult<List<object>>>(cacheKey);
            if (cached is not null)
            {
                _logger.LogDebug("Cache hit – groups for user {UserId}", userId);
                return cached;
            }

            var memberships = await unit.GroupMember.GetAllAsync(g => g.UserId == userId && g.IsActive);
            var groupIds = memberships.Select(g => g.GroupId).ToList();
            var allMembers = await unit.GroupMember.GetAllAsync(m => groupIds.Contains(m.GroupId) && m.IsActive);
            var groupsInfo = await unit.Group.GetAllAsync(u => groupIds.Contains(u.Id));
            var groupsDict = groupsInfo.ToDictionary(g => g.Id,g=>g);
            
            var data = memberships.Select(  membership =>
            {
                var groupMembers = allMembers.Where(m => m.GroupId == membership.GroupId).ToList();
                var isAdmin = groupMembers.FirstOrDefault(m => m.UserId == userId)?.IsAdmin ?? false;
                groupsDict.TryGetValue(membership.GroupId, out var g);
                
                return (object)new
                {
                    id = membership.GroupId,
                    name = g?.Name,
                    imageUrl = string.IsNullOrEmpty(g?.ImageUrl) ? null : baseUrl + g.ImageUrl,
                    membersCount = groupMembers.Count,
                    isAdmin,
                    tourType = g?.tour_type,
                    Expected_Number_of_members = g?.NumberOf_members,
                    CreatedAT = g?.CreatedAt
                };
            })
            .OrderByDescending(g => ((dynamic)g).CreatedAT)
            .ToList();

            _logger.LogInformation("Fetched {Count} groups for userId: {UserId}", data.Count, userId);

            var result = new ServiceResult<List<object>> { IsSuccess = true, Data =  data };
            await _cache.SetAsync(cacheKey, result, GroupExpiry);
            return result;
        }

        public async Task<ServiceResult<UpdateGroupResultDTO>> UpdateGroupAsync(
            string userId, string groupId, UpdateGroupDTO dto, string baseUrl, IWebHostEnvironment env)
        {
            _logger.LogInformation("Updating group – groupId: {GroupId}", groupId);

            var isAdmin = (await unit.GroupMember.GetByFilterAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive))?.IsAdmin ?? false;
            if (!isAdmin)
                return new() { IsSuccess = false, Message = "Only admins can update group info", ErrorType = "Forbidden" };

            var group = await unit.Group.GetByFilterAsync(g => g.Id == groupId);
            if (group == null)
                return new() { IsSuccess = false, Message = "Group not found", ErrorType = "NotFound" };

            if (!string.IsNullOrEmpty(dto.Name)) group.Name = dto.Name;
            group.Description = dto.Description;
            group.tour_type = dto.tour_type;
            group.NumberOf_members = dto.NumberOf_members;
            if (dto.Image != null) group.ImageUrl = unit.Group.GetImageURL(dto.Image, userId, env);

            await unit.Group.UpdateAsync(g => g.Id == group.Id, group);

            // Invalidate group detail + all members' group lists
            await InvalidateGroupCacheAsync(groupId);
            await _cache.RemoveAsync(string.Format(USER_GROUPS_KEY, userId));

            _logger.LogInformation("Group updated – groupId: {GroupId}", groupId);
            return new()
            {
                IsSuccess = true,
                Message = "Group updated successfully",
                Data = new UpdateGroupResultDTO
                {
                    GroupId = groupId,
                    Name = group.Name,
                    Description = group.Description,
                    TourType = group.tour_type,
                    NumberOfMembers = group.NumberOf_members,
                    ImageUrl = string.IsNullOrEmpty(group.ImageUrl) ? null : $"{baseUrl}/{group.ImageUrl}"
                }
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // Membership
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<JoinGroupResultDTO>> JoinGroupAsync(string userId, JoinGroupDTO dto, string baseUrl)
        {
            _logger.LogInformation("User joining group – userId: {UserId}, joinCode: {JoinCode}", userId, dto.JoinCode);

            var group = await unit.Group.GetByFilterAsync(g => g.JoinCode == dto.JoinCode);
            if (group == null)
                return new() { IsSuccess = false, Message = "Group not found", ErrorType = "NotFound" };

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == group.Id && m.UserId == userId && m.IsActive)).Any();
            if (isMember)
                return new() { IsSuccess = false, Message = "You are already a member of this group", ErrorType = "BadRequest" };

            var memberCount = (await unit.GroupMember.GetAllAsync(m => m.GroupId == group.Id && m.IsActive)).Count();
            if (memberCount >= group.NumberOf_members)
                return new() { IsSuccess = false, Message = "Group is full", ErrorType = "BadRequest" };

            var groupMember = new GroupMember
            {
                GroupId = group.Id,
                UserId = userId,
                IsAdmin = false,
                JoinedAt = DateTime.UtcNow,
                IsActive = true
            };

            var wasMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == group.Id && m.UserId == userId && !m.IsActive)).Any();
            if (wasMember)
            {
                groupMember = await unit.GroupMember.GetByFilterAsync(m => m.GroupId == group.Id && m.UserId == userId && !m.IsActive);
                groupMember.IsActive = true;
                await unit.GroupMember.UpdateAsync(u => u.Id == groupMember.Id, groupMember);
            }
            else
            {
                await unit.GroupMember.CreateAsync(groupMember);
            }

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            if (user == null)
                return new() { IsSuccess = false, Message = "User not found", ErrorType = "NotFound" };

            // Invalidate: new member changes group list + member list + group detail (membersCount)
            await _cache.RemoveAsync(string.Format(USER_GROUPS_KEY, userId));
            await _cache.RemoveAsync(string.Format(GROUP_MEMBERS_KEY, group.Id));
            await _cache.RemoveAsync(string.Format(GROUP_KEY, group.Id));

            _logger.LogInformation("User joined group – userId: {UserId}, groupId: {GroupId}", userId, group.Id);
            return new()
            {
                IsSuccess = true,
                Message = "Joined group successfully",
                Data = new JoinGroupResultDTO
                {
                    GroupId = group.Id,
                    GroupName = group.Name,
                    UserId = userId,
                    UserName = user.Name,
                    UserImage = string.IsNullOrEmpty(user.ImageURL) ? null : baseUrl + user.ImageURL,
                    JoinedAt = groupMember.JoinedAt
                }
            };
        }

        public async Task<ServiceResult<AddMembersResultDTO>> AddMembersAsync(string userId, AddMembersDTO dto, string baseUrl)
        {
            _logger.LogInformation("Adding members – groupId: {GroupId}", dto.GroupId);

            var group = await unit.Group.GetByFilterAsync(u => u.Id == dto.GroupId);
            if (group == null)
                return new() { IsSuccess = false, Message = "Group not found", ErrorType = "NotFound" };

            var isAdmin = (await unit.GroupMember.GetByFilterAsync(g => g.GroupId == dto.GroupId && g.UserId == userId && g.IsActive))?.IsAdmin;
            if (isAdmin != true)
                return new() { IsSuccess = false, Message = "Only admins can add members", ErrorType = "Forbidden" };

            var memberCount = (await unit.GroupMember.GetAllAsync(m => m.GroupId == group.Id && m.IsActive)).Count();
            if (memberCount >= group.NumberOf_members || (memberCount + dto.UserIds.Count()) > group.NumberOf_members)
                return new() { IsSuccess = false, Message = "Group is full", ErrorType = "BadRequest" };

            var users = await unit.Users.GetAllAsync(u => dto.UserIds.Contains(u.Id));
            var addedMembers = new List<AddedMemberDTO>();

            foreach (var targetUser in users)
            {
                var groupMember = new GroupMember
                {
                    GroupId = group.Id,
                    UserId = targetUser.Id,
                    IsAdmin = false,
                    JoinedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var wasMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == group.Id && m.UserId == targetUser.Id && !m.IsActive)).Any();
                if (wasMember)
                {
                    groupMember = await unit.GroupMember.GetByFilterAsync(m => m.GroupId == group.Id && m.UserId == targetUser.Id && !m.IsActive);
                    groupMember.IsActive = true;
                    await unit.GroupMember.UpdateAsync(u => u.Id == groupMember.Id, groupMember);
                }
                else
                {
                    await unit.GroupMember.CreateAsync(groupMember);
                }

                // Bust each new member's group-list cache
                await _cache.RemoveAsync(string.Format(USER_GROUPS_KEY, targetUser.Id));

                addedMembers.Add(new AddedMemberDTO
                {
                    UserId = targetUser.Id,
                    UserName = targetUser.Name,
                    UserImage = string.IsNullOrEmpty(targetUser.ImageURL) ? null : baseUrl + targetUser.ImageURL
                });
            }

            // Bust group-level caches
            await _cache.RemoveAsync(string.Format(GROUP_MEMBERS_KEY, dto.GroupId));
            await _cache.RemoveAsync(string.Format(GROUP_KEY, dto.GroupId));

            _logger.LogInformation("{Count} member(s) added to groupId: {GroupId}", addedMembers.Count, dto.GroupId);
            return new()
            {
                IsSuccess = true,
                Message = $"{addedMembers.Count} member(s) added successfully",
                Data = new AddMembersResultDTO { GroupId = dto.GroupId, Members = addedMembers }
            };
        }

        public async Task<ServiceResult<RemoveMemberResultDTO>> RemoveMemberAsync(string userId, RemoveMemberDTO dto)
        {
            _logger.LogInformation("Removing member – targetUserId: {TargetUserId}, groupId: {GroupId}", dto.UserId, dto.GroupId);

            var isAdmin = (await unit.GroupMember.GetByFilterAsync(g => g.GroupId == dto.GroupId && g.UserId == userId && g.IsActive))?.IsAdmin;
            if (isAdmin != true)
                return new() { IsSuccess = false, Message = "Only admins can remove members", ErrorType = "Forbidden" };

            var group = await unit.Group.GetByFilterAsync(g => g.Id == dto.GroupId);
            if (group?.CreatorId == dto.UserId)
                return new() { IsSuccess = false, Message = "Cannot remove group creator", ErrorType = "BadRequest" };

            var member = await unit.GroupMember.GetByFilterAsync(gm => gm.GroupId == dto.GroupId && gm.UserId == dto.UserId);
            member.IsActive = false;
            await unit.GroupMember.UpdateAsync(m => m.Id == member.Id, member);

            var removedUser = await unit.Users.GetByFilterAsync(u => u.Id == dto.UserId);

            // Bust removed user's group-list + group member list
            await _cache.RemoveAsync(string.Format(USER_GROUPS_KEY, dto.UserId));
            await _cache.RemoveAsync(string.Format(GROUP_MEMBERS_KEY, dto.GroupId));
            await _cache.RemoveAsync(string.Format(GROUP_KEY, dto.GroupId));

            _logger.LogInformation("Member removed – userId: {RemovedUserId}, groupId: {GroupId}", dto.UserId, dto.GroupId);
            return new()
            {
                IsSuccess = true,
                Message = "Member removed successfully",
                Data = new RemoveMemberResultDTO
                {
                    GroupId = dto.GroupId,
                    GroupName = group?.Name,
                    RemovedUserId = dto.UserId,
                    RemovedUserName = removedUser?.Name
                }
            };
        }

        public async Task<ServiceResult<LeaveGroupResultDTO>> LeaveGroupAsync(string userId, string groupId)
        {
            _logger.LogInformation("User leaving group – userId: {UserId}, groupId: {GroupId}", userId, groupId);

            var group = await unit.Group.GetByFilterAsync(g => g.Id == groupId);
            if (group?.CreatorId == userId)
                return new() { IsSuccess = false, Message = "Group creator cannot leave", ErrorType = "BadRequest" };

            var member = await unit.GroupMember.GetByFilterAsync(gm => gm.GroupId == groupId && gm.UserId == userId);
            member.IsActive = false;
            await unit.GroupMember.UpdateAsync(m => m.Id == member.Id, member);

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);

            await _cache.RemoveAsync(string.Format(USER_GROUPS_KEY, userId));
            await _cache.RemoveAsync(string.Format(GROUP_MEMBERS_KEY, groupId));
            await _cache.RemoveAsync(string.Format(GROUP_KEY, groupId));

            _logger.LogInformation("User left group – userId: {UserId}, groupId: {GroupId}", userId, groupId);
            return new()
            {
                IsSuccess = true,
                Message = "Left group successfully",
                Data = new LeaveGroupResultDTO { GroupId = groupId, UserId = userId, UserName = user?.Name }
            };
        }

        public async Task<ServiceResult<List<object>>> GetGroupMembersAsync(string userId, string groupId, string baseUrl)
        {
            _logger.LogInformation("Fetching group members – groupId: {GroupId}", groupId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var cacheKey = string.Format(GROUP_MEMBERS_KEY, groupId);
            var cached = await _cache.GetAsync<ServiceResult<List<object>>>(cacheKey);
            if (cached is not null)
            {
                _logger.LogDebug("Cache hit – members for group {GroupId}", groupId);
                return cached;
            }

            var members = await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.IsActive);
            var userIds = members.Select(m => m.UserId);
            var users = await unit.Users.GetAllAsync(u => userIds.Contains(u.Id));
            var userDict = users.ToDictionary(u => u.Id, u => u);

            var data = members.Select(m => (object)new
            {
                userId = m.UserId,
                name = userDict[m.UserId]?.Name,
                imageUrl = baseUrl + userDict[m.UserId]?.ImageURL,
                joinedAt = m.JoinedAt,
                isAdmin = m.IsAdmin
            }).ToList();

            _logger.LogInformation("Fetched {Count} members for groupId: {GroupId}", data.Count, groupId);

            var result = new ServiceResult<List<object>> { IsSuccess = true, Data = data };
            await _cache.SetAsync(cacheKey, result, MembersExpiry);
            return result;
        }

        public async Task<ServiceResult<List<object>>> GetAvailableUsersAsync(string userId, string groupId, string baseUrl)
        {
            _logger.LogInformation("Fetching available users – groupId: {GroupId}", groupId);

            var isAdmin = (await unit.GroupMember.GetByFilterAsync(g => g.GroupId == groupId && g.UserId == userId && g.IsActive))?.IsAdmin;
            if (isAdmin != true)
                return new() { IsSuccess = false, Message = "Only admins can view available users", ErrorType = "Forbidden" };

            var allUsers = await unit.Users.GetAllAsync();
            var existingMembers = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.IsActive))
                                  .Select(m => m.UserId).ToHashSet();

            var result = allUsers
                .Where(u => !existingMembers.Contains(u.Id))
                .Select(u => (object)new { userId = u.Id, name = u.Name, email = u.Email, imageUrl = baseUrl + u.ImageURL })
                .ToList();

            _logger.LogInformation("Found {Count} available users for groupId: {GroupId}", result.Count, groupId);
            return new() { IsSuccess = true, Data = result };
        }

        // ════════════════════════════════════════════════════════════════════
        // Messages
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<GroupMessageResultDTO>> SendMessageAsync(
            string senderId, SendGroupMessageDTO dto, string baseUrl, IWebHostEnvironment env)
        {
            _logger.LogInformation("Sending message – senderId: {SenderId}, groupId: {GroupId}", senderId, dto.GroupId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == dto.GroupId && m.UserId == senderId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var message = BuildMessage(dto.GroupId, senderId, dto.Type, dto.Text, dto.File, env);
            await unit.GroupMessages.CreateAsync(message);

            // New message → bust messages cache for all members (pattern delete)
            await _cache.RemoveByPatternAsync($"group:{dto.GroupId}:messages:user:*");

            var sender = await unit.Users.GetByFilterAsync(u => u.Id == senderId);
            var activeMembers = (await unit.GroupMember.GetAllAsync(m => m.GroupId == dto.GroupId))
                                .Count(m => m.UserId != senderId && m.IsActive);

            _logger.LogInformation("Message sent – messageId: {MessageId}, groupId: {GroupId}", message.Id, dto.GroupId);
            return new()
            {
                IsSuccess = true,
                Data = new GroupMessageResultDTO
                {
                    Id = message.Id,
                    GroupId = message.GroupId,
                    SenderId = message.SenderId,
                    SenderName = sender?.Name,
                    SenderImage = baseUrl + sender?.ImageURL,
                    Type = message.Type,
                    TextContent = message.TextContent,
                    MediaUrl = string.IsNullOrEmpty(message.MediaUrl) ? null : $"{baseUrl}/{message.MediaUrl}",
                    MediaDuration = message.MediaDuration,
                    CreatedAt = message.CreatedAt,
                    TotalMembers = activeMembers
                }
            };
        }

        public async Task<ServiceResult<GroupMessageResultDTO>> ReplyToMessageAsync(
            string userId, ReplyToMessageDTO dto, string baseUrl, IWebHostEnvironment env)
        {
            _logger.LogInformation("Replying to message – userId: {UserId}, groupId: {GroupId}", userId, dto.GroupId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == dto.GroupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var repliedTo = await unit.GroupMessages.GetByFilterAsync(m => m.Id == dto.ReplyToMessageId);
            if (repliedTo == null)
                return new() { IsSuccess = false, Message = "Message not found", ErrorType = "NotFound" };

            var message = BuildMessage(dto.GroupId, userId, dto.Type, dto.Text, dto.File, env);
            message.ReplyToMessageId = dto.ReplyToMessageId;
            await unit.GroupMessages.CreateAsync(message);

            await _cache.RemoveByPatternAsync($"group:{dto.GroupId}:messages:user:*");

            var sender = await unit.Users.GetByFilterAsync(u => u.Id == userId);
            var replyToSender = await unit.Users.GetByFilterAsync(u => u.Id == repliedTo.SenderId);
            var activeMembers = (await unit.GroupMember.GetAllAsync(m => m.GroupId == dto.GroupId))
                                .Count(m => m.UserId != userId && m.IsActive);

            _logger.LogInformation("Reply sent – messageId: {MessageId}, groupId: {GroupId}", message.Id, dto.GroupId);
            return new()
            {
                IsSuccess = true,
                Data = new GroupMessageResultDTO
                {
                    Id = message.Id,
                    GroupId = message.GroupId,
                    SenderId = message.SenderId,
                    SenderName = sender?.Name,
                    SenderImage = baseUrl + sender?.ImageURL,
                    Type = message.Type,
                    TextContent = message.TextContent,
                    MediaUrl = string.IsNullOrEmpty(message.MediaUrl) ? null : $"{baseUrl}/{message.MediaUrl}",
                    MediaDuration = message.MediaDuration,
                    CreatedAt = message.CreatedAt,
                    ReplyToMessageId = message.ReplyToMessageId,
                    ReplyToText = repliedTo.TextContent,
                    ReplyToSenderName = replyToSender?.Name,
                    ReplyToType = repliedTo.Type,
                    ReplyToMediaUrl = repliedTo.MediaUrl,
                    TotalMembers = activeMembers
                }
            };
        }

        public async Task<ServiceResult<List<object>>> GetGroupMessagesAsync(string userId, string groupId, string baseUrl)
        {
            _logger.LogInformation("Fetching messages – groupId: {GroupId}, userId: {UserId}", groupId, userId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var cacheKey = string.Format(GROUP_MESSAGES_KEY, groupId, userId);
            var cached = await _cache.GetAsync<ServiceResult<List<object>>>(cacheKey);
            if (cached is not null)
            {
                _logger.LogDebug("Cache hit – messages for group {GroupId}, user {UserId}", groupId, userId);
                return cached;
            }

            var member = await unit.GroupMember.GetByFilterAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive);
            var messages = await unit.GroupMessages.GetAllAsync(m => m.GroupId == groupId && m.CreatedAt >= member.JoinedAt);
            var deletedMessages = await unit.GroupMessageDeleted.GetAllAsync(d => d.UserId == userId || d.DeletedForEveryone);
            var allMembers = await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.IsActive);
            var group = await unit.Group.GetByFilterAsync(u => u.Id == groupId);

            var memberIds = allMembers.Select(m => m.UserId);
            var users = await unit.Users.GetAllAsync(u => memberIds.Contains(u.Id));
            var usersDict = users.ToDictionary(u => u.Id, u => u);

            var replyToIds = messages.Select(m => m.ReplyToMessageId);
            var replyToMessages = await unit.GroupMessages.GetAllAsync(m => replyToIds.Contains(m.Id));
            var replyToDict = replyToMessages.ToDictionary(m => m.Id, m => m);

            var groupImageUrl = string.IsNullOrEmpty(group.ImageUrl) ? null : $"{baseUrl}/{group.ImageUrl}";

            var data = messages.Select(m =>
            {
                var replyTo = new GroupMessage();
                usersDict.TryGetValue(m.SenderId, out var sender);
                if (m.ReplyToMessageId != null)
                    replyToDict.TryGetValue(m.ReplyToMessageId, out replyTo);

                var deletedForMe = deletedMessages.Any(d => d.MessageId == m.Id && d.UserId == userId);
                var deletedForEveryone = deletedMessages.Any(d => d.MessageId == m.Id && d.DeletedForEveryone);
                var activeMembersCount = allMembers.Count(mem => mem.UserId != m.SenderId && mem.IsActive);

                return new
                {
                    id = m.Id,
                    groupId = m.GroupId,
                    groupName = group.Name,
                    groupImage = groupImageUrl,
                    senderId = m.SenderId,
                    senderName = sender?.Name,
                    senderImage = string.IsNullOrEmpty(sender?.ImageURL) ? null : baseUrl + sender.ImageURL,
                    type = m.Type,
                    textContent = deletedForEveryone ? "This message was deleted" : deletedForMe ? null : m.TextContent,
                    mediaUrl = deletedForEveryone || deletedForMe ? null : string.IsNullOrEmpty(m.MediaUrl) ? null : $"{baseUrl}/{m.MediaUrl}",
                    mediaDuration = m.MediaDuration,
                    createdAt = m.CreatedAt,
                    totalMembers = activeMembersCount,
                    isDeleted = deletedForMe || deletedForEveryone,
                    deletedForMe,
                    deletedForEveryone,
                    replyToMessageId = m.ReplyToMessageId,
                    replyToText = replyTo?.TextContent,
                    replyToSenderName = replyTo?.SenderId != null &&
                                        usersDict.TryGetValue(replyTo.SenderId, out var rSender)
                                        ? rSender?.Name : null,
                    replyToType = replyTo?.Type,
                    replyToMediaUrl = replyTo?.MediaUrl
                };
            })
            .Where(m => !m.deletedForMe)
            .Select(m => (object)m)
            .ToList();

            _logger.LogInformation("Fetched {Count} messages for groupId: {GroupId}", data.Count, groupId);

            var result = new ServiceResult<List<object>> { IsSuccess = true, Data = data };
            await _cache.SetAsync(cacheKey, result, MessagesExpiry);
            return result;
        }

        public async Task<ServiceResult<object>> LastMessage(string userId, string groupId, string baseUrl)
        {
            _logger.LogInformation("Fetching last message – groupId: {GroupId}", groupId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var messages = await GetGroupMessagesAsync(userId, groupId, baseUrl);
            var lastMessage = messages.Data?.LastOrDefault();
            return new() { IsSuccess = true, Data = lastMessage };
        }

        public async Task<ServiceResult<MarkAsReadResultDTO>> MarkAsReadAsync(string userId, string messageId)
        {
            _logger.LogInformation("Marking message as read – messageId: {MessageId}, userId: {UserId}", messageId, userId);

            var message = await unit.GroupMessages.GetByFilterAsync(m => m.Id == messageId);
            if (message == null)
                return new() { IsSuccess = false, Message = "Message not found", ErrorType = "NotFound" };

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == message.GroupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var existingRead = await unit.GroupMessageRead.GetByFilterAsync(r => r.MessageId == messageId && r.UserId == userId);
            if (existingRead != null)
            {
                return new()
                {
                    IsSuccess = true,
                    Message = "Message is already read",
                    Data = new MarkAsReadResultDTO { AlreadyRead = true }
                };
            }

            var readAt = DateTime.UtcNow;
            await unit.GroupMessageRead.CreateAsync(new GroupMessageRead
            {
                MessageId = messageId,
                UserId = userId,
                ReadAt = readAt
            });

            var user = await unit.Users.GetByFilterAsync(u => u.Id == userId);

            _logger.LogInformation("Message marked as read – messageId: {MessageId}, userId: {UserId}", messageId, userId);
            return new()
            {
                IsSuccess = true,
                Message = "Message marked as read",
                Data = new MarkAsReadResultDTO
                {
                    MessageId = messageId,
                    GroupId = message.GroupId,
                    UserId = userId,
                    UserName = user?.Name,
                    ReadAt = readAt,
                    AlreadyRead = false
                }
            };
        }

        public async Task<ServiceResult<List<object>>> WhoReadMessageAsync(string userId, string messageId, string baseUrl)
        {
            _logger.LogInformation("Fetching read receipts – messageId: {MessageId}", messageId);

            var message = await unit.GroupMessages.GetByFilterAsync(m => m.Id == messageId);
            if (message == null)
                return new() { IsSuccess = false, Message = "Message not found", ErrorType = "NotFound" };

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == message.GroupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var reads = await unit.GroupMessageRead.GetAllAsync(r => r.MessageId == messageId);
            var allMembers = await unit.GroupMember.GetAllAsync(m => m.GroupId == message.GroupId && m.IsActive);
            var memberUserIds = allMembers.Where(m => m.UserId != userId).Select(m => m.UserId).ToList();
            var users = await unit.Users.GetAllAsync(u => memberUserIds.Contains(u.Id));
            var usersDict = users.ToDictionary(u => u.Id, u => u);

            var result = allMembers
                .Where(m => m.UserId != userId)
                .Select(m =>
                {
                    var read = reads.FirstOrDefault(r => r.UserId == m.UserId);
                    usersDict.TryGetValue(m.UserId, out var userInfo);
                    return (object)new
                    {
                        userId = m.UserId,
                        userName = userInfo?.Name,
                        userImage = baseUrl + userInfo?.ImageURL,
                        hasRead = read != null,
                        readAt = read?.ReadAt
                    };
                })
                .OrderByDescending(m => ((dynamic)m).hasRead)
                .ThenBy(m => ((dynamic)m).readAt)
                .ToList();

            _logger.LogInformation("Fetched read receipts for messageId: {MessageId}", messageId);
            return new() { IsSuccess = true, Data = result };
        }

        public async Task<ServiceResult<object>> GetUnreadCountAsync(string userId, string groupId)
        {
            _logger.LogInformation("Fetching unread count – groupId: {GroupId}, userId: {UserId}", groupId, userId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var member = await unit.GroupMember.GetByFilterAsync(g => g.GroupId == groupId && g.UserId == userId && g.IsActive);
            if (member == null)
                return new() { IsSuccess = false, Message = "Member not found", ErrorType = "NotFound" };

            var messages = await unit.GroupMessages.GetAllAsync(g => g.GroupId == groupId && g.SenderId != userId && g.CreatedAt > member.JoinedAt);
            var messageIds = messages.Select(m => m.Id).ToList();
            var reads = await unit.GroupMessageRead.GetAllAsync(r => r.UserId == userId && messageIds.Contains(r.MessageId));
            var readsIds = reads.Select(r => r.MessageId).ToHashSet();

            var unreadCount = messageIds.Count(id => !readsIds.Contains(id));
            _logger.LogInformation("Unread count for userId: {UserId}, groupId: {GroupId} = {UnreadCount}", userId, groupId, unreadCount);
            return new() { IsSuccess = true, Data = new { unreadCount } };
        }

        public async Task<ServiceResult<List<object>>> MessageNotification(string userId, string groupId, string baseUrl)
        {
            _logger.LogInformation("Fetching notifications – groupId: {GroupId}, userId: {UserId}", groupId, userId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var member = await unit.GroupMember.GetByFilterAsync(g => g.GroupId == groupId && g.UserId == userId && g.IsActive);
            if (member == null)
                return new() { IsSuccess = false, Message = "Member not found", ErrorType = "NotFound" };

            var messages = await unit.GroupMessages.GetAllAsync(g => g.GroupId == groupId && g.SenderId != userId && g.CreatedAt > member.JoinedAt);
            var messageIds = messages.Select(m => m.Id).ToList();
            var reads = await unit.GroupMessageRead.GetAllAsync(r => r.UserId == userId && messageIds.Contains(r.MessageId));
            var readsIds = reads.Select(r => r.MessageId).ToHashSet();

            var unreadMessages = messages.Where(m => !readsIds.Contains(m.Id)).ToList();
            var senderIds = unreadMessages.Select(r => r.SenderId);
            var Users = await unit.Users.GetAllAsync(u => senderIds.Contains(u.Id));
            var userDict = Users.ToDictionary(u => u.Id, u => u);
            var group = await unit.Group.GetByFilterAsync(g => g.Id == groupId);
            var groupImageUrl = string.IsNullOrEmpty(group.ImageUrl) ? null : $"{baseUrl}/{group.ImageUrl}";

            var result = unreadMessages.Select(m =>
            {
                return (object)new
                {
                    id = m.Id,
                    groupId,
                    groupName = group.Name,
                    groupImage = groupImageUrl,
                    senderId = m.SenderId,
                    senderName = userDict[m.SenderId]?.Name,
                    senderImage = string.IsNullOrEmpty(userDict[m.SenderId]?.ImageURL) ? null : baseUrl + userDict[m.SenderId]?.ImageURL,
                    type = m.Type,
                    textContent = m.TextContent,
                    mediaUrl = string.IsNullOrEmpty(m.MediaUrl) ? null : $"{baseUrl}/{m.MediaUrl}",
                    mediaDuration = m.MediaDuration,
                    createdAt = m.CreatedAt
                };
            }).Select(m => (object)m).ToList();

            _logger.LogInformation("Found {Count} unread notifications for userId: {UserId}, groupId: {GroupId}",
                result.Count, userId, groupId);
            return new() { IsSuccess = true, Data = result };
        }

        public async Task<ServiceResult<DeleteMessageResultDTO>> DeleteMessageForMeAsync(string userId, string messageId)
        {
            _logger.LogInformation("Deleting message for me – messageId: {MessageId}, userId: {UserId}", messageId, userId);

            var message = await unit.GroupMessages.GetByFilterAsync(m => m.Id == messageId);
            if (message == null)
                return new() { IsSuccess = false, Message = "Message not found", ErrorType = "NotFound" };

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == message.GroupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var existing = await unit.GroupMessageDeleted.GetByFilterAsync(d => d.MessageId == messageId && d.UserId == userId);
            if (existing != null)
                return new() { IsSuccess = true, Message = "Message already deleted for you", Data = new DeleteMessageResultDTO { MessageId = messageId, GroupId = message.GroupId } };

            await unit.GroupMessageDeleted.CreateAsync(new GroupMessageDeleted
            {
                MessageId = messageId,
                UserId = userId,
                DeletedAt = DateTime.UtcNow,
                DeletedForEveryone = false
            });

            // Only bust this user's message cache
            await _cache.RemoveAsync(string.Format(GROUP_MESSAGES_KEY, message.GroupId, userId));

            _logger.LogInformation("Message deleted for me – messageId: {MessageId}", messageId);
            return new() { IsSuccess = true, Message = "Message deleted for you", Data = new DeleteMessageResultDTO { MessageId = messageId, GroupId = message.GroupId } };
        }

        public async Task<ServiceResult<DeleteMessageResultDTO>> DeleteMessageForEveryoneAsync(string userId, string messageId)
        {
            _logger.LogInformation("Deleting message for everyone – messageId: {MessageId}", messageId);

            var message = await unit.GroupMessages.GetByFilterAsync(m => m.Id == messageId);
            if (message == null)
                return new() { IsSuccess = false, Message = "Message not found", ErrorType = "NotFound" };

            var member = await unit.GroupMember.GetByFilterAsync(m => m.GroupId == message.GroupId && m.UserId == userId && m.IsActive);
            if (member == null)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            if (!member.IsAdmin && message.SenderId != userId)
                return new() { IsSuccess = false, Message = "Only message sender or admins can delete for everyone", ErrorType = "Forbidden" };

            if (message.SenderId == userId && !member.IsAdmin && (DateTime.UtcNow - message.CreatedAt).TotalMinutes > 5)
                return new() { IsSuccess = false, Message = "Can only delete for everyone within 5 minutes of sending", ErrorType = "BadRequest" };

            await unit.GroupMessageDeleted.CreateAsync(new GroupMessageDeleted
            {
                MessageId = messageId,
                UserId = userId,
                DeletedAt = DateTime.UtcNow,
                DeletedForEveryone = true
            });

            // Bust all users' message caches for this group
            await _cache.RemoveByPatternAsync($"group:{message.GroupId}:messages:user:*");

            _logger.LogInformation("Message deleted for everyone – messageId: {MessageId}", messageId);
            return new() { IsSuccess = true, Message = "Message deleted for everyone", Data = new DeleteMessageResultDTO { MessageId = messageId, GroupId = message.GroupId } };
        }

        public async Task<ServiceResult<object>> ClearChatForMeAsync(string userId, string groupId)
        {
            _logger.LogInformation("Clearing chat – groupId: {GroupId}, userId: {UserId}", groupId, userId);

            var isMember = (await unit.GroupMember.GetAllAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive)).Any();
            if (!isMember)
                return new() { IsSuccess = false, Message = "You are not a member of this group", ErrorType = "BadRequest" };

            var member = await unit.GroupMember.GetByFilterAsync(m => m.GroupId == groupId && m.UserId == userId && m.IsActive);
            var messages = await unit.GroupMessages.GetAllAsync(m => m.GroupId == groupId && m.CreatedAt >= member.JoinedAt);
            var alreadyDeleted = (await unit.GroupMessageDeleted.GetAllAsync(d => d.UserId == userId)).ToDictionary(m => m.MessageId);

            int deletedCount = 0;
            foreach (var msg in messages)
            {
                if (!alreadyDeleted.ContainsKey(msg.Id))
                {
                    await unit.GroupMessageDeleted.CreateAsync(new GroupMessageDeleted
                    {
                        MessageId = msg.Id,
                        UserId = userId,
                        DeletedAt = DateTime.UtcNow,
                        DeletedForEveryone = false
                    });
                    deletedCount++;
                }
            }

            // Only this user's view changes
            await _cache.RemoveAsync(string.Format(GROUP_MESSAGES_KEY, groupId, userId));

            _logger.LogInformation("Chat cleared – userId: {UserId}, groupId: {GroupId}, deletedCount: {Count}", userId, groupId, deletedCount);
            return new() { IsSuccess = true, Data = new { message = "Chat cleared successfully", deletedCount } };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Removes group detail + member list caches.
        /// Call after any structural change to the group.
        /// </summary>
        private async Task InvalidateGroupCacheAsync(string groupId)
        {
            await _cache.RemoveAsync(string.Format(GROUP_KEY, groupId));
            await _cache.RemoveAsync(string.Format(GROUP_MEMBERS_KEY, groupId));
        }
    }
}