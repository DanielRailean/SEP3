using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IIngredientService
    {
        Task<IList<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient> GetIngredientAsync(int ingredientId);
        Task CreateIngredientAsync(Ingredient ingredient);
        Task RemoveIngredientAsync(int ingredientId);
        Task UpdateIngredientAsync(Ingredient ingredient);
    }
}