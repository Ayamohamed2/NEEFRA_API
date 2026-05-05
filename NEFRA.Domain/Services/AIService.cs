using NEEFRA.Core.DTO.AIDescription;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities.Route;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA.Domain.IReposatory;


namespace NEEFRA.Core.Services
{
    public class AIService :IAIService
    {
        private readonly IUnitOfWork unit;
        private readonly IAIFakeService Service;
        public AIService(IUnitOfWork unit,IAIFakeService Service)
        {
            this.unit = unit;
            this.Service = Service;
        }

        public async Task<ServiceResult<object>> AIDescription(AIDescriptionDTO dto,string userId)
        {
            var result = await Service.AnalyzePiece(dto.PieceName, dto.Img);

            var visitorder = (await unit.RoutePiece.GetAllAsync(r => r.VisitId == dto.VisitId)).Count() + 1;
            var peice = await unit.ArtPiece.GetByFilterAsync(p => p.Name == dto.PieceName);
            var entity = new RoutePiece
            {
                VisitId = dto.VisitId,
                PieceName = result.PieceName,
                PieceId = peice?.Id,//TODO
                UserId = userId,
                Visited = true,
                VisitOrder= visitorder

            };

            await unit.RoutePiece.CreateAsync(entity);

            return new()
            {
                IsSuccess = true,
                Data = result
            };


        }


    }
}
