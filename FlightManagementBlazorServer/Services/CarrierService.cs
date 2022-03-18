using DomainModel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Services
{
    public class CarrierService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:44334/api/Carrier";
        public CarrierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Carrier>> GetCarriersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Carrier>>(BaseApiUrl);
        }

        public async Task<Carrier> GetCarrierAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Carrier>($"{BaseApiUrl}/{id}");
        }

        public async Task UpdateCarrierAsync(Carrier carrier)
        {
            var httpPutRequest = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            httpPutRequest.Content = new StringContent(JsonSerializer.Serialize(carrier), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(httpPutRequest);
        }

        public async Task AddCarrierAsync(Carrier carrier)
        {
            var httpPostRequest = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            httpPostRequest.Content = new StringContent(JsonSerializer.Serialize(carrier), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(httpPostRequest);
        }

        public async Task DeleteCarrierAsync(int carrierId)
        {
            var httpDeleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{carrierId}");
            await _httpClient.SendAsync(httpDeleteRequest);
        }
    }
}
