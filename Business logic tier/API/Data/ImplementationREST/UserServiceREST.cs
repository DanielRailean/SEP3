using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class UserServiceREST : IUserService
    {
        private string uri = "https://192.168.1.117:8080";
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
            HttpResponseMessage response =  await client.PostAsync(uri+"/RegisterUser", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            User userReceived = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            logged = userReceived;
            return userReceived;
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            HttpResponseMessage response =
                await client.GetAsync(uri +"/ValidateUser"+ $"?Email={@email}&Password={@password}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            logged = user;
            return user;
        }

        public async Task<User> UpdateUser(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> RemoveUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}