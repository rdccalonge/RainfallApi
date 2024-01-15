namespace RainfallApi.Core.Error
{
    /// <summary>
    /// Error response
    /// </summary>
    public class Error
    {
        public string Message { get; set; }
        public ErrorDetail Details { get; set; }
    }

    /// <summary>
    /// Details of invalid request property
    /// </summary>
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
