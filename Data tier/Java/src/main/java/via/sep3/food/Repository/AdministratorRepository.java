package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.Administrator;
import via.sep3.food.Model.User;

import java.util.List;
import java.util.Map;

public interface AdministratorRepository extends CrudRepository<Administrator, Integer> {
    List<Administrator> findByEmail(String email);
}
