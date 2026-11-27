namespace Fincore.Models.DTOs
{
    public class TokenResponseDTO
    {
        public string? token { get; set; }
        public int login_id { get; set; }
        public int reg_id { get; set; }
        public string? role { get; set; }
    }
}
