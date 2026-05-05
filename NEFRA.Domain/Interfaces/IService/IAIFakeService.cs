using Microsoft.AspNetCore.Http;
using NEEFRA.Core.DTO.Service.FakeAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Interfaces.IService
{
    public interface IAIFakeService
    {
        Task<FakeAI> AnalyzePiece(string pieceName, IFormFile image);

    }
}
