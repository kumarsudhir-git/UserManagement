namespace UserManagementServices.Helpers
{
    public class GlobalExceptionHandle
    {
        private readonly ILogger<GlobalExceptionHandle> _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionHandle(RequestDelegate next, ILogger<GlobalExceptionHandle> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
