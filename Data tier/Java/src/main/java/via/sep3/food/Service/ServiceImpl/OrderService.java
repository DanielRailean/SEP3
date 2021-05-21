package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sep3.food.Model.Order;
import via.sep3.food.Repository.OrderRepository;
import via.sep3.food.Service.IOrderService;

@Service
public class OrderService implements IOrderService {

    @Autowired
    private OrderRepository orderRepository;

    @Override
    public Order GetOrder(int Id) {
        return orderRepository.findByOrderId( Id);
    }

    @Override
    public Order GetUserOrders(int UserId) {
        return orderRepository.findByOrderId(UserId);
    }

    @Override
    public Order AddOrder(Order order) {
        return orderRepository.save(order);
    }

    @Override
    public Order RemoveOrder(Order order) {
        return orderRepository.deleterOrder(order);
    }

    @Override
    public Order UpdateOrder(Order order) {
        return orderRepository.save(order);
    }
}
