package via.sep3.demo.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import via.sep3.demo.Model.Ingredient;
import via.sep3.demo.Model.Order;
import via.sep3.demo.Model.User;
import via.sep3.demo.persistance.IOrderService;

import java.util.List;

@RestController
public class OrderController {


    private IOrderService OrderService;

    @GetMapping("/GetAllOrders")
    public List<Order> GetAllOrders(){
        return OrderService.GetAllOrders();
    }
    @GetMapping("/GetOrder")
    public Order GetOrder(@RequestParam int id){
        return OrderService.GetOrder(id);
    }

    @PostMapping("/CreateOrder")
    public Order CreateOrder(@RequestBody Order order) {
        return OrderService.CreateOrder(order);

    }
    @DeleteMapping("/RemoveOrder")
    public Order RemoveOrder(@RequestBody Order order){
        return OrderService.RemoveOrder(order);
    }

    @PutMapping("/UpdateOrder")
    public Order UpdateOrder(@RequestBody Order order){

        return OrderService.UpdateOrder(order);
    }
}
