package via.sep3.food.Service.ServiceImpl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sep3.food.Model.UserOrder;
import via.sep3.food.Repository.OrderRepository;
import via.sep3.food.Service.IOrderService;

import java.util.List;

@Service
public class OrderService implements IOrderService {

    @Autowired
    private OrderRepository orderRepository;


    @Override
    public UserOrder GetOrder(int id) throws Exception {
        try {
            return orderRepository.findById(id).get(0);
        } catch (Exception e) {
            throw new Exception("Order does not exist");
        }
    }

    @Override
    public List<UserOrder> GetUserOrders(int userId) throws Exception {
        List<UserOrder> found = null;
        try {
            found  = orderRepository.findByUserId(userId);
        } catch (Exception e) {
        }
        if(found.size()<1){
            throw  new Exception("This user has no orders");
        }
        return found;

    }

    @Override
    public UserOrder AddOrder(UserOrder userOrder) throws Exception {
        try {
            return orderRepository.save(userOrder);
        } catch (Exception e) {
            throw  new Exception("Error occurred while adding the order");
        }
    }

    @Override
    public UserOrder RemoveOrder(int id) throws Exception {
        UserOrder deleted = GetOrder(id);
        orderRepository.deleteById(id);
        return deleted;
    }

    @Override
    public UserOrder UpdateOrder(UserOrder userOrder) throws Exception {
        UserOrder updated = GetOrder(userOrder.getId());
        updated.setRecipesIdList(userOrder.getRecipesIdList());
        updated.setRecipesQuantityList(userOrder.getRecipesQuantityList());
        updated.setCity(userOrder.getCity());
        updated.setCurrency(userOrder.getCurrency());
        updated.setDelivered(userOrder.isDelivered());
        updated.setDeliveryAddress(userOrder.getDeliveryAddress());
        updated.setInvoiceAddress(userOrder.getInvoiceAddress());
        updated.setDeliveryPrice(userOrder.getDeliveryPrice());
        updated.setItemPrice(userOrder.getItemPrice());
        updated.setOrderStatus(userOrder.getOrderStatus());
        updated.setPostalCode(userOrder.getPostalCode());
        updated.setTotalPrice(userOrder.getTotalPrice());
        orderRepository.save(updated);
        return GetOrder(userOrder.getId());
    }
}
