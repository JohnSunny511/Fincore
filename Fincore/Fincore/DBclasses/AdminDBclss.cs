using System.Data;
using System.Data.SqlClient;
using Fincore.Models;
using Fincore.Models.DTOs;



namespace Fincore.DBclasses
{
    public class AdminDBclss
    {

        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");


        // Get All Customers
        public List<Customerclss> GetAllCustomersDB()
        {
            List<Customerclss> customers = new List<Customerclss>();

            SqlCommand cmd = new SqlCommand("sp_GetAllCustomers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Customerclss obj = new Customerclss();

                obj.customer_id = Convert.ToInt32(sdr["customer_id"]);
                obj.customer_name = sdr["customer_name"].ToString();
                obj.age = Convert.ToInt32(sdr["age"]);
                obj.email = sdr["email"].ToString();
                obj.phone = sdr["phone"].ToString();
                obj.address = sdr["address"].ToString();

                customers.Add(obj);
            }

            con.Close();
            return customers;
        }

        // Get All Accounts
        public List<Accountclss> GetAllAccountsDB()
        {
            List<Accountclss> accounts = new List<Accountclss>();

            SqlCommand cmd = new SqlCommand("sp_GetAllAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;

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

        // Get All Transactions
        public List<Transactionclss> GetAllTransactionsDB()
        {
            List<Transactionclss> transactions = new List<Transactionclss>();

            SqlCommand cmd = new SqlCommand("sp_GetAllTransactions", con);
            cmd.CommandType = CommandType.StoredProcedure;

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

        // Get Bank Statistics
        public AdminDashboardDTO GetBankStatisticsDB()
        {
            AdminDashboardDTO obj = new AdminDashboardDTO();

            SqlCommand cmd = new SqlCommand("sp_GetBankStatistics", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                obj.total_customers = Convert.ToInt32(sdr["total_customers"]);
                obj.total_accounts = Convert.ToInt32(sdr["total_accounts"]);
                obj.active_accounts = Convert.ToInt32(sdr["active_accounts"]);
                obj.blocked_accounts = Convert.ToInt32(sdr["blocked_accounts"]);
                obj.total_deposits = Convert.ToDecimal(sdr["total_deposits"]);
                obj.total_withdrawals = Convert.ToDecimal(sdr["total_withdrawals"]);
                obj.total_transfers = Convert.ToDecimal(sdr["total_transfers"]);
            }

            con.Close();
            return obj;
        }


    }
}
