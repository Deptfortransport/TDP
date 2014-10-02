// *********************************************** 
// NAME                 : FindPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 12/07/2004 
// DESCRIPTION  : Base class for all Find page states.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindPageState.cs-arc  $ 
//
//   Rev 1.6   Feb 11 2010 08:53:16   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Sep 21 2009 14:56:44   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Oct 14 2008 15:20:16   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jun 18 2008 16:05:26   dgath
//Added property ITPJourney to fix ITP issues
//Resolution for 5025: ITP: Workstream
//
//   Rev 1.2.1.0   Jun 18 2008 14:32:02   mmodi
//Updated for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 10 2008 15:27:04   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:48:26   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Amended to include new read/write property to record a 
//JourneyPlanning.CJPInterface.ModeType value
//
//   Rev 1.21   Jun 06 2007 12:12:50   sangle
//Added Footnote to journey results page for all journeys except returns and ParkAndRide.
//
//   Rev 1.20   Nov 14 2006 09:46:28   rbroddle
//Merge for stream4220
//
//   Rev 1.19.1.0   Nov 07 2006 11:23:42   tmollart
//Updated for Rail Search By Price.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.19   Oct 06 2006 13:32:14   mturner
//Merge for stream SCR-4143
//
//   Rev 1.18.1.0   Aug 07 2006 10:50:24   esevern
//added carpark find a mode
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.18   May 03 2006 10:16:40   mtillett
//Prevent reinstatement of data for fixed park and ride destination
//Resolution for 4039: DN058 Park and Ride Phase 2 - Find a car route has P&R sites drop-down box in To field
//
//   Rev 1.17   Apr 05 2006 15:42:42   build
//Automatically merged from branch for stream0030
//
//   Rev 1.16.1.0   Mar 21 2006 12:04:06   esevern
//added Bus mode to FindAMode enumerations
//Resolution for 29: DEL8.1 Workstream - Find a Bus (NO LONGER USED)
//
//   Rev 1.16   Feb 11 2005 14:54:38   tmollart
//Changed createInstance method so that correct page state objects are created for fare/trunk cost based journeys.
//
//   Rev 1.15   Jan 31 2005 16:59:38   tmollart
//Changed savejourneyparameters methods to use TDJourneyParams instead of TDJourneyParamsMulti.
//
//   Rev 1.14   Jan 31 2005 13:48:18   tmollart
//Added additional case to CreateInstance method for FindAFare (Del 7) functionality.
//
//   Rev 1.13   Dec 22 2004 15:31:10   tmollart
//Updated FindAMode enumeration.
//
//   Rev 1.12   Nov 03 2004 12:54:46   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.11   Nov 01 2004 15:30:24   passuied
//overloaded CreateInstance to instanciate new FindTrunkInput in StationMode
//
//   Rev 1.10   Sep 08 2004 15:42:16   jmorrissey
//IR1417 - added amend mode, used by FindCarInput
//
//   Rev 1.9   Sep 06 2004 19:04:52   jgeorge
//Added PrepareForAmendJourney method
//Resolution for 1255: Impossible to go back to change flight details
//
//   Rev 1.8   Aug 19 2004 12:54:10   COwczarek
//Remove SetStationMode method. CreateInstance method now
//creates instance of FindStationAirportPageState for a supplied
//mode of Station to indicate Find A mode correctly.
//Resolution for 1345: Clicking Find A tab should display page for current Find A mode
//
//   Rev 1.7   Aug 03 2004 11:51:14   COwczarek
//Remove unset enumerator from FindAMode. Make FindPageState abstract.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.6   Aug 02 2004 15:41:32   passuied
//added extra 'car' case for creating instance of FindPageState
//
//   Rev 1.5   Jul 29 2004 17:20:04   passuied
//addition of FindCarPageState and changes to avoid duplication of code
//
//   Rev 1.4   Jul 22 2004 13:49:30   passuied
//Added Car in FindAMode enum
//
//   Rev 1.3   Jul 19 2004 15:25:46   jgeorge
//Del 6.1 updates
//
//   Rev 1.2   Jul 14 2004 16:37:02   passuied
//Changes for del6.1. FindFlight functionality working after SessionManager changes.
//
//   Rev 1.1   Jul 14 2004 13:01:28   passuied
//Changes in SessionManager for Del6.1. Compiles
//
//   Rev 1.0   Jul 12 2004 14:12:00   passuied
//Initial Revision


using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.SessionManager
{

	/// <summary>
	/// Enumeration defining the different modes
	/// of the FindA functionality
	/// </summary>
	public enum FindAMode
	{
		None,
		Flight,
		Station,
		Train,
		Coach,
		Fare,
		TrunkCostBased,
		Trunk,
		TrunkStation,
		Car,
		Bus,
		CarPark,
		RailCost,
		ParkAndRide,
        Cycle,
        EnvironmentalBenefits,
        International
	}
	/// <summary>
	/// Base class for all Find page states
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public abstract class FindPageState
	{
		#region Declaration

		protected bool bAmbiguityMode = false;
		protected bool amendMode = false;
		protected bool bTravelDetailsVisible = false;
		protected bool boolCalendarIsForOutward = true;

		protected FindAMode findMode = FindAMode.None; 

		protected FindJourneySummaryData storedSummaryDataOutward = null;
		protected FindJourneySummaryData storedSummaryDataReturn = null;

		protected LocationSearch originLocationSearch;
		protected TDLocation originLocation;
		protected LocationSelectControlType originType;

		protected LocationSearch destinationLocationSearch;
		protected TDLocation destinationLocation;
		protected LocationSelectControlType destinationType;

		protected string outwardHour;
		protected string outwardDayOfMonth;
		protected string outwardMinute;
		protected string outwardMonthYear;

		protected string returnHour;
		protected string returnDayOfMonth;
		protected string returnMinute;
		protected string returnMonthYear;

        protected ModeType[] modeType;
        protected bool itpJourney = false;

		#endregion
        
		protected FindPageState()
		{
		}

		/// <summary>
		/// The stored summary info for the outward journey
		/// </summary>
		public FindJourneySummaryData StoredSummaryDataOutward
		{
			get { return storedSummaryDataOutward; }
			set { storedSummaryDataOutward = value; }
		}

		/// <summary>
		/// The stored summary info for the return journey
		/// </summary>
		public FindJourneySummaryData StoredSummaryDataReturn
		{
			get { return storedSummaryDataReturn; }
			set { storedSummaryDataReturn = value; }
		}

		/// <summary>
		/// Read/Write property. Mode in which the FindA functionality is accessed
		/// </summary>
		public FindAMode Mode
		{
			get
			{
				return findMode;
			}
		}

		/// <summary>
		/// Read/Write property indicating if in Ambiguity mode
		/// </summary>
		public bool AmbiguityMode
		{
			get
			{
				return bAmbiguityMode;
			}
			set
			{
				bAmbiguityMode = value;
			}
		}

		/// <summary>
		/// Read/Write property indicating if in Amend mode
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return amendMode;
			}
			set
			{
				amendMode = value;
			}
		}

        /// <summary>
        /// Read/Write property indicating the modes of transport required
        /// </summary>
        public ModeType[] ModeType
        {
            get
            {
                return modeType;
            }
            set
            {
                modeType = value;
            }
        }


        /// <summary>
        ///  Read/Write property indicating if the journey result is a mixed mode ITP Journey.
        ///</summary>
        public bool ITPJourney
        {
            get
            {
                return itpJourney;
            }
            set
            {
                itpJourney = value;
            }
        }

		/// <summary>
		/// Read/Write property indicating if Travel Details are visible
		/// on page
		/// </summary>
		public bool TravelDetailsVisible
		{
			get
			{
				return bTravelDetailsVisible;
			}
			set
			{
				bTravelDetailsVisible = value;
			}
		}

		/// <summary>
		/// Used to identify which date the calendar is being used for
		/// </summary>
		public bool CalendarIsForOutward
		{
			get
			{
				return boolCalendarIsForOutward;
			}
			set
			{
				boolCalendarIsForOutward = value;
			}
		}

		/// <summary>
		/// Method initialising PageState components
		/// </summary>
		public virtual void Initialise()
		{
		}

		/// <summary>
		/// Create a FindPageState of type corresponding to the given mode
		/// </summary>
		/// <param name="mode">given mode</param>
		/// <returns>FindPageState object</returns>
		static public FindPageState CreateInstance(FindAMode mode)
		{
			switch(mode)
			{
				case FindAMode.Flight:
					return new FindFlightPageState();
				case FindAMode.Coach :
					return new FindCoachPageState();
				case FindAMode.Train :
					return new FindTrainPageState();
				case FindAMode.Trunk :
					return new FindTrunkPageState();
				case FindAMode.TrunkStation :
					return new FindTrunkStationPageState();
				case FindAMode.ParkAndRide :
					return new FindParkAndRidePageState();
				case FindAMode.Car :
					return new FindCarPageState();
				case FindAMode.Station :
					return new FindStationAirportPageState();
				case FindAMode.Fare :
					return new FindFarePageState();
				case FindAMode.TrunkCostBased :
					return new FindTrunkCostBasedPageState();
				case FindAMode.Bus :
					return new FindBusPageState();
				case FindAMode.CarPark :
					return new FindCarParkPageState();
				case FindAMode.RailCost :
					return new RailCostPageState();
                case FindAMode.Cycle:
                    return new FindCyclePageState();
                case FindAMode.EnvironmentalBenefits:
                    return new FindEBCPageState();
                case FindAMode.International:
                    return new FindInternationalPageState();
				default :
					return null;
			}
		}


		/// <summary>
		/// Sets the journey parameters currently associated with the session to be those
		/// stored by this object, saved by previously calling SaveJourneyParameters()
		/// </summary>
		public virtual void ReinstateJourneyParameters(TDJourneyParameters journeyParameters) 
		{
			journeyParameters.Origin = originLocationSearch;
			journeyParameters.OriginLocation = originLocation;
			journeyParameters.OriginType = originType;

			//only Reinstate Journey Parameters for destination if not park and ride location select by drive to
			if (journeyParameters.Destination.SearchType != SearchType.ParkAndRide || !journeyParameters.Destination.LocationFixed)
			{
				journeyParameters.Destination = destinationLocationSearch;
				journeyParameters.DestinationLocation = destinationLocation;
				journeyParameters.DestinationType = destinationType;
			}

			journeyParameters.OutwardHour = outwardHour;
			journeyParameters.OutwardDayOfMonth = outwardDayOfMonth;
			journeyParameters.OutwardMinute = outwardMinute;
			journeyParameters.OutwardMonthYear = outwardMonthYear;

			journeyParameters.ReturnHour = returnHour;
			journeyParameters.ReturnDayOfMonth = returnDayOfMonth;
			journeyParameters.ReturnMinute = returnMinute;
			journeyParameters.ReturnMonthYear = returnMonthYear;
		}

		/// <summary>
		/// Stores (references) of the journey parameters currently associated with the
		/// session so that they may be reinstated when switching from ambiguity mode
		/// back to input mode
		/// </summary>
		public virtual void SaveJourneyParameters(TDJourneyParameters journeyParameters) 
		{
			originLocationSearch = journeyParameters.Origin;
			originLocation = journeyParameters.OriginLocation;
			originType = journeyParameters.OriginType;

			destinationLocationSearch = journeyParameters.Destination;
			destinationLocation = journeyParameters.DestinationLocation;
			destinationType = journeyParameters.DestinationType;

			outwardHour = journeyParameters.OutwardHour;
			outwardDayOfMonth = journeyParameters.OutwardDayOfMonth;
			outwardMinute = journeyParameters.OutwardMinute;
			outwardMonthYear = journeyParameters.OutwardMonthYear;

			returnHour = journeyParameters.ReturnHour;
			returnDayOfMonth = journeyParameters.ReturnDayOfMonth;
			returnMinute = journeyParameters.ReturnMinute;
			returnMonthYear = journeyParameters.ReturnMonthYear;
		}

		/// <summary>
		/// Prepares the object for journey amendments. This can be called when the user
		/// clicks "Amend Journey" from the results page.
		/// </summary>
		public virtual void PrepareForAmendJourney()
		{
			AmbiguityMode = false;
			
			//IR1417 - set amend mode, used by FindCarInput
			AmendMode = true;
		}

	}
}
