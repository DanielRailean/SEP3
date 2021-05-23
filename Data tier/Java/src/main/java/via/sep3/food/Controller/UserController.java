package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import via.sep3.food.Exceptions.PasswordIncorrect;
import via.sep3.food.Exceptions.UserExistsException;
import via.sep3.food.Exceptions.UserNotExists;
import via.sep3.food.Model.User;
import via.sep3.food.Service.IUserService;

import java.util.List;

@RestController
public class UserController {
    @Autowired
    private IUserService userService;

    @PostMapping("/RegisterUser")
    public User RegisterUser(@RequestBody User user){
        try {
            User returnUser = userService.RegisterUser(user);
            System.out.println(returnUser);
            return returnUser;
        }catch (Exception e){
            throw new UserExistsException();
        }
    }

    @GetMapping("/ValidateUser")
    public User ValidateUser(@RequestParam String Email,@RequestParam String Password) {
        try {
            User returnUser = userService.ValidateUser(Email, Password);
            System.out.println(returnUser);
            return returnUser;
        } catch (UserNotExists e) {
            throw new UserNotExists();
        } catch (PasswordIncorrect e) {
            throw new PasswordIncorrect();
        } catch (Exception e) {
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @DeleteMapping("/RemoveUser")
    public User RemoverUser(@RequestBody User user){
        try {
            User returnUser = userService.RemoveUser(user);
            System.out.println(returnUser);
            return returnUser;
        }catch (Exception e){
            throw new UserNotExists();
        }
    }

    @PutMapping("/UpdateUser")
    public User UpdateUser(@RequestBody User user){
        try {
            User updated = userService.UpdateUser(user);
            System.out.println(updated);
            return updated;
        }catch (Exception e){
            throw new UserNotExists();
        }
    }
}
