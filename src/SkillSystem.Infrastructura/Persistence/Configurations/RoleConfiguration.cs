using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Infrastructura.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(r => r.RoleId);
        builder.Property(r => r.RoleName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(r => r.Description)
            .IsRequired(false)
            .HasMaxLength(255);
        builder.HasMany(r => r.Users)
            .WithOne(u => u.UserRole)
            .HasForeignKey(u => u.UserRole)
            .OnDelete(DeleteBehavior.Cascade);

    }
}


