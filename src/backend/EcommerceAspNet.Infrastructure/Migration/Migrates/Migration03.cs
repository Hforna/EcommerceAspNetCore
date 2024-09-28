using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(3, "create categories table")]
    public class Migration03 : BaseMigration
    {
        public override void Up()
        {
            CreateTable("categories")
                .WithColumn("Name").AsString().NotNullable();
        }
    }
}
