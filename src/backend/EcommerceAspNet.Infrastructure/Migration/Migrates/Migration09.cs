using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(9, "Create discount coupon table")]
    public class Migration09 : BaseMigration
    {
        public override void Up()
        {
            CreateTable("coupons")
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("valueDiscount").AsFloat().NotNullable()
                .WithColumn("validateData").AsDateTime().WithDefaultValue(DateTime.Now.AddDays(7));               
        }
    }
}