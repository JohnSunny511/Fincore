namespace Fincore.Models
{
    public class Ledgerclss
    {
        public int ledger_id { get; set; }
        public int transaction_id { get; set; }
        public int account_id { get; set; }
        public string? entry_type { get; set; }
        public decimal amount { get; set; }
        public DateTime created_date { get; set; }
    }
}
