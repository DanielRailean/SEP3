package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import via.sep3.food.Model.User;
import via.sep3.food.Service.IUserService;

import java.util.List;

@RestController
public class UserController {
    @Autowired
    private IUserService userService;

    @PostMapping("/RegisterUser")
    public User RegisterUser(@RequestBody User user){
        System.out.println(user);
        return userService.RegisterUser(user);
    }

    @GetMapping("/ValidateUser")
    public User ValidateUser(@RequestParam String Email,@RequestParam String Password) {
        try {
            User returnUser = userService.ValidateUser(Email, Password);
            System.out.println(returnUser);
            return returnUser;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @DeleteMapping("/RemoveUser")
    public User RemoverUser(@RequestBody User user){
       return userService.RemoveUser(user);
    }

    @PutMapping("/UpdateUser")
    public User UpdateUser(@RequestBody User user){
        try {
            User updated = userService.UpdateUser(user);
            System.out.println(updated);
            return updated;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }
}
