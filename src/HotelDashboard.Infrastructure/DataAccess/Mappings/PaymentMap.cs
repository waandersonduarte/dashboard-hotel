using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class PaymentMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payment");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.Amount).HasColumnName("amount");
        builder.Property(h => h.MethodPayment).HasColumnName("method_payment");
        builder.Property(h => h.PaymentDate).HasColumnName("payment_date");
        builder.Property(h => h.Status).HasColumnName("status");

        builder.HasMany(x => x.Allocations).WithOne(x => x.Payment).HasForeignKey(x => x.IdPayment).OnDelete(DeleteBehavior.Restrict);
    }
}