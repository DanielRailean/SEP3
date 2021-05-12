package via.sep3.demo.persistance;

import org.springframework.stereotype.Component;
import via.sep3.demo.Model.User;


public interface IUserService {
    User RegisterUser(User user);
    User ValidateUser(String Email,String Password);
    User RemoveUser(User user);
    User UpdateUser(User user,String Password);
}
