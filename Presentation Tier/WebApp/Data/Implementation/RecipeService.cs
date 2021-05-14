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

        public Task<IList<Recipe>> GetAllRecipesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Recipe> GetRecipeAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateRecipeAsync(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRecipeAsync(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateRecipeAsync(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}