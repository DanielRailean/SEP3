package via.sep3.demo.persistance;

import via.sep3.demo.Model.Administrator;
import via.sep3.demo.Model.Ingredient;
import via.sep3.demo.Model.Order;

import java.util.ArrayList;
import java.util.List;

public interface IIngredientService {
    ArrayList<Ingredient> GetAllIngredients();
    Ingredient GetOneIngredient(String name);
    Ingredient AddIngredient(Ingredient ingredient);
    Ingredient RemoveIngredient(Ingredient ingredient);
    Ingredient UpdateIngredient(Ingredient ingredient);
}
