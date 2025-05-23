using BlazorWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebApi.Services;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }


   public DbSet<Factions> Factions { get; set; }
   public DbSet<WowClass> WowClasses { get; set; }
   public DbSet<CharacterRace> CharacterRaces { get; set; }
   public DbSet<Roles> Roles { get; set; }
   public DbSet<Specialization> Specializations { get; set; }
   public DbSet<RaceWowClass> RaceWowClasses { get; set; }

   public DbSet<TalentNode> TalentTree { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RaceWowClass>()
      .HasKey(rc => new { rc.CharacterRaceId, rc.WowClassId });

        modelBuilder.Entity<RaceWowClass>()
            .HasOne(rc => rc.CharacterRace)
            .WithMany(r => r.RaceWowClasses)
            .HasForeignKey(rc => rc.CharacterRaceId);

        modelBuilder.Entity<RaceWowClass>()
            .HasOne(rc => rc.WowClass)
            .WithMany(wc => wc.RaceWowClasses)
            .HasForeignKey(rc => rc.WowClassId);

        modelBuilder.Entity<Specialization>()
            .HasOne(s => s.WowClass)
            .WithMany(w => w.Specializations)
            .HasForeignKey(s => s.WowClassId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Specialization>()
            .HasOne(s => s.Role)
            .WithMany(r => r.ClassSpecializations)
            .HasForeignKey(s => s.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TalentNode>()
            .HasOne(t => t.Parent)
            .WithMany(t => t.Children)
            .HasForeignKey(t => t.ParentId)
            .OnDelete(DeleteBehavior.Restrict);



        base.OnModelCreating(modelBuilder);
    }
}
