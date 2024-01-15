using RainfallApi.Core.Error;

namespace RainfallApi.Web.Responses
{
    /// <summary>
    /// An error object returned for failed requests
    /// </summary>
    public class ErrorResponse
    {
        public Error Error { get; set; }
    }
}
