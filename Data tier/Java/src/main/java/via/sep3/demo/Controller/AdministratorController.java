package via.sep3.demo.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.demo.Model.Administrator;
import via.sep3.demo.Model.User;
import via.sep3.demo.persistance.IAdministratorService;

@RestController
public class AdministratorController {
    private IAdministratorService AdministratorService;

    @PostMapping("/AddAdministrator")
    public Administrator AddAdministrator(@RequestBody Administrator administrator){
        return AdministratorService.AddAdministrator(administrator);
    }

    @GetMapping("/ValidateAdministrator")
    public Administrator ValidateAdministrator(@RequestParam String Email, String Password) {
        return AdministratorService.ValidateAdministrator(Email,Password);

    }
    @DeleteMapping("/RemoveAdministrator")
    public Administrator RemoveAdministrator(@RequestBody Administrator administrator){
        return AdministratorService.RemoveAdministrator(administrator);
    }

    @PutMapping("/UpdateAdministrator")
    public Administrator UpdateAdministrator(@RequestBody Administrator administrator,String Password){

        return AdministratorService.UpdateAdministrator(administrator,Password);
    }

}
