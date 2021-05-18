package via.sep3.food.Service;

import via.sep3.food.Model.Recipe;

import java.util.List;

public interface IRecipeService {

    List<Recipe> GetAllRecipes();
    Recipe GetRecipe(String Name);
    Recipe AddRecipe(Recipe recipe);
    Recipe RemoveRecipe(Recipe recipe);
    Recipe UpdateRecipe(Recipe recipe);
}
