using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineRetailShop.Data.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmailID { get; set; }
        public string Mobile { get; set; }
    }
}
