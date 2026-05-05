using Microsoft.Extensions.Logging;
using NEEFRA.Domain.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEEFRA.Core.Services
{
    // ExpiredTokensCleanupService.cs
    public class ExpiredTokensCleanupService
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger<ExpiredTokensCleanupService> _logger;

        public ExpiredTokensCleanupService(
            IUnitOfWork unit,
            ILogger<ExpiredTokensCleanupService> logger)
        {
            _unit = unit;
            _logger = logger;
        }

        // ✅ Public عشان Hangfire يقدر يستدعيها
        public async Task CleanupAsync()
        {
            var now = DateTime.UtcNow;

            var expiredEmail = await _unit.EmailTokens
                .GetAllAsync(t => t.ExpireAt < now);
            foreach (var t in expiredEmail)
                await _unit.EmailTokens.DeleteAsync(t.Id);

            var expiredPassword = await _unit.PasswordResetTokens
                .GetAllAsync(t => t.ExpireAt < now);
            foreach (var t in expiredPassword)
                await _unit.PasswordResetTokens.DeleteAsync(t.Id);

            _logger.LogInformation(
                "Cleanup done — Email:{E}, Password:{P}",
                expiredEmail.Count, expiredPassword.Count);
        }
    }



}
