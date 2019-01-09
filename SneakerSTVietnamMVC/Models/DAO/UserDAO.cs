using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SneakerSTVietnamMVC.Models.DataView;
using System.Configuration;

namespace SneakerSTVietnamMVC.Models.DAO
{
    public class UserDAO
    {
        private string strConnect = ConfigurationManager.ConnectionStrings["DB_SNEAKERSTV2ConnectionString"].ToString();

        public bool FindEmail(string email)
        {
            SqlConnection conn = null;
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT Email FROM [User] WHERE Email = @e");
                cmd.Parameters.AddWithValue("@e", email);
                conn = new SqlConnection(strConnect);
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (dr != null) dr.Close();
                if (conn != null) conn.Close();
            }
            return false;
        }

        public int FindRoleIDByRoleName(string roleName)
        {
            SqlConnection conn = null;
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT RoleID FROM Role WHERE RoleName = @n");
                cmd.Parameters.AddWithValue("@n", roleName);
                conn = new SqlConnection(strConnect);
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    return Convert.ToInt32(dr["RoleID"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (dr != null) dr.Close();
                if (conn != null) conn.Close();
            }
            return -1;
        }

        public bool RegisterNewUser(ViewRegisterModel u, string securityCode)
        {
            string roleName = "Customer";
            u.RoleID = FindRoleIDByRoleName(roleName);
            if (u.RoleID >= 0)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [User] (Email, Password, FirstName, LastName, Gender, Address, City, Postcode, State, PhoneNumber, Country, RoleID, Active, ReceiveEmail, SecurityCode) "
                    + " VALUES(@email, @pass, @fn, @ln, @gen, @addr, @city, @post, @state, @phone, @country, @rid, @active, @receiveemail, @security) ");
                cmd.Parameters.AddWithValue("@email", u.Email);
                cmd.Parameters.AddWithValue("@pass", u.Password);
                cmd.Parameters.AddWithValue("@fn", u.FirstName);
                cmd.Parameters.AddWithValue("@ln", u.LastName);
                cmd.Parameters.AddWithValue("@gen", u.Gender);
                cmd.Parameters.AddWithValue("@addr", u.Address);
                cmd.Parameters.AddWithValue("@city", u.City);
                cmd.Parameters.AddWithValue("@post", u.Postcode);
                cmd.Parameters.AddWithValue("@state", u.State);
                cmd.Parameters.AddWithValue("@phone", u.PhoneNumber);
                cmd.Parameters.AddWithValue("@country", u.Country);
                cmd.Parameters.AddWithValue("@rid", u.RoleID);
                cmd.Parameters.AddWithValue("@active", false);
                cmd.Parameters.AddWithValue("@receiveemail", u.ReceiveEmail);
                cmd.Parameters.AddWithValue("@security", securityCode);
                if (new DatabaseContext().UpdateData(cmd))
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteRegisterDataFailed(string email)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [User] WHERE Email = @e");
            cmd.Parameters.AddWithValue("@e", email);
            if (new DatabaseContext().UpdateData(cmd))
            {
                Console.WriteLine("Deleted Account Registed Failed With Email: " + email);
            }
        }

        public ViewConfirmModel GetConfirmInformation(string email)
        {
            ViewConfirmModel vcm = null;
            SqlConnection conn = null;
            SqlDataReader dr = null;
            try
            {
                conn = new SqlConnection(strConnect);
                SqlCommand cmd = new SqlCommand("SELECT UserID, SecurityCode, Active FROM [User] WHERE Email = @e");
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    vcm = new ViewConfirmModel();
                    vcm.UserID = Convert.ToInt32(dr["UserID"]);
                    vcm.SecurityCode = dr["SecurityCode"].ToString();
                    vcm.Active = Convert.ToBoolean(dr["Active"]);
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
            return vcm;
        }

        public ViewActiveModel UserIsActive(int userID, string code)
        {
            SqlConnection conn = null;
            SqlDataReader dr = null;
            ViewActiveModel vam = null;
            try
            {
                conn = new SqlConnection(strConnect);
                SqlCommand cmd = new SqlCommand("SELECT UserID, Active FROM [User] WHERE UserID = @id AND SecurityCode = @code");
                cmd.Parameters.AddWithValue("@id", userID);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Connection = conn;
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    vam = new ViewActiveModel();
                    vam.UserID = Convert.ToInt32(dr["UserID"]);
                    vam.Active = Convert.ToBoolean(dr["Active"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw new Exception();
            }
            finally
            {
                if (dr != null) dr.Close();
                if (conn != null) conn.Close();
            }
            return vam;
        }

        public bool UpdateActiveUser(int userID)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [User] SET Active = @a WHERE UserID = @id");
            cmd.Parameters.AddWithValue("@a", true);
            cmd.Parameters.AddWithValue("@id", userID);
            if (new DatabaseContext().UpdateData(cmd))
            {
                return true;
            }
            return false;
        }
    }
}
