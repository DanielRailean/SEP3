package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Service.IIngredientService;

import java.util.List;

@RestController
public class IngredientController {
    @Autowired
    private IIngredientService IngredientService;


    @GetMapping("/GetAllIngredients")
    public List<Ingredient> GetAllIngredients(){
        return IngredientService.GetAllIngredients();
    }

    @GetMapping("/GetOneIngredient")
    public Ingredient GetOneIngredient(@RequestParam String name){
        return IngredientService.GetOneIngredient(name);
    }

    @PostMapping("/AddIngredient")
    public Ingredient AddIngredient(@RequestBody Ingredient ingredient) {
        return IngredientService.AddIngredient(ingredient);

    }
    @DeleteMapping("/RemoveIngredient")
    public Ingredient RemoveIngredient(@RequestBody Ingredient ingredient){
        return IngredientService.RemoveIngredient(ingredient);
    }

    @PutMapping("/UpdateIngredient")
    public Ingredient UpdateIngredient(@RequestBody Ingredient ingredient){

        return IngredientService.UpdateIngredient(ingredient);
    }
}
