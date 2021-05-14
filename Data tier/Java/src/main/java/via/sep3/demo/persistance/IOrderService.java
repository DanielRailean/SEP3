package via.sep3.demo.persistance;

import via.sep3.demo.Model.Order;
import via.sep3.demo.Model.User;

import java.util.ArrayList;
import java.util.List;

public interface IOrderService {
    Order GetOrder(int orderid);
    Order GetUserOrders(int userId);
    Order AddOrder(Order order);
    Order RemoveOrder(Order order);
    Order UpdateOrder(Order order);

}
