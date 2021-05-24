package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sep3.food.Exceptions.IngredientExists;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Repository.IngredientRepository;
import via.sep3.food.Service.IIngredientService;

import java.util.List;

@Service
public class IngredientService implements IIngredientService {

    @Autowired
    public IngredientRepository ingredientRepository;
    @Override
    public List<Ingredient> GetAllIngredients() {
        return (List<Ingredient>) ingredientRepository.findAll();
    }

    @Override
    public Ingredient GetIngredient(int id) throws Exception {
        try {
            return ingredientRepository.findById(id).get(0);
        } catch (Exception e) {
            throw new Exception("Ingredient does not exist");
        }
    }

    @Override
    public Ingredient AddIngredient(Ingredient ingredient) throws Exception {
       List<Ingredient> found = null;
        try {
            found  = ingredientRepository.findByName(ingredient.getName());
        } catch (Exception e) {
        }
        if(found.size()>0){
            throw  new Exception("Ingredient with this name already exists");
        }
        return ingredientRepository.save(ingredient);

    }

    @Override
    public Ingredient RemoveIngredient(int id) throws Exception {
        Ingredient deleted = GetIngredient(id);
        ingredientRepository.deleteById(id);
        return deleted;
    }


    @Override
    public Ingredient UpdateIngredient(Ingredient ingredient) throws Exception {
        List<Ingredient> found = null;
        try {
            found  = ingredientRepository.findByName(ingredient.getName());
        } catch (Exception e) {
        }
        if(found.size()>0){
            throw  new IngredientExists();
        }
        Ingredient updated = GetIngredient(ingredient.getId());
        updated.setCalories(ingredient.getCalories());
        updated.setName(ingredient.getName());
        updated.setUnitOfMeasure(ingredient.getUnitOfMeasure());
        ingredientRepository.save(updated);
        return GetIngredient(ingredient.getId());
    }
}
