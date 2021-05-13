using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class IngredientService : IIngredientService
    {
        public Task<IList<Ingredient>> GetAllIngredientsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Ingredient> GetIngredientAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateIngredientAsync(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveIngredientAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateIngredientAsync(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}