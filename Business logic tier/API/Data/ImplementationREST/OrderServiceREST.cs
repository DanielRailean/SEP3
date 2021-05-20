using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.ImplementationREST
{
    public class OrderServiceREST : IOrderService
    {
        private string uri = "https://localhost:8080/Order";
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
            string orderAsJson = JsonSerializer.Serialize(order);
            HttpContent content = new StringContent(
                orderAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Order gotOrders = JsonSerializer.Deserialize<Order>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotOrders;
        }

        public async Task<Order> GetOrder(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(uri + $"?id={@id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {responseMessage.ReasonPhrase}");
            }

            string result = await responseMessage.Content.ReadAsStringAsync();
            Order gotOrder = JsonSerializer.Deserialize<Order>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return gotOrder;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            string orderAsJson = JsonSerializer.Serialize(order);
            Console.WriteLine(orderAsJson);
            HttpContent content = new StringContent(
                orderAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Order updatedOrder = JsonSerializer.Deserialize<Order>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return updatedOrder;
        }

        public async Task<Order> RemoveOrder(Order order)
        {
            HttpResponseMessage response =
                await client.DeleteAsync(uri + $"?order={@order}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.ReasonPhrase}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Order removedOrder = JsonSerializer.Deserialize<Order>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return removedOrder;

        }

        public async Task<IList<Order>> GetUserOrders(string email, string password)
        {
            throw new System.NotImplementedException();
        }


    }

}