using System;

namespace OnlineRetailShop.Model
{
    public class CreateProductInput
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateProductInput
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
