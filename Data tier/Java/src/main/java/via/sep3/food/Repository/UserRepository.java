package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.User;

import javax.transaction.Transactional;
import java.util.List;

public interface UserRepository extends CrudRepository<User, Long> {
    List<User> findByEmail(String Email);
    @Transactional
    void deleteByEmail(String email);
}
