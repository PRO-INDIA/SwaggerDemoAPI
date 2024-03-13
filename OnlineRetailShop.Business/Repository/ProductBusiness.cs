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
    public class ProductBusiness : IProductBusiness
    {
        public OnlineRetailShopEntity dbContext;
        public ProductBusiness(OnlineRetailShopEntity onlineRetailShopEntity)
        {
            dbContext = onlineRetailShopEntity;
        }
        public ContentResult GetAllProduct()
        {
            try
            {
                var product = dbContext.Products.Where(x => x.IsActive).ToList();
                if (product.Count is 0)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("No Product Available"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };
                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(product),
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
        public ContentResult GetProductById(Guid productId)
        {
            try
            {
                var product = dbContext.Products.Where(x => x.ProductId == productId && x.IsActive).FirstOrDefault();
                if (product is null)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Product Not Available"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject(product),
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
        public ContentResult AddProduct(CreateProductInput inputData)
        {
            try
            {
                var pro = dbContext.Products.FirstOrDefault(x => x.ProductName == inputData.ProductName);
                if (pro is null)
                {
                    var product = new Product()
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = inputData.ProductName,
                        Quantity = inputData.Quantity,
                        IsActive = true
                    };

                    dbContext.Products.Add(product);
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
                else
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Entered Product Name Already Exists"),
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
        public ContentResult EditProduct(UpdateProductInput inputData)
        {
            try
            {
                var product = dbContext.Products.FirstOrDefault(x => x.ProductId == inputData.ProductId);
                if (product is null)
                {
                    return new ContentResult
                    {
                        Content = JsonConvert.SerializeObject("Invaild Product Id"),
                        ContentType = "application/json",
                        StatusCode = 204
                    };

                }
                else
                {
                    product.ProductName = inputData.ProductName;
                    product.Quantity = inputData.Quantity;
                    product.IsActive = inputData.IsActive;
                    dbContext.Products.Update(product);
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
        public ContentResult DeleteProduct(Guid productId)
        {
            var product = dbContext.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product is null)
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject("Product Not Avalible"),
                    ContentType = "application/json",
                    StatusCode = 204
                };
            }
            else
            {
                dbContext.Products.Remove(product);
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
