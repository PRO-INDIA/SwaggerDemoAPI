using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Data.DBContext;
using OnlineRetailShop.Data.Entities;
using OnlineRetailShop.Model;
using System;
using System.Linq;

namespace OnlineRetailShop.Business.Repository
{
    public class CustomerBusiness : ICustomerBusiness
    {
        public OnlineRetailShopEntity dbContext;
        public CustomerBusiness(OnlineRetailShopEntity onlineRetailShopEntity)
        {
            dbContext = onlineRetailShopEntity;
        }

        public ContentResult GetCustomerById(Guid customerId)
        {
            try
            {
                var customer = dbContext.Customers.Where(x => x.CustomerId == customerId).FirstOrDefault();
                if (customer is null)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Customer Not Found"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(customer),
                        ContentType = "application/json",
                        StatusCode = 200
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

        public ContentResult GetAllCustomer()
        {
            try
            {
                var customer = dbContext.Customers.ToList();
                if (customer.Count is 0)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("No Customers Found"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(customer),
                        ContentType = "application/json",
                        StatusCode = 200
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

        public ContentResult AddCustomer(CreateCustomerInput inputData)
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
                var result = dbContext.SaveChanges();

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

        public ContentResult EditCustomer(UpdateCustomerInput inputData)
        {
            try
            {
                var customer = dbContext.Customers.FirstOrDefault(x => x.CustomerId == inputData.CustomerId);
                if (customer is null)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Invaild Customer Id"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
                else
                {
                    customer.CustomerName = inputData.CustomerName;
                    customer.Mobile = inputData.Mobile;
                    customer.EmailID = inputData.EmailID;
                    dbContext.Customers.Update(customer);
                    var result = dbContext.SaveChanges();

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
        public ContentResult DeleteCustomer(Guid customerId)
        {
            var customer = dbContext.Customers.FirstOrDefault(x => x.CustomerId == customerId);
            if (customer is null)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Customer Not Found"),
                    ContentType = "application/json",
                    StatusCode = 204
                };
            }
            else
            {
                dbContext.Customers.Remove(customer);
                var result = dbContext.SaveChanges();

                if (result is 1)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Success"),
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Fail"),
                    ContentType = "application/json",
                    StatusCode = 204
                };
            }
        }
    }
}
