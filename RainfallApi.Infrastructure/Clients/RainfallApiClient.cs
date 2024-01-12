using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RainfallApi.Core.Entities;
using RainfallApi.Core.Error;
using RainfallApi.Infrastructure.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Infrastructure.Clients
{
    public class RainfallApiClient
    {
        private readonly HttpClient _httpClient;

        public RainfallApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ClientResponse<ResponseDTO>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            using var result = await _httpClient.GetAsync($"flood-monitoring/id/stations/{stationId}?count={count}");

            if (!result.IsSuccessStatusCode)
            {
                return new ClientResponse<ResponseDTO> { IsSuccess = false, ErrorResponse = JsonConvert.DeserializeObject<Error>(await result.Content.ReadAsStringAsync()) };
            }

            return new ClientResponse<ResponseDTO>() { IsSuccess = true, SuccessResponse = JsonConvert.DeserializeObject<ResponseDTO>(await result.Content.ReadAsStringAsync()) };
        }
    }
}
