namespace Fincore.Models.DTOs
{
    public class TransferDTO
    {
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public decimal amount { get; set; }
        public string? description { get; set; }
    }
}
