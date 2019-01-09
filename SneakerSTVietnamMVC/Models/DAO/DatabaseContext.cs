using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace SneakerSTVietnamMVC.Models.DAO
{
    public class DatabaseContext
    {
        private string strConnect = ConfigurationManager.ConnectionStrings["DB_SNEAKERSTV2ConnectionString"].ToString();

        public bool UpdateData(SqlCommand cmd)
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(strConnect);
                conn.Open();
                cmd.Connection = conn;
                trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                trans.Rollback();
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}