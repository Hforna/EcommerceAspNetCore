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
                .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
                .RuleFor(d => d.Name, f => f.Lorem.Sentences())
                .RuleFor(d => d.Id, 1)
                .RuleFor(d => d.Stock, 500);
        }

        public static List<Product> GenerateProducts(int qty)
        {
            var products = new List<Product>();
            for(var i = 1; i <= qty; i++)
            {
                products.Add(new Faker<Product>()
                .RuleFor(d => d.Description, f => f.Lorem.Paragraph())
                .RuleFor(d => d.Name, f => f.Lorem.Sentences())
                .RuleFor(d => d.Stock, 500));
            }
            return products;
        }
    }
}
