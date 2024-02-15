// FDRequest.cs
namespace dotnetapp.Models
{
    public class FDRequest
    {
        public long RequestId { get; set; }
        public long FDId { get; set; } // Foreign key referencing FixedDeposit
        public string Status { get; set; } // Pending, Approved, Rejected
        // Additional properties as needed

        // Navigation property
        public FixedDeposit FixedDeposit { get; set; }
    }
}
