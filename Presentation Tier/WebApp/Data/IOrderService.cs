﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int orderId);
        Task<IList<Order>> GetOrdersByUserAsync(string email, string password);
        Task CreateOrderAsync(Order order);
        Task RemoveOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}