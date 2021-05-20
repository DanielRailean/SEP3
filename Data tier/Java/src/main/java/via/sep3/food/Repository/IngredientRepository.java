package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Model.User;

import java.util.List;

public interface IngredientRepository extends CrudRepository<Ingredient, Integer> {
    List<Ingredient> findAllIngredients();

    Ingredient deleteIngredient(Ingredient ingredient);

    Ingredient findIngredientByName(String name);
}
