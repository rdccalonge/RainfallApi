using Newtonsoft.Json;
using RainfallApi.Core.Error;

namespace RainfallApi.Infrastructure.Responses
{
    public class ErrorResponse
    {
        public Error Error { get; set; }
    }
}
