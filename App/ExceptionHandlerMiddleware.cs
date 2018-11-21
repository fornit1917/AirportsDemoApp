using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportsDemo.App
{
    public class ExceptionHandlerMiddleware
    {
        private RequestDelegate next;
        private ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger) {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            try {
                await next(httpContext);
            } catch (Exception ex) {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) {
            logger.LogError(exception, "Unhandled exception");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            string content = JsonConvert.SerializeObject(new { Message = "Internal server error" });
            return context.Response.WriteAsync(content);
        }
    }
}
