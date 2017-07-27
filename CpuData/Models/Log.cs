using System;

namespace CpuData.Models
{
    /// <summary>
    /// Represents model for Log DB table.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Gets or sets the log identifier.
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the log severity.
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the process identifier.
        /// </summary>
        public int? ProcessId { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the name of the process.
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the log message.
        /// </summary>
        public string Message { get; set; }
    }
}
