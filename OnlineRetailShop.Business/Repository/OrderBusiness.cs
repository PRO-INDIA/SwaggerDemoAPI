using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Data.Entities;
using OnlineRetailShop.Model;
using System;
using System.Linq;

namespace OnlineRetailShop.Business.Repository
{
    public class OrderBusiness : IOrderBusiness
    {
        public OnlineRetailShopEntity dbContext;
        public OrderBusiness(OnlineRetailShopEntity onlineRetailShopEntity)
        {
            dbContext = onlineRetailShopEntity;
        }
        public ContentResult AddOrder(OrderInput inputData)
        {
            try
            {
                var order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    ProductId = inputData.ProductId,
                    CustomerId = inputData.CustomerId,
                    Quantity = inputData.Quantity,
                    IsCancel = false,

                };

                dbContext.Orders.Add(order);

                var getProduct = dbContext.Products.Where(x => x.ProductId == inputData.ProductId).FirstOrDefault();

                if (getProduct is null || inputData.Quantity > getProduct.Quantity)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Product Unavailable"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };
                }
                else
                {

                    getProduct.Quantity -= inputData.Quantity; //Reduced Product Quantity
                    dbContext.Products.Update(getProduct);  //Update Product

                    var result = dbContext.SaveChanges();

                    if (result is 2)
                    {
                        return new ContentResult
                        {
                            Content = JsonConvert.SerializeObject("Success"),
                            ContentType = "application/json",
                            StatusCode = 200
                        };
                    }
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Fail"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(ex.InnerException.ToString()),
                    ContentType = "application/json",
                    StatusCode = 417
                };
            }
        }
        public ContentResult CancelOrder(Guid orderId)
        {
            var getOrder = dbContext.Orders.Where(x => x.OrderId == orderId && x.IsCancel != true).FirstOrDefault();

            if (getOrder is null)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Invalid OrderId"),
                    ContentType = "application/json",
                    StatusCode = 204
                };
            }
            else
            {
                getOrder.IsCancel = true;
                dbContext.Orders.Update(getOrder);

                var getProduct = dbContext.Products.Where(x => x.ProductId == getOrder.ProductId).FirstOrDefault();

                if (getProduct != null)
                {
                    getProduct.Quantity += getOrder.Quantity;
                    dbContext.Products.Update(getProduct);
                }

                var result = dbContext.SaveChanges();

                if (result is 2)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Success"),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Fail"),
                    ContentType = "application/json",
                    StatusCode = 204
                };

            }
        }
        public ContentResult GetAllOrder()
        {
            try
            {
                var orders = dbContext.Orders.Where(x => x.IsCancel != true).ToList();
                if (orders is null || orders.Count <= 0)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("No Orders Available"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(orders),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }

            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(ex.InnerException.ToString()),
                    ContentType = "application/json",
                    StatusCode = 417
                };
            }
        }
        public ContentResult GetOrderById(Guid orderId)
        {
            try
            {
                var order = dbContext.Orders.Where(x => x.OrderId == orderId && x.IsCancel != true).FirstOrDefault();
                if (order is null)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Order Not Avalible"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(order),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }

            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(ex.InnerException.ToString()),
                    ContentType = "application/json",
                    StatusCode = 417
                };
            }
        }
        public ContentResult UpdateOrder(Guid orderId, int quantity)
        {
            var getOrder = dbContext.Orders.Where(x => x.OrderId == orderId && x.IsCancel != true).FirstOrDefault();

            if (getOrder is null)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Invalid OrderId"),
                    ContentType = "application/json",
                    StatusCode = 204
                };
            }
            else
            {
                var getProduct = dbContext.Products.Where(x => x.ProductId == getOrder.ProductId).FirstOrDefault();

                if (getProduct != null)
                {
                    getProduct.Quantity += (getOrder.Quantity - quantity);
                    dbContext.Products.Update(getProduct);
                }

                getOrder.Quantity = quantity;
                dbContext.Orders.Update(getOrder);
                var result = dbContext.SaveChanges();

                if (result is 2)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Success"),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Fail"),
                    ContentType = "application/json",
                    StatusCode = 204
                };

            }
        }
    }
}
