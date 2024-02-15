using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Schedule>()
        .HasOne(s => s.Team1)
        .WithMany()
        .HasForeignKey(s => s.Team1Id)
        .OnDelete(DeleteBehavior.Restrict); // Adjust this behavior as needed

    modelBuilder.Entity<Schedule>()
        .HasOne(s => s.Team2)
        .WithMany()
        .HasForeignKey(s => s.Team2Id)
        .OnDelete(DeleteBehavior.Restrict); // Adjust this behavior as needed

    // Configure other relationships

    base.OnModelCreating(modelBuilder);
}

    }
}

