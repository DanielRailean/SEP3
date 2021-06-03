using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    /// <summary>
    /// Consumes REST endpoints and does CRUD operations on Recipes.
    /// </summary>
    public class RecipeServiceREST : IRecipeService
    {
        private string uri = "http://localhost:8080";
        private HttpClient client;
        JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

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
            DataRecipe returned = BusinessToData(recipe);   
            string recipeAsJson = JsonSerializer.Serialize(returned,serializeOptions);
            Console.WriteLine("request"+recipeAsJson);
            HttpContent content = new StringContent(
                recipeAsJson,
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage response = await client.PostAsync(uri+"/AddRecipe", content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataRecipe gotRecipes = JsonSerializer.Deserialize<DataRecipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Recipe secondReturned = DataToBusiness(gotRecipes);
            Console.WriteLine("result"+result);
            return secondReturned;
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(uri+"/GetRecipe" + $"?id={@id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await responseMessage.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();
            DataRecipe gotRecipe = JsonSerializer.Deserialize<DataRecipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Recipe secondReturned = DataToBusiness(gotRecipe);
            Console.WriteLine("result"+result);
            return secondReturned;
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            DataRecipe returned = BusinessToData(recipe);  
            string recipeAsJson = JsonSerializer.Serialize(returned,serializeOptions);
            Console.WriteLine(recipeAsJson);
            HttpContent content = new StringContent(
                recipeAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri+"/UpdateRecipe", content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataRecipe updatedRecipe = JsonSerializer.Deserialize<DataRecipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Recipe secondReturned = DataToBusiness(updatedRecipe);
            Console.WriteLine("result"+result);
            return secondReturned;
        }

        public async Task<Recipe> RemoveRecipe(Recipe recipe)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(uri + $"/RemoveRecipe?id={@recipe.Id}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataRecipe removedRecipe = JsonSerializer.Deserialize<DataRecipe>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Recipe secondReturned = DataToBusiness(removedRecipe);
            Console.WriteLine("result"+result);
            return secondReturned;
        }
        
        public async Task<IList<Recipe>> GetAllRecipes()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(uri+"/GetAllRecipes" );
            if (!responseMessage.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await responseMessage.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();
            List<DataRecipe> gotRecipe = JsonSerializer.Deserialize<List<DataRecipe>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            List<Recipe> listRecipe = new List<Recipe>();
            foreach (var item in gotRecipe)
            {
                var temp = DataToBusiness(item);
                listRecipe.Add(temp);
            }
            Console.WriteLine("result"+result);
            return listRecipe;
        }

        public Recipe DataToBusiness(DataRecipe dataRecipe)
        {
            Recipe returned = new Recipe();
            returned.Ingredient = new List<RecipeItem>();
            returned.Name = dataRecipe.Name;
            returned.Id = dataRecipe.Id;
            returned.MinutesToMake = dataRecipe.MinutesToMake;
            returned.Description = dataRecipe.Description;
            returned.Calories = dataRecipe.Calories;
            returned.NutritionType= dataRecipe.NutritionType;
            returned.Image = dataRecipe.Image;
            returned.Price = dataRecipe.Price;
            
            List<string> ids = dataRecipe.IngredientIdList.Split(",").ToList();
            List<string> quantity = dataRecipe.IngredientQuantityList.Split(",").ToList();
            for(int i=0;i<ids.Count-1;i++)
            {
                RecipeItem temp = new RecipeItem();
                temp.IngredientId = Convert.ToInt32(ids[i]);
                temp.Amount=Convert.ToInt32(quantity[i]);
                returned.Ingredient.Add(temp);
            }

            return returned;
        }

        public DataRecipe BusinessToData(Recipe recipe)
        {
            DataRecipe returned = new DataRecipe();
            returned.Name = recipe.Name;
            returned.Id = recipe.Id;
            returned.MinutesToMake = recipe.MinutesToMake;
            returned.Description = recipe.Description;
            returned.Calories = recipe.Calories;
            returned.NutritionType= recipe.NutritionType;
            returned.Image = recipe.Image;
            returned.Price = recipe.Price;
            
            foreach (var item in recipe.Ingredient)
            {
                returned.IngredientIdList += item.IngredientId + ",";
                returned.IngredientQuantityList += item.Amount + ",";
            }

            return returned;
        }

        
    }
}