// *********************************************** 
// NAME                 : EBCCalculationDetailsTableControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 21/09/2009
// DESCRIPTION          : Control to display the EBC calculation details in a table
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/EBCCalculationDetailsTableControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Oct 12 2009 09:11:30   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:40:00   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 06 2009 14:17:30   mmodi
//Updated for EBC
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 14:59:06   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class EBCCalculationDetailsTableControl : TDUserControl
    {
        #region Private members
        
        // Environmental benefits to display in details table
        private EB.EnvironmentalBenefits environmentalBenefits;
        private RoadUnitsEnum roadUnits;
        private bool printable;
        
        // Array of column titles for details table
        private IList headerDetail;

        // Array of column titles for details table
        private IList footerDetail;

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(EB.EnvironmentalBenefits environmentalBenefits)
        {
            this.environmentalBenefits = environmentalBenefits;
        }

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            LoadResources();

            // Load the ebc details in to the display table
            DisplayData();

            DisplayErrorMessages();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load the text and image resources for the page
        /// </summary>
        private void LoadResources()
        {
            labelCalculationTitle.Text = GetResource("EBCCalculationDetailsTableControl.labelCalculationTitle");
        }

        /// <summary>
        /// Method which displays the EBC details in the table.
        /// Calls the CycleJourneyDetailFormatter and binds this to the data table
        /// </summary>
        private void DisplayData()
        {
            if ((environmentalBenefits != null) && (environmentalBenefits.IsValid))
            {
                // Use the formatter to give the details to display
                EBCCalculationFormatter detailFormatter = new EBCCalculationFormatter(environmentalBenefits, roadUnits);

                // Get the formatted details
                headerDetail = new ArrayList(detailFormatter.GetHeaders());
                footerDetail = new ArrayList(detailFormatter.GetFooters());

                ebcDetailsRepeater.DataSource = detailFormatter.GetDetails();

                // Ensures controls on ascx are evaluated, i.e. the <# #>,
                // including the repeater
                this.DataBind();
            }
        }

        /// <summary>
        /// Populates the error control with an error message if the EnvironmentalBenefits object
        /// is invalid
        /// </summary>
        private void DisplayErrorMessages()
        {
            errorMessagePanel.Visible = false;
            errorDisplayControl.Visible = false;

            if ((environmentalBenefits != null) && (!environmentalBenefits.IsValid))
            {
                string[] errors = new string[1];

                errors[0] = GetResource("EBCCalculationDetailsTableControl.labelErrorMessage");

                errorDisplayControl.ErrorStrings = errors;

                errorMessagePanel.Visible = true;
                errorDisplayControl.Visible = true;
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Printable mode of control
        /// </summary>
        public bool Printable
        {
            get { return printable; }
            set { printable = value; }
        }

        /// <summary>
        /// Read/write. RoadUnits to be passed to the inner controls
        /// </summary>
        public RoadUnitsEnum RoadUnits
        {
            get { return roadUnits; }
            set { roadUnits = value; }
        }

        /// <summary>
        /// Array of column titles for details table
        /// </summary>
        public IList HeaderDetail
        {
            get
            {
                return headerDetail;
            }
        }

        /// <summary>
        /// Array of column footers for details table
        /// </summary>
        public IList FooterDetail
        {
            get
            {
                return footerDetail;
            }
        }

        #endregion
    }
}