using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SneakerSTVietnamMVC.Models.DAO;

namespace SneakerSTVietnamMVC.Models
{

    public class BusinessDAO
    {
        private string strConnect = ConfigurationManager.ConnectionStrings["DB_SNEAKERSTV2ConnectionString"].ToString();
        public List<BusinessDataView> GetBusinessGrantedByRoleID(int roleID)
        {
            SqlConnection conn = null;
            SqlDataReader dr1 = null;

            List<BusinessDataView> businessList = new List<BusinessDataView>();
            try
            {
                conn = new SqlConnection(strConnect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(" SELECT b.BusinessID, b.BusinessName, b.BusinessDescription, b.AreaName "
                                                + " FROM Business b JOIN RoleBusiness rb ON b.BusinessID = rb.BusinessID "
                                                + " WHERE rb.RoleID = @id ", conn);
                cmd.Parameters.AddWithValue("@id", roleID);
                dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    BusinessDataView bdw = new BusinessDataView() { BusinessID = Convert.ToInt32(dr1["BusinessID"]), BusinessName = dr1["BusinessName"].ToString(), BusinessDescription = dr1["BusinessDescription"].ToString(), IsGranted = true, AreaName = dr1["AreaName"].ToString() };
                    businessList.Add(bdw);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dr1 != null) dr1.Close();
                if (conn != null) conn.Close();
            }
            return businessList;
        }

        public List<BusinessDataView> GetBusinessNotGrantedByRoleID(int roleID)
        {
            SqlConnection conn = null;
            SqlDataReader dr2 = null;
            List<BusinessDataView> businessList = new List<BusinessDataView>();
            try
            {
                conn = new SqlConnection(strConnect);
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(" SELECT b.BusinessID, b.BusinessName, b.BusinessDescription, b.AreaName "
                                    + " FROM Business b "
                                    + " EXCEPT "
                                    + " SELECT b.BusinessID, b.BusinessName, b.BusinessDescription, b.AreaName  "
                                    + " FROM Business b JOIN RoleBusiness rb ON b.BusinessID = rb.BusinessID "
                                    + " WHERE rb.RoleID = @id ", conn);
                cmd2.Parameters.AddWithValue("@id", roleID);
                dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    BusinessDataView bdw = new BusinessDataView() { BusinessID = Convert.ToInt32(dr2["BusinessID"]), BusinessName = dr2["BusinessName"].ToString(), BusinessDescription = dr2["BusinessDescription"].ToString(), IsGranted = false, AreaName = dr2["AreaName"].ToString() };
                    businessList.Add(bdw);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dr2 != null) dr2.Close();
                if (conn != null) conn.Close();
            }
            return businessList;
        }

        public bool UpdateBusinessRole(int businessID, int roleID)
        {
            SqlConnection conn = null;
            SqlDataReader dr = null;
            try
            {
                conn = new SqlConnection(strConnect);
                SqlCommand cmd = new SqlCommand("SELECT * FROM RoleBusiness WHERE RoleID = @rid AND BusinessID = @bid", conn);
                cmd.Parameters.AddWithValue("@rid", roleID);
                cmd.Parameters.AddWithValue("@bid", businessID);
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (InsertOrDeleteBusinessRole(true, businessID, roleID)) return true;
                }
                else
                {
                    if (InsertOrDeleteBusinessRole(false, businessID, roleID)) return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (dr != null) dr.Close();
                if (conn != null) conn.Close();
            }
            return false;
        }

        public bool InsertOrDeleteBusinessRole(bool isDelete, int businessID, int roleID)
        {
            SqlCommand cmd = new SqlCommand();
            if (isDelete)
            {
                cmd.CommandText = "DELETE FROM RoleBusiness WHERE RoleID = @rid AND BusinessID = @bid";
            }
            else
            {
                cmd.CommandText = "INSERT INTO RoleBusiness (RoleID, BusinessID) VALUES (@rid, @bid)";
            }
            cmd.Parameters.AddWithValue("@rid", roleID);
            cmd.Parameters.AddWithValue("@bid", businessID);
            if (new DatabaseContext().UpdateData(cmd)) return true;
            return false;
        }

        public List<BusinessDataAuthorizeView> GetBusinessDataNameForAuthorizeByRoleID(int roleID)
        {
            SqlConnection conn = null;
            SqlDataReader dr = null;
            List<BusinessDataAuthorizeView> businessList = new List<BusinessDataAuthorizeView>();
            try
            {
                conn = new SqlConnection(strConnect);
                SqlCommand cmd = new SqlCommand(" SELECT b.BusinessName, b.AreaName "
                                                + " FROM Business b JOIN RoleBusiness rb ON b.BusinessID = rb.BusinessID "
                                                + " WHERE rb.RoleID = @id ", conn);
                cmd.Parameters.AddWithValue("@id", roleID);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BusinessDataAuthorizeView b = new BusinessDataAuthorizeView() { BusinessName = dr["BusinessName"].ToString(), AreaName = dr["AreaName"].ToString() };
                    businessList.Add(b);

                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                if (dr != null) dr.Close();
                if (conn != null) conn.Close();
            }
            return businessList;
        }
    }
}