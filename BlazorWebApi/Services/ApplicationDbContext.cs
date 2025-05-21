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

        modelBuilder.Entity<RaceWowClass>().HasData(
            new RaceWowClass { CharacterRaceId = 17, WowClassId = 1 },
            new RaceWowClass { CharacterRaceId = 17, WowClassId = 2 }
            );

        base.OnModelCreating(modelBuilder);
    }
}
