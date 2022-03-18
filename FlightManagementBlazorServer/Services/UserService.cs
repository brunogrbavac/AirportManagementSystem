using DomainModel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace FlightManagementBlazorServer.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:44334/api/User";
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>(BaseApiUrl);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"{BaseApiUrl}/{id}");
        }

        public async Task UpdateUserAsync(User user)
        {
            var httpPutRequest = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            httpPutRequest.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(httpPutRequest);
        }

        public async Task AddUserAsync(User user)
        {
            var httpPostRequest = new HttpRequestMessage(HttpMethod.Post, BaseApiUrl);
            httpPostRequest.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(httpPostRequest);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var httpDeleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{userId}");
            await _httpClient.SendAsync(httpDeleteRequest);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await _httpClient.GetFromJsonAsync<User>($"{BaseApiUrl}/{username}/{password}");
        }
    }
}
