using System;
using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Model;

namespace OnlineRetailShop.Business.Interface
{
    public interface ICustomerBusiness
    {
        ContentResult AddCustomer(CreateCustomerInput inputData);
        ContentResult EditCustomer(UpdateCustomerInput inputData);
        ContentResult DeleteCustomer(Guid customerId);
        ContentResult GetCustomerById(Guid customerId);
        ContentResult GetAllCustomer();
    }
}
