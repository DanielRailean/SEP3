package via.sep3.demo.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.demo.Model.User;
import via.sep3.demo.persistance.IUserService;

@RestController
public class UserController {
    @Autowired
    private IUserService UserSerivce;

    @PostMapping("/AddUser")
    public void AddUser(@RequestBody User user){
        UserSerivce.AddUser(user);
    }

    @GetMapping("/GetUser")
    public User getUser(@RequestParam String Email) {
       return UserSerivce.getUser(Email);

    }
    @DeleteMapping("/DeleteUser")
    public void deleteUser(@RequestHeader String Email){
        UserSerivce.DeleteUser(Email);
    }

    @PutMapping("/UpdateUser")
    public void UpdateUser(@RequestBody User user){
        UserSerivce.UpdateUser(user);
    }
}
