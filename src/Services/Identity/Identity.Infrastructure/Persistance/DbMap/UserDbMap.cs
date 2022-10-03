using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Persistance.DbMap;

internal class UserDbMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.PhoneNumber).HasColumnType("VARCHAR(13)").IsRequired();
        builder.Property(p => p.FirstName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.MiddleName).HasColumnType("VARCHAR(100)");
        builder.Property(p => p.LastName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.InstagramLink).HasColumnType("VARCHAR(100)");
        builder.Property(p => p.RegistrationDate).HasColumnType("TIMESTAMP").IsRequired();
        builder.Property(p => p.Citizenship).HasColumnType("VARCHAR(60)").IsRequired();
        builder.Property(p => p.AvatarLink).HasColumnType("VARCHAR(60)");
    }
}
