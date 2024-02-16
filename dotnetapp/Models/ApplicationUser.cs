using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace dotnetapp.Models;

public class ApplicationUser : IdentityUser
{
    [MaxLength(30)]
    public string? Name { get; set; }
}




// dotnet dotnet-ef --To check the EF installed or not



// dotnet dotnet-ef migrations add "InitialSetup" --command to setup the initial creation of tables mentioned in DBContext

 

// dotnet dotnet-ef database update --command to update the database