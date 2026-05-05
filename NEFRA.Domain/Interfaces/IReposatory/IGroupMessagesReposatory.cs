using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NEEFRA.Core.Entities.Group;

namespace NEEFRA.Domain.IReposatory
{
    public interface IGroupMessagesReposatory : IReposatory<GroupMessage>
    {
        void DeleteImageMethod(string file, IWebHostEnvironment env);
        string GetImageURL(IFormFile file, IWebHostEnvironment env, MessageType Type);
    }
}
