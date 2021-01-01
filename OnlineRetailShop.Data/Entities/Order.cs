using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineRetailShop.Data.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }       

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }      
        public int Quantity { get; set; }
        public bool IsCancel { get; set; } 
        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
