using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.Migration
{
    public static class DatabaseMigration
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDatabase(connectionString);
            MigrateDatabase(serviceProvider);
        }

        private static void EnsureDatabase(string connectionString)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);
            var dbName = connectionBuilder.InitialCatalog;
            connectionBuilder.Remove(dbName);

            var connectServer = new SqlConnection(connectionBuilder.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            var query = connectServer.Query("SELECT * FROM sys.databases where name = @name", parameters);

            if (query is null)
                connectServer.Execute($"CREATE DATABASE {dbName}");
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();

            runner.MigrateUp();
        }
    }
}
