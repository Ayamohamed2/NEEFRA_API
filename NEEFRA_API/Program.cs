using Microsoft.OpenApi.Models;
using NEEFRA.Domain.IReposatory;
using NEEFRA_API.DataAccess.Data;
using NEEFRA_API.Settings;
using SignalIR_practice.Hubs;
using System.Text.Json.Serialization;
using Villa_API_Project.Custom_Middleware;
using Villa_API_Project.DataAccess.Reposatory;
using NEEFRA.Core.Services;
using NEEFRA.Core;
using NEEFRA.Core.Interfaces.IService;
using Serilog;
using StackExchange.Redis;
using NEEFRA.Core.Services.EmailService;
using Restaurant.API.Custom_Middleware;
using Restaurant.Core.Interfaces.IService.Redis;
using Restaurant.Core.Services.Redis;
using NEEFRA.Core.DataSeed;
using NEEFRA.Infrastructure.DataSeed;
using NEEFRA_API.Data;
using AspNetCoreRateLimit;
using NEEFRA.API.Custom_Middleware;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using NEEFRA_API.DataAccess.Reposatory.IReposatory;
using NEEFRA_API.DataAccess.Reposatory;
namespace NEEFRA_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Serilog — Logging
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services);
            });
            #endregion

            #region Controllers & JSON
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            #endregion

            #region SignalR
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromSeconds(10);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                options.MaximumReceiveMessageSize = 102400;
                options.StreamBufferCapacity = 10;
            });
            #endregion

            #region MongoDB
            builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddSingleton<MongoDbContext>();
            #endregion

            #region Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter JWT token in the format: Bearer {your token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            #region Authentication & JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddCookie("ExternalCookies")
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };

                options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chat")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            }).AddGoogle("Google", options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                options.CallbackPath = "/signin-google";
                options.SignInScheme = "ExternalCookies";
            });

            builder.Services.AddAuthorization();
            #endregion

            #region CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .SetIsOriginAllowed(_ => true) // 🔥 يسمح لأي domain
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            #endregion

            #region Redis Cache
            var connectionString = builder.Configuration.GetConnectionString("Redis");
            try
            {
                var redis = ConnectionMultiplexer.Connect(connectionString);
                Console.WriteLine("✅ Connected to Redis successfully!");
                redis.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Redis connection failed: {ex.Message}");
            }

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "";
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connectionString);
            });
            #endregion
       
            #region Rate Limiting
            builder.Services.AddMemoryCache();

            builder.Services.Configure<IpRateLimitOptions>(
                builder.Configuration.GetSection("IpRateLimiting"));

            builder.Services.Configure<IpRateLimitPolicies>(
                builder.Configuration.GetSection("IpRateLimitPolicies"));

            // لو بتستخدم Redis (موجود في مشروعك) أحسن من MemoryCache في Production
            builder.Services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            builder.Services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            #endregion

            #region Health Checks
            builder.Services.AddHealthChecks()

             // ✅ MongoDB
             .AddCheck<MongoHealthCheck>(          // ✅ بدل AddMongoDb
        name: "MongoDB",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "mongo" })

                // ✅ Redis
                .AddRedis(
                    redisConnectionString: builder.Configuration.GetConnectionString("Redis"),
                    name: "Redis",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "cache", "redis" });

               

            // ✅ Health Check UI
            builder.Services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);      // بيعمل check كل 15 ثانية
                options.MaximumHistoryEntriesPerEndpoint(50); // بيحتفظ بآخر 50 نتيجة
                options.AddHealthCheckEndpoint("NEEFRA API", "/health");
            })
            .AddInMemoryStorage();
            #endregion

            #region Hangfire
            builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMongoStorage(
                    builder.Configuration["MongoDBSettings:ConnectionString"],
                    builder.Configuration["HangfireSettings:DatabaseName"],
                    new MongoStorageOptions
                    {
                        MigrationOptions = new MongoMigrationOptions
                        {
                            MigrationStrategy = new MigrateMongoMigrationStrategy(),
                        },
                        CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
                    }
                ));

            builder.Services.AddHangfireServer();
            #endregion


            #region Application Services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IJWT_TokenReposatory, JWT_TokenReposatory>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IInterestService, InterestService>();
            builder.Services.AddScoped<IAIFakeService, FakeAIService>();
            builder.Services.AddScoped<IAIService, AIService>();
            builder.Services.AddScoped<ISummaryService, SummaryService>();
            builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
            builder.Services.AddScoped<ISmartRouteService, SmartRouteService>();
            builder.Services.AddScoped<MongoHealthCheck>();
            builder.Services.AddScoped<ExpiredTokensCleanupService>();
            builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
            builder.Services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            builder.Services.AddScoped<IMuseumRepository, MuseumRepository>();
            builder.Services.AddScoped<IArtPieceRepository, ArtPieceRepository>();
            builder.Services.AddScoped<IVisitRepository, VisitRepository>();
            builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            builder.Services.AddScoped<INoteRepository, NoteRepository>();


            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddScoped<IMuseumService, MuseumService>();
            builder.Services.AddScoped<IVisitService, VisitService>();
            builder.Services.AddScoped<IArtPieceService, ArtPieceService>();
            builder.Services.AddScoped<IFavouriteService, FavouriteService>();
            builder.Services.AddScoped<IGovernorateService, GovernorateService>();

            builder.Services.AddScoped<IGovernoratePhotoRepository, GovernoratePhotoRepository>();
            builder.Services.AddScoped<IMuseumFacilitiesRepository, MuseumFacilitiesRepository>();
            builder.Services.AddScoped<INearbyHotelRepository, NearbyHotelRepository>();
            builder.Services.AddScoped<INearbyRestaurantRepository, NearbyRestaurantRepository>();
            builder.Services.AddScoped<IGiftShopRepository, GiftShopRepository>();
            builder.Services.AddScoped<ICafeRepository, CafeRepository>();

            // Services
            builder.Services.AddScoped<IGovernoratePhotoService, GovernoratePhotoService>();
            builder.Services.AddScoped<IMuseumFacilitiesService, MuseumFacilitiesService>();
            builder.Services.AddScoped<INearbyHotelService, NearbyHotelService>();
            builder.Services.AddScoped<INearbyRestaurantService, NearbyRestaurantService>();
            builder.Services.AddScoped<IGiftShopService, GiftShopService>();
            builder.Services.AddScoped<ICafeService, CafeService>();





            #endregion


            builder.Services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.ResultsAuthorize = request => true; // 🔥 مهم
            });
            #region HTTP Clients
            builder.Services.AddHttpClient<GradioService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            #endregion

            // ─────────────────────────────────────────────
            //  Build & Configure Middleware Pipeline
            // ─────────────────────────────────────────────
            var app = builder.Build();

            #region Data Seeding
            var mongoContext = app.Services.GetRequiredService<MongoDbContext>();
            InterestSeeder.Seed(mongoContext.Database);
            ArtPieceSeeder.Seed(mongoContext.Database);
            ArtifactSeeder.Seed(mongoContext.Database);
            GovernorateSeeder.Seed(mongoContext.Database);

          //  MuseumExtrasSeeder.Seed(mongoContext.Database);
            #endregion

            #region Middleware Pipeline
            app.UseIpRateLimiting();
            app.UseMiddleware<ConcurrentRequestsMiddleware>(); // بعد IpRateLimiting
            app.UseMiddleware<RequestTimeLoggingMiddleware>();
            app.UseHangfireDashboard("/hangfire"
            );
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExeptionHandlingMiddleware>();
            app.UseStaticFiles();
             app.UseMiniProfiler();
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseMiddleware<BlackListTokensMiddleware>();
            app.UseAuthorization();
     
            #endregion
            // في Program.cs بعد app.Build()


            #region Hangfire Recurring Jobs
            var recurringJobs = app.Services.GetRequiredService<IRecurringJobManager>();

      
            recurringJobs.AddOrUpdate<ExpiredTokensCleanupService>(
                recurringJobId: "cleanup-expired-tokens",
                methodCall: service => service.CleanupAsync(),
                cronExpression: Cron.Hourly);

      
            #endregion

            #region Endpoints
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes =
    {
        [HealthStatus.Healthy]   = 200,
        [HealthStatus.Degraded]  = 200,
        [HealthStatus.Unhealthy] = 503
    }
            });

            // ✅ Endpoint بسيط للـ Load Balancer (بس Healthy/Unhealthy)
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false // بيشيل كل الـ checks
            });

            // ✅ Dashboard UI
            app.MapHealthChecksUI(options =>
            {
                options.UIPath = "/health-ui"; // افتحه في البراوزر
            });

            app.MapHub<ChatHub>("/chat");
         
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}