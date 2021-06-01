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
        public async Task<ActionResult<Order>> AddOrder([FromBody]Order order)
        {
            try
            {
                Order returned = await orderService.AddOrder(order);
                return Ok(returned);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetOrder")]
        public async Task<ActionResult<Order>> GetOrder([FromQuery]int id)
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
        public async Task<ActionResult<IList<Order>>> GetUserOrders([FromQuery]string email, [FromQuery]string password)
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
        public async Task<ActionResult<Order>> UpdateOrder([FromBody]Order order)
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
        public async Task<ActionResult<Order>> RemoveOrder([FromBody]Order order)
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
        [HttpGet("GetOrdersAdmin")]
        public async Task<ActionResult<IList<Order>>> GetOrdersAdmin([FromQuery]int id, [FromQuery]string email, [FromQuery]string password)
        {
            try
            {
                IList<Order> valid = await orderService.GetOrdersAdmin(id,email,password);
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