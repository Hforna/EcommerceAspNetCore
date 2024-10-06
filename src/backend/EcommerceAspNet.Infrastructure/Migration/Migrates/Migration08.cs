using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(8, "create comments table and user image")]
    public class Migration08 : BaseMigration
    {
        public override void Up()
        {
            CreateTable("comments")
                .WithColumn("Text").AsString()
                .WithColumn("Note").AsInt32()
                .WithColumn("UserId").AsInt64().ForeignKey("FK_COMMENT_USER", "users", "Id").OnDelete(System.Data.Rule.Cascade)
                .WithColumn("ProductId").AsInt64().ForeignKey("FK_COMMENT_PRODUCT", "products", "Id").OnDelete(System.Data.Rule.Cascade);

            Alter.Table("users")
                .AddColumn("ImageIdentifier").AsString().Nullable();
        }
    }
}
