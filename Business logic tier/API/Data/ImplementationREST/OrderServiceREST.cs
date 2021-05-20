using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class OrderServiceREST : IOrderService
    {
        private string uri = "https://localhost:8080/User";
        private Order ordered;
        private HttpClient client;

        public OrderServiceREST()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }
        public async Task<Order> AddOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> GetOrder(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Order> RemoveOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Order>> GetUserOrders(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}