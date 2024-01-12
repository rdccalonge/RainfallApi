using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Infrastructure.Responses
{
    public class ClientResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T SuccessResponse { get; set; }

        public dynamic ErrorResponse { get; set; }
    }
}
