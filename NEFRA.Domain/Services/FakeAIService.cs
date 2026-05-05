using Microsoft.AspNetCore.Http;
using NEEFRA.Core.DTO.Service.FakeAI;
using NEEFRA.Core.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Services
{
    public class FakeAIService : IAIFakeService
    {
        public async Task<FakeAI> AnalyzePiece(string pieceName ,IFormFile image)
        {
            await Task.Delay(1500); // simulate AI delay

            return new()
            {

                PieceName = pieceName,
                Description = "This artifact belongs to the ancient Egyptian civilization.",
                AudioUrl = "/audio/fake-audio.mp3"
            };
            }

     
    }
}
