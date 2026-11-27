using System.Data;
using System.Data.SqlClient;

namespace Fincore.DBclasses
{
    public class AuditDBclss
    {
                SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=FincoreDB;Integrated Security=true;");


        public void InsertAuditLogDB(
            int user_id,
            string role,
            string action,
            string description)
        {
            SqlCommand cmd = new SqlCommand("sp_InsertAuditLog", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@action", action);
            cmd.Parameters.AddWithValue("@description", description);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}