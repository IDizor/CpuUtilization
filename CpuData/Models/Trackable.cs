using CpuData.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CpuData.Models
{
    /// <summary>
    /// Represents fields to track entity create/update information.
    /// </summary>
    public abstract class Trackable : ITrackable
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the created by name.
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the modified by name.
        /// </summary>
        [Required]
        public string ModifiedBy { get; set; }
    }
}
