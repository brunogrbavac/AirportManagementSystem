using DomainModel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace FlightManagementBlazorServer.Services
{
    public class PassengerService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:44334/api/Passenger";
        public PassengerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Passenger>> GetPassengersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Passenger>>(BaseApiUrl);
        }

        public async Task<Passenger> GetPassengerAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Passenger>($"{BaseApiUrl}/{id}");
        }

        public async Task UpdatePassengerAsync(Passenger passenger)
        {
            var httpPutRequest = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            httpPutRequest.Content = new StringContent(JsonSerializer.Serialize(passenger), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(httpPutRequest);
        }

        public async Task AddPassengerAsync(Passenger passenger)
        {
            var httpPostRequest = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            httpPostRequest.Content = new StringContent(JsonSerializer.Serialize(passenger), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(httpPostRequest);
        }

        public async Task DeletePassengerAsync(int passengerId)
        {
            var httpDeleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{passengerId}");
            await _httpClient.SendAsync(httpDeleteRequest);
        }
    }
}
