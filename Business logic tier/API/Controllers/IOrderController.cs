using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Data
{
    public interface IOrderController
    {
        Task<ActionResult<Order>> AddOrder([FromBody] Order order);
        Task<ActionResult<Order>> GetOrder([FromBody] int id);
        Task<ActionResult<IList<Order>>> GetUserOrders([FromBody] string email, string password);
        Task<ActionResult<Order>> UpdateOrder([FromBody] Order order);
        Task<ActionResult<Order>> RemoveOrder([FromBody] Order order);
        Task<ActionResult<IList<Order>>> GetOrdersAdmin([FromQuery] int id, [FromQuery]string email, [FromQuery]string password );
        
            
        
    }
}