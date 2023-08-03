using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyCF.DAL.Entities;

namespace QuanLyCF.DAL.Configurations
{
    public class BillInfoConfiguration : IEntityTypeConfiguration<BillInfo>
    {
        public void Configure(EntityTypeBuilder<BillInfo> builder)
        {
            builder.ToTable("BillInfos");
            builder.HasKey(p => p.BillInfoId);
            builder.Property(p => p.BillId).IsRequired();
            builder.Property(p => p.FoodId).IsRequired();
            builder.Property(p => p.Count).IsRequired();
            builder.HasOne<Bill>(p => p.Bill).WithMany(p => p.BillInfos).HasForeignKey(p => p.BillId);
            builder.HasOne<Food>(p => p.Food).WithMany(p => p.BillInfos).HasForeignKey(p => p.FoodId);
        }
    }
}
