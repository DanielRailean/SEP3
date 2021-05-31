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
    /// <summary>
    /// This class is responsible for doing CRUD operations
    /// on BasketItems in the LocalStorage.
    /// </summary>
    public class BasketService : IBasketService
    {
        private ILocalStorageService localStorage;
        private IList<BasketItem> basketItems = new List<BasketItem>();
        private IList<Recipe> addedRecipes = new List<Recipe>();
        private string BasketCookieName = "basketItems";
        private string RecipesCookieName = "addedRecipes";

        /// <summary>
        /// Constructor that initializes the LocalStorage and
        /// fills it up with data if available.
        /// </summary>
        /// <param name="localStorage">LocalStorage interface.</param>
        public BasketService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
            Fetch();
        }

        /// <summary>
        /// This method gets all the elements from the storage.
        /// </summary>
        /// <returns>A list of BasketItems.</returns>
        public async Task<IList<BasketItem>> GetAllBasketItems()
        {
            return basketItems;
        }

        /// <summary>
        /// This method gets all the elements from the storage.
        /// </summary>
        /// <returns>A list of Recipes.</returns>
        public async Task<IList<Recipe>> GetAllBasketRecipes()
        {
            return addedRecipes;
        }

        /// <summary>
        /// Matching the recipe to a BasketItem, by creating one,
        /// then adding both into basketItems and addedRecipes list,
        /// then call the Save() to write to LocalStorage.
        /// </summary>
        /// <param name="recipe">Recipe item to be added.</param>
        /// <exception cref="Exception">The amount of added item has to be more than 0 and maximum 10.</exception>
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

        /// <summary>
        /// This method calls the Fetch() to get
        /// all the items from LocalStorage, then if
        /// the parameter matches an item in the two list,
        /// it removes it, then calls Save() to write to LocalStorage.
        /// </summary>
        /// <param name="recipe">Recipe item to remove.</param>
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

        /// <summary>
        /// Clears all items from LocalStorage.
        /// </summary>
        public async Task Clear()
        {
            await localStorage.RemoveItemAsync(BasketCookieName);
            await localStorage.RemoveItemAsync(RecipesCookieName);
            addedRecipes = new List<Recipe>();
            basketItems = new List<BasketItem>();
        }

        /// <summary>
        /// This method is to check if a list has a specific item.
        /// </summary>
        /// <param name="item">Item to check.</param>
        /// <returns>A boolean value corresponding to the parameter.</returns>
        private bool ContainsRecipe(Recipe item)
        {
            return (addedRecipes.FirstOrDefault(i => i.Id == item.Id) != null);
        }
        
        /// <summary>
        /// This method is to check if a list has a specific item.
        /// </summary>
        /// <param name="item">Item to check.</param>
        /// <returns>A boolean value corresponding to the parameter.</returns>
        private bool ContainsItem(BasketItem item)
        {
            return (basketItems.FirstOrDefault(i => i.RecipeId == item.RecipeId) != null);
        }

        /// <summary>
        /// Writes the two lists to the LocalStorage.
        /// </summary>
        private async Task Save()
        {
            string serialised = JsonSerializer.Serialize(basketItems);
            await localStorage.SetItemAsync(BasketCookieName, serialised);
            serialised = JsonSerializer.Serialize(addedRecipes);
            await localStorage.SetItemAsync(RecipesCookieName, serialised);
        }

        /// <summary>
        /// Fills the two lists with the values from LocalStorage.
        /// </summary>
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

        /// <summary>
        /// Deletes a specific Recipe from list.
        /// </summary>
        /// <param name="id">Id of Recipe.</param>
        private void removeRecipeById(int id)
        {
            Recipe toRemove = addedRecipes.FirstOrDefault(i => i.Id == id);
            if (toRemove != null) addedRecipes.Remove(toRemove);
        }
        
        /// <summary>
        /// Deletes a specific BasketItem from list.
        /// </summary>
        /// <param name="id">Id of BasketItem.</param>
        private void removeBasketItemById(int id)
        {
            BasketItem toRemove = basketItems.FirstOrDefault(i => i.RecipeId == id);
            if (toRemove != null) basketItems.Remove(toRemove);
        }
    }
}