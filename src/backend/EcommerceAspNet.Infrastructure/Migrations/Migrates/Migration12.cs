using EcommerceAspNet.Infrastructure.Migration.Migrates;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migrations.Migrates
{
    [Migration(14, "add UserId on comments table")]
    public class Migration12 : BaseMigration
    {
        public override void Up()
        {
            Alter.Table("comments")
                .AddColumn("UserId").AsInt64().ForeignKey("FK_USER_COMMENT", "AspNetUsers", "Id").OnDelete(System.Data.Rule.Cascade).Nullable();
        }
    }
}
