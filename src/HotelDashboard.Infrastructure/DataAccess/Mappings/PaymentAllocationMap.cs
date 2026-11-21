using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class PaymentAllocationMap : IEntityTypeConfiguration<PaymentAllocation>
{
    public void Configure(EntityTypeBuilder<PaymentAllocation> builder)
    {
        builder.ToTable("payment_allocation");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.IdPayment).HasColumnName("id_payment");
        builder.Property(h => h.IdCharge).HasColumnName("id_charge");
        builder.Property(h => h.Amount).HasColumnName("amount");

        builder.HasOne(x => x.Charge).WithMany(x => x.Allocations).HasForeignKey(x => x.IdCharge);
        builder.HasOne(x => x.Payment).WithMany(x => x.Allocations).HasForeignKey(x => x.IdPayment);
    }
}