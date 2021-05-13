using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IRecipeService
    {
        Task<IList<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipe(int id);
        Task CreateRecipe(Order order);
        Task RemoveRecipe(Order order);
        Task UpdateRecipe(Order order);
    }
}