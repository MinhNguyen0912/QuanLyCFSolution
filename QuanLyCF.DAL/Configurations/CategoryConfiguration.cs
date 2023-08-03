using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired();
        }
    }
}
