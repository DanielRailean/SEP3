using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            var orderAsJson = await client.GetStringAsync($"{uri}/getorder?id={orderId}");
            Order order = JsonSerializer.Deserialize<Order>(orderAsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return order;
        }

        public async Task<IList<Order>> GetOrdersByUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateOrderAsync(Order order)
        {
            var orderAsJson = JsonSerializer.Serialize(order);
            HttpContent content = new StringContent(orderAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task RemoveOrderAsync(Order order)
        {
            await client.DeleteAsync(uri);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var orderAsJson = JsonSerializer.Serialize(order);
            HttpContent content = new StringContent(orderAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync($"{uri}/{order.Id}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}