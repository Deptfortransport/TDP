// *********************************************** 
// NAME                 : MapJourneyDisplayDetailsControl.ascx
// AUTHOR               : Atos Origin
// DATE CREATED         : 09/02/2004 
// DESCRIPTION          : Control used alongside a map control showing a drop down list of journey legs or directions.
//                      : The show button should be attached onClick event should be attached to by the user, to perform
//                      : any actions required.
//
//                      : The control has been updated to utilise the new AJAX enabled MapControl2. 
//                      : The show button now fires a javascript event to tell the map to zoom to appropriate
//                      : section of the journey.
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapJourneyDisplayDetailsControl.ascx.cs-arc  $
//
//   Rev 1.5   Nov 29 2009 12:40:02   mmodi
//Moved register javascript to correct show selected leg for VisitPlanner
//
//   Rev 1.4   Nov 15 2009 11:07:26   mmodi
//Updated to display map at selected leg
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 09 2009 15:46:14   mmodi
//Updated with javascript for zooming in the new mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:21:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:22   mturner
//Initial revision.
//
//   Rev 1.15   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.14.1.0   Jan 10 2006 15:26:16   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.14   Nov 04 2005 11:49:06   ralonso
//Manual merger stream2816
//
//   Rev 1.13   Nov 01 2005 15:11:36   build
//Automatically merged from branch for stream2638
//
//   Rev 1.12.2.3   Oct 27 2005 14:34:16   ralonso
//HTML Buttons added
//
//   Rev 1.12.2.2   Oct 25 2005 14:49:22   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.12.2.1   Oct 25 2005 14:40:32   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.12.2.0   Oct 24 2005 14:25:50   ralonso
//TD089 ES020 image button replacement
//
//   Rev 1.12.1.0   Oct 14 2005 09:41:46   jbroome
//Replaces checks for SelectedItinerarySegment with FullItinerarySelected
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.12   Apr 27 2005 16:27:30   pcross
//IR2355. Handle scenario where there is a single leg in a journey and therefore no associated leg breakdown
//Resolution for 2355: View map dropdown for single leg journey
//
//   Rev 1.11   Apr 12 2005 10:57:16   bflenk
//Work in Progress - IR 1986
//
//   Rev 1.10   Mar 18 2005 15:44:26   asinclair
//Added SelectedJourneyValue property
//
//   Rev 1.9   Sep 20 2004 11:55:34   jbroome
//IR 1533 - Added set method to SelectedJourneySegmentIndex property, so value can be retained after returning form More Help.
//
//   Rev 1.8   Sep 17 2004 15:13:58   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.7   Jul 12 2004 15:11:12   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.6   Jun 07 2004 15:14:56   jbroome
//ExtendJourney - added support for TDItineraryManager and multiple journeys.
//
//   Rev 1.5   Mar 31 2004 09:41:02   CHosegood
//removed PopulateJourneySegments( ArrayList list ) method
//
//   Rev 1.4   Mar 18 2004 15:20:16   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.3   Mar 16 2004 16:38:38   CHosegood
//Del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.2   Mar 14 2004 19:12:22   CHosegood
//DEL 5.2 Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.1   Mar 12 2004 16:59:24   CHosegood
//Removed imageButtonShow_Click event handler
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.0   Mar 03 2004 16:53:58   CHosegood
//Initial Revision

#region Using
using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;	
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
#endregion

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for MapJourneyDisplayDetails.
	/// </summary>
	public partial  class MapJourneyDisplayDetailsControl : TDUserControl
	{
		#region Constants
		private const string STOPS = "STOPS";
		private const string ROADS = "ITN";
		private const string POINTX = "POINTX";
		private const string CLICKPOINT = "ClickPoint";
		private const string X = "X";
		private const string Y = "Y";
		private const int DROPD_TYPE = 0;
		private const int DROPD_X = 1;
		private const int DROPD_Y = 2;
		private const int DROPD_NAPTAN = 3;
		private const int DROPD_UNIQUEID = 4;
		private const char SEPARATOR = ',';
		#endregion

        #region Private members
        
        private bool publicJourney;
        private bool usingItinerary;

        // Variables needed to perform javascript actions on the map
        private bool javascriptEnabled = false;
        private string mapId = "map";
        private string sessionId = "session";
        private int journeyRouteNumber = 0;
        private string journeyType = "PT";

        #endregion

        #region Public events

        /// <summary>
		/// Event raised when there are locations available for processing
		/// </summary>
		public event EventHandler LocationsAvailable;
		public event LocationSelectedEventHandler LocationSelectedEvent;
		
        #endregion 
                
		#region Public Properties
		/// <summary>
		/// Get property - returns the item selected in the journey segment drop down.
		/// </summary>
		public ListItem SelectedJourneySegment
		{
			get { return dropDownListJourneySegment.SelectedItem; }
		}

		/// <summary>
		/// Get or set property - the index of the item selected in the journey segment drop down.
		/// </summary>
		public int  SelectedJourneySegmentIndex
		{
			get { return dropDownListJourneySegment.SelectedIndex; }
			set
			{
				// In an error handler designed to catch the scenario where there is only a single leg to a journey
				// and therefore there are no leg entries in the dropdown (only 'entire' map option)
				try
				{
					dropDownListJourneySegment.SelectedIndex = value;
				}
				catch (System.ArgumentOutOfRangeException)
				{
                    if (dropDownListJourneySegment.Items.Count > 0)
                    {
                        dropDownListJourneySegment.SelectedIndex = 0;
                    }
				}
			}
		}

		/// <summary>
		/// Get or set property - the value of the item selected in the journey segment drop down.
		/// </summary>
		public string SelectedJourneyValue
		{
			get { return dropDownListJourneySegment.SelectedValue;}
			set { dropDownListJourneySegment.SelectedValue = value;}
		}

		public DropDownList DropDownListJourneySegment
		{
			get { return dropDownListJourneySegment;}
			set { dropDownListJourneySegment = value ;}
		}

		/// <summary>
		/// Clear the selections in the drop downs
		/// </summary>
		public void ClearSelection() 
		{
			this.dropDownListJourneySegment.ClearSelection();
		}

		/// <summary>
		/// Get/Set property - bool value used to determine whether to display the road
		/// journey drop down.
		/// </summary>
		public bool PublicJourney 
		{
			get { return this.publicJourney; }
			set { this.publicJourney = value; }
		}

		/// <summary>
		/// Get/Set property - bool value used to determine/specify whether to use the ItineraryManager
		/// or the SessionManager.
		/// </summary>
		public bool UsingItinerary 
		{
			get { return this.usingItinerary; }
			set { this.usingItinerary = value; }
		}

		/// <summary>
		/// Get property - returns the "Show Route" image button object.
		/// </summary>
		public TDButton ButtonShow 
		{
			get { return this.buttonShow; }
		}

        /// <summary>
        /// Write only. Sets the add javascript flag to attach javascript to this control, to allow 
        /// the Map to be updated using javascript
        /// </summary>
        public bool EnableJavascript
        {
            set { javascriptEnabled = value; }
        }

        /// <summary>
        /// Write only. Sets the Client ID of the map control to perform the 
        /// map journey zoom on
        /// </summary>
        public string MapId
        {
            set { mapId = value; }
        }

        /// <summary>
        /// Write only. Sets the Session ID needed by the map control
        /// </summary>
        public string SessionId
        {
            set { sessionId = value; }
        }

        /// <summary>
        /// Write only. Sets the Journey Route Number to be shown on the map control
        /// </summary>
        public int JourneyRouteNumber
        {
            set { journeyRouteNumber = value; }
        }

        /// <summary>
        /// Write only. Sets the Journey type to be shown on the map control
        /// </summary>
        public string JourneyType
        {
            set { journeyType = value; }
        }

		#endregion

        #region Public methods

        /// <summary>
		/// Calls the relevant method in the mapHelper to populate the journey segments drop down.
		/// </summary>
		/// <param name="outward"></param>
		public void PopulateJourneySegments( bool outward, bool usingItinerary , int journeys) 
		{
			MapHelper helper = new MapHelper();
			SessionManager.ITDSessionManager current = SessionManager.TDSessionManager.Current;

			if ( journeys == 1 && (!usingItinerary || (usingItinerary && current.ItineraryManager.SelectedItinerarySegment != -1)) )
			{
                helper.PopulateSingleJourneySegment(dropDownListJourneySegment, outward, usingItinerary, javascriptEnabled);
			}
			else 
			{
                helper.PopulateMultiJourneySegments(dropDownListJourneySegment, outward, journeys, javascriptEnabled);
			}
        }

        #endregion

        #region Page Load, Page PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			buttonShow.Text = Global.tdResourceManager.GetString(
				"MapJourneyDisplayDetailsControl.buttonShow.Text", TDCultureInfo.CurrentUICulture);
			buttonShow.ToolTip = Global.tdResourceManager.GetString(
				"MapJourneyDisplayDetailsControl.buttonShow.AlternateText", TDCultureInfo.CurrentUICulture);

            RegisterJavaScripts();
		}

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegisterButtonJavaScript();
        }

		#endregion

        #region Private methods

        /// <summary>
        /// Method which adds the javascript files needed by the control
        /// </summary>
        private void RegisterJavaScripts()
        {
            // Determine if javascript is support and determine the JavascriptDom value
            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            string scriptName = "MapJourneyDisplayDetailsControl";

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            // Register the javascript file to zoom map to selected journey leg
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));

        }

        /// <summary>
        /// Method which adds the javascript to the controls
        /// </summary>
        private void RegisterButtonJavaScript()
        {
            if (javascriptEnabled)
            {
                // Add the button click javascript to fire
                buttonShow.OnClientClick = "return zoomJourneySegmentOnMap('" + mapId + "', '" + sessionId + "', '"
                    + journeyRouteNumber + "', '" + journeyType + "', '" + dropDownListJourneySegment.ClientID + "');";

            }
        }

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Fires the locations available event
		/// </summary>
		private void OnLocationsAvailable()
		{
			if( LocationsAvailable != null )
			{
				LocationsAvailable( this, EventArgs.Empty );
			}
		}

		/// <summary>
		/// Fires the Location Selected event
		/// </summary>
		private void OnLocationSelected(int x, int y, string name)
		{
			if( LocationsAvailable != null )
			{
				LocationSelectedEventArgs eventArgs = new LocationSelectedEventArgs(x, y, name);

				// Fire the appropriate event defined in the event declaration
				// Add new MapMode to the event args
				LocationSelectedEvent( this,  eventArgs );
			}
		}

		#endregion
	}
}
