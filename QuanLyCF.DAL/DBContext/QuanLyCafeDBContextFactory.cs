using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace QuanLyCF.DAL.DBContext
{
    public class QuanLyCafeDBContextFactory : IDesignTimeDbContextFactory<QuanLyCafeDBContext>
    {
        public QuanLyCafeDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("QuanLyCafeDB");

            var builder = new DbContextOptionsBuilder<QuanLyCafeDBContext>();
            builder.UseSqlServer(connectionString);

            return new QuanLyCafeDBContext(builder.Options);
        }
    }
}
