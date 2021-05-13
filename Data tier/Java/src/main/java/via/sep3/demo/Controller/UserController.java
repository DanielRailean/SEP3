package via.sep3.demo.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.demo.Model.User;
import via.sep3.demo.persistance.IUserService;

@RestController
public class UserController {
    @Autowired
    private IUserService UserSerivce;

    //User testUser = new User("satish", "gurung", "satish", "gurung",123456, "Horsens", 8700);

    @PostMapping("/RegisterUser")
    public User RegisterUser(@RequestBody User user){
       return UserSerivce.RegisterUser(user);
    }

    @GetMapping("/ValidateUser")
    public User ValidateUser(@RequestParam String Email,@RequestParam String Password) {
       User returnUser = UserSerivce.ValidateUser(Email, Password);
        System.out.println(returnUser);
        return returnUser;
    }

    @DeleteMapping("/RemoveUser")
    public User RemoverUser(@RequestBody User user){
       return UserSerivce.RemoveUser(user);
    }

    @PutMapping("/UpdateUser")
    public User UpdateUser(@RequestBody User user,String Password){
        return UserSerivce.UpdateUser(user,Password);
    }
}
