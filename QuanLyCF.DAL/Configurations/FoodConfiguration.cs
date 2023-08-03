using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.DAL.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("Foods");
            builder.HasKey(p => p.FoodId);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasOne<Category>(p => p.Category).WithMany(p => p.Foods).HasForeignKey(p => p.CategoryId);
        }
    }
}
