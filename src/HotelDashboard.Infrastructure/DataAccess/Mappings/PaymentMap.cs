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
        builder.Property(h => h.IdReservation).HasColumnName("id_reservation");
        builder.Property(h => h.IdAccount).HasColumnName("id_account");
        builder.Property(h => h.Amount).HasColumnName("amount");
        builder.Property(h => h.MethodPayment).HasColumnName("method_payment");
        builder.Property(h => h.PaymentDate).HasColumnName("payment_date");
        builder.Property(h => h.Status).HasColumnName("status");

        builder.HasOne(x => x.Reservation).WithMany().HasForeignKey(x => x.IdReservation);
        builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.IdAccount);
    }
}