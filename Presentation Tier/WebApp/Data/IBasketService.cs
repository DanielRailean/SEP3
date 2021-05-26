using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using WebApp.Models;
using WebApp.Pages.Customer;

namespace WebApp.Data
{
    public interface IBasketService
    {
        Task<IList<BasketItem>> GetAllBasketItems();
        Task<IList<Recipe>> GetAllBasketRecipes();
        Task AddRecipe(Recipe recipe);
        Task RemoveRecipe(Recipe recipe);
        Task Clear();
    }
}