using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class RecipeServiceREST : IRecipeService
    {
        private string uri = "https://localhost:8080/Recipe";
        private HttpClient client;

        public RecipeServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }
        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            string recipeAsJson = JsonSerializer.Serialize(recipe);
            HttpContent content = new StringContent(
                recipeAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Recipe gotRecipes = JsonSerializer.Deserialize<Recipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotRecipes;
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(uri + $"?id={@id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {responseMessage.ReasonPhrase}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();
            Recipe gotRecipe = JsonSerializer.Deserialize<Recipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotRecipe;
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            string recipeAsJson = JsonSerializer.Serialize(recipe);
            Console.WriteLine(recipeAsJson);
            HttpContent content = new StringContent(
                recipeAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Recipe updatedRecipe = JsonSerializer.Deserialize<Recipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return updatedRecipe;
        }

        public async Task<Recipe> RemoveRecipe(Recipe recipe)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(uri + $"?order={@recipe}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Recipe removedRecipe = JsonSerializer.Deserialize<Recipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return removedRecipe;
        }
        
        public async Task<IList<Recipe>> GetAllRecipes()
        {
            throw new System.NotImplementedException();
        }
    }
}