using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using NEEFRA_API.DataAccess.Data;

namespace NEEFRA.API.Custom_Middleware
{
    // MongoHealthCheck.cs
    public class MongoHealthCheck : IHealthCheck
    {
        private readonly MongoDbContext _context;

        public MongoHealthCheck(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Database.RunCommandAsync(
                    (Command<BsonDocument>)"{ping:1}",
                    cancellationToken: cancellationToken);

                return HealthCheckResult.Healthy("MongoDB is connected ✅");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    "MongoDB connection failed ❌",
                    exception: ex);
            }
        }
    }
}
