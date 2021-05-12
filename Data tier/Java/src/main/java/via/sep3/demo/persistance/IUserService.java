package via.sep3.demo.persistance;

import org.springframework.stereotype.Component;
import via.sep3.demo.Model.User;


public interface IUserService {
    void AddUser(User user);
    User getUser(String Email);
    void DeleteUser(String Email);
    void UpdateUser(User user);
}
