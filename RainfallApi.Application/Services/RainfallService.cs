using RainfallApi.Core.Entities;
using RainfallApi.Core.Interfaces;
using RainfallApi.Infrastructure.Clients;

namespace RainfallApi.Application.Services
{
    public class RainfallService : IRainfallService
    {
        private readonly IRainfallApiClient _rainfallApiClient;

        public RainfallService(IRainfallApiClient rainfallApiClient)
        {
            _rainfallApiClient = rainfallApiClient ?? throw new ArgumentNullException(nameof(rainfallApiClient));
        }

        public async Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            try
            {
                var apiReadings = await _rainfallApiClient.GetRainfallReadingsAsync(stationId, count);
                throw new NotImplementedException();
            }
            catch (HttpRequestException)
            {
                throw new ApplicationException("Error connecting to the Rainfall API.");
            }
        }
    }
}
