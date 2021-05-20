package via.sep3.food.Repository;

import org.aspectj.weaver.ast.Or;
import org.springframework.data.repository.CrudRepository;
import via.sep3.food.Model.Order;
import via.sep3.food.Model.User;

public interface OrderRepository extends CrudRepository<Order, Integer> {
    Order findByOrderId(int id);

    Order deleterOrder(Order order);
}
