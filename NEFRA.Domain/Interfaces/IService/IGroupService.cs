using Microsoft.AspNetCore.Hosting;
using NEEFRA.Core.DTO.Group;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.DTO.Service.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IGroupService
    {
        Task<ServiceResult<CreateGroupResultDTO>> CreateGroupAsync(string userId, CreateGroupDTO dto, string baseUrl, IWebHostEnvironment env);
        Task<ServiceResult<GroupDataDTO>> GetGroupByIdAsync(string userId, string groupId, string baseUrl);
        Task<ServiceResult<List<object>>> GetMyGroupsAsync(string userId, string baseUrl);
        Task<ServiceResult<UpdateGroupResultDTO>> UpdateGroupAsync(string userId, string groupId, UpdateGroupDTO dto, string baseUrl, IWebHostEnvironment env);

        // Membership
        Task<ServiceResult<JoinGroupResultDTO>> JoinGroupAsync(string userId, JoinGroupDTO dto, string baseUrl);
        Task<ServiceResult<AddMembersResultDTO>> AddMembersAsync(string userId, AddMembersDTO dto, string baseUrl);
        Task<ServiceResult<RemoveMemberResultDTO>> RemoveMemberAsync(string userId, RemoveMemberDTO dto);
        Task<ServiceResult<LeaveGroupResultDTO>> LeaveGroupAsync(string userId, string groupId);
        Task<ServiceResult<List<object>>> GetGroupMembersAsync(string userId, string groupId, string baseUrl);
        Task<ServiceResult<List<object>>> GetAvailableUsersAsync(string userId, string groupId, string baseUrl);

        // Messages
        Task<ServiceResult<GroupMessageResultDTO>> SendMessageAsync(string senderId, SendGroupMessageDTO dto, string baseUrl, IWebHostEnvironment env);
        Task<ServiceResult<GroupMessageResultDTO>> ReplyToMessageAsync(string userId, ReplyToMessageDTO dto, string baseUrl, IWebHostEnvironment env);
        Task<ServiceResult<List<object>>> GetGroupMessagesAsync(string userId, string groupId, string baseUrl);
        Task<ServiceResult<object>> LastMessage(string userId, string groupId, string baseUrl);
        Task<ServiceResult<MarkAsReadResultDTO>> MarkAsReadAsync(string userId, string messageId);
        Task<ServiceResult<List<object>>> WhoReadMessageAsync(string userId, string messageId, string baseUrl);
        Task<ServiceResult<object>> GetUnreadCountAsync(string userId, string groupId);
        Task<ServiceResult<DeleteMessageResultDTO>> DeleteMessageForMeAsync(string userId, string messageId);
        Task<ServiceResult<DeleteMessageResultDTO>> DeleteMessageForEveryoneAsync(string userId, string messageId);
        Task<ServiceResult<object>> ClearChatForMeAsync(string userId, string groupId);
        Task<ServiceResult<List<object>>> MessageNotification(string userId, string groupId, string baseUrl);
    }
}
