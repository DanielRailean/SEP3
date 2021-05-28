using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Models;
using WebApp.Models;

namespace WebApp.Data.Implementation
{
    public class BasketService : IBasketService
    {
        private ILocalStorageService localStorage;
        private IList<BasketItem> basketItems = new List<BasketItem>();
        private IList<Recipe> addedRecipes = new List<Recipe>();
        private string BasketCookieName = "basketItems";
        private string RecipesCookieName = "addedRecipes";

        public BasketService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
            Fetch();
        }

        public async Task<IList<BasketItem>> GetAllBasketItems()
        {
            return basketItems;
        }

        public async Task<IList<Recipe>> GetAllBasketRecipes()
        {
            return addedRecipes;
        }

        public async Task AddRecipe(Recipe recipe)
        {
            BasketItem item = new BasketItem(recipe.Amount, recipe.Id);
            if (item.Amount < 1)
            {
                throw new Exception("Please select at least one piece to add to cart.");
            }
            if (!ContainsRecipe(recipe)&&(!ContainsItem(item)))
            {
                basketItems.Add(item);
                addedRecipes.Add(recipe);
            }
            else
            {
                if (basketItems.FirstOrDefault(i => i.RecipeId == item.RecipeId).Amount + item.Amount <= 10)
                {
                    basketItems.FirstOrDefault(i => i.RecipeId == item.RecipeId).Amount += item.Amount;
                }
                else throw new Exception("Max 10 units of each recipe per order");
            }
            await Save();
        }

        public async Task RemoveRecipe(Recipe recipe)
        {
            await Fetch();
            if (ContainsRecipe(recipe))
            {
                removeRecipeById(recipe.Id);
                removeBasketItemById(recipe.Id);
            }
            await Save();
        }

        public async Task Clear()
        {
            addedRecipes = new List<Recipe>();
            basketItems = new List<BasketItem>();
            await Save();
        }

        private bool ContainsRecipe(Recipe item)
        {
            return (addedRecipes.FirstOrDefault(i => i.Id == item.Id) != null);
        }
        private bool ContainsItem(BasketItem item)
        {
            return (basketItems.FirstOrDefault(i => i.RecipeId == item.RecipeId) != null);
        }

        private async Task Save()
        {
            string serialised = JsonSerializer.Serialize(basketItems);
            await localStorage.SetItemAsync(BasketCookieName, serialised);
            serialised = JsonSerializer.Serialize(addedRecipes);
            await localStorage.SetItemAsync(RecipesCookieName, serialised);
        }

        private async Task Fetch()
        {
            var cookieContent = await localStorage.GetItemAsync<string>(BasketCookieName);

            if (cookieContent == null)
            {
                Console.WriteLine("Basket items Cookie is blank");
            }
            else
            {
                basketItems = JsonSerializer.Deserialize<IList<BasketItem>>(cookieContent);
            }
            cookieContent = await localStorage.GetItemAsync<string>(RecipesCookieName);
            if (cookieContent == null)
            {
                Console.WriteLine("Recipes Cookie is blank");
            }
            else
            {
                addedRecipes = JsonSerializer.Deserialize<IList<Recipe>>(cookieContent);
            }
        }

        private void removeRecipeById(int id)
        {
            Recipe toRemove = addedRecipes.FirstOrDefault(i => i.Id == id);
            if (toRemove != null) addedRecipes.Remove(toRemove);
        }
        private void removeBasketItemById(int id)
        {
            BasketItem toRemove = basketItems.FirstOrDefault(i => i.RecipeId == id);
            if (toRemove != null) basketItems.Remove(toRemove);
        }
    }
}