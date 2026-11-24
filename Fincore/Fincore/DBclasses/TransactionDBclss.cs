using System.Data;
using System.Data.SqlClient;
using Fincore.Models;

namespace Fincore.DBclasses
{
    public class TransactionDBclss
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");

        // Deposit
        public int DepositDB(Transactionclss obj)
        {
            SqlCommand cmd = new SqlCommand("sp_Deposit", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", obj.to_account_id);
            cmd.Parameters.AddWithValue("@amount", obj.amount);
            cmd.Parameters.AddWithValue("@description", obj.description ?? "");

            con.Open();
            int transaction_id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return transaction_id;
        }

        // Withdrawal
        public int WithdrawDB(Transactionclss obj)
        {
            SqlCommand cmd = new SqlCommand("sp_Withdraw", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", obj.from_account_id);
            cmd.Parameters.AddWithValue("@amount", obj.amount);
            cmd.Parameters.AddWithValue("@description", obj.description ?? "");

            con.Open();
            int transaction_id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return transaction_id;
        }

        // Transfer
        public int TransferDB(Transactionclss obj)
        {
            SqlCommand cmd = new SqlCommand("sp_Transfer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@from_account_id", obj.from_account_id);
            cmd.Parameters.AddWithValue("@to_account_id", obj.to_account_id);
            cmd.Parameters.AddWithValue("@amount", obj.amount);
            cmd.Parameters.AddWithValue("@description", obj.description ?? "");

            con.Open();
            int transaction_id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return transaction_id;
        }


    }
}
