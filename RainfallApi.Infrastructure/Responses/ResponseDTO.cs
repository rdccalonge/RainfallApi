using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Infrastructure.Responses
{
    public class ResponseDTO<T>
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }
    }
}
