using Newtonsoft.Json;
using RainfallApi.Core.Error;
using RainfallApi.Infrastructure.Responses;

namespace RainfallApi.Infrastructure.Clients
{
    public class RainfallApiClient : IRainfallApiClient
    {
        private readonly HttpClient _httpClient;

        public RainfallApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ClientResponse<ResponseDTO<RainfallReadingDTO>>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            using var result = await _httpClient.GetAsync($"flood-monitoring/id/stations/{stationId}/readings?_limit={count}");

            if (!result.IsSuccessStatusCode)
            {
                return new ClientResponse<ResponseDTO<RainfallReadingDTO>> { IsSuccess = false, ErrorResponse = JsonConvert.DeserializeObject<Error>(await result.Content.ReadAsStringAsync()) };
            }

            return new ClientResponse<ResponseDTO<RainfallReadingDTO>>() { IsSuccess = true, SuccessResponse = JsonConvert.DeserializeObject<ResponseDTO<RainfallReadingDTO>>(await result.Content.ReadAsStringAsync()) };
        }
    }
}
