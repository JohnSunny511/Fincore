using Fincore.Models;
using System.Data;
using System.Data.SqlClient;

namespace Fincore.DBclasses
{
    public class AccountDBclss
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");

        // Create Account
        public int CreateAccountDB(Accountclss obj)
        {
            SqlCommand cmd = new SqlCommand("sp_CreateAccount", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customer_id", obj.customer_id);
            cmd.Parameters.AddWithValue("@account_type", obj.account_type);

            con.Open();
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return id;
        }

        // Get all accounts of a customer
        public List<Accountclss> GetCustomerAccountsDB(int customer_id)
        {
            List<Accountclss> accounts = new List<Accountclss>();

            SqlCommand cmd = new SqlCommand("sp_GetCustomerAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@customer_id", customer_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Accountclss obj = new Accountclss();

                obj.account_id = Convert.ToInt32(sdr["account_id"]);
                obj.customer_id = Convert.ToInt32(sdr["customer_id"]);
                obj.account_number = Convert.ToInt64(sdr["account_number"]);
                obj.account_type = sdr["account_type"].ToString();
                obj.balance = Convert.ToDecimal(sdr["balance"]);
                obj.account_status = sdr["account_status"].ToString();
                obj.created_date = Convert.ToDateTime(sdr["created_date"]);

                accounts.Add(obj);
            }

            con.Close();
            return accounts;
        }

        // Get account details
        public Accountclss GetAccountDetailsDB(int account_id)
        {
            Accountclss obj = new Accountclss();

            SqlCommand cmd = new SqlCommand("sp_GetAccountDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", account_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                obj.account_id = Convert.ToInt32(sdr["account_id"]);
                obj.customer_id = Convert.ToInt32(sdr["customer_id"]);
                obj.account_number = Convert.ToInt64(sdr["account_number"]);
                obj.account_type = sdr["account_type"].ToString();
                obj.balance = Convert.ToDecimal(sdr["balance"]);
                obj.account_status = sdr["account_status"].ToString();
                obj.created_date = Convert.ToDateTime(sdr["created_date"]);
            }

            con.Close();
            return obj;
        }

        // Get account balance
        public decimal? GetAccountBalanceDB(int account_id)
        {
            SqlCommand cmd = new SqlCommand("sp_GetAccountBalance", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", account_id);

            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();

            if (result == null || result == DBNull.Value)
                return null;

            return Convert.ToDecimal(result);
        }


        // Update Account Status
        public int UpdateAccountStatusDB(int account_id, string account_status)
        {
            SqlCommand cmd = new SqlCommand("sp_UpdateAccountStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", account_id);
            cmd.Parameters.AddWithValue("@account_status", account_status);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();

            return rows;
        }
    }
}
