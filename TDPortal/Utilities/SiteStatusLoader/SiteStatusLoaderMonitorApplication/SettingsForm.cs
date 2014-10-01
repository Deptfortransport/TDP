// *********************************************** 
// NAME                 : SettingsForm.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/04/2009
// DESCRIPTION			: Settings form - allows user to change values in the properties file
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/SettingsForm.cs-arc  $
//
//   Rev 1.0   Apr 09 2009 10:45:00   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using Logger = System.Diagnostics.Trace;
using PropertyService = AO.Properties.Properties;
using AO.EventLogging;
using AO.Common;

namespace AO.SiteStatusLoaderMonitorApplication
{
    public partial class SettingsForm : Form
    {
        #region Enums

        private enum CurrentTab
        {
            Common,
            Service,
            Monitor
        }

        #endregion

        #region Private members
        
        // Config id value for property file location read from application Config file
        private const string PropertyFile = "propertyservice.providers.fileprovider.filepath";

        // DataGridView column names
        private const string ColumnPropertyName = "Property Name";
        private const string ColumnPropertyValue = "Property Value";

        private const string XmlPropertyNode = "property";

        private const string GID = "SiteStatus";
        private const string AIDService = "SiteStatusLoaderService";
        private const string AIDMonitor = "SiteStatusLoaderMonitor";
        private const string AIDCommon = "";

        private const string PropertyVersionNumber = "propertyservice.version";

        private string applicationName = string.Empty;

        // XmlDoc used to read and (temporarily) store any property value changes from the DataGridView
        private XmlDocument xmlDoc;

        // Flag used to track if the properties have been changed by user since they were loaded
        private bool propertiesEdited = false;
        
        // Dictionary to keep track of edited cells, to allow the edited cell colour to be changed
        private Dictionary<string, int> editedCellsCommon = new Dictionary<string, int>();
        private Dictionary<string, int> editedCellsMonitor = new Dictionary<string, int>();
        private Dictionary<string, int> editedCellsService = new Dictionary<string, int>();

        // Count used to prevent repeated "attempt to saves" messages being shown
        private int saveFileErrorCount = 0;

        // Keeps track of the current Tab view to allow the correct edited cells Dictionary to be used
        private CurrentTab currentTabSelected = CurrentTab.Common;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsForm(string applicationName)
        {
            this.applicationName = applicationName;

            InitializeComponent();
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Load event of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsForm_Load(object sender, System.EventArgs e)
        {
            this.Text = applicationName + " - Settings";

            SetupDataGrid();

            LoadSettings();

            EnableApplyButton(propertiesEdited);
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
        
        #region Button Click Events

        /// <summary>
        /// Click event for the Apply button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// Click event for the OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Only close if saved ok
            if (SaveSettings())
            {
                Close();
            }
        }

        /// <summary>
        /// Click event for the Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region DataGridView Events

        /// <summary>
        /// Event handler when a cell has completed being Edited by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // If this is the property value editing
            if (e.ColumnIndex == 1)
            {
                string propertyName = string.Empty;
                
                #region Get the property name for the value

                DataGridViewRow row = null;

                switch (currentTabSelected)
                {
                    case CurrentTab.Common:
                        row = dgvCommon.Rows[e.RowIndex];
                        break;
                    case CurrentTab.Monitor:
                        row = dgvMonitor.Rows[e.RowIndex];
                        break;
                    case CurrentTab.Service:
                        row = dgvService.Rows[e.RowIndex];
                        break;
                }

                #endregion

                if (row != null)
                {
                    DataGridViewCell cell = row.Cells[ColumnPropertyName];

                    propertyName = (string)cell.Value;

                    string aid = GetAid(currentTabSelected);
                    string gid = GID;
                    
                    // Now update the XmlDoc which is maintaining the properties loaded in the data grids
                    XmlNode node = xmlDoc.SelectSingleNode("//property[@name='" 
                        + propertyName + "' and @AID='" 
                        + aid + "' and @GID='" 
                        + gid + "']");

                    if (node != null)
                    {
                        node.InnerText = (string)e.Value;

                        // Set the global flag
                        propertiesEdited = true;

                        // Add this property name to the edited list
                        AddEditedCell(currentTabSelected, propertyName, -1);
                    }
                }
                
            }

            EnableApplyButton(propertiesEdited);
        }

        /// <summary>
        /// Event handler to apply specific cell format for the datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // If a cell has been edited, then change its colour so user knows it has been
            if (e.ColumnIndex == 0)
            {
                if (e.Value != null)
                {
                    string propertyName = (string)e.Value;

                    // Ensure the the correct RowIndex is associated with the Property value that was edited
                    if (HasPropertyBeenEdited(currentTabSelected, propertyName))
                    {
                        AddEditedCell(currentTabSelected, propertyName, e.RowIndex);
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                // Set edited cell colour
                if (DoesEditedCellsContainValue(currentTabSelected, e.RowIndex))
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// Event handler when a column Sort has been completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Sorted(object sender, System.EventArgs e)
        {
            // Ensures once a sort has been completed, the correct RowIndex is associated with the Property
            // that was edited
            foreach (DataGridViewRow row in dgvCommon.Rows)
            {
                string propertyName = (string)row.Cells[ColumnPropertyName].Value;

                if (HasPropertyBeenEdited(currentTabSelected, propertyName))
                {
                    AddEditedCell(currentTabSelected, propertyName, row.Index);
                }
            }
        }

        #endregion

        #region Tab Control events
        /// <summary>
        /// Event handler when a tab is selected.
        /// Updates the global current selected tab value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    currentTabSelected = CurrentTab.Common;
                    break;
                case 1:
                    currentTabSelected = CurrentTab.Monitor;
                    break;
                case 2:
                    currentTabSelected = CurrentTab.Service;
                    break;
                default:
                    currentTabSelected = CurrentTab.Common;
                    tabControl1.SelectedTab = tabCommon;
                    break;
            }
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Sets up the datagrid, manually adding the columns to be shown
        /// </summary>
        private void SetupDataGrid()
        {
            #region Set-up datagrids

            dgvCommon.AutoGenerateColumns = false;
            dgvMonitor.AutoGenerateColumns = false;
            dgvService.AutoGenerateColumns = false;

            // Set up the cell style for the Property Name column
            DataGridViewCellStyle columnPropertyNameStyle = new DataGridViewCellStyle();
            columnPropertyNameStyle.ForeColor = Color.Black;
            columnPropertyNameStyle.BackColor = Color.LightGray;
            columnPropertyNameStyle.SelectionForeColor = Color.Black;
            columnPropertyNameStyle.SelectionBackColor = Color.LightGray;

            // Set up the columns
            DataGridViewColumn column;

            // Column 1 - Property Name
            column = new DataGridViewTextBoxColumn();
            column.Name = ColumnPropertyName;
            column.DataPropertyName = "name";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            column.DefaultCellStyle = columnPropertyNameStyle;
            dgvCommon.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = ColumnPropertyName;
            column.DataPropertyName = "name";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            column.DefaultCellStyle = columnPropertyNameStyle;
            dgvMonitor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = ColumnPropertyName;
            column.DataPropertyName = "name";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            column.DefaultCellStyle = columnPropertyNameStyle;
            dgvService.Columns.Add(column);
            
            // Column 2 - Property Value
            column = new DataGridViewTextBoxColumn();
            column.Name = ColumnPropertyValue;
            column.DataPropertyName = "property_Text";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCommon.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = ColumnPropertyValue;
            column.DataPropertyName = "property_Text";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMonitor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = ColumnPropertyValue;
            column.DataPropertyName = "property_Text";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvService.Columns.Add(column);
            
            #endregion
        }

        /// <summary>
        /// Loads the settings from the properties file in to the Data grids
        /// </summary>
        private void LoadSettings()
        {
            // Read in the properties file
            string propertyFile = ConfigurationManager.AppSettings[PropertyFile];

            if (File.Exists(propertyFile))
            {
                // Load the global xml doc (used to preserve existing formatting and contents of the xml file)
                if (xmlDoc == null)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(propertyFile);
                }

                // Load the xml
                XmlDataDocument xmlDatadoc = new XmlDataDocument();
                xmlDatadoc.DataSet.ReadXml(propertyFile);

                // Don't load the version number property (this is automatically updated by the save)
                string filter = "AND [name]<>'" + PropertyVersionNumber + "'";

                #region Data Grid Common
                // Filter the data we want to show
                DataView dvCommon = new DataView(xmlDatadoc.DataSet.Tables[XmlPropertyNode]);
                dvCommon.RowFilter = "AID='" + AIDCommon + "'" + filter;

                // Bind to the data grid
                dgvCommon.DataSource = dvCommon;
                
                // Set the first cell in the PropertyValue column to be selected
                if (dgvCommon.Rows.Count > 0)
                {
                    dgvCommon.Rows[0].Cells[1].Selected = true;
                }
                #endregion

                #region Data Grid Monitor
                // Filter the data we want to show
                DataView dvMonitor = new DataView(xmlDatadoc.DataSet.Tables[XmlPropertyNode]);
                dvMonitor.RowFilter = "AID='" + AIDMonitor + "'" + filter;

                // Bind to the data grid
                dgvMonitor.DataSource = dvMonitor;

                // Set the first cell in the PropertyValue column to be selected
                if (dgvMonitor.Rows.Count > 0)
                {
                    dgvMonitor.Rows[0].Cells[1].Selected = true;
                }
                #endregion

                #region Data Grid Service
                // Filter the data we want to show
                DataView dvService = new DataView(xmlDatadoc.DataSet.Tables[XmlPropertyNode]);
                dvService.RowFilter = "AID='" + AIDService + "'" + filter;

                // Bind to the data grid
                dgvService.DataSource = dvService;

                // Set the first cell in the PropertyValue column to be selected
                if (dgvService.Rows.Count > 0)
                {
                    dgvService.Rows[0].Cells[1].Selected = true;
                }
                #endregion
            }
            else
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                        Messages.SSMAPropertiesLoadError));

                // Display error message. This shouldnt happen because the application cannot load without a properties file
                tabControl1.Visible = false;
                
                // Show message to user
                MessageBox.Show("Unable to load the Properties file, please check it exists and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // No point showing this form, so close it
                Close();
            }
        }

        /// <summary>
        /// Saves the displayed settings back to the properties file
        /// </summary>
        private bool SaveSettings()
        {
            try
            {
                // Save the xml doc if the property values have been edited
                if ((xmlDoc != null) && (propertiesEdited))
                {
                    string propertyFile = ConfigurationManager.AppSettings[PropertyFile];

                    // Get and update the Version number of the properties in the xml doc
                    XmlNode node = xmlDoc.SelectSingleNode("//property[@name='"
                        + PropertyVersionNumber + "' and @AID='"
                        + AIDCommon + "' and @GID='"
                        + GID + "']");

                    int versionNumber = Convert.ToInt32(node.InnerText) + 1;

                    node.InnerText = versionNumber.ToString();
                    
                    // Save the xml doc
                    xmlDoc.Save(propertyFile);

                    // Reset flags
                    propertiesEdited = false;
                    
                    saveFileErrorCount = 0;

                    editedCellsCommon.Clear();
                    editedCellsMonitor.Clear();
                    editedCellsService.Clear();

                    // Disable the Apply button
                    EnableApplyButton(propertiesEdited);

                    // To reset the colours of cells in the datagrid
                    Refresh();

                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                        Messages.SSMAPropertiesSaved));
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                        string.Format(Messages.SSMAPropertiesSaveError, ex.Message)));

                // If a save error repeatedly occurs, display a message saying it can't be done. Provides guidance to user to quit!
                if (saveFileErrorCount == 0)
                {
                    saveFileErrorCount++;

                    MessageBox.Show("Error occurred saving to the Properties file, please try again. \r\nError message:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Unable to save to the Properties file at this time, please close the form and try later. \r\nError message:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
        }

        /// <summary>
        /// Method sets the enabled status of the Apply button
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableApplyButton(bool enabled)
        {
            btnApply.Enabled = enabled;
        }

        #region Maintain edited cell value helpers

        /// <summary>
        /// Returns the AID dependent on the tab selected
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        private string GetAid(CurrentTab tab)
        {
            switch (tab)
            {
                case CurrentTab.Common:
                    return AIDCommon;
                case CurrentTab.Monitor:
                    return AIDMonitor;
                case CurrentTab.Service:
                    return AIDService;
            }

            return string.Empty;
        }
        
        /// <summary>
        /// Adds the Edited cell property name to the appropriate "EditedCells" dictionary
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void AddEditedCell(CurrentTab tab, string key, int value)
        {
            switch (tab)
            {
                case CurrentTab.Common:
                    if (editedCellsCommon.ContainsKey(key))
                    {
                        editedCellsCommon.Remove(key);
                    }
                    editedCellsCommon.Add(key, value);
                    break;

                case CurrentTab.Monitor:
                    if (editedCellsMonitor.ContainsKey(key))
                    {
                        editedCellsMonitor.Remove(key);
                    }
                    editedCellsMonitor.Add(key, value);
                    break;

                case CurrentTab.Service:
                    if (editedCellsService.ContainsKey(key))
                    {
                        editedCellsService.Remove(key);
                    }
                    editedCellsService.Add(key, value);
                    break;
            }
        }

        /// <summary>
        /// Checks if the appropriate "EditedCells" dictionary contains a key
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool HasPropertyBeenEdited(CurrentTab tab, string key)
        {
            switch (tab)
            {
                case CurrentTab.Common:
                    if (editedCellsCommon.ContainsKey(key))
                    {
                        return true;
                    }
                    break;

                case CurrentTab.Monitor:
                    if (editedCellsMonitor.ContainsKey(key))
                    {
                        return true;
                    }
                    break;

                case CurrentTab.Service:
                    if (editedCellsService.ContainsKey(key))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Checks if the appropriate "EditedCells" dictionary contains the value
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool DoesEditedCellsContainValue(CurrentTab tab, int value)
        {
            switch (tab)
            {
                case CurrentTab.Common:
                    if (editedCellsCommon.ContainsValue(value))
                    {
                        return true;
                    }
                    break;

                case CurrentTab.Monitor:
                    if (editedCellsMonitor.ContainsValue(value))
                    {
                        return true;
                    }
                    break;

                case CurrentTab.Service:
                    if (editedCellsService.ContainsValue(value))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        #endregion

        #endregion
    }
}