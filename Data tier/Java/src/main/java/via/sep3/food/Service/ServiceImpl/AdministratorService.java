package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sep3.food.Model.Administrator;
import via.sep3.food.Model.User;
import via.sep3.food.Repository.AdministratorRepository;
import via.sep3.food.Service.IAdministratorService;

@Service
public class AdministratorService implements IAdministratorService {

    @Autowired
    private AdministratorRepository administratorRepository;

    @Override
    public Administrator ValidateAdministrator(String Email, String Password) throws Exception {
        Administrator administrator = administratorRepository.findByEmail(Email).get(0);
        if(administrator==null)
        {throw new Exception("Administrator do not exist"); }
        if(administrator.getPassword().equals(Password)) return administrator;
        throw new Exception("Password incorrect");    }
}
