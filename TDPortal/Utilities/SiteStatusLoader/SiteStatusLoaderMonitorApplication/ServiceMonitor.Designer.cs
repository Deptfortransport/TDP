namespace AO.SiteStatusLoaderMonitorApplication
{
    partial class ServiceMonitor
    {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceMonitor));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblError = new System.Windows.Forms.Label();
            this.lblAlertGreen = new System.Windows.Forms.Label();
            this.lblAlertAmber = new System.Windows.Forms.Label();
            this.lblAlertRed = new System.Windows.Forms.Label();
            this.lblAlertGreenCount = new System.Windows.Forms.Label();
            this.lblAlertAmberCount = new System.Windows.Forms.Label();
            this.lblAlertRedCount = new System.Windows.Forms.Label();
            this.lblNumberOfAlerts = new System.Windows.Forms.Label();
            this.lblAlertsSinceDateTime = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusVersionNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLastUpdatedDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 32);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(413, 196);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(12, 241);
            this.lblError.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(57, 13);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "Last error: ";
            this.lblError.Visible = false;
            // 
            // lblAlertGreen
            // 
            this.lblAlertGreen.AutoSize = true;
            this.lblAlertGreen.Location = new System.Drawing.Point(451, 76);
            this.lblAlertGreen.Name = "lblAlertGreen";
            this.lblAlertGreen.Size = new System.Drawing.Size(36, 13);
            this.lblAlertGreen.TabIndex = 3;
            this.lblAlertGreen.Text = "Green";
            // 
            // lblAlertAmber
            // 
            this.lblAlertAmber.AutoSize = true;
            this.lblAlertAmber.Location = new System.Drawing.Point(451, 106);
            this.lblAlertAmber.Name = "lblAlertAmber";
            this.lblAlertAmber.Size = new System.Drawing.Size(37, 13);
            this.lblAlertAmber.TabIndex = 4;
            this.lblAlertAmber.Text = "Amber";
            // 
            // lblAlertRed
            // 
            this.lblAlertRed.AutoSize = true;
            this.lblAlertRed.Location = new System.Drawing.Point(451, 136);
            this.lblAlertRed.Name = "lblAlertRed";
            this.lblAlertRed.Size = new System.Drawing.Size(27, 13);
            this.lblAlertRed.TabIndex = 5;
            this.lblAlertRed.Text = "Red";
            // 
            // lblAlertGreenCount
            // 
            this.lblAlertGreenCount.AutoSize = true;
            this.lblAlertGreenCount.BackColor = System.Drawing.Color.White;
            this.lblAlertGreenCount.Location = new System.Drawing.Point(488, 76);
            this.lblAlertGreenCount.MinimumSize = new System.Drawing.Size(13, 13);
            this.lblAlertGreenCount.Name = "lblAlertGreenCount";
            this.lblAlertGreenCount.Size = new System.Drawing.Size(13, 13);
            this.lblAlertGreenCount.TabIndex = 6;
            this.lblAlertGreenCount.Text = "0";
            // 
            // lblAlertAmberCount
            // 
            this.lblAlertAmberCount.AutoSize = true;
            this.lblAlertAmberCount.BackColor = System.Drawing.Color.White;
            this.lblAlertAmberCount.Location = new System.Drawing.Point(488, 106);
            this.lblAlertAmberCount.Name = "lblAlertAmberCount";
            this.lblAlertAmberCount.Size = new System.Drawing.Size(13, 13);
            this.lblAlertAmberCount.TabIndex = 7;
            this.lblAlertAmberCount.Text = "0";
            // 
            // lblAlertRedCount
            // 
            this.lblAlertRedCount.AutoSize = true;
            this.lblAlertRedCount.BackColor = System.Drawing.Color.White;
            this.lblAlertRedCount.Location = new System.Drawing.Point(488, 136);
            this.lblAlertRedCount.Name = "lblAlertRedCount";
            this.lblAlertRedCount.Size = new System.Drawing.Size(13, 13);
            this.lblAlertRedCount.TabIndex = 8;
            this.lblAlertRedCount.Text = "0";
            // 
            // lblNumberOfAlerts
            // 
            this.lblNumberOfAlerts.AutoSize = true;
            this.lblNumberOfAlerts.Location = new System.Drawing.Point(420, 32);
            this.lblNumberOfAlerts.Name = "lblNumberOfAlerts";
            this.lblNumberOfAlerts.Size = new System.Drawing.Size(115, 13);
            this.lblNumberOfAlerts.TabIndex = 9;
            this.lblNumberOfAlerts.Text = "Number of alerts since:";
            // 
            // lblAlertsSinceDateTime
            // 
            this.lblAlertsSinceDateTime.AutoSize = true;
            this.lblAlertsSinceDateTime.Location = new System.Drawing.Point(422, 48);
            this.lblAlertsSinceDateTime.Name = "lblAlertsSinceDateTime";
            this.lblAlertsSinceDateTime.Size = new System.Drawing.Size(110, 13);
            this.lblAlertsSinceDateTime.TabIndex = 10;
            this.lblAlertsSinceDateTime.Text = "2009/01/01 12:00:00";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.serviceToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(555, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            this.serviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.serviceToolStripMenuItem.Text = "&Service";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.pauseToolStripMenuItem.Text = "&Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.continueToolStripMenuItem.Text = "&Continue";
            this.continueToolStripMenuItem.Click += new System.EventHandler(this.continueToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.stopToolStripMenuItem.Text = "S&top";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.connectionTestsToolStripMenuItem,
            this.importToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // connectionTestsToolStripMenuItem
            // 
            this.connectionTestsToolStripMenuItem.Name = "connectionTestsToolStripMenuItem";
            this.connectionTestsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.connectionTestsToolStripMenuItem.Text = "&Connection Tests";
            this.connectionTestsToolStripMenuItem.Click += new System.EventHandler(this.connectionTestsToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.importToolStripMenuItem.Text = "Import Data";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importTestsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusVersionNumber,
            this.toolStripStatusLastUpdated,
            this.toolStripStatusLastUpdatedDateTime,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 263);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(555, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "Status Strip";
            // 
            // toolStripStatusVersionNumber
            // 
            this.toolStripStatusVersionNumber.Name = "toolStripStatusVersionNumber";
            this.toolStripStatusVersionNumber.Padding = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.toolStripStatusVersionNumber.Size = new System.Drawing.Size(152, 17);
            this.toolStripStatusVersionNumber.Text = "v 0.0.0.1";
            this.toolStripStatusVersionNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLastUpdated
            // 
            this.toolStripStatusLastUpdated.Name = "toolStripStatusLastUpdated";
            this.toolStripStatusLastUpdated.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripStatusLastUpdated.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLastUpdated.Text = "Last updated:";
            this.toolStripStatusLastUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLastUpdatedDateTime
            // 
            this.toolStripStatusLastUpdatedDateTime.Name = "toolStripStatusLastUpdatedDateTime";
            this.toolStripStatusLastUpdatedDateTime.Size = new System.Drawing.Size(110, 17);
            this.toolStripStatusLastUpdatedDateTime.Text = "2009/01/01 00:00:00";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lblStatus.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(195, 17);
            this.lblStatus.Text = "Stopped";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // ServiceMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 285);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblAlertsSinceDateTime);
            this.Controls.Add(this.lblNumberOfAlerts);
            this.Controls.Add(this.lblAlertRedCount);
            this.Controls.Add(this.lblAlertAmberCount);
            this.Controls.Add(this.lblAlertGreenCount);
            this.Controls.Add(this.lblAlertRed);
            this.Controls.Add(this.lblAlertAmber);
            this.Controls.Add(this.lblAlertGreen);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceMonitor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site Status Loader Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.ServiceMonitor_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceMonitor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblAlertGreen;
        private System.Windows.Forms.Label lblAlertAmber;
        private System.Windows.Forms.Label lblAlertRed;
        private System.Windows.Forms.Label lblAlertGreenCount;
        private System.Windows.Forms.Label lblAlertAmberCount;
        private System.Windows.Forms.Label lblAlertRedCount;
        private System.Windows.Forms.Label lblNumberOfAlerts;
        private System.Windows.Forms.Label lblAlertsSinceDateTime;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLastUpdated;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLastUpdatedDateTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusVersionNumber;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
    }
}