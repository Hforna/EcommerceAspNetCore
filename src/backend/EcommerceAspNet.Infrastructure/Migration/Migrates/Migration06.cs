using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(6, "add total price to order table")]
    public class Migration06 : BaseMigration
    {
        public override void Up()
        {
            Alter.Table("orders")
                .AddColumn("TotalPrice").AsFloat().NotNullable();
        }
    }
}
