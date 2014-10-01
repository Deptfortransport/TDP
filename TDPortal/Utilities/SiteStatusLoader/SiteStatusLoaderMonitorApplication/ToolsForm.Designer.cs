namespace AO.SiteStatusLoaderMonitorApplication
{
    partial class ToolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolsForm));
            this.lblSiteStatusRealTime = new System.Windows.Forms.Label();
            this.btnSiteStatusRealTimeTest = new System.Windows.Forms.Button();
            this.txtTestStatus = new System.Windows.Forms.TextBox();
            this.lblReportStagingDB = new System.Windows.Forms.Label();
            this.btnReportStagingDBTest = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSiteStatusHistoric = new System.Windows.Forms.Label();
            this.btnSiteStatusHistoricTest = new System.Windows.Forms.Button();
            this.txtUrlRealTime = new System.Windows.Forms.TextBox();
            this.txtUrlHistoric = new System.Windows.Forms.TextBox();
            this.txtReportStagingDB = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
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
            // btnSiteStatusRealTimeTest
            // 
            this.btnSiteStatusRealTimeTest.Location = new System.Drawing.Point(478, 23);
            this.btnSiteStatusRealTimeTest.Name = "btnSiteStatusRealTimeTest";
            this.btnSiteStatusRealTimeTest.Size = new System.Drawing.Size(75, 23);
            this.btnSiteStatusRealTimeTest.TabIndex = 1;
            this.btnSiteStatusRealTimeTest.Text = "Test";
            this.btnSiteStatusRealTimeTest.UseVisualStyleBackColor = true;
            this.btnSiteStatusRealTimeTest.Click += new System.EventHandler(this.btnSiteStatusRealTimeTest_Click);
            // 
            // txtTestStatus
            // 
            this.txtTestStatus.BackColor = System.Drawing.Color.White;
            this.txtTestStatus.Location = new System.Drawing.Point(4, 150);
            this.txtTestStatus.Multiline = true;
            this.txtTestStatus.Name = "txtTestStatus";
            this.txtTestStatus.ReadOnly = true;
            this.txtTestStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestStatus.Size = new System.Drawing.Size(549, 105);
            this.txtTestStatus.TabIndex = 6;
            this.txtTestStatus.TabStop = false;
            // 
            // lblReportStagingDB
            // 
            this.lblReportStagingDB.AutoSize = true;
            this.lblReportStagingDB.Location = new System.Drawing.Point(1, 101);
            this.lblReportStagingDB.Name = "lblReportStagingDB";
            this.lblReportStagingDB.Size = new System.Drawing.Size(127, 13);
            this.lblReportStagingDB.TabIndex = 101;
            this.lblReportStagingDB.Text = "Report Staging Database";
            // 
            // btnReportStagingDBTest
            // 
            this.btnReportStagingDBTest.Location = new System.Drawing.Point(478, 117);
            this.btnReportStagingDBTest.Name = "btnReportStagingDBTest";
            this.btnReportStagingDBTest.Size = new System.Drawing.Size(75, 23);
            this.btnReportStagingDBTest.TabIndex = 5;
            this.btnReportStagingDBTest.Text = "Test";
            this.btnReportStagingDBTest.UseVisualStyleBackColor = true;
            this.btnReportStagingDBTest.Click += new System.EventHandler(this.btnReportStagingDBTest_Click);
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
            // btnSiteStatusHistoricTest
            // 
            this.btnSiteStatusHistoricTest.Location = new System.Drawing.Point(478, 67);
            this.btnSiteStatusHistoricTest.Name = "btnSiteStatusHistoricTest";
            this.btnSiteStatusHistoricTest.Size = new System.Drawing.Size(75, 23);
            this.btnSiteStatusHistoricTest.TabIndex = 3;
            this.btnSiteStatusHistoricTest.Text = "Test";
            this.btnSiteStatusHistoricTest.UseVisualStyleBackColor = true;
            this.btnSiteStatusHistoricTest.Click += new System.EventHandler(this.btnSiteStatusHistoricTest_Click);
            // 
            // txtUrlRealTime
            // 
            this.txtUrlRealTime.Location = new System.Drawing.Point(4, 24);
            this.txtUrlRealTime.Name = "txtUrlRealTime";
            this.txtUrlRealTime.Size = new System.Drawing.Size(468, 20);
            this.txtUrlRealTime.TabIndex = 0;
            this.txtUrlRealTime.Text = "http://www.test.com";
            // 
            // txtUrlHistoric
            // 
            this.txtUrlHistoric.Location = new System.Drawing.Point(4, 68);
            this.txtUrlHistoric.Name = "txtUrlHistoric";
            this.txtUrlHistoric.Size = new System.Drawing.Size(468, 20);
            this.txtUrlHistoric.TabIndex = 2;
            this.txtUrlHistoric.Text = "http://www.test.com";
            // 
            // txtReportStagingDB
            // 
            this.txtReportStagingDB.Location = new System.Drawing.Point(4, 118);
            this.txtReportStagingDB.Name = "txtReportStagingDB";
            this.txtReportStagingDB.Size = new System.Drawing.Size(468, 20);
            this.txtReportStagingDB.TabIndex = 4;
            this.txtReportStagingDB.Text = "Server=.";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(397, 261);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 103;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 287);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtReportStagingDB);
            this.Controls.Add(this.txtUrlHistoric);
            this.Controls.Add(this.txtUrlRealTime);
            this.Controls.Add(this.btnSiteStatusHistoricTest);
            this.Controls.Add(this.lblSiteStatusHistoric);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReportStagingDBTest);
            this.Controls.Add(this.lblReportStagingDB);
            this.Controls.Add(this.txtTestStatus);
            this.Controls.Add(this.btnSiteStatusRealTimeTest);
            this.Controls.Add(this.lblSiteStatusRealTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection Tests";
            this.Load += new System.EventHandler(this.ToolsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSiteStatusRealTime;
        private System.Windows.Forms.Button btnSiteStatusRealTimeTest;
        private System.Windows.Forms.TextBox txtTestStatus;
        private System.Windows.Forms.Label lblReportStagingDB;
        private System.Windows.Forms.Button btnReportStagingDBTest;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSiteStatusHistoric;
        private System.Windows.Forms.Button btnSiteStatusHistoricTest;
        private System.Windows.Forms.TextBox txtUrlRealTime;
        private System.Windows.Forms.TextBox txtUrlHistoric;
        private System.Windows.Forms.TextBox txtReportStagingDB;
        private System.Windows.Forms.Button btnReset;
    }
}