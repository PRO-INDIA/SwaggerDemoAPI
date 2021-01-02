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
                ProductName = "Surface ProX",
                Quantity = 100
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
                ProductId = Guid.Parse("99714c41-e378-44f1-b0b4-4c5eec741909"),
                ProductName = "IPhone5SE",
                Quantity = 200,
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
            var deleteProduct = productService.DeleteProduct(Guid.Parse("ec2e823e-564a-43e3-8193-41cda041a9ce"));
            var valConv = JsonConvert.DeserializeObject(deleteProduct.Content.ToString());
            var _result = valConv.ToString() == "Success" ? true : false; 
            Assert.True(_result); 
        }
    }
}
