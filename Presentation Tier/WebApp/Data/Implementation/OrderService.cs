using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using WebApp.Models;

namespace WebApp.Data.Implementation
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
        
        /// <summary>
        /// Consumes REST endpoint to get order by
        /// a specific id.
        /// </summary>
        /// <param name="orderId">Id of order to get.</param>
        /// <returns>Order item.</returns>
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

        /// <summary>
        /// Consumes REST endpoint to get order(s) by
        /// a specific user.
        /// </summary>
        /// <param name="user">User item to get the order(s) by.</param>
        /// <returns>List of orders.</returns>
        /// <exception cref="Exception">Response phrase.</exception>
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

        public async Task<IList<Order>> GetOrderAdmin(int id, User user)
        {
            HttpResponseMessage response = await client.GetAsync(uri+$"/GetOrdersAdmin?id={id}&email={user.Email}&password={user.Password}");
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

        /// <summary>
        /// Posts a new order to the REST endpoint,
        /// in JSON format.
        /// </summary>
        /// <param name="order">Order to create.</param>
        /// <exception cref="Exception">Response phrase.</exception>
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

        /// <summary>
        /// Sends a Delete request to the REST endpoint,
        /// to remove a specific order, in JSON format.
        /// </summary>
        /// <param name="order">Order to remove.</param>
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

        /// <summary>
        /// Sends a Put request to the REST endpoint,
        /// to update an existing order, in JSON format.
        /// </summary>
        /// <param name="order">Order to update.</param>
        /// <exception cref="Exception">Response phrase.</exception>
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