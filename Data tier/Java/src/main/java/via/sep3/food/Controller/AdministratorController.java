package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.food.Model.Administrator;
import via.sep3.food.Service.IAdministratorService;

@RestController
public class AdministratorController {
    @Autowired
    private IAdministratorService AdministratorService;



    @GetMapping("/ValidateAdministrator")
    public Administrator ValidateAdministrator(@RequestParam String Email, @RequestParam String Password) {
        return AdministratorService.ValidateAdministrator(Email,Password);

    }
}
