using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stt = Map.Properties.Settings;

namespace Map
{
    public partial class Main : Form
    {
        BackgroundWorker bg = new BackgroundWorker();
        Boolean cancel = false;
        Boolean single = true;

        public Main()
        {
            InitializeComponent();
            bg.WorkerSupportsCancellation = true;
            bg.WorkerReportsProgress = true;
            bg.ProgressChanged += Bg_ProgressChanged;
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.DoWork += Bg_DoWork;            

            pgbProcess.Maximum = 100;
            btCancel.Enabled = false;
        }

        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            GeoAPI.GeometryServiceProvider.Instance = new NetTopologySuite.NtsGeometryServices();
            MyEncoder ec = new MyEncoder();

            if (single)
            {
                // Single shapefile
                if (String.IsNullOrEmpty(tbPath.Text))
                {
                    MessageBox.Show("Please choose Shapefile");
                    return;
                }
                if (!File.Exists(tbPath.Text))
                {
                    MessageBox.Show("File does not Exist");
                    return;
                }
                if (String.IsNullOrEmpty(tbTableName.Text))
                {
                    MessageBox.Show("Please Enter your SQL Table name");
                    return;
                }
                String path = tbPath.Text;                
                using (SharpMap.Data.Providers.ShapeFile ds = new SharpMap.Data.Providers.ShapeFile(path))
                {
                    ds.Open();
                    int dem = 0, demLoi = 0, demSuaLoi = 0;

                    // Create table in database
                    Boolean tableCreated = false;
                    var feature = ds.GetFeature(0);
                    String cmd = "Create Table " + tbTableName.Text + "(AutoID int IDENTITY(1, 1) PRIMARY KEY, ";
                    String cols = "", iCols = "";
                    if (feature != null)
                    {
                        List<String> intList = new List<string> { "system.uint64", "system.int64", "system.int", "system.uint", "system.byte", "system.int32", "system.uint32", "system.int16", "system.uint16" };
                        for (int j = 0; j < feature.Table.Columns.Count; j++)
                        {
                            String dtt = feature.Table.Columns[j].DataType.ToString().ToLower();
                            String colName = feature.Table.Columns[j].ColumnName;
                            if (colName.ToLower().Equals("oid"))
                                continue;
                            iCols += colName + ",";
                            if (intList.Contains(dtt))
                            {
                                if (colName.ToLower().Equals("kd") || colName.ToLower().Equals("vd"))
                                    cols += colName + " float, ";
                                else
                                    cols += colName + " int, ";
                            }
                            else if (dtt.Equals("system.string"))
                            {
                                cols += colName + " nvarchar(max), ";
                            }
                            else if (dtt.Equals("system.double"))
                                cols += colName + " float, ";
                        }
                        cols += "geom geometry";
                        iCols += "geom";
                        cmd += cols + ")";
                        int rs = WriteData(cmd);
                        if (rs == -1)
                            tableCreated = true;
                    }
                    if (!tableCreated)
                    {
                        MessageBox.Show("Check your SQL Server connection.");
                        return;
                    }

                    // Add data to SQL table
                    int total = ds.GetFeatureCount();
                    for (uint i = 0; i < ds.GetFeatureCount(); i++)
                    {
                        if (bg.CancellationPending)
                        {
                            return;
                        }
                        feature = ds.GetFeature(i);
                        Double kd = feature.Geometry.Centroid.X;
                        Double vd = feature.Geometry.Centroid.Y;
                        // Get values
                        String values = "";
                        for (int j = 0; j < feature.Table.Columns.Count; j++)
                        {
                            String dtt = feature.Table.Columns[j].DataType.ToString().ToLower();
                            String colName = feature.Table.Columns[j].ColumnName;
                            if (colName.ToLower().Equals("oid"))
                                continue;
                            var value = feature.ItemArray[j];
                            if (dtt.Equals("system.string"))
                                values += "N'" + value + "', ";
                            else if (colName.ToLower().Equals("kd"))
                                values += kd.ToString() + ", ";
                            else if (colName.ToLower().Equals("vd"))
                                values += vd.ToString() + ", ";
                            else
                                values += value + ", ";
                        }

                        // Get geometry
                        String poly = "";
                        Boolean loi = false;
                        if (feature.Geometry == null)
                        {
                            poly = "NULL";
                        }
                        else if (!feature.Geometry.OgcGeometryType.ToString().Equals("Polygon"))
                        {
                            demLoi++;
                            loi = true;
                            String mp = feature.Geometry.ToString().Replace(", ", ",").Replace(" ,", ",");
                            int sIndex = mp.IndexOf("(((");
                            sIndex += 3;
                            int lIndex = mp.IndexOf(")", sIndex);
                            int len = lIndex - sIndex;
                            int tmp = sIndex;
                            while (mp.IndexOf(",(", tmp) > 0)
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
                            poly = mp.Substring(sIndex, len);

                            // Make polygon valid
                            while (poly.Contains("("))
                                poly = poly.Replace("(", "");
                            while (poly.Contains(")"))
                                poly.Replace(")", "");
                            String first = poly.Split(',')[0].Trim();
                            String last = poly.Split(',')[poly.Split(',').Length - 1].Trim();
                            if (!first.Equals(last))
                                poly += ", " + first;
                            poly = "POLYGON ((" + poly + "))";
                        }
                        else
                            poly = feature.Geometry.ToString();

                        // Insert to DB
                        if (feature.Geometry != null)
                            values += "geometry::STPolyFromText('" + poly + "', 4326)";
                        else
                            values += "NULL";
                        cmd = "INSERT INTO " + tbTableName.Text + "(" + iCols + ") Values(" + values + ")";
                        int dem1 = WriteData(cmd);
                        dem += dem1;
                        if (loi && dem1 == 1)
                            demSuaLoi++;
                        bg.ReportProgress(dem * 100 / total, "m:" + dem + ":" + total);
                    }
                    Logs(DateTime.Now.ToString() + ": " + tbTableName.Text + " - Complete with: " + dem.ToString() + "/" + total.ToString() + " updated; "
                        + demSuaLoi.ToString() + "/" + demLoi.ToString() + " error fixed");
                }
            }
            else
            {
                // Multi shapefiles
                if(String.IsNullOrEmpty(tbFolder.Text))
                {
                    MessageBox.Show("Please select a folder contains Shapefiles");
                    return;
                }
                if(!Directory.Exists(tbFolder.Text))
                {
                    MessageBox.Show("Folder does not Exist");
                    return;
                }
                DirectoryInfo di = new DirectoryInfo(tbFolder.Text);
                FileInfo[] files = di.GetFiles("*.shp", SearchOption.AllDirectories);
                int demTong = 0, tong = files.Length;
                foreach (FileInfo fi in files)
                {
                    String path = fi.FullName;
                    String tableName = fi.Name.Substring(0, fi.Name.IndexOf("."));
                    using (SharpMap.Data.Providers.ShapeFile ds = new SharpMap.Data.Providers.ShapeFile(path))
                    {
                        try
                        {
                            ds.Open();
                        }
                        catch
                        {

                        }
                        int dem = 0, demLoi = 0, demSuaLoi = 0; ;

                        // Create table in database
                        Boolean tableCreated = false;
                        var feature = ds.GetFeature(0);
                        String cmd = "Create Table " + tableName + "(AutoID int IDENTITY(1, 1) PRIMARY KEY, ";
                        String cols = "", iCols = "";
                        if (feature != null)
                        {
                            List<String> intList = new List<string> { "system.int64", "system.uint64", "system.int", "system.uint", "system.byte", "system.int32", "system.uint32", "system.int16", "system.uint16" };
                            for (int j = 0; j < feature.Table.Columns.Count; j++)
                            {
                                String dtt = feature.Table.Columns[j].DataType.ToString().ToLower();
                                String colName = feature.Table.Columns[j].ColumnName;
                                if (colName.ToLower().Equals("oid"))
                                    continue;
                                iCols += colName + ",";
                                if (intList.Contains(dtt))
                                {
                                    if (colName.ToLower().Equals("kd") || colName.ToLower().Equals("vd"))
                                        cols += colName + " float, ";
                                    else
                                        cols += colName + " int, ";
                                }
                                else if (dtt.Equals("system.string"))
                                {
                                    cols += colName + " nvarchar(max), ";
                                }
                                else if (dtt.Equals("system.double"))
                                    cols += colName + " float, ";
                            }
                            cols += "geom geometry";
                            iCols += "geom";
                            cmd += cols + ")";
                            int rs = WriteData(cmd);
                            if (rs == -1)
                                tableCreated = true;
                        }
                        if (!tableCreated)
                        {
                            MessageBox.Show("Check your SQL Server connection.");
                            return;
                        }

                        // Add data to SQL table
                        int total = ds.GetFeatureCount();
                        for (uint i = 182927; i < ds.GetFeatureCount(); i++)
                        {
                            if (bg.CancellationPending)
                            {
                                return;
                            }
                            feature = ds.GetFeature(i);
                            Double kd = 0;
                            Double vd = 0;
                            if (feature.Geometry != null)
                            {
                                kd = feature.Geometry.Centroid.X;
                                vd = feature.Geometry.Centroid.Y;
                            }
                            // Get values
                            String values = "";
                            for (int j = 0; j < feature.Table.Columns.Count; j++)
                            {
                                String dtt = feature.Table.Columns[j].DataType.ToString().ToLower();
                                String colName = feature.Table.Columns[j].ColumnName;
                                if (colName.ToLower().Equals("oid"))
                                    continue;
                                String value = feature.ItemArray[j].ToString();
                                if (dtt.Equals("system.string"))
                                    values += "N'" + value + "', ";
                                else if (colName.ToLower().Equals("kd"))
                                    values += kd.ToString() + ", ";
                                else if (colName.ToLower().Equals("vd"))
                                    values += vd.ToString() + ", ";
                                else
                                    values += value + ", ";
                            }

                            // Get geometry
                            String poly = "";
                            Boolean loi = false;
                            if(feature.Geometry == null)
                            {
                                poly = "NULL";
                            }
                            else if (!feature.Geometry.OgcGeometryType.ToString().Equals("Polygon"))
                            {
                                loi = true;
                                demLoi++;
                                String mp = feature.Geometry.ToString().Replace(", ", ",").Replace(" ,", ",");
                                int sIndex = mp.IndexOf("(((");
                                sIndex += 3;
                                int lIndex = mp.IndexOf(")", sIndex);
                                int len = lIndex - sIndex;
                                int tmp = sIndex;
                                while (mp.IndexOf(",(", tmp) > 0)
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
                                poly = mp.Substring(sIndex, len);

                                // Make polygon valid
                                while (poly.Contains("("))
                                    poly = poly.Replace("(", "");
                                while (poly.Contains(")"))
                                    poly.Replace(")", "");
                                String first = poly.Split(',')[0].Trim();
                                String last = poly.Split(',')[poly.Split(',').Length - 1].Trim();
                                if (!first.Equals(last))
                                    poly += ", " + first;
                                poly = "POLYGON ((" + poly + "))";
                            }
                            else
                                poly = feature.Geometry.ToString();

                            // Insert to DB
                            if (feature.Geometry != null)
                                values += "geometry::STPolyFromText('" + poly + "', 4326)";
                            else
                                values += "NULL";
                            cmd = "INSERT INTO " + tableName + "(" + iCols + ") Values(" + values + ")";
                            int dem1 = WriteData(cmd);
                            dem += dem1;
                            if (loi && dem1 == 1)
                                demSuaLoi++;
                            bg.ReportProgress(dem * 100 / total, "s:" + dem + ":" + total);
                        }
                        Logs(DateTime.Now.ToString() + ": " + tableName + " - Complete with: " + dem.ToString() + "/" + total.ToString() + " updated; "
                        + demSuaLoi.ToString() + "/" + demLoi.ToString() + " error fixed");
                    }
                    demTong++;
                    bg.ReportProgress(demTong * 100 / tong, "m:" + demTong + ":" + tong);
                }
            }
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (cancel)
            {
                pgbProcess.Value = 0;
                pgbProcess.PerformStep();
                pgbProcess.Refresh();
                MessageBox.Show("Canceled");
                cancel = false;
            }
            else
            {
                Logs(DateTime.Now.ToString() + ": Completed process");
                MessageBox.Show("Completed.");
            }
        }

        private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new Action<object, ProgressChangedEventArgs>(Bg_ProgressChanged), sender, e);
                return;
            }
            String us = e.UserState.ToString();
            if (us.Split(':')[0].Equals("s"))
            {
                pgbProcessDetail.Value = e.ProgressPercentage;
                pgbProcessDetail.PerformStep();
                pgbProcessDetail.Refresh();

                lblMultiDetail.Text = us.Split(':')[1] + "/" + us.Split(':')[2];
                lblMultiDetail.Refresh();

                
            }
            else
            {
                pgbProcess.Value = e.ProgressPercentage;
                pgbProcess.PerformStep();
                pgbProcess.Refresh();

                if (single)
                {
                    lblSingleProcess.Text = us.Split(':')[1] + "/" + us.Split(':')[2];
                    lblSingleProcess.Refresh();
                }
                else
                {
                    lblMultiProcess.Text = us.Split(':')[1] + "/" + us.Split(':')[2];
                    lblMultiProcess.Refresh();
                }
                
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Shapefile (*.shp) | *.shp";
            if (od.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = od.FileName;
            }
        }

        private void btProcess_Click(object sender, EventArgs e)
        {
            Logs(DateTime.Now.ToString() + ": Start process single shapefile");
            single = true;
            bg.RunWorkerAsync();
            btProcess.Enabled = false;
            btBrowse.Enabled = false;
            btCancel.Enabled = true;
            tbPath.Enabled = false;
            tbTableName.Enabled = false;
        }


        public int WriteData(String cmds)
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
                return da;
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                else
                    MessageBox.Show("Cannot connect to SQL Server. Please check your SQL Server Setting.");
                return 0;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Logs(DateTime.Now.ToString() + ": Cancel process single shapefiles");

            bg.CancelAsync();
            btBrowse.Enabled = true;
            btProcess.Enabled = true;
            btCancel.Enabled = false;
            tbPath.Enabled = true;
            tbTableName.Enabled = true;
            cancel = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SQLSettings st = new SQLSettings(); 
            st.ShowDialog();
        }

        private void btSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                tbFolder.Text = fd.SelectedPath;
        }

        private void btProcessMulti_Click(object sender, EventArgs e)
        {
            Logs(DateTime.Now.ToString() + ": Start process multi shapefiles");
            
            single = false;
            bg.RunWorkerAsync();
            btSelectFolder.Enabled = false;
            btProcessMulti.Enabled = false;
            tbFolder.Enabled = false;
            btCancelMulti.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SQLSettings st = new SQLSettings();
            st.ShowDialog();
        }

        private void btCancelMulti_Click(object sender, EventArgs e)
        {
            bg.CancelAsync();
            btSelectFolder.Enabled = true;
            btProcessMulti.Enabled = true;
            tbFolder.Enabled = true;
            btCancelMulti.Enabled = false;
            cancel = true;
        }

        private void Logs(String content)
        {
            using (StreamWriter sw = new StreamWriter("logs.txt", true))
            {
                sw.WriteLine(content);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            String content = File.ReadAllText("logs.txt");
            tbLogs.Text = content;
        }
    }
}
