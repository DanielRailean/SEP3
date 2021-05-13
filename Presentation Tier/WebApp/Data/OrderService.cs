using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class OrderService : IOrderService
    {
        private const string uri = "https://localhost:5001/order";
        private readonly HttpClient client;
        
        public async Task<IList<Order>> GetAllOrdersAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Order>> GetOrdersByUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateOrderAsync(Order order)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateOrderAsync(int orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}