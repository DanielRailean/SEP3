using System.Collections;
using System.Collections.Generic;
using API.Models;

namespace API.Data
{
    public interface IOrderService
    {
        Order AddOrder(Order order);
        Order GetOrder(int id);
        Order UpdateOrder(Order order);
        Order RemoveOrder(Order order);

        IList<Order> GetUserOrders(string email, string password);
    }
}