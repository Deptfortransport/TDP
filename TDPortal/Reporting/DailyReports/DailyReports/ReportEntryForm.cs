using System;
using DailyReportFramework;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Configuration;

namespace DailyReports
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class ReportEntryForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new ReportEntryForm());
        }

        #region Private members

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private string selectString;
        private string[] reportTitle = new string[50];
        private int reportNumInt;
        private string reportNumString;
        private int ReportNo;
        private EventLog EventLogger;

        private SqlConnection reportingSqlConnection;
        private SqlDataReader myDataReader;
        private SqlCommand mySqlCommand;
        private string propertyConnectionString;
        private string reportingConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button PrintSelectedButton;
        private int statusFlag;
        private System.Windows.Forms.Label Results;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.CheckedListBox checkedListBox1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion

        #region Form Constructor

        public ReportEntryForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();


            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";


            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];

            reportingSqlConnection = new SqlConnection(propertyConnectionString);

            selectString = "select reportNumber,value from ReportProperties "
                + "where propertykey = 'Title' order by reportNumber";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (myDataReader.Read())
                {
                    ReportNo++;
                    reportNumString = myDataReader.GetSqlString(0).ToString();

                    reportNumInt = Convert.ToInt16(reportNumString);
                    reportTitle[reportNumInt] = reportNumString
                        + " " +
                      (myDataReader.GetSqlString(1).ToString());
                    reportTitle[reportNumInt] = reportTitle[reportNumInt].Replace("%YY-MM-DD%", " ");
                    reportTitle[reportNumInt] = reportTitle[reportNumInt].Replace("%YY-MM%", " ");

                } // end of while
                myDataReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failure reading reporting properties table " + e.ToString(), "Error Reading Parameters");
                EventLogger.WriteEntry("Failure reading reporting properties table " + e.ToString(), EventLogEntryType.Error);
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
            for (int i = 1; i <= ReportNo; i++)
            {
                checkedListBox1.Items.Add(reportTitle[i]);
            }
            checkedListBox1.SelectedIndex = 0;
        }

        #endregion

        #region Events

        private void PrintSelectedButton_Click(object sender, System.EventArgs e)
        {
            DailyReportController newReport = new DailyReportController();
            DateTime reportDate = dateTimePicker1.Value;

            newReport.ReportDate = reportDate;
            PrintSelectedButton.Enabled = false;
            Results.Visible = true;

            for (int i = 1; i <= ReportNo; i++)
            {
                if (checkedListBox1.GetItemCheckState(i - 1) == CheckState.Checked)
                {
                    statusFlag = newReport.ProduceReport(i);
                    if (statusFlag == 0)
                    {
                        Results.Text = "Report " + i.ToString() + " has printed successfully.";
                    }
                    else
                    {
                        StatusCode statusCode = StatusCode.UnknownStatusCode;

                        try
                        { // Get text version of status code
                            statusCode = (StatusCode)statusFlag;
                        }
                        catch
                        { // ignore, status code hasn;t been enum'd yet
                        }

                        Results.Text = string.Format("Report {0} has failed with status {1} {2}", i.ToString(), statusFlag.ToString(), statusCode.ToString());
                    }

                }
            }
            ContinueButton.Visible = true;
        }

        private void ReportEntryForm_Load(object sender, System.EventArgs e)
        {

        }

        private void ContinueButton_Click(object sender, System.EventArgs e)
        {
            ContinueButton.Visible = false;
            Results.Text = " ";
            Results.Visible = false;
            PrintSelectedButton.Enabled = true;

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PrintSelectedButton = new System.Windows.Forms.Button();
            this.Results = new System.Windows.Forms.Label();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(176, 16);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Date to report on:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Report to Print";
            // 
            // PrintSelectedButton
            // 
            this.PrintSelectedButton.Location = new System.Drawing.Point(16, 112);
            this.PrintSelectedButton.Name = "PrintSelectedButton";
            this.PrintSelectedButton.Size = new System.Drawing.Size(128, 40);
            this.PrintSelectedButton.TabIndex = 4;
            this.PrintSelectedButton.Text = "Press to Print";
            this.PrintSelectedButton.Click += new System.EventHandler(this.PrintSelectedButton_Click);
            // 
            // Results
            // 
            this.Results.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Results.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Results.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Results.Location = new System.Drawing.Point(16, 336);
            this.Results.Name = "Results";
            this.Results.Size = new System.Drawing.Size(480, 48);
            this.Results.TabIndex = 5;
            this.Results.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Results.Visible = false;
            // 
            // ContinueButton
            // 
            this.ContinueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.ContinueButton.Location = new System.Drawing.Point(352, 392);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(144, 24);
            this.ContinueButton.TabIndex = 6;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.Visible = false;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Location = new System.Drawing.Point(176, 64);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(288, 259);
            this.checkedListBox1.TabIndex = 7;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // ReportEntryForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(528, 429);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.checkedListBox1,
																		  this.ContinueButton,
																		  this.Results,
																		  this.PrintSelectedButton,
																		  this.label2,
																		  this.label1,
																		  this.dateTimePicker1});
            this.Name = "ReportEntryForm";
            this.Text = "Daily Report Runner";
            this.Load += new System.EventHandler(this.ReportEntryForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
