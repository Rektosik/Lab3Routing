using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsWithRouting.Models;

namespace ProductsWithRouting.Services
{
    public class Data
    {
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Description = "ProductDescription1", Price = 100 },
            new Product { Id = 2, Name = "Product2", Description = "ProductDescription2", Price = 200 },
            new Product { Id = 3, Name = "Product3", Description = "ProductDescription3", Price = 300 }
        };

        public static List<User> Users { get; set; } = new List<User>
        {
            new User { Id = 1, Name = "UserAdmin", Email = "admin@gmail.com" },
            new User { Id = 2, Name = "User1", Email = "user1@gmail.com" }
        };
    }
}