package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import via.sep3.food.Exceptions.IngredientExists;
import via.sep3.food.Exceptions.IngredientNotExists;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Model.User;
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

    @GetMapping("/GetIngredient")
    public Ingredient GetOneIngredient(@RequestParam int id){

        try {
            Ingredient returned = IngredientService.GetIngredient(id);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new IngredientNotExists();
        }
    }

    @PostMapping("/AddIngredient")
    public Ingredient AddIngredient(@RequestBody Ingredient ingredient) {
        try {
            Ingredient returned = IngredientService.AddIngredient(ingredient);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new IngredientExists();
        }

    }
    @DeleteMapping("/RemoveIngredient")
    public Ingredient RemoveIngredient(@RequestParam int id){
        try {
            Ingredient returned = IngredientService.RemoveIngredient(id);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new IngredientNotExists();
        }
    }

    @PutMapping("/UpdateIngredient")
    public Ingredient UpdateIngredient(@RequestBody Ingredient ingredient){

        try {
            Ingredient returned = IngredientService.UpdateIngredient(ingredient);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new IngredientNotExists();
        }
    }
}
