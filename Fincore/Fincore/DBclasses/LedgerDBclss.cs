using Fincore.Models;
using System.Data;
using System.Data.SqlClient;

namespace Fincore.DBclasses
{
    public class LedgerDBclss
    {

        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");


        // Get Account Ledger
        public List<Ledgerclss> GetAccountLedgerDB(int account_id)
        {
            List<Ledgerclss> ledger = new List<Ledgerclss>();

            SqlCommand cmd = new SqlCommand("sp_GetAccountLedger", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@account_id", account_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Ledgerclss obj = new Ledgerclss();

                obj.ledger_id = Convert.ToInt32(sdr["ledger_id"]);
                obj.transaction_id = Convert.ToInt32(sdr["transaction_id"]);
                obj.account_id = Convert.ToInt32(sdr["account_id"]);
                obj.entry_type = sdr["entry_type"].ToString();
                obj.amount = Convert.ToDecimal(sdr["amount"]);
                obj.created_date = Convert.ToDateTime(sdr["created_date"]);

                ledger.Add(obj);
            }

            con.Close();

            return ledger;
        }

        // Get Transaction Ledger
        public List<Ledgerclss> GetTransactionLedgerDB(int transaction_id)
        {
            List<Ledgerclss> ledger = new List<Ledgerclss>();

            SqlCommand cmd = new SqlCommand("sp_GetTransactionLedger", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@transaction_id", transaction_id);

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Ledgerclss obj = new Ledgerclss();

                obj.ledger_id = Convert.ToInt32(sdr["ledger_id"]);
                obj.transaction_id = Convert.ToInt32(sdr["transaction_id"]);
                obj.account_id = Convert.ToInt32(sdr["account_id"]);
                obj.entry_type = sdr["entry_type"].ToString();
                obj.amount = Convert.ToDecimal(sdr["amount"]);
                obj.created_date = Convert.ToDateTime(sdr["created_date"]);

                ledger.Add(obj);
            }

            con.Close();

            return ledger;
        }
    }
}
