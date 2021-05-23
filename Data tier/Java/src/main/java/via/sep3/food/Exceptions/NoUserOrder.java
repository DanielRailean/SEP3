
package via.sep3.food.Exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.NOT_FOUND, reason = "This user has no orders")
public class NoUserOrder extends RuntimeException{
}
