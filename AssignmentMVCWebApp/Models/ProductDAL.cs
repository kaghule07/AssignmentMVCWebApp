using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
namespace AssignmentMVCWebApp.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductDAL()
        {
            con = new SqlConnection(Startup.ConnectionString);
        }
        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();
            string str = "Select * from Products";
            cmd = new SqlCommand(str, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product p = new Product();
                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.ProductName = dr["ProductName"].ToString();
                    p.Price = Convert.ToDouble(dr["Price"]);
                    p.Company = dr["Company"].ToString();
                    p.Description = dr["Description"].ToString();
                    list.Add(p);
                }
                con.Close();
                return list;
            }
            else
            {
                return list;
            }
        }
        public Product GetProductById(int id)
        {
            Product p = new Product();
            string str = "select * from Products where Id=@id";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.ProductName = dr["ProductName"].ToString();
                    p.Price = Convert.ToDouble(dr["Price"]);
                    p.Company = dr["Company"].ToString();
                    p.Description = dr["Description"].ToString();

                }
                con.Close();
                return p;
            }
            else
            {
                con.Close();
                return p;
            }
        }
        public int Save(Product prod)
        {
            string str = "insert into Products values(@productname,@price,@company,@description)";
            cmd = new SqlCommand(str, con);
            con.Open();
            cmd.Parameters.AddWithValue("@productname", prod.ProductName);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@company", prod.Company);
            cmd.Parameters.AddWithValue("@description", prod.Description);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Update(Product prod)
        {
            string str = "update Products set ProductName=@productname,Price=@price,Company=@company,Description=@description where Id=@id";
            cmd = new SqlCommand(str, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", prod.Id);
            cmd.Parameters.AddWithValue("@productname", prod.ProductName);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            cmd.Parameters.AddWithValue("@company", prod.Company);
            cmd.Parameters.AddWithValue("@description", prod.Description);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int Delete(int id)
        {
            string str = "delete from Products where Id=@id";
            cmd = new SqlCommand(str, con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
    }
}