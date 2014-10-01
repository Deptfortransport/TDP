// *********************************************** 
// NAME                 : ToolsForm.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/04/2009
// DESCRIPTION			: Tools form - provides options to perform connection tests to a URL and Database
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/ImportForm.cs-arc  $
//
//   Rev 1.0   Aug 24 2011 11:11:42   mmodi
//Initial revision.
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
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using AO.Common;
using AO.DatabaseInfrastructure;
using AO.EventLogging;
using AO.SiteStatusLoaderEvents;

using PropertyService = AO.Properties.Properties;
using Logger = System.Diagnostics.Trace;

namespace AO.SiteStatusLoaderMonitorApplication
{
    public partial class ImportForm : Form
    {
        #region Private members

        private const string IMPORTSUCCESS = "Import completed successfully.";
        private const string IMPORTFAIL = "Import failed.";
        private const string IMPORTERROR = "Error occurred during import: ";
                
        private string applicationName = string.Empty;

        private bool importInProgress = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ImportForm(string applicationName)
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
            txtRealTimeFile.Text = string.Empty;
            txtHistoricFile.Text = string.Empty;
        }

        /// <summary>
        /// Sets all the Test buttons enabled status
        /// </summary>
        /// <param name="enable"></param>
        private void EnableImportButtons(bool enable)
        {
            btnRealTimeImport.Enabled = enable;
            btnHistoricImport.Enabled = enable;
            btnClose.Enabled = enable;
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
        private void ImportForm_Load(object sender, System.EventArgs e)
        {

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

        #region Import events

        /// <summary>
        /// Event handler for the real time data import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRealTimeImport_Click(object sender, EventArgs e)
        {
            // Prevent any other Tests from starting
            EnableImportButtons(false);

            if (!importInProgress)
            {
                importInProgress = true;
                
                SetInputBoxColour(txtRealTimeFile, Color.White, Color.Black);

                // Refocus on the text box for test being done
                SetFocus(txtRealTimeFile);
                
                StringBuilder testStatus = new StringBuilder();

                testStatus.AppendLine("Starting site status \"real time\" import");
                txtTestStatus.Text = testStatus.ToString();
                this.Refresh();

                // Read the location details of the data
                string filepath = txtRealTimeFile.Text.Trim();
                
                try
                {
                    #region Perform import

                    testStatus.AppendLine("Loading xml data from file [" + filepath + "]");
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    // Get the realtime data
                    XmlDocument siteStatusXmlDoc =  new XmlDocument();

                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.XmlResolver = null;
                    settings.ValidationType = ValidationType.None;
                    settings.ProhibitDtd = false;

                    using (XmlReader reader = XmlReader.Create(filepath, settings))
                    {
                        siteStatusXmlDoc.Load(reader);
                    }
                    
                    // Convert into useable EventStatus objects
                    EventStatus[] eventStatusArray = EventStatusParser.Instance.ConvertXMLToEventStatus(siteStatusXmlDoc);
                    
                    testStatus.AppendLine(string.Format("{0} event status objects read", eventStatusArray.Length.ToString()));
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    if (eventStatusArray.Length > 0)
                    {
                        testStatus.AppendLine("Updating alert levels for events");
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        eventStatusArray = EventStatusAlertSettings.Instance.UpdateAlertLevelsForEvents(eventStatusArray);

                        testStatus.AppendLine("Importing...");
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        foreach (EventStatus eventStatus in eventStatusArray)
                        {
                            // Log a ReferenceTransaction event to the database
                            ReferenceTransactionEvent rte = new ReferenceTransactionEvent(
                                eventStatus.ReferenceTransactionType,
                                eventStatus.SlaTransaction,
                                eventStatus.TimeSubmitted,
                                eventStatus.TimeCompleted,
                                applicationName,
                                eventStatus.Successful,
                                false);

                            Logger.Write(rte);

                            testStatus.AppendLine();
                            testStatus.AppendLine(eventStatus.ToString());
                            txtTestStatus.Text = testStatus.ToString();
                            this.Refresh();
                        }

                        SetInputBoxColour(txtRealTimeFile, Color.Green, Color.White);
                    }

                    testStatus.AppendLine();
                    testStatus.AppendLine(IMPORTSUCCESS);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    #endregion
                }
                #region Error handling

                catch (SSException ssEx)
                {
                    SetInputBoxColour(txtRealTimeFile, Color.Red, Color.White);
                    
                    testStatus.AppendLine();
                    testStatus.AppendLine(IMPORTERROR);
                    testStatus.AppendLine(ssEx.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(IMPORTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    SetInputBoxColour(txtRealTimeFile, Color.Red, Color.White);

                    testStatus.AppendLine();
                    testStatus.AppendLine(IMPORTERROR);
                    testStatus.AppendLine(ex.Message);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    testStatus.AppendLine();
                    testStatus.AppendLine(IMPORTFAIL);
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();
                }

                #endregion

                importInProgress = false;
            }
            
            EnableImportButtons(true);
        }

        /// <summary>
        /// Event handler for the historic data import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHistoricImport_Click(object sender, EventArgs e)
        {
            // Prevent any other Imports from starting
            EnableImportButtons(false);

            if ((MessageBox.Show(
                "Ensure the PID is correctly populated for the Historic data file selected",
                "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                &&
                (!string.IsNullOrEmpty(txtHistoricFilePID.Text)))
            {
                if (!importInProgress)
                {
                    importInProgress = true;

                    SetInputBoxColour(txtHistoricFile, Color.White, Color.Black);
                    SetInputBoxColour(txtHistoricFilePID, Color.White, Color.Black);

                    // Refocus on the text box for test being done
                    SetFocus(txtHistoricFile);

                    StringBuilder testStatus = new StringBuilder();

                    testStatus.AppendLine("Starting site status \"historic\" import");
                    txtTestStatus.Text = testStatus.ToString();
                    this.Refresh();

                    // Read the location details of the data
                    string filepath = txtHistoricFile.Text.Trim();
                    string pid = txtHistoricFilePID.Text;

                    try
                    {
                        #region Perform import

                        testStatus.AppendLine("Loading csv data from file [" + filepath + "]");
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        // Get the realtime data
                        CSVReader csvReader = new CSVReader(filepath);

                        // Convert into useable EventStatus objects
                        EventStatus[] eventStatusArray = EventStatusParser.Instance.ConvertCSVToEventStatus(csvReader, pid);

                        testStatus.AppendLine(string.Format("{0} event status objects read", eventStatusArray.Length.ToString()));
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        if (eventStatusArray.Length > 0)
                        {
                            testStatus.AppendLine("Updating alert levels for events");
                            txtTestStatus.Text = testStatus.ToString();
                            this.Refresh();

                            eventStatusArray = EventStatusAlertSettings.Instance.UpdateAlertLevelsForEvents(eventStatusArray);

                            testStatus.AppendLine("Importing...");
                            txtTestStatus.Text = testStatus.ToString();
                            this.Refresh();

                            int refreshCount = 0;

                            foreach (EventStatus eventStatus in eventStatusArray)
                            {
                                // Log a ReferenceTransaction event to the database
                                ReferenceTransactionEvent rte = new ReferenceTransactionEvent(
                                    eventStatus.ReferenceTransactionType,
                                    eventStatus.SlaTransaction,
                                    eventStatus.TimeSubmitted,
                                    eventStatus.TimeCompleted,
                                    applicationName,
                                    eventStatus.Successful,
                                    false);

                                Logger.Write(rte);

                                testStatus.AppendLine(eventStatus.ToString());

                                // Because there may be 100's of these, only refresh at set intervals
                                refreshCount++;
                                if ((refreshCount % 100) == 0)
                                {
                                    txtTestStatus.Text = testStatus.ToString();
                                    this.Refresh();
                                }
                            }

                            SetInputBoxColour(txtHistoricFile, Color.Green, Color.White);
                            SetInputBoxColour(txtHistoricFilePID, Color.Green, Color.White);
                        }

                        testStatus.AppendLine();
                        testStatus.AppendLine(IMPORTSUCCESS);
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        #endregion
                    }
                    #region Error handling

                    catch (SSException ssEx)
                    {
                        SetInputBoxColour(txtHistoricFile, Color.Red, Color.White);
                        SetInputBoxColour(txtHistoricFilePID, Color.Red, Color.White);

                        testStatus.AppendLine();
                        testStatus.AppendLine(IMPORTERROR);
                        testStatus.AppendLine(ssEx.Message);
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        testStatus.AppendLine();
                        testStatus.AppendLine(IMPORTFAIL);
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();
                    }
                    catch (Exception ex)
                    {
                        SetInputBoxColour(txtHistoricFile, Color.Red, Color.White);
                        SetInputBoxColour(txtHistoricFilePID, Color.Red, Color.White);

                        testStatus.AppendLine();
                        testStatus.AppendLine(IMPORTERROR);
                        testStatus.AppendLine(ex.Message);
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();

                        testStatus.AppendLine();
                        testStatus.AppendLine(IMPORTFAIL);
                        txtTestStatus.Text = testStatus.ToString();
                        this.Refresh();
                    }

                    #endregion

                    importInProgress = false;
                }
            }

            EnableImportButtons(true);
        }
        
        #endregion

        /// <summary>
        /// Event handler to select realtime data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenRealtimeFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtRealTimeFile.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Event handler to select historic data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenHistoricFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtHistoricFile.Text = openFileDialog.FileName;

                // Try and get a PID out of the filename, simple attempt
                // assuming filename is in format "20110823010000-PID7439-HistoricStatus.csv"
                try
                {
                    if (!string.IsNullOrEmpty(txtHistoricFile.Text))
                    {
                        string filename = txtHistoricFile.Text;

                        int start = filename.IndexOf("PID");
                        int end = filename.IndexOf("-", start);

                        string pid = filename.Substring((start + 3), (end - start - 3));

                        txtHistoricFilePID.Text = pid;
                    }
                }
                catch
                {
                    // Ignore any exceptions, user can ensure PID is correct
                }
            }
        }

        #endregion
    }
}