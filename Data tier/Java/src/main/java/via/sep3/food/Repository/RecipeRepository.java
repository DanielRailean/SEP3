package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.Recipe;
import via.sep3.food.Model.User;

public interface RecipeRepository extends CrudRepository<Recipe, Integer> {
}
