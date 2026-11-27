using System.Data;
using System.Data.SqlClient;
using Fincore.Models;

namespace Fincore.DBclasses
{
    public class TransactionDBclss
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");

        // Deposit
        public int DepositDB(Transactionclss obj, int user_id, string role)
        {
            SqlCommand cmd = new SqlCommand("sp_Deposit", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@account_id", obj.to_account_id);
            cmd.Parameters.AddWithValue("@amount", obj.amount);
            cmd.Parameters.AddWithValue("@description", obj.description ?? "");

            con.Open();
            int transaction_id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return transaction_id;
        }

        // Withdrawal
        public int WithdrawDB(Transactionclss obj, int user_id, string role)
        {
            SqlCommand cmd = new SqlCommand("sp_Withdraw", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@account_id", obj.from_account_id);
            cmd.Parameters.AddWithValue("@amount", obj.amount);
            cmd.Parameters.AddWithValue("@description", obj.description ?? "");

            con.Open();
            int transaction_id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return transaction_id;
        }

        // Transfer
        public int TransferDB(Transactionclss obj, int user_id, string role)
        {
            SqlCommand cmd = new SqlCommand("sp_Transfer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@from_account_id", obj.from_account_id);
            cmd.Parameters.AddWithValue("@to_account_id", obj.to_account_id);
            cmd.Parameters.AddWithValue("@amount", obj.amount);
            cmd.Parameters.AddWithValue("@description", obj.description ?? "");

            con.Open();
            int transaction_id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return transaction_id;
        }


        // Get Account Transactions
        public List<Transactionclss> GetAccountTransactionsDB(int account_id)
        {
            List<Transactionclss> transactions = new List<Transactionclss>();

            SqlCommand cmd = new SqlCommand("sp_GetAccountTransactions", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", account_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Transactionclss obj = new Transactionclss();

                obj.transaction_id = Convert.ToInt32(sdr["transaction_id"]);

                obj.from_account_id = sdr["from_account_id"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(sdr["from_account_id"]);

                obj.to_account_id = sdr["to_account_id"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(sdr["to_account_id"]);

                obj.transaction_type = sdr["transaction_type"].ToString();
                obj.amount = Convert.ToDecimal(sdr["amount"]);
                obj.transaction_status = sdr["transaction_status"].ToString();
                obj.description = sdr["description"].ToString();
                obj.transaction_date = Convert.ToDateTime(sdr["transaction_date"]);

                transactions.Add(obj);
            }

            con.Close();

            return transactions;
        }

        // Get Transaction Details
        public Transactionclss GetTransactionDetailsDB(int transaction_id)
        {
            Transactionclss obj = new Transactionclss();

            SqlCommand cmd = new SqlCommand("sp_GetTransactionDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@transaction_id", transaction_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                obj.transaction_id = Convert.ToInt32(sdr["transaction_id"]);

                obj.from_account_id = sdr["from_account_id"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(sdr["from_account_id"]);

                obj.to_account_id = sdr["to_account_id"] == DBNull.Value
                    ? null
                    : Convert.ToInt32(sdr["to_account_id"]);

                obj.transaction_type = sdr["transaction_type"].ToString();
                obj.amount = Convert.ToDecimal(sdr["amount"]);
                obj.transaction_status = sdr["transaction_status"].ToString();
                obj.description = sdr["description"].ToString();
                obj.transaction_date = Convert.ToDateTime(sdr["transaction_date"]);
            }

            con.Close();

            return obj;
        }


        public bool IsTransactionOwnedByCustomerDB(int transaction_id, int customer_id)
        {
            SqlCommand cmd = new SqlCommand(
                "sp_CheckTransactionOwnership", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@transaction_id", transaction_id);
            cmd.Parameters.AddWithValue("@customer_id", customer_id);

            con.Open();

            int result = Convert.ToInt32(cmd.ExecuteScalar());

            con.Close();

            return result > 0;
        }   

    }
}
