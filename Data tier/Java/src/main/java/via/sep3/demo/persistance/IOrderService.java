package via.sep3.demo.persistance;

import via.sep3.demo.Model.Order;

import java.util.ArrayList;
import java.util.List;

public interface IOrderService {
    List<Order> GetAllOrders();
    Order GetOrder(int order_id);
    Order CreateOrder(Order order);
    Order RemoveOrder(Order order);
    Order UpdateOrder(Order order);

}
