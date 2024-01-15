using RainfallApi.Core.Entities;
using System.ComponentModel;

namespace RainfallApi.Web.Responses
{
    /// <summary>
    /// Get rainfall readings response
    /// </summary>
    public class RainfallReadingResponse
    {
        public List<RainfallReading> Readings{ get; set; }
    }
}
