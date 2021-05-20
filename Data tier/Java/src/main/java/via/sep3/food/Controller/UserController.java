package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.food.Model.User;
import via.sep3.food.Service.IUserService;

import java.util.List;

@RestController
public class UserController {
    @Autowired
    private IUserService userService;

    //User testUser = new User("satish", "gurung", "satish", "gurung",123456, "Horsens", 8700);

    @PostMapping("/RegisterUser")
    public User RegisterUser(@RequestBody User user){
       return userService.RegisterUser(user);
    }

    @GetMapping("/ValidateUser")
    public User ValidateUser(@RequestParam String Email,@RequestParam String Password) {
        try {
            User returnUser = userService.ValidateUser(Email, Password);
            System.out.println(returnUser);
            return returnUser;
        }catch (Exception e){

        }
        return null;
    }
    @GetMapping("/GetAllUsers")
    public List<User> GetAllUsers(){
        return userService.GetAllUsers();
    }

    @DeleteMapping("/RemoveUser")
    public User RemoverUser(@RequestBody User user){
       return userService.RemoveUser(user);
    }

    @PutMapping("/UpdateUser")
    public User UpdateUser(@RequestBody User user,String Password){
        return userService.UpdateUser(user,Password);
    }
}
