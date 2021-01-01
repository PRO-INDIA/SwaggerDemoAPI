using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineRetailShop.Data.Entities;

namespace OnlineRetailShop.Data.DBContext
{
    public class OnlineRetailShopEntity : DbContext
    {
        public OnlineRetailShopEntity(DbContextOptions<OnlineRetailShopEntity> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
       
    }
}
