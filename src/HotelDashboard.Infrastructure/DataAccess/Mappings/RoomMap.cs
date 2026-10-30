using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

internal sealed class RoomMap : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("room");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id").IsRequired();
        builder.Property(h => h.Number).HasColumnName("number").IsRequired();
        builder.Property(h => h.Type).HasColumnName("type").IsRequired().HasMaxLength(50);
        builder.Property(h => h.Capacity).HasColumnName("capacity").IsRequired();
        builder.Property(h => h.DailyRate).HasColumnName("daily_rate");
        builder.Property(h => h.Status).HasColumnName("status");
    }
}

