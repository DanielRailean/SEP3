using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrder(int id);
        Task<Order> UpdateOrder(Order order);
        Task<Order> RemoveOrder(Order order);

        Task<IList<Order>> GetUserOrders(string email, string password);
    }
}