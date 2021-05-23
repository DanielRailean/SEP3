package via.sep3.food.Service;

import via.sep3.food.Model.User;

import java.util.List;

public interface IUserService{
    User RegisterUser(User user) throws Exception;
    User ValidateUser(String Email,String Password) throws Exception;
    User RemoveUser(String email) throws Exception;
    User UpdateUser(User user) throws Exception;
}
