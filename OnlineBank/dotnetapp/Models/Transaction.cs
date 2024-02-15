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

        // Navigation properties
        public User User { get; set; }
        public Account Account { get; set; }
    }
}
