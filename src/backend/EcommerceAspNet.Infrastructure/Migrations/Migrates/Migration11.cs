using EcommerceAspNet.Infrastructure.Migration.Migrates;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migrations.Migrates
{
    [Migration(13, "create comments table")]
    public class Migration11 : BaseMigration
    {
        public override void Up()
        {
            CreateTable("comments")
                .WithColumn("Text").AsString().NotNullable()
                .WithColumn("Note").AsInt16().Nullable()
                .WithColumn("ProductId").AsInt64().ForeignKey("FK_PRODUCT_COMMENTS", "products", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("UserId").AsInt64().ForeignKey("FK_USER_COMMENTS", "AspNetUsers", "Id").OnDelete(System.Data.Rule.Cascade);
        }
    }
}
