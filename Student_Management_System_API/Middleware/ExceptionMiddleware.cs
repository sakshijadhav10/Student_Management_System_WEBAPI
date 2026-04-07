using System.Net;

namespace Student_Management_System_API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await ctx.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
