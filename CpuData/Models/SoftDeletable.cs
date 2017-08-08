using CpuData.Interfaces;

namespace CpuData.Models
{
    /// <summary>
    /// Represents fields for soft deletable entities.
    /// </summary>
    public abstract class SoftDeletable : ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
