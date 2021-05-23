package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import via.sep3.food.Model.Recipe;
import via.sep3.food.Model.Recipe;
import via.sep3.food.Service.IRecipeService;

import java.util.List;

@RestController
public class RecipeController {
    @Autowired
    private IRecipeService RecipeService;

    @GetMapping("/GetAllRecipes")
    public List<Recipe> GetAllRecipes(){
        return RecipeService.GetAllRecipes();
    }

    @GetMapping("/GetRecipe")
    public Recipe GetRecipe(@RequestParam int id){

        try {
            Recipe returned = RecipeService.GetRecipe(id);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @PostMapping("/AddRecipe")
    public Recipe AddRecipe(@RequestBody Recipe recipe) {
        try {
            Recipe returned = RecipeService.AddRecipe(recipe);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }

    }
    @DeleteMapping("/RemoveRecipe")
    public Recipe RemoveRecipe(@RequestParam int id){
        try {
            Recipe returned = RecipeService.RemoveRecipe(id);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @PutMapping("/UpdateRecipe")
    public Recipe UpdateRecipe(@RequestBody Recipe recipe){

        try {
            Recipe returned = RecipeService.UpdateRecipe(recipe);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }
}
