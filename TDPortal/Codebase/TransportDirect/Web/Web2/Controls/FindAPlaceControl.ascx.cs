// *********************************************** 
// NAME                 : FindAPlaceControl.ascx
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/10/2005 
// DESCRIPTION  		: Home page Find A place control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindAPlaceControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Nov 17 2009 11:33:26   mmodi
//Transfer to the new Map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:20:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:56   mturner
//Initial revision.
//
//   Rev 1.16   Sep 12 2007 12:22:44   asinclair
//Changed the PageTransferDetails link - now links to mini homepage for maps
//
//   Rev 1.15   Oct 06 2006 14:28:52   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.14.1.0   Aug 14 2006 10:43:16   esevern
//Added SetupCarParkPlannerSearch for find nearest car park search
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.14   Mar 09 2006 11:48:56   esevern
//corrected PageId used when logging PageEntryEvent
//Resolution for 3586: Additional PageEntryEvents on HomePage
//
//   Rev 1.13   Feb 23 2006 16:10:54   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.12   Feb 20 2006 14:24:48   esevern
//added logging of PageEntryEvent on submit
//Resolution for 3586: Additional PageEntryEvents on HomePage
//
//   Rev 1.11   Feb 10 2006 15:04:46   build
//Automatically merged from branch for stream3180
//
//   Rev 1.10.1.0   Jan 03 2006 10:43:14   RGriffith
//Changes to us TDImage
//
//   Rev 1.11   Dec 23 2005 14:26:24   RGriffith
//Changes to use TDImage
//
//   Rev 1.10   Nov 24 2005 09:56:48   RGriffith
//Code review suggestions implemented for stream2880
//
//   Rev 1.9   Nov 15 2005 16:34:06   pcross
//Minor updates. Includes update to make image a hyperlink instead of an image button
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.8   Nov 14 2005 13:05:28   pcross
//Replaced deprecated GetString with GetResource
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.7   Nov 14 2005 11:43:02   RGriffith
//Code change to highlight difference between SearchType.Locality and SearchType.City
//
//   Rev 1.6   Nov 10 2005 15:35:00   pcross
//Minor updates
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.5   Nov 07 2005 18:07:10   schand
//Correct resource Id name for Show label
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.4   Nov 07 2005 16:29:24   schand
//FxCop fix
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.3   Nov 07 2005 15:24:42   schand
//Removed additional commented line from the code
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.2   Nov 07 2005 12:19:18   schand
//Added Table Summary to the Table & also added comments for each methods and properties
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.1   Nov 04 2005 15:25:22   schand
//added little tweak for city/town nased search
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.0   Nov 03 2005 11:48:22   schand
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region Using statements
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ServiceDiscovery;	
	using TransportDirect.UserPortal.DataServices;  
	using TransportDirect.UserPortal.SessionManager;  
	using TransportDirect.Common;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.LocationService; 
	using TransportDirect.UserPortal.Web.Adapters;  
	using TransportDirect.UserPortal.ScreenFlow;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	
    using Logger = System.Diagnostics.Trace;
    using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
    using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
	#endregion
	

	/// <summary>
	///		FindAPlaceControl to be used on Home page.
	/// </summary>	
	[System.Runtime.InteropServices.ComVisible(false)] 
	public partial class FindAPlaceControl : TDUserControl
	{   		
		
		#region Protected Members
		protected IDataServices populator;
		#endregion


		#region Event Handlers
		/// <summary>
		/// Event handler for Page Load Event
		/// </summary>
		/// <param name="sender"> sender object</param>
		/// <param name="e">Event Arguments</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //We need to persist drop down list values, since these will be
            //cleared by the UpdateControl call:
            int dropDownLocationGazetteerOptionsSelectedIndex = dropDownLocationGazetteerOptions.SelectedIndex;
            int dropDownLocationShowOptionsSelectedIndex = dropDownLocationShowOptions.SelectedIndex;

			// Update control for dropdown list, image path, soft content etc
			UpdateControl();

            //Put the values back:
            dropDownLocationGazetteerOptions.SelectedIndex = dropDownLocationGazetteerOptionsSelectedIndex;
            dropDownLocationShowOptions.SelectedIndex = dropDownLocationShowOptionsSelectedIndex;
		}

		/// <summary>
		/// Page Initial Event 
		/// </summary>
		/// <param name="sender"> sender object</param>
		/// <param name="e">Event Arguments</param> 
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Poulating dropdown list
			populator  = (IDataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
           
		}

		
		/// <summary>
		///	 Submit Button Event Handler
		/// </summary>
		/// <param name="sender"> sender object</param>
		/// <param name="e">Event Arguments</param>
		protected void buttonSubmit_Click(object sender, System.EventArgs e)
		{
			SearchType searchType = GetSearchType();
			FindLocationShowOptions findLocationShowOptions = GetFindLocationShowOptions();

			//Log submit event
			PageEntryEvent logPage = new PageEntryEvent(PageId.HomePageFindAPlacePlanner, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(logPage);
		    
			SetupLocationOrPlannerSearch(searchType, findLocationShowOptions, textBoxPlace.Text);

		}

		#endregion
		
		#region Private & Protected Methods
		/// <summary>
		/// This method sets the session parameter required for Location Map or Find nearest station page.
		/// </summary>
		/// <param name="searchType">Type of Search to be performed</param>
		/// <param name="findLocationShowOptions">Location Type Show Option</param>
		/// <param name="locationText">Location text</param>
		private void SetupLocationOrPlannerSearch(SearchType searchType, FindLocationShowOptions findLocationShowOptions, string locationText)
		{
			// Always initialise InputPageState
			TDSessionManager.Current.InputPageState = new InputPageState();	
			
			if (findLocationShowOptions == FindLocationShowOptions.StationAirport)
			{
				// Set up parameter and session for Quick Planner search
				SetupPlannerSearch(searchType, locationText);
			}
			else if(findLocationShowOptions == FindLocationShowOptions.CarPark)
			{
				SetupCarParkPlannerSearch(searchType, locationText);
			}
			else
			{	// Set up parameter and session for Location Map
				SetupLocationSearch(searchType, locationText);
			}

			SetTransitionForLocationOrPlannerSearch(findLocationShowOptions);  

		}

		
		/// <summary>
		///	This method sets the session parameter required for Location Map
		/// </summary>
		/// <param name="searchType">Type of Search to be performed</param>		
		/// <param name="locationText">Location text</param>
		private void SetupLocationSearch(SearchType searchType, string locationText)
		{   			
			LocationSearch newLocationSearch = new LocationSearch();
			
			newLocationSearch.FuzzySearch = false;
			newLocationSearch.VagueSearch = false;
			newLocationSearch.InputText = locationText;			
			
			//Check to see what Gazeteer option they have selected and set the
			//SearchType accordingly. The code here defaults to the AddressPostCode
			newLocationSearch.SearchType = searchType;

            LocationSelectControlType locationControlType = new LocationSelectControlType(ControlType.Default);
            
			TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
			TDSessionManager.Current.InputPageState.MapLocationSearch = newLocationSearch;
            TDSessionManager.Current.InputPageState.MapLocationControlType = locationControlType;
		}


		/// <summary>
		///	This method sets the session parameter required for Find nearest station page.
		/// </summary>
		/// <param name="searchType">Type of Search to be performed</param>		
		/// <param name="locationText">Location text</param>
		private void SetupPlannerSearch(SearchType searchType, string locationText)
		{
			FindStationPageState pageState = new FindStationPageState(); 			 
			pageState.CurrentLocation = new TDLocation();
			pageState.SearchFrom.FuzzySearch = false;
			pageState.SearchFrom.VagueSearch = false;
			pageState.SearchFrom.InputText = locationText;
		
			// setting up the search type
			pageState.SearchFrom.SearchType = searchType;
		
			TDSessionManager.Current.FindStationPageState = pageState;
			TDSessionManager.Current.FindStationPageState.CurrentSearch =  pageState.SearchFrom;
		}

		/// <summary>
		///	This method sets the session parameter required for find nearest car park.
		/// </summary>
		/// <param name="searchType">Type of Search to be performed</param>		
		/// <param name="locationText">Location text</param>
		private void SetupCarParkPlannerSearch(SearchType searchType, string locationText)
		{
			FindCarParkPageState pageState = new FindCarParkPageState(); 			 
			pageState.CurrentLocation = new TDLocation();
			pageState.SearchFrom.FuzzySearch = false;
			pageState.SearchFrom.VagueSearch = false;
			pageState.SearchFrom.InputText = locationText;
			pageState.SearchFrom.SearchType = searchType;
			TDSessionManager.Current.FindCarParkPageState = pageState;
			TDSessionManager.Current.FindCarParkPageState.CurrentSearch =  pageState.SearchFrom;
		}
        	
       
		/// <summary>
		/// Sets the destination page type 
		/// </summary>
		/// <param name="findLocationShowOptions">Location Type Show Option</param>
		private void SetTransitionForLocationOrPlannerSearch(FindLocationShowOptions findLocationShowOptions)
		{
			//Check to see what Show option they have selected and set the SessionKey 
			//accordingly. The code here defaults to the location map
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			switch(findLocationShowOptions)
			{
				case FindLocationShowOptions.StationAirport: 
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationResultsNewLocation;
					break;
				case FindLocationShowOptions.CarPark:
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
					break;
				case FindLocationShowOptions.TrafficLevels:
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTrafficMap;
					break;
				case FindLocationShowOptions.MapOfArea:					
				case FindLocationShowOptions.SelectOption:
				default:
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindMapInputDefault;
					break;
			}
					
			//Set this property to tell the target page we are coming in from the FindAPlace			
			sessionManager.SetOneUseKey(SessionKey.FindALocationFromHomePage,string.Empty);
		}


		/// <summary>
		///	 Sets the destination if Map icon clicked
		/// </summary>
		private void SetTransitionEventForHomeMapImageButton()
		{
			JourneyPlannerInputAdapter.SetTransitionForMap();  
		}
		
		
		/// <summary>
		/// Extracts the search type from the dropdown list
		/// </summary>
		/// <returns></returns>
		private SearchType GetSearchType()
		{
			string selectedGazOption;
			SearchType returnSearchType;
			selectedGazOption =	dropDownLocationGazetteerOptions.SelectedValue.ToString();
			FindLocationGazeteerOptions findLocationGazeteerOptions =   GetFindLocationGazetteerOptions(selectedGazOption);
			
			switch(findLocationGazeteerOptions)
			{
				case FindLocationGazeteerOptions.AttractionFacility: 
					returnSearchType = SearchType.POI; 
					break;
				case FindLocationGazeteerOptions.CityTownSuburb: 
					// Note: Search Type Locality = City/Town/Suburb - NOT City
					returnSearchType = SearchType.Locality; 
					break;
				case FindLocationGazeteerOptions.StationAirport: 
					returnSearchType = SearchType.MainStationAirport; 
					break;
				case FindLocationGazeteerOptions.AddressPostcode:									
				default:
					returnSearchType = SearchType.AddressPostCode; 
					break;
			}

			return returnSearchType;
			
		}

		
		/// <summary>
		/// Extracts the Show Option type from the dropdown list
		/// </summary>
		/// <returns></returns>
		private FindLocationShowOptions GetFindLocationShowOptions()
		{
			string selectedShowOption;			
			selectedShowOption = dropDownLocationShowOptions.SelectedValue.ToString();			   
			return GetFindLocationShowOptions(selectedShowOption);
		}

       
		/// <summary>
		/// Update the control with resource text and list items
		/// </summary>
		private void UpdateControl()
		{
			// populate dropdown list for place type and show options
			PopulateList();

			// Assign URLs to hyperlinks
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			string baseChannel = String.Empty;
			string url = String.Empty;
			if (TDPage.SessionChannelName != null)
				baseChannel = TDPage.getBaseChannelURL(TDPage.SessionChannelName);

			// Hyperlink Transfer details for DoorToDoor Hyperlink
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyPlannerInput);

			// Hyperlink Transfer details for DoorToDoor Hyperlink
			// Transfer details for Find a place More... link
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomeFindAPlace);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindAPlaceMore.NavigateUrl = url;

			// Updatng Image url path
			imageMap.ImageUrl = GetResource("FindAPlaceControl.imageMap.ImageUrl");
			// Updating alt text 
			imageMap.AlternateText =  GetResource("FindAPlaceControl.imageMap.AltText");
			hyperlinkFindAPlaceMore.ToolTip = GetResource("FindAPlaceControl.imageMap.AltText");

			buttonSubmit.ToolTip = GetResource("FindAPlaceControl.buttonSubmit.AltText"); 

			// Getting text for Labels
			labelPlace.Text = GetResource("FindAPlaceControl.labelPlace.Text");

			// setting screen reader field
			labelPlaceTypeScreenReader.Text = GetResource("FindAPlaceControl.labelPlaceTypeScreenReader.Text");

			// setting show option field
			textBoxShowOption.Text = GetResource("FindAPlaceControl.textboxShowOption.Text");

			// setting the text for submit button
			buttonSubmit.Text = GetResource("FindAPlaceControl.buttonSubmit.Text"); 
		}
  

		/// <summary>
		/// Polulate the dropdown list with the list items
		/// </summary>
		private void PopulateList()
		{
		  
			// Populating LocationGazeteerOptions
			populator.LoadListControl(
				DataServiceType.FindLocationGazeteerOptions, dropDownLocationGazetteerOptions, resourceManager ); 

			// Populating FindLocationShowOptions
			populator.LoadListControl(
				DataServiceType.FindLocationShowOptions, dropDownLocationShowOptions, resourceManager); 
         		 
 
		}

		
		/// <summary>
		///	 Returns the GazeteerOptions selected by the user 
		/// </summary>
		/// <param name="listValue">GazeteerOptions as string</param>
		/// <returns>FindLocationGazeteerOptions selected by the user</returns>
		private FindLocationGazeteerOptions  GetFindLocationGazetteerOptions(string listValue)
		{			
			return (FindLocationGazeteerOptions) Enum.Parse(typeof(FindLocationGazeteerOptions), listValue);  
		}

		
		/// <summary>
		/// Returns the ShowOptions selected by the user 
		/// </summary>
		/// <param name="listValue">FindLocationShowOptions as string</param>
		/// <returns>FindLocationShowOptions selected by the user</returns>
		private FindLocationShowOptions  GetFindLocationShowOptions(string listValue)
		{			
			return (FindLocationShowOptions) Enum.Parse(typeof(FindLocationShowOptions), listValue);  
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonSubmit.Click += new EventHandler(this.buttonSubmit_Click);
		}
		#endregion
		
	}
}
