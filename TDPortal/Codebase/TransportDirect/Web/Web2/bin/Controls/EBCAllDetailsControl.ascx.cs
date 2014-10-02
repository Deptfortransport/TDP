// *********************************************** 
// NAME                 : EBCAllDetailsControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/09/2009
// DESCRIPTION          : Wrapper control for the Car details, and EBC Benefits items
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/EBCAllDetailsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Oct 26 2009 10:08:38   mmodi
//Show EBC distance to 2dp
//
//   Rev 1.2   Oct 16 2009 14:06:12   mmodi
//Moved roadunits property into page load to ensure selected units are correctly displayed
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 06 2009 14:17:26   mmodi
//Updated for EBC
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 14:59:04   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class EBCAllDetailsControl : TDUserControl
    {
        #region Private members

        private RoadUnitsEnum roadUnits;
        private bool printable;
        private bool outward;

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(RoadJourney roadJourney, bool outward, TDJourneyParametersMulti journeyParameters, 
            TDJourneyViewState viewState, EB.EnvironmentalBenefits environmentalBenefits)
        {
            this.outward = outward;

            // Initialise the controls
            carSummaryControl.Initialise(roadJourney, outward, 2);
            carJourneyDetailsTableControl.Initialise(roadJourney, viewState, outward);
            ebcCalculationDetailsTableControl.Initialise(environmentalBenefits);
        }

        #endregion

        #region Page_Load, Page_PreRender
        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set other properties
            carSummaryControl.RoadUnits = roadUnits;
            carJourneyDetailsTableControl.RoadUnits = roadUnits;
            carJourneyDetailsTableControl.DirectionsLabelVisible = true;
            carJourneyDetailsTableControl.ShowDirectionDistance = true;
            carJourneyDetailsTableControl.Formatter = CarJourneyDetailsTableControl.CarJourneyDetailsTableFormatter.EnvironmentalBenefits;
            ebcCalculationDetailsTableControl.RoadUnits = roadUnits;
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            // Set controls printable values
            carSummaryControl.NonPrintable = !printable;
            carJourneyDetailsTableControl.NonPrintable = !printable;
            ebcCalculationDetailsTableControl.Printable = printable;

            LoadResources();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load the text and image resources for the page
        /// </summary>
        private void LoadResources()
        {
            labelEBCAllDetailsControlTitle.Text = (outward ?
                GetResource("EBCPlanner.labelEBCAllDetailsControlTitle.Text.OutwardJourney") :
                GetResource("EBCPlanner.labelEBCAllDetailsControlTitle.Text.ReturnJourney"));
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
        /// Read only. Expose Show map button to allow users to attach to event
        /// </summary>
        public TDButton ButtonShowMap
        {
            get { return buttonShowMap; }
        }

        #endregion
    }
}