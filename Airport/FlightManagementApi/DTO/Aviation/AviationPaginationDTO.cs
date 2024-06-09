using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO.Aviation
{
    public class AviationPaginationDTO
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }
}
