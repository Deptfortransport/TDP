// *************************************************************************** 
// NAME                 : FindViaLocationControl.ascx
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 14/07/2004 
// DESCRIPTION			: Control allows user to specify a via location
// on Find a Input pages
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindViaLocationControl.ascx.cs-arc  $
//
//   Rev 1.5   Aug 28 2012 10:21:12   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.4   Oct 27 2010 15:16:10   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Jun 26 2008 14:04:12   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:20:56   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 30 2008 08:32:00 apatel
//  White Labeling - modified to add PageOptionsControl.
//
//   Rev 1.0   Nov 08 2007 13:14:36   mturner
//Initial revision.
//
//   Rev 1.30   Apr 13 2006 11:41:10   rgreenwood
//IR3743 Added case for ExtendJourneyInput page to set via label
//Resolution for 3743: DN068 Extend: Headers missing from advanced options on Extend input page
//
//   Rev 1.29   Apr 11 2006 11:16:52   esevern
//added check for FindBusInput pageId in SetupResources() so that travel via instructional text is set correctly 
//Resolution for 3867: DN093 Find a Bus:  Advanced options 'via' panel has text missing
//
//   Rev 1.28   Apr 05 2006 15:23:42   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.27   Apr 01 2006 15:13:32   asinclair
//Added case for ExtendJourneyInput page
//Resolution for 3744: DN068 Extend: Problems with 'via' fields on Extend input page
//
//   Rev 1.26   Feb 23 2006 19:16:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.25.1.1   Jan 30 2006 14:41:06   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.25.1.0   Jan 10 2006 15:25:02   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.25   Nov 04 2005 14:18:10   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.24   Nov 02 2005 16:54:36   jgeorge
//Merged 7.2.3.0 fix from branch 1.22.3.0.
//Resolution for 2935: Del 7.2: Expanding Train Preferences prevents user planning journey
//
//   Rev 1.23   Nov 01 2005 15:11:38   build
//Automatically merged from branch for stream2638
//
//   Rev 1.22.2.0   Oct 25 2005 20:05:58   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.22.1.0   Oct 11 2005 09:35:06   asinclair
//Added code to disbale control on VP pages
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.22   May 09 2005 16:45:30   Ralavi
//Removed extra "I want to travel via" from find a train/coach
//
//   Rev 1.21   Apr 15 2005 15:18:30   rscott
//Code added for IR1980
//
//   Rev 1.20   Mar 18 2005 10:34:50   RAlavi
//Added a case for journeyPlannerAmbiguity
//
//   Rev 1.19   Mar 02 2005 15:25:42   RAlavi
//changes for ambiguity 
//
//   Rev 1.18   Feb 23 2005 16:33:42   RAlavi
//Changed for car costing
//
//   Rev 1.17   Jan 28 2005 18:44:26   ralavi
//Updated for car costing
//
//   Rev 1.16   Sep 20 2004 16:56:22   jgeorge
//Modified Page_Load to ensure help label is set every time the page is loaded rather than just the first time.
//Resolution for 1607: Find a coach/Train/car - Selecting 'More'  "travel via" help takes user to home page
//
//   Rev 1.15   Sep 17 2004 12:10:10   rgeraghty
//Correction to help for Confirming car via
//
//   Rev 1.14   Sep 09 2004 14:10:58   jmorrissey
//IR1554 - Updated SetHelpLabel method in FindViaLocationControl to check for location status and set help accordingly. 
//
//Also added call to SetHelpLabel in PagePreRender.
//
//
//   Rev 1.13   Sep 08 2004 15:43:50   jmorrissey
//IR1417 - added amend mode property, used by FindCarInput
//
//   Rev 1.12   Sep 02 2004 10:18:24   jmorrissey
//Added alt tags for Find A
//
//   Rev 1.11   Aug 26 2004 14:33:10   esevern
//added setting of help label text when ambiguous car via - fix for IR1299
//
//   Rev 1.10   Aug 26 2004 10:39:10   passuied
//usage of new DataServiceType FindCarLocationDrop in FindCarInput page
//Resolution for 1441: Find A Car : Add extra station/Airport search type in location selection
//
//   Rev 1.9   Aug 16 2004 12:08:10   jmorrissey
//Added travelViaLabel
//
//   Rev 1.8   Aug 12 2004 12:00:46   jmorrissey
//Updates to help text
//
//   Rev 1.7   Aug 10 2004 13:10:16   jmorrissey
//Renamed tristate control and updated help text
//
//   Rev 1.6   Aug 02 2004 15:37:26   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.5   Jul 26 2004 10:10:58   COwczarek
//Add handling of new location event raised by nested TriStateLocationControl object
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.4   Jul 21 2004 14:22:34   COwczarek
//Add SetLocationUnspecified and Search methods. Remove DisplayMode property
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 19 2004 10:41:32   jmorrissey
//Updated visual layout of control
//
//   Rev 1.2   Jul 15 2004 11:56:54   jmorrissey
//Fixed build error.
//
//   Rev 1.1   Jul 15 2004 11:49:56   jmorrissey
//Updated help label styles
//
//   Rev 1.0   Jul 15 2004 10:56:16   jmorrissey
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region System namespaces

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	#endregion

	#region TransportDirect namespaces

	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.LocationService;	
	using TransportDirect.Common.ServiceDiscovery;

	#endregion
	

	/// <summary>
	///	Summary description for FindViaLocationControl.
	/// </summary>
	public partial  class FindViaLocationControl : TDUserControl
		{

		#region Controls

		protected TriStateLocationControl2 tristateLocationControl;
	
		#endregion

		#region private fields

		//private field for holding location type of this control
		private CurrentLocationType locationType;
		//private field for holding station type of this control
		private StationType stationType;
		//private field for holding LocationSearch session information for this control
		private LocationSearch theSearch;
		//private field for holding TDLocation session information for this control
		private TDLocation theLocation;
		//private field for holding LocationSelectControlType session information for this control
		private TDJourneyParameters.LocationSelectControlType locationControlType;
		protected System.Web.UI.WebControls.Panel stationTypePanel;

        //private field for holding value for PageOptionsControl visiblility.
        private bool pageOptionsVisible = true;
	
        /// <summary>
        /// Event fired to signal new location button has been clicked
        /// </summary>
		public event EventHandler NewLocation;
		public event EventHandler MapClick;

		#endregion
		
		#region Page Load

        /// <summary>
        /// Event handler for page initialisation
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event arguments</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            tristateLocationControl.NewLocation += new EventHandler(OnNewLocation);
            tristateLocationControl.MapClick += new EventHandler(OnMapClick);
        }

		/// <summary>
		/// Page Load method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Only refresh the location controls if this control is being displayed
            if (this.Visible)
            {
                //set up resource strings on initial page load
                SetUpResources();

                // Put user code to initialize the page here
                if (!IsPostBack)
                {
                    //do not refresh the tristate location control
                    RefreshTristateControl(false);
                }
                else
                {
                    //refresh the tristate location control
                    RefreshTristateControl(true);
                }
            }
		}

        /// <summary>
        /// Event handler for page pre-render event
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event arguments</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //ensure resource strings are set up after any click events have been handled
            SetUpResources();

            if (Page.IsPostBack)
            {
                // newLocationClicked indicates if there was a new location event triggered
                // in which case don't check the input
                RefreshTristateControl(theSearch.InputText.Length != 0);
            }

            //Setting PageOptionsControl's visibility
            pageOptionsControl.Visible = IsPageOptionsVisible;
        }

		#endregion

        #region Public Methods

        public void Search()
        {
            tristateLocationControl.Search(true);
        }

        /// <summary>
        /// Sets the control back into the default state that allows a location to be entered by user
        /// </summary>
        public void SetLocationUnspecified() 
        {
            theLocation.Status = TDLocationStatus.Unspecified;
			locationControlType.Type = TDJourneyParameters.ControlType.Default;
            theSearch.ClearSearch();
            tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, true, true, stationType, false);
        }


		#endregion
	
		#region Control Appearance
		
		/// <summary>
		/// set controls visible according to which Find a page this control is on
		/// </summary>
		private void SetUpResources()
		{
			
			//journey options text
			labelJourneyOptions.Text = Global.tdResourceManager.GetString("FindViaLocationControl.labelJourneyOptions",TDCultureInfo.CurrentUICulture);

			//evaluate the containing page
			switch (this.PageId)
			{

					//Find a train input page
				case TransportDirect.Common.PageId.FindTrainInput :					
					instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelTrain",TDCultureInfo.CurrentUICulture);
					break;

					//Find a coach input page
				case TransportDirect.Common.PageId.FindCoachInput :					
					instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCoach",TDCultureInfo.CurrentUICulture);
					break;	

					//Find a car journey input page
				case TransportDirect.Common.PageId.FindCarInput :					
					instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCar",TDCultureInfo.CurrentUICulture);
					break;
					
					//JourneyPlannerInput page
				case TransportDirect.Common.PageId.JourneyPlannerInput :	
				case TransportDirect.Common.PageId.FindBusInput :
					instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCar",TDCultureInfo.CurrentUICulture);
					break;

					//JourneyPlannerAmbiguity page
				case TransportDirect.Common.PageId.JourneyPlannerAmbiguity :					
					instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCar",TDCultureInfo.CurrentUICulture);
					break;

					//ExtendJourneyInput page
				case TransportDirect.Common.PageId.ExtendJourneyInput :
					instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.directionLabelVia",TDCultureInfo.CurrentUICulture);
					break;

					//default, hide the label
				default :
					instructionLabel.Text = String.Empty;
					instructionLabel.Visible = false;

					break;
			}

            // setting the label for the tri state location input box
            tristateLocationControl.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("FindStationInput.labelSRLocation");
		}

					
		
		/// <summary>
		/// Refresh the tristate control
		/// </summary>
		/// <param name="checkInput">indicates if location control checks if input has changed/not</param>
		private void RefreshTristateControl(bool checkInput)
		{

			//evaluate the containing page
			switch (this.PageId)
			{
					//Find a train input page
				case TransportDirect.Common.PageId.FindTrainInput :

					//set station type so that a rail station filter is applied to the gazetteer search
					stationType = StationType.RailNoGroup;
					//also set the search type to MainStationAirport so that a main station gazetteer search is invoked
					theSearch.SearchType = SearchType.MainStationAirport;
					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, true, true, stationType, checkInput);
					break;

					//Find a train input page
				case TransportDirect.Common.PageId.FindCoachInput :

					//set station type so that a coach station filter is applied to the gazetteer search
					stationType = StationType.CoachNoGroup;
					//also set the search type to MainStationAirport so that a main station gazetteer search is invoked
					theSearch.SearchType = SearchType.MainStationAirport;
					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, true, true, stationType, checkInput);
					break;

				case Common.PageId.JourneyPlannerInput :
					//set station type so that gazetteer search is not filtered
					stationType = StationType.UndeterminedNoGroup;
					tristateLocationControl.Populate(DataServiceType.PTViaDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, true, true, stationType, checkInput);
					break;

				case Common.PageId.JourneyPlannerAmbiguity :
					//set station type so that gazetteer search is not filtered
					stationType = StationType.UndeterminedNoGroup;
					break;

				case Common.PageId.FindBusInput :
					stationType = StationType.UndeterminedNoGroup;
					tristateLocationControl.Populate(DataServiceType.PTViaDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, true, true, stationType, checkInput);
					break;

				//We do not display this control on the Visit Planner pages
				case Common.PageId.VisitPlannerInput:
					tristateLocationControl.Visible = false;
					break;

				case Common.PageId.ExtendJourneyInput :
					stationType = StationType.UndeterminedNoGroup;
					tristateLocationControl.Populate(DataServiceType.PTViaDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, true, true, stationType, checkInput);
					break;

					//default
				default:

					//set station type so that gazetteer search is not filtered
					stationType = StationType.UndeterminedNoGroup;
					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, true, true, stationType, checkInput);
					break;
			}			
		}

		#endregion
		
		#region Properties		
		/// <summary>
		/// property holds the location type
		/// this determines the label used for the direction label i.e. Via
		/// </summary>
		public CurrentLocationType LocationType
		{
			get
			{
				return locationType;
			}
			set
			{
				locationType = value;
			}
		}

		/// <summary>
		/// property holds the LocationSearch object used to populate the viaLocationControl		
		/// </summary>
		public LocationSearch TheSearch
		{
			get
			{
				return theSearch;
			}
			set
			{
				theSearch = value;				
			}

		}

		/// <summary>
		/// property holds the TDLocation object used to populate the viaLocationControl	
		/// </summary>
		public TDLocation TheLocation
		{
			get
			{
				return theLocation;
			}
			set
			{
				theLocation = value;
			}

		}

		/// <summary>
		/// property holds the LocationSelectControlType object used to populate the viaLocationControl	
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType LocationControlType			
		{
			get
			{
				return locationControlType;
			}
			set
			{
				locationControlType = value;				
			}

		}

		/// <summary>
		/// IR1417 - Sets the Amend mode in the tristate control. See triStateControl for explanations.
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return tristateLocationControl.AmendMode;
			}
			set
			{
				tristateLocationControl.AmendMode = value;
			}
		}

		/// <summary>
		/// Allows access to the TristateLocationControl contained within this control.
		/// </summary>
		public TriStateLocationControl2 TristateLocationControl
		{
			get { return tristateLocationControl; }
		}

        /// <summary>
        /// Read Only property allows access to PageOptionsControl contained within this control.
        /// </summary>
        public FindPageOptionsControl PageOptionsControl
        {
            get
            {
                return pageOptionsControl;
            }

        }

        /// <summary>
        /// White labeling - Allows to set wether PageOptionsControl should be visible or not.
        /// </summary>
        public bool IsPageOptionsVisible
        {

            get { return pageOptionsVisible; }
            set { pageOptionsVisible = value; }
        }

		#endregion

        #region Event Handlers

        /// <summary>
        /// Event handler to handle the event that signals new location button has been clicked on nested
        /// TriStateLocationControl instance.
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event arguments</param>
        private void OnNewLocation (object sender, EventArgs e)
        {
            theLocation = new TDLocation();
            theSearch = new LocationSearch();
            locationControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);

            if (NewLocation != null)
                NewLocation(sender, e);
        }

		private void OnMapClick(object sender, EventArgs e)
		{
			if (MapClick != null)
				MapClick(sender, e);
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
	}
}
		
