using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddlewares
    {
        private readonly ILogger<ExceptionHandlerMiddlewares> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddlewares(ILogger<ExceptionHandlerMiddlewares> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log the exception
                logger.LogError(ex, $"{errorId}: {ex.Message}");

                // return error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    Message = "An unexpected error occurred. Please try again later."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
