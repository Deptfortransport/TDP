// *********************************************** 
// NAME                 : EBCAllDetailsControl.ascx
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/10/2009
// DESCRIPTION          : Wrapper control for the Car summary, and Map items
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/EBCMapDetailsControl.ascx.cs-arc  $
//
//   Rev 1.6   Nov 29 2009 12:37:14   mmodi
//Updated map initialise to hide the show journey buttons
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 27 2009 13:14:50   mmodi
//Increased map width
//
//   Rev 1.4   Nov 23 2009 13:03:06   apatel
//Updated for map when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 16 2009 17:07:00   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 11 2009 18:30:10   mmodi
//Updated to use new MapJourneyControl
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Oct 16 2009 14:06:12   mmodi
//Moved roadunits property into page load to ensure selected units are correctly displayed
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
// Added header info (Amit forgot to add)

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// EBCMapDetailsControl
    /// </summary>
    public partial class EBCMapDetailsControl : TDUserControl
    {
        #region Private members

        private RoadUnitsEnum roadUnits;
        private bool outward;
        private bool printable;

        // Override the default journey map height/width so it fits in to this control
        private int mapHeight = 554;
        private int mapWidth = 792;
        
        #endregion

        #region Public Properties
        public MapJourneyControl MapControl
        {
            get
            {
                return this.mapJourneyControl;
            }
        }
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
            carSummaryControl.Initialise(roadJourney, outward);
            mapJourneyControl.Initialise(outward, false, false, mapHeight, mapWidth);
        }

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            TDPage page = (TDPage)this.Page;

            // Set other properties
            carSummaryControl.RoadUnits = roadUnits;
            mapJourneyControl.Visible = page.IsJavascriptEnabled;
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            // Set controls printable values
            carSummaryControl.NonPrintable = !printable;
            
            LoadResources();
        }

        #endregion
                

        #region Private Methods

        /// <summary>
        /// Method to load the text and image resources for the page
        /// </summary>
        private void LoadResources()
        {
            labelEBCMapControlTitle.Text = (outward ?
                GetResource("EBCPlanner.labelEBCMapControlTitle.Text.OutwardJourney") :
                GetResource("EBCPlanner.labelEBCMapControlTitle.Text.ReturnJourney"));
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
        public TDButton ButtonShowDetails
        {
            get { return buttonShowDetails; }
        }

        #endregion
    }
}