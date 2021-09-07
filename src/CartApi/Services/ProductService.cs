using CartApi.Data;
using System;
using System.Linq;

namespace CartApi.Services
{
    public class ProductService
    {
        private readonly Product[] Products =
        {
            new Product { Name = "T-shirt", Price = 10.99M },
            new Product { Name = "Pants", Price = 14.99M },
            new Product { Name = "Jacket", Price = 19.99M },
            new Product { Name = "Shoes", Price = 24.99M }
        };

        public Product SearchProductByName(string name)
        {
            return Products.Where(p => p.Name == name).FirstOrDefault() ?? throw new ProductNotFoundException();
        }
    }

    public class ProductNotFoundException : Exception {}
}
