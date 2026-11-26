using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class CheckoutMap : IEntityTypeConfiguration<Checkout>
{
    public void Configure(EntityTypeBuilder<Checkout> builder)
    {
        builder.ToTable("checkout");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.IdCheckin).HasColumnName("id_checkin");
        builder.Property(h => h.CheckoutDate).HasColumnName("checkout_Date");

        builder.HasOne(x => x.Checkin).WithMany().HasForeignKey(x => x.IdCheckin);
    }
}