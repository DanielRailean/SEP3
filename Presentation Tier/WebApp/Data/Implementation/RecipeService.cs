using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class RecipeService : IRecipeService
    {
        private const string uri = "https://localhost:5001/recipe";
        private readonly HttpClient client;

        public async Task<IList<Recipe>> GetAllRecipesAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri + "/getallrecipes");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            string result = await response.Content.ReadAsStringAsync();
            List<Recipe> recipes = JsonSerializer.Deserialize<List<Recipe>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return recipes;
        }

        public async Task<Recipe> GetRecipeAsync(int recipeId)
        {
            var adultAsJson = await client.GetStringAsync($"{uri}/getrecipe?id={recipeId}");
            var recipe = JsonSerializer.Deserialize<Recipe>(adultAsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return recipe;
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            var recipeAsJson = JsonSerializer.Serialize(recipe);
            HttpContent content = new StringContent(recipeAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task RemoveRecipeAsync(Recipe recipe)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json")
            };
            await client.SendAsync(request);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonSerializer.Serialize(recipe), Encoding.UTF8, "application/json")
            };
            await client.SendAsync(request);
        }
    }
}