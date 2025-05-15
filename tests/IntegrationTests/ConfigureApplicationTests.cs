using EcommerceAspNet.Communication.Response.User;
using EcommerceAspNet.Domain.Entitie.Identity;
using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.Security;
using EcommerceAspNet.Domain.Repository.Security.Cryptography;
using EcommerceAspNet.Domain.Repository.User;
using EcommerceAspNet.Exception.Exception;
using EcommerceAspNet.Infrastructure.DataEntity;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class ConfigureApplicationTests : WebApplicationFactory<Program>
    {
        public ProjectDbContext DbContext;
        private readonly IServiceProvider _serviceProvider;

        public ConfigureApplicationTests()
        {
            _serviceProvider = Services;
            var scope = _serviceProvider.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async service =>
            {
                var verifyDbContext = service.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProjectDbContext>));

                if (verifyDbContext is not null)
                    service.Remove(verifyDbContext);

                service.AddDbContext<ProjectDbContext>(d =>
                {
                    d.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                using var scope = service.BuildServiceProvider().CreateScope();
                var uof = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var userWrite = scope.ServiceProvider.GetRequiredService<IUserWriteOnlyRepository>();
                var crypt = scope.ServiceProvider.GetRequiredService<IPasswordCryptography>();
                var userS = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roles = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntitie>>();
                await roles.CreateAsync(new RoleEntitie("admin"));
                await roles.CreateAsync(new RoleEntitie("customer"));
                
                var user = new User()
                {
                    Active = true,
                    Email = "testemail@gmail.com",
                    Password = crypt.Encrypt("testpassword"),
                    EmailConfirmed = true,
                    UserName = "testuser",
                    UserIdentifier = Guid.NewGuid(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                await userWrite.Add(user);
                await uof.Commit();

                await userS.AddToRoleAsync(user, "customer");
                
            });
            base.ConfigureWebHost(builder);
        }

        public async Task<HttpClient> GenerateClientWithToken()
        {
            var client = this.CreateClient();

            var request = await client.PostAsJsonAsync("api/login", new { email = "testemail@gmail.com", password = "testpassword" });

            var response = await request.Content.ReadAsStringAsync();
            if(request.IsSuccessStatusCode)
            {
                var serializer = JsonSerializer.Deserialize<ResponseCreateUser>(response);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", serializer.TokenGenerated);

                return client;
            }
            throw new UserException(response);
        }
    }
}
