using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int orderId);
        Task<IList<Order>> GetOrdersByUserAsync(User user);
        Task<IList<Order>> GetOrderAdmin(int id,User user);
        Task CreateOrderAsync(Order order);
        Task RemoveOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}