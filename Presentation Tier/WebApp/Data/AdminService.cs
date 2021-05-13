using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class AdminService : IAdminService
    {
        private const string uri = "https://localhost:5001/admin";
        private readonly HttpClient client;
        
        public async Task<Administrator> ValidateAdminAsync(string email, string password)
        {
            HttpResponseMessage response = await client.GetAsync(uri + $"?email={@email}&password={@password}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            var admin = JsonSerializer.Deserialize<Administrator>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            return admin;
        }
    }
}