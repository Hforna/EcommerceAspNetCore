using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(5, "add product identifier to products")]
    public class Migration05 : BaseMigration
    {
        public override void Up()
        {
            Alter.Table("products")
                .AddColumn("ProductIdentifier").AsGuid().WithDefaultValue(Guid.NewGuid());
        }
    }
}
