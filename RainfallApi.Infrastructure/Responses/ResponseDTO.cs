using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Infrastructure.Responses
{
    public class ResponseDTO
    {
        [JsonProperty("items")]
        public string Items { get; set; }
    }
}
