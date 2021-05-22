package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.validation.annotation.Validated;
import via.sep3.food.Model.User;
import via.sep3.food.Repository.UserRepository;
import via.sep3.food.Service.IUserService;

import java.util.List;


@Service
public class UserService implements IUserService {
    @Autowired
    private UserRepository userRepository;

    @Override
    public User RegisterUser(@Validated User user) {
        return userRepository.save(user);

    }

    @Override
    public User ValidateUser(String Email, String Password) throws Exception {
        User user;
        try {
            List<User> users = userRepository.findByEmail(Email);
            user = users.get(0);
        } catch (Exception e) {
            throw new Exception("User do not exist");
        }
        if(user.getPassword().equals(Password)) return user;
        throw new Exception("Password incorrect");

    }

    @Override
    public User RemoveUser( User user) {
        User returned = userRepository.findByEmail(user.getEmail()).get(0);
        userRepository.deleteByEmail(user.getEmail());
        return returned;
    }

    @Override
    public User UpdateUser(User user) throws Exception {
        User returned;
        try {
            User updated = ValidateUser(user.getEmail(), user.getPassword());
            updated.setPassword(user.getPassword());
            updated.setAddress(user.getAddress());
            updated.setFirstName(user.getFirstName());
            updated.setLastName(user.getLastName());
            updated.setPhone(user.getPhone());
            updated.setPostalCode(user.getPostalCode());
            userRepository.save(updated);
            returned = ValidateUser(updated.getEmail(),updated.getPassword());
        } catch (Exception e) {
            throw new Exception("User does not exist");
        }
        return returned;
    }

}
