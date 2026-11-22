namespace Fincore.Models
{
    public class Adminclss
    {
        public int admin_id { get; set; }
        public string? admin_name { get; set; }
        public string? admin_email { get; set; }


        //Login
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
