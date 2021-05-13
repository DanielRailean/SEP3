using API.Models;

namespace API.Data
{
    public interface IIngredientService
    {
        Ingredient AddIngredient(Ingredient ingredient);
        Ingredient GetIngredient(int id);
        Ingredient UpdateIngredient(Ingredient ingredient);
        Ingredient RemoveIngredient(Ingredient ingredient);
    }
}