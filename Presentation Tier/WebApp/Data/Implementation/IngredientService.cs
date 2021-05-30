using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Implementation
{
    /// <summary>
    /// This class is responsible for doing CRUD operations
    /// on Ingredients through REST requests.
    /// </summary>
    public class IngredientService : IIngredientService
    {
        private const string uri = "https://localhost:5001/ingredient";
        private readonly HttpClient client;
        
        /// <summary>
        /// Constructor that initializes the Client
        /// with the created ClientHandler.
        /// </summary>
        public IngredientService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            }; 
            client = new HttpClient(clientHandler);
        }
        
        /// <summary>
        /// Consumes the REST endpoint and returns JSON formatted value.
        /// </summary>
        /// <returns>List of all ingredients.</returns>
        /// <exception cref="Exception">Response phrase.</exception>
        public async Task<IList<Ingredient>> GetAllIngredientsAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri + "/getallingredients");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            string result = await response.Content.ReadAsStringAsync();
            List<Ingredient> ingredients = JsonSerializer.Deserialize<List<Ingredient>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return ingredients;
        }

        /// <summary>
        /// Consumes the REST endpoint to get a specific
        /// ingredient JSON formatted by id.
        /// </summary>
        /// <param name="ingredientId">Id of ingredient to get.0</param>
        /// <returns>Specific ingredient.</returns>
        public async Task<Ingredient> GetIngredientAsync(int ingredientId)
        {
            var ingredientAsJson = await client.GetStringAsync($"{uri}/getingredient?id={ingredientId}");
            var ingredient = JsonSerializer.Deserialize<Ingredient>(ingredientAsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return ingredient;
        }

        /// <summary>
        /// Posts a new ingredient of type to the REST endpoint,
        /// in JSON format.
        /// </summary>
        /// <param name="ingredient">Ingredient to create.</param>
        /// <exception cref="Exception">Response phrase.</exception>
        public async Task CreateIngredientAsync(Ingredient ingredient)
        {
            var ingredientAsJson = JsonSerializer.Serialize(ingredient);
            HttpContent content = new StringContent(ingredientAsJson,
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
        /// to remove a specific ingredient,
        /// in JSON format.
        /// </summary>
        /// <param name="ingredient">Ingredient to delete.</param>
        public async Task RemoveIngredientAsync(Ingredient ingredient)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonSerializer.Serialize(ingredient), Encoding.UTF8, "application/json")
            };
            await client.SendAsync(request);
        }

        /// <summary>
        /// Sends a Put request to the REST endpoint,
        /// to update an existing ingredient, in JSON format.
        /// </summary>
        /// <param name="ingredient">Ingredient to update.</param>
        /// <exception cref="Exception">Response phrase.</exception>
        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            var ingredientAsJson = JsonSerializer.Serialize(ingredient);
            HttpContent content = new StringContent(ingredientAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync($"{uri}/", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}