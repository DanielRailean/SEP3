package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sep3.food.Model.Recipe;
import via.sep3.food.Repository.RecipeRepository;
import via.sep3.food.Service.IRecipeService;

import java.util.List;

@Service
public class RecipeService implements IRecipeService {

  @Autowired
  public RecipeRepository recipeRepository;


    @Override
    public List<Recipe> GetAllRecipes() {
        return recipeRepository.findAllRecipes();
    }

    @Override
    public Recipe GetRecipe(String Name) {
        return recipeRepository.findRecipeByName(Name);
    }

    @Override
    public Recipe AddRecipe(Recipe recipe) {
        return recipeRepository.save(recipe);
    }

    @Override
    public Recipe RemoveRecipe(Recipe recipe) {
        return recipeRepository.deleteRecipe(recipe);
    }

    @Override
    public Recipe UpdateRecipe(Recipe recipe) {
        return recipeRepository.save(recipe);
    }
}
