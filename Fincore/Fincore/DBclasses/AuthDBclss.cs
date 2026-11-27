using Fincore.Models;
using System.Data;
using System.Data.SqlClient;

namespace Fincore.DBclasses
{
    public class AuthDBclss
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");

        // Customer Registration
        public int CustomerRegisterDB(Customerclss obj)
        {
            SqlCommand cmd = new SqlCommand("sp_CustomerRegister", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customer_name", obj.customer_name);
            cmd.Parameters.AddWithValue("@age", obj.age);
            cmd.Parameters.AddWithValue("@email", obj.email);
            cmd.Parameters.AddWithValue("@phone", obj.phone);
            cmd.Parameters.AddWithValue("@address", obj.address);
            cmd.Parameters.AddWithValue("@username", obj.username);
            cmd.Parameters.AddWithValue("@password", obj.password);

            con.Open();
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return id;
        }

        // Admin Registration
        public int AdminRegisterDB(Adminclss obj)
        {
            SqlCommand cmd = new SqlCommand("sp_AdminRegister", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@admin_name", obj.admin_name);
            cmd.Parameters.AddWithValue("@admin_email", obj.admin_email);
            cmd.Parameters.AddWithValue("@username", obj.username);
            cmd.Parameters.AddWithValue("@password", obj.password);

            con.Open();
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return id;
        }

        // Login
        public Loginclss LoginDB(Loginclss obj)
        {
            Loginclss login = new Loginclss();

            SqlCommand cmd = new SqlCommand("sp_Login", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", obj.username);
            cmd.Parameters.AddWithValue("@password", obj.password);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                login.login_id = Convert.ToInt32(sdr["login_id"]);
                login.reg_id = Convert.ToInt32(sdr["reg_id"]);
                login.role = sdr["role"].ToString();
            }

            con.Close();

            return login;
        }
    }
}
