using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Business.Repository;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Model;
using Xunit;

namespace OnlineRetailShop.Tests
{
    public class CustomerService
    {
        public OnlineRetailShopEntity dbContext;
        public ICustomerBusiness customerService;
        private IConfiguration _config;
        public CustomerService()
        {
            var connection = Configuration.GetConnectionString("DatabaseConnection");
            var options = new DbContextOptionsBuilder<OnlineRetailShopEntity>().UseSqlServer(connection).Options;
            this.dbContext = new OnlineRetailShopEntity(options);
            this.customerService = new CustomerBusiness(dbContext);
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

            var customer = new CreateCustomerInput()
            {
                CustomerName = "Anbu Mani",
                EmailID = "anbu@anbu",
                Mobile = "0000000000"
            };

            var addProduct = customerService.AddCustomer(customer);
            var result = JsonConvert.DeserializeObject(addProduct.Content.ToString());
            Assert.True(result.Equals("Success"));
            //Assert.Throws<Exception>(() => productService.AddProduct(product));
        }
    }
}
