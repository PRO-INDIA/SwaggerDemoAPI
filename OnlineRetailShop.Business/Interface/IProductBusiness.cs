using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Data.Entities;
using OnlineRetailShop.Model;
using System;

namespace OnlineRetailShop.Business.Interface
{
    public interface IProductBusiness
    {
        ContentResult AddProduct(ProductInput inputData);
        ContentResult EditProduct(ProductInput inputData);
        ContentResult DeleteProduct(Guid productId);
        ContentResult GetProductById(Guid productId);
       ContentResult GetAllProduct();
    }
}
