namespace Fincore.Models.DTOs
{
    public class CustomerRegisterDTO
    {
        public string? customer_name { get; set; }
        public int age { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }

        public string? username { get; set; }
        public string? password { get; set; }   
    }
}
