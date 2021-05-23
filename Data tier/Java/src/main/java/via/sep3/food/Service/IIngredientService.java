package via.sep3.food.Service;

import via.sep3.food.Model.Ingredient;

import java.util.List;

public interface IIngredientService {
    List<Ingredient> GetAllIngredients();
    Ingredient GetIngredient(int id) throws Exception;
    Ingredient AddIngredient(Ingredient ingredient) throws Exception;
    Ingredient RemoveIngredient(int id) throws Exception;
    Ingredient UpdateIngredient(Ingredient ingredient) throws Exception;
}
