using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Table = QuanLyCF.DAL.Entities.Table;

namespace QuanLyCF.DAL.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");
            builder.HasKey(p => p.IdTable);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired();
        }
    }
}
