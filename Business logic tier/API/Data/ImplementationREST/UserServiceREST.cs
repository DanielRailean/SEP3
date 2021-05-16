using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class UserServiceREST : IUserService
    {
        private string uri = "https://localhost:8080/User";
        private User logged;
        private HttpClient client;

        public UserServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                 return true;
            };
            client = new HttpClient(clientHandler);
        }
        public async Task<User> RegisterUser(User user)
        {
            string UserAsJson = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(
                UserAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response =  await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            User userReceived = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            logged = userReceived;
            return userReceived;
        }

        public Task<User> ValidateUser(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateUser(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> RemoveUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}