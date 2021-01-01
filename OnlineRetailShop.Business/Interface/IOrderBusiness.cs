using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Model;
using System;

namespace OnlineRetailShop.Business.Interface
{
    public interface IOrderBusiness
    {

        ContentResult GetAllOrder();
        ContentResult GetOrderById(Guid orderId);
        ContentResult AddOrder(OrderInput inputData);
        ContentResult UpdateOrder(Guid orderId, int quantity);
        ContentResult CancelOrder(Guid orderId);
    }
}
