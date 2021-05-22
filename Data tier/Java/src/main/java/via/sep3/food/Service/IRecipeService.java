package via.sep3.food.Service;

import via.sep3.food.Model.Recipe;

import java.util.List;

public interface IRecipeService {

    List<Recipe> GetAllRecipes();
    Recipe GetRecipe(int id) throws Exception;
    Recipe AddRecipe(Recipe recipe) throws Exception;
    Recipe RemoveRecipe(int id) throws Exception;
    Recipe UpdateRecipe(Recipe recipe) throws Exception;
}
