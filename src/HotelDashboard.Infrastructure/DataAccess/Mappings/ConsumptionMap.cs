using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class ConsumptionMap : IEntityTypeConfiguration<Consumption>
{
    public void Configure(EntityTypeBuilder<Consumption> builder)
    {
        builder.ToTable("consumption");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.IdAccount).HasColumnName("Id_account");
        builder.Property(h => h.Item).HasColumnName("item");
        builder.Property(h => h.Description).HasColumnName("description");
        builder.Property(h => h.Value).HasColumnName("Value");
        builder.Property(h => h.Quantity).HasColumnName("Quantity");
        builder.Property(h => h.Date).HasColumnName("Date");
        builder.Property(h => h.Status).HasColumnName("status");

        builder.HasOne(x => x.Account).WithMany().HasForeignKey(x => x.IdAccount);
    }
}