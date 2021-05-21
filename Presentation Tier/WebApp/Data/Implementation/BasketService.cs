using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using MudBlazor.Extensions;
using WebApp.Models;

namespace WebApp.Data
{
    public class BasketService : IBasketService
    {
        private IJSRuntime jsRuntime;
        private IList<BasketItem> basketItems;

        public BasketService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
            basketItems = new List<BasketItem>();
        }

        public async Task<IList<BasketItem>> GetAllRecipesByOrder()
        {
            try
            {
                var recipeCount = await jsRuntime.InvokeAsync<int>("localStorageService.sizeOfLocalStorage");
                for (int i = 0; i < recipeCount; i++)
                {
                    var recipesAsString = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "recipe");
                    var recipeAsJson = JsonSerializer.Deserialize<BasketItem>(recipesAsString);
                    basketItems.Add(recipeAsJson);
                }

                return basketItems;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AddRecipe(BasketItem basketItem)
        {
            try
            {
                // string serializedRecipe = JsonSerializer.Serialize(recipe);
                // string[] arr = {amount.ToString(), serializedRecipe};
                // basketItems.Add(recipe);
                // await jsRuntime.InvokeVoidAsync("localStorage.setItem", $"{amount}x{recipe.Name}", serializedRecipe);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RemoveRecipe(BasketItem basketItem)
        {
            try
            {
                // string serializedRecipe = JsonSerializer.Serialize(recipe);
                // string[] arr = {amount.ToString(), serializedRecipe};
                // basketItems.Remove(recipe);
                // await jsRuntime.InvokeVoidAsync("localStorage.removeItem", recipe.Id, arr);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}