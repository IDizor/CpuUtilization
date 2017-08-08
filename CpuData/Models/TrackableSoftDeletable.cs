using CpuData.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CpuData.Models
{
    /// <summary>
    /// Represents fields for soft deletable entities to track create/update information.
    /// </summary>
    public abstract class TrackableSoftDeletable : ITrackable, ISoftDeletable
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
