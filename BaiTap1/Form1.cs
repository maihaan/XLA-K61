using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Map2SQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Shapefiles (*.shp) | *.shp";
            if(od.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = od.FileName;
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            MyShapefile ms = new MyShapefile();
            ms.FileName = tbPath.Text;
            ms.SQLTableName = tbTableName.Text;
            ms.UpdateToDB();
        }
    }
}
