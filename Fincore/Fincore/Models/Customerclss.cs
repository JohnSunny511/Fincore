namespace Fincore.Models
{
    public class Customerclss
    {
        public int customer_id { get; set; }
        public string? customer_name { get; set; }
        public int age { get; set; }
        public string? email {  get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }

        //Login Details
        public string? username { get;set; }
        public string? password { get; set; }
    }
}
