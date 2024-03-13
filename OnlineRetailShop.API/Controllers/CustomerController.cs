using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Model;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineRetailShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        ICustomerBusiness customer;
        public CustomerController(ICustomerBusiness customerBusiness)
        {
            customer = customerBusiness;
        }

        [HttpGet("GetCustomerById")]
        public ContentResult GetCustomerById(Guid customerId)
        {
            return customer.GetCustomerById(customerId);
        }

        [HttpGet("GetAllCustomer")]
        public ContentResult GetAllCustomer()
        {
            return customer.GetAllCustomer();
        }

        [HttpPost("CreateCustomer")]
        public ContentResult CreateCustomer([FromBody] CreateCustomerInput input)
        {
            return customer.AddCustomer(input);
        }

        [HttpPut("EditCustomer")]
        public ContentResult EditCustomer([FromBody] UpdateCustomerInput input)
        {
            return customer.EditCustomer(input);
        }

        [HttpDelete("DeleteCustomer")]
        public ContentResult DeleteCustomer(Guid customerId)
        {
            return customer.DeleteCustomer(customerId);
        }
    }
}
