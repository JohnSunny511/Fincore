namespace Fincore.Models
{
    public class Transactionclss
    {
        public int transaction_id { get; set; }
        public int? from_account_id { get; set; }
        public int? to_account_id { get; set; }
        public string? transaction_type { get; set; }
        public decimal amount { get; set; }
        public string? transaction_status { get; set; }
        public string? description { get; set; }
        public DateTime transaction_date { get; set; }  
    }
}
