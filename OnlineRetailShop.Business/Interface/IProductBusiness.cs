using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Model;
using System;

namespace OnlineRetailShop.Business.Interface
{
    public interface IProductBusiness
    {
        ContentResult AddProduct(CreateProductInput inputData);
        ContentResult EditProduct(UpdateProductInput inputData);
        ContentResult DeleteProduct(Guid productId);
        ContentResult GetProductById(Guid productId);
        ContentResult GetAllProduct();
    }
}
