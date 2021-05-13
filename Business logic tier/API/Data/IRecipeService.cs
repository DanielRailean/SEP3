using System.Collections.Generic;
using API.Models;

namespace API.Data
{
    public interface IRecipeService
    {
        Recipe AddRecipe(Recipe recipe);
        Recipe GetRecipe(int id);
        IList<Recipe> GetAllRecipes();
        Recipe UpdateRecipe(Recipe recipe);
        Recipe RemoveRecipe(Recipe recipe);
    }
}