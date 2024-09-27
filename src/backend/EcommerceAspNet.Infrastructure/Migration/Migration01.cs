using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration
{
    [Migration(1, "add users table")]
    public class Migration01 : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("CreatedOn").AsDateTime().WithDefaultValue(DateTime.UtcNow)
                .WithColumn("Id").AsInt64().ForeignKey().Identity().Unique()
                .WithColumn("Active").AsBoolean().WithDefaultValue(true)
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("UserIdentifier").AsGuid().NotNullable();
        }
    }
}
