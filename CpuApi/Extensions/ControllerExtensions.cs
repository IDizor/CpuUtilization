using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Diagnostics;

namespace CpuApi.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Microsoft.AspNetCore.Mvc.Controller"/> class.
    /// </summary>
    [DebuggerStepThrough()]
    public static class ControllerExtensions
    {
        /// <summary>
        /// Creates response object for success cases.
        /// </summary>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="response">The response object.</param>
        /// <param name="totalCount">The total count of items when response object is a collection.</param>
        /// <returns></returns>
        public static IActionResult JsonOk(this Controller controller, object response = null, long? totalCount = null)
        {
            if (response != null && !totalCount.HasValue && response is ICollection)
            {
                var collection = response as ICollection;
                totalCount = collection.Count;
            }

            var serializerSettings = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            if (totalCount.HasValue)
            {
                return new JsonResult(new
                {
                    Data = response,
                    TotalCount = totalCount.Value,
                    Error = (object)null
                }, serializerSettings);
            }
            else
            {
                return new JsonResult(new
                {
                    Data = response,
                    Error = (object)null
                }, serializerSettings);
            }
        }

        /// <summary>
        /// Creates response object for error cases.
        /// </summary>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public static IActionResult JsonError(this Controller controller, string errorMessage)
        {
            return ConstructJsonError(errorMessage);
        }

        #region Helper_Methods
        /// <summary>
        /// Constructs the JSON action result with error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns></returns>
        public static JsonResult ConstructJsonError(string errorMessage)
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            return new JsonResult(new
            {
                Data = (object)null,
                Error = errorMessage
            }, serializerSettings);
        }
        #endregion
    }
}
