using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public class OrderServiceJSON : IOrderService
    {
        private string OrdersFile = "orders.json";
        private IList<Order> allOrders;
        private IUserService UserService;

        public OrderServiceJSON(IUserService userService)
        {
            this.UserService = userService;
            if (File.Exists(OrdersFile))
            {
                string ordersInJSON = File.ReadAllText(OrdersFile);
                allOrders = JsonSerializer.Deserialize<IList<Order>>(ordersInJSON);
            }
            else
            {
                Seed();
                Save();
            }
        }

        private void Seed()
        {
            Order[] orders =
            {
            };
            allOrders = orders.ToList();
        }

        private void Save()
        {
            string ordersInJson = JsonSerializer.Serialize(allOrders);
            File.WriteAllText(OrdersFile, ordersInJson);
        }

        public async Task<Order> GetOrder(int id)
        {
            var returned = allOrders.First(i => i.Id == id);
            if (returned == null) throw new Exception("Order do not exist");
            return returned;
        }

        public async Task<Order> AddOrder(Order order)
        {
            Order first = null;
            int max = allOrders.Max(i => i.Id);
            order.Id = (++max);
            allOrders.Add(order);
            Save();
            return order;
        }


        public async Task<Order> UpdateOrder(Order order)
        {
            Order toUpdate = allOrders.First(i => i.Id == order.Id);
            if (toUpdate == null) throw new Exception("Order does not exist");
            toUpdate.Status = order.Status;
            Save();
            return toUpdate;
        }

        public async Task<Order> RemoveOrder(Order order)
        {
            Order toRemove = allOrders.First(i => i.Id == order.Id);
            if (toRemove == null) throw new Exception("Order does not exist");
            allOrders.Remove(toRemove);
            Save();
            return toRemove;
        }

        public async Task<IList<Order>> GetUserOrders(string email, string password)
        {
            try
            {
                User user = await UserService.ValidateUser(email, password);
                IList<Order> userOrders = new List<Order>();
                foreach (var order in allOrders)
                {
                    if (order.UserId == user.id)
                    {
                        userOrders.Add(order);
                    }
                }

                return userOrders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}