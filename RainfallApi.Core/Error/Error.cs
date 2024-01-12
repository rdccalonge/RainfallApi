namespace RainfallApi.Core.Error
{
    public class Error
    {
        public string Message { get; set; }
        public List<ErrorDetail> Details { get; set; }
    }

    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
