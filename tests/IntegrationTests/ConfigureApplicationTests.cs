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
using Testcontainers.MsSql;

namespace IntegrationTests
{
    public class ConfigureApplicationTests : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .Build();

        public ProjectDbContext DbContext { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ProjectDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ProjectDbContext>(options =>
                    options.UseSqlServer(_sqlContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();

            var options = new DbContextOptionsBuilder<ProjectDbContext>()
                .UseSqlServer(_sqlContainer.GetConnectionString())
                .Options;

            DbContext = new ProjectDbContext(options);

            await DbContext.Database.MigrateAsync();

            await SeedTestData();
        }

        public async Task DeleteTables()
        {
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM comments");
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM coupons");
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM orderItems");
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM orders");
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM categories");
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM products");
        }

        private async Task SeedTestData()
        {
            using var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var userManager = scopedServices.GetRequiredService<UserManager<User>>();
            var roleManager = scopedServices.GetRequiredService<RoleManager<RoleEntitie>>();
            var crypt = scopedServices.GetRequiredService<IPasswordCryptography>();
            var uow = scopedServices.GetRequiredService<IUnitOfWork>();
            var userWrite = scopedServices.GetRequiredService<IUserWriteOnlyRepository>();

            await roleManager.CreateAsync(new RoleEntitie("admin"));
            await roleManager.CreateAsync(new RoleEntitie("customer"));

            var user = new User
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
            await uow.Commit();
            await userManager.AddToRoleAsync(user, "customer");
        }

        public async Task<HttpClient> GenerateClientWithToken()
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/login", new
            {
                email = "testemail@gmail.com",
                password = "testpassword"
            });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ResponseCreateUser>();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", content.TokenGenerated);
                return client;
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new UserException(error);
        }

        public new async Task DisposeAsync()
        {
            if (DbContext != null)
            {
                await DbContext.DisposeAsync();
            }
            await _sqlContainer.DisposeAsync();
        }
    }
}
