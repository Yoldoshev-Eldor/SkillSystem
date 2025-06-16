using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SkillSystem.Infrastructure.Data;

public class SkillSystemDbContext : DbContext
{
    public DbSet<Skill> Skills { get; set; }

    public SkillSystemDbContext(DbContextOptions<SkillSystemDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Skill>()
            .Property(s => s.Category)
            .IsRequired()
            .HasMaxLength(50);
    }
}