using System;

namespace CpuApi.Validation
{
    /// <summary>
    /// Represents exception type and related error message template.
    /// </summary
    public struct ExceptionTypedTemplate
    {
        /// <summary>
        /// The exception type.
        /// </summary>
        public Type ExceptionType;

        /// <summary>
        /// The error message template.
        /// </summary>
        public string MessageTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionTypedTemplate"/> struct.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="messageTemplate">The error message template.</param>
        public ExceptionTypedTemplate(Type exceptionType, string messageTemplate)
        {
            ExceptionType = exceptionType;
            MessageTemplate = messageTemplate;
        }
    }
}
