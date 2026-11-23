namespace Fincore.Models
{
    public class Accountclss
    {
        public int account_id { get; set; }
        public int customer_id { get; set; }
        public long account_number { get; set; }
        public string? account_type { get; set; }
        public decimal balance { get; set; }
        public string? account_status { get; set; }
        public DateTime created_date { get; set; }
    }
}
