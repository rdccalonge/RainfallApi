using Newtonsoft.Json;
using RainfallApi.Core.Error;

namespace RainfallApi.Infrastructure.Responses
{
    public class ErrorDTO
    {
        public Error Error { get; set; }
    }
}
