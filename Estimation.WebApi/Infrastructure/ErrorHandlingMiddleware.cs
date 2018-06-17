using Estimation.Services.Logger;
using Kaewsai.Utilities.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Estimation.WebApi.Infrastructure
{
    /// <summary>
    /// Error handling middleware.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MiniWing.WebApi.ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next.</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
            _logger = AppLogger.LoggerFactory.CreateLogger(nameof(ErrorHandlingMiddleware));
        }

        /// <summary>
        /// Invoke the specified context.
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="context">Context.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
                await HandleExpectedContext(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExpectedContext(HttpContext context)
        {
            if (context.Response.StatusCode == 401 || context.Response.StatusCode == 403)
            {
                var result = OutgoingResult<string>.FailResponse(null, "You are not authorized.");
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }

            return Task.CompletedTask;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = OutgoingResult<Exception>.ExceptionResponse(exception);
            if (exception is ArgumentOutOfRangeException) code = HttpStatusCode.BadRequest;
            else if (exception is NullReferenceException) code = HttpStatusCode.BadRequest;
            else if (exception is ArgumentException) code = HttpStatusCode.BadRequest;
            else if (exception is KeyNotFoundException) code = HttpStatusCode.BadRequest;


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
