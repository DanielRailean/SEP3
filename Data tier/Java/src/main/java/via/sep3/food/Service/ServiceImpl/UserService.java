package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.validation.annotation.Validated;
import via.sep3.food.Exceptions.PasswordIncorrect;
import via.sep3.food.Exceptions.UserNotExists;
import via.sep3.food.Model.User;
import via.sep3.food.Repository.UserRepository;
import via.sep3.food.Service.IUserService;

import java.util.List;


@Service
public class UserService implements IUserService {
    @Autowired
    private UserRepository userRepository;

    @Override
    public User RegisterUser(@Validated User user) throws Exception {
        List<User> users = userRepository.findByEmail(user.getEmail());
        if(users.size()>0){throw new Exception("User with this email already exists");}
        return userRepository.save(user);
    }

    @Override
    public User ValidateUser(String Email, String Password) throws Exception {
        User user;
        try {
            List<User> users = userRepository.findByEmail(Email);
            user = users.get(0);
        } catch (Exception e) {
            throw new UserNotExists();
        }
        if(user.getPassword().equals(Password)) return user;
        throw new PasswordIncorrect();

    }

    @Override
    public User RemoveUser(String email) throws Exception {
        try {
            User returned = userRepository.findByEmail(email).get(0);
            userRepository.deleteByEmail(email);
            return returned;
        }catch (Exception e){
            throw new Exception("User does not exist");
        }
    }

    @Override
    public User UpdateUser(User user) throws Exception {
        User returned;
        try {
            User updated = userRepository.findByEmail(user.getEmail()).get(0);
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
