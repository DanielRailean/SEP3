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
        
        JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        public UserService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            }; 
            client = new HttpClient(clientHandler);
        }
        
        /// <summary>
        /// Posts a new user to the REST endpoint,
        /// in JSON format.
        /// </summary>
        /// <param name="user">User to create.</param>
        public async Task RegisterUserAsync(User user)
        {
            var userAsJson = JsonSerializer.Serialize(user,serializeOptions);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri, content);
        }

        /// <summary>
        /// Consumes REST endpoint to get a user as in validate,
        /// by sending parameters in body to check if user exists
        /// by these, in JSON format.
        /// </summary>
        /// <param name="email">Email address to check.</param>
        /// <param name="password">Password to check.</param>
        /// <returns>User item.</returns>
        /// <exception cref="Exception">Response phrase.</exception>
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

        /// <summary>
        /// Sends a Put request to the REST endpoint,
        /// and validates if user is logged in and is eligible by
        /// asking for password.
        /// </summary>
        /// <param name="user">User to update.</param>
        /// <param name="password">Password to confirm.</param>
        /// <exception cref="Exception">Response phrase.</exception>
        public async Task UpdateUserAsync(User user,string password)
        {
            var userAsJson = JsonSerializer.Serialize(user,serializeOptions);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri + "?password=" + password ,content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        /// <summary>
        /// Sends a Delete request to the REST endpoint,
        /// to remove a specific user, in JSON format.
        /// </summary>
        /// <param name="user">User to remove.</param>
        public async Task RemoveUserAsync(User user)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonSerializer.Serialize(user,serializeOptions), Encoding.UTF8, "application/json")
            };
            await client.SendAsync(request);
        }
    }
}