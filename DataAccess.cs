using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace Map2SQL
{
    public class DataAccess
    {
        // Thuoc tinh
        String Connection;

        // Phuong thuc
        public DataAccess()
        {
            Connection = "Data Source=" + Properties.Settings.Default.server
                + "; Initial catalog=" + Properties.Settings.Default.database
                + "; User id=" + Properties.Settings.Default.userid
                + "; Password=" + Properties.Settings.Default.password + ";";
        }

        public DataTable Read(String query)
        {
            SqlConnection con = new SqlConnection(Connection);
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
            SqlConnection con = new SqlConnection(Connection);
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

        public int GetMaxColumnValue(String tableName, String columnName)
        {
            int ma = 0;
            String queryMa = "SELECT Max(" + columnName + ") FROM " + tableName;
            DataTable tb = Read(queryMa);
            if (tb != null && tb.Rows.Count == 1)
            {
                ma = int.Parse(tb.Rows[0][0].ToString());
            }
            return ma;
        }
    }
}
