using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;

namespace ShopLite.Middlewares
{
    //this intercepts all exceptions thrown in the application
    //and handles them globally, logging the error and returning a standardized response
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e, "Resource not found");
                var response = context.Response;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json"; //set the content type to JSON

                var responseBody = new
                {
                    ErrorCode = 404,
                    ErrorMessage = "Resource not found",
                    Details = e.Message
                };

                var result = JsonSerializer.Serialize(responseBody);
                await response.WriteAsync(result); //write the response to the client
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception");
                var response = context.Response;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json"; //set the content type to JSON

                var responseBody = new
                {
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred. Please try again later.",
                    Details = e.Message
                };

                var result = JsonSerializer.Serialize(responseBody);
                await response.WriteAsync(result); //write the response to the client
            }
        }
    }
}