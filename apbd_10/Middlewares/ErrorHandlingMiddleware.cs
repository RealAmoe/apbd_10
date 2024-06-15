using apbd_10.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace apbd_10.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is InvalidDataException || exception is SecurityTokenException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is DoesntExistException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (exception is UnauthorisedException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(exception.Message);
        }
    }
}
