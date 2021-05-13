using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class RecipeService : IRecipeService
    {
        private const string uri = "https://localhost:5001/recipe";
        private readonly HttpClient client;
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