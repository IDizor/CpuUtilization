using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace CpuApi.Validation
{
    /// <summary>
    /// Provides validation functionality for methods parameters.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// The error templates.
        /// </summary>
        public static Dictionary<ErrorCode, ExceptionTypedTemplate> ErrorTemplates = new Dictionary<ErrorCode, ExceptionTypedTemplate>
        {
            { ErrorCode.Invalid, new ExceptionTypedTemplate(typeof(ArgumentException), "Invalid {0}.") },
            { ErrorCode.ShouldHaveValue, new ExceptionTypedTemplate(typeof(ArgumentNullException), "The {0} should have a value.") },
            { ErrorCode.OutOfRange, new ExceptionTypedTemplate(typeof(ArgumentOutOfRangeException), "The value of {0} is out of valid range.") },
        };

        /// <summary>
        /// Throws the appropriate exception when condition is failed.
        /// </summary>
        /// <param name="validCondition">If set to <c>true</c> do not throw an exception.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="parameters">The message template parameters.</param>
        [DebuggerStepThrough()]
        public static void Requires(bool validCondition, ErrorCode errorCode, params object[] parameters)
        {
            if (validCondition)
            {
                return;
            }

            bool hasTemplate = ErrorTemplates.ContainsKey(errorCode);
            Type exceptionType = hasTemplate ? ErrorTemplates[errorCode].ExceptionType : typeof(ArgumentException);
            string messageTemplate = hasTemplate ? ErrorTemplates[errorCode].MessageTemplate : String.Empty;
            string errorMessage = String.Format(messageTemplate, parameters);

            object exception = null;
            ConstructorInfo constructor = exceptionType.GetConstructor(new Type[] { typeof(string), typeof(string) });

            if (constructor != null)
            {
                if (constructor.GetParameters()[0].Name == "paramName")
                {
                    exception = constructor.Invoke(new object[] { String.Empty, errorMessage });
                }
                else
                {
                    exception = constructor.Invoke(new object[] { errorMessage, String.Empty });
                }
            }
            else
            {
                constructor = exceptionType.GetConstructor(new Type[] { typeof(string) });

                if (constructor != null)
                {
                    exception = constructor.Invoke(new object[] { errorMessage });
                }
            }

            if (exception == null)
            {
                throw new ArgumentException(errorMessage, String.Empty);
            }

            throw (Exception)exception;
        }
    }
}
