using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class UserService : IUserService
    {
        private const string uri = "https://localhost:5001/user";
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
            HttpResponseMessage response = await client.GetAsync(uri + $"/getuser?email={@email}&password={@password}");
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

        public async Task UpdateUserAsync(User user)
        {
            var userAsJson = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri + "?password=" + user.Password, content);
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

        public async Task<IList<User>> GetAllUsersAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri + "/getallusers");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            string result = await response.Content.ReadAsStringAsync();
            List<User> users = JsonSerializer.Deserialize<List<User>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return users;
        }

        public User GetCurrentUser()
        {
            return currentUser;
        }
    }
}