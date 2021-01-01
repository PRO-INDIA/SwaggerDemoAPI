using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineRetailShop.Model
{
    public class ProductInput
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
