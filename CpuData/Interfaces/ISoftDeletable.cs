namespace CpuData.Interfaces
{
    /// <summary>
    /// Represents fields for soft deletable entities.
    /// </summary>
    public interface ISoftDeletable
    {
        bool IsActive { get; set; }
    }
}
