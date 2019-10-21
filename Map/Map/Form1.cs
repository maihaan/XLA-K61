using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stt = Map.Properties.Settings;

namespace Map
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public String WriteData(String cmds)
        {
            String conn = "Data Source=.\\" + stt.Default.server + ";Initial Catalog=" + stt.Default.database + ";user id=" + stt.Default.username + "; password=" + stt.Default.password + ";";
            SqlConnection con = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(cmds, con);
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                int da = cmd.ExecuteNonQuery();
                con.Close();
                return da.ToString();
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                else
                    MessageBox.Show("Cannot connect to SQL Server. Please check your SQL Server Setting.");
                return "ERR";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeoAPI.GeometryServiceProvider.Instance = new NetTopologySuite.NtsGeometryServices();
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Shapefile (*.shp) | *.shp";
            if(od.ShowDialog() == DialogResult.OK)
            {
                String path = od.FileName;
                using (SharpMap.Data.Providers.ShapeFile ds = new SharpMap.Data.Providers.ShapeFile(path))
                {
                    ds.Open();
                    int dem = 0;
                    String rs = "";
                    for (uint i = 0; i < ds.GetFeatureCount(); i++)
                    {
                        var feature = ds.GetFeature(i);
                        String dt = "";
                        for(int j = 0; j < feature.Table.Columns.Count; j++)
                        {
                            dt += feature.Table.Columns[j].ColumnName + ":" + feature.Table.Columns[j].DataType.ToString() + "-" + feature.Table.Columns[j].MaxLength + "\r\n";
                        }
                        if(dt.Length > 0)
                        {
                            textBox1.Text = dt;
                            break;
                        }
                        
                        if (!feature.Geometry.OgcGeometryType.ToString().Equals("Polygon"))
                        {
                            String mp = feature.Geometry.ToString().Replace(", ", ",").Replace(" ,", ",");
                            int sIndex = mp.IndexOf("(((");
                            sIndex += 3;
                            int lIndex = mp.IndexOf(")", sIndex);
                            int len = lIndex - sIndex;
                            int tmp = sIndex;
                            while( mp.IndexOf(",(", tmp) > 0)
                            {
                                int sta = mp.IndexOf(",(", tmp);
                                if (sta > 0)
                                {
                                    int sto = mp.IndexOf(")", sta);
                                    if (sto > 0 && sto > sta)
                                    {
                                        tmp = sto + 3;
                                        if (sto - sta > len)
                                        {
                                            sIndex = sta + 2;
                                            len = sto - sta - 2;
                                        }
                                    }
                                }
                            }
                            String poly = mp.Substring(sIndex, len);                            
                            rs += "POLYGON ((" + poly + "))\r\n";
                            dem++;
                        }
                    }
                    MessageBox.Show(dem.ToString());
                    //textBox1.Text = rs;
                }
                
            }
        }
    }
}
