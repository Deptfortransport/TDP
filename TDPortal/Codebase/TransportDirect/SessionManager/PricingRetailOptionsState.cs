// *********************************************** 
// NAME			: PricingRetailOptionsState.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 09.10.03
// DESCRIPTION	: Responsible for holding and manipulating user options
// pertinent to pricing and retail functionality.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/PricingRetailOptionsState.cs-arc  $
//
//   Rev 1.1   Nov 29 2007 15:16:08   mturner
//Updated by PS post Del 9.8
//
//   Rev 1.50   Nov 14 2007 12:52:12   pscott
//4520 - Recalculate fare when mid journey adjustment
//
//   Rev 1.49   Jun 06 2006 18:06:20   rphilpott
//Changes to prevent unnecessary creation of new PricingRetailOptionsState objects when pricing extended/replanned journeys that include car sections.
//Resolution for 4053: DN068: Amend tool when used on Extend pages loses selected values
//
//   Rev 1.48   May 03 2006 11:37:24   RPhilpott
//Move PricingRetailOptionsState to partition-specific deferred storage.
//Resolution for 4005: DD075: Discount card entered in Find Cheaper retained if switch back to search by time
//Resolution for 4040: DD075: City-to-city shows return fares if change mode and causes an error
//
//   Rev 1.47   Apr 27 2006 16:34:06   RPhilpott
//Correct handling of Replan when in cost-based partition. 
//Resolution for 4012: DD075: Server error viewing costs for PT journey replaced by car
//
//   Rev 1.46   Apr 26 2006 12:17:46   RPhilpott
//Manual merge of stream0035
//Resolution for 3973: DD075: Return fares are displayed instead of singles when viewing other results
//
//   Rev 1.45   Apr 19 2006 15:44:28   RGriffith
//IR3936 &3937 - Fixes to allow fares to work with single journeys (following fixes to IR3825)
//Resolution for 3936: Error on Ticket and Costs option
//Resolution for 3937: DN068 Extend:  Server Error on Extend Tickets and costs results page
//
//   Rev 1.44   Apr 12 2006 13:16:30   RGriffith
//IR3709, IR3710, IR3825  Fixes: Changes to how matching outward and return fares are calculated
//
//   Rev 1.43   Mar 22 2006 20:27:38   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.42   Mar 14 2006 15:39:56   rgreenwood
//IR 3600 - Del 8.1 change for JourneyFares - default fares control to table view, not diagram
//
//   Rev 1.41   Mar 14 2006 08:41:40   build
//Automatically merged from branch for stream3353
//
//   Rev 1.40.1.3   Mar 13 2006 16:30:32   tmollart
//Updates from code review.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.40.1.2   Mar 10 2006 11:17:40   RGriffith
//Change to get journey reference number from session manager if not using an itinerary
//
//   Rev 1.40.1.1   Mar 06 2006 17:22:46   RGriffith
//Changes made for tickets/costs
//
//   Rev 1.40.1.0   Feb 22 2006 11:59:36   RGriffith
//Changes made for multiple asynchronous ticket/costing. Methods moved in from the Web Adapter class
//
//   Rev 1.40   Nov 15 2005 08:57:20   jgeorge
//Changed CJP Request ID to int
//Resolution for 2995: DN040: Incorrect retailer and ticket information after using Back button
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//Resolution for 3030: DN040: Switching between options on results page invokes wait page
//
//   Rev 1.39   Nov 09 2005 14:29:18   RPhilpott
//Merge for stream2818.
//
//   Rev 1.38   Nov 01 2005 15:12:16   build
//Automatically merged from branch for stream2638
//
//   Rev 1.37.1.1   Oct 29 2005 14:39:26   tmollart
//Modified casting of ItineraryManager for consistency.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.37.1.0   Sep 22 2005 09:43:52   asinclair
//Modified so that itinerary manager objects are cast to Extend Itinerary Manager
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.37   Jul 14 2005 18:56:46   RPhilpott
//TicketClass should always be "All", never "Second".
//Resolution for 2588: DEL 7 First class fares are not shown in Tickets/Costs on find a train or door-to-door
//
//   Rev 1.36   May 10 2005 14:40:04   jgeorge
//Adjusted condition in HasProcessedTicketRetailerInfoJourneyChanged method.
//Resolution for 2470: PT: Unable to select outward and return tickets for journey with multiple pricing units
//
//   Rev 1.35   May 03 2005 15:17:50   jgeorge
//Fix to ensure discount cards are taken into account on Tickets/Retailers page.
//
//   Rev 1.34   Apr 28 2005 14:13:44   jgeorge
//Alterations so that cost based journey results can be used with the itinerary class.
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.33   Apr 25 2005 11:50:42   jgeorge
//Corrections to make it work correctly with extend journey
//Resolution for 2299: Del 7 - PT- Find Cheaper not displayed for City to city with extension
//
//   Rev 1.32   Apr 11 2005 11:15:02   jgeorge
//Corrections for handling find a fare results
//Resolution for 2073: PT: Ticket Retailers page does not work correctly with Find a Fare
//
//   Rev 1.31   Mar 31 2005 16:26:04   jgeorge
//Bug fix
//
//   Rev 1.30   Mar 22 2005 17:42:40   jgeorge
//FxCop changes
//
//   Rev 1.29   Mar 21 2005 13:57:36   jgeorge
//Updated commenting and changed showOutboundFaresInTableFormat and showReturnFaresInTableFormat to non-static members.
//
//   Rev 1.28   Mar 14 2005 14:49:14   rgeraghty
//Added LeaveTicketDisplay property
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.27   Mar 04 2005 09:18:36   jgeorge
//Some modifications for ticket retailer handoff, and to allow detection of when selected tickets have changed
//
//   Rev 1.26   Feb 23 2005 16:44:34   rgeraghty
//Updated NoTicket definition and GetSelectedTicket/SetSelectedTicket methods
//
//   Rev 1.25   Feb 22 2005 17:27:42   jgeorge
//Added properties for currently selected RetailUnit
//
//   Rev 1.24   Feb 18 2005 14:46:08   rgeraghty
//Added properties for view mode of fares
//
//   Rev 1.23   Jan 13 2005 15:01:48   jgeorge
//Added methods to store ticket selection
//
//   Rev 1.22   Dec 23 2004 11:52:20   jgeorge
//Interim check in for DEL7 changes

using System;
using System.Collections;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CostSearch;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
    /// Responsible for holding and manipulating user options
    /// pertinent to pricing and retail functionality.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class PricingRetailOptionsState
	{

		#region Default values

		/// <summary>
		/// Default ticket short code for a dummy ticket
		/// </summary>
		private const string noTicketShortCode = "TD DUMMY SHORT CODE";

		/// <summary>
		/// Default values used for discounts in absence of any user preferences
		/// </summary>
		private static Discounts DefaultDiscounts = new Discounts( string.Empty, string.Empty, TicketClass.All );
        
		/// <summary>
		/// Initial value for instance variables that record which journey id was used to
		/// create the itinerary
		/// </summary>
		public const int NotProcessed = -1;

		/// <summary>
		/// Returns an 'empty' ticket
		/// </summary>
		public static Ticket NoTicket = new Ticket(noTicketShortCode, Flexibility.NoFlexibility, noTicketShortCode, 0, 0, 0, 0, 0, 0);

		#endregion

		#region Private member fields

		private Itinerary itinerary;
		private ItineraryType overrideItineraryType;
		private bool showChildFares;
		private int processedOutwardJourney;
		private int processedReturnJourney;
		private int itinerarySegment = -1;
		private Discounts discounts;

		private bool applyNewDiscounts;
		private bool itineraryViewChanged;
		private int processedCjpCallRequestID;
		private int adultPassengers;
		private int childPassengers;
		private TicketRetailerInfo outwardTicketRetailerInfo;
		private RetailUnit selectedOutwardRetailUnit;
		private TicketRetailerInfo returnTicketRetailerInfo;
		private RetailUnit selectedReturnRetailUnit;
		private Retailer lastRetailerSelection;
		private bool lastRetailerSelectionIsForReturn;
		private bool leaveTicketDisplay;
		private bool showOutboundFaresInTableFormat = true;
		private bool showReturnFaresInTableFormat = true;
		private FindFareBuyOption selectedCostBasedBuyOption;
		private bool selectedBuyOptionChangedSinceOutwardInfoSet;
		private bool selectedBuyOptionChangedSinceReturnInfoSet;

		/// <summary>
		/// state of user's authentication, true user currently logged in, false otherwise.
		/// </summary>
		private bool loggedIn;

		/// <summary>
		/// Stores the selected tickets for a journey. PricingUnit objects are used for
		/// the keys and Ticket objects for the values
		/// </summary>
		private Hashtable selectedTicketsHash;

		/// <summary>
		/// Used to store whether or not the selected tickets hash has been modified
		/// since the last time the outwardTicketRetailerInfo was set
		/// </summary>
		private bool selectedTicketsChangedSinceOutwardInfoSet;

		/// <summary>
		/// Used to store whether or not the selected tickets hash has been modified
		/// since the last time the outwardTicketRetailerInfo was set
		/// </summary>
		private bool selectedTicketsChangedSinceReturnInfoSet;

		/// <summary>
		/// Flag to help a page track if fares should be calculated again
		/// </summary>
		private bool calculateFaresOverride = false;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public PricingRetailOptionsState()
		{
			processedOutwardJourney = NotProcessed;
			processedReturnJourney = NotProcessed;
			processedCjpCallRequestID = int.MinValue;
			discounts = DefaultDiscounts;
			selectedTicketsHash = new Hashtable();
			adultPassengers = 1;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Read/Write. Discount details for fares processing
		/// </summary>
		public Discounts Discounts
		{
			get { return discounts; }
			set { discounts = value; } 
		}

		/// <summary>
		/// Read/Write. Used to determine if fares need to be recalculated because 
		/// user changed a discount parameter. True if user changed discount parameter, false otherwise.
		/// </summary>
		public bool ApplyNewDiscounts 
		{
			get { return this.applyNewDiscounts; }
			set 
			{ 
				this.applyNewDiscounts = value;
				
				// The value is being set to false because the new discounts have been
				// taken into account in a new call to JourneyItinerary.Price. This means
				// that the two ticketretailerinfo properties are now out of date.
				if ( value == false )
				{
					selectedTicketsChangedSinceOutwardInfoSet = true;
					selectedTicketsChangedSinceReturnInfoSet = true;
				}
			}
		}

		/// <summary>
		/// Read/Write. Used to determine if user has selected an option that 
		/// requires the contents of the same itinerary to be redisplayed in 
		/// a different way
		/// </summary>
		public bool ItineraryViewChanged 
		{
			get { return this.itineraryViewChanged; }
			set { this.itineraryViewChanged = value;}
		}
        
		/// <summary>
		/// Read/Write. A unique value identifying the CJP request for the current journey plan.
		/// Used to determine if user has submitted a new journey request to the CJP in which
		/// case fares and retailer data will need to be recalculated.
		/// </summary>
		public int ProcessedCjpCallRequestID
		{
			get { return this.processedCjpCallRequestID; }
			set { this.processedCjpCallRequestID = value; }
		}
        
		/// <summary>
		/// Read/Write. Flag to indicate whether child fares are to be displayed
		/// </summary>
		public bool ShowChildFares 
		{
			get { return showChildFares; }
			set { showChildFares = value; }
		}

		/// <summary>
		/// Read/Write. The user's override for the default itinerary type
		/// </summary>
		public ItineraryType OverrideItineraryType 
		{
			get { return overrideItineraryType; }
			set { overrideItineraryType = value; }
		}

		/// <summary>
		/// Read/Write. Itinerary for currently selected journeys
		/// </summary>
		public Itinerary JourneyItinerary 
		{
			get {return itinerary;}
			set 
			{
				if (itinerary != value)
				{
					itinerary = value;

					// Selected tickets are no longer valid, so clear them to avoid accumulating 
					// a hashtable full of unnecessary information.
					ClearSelectedTickets();
				}
			}
		}

		/// <summary>
		/// Read/Write. Flag to help a page track if fares should be calculated again
		/// </summary>
		public bool CalculateFaresOverride
		{
			get { return calculateFaresOverride; }
			set { calculateFaresOverride = value; }
		}

		/// <summary>
		/// Read/Write. The selected outbound journey id used in the creation 
		/// of the itinerary       
		/// </summary>
		public int ProcessedOutwardJourney 
		{
			get {return processedOutwardJourney;}
			set {processedOutwardJourney = value;}
		}

		/// <summary>
		/// Read/Write. The selected return journey id used in the 
		/// creation of the itinerary    
		/// </summary>
		public int ProcessedReturnJourney 
		{
			get {return processedReturnJourney;}
			set {processedReturnJourney = value;}
		}

		/// <summary>
		/// Read/Write. The Itinerary segment used in the creation of the itinerary       
		/// </summary>
		public int ItinerarySegment 
		{
			get {return itinerarySegment;}
			set {itinerarySegment = value;}
		}

		/// <summary>
		/// Read/Write. The number of adults travelling, for ticket sales purposes
		/// </summary>
		public int AdultPassengers 
		{
			get { return adultPassengers; }
			set { adultPassengers = value; }
		}

		/// <summary>
		/// Read/Write. The number of children travelling, for ticket sales purposes
		/// </summary>
		public int ChildPassengers
		{
			get { return childPassengers; }
			set { childPassengers = value; }
		}

		/// <summary>
		/// Read/Write. Cache for TicketRetailerInfo object for outward journey
		/// </summary>
		public TicketRetailerInfo OutwardTicketRetailerInfo
		{
			get { return outwardTicketRetailerInfo; }
			set 
			{
				outwardTicketRetailerInfo = value; 
				selectedBuyOptionChangedSinceOutwardInfoSet = false;
				selectedTicketsChangedSinceOutwardInfoSet = false;
				selectedOutwardRetailUnit = null;
			}
		}

		/// <summary>
		/// Read/Write. Cache for selected retail unit from the outwardTicketRetailerInfo
		/// </summary>
		public RetailUnit SelectedOutwardRetailUnit
		{
			get { return selectedOutwardRetailUnit; }
			set { selectedOutwardRetailUnit = value; }
        }

		/// <summary>
		/// Read/Write. Cache for TicketRetailerInfo object for return journey
		/// </summary>
		public TicketRetailerInfo ReturnTicketRetailerInfo
		{
			get { return returnTicketRetailerInfo; }
			set 
			{
				returnTicketRetailerInfo = value; 
				selectedTicketsChangedSinceReturnInfoSet = false;
				selectedBuyOptionChangedSinceReturnInfoSet = false;
				selectedReturnRetailUnit = null;
			}
		}

		/// <summary>
		/// Read/Write. Cache for selected retail unit from the returnTicketRetailerInfo
		/// </summary>
		public RetailUnit SelectedReturnRetailUnit
		{
			get { return selectedReturnRetailUnit; }
			set { selectedReturnRetailUnit = value; }
		}

		/// <summary>
		/// Read/Write. Used to determine if user has selected to display 
		/// outward fare information in a table
		/// </summary>
		public bool ShowOutboundFaresInTableFormat
		{
			get { return showOutboundFaresInTableFormat; }
			set { showOutboundFaresInTableFormat = value; }
			
		}

		/// <summary>
		/// Read/Write. Used to determine if user has selected to display 
		/// return fare information in a table
		/// </summary>
		public bool ShowReturnFaresInTableFormat
		{
			get { return showReturnFaresInTableFormat; }
			set { showReturnFaresInTableFormat = value; }
		}


		/// <summary>
		/// Read/Write. The most recently selected Retailer from the 
		/// ticket retailers page
		/// </summary>
		public Retailer LastRetailerSelection
		{
			get { return lastRetailerSelection; }
			set { lastRetailerSelection = value; }
		}

		/// <summary>
		/// Read/Write. Whether or not the last retailer selection was for the 
		/// outward or return part of the journey
		/// </summary>		
		public bool LastRetailerSelectionIsForReturn
		{
			get { return lastRetailerSelectionIsForReturn; }
			set { lastRetailerSelectionIsForReturn = value; }

		}

		/// <summary>
		/// Read/Write. Used to flag whether the display of selected tickets 
		/// should be left unchanged
		/// </summary>
		public bool LeaveTicketDisplay 
		{
			get { return leaveTicketDisplay; }
			set { leaveTicketDisplay = value; }
		}

		/// <summary>
		/// Read/Write. 
		/// </summary>
		public FindFareBuyOption SelectedCostBasedBuyOption
		{
			get { return selectedCostBasedBuyOption; }
			set
			{
				selectedCostBasedBuyOption = value;
				selectedBuyOptionChangedSinceOutwardInfoSet = true;
				selectedBuyOptionChangedSinceReturnInfoSet = true;
			}
				
		}

        #endregion Properties        
        
        #region Public Methods
                                
		/// <summary>
		/// Stores the details of the journey that the current itinerary applies to. If this is
		/// called after the itinerary has been set, then calls to Has...Changed methods will be
		/// able to determine if the itinerary is still valid.
		/// </summary>
        public void SetProcessedJourneys()
        {
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyViewState journeyViewState = itineraryManager.JourneyViewState;
			
			processedOutwardJourney = journeyViewState.SelectedOutwardJourney;
			processedReturnJourney = journeyViewState.SelectedReturnJourney;

			processedCjpCallRequestID = GetCJPRequestIdFromSession();
        }
        
		/// <summary>
		/// Retrieves the current CJP Request ID from the session. 
		/// </summary>
		/// <returns></returns>
		private int GetCJPRequestIdFromSession()
		{
			int referenceNumber;

			if (itinerarySegment != -1)
			{
				referenceNumber = TDItineraryManager.Current.SpecificJourneyReferenceNumber(itinerarySegment);
			}
			else
			{
				ITDJourneyResult result = TDSessionManager.Current.JourneyResult;
				referenceNumber = result.JourneyReferenceNumber;
			}

			return referenceNumber;
		}


		/// <summary>
		/// Handles loading user preferences for discount cards, etc
		/// </summary>
		public void CheckForNewUserLogin()
		{
			// Initialise the session with pricing and retail user options if this
			// if the first time the user has visited pricing and ticket retailing pages
			// during the session
			if ( NewUserLogin() ) 
			{
				// User has just logged in so load discounts from profile
				LoadDiscountsFromProfile(TDSessionManager.Current.CurrentUser.UserProfile);
				ApplyNewDiscounts = true;
			}
		}

		/// <summary>
		/// Returns true if the selected journey has changed since the last call to 
		/// SetProcessedJourney
		/// </summary>
        public bool HasProcessedRetailersJourneyChanged
        {
            get 
            {
				TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;

				return (
					journeyViewState.SelectedOutwardJourney != processedOutwardJourney
					|| journeyViewState.SelectedReturnJourney != processedReturnJourney
					|| processedCjpCallRequestID != GetCJPRequestIdFromSession()
					);
			}
        }

		/// <summary>
		/// Returns true if the selected journey has changed since the last call to 
		/// SetProcessedJourney, or if the ApplyNewDiscounts property is true
		/// </summary>
        public bool HasProcessedFaresJourneyChanged
        {
            get 
            {
				TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;

				return (
					journeyViewState.SelectedOutwardJourney != processedOutwardJourney 
					|| journeyViewState.SelectedReturnJourney != processedReturnJourney
					|| processedCjpCallRequestID != GetCJPRequestIdFromSession()
					|| applyNewDiscounts
					);
			}
        }

		/// <summary>
		/// Returns true if the TicketRetailerInfo objects are out of date. This can because
		/// they have never been set, or they are for a previously selected journey, or
		/// for a different ticket selection.
		/// </summary>
		public bool HasProcessedTicketRetailerInfoJourneyChanged
		{
			get
			{
				if ( HasProcessedFaresJourneyChanged )
				{
					return true;
				}
				else if ((OverrideItineraryType == ItineraryType.Single) || (OverrideItineraryType == ItineraryType.Return && JourneyItinerary.Type == ItineraryType.Single))
				{
					if ( (OutwardTicketRetailerInfo == null) && (ReturnTicketRetailerInfo == null) )
						return true;
					else if ( (OutwardTicketRetailerInfo != null) && ((OutwardTicketRetailerInfo.CjpRequestId != ProcessedCjpCallRequestID) || (selectedTicketsChangedSinceOutwardInfoSet) || (selectedBuyOptionChangedSinceOutwardInfoSet) ) )
						return true;
					else if ( (ReturnTicketRetailerInfo != null) && ((ReturnTicketRetailerInfo.CjpRequestId != ProcessedCjpCallRequestID) || (selectedTicketsChangedSinceReturnInfoSet) || (selectedBuyOptionChangedSinceReturnInfoSet) ) )
						return true;
					else
						return false;
				}
				else
				{
					// The OutwardTicketRetailerInfo should be set and the 
					// ReturnTicketRetailerInfo should be null
					
					if ( (OutwardTicketRetailerInfo != null) && (ReturnTicketRetailerInfo == null) )
						return selectedTicketsChangedSinceOutwardInfoSet;
					else
						return true;
				}
			}
		}
        
		/// <summary>
		/// Initialises the override itinerary type. If user has specified an open return in the 
		/// journey parameters then use ItineraryType.Return, otherwise use the default type from
		/// the Itinerary.
		/// </summary>
		/// <param name="type"></param>
        public void InitOverrideItineraryType(ItineraryType type)
        {
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			TDJourneyParametersMulti journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
			if (journeyParameters == null)
			{
				OverrideItineraryType = type;
			}
			else
			{

				if (journeyParameters.IsOpenReturn) 
				{
					OverrideItineraryType = ItineraryType.Return;
				} 
				else 
				{
					OverrideItineraryType = type;
				}
			}
        }
        
		/// <summary>
		/// Returns true if a new user has logged in since this method was last called.
		/// </summary>
		/// <returns></returns>
		private bool NewUserLogin() 
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			bool result = (sessionManager.Authenticated == true && loggedIn == false);
			loggedIn = sessionManager.Authenticated;
			return result;
		}

		/// <summary>
		/// True if we are currently in cost-based mode
		/// </summary>
		private bool IsCostBased
		{
			get { return (TDSessionManager.Current.Partition == TDSessionPartition.CostBased); }
		}

        /// <summary>
        /// This method loads the discounts from the user profile if they exists.
        /// </summary>
        /// <param name="faresPreferences">The users profile</param>
        /// <returns></returns>
        public void LoadDiscountsFromProfile( TDProfile faresPreferences ) 
		{
			//populate fares preference fields with user values
			if(faresPreferences != null) 
			{
				// user may not have fares preferences, in which case, use defaults
				string railcard = faresPreferences.Properties[ProfileKeys.DISCOUNT_CARD_RAIL].Value 
					!= null 
					? faresPreferences.Properties[ProfileKeys.DISCOUNT_CARD_RAIL].Value.ToString() 
					: string.Empty;
				string coachcard = faresPreferences.Properties[ProfileKeys.DISCOUNT_CARD_COACH].Value
					!= null 
					? faresPreferences.Properties[ProfileKeys.DISCOUNT_CARD_COACH].Value.ToString()
					: string.Empty;

				Discounts = new Discounts( railcard, coachcard, TicketClass.All );
			}
		}

		/// <summary>
		/// Stores the selected ticket for a given pricing unit. Specify 
		/// PricingRetailOptionsState.NoTicket for selectedTicket if no ticket 
		/// is selected. 
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="selectedTicket"></param>
		public void SetSelectedTicket(PricingUnit pricingUnit, Ticket selectedTicket)
		{
			bool changed = false;
			if (selectedTicketsHash.ContainsKey(pricingUnit))
			{
				if (selectedTicket == null)
				{
					selectedTicketsHash.Remove(pricingUnit);
					changed = true;
				}
				else
				{
					if (!((Ticket)selectedTicketsHash[pricingUnit]).Equals(selectedTicket))
					{
						selectedTicketsHash[pricingUnit] = selectedTicket;
						changed = true;
					}
				}
			}
			else if (selectedTicket != null)
			{
				selectedTicketsHash[pricingUnit] = selectedTicket;
				changed = true;
			}

			if (changed)
			{
				selectedTicketsChangedSinceOutwardInfoSet = true;
				selectedTicketsChangedSinceReturnInfoSet = true;
			}
		}

		/// <summary>
		/// Stores the selected ticket for a given pricing unit. 
		/// </summary>
		public void SetSelectedTicket()
		{
			CostSearchTicket[] selectedTicketsOut = null;
			CostSearchTicket[] selectedTicketsIn = null;

			FindCostBasedPageState pageState = TDSessionManager.Current.FindPageState as FindCostBasedPageState;

			if (pageState != null)
			{
				// Update the selected tickets
				switch ( this.SelectedCostBasedBuyOption )
				{
					case FindFareBuyOption.SingleOrReturn:
						selectedTicketsOut = pageState.SingleOrReturnTicketTable[pageState.SelectedSingleOrReturnTicketIndex].CostSearchTickets;
						break;
					case FindFareBuyOption.OutwardSingle:
						selectedTicketsOut = pageState.OutwardTicketTable[pageState.SelectedOutwardTicketIndex].CostSearchTickets;
						break;
					case FindFareBuyOption.ReturnSingle:
						selectedTicketsIn = pageState.InwardTicketTable[pageState.SelectedInwardTicketIndex].CostSearchTickets;
						break;
					case FindFareBuyOption.BothSingle:
						selectedTicketsOut = pageState.OutwardTicketTable[pageState.SelectedOutwardTicketIndex].CostSearchTickets;
						selectedTicketsIn = pageState.InwardTicketTable[pageState.SelectedInwardTicketIndex].CostSearchTickets;
						break;
					default:
						selectedTicketsOut = pageState.SingleOrReturnTicketTable[pageState.SelectedSingleOrReturnTicketIndex].CostSearchTickets;
						break;
				}
			}

			ClearSelectedTickets();

			if (selectedTicketsOut != null)
			{
				for ( int index = 0; index < selectedTicketsOut.Length; index++ )
					SetSelectedTicket( (PricingUnit)this.JourneyItinerary.OutwardUnits[index], (Ticket)selectedTicketsOut[index] );
			}
			if (selectedTicketsIn != null)
			{
				for ( int index = 0; index < selectedTicketsIn.Length; index++ )
					SetSelectedTicket( (PricingUnit)this.JourneyItinerary.ReturnUnits[index], (Ticket)selectedTicketsIn[index] );
			}
		}

		/// <summary>
		/// Retrieves the selected ticket for a given pricing unit.
		/// If no ticket is selected, PricingRetailOptionsState.NoTicket is returned.
		/// If no ticket has been specified for the pricing unit, null is returned
		/// in which case it can be assumed that the first ticket in the appropriate
		/// PricingResult from the PricingUnit can be used.
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <returns></returns>
		public Ticket GetSelectedTicket(PricingUnit pricingUnit)
		{			
			if (selectedTicketsHash.ContainsKey(pricingUnit))
				return (Ticket)selectedTicketsHash[pricingUnit];
			else
				return null;
		}

		/// <summary>
		/// Clears the list of selected tickets
		/// </summary>
		public void ClearSelectedTickets()
		{
			selectedTicketsHash = new Hashtable();
		}

		/// <summary>
		/// Creates a JourneyItinerary for the pricing options object
		/// </summary>
		/// <param name="costBasedBuyOption">The FindFareBuyOption to be selected</param>
		/// <param name="outwardJourney">The outward Journey details</param>
		/// <param name="returnJourney">The return Journey details</param>
		public void CreateItinerary(FindFareBuyOption costBasedBuyOption, Journey outwardJourney, Journey returnJourney)
		{
			// Call the method doing the calculations but indicate that it hasn't come from the RefineTickets page
			CreateItinerary(costBasedBuyOption, outwardJourney, returnJourney, false);
		}

		/// <summary>
		/// Creates a JourneyItinerary for the pricing options object
		/// </summary>
		/// <param name="costBasedBuyOption">The FindFareBuyOption to be selected</param>
		/// <param name="outwardJourney">The outward Journey details</param>
		/// <param name="returnJourney">The return Journey details</param>
		/// <param name="fromRefineTicketsPage">Determines if it has come from the RefineTickets Page</param>
		public void CreateItinerary(FindFareBuyOption costBasedBuyOption, Journey outwardJourney, Journey returnJourney, bool fromRefineTicketsPage)
		{
			string railDiscount;
			string coachDiscount;
			TicketType currentTicketType;

			FindCostBasedPageState pageState = TDSessionManager.Current.FindPageState as FindCostBasedPageState;

			if (pageState != null)
			{
				if (pageState.SearchRequest == null)
				{
					pageState.SearchRequest = new CostSearchRequest();
				}
				railDiscount  = pageState.SearchRequest.RailDiscountedCard;
				coachDiscount = pageState.SearchRequest.CoachDiscountedCard;

				currentTicketType = pageState.SelectedTravelDate.TravelDate.TicketType;
			}
			else
			{
				railDiscount  = String.Empty;
				coachDiscount = String.Empty;
				currentTicketType = ((outwardJourney != null) && (returnJourney != null)) ? TicketType.Return : TicketType.Single;
			}

			ItineraryType targetItineraryType = ItineraryType.Single;

			switch ( costBasedBuyOption )
			{
				case FindFareBuyOption.SingleOrReturn:
					if (currentTicketType == TicketType.Single)
						targetItineraryType = ItineraryType.Single;
					else
						targetItineraryType = ItineraryType.Return;
					break;
				case FindFareBuyOption.OutwardSingle:
					targetItineraryType = ItineraryType.Single;
					break;
				case FindFareBuyOption.ReturnSingle:
					targetItineraryType = ItineraryType.Single;
					break;
				case FindFareBuyOption.BothSingle:
					targetItineraryType = ItineraryType.Single;
					break;
				default:
					targetItineraryType = ItineraryType.Single;
					break;
			}

			// Null journeys will be passed if selected journeys are not public
			if ( targetItineraryType == ItineraryType.Single )
			{
				this.JourneyItinerary = new Itinerary(outwardJourney, returnJourney, false);
			}
			else
			{
				// Note: If coming from the RefineTickets page we don't want to force a Matching Return ticket option
				this.JourneyItinerary = new Itinerary(outwardJourney, returnJourney, !fromRefineTicketsPage);
			}
			
			// If having come from the refineTickets page - examine the PricingUnit "matchingReturn" property
			// to determine if the return pricing units match the outward journey - otherwise single tickets will be required
			// Note: if returnJourney is null it will be a single journey so targetItineraryType will = Single
			if (fromRefineTicketsPage)
			{
				if (returnJourney != null && this.JourneyItinerary.ReturnUnits != null && this.JourneyItinerary.ReturnUnits.Count > 0)
				{
					PricingUnit pRet = (PricingUnit)this.JourneyItinerary.ReturnUnits[0];
					
					if  (pRet.MatchingReturn && costBasedBuyOption == FindFareBuyOption.SingleOrReturn)
					{
						targetItineraryType = ItineraryType.Return;
					}
					else
					{
						targetItineraryType = ItineraryType.Single;
					}
				}
				else
				{
					targetItineraryType = ItineraryType.Single;
				}
			}
					
			this.OverrideItineraryType = targetItineraryType;

			// Get the discounts
			this.Discounts = new Discounts(railDiscount, coachDiscount, TicketClass.All);

			//As we have just calculated pricing information there cannot be
			//any new discounts to apply so set to false.
			this.ApplyNewDiscounts = false;

			//Set the state objects to reflect the currectly pricing journeys
            this.SetProcessedJourneys();
         
		}

        public bool IsPublic () 
        {

            bool outwardIsPublic = (this.itinerary.OutwardJourney != null && (this.itinerary.OutwardJourney.Type == TDJourneyType.PublicAmended || 
                this.itinerary.OutwardJourney.Type == TDJourneyType.PublicOriginal));
            bool returnIsPresent = this.itinerary.ReturnJourney != null;
            bool returnIsPublic = (this.itinerary.ReturnJourney != null && (this.itinerary.ReturnJourney.Type == TDJourneyType.PublicAmended ||
                this.itinerary.ReturnJourney.Type == TDJourneyType.PublicOriginal));

            return (outwardIsPublic && !returnIsPresent) || 
                (outwardIsPublic && returnIsPresent && returnIsPublic);

        }

		/// <summary>
		/// Generates the TicketRetailerInfo objects for time based searches
		/// </summary>
		/// <param name="options"></param>
		public void PopulateTicketRetailerInfo()
		{
			// Time based, so there is an itinerary
			if ((this.JourneyItinerary.Type == ItineraryType.Single) || (this.OverrideItineraryType == ItineraryType.Single))
			{
				SelectedTicket[] outwardSelectedTickets = BuildSelectedTickets(this.JourneyItinerary.OutwardUnits, this.JourneyItinerary.OutwardJourney);
				if ((outwardSelectedTickets != null) && (outwardSelectedTickets.Length != 0))
				{
					TDDateTime outwardJourneyDate = ((PublicJourney)this.JourneyItinerary.OutwardJourney).Details[0].LegStart.DepartureDateTime;
					this.OutwardTicketRetailerInfo = new TicketRetailerInfo(outwardJourneyDate, this.OverrideItineraryType, false, outwardSelectedTickets, this.ProcessedCjpCallRequestID);
					if (this.OutwardTicketRetailerInfo.RetailUnits.Length == 1)
						this.SelectedOutwardRetailUnit = this.OutwardTicketRetailerInfo.RetailUnits[0];
				}
				else
					this.OutwardTicketRetailerInfo = null;

				if (this.JourneyItinerary.ReturnJourney != null)
				{
					SelectedTicket[] inwardSelectedTickets = BuildSelectedTickets(this.JourneyItinerary.ReturnUnits, this.JourneyItinerary.ReturnJourney);
					if ((inwardSelectedTickets != null) && (inwardSelectedTickets.Length != 0))
					{
						TDDateTime inwardJourneyDate = ((PublicJourney)this.JourneyItinerary.ReturnJourney).Details[0].LegStart.DepartureDateTime;
						this.ReturnTicketRetailerInfo = new TicketRetailerInfo(inwardJourneyDate, this.OverrideItineraryType, true, inwardSelectedTickets, this.ProcessedCjpCallRequestID);
						if (this.ReturnTicketRetailerInfo.RetailUnits.Length == 1)
							this.SelectedReturnRetailUnit = this.ReturnTicketRetailerInfo.RetailUnits[0];
					}
					else
						this.ReturnTicketRetailerInfo = null;
				}
				else
					this.ReturnTicketRetailerInfo = null;
			}
			else
			{
				TDDateTime journeyDate = ((PublicJourney)this.JourneyItinerary.OutwardJourney).Details[0].LegStart.DepartureDateTime;
				this.OutwardTicketRetailerInfo = new TicketRetailerInfo(journeyDate, ItineraryType.Return, false, BuildSelectedTickets(this.JourneyItinerary.OutwardUnits, this.JourneyItinerary.OutwardJourney), this.ProcessedCjpCallRequestID);
				if (this.OutwardTicketRetailerInfo.RetailUnits.Length == 1)
					this.SelectedOutwardRetailUnit = this.OutwardTicketRetailerInfo.RetailUnits[0];
				this.ReturnTicketRetailerInfo = null;
			}

		}

		/// <summary>
		/// Returns the current PricingRetailOptionsState object, creating it first if necessary
		/// </summary>
		/// <param name="costBasedBuyOption">The FindFareBuyOption to be selected</param>
		/// <param name="outwardJourney">The outward Journey details</param>
		/// <param name="returnJourney">The return Journey details</param>
		/// <returns></returns>
		public void GetPricingRetailOptionsState(FindFareBuyOption costBasedBuyOption, Journey outwardJourney, Journey returnJourney)
		{
			// Call the method doing the calculations but indicate that it hasn't come from the RefineTickets page
			GetPricingRetailOptionsState(costBasedBuyOption, outwardJourney, returnJourney, false);
		}
		
		/// <summary>
		/// Returns the current PricingRetailOptionsState object, creating it first if necessary
		/// </summary>
		/// <param name="ensureTicketRetailerInfoPopulated">Whether or not to populate the 
		/// TicketRetailerInfo properties they are not present/out of date necessary</param>
		/// <param name="costBasedBuyOption">The FindFareBuyOption to be selected</param>
		/// <param name="outwardJourney">The outward Journey details</param>
		/// <param name="returnJourney">The return Journey details</param>
		/// <param name="fromRefineTicketsPage">Determines if it has come from the RefineTickets Page</param>
		/// <returns></returns>
		public void GetPricingRetailOptionsState(FindFareBuyOption costBasedBuyOption, Journey outwardJourney, Journey returnJourney, bool fromRefineTicketsPage)
		{
			// Initialise the session with pricing and retail user options if this
			// if the first time the user has visited pricing and ticket retailing pages
			// during the session

			if (this.HasProcessedFaresJourneyChanged)
			{
				this.CreateItinerary(costBasedBuyOption, outwardJourney, returnJourney, fromRefineTicketsPage);
			}

			// it doesn't appear that this secenario can occur, since we can only get here  
			// from the retailers page, and there is no way to change journey or fare 
			// selection from there without returning to the fares page first ...
			// If the buy options has changed, look up the correct tickets from cost based page state
			
			if  ((this.SelectedCostBasedBuyOption != costBasedBuyOption) || this.HasProcessedFaresJourneyChanged)
			{
				if	(!fromRefineTicketsPage)
				{
					this.SelectedCostBasedBuyOption = costBasedBuyOption;
					SetSelectedTicket();
				}
				else
				{
					ClearSelectedTickets();
				}
			}


			// Find the retailers if necessary
			if (!this.JourneyItinerary.RetailersInitialised)
				this.JourneyItinerary.FindRetailers();

			if (this.HasProcessedTicketRetailerInfoJourneyChanged)
			{
				PopulateTicketRetailerInfo();
			}
		}

		/// <summary>
		/// Builds an array of selected ticket objects for the pricing units supplied.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="pricingUnits"></param>
		/// <returns></returns>
		public SelectedTicket[] BuildSelectedTickets(IList pricingUnits, Journey journey)
		{
			ArrayList selectedTickets = new ArrayList();
			Ticket selectedUnitTicket;
			Ticket currentTicket;
			bool minimumFareApplies = false;

			PublicJourney publicJourney = journey as PublicJourney;

			if ( publicJourney != null )
			{
				minimumFareApplies = publicJourney.MinimumFareApplies;
			}

			foreach (PricingUnit currentUnit in pricingUnits)
			{
				selectedUnitTicket = this.GetSelectedTicket(currentUnit);

				if ((selectedUnitTicket == null) || (selectedUnitTicket.Equals( PricingRetailOptionsState.NoTicket )))
				{
					currentTicket = PricingRetailOptionsState.NoTicket;
				}
				else
				{
					currentTicket = currentUnit.FindSelectedTicket(selectedUnitTicket);
				}

				CostSearchTicket currentCostSearchTicket = currentTicket as CostSearchTicket;
				if ( minimumFareApplies && ( currentCostSearchTicket != null) )
				{
					float adultFare = Math.Min(currentCostSearchTicket.AdultFare, Math.Max( currentCostSearchTicket.DiscountedAdultFare, currentCostSearchTicket.MinimumAdultFare ));
					float childFare = Math.Min(currentCostSearchTicket.ChildFare, Math.Max( currentCostSearchTicket.DiscountedChildFare, currentCostSearchTicket.MinimumChildFare ));
					currentTicket = new Ticket(currentCostSearchTicket.Code, currentCostSearchTicket.Flexibility, currentCostSearchTicket.ShortCode, currentCostSearchTicket.AdultFare, currentCostSearchTicket.ChildFare, adultFare, childFare, currentCostSearchTicket.MinChildAge, currentCostSearchTicket.MaxChildAge);
					// No need to add the TicketRailFareData as it isn't required from here on in
				}

				if ((currentTicket != null) && (!currentTicket.Equals(PricingRetailOptionsState.NoTicket)))
				{
					selectedTickets.Add( new SelectedTicket( currentUnit, currentTicket ) );
				}
			}
			return (SelectedTicket[])selectedTickets.ToArray(typeof(SelectedTicket));
		}

		#endregion Public Methods
    }

	/// <summary>
	/// The options the user has for buying tickets after a cost based search
	/// </summary>
	[Serializable]
	public enum FindFareBuyOption
	{
		None,
		SingleOrReturn,
		OutwardSingle,
		ReturnSingle,
		BothSingle
	}
}
