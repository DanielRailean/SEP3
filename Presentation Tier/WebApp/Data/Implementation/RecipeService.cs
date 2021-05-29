using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace WebApp.Data.Implementation
{
    public class RecipeService : IRecipeService
    {
        private const string uri = "https://localhost:5001/recipe";
        private readonly HttpClient client;

        public RecipeService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }

        /// <summary>
        /// Consumes REST endpoint to return all the recipes,
        /// in JSON format.
        /// </summary>
        /// <returns>List of recipes.</returns>
        /// <exception cref="Exception">Response phrase.</exception>
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

        /// <summary>
        /// Consumes REST endpoint to get a specific recipe.
        /// </summary>
        /// <param name="recipeId">Id of recipe to get.</param>
        /// <returns>Recipe item.</returns>
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

        /// <summary>
        /// Posts a new recipe to the REST endpoint,
        /// in JSON format.
        /// </summary>
        /// <param name="recipe">Recipe to create.</param>
        /// <exception cref="Exception">Response phrase.</exception>
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
        
        /// <summary>
        /// Sends a Delete request to the REST endpoint,
        /// to remove a specific recipe, in JSON format.
        /// </summary>
        /// <param name="recipe">Recipe to remove.</param>
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

        /// <summary>
        /// Sends a Put request to the REST endpoint,
        /// to update an existing recipe, in JSON format.
        /// </summary>
        /// <param name="recipe">Recipe to update.</param>
        /// <exception cref="Exception">Response phrase.</exception>
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