using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Data_PLL
{
    public class AtmDbContextFactory : IDesignTimeDbContextFactory<AtmDbContext>
    {
        public AtmDbContextFactory()
        {

        }

        public AtmDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AtmDbContext>();

            string connectionString = @"Data Source=DESKTOP-BJR8R95\SQLEXPRESS;Initial Catalog=EFCoreAtmAppDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });

            // Test the connection and print the connection status
            using (var dbContext = new AtmDbContext(optionsBuilder.Options))
            {
                try
                {
                    dbContext.Database.OpenConnection();
                    //Console.WriteLine("Database connection successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database connection failed. Error: {ex.Message}");
                }
                finally
                {
                    dbContext.Database.CloseConnection();
                }
            }

            return new AtmDbContext(optionsBuilder.Options);
        }

    }
}
