using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class IngredientServiceREST : IIngredientService
    {
        private string uri = "http://localhost:8080";
        private HttpClient client;
        JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        public IngredientServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }
        public async Task<Ingredient> AddIngredient(Ingredient ingredient)
        {
            
            string ingredientAsJson = JsonSerializer.Serialize(ingredient, serializeOptions);
            HttpContent content = new StringContent(
                ingredientAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri+"/AddIngredient", content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Ingredient gotIngredients = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotIngredients;
        }

        public async Task<Ingredient> GetIngredient(int id)
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(uri + $"/GetIngredient?id={@id}");
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            Ingredient gotIngredient = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotIngredient;
        }

        public async Task<Ingredient> UpdateIngredient(Ingredient ingredient)
        {
        string ingredientAsJson = JsonSerializer.Serialize(ingredient, serializeOptions);
                    HttpContent content = new StringContent(
                        ingredientAsJson,
                        Encoding.UTF8,
                        "application/json");
            HttpResponseMessage httpResponseMessage = await client.PutAsync(uri + $"/UpdateIngredient",content);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            Ingredient gotIngredient = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotIngredient;
        }

        public async Task<Ingredient> RemoveIngredient(Ingredient ingredient)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(uri + $"/RemoveIngredient?id={ingredient.Id}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Ingredient removedIngredient = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return removedIngredient;

        }

        public async Task<IList<Ingredient>> GetAllIngredients()
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(uri + $"/GetAllIngredients");
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await httpResponseMessage.Content.ReadAsStringAsync();
            List<Ingredient> gotIngredient = JsonSerializer.Deserialize<List<Ingredient>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotIngredient;
        }
    }
}