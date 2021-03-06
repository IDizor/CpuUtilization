﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CpuData.Models
{
    /// <summary>
    /// Represents model for CpuStatus DB table.
    /// </summary>
    public class CpuStatus : TrackableSoftDeletable
    {
        /// <summary>
        /// Gets or sets the CPU status identifier.
        /// </summary>
        public int CpuStatusId { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the name of the PC where CPU status is monitored.
        /// </summary>
        [Required]
        public string PcName { get; set; }

        /// <summary>
        /// Gets or sets the CPU usage.
        /// </summary>
        public double Usage { get; set; }
    }
}
