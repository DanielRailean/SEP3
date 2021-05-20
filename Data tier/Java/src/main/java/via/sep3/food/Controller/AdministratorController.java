package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import via.sep3.food.Model.Administrator;
import via.sep3.food.Model.User;
import via.sep3.food.Service.IAdministratorService;

@RestController
public class AdministratorController {
    @Autowired
    private IAdministratorService AdministratorService;



    @GetMapping("/ValidateAdministrator")
    public Administrator ValidateAdministrator(@RequestParam String Email, @RequestParam String Password) {
        try {
            Administrator returnAdmin = AdministratorService.ValidateAdministrator(Email, Password);
            System.out.println(returnAdmin);
            return returnAdmin;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }
}
