using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(10, "add group price on products")]
    public class Migration10 : BaseMigration
    {
        public override void Up()
        {
            Alter.Table("products")
                .AddColumn("groupPrice").AsInt32().NotNullable();
        }
    }
}
