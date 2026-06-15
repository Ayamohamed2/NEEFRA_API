using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NEEFRA.Core.DTO.AIDescription;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities.AI;
using NEEFRA.Core.Entities.Route;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;
using YourApp.Services;


namespace NEEFRA.Core.Services
{
    public class AIService :IAIService
    {
        private readonly SummarizeService summary;
        private readonly IUnitOfWork unit;
        private readonly TtsService _ttsService;
        private readonly SpanishTtsService _spanishTtsService;

        private readonly ArabicTtsService _arabicTtsService;

        public AIService(SummarizeService summary,IUnitOfWork unit, TtsService ttsService, SpanishTtsService _spanishTtsService,ArabicTtsService _arabicTtsService)
        {
            this.summary = summary;
            this.unit = unit;
            _ttsService = ttsService;
            this._spanishTtsService = _spanishTtsService;
            this._arabicTtsService = _arabicTtsService;
        }

        public async Task<ServiceResult<object>> AIDescription(AIDescriptionDTO dto, string userId, IWebHostEnvironment env, string baseurl)
        {
            if (dto.type != "summrized" && dto.type != "detailed")
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "Type is summrized or detailed only"
                };
            }
            var visitorder = (await unit.RoutePiece.GetAllAsync(r => r.VisitId == dto.VisitId)).Count() + 1;
            var peice = await unit.ArtPiece.GetByFilterAsync(p => p.Name == dto.PieceName);
            var entity = new RoutePiece
            {
                VisitId = dto.VisitId,
                PieceName = dto.PieceName,
                PieceId = peice?.Id,//TODO
                UserId = userId,
                Visited = true,
                VisitOrder = visitorder,
                ImageURl =  peice?.ImageUrl

            };

            await unit.RoutePiece.CreateAsync(entity);


            var result = await unit.AI_a.GetByFilterAsync(p => p.Name == dto.PieceName && p.Language == dto.lang&& p.Type==dto.type);
           
            if (result == null)
            {
             var data = (await unit.PieceDescription.GetByFilterAsync(p => p.Name == dto.PieceName))?.Description;

            string? Text = "";
            if (dto.lang == "English")
            {
                Text = data;
            }
            else if (dto.lang == "Spanish")
            {
                Text = (await unit.SpanishPieceDescription.GetByFilterAsync(p => p.Name == dto.PieceName))?.Description;
            }
            else if (dto.lang == "Arabic")
            {
                Text = (await unit.ArabicPieceDescription.GetByFilterAsync(p => p.Name == dto.PieceName))?.Description;

            }

            if (dto.type == "summrized")
            {
                data = (await summary.SummarizeAsync(new SummarizeRequest { Language ="English" , Paragraph = data }))?.Text;
                if (dto.lang == "English")
                {
                    Text = data;
                }
                if (dto.lang == "Arabic")
                {
                    Text= (await summary.SummarizeAsync(new SummarizeRequest { Language = dto.lang, Paragraph = Text }))?.Text;
                }
            }
                IFormFile audio;
                if (data == null)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "notfound"
                    };
                }
                if (dto.lang == "English")
                {
                    var a = (await _ttsService.GenerateSpeechAsync(data, "bm_lewis", 1.0));

                    audio = BytesToFormFile(a);
                }
                else if (dto.lang == "Spanish")
                {

                    var a = await _spanishTtsService.GenerateSpeechAsync(data);

                    audio = BytesToFormFile(a);


                }
                else if (dto.lang == "Arabic")
                {
                    var a = await _arabicTtsService.TranslateAndSpeakAsync(data);
                    audio = BytesToFormFile(a, "audio.mp3"); // MP3 مش WAV


                }
                else
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "notfound"
                    };
                }


                var audioUrl = unit.AI_a.GetImageURL(audio, dto.PieceName, dto.lang, env,dto.type);

                var ai_a = new Ai_A
                {
                    Name = dto.PieceName,
                    Language = dto.lang,
                    Type=dto.type,
                    text=Text,
                    AudioUrl = audioUrl
                };

                await unit.AI_a.CreateAsync(ai_a);

                var bs = string.IsNullOrEmpty(audioUrl)
                   ? null
               : baseurl + audioUrl;
                return new()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        text = Text,
                        AudioUrl = bs
                    }
                };
            }
            else
            {
                var bs = string.IsNullOrEmpty(result.AudioUrl)
                   ? null
               : baseurl + result.AudioUrl;

                await Task.Delay(TimeSpan.FromSeconds(10));

                return new()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        text = result.text,
                        AudioUrl = bs
                    }
                };

            }


        }


        public static IFormFile BytesToFormFile(byte[] audioBytes, string fileName = "audio.wav")
            {
                var stream = new MemoryStream(audioBytes);

                IFormFile formFile = new FormFile(
                    baseStream: stream,
                    baseStreamOffset: 0,
                    length: stream.Length,
                    name: "file",          // اسم الـ field
                    fileName: fileName     // اسم الملف
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "audio/wav"
                };

                return formFile;
            }

   
    }
}
