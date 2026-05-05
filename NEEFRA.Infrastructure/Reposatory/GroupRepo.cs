using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NEEFRA.Core.Entities.Group;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using Villa_API_Project.DataAccess.Reposatory;

namespace Realtima_Chat_project.DataAccess.Reposatory
{
    public class GroupRepo : Reposatory<Group>, IGroupReposatory
    {
        MongoDbContext Context;
        public GroupRepo(MongoDbContext context) : base(context)
        {
            this.Context = context;

        }

        public void DeleteImageMethod(string imageURL, IWebHostEnvironment env)
        {
            if (!string.IsNullOrEmpty(imageURL))
            {
                var relativePath = imageURL.TrimStart('\\', '/');
                var oldImagePath = Path.Combine(env.WebRootPath, relativePath);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

        }

   

        public string GetImageURL(IFormFile ImageFile, string id, IWebHostEnvironment env)
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                return null;
            }

            string folderpath = Path.Combine(env.WebRootPath, "Images/Group" + id);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string path = Path.Combine(folderpath, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }


            return "/Images/Group" + id + "/" + fileName;


        }

      
    }
    
}
