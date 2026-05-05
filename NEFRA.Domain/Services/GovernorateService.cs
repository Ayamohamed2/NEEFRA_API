using Microsoft.Extensions.Logging;
using NEEFRA.Core.DTO.Service;
using NEEFRA.Core.Entities;
using NEEFRA.Core.Interfaces.IService;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DTO;
using NEEFRA_API.Models;

namespace NEEFRA.Core.Services
{
    public class GovernorateService : IGovernorateService
    {
        private readonly IGovernorateRepository _govRepo;
        private readonly ILogger<GovernorateService> _logger;

        public GovernorateService(IGovernorateRepository govRepo, ILogger<GovernorateService> logger)
        {
            _govRepo = govRepo;
            _logger = logger;
        }

        // ════════════════════════════════════════════════════════════════════
        // Governorate Operations
        // ════════════════════════════════════════════════════════════════════

        public async Task<ServiceResult<List<GovernorateDTO>>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all governorates");

            var governorates = await _govRepo.GetAllAsync();

            var data = governorates.Select(g => MapToDTO(g)).ToList();

            _logger.LogInformation("Fetched {Count} governorates", data.Count);

            return new() { IsSuccess = true, Data = data };
        }

        public async Task<ServiceResult<GovernorateDTO>> AddAsync(CreateGovernorateDTO dto)
        {
            _logger.LogInformation("Adding new governorate – name: {Name}", dto.Name);

            if (dto == null)
            {
                _logger.LogWarning("Add governorate failed – dto is null");
                return new() { IsSuccess = false, Message = "Data is not accurate", ErrorType = "BadRequest" };
            }

            var governorate = new Governorate { Name = dto.Name };
            var created = await _govRepo.AddAsync(governorate);

            _logger.LogInformation("Governorate created – id: {Id}, name: {Name}", created.Id, created.Name);

            return new()
            {
                IsSuccess = true,
                Message = "Governorate added successfully",
                Data = MapToDTO(created)
            };
        }

        // ════════════════════════════════════════════════════════════════════
        // Private helpers
        // ════════════════════════════════════════════════════════════════════

        private GovernorateDTO MapToDTO(Governorate g) => new GovernorateDTO
        {
            Id = g.Id,
            Name = g.Name
        };
    }
}
