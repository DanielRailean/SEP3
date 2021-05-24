using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IRecipeService
    {
        Task<Recipe> AddRecipe(Recipe recipe);
        Task<Recipe> GetRecipe(int id);
        Task<IList<Recipe>> GetAllRecipes();
        Task<Recipe> UpdateRecipe(Recipe recipe);
        Task<Recipe> RemoveRecipe(Recipe recipe);
        Recipe DataToBusiness(DataRecipe dataRecipe);
        DataRecipe BusinessToData(Recipe recipe);

    }
}