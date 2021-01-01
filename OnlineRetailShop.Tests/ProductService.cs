using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Business.Repository;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Data.Entities;
using OnlineRetailShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineRetailShop.Tests
{
    public class ProductService
    {
        public OnlineRetailShopEntity dbContext;
        public IProductBusiness productService;
        private IConfiguration _config;
       
        public ProductService()
        {
            var connection = Configuration.GetConnectionString("DatabaseConnection");
            var options = new DbContextOptionsBuilder<OnlineRetailShopEntity>().UseSqlServer(connection).Options;
            this.dbContext = new OnlineRetailShopEntity(options);
            this.productService = new ProductBusiness(dbContext);
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
        public void TestAddProductExpectTrue()
        {

            var product = new ProductInput()
            {
                ProductId = Guid.NewGuid(),
                ProductName = "MS",
                Quantity = 150
            };   
            var addProduct = productService.AddProduct(product);
            var valConv = JsonConvert.DeserializeObject(addProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;
            Assert.True(_result); 
        }

        [Fact]
        public void TestGetAllProductExpectTrue()
        {
            var products = productService.GetAllProduct();
            var result = JsonConvert.DeserializeObject<List<Product>>(products.Content);
            var product = result.Where(x => x.ProductId != Guid.Empty);
            Assert.True(product != null);
        }

        [Fact]
        public void TestEditProductExpectTrue()
        {

            var product = new ProductInput()
            {
                ProductId = Guid.Parse("08b342c3-0c51-4f2b-952d-9c70fd4fb9ea"),
                ProductName = "IPhone4S",
                Quantity = 100,
                IsActive = true

            }; 
            var editProduct = productService.EditProduct(product);
            var valConv = JsonConvert.DeserializeObject(editProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false;

            Assert.True(_result);
        }

        [Fact]
        public void TestDeleteProductExpectTrue()
        {
            var deleteProduct = productService.DeleteProduct(Guid.Parse("5e11d404-3925-46d0-8b50-bd3329491cac"));
            var valConv = JsonConvert.DeserializeObject(deleteProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false; 
            Assert.True(_result); 
        }
    }
}
