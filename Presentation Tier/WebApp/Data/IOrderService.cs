using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IOrderService
    {
        Task<IList<Order>> GetAllOrders();
        Task<Order> GetOrder(int id);
        Task CreateOrder(Order order);
        Task RemoveOrder(Order order);
        Task UpdateOrder(Order order);
    }
}