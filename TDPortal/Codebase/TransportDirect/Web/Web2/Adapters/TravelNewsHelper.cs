// ***********************************************
// NAME 		: TravelNewsHelper.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 14/07/2005
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/TravelNewsHelper.cs-arc  $
//
//   Rev 1.5   Jun 23 2010 09:06:14   apatel
//Updated to resolve the issue of error page display when user navigates away from travel news page and no region is selected
//Resolution for 5558: Travel News Navigation issue
//
//   Rev 1.4   Nov 26 2009 15:47:20   apatel
//TravelNews page and controls updated for new mapping functionality
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Sep 29 2009 11:46:48   rbroddle
//CCN 485a Travel News Parts 3 and 4 Hierarchy & Roadworks.
//Resolution for 5321: Travel News Parts 3 and 4 Hierarchy & Roadworks
//
//   Rev 1.2   Mar 31 2008 12:59:14   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 15 2008 11:04:00 apatel
//  CCN 0427 Added Properties to configure functional areas of live travel news home page.
//
//   Rev DevFactory   Feb 04 2008 17:00:00   mmodi
//Addtional error strings
//
//   Rev 1.0   Nov 08 2007 13:11:30   mturner
//Initial revision.
//
//   Rev 1.9   Aug 20 2007 11:05:14   pscott
//IR 4479 - Travel News freeform text search changes
//
//   Rev 1.8   Aug 09 2006 11:50:50   mmodi
//Added check page language methods
//Resolution for 4146: Selecting Welsh link on Travel news map reverts view back to table
//
//   Rev 1.7   Mar 28 2006 11:09:04   build
//Automatically merged from branch for stream0024
//
//   Rev 1.6.1.1   Mar 20 2006 15:20:26   mdambrine
//Fixed vanive with saving the preferences. Was failing because of invalid parsing.
//Resolution for 3643: Saved preferences do not save
//
//   Rev 1.6.1.0   Mar 03 2006 16:21:30   AViitanen
//Removed IsTransportValidForMapView and refs to SelectedDetails. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.6   Feb 23 2006 17:16:32   RWilby
//Merged stream3129
//
//   Rev 1.5   Jan 24 2006 15:39:30   RWilby
//Added logic to check for valid dates in the TravelNewsHelper.IsDelayValid method
//Resolution for 3506: Live Travel: searching for recent delays on an invalid date causes server error
//
//   Rev 1.4   Dec 20 2005 11:11:14   jgeorge
//Added Page Landing Code 
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.3   Oct 06 2005 15:16:36   kjosling
//Merged with stream 2817
//Resolution for 2817: DEL 7.1.4 Stream Label
//
//   Rev 1.2.1.0   Oct 03 2005 14:12:44   CRees
//fix for esri dll changes
//
//   Rev 1.2   Aug 18 2005 15:38:02   jgeorge
//Added additional property
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.1   Aug 03 2005 14:00:56   jgeorge
//FxCop recommended changes
//
//   Rev 1.0   Aug 03 2005 12:36:36   jgeorge
//Initial revision.

using System;
using System.Diagnostics;
using System.Collections;
using System.Globalization;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Helper class for the TravelNews and PrintableTravelNews pages.
	/// </summary>
	public sealed class TravelNewsHelper
	{
		/// <summary>
		/// Resource key for langStrings entry to use when displaying an "Invalid Region" message
		/// </summary>
		public const string ResourceKeyInvalidRegion = "ShowNewsControl.ErrorMessage.InvalidRegion";

        /// <summary>
        /// Resource key for langStrings entry to use when displaying an error message for user switching to map view with search text entered
        /// </summary>
        public const string InvalidRegionAndSearchPhrase = "ShowNewsControl.ErrorMessage.InvalidRegionAndSearchPhrase";
        
        /// <summary>
        /// Resource key for langStrings entry to use when displaying an error message when a user selects a regions with search text entered
        /// </summary>
        public const string RegionSelectWithSearchPhraseEntered = "ShowNewsControl.ErrorMessage.RegionSelectWithSearchPhraseEntered";

        /// <summary>
        /// Resource key for langStrings entry to use when displaying an error message when a user switches to map view with search text entered
        /// </summary>
        public const string SwitchToMapWithSearchPhraseEntered = "ShowNewsControl.ErrorMessage.SwitchToMapWithSearchPhraseEntered";

		/// <summary>
		/// Resource key for langStrings entry to use when displaying an "Invalid Transport" message
		/// </summary>
		public const string ResourceKeyInvalidTransport = "ShowNewsControl.ErrorMessage.InvalidTransport";
		
		/// <summary>
		/// Resource key for langStrings entry to use when displaying an "Invalid Date" message
		/// </summary>
		public const string ResourceKeyInvalidDate = "ShowNewsControl.ErrorMessage.InvalidDate";

		/// <summary>
		/// Resource key for langStrings entry to use when displaying an "Invalid Delay" message
		/// </summary>
		public const string ResourceKeyInvalidDelay = "ShowNewsControl.ErrorMessage.InvalidDelay";

        public const string KeyNewsHoverPopup = "Web.TravelNewsHeadlineControl.ShowPopup";


		/// <summary>
		/// Private constructor to prevent instantiation
		/// </summary>
		private TravelNewsHelper()
		{ }

		/// <summary>
		/// Sets the curent travel news state to a new object with default values
		/// </summary>
		/// <returns>True if entering from page landing, false if normal entry</returns>
		public static bool InitialiseTravelNewsState()
		{
			bool result;
			TravelNewsState newState = new TravelNewsState();
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ] == true)
			{
				result = true;
				InitialiseForPageLanding(newState);
			}
			else
			{
				result = false;
				newState.SetDefaultState();
			}

			CurrentTravelNewsState = newState;
			return result;
		}

		/// <summary>
		/// Read/write property returning the current TravelNewsState object
		/// </summary>
		public static TravelNewsState CurrentTravelNewsState
		{
			get { return TDSessionManager.Current.TravelNewsState; }
			set { TDSessionManager.Current.TravelNewsState = value; }
		}

		/// <summary>
		/// Only available to logged in users.  Retrieves the user's saved 
		/// travel news preferences (if they had any) from profile storage
		/// and updates the travelNewsState accordingly.
		/// </summary>
		public static void LoadPreferences() 
		{			
			// Retrieve session data
			TDProfile userPreferences = TDSessionManager.Current.CurrentUser.UserProfile;
			TravelNewsState travelNewsState = CurrentTravelNewsState;

			// Set up values for each session value that exists				
			if (userPreferences.Properties[ProfileKeys.TRAVEL_NEWS_AREA].Value != null) 
				travelNewsState.SelectedRegion = userPreferences.Properties[ProfileKeys.TRAVEL_NEWS_AREA].Value.ToString(); 

			if (userPreferences.Properties[ProfileKeys.TRAVEL_NEWS_MODE].Value != null) 
				travelNewsState.SelectedTransport = (TransportType) userPreferences.Properties[ProfileKeys.TRAVEL_NEWS_MODE].Value;

			if (userPreferences.Properties[ProfileKeys.TRAVEL_NEWS_SEVERITY].Value != null) 
				travelNewsState.SelectedDelays = (DelayType) userPreferences.Properties[ProfileKeys.TRAVEL_NEWS_SEVERITY].Value;
		}

		/// <summary>
		/// Only available to logged in users.  Transfers the user's travel news preferences 
		/// from travel news state to profile storage.
		/// </summary>
		public static void SavePreferences()
		{
			TDProfile newsPreferences = TDSessionManager.Current.CurrentUser.UserProfile;

			// Populate preference fields with user selected values
			newsPreferences.Properties[ProfileKeys.TRAVEL_NEWS_AREA].Value = CurrentTravelNewsState.SelectedRegion;;  
			newsPreferences.Properties[ProfileKeys.TRAVEL_NEWS_MODE].Value = CurrentTravelNewsState.SelectedTransport; 
			newsPreferences.Properties[ProfileKeys.TRAVEL_NEWS_SEVERITY].Value = CurrentTravelNewsState.SelectedDelays;  
			newsPreferences.Update();

			// Log the save preferences event
			UserPreferenceSaveEvent pe = new UserPreferenceSaveEvent(UserPreferenceSaveEventCategory.News, TDSessionManager.Current.Session.SessionID);
			Trace.Write(pe);
		}

		/// <summary>
		/// Retrieves the array of TravelNewsItem that match the current travel news state
		/// </summary>
		/// <returns></returns>
		public static TravelNewsItem[] GetNewsItems()
		{
			ITravelNewsHandler travelNewsHandler = (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];

			return travelNewsHandler.GetDetails(CurrentTravelNewsState);
		}

        /// <summary>
        /// Retrieves the array of TravelNewsItems that are children of the selected incident
        /// </summary>
        /// <returns></returns>
        public static TravelNewsItem[] GetChildrenTravelNewsItems(string uid)
        {
            ITravelNewsHandler travelNewsHandler = (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];

            return travelNewsHandler.GetChildrenDetailsByUid(uid);
        }

		/// <summary>
		/// Read only property which returns whether or not the current travel news
		/// state indicates map display.
		/// </summary>
		public static bool ShowMap
		{
			get { return CurrentTravelNewsState.SelectedView == TravelNewsViewType.Map; }
		}

		/// <summary>
		/// Read only property which returns whether or not the current travel news
		/// state indicates table display.
		/// </summary>
		public static bool ShowGrid
		{
			get { return CurrentTravelNewsState.SelectedView == TravelNewsViewType.Details; }
		}

		/// <summary>
		/// Read only property which returns the current IncidentSeverityLevel of incidents
		/// that should be displayed.
		/// </summary>
		public static Map.IncidentSeverityLevel MapIncidentSeverityLevel
		{
			get 
			{
				switch (CurrentTravelNewsState.SelectedDelays)
				{
					case DelayType.Major :
						return Map.IncidentSeverityLevel.Severe;
					case DelayType.Recent :
					case DelayType.All :
					default:
						return Map.IncidentSeverityLevel.All;
				}
			}
		}

		/// <summary>
		/// Read only property which returns true if only recent incidents should be displayed
		/// </summary>
		public static bool MapDisplayRecent
		{
			get
			{
				switch (CurrentTravelNewsState.SelectedDelays)
				{
					case DelayType.Recent :
						return true;
					case DelayType.Major :
					case DelayType.All :
					default:
						return false;
				}			
			}
		}

		/// <summary>
		/// Returns the travel news item corresponding to the given UID.
		/// </summary>
		/// <param name="uid"></param>
		/// <returns></returns>
		public static TravelNewsItem GetTravelNewsItem(string uid)
		{
			ITravelNewsHandler handler = (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];
			return handler.GetDetailsByUid(uid);
		}

		/// <summary>
		/// The MapRegionSelectControl uses Ids to represent the regions. The travel news code expects
		/// resource Ids. This method converts between the two.
		/// </summary>
		/// <param name="id">A region Id</param>
		/// <returns>Corresponding resource Id</returns>
		public static string RegionIdToNewsValue(string id)
		{
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			return ds.GetResourceId(DataServiceType.NewsRegionDrop, id);
		}

		/// <summary>
		/// The MapRegionSelectControl uses Ids to represent the regions. The travel news code expects
		/// resource Ids. This method converts between the two.
		/// </summary>
		/// <param name="newsValue">A resource Id</param>
		/// <returns>Corresponding region Id</returns>
		public static string RegionNewsValueToId(string newsValue)
		{
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			return ds.GetValue(DataServiceType.NewsRegionDrop, newsValue);
		}

		/// <summary>
		/// Checks if the region selected is valid (for map view)
		/// </summary>
		/// <returns>valid region</returns>
		public static bool IsRegionValidForMapView(string travelNewsRegion)
		{
			return (travelNewsRegion != "All");
		}

		/// <summary>
		/// Validates that the date the user has selected 
		/// to view incidents for is not in the past
		/// </summary>
		public static bool IsDateValid(TDDateTime date)
		{
			// Compare the user's selected date with the current date
			return ( (date != null) && (date >= DateTime.Today) );
		}

		/// <summary>
		/// Validates that the type of delay chosen is valid for the date the user has selected 
		/// </summary>
		public static bool IsDelayValid(TDDateTime date, DelayType selectedDelays)
		{
			// Compare the user's selected date with the current date
			if (selectedDelays == DelayType.Recent) 
			{
				//IR3506: Check for valid date before comparing dates
				if(IsDateValid(date))
				{
					if (TDDateTime.AreSameDate(date,TDDateTime.Now))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Returns the zoom level to use when displaying a specific incident on the map
		/// </summary>
		public static int DefaultIncidentZoomLevel
		{
			get { return Convert.ToInt32( Properties.Current["TravelNews.IncidentMapping.DefaultIncidentZoomLevel"], CultureInfo.InvariantCulture ); }
		}
			

		/// <summary>
		/// Gets a comma delimited string of coordinates for a region
		/// from Properties and converts it into a int array.
		/// </summary>
		/// <returns>array of coords</returns>
		public static int[] GetRegionCoordinates()
		{
            if (CurrentTravelNewsState.SelectedRegion == "All")
            {
                return new int[0];
            }
			string regionId = RegionNewsValueToId(CurrentTravelNewsState.SelectedRegion);

			// Generate resource ID based on region name
			string resourceID = string.Format(CultureInfo.InvariantCulture, "Web.MappingComponent.RegionCoordinates.{0}", new object[] { regionId });

			// Obtain coordinate string from properties
			string strCoordinates = Properties.Current[resourceID];

			// Set up comma delimiter
			char[] seperator = new char[] { ',' };

			// Split string into four coordinates
			string[] arrCoordinates =  strCoordinates.Split(seperator);

			// Convert strings to int values and add to array
			int[] coords = new int[4];
			for (int i=0; i<arrCoordinates.Length; i++)
				coords[i] = Convert.ToInt32(arrCoordinates[i], CultureInfo.InvariantCulture);

			// Return int array
			return coords;
		}


        /// <summary>
        /// Gets a comma delimited string of coordinates for a region
        /// from Properties and converts it into a int array.
        /// </summary>
        /// <returns>array of coords</returns>
        public static string[][] GetAllRegionCoordinates()
        {
            string[][] regionCoordinates = new string[12][];

            for (int count = 0; count < 12; count++)
            {
                if (count == 0)
                {
                    regionCoordinates[count] = new string[1];
                    regionCoordinates[count][0] = RegionIdToNewsValue(count.ToString());
                }
                else
                {
                    // Generate resource ID based on region name
                    string resourceID = string.Format(CultureInfo.InvariantCulture, "Web.MappingComponent.RegionCoordinates.{0}", new object[] { count });

                    // Obtain coordinate string from properties
                    string strCoordinates = Properties.Current[resourceID];

                    regionCoordinates[count] = new string[2];
                    regionCoordinates[count][0] = RegionIdToNewsValue(count.ToString());
                    regionCoordinates[count][1] = strCoordinates;
                }
            }
            // Return int array
            return regionCoordinates;
        }

		private static void InitialiseForPageLanding(TravelNewsState travelNewsState)
		{
			string displaytype = TDSessionManager.Current.Session[ SessionKey.LandingPageNewsDisplayInputType ];
			string regiontype = TDSessionManager.Current.Session[ SessionKey.LandingPageNewsRegionInputType ];
			string severitytype = TDSessionManager.Current.Session[ SessionKey.LandingPageNewsSeverityInputType ];
			string transporttype = TDSessionManager.Current.Session[ SessionKey.LandingPageNewsTransportInputType ];
			
			travelNewsState.SelectedDate = TDDateTime.Now;

			try
			{
				switch (regiontype)
				{

					case "London":
					case "South West":
					case "South East":
					case "East Anglia":
					case "East Midlands":
					case "West Midlands":
					case "Yorkshire and Humber":
					case "North West":
					case "North East":
					case "Scotland":
					case "Wales":
						travelNewsState.SelectedRegion = regiontype;
						break;
					default: 
						travelNewsState.SelectedRegion = "All";
						break;
				}
				// if search string has been entered revert to searching
				// All regions as per the on screen message
				if(travelNewsState.SearchPhrase.Length > 0)
				{
					travelNewsState.SelectedRegion = "All";
				}
			}
			catch (System.NullReferenceException)
			{
				travelNewsState.SelectedRegion = "All";
			}

			try
			{
				switch (transporttype.ToUpper())
				{

					case "R":
						travelNewsState.SelectedTransport = TransportType.Road;
						break;

					case "P":
						travelNewsState.SelectedTransport = TransportType.PublicTransport;
						break;

					default: 
						travelNewsState.SelectedTransport = TransportType.All;
						break;
				}
			}
			catch (System.NullReferenceException)
			{
				travelNewsState.SelectedTransport = TransportType.All;
			}

			try
			{
				switch (severitytype.ToUpper())
				{
					case "ALL":
						travelNewsState.SelectedDelays = DelayType.All;
						break;

					default: 
						travelNewsState.SelectedDelays = DelayType.Major;
						break;
				}
			}
			catch (System.NullReferenceException)
			{
				travelNewsState.SelectedDelays = DelayType.Major;
			}

			try
			{
				switch (displaytype.ToUpper())
				{

					case "M":
						travelNewsState.SelectedView = TravelNewsViewType.Map;
						break;

					default: 
						travelNewsState.SelectedView = TravelNewsViewType.Details;
						break;
				}
			}
			catch (System.NullReferenceException)
			{
				travelNewsState.SelectedView = TravelNewsViewType.Details;
			}
		}

		/// <summary>
		/// Performs a check to find out if the language has been changed while
		/// on this page, uses the TravelNewsPageLanguage in the TravelNewsState
		/// </summary>
		public static bool HasLanguageChanged() 
		{
			// Added to prevent Travel news page details/view being reset
			// if language is changed while on this page
			
			TravelNewsState travelNewsState = CurrentTravelNewsState;
			if (travelNewsState != null)
			{
				string previousLanguage = travelNewsState.PageLanguage;
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
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Sends back the current page language as a string
		/// </summary>
		private static string GetPageLanguage()
		{
			// Obtain page language using the CurrentUICulture setting	
			string currentLanguage = Thread.CurrentThread.CurrentUICulture.ToString();

			// If the channel is null, assume not using Content Management Server
			if (TDPage.SessionChannelName !=  null )
			{	
				// get ISO language code for this channel
				currentLanguage	= TDPage.GetChannelLanguage(TDPage.SessionChannelName.ToString());
			}
			return currentLanguage;
		}

		/// <summary>
		/// Saves the current page language to the InputPageState
		/// </summary>
		public static void SavePageLanguageToSession()
		{
			string currentLanguage = GetPageLanguage();

			// Update session variable to the current language
			TravelNewsState travelNewsState = CurrentTravelNewsState;
			travelNewsState.PageLanguage = currentLanguage;
		}


        /// <summary>
        /// Gets the property from permanant portal properties table
        /// to show/hide home page travel news headline popup
        /// </summary>
        /// <returns></returns>
        public static bool ShowTravelNewsPopup()
        {
            string showPopup = Properties.Current[KeyNewsHoverPopup].ToLower().Trim();

            if (showPopup == "true")
                return true;

            return false;

        }

        /// <summary>
        /// Static read-only property indicating if Travel News is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool TravelNewsAvailable
        {
            get
            {
                string property = Properties.Current["TravelNewsAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Departure Boards is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool DepartureBoardsAvailable
        {
            get
            {
                string property = Properties.Current["DepartureBoardsAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }
	}
}
