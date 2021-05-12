using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using WebApp.Models;

namespace WebApp.Data
{
    public class UserService : IUserService
    {
        private const string uri = "https://localhost:5001/User";
        private readonly HttpClient client;
        
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

        public Task<User> ValidateUserAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUserAsync(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}