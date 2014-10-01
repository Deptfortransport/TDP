// *********************************************** 
// NAME                 : AboutForm.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: About form
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/AboutForm.cs-arc  $
//
//   Rev 1.3   Aug 24 2011 11:07:52   mmodi
//Added a manual import facility to the SSL UI tool
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Apr 09 2009 10:46:18   mmodi
//Added application name value
//Resolution for 5273: Site Status Loader
//
//   Rev 1.1   Apr 06 2009 16:06:40   mmodi
//Set about text strings using properties
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:34:36   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PropertyService = AO.Properties.Properties;

namespace AO.SiteStatusLoaderMonitorApplication
{
    public partial class AboutForm : Form
    {
        #region Private members

        private string applicationName = string.Empty;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="applicationName"></param>
        public AboutForm(string applicationName)
        {
            this.applicationName = applicationName;

            InitializeComponent();
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Load event of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_Load(object sender, System.EventArgs e)
        {
            this.Text = applicationName + " - About";

            // Set the version label
            lblVersion.Text = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblSiteStatusLoaderApp.Text = PropertyService.Instance["SiteStatusMonitorApplication.About.ApplicationName"];
            lblCompany.Text = PropertyService.Instance["SiteStatusMonitorApplication.About.Owner"];
        }

        /// <summary>
        /// Event handler for OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}