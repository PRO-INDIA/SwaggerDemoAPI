using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      
        [HttpPost("CreateCustomer")] 
        public ContentResult CreateCustomer([FromBody] CustomerInput input)
        {
           return customer.AddCustomer(input);
        }

        
    }
}
