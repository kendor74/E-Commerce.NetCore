using System.Net;
using System.Text.Json;

namespace E_Commerce.MiddleWare
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Pass the request to the next middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new
            {
                StatusCode = 500,
                Message = "An unexpected error occurred. Please try again later.",
                DetailedError = exception.Message,
                StackTrace = exception.StackTrace // Include for debugging
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
