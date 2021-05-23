package via.sep3.food.Controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import via.sep3.food.Model.UserOrder;
import via.sep3.food.Service.IOrderService;

import java.util.List;

@RestController
public class OrderController {

    @Autowired
    private IOrderService OrderService;

    @GetMapping("/GetOrder")
    public UserOrder GetOrder(@RequestParam int id){
        try {
            UserOrder returned = OrderService.GetOrder(id);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @GetMapping("/GetUserOrders")
    public List<UserOrder> GetUserOrders(@RequestParam int userId){
        try {
            List<UserOrder> returned = OrderService.GetUserOrders(userId);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @PostMapping("/AddOrder")
    public UserOrder AddOrder(@RequestBody UserOrder userOrder) {
        try {
            UserOrder returned = OrderService.AddOrder(userOrder);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }

    }
    @DeleteMapping("/RemoveOrder")
    public UserOrder RemoveOrder(@RequestParam int id){
        try {
            UserOrder returned = OrderService.RemoveOrder(id);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }

    @PutMapping("/UpdateOrder")
    public UserOrder UpdateOrder(@RequestBody UserOrder userOrder){
        try {
            UserOrder returned = OrderService.UpdateOrder(userOrder);
            System.out.println(returned);
            return returned;
        }catch (Exception e){
            throw new ResponseStatusException(
                    HttpStatus.NOT_FOUND, "Error", e);
        }
    }
}
