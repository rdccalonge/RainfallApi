using RainfallApi.Core.Entities;
using RainfallApi.Core.Interfaces;

namespace RainfallApi.Application.Services
{
    public class RainfallService : IRainfallService
    {
        public async Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            var dummyData = new List<RainfallReading>
            {
                new RainfallReading { DateMeasured = DateTime.Now, AmountMeasured = 10.5M },
            };

            return dummyData;
        }
    }
}
