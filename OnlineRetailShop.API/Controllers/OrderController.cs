using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Model;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineRetailShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderBusiness order;
        public OrderController(IOrderBusiness orderBusiness)
        {
            order = orderBusiness;
        }
        [HttpGet("GetOrderById")]
        public ContentResult GetOrderById(Guid productId)
        {
            return order.GetOrderById(productId);
        }

        [HttpGet("GetAllOrder")]
        public ContentResult GetAllOrder()
        {
            return order.GetAllOrder();
        }

        [HttpPost("AddOrder")]
        public ContentResult AddOrder([FromBody] CreateOrderInput orderInput)
        {
            return order.AddOrder(orderInput);
        }

        [HttpPut("UpdateOrder")]
        public ContentResult UpdateOrder(Guid orderId, int quantity)
        {
            return order.UpdateOrder(orderId, quantity);
        }

        [HttpPut("CancelOrder")]
        public ContentResult CancelOrder(Guid orderId)
        {
            return order.CancelOrder(orderId);
        }
    }
}
