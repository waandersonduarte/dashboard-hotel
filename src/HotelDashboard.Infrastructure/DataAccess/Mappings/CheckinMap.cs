using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class CheckinMap : IEntityTypeConfiguration<Checkin>
{
    public void Configure(EntityTypeBuilder<Checkin> builder)
    {
        builder.ToTable("checkin");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.IdGuest).HasColumnName("id_guest");
        builder.Property(h => h.IdRoom).HasColumnName("id_room");
        builder.Property(h => h.EntryDate).HasColumnName("entry_date");
        builder.Property(h => h.DepartureDate).HasColumnName("departure_date");
        builder.Property(h => h.Status).HasColumnName("status");

        builder.HasOne(x => x.Guest).WithMany().HasForeignKey(x => x.IdGuest);
        builder.HasOne(x => x.Room).WithMany().HasForeignKey(x => x.IdRoom);
    }
}