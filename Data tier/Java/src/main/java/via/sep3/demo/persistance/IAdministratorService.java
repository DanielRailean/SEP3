package via.sep3.demo.persistance;

import via.sep3.demo.Model.Administrator;

public interface IAdministratorService {
    Administrator AddAdministrator(Administrator administrator);
    Administrator ValidateAdministrator(String Email,String Password);
    Administrator RemoveAdministrator(Administrator administrator);
    Administrator UpdateAdministrator(Administrator administrator,String Password);
}
