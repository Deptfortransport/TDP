// *********************************************** 
// NAME                 : TrafficMap.aspx.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 08/10/2003 
// DESCRIPTION          : Traffic Map page  
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/TrafficMaps.aspx.cs-arc  $ 
//
//   Rev 1.13   Oct 12 2011 12:02:20   mmodi
//Updated logic for Traffic Maps page to set and persist the selected date and times
//Resolution for 5753: Traffic Levels map does not use selected date
//
//   Rev 1.12   Sep 16 2011 09:44:18   DLane
//WAI additional changes
//Resolution for 5738: WAI additional changes
//
//   Rev 1.11   Jul 28 2011 16:21:14   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.10   Jan 30 2009 10:44:28   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.9   Jan 07 2009 11:29:00   devfactory
//XHTML Compliance Changes
//
//   Rev 1.8   Jul 01 2008 15:41:02   pscott
//SCR4996 - post code review changes
//
//   Rev 1.7   Jun 26 2008 14:04:48   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.6   May 28 2008 11:45:34   pscott
//SCR4996 - fix loss of location description
//
//   Rev 1.5   May 28 2008 09:12:10   pscott
//SCR 4996    UK:2634536
//Ensure traffic level map is preserved when changing language
//
//   Rev 1.4   May 01 2008 17:18:24   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 02 2008 13:30:46   apatel
//moved new location button to top as back button
//
//   Rev 1.2   Mar 31 2008 13:26:06   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 28 2008 19:40:00 apatel
//  added select new location button.
//
//  Rev Devfactory Feb 04 2008 15:00:00 apatel
//  CCN 0427 - Removed overview map
//
//   Rev 1.0   Nov 08 2007 13:31:56   mturner
//Initial revision.
//
//   Rev 1.1   Sep 03 2007 15:25:44   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.0   Nov 08 2006 10:20:18   dsawe
//Initial revision.
//
//   Rev 1.57   Feb 24 2006 10:17:24   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.56   Feb 10 2006 11:15:38   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.55   Dec 05 2005 16:16:54   ralonso
//Fixed problem with PrinterFriendly button
//Resolution for 3307: UEE HTML Button:  Printer Friendly button appearing on the Traffic Maps Input page
//
//   Rev 1.54   Nov 23 2005 19:14:16   RGriffith
//Code review suggestions for stream2880
//
//   Rev 1.53   Nov 17 2005 18:27:36   pcross
//Merge fix
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.50.2.0   Nov 03 2005 15:23:16   schand
//Added changes for stream2880 FindAPlaceControl
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.51   Nov 03 2005 16:55:34   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.50.1.1   Oct 24 2005 18:51:02   RGriffith
//Changes to account for changes to TrafficDateTimeDropDownControl.aspx
//
//   Rev 1.50.1.0   Oct 14 2005 14:51:10   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.50   Sep 29 2005 12:54:08   build
//Automatically merged from branch for stream2673
//
//   Rev 1.49.1.4   Sep 16 2005 17:30:02   Schand
//DN079 UEE
//Updates for Code Review
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.49.1.3   Sep 14 2005 13:11:08   rgreenwood
//DN079 ES015 Code Review
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.49.1.2   Sep 09 2005 14:21:50   Schand
//DN079 UEE Enter Key.
//Updates for UEE.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.49   Aug 23 2004 15:26:48   RHopkins
//IR1363  Flag to determine whether location is ambiguous/valid is stored in the page's viewstate so that it can be restored to the Session property if the page is submitted after using the browser's Back button.
//
//   Rev 1.48   Aug 16 2004 10:04:20   jbroome
//IR 1358 - Correct handling of invalid date values.
//
//   Rev 1.47   Aug 10 2004 17:14:38   asinclair
//Fix for IR 1273
//
//   Rev 1.46   Aug 10 2004 11:30:06   jgeorge
//IR 1289
//
//   Rev 1.45   Aug 05 2004 11:40:30   asinclair
//Fix for IR 1280
//
//   Rev 1.44   Jul 23 2004 11:48:36   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.43   Jul 19 2004 09:43:30   asinclair
//Fix for IR 1227 and 977
//
//   Rev 1.42   Jul 13 2004 12:07:36   JHaydock
//DEL 5.4.7 Merge: IR 1062 Correction
//
//   Rev 1.41   Jul 12 2004 19:53:48   JHaydock
//DEL 5.4.7 Merge: IR 1132
//
//   Rev 1.40   Jul 12 2004 18:44:18   JHaydock
//DEL 5.4.7 Merge: IR 1088
//
//   Rev 1.39   Jul 12 2004 18:26:52   JHaydock
//DEL 5.4.7 Merge: IR 1074
//
//   Rev 1.38   Jul 12 2004 17:55:20   JHaydock
//DEL 5.4.7 Merge: IR 1062
//
//   Rev 1.37   Jul 12 2004 14:02:32   jbroome
//InjectViewState no longer causes an error - removed comment.
//(Extend Journey code review)
//
//   Rev 1.36   Jun 18 2004 13:02:08   asinclair
//Fix for IR1034
//
//   Rev 1.35   Jun 17 2004 12:39:04   jgeorge
//IR1027
//
//   Rev 1.34   Jun 10 2004 10:17:00   RPhilpott
//Get default location type from DataServices, not hard coding.
//Resolution for 993: Traffic maps default set to Address/postcode
//
//   Rev 1.33   May 18 2004 13:27:56   jbroome
//IR861 Resolving issue of Zoom Control levels and Map Symbols.
//
//   Rev 1.32   May 11 2004 10:44:38   asinclair
//Fix for IR 851
//
//   Rev 1.31   Apr 27 2004 14:09:38   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.30   Apr 20 2004 14:53:18   COwczarek
//Fixes to ensure date/time control correctly refreshes with selected date
//Resolution for 789: Using calender to change date on traffic maps does not work
//
//   Rev 1.29   Apr 19 2004 17:45:04   COwczarek
//When selecting a new date do not reset currently selected time to 12:00
//Resolution for 789: Using calender to change date on traffic maps does not work
//
//   Rev 1.28   Apr 15 2004 17:07:52   ESevern
//added header text

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.Common.DatabaseInfrastructure.Content;



namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for TrafficMap.
	/// </summary>
	public partial class TrafficMap : TDPage
	{
		#region Images, labels, panels, hyperlinks
		// Web-User Controls
		protected System.Web.UI.WebControls.Label labelMapTools;
		protected System.Web.UI.WebControls.Label LabelSelected;
		protected System.Web.UI.WebControls.Label labelLocationTitle;
        protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;

		#endregion

        #region Constant declarations

        private const string RES_URL_PRINTER = "JourneyPlanner.imageButtonPrinterFormat.ImageUrl";
        private const string RES_ALT_PRINTER = "JourneyPlanner.imagePrinter.AlternateText";
        private const string RES_ANCHOR_PRINTER = "TrafficMap.UrlPrintableTrafficMap";
        private const string RES_ALT_SUMMAP = "JourneyMapControl.imageSummaryMap.AlternateText";
        private const string RES_URL_BACK = "JourneyPlanner.imageButtonBack.ImageUrl";
        private const string RES_ALT_BACK = "JourneyPlannerLocationMap.imageButtonBack.AlternateText";

        private const string RES_TEXT_INSTRUCTIONS_UNSPEC_EMPTY = "JourneyPlannerLocationMap.ErrorMessage.Prompt";
        private const string RES_TEXT_INSTRUCTIONS_UNSPEC = "JourneyPlannerLocationMap.ErrorMessage.Unspecified";
        private const string RES_TEXT_INSTRUCTIONS_DATE = "TrafficMap.labelInstructions.Text";
		private const string RES_TEXT_INSTRUCTIONS_UNSPEC_AMBIG = "JourneyPlannerLocationMap.ErrorMessage.Ambiguous";
        private const string RES_TEXT_INSTRUCTIONS_INVALID_DATE = "TrafficMap.ErrorMessage.InvalidDateTime";
		private const string RES_TEXT_SHOWMAP = "TrafficMap.labelTrafficDateTimeDropDown.Text";
		private const string RES_ALT_MAPCONTROL = "SimpleMapControl.Map.AlternateText";

        private const string ZOOM_SETTING = "SimpleMappingComponent";

		private bool mapToRefresh = false;

		#endregion

        #region Constructor

		/// <summary>
		/// Constructor, sets the PageId and calls base.
		/// </summary>
		public TrafficMap() : base()
		{
			pageId = PageId.TrafficMap;
		}

		#endregion
		
		#region Custom web user controls
		protected CalendarControl CalendarControl1;
		protected TriStateLocationControl2 TriStateLocationControl1;
		protected MapKeyControl MapKeyControl1;
        protected MapDisabledControl MapDisabledControl1;
		protected SimpleMapControl SimpleMapControl1;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
        protected TransportDirect.UserPortal.Web.Controls.MapZoomControl mapZoomControl;
        protected TransportDirect.UserPortal.Web.Controls.TrafficDateTimeDropDownControl trafficDateTimeDropDownControl;

        #endregion
		
		#region Private variables

		private LocationSearch mapSearch;
		private TDLocation mapLocation;
		private LocationSelectControlType locationControlType;

		// Indicates if the location was initially valid.
		// (As the map will have to be refreshed if Map Location was
		// initially invalid and consequently becomes valid).
		private bool locationNotValid = false;

		#endregion


		#region ViewState Code

		/// <summary>
		/// Loads the internal viewstate for this page.
		/// </summary>
		/// <param name="savedState">Object containing the saved state.</param>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					TDSessionManager.Current.InputPageState.MapLocation.Status = (TDLocationStatus)myState[1];
			}
		}

		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[2];
			allStates[0] = baseState;
			allStates[1] = TDSessionManager.Current.InputPageState.MapLocation.Status;

			return allStates;
		}

		#endregion


		#region Page Load

		/// <summary>
		/// Page Load.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            SimpleMapControl1.DisplayLowZoomLevelBox = true;
            trafficDateTimeDropDownControl.ControlShowOnMap.Visible = false;
			//mapZoomControl.DisplayLowZoomLevelBox = false;
			MapKeyControl1.InitialisePrivate( true, false );

            TriStateLocationControl1.LocationAmbiguousControl.NewLocationVisible = false;

            #region Populate the selected traffic date time 

            // Set the initial traffic date time
            if (!Page.IsPostBack)
            {
                trafficDateTimeDropDownControl.DateTimeValue = TDDateTime.Now;

                // Retrieve and set the last selected traffic date time, only on first page load
                if (pageState.CalendarDateTime != null)
                {
                    trafficDateTimeDropDownControl.DateTimeValue = pageState.CalendarDateTime;
                }

                // Clear to ensure if user comes back to page in future the datetime defaults to now
                pageState.CalendarDateTime = null;
            }

            #endregion

            if (!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) == null)
			{
				// Checking if it hasn't came from FindAPlaceControl on the HomePage 
                if (TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) == null)
                {
                   if (!HasLanguageChanged())
                   {
                     // Initialisation required for the TriState control
                        TDSessionManager.Current.InputPageState.MapLocationSearch.ClearAll();
                        TDSessionManager.Current.InputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.TrafficMapDrop);
                        TDSessionManager.Current.InputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
                        TDSessionManager.Current.InputPageState.MapLocationControlType.Type = ControlType.Default;
                    }
                    else
                    { 
                            // Only language has changed so restore last page state and map settings
                        pageState = TDSessionManager.Current.InputPageState;
                        pageState.PageLanguage = GetPageLanguage();
                        labelSelectedLocation.Text = TDSessionManager.Current.InputPageState.MapLocation.Description;
                        // Retrieve original point user clicked on the map
                        int x = pageState.MapLocation.GridReference.Easting;
                        int y = pageState.MapLocation.GridReference.Northing;
                        int z = pageState.MapScaleOutward;
                        SimpleMapControl1.Map.TravelDate = DateTime.Now;
                        SimpleMapControl1.Map.TimePeriod = GetTimePeriod(DateTime.Now);
                        SimpleMapControl1.Map.ZoomToPoint(x,y,z);
                        SimpleMapControl1.Map.SetScale(z);
                    }
                }
				
				// Zoom the map initially.
				ZoomMapInitial(); 

			}
			else if (!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) != null)
			{
				this.mapToRefresh = true;
			}

			// Loading Session variables
			mapSearch = pageState.MapLocationSearch;
			mapLocation = pageState.MapLocation;
			locationControlType = pageState.MapLocationControlType;

			if( TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack ) != null && Visible )
			{
				
				// Get map data
				// Inject it
				try
				{	
					// Need to select the right data
					SimpleMapControl1.Map.InjectViewState(TDSessionManager.Current.StoredMapViewState [ TDSessionManager.OUTWARDMAP] );
	
				}
				catch( MapExceptionGeneral exc)
				{
					Logger.Write( new OperationalEvent( TDEventCategory.ThirdParty, TDTraceLevel.Warning,exc.Message +"\nStacktrace:\n"+exc.StackTrace));
				}
                
				labelSelectedLocation.Text = TDSessionManager.Current.InputPageState.MapLocation.Description;
				locationNotValid = pageState.MapLocation.Status != TDLocationStatus.Valid;
			}

			// Checking if it has came from FindAPlaceControl on the HomePage 
			if (TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
			{
				pageState.MapMode = CurrentMapMode.FindJourneys;
								
				TriStateLocationControl1.Populate(
					DataServices.DataServiceType.FromToDrop,
					pageState.MapType,
					ref mapSearch,
					ref mapLocation,
					ref locationControlType,
					true,
					true,
					true, // Same rules apply for Partial Postcode in this case	
					false);
				downAmbiguityLevel();	

			}
			//-----------------------------------------


			// initialise the tristatecontrol
			RefreshLocationControl(Page.IsPostBack);
			LoadResources();

			// DN079 UEE
			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);

            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextTrafficMaps);
            expandableMenuControl.AddExpandedCategory("Related links");


            // Saves the page language to session. Used when checking if the page
            // language has changed, to prevent map from being reset
            SavePageLanguageToSession();

        }

		private void LoadResources()
		{
			SimpleMapControl1.Map.AlternateText = GetResource( RES_ALT_MAPCONTROL);
			labelTrafficDateTimeDropDown.Text = GetResource(RES_TEXT_SHOWMAP);

            imageTrafficMaps.ImageUrl = GetResource("HomeFindAPlace.imageTrafficMaps.ImageUrl");
            imageTrafficMaps.AlternateText = " ";
            labelTrafficMapTitle.Text = GetResource("panelLocation.labelJourneys");

            
			previousLocationButton.Text = GetResource("AmbiguousLocationSelectControl2.backButton.Text");
			resolveLocationButton.Text = GetResource("AmbiguousLocationSelectControl2.nextButton.Text");
            ShowOnMapButton.Text = GetResource("TrafficDateTimeDropDownControl.commandShowOnMap.Text");

            // CCN 0427 new Select New Location button text
            SelectNewLocationButton.Text = GetResource("TrafficMap.SelectNewLocationButton.Text");

            // Set the browser title
            PageTitle = GetResource("TrafficMaps.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            
            if (!IsPostBack)
			{
				labelKey.Text = Global.tdResourceManager.GetString(
					"MapKeyControl.labelKey", TDCultureInfo.CurrentUICulture);
			}
		}


		#endregion

		#region Method to zoom the map on intial page load.

		/// <summary>
		/// Sets the initial zoom level of the map.
		/// </summary>
		private void ZoomMapInitial()
		{
			if ( !SimpleMapControl1.Map.IsStarted() ) 
			{
				try
				{
					SimpleMapControl1.Map.TravelDate = DateTime.Now;
					SimpleMapControl1.Map.TimePeriod = GetTimePeriod( DateTime.Now );
					SimpleMapControl1.Map.ZoomFull();
					
				}
				catch( PropertiesNotSetException pnse )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap PropertiesNotSetException." + pnse.Message );

					Logger.Write( operationalEvent );
				}
				catch( MapNotStartedException mnse )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap MapNotStartedException." + mnse.Message );

					Logger.Write( operationalEvent );
				}
				catch( ScaleOutOfRangeException soore )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap ScaleOutOfRangeException." + soore.Message );

					Logger.Write( operationalEvent );
				}
				catch( ScaleZeroOrNegativeException szone )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap ScaleZeroOrNegativeException." + szone.Message );

					Logger.Write( operationalEvent );
				}
				catch( NoPreviousExtentException npee )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap NoPreviousExtentException." + npee.Message );
			
					Logger.Write( operationalEvent );
				}
				catch( NoDayTypeForDateException ndte )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap NoDayTypeForMapException." + ndte.Message );

					Logger.Write( operationalEvent );
				}
				catch( MapExceptionGeneral mge )
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap MapExceptionGenral." + mge.Message );

					Logger.Write( operationalEvent );
				}
			}
		}
		#endregion 

		#region OnPreRender method
		/// <summary>
		/// Overrides base OnPreRender. Updates the Map Tools Control
		/// and calls base OnPreRender.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
		
			// Enable/Disable the Find New Map button depending on the current location status
			
			if ( CalendarControl1.TravelDate != null )
			{
				// A date was specified using the the calendar control.
				// Set date/time control to reflect date specified using calendar control.
				// Only update date/time control with day/month ensuring time is not effected
				TDDateTime newSelectedDate = CalendarControl1.TravelDate;
				TDDateTime currentSelectedDate = trafficDateTimeDropDownControl.DateTimeValue;
                trafficDateTimeDropDownControl.DateTimeValue =
                    new TDDateTime(newSelectedDate.Year, newSelectedDate.Month, newSelectedDate.Day, 
                    currentSelectedDate.Hour, currentSelectedDate.Minute, currentSelectedDate.Second);
                // refresh drop downs
				trafficDateTimeDropDownControl.Populate();
			}
            
			ZoomMapInitial();

			if (mapToRefresh)
			{
				this.SimpleMapControl1.Map.Refresh();
			}


            // setting the label for the tri state location input box
            TriStateLocationControl1.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("FindStationInput.labelSRLocation");
            

			base.OnPreRender(e);
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraWiringEvents();

			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Code to wire up events
		private void ExtraWiringEvents()
		{
           
			// Add a handler for the OnMapChangedEvent.
			SimpleMapControl1.Map.OnMapChangedEvent += new MapChangedEventHandler(this.Map_Changed);

			TriStateLocationControl1.ValidLocation += new EventHandler(OnValidLocation);
            commandBack.Click  += new EventHandler(OnNewLocation);

			// Wiring for the DateTimeDropDownControl
			trafficDateTimeDropDownControl.ControlCalendar.Click += new System.Web.UI.ImageClickEventHandler(this.calendarButton_Click);
			trafficDateTimeDropDownControl.ControlShowOnMap.Click += new EventHandler(this.showOnMapButton_Click);
            ShowOnMapButton.Click +=new EventHandler(this.showOnMapButton_Click);

			//This ensures that the Map viewstate is saved for the 'More' help click
			this.helpLabelLocationSelect.MoreHelpEvent += new EventHandler (this.OnMapStore);
			this.helpLabelMapZoom.MoreHelpEvent += new EventHandler (this.OnMapStore);
			this.helpLabelToolsSecond.MoreHelpEvent += new EventHandler (this.OnMapStore);
			this.helpLabelLocationSelectAmbig.MoreHelpEvent += new EventHandler (this.OnMapStore);
			this.trafficDateTimeDropDownControl.DateSelectionEvent += new EventHandler (this.OnMapStore);
			// DN079 UEE
			// Event Handler for default action button			
			headerControl.DefaultActionEvent +=  new EventHandler(this.DefaultActionClick); 

			// Wire up TDButtons
			previousLocationButton.Click += new EventHandler(this.previousLocationButton_Click);
			resolveLocationButton.Click += new EventHandler(this.resolveLocationButton_Click);

            //CCN 0427 Select New Location button's event
            SelectNewLocationButton.Click += new EventHandler(OnNewLocation);
		}

        
		#endregion

		#region Handler for Map Changed event
		private void Map_Changed(object sender, MapChangedEventArgs e)
		{
			// Update the image url of the summary map
			//imageSummaryMap.ImageUrl = e.OvURL;

            //labelZoomLevel.Text = "1:"+e.MapScale;

			SimpleMapControl1.ScaleChange( e.MapScale, e.NaptanInRange );
			// -----------------------------------

			// Update the map URLS and scales in InputPageState for printer friendly maps
			TDSessionManager.Current.InputPageState.MapUrlOutward =
				SimpleMapControl1.Map.ImageUrl;

			TDSessionManager.Current.InputPageState.OverviewMapUrlOutward =
				e.OvURL;

			TDSessionManager.Current.InputPageState.MapScaleOutward =
				e.MapScale;

            TDSessionManager.Current.InputPageState.CalendarDateTimePrintable =
                trafficDateTimeDropDownControl.DateTimeValue;
		}

		#endregion

        #region Button handlers
		/// <summary>
		/// Handler for the Zoom Event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMap(object sender, ZoomLevelEventArgs e)
		{
			if(e.ZoomLevel > 0)
				this.SimpleMapControl1.Map.SetScale(e.ZoomLevel);
		}

		/// <summary>
		/// Handler for the zoom in event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMapIn(object sender, EventArgs e)
		{
			// Get the current map click mode
			SimpleMap.ClickModeType currentClickMode = SimpleMapControl1.Map.ClickMode;

			// Set the click mode to zoom in
			SimpleMapControl1.Map.ClickMode = SimpleMap.ClickModeType.ZoomIn;
			
			// Raise the click event
			SimpleMapControl1.Map.FireClickEvent();

			// Restore the click mode
			SimpleMapControl1.Map.ClickMode = currentClickMode;
		}

		/// <summary>
		/// Handler for the Zoom out event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMapOut(object sender, EventArgs e)
		{
			// Get the current map click mode
			SimpleMap.ClickModeType currentClickMode = SimpleMapControl1.Map.ClickMode;

			// Set the click mode to zoom in
			SimpleMapControl1.Map.ClickMode = SimpleMap.ClickModeType.ZoomOut;
			
			// Raise the click event
			SimpleMapControl1.Map.FireClickEvent();

			// Restore the click mode
			SimpleMapControl1.Map.ClickMode = currentClickMode;
		}

		/// <summary>
		/// Handler for the Find New Map event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void MapFindNew(object sender, EventArgs e)
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			// Set status of the map location to unspecified.
			pageState.MapLocation.Status = TDLocationStatus.Unspecified;

			pageState.MapLocationSearch.ClearAll();
			pageState.MapLocationControlType.Type = ControlType.Default;

			// Set the transition event to load this page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			// Set flag in session to force load to True.
			pageState.JourneyPlannerLocationMapForceLoad = true;

			// Set ForceRedirect to true.
			sessionManager.FormShift[SessionKey.ForceRedirect]= true;

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.GoTrafficMap;
		}

        /// <summary>
        /// Handler for the Previous View event.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
		private void MapPreviousView(object sender, EventArgs e)
		{
			try
			{
				SimpleMapControl1.Map.ZoomPrevious();
			}
			catch( PropertiesNotSetException pnse )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap PropertiesNotSetException." + pnse.Message );

				Logger.Write( operationalEvent );
			}
			catch( MapNotStartedException mnse )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap MapNotStartedException." + mnse.Message );

				Logger.Write( operationalEvent );
			}
			catch( ScaleOutOfRangeException soore )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap ScaleOutOfRangeException." + soore.Message );

				Logger.Write( operationalEvent );
			}
			catch( ScaleZeroOrNegativeException szone )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap ScaleZeroOrNegativeException." + szone.Message );

				Logger.Write( operationalEvent );
			}
			catch( NoPreviousExtentException npee )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap NoPreviousExtentException." + npee.Message );
			
				Logger.Write( operationalEvent );
			}
			catch( NoDayTypeForDateException ndte )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap NoDayTypeForMapException." + ndte.Message );

				Logger.Write( operationalEvent );
			}
			catch( MapExceptionGeneral mge )
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap MapExceptionGenral." + mge.Message );

				Logger.Write( operationalEvent );
			}
		}

		public void calendarButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            TDDateTime selectedDateTime = trafficDateTimeDropDownControl.DateTimeValue;
            if (selectedDateTime == TrafficDateTimeDropDownControl.NullDate )
            {
                selectedDateTime = TDDateTime.Now;
            } 
            CalendarControl1.SetCalendar(
                selectedDateTime, Global.tdResourceManager.GetString( "OutboundCalendarTitle" ) );
			CalendarControl1.Open();
		}

		public void showOnMapButton_Click(object sender, EventArgs e)
		{
			if ( SimpleMapControl1.Map.IsStarted() ) 
			{
				if (trafficDateTimeDropDownControl.IsValidDateTime)
				{
					DateTime selectedDate = trafficDateTimeDropDownControl.DateTimeValue.GetDateTime();

					if(TDSessionManager.Current.InputPageState.MapLocation.Status == TDLocationStatus.Valid)
					{
						labelTime.Visible = true;
						labelTime.CssClass = "txttenb";
						labelTime.Text = selectedDate.ToString("dd MMMM yyyy, HH:mm");
					}

					labelInstructions.Visible = false;

					try
					{		
						SimpleMapControl1.Map.TravelDate = selectedDate;
						SimpleMapControl1.Map.TimePeriod = GetTimePeriod( selectedDate );
						SimpleMapControl1.Map.Refresh();
					}
					catch( PropertiesNotSetException pnse )
					{
						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap PropertiesNotSetException." + pnse.Message );

						Logger.Write( operationalEvent );
					}
					catch( MapNotStartedException mnse )
					{
						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap MapNotStartedException." + mnse.Message );

						Logger.Write( operationalEvent );
					}
					catch( ScaleOutOfRangeException soore )
					{
						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap ScaleOutOfRangeException." + soore.Message );

						Logger.Write( operationalEvent );
					}
					catch( ScaleZeroOrNegativeException szone )
					{
						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap ScaleZeroOrNegativeException." + szone.Message );

						Logger.Write( operationalEvent );
					}
					catch( NoPreviousExtentException npee )
					{
						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap NoPreviousExtentException." + npee.Message );
		
						Logger.Write( operationalEvent );
					}
					catch( NoDayTypeForDateException ndte )
					{
						// Display the date error message as no DayType is stored in the DB
						labelInstructions.Visible = true;
						labelInstructions.CssClass = "txtsevenb";
						labelInstructions.Text = GetResource( RES_TEXT_INSTRUCTIONS_INVALID_DATE );
						labelTime.Visible = false;

						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap NoDayTypeForMapException." + ndte.Message );

						Logger.Write( operationalEvent );
					}
					catch( MapExceptionGeneral mge )
					{
						// Log the exception
						OperationalEvent operationalEvent = new OperationalEvent
							( TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI SimpleMap MapExceptionGenral." + mge.Message );

						Logger.Write( operationalEvent );
					}
				}
				else
				{
					// Invalid DateTime has been specified.
					labelTime.Visible = false;
					labelInstructions.Visible = true;
					labelInstructions.CssClass = "txtsevenb";
					labelInstructions.Text = GetResource( RES_TEXT_INSTRUCTIONS_INVALID_DATE );					
				}
			}
		}
        /// <summary>
        /// Handle button click to move up one level in a drillable search
        /// Same behaviour as for clicking page's main back buttons.
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        private void previousLocationButton_Click(object sender, EventArgs e) {
            upAmbiguityLevel();        
        }

        /// <summary>
        /// Handle button click to move down one level in a drillable search
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        private void resolveLocationButton_Click(object sender, EventArgs e) {
            downAmbiguityLevel();        
        }

		/// <summary>
		/// DN079 UEE
		/// Event handler for default action
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void DefaultActionClick(object sender, EventArgs e)
		{
			resolveLocationButton_Click(sender, e); 
		}

        

		#endregion

		#region Convience methods (Location Search Methods/Handlers and date/time methods)

        /// <summary>
        /// Saves the current page language to the InputPageState
        /// </summary>
        private void SavePageLanguageToSession()
        {
            string currentLanguage = GetPageLanguage();

            // Update session variable to the current language
            InputPageState pageState = TDSessionManager.Current.InputPageState;
            pageState.PageLanguage = currentLanguage;


        }


        /// <summary>
        /// Sends back the current page language as a string
        /// </summary>
        private string GetPageLanguage()
        {

            Language currentLanguage = CurrentLanguage.Value;
            return currentLanguage.ToString();
        }

        private bool HasLanguageChanged()
        {
            // Added to prevent map being cleared if language is changed while on this page

            InputPageState pageState = TDSessionManager.Current.InputPageState;
            string previousLanguage = pageState.PageLanguage;
            string currentLanguage = GetPageLanguage();

            // An empty string indicates this page has been accessed or loaded for the first time, 
            // therefore the language has not been changed and is the same as the current language
            if (previousLanguage == string.Empty)
                previousLanguage = currentLanguage;

            if (currentLanguage == previousLanguage)
                return false;
            else
                return true;
        }


		private int GetTimePeriod( DateTime dateTime )
		{
			// Returns the correct day type for the SimpleMap object dependant on the passed in date.
			double totalMinutes = ( dateTime.Hour * 60 ) + dateTime.Minute;
			int timePeriod = ( int ) Math.Floor( totalMinutes/15 );

			return timePeriod;
		}

        /// <summary>
        /// Refreshes the contents of the location selection control and other controls
        /// on the page that are dependant on the ambiguity resolution of the location
        /// specified.
        /// </summary>
        /// <param name="checkInput">True if form input values should be compared with current session 
        /// state values, updating the session state values if different</param>
		private void RefreshLocationControl(bool checkInput)
		{

            bool locationValid = TDSessionManager.Current.InputPageState.MapLocation.Status == TDLocationStatus.Valid;
            // CCN 0427 Added to show either map or location panel
            SearchTrafficMapRow.Visible = !locationValid;
            panelLocationSelect.Visible = !locationValid;
            panelMapTools.Visible = locationValid;
            panelMapKey.Visible = locationValid;
            //labelZoomLevel.Visible = locationValid;
            SimpleMapControl1.Visible = locationValid;
            MapDisabledControl1.Visible = !locationValid;
			printerFriendlyPageButton.Visible = locationValid;
            // CCN 0427 Added to show either map or location panel
            TrafficMapResults.Visible = locationValid;
            previousLocationButton.Visible = mapSearch.CurrentLevel > 0;

            // Set the back button for ambiguity mode
            commandBack.Text = resourceManager.GetString("JourneyPlannerLocationMap.buttonTopBack.Text");
            commandBack.Visible = mapLocation.Status == TDLocationStatus.Ambiguous;

			//Set the Help Label Text depending up the location state
			if(mapLocation.Status == TDLocationStatus.Unspecified)
			{
				HelpControlLocationSelectTraffic.HelpLabel = "helpLabelLocationSelect";
			}

			if(mapLocation.Status == TDLocationStatus.Ambiguous)
			{
				HelpControlLocationSelectTraffic.HelpLabel = "helpLabelLocationSelectAmbig";
			}

            if (mapLocation.Status == TDLocationStatus.Unspecified)
            {
                TriStateLocationControl1.LocationUnspecifiedControl.SelectInstruction.Text = Global.tdResourceManager.GetString(
                    "originSelect.labelSRSelect", TDCultureInfo.CurrentUICulture);
            }

			if (!locationValid)
			{
				InputPageState pageState = TDSessionManager.Current.InputPageState;

				labelInstructions.CssClass = "txtsevenb";
				labelInstructions.Visible = true;
				labelTime.Visible = false;
				labelSelectedLocation.Text = string.Empty;

				TriStateLocationControl1.Populate(
					DataServices.DataServiceType.TrafficMapDrop,
                    pageState.MapType,
					ref mapSearch,
					ref mapLocation,
					ref locationControlType,
					true,
					true,
                    true,
					checkInput
					);
                
				

				// error message
				// if location unspecified and user has typed something
				if (pageState.MapLocation.Status == TDLocationStatus.Unspecified 
					&& pageState.MapLocationSearch.InputText.Length == 0)
				{
					labelInstructions.Text = GetResource( RES_TEXT_INSTRUCTIONS_UNSPEC_EMPTY );
				}
				else if( pageState.MapLocation.Status == TDLocationStatus.Unspecified  )
				{
					labelInstructions.Text = GetResource( RES_TEXT_INSTRUCTIONS_UNSPEC );
				}
				else if (pageState.MapLocation.Status == TDLocationStatus.Ambiguous) // else if ambiguous
				{
					labelInstructions.Text = GetResource( RES_TEXT_INSTRUCTIONS_UNSPEC_AMBIG );
				}
			}
                        
		}

		private void OnValidLocation(object sender, EventArgs e)
		{
			labelSelectedLocation.Text = TDSessionManager.Current.InputPageState.MapLocation.Description;
			labelInstructions.CssClass = "txtsevenb";
			labelInstructions.Text = GetResource( RES_TEXT_INSTRUCTIONS_DATE );

			double easting = TDSessionManager.Current.InputPageState.MapLocation.GridReference.Easting;
			double northing = TDSessionManager.Current.InputPageState.MapLocation.GridReference.Northing;
			int zoom = 100000;

			
			try
			{
				
				DateTime time;
				if (TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
				{
					time = DateTime.Now; 
				}
				else
				{
					// The time for the map comes from the drop down.
					time = trafficDateTimeDropDownControl.DateTimeValue.GetDateTime();
				}

				SimpleMapControl1.Map.TravelDate = time;
				SimpleMapControl1.Map.TimePeriod = GetTimePeriod( time );

				SimpleMapControl1.Map.ZoomToPoint( easting, northing, zoom);				
			}
			catch(PropertiesNotSetException pnse)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

				Logger.Write(operationalEvent);
			}
			catch(MapNotStartedException mnse)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

				Logger.Write(operationalEvent);
			}
			catch(ScaleOutOfRangeException soore)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

				Logger.Write(operationalEvent);
			}
			catch(ScaleZeroOrNegativeException szone)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

				Logger.Write(operationalEvent);
			}
			catch(NoPreviousExtentException npee)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);
			
				Logger.Write(operationalEvent);
			}
			catch(MapExceptionGeneral mge)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mge.Message);

				Logger.Write(operationalEvent);
			}
			RefreshLocationControl(Page.IsPostBack);
		}

        /// <summary>
        /// Creates new location and search objects and associates them with this object and 
        /// the map location and search objects held in the session's page state object.
        /// Updates the location selection control to accept input for a new location
        /// </summary>
        /// <param name="sender">Event originator</param>
        /// <param name="e">Event parameters</param>
        private void OnNewLocation(object sender, EventArgs e)
        {
            InputPageState pageState = TDSessionManager.Current.InputPageState;

            mapLocation = new TDLocation();
            pageState.MapLocation = mapLocation;
            mapSearch = new LocationSearch();
            pageState.MapLocationSearch = mapSearch;
            locationControlType = new LocationSelectControlType(ControlType.NewLocation);
            pageState.MapLocationControlType = locationControlType;
			TDSessionManager.Current.InputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.TrafficMapDrop);
			RefreshLocationControl(false);

        }

        /// <summary>
        /// For an ambiguous location search that is drillable, attempts
        /// to decrease the current drill down level by one. If the search is
        /// not ambiguous, or the current level is zero, then the user is
        /// redirected to the previous page.
        /// </summary>
        private void upAmbiguityLevel() {
            if (mapLocation.Status == TDLocationStatus.Ambiguous && mapSearch.CurrentLevel > 0) {
                mapSearch.DecrementLevel();
                RefreshLocationControl(Page.IsPostBack);
            } else {
                // Write the Transition Event
                ITDSessionManager sessionManager = 
                    (ITDSessionManager)TDServiceDiscovery.Current
                    [ServiceDiscoveryKey.SessionManager];

                sessionManager.FormShift[SessionKey.TransitionEvent] =
                    TransitionEvent.LocationMapBack;
            }
        }

        /// <summary>
        /// Performs a search for the currently selected location in an
        /// attempt to resolve it. Refreshes the page with the result.
        /// </summary>
        private void downAmbiguityLevel() {
            TriStateLocationControl1.Search(true);
            RefreshLocationControl(Page.IsPostBack);
        }

        #endregion

        /// <summary>
        /// Store state of page's control (data/time and map view state) in session so that
        /// they values can be reinstated when user returns to page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void OnMapStore(object sender, EventArgs e )
		{
            // Persist the Calendar date time to allow reload if coming back to page
            TDSessionManager.Current.InputPageState.CalendarDateTime = trafficDateTimeDropDownControl.DateTimeValue;

			object o = SimpleMapControl1.Map.ExtractViewState();

			TDSessionManager.Current.StoredMapViewState[ TDSessionManager.OUTWARDMAP ] = o;
		}

		/// <summary>
		/// Gets the default location search type from DataServices. 
		/// </summary>
		/// <param name="listType">The drop-down type required</param>
		/// <returns>The default search type to be used</returns>
		private SearchType GetDefaultSearchType(DataServiceType listType)
		{
			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultItemValue = ds.GetDefaultListControlValue(listType);
			return (SearchType) (Enum.Parse(typeof(SearchType), defaultItemValue));
		}

	}
}
