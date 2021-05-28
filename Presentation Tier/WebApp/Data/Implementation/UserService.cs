using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Implementation
{
    public class UserService : IUserService
    {
        private const string uri = "http://localhost:5000/user";
        private readonly HttpClient client;
        private User currentUser;
        public UserService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            }; 
            client = new HttpClient(clientHandler);
        }
        
        public async Task RegisterUserAsync(User user)
        {
            var userAsJson = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri, content);
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            HttpResponseMessage response = await client.GetAsync(uri + $"?email={@email}&password={@password}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });
            return user;
        }

        public async Task UpdateUserAsync(User user,string password)
        {
            var userAsJson = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri + "?password=" + password ,content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task RemoveUserAsync(User user)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json")
            };
            await client.SendAsync(request);
        }
        

        public User GetCurrentUser()
        {
            return currentUser;
        }
    }
}