using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class ChargeMap : IEntityTypeConfiguration<Charge>
{
    public void Configure(EntityTypeBuilder<Charge> builder)
    {
        builder.ToTable("charge");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.IdReservation).HasColumnName("id_reservation");
        builder.Property(h => h.IdAccount).HasColumnName("id_account");
        builder.Property(h => h.IdConsumption).HasColumnName("id_consumption");
        builder.Property(h => h.Type).HasColumnName("type");
        builder.Property(h => h.Description).HasColumnName("description");
        builder.Property(h => h.Quantity).HasColumnName("quantity");
        builder.Property(h => h.UnitAmount).HasColumnName("unit_amount");
        builder.Property(h => h.Amount).HasColumnName("amount");
        builder.Property(h => h.Date).HasColumnName("date");

        builder.HasOne(x => x.Reservation).WithMany().HasForeignKey(x => x.IdReservation);
        builder.HasOne(x => x.Consumption).WithMany().HasForeignKey(x => x.IdConsumption);
        builder.HasOne(x => x.Account).WithMany(x => x.Charges).HasForeignKey(x => x.IdAccount);
        builder.HasMany(x => x.Allocations).WithOne(x => x.Charge).HasForeignKey(x => x.IdCharge).OnDelete(DeleteBehavior.Restrict);
    }
}