using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NEEFRA.API.Helpers;
using NEEFRA.Core.DTO.Group;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Interfaces.IService;
using Restaurant.API.Controllers;
using SignalIR_practice.Hubs;
using System.Security.Claims;
namespace NEEFRA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly IWebHostEnvironment env;
        private readonly IHubContext<ChatHub> hub;

        public GroupController(IGroupService groupService, IWebHostEnvironment env, IHubContext<ChatHub> hubContext)
        {
            this.groupService = groupService;
            this.env = env;
            this.hub = hubContext;
        }


    

        [HttpPost("Create")]
        public async Task<IActionResult> CreateGroup([FromForm] CreateGroupDTO dto)
        {
            var result = await groupService.CreateGroupAsync(UserId, dto, BaseUrl, env);
            return HandleResult(result);
        }

        [HttpGet("GetGroup/{groupId}")]
        public async Task<IActionResult> GetGroupById(string groupId)
        {
            var result = await groupService.GetGroupByIdAsync(UserId, groupId, BaseUrl);
            return HandleResult(result);
        }

        [HttpGet("MyGroups")]
        public async Task<IActionResult> GetMyGroups()
        {
            var result = await groupService.GetMyGroupsAsync(UserId, BaseUrl);
            return HandleResult(result);
        }

        [HttpPut("UpdateGroup/{groupId}")]
        public async Task<IActionResult> UpdateGroup(string groupId, [FromForm] UpdateGroupDTO dto)
        {
            var result = await groupService.UpdateGroupAsync(UserId, groupId, dto, BaseUrl, env);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            await hub.Clients.Group($"Group{groupId}").SendAsync("GroupInfoUpdated", new
            {
                groupId = d.GroupId,
                name = d.Name,
                description = d.Description,
                tour_Type = d.TourType,
                Expexted_numberOfmembers = d.NumberOfMembers,
                imageUrl = d.ImageUrl,
                updatedBy = UserId
            });

            return HandleResult(result);
        }


        [HttpPost("Join")]
        public async Task<IActionResult> JoinGroup([FromBody] JoinGroupDTO dto)
        {
            var result = await groupService.JoinGroupAsync(UserId, dto, BaseUrl);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            await hub.Clients.Group("Group" + d.GroupId).SendAsync("MemberJoined", new
            {
                groupId = d.GroupId,
                userId = d.UserId,
                userName = d.UserName,
                userImage = d.UserImage,
                joinedAt = d.JoinedAt
            });

            return HandleResult(result);
        }

        [HttpPost("AddMembers")]
        public async Task<IActionResult> AddMembers([FromBody] AddMembersDTO dto)
        {
            var result = await groupService.AddMembersAsync(UserId, dto, BaseUrl);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            if (d.Members.Any())
            {
                await hub.Clients.Group("Group" + d.GroupId).SendAsync("MembersAdded", new
                {
                    groupId = d.GroupId,
                    members = d.Members,
                    addedBy = UserId
                });
            }

            return HandleResult(result);
        }

        [HttpPost("RemoveMember")]
        public async Task<IActionResult> RemoveMember([FromBody] RemoveMemberDTO dto)
        {
            var result = await groupService.RemoveMemberAsync(UserId, dto);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            await hub.Clients.Group("Group" + d.GroupId).SendAsync("MemberRemoved", new
            {
                groupId = d.GroupId,
                userId = d.RemovedUserId,
                removedBy = UserId,
                userName = d.RemovedUserName
            });
            await hub.Clients.User(d.RemovedUserId).SendAsync("RemovedFromGroup", new
            {
                groupId = d.GroupId,
                groupName = d.GroupName,
                userName = d.RemovedUserName
            });

            return HandleResult(result);
        }

        [HttpPost("Leave/{groupId}")]
        public async Task<IActionResult> LeaveGroup(string groupId)
        {
            var result = await groupService.LeaveGroupAsync(UserId, groupId);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            await hub.Clients.Group("Group" + d.GroupId).SendAsync("MemberLeft", new
            {
                groupId = d.GroupId,
                userId = d.UserId,
                userName = d.UserName
            });

            return HandleResult(result);
        }

        [HttpGet("Members/{groupId}")]
        public async Task<IActionResult> GetGroupMembers(string groupId)
        {
            var result = await groupService.GetGroupMembersAsync(UserId, groupId, BaseUrl);
            return HandleResult(result);
        }

        [HttpGet("AvailableUsers/{groupId}")]
        public async Task<IActionResult> GetAvailableUsers(string groupId)
        {
            var result = await groupService.GetAvailableUsersAsync(UserId, groupId, BaseUrl);
            return HandleResult(result);
        }

        

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromForm] SendGroupMessageDTO dto)
        {
            var result = await groupService.SendMessageAsync(UserId, dto, BaseUrl,env);
            if (!result.IsSuccess) return HandleResult(result);

            await hub.Clients.Group("Group" + dto.GroupId).SendAsync("ReceiveGroupMessage", result.Data);

            return HandleResult(result);
        }

        [HttpPost("ReplyToMessage")]
        public async Task<IActionResult> ReplyToMessage([FromForm] ReplyToMessageDTO dto)
        {
            var result = await groupService.ReplyToMessageAsync(UserId, dto, BaseUrl,env);
            if (!result.IsSuccess) return HandleResult(result);

            await hub.Clients.Group("Group" + dto.GroupId).SendAsync("ReceiveGroupMessage", result.Data);

            return HandleResult(result);
        }

        [HttpGet("Messages/{groupId}")]
        public async Task<IActionResult> GetGroupMessages(string groupId)
        {
            var result = await groupService.GetGroupMessagesAsync(UserId, groupId, BaseUrl);
            return HandleResult(result);
        }

        [HttpGet("LastMessage/{groupId}")]
        public async Task<IActionResult> LastMessage(string groupId)
        {
            var result = await groupService.LastMessage(UserId, groupId, BaseUrl);
            return HandleResult(result);
        }

        [HttpPost("MarkAsRead/{messageId}")]
        public async Task<IActionResult> MarkAsRead(string messageId)
        {
            var result = await groupService.MarkAsReadAsync(UserId, messageId);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            if (!d.AlreadyRead)
            {
                await hub.Clients.User(d.UserId).SendAsync("MessageRead", new
                {
                    messageId = d.MessageId,
                    groupId = d.GroupId,
                    userId = d.UserId,
                    userName = d.UserName,
                    readAt = d.ReadAt
                });
            }

            return HandleResult(result);
        }

        [HttpGet("WhoRead/{messageId}")]
        public async Task<IActionResult> WhoReadMessage(string messageId)
        {
            var result = await groupService.WhoReadMessageAsync(UserId, messageId, BaseUrl);
            return HandleResult(result);
        }

        [HttpGet("UnreadCount/{groupId}")]
        public async Task<IActionResult> GetUnreadCount(string groupId)
        {
            var result = await groupService.GetUnreadCountAsync(UserId, groupId);
            return HandleResult(result);
        }
        [HttpGet("MessageNotification/{groupId}")]
        public async Task<IActionResult> MessageNotification(string groupId)
        {
            var result = await groupService.MessageNotification(UserId, groupId,BaseUrl);
            return HandleResult(result);
        }


        [HttpDelete("DeleteMessageForMe/{messageId}")]
        public async Task<IActionResult> DeleteMessageForMe(string messageId)
        {
            var result = await groupService.DeleteMessageForMeAsync(UserId, messageId);
            return HandleResult(result);
        }

        [HttpDelete("DeleteMessageForEveryone/{messageId}")]
        public async Task<IActionResult> DeleteMessageForEveryone(string messageId)
        {
            var result = await groupService.DeleteMessageForEveryoneAsync(UserId, messageId);
            if (!result.IsSuccess) return HandleResult(result);

            var d = result.Data!;
            await hub.Clients.Group($"Group{d.GroupId}").SendAsync("MessageDeletedForEveryone", new
            {
                messageId = d.MessageId,
                groupId = d.GroupId,
                deletedBy = UserId
            });

            return HandleResult(result);
        }

        [HttpDelete("ClearChat/{groupId}")]
        public async Task<IActionResult> ClearChatForMe(string groupId)
        {
            var result = await groupService.ClearChatForMeAsync(UserId, groupId);
            return HandleResult(result);
        }

    }
}
