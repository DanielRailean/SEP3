using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IIngredientService
    {
        Task<IList<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient> GetIngredientAsync(int id);
        Task CreateIngredientAsync(Ingredient ingredient);
        Task RemoveIngredientAsync(int Id);
        Task UpdateIngredientAsync(int Id);
    }
}