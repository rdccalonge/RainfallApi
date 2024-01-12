using RainfallApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Core.Interfaces
{
    public interface IRainfallService
    {
        Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10);
    }
}
