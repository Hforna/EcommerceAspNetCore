using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Entitie.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserEntitie> Users {  get; set; }
        public DbSet<ProductEntitie> Products { get; set; }
        public DbSet<CategoryEntitie> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItemEntitie> OrderItems { get; set; }
        public DbSet<CommentEntitie> Comments { get; set; }
        public DbSet<DiscountCouponEntitie> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectDbContext).Assembly);
        }
    }
}
