// Transaction.cs
using System;

namespace dotnetapp.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public long UserId { get; set; } // Foreign key referencing User
        public long AccountId { get; set; } // Foreign key referencing Account
        public string Type { get; set; } // Credit or Debit
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        // Additional properties as needed
        public User User { get; set; }
        public Account Account { get; set; }
    }
}


// dotnet new tool-manifest
 
 
// dotnet tool install --local dotnet-ef --version 6.0.6
 
 
// dotnet dotnet-ef --To check the EF installed or not
 
 
// dotnet dotnet-ef migrations add "InitialSetup" --command to setup the initial creation of tables mentioned in DBContext
 
 
// dotnet dotnet-ef database update --command to update the database