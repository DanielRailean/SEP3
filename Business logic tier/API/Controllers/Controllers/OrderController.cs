using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrderController : ControllerBase, IOrderController
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            try
            {
                orderService.AddOrder(order);
                return Ok(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetOrder")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                Order valid = await orderService.GetOrder(id);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<IList<Order>>> GetUserOrders(string email, string password)
        {
            try
            {
                IList<Order> valid = await orderService.GetUserOrders(email,password);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Order>> UpdateOrder(Order order)
        {
            try
            {
                Order valid = await orderService.UpdateOrder(order);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Order>> RemoveOrder(Order order)
        {
            try
            {
                Order valid = await orderService.RemoveOrder(order);
                return Ok(valid);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}