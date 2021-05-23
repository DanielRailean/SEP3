package via.sep3.food.Exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.NOT_ACCEPTABLE, reason = "Ingredient with such name already exists")
public class IngredientExists extends RuntimeException{
}
