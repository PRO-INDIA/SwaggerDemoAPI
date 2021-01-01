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
                ProductId = Guid.Parse("ec2e823e-564a-43e3-8193-41cda041a9ce"),
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
            var editProduct = orderService.UpdateOrder(Guid.Parse("4806b6bd-c766-4c31-beb7-aae923b39f0f"), 20);
            var valConv = JsonConvert.DeserializeObject(editProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result);
        }

        [Fact]
        public void TestCancelOrderExpectTrue()
        {
            var deleteProduct = orderService.CancelOrder(Guid.Parse("19be237e-2f57-447b-888d-fa8684b9dc84"));
            var valConv = JsonConvert.DeserializeObject(deleteProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result); 
        }
    }
}
