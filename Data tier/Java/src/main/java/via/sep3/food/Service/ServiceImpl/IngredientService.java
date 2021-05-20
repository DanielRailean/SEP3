package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
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
        return null;
    }

    @Override
    public Ingredient GetOneIngredient(String name) {
        return null;
    }

    @Override
    public Ingredient AddIngredient(Ingredient ingredient) {
        return ingredientRepository.save(ingredient);
    }

    @Override
    public Ingredient RemoveIngredient(Ingredient ingredient) {
        return null;
    }

    @Override
    public Ingredient UpdateIngredient(Ingredient ingredient) {
        return null;
    }
}
