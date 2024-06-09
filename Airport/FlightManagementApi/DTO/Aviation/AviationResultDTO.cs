using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManagementApi.DTO.Aviation
{
    public class AviationResultDTO
    {
        public AviationPaginationDTO pagination { get; set; }
        public IEnumerable<AviationFlightDataDTO> data { get; set; }
    }
}
