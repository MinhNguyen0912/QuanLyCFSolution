using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.DAL.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");
            builder.HasKey(p => p.IdBill);
            builder.Property(p => p.DateCheckIn).IsRequired();
            builder.Property(p => p.DateCheckOut).IsRequired(false);
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Discount).IsRequired();
            builder.Property(p => p.IdTable).IsRequired();
            builder.Property(p => p.Note).IsRequired(false);
            builder.HasOne<Table>(p => p.Table).WithMany(p => p.Bills).HasForeignKey(p => p.IdTable);
        }
    }
}
