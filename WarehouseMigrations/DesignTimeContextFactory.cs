using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;
using WarehouseData;

namespace WarehouseMigrations
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<WarehouseContext>
    {
        private const string LocalConnection = @"Filename=..\..\..\..\WarehouseApi\Warehouse.db";

        private static readonly string MigrationAssemblyName = typeof(DesignTimeContextFactory).Assembly.GetName().Name;

        public WarehouseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WarehouseContext>()
                .UseSqlite(args.FirstOrDefault() ?? LocalConnection,
                op => op.MigrationsAssembly(MigrationAssemblyName));
            return new WarehouseContext(builder.Options);
        }
    }
}