using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyCF.DAL.Configurations;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.DAL.DBContext
{
    public class QuanLyCafeDBContext : IdentityDbContext<User, Role, Guid>
    {
        public QuanLyCafeDBContext()
        {

        }
        public QuanLyCafeDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new BillConfiguration());
            builder.ApplyConfiguration(new BillInfoConfiguration());
            builder.ApplyConfiguration(new TableConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new FoodConfiguration());

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(p =>new { p.RoleId,p.UserId } );
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(p => p.UserId);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(p => p.UserId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Data Source=.;Initial Catalog=QuanLyCafe;Trusted_Connection=True;TrustServerCertificate=true;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillInfo> BillInfos { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
