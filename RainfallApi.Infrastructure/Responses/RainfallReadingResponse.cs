using Newtonsoft.Json;

namespace RainfallApi.Infrastructure.Responses
{
    public class RainfallReadingResponse<T>
    {
        [JsonProperty("items")]
        public T Items { get; set; }
    }
}
