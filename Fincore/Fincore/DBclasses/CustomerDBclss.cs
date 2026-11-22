using System.Data.SqlClient;
using System.Data;
using Fincore.Models;

namespace Fincore.DBclasses
{
    public class CustomerDBclss
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");
        public Customerclss GetCustomerDB(int id)
        {
            Customerclss cust = new Customerclss();

            SqlCommand cmd = new SqlCommand("sp_GetCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customer_id", id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                cust.customer_id = Convert.ToInt32(sdr["customer_id"]);
                cust.customer_name = sdr["customer_name"].ToString();
                cust.age = Convert.ToInt32(sdr["age"]);
                cust.email = sdr["email"].ToString();
                cust.phone = sdr["phone"].ToString();
                cust.address = sdr["address"].ToString();
            }

            con.Close();

            return cust;
        }


}
}
