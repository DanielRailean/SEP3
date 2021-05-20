using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IBasketService
    {
        Task<IList<Recipe>> GetAllRecipesByOrder();
        Task AddRecipe(Recipe recipe);
        Task RemoveRecipe(Recipe recipe);
    }
}