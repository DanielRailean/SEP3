using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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
            HttpResponseMessage response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
            
            string result = await response.Content.ReadAsStringAsync();
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return orders;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var orderAsJson = await client.GetStringAsync($"{uri}/getorder?{orderId}");
            Order order = JsonSerializer.Deserialize<Order>(orderAsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return order;
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