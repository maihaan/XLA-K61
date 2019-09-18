using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace QuanLyNhaO.Objects
{
    public class DataAccess
    {
        String connection = System.Configuration.ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        public DataTable Read(String query)
        {
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable tb = new DataTable();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                tb.Load(dr, LoadOption.OverwriteChanges);
                con.Close();
                return tb;
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                return null;
            }
        }

        public int Write(String query)
        {
            SqlConnection con = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                int dem = 0;
                dem = cmd.ExecuteNonQuery();
                con.Close();
                return dem;
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                return -1;
            }
        }
    }
}