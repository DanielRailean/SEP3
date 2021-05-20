package via.sep3.food.Service;

import via.sep3.food.Model.Administrator;

public interface IAdministratorService {
    Administrator ValidateAdministrator(String Email, String Password) throws Exception;
}
