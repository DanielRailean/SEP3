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
        private string uri = "https://localhost:8080/Ingredient";
        private HttpClient client;

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
            string ingredientAsJson = JsonSerializer.Serialize(ingredient);
            HttpContent content = new StringContent(
                ingredientAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Ingredient gotIngredients = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotIngredients;
        }

        public async Task<Ingredient> GetIngredient(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(uri + $"?id={@id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {responseMessage.ReasonPhrase}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();
            Ingredient gotIngredient = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotIngredient;
        }

        public async Task<Ingredient> UpdateIngredient(Ingredient ingredient)
        {
            string ingredientAsJson = JsonSerializer.Serialize(ingredient);
            Console.WriteLine(ingredientAsJson);
            HttpContent content = new StringContent(
                ingredientAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }
            string result = await response.Content.ReadAsStringAsync();
            Ingredient updatedIngredient = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return updatedIngredient;
        }

        public async Task<Ingredient> RemoveIngredient(Ingredient ingredient)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(uri + $"?ingredient={@ingredient}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Ingredient removedIngredient = JsonSerializer.Deserialize<Ingredient>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return removedIngredient;

        }

        public async Task<IList<Ingredient>> GetAllIngredients()
        {
            throw new System.NotImplementedException();
        }
    }
}