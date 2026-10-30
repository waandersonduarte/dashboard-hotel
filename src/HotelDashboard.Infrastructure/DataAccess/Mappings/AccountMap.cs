using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("account");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id");
        builder.Property(h => h.IdCheckin).HasColumnName("id_checkin");
        builder.Property(h => h.TotalAmount).HasColumnName("total_amount");
        builder.Property(h => h.DateClosing).HasColumnName("date_closing");
        builder.Property(h => h.Status).HasColumnName("status");

        builder.HasOne(x => x.Checkin).WithMany().HasForeignKey(x => x.IdCheckin);
    }
}