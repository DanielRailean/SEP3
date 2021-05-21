using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Pages.Customer;

namespace WebApp.Data
{
    public interface IBasketService
    {
        Task<IList<BasketItem>> GetAllRecipesByOrder();
        Task AddRecipe(BasketItem basketItem);
        Task RemoveRecipe(BasketItem basketItem);
    }
}