using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IIngredientService
    {
        Task<Ingredient> AddIngredient(Ingredient ingredient);
        Task<Ingredient> GetIngredient(int id);
        Task<Ingredient> UpdateIngredient(Ingredient ingredient);
        Task<Ingredient> RemoveIngredient(Ingredient ingredient);
        Task<IList<Ingredient>> GetAllIngredients();
    }
}