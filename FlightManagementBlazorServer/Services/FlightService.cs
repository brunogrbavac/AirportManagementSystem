using DomainModel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Services
{
    public class FlightService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl= "https://localhost:44334/api/Flight";
        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Flight>> GetFlights()
        {
            return await _httpClient.GetFromJsonAsync<List<Flight>>(BaseApiUrl);
        }

        public async Task AddFlightAsync(Flight flight)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(flight),
                Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task<Flight> GetFlightAsync(int flightId)
        {
            return await _httpClient.GetFromJsonAsync<Flight>($"{BaseApiUrl}/{flightId}");
        }

        public async Task UpdateFlight(Flight flight)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(flight),
                Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task DeleteFlight(int flightId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{flightId}");
            await _httpClient.SendAsync(httpRequest);
        }

        public async Task<List<Flight>> GetArchivedFlights()
        {
            return await _httpClient.GetFromJsonAsync<List<Flight>>($"{BaseApiUrl}/archivedFlights");
        }

        public async Task ArchiveFlight(int flightId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{BaseApiUrl}/archiveFlight/{flightId}");
            await _httpClient.SendAsync(httpRequest);
        }
    }
}
