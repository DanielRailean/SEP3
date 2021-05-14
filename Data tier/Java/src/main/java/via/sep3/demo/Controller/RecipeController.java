package via.sep3.demo.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.demo.Model.Order;
import via.sep3.demo.Model.Recipe;
import via.sep3.demo.Model.User;
import via.sep3.demo.persistance.IRecipeService;

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
    public Recipe GetRecipe(@RequestParam String Name){
        return RecipeService.GetRecipe(Name);
    }

    @PostMapping("/AddRecipe")
    public Recipe AddRecipe(@RequestBody Recipe recipe) {
        return RecipeService.AddRecipe(recipe);

    }
    @DeleteMapping("/RemoveRecipe")
    public Recipe RemoveRecipe(@RequestBody Recipe recipe){
        return RecipeService.RemoveRecipe(recipe);
    }

    @PutMapping("/UpdateRecipe")
    public Recipe UpdateRecipe(@RequestBody Recipe recipe){

        return RecipeService.UpdateRecipe(recipe);
}}
