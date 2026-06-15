using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NEEFRA.Core.Entities.AI;
using NEEFRA.Domain.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IReposatory
{
    public interface IAi_ARepo : IReposatory<Ai_A>
    {
        string GetImageURL(IFormFile audiofile, string peice_name, string lang, IWebHostEnvironment env, string type);
    }
}
