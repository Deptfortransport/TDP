// ***********************************************
// NAME         : JourneyLandingPage.aspx.cs
// AUTHOR       : Darshan Sawe
// DATE CREATED : 
// DESCRIPTION  : Journey landing page for use by the journey planning iFrame. Accepts a place name and gazetteer type
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/iFrames/LocationLandingPage.aspx.cs-arc  $
//
//   Rev 1.1   Nov 17 2009 18:01:54   mmodi
//Take user to new map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
// Darshan forgot to add version heading details

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
using TransportDirect.JourneyPlanning.CJPInterface;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// This is a Landing Page for iFrame Find A Place Mini-Page
	/// </summary>
	public partial class LocationLandingPage : TDPage
	{
		//private variables for storing query string parameters
		private string place = String.Empty;
		private string locationGazetteer = String.Empty;
		private string locationShowOptions = String.Empty;
		private string partnerId = String.Empty;

		public LocationLandingPage()
		{
			this.pageId = TransportDirect.Common.PageId.LocationLandingPage;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			saveQueryString();

			SearchType searchType = GetSearchType();
			FindLocationShowOptions findLocationShowOptions = GetFindLocationShowOptions();

			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.iFrameLocationLandingpage, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);
		    
			SetupLocationOrPlannerSearch(searchType, findLocationShowOptions, place);
		}

		#region Private & Protected Methods

		/// <summary>
		/// saving Query String received from iFrameFindAPlaceControl
		/// </summary>
		private void saveQueryString()
		{
			this.place = Server.HtmlDecode(GetValidParam("place"));

			this.locationGazetteer = Server.HtmlDecode(GetValidParam("locGaz"));
			
			this.locationShowOptions = Server.HtmlDecode(GetValidParam("locOpt"));

			this.partnerId = Server.HtmlDecode(GetValidParam("pid"));
		}
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

			TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
			TDSessionManager.Current.InputPageState.MapLocationSearch = newLocationSearch;     
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
		/// Extracts the search type from the dropdown list
		/// </summary>
		/// <returns></returns>
		private SearchType GetSearchType()
		{
			SearchType returnSearchType;
			FindLocationGazeteerOptions findLocationGazeteerOptions = GetFindLocationGazetteerOptions(locationGazetteer);
			
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
			return GetFindLocationShowOptions(locationShowOptions);
		}

       
		/// <summary>
		///	 Returns the GazeteerOptions selected by the user 
		/// </summary>
		/// <param name="listValue">GazeteerOptions as string</param>
		/// <returns>FindLocationGazeteerOptions selected by the user</returns>
		private FindLocationGazeteerOptions  GetFindLocationGazetteerOptions(string listValue)
		{
			if (!(listValue ==""))
			{
				return (FindLocationGazeteerOptions) Enum.Parse(typeof(FindLocationGazeteerOptions), listValue);  
			}
				// hardcoding for default value to be address postcode
			else
			{
				return (FindLocationGazeteerOptions) Enum.Parse(typeof(FindLocationGazeteerOptions), "AddressPostcode"); 
			}
		}

		
		/// <summary>
		/// Returns the ShowOptions selected by the user 
		/// </summary>
		/// <param name="listValue">FindLocationShowOptions as string</param>
		/// <returns>FindLocationShowOptions selected by the user</returns>
		private FindLocationShowOptions  GetFindLocationShowOptions(string listValue)
		{
			if (!(listValue ==""))
			{
				return (FindLocationShowOptions) Enum.Parse(typeof(FindLocationShowOptions), listValue);  
			}
				// hardcoding for default value to be MapOfArea
			else
			{
				return (FindLocationShowOptions) Enum.Parse(typeof(FindLocationShowOptions), "MapOfArea");
			}
		}

		/// <summary>
		/// Parses the query string variables and return param value
		/// </summary>
		/// <param name="paramName">Query string Parameter to be fetched</param>
		/// <returns></returns>
		private string GetValidParam(string paramName)
		{
			string tempCache = string.Empty;
			switch (paramName)
			{
					//Protect against XSS attacks (cross site scripting) by HTML Encoding the text.
				case "place":
				case "locGaz":
				case "locOpt":
				case "pid":
					tempCache = Server.UrlDecode(Page.Request.Params.Get(paramName));
					break;
				default:
					tempCache = string.Empty;
					break;
			}

			if (tempCache!= null)
			{
				return Server.HtmlEncode(tempCache);
			}
			else
			{
				return string.Empty;
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
