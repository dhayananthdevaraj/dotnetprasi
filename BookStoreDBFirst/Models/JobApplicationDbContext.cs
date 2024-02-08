using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDBFirst.Models;

public class JobApplicationDbContext : IdentityDbContext<IdentityUser>
{

    public JobApplicationDbContext(DbContextOptions<JobApplicationDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Job> Jobs { get; set; }
    public virtual DbSet<Application> Applications { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the foreign key relationship
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobTitle)
            .HasPrincipalKey(j => j.JobTitle);

        base.OnModelCreating(modelBuilder);
    }
}
