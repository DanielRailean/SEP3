package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Model.User;

import javax.transaction.Transactional;
import java.util.List;

public interface IngredientRepository extends CrudRepository<Ingredient, Integer> {
    @Transactional
    void deleteById(int id);
    List<Ingredient> findById(int id);
    List<Ingredient> findByName(String name);
}
