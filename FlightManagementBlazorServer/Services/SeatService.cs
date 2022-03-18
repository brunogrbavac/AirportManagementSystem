using DomainModel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Services
{
    public class SeatService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:44334/api/Seat";
        public SeatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Seat>> GetSeats()
        {
            return await _httpClient.GetFromJsonAsync<List<Seat>>(BaseApiUrl);
        }

        public async Task<List<Seat>> GetFlightSeats(int flightId)
        {
            return await _httpClient.GetFromJsonAsync<List<Seat>>($"{BaseApiUrl}/flightSeats/{flightId}");
        }

        public async Task AddSeatAsync(Seat seat)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(seat),
                Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task<Seat> GetSeatAsync(int seatId)
        {
            return await _httpClient.GetFromJsonAsync<Seat>($"{BaseApiUrl}/{seatId}");
        }

        public async Task UpdateSeat(Seat seat)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(seat),
                Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }

        public async Task DeleteSeat(int seatId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{seatId}");
            await _httpClient.SendAsync(httpRequest);
        }

        public async Task<List<Seat>> GetCheckedSeats(int flightId)
        {
            return await _httpClient.GetFromJsonAsync<List<Seat>>($"{BaseApiUrl}/checkedSeats/{flightId}");
        }

        public async Task CheckSeat(int seatId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{BaseApiUrl}/checkSeat/{seatId}");
            await _httpClient.SendAsync(httpRequest);
        }
        public async Task<bool> UniqueSeat(string number, int flightId)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"{BaseApiUrl}/uniqueSeat/{number}/{flightId}");
        }
    }
}
