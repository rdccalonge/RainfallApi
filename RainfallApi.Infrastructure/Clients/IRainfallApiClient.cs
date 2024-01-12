using RainfallApi.Infrastructure.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Infrastructure.Clients
{
    public interface IRainfallApiClient
    {
        Task<ClientDTO<ResponseDTO<RainfallReadingDTO>>> GetRainfallReadingsAsync(int stationId, int count = 10);
    }
}
