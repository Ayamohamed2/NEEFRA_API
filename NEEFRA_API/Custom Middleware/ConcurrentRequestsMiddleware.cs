namespace NEEFRA.API.Custom_Middleware
{
    // ConcurrentRequestsMiddleware.cs
    public class ConcurrentRequestsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _semaphore;
        private readonly ILogger<ConcurrentRequestsMiddleware> _logger;

        // الـ maxConcurrentRequests بتحدد أقصى عدد Requests في نفس الوقت
        public ConcurrentRequestsMiddleware(
            RequestDelegate next,
            ILogger<ConcurrentRequestsMiddleware> logger,
            int maxConcurrentRequests = 50)
        {
            _next = next;
            _logger = logger;
            _semaphore = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // لو مفيش slot متاح، رجّع 429 على طول
            if (!await _semaphore.WaitAsync(TimeSpan.Zero))
            {
                _logger.LogWarning("Concurrent request limit reached for {Path}",
                    context.Request.Path);

                context.Response.StatusCode = 429;
                context.Response.Headers["Retry-After"] = "5";
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Too many concurrent requests. Please try again shortly.",
                    retryAfterSeconds = 5
                });
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                // مهم جداً: دايماً Release حتى لو حصل Exception
                _semaphore.Release();
            }
        }
    }
}
