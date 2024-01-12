using RainfallApi.Core.Entities;

namespace RainfallApi.Core.Interfaces
{
    public interface IRainfallService
    {
        Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10);
    }
}
