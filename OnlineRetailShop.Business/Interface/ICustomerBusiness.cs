using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Model;

namespace OnlineRetailShop.Business.Interface
{
    public interface ICustomerBusiness
    {
        ContentResult AddCustomer(CustomerInput inputData);
    }
}
