using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WarehouseMigrations
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Trying migration...");
            var context = new DesignTimeContextFactory().CreateDbContext(args);
            context.Database.Migrate();
            logger.LogInformation("Done.");
        }
    }
}