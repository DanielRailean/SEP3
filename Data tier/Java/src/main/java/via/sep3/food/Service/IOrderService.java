package via.sep3.food.Service;

import via.sep3.food.Model.Order;

public interface IOrderService {
    Order GetOrder(int orderid);
    Order GetUserOrders(int userId);
    Order AddOrder(Order order);
    Order RemoveOrder(Order order);
    Order UpdateOrder(Order order);

}
