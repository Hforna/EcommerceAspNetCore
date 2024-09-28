using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(1, "add users table")]
    public class Migration01 : BaseMigration
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
