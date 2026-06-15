using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NEEFRA.Core.Entities.AI;
using NEEFRA.Core.Interfaces.IReposatory;
using NEEFRA_API.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villa_API_Project.DataAccess.Reposatory;

namespace NEEFRA.Infrastructure.Reposatory
{
    public class Ai_ARepo : Reposatory<Ai_A>, IAi_ARepo
    {
        MongoDbContext Context;
        public Ai_ARepo(MongoDbContext context) : base(context)
        {
            this.Context = context;
        }

        public string GetImageURL(IFormFile audiofile, string peice_name, string lang, IWebHostEnvironment env,string type)
        {
            if (audiofile == null || audiofile.Length == 0)
            {
                return null;
            }

            string folderpath = Path.Combine(env.WebRootPath, "Audio/" + peice_name+"__"+lang+"__"+type);
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(audiofile.FileName);
            string path = Path.Combine(folderpath, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                audiofile.CopyTo(stream);
            }


            return "/Audio/" +peice_name+"__" + lang + "__" + type + "/" + fileName;
        }
    }
}
