using System;

namespace CpuData.Interfaces
{
    /// <summary>
    /// Represents fields to track entity create/update information.
    /// </summary>
    public interface ITrackable
    {
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        DateTime Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}
