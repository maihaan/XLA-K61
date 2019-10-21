namespace Map
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pgbProcess = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btProcess = new System.Windows.Forms.Button();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbTableName = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.btSelectFolder = new System.Windows.Forms.Button();
            this.btProcessMulti = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btCancelMulti = new System.Windows.Forms.Button();
            this.pgbProcessDetail = new System.Windows.Forms.ProgressBar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.lblSingleProcess = new System.Windows.Forms.Label();
            this.lblMultiProcess = new System.Windows.Forms.Label();
            this.lblMultiDetail = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgbProcess
            // 
            this.pgbProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgbProcess.Location = new System.Drawing.Point(0, 170);
            this.pgbProcess.Name = "pgbProcess";
            this.pgbProcess.Size = new System.Drawing.Size(512, 23);
            this.pgbProcess.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 170);
            this.tabControl1.TabIndex = 101;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblSingleProcess);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.btCancel);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btProcess);
            this.tabPage1.Controls.Add(this.btBrowse);
            this.tabPage1.Controls.Add(this.tbTableName);
            this.tabPage1.Controls.Add(this.tbPath);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 144);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Single Shapefile";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btProcess
            // 
            this.btProcess.Location = new System.Drawing.Point(176, 72);
            this.btProcess.Name = "btProcess";
            this.btProcess.Size = new System.Drawing.Size(75, 23);
            this.btProcess.TabIndex = 3;
            this.btProcess.Text = "Process";
            this.btProcess.UseVisualStyleBackColor = true;
            this.btProcess.Click += new System.EventHandler(this.btProcess_Click);
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(421, 30);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 2;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tbTableName
            // 
            this.tbTableName.Location = new System.Drawing.Point(12, 74);
            this.tbTableName.Name = "tbTableName";
            this.tbTableName.Size = new System.Drawing.Size(158, 20);
            this.tbTableName.TabIndex = 1;
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 32);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(403, 20);
            this.tbPath.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblMultiDetail);
            this.tabPage2.Controls.Add(this.lblMultiProcess);
            this.tabPage2.Controls.Add(this.pgbProcessDetail);
            this.tabPage2.Controls.Add(this.btCancelMulti);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.btProcessMulti);
            this.tabPage2.Controls.Add(this.btSelectFolder);
            this.tabPage2.Controls.Add(this.tbFolder);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 144);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Multi Shapefile";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select shapefile";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "DB Table name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select folder";
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(12, 33);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(403, 20);
            this.tbFolder.TabIndex = 1;
            // 
            // btSelectFolder
            // 
            this.btSelectFolder.Location = new System.Drawing.Point(421, 31);
            this.btSelectFolder.Name = "btSelectFolder";
            this.btSelectFolder.Size = new System.Drawing.Size(75, 23);
            this.btSelectFolder.TabIndex = 2;
            this.btSelectFolder.Text = "Browse";
            this.btSelectFolder.UseVisualStyleBackColor = true;
            this.btSelectFolder.Click += new System.EventHandler(this.btSelectFolder_Click);
            // 
            // btProcessMulti
            // 
            this.btProcessMulti.Location = new System.Drawing.Point(12, 60);
            this.btProcessMulti.Name = "btProcessMulti";
            this.btProcessMulti.Size = new System.Drawing.Size(75, 23);
            this.btProcessMulti.TabIndex = 3;
            this.btProcessMulti.Text = "Process";
            this.btProcessMulti.UseVisualStyleBackColor = true;
            this.btProcessMulti.Click += new System.EventHandler(this.btProcessMulti_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(257, 72);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Map.Properties.Resources.settingicon;
            this.pictureBox1.Location = new System.Drawing.Point(466, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = global::Map.Properties.Resources.settingicon;
            this.pictureBox2.Location = new System.Drawing.Point(466, 108);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btCancelMulti
            // 
            this.btCancelMulti.Location = new System.Drawing.Point(94, 60);
            this.btCancelMulti.Name = "btCancelMulti";
            this.btCancelMulti.Size = new System.Drawing.Size(75, 23);
            this.btCancelMulti.TabIndex = 9;
            this.btCancelMulti.Text = "Cancel";
            this.btCancelMulti.UseVisualStyleBackColor = true;
            this.btCancelMulti.Click += new System.EventHandler(this.btCancelMulti_Click);
            // 
            // pgbProcessDetail
            // 
            this.pgbProcessDetail.Location = new System.Drawing.Point(175, 60);
            this.pgbProcessDetail.Name = "pgbProcessDetail";
            this.pgbProcessDetail.Size = new System.Drawing.Size(321, 23);
            this.pgbProcessDetail.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbLogs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(504, 144);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Logs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            this.tbLogs.Location = new System.Drawing.Point(9, 7);
            this.tbLogs.Multiline = true;
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(489, 131);
            this.tbLogs.TabIndex = 0;
            // 
            // lblSingleProcess
            // 
            this.lblSingleProcess.AutoSize = true;
            this.lblSingleProcess.Location = new System.Drawing.Point(9, 125);
            this.lblSingleProcess.Name = "lblSingleProcess";
            this.lblSingleProcess.Size = new System.Drawing.Size(24, 13);
            this.lblSingleProcess.TabIndex = 8;
            this.lblSingleProcess.Text = "0/0";
            // 
            // lblMultiProcess
            // 
            this.lblMultiProcess.AutoSize = true;
            this.lblMultiProcess.Location = new System.Drawing.Point(9, 125);
            this.lblMultiProcess.Name = "lblMultiProcess";
            this.lblMultiProcess.Size = new System.Drawing.Size(24, 13);
            this.lblMultiProcess.TabIndex = 11;
            this.lblMultiProcess.Text = "0/0";
            // 
            // lblMultiDetail
            // 
            this.lblMultiDetail.AutoSize = true;
            this.lblMultiDetail.Location = new System.Drawing.Point(175, 90);
            this.lblMultiDetail.Name = "lblMultiDetail";
            this.lblMultiDetail.Size = new System.Drawing.Size(24, 13);
            this.lblMultiDetail.TabIndex = 12;
            this.lblMultiDetail.Text = "0/0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 193);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pgbProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(528, 232);
            this.MinimumSize = new System.Drawing.Size(528, 232);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shapefile to MS SQL DB";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbProcess;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btProcess;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TextBox tbTableName;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btProcessMulti;
        private System.Windows.Forms.Button btSelectFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btCancelMulti;
        private System.Windows.Forms.ProgressBar pgbProcessDetail;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.Label lblSingleProcess;
        private System.Windows.Forms.Label lblMultiProcess;
        private System.Windows.Forms.Label lblMultiDetail;
    }
}