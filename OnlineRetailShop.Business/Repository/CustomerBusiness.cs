using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Data.Entities;
using OnlineRetailShop.Model;
using System;

namespace OnlineRetailShop.Business.Repository
{
    public class CustomerBusiness : ICustomerBusiness
    {
        public OnlineRetailShopEntity dbContext;
        public CustomerBusiness(OnlineRetailShopEntity onlineRetailShopEntity)
        {
            dbContext = onlineRetailShopEntity;
        }
        public ContentResult AddCustomer(CustomerInput inputData)
        {
            try
            {
                var customer = new Customer()
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = inputData.CustomerName,
                    EmailID = inputData.EmailID,
                    Mobile = inputData.Mobile 
                };

                dbContext.Customers.Add(customer);
                var result=dbContext.SaveChanges();

                if (result is 1)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Success"),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Fail"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
            }
            catch (Exception ex)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(ex.InnerException.ToString()),
                    ContentType = "application/json",
                    StatusCode = 417
                };
            }

        }
    }
}
