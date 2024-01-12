using AutoMapper;
using RainfallApi.Core.Entities;
using RainfallApi.Core.Interfaces;
using RainfallApi.Infrastructure.Clients;
using System.Collections.Generic;

namespace RainfallApi.Application.Services
{
    public class RainfallService : IRainfallService
    {
        private readonly IRainfallApiClient _rainfallApiClient;

        public RainfallService(IRainfallApiClient rainfallApiClient)
        {
            _rainfallApiClient = rainfallApiClient ?? throw new ArgumentNullException(nameof(rainfallApiClient));
        }

        public async Task<List<RainfallReading>> GetRainfallReadingsAsync(int stationId, int count = 10)
        {
            List<RainfallReading> rainfallReadings = new List<RainfallReading>();
            try
            {
                var result = await _rainfallApiClient.GetRainfallReadingsAsync(stationId, count);
                if (result.IsSuccess)
                {
                    rainfallReadings = result.SuccessResponse.Items.Select(x => 
                    new RainfallReading
                    {
                        AmountMeasured = x.Value,
                        DateMeasured = x.DateTime
                    })
                    .ToList();
                }
                return rainfallReadings;
            }
            catch (HttpRequestException)
            {
                throw new ApplicationException("Error connecting to the Rainfall API.");
            }
        }
    }
}
