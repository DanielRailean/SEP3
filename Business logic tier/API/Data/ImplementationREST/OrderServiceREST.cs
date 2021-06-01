using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;
using WebApp.Models;

namespace API.Data.ImplementationREST
{
    public class OrderServiceREST : IOrderService
    {
        private string uri = "http://localhost:8080";
        private HttpClient client;
        private IUserService userService;
        
        JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public OrderServiceREST(IUserService userService)
        {
            this.userService = userService;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }

        public async Task<Order> AddOrder(Order order)
        {
            DataOrder transformed = BusinessToData(order);
            string orderAsJson = JsonSerializer.Serialize(transformed,serializeOptions);
            Console.WriteLine("request"+orderAsJson);
            HttpContent content = new StringContent(
                orderAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri+"/AddOrder", content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataOrder gotOrders = JsonSerializer.Deserialize<DataOrder>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Order returned = DataToBusiness(gotOrders);
            Console.WriteLine("result"+result);
            return returned;
        }

        public async Task<Order> GetOrder(int id)
        {
            HttpResponseMessage response = await client.GetAsync(uri+$"/GetOrder?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataOrder gotOrders = JsonSerializer.Deserialize<DataOrder>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Order returned = DataToBusiness(gotOrders);
            Console.WriteLine("result"+result);
            return returned;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            DataOrder transformed = BusinessToData(order);
            string orderAsJson = JsonSerializer.Serialize(transformed,serializeOptions);
            Console.WriteLine("request"+orderAsJson);
            HttpContent content = new StringContent(
                orderAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PutAsync(uri+"/UpdateOrder", content);
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataOrder gotOrders = JsonSerializer.Deserialize<DataOrder>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Order returned = DataToBusiness(gotOrders);
            Console.WriteLine("result"+result);
            return returned;
        }

        public async Task<Order> RemoveOrder(Order order)
        {
            HttpResponseMessage response = await client.DeleteAsync(uri+$"/RemoveOrder?id={order.Id}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            DataOrder gotOrders = JsonSerializer.Deserialize<DataOrder>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            Order returned = DataToBusiness(gotOrders);
            Console.WriteLine("result"+result);
            return returned;
        }

        public async Task<IList<Order>> GetUserOrders(string email, string password)
        {
            User check = await userService.ValidateUser(email, password);
            if (check == null) throw new Exception("Wrong email or password");
            HttpResponseMessage response = await client.GetAsync(uri+$"/GetUserOrders?userId={check.id}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            List<DataOrder> gotOrders = JsonSerializer.Deserialize<List<DataOrder>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            List<Order> userOrders = new List<Order>();
            foreach (var item in gotOrders)
            {
                userOrders.Add(DataToBusiness(item));
            }
            Console.WriteLine("result"+result);
            return userOrders;
        }

        public async Task<IList<Order>> GetOrdersAdmin(int id, string email, string password)
        {
            User check = await userService.ValidateUser(email, password);
            if (check == null) throw new Exception("Wrong email or password");
            HttpResponseMessage response = await client.GetAsync(uri+$"/GetUserOrders?userId={id}");
            if (!response.IsSuccessStatusCode)
            {
                APIError apiError = JsonSerializer.Deserialize<APIError>(await response.Content.ReadAsStringAsync());
                throw new Exception($@"Error: {apiError.message}");
            }

            string result = await response.Content.ReadAsStringAsync();
            List<DataOrder> gotOrders = JsonSerializer.Deserialize<List<DataOrder>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            List<Order> userOrders = new List<Order>();
            foreach (var item in gotOrders)
            {
                userOrders.Add(DataToBusiness(item));
            }
            Console.WriteLine("result"+result);
            return userOrders;
            
        }
        
        

        public Order DataToBusiness(DataOrder dataOrder)
        {
            Order returned = new Order();
            returned.Items = new List<BasketItem>();
            returned.Id = dataOrder.Id;
            returned.UserId = dataOrder.UserId;
            returned.OrderDate = dataOrder.OrderDate;
            returned.Status = dataOrder.Status;
            returned.Currency = dataOrder.Currency;
            returned.City = dataOrder.City;
            returned.DeliveryAddress = dataOrder.DeliveryAddress;
            returned.IsDelivered = dataOrder.IsDelivered;
            returned.InvoiceAddress = dataOrder.InvoiceAddress;
            returned.PostalCode = dataOrder.PostalCode;
            returned.DeliveryPrice = dataOrder.DeliveryPrice;
            returned.TotalPrice = dataOrder.TotalPrice;
            returned.ItemPrice = dataOrder.ItemPrice;
            List<string> ids = dataOrder.RecipesIdList.Split(",").ToList();
            List<string> quantity = dataOrder.RecipesQuantityList.Split(",").ToList();
            for(int i=0;i<ids.Count-1;i++)
            {
                BasketItem temp = new BasketItem();
                temp.RecipeId = Convert.ToInt32(ids[i]);
                temp.Amount=Convert.ToInt32(quantity[i]);
                returned.Items.Add(temp);
            }
            return returned;
        }

        public DataOrder BusinessToData(Order order)
        {
            DataOrder returned = new DataOrder();
            returned.Id = order.Id;
            returned.UserId = order.UserId;
            returned.OrderDate = order.OrderDate;
            returned.Status = order.Status;
            returned.Currency = order.Currency;
            returned.City = order.City;
            returned.DeliveryAddress = order.DeliveryAddress;
            returned.IsDelivered = order.IsDelivered;
            returned.InvoiceAddress = order.InvoiceAddress;
            returned.PostalCode = order.PostalCode;
            returned.DeliveryPrice = order.DeliveryPrice;
            returned.TotalPrice = order.TotalPrice;
            returned.ItemPrice = order.ItemPrice;
            foreach (var item in order.Items)
            {
                returned.RecipesIdList = returned.RecipesIdList + item.RecipeId+",";
                returned.RecipesQuantityList += item.Amount+",";
            }
            return returned;
        }
    }

}