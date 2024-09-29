using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(4, "add user table")]
    public class Migration04 : BaseMigration
    {
        public override void Up()
        {
            CreateTable("users")
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("UserIdentifier").AsGuid().NotNullable();
        }
    }
}
