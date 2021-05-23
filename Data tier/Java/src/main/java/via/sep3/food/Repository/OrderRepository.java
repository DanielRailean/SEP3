package via.sep3.food.Repository;

import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.UserOrder;

import java.util.List;

public interface OrderRepository extends CrudRepository<UserOrder, Integer> {
    List<UserOrder> findById(int id);
    List<UserOrder> findByUserId(int id);
    void deleteById(int id);
}
