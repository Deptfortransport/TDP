// ***********************************************
// NAME 		: TDJourneyParameters.cs
// AUTHOR 		: Callum
// DATE CREATED : 19/09/2003
// DESCRIPTION 	: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDJourneyParameters.cs-arc  $
//
//   Rev 1.2   Sep 01 2011 10:43:52   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Feb 02 2009 16:59:24   mmodi
//Added Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:48:38   mturner
//Initial revision.
//
//   Rev 1.42   May 03 2007 14:29:24   Pscott
//IR 4382
//vantive 4528668 - default dates late at night fix
//
//   Rev 1.41   Jun 02 2006 15:03:44   rphilpott
//Initialise PublicAlgorithm to Default, not Fastest.
//Resolution for 4104: Wrong PublicAlgorithm setting from Find Nearest
//
//   Rev 1.40   Nov 01 2005 15:12:14   build
//Automatically merged from branch for stream2638
//
//   Rev 1.39.1.0   Sep 21 2005 10:57:30   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.39   Sep 06 2005 13:05:50   jgeorge
//Removed incorrect code.
//
//   Rev 1.38   Aug 19 2005 14:06:50   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.37.1.1   Aug 09 2005 19:53:22   asinclair
//Removed commented out code
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.37.1.0   Jul 27 2005 18:11:40   asinclair
//Check in to fix build errors.  Work in progress
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.37   Nov 25 2004 11:19:18   jgeorge
//Moved DirectFlightsOnly property into base class
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.36   Oct 05 2004 14:26:44   jgeorge
//Corrected InitialiseDefaultOutwardTime() method. 
//Resolution for 1681: Default Journey Time incorrect when planning after midnight
//
//   Rev 1.35   Sep 21 2004 17:26:36   jbroome
//IR 1596 - Added ExtendEndOfItinerary property.
//
//   Rev 1.34   Sep 13 2004 16:15:42   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.33   Jul 28 2004 16:12:54   passuied
//Extracted the initialisation of the default outward time to a public method so it is accessible outside.
//
//   Rev 1.32   Jul 14 2004 14:47:02   RPhilpott
//Move "any time" parameters from flight to base class to make them accessible to non-flight traunk journeys.
//
//   Rev 1.31   Jul 08 2004 15:41:20   jgeorge
//Actioned review comments
//
//   Rev 1.30   Jun 28 2004 20:49:12   JHaydock
//JourneyPlannerInput clear page and back buttons for extend journey
//
//   Rev 1.29   Jun 25 2004 12:25:10   RPhilpott
//Make support for operatror selection accessible to mult-modal journeys as well as flights.
//
//   Rev 1.28   Jun 15 2004 15:47:00   RPhilpott
//Move walking speed, etc, from ...Multi to base class so that we can use the default values more easily for ...Flight.
//
//   Rev 1.27   Jun 04 2004 09:58:24   RPhilpott
//Use DataServices to get default location search types instead of rndom hard-coding. 
//
//   Rev 1.26   Jun 04 2004 09:45:54   jgeorge
//Made location properties virtual to allow overriding in TDJourneyParametersFlight
//
//   Rev 1.25   May 26 2004 08:58:54   jgeorge
//Changed into abstract class as part of Find a... Del 6 work. New classes TDJourneyParametersMulti and TDJourneyParametersFlight inherit from this and are used for the multi modal planner and Find a Flight respectively.
//
//   Rev 1.24   May 10 2004 12:12:36   jbroome
//IR840 and IR636
//
//   Rev 1.23   Apr 15 2004 09:20:40   AWindley
//DEL5.2 QA: Resolution for 767: Leave on default time should be 7am after midnight
//
//   Rev 1.22   Mar 31 2004 16:10:34   jgeorge
//Updated to use IDataService interface to facilitate testing
//
//   Rev 1.21   Mar 10 2004 09:42:38   COwczarek
//Remove redundant attributes/properties
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.20   Jan 02 2004 10:20:24   passuied
//fix error in initialisation. DateTime MonthYear in format mm/yyyy
//
//   Rev 1.19   Dec 16 2003 09:52:42   PNorell
//Corrected initialisation code.
//
//   Rev 1.18   Dec 13 2003 14:06:26   passuied
//added properties for controltype
//
//   Rev 1.17   Nov 24 2003 14:05:22   passuied
//Fix for IR313 : set default value for ReturnMonthYear
//Resolution for 313: Date error on Amend journey function
//
//   Rev 1.16   Nov 17 2003 12:43:56   passuied
//1. if time is less than 15 minutes before the evening time, set time to next morning
//2. if minute is not a divisor of 5, set minute to next divisor of 5 (upper boundary)
//Resolution for 209: Default time incorrect
//
//   Rev 1.15   Oct 24 2003 11:42:48   passuied
//added properties
//
//   Rev 1.14   Oct 20 2003 16:23:04   passuied
//added boolean to indicates if user wants to save details
//
//   Rev 1.13   Oct 17 2003 14:52:32   passuied
//Update with new properties for favourite journey storage
//
//   Rev 1.12   Oct 15 2003 13:15:42   passuied
//added searchtype default value for locationSearch in Initialisation
//
//   Rev 1.11   Oct 03 2003 13:38:56   PNorell
//Updated the new exception identifier.
//
//   Rev 1.10   Oct 03 2003 11:25:40   passuied
//corrected problem with days from 1 to 9
//
//   Rev 1.9   Oct 02 2003 17:48:00   COwczarek
//id parameter passed in TDException constructor set to -1 to enable compilation after introduction of new TDException constructor which takes an enum type for id. This is a temporary fix - the constructor taking an
//id of type long will be removed.
//
//   Rev 1.8   Sep 24 2003 15:04:58   passuied
//added Indexes for ambiguity locations
//
//   Rev 1.7   Sep 19 2003 21:21:16   passuied
//working version up to date
//
//   Rev 1.6   Sep 18 2003 14:48:44   kcheung
//Marked as serializable as it was causing errors during load
//
//   Rev 1.5   Sep 18 2003 14:37:50   passuied
//stupid spelling mistake
//
//   Rev 1.4   Sep 18 2003 13:30:30   kcheung
//corrected spelling error
//
//   Rev 1.3   Sep 18 2003 12:05:54   passuied
//Changed to follow design + Initialisation
//
//   Rev 1.2   Sep 17 2003 16:46:16   PNorell
//Moved to session manager.
//Added header.

using System;
//using System.Web.SessionState;
//
//using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using System.Globalization;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using System.Text;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDJourneyParameters.
	/// </summary>
	///
	[CLSCompliant(false)]
	[Serializable()]
	public abstract class TDJourneyParameters
    {
        #region Public variables
        public enum ControlType
		{
			Default,
			NoMatch,
			NewLocation
		}

		[Serializable()]
	    public class LocationSelectControlType
		{
			private ControlType myType;

			public LocationSelectControlType(ControlType type)
			{
				myType = type;
			}
			/// <summary>
			/// Get/Set property.
			/// </summary>
			public ControlType Type
			{
				get
				{
					return myType;
				}
				set
				{
					myType = value;
				}
			}
        }
        #endregion

        #region Private variables
        private bool defaultValues;

        private bool isOutwardRequired = true;
		private bool isReturnRequired;
        
		private bool outwardArriveBefore;
		private bool returnArriveBefore;

		private string outwardDayOfMonth;
		private string outwardMonthYear;
		private string outwardHour; 
		private string outwardMinute;
		private string returnDayOfMonth;
		private string returnMonthYear;
		private string returnHour; 
		private string returnMinute;

		private bool outwardAnyTime;
		private bool returnAnyTime;

		private int walkingSpeed;
		private int maxWalkingTime;
		private int interchangeSpeed;

		private TDLocation originLocation;
		private TDLocation destinationLocation;
		private TDLocation returnOriginLocation;
		private TDLocation returnDestinationLocation;

		private bool onlyUseSpecifiedOperators;
		private string[] selectedOperators;

		private LocationSearch origin;
		private LocationSearch destination;

		// LocationSelectControl control type
		private LocationSelectControlType typeOrigin;
		private LocationSelectControlType typeDestination;

		// storage of favourite strings
		private string stringSavedJourneyOldName = string.Empty;
		private string stringSavedJourneyNewName = string.Empty;
		private string stringFavouritesIdOverwritten = string.Empty;

		private bool directFlightsOnly;

		// Mode storage
		protected ModeType[] publicModes;
		protected PublicAlgorithmType publicAlgorithmType;

		private bool boolSaveDetails = false;

		// Extend journey - Find transport to start / from end 
		private bool extendEndOfItinerary = true;

        // Routing guide
        private bool routingGuideInfluenced;
        private bool routingGuideCompliantJourneysOnly;
        private string routeCodes;
        #endregion

        #region Public properties
        public bool DefaultValues
		{
			get { return defaultValues; }
			set { defaultValues = value; }
		}

        /// <summary>
        /// Read/Write. Determines if the outward journey is required.
        /// </summary>
        public bool IsOutwardRequired
        {
            get { return isOutwardRequired; }
            set { isOutwardRequired = value; }
        }

		public bool IsReturnRequired
		{
			get { return isReturnRequired; }
			set { isReturnRequired = value; }
		}

		public bool OutwardArriveBefore
		{
			get { return outwardArriveBefore; }
			set { outwardArriveBefore = value; }
		}

		public bool ReturnArriveBefore
		{
			get { return returnArriveBefore; }
			set { returnArriveBefore = value; }
		}

		public string OutwardDayOfMonth
		{
			get { return outwardDayOfMonth; }
			set { outwardDayOfMonth = value; }
		}

		public string OutwardMonthYear
		{
			get { return outwardMonthYear; }
			set { outwardMonthYear = value; }
		}

		public string ReturnDayOfMonth
		{
			get { return returnDayOfMonth; }
			set { returnDayOfMonth = value; }
		}

		public string ReturnMonthYear
		{
			get { return returnMonthYear; }
			set { returnMonthYear = value; }
		}

		public bool OutwardAnyTime
		{
			get { return outwardAnyTime; }
			set { outwardAnyTime = value; }
		}

		public bool ReturnAnyTime
		{
			get { return returnAnyTime; }
			set { returnAnyTime = value; }
		}

		public int WalkingSpeed
		{
			get { return walkingSpeed; }
			set { walkingSpeed = value; }
		}

		public int MaxWalkingTime
		{
			get { return maxWalkingTime; }
			set { maxWalkingTime = value; }
		}

		public int InterchangeSpeed
		{
			get { return interchangeSpeed; }
			set { interchangeSpeed = value; }
		}

		public virtual TDLocation OriginLocation
		{
			get { return originLocation; }
			set { originLocation = value; }
		}

		public virtual TDLocation DestinationLocation
		{
			get { return destinationLocation; }
			set { destinationLocation = value; }
		}

		public virtual TDLocation ReturnOriginLocation
		{
			get { return returnOriginLocation; }
			set { returnOriginLocation = value; }
		}

		public virtual TDLocation ReturnDestinationLocation
		{
			get { return returnDestinationLocation; }
			set { returnDestinationLocation = value; }
		}


		/// <summary>
		/// Returns string containing details of journey
		/// </summary>
		public abstract string InputSummary();


		/// <summary>
		/// If true, the SelectedOperators will be treated as ones to use. Otherwise, they
		/// will be treated as ones to avoid.
		/// <seealso cref="SelectedOperators"/>
		/// </summary>
		public bool OnlyUseSpecifiedOperators
		{
			get { return onlyUseSpecifiedOperators; }
			set { onlyUseSpecifiedOperators = value; }
		}

		/// <summary>
		/// The list of selected operators. Used in conjunction with OnlyUseSpecifiedOperators
		/// <seealso cref="OnlyUseSpecifiedOperators"/>
		/// </summary>
		public string[] SelectedOperators
		{
			get { return selectedOperators; }
			set { selectedOperators = value; }
		}

		public LocationSearch Origin
		{
			get { return origin; }
			set { origin = value; }
		}

		public LocationSearch Destination
		{
			get { return destination; }
			set { destination = value; }
		}

		/// <summary>
		/// Get/Set property. Control type for origin control
		/// </summary>
		public LocationSelectControlType OriginType
		{
			get { return typeOrigin; }
			set { typeOrigin = value; }
		}

		/// <summary>
		/// Get/Set property. Control type for destination control 
		/// </summary>
		public LocationSelectControlType DestinationType
		{
			get { return typeDestination; }
			set { typeDestination = value; }
		}

		public string OutwardHour
		{
			get{ return outwardHour; }
			set{ outwardHour = value;}
		}

		public string OutwardMinute
		{
			get{ return outwardMinute;}
			set{ outwardMinute = value;}
		}
		public string ReturnHour
		{
			get{ return returnHour; }
			set{ returnHour = value;}
		}

		public string ReturnMinute
		{
			get{ return returnMinute;}
			set{ returnMinute = value;}
		}


		/// <summary>
		/// Get/Set property. Indicates if the user ticked the save details checkbox
		/// </summary>
		public bool SaveDetails
		{
			get { return boolSaveDetails; }
			set { boolSaveDetails = value; }
		}

		/// <summary>
		/// Get/Set property. Indicates if the user is extending the journey from the 
		/// end location (or to the start location)
		/// </summary>
		public bool ExtendEndOfItinerary
		{
			get { return extendEndOfItinerary; }
			set { extendEndOfItinerary = value; }
		}

		// Properties for Favourite journeys
		/// <summary>
		/// Get/Set property for saved journey old name if max is reached
		/// </summary>
		public string SavedJourneyOldName
		{
			get { return stringSavedJourneyOldName; }
			set { stringSavedJourneyOldName = value; }
		}

		/// <summary>
		/// Get/Set property for saved journey New name.
		/// </summary>
		public string SavedJourneyNewName
		{
			get { return stringSavedJourneyNewName; }
			set { stringSavedJourneyNewName = value; }
		}

		/// <summary>
		/// Get/Set property. UserId of the journey to be overwriten.
		/// </summary>
		public string FavouritesIdOverwritten
		{
			get { return stringFavouritesIdOverwritten; }
			set { stringFavouritesIdOverwritten = value; }
		}

		/// <summary>
		/// True indicates that only direct flights are required
		/// </summary>
		public bool DirectFlightsOnly
		{
			get { return directFlightsOnly; }
			set { directFlightsOnly = value; }

        }

		/// <summary>
		/// Array of public transport modes.
		/// </summary>
		public ModeType[] PublicModes
		{
			get { return publicModes; }
			set { publicModes = value; }
		}

		/// <summary>
		/// Public algorithm type. Tells the CJP which algortithm the CJP
		/// should use for public transport journeys. E.g. No changes,
		/// limited changes etc.
		/// </summary>
		public PublicAlgorithmType PublicAlgorithmType
		{
			get { return publicAlgorithmType; }
			set { publicAlgorithmType = value; }
        }

        #region Routing guide

        /// <summary>
        /// Read/write. If this is true, then the journey request will attempt to return journeys 
        /// that comply with the routing guide
        /// </summary>
        public bool RoutingGuideInfluenced
        {
            get { return routingGuideInfluenced; }
            set { routingGuideInfluenced = value; }
        }

        /// <summary>
        /// Read/write. If this is true, then the journeys returned are routing guide compliant.
        /// </summary>
        public bool RoutingGuideCompliantJourneysOnly
        {
            get { return routingGuideCompliantJourneysOnly; }
            set { routingGuideCompliantJourneysOnly = value; }
        }

        /// <summary>
        /// Read/write. Used to specify a route code to be used to restrict journeys.  
        /// Route code can be obtained from a fare code.
        /// </summary>
        public string RouteCodes
        {
            get { return routeCodes; }
            set { routeCodes = value; }
        }
        #endregion

        #endregion

        #region Public methods
        public virtual void Initialise()
		{
			defaultValues = true;

            isOutwardRequired = true;
			isReturnRequired = false;
			outwardArriveBefore = false;
			returnArriveBefore = false;

			directFlightsOnly = true;

			TDDateTime leaveDateTime = InitialiseDefaultOutwardTime();

			OutwardDayOfMonth = leaveDateTime.ToString("dd");
			OutwardMonthYear = leaveDateTime.ToString("MM")
				+ "/"
				+ leaveDateTime.ToString("yyyy");

			// ReturnDate Time set to No Return
			returnHour = string.Empty;
			returnMinute = string.Empty;
			returnDayOfMonth = string.Empty;
			returnMonthYear = "NoReturn";

			originLocation = new TDLocation();
			destinationLocation = new TDLocation();
			returnOriginLocation = null;
			returnDestinationLocation = null;

			onlyUseSpecifiedOperators = false;
			selectedOperators = new string[0];

			// LocationSearch set to empty locationSearch objects
			origin = new LocationSearch();
			origin.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);
			destination = new LocationSearch();
			destination.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

			OriginType = new LocationSelectControlType( ControlType.NoMatch);
			DestinationType = new LocationSelectControlType( ControlType.NoMatch);

			stringSavedJourneyNewName = string.Empty;
			stringSavedJourneyOldName = string.Empty;

			DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			publicAlgorithmType = PublicAlgorithmType.Default;

			walkingSpeed = Convert.ToInt32( populator.GetDefaultListControlValue( DataServiceType.WalkingSpeedDrop ), CultureInfo.CurrentCulture );
			maxWalkingTime = Convert.ToInt32( populator.GetDefaultListControlValue( DataServiceType.WalkingMaxTimeDrop ), CultureInfo.CurrentCulture );
			interchangeSpeed = Convert.ToInt32( populator.GetDefaultListControlValue( DataServiceType.ChangesSpeedDrop ), CultureInfo.CurrentCulture );

            // Routing guide
            routingGuideInfluenced = false;
            routingGuideCompliantJourneysOnly = false;
            routeCodes = string.Empty;
		}

		/// <summary>
		/// Initialises the default Outward Time and returns the TDDateTime object used for 
		/// this initialisation
		/// </summary>
		/// <returns>TDDateTime to use for further initialisation</returns>
		public TDDateTime InitialiseDefaultOutwardTime()
		{
			// Outward Date : Same day (or next day if time is in evening)
			// Outward Time : +15 minutes or next day if in the evening for multimodal, any time if Find a Flight
			
			// Get evening and morning properties
			string evening = Properties.Current["journeyparameters.eveningtime"];
			string morning = Properties.Current["journeyparameters.morningtime"];
			string sMinutesToAdd = Properties.Current["journeyparameters.minutestoadd"];

			int eveningHour = 0;
			int eveningMinute = 0;
			int morningHour = 0;
			int morningMinute = 0;
			double minutesToAdd = 0;

			try
			{
				string[] eveningTime = evening.Split(':');
				string[] morningTime = morning.Split(':');

				eveningHour = Convert.ToInt32(eveningTime[0], CultureInfo.CurrentCulture.NumberFormat);
				eveningMinute = Convert.ToInt32(eveningTime[1], CultureInfo.CurrentCulture.NumberFormat);
				morningHour = Convert.ToInt32(morningTime[0], CultureInfo.CurrentCulture.NumberFormat);
				morningMinute = Convert.ToInt32(morningTime[1], CultureInfo.CurrentCulture.NumberFormat);
				minutesToAdd = Convert.ToDouble(sMinutesToAdd, CultureInfo.CurrentCulture.NumberFormat);
			}
			catch (Exception)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					"Bad format/Unset Journey parameters 'eveningTime' or 'morningTime' properties");

				Logger.Write(oe);

				throw new TDException(
					"Bad format/Unset Journey parameters 'eveningtime', 'morningtime' or 'minutestoadd properties",
					true,
					TDExceptionIdentifier.SMUnsetParameters);
			}

			// current time should be 15 minutes ahead (or whatever minutesToAdd property is set to)
			// so if current time +15 minutes is after evening time, time is set to next morning
			TDDateTime timeToCompare = TDDateTime.Now.AddMinutes(minutesToAdd);
			int nowHour = timeToCompare.Hour;
			int nowMinute = timeToCompare.Minute;

			TDDateTime leaveDateTime = TDDateTime.Now;
			// If now is between evening time and midnight set time to morning time the next day 
			if (((nowHour*100+nowMinute) >= (eveningHour*100+eveningMinute)) && 
				((nowHour*100+nowMinute) <= 2359))
			{
				leaveDateTime = leaveDateTime.AddDays(1);
				outwardHour = morningHour.ToString(CultureInfo.CurrentCulture.NumberFormat);
				outwardMinute = morningMinute.ToString(CultureInfo.CurrentCulture.NumberFormat);
			}
				// If now is between midnight and morning time, set time to morning time
			else if ((nowHour*100+nowMinute) <= (morningHour*100+morningMinute))
			{
				if(leaveDateTime < timeToCompare)
				{
					leaveDateTime = timeToCompare;
				}
				outwardHour = morningHour.ToString(CultureInfo.CurrentCulture.NumberFormat);
				outwardMinute = morningMinute.ToString(CultureInfo.CurrentCulture.NumberFormat);
			}
			else
			{
				// else take current day + 15 minutes (from properties)
				
				// if minute is not divisible by 5, adjust it to the next divisor of 5 
				//(because minutes are graduated by units of 5)
				if (leaveDateTime.Minute%5 != 0)
					leaveDateTime = leaveDateTime.AddMinutes(-leaveDateTime.Minute%5 + 5);

				// then add the minutes to add!
				leaveDateTime = leaveDateTime.AddMinutes(minutesToAdd);

				outwardHour = leaveDateTime.Hour.ToString(CultureInfo.CurrentCulture.NumberFormat);

				outwardMinute = leaveDateTime.Minute.ToString(CultureInfo.CurrentCulture.NumberFormat);

			}

			return leaveDateTime;


		}
		/// <summary>
		/// Initialisation for outward or return extend journey information only
		/// </summary>
		/// <param name="originReset">Whether to initialise origin or destination information (Extend "to start" or "from end")</param>
		public virtual void Initialise(bool originReset)
		{
			//Specific reset
			if (originReset)
			{
				originLocation = new TDLocation();

				// LocationSearch set to empty locationSearch objects
				origin = new LocationSearch();
				origin.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

				OriginType = new LocationSelectControlType(ControlType.NoMatch);
			}
			else
			{
				destinationLocation = new TDLocation();

				// LocationSearch set to empty locationSearch objects
				destination = new LocationSearch();
				destination.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

				DestinationType = new LocationSelectControlType(ControlType.NoMatch);
			}

			//Generic reset
			selectedOperators = new string[0];

			DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			walkingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop), CultureInfo.CurrentCulture);
			maxWalkingTime = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop), CultureInfo.CurrentCulture);
			interchangeSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop), CultureInfo.CurrentCulture);
        }

        #endregion

        #region Constructor
        protected TDJourneyParameters()
		{
			Initialise();
        }
        #endregion

        #region Protected methdos
        protected SearchType GetDefaultSearchType(DataServiceType listType)
		{
			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultItemValue = ds.GetDefaultListControlValue(listType);
			return (SearchType) (Enum.Parse(typeof(SearchType), defaultItemValue));
		}
		/// <summary>
		/// Returns the outward date/time as a string formatted 
		/// as dd/mm/yyyy hh:mm
		/// </summary>
		protected string GetFormattedOutwardDateTime()
		{
			StringBuilder formattedDate = new StringBuilder();
			formattedDate.Append(outwardDayOfMonth);
			formattedDate.Append("/");
			formattedDate.Append(outwardMonthYear);
			formattedDate.Append(" ");
			formattedDate.Append(outwardHour);
			formattedDate.Append(":");
			formattedDate.Append(outwardMinute);
			return formattedDate.ToString();
		}
	
		/// <summary>
		/// Returns the return date/time as a string formatted 
		/// as dd/mm/yyyy hh:mm
		/// </summary>
		protected string GetFormattedReturnDateTime()
		{
			StringBuilder formattedDate = new StringBuilder();
			formattedDate.Append(returnDayOfMonth);
			formattedDate.Append("/");
			formattedDate.Append(returnMonthYear);
			formattedDate.Append(" ");
			formattedDate.Append(returnHour);
			formattedDate.Append(":");
			formattedDate.Append(returnMinute);
			return formattedDate.ToString();
		}
	

		/// <summary>
		/// Returns a formatted string containing the public transport
		/// options. String only contains options that have changed from
		/// the default values.
		/// </summary>
		protected string GetFormattedPTOptions()
		{
			//Only do anythung if there are public transport modes specified.
			if(publicModes.Length >0)
			{
				//Create dataservices object and string builder that will be used to
				//create the formatted string.
				DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				StringBuilder optionsString = new StringBuilder();

				//Walking speed.
				if(!WalkingSpeed.Equals(Convert.ToInt32 (populator.GetDefaultListControlValue( DataServiceType.WalkingSpeedDrop) )))
				{
					optionsString.Append("Walking speed: " + WalkingSpeed.ToString( ) +"\n");
					optionsString.Append("\n");
				}

				//Maximum walking time
				if(!MaxWalkingTime.Equals(Convert.ToInt32 (populator.GetDefaultListControlValue (DataServiceType.WalkingMaxTimeDrop))))
				{
					optionsString.Append("Maximum walking time: " + MaxWalkingTime.ToString() + " mins" +"\n");
					optionsString.Append("\n");
				}

				//Public transport algorithm
				if(publicAlgorithmType != PublicAlgorithmType.Default)
				{
					optionsString.Append("Number of changes: " + publicAlgorithmType.ToString() +"\n");
					optionsString.Append("\n");
				}

				//Interchange speed
				if(interchangeSpeed != (Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop))))
				{
					optionsString.Append("Change speed: " + interchangeSpeed.ToString());
					optionsString.Append("\n");
				}

				//Return formatted string.
				return optionsString.ToString();
			}
			else
			{
				//Return an empty string
				return string.Empty;
			}
        }

        #endregion
    }
}
