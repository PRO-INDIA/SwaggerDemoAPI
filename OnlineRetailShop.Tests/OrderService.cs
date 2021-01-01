using System;
using Xunit; 
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Business.Repository;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineRetailShop.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRetailShop.Tests
{
    public class OrderService
    {
        public OnlineRetailShopEntity dbContext;
        public IOrderBusiness orderService;
        private IConfiguration _config;
      
        public OrderService()
        {
            var connection = Configuration.GetConnectionString("DatabaseConnection");
            var options = new DbContextOptionsBuilder<OnlineRetailShopEntity>().UseSqlServer(connection).Options;
            this.dbContext = new OnlineRetailShopEntity(options);
            this.orderService = new OrderBusiness(dbContext);
        }

        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }

        [Fact]
        public void TestAddOrderExpectTrue()
        {
            var order = new OrderInput()
            {
                OrderId = Guid.NewGuid(),
                ProductId = Guid.Parse("0ffde5df-f178-4700-818a-3888aa7a84e2"),
                CustomerId = Guid.Parse("a663a20c-4b0d-4e46-88b4-b6d9d932b7b5"),
                Quantity = 100
            };
          
            var addProduct = orderService.AddOrder(order);
            var valConv = JsonConvert.DeserializeObject(addProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result); 
        }

        [Fact]
        public void TestGetAllOrderExpectTrue()
        {
            var orders = orderService.GetAllOrder();
            var result = JsonConvert.DeserializeObject<List<Order>>(orders.Content);
            var orderResult = result.Where(x => x.OrderId != Guid.Empty);
            Assert.True(orderResult != null);
        }

        [Fact]
        public void TestEditOrderExpectTrue()
        {  
            var editProduct = orderService.UpdateOrder(Guid.Parse("ad696e46-dc2c-4141-8ce8-ac617dde0f67"), 50);
            var valConv = JsonConvert.DeserializeObject(editProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result);
        }

        [Fact]
        public void TestCancelOrderExpectTrue()
        {
            var deleteProduct = orderService.CancelOrder(Guid.Parse("b71a497e-6b83-4139-b87c-3ed8a8975ad8"));
            var valConv = JsonConvert.DeserializeObject(deleteProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result); 
        }
    }
}
