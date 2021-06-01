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
        private string uri = "http://localhost:8080";
        private HttpClient client;

        public UserServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            client = new HttpClient(clientHandler);
        }
        public async Task<User> RegisterUser(User user)
        {
            user.SecurityLevel = 1;
            string UserAsJson = JsonSerializer.Serialize(user);
            Console.WriteLine(UserAsJson);
            HttpContent content = new StringContent(
                UserAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response =  await client.PostAsync(uri+"/RegisterUser", content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }
            string result = await response.Content.ReadAsStringAsync();
            User userReceived = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions{ PropertyNamingPolicy
                = JsonNamingPolicy.CamelCase});
            return userReceived;
        }

        public async Task<User> ValidateUser(string email, string password)
        {
            HttpResponseMessage response =
                await client.GetAsync(uri +"/ValidateUser"+$"?Email={@email}&Password={@password}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }
            string result = await response.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return user;
        }

        public async Task<User> RemoveUser(User user)
        {
            User valid = await ValidateUser(user.Email,user.Password);
            if (valid == null)
            {
                throw new Exception($@"Error: Password or email incorrect");
            }
            HttpResponseMessage response =
                await client.DeleteAsync(uri + $"/RemoveUser?email={user.Email}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            User removedUser = JsonSerializer.Deserialize<User>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return removedUser;
        }
        
        public async Task<User> UpdateUser(User user, string password)
        {
            User valid = await ValidateUser(user.Email,password);
            if (valid == null)
            {
                throw new Exception($@"Error: Password or email incorrect");
            }

            string UserAsJson = JsonSerializer.Serialize(user);
            Console.WriteLine(UserAsJson);
            HttpContent content = new StringContent(
                UserAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response =
                await client.PutAsync(uri + $"/UpdateUser",content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            User updateUser = JsonSerializer.Deserialize<User>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return updateUser;
        }
        
    }
}