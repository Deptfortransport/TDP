// *********************************************** 
// NAME                 : ToolsForm.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/04/2009
// DESCRIPTION			: Tools form - provides options to perform connection tests to a URL and Database
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/ToolsForm.cs-arc  $
//
//   Rev 1.3   Aug 24 2011 11:08:00   mmodi
//Added a manual import facility to the SSL UI tool
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Apr 09 2009 15:06:58   mmodi
//Allow user to specify Database connection string for Test
//Resolution for 5273: Site Status Loader
//
//   Rev 1.1   Apr 09 2009 10:47:50   mmodi
//Added colours to indicate test success
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 06 2009 16:17:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using AO.Common;
using AO.DatabaseInfrastructure;

using PropertyService = AO.Properties.Properties;

namespace AO.SiteStatusLoaderMonitorApplication
{
    public partial class ToolsForm : Form
    {
        #region Private members

        private const string CONNECTIONTESTSUCCESS = "Connection test completed successfully.";
        private const string CONNECTIONTESTFAIL = "Connection test failed.";
        private const string CONNECTIONTESTERROR = "Error occurred during connection test: ";

        private string applicationName = string.Empty;

        private bool testInProgress = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ToolsForm(string applicationName)
        {
            this.applicationName = applicationName;

            InitializeComponent();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Populates the text boxes with their default values
        /// </summary>
        private void PopulateTextBoxes()
        {
            // Set the url textboxes
            txtUrlRealTime.Text = PropertyService.Instance["SiteStatusLoaderService.Current.URL"];
            txtUrlHistoric.Text = GenerateHistoricStatusURL();
            txtReportStagingDB.Text = PropertyService.Instance["ReportStagingDB"];
        }

        /// <summary>
        /// Sets all the Test buttons enabled status
        /// </summary>
        /// <param name="enable"></param>
        private void EnableTestButtons(bool enable)
        {
            btnSiteStatusRealTimeTest.Enabled = enable;
            btnSiteStatusHistoricTest.Enabled = enable;
            btnReportStagingDBTest.Enabled = enable;
            btnClose.Enabled = enable;
            btnReset.Enabled = enable;
        }

        /// <summary>
        /// Sets the back colour of the test input boxes back to default
        /// </summary>
        /// <param name="control"></param>
        /// <param name="backColour"></param>
        /// <param name="foreColour"></param>
        private void SetInputBoxColour(Control control, Color backColour, Color foreColour)
        {
            control.BackColor = backColour;
            control.ForeColor = foreColour;
        }

        /// <summary>
        /// Generates the url for the historic site status data. Uses the first PID in the array if defined
        /// </summary>
        private string GenerateHistoricStatusURL()
        {
            string urlBase = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Base"];
            string urlPIDs = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Base.PID"];
            string urlPIDPrefix = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Base.PIDPrefix"];

            // Get the Page ID for the url
            string[] urlPIDArray = urlPIDs.Split(',');

            string urlFile = string.Empty;

            if ((urlPIDArray != null) && (urlPIDArray.Length > 0))
            {
                // Test against the first Page ID only
                urlFile = urlBase + urlPIDPrefix + urlPIDArray[0];
            }
            else
            {
                // Otherwise no Page ID, so use base only
                urlFile = urlBase;
            }

            return urlFile;
        }

        /// <summary>
        /// Open and closes a connection to the ReportStagingDB
        /// </summary>
        private void PerformDatabaseConnectionTest(ref StringBuilder testStatus)
        {
            SqlHelper sqlHelper = new SqlHelper();

            try
            {
                testStatus.AppendLine("Opening connection to ReportStagingDB");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                if (!string.IsNullOrEmpty(txtReportStagingDB.Text.Trim()))
                {
                    sqlHelper.ConnOpen(txtReportStagingDB.Text.Trim());
                }
                else
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.ReportStagingDB);
                }

                testStatus.AppendLine("Connection is open");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                System.Threading.Thread.Sleep(100);

                testStatus.AppendLine("Closing connection to ReportStagingDB");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                sqlHelper.ConnClose();

                testStatus.AppendLine("Connection is closed");
                txtTestStatus.Text = testStatus.ToString();

                testStatus.AppendLine();
                testStatus.AppendLine(CONNECTIONTESTSUCCESS);
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();
            }
            catch (SqlException sqlEx) // SQLHelper does not catch SqlException so catch here.
            {
                // If this is a primary key violation, then throw the relevant exception
                if (sqlEx.Number == 2627)
                {
                    throw new SSException(sqlEx.Message, false, SSExceptionIdentifier.DBSQLHelperPrimaryKeyViolation);
                }
                else
                {
                    throw new SSException(sqlEx.Message, false, SSExceptionIdentifier.DBError);
                }
            }
            catch (SqlTypeException ste)
            {
                throw new SSException(ste.Message, false, SSExceptionIdentifier.DBError);
            }
            catch (Exception ex)
            {
                throw new SSException(ex.Message, false, SSExceptionIdentifier.DBError);
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                {
                    sqlHelper.ConnClose();
                }

                sqlHelper.Dispose();
            }
        }

        /// <summary>
        /// Sets the focus on the supplied control, deselecting any text
        /// </summary>
        /// <param name="control"></param>
        private void SetFocus(Control control)
        {
            control.Focus();
            
            if (control.GetType() == typeof(TextBox))
            {
                TextBox txtBox = (TextBox)control;
                txtBox.DeselectAll();
            }
        }
        #endregion

        #region Event handlers

        /// <summary>
        /// Load event of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolsForm_Load(object sender, System.EventArgs e)
        {
            this.Text = applicationName + " - Connection Tests";

            PopulateTextBoxes();

            SetFocus(txtUrlRealTime);
        }
        
        /// <summary>
        /// Close button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Reset  button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            SetInputBoxColour(txtReportStagingDB, Color.White, Color.Black);
            SetInputBoxColour(txtUrlHistoric, Color.White, Color.Black);
            SetInputBoxColour(txtUrlRealTime, Color.White, Color.Black);

            PopulateTextBoxes();

            txtTestStatus.Text = string.Empty;

            SetFocus(txtUrlRealTime);
        }

        /// <summary>
        /// Method which is called by the windows processing. 
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            // Stop the form from being moved (thus forcing user to close form to return to the main form)
            if (WinApi.IsMovingForm(ref message))
            {
                return;
            }

            base.WndProc(ref message);
        }

        #region Connnection tests
        /// <summary>
        /// Event handler for the Site Status real time url connection test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteStatusRealTimeTest_Click(object sender, EventArgs e)
        {
            // Prevent any other Tests from starting
            EnableTestButtons(false);

            if (!testInProgress)
            {
                testInProgress = true;
                
                SetInputBoxColour(txtUrlRealTime, Color.White, Color.Black);

                // Refocus on the text box for test being done
                SetFocus(txtUrlRealTime);
                
                StringBuilder testStatus = new StringBuilder();

                testStatus.AppendLine("Starting site status \"real time\" url connection test");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                // Read the location details of the data
                //string url = PropertyService.Instance["SiteStatusLoaderService.Current.URL"];
                string url = txtUrlRealTime.Text.Trim();
                string username = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Username"];
                string password = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Password"];
                string domain = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Domain"];
                string proxy = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Proxy"];

                try
                {
                    #region Perform test

                    testStatus.AppendLine("Retrieving xml data from URL [" + url + "]");
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    // Get the status data
                    XmlDocument siteStatusXmlDoc = HttpAccess.Instance.GetXMLFromHttp(
                        url, username, password, domain, proxy);

                    testStatus.AppendLine("Data returned from URL.");
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine("First 200 characters of xml data:");
                    if (siteStatusXmlDoc.InnerXml.Length > 200)
                    {
                        testStatus.AppendLine(siteStatusXmlDoc.InnerXml.Substring(0, 200));
                    }
                    else
                    {
                        testStatus.AppendLine(siteStatusXmlDoc.InnerXml);
                    }
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTSUCCESS);
                    txtTestStatus.Text = testStatus.ToString();

                    SetInputBoxColour(txtUrlRealTime, Color.Green, Color.White);
                    this.Refresh();

                    #endregion
                }
                catch (SSException ssEx)
                {
                    SetInputBoxColour(txtUrlRealTime, Color.Red, Color.White);

                    testStatus.AppendLine(CONNECTIONTESTERROR);
                    testStatus.AppendLine(ssEx.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    SetInputBoxColour(txtUrlRealTime, Color.Red, Color.White);

                    testStatus.AppendLine(CONNECTIONTESTERROR);
                    testStatus.AppendLine(ex.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }

                testInProgress = false;
            }
            
            EnableTestButtons(true);
        }

        /// <summary>
        /// Event handler for the Site Status real time url connection test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteStatusHistoricTest_Click(object sender, EventArgs e)
        {
            // Prevent any other Tests from starting
            EnableTestButtons(false);

            if (!testInProgress)
            {
                testInProgress = true;
                
                SetInputBoxColour(txtUrlHistoric, Color.White, Color.Black);

                // Refocus on the text box for test being done
                SetFocus(txtUrlHistoric);

                StringBuilder testStatus = new StringBuilder();

                testStatus.AppendLine("Starting site status \"historic\" url connection test");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                // Read the location details of the data

                string username = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Username"];
                string password = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Password"];
                string domain = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Domain"];
                string proxy = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Proxy"];

                try
                {
                    #region Perform test

                    string urlFile = txtUrlHistoric.Text;

                    testStatus.AppendLine("Retrieving csv data from URL [" + urlFile + "]");
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    // Get the status data
                    CSVReader csvReader = HttpAccess.Instance.GetCSVFromHTTP(
                        urlFile, username, password, domain, proxy);

                    testStatus.AppendLine("Data returned from URL.");
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine("First three rows in csv data:");

                    for (int i = 0; i < 3; i++)
                    {
                        string csvLine = csvReader.GetCSVRow(false);

                        if (string.IsNullOrEmpty(csvLine))
                        {
                            csvLine = string.Empty;
                        }

                        testStatus.AppendLine(csvLine);
                    }

                    txtTestStatus.Text = testStatus.ToString();               
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTSUCCESS);
                    txtTestStatus.Text = testStatus.ToString();

                    SetInputBoxColour(txtUrlHistoric, Color.Green, Color.White);

                    this.Refresh();

                    #endregion
                }
                catch (SSException ssEx)
                {
                    SetInputBoxColour(txtUrlHistoric, Color.Red, Color.White);

                    testStatus.AppendLine(CONNECTIONTESTERROR);
                    testStatus.AppendLine(ssEx.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    SetInputBoxColour(txtUrlHistoric, Color.Red, Color.White);

                    testStatus.AppendLine(CONNECTIONTESTERROR);
                    testStatus.AppendLine(ex.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }

                testInProgress = false;
            }

            EnableTestButtons(true);
        }

        /// <summary>
        /// Event handler for the Report Staging Database test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReportStagingDBTest_Click(object sender, EventArgs e)
        {
            // Prevent any other Tests from starting
            EnableTestButtons(false);

            if (!testInProgress)
            {
                testInProgress = true;
                
                SetInputBoxColour(txtReportStagingDB, Color.White, Color.Black);

                // Refocus on the text box for test being done
                SetFocus(txtReportStagingDB);
                
                StringBuilder testStatus = new StringBuilder();

                testStatus.AppendLine("Starting Report Staging database connection test");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                try
                {
                    PerformDatabaseConnectionTest(ref testStatus);

                    SetInputBoxColour(txtReportStagingDB, Color.Green, Color.White);

                    this.Refresh();
                }
                catch (SSException ssEx)
                {
                    SetInputBoxColour(txtReportStagingDB, Color.Red, Color.White);

                    testStatus.AppendLine(CONNECTIONTESTERROR);
                    testStatus.AppendLine(ssEx.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(CONNECTIONTESTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }

                testInProgress = false;
            }
            
            EnableTestButtons(true);
        }

        #endregion
        
        #endregion

    }
}