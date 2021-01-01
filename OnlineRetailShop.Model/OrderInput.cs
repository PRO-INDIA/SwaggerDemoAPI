using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineRetailShop.Model
{
    public class OrderInput
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
