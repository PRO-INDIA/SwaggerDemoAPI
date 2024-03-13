using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Business.Interface;
using OnlineRetailShop.Model;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineRetailShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductBusiness product;
        public ProductController(IProductBusiness productBusiness)
        {
            product = productBusiness;
        }

        [HttpGet("GetProductById")]
        public ContentResult GetProductById(Guid productId)
        {
            return product.GetProductById(productId);
        }

        [HttpGet("GetAllProduct")]
        public ContentResult GetAllProduct()
        {
            return product.GetAllProduct();
        } 

        [HttpPost("AddProduct")]
        public ContentResult AddProduct([FromBody] CreateProductInput productInput)
        {
            return product.AddProduct(productInput);
        }

        [HttpPut("EditProduct")]
        public ContentResult EditProduct([FromBody] UpdateProductInput productInput)
        {
            return product.EditProduct(productInput);
        }

        [HttpDelete("DeleteProduct")]
        public ContentResult DeleteProduct(Guid productId)
        {
            return product.DeleteProduct(productId);
        } 
    }
}
