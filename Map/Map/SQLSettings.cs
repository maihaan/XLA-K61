using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using stt = Map.Properties.Settings;

namespace Map
{
    public partial class SQLSettings : Form
    {
        public SQLSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbServer.Text.Contains("\\"))
                stt.Default.server = tbServer.Text.Split('\\')[1];
            else
                stt.Default.server = tbServer.Text;
            stt.Default.database = tbDatabase.Text;
            stt.Default.username = tbUsername.Text;
            stt.Default.password = tbPassword.Text;
            stt.Default.Save();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SQLSettings_Load(object sender, EventArgs e)
        {
            tbServer.Text = stt.Default.server;
            tbDatabase.Text = stt.Default.database;
            tbUsername.Text = stt.Default.username;
            tbPassword.Text = stt.Default.password;
        }
    }
}
