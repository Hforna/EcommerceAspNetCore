using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(7, "add name to orderItems table")]
    public class Migration07 : BaseMigration
    {
        public override void Up()
        {
            Alter.Table("orderItems")
                .AddColumn("Name").AsString();
        }
    }
}
