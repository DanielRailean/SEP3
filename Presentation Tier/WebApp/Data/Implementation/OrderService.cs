using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using WebApp.Models;

namespace WebApp.Data
{
    public class OrderService : IOrderService
    {
        private const string uri = "https://localhost:5001/order";
        private readonly HttpClient client;

        public OrderService()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
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
            HttpResponseMessage response = await client.GetAsync(uri+$"/GetUserOrders?email={user.Email}&password={user.Password}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            IList<Order> order = JsonSerializer.Deserialize<IList<Order>>(result, 
                new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return order;
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
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json")
            };
            await client.SendAsync(request);
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