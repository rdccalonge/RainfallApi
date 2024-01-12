using AutoMapper;
using RainfallApi.Core.Entities;
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
        private readonly IMapper _mapper;

        public RainfallApiClient(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<RainfallReading>> GetRainfallReadingsAsync(string stationId, int count = 10)
        {
            throw new NotImplementedException();
        }
    }
}
