package via.sep3.food.Service;

import via.sep3.food.Model.User;

import java.util.List;

public interface IUserService{
    User RegisterUser(User user);
    User ValidateUser(String Email,String Password) throws Exception;
    User RemoveUser(User user);
    User UpdateUser(User user,String Password);
    List<User> GetAllUsers();
}
