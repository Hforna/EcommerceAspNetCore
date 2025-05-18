using CommonUtilities;
using EcommerceAspNet.Communication.Request.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerceAspNet.Domain.Entitie.Ecommerce;
using System.Text.Json;
using EcommerceAspNet.Communication.Response.Product;

namespace IntegrationTests
{
    [Collection(nameof(CollectionTest))]
    public class GetProductsTests
    {
        private readonly ConfigureApplicationTests _app;

        public GetProductsTests(ConfigureApplicationTests app) => _app = app;

        [Fact]
        public async Task Page_Out_Range_No_Content()
        {
            //Arrange
            var client = await _app.GenerateClientWithToken();

            var products = ProductFaker.GenerateProducts(100);
            await _app.DbContext.Products.AddRangeAsync(products);
            await _app.DbContext.SaveChangesAsync();

            //Act
            var response = await client.PostAsJsonAsync("api/product/11", new RequestProducts());

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Success()
        {
            var client = await _app.GenerateClientWithToken();

            var products = ProductFaker.GenerateProducts(50);
            await _app.DbContext.AddRangeAsync(products);
            await _app.DbContext.SaveChangesAsync();

            var request = await client.PostAsJsonAsync("api/product/5", new RequestProducts());
            var response = await request.Content.ReadAsStreamAsync();
            var toJson = JsonDocument.Parse(response);
            Assert.Equal(10, toJson.RootElement.GetProperty("count").GetInt32());
        }
    }
}
