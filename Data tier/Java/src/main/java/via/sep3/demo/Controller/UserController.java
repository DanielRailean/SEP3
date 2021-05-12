package via.sep3.demo.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.demo.Model.User;
import via.sep3.demo.persistance.IUserService;

@RestController
public class UserController {
    @Autowired
    private IUserService UserSerivce;

    @PostMapping("/RegisterUser")
    public User RegisterUser(@RequestBody User user){
       return UserSerivce.RegisterUser(user);
    }

    @GetMapping("/ValidateUser")
    public User ValidateUser(@RequestParam String Email,String Password) {
       return UserSerivce.ValidateUser(Email,Password);

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
