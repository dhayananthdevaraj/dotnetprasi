using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<CricketTournament> CricketTournaments { get; set; }
      
    }
}



//  dotnet new tool-manifest

 

// dotnet tool install --local dotnet-ef --version 6.0.6

 

// dotnet dotnet-ef --To check the EF installed or not



// dotnet dotnet-ef migrations add "InitialSetup" --command to setup the initial creation of tables mentioned in DBContext

 

// dotnet dotnet-ef database update --command to update the database