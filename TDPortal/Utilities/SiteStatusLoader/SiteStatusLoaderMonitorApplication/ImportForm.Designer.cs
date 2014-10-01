namespace AO.SiteStatusLoaderMonitorApplication
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.lblSiteStatusRealTime = new System.Windows.Forms.Label();
            this.btnRealTimeImport = new System.Windows.Forms.Button();
            this.txtTestStatus = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSiteStatusHistoric = new System.Windows.Forms.Label();
            this.btnHistoricImport = new System.Windows.Forms.Button();
            this.txtRealTimeFile = new System.Windows.Forms.TextBox();
            this.txtHistoricFile = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenRealtimeFile = new System.Windows.Forms.Button();
            this.btnOpenHistoricFile = new System.Windows.Forms.Button();
            this.lblSiteStatusHistoricPID = new System.Windows.Forms.Label();
            this.txtHistoricFilePID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSiteStatusRealTime
            // 
            this.lblSiteStatusRealTime.AutoSize = true;
            this.lblSiteStatusRealTime.Location = new System.Drawing.Point(1, 8);
            this.lblSiteStatusRealTime.Name = "lblSiteStatusRealTime";
            this.lblSiteStatusRealTime.Size = new System.Drawing.Size(111, 13);
            this.lblSiteStatusRealTime.TabIndex = 100;
            this.lblSiteStatusRealTime.Text = "Site Status (Real time)";
            // 
            // btnRealTimeImport
            // 
            this.btnRealTimeImport.Location = new System.Drawing.Point(478, 23);
            this.btnRealTimeImport.Name = "btnRealTimeImport";
            this.btnRealTimeImport.Size = new System.Drawing.Size(75, 23);
            this.btnRealTimeImport.TabIndex = 1;
            this.btnRealTimeImport.Text = "Import";
            this.btnRealTimeImport.UseVisualStyleBackColor = true;
            this.btnRealTimeImport.Click += new System.EventHandler(this.btnRealTimeImport_Click);
            // 
            // txtTestStatus
            // 
            this.txtTestStatus.BackColor = System.Drawing.Color.White;
            this.txtTestStatus.Location = new System.Drawing.Point(4, 112);
            this.txtTestStatus.Multiline = true;
            this.txtTestStatus.Name = "txtTestStatus";
            this.txtTestStatus.ReadOnly = true;
            this.txtTestStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestStatus.Size = new System.Drawing.Size(549, 143);
            this.txtTestStatus.TabIndex = 6;
            this.txtTestStatus.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(478, 261);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSiteStatusHistoric
            // 
            this.lblSiteStatusHistoric.AutoSize = true;
            this.lblSiteStatusHistoric.Location = new System.Drawing.Point(1, 52);
            this.lblSiteStatusHistoric.Name = "lblSiteStatusHistoric";
            this.lblSiteStatusHistoric.Size = new System.Drawing.Size(102, 13);
            this.lblSiteStatusHistoric.TabIndex = 102;
            this.lblSiteStatusHistoric.Text = "Site Status (Historic)";
            // 
            // btnHistoricImport
            // 
            this.btnHistoricImport.Location = new System.Drawing.Point(478, 67);
            this.btnHistoricImport.Name = "btnHistoricImport";
            this.btnHistoricImport.Size = new System.Drawing.Size(75, 23);
            this.btnHistoricImport.TabIndex = 3;
            this.btnHistoricImport.Text = "Import";
            this.btnHistoricImport.UseVisualStyleBackColor = true;
            this.btnHistoricImport.Click += new System.EventHandler(this.btnHistoricImport_Click);
            // 
            // txtRealTimeFile
            // 
            this.txtRealTimeFile.Location = new System.Drawing.Point(4, 24);
            this.txtRealTimeFile.Name = "txtRealTimeFile";
            this.txtRealTimeFile.Size = new System.Drawing.Size(436, 20);
            this.txtRealTimeFile.TabIndex = 0;
            // 
            // txtHistoricFile
            // 
            this.txtHistoricFile.Location = new System.Drawing.Point(4, 68);
            this.txtHistoricFile.Name = "txtHistoricFile";
            this.txtHistoricFile.Size = new System.Drawing.Size(320, 20);
            this.txtHistoricFile.TabIndex = 2;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open Site Status Data File";
            // 
            // btnOpenRealtimeFile
            // 
            this.btnOpenRealtimeFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenRealtimeFile.Image")));
            this.btnOpenRealtimeFile.Location = new System.Drawing.Point(444, 23);
            this.btnOpenRealtimeFile.Name = "btnOpenRealtimeFile";
            this.btnOpenRealtimeFile.Size = new System.Drawing.Size(28, 23);
            this.btnOpenRealtimeFile.TabIndex = 103;
            this.btnOpenRealtimeFile.UseVisualStyleBackColor = true;
            this.btnOpenRealtimeFile.Click += new System.EventHandler(this.btnOpenRealtimeFile_Click);
            // 
            // btnOpenHistoricFile
            // 
            this.btnOpenHistoricFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenHistoricFile.Image")));
            this.btnOpenHistoricFile.Location = new System.Drawing.Point(444, 68);
            this.btnOpenHistoricFile.Name = "btnOpenHistoricFile";
            this.btnOpenHistoricFile.Size = new System.Drawing.Size(28, 23);
            this.btnOpenHistoricFile.TabIndex = 104;
            this.btnOpenHistoricFile.UseVisualStyleBackColor = true;
            this.btnOpenHistoricFile.Click += new System.EventHandler(this.btnOpenHistoricFile_Click);
            // 
            // lblSiteStatusHistoricPID
            // 
            this.lblSiteStatusHistoricPID.AutoSize = true;
            this.lblSiteStatusHistoricPID.Location = new System.Drawing.Point(331, 52);
            this.lblSiteStatusHistoricPID.Name = "lblSiteStatusHistoricPID";
            this.lblSiteStatusHistoricPID.Size = new System.Drawing.Size(25, 13);
            this.lblSiteStatusHistoricPID.TabIndex = 105;
            this.lblSiteStatusHistoricPID.Text = "PID";
            // 
            // txtHistoricFilePID
            // 
            this.txtHistoricFilePID.Location = new System.Drawing.Point(334, 67);
            this.txtHistoricFilePID.Name = "txtHistoricFilePID";
            this.txtHistoricFilePID.Size = new System.Drawing.Size(100, 20);
            this.txtHistoricFilePID.TabIndex = 106;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 287);
            this.Controls.Add(this.txtHistoricFilePID);
            this.Controls.Add(this.lblSiteStatusHistoricPID);
            this.Controls.Add(this.btnOpenHistoricFile);
            this.Controls.Add(this.btnOpenRealtimeFile);
            this.Controls.Add(this.txtHistoricFile);
            this.Controls.Add(this.txtRealTimeFile);
            this.Controls.Add(this.btnHistoricImport);
            this.Controls.Add(this.lblSiteStatusHistoric);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtTestStatus);
            this.Controls.Add(this.btnRealTimeImport);
            this.Controls.Add(this.lblSiteStatusRealTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Site Status Data";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSiteStatusRealTime;
        private System.Windows.Forms.Button btnRealTimeImport;
        private System.Windows.Forms.TextBox txtTestStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSiteStatusHistoric;
        private System.Windows.Forms.Button btnHistoricImport;
        private System.Windows.Forms.TextBox txtRealTimeFile;
        private System.Windows.Forms.TextBox txtHistoricFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnOpenRealtimeFile;
        private System.Windows.Forms.Button btnOpenHistoricFile;
        private System.Windows.Forms.Label lblSiteStatusHistoricPID;
        private System.Windows.Forms.TextBox txtHistoricFilePID;
    }
}