package via.sep3.food.Service;

import via.sep3.food.Model.UserOrder;

import java.util.List;

public interface IOrderService {
    UserOrder GetOrder(int orderId) throws Exception;
    List<UserOrder> GetUserOrders(int userId) throws Exception;
    UserOrder AddOrder(UserOrder userOrder)  throws Exception;
    UserOrder RemoveOrder(int id)  throws Exception;
    UserOrder UpdateOrder(UserOrder userOrder)  throws Exception;

}
