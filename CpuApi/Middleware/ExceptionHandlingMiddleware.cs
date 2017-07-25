using CpuApi.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpuApi.Middleware
{
    /// <summary>
    /// The exception handling middleware implementation.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the specified context processing.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context/*, ILogger logger*/)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //if (logger != null)
                //{
                //    // log exception to the database
                //    logger.LogError(new EventId(0), ex, DateTime.UtcNow.ToString());
                //}

                await HandleException(context, ex);
            }
        }

        #region Private_Methods
        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is ArgumentException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (exception is NotImplementedException)
            {
                context.Response.StatusCode = StatusCodes.Status501NotImplemented;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            var jsonResult = ControllerExtensions.ConstructJsonError(CollectMessages(exception));
            var response = JsonConvert.SerializeObject(jsonResult.Value, jsonResult.SerializerSettings);

            return context.Response.WriteAsync(response);
        }

        /// <summary>
        /// Collects the messages from exceptions chain.
        /// </summary>
        /// <param name="exception">The initial exception.</param>
        /// <returns></returns>
        private static string CollectMessages(Exception exception)
        {
            var messages = String.Join(" / ", GetExceptionsChain(exception).Select(ex => ex.Message));

            return messages;
        }

        /// <summary>
        /// Gets the exceptions chain.
        /// </summary>
        /// <param name="exception">The top exception.</param>
        /// <returns></returns>
        private static IEnumerable<Exception> GetExceptionsChain(Exception exception)
        {
            while (exception != null)
            {
                yield return exception;
                exception = exception.InnerException;
            }
        }
        #endregion
    }
}
