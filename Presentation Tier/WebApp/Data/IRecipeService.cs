using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IRecipeService
    {
        Task<IList<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeAsync(int recipeId);
        Task CreateRecipeAsync(Recipe recipe);
        Task RemoveRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
    }
}