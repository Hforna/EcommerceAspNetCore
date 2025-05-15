using EcommerceAspNet.Communication.Response.Product;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using Microsoft.AspNetCore.Http;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class GetProductTests : IClassFixture<ConfigureApplicationTests>
    {
        private readonly ConfigureApplicationTests _app;

        public GetProductTests(ConfigureApplicationTests app) => _app = app;

        [Fact]
        public async Task Product_doesnt_exists()
        {
            var client = await _app.GenerateClientWithToken();
            var sqids = new SqidsEncoder<long>(new SqidsOptions() { Alphabet = "f78D", MinLength = 4 });
            
            var request = await client.GetAsync($"api/product/{sqids.Encode(30)}");

            var response = await request.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, request.StatusCode);
            Assert.Contains("This product doesn't exist", response);
        }

        [Fact]
        public async Task Product_Success()
        {
            var client = await _app.GenerateClientWithToken();
            var sqids = new SqidsEncoder<long>(new SqidsOptions() { Alphabet = "f78D", MinLength = 4 });
            var product = new Product()
            {
                Id = 18,
                Active = true,
                Name = "Iphone 15",
                Price = 1000,
                Stock = 2000,
                CreatedOn = DateTime.UtcNow
            };

            await _app.DbContext.Products.AddAsync(product);
            await _app.DbContext.SaveChangesAsync();

            var request = await client.GetAsync($"api/product/{sqids.Encode(product.Id)}");

            var response = await request.Content.ReadAsStreamAsync();
            var responseAsJson = JsonDocument.Parse(response);
            Assert.Equal(HttpStatusCode.OK, request.StatusCode);
            Assert.Equal(product.Name, responseAsJson.RootElement.GetProperty("name").GetString());
        }
    }
}
