using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Pages.Customer;

namespace WebApp.Data
{
    public interface IBasketService
    {
        Task<IList<BasketItem>> GetAllBasketItems();
        Task AddRecipe(BasketItem basketItem);
        Task RemoveRecipe(BasketItem basketItem);
        Task Clear();
    }
}