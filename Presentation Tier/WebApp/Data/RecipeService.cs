using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class RecipeService : IRecipeService
    {
        public Task<IList<Recipe>> GetAllRecipes()
        {
            throw new System.NotImplementedException();
        }

        public Task<Recipe> GetRecipe(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateRecipe(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRecipe(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateRecipe(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}