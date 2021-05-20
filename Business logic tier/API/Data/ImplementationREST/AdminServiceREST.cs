using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class AdminServiceREST : IAdminService
    {
        private string uri = "https://localhost:8080/Administrator";
        private HttpClient client;
        private Administrator loggedAdministrator;

        public AdminServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }
        public async Task<Administrator> ValidateAdministrator(string email, string password)
        {
            HttpResponseMessage response =
                await client.GetAsync(uri +"/ValidateUser"+$"?Email={@email}&Password={@password}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            Administrator administrator = JsonSerializer.Deserialize<Administrator>(result, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            loggedAdministrator = administrator;
            return administrator;
        }
    }
}