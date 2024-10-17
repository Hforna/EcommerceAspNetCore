using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    public abstract class BaseMigration : ForwardOnlyMigration
    {
        public FluentMigrator.Builders.Create.Table.ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
        {
            return Create.Table(tableName)
                .WithColumn("Id").AsInt64().ForeignKey().Unique().Identity()
                .WithColumn("CreatedOn").AsDateTime().WithDefaultValue(DateTime.UtcNow)
                .WithColumn("Active").AsBoolean().WithDefaultValue(true);
        }
    }
}
