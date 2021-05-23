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
        return (List<Recipe>) recipeRepository.findAll();
    }

    @Override
    public Recipe GetRecipe(int id) throws Exception {
        try {
            return recipeRepository.findById(id).get(0);
        } catch (Exception e) {
            throw new Exception("Recipe does not exist");
        }
    }

    @Override
    public Recipe AddRecipe(Recipe recipe) throws Exception {
        List<Recipe> found = null;
        try {
            found  = recipeRepository.findByName(recipe.getName());
        } catch (Exception e) {
        }
        if(found.size()>0){
            throw  new Exception("Recipe with this name already exists");
        }
        return recipeRepository.save(recipe);

    }

    @Override
    public Recipe RemoveRecipe(int id) throws Exception {
        Recipe deleted = GetRecipe(id);
        recipeRepository.deleteById(id);
        return deleted;
    }

    @Override
    public Recipe UpdateRecipe(Recipe recipe) throws Exception {
        Recipe updated = GetRecipe(recipe.getId());
        updated.setCalories(recipe.getCalories());
        updated.setName(recipe.getName());
        updated.setIngredientIdList(recipe.getIngredientIdList());
        updated.setIngredientQuantityList(recipe.getIngredientQuantityList());
        updated.setNutritionType(recipe.getNutritionType());
        updated.setDescription(recipe.getDescription());
        updated.setMinutesToMake(recipe.getMinutesToMake());
        updated.setImage(recipe.getImage());
        recipeRepository.save(updated);
        return GetRecipe(recipe.getId());
    }
}
