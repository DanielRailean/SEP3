using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class OrderService : IOrderService
    {
        public Task<IList<Order>> GetAllOrders()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrder(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}