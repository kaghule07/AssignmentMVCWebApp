using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
namespace AssignmentMVCWebApp.Models
{
    public class UserDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public UserDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }
        public int Save(User us)
        {
            string str = "insert into UserKG values(@fullname,@emailid,@password,@roleid)";
            cmd = new SqlCommand(str, con);
            con.Open();
            cmd.Parameters.AddWithValue("@fullname", us.FullName);
            cmd.Parameters.AddWithValue("@emailid", us.EmailID);
            cmd.Parameters.AddWithValue("@password", us.Password);
            cmd.Parameters.AddWithValue("@roleid", 2);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public bool Verify(User us)
        {
            string emailid;
            string password;
            string str = "Select EmailId,Password from UserKG where EmailId=@emailid";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@emailid",us.EmailID);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                emailid = dr["EmailId"].ToString();
                password = dr["Password"].ToString();
            }
            else
            {
                con.Close();
                return false;
            }
            if (emailid == us.EmailID && password == us.Password)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }
    }
}
