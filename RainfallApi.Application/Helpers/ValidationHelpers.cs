using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Application.Helpers
{
    public static class ValidationHelpers
    {
        public static bool IsValidStationId(string stationIdString, out int stationId) => int.TryParse(stationIdString, out stationId);
    }
}
