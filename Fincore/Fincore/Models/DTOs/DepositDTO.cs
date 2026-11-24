namespace Fincore.Models.DTOs
{
    public class DepositDTO
    {
        public int account_id { get; set; }
        public decimal amount { get; set; }
        public string? description { get; set; }
    }
}
