package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Model.User;

public interface IngredientRepository extends CrudRepository<Ingredient, Integer> {
}
