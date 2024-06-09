using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlightManagementApi.DBContext
{
    public class WebserviceContext
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WebserviceContext(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> Get(string action, string parameters)
        {
            var response = await _httpClient.GetAsync(action + $"?{_config.GetConnectionString("ArrivalFlightsApiKey")}&{parameters}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
