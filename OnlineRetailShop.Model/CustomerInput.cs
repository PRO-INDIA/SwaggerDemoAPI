using System;

namespace OnlineRetailShop.Model
{
    public class CreateCustomerInput
    {
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string EmailID { get; set; }
    }

    public class UpdateCustomerInput
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string EmailID { get; set; }
    }
}
