package via.sep3.food.Service;

import via.sep3.food.Model.Ingredient;

import java.util.List;

public interface IIngredientService {
    List<Ingredient> GetAllIngredients();
    Ingredient GetOneIngredient(String name);
    Ingredient AddIngredient(Ingredient ingredient);
    Ingredient RemoveIngredient(Ingredient ingredient);
    Ingredient UpdateIngredient(Ingredient ingredient);
}
