using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using WebApp.Models;

namespace WebApp.Data
{
    public class BasketService : IBasketService
    {
        private IJSRuntime jsRuntime;
        private IList<Recipe> recipes;

        public BasketService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            recipes = new List<Recipe>();
        }

        public async Task<IList<Recipe>> GetAllRecipesByOrder()
        {
            try
            {
                var recipesAsString = await jsRuntime.InvokeAsync<IList<string>>("sessionStorage.getItem", "recipe");
                foreach (var recipe in recipesAsString)
                {
                    var recipeAsJson = JsonSerializer.Deserialize<Recipe>(recipe);
                    recipes.Add(recipeAsJson);
                }

                return recipes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AddRecipe(Recipe recipe)
        {
            try
            {
                string serializedRecipe = JsonSerializer.Serialize(recipe);
                recipes.Add(recipe);
                await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "recipe", serializedRecipe);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RemoveRecipe(Recipe recipe)
        {
            try
            {
                string serializedRecipe = JsonSerializer.Serialize(recipe);
                recipes.Remove(recipe);
                await jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "recipe", serializedRecipe);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}