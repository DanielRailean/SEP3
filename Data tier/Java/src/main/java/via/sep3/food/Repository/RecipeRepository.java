package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.Recipe;
import via.sep3.food.Model.User;

import java.util.List;

public interface RecipeRepository extends CrudRepository<Recipe, Integer> {

    List<Recipe> findAllRecipes();

    Recipe findRecipeByName(String name);

    Recipe deleteRecipe(Recipe recipe);
}
