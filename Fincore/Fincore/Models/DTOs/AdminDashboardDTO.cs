namespace Fincore.Models.DTOs
{
    public class AdminDashboardDTO
    {
        public int total_customers { get; set; }
        public int total_accounts { get; set; }
        public int active_accounts { get; set; }
        public int blocked_accounts { get; set; }
        public decimal total_deposits { get; set; }
        public decimal total_withdrawals { get; set; }
        public decimal total_transfers { get; set; }
    }
}
