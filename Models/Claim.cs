namespace ContractMonthlyClaimsSystemWeb.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string LecturerName { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public string DocumentFilePath { get; set; }  // Path to the uploaded document
        public ClaimStatus Status { get; set; }  // Enum for claim status (Pending, Approved, Rejected)
        public DateTime DateSubmitted { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}