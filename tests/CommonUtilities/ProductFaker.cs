using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using EcommerceAspNet.Domain.Entitie.Ecommerce;

namespace CommonUtilities
{
    public static class ProductFaker
    {
        public static Product GenerateProduct()
        {
            return new Faker<Product>()
                .RuleFor(d => d.Description, f => f.Lorem.Sentence(10))
                .RuleFor(d => d.Name, f => f.Commerce.ProductName())
                .RuleFor(d => d.Stock, 500)
                .Ignore(d => d.Id);
        }

        public static List<Product> GenerateProducts(int qty)
        {
            var products = new List<Product>();
            for(var i = 1; i <= qty; i++)
            {
                products.Add(new Faker<Product>()
                .RuleFor(d => d.Description, f => f.Lorem.Sentence(10))
                .RuleFor(d => d.Name, f => f.Commerce.ProductName())
                .RuleFor(d => d.Stock, 500)
                .Ignore(d => d.Id));
            }
            return products;
        }
    }
}
