using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineRetailShop.Data.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
