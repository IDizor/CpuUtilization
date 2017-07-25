namespace CpuApi.Validation
{
    /// <summary>
    /// Validation error codes.
    /// </summary>
    public enum ErrorCode : int
    {
        /// <summary>
        /// Invalid {0}.
        /// </summary>
        Invalid = 1,

        /// <summary>
        /// The {0} should have a value.
        /// </summary>
        ShouldHaveValue = 2,

        /// <summary>
        /// The value of {0} is out of valid range.
        /// </summary>
        OutOfRange = 3,
    }
}