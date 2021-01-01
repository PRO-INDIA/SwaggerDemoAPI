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
                ProductId = Guid.Parse("b9f989e0-0831-4914-b547-3abe16ed2637"),
                CustomerId = Guid.Parse("18dc7162-7a05-4e00-a5e0-5b9b1b1d4ea6"),
                Quantity = 100
            };
          
            var addProduct = orderService.AddOrder(order);
            var valConv = JsonConvert.DeserializeObject(addProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result);
            //Assert.Throws<Exception>(() => productService.AddProduct(product));
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
        public void TestEditProductExpectTrue()
        { 
            //Assert.Throws<Exception>(() => productService.EditProduct(product));
            var editProduct = orderService.UpdateOrder(Guid.Parse("cc84a749-c67c-4cb1-856a-eefb4f90c3d0"), 10);
            var valConv = JsonConvert.DeserializeObject(editProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result);
        }

        [Fact]
        public void TestCancelOrderExpectTrue()
        {
            var deleteProduct = orderService.CancelOrder(Guid.Parse("3278a73d-8550-41f1-81e8-af520c6a8983"));
            var valConv = JsonConvert.DeserializeObject(deleteProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result);
            //Assert.False(deleteProduct.Content != "Success");
            //Assert.Throws<Exception>(() => productService.DeleteProduct(product.ProductId));
        }
    }
}
