using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Order.API.Middlewares
{
    public class ExceptionHandelingMidlleware
    {
        
            private readonly ILogger<ExceptionHandelingMidlleware> _logger;
            private readonly RequestDelegate _next;

            public ExceptionHandelingMidlleware(ILogger<ExceptionHandelingMidlleware> logger, RequestDelegate next)
            {
                _logger = logger;
                _next = next;
            }
            public async Task InvokeAsync(HttpContext context)
            {
                try
                {

                    await _next(context);

                }
                catch (Exception err)
                {

                    await HandleExceptionAsync(context, err);

                }
            }

            private async Task HandleExceptionAsync(HttpContext context, Exception err)
            {
                string title = "Something went Wrong";
                string details = $"Path: {context.Request.Path}";
                int statusCode = 500;   
 

                _logger.LogError(err, err.Message);

                var problemDetails = new ProblemDetails()
                {
                    Status = statusCode,
                    Title = title,
                    Detail = details
                };

                context.Response.ContentType = "application/problem+json";

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
        
    }
}
