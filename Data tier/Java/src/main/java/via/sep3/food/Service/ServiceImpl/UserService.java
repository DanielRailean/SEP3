package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.validation.annotation.Validated;
import via.sep3.food.Model.User;
import via.sep3.food.Repository.UserRepository;
import via.sep3.food.Service.IUserService;

import java.util.List;
import java.util.Optional;

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
        User user = userRepository.findByEmail(Email).get(0);
        if(user==null){throw new Exception("User do not exist"); }
        if(user.getPassword().equals(Password)) return user;
        throw new Exception("Password incorrect");
    }

    @Override
    public User RemoveUser( User user) {
        return userRepository.deleteUser(user);
    }

    @Override
    public User UpdateUser(User user, String Password) {
        return userRepository.save(user);
    }

    @Override
    public List<User> GetAllUsers() {
        return userRepository.findAllUsers();
    }
}
