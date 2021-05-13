package via.sep3.demo.persistance;

import via.sep3.demo.Model.Administrator;

public interface IAdministratorService {
    Administrator ValidateAdministrator(String Email, String Password);
}
