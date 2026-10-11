

using HotelDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelDashboard.Infrastructure.DataAccess.Mappings;

public sealed class GuestMap : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("guest");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(h => h.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
        builder.Property(h => h.CPF).HasColumnName("cpf").IsRequired().HasMaxLength(11);
        builder.Property(h => h.Telephone).HasColumnName("telephone").IsRequired().HasMaxLength(11);
        builder.Property(h => h.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
        builder.Property(h => h.DateRegister).HasColumnName("date_register").IsRequired();
    }
}

