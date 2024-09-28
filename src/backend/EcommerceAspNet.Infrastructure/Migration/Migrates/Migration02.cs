﻿using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration.Migrates
{
    [Migration(2, "create base ecommerce tables")]
    public class Migration02 : BaseMigration
    {
        public override void Up()
        {
            CreateTable("products")
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString()
                .WithColumn("Price").AsFloat().NotNullable()
                .WithColumn("Stock").AsInt64()
                .WithColumn("CategoryId").AsInt64().ForeignKey();

            CreateTable("orders")
                .WithColumn("UserId").AsInt64().ForeignKey();

            CreateTable("orderItems")
                .WithColumn("productId").AsInt64().ForeignKey()
                .WithColumn("orderId").AsInt64().ForeignKey()
                .WithColumn("Quantity").AsInt32()
                .WithColumn("UnitPrice").AsFloat();
        }
    }
}
