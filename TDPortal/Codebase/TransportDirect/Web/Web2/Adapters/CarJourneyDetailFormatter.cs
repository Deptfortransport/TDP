// *********************************************** 
// NAME			: CarJourneyDetailFormatter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 21.01.04
// DESCRIPTION	: Wraps a RoadJourney object to provide easier
// formatting of car journey details for presentation purposes.
// Subclasses will implement formatting for specific purposes e.g.
// web page output, email output and so on. Most of the code here
// originated in CarJourneyDetailsTableControl.aspx.cs and has 
// been moved from their as part of a refactoring exercise to 
// separate the business logic of formatting journey details 
// from the presentation layer.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CarJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.15   Sep 01 2011 10:44:36   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.14   Mar 14 2011 15:12:00   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.13   Nov 08 2010 10:02:42   apatel
//Added extra error handling code for GetSpeed method
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.12   Oct 28 2010 11:18:06   apatel
//Updated to correct the pvc comment as files assigned to wrong IR 5622 instead of IR 5623
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.11   Oct 26 2010 14:37:34   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.10   Aug 25 2010 16:08:00   RBroddle
//London Congestion Charge Additional Text added for CCN0602
//Resolution for 5596: CCN0602 London CCzone text change
//
//   Rev 1.9   Oct 26 2009 10:07:02   mmodi
//Display distances to 2dp
//
//   Rev 1.8   Oct 21 2009 10:50:32   mmodi
//Added flag to override the display of the congestion charge
//
//   Rev 1.7   Oct 16 2009 17:05:36   mmodi
//Output metres distance is CJP user
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Oct 15 2009 13:20:16   mmodi
//Added display congestion symbol flag
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.5   Oct 11 2009 12:38:54   mmodi
//Added display Toll charge flag
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Sep 29 2009 15:24:28   nrankin
//Accessibility - opens in new window
//Resolution for 5320: Accessibility - Opens in new window
//
//   Rev 1.3   Sep 21 2009 14:57:04   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Mar 31 2008 12:58:44   mturner
//Drop3 from Dev Factory
//
//  Rev devfactory Feb 12 2008 12:30:00   mmodi
//Property to display the Find car parks link
//
//   Rev 1.0   Nov 08 2007 13:11:12   mturner
//Initial revision.
//
//   Rev 1.55   Jan 12 2007 13:23:06   jfrank
//Changed for IR4277 - Adding and end instruction to a road journey ending in the congegestion zone at a time when a charge applies, if it entered at a time when a charge didn't apply.
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.54   Nov 29 2006 16:22:44   mmodi
//Minor adjustments to congestion charge text
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.53   Nov 24 2006 15:27:00   PScott
//CCN0342a draft changes
//
//   Rev 1.52   Apr 24 2006 13:41:42   jbroome
//Removed unnecessary checking for arriveBefore
//Resolution for 3981: DN083 Car Ambiguity: Details view shows wrong arrival time for an arriving by journey
//
//   Rev 1.51   Apr 20 2006 10:57:18   esevern
//added setting of park and ride, and car park instructional text
//Resolution for 3839: DN058 Park and Ride Phase 2 - Final instruction in car journey details is not the correct format
//
//   Rev 1.50   Apr 13 2006 15:39:48   mtillett
//Update logic to output "until junction XX" instruction
//Resolution for 3892: Ambiguity: all suffixes of 'until junction no' have disappeared
//
//   Rev 1.49   Apr 12 2006 15:01:14   jbroome
//Updates for Ambiguity IRs
//Resolution for 3861: Ambiguity - Never use 'Follow road bearing ...'
//Resolution for 3883: Ambiguity - Now get a continue instruction - previously got a bear
//
//   Rev 1.48   Apr 07 2006 17:11:20   mguney
//Updated for new cases.
//Resolution for 3849: Car Journey Instructions Update after stream0030
//
//   Rev 1.46.1.1   Mar 31 2006 09:37:42   tolomolaiye
//Further Car Enhancement updates
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.46.1.0   Mar 30 2006 11:37:28   tolomolaiye
//Changes for driving enhancements
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.46   Mar 14 2006 10:09:54   NMoorhouse
//stream3353 manual merge from branch -> trunk
//
//   Rev 1.45   Mar 03 2006 14:41:10   rgreenwood
//Manual merge Stream0004
//
//   Rev 1.44   Feb 23 2006 19:16:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.43.3.2   Mar 10 2006 13:05:58   NMoorhouse
//Fix a problem with displaying extended return journeys
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.43.3.1   Mar 02 2006 17:47:08   NMoorhouse
//extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.43.3.0   Feb 24 2006 14:34:18   NMoorhouse
//Changes to support the addition of new page to display CarDetails
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.43.2.1   Jan 30 2006 12:15:16   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.43.2.0   Jan 10 2006 15:17:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.43   Aug 03 2005 21:06:20   asinclair
//Fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.42   Aug 02 2005 12:49:52   jmorrissey
//Fix for Vantive 3868786
//
//   Rev 1.41   May 19 2005 10:31:54   rscott
//IR2499 - changes to charge message in detail descriptions for Ferries, Tolls and Congestion Charges.
//
//   Rev 1.40   May 13 2005 15:09:42   asinclair
//Fix for IR 2497
//
//   Rev 1.39   May 12 2005 17:31:44   asinclair
//Updated to pass a print bool into Construtor
//
//   Rev 1.38   May 11 2005 17:26:48   asinclair
//Fix for IR 2483
//
//   Rev 1.37   May 10 2005 16:25:30   tmollart
//Fixed problem with hyperlinks (// instead of \\ is now used).
//Modified code to use StringBuilder class as well.
//Removed variables that were not really needed e.g. defining cost and populating with costPence.
//Resolution for 2471: Firefox/Netscape Car Details Congestion Charge Info Link Failure
//
//   Rev 1.36   May 10 2005 14:47:16   asinclair
//Fix for IR 2453
//
//   Rev 1.35   May 06 2005 10:36:32   asinclair
//Fix for IR 2450 and 2321
//
//   Rev 1.34   May 04 2005 10:55:44   tmollart
//Modified ferry formatter so if price < 0 we dont detail the price on the journey details.
//Resolution for 2402: Car - Ferry cost minus 1p is incorrect
//
//   Rev 1.33   May 04 2005 10:21:18   jbroome
//FerryExit() method takes disembark time into account
//Resolution for 2354: Del 7  - Car costing - Ferry journey time
//
//   Rev 1.32   May 03 2005 15:32:22   jbroome
//Modified congestion charge instruction when no charge is applicable.
//Resolution for 2395: DEL 7 Car Costing - Send to a firend
//
//   Rev 1.31   May 03 2005 10:35:22   asinclair
//Removed 'Congestion zone' text in congestion zone entry string
//
//   Rev 1.30   Apr 26 2005 17:11:32   rgreenwood
//IR2221: Enhanced TollEntry(), FerryEntry() and CongestionEntry() methods to correct the conversion and display of charges in the details control.
//
//   Rev 1.29   Apr 26 2005 09:32:10   rgeraghty
//Check added for Company Url being > 0 - if so display as hyperlink, if not display as text
//Resolution for 2233: DEl 7 - Car costing - identifying ferry
//
//   Rev 1.28   Apr 23 2005 19:29:10   asinclair
//Fix for IR 2244 and 1983
//
//   Rev 1.27   Apr 23 2005 11:36:14   asinclair
//Added code to check for StopoverSection of 'UndefinedWait'. Fix for IR 2244
//
//   Rev 1.26   Apr 21 2005 16:10:12   rgreenwood
//IR2221: Refactored fix after feedback from ES
//Resolution for 2221: Del 7 -  Car Costing - Toll charge should read £0.80 rather than £.80
//
//   Rev 1.25   Apr 21 2005 13:16:18   rgreenwood
//IR 2221: Removed string.format for toll charge and replaced with a check of toll value
//Resolution for 2221: Del 7 -  Car Costing - Toll charge should read £0.80 rather than £.80
//
//   Rev 1.24   Apr 16 2005 14:46:32   asinclair
//Fix for IR 2017 - Added Bold to a via location
//
//   Rev 1.23   Apr 13 2005 14:41:20   asinclair
//Commented out code casuing errors on Congestion Exit
//
//   Rev 1.22   Apr 11 2005 10:42:14   bflenk
//Check if a RoadJourney is being passed before formatting.
//Resolution for 2037: Printer Friendly Error on Details Page for Car journey
//
//   Rev 1.21   Apr 11 2005 09:49:26   asinclair
//Fix for IR1989 in progress
//
//   Rev 1.20   Apr 04 2005 14:56:40   rgreenwood
//IR 1906 : Put change done in Del 6 into this Del 7 code, for motorway junction merge instructions.
//
//   Rev 1.19   Mar 30 2005 19:04:08   asinclair
//Added congestion zone exit
//
//   Rev 1.18   Mar 30 2005 17:01:06   asinclair
//Added code for ferries with no cost data and to display date and time of departure in instruction
//
//   Rev 1.17   Mar 24 2005 19:57:26   asinclair
//Tidied up and added code for Stopover Wait sections
//
//   Rev 1.16   Mar 18 2005 16:24:46   asinclair
//Changed string[] to object[]
//
//   Rev 1.15   Mar 01 2005 16:23:28   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.14   Feb 28 2005 15:20:36   asinclair
//Added Code for DEL 7 car costing
//
//   Rev 1.13   Jan 20 2005 10:39:08   asinclair
//Work in progress - Del 7 Car Costing
//
//   Rev 1.12   Dec 02 2004 14:56:46   jbroome
//Fix for IR 1800
//
//   Rev 1.11   Nov 26 2004 14:56:00   jmorrissey
//CCN156 Motorway junction changes
//
//   Rev 1.10   Sep 17 2004 15:13:40   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.9   Jun 17 2004 11:24:02   RPhilpott
//Correctly obtain all "bear" text from LangStrings. 
//Resolution for 1026: Missing instruction on a step for  car journey
//
//   Rev 1.8   Jun 16 2004 20:23:50   JHaydock
//Update for JourneyDetailsTableControl to use ItineraryManager
//
//   Rev 1.7   May 24 2004 17:05:16   RPhilpott
//Ensure correct "bear ... " text is used when appropriate.
//Resolution for 924: 'Bear' instruction not appearing
//
//   Rev 1.6   May 20 2004 13:51:42   COwczarek
//Fix to calculate arrival time correctly and to display "Immediately" text on the correct instruction step
//Resolution for 823: Car journey, timings, arrival times and immediate directions
//
//   Rev 1.5   Feb 04 2004 16:58:28   COwczarek
//Complete formatting required for DEL 5.2
//Resolution for 613: Refactoring of code that displays car journey details

using System;
using TransportDirect.Common.ResourceManager;
using System.Text;
using System.Collections;
using System.Globalization;
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Wraps a RoadJourney object to provide easier
	/// formatting of car journey details for presentation purposes.
	/// Subclasses will implement formatting for specific purposes e.g.
	/// web page output, email output and so on.
	/// </summary>
	public abstract class CarJourneyDetailFormatter
	{

		#region Instance Variables
		
		// Used to control rendering of the step number in the table.
		protected int stepNumber;

		// Keeps the accumulated distance of the journey currently being rendered.
		protected int accumulatedDistance;

		// Keeps the accumulated arrival time (updated after each leg)
		protected TDDateTime currentArrivalTime;

		// Constant representing space used in string formatting
		protected readonly static string space = " ";

		// Constant representing space used in string formatting
		protected readonly static string comma = ",";

		// Constant representing full stop used in string formatting
		protected readonly static string fullstop = ".";

        // Constant representing dash/hyphen used in string formatting
        protected readonly static string dash = "-";

		// Road journey to render.
		protected RoadJourney roadJourney;

		// Options pertaining to journey
		protected TDJourneyViewState journeyViewState;
		
		// Culture for returned text
		protected CultureInfo currentCulture;
		
		// True if journey details are for outward journey, false if return
		protected bool outward;

		// True if the high traffic level symbol needs to be displayed.
		protected bool highTrafficLevel;
		
		// Road distance in metres within which "immediate" text should be 
		// prepended to journey direction
		protected int immediateTurnDistance;

		// slip road distance limit parameter
		protected int slipRoadDistance;

		// string for through route
		protected string throughRoute = String.Empty;	
	
		protected RoadUnitsEnum roadUnits;

        // bool to display find car park link in footer
        private bool showFindNearestCarParkLink = true;

        // bool to display/hide the toll charge cost 
        //(the url and text will continue to be displayed but cost part removed)
        protected bool displayTollCharge = true;

        // bool to display/hide the congestion symbol (traffic warning icon)
        protected bool displayCongestionSymbol = true;

        // bool to display/hide the congestion charge (e.g. in London congestion zone)
        protected bool displayCongestionCharge = true;

        // int to determine how many decimal places should be used for the distance display
        protected int distanceDecimalPlaces = 1;

		protected bool print;

        protected bool isCJPUser = false;

		#region Strings that are used to generate the route text descriptions.

		// string for roundabout exit
		protected string roundaboutExitOne = String.Empty;
		protected string roundaboutExitTwo = String.Empty;
		protected string roundaboutExitThree = String.Empty;
		protected string roundaboutExitFour = String.Empty;
		protected string roundaboutExitFive = String.Empty;
		protected string roundaboutExitSix = String.Empty;
		protected string roundaboutExitSeven = String.Empty;
		protected string roundaboutExitEight = String.Empty;
		protected string roundaboutExitNine = String.Empty;
		protected string roundaboutExitTen = String.Empty;

		protected string continueString = String.Empty;
		protected string continueMiniRoundabout = String.Empty;
		protected string leftMiniRoundabout = String.Empty;
		protected string rightMiniRoundabout = String.Empty;
		protected string uTurnMiniRoundabout = String.Empty;
		protected string leftMiniRoundabout2 = String.Empty;
		protected string rightMiniRoundabout2 = String.Empty;
		protected string uTurnMiniRoundabout2 = String.Empty;

		protected string immediatelyTurnLeft = String.Empty;
		protected string immediatelyTurnRight = String.Empty;

		protected string turnLeftOne = String.Empty;
		protected string turnLeftTwo = String.Empty;
		protected string turnLeftThree = String.Empty;
		protected string turnLeftFour = String.Empty;
		
		protected string turnRightOne = String.Empty;
		protected string turnRightTwo = String.Empty;
		protected string turnRightThree = String.Empty;
		protected string turnRightFour = String.Empty;

		protected string turnLeftInDistance = String.Empty;
		protected string turnRightInDistance = String.Empty;

		protected string bearLeft = String.Empty;
		protected string bearRight = String.Empty;

		protected string immediatelyBearLeft = String.Empty;
		protected string immediatelyBearRight = String.Empty;

		protected string leaveFrom = String.Empty;
		protected string arriveAt = String.Empty;
		protected string notApplicable = String.Empty;

		protected string localRoad = String.Empty;

		//route text for motorway junctions
		protected string atJunctionLeave = String.Empty;
		protected string leaveMotorway = String.Empty;
		protected string towards = String.Empty; 
		protected string continueFor = String.Empty; 
		protected string miles = String.Empty;
		protected string turnLeftToJoin = String.Empty; 
		protected string turnRightToJoin = String.Empty; 
		protected string atJunctionJoin = String.Empty; 
		protected string bearLeftToJoin = String.Empty; 
		protected string bearRightToJoin = String.Empty;
		protected string join = String.Empty; 
		protected string forText = String.Empty; 
		protected string follow = String.Empty; 
		protected string to = String.Empty; 
		protected string routeTextFor = String.Empty; 		
		protected string continueText = String.Empty; 
		protected string untilJunction = String.Empty; 
		protected string onTo = String.Empty; 

		//Strings for Del 7 Car Costing
		protected string enter = String.Empty;
		protected string exit = String.Empty;
		protected string end = String.Empty;
		protected string congestionZone = String.Empty;
		protected string charge = String.Empty;
		protected string certainTimes = String.Empty;
		protected string certainTimesNoCharge = String.Empty;
		protected string board = String.Empty;
		protected string departingAt = String.Empty;
		protected string toll = String.Empty;
		protected string notAvailable = String.Empty;
		
		protected string HighTraffic = String.Empty;
		protected string PlanStop = String.Empty;
		protected string FerryWait = String.Empty;
		protected string UnspecifedFerryWait = String.Empty;
		protected string IntermediateFerryWait = String.Empty;
		protected string WaitAtTerminal = String.Empty;

		protected string viaArriveAt = String.Empty;
		protected string leaveFerry = String.Empty;

		//Ambiguity text
		protected string straightOn = string.Empty;
		protected string atMiniRoundabout = string.Empty;
		protected string immediatelyTurnRightOnto = string.Empty;
		protected string immediatelyTurnLeftOnto = string.Empty;
		protected string whereRoadSplits = string.Empty;

		// park and ride specific
		protected string parkAndRide = string.Empty;
		protected string carParkText = string.Empty;

        // open new window icon
        protected string openNewWindowImageUrl = string.Empty;


        //London Congestion Charge Additional Text added for CCN0602
        protected string londonCCAdditionalText = String.Empty;
        protected bool londonCCzoneExtraTextVisible = true;

		
		#endregion

		#endregion Instance Variables

		#region Constructors

		/// <summary>
		/// Constructs a formatter.
		/// </summary>
		/// <param name="journeyResult">Selected journey details</param>
		/// <param name="journeyViewState">Options pertaining to journey</param>
		/// <param name="outward">True if journey details are for outward journey, false if return</param>
		/// <param name="currentCulture">Culture for returned text</param>
		protected CarJourneyDetailFormatter(
			ITDJourneyResult journeyResult, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print
			)
		{
			if(outward)
			{
				Constructor(journeyResult.OutwardRoadJourney(), journeyViewState, outward, currentCulture, roadUnits, print);
			}
			else
			{
				Constructor(journeyResult.ReturnRoadJourney(), journeyViewState, outward, currentCulture, roadUnits, print);
			}
		}

		/// <summary>
		/// Constructs a formatter.
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
		/// <param name="currentCulture">Culture for returned text</param>
		protected CarJourneyDetailFormatter(
			RoadJourney roadJourney, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print
			)
		{
			Constructor(roadJourney, journeyViewState, outward, currentCulture, roadUnits, print);
		}

		/// <summary>
		/// Private default constructor method
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
		/// <param name="currentCulture">Culture for returned text</param>
		private void Constructor(
			RoadJourney roadJourney, 
			TDJourneyViewState journeyViewState,
			bool outward,
			CultureInfo currentCulture,
			RoadUnitsEnum roadUnits, bool print
			)
		{
			this.roadJourney = roadJourney;
			this.journeyViewState = journeyViewState;
			this.outward = outward;
			this.currentCulture = currentCulture;
			this.roadUnits = roadUnits;
			this.print = print;
			
			InitialiseRouteTextDescriptionStrings();
		
			// Get road distance in metres within which "immediate" text should be 
			// prepended to journey direction
			immediateTurnDistance = Convert.ToInt32(Properties.Current["Web.CarJourneyDetailsControl.ImmediateTurnDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);

			// Get slip road distance limit parameter
			slipRoadDistance = Convert.ToInt32(Properties.Current["Web.CarJourneyDetailsControl.SlipRoadDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);
            
            // property switch for CCN0602 London CCzone Extra Text - displayed when CCzone boundaries under review
            londonCCzoneExtraTextVisible = bool.Parse(Properties.Current["CCN0602LondonCCzoneExtraTextVisible"]);

		}
		
		#endregion Constructors

		#region Properties
				
		/// <summary>
		/// The accummulated distance of a journey in miles. The value of the
		/// accummulated distance is calculated during the formatting of
		/// journey instructions and is incremented by GetDistance.
		/// </summary>
		protected virtual string TotalDistance
		{
			get
			{
				return (accumulatedDistance > 0 ? ConvertMetresToMileage(accumulatedDistance) : notApplicable);
			}
		}

		/// <summary>
		/// The accummulated distance of a journey in kms. The value of the
		/// accummulated distance is calculated during the formatting of
		/// journey instructions and is incremented by GetDistance.
		/// </summary>
		protected virtual string TotalKmDistance
		{
			get
			{
				return (accumulatedDistance > 0 ? ConvertMetresToKm(accumulatedDistance) : notApplicable);
			}
		}

        // Read/write property to display the find nearest car park link for a (non carpark) destination
        public bool ShowFindNearestCarParksLink
        {
            get { return showFindNearestCarParkLink; }
            set { showFindNearestCarParkLink = value; }
        }

		#endregion Properties
		
		#region Public methods

		/// <summary>
		/// Returns an ordered list of journey details. Each object in the
		/// list contains details for a single journey instruction. Each object
		/// is a string array of details (e.g, road name, distance, directions)
		/// The contents of the list and each string array element is
		/// determined by methods in subclasses that produce specific results
		/// dependant on purpose (e.g. output for web page, email, and so on).
		/// </summary>
		/// <returns>Returns an ordered list of journey instructions where each 
		/// element is a string array of details (e.g, road name, distance, directions)</returns>
		public virtual IList GetJourneyDetails() 
		{
			return processRoadJourney();
		}
		
		/// <summary>
		/// Returns culture specific strings that are used to identify
		/// the elements of the string arrays returned by GetJourneyDetails. 
		/// </summary>
		/// <returns>Heading labels identifying elements of the string arrays
		/// returned by GetJourneyDetails</returns>
		public abstract string[] GetDetailHeadings();
		
		/// <summary>
		/// Returns culture specific strings that are used to identify
		/// the elements of the string arrays returned by GetJourneyDetails. 
		/// </summary>
		/// <returns>Heading labels identifying elements of the string arrays
		/// returned by GetJourneyDetails</returns>
		//public abstract string[] GetFooterHeadings();
		public abstract object[] GetFooterHeadings();//FooterHeadings()
		//		{
		//			return ;
		//		}

		#endregion Public methods
		
		#region Protected methods

		/// <summary>
		/// Returns the current instruction step number. Getting the instruction step 
		/// number also increments it
		/// </summary>
		protected virtual string GetCurrentStepNumber()
		{
			string currentStepNumberString = stepNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			stepNumber++;
			return currentStepNumberString;
		}

		/// <summary>
		/// Called by the template method ProcessRoadJourney prior to 
		/// formatting. Initialises instance variables used during the
		/// formatting process.
		/// </summary>
		protected virtual void initFormatting() 
		{
			accumulatedDistance = 0;
			stepNumber = 1;
			currentArrivalTime = roadJourney.DepartDateTime;
		}
		
		/// <summary>
		/// The is a template method that controls the format process. It calls
		/// into hook methods to generate specific output defined by 
		/// subclasses.
		/// </summary>
		/// <returns>Returns an ordered list of journey instructions where each 
		/// element is a string array of details (e.g, road name, distance, directions)</returns>
		protected virtual IList processRoadJourney() 
		{
			ArrayList details = new ArrayList();

			#region Vantive 3868786
			bool returnExists = false;
			bool sameDayReturn = false;
			bool showCongestionCharge = true;

			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			bool itineraryExists = (itineraryManager.Length > 0);
			bool extendInProgress = itineraryManager.ExtendInProgress;
			bool showItinerary = (itineraryExists && !extendInProgress);

			if ( showItinerary )
			{
				returnExists = (itineraryManager.ReturnLength > 0);
				if(returnExists)
				{
					//Used to check if the Outward and Return Dates are the same
					if (itineraryManager.OutwardDepartDateTime().GetDifferenceDates(itineraryManager.ReturnDepartDateTime()) == 0)
					{
						sameDayReturn = true;
					}
				}
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = TDSessionManager.Current.JourneyResult;
				if(result != null) 
				{
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;
				}
				if(returnExists)
				{
					//Used to check if the Outward and Return Dates are the same
					if (journeyViewState.JourneyLeavingDateTime.GetDifferenceDates(journeyViewState.JourneyReturningDateTime) == 0)
					{
						sameDayReturn = true;
					}
				}
			}

			//Check if congestion charge cost should be shown for this journey
			if (outward)
			{
				showCongestionCharge = true;
			}
			else if ((journeyViewState.CongestionChargeAdded) && (sameDayReturn))
			{
				showCongestionCharge = false;
			}
			else
			{
				showCongestionCharge = true;
			}
			#endregion
														
			if ((roadJourney == null) || (roadJourney.Details.Length == 0))
			{
				return details;			
			}
			else
			{
				initFormatting();

				details.Add(addFirstDetailLine());
			
				for (int journeyDetailIndex=0; 
					journeyDetailIndex < roadJourney.Details.Length;
					journeyDetailIndex++) 
				{		
					details.Add(processRoadJourneyDetail(journeyDetailIndex, showCongestionCharge));
				}

				details.Add(addLastDetailLine());
		
				return details;
			}			
		}

        //protected abstract string AddContinueFor(RoadJourneyDetail detail, string routeText);
        protected abstract string AddContinueFor(RoadJourneyDetail detail,
            bool nextDetailHasJunctionExitJunction, string routeText);

        /// <summary>
        /// A hook method to append the limited access text, if any,
        /// to the existing instruction text 
        /// </summary>
        /// <param name="detail">The detail</param>
        /// <param name="routeText">Existing instruction text</param>
        /// <returns>Text with limited access text appended</returns>
        protected abstract string AddLimitedAccessText(RoadJourneyDetail detail, string routeText);
		
		/// <summary>
		/// A hook method called by processRoadJourney to process each road journey
		/// instruction. The returned string array should contain details for each 
		/// instruction (e.g, road name, distance, directions)
		/// </summary>
		/// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
		/// <returns>details for each instruction (e.g, road name, distance, directions)</returns>
		protected abstract object[] processRoadJourneyDetail(int journeyDetailIndex, bool showCongestionCharge);

		/// <summary>
		/// A hook method called by processRoadJourney to add the first element to the 
		/// ordered list of journey instructions. The returned string array
		/// should contain details for the first instruction.
		/// </summary>
		/// <returns>details for first instruction</returns>
		protected abstract object[] addFirstDetailLine();
		
		/// <summary>
		/// A hook method called by processRoadJourney to add the last element to the 
		/// ordered list of journey instructions. The returned string array
		/// should contain details for the last instruction.
		/// </summary>
		/// <returns>details for last instruction</returns>
		protected abstract object[] addLastDetailLine();
		
		/// <summary>
		/// Returns formatted string of the road name for the supplied
		/// instruction
		/// </summary>
		/// <param name="detail">Details of journey instruction</param>
		/// <returns>The road name</returns>
		protected virtual string FormatRoadName(RoadJourneyDetail detail)
		{
			string roadName = detail.RoadName;
			string roadNumber = detail.RoadNumber;
			string result = String.Empty;

			if ((roadName == null || roadName.Length == 0) &&
				(roadNumber == null || roadNumber.Length == 0)) 
			{
				return localRoad;
			}
				
			if(roadNumber != null && roadNumber.Length != 0)
			{
				result += roadNumber;
			}

			if(roadName != null && roadName.Length != 0)
			{
				// Check to see if road number was empty and if so
				// then need to bracket the road name.

				if(roadNumber != null && roadNumber.Length != 0)
				{
					result += space + "(" + roadName + ")";
				}
				else
				{
					// No road number exists so just concatinate the road name
					result += roadName;
				}
			}
			return result;
		}

		/// <summary>
		/// Returns formatted string of the road name for the supplied
		/// instruction.
		/// </summary>
		/// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
		/// <returns>The road name</returns>
		protected virtual string GetRoadName(int journeyDetailIndex)
		{
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];		
			string result = FormatRoadName(detail);
			return result;
		}

		protected abstract object[] addThinkSymbol();

		protected abstract object[] addTrafficWarning();


		/// <summary>
		/// Returns a formatted string (defined by the ConvertMetresToMileage
		/// method) of the distance for the current driving instruction. 
		/// </summary>
		/// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
		/// <returns>Formatted distance in miles</returns>
		protected virtual string GetDistance(int journeyDetailIndex, RoadUnitsEnum roadUnits)
		{
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

            if (detail.Distance >= 100)
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    return (ConvertMetresToMileage(detail.Distance));
                }
                else
                {
                    return (ConvertMetresToKm(detail.Distance));
                }
            }

            return dash;
		}

        /// <summary>
        /// Returns a speed in mph or kmph based on the road units
        /// </summary>
        /// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
        /// <returns>Formatted distance in miles</returns>
        protected virtual string GetSpeed(int journeyDetailIndex, RoadUnitsEnum roadUnits)
        {
            RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

            double result;

            try
            {
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    // Retrieve the conversion factor from the Properties Service.
                    double conversionFactor =
                        Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentUICulture.NumberFormat);

                    result = ((double)detail.Distance) * 3600 / (conversionFactor * detail.Duration);
                }
                else
                {
                    result = ((double)detail.Distance) * 3600 / (1000 * detail.Duration);
                }
            }
            catch
            {
                result = 0;
            }


            return string.Format("{0}{1}ph", result.ToString("F"), roadUnits == RoadUnitsEnum.Miles ? "m" : "km");
        }

		/// <summary>
		/// Returns a formatted string (defined by FormatDuration method)
		/// of the duration of the current driving instruction.
		/// </summary>
		/// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
		/// <returns>Formatted string of the duration.</returns>
		protected virtual string GetDuration(int journeyDetailIndex)
		{
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];
			return FormatDuration(detail.Duration);
		}

		/// <summary>
		/// Formats a duration (expressed in seconds) into a 
		/// string in the form "hh:mm". If the duration is
		/// less than 30 seconds then a "less 30 secs" string is returned.
		/// The returned time is rounded to the nearest minute.
		/// </summary>
		/// <param name="durationInSeconds">Duration in seconds.</param>
		/// <returns>Formatted string of the duration.</returns>
		protected virtual string FormatDuration(long durationInSeconds)
		{

			// Get the minutes
			double durationInMinutes = (double)durationInSeconds / 60.0;

			// Check to see if less than 30 seconds
			if( durationInMinutes / 60.0  < 1.00 &&
				durationInMinutes % 60.0 < 0.5 )
			{
				string secondsString = Global.tdResourceManager.GetString(
					"CarJourneyDetailsTableControl.labelDurationSeconds", TDCultureInfo.CurrentUICulture);

				return "< 30 " + secondsString;
			}
			else
			{
				// Duration is greater than 30 seconds

				// Round to the nearest minute
				durationInMinutes = Round(durationInMinutes);

				// Calculate the number of hours in the minute
				int hours = (int)durationInMinutes / 60;

				// Get the minutes (afer the hours has been subracted so always < 60)
				int minutes = (int)durationInMinutes % 60;
				
				return String.Format("{0:D2}:{1:D2}",hours,minutes);
				
			}
		}

		/// <summary>
		/// Returns a formatted string of the arrival time for the current
		/// driving instruction in the format defined by the method FormatTime.
		/// The time is calculated by adding the current journey
		/// instruction duration to an accumulated journey time.
		/// </summary>
		/// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
		/// <returns>Formatted arrival time</returns>
		protected virtual string GetArrivalTime(int journeyDetailIndex)
		{
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];
			//If it is a Stopover (unless a wait) We don't want to add the time

			if (detail.ferryEntry)
			{
				TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
				TDDateTime arrivalTime = currentArrivalTime.Subtract(span);
				return FormatTime(arrivalTime);

			}

			else if (detail.IsStopOver && !detail.wait)
			{
				return FormatTime(currentArrivalTime);
			}
			else
			{
				TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
				TDDateTime arrivalTime = currentArrivalTime;
				currentArrivalTime = currentArrivalTime.Add(span);
				return FormatTime(arrivalTime);
			}
		}

		protected virtual RoadJourneyDetailMapInfo GetMapInfo(int journeyDetailIndex)
		{
			RoadJourneyDetail currentDetail = roadJourney.Details[journeyDetailIndex];
			RoadJourneyDetail previousDetail = null;
			string test1 = currentDetail.RoadJourneyDetailMapInfo.firstToid;

			
			if (journeyDetailIndex == 0)
			{
				previousDetail = roadJourney.Details[journeyDetailIndex];
			}
			else
			{
				previousDetail = roadJourney.Details[journeyDetailIndex-1];
			
			}

			string test2 = previousDetail.RoadJourneyDetailMapInfo.lastToid;

			if (journeyDetailIndex == 0 || ! previousDetail.IsStopOver)
			{
				RoadJourneyDetailMapInfo mapInfo = new RoadJourneyDetailMapInfo(test2, test1);
				return mapInfo;
			
			}

			if (journeyDetailIndex == 1 && previousDetail.IsStopOver)
			{
				RoadJourneyDetailMapInfo mapInfo = new RoadJourneyDetailMapInfo(test2, test1);
				return mapInfo;

			}	

			else 
			{
				previousDetail = roadJourney.Details[journeyDetailIndex-2];
				string test3 = previousDetail.RoadJourneyDetailMapInfo.lastToid;
				RoadJourneyDetailMapInfo mapInfo = new RoadJourneyDetailMapInfo(test3, test1);
				return mapInfo;
			}
		}

        /// <summary>
        /// Returns an array of travel news incident ids matching to the roar journey detail
        /// specified by journeyDetailIndex.
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into roadJourney.Details for journey instruction</param>
        /// <returns>Array of travel news incidents affecting the current road journey detail</returns>
        protected virtual string[] GetMatchingTravelNewsIncidents(int journeyDetailIndex)
        {
            RoadJourneyDetail currentDetail = roadJourney.Details[journeyDetailIndex];

            return currentDetail.TravelNewsIncidentIDs.ToArray();
        }


		/// <summary>
		/// Returns a formatted string of the directions for the current
		/// driving instruction e.g. "Continue on to FOXHUNT GROVE".
		/// </summary>
		/// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
		/// <returns>Formatted string of the directions</returns>
		protected virtual string GetDirections(int journeyDetailIndex, bool showCongestionCharge)
		{
			//temp variables for this method
			string routeText = String.Empty;
	
			//assign the detail to be formatted as an instruction
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];	
			//last Detail, which is also needed for formatting logic			
			RoadJourneyDetail lastDetail = roadJourney.Details[roadJourney.Details.Length - 1];	
	
			bool nextDetailHasJunctionExitJunction = false;
			
			if (detail != lastDetail)
			{
				RoadJourneyDetail nextDetail = detail;
				nextDetail = roadJourney.Details[journeyDetailIndex + 1];
				nextDetailHasJunctionExitJunction = ((nextDetail.IsJunctionSection) && 
					((nextDetail.JunctionAction == JunctionType.Exit) || 
					(nextDetail.JunctionAction == JunctionType.Merge)));
			}

			//Returns specific text depending on the StopoverSection
			if(detail.IsStopOver)
			{
				if (detail.CongestionZoneEntry != null)
				{
					routeText = CongestionEntry(detail, showCongestionCharge);
				}
				if (detail.CongestionZoneExit != null)
				{
					routeText = CongestionExit(detail, showCongestionCharge);
				}
				if (detail.CongestionZoneEnd != null)
				{
					routeText = CongestionEnd(detail, showCongestionCharge);
				}
				if (detail.FerryCheckIn != null)
				{
					//We need to pass the previous detail so we can find out if it was 
					//an UndefinedWait or not (as this changes the instruction passed to the user)
					RoadJourneyDetail previousDetail = roadJourney.Details[journeyDetailIndex- 1];
					routeText = FerryEntry(detail, previousDetail);
				}
				if (detail.TollEntry != null)
				{
					routeText = TollEntry(detail);
				}
				if (detail.TollExit !=null)
				{
					routeText = TollExit(detail);
				}
				if (detail.viaLocation == true)
				{
					routeText = ViaLocation();
				}
				if(detail.FerryCheckOut == true)
				{
					routeText = FerryExit(detail);
				}
				if(detail.wait == true)
				{
					RoadJourneyDetail previousDetail = roadJourney.Details[journeyDetailIndex - 1];
					RoadJourneyDetail nextDetail = roadJourney.Details[journeyDetailIndex + 1];
					routeText = WaitForFerry(detail, previousDetail, nextDetail);
				}
				if(detail.undefindedWait == true)
				{
					RoadJourneyDetail previousDetail = roadJourney.Details[journeyDetailIndex - 1];
					RoadJourneyDetail nextDetail = roadJourney.Details[journeyDetailIndex + 1];
					routeText = UndefindedFerryWait(detail, previousDetail, nextDetail);
				}
			}
			else
			{				
				//check turns and format accordingly			
				if(detail.Roundabout)
					routeText = Roundabout(detail);
				else if(detail.ThroughRoute)
					routeText = ThroughRoute(detail);
				else
				{
					// Not a roundabout or a through route.  
					if(detail.Angle == TurnAngle.Continue)
						routeText = TurnAngleContinue(journeyDetailIndex);
					else if(detail.Angle == TurnAngle.Bear)
						routeText = TurnAngleBear(journeyDetailIndex);
					else if(detail.Angle == TurnAngle.Turn)
						routeText = TurnAngleTurn(journeyDetailIndex);					
				}
			
				//check if the current road journey detail is a motorway junction section
				if (detail.IsJunctionSection)
				{
					//add junction number to the instruction
					routeText = FormatMotorwayJunction(detail, routeText);
				}	
				
				//Add formatting place holder for where the road splits
				routeText += "{0}";					

				//check if place name was returned
				bool placeNameExists = false;
				if(detail.PlaceName != null && detail.PlaceName.Length > 0)
				{		
					if ((detail.JunctionAction == JunctionType.Entry) && detail.IsJunctionSection && 
						detail.Roundabout && (detail.Distance < immediateTurnDistance) && 
						nextDetailHasJunctionExitJunction)
					{
						//don't add place name
					}
					else
					{
						routeText = AddPlaceName(detail, routeText);	
					}
					placeNameExists = true;
				}

				//Add formatting place holder for where the road splits
				routeText += "{1}";

				if (detail.IsJunctionSection &&
					((detail.JunctionAction == JunctionType.Exit) || 
					(detail.JunctionAction == JunctionType.Merge)) &&					
					(detail.Distance > immediateTurnDistance) )					
				{
					//don't add continue for
				}
				else
				{
					//Add 'continue for' 
					routeText = AddContinueFor(detail,nextDetailHasJunctionExitJunction, routeText);
				}

                routeText = AddLimitedAccessText(detail, routeText);

				//Add formatting place holder for where the road splits
				routeText += "{2}";

				//check the next Detail providing we are not on the last detail already 
				
				if (detail != lastDetail)
				{	
					//next Detail, which is needed for formatting logic	
					RoadJourneyDetail nextDetail = roadJourney.Details[journeyDetailIndex + 1];	
				
					if (!((detail.JunctionAction == JunctionType.Exit) || 
						(detail.JunctionAction == JunctionType.Merge)))
					{
						//amend the current instruction if necessary
						routeText = CheckNextDetail(nextDetail, routeText);	
					}													
				}
				//Add formatting place holder for where the road splits
				routeText += "{3}";

				if (detail.CongestionLevel == true)
				{
                    if (displayCongestionSymbol)
                    {
                        routeText = AddCongestionSymbol(routeText);
                        highTrafficLevel = true;
                    }
				}

				// Add the distance to the running count
				accumulatedDistance += detail.Distance;

				//Add where the road splits to the appropriate place


				//Fille in the place holders for where the road splits for Motorway Entry
				if ((detail.JunctionAction == JunctionType.Entry) &&					
					(detail.IsJunctionSection) && (detail.RoadSplits))					
				{			
					if (detail.Roundabout)
					{
						//after place name
						if (placeNameExists && (detail.Distance > immediateTurnDistance))							
						{
							routeText = String.Format(routeText,
								String.Empty,space + whereRoadSplits,String.Empty,String.Empty);
						}
						//after jno2
						else if ((!placeNameExists) && (detail.Distance < immediateTurnDistance) &&
							nextDetailHasJunctionExitJunction)							
						{
							routeText = String.Format(routeText,
								String.Empty,String.Empty,String.Empty,space + whereRoadSplits);
						}						
						else routeText = String.Format(routeText,space + whereRoadSplits,String.Empty,
								 String.Empty,String.Empty);						
					}
					else
					{
						if (nextDetailHasJunctionExitJunction && (!placeNameExists) && 
							(detail.Distance < immediateTurnDistance))
						{
							routeText = String.Format(routeText,
								String.Empty,String.Empty,String.Empty,space + whereRoadSplits);
						}
						else routeText = String.Format(routeText,space + whereRoadSplits,
								 String.Empty,String.Empty,String.Empty);
					}
				}
				else routeText = String.Format(routeText,String.Empty,
						 String.Empty,String.Empty,String.Empty);
			}

			//return complete formatted instruction
			return routeText;
			
		}

		/// <summary>
		/// Return a congestion symbol next to the route text.
		/// </summary>
		/// <param name="routeText">Route directions</param>
		/// <returns>Congestion symbol</returns>
		protected virtual string AddCongestionSymbol (string routeText)
		{
            routeText += Global.tdResourceManager.GetString("CarCostingDetails.highTrafficSymbol");
                //"<img src=\"/web2/images/gifs/JourneyPlanning/roadexclamation.gif\" align=\"Middle\">";
			return routeText;
		}

        /// <summary>
        /// Return a high value motorway symbol next to the route text.
        /// </summary>
        /// <param name="routeText">Route directions</param>
        /// <returns></returns>
        protected virtual string AddHighValueMotorwaySymbol(string routeText)
        {
            routeText += space + Global.tdResourceManager.GetString("CarJourneyDetailsTableControl.HighValueMotorway.Image");
            return routeText;
        }

        /// <summary>
        /// Returns the distance in metres appended to the route text (if the isCJPUser flag is set)
        /// </summary>
        /// <param name="routeText"></param>
        /// <returns></returns>
        protected virtual string AddDirectionDistanceMetres(string routeText, int journeyDetailIndex)
        {
            if (this.isCJPUser)
            {
                RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

                string distanceMetres = "<span class=\"txterror\"> (" + detail.Distance + " metres)</span>";

                routeText += distanceMetres;
            }

            return routeText;
        }

		/// <summary>
		/// Return a formatted string by converting the given metres
		/// to a mileage
		/// </summary>
		/// <param name="metres">Metres to convert</param>
		/// <returns>Formatted string</returns>
		protected virtual string ConvertMetresToMileage(int metres)
		{
			// Retrieve the conversion factor from the Properties Service.
			double conversionFactor =
				Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentUICulture.NumberFormat);

			double result = (double)metres / conversionFactor;

            string distanceFormat = GetDistanceFormat();
            
			// Return the result
			return result.ToString(distanceFormat, TDCultureInfo.CurrentUICulture.NumberFormat);
		}

		/// <summary>
		/// Return a formatted string by converting the given metres
		/// to km.
		/// </summary>
		/// <param name="metres">Metres to convert</param>
		/// <returns>Formatted string</returns>
		protected virtual string ConvertMetresToKm(int metres)
		{
			double result = (double)metres / 1000;

            string distanceFormat = GetDistanceFormat();

			// Return the result
			return result.ToString(distanceFormat, TDCultureInfo.CurrentUICulture.NumberFormat);
		}

		/// <summary>
		/// Formats the given time for display.
		/// Seconds are rounded to the nearest minute
		/// </summary>
		/// <param name="time">Time to format</param>
		/// <returns>Formatted string of the time in the form hh:mm</returns>
		protected virtual string FormatTime(TDDateTime time)
		{			
			if (time.Second >= 30) 
			{
				return time.AddMinutes(1).ToString("HH:mm");
			} 
			else 
			{
				return time.ToString("HH:mm");
			}
		}

		/// <summary>
		/// Formats the given time and date for display.
		/// Seconds are rounded to the nearest minute
		/// </summary>
		/// <param name="time">Time to format</param>
		/// <returns>Formatted string of the time in the form hh:mm (dd/mm/yy)</returns>
		protected virtual string FormatDateTime(TDDateTime time)
		{			
			if (time.Second >= 30) 
			{
				return time.AddMinutes(1).ToString ("HH:mm (dd/MM)");
			} 
			else 
			{
				return time.ToString("HH:mm (dd/MM)");
			}
		}

		/// <summary>
		/// Generates the route text for the given RoadJourney where
		/// the RoadJourney type is "Roundabout". This method assumes
		/// that the type is "Roundabout".
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string Roundabout(RoadJourneyDetail detail)
		{
			string routeText = String.Empty;
			string thisRoad = FormatRoadName(detail);			

			switch(detail.TurnCount)
			{
				case 1 : routeText = roundaboutExitOne;		break;
				case 2 : routeText = roundaboutExitTwo;		break;
				case 3 : routeText = roundaboutExitThree;	break;
				case 4 : routeText = roundaboutExitFour;	break;
				case 5 : routeText = roundaboutExitFive;	break;
				case 6 : routeText = roundaboutExitSix;		break;
				case 7 : routeText = roundaboutExitSeven;	break;
				case 8 : routeText = roundaboutExitEight;	break;
				case 9 : routeText = roundaboutExitNine;	break;
				case 10: routeText = roundaboutExitTen;		break;
			}

			routeText += space + thisRoad;
		
			if (detail.RoadSplits && !detail.IsJunctionSection)
			{
				routeText += space + whereRoadSplits;						
			}

			return routeText;
		}
		
		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail type is "ThroughRoute". This method assumes
		/// that the type is "ThroughRoute".
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string ThroughRoute(RoadJourneyDetail detail)
		{
			string roadName = FormatRoadName(detail);
			string routeText = String.Empty;

			//add "Follow..." to start of instruction			
			routeText = follow + space + roadName;	
		
			if (detail.RoadSplits)
			{
				routeText += space + whereRoadSplits;						
			}
			return routeText;
		}
		
		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'TollEntry'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string TollEntry(RoadJourneyDetail detail)
		{
			StringBuilder routeText = new StringBuilder(string.Empty); 

			//Convert TollCost value to pounds & pence
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);

			//display company url as hyerplink if it exists
			if (detail.CompanyUrl.Length!=0 && print)
			{
				//add "Toll:" to start of instruction
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append("<a href=\"");
				routeText.Append("http://");
				routeText.Append(detail.CompanyUrl.Trim());
				routeText.Append("\" target=\"_blank\">");
				routeText.Append(detail.TollEntry);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
				routeText.Append("</a>");
			}
			else
			{
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append(detail.TollEntry);
			}

            if (displayTollCharge)
            {
                //If price in pence is less than zero return the route text, otherwise
                //return route text inclusive of the charge.
                if (pence < 0)
                {
                    //IR2499 added in message when charge is unknown "Charge: Not Available"
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(notAvailable);
                    routeText.Append("</b>");
                }
                else if (pence == 0)
                {
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append("</b>");
                }
                else
                {
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append("</b>");
                }
            }

			return routeText.ToString();
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'TollEntry'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string TollExit(RoadJourneyDetail detail)
		{
			StringBuilder routeText = new StringBuilder(string.Empty); 

			//Convert TollCost value to pounds & pence
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);

			//display company url as hyerplink if it exists
			if (detail.CompanyUrl.Length!=0 && print)
			{
				//add "Toll:" to start of instruction	
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append("<a href=\"");
				routeText.Append("http://");
				routeText.Append(detail.CompanyUrl.Trim());
				routeText.Append("\" target=\"_blank\">");
				routeText.Append(detail.TollExit);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
				routeText.Append("</a>");
			}
			else
			{
				routeText.Append(toll);
				routeText.Append(space);
				routeText.Append(detail.TollExit);
			}

            if (displayTollCharge)
            {
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(charge);
            }

			return routeText.ToString();
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'FerryEntry'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string FerryEntry(RoadJourneyDetail detail, RoadJourneyDetail PreviousDetail)
		{

			bool previousInstruction = PreviousDetail.undefindedWait;

			StringBuilder routeText  = new StringBuilder(string.Empty);
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);
			
			TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
			TDDateTime arrivalTime = currentArrivalTime;
			currentArrivalTime = currentArrivalTime.Add(span);
			string time = FormatDateTime(currentArrivalTime);
			
			//add "Board:" to start of instruction			
			//If previous instruction was an undefinedWait we don't want to display a time(as we don't know it)
			if (previousInstruction)
			{
				//display company url as hyerplink if it exists
				if (detail.CompanyUrl.Length !=0 && print)
				{
					routeText.Append(board);
					routeText.Append(space);
					routeText.Append("<a href=\"");
					routeText.Append("http://");
					routeText.Append(detail.CompanyUrl.Trim());
					routeText.Append("\" target=\"_blank\">");
					routeText.Append(detail.FerryCheckIn);
                    routeText.Append(space);
                    routeText.Append(openNewWindowImageUrl);
					routeText.Append("</a>");
				}
				else
				{
					routeText.Append(board);
					routeText.Append(space);
					routeText.Append(detail.FerryCheckIn);
				}
			}
			else
			{
				//display company url as hyerplink if it exists
				if (detail.CompanyUrl.Length !=0 && print)
				{
					routeText.Append(board);
					routeText.Append(space);
					routeText.Append("<a href=\"");
					routeText.Append("http://");
					routeText.Append(detail.CompanyUrl.Trim());
					routeText.Append("\" target=\"_blank\">");
					routeText.Append(detail.FerryCheckIn);
                    routeText.Append(space);
                    routeText.Append(openNewWindowImageUrl);
					routeText.Append("</a>");
					routeText.Append(space);
					routeText.Append(departingAt);
					routeText.Append(space);
					routeText.Append(time);
				}
				else
				{
					routeText.Append(board);
					routeText.Append(space);
					routeText.Append(detail.FerryCheckIn);
					routeText.Append(space);
					routeText.Append(departingAt);
					routeText.Append(space);
					routeText.Append(time);
				}
			}
			
			//If price in pence is less than zero return the route text, otherwise
			//return route text inclusive of the charge.
			if (pence < 0)
			{
				//IR2499 added in message when charge is unknown "Charge: Not Available"
				routeText.Append(";");
				routeText.Append(space);
				routeText.Append("<b>");
				routeText.Append(charge);
				routeText.Append(notAvailable);
				routeText.Append("</b>");
			}
			else if (pence == 0)
			{
				//IR2499 added in message when charge is unknown "Charge: Not Available"
				routeText.Append(";");
				routeText.Append(space);
				routeText.Append("<b>");
				routeText.Append(charge);
				routeText.Append("£0.00");
				routeText.Append("</b>");		
			}
			else
			{
				routeText.Append(";");
				routeText.Append(space);
				routeText.Append("<b>");
				routeText.Append(charge);
				routeText.Append(cost);
				routeText.Append("</b>");
			}
			return routeText.ToString();
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'CongestionEntry'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string CongestionEntry(RoadJourneyDetail detail, bool showCongestionCharge)
		{
			StringBuilder routeText = new StringBuilder(string.Empty); 
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);

			//display company name as hyperplink if it exists
			if (detail.CompanyUrl.Length !=0 && print)
			{
				routeText.Append(enter);
				routeText.Append(space);
				routeText.Append("<a href=\"");
				routeText.Append("http://");
				routeText.Append(detail.CompanyUrl.Trim());
				routeText.Append("\" target=\"_blank\">");
				routeText.Append(detail.CongestionZoneEntry);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
				routeText.Append("</a>");
			}
			else
			{
				routeText.Append(enter);
				routeText.Append(space);
				routeText.Append(detail.CongestionZoneEntry);
			}

			// If there is a toll charge
			// Amended for IR 2639 - Check if we already have this C Charge in ViewState or 
			// there is a cost and showCongestionCharge
			if ((detail.TollCost > 0) && ((showCongestionCharge) || 
				(!journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl))))
			{
				//Viewstate contains this C Charge and we don't want to show it
				if(journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
				{
					// No toll charge for this day/time
					routeText.Append(fullstop);
					routeText.Append(space);
                    
                    if (displayCongestionCharge)
                    {
                        routeText.Append("<b>");
                        routeText.Append(charge);
                        routeText.Append("£0.00");
                        routeText.Append("</b>");
                        routeText.Append(space);
                    }
					
                    routeText.Append(certainTimesNoCharge);
				}
				else

					//We want to show it
				{
					journeyViewState.VisitedCongestionCompany.Add(detail.CompanyUrl);
					routeText.Append(fullstop);
					routeText.Append(space);

                    if (displayCongestionCharge)
                    {
                        routeText.Append("<b>");
                        routeText.Append(charge);
                        routeText.Append(cost);
                        routeText.Append("</b>");
                        routeText.Append(space);
                        routeText.Append(certainTimes);
                    }
                    else
                    {
                        routeText.Append(certainTimesNoCharge);
                    }

					if (outward)
						journeyViewState.CongestionChargeAdded = true;

				}
			
				
			}
																			
			else if ((detail.TollCost == 0) || (!showCongestionCharge))
			{
				// No toll charge for this day/time
				routeText.Append(fullstop);
				routeText.Append(space);

                if (displayCongestionCharge)
                {
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append("</b>");
                    routeText.Append(space);
                }

				routeText.Append(certainTimesNoCharge);
			}
			else
			{
                if (displayCongestionCharge)
                {
                    //in the event the charge is unavailable - IR2499
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(notAvailable);
                    routeText.Append("</b>");
                }
			}

            //display company name as hyperplink if it exists
            string ccURL = detail.CompanyUrl.ToLower();
            if (ccURL.StartsWith("www.tfl.gov.uk") && londonCCzoneExtraTextVisible)
            {
                routeText.Append(londonCCAdditionalText);
            }

			return routeText.ToString();
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'CongestionExit'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string CongestionExit(RoadJourneyDetail detail, bool showCongestionCharge)
		{
			StringBuilder routeText  = new StringBuilder(string.Empty);
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);

			if (detail.CompanyUrl.Length !=0 && print)
			{
				//add "Exit" to start of instruction						
				routeText.Append(exit);
				routeText.Append(space);
				routeText.Append("<a href=\"");
				routeText.Append("http://");
				routeText.Append(detail.CompanyUrl.Trim());
				routeText.Append("\" target=\"_blank\">");
				routeText.Append(detail.CongestionZoneExit);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
				routeText.Append("</a>");
			}
			else
			{
				//add "Exit" to start of instruction						
				routeText.Append(exit);
				routeText.Append(space);
				routeText.Append(detail.CongestionZoneExit);
			}


			// If there is a toll charge
			// Amended for IR 2639 - Check if we already have this C Charge in ViewState or 
			// there is a cost and showCongestionCharge
			if ((detail.TollCost > 0) && ((showCongestionCharge) || 
				(!journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl))))
			{
				//Viewstate contains this C Charge and we don't want to show it
				if(journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
				{
					// No toll charge for this day/time
					routeText.Append(fullstop);
					routeText.Append(space);

                    if (displayCongestionCharge)
                    {
                        routeText.Append("<b>");
                        routeText.Append(charge);
                        routeText.Append("£0.00");
                        routeText.Append("</b>");
                        routeText.Append(space);
                    }

					routeText.Append(certainTimesNoCharge);
				}
				else
					//We want to show it
				{
					journeyViewState.VisitedCongestionCompany.Add(detail.CompanyUrl);
					routeText.Append(fullstop);
					routeText.Append(space);

                    if (displayCongestionCharge)
                    {
                        routeText.Append("<b>");
                        routeText.Append(charge);
                        routeText.Append(cost);
                        routeText.Append("</b>");
                        routeText.Append(space);
                        routeText.Append(certainTimes);
                    }
                    else
                    {
                        routeText.Append(certainTimesNoCharge);
                    }

					if (outward)
						journeyViewState.CongestionChargeAdded = true;
					
				}
			}										
			else if ((detail.TollCost == 0) || (!showCongestionCharge))
			{
				// No toll charge, so dont want to display the charge text
			}
			else
			{
                if (displayCongestionCharge)
                {
                    //in the event the charge is unavailable - IR2499
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(notAvailable);
                    routeText.Append("</b>");
                }
			}

			return routeText.ToString();

		}


		/// <summary>
		/// 
		/// THIS needs to be activated when ATKINS CJP changes in place
		/// 
		/// 
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'CongestionEnd'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string CongestionEnd(RoadJourneyDetail detail, bool showCongestionCharge)
		{
			StringBuilder routeText  = new StringBuilder(string.Empty);
			decimal pence = (Convert.ToDecimal(detail.TollCost))/100;
			string cost = string.Format("{0:C}", pence);

			if (detail.CompanyUrl.Length !=0 && print)
			{
				//add "End" to start of instruction						
				routeText.Append(end);
				routeText.Append(space);
				routeText.Append("<a href=\"");
				routeText.Append("http://");
				routeText.Append(detail.CompanyUrl.Trim());
				routeText.Append("\" target=\"_blank\">");
				routeText.Append(detail.CongestionZoneEnd);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
				routeText.Append("</a>");
			}
			else
			{
				//add "End" to start of instruction						
				routeText.Append(end);
				routeText.Append(space);
				routeText.Append(detail.CongestionZoneEnd);
			}


			// If there is a toll charge
			// Amended for IR 2639 - Check if we already have this C Charge in ViewState or 
			// there is a cost and showCongestionCharge
			if ((detail.TollCost > 0) && ((showCongestionCharge) || 
				(!journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl))))
			{
				//Viewstate contains this C Charge and we don't want to show it
				if(journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
				{
					// No toll charge for this day/time
					routeText.Append(fullstop);
					routeText.Append(space);

                    if (displayCongestionCharge)
                    {
                        routeText.Append("<b>");
                        routeText.Append(charge);
                        routeText.Append("£0.00");
                        routeText.Append("</b>");
                        routeText.Append(space);
                    }

					routeText.Append(certainTimesNoCharge);
				}
				else
					//We want to show it
				{
					journeyViewState.VisitedCongestionCompany.Add(detail.CompanyUrl);
					routeText.Append(fullstop);
					routeText.Append(space);

                    if (displayCongestionCharge)
                    {
                        routeText.Append("<b>");
                        routeText.Append(charge);
                        routeText.Append(cost);
                        routeText.Append("</b>");
                        routeText.Append(space);
                        routeText.Append(certainTimes);
                    }
                    else
                    {
                        routeText.Append(certainTimesNoCharge);
                    }

					if (outward)
						journeyViewState.CongestionChargeAdded = true;
					
				}
			}										
			else if ((detail.TollCost == 0) || (!showCongestionCharge))
			{
				// No toll charge, so dont want to display the charge text
			}
			else
			{
                if (displayCongestionCharge)
                {
                    //in the event the charge is unavailable - IR2499
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(notAvailable);
                    routeText.Append("</b>");
                }
			}

			return routeText.ToString();

		}








		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'ViaLocation'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string ViaLocation()
		{
			string routeText = String.Empty;

			//add "Arrive at" to start of instruction			
			routeText = viaArriveAt + space + "<b>" + roadJourney.RequestedViaLocation.Description + "</b>";
			return routeText;
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'FerryExit'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string FerryExit(RoadJourneyDetail detail)
		{
			TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
			currentArrivalTime = currentArrivalTime.Add(span);

			string routeText = String.Empty;
			
			routeText= leaveFerry;
			return routeText;
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'Wait'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string WaitForFerry(RoadJourneyDetail detail,  RoadJourneyDetail previousDetail, RoadJourneyDetail nextDetail)
		{
			string routeText = String.Empty;

			if (nextDetail.RoadNumber == "FERRY" && previousDetail.RoadNumber == "FERRY" )
			{
				routeText = IntermediateFerryWait;
			}
			else
			{
				if (previousDetail.FerryCheckOut)
				{
					routeText = WaitAtTerminal;
				}
				else
				{
					routeText = FerryWait; 
				}
			}
			return routeText;
		}

		/// <summary>
		/// Generates the route text for the given RoadJourneyDetail where
		/// the RoadJourneyDetail is a StopoverSection of type 'UndefindedWait'.
		/// </summary>
		/// <param name="detail">Road Journey Detail to generate route text for.</param>
		/// <returns>Route text.</returns>
		protected virtual string UndefindedFerryWait(RoadJourneyDetail detail, RoadJourneyDetail previousDetail, RoadJourneyDetail nextDetail)
		{
			string routeText = String.Empty;

			if (nextDetail.RoadNumber == "FERRY" && previousDetail.RoadNumber == "FERRY" )
			{
				routeText = IntermediateFerryWait;
			}
			else
			{
				if (previousDetail.FerryCheckOut)
				{
					routeText = WaitAtTerminal;
				}
				else
				{
					routeText = UnspecifedFerryWait; 
				}
			}
			return routeText;
		}



		/// <summary>
		/// Generate the text where the turn angle of the given
		/// RoadJourneyDetail is "Continue". This method assumes that
		/// the turn angle for the given detail is "Continue".
		/// </summary>
		/// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
		/// <returns>Route text.</returns>
		protected virtual string TurnAngleContinue(int journeyDetailIndex)
		{
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

			string nextRoad = FormatRoadName(detail);
			string routeText = String.Empty;
			bool addedWhereRoadSplits = false;

			
			//When the CJP produces a JunctionDriveSection of type Merge (or Exit) 
			//without a junction number (regardless of whether the junction is indicated as ambiguous) 
			//for a Motorway junction (including slip roads) then ignore the turn count 
			//and the immediate parameter. 			
			if (detail.JunctionDriveSectionWithoutJunctionNo &&				
				(detail.JunctionAction == JunctionType.Exit || detail.JunctionAction == JunctionType.Merge))
			{
				string straightOnInstruction = 
					((detail.PlaceName == null || detail.PlaceName.Length == 0) && 
					(detail.Distance > immediateTurnDistance) && detail.RoadSplits) ? 					
						String.Empty : space + straightOn;

				if (detail.Direction == TurnDirection.Continue)
					routeText = follow + space + nextRoad + straightOnInstruction;						
				else if (detail.Direction == TurnDirection.Right)
					routeText = follow + space + nextRoad + straightOnInstruction;
				else if (detail.Direction == TurnDirection.Left)
					routeText = follow + space + nextRoad + straightOnInstruction;						
			}
			else if (!detail.IsJunctionSection)
			{
				if ((detail.Direction == TurnDirection.Continue) || 
					(detail.Direction == TurnDirection.Left) ||
					(detail.Direction == TurnDirection.Right) )
				{

					if ((detail.PlaceName == null || detail.PlaceName.Length == 0) && 
						(detail.Distance > immediateTurnDistance) && detail.RoadSplits)
					{
						routeText = follow + space + nextRoad; 
					}
					else
					{
						routeText = follow + space + nextRoad + space + straightOn; 

					}
				}
				else if(detail.Direction == TurnDirection.MiniRoundaboutContinue)
				{
					routeText = atMiniRoundabout + follow.ToLower(CultureInfo.CurrentCulture) + 
						space + nextRoad + space + straightOn;
				}
				else 
				{
					// Special case if no place name, < immediate distance and road splits
					if ((detail.PlaceName == null || detail.PlaceName.Length == 0) &&
						(detail.Distance < immediateTurnDistance) && detail.RoadSplits)
					{
						if(detail.Direction == TurnDirection.MiniRoundaboutLeft)
						{						
							routeText = leftMiniRoundabout2 + space + whereRoadSplits + space + onTo + space + nextRoad;
						}
						else if(detail.Direction == TurnDirection.MiniRoundaboutRight)
						{						
							routeText = rightMiniRoundabout2 + space + whereRoadSplits + space + onTo + space + nextRoad;
						}
						else if(detail.Direction == TurnDirection.MiniRoundaboutReturn)
						{						
							routeText = uTurnMiniRoundabout2 + space + whereRoadSplits + space + onTo + space + nextRoad;
						}
						// Set flag so dont add "where road splits" again at end of string
						addedWhereRoadSplits = true;
					}
					else
					{
						if(detail.Direction == TurnDirection.MiniRoundaboutLeft)
						{						
							routeText = leftMiniRoundabout + space + nextRoad;
						}
						else if(detail.Direction == TurnDirection.MiniRoundaboutRight)
						{
							routeText =
								rightMiniRoundabout + space + nextRoad;
						}
						else if(detail.Direction == TurnDirection.MiniRoundaboutReturn)
						{
							routeText =
								uTurnMiniRoundabout + space + nextRoad;
						}	

					}
				}
			}
			else if(detail.IsJunctionSection)
			{
				//Motorway entry 
				if ((detail.JunctionAction == JunctionType.Entry) &&
					(detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
				{
					routeText = join + space + nextRoad;
					return routeText; //without where the road splits
				}

				if (detail.Direction == TurnDirection.Continue)
				{
					routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn : 
						join + space + nextRoad;
				}
				else if (detail.Direction == TurnDirection.Right)
				{
					routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn : 
						join + space + nextRoad;
				}
				else if (detail.Direction == TurnDirection.Left)				
				{
					routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn : 
						join + space + nextRoad;
				}
				else if(detail.Direction == TurnDirection.MiniRoundaboutContinue)
				{
					routeText = (detail.RoadSplits) ? 
						atMiniRoundabout + follow.ToLower(CultureInfo.CurrentCulture) + 
						space + nextRoad + space + straightOn : 
						continueMiniRoundabout + space + nextRoad;
				}
				else if(detail.Direction == TurnDirection.MiniRoundaboutLeft)
				{						
					routeText = leftMiniRoundabout + space + nextRoad;
				}
				else if(detail.Direction == TurnDirection.MiniRoundaboutRight)
				{
					routeText = rightMiniRoundabout + space + nextRoad;
				}
				else if(detail.Direction == TurnDirection.MiniRoundaboutReturn)
				{
					routeText = uTurnMiniRoundabout + space + nextRoad;
				}

				

			}
			//if detail does not include a motorway junction

			if (detail.RoadSplits && !addedWhereRoadSplits)
			{
				routeText += space + whereRoadSplits;						
			}
			
			return routeText;
		}

		/// <summary>
		/// Generates the route text for the given RoadJourney where
		/// the TurnAngle for the given RoadJourney is "Bear".  This method
		/// assumes that the TurnAngle is "Bear".
		///
		/// Note that for "counted turns" (TurnCount 1 - 4) the text is actually
		/// "Take the ..." which can be used for both "bear" and "turn".
		/// </summary>
		/// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
		/// <returns>Route text.</returns>
		protected virtual string TurnAngleBear(int journeyDetailIndex)
		{
			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];
			
			int previousDistance = 0;
			if (journeyDetailIndex > 0) 
			{
				previousDistance = roadJourney.Details[journeyDetailIndex-1].Distance;
			};

			string nextRoad = FormatRoadName(detail);
			string routeText = String.Empty;

			//When the CJP produces a JunctionDriveSection of type Merge (or Exit) 
			//without a junction number (regardless of whether the junction is indicated as ambiguous) 
			//for a Motorway junction (including slip roads) then ignore the turn count 
			//and the immediate parameter. 
			if (detail.JunctionDriveSectionWithoutJunctionNo &&								
				(detail.JunctionAction == JunctionType.Exit || detail.JunctionAction == JunctionType.Merge))
			{
				if (detail.Direction == TurnDirection.Continue)
				{
					string straightOnInstruction = space + straightOn;

					if (detail.Direction == TurnDirection.Continue)
						routeText = follow + space + nextRoad + straightOnInstruction;						
					else if (detail.Direction == TurnDirection.Right)
						routeText = follow + space + nextRoad + straightOnInstruction;
					else if (detail.Direction == TurnDirection.Left)
						routeText = follow + space + nextRoad + straightOnInstruction;
				}
				else 
				{
					if(detail.Direction == TurnDirection.Left)
						routeText = bearLeft + space + nextRoad;
					else if(detail.Direction == TurnDirection.Right)
						routeText = bearRight + space + nextRoad;				
				}
	
				if (detail.RoadSplits)
					routeText += space + whereRoadSplits;

				return routeText;	
			}						

			// Check to see if this detail is a junction drive section
			if(detail.IsJunctionSection)
			{
				if(detail.Direction == TurnDirection.Left)
				{
					//Motorway entry 
					if ((detail.JunctionAction == JunctionType.Entry) &&
						(detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
					{
						routeText = bearLeftToJoin;
					}
					
				}
				else if(detail.Direction == TurnDirection.Right)
				{
					//Motorway entry 
					if ((detail.JunctionAction == JunctionType.Entry) &&
						(detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
					{
						routeText = bearRightToJoin;
					}
					
				}
				else if(detail.Direction == TurnDirection.MiniRoundaboutLeft)
					routeText = leftMiniRoundabout;
				else if(detail.Direction == TurnDirection.MiniRoundaboutRight)
					routeText = rightMiniRoundabout;
				else if (detail.Direction == TurnDirection.Continue)
					//check flag for ambiguous junction
					routeText = (detail.RoadSplits) ? continueString : join;
				else if (detail.Direction == TurnDirection.MiniRoundaboutReturn)
					routeText = uTurnMiniRoundabout;
					
				// Append the next road to the route text
				routeText += space + nextRoad;
			}
				//this detail is a drive section
			else
			{
				if(detail.Direction == TurnDirection.Left)
				{
					switch(detail.TurnCount)
					{
						case 1 :
							
							// Check to see if the distance of this leg is less than the
							// immediate turn distance.
							if (journeyDetailIndex > 0) 
							{
								if(previousDistance < immediateTurnDistance)
									routeText = immediatelyBearLeft;
								else
									routeText = turnLeftOne;
							} 
							else 
							{
								routeText = turnLeftOne;
							}

							break;
				
						case 2 : routeText = turnLeftTwo;	break;
						case 3 : routeText = turnLeftThree;	break;
						case 4 : routeText = turnLeftFour;	break;

						default: routeText = bearLeft; break;
					}
				}
				else if(detail.Direction == TurnDirection.Right)
				{
					switch(detail.TurnCount)
					{
						case 1 :
							
							// Check to see if the distance of this leg is less than the
							// immediate turn distance.
							if (journeyDetailIndex > 0) 
							{
								if(previousDistance < immediateTurnDistance)
									routeText = immediatelyBearRight;
								else
									routeText = turnRightOne;
							} 
							else 
							{
								routeText = turnRightOne;
							}
							break;
				
						case 2 : routeText = turnRightTwo;		break;
						case 3 : routeText = turnRightThree;	break;
						case 4 : routeText = turnRightFour;		break;

						default: routeText = bearRight; break;
					}
				}
				else if (detail.Direction == TurnDirection.MiniRoundaboutContinue)
					routeText = atMiniRoundabout + ChangeFirstCharacterCapitalisation(follow, false);
				else if(detail.Direction == TurnDirection.MiniRoundaboutLeft)
					routeText = leftMiniRoundabout;
				else if(detail.Direction == TurnDirection.MiniRoundaboutRight)
					routeText = rightMiniRoundabout;
				else if (detail.Direction == TurnDirection.Continue)
				{
					if ((detail.PlaceName == null || detail.PlaceName.Length == 0) && 
						(detail.Distance > immediateTurnDistance) && detail.RoadSplits)
					{
						routeText = follow + space + to;
					}
					else
					{
						routeText = follow;
					}
				}
				else if (detail.Direction == TurnDirection.MiniRoundaboutReturn)
					routeText = uTurnMiniRoundabout;
					
				// Append the next road to the route text
				routeText += space + nextRoad;	
		
				if ((detail.Direction == TurnDirection.Continue)|| 
					(detail.Direction == TurnDirection.MiniRoundaboutContinue))					
					routeText += space + straightOn;

				if (detail.RoadSplits)
				{
					if ((detail.TurnCount > 4) && 
						((detail.Direction == TurnDirection.Left) || (detail.Direction == TurnDirection.Right)))
						routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits,true) + space + 
							ChangeFirstCharacterCapitalisation(routeText,false);
					else routeText += space + whereRoadSplits;						
				}
				
			}



			return routeText;
		}		

		/// <summary>
		/// Generates the route text for the given RoadJourney where
		/// the TurnAngle for the given RoadJourney is "Turn".  This method
		/// assumes that the TurnAngle is "Turn".
		///
		/// Note that for "counted turns" (TurnCount 1 - 4) the text is actually
		/// "Take the ..." which can be used for both "bear" and "turn".
		/// </summary>
		/// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
		/// <returns>Route text.</returns>
		protected virtual string TurnAngleTurn(int journeyDetailIndex)
		{

			RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

			int previousDistance = 0;
			if (journeyDetailIndex > 0) 
			{
				previousDistance = roadJourney.Details[journeyDetailIndex-1].Distance;
			};

			string routeText = String.Empty;
			string nextRoad = FormatRoadName(detail);

			//When the CJP produces a JunctionDriveSection of type Merge (or Exit) 
			//without a junction number (regardless of whether the junction is indicated as ambiguous) 
			//for a Motorway junction (including slip roads) then ignore the turn count 
			//and the immediate parameter. 
			if (detail.JunctionDriveSectionWithoutJunctionNo &&								
				(detail.JunctionAction == JunctionType.Exit || detail.JunctionAction == JunctionType.Merge))
			{
				if (detail.Direction == TurnDirection.Continue)
				{
					string straightOnInstruction = space + straightOn;

					if (detail.Direction == TurnDirection.Continue)
						routeText = follow + space + nextRoad + straightOnInstruction;						
					else if (detail.Direction == TurnDirection.Right)
						routeText = follow + space + nextRoad + straightOnInstruction;
					else if (detail.Direction == TurnDirection.Left)
						routeText = follow + space + nextRoad + straightOnInstruction;

					if (detail.RoadSplits)
						routeText += space + whereRoadSplits;
				}
				else 
				{
					if(detail.Direction == TurnDirection.Left)
						routeText = turnLeftInDistance + space + nextRoad;
					else if(detail.Direction == TurnDirection.Right)
						routeText = turnRightInDistance + space + nextRoad;	
			
					if (detail.RoadSplits)
						routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits,true) + space + 
							ChangeFirstCharacterCapitalisation(routeText,false);
				}
	
				

				return routeText;	
			}
			
			// Check to see if this detail is a junction drive section
			if(detail.IsJunctionSection)
			{
				if( detail.Direction == TurnDirection.Left )
				{
					//Motorway entry 
					if ((detail.JunctionAction == JunctionType.Entry) &&
						(detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
					{
						routeText = turnLeftToJoin;
					}
					
				}
				else if( detail.Direction == TurnDirection.Right )
				{
					//Motorway entry 
					if ((detail.JunctionAction == JunctionType.Entry) &&
						(detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
					{
						routeText = turnRightToJoin;
					}
				}
				else if( detail.Direction == TurnDirection.MiniRoundaboutLeft )
				{
					routeText = leftMiniRoundabout;
				}
				else if( detail.Direction == TurnDirection.MiniRoundaboutRight )
				{
					routeText = rightMiniRoundabout;
				}
				else if( detail.Direction == TurnDirection.MiniRoundaboutReturn )
				{
					routeText = uTurnMiniRoundabout;
				} 
				else if( detail.Direction == TurnDirection.MiniRoundaboutContinue )
				{
					routeText = (detail.RoadSplits) ? 
						atMiniRoundabout + follow.ToLower(CultureInfo.CurrentCulture) + 
						space + nextRoad + space + straightOn : 
						continueString;
				} 
				else if( detail.Direction == TurnDirection.Continue )
				{
					routeText = continueString;
				} 

				// Append the next road to the route text.
				routeText += space + nextRoad;
			}
				//this detail is a drive section
			else
			{

				if( detail.Direction == TurnDirection.Left )
				{
					switch(detail.TurnCount)
					{
						case 1 :
						
							// Check to see if the distance of this leg is less than the
							// immediate turn distance.
							if (journeyDetailIndex > 0) 
							{
								if(previousDistance < immediateTurnDistance)
									routeText = immediatelyTurnLeftOnto;
								else
									routeText = turnLeftOne;
							} 
							else 
							{
								routeText = turnLeftOne;
							}

							break;

						case 2 : routeText = turnLeftTwo;	break;
						case 3 : routeText = turnLeftThree;	break;
						case 4 : routeText = turnLeftFour;	break;
					
							// Greater than 4 - assuming that the turn count is never 0 when
							// turn angle is Turn.
						default : routeText = turnLeftInDistance; break;
					}
				}
				else if( detail.Direction == TurnDirection.Right )
				{
					switch(detail.TurnCount)
					{
						case 1 :
						
							// Check to see if the distance of this leg is less than the
							// immediate turn distance.
							if (journeyDetailIndex > 0) 
							{
								if(previousDistance < immediateTurnDistance)
									routeText = immediatelyTurnRightOnto;
								else
									routeText = turnRightOne;
							} 
							else 
							{
								routeText = turnRightOne;
							}

							break;

						case 2 : routeText = turnRightTwo;		break;
						case 3 : routeText = turnRightThree;	break;
						case 4 : routeText = turnRightFour;		break;
					
							// Greater than 4 - assuming that the turn count is never 0 when
							// turn angle is Turn.
						default : routeText = turnRightInDistance; break;
					}
				}
				else if( detail.Direction == TurnDirection.MiniRoundaboutLeft )
				{
					routeText = leftMiniRoundabout;
				}
				else if( detail.Direction == TurnDirection.MiniRoundaboutRight )
				{
					routeText = rightMiniRoundabout;
				}
				else if( detail.Direction == TurnDirection.MiniRoundaboutReturn )
				{
					routeText = uTurnMiniRoundabout;
				} 
				else if( detail.Direction == TurnDirection.MiniRoundaboutContinue )
				{
					routeText = atMiniRoundabout + ChangeFirstCharacterCapitalisation(follow, false);
				} 
				else if( detail.Direction == TurnDirection.Continue )
				{
					routeText = follow;
				} 

				// Append the next road to the route text.
				routeText += space + nextRoad;

				if ((detail.Direction == TurnDirection.Continue)|| 
					(detail.Direction == TurnDirection.MiniRoundaboutContinue))					
					routeText += space + straightOn;

				if (detail.RoadSplits)
				{
					if ((detail.TurnCount > 4) && 
						((detail.Direction == TurnDirection.Left) || (detail.Direction == TurnDirection.Right)))
						routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits,true) + space + 
							ChangeFirstCharacterCapitalisation(routeText,false);
					else routeText += space + whereRoadSplits;						
				}
			}

			

			return routeText;
		}

		/// <summary>
		/// Rounds the given double to the nearest int.
		/// If double is 0.5, then rounds up.
		/// Using this instead of Math.Round because Math.Round
		/// ALWAYS returns the even number when rounding a .5 -
		/// this is not behaviour we want.
		/// </summary>
		/// <param name="valueToRound">Value to round.</param>
		/// <returns>Nearest integer</returns>
		protected static int Round(double valueToRound)
		{
			// Get the decimal point
			double valueFloored = Math.Floor(valueToRound);
			double remain = valueToRound - valueFloored;

			if(remain >= 0.5)
				return (int)Math.Ceiling(valueToRound);
			else
				return (int)Math.Floor(valueToRound);
		}

        /// <summary>
        /// Method to return the text format of a distance to be converted to string.
        /// The distanceDecimalPlaces value is used to determine number decimal places
        /// </summary>
        /// <returns></returns>
        private string GetDistanceFormat()
        {
            string numberFormat = "F";

            // determine the format based on number of decimal places, 3 or less (this can be changed in future as its only set to avoid large numbers)
            if ((distanceDecimalPlaces >= 0) && (distanceDecimalPlaces <= 3))
            {
                numberFormat += distanceDecimalPlaces.ToString();
            }
            else
            {
                numberFormat += "1";
            }

            return numberFormat;
        }

		/// <summary>
		/// Gets corresponding capitalised string.
		/// </summary>
		/// <param name="upper"></param>
		/// <param name="originalString"></param>
		/// <returns></returns>
		private string ChangeFirstCharacterCapitalisation(string originalString,bool upper)
		{
			string firstChar = originalString[0].ToString();
			firstChar = upper ? firstChar.ToUpper(CultureInfo.CurrentCulture)
				: firstChar.ToLower(CultureInfo.CurrentCulture);

			string newString = firstChar + originalString.Substring(1);

			return newString;			
		}

		#endregion Protected methods

		#region Del 6.3.1 changes

		/// <summary>
		/// checks if a place name exists and formats the instruction accordingly
		/// </summary>
		/// <param name="detail">the RoadJourneyDetail being formatted </param>
		/// <param name="routeText">the existing instruction text </param>
		/// <returns>updated formatted string of the directions</returns>
		protected virtual string AddPlaceName(RoadJourneyDetail detail, string routeText)
		{
			//add "towards {placename}" to the end of the instruction 
			routeText = routeText + space + towards + " <b>" + detail.PlaceName + "</b>";

			return routeText;
		}


		/// <summary>
		/// processes the junction number and junction action values
		/// </summary>
		protected virtual string FormatMotorwayJunction(RoadJourneyDetail detail, string routeText)
		{
			string junctionText = String.Empty;

			//apply the junction number rules 
			switch (detail.JunctionAction)
			{

				case JunctionType.Entry :

					//add a join motorway message to the instructional text
					routeText = routeText + space + atJunctionJoin + " <b>" 
						+ detail.JunctionNumber + "</b>";
										
					break;

				case JunctionType.Exit :

					//replace the normal instructional text with a leave motorway message
					routeText = atJunctionLeave + " <b>" + detail.JunctionNumber + "</b> " + leaveMotorway;					

					break;

				case JunctionType.Merge :

					//replace the normal instructional text with a leave motorway message
					routeText = atJunctionLeave + " <b>" + detail.JunctionNumber + "</b> " + leaveMotorway;
					
					break;

				default :

					//no action required
					return routeText;					
			}			
			
			//return amended route text
			return routeText;

		}
		
		/// <summary>
		/// Checks the subsequent RoadJourneyDetail
		/// </summary>
		/// <param name="detail"></param>
		/// <param name="routeText"></param>
		/// <returns></returns>
		protected virtual string CheckNextDetail(RoadJourneyDetail nextDetail, string routeText)
		{
			//add "until junction number {no}" to end of current instruction
			if ((nextDetail.IsJunctionSection) && ((nextDetail.JunctionAction == JunctionType.Exit) || (nextDetail.JunctionAction == JunctionType.Merge)))
			{
				routeText = routeText + space + untilJunction + " <b>" + nextDetail.JunctionNumber + "</b>";
			}
			return routeText;
		}

		#endregion Del 6.3.1 changes
		
		#region Private methods
		
		/// <summary>
		/// Initialises all language sensitive text strings using
		/// the resource manager.
		/// </summary>
		private void InitialiseRouteTextDescriptionStrings()
		{

			throughRoute = Global.tdResourceManager.GetString
				("RouteText.ThroughRoute", currentCulture);

			// string for roundabout exit
			roundaboutExitOne = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitOne", currentCulture);
			roundaboutExitTwo = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitTwo", currentCulture);
			roundaboutExitThree = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitThree", currentCulture);
			roundaboutExitFour = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitFour", currentCulture);
			roundaboutExitFive = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitFive", currentCulture);
			roundaboutExitSix = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitSix", currentCulture);
			roundaboutExitSeven = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitSeven", currentCulture);
			roundaboutExitEight = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitEight", currentCulture);
			roundaboutExitNine = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitNine", currentCulture);
			roundaboutExitTen = Global.tdResourceManager.GetString
				("RouteText.RoundaboutExitTen", currentCulture);

			continueString = Global.tdResourceManager.GetString
				("RouteText.Continue", currentCulture);
			continueMiniRoundabout = Global.tdResourceManager.GetString
				("RouteText.ContinueMiniRoundabout", currentCulture);
			leftMiniRoundabout = Global.tdResourceManager.GetString
				("RouteText.LeftMiniRoundabout", currentCulture);
			rightMiniRoundabout = Global.tdResourceManager.GetString
				("RouteText.RightMiniRoundabout", currentCulture);
			uTurnMiniRoundabout = Global.tdResourceManager.GetString
				("RouteText.UTurnMiniRoundabout", currentCulture);
			leftMiniRoundabout2 = Global.tdResourceManager.GetString
				("RouteText.LeftMiniRoundabout2", currentCulture);
			rightMiniRoundabout2 = Global.tdResourceManager.GetString
				("RouteText.RightMiniRoundabout2", currentCulture);
			uTurnMiniRoundabout2 = Global.tdResourceManager.GetString
				("RouteText.UTurnMiniRoundabout2", currentCulture);

			immediatelyTurnLeft = Global.tdResourceManager.GetString
				("RouteText.ImmediatelyTurnLeft", currentCulture);
			immediatelyTurnRight = Global.tdResourceManager.GetString
				("RouteText.ImmediatelyTurnRight", currentCulture);

			turnLeftOne = Global.tdResourceManager.GetString
				("RouteText.TurnLeftOne", currentCulture);
			turnLeftTwo = Global.tdResourceManager.GetString
				("RouteText.TurnLeftTwo", currentCulture);
			turnLeftThree = Global.tdResourceManager.GetString
				("RouteText.TurnLeftThree", currentCulture);
			turnLeftFour = Global.tdResourceManager.GetString
				("RouteText.TurnLeftFour", currentCulture);
			
			turnRightOne = Global.tdResourceManager.GetString
				("RouteText.TurnRightOne", currentCulture);
			turnRightTwo = Global.tdResourceManager.GetString
				("RouteText.TurnRightTwo", currentCulture);
			turnRightThree = Global.tdResourceManager.GetString
				("RouteText.TurnRightThree", currentCulture);
			turnRightFour = Global.tdResourceManager.GetString
				("RouteText.TurnRightFour", currentCulture);

			turnLeftInDistance = Global.tdResourceManager.GetString
				("RouteText.TurnLeftInDistance", currentCulture);
			turnRightInDistance = Global.tdResourceManager.GetString
				("RouteText.TurnRightInDistance", currentCulture);

			bearLeft = Global.tdResourceManager.GetString
				("RouteText.BearLeft", currentCulture);
			bearRight = Global.tdResourceManager.GetString
				("RouteText.BearRight", currentCulture);

			immediatelyBearLeft = Global.tdResourceManager.GetString
				("RouteText.ImmediatelyBearLeft", currentCulture);
			immediatelyBearRight = Global.tdResourceManager.GetString
				("RouteText.ImmediatelyBearRight", currentCulture);

			
			arriveAt = Global.tdResourceManager.GetString
				("RouteText.ArriveAt", currentCulture);

			leaveFrom = Global.tdResourceManager.GetString
				("RouteText.Leave", currentCulture);

			notApplicable = Global.tdResourceManager.GetString
				("RouteText.NotApplicable", currentCulture);

			localRoad = Global.tdResourceManager.GetString
				("RouteText.LocalRoad", currentCulture);			

			//motorway instructions
			atJunctionLeave = Global.tdResourceManager.GetString
				("RouteText.AtJunctionLeave", currentCulture);

			leaveMotorway = Global.tdResourceManager.GetString
				("RouteText.LeaveMotorway", currentCulture);

			untilJunction = Global.tdResourceManager.GetString
				("RouteText.UntilJunction", currentCulture);

			onTo = Global.tdResourceManager.GetString
				("RouteText.OnTo", currentCulture);

			towards = Global.tdResourceManager.GetString
				("RouteText.Towards", currentCulture);

			continueFor = Global.tdResourceManager.GetString
				("RouteText.ContinueFor", currentCulture);

			miles = Global.tdResourceManager.GetString
				("RouteText.Miles", currentCulture);

			turnLeftToJoin = Global.tdResourceManager.GetString
				("RouteText.TurnLeftToJoin", currentCulture);

			turnRightToJoin = Global.tdResourceManager.GetString
				("RouteText.TurnRightToJoin", currentCulture);

			atJunctionJoin = Global.tdResourceManager.GetString
				("RouteText.AtJunctionJoin", currentCulture);

			bearLeftToJoin = Global.tdResourceManager.GetString
				("RouteText.BearLeftToJoin", currentCulture);

			bearRightToJoin = Global.tdResourceManager.GetString
				("RouteText.BearRightToJoin", currentCulture);

			join = Global.tdResourceManager.GetString
				("RouteText.Join", currentCulture);

			forText = Global.tdResourceManager.GetString
				("RouteText.For", currentCulture);

			follow = Global.tdResourceManager.GetString
				("RouteText.Follow", currentCulture);

			to = Global.tdResourceManager.GetString
				("RouteText.To", currentCulture);

			routeTextFor = Global.tdResourceManager.GetString
				("RouteText.For", currentCulture);			

			continueText = Global.tdResourceManager.GetString
				("RouteText.Continue", currentCulture);	

			//Del 7 Car Costing strings
			enter = Global.tdResourceManager.GetString
				("RouteText.Enter", currentCulture);
			congestionZone = Global.tdResourceManager.GetString
				("RouteText.CongestionCharge", currentCulture);
			charge = Global.tdResourceManager.GetString
				("RouteText.Charge", currentCulture);
			certainTimes = Global.tdResourceManager.GetString
				("RouteText.CertainTimes", currentCulture);
			certainTimesNoCharge = Global.tdResourceManager.GetString
				("RouteText.CertainTimesNoCharge", currentCulture);
			board = Global.tdResourceManager.GetString
				("RouteText.Board", currentCulture);
			departingAt = Global.tdResourceManager.GetString
				("RouteText.DepartingAt", currentCulture);
			toll = Global.tdResourceManager.GetString
				("RouteText.Toll", currentCulture);
			HighTraffic = Global.tdResourceManager.GetString
				("RouteText.HighTraffic", currentCulture);
			PlanStop = Global.tdResourceManager.GetString
				("RouteText.PlanStop", currentCulture);
			FerryWait = Global.tdResourceManager.GetString
				("RouteText.WaitForFerry", currentCulture);
			viaArriveAt = Global.tdResourceManager.GetString
				("RouteText.ViaArriveAt", currentCulture);
			leaveFerry = Global.tdResourceManager.GetString
				("RouteText.LeaveFerry", currentCulture);
			exit = Global.tdResourceManager.GetString
				("RouteText.Exit", currentCulture);
			end = Global.tdResourceManager.GetString
				("RouteText.End", currentCulture);
			UnspecifedFerryWait = Global.tdResourceManager.GetString
				("RouteText.UnspecifedWaitForFerry", currentCulture);
			IntermediateFerryWait = Global.tdResourceManager.GetString
				("RouteText.IntermediateFerry", currentCulture);
			WaitAtTerminal = Global.tdResourceManager.GetString
				("RouteText.WaitAtTerminal", currentCulture);
			notAvailable = Global.tdResourceManager.GetString
				("RouteText.NotAvailable", currentCulture);

			straightOn = Global.tdResourceManager.GetString
				("RouteText.StraightOn", currentCulture);
			atMiniRoundabout = Global.tdResourceManager.GetString
				("RouteText.AtMiniRoundabout", currentCulture);
			immediatelyTurnRightOnto = Global.tdResourceManager.GetString
				("RouteText.ImmediatelyTurnRightOnto", currentCulture);
			immediatelyTurnLeftOnto = Global.tdResourceManager.GetString
				("RouteText.ImmediatelyTurnLeftOnto", currentCulture);
			whereRoadSplits = Global.tdResourceManager.GetString
				("RouteText.WhereRoadSplits", currentCulture);
		
			// park and ride
			parkAndRide = Global.tdResourceManager.GetString
				("ParkAndRide.Suffix", currentCulture);
			carParkText = Global.tdResourceManager.GetString
				("ParkAndRide.CarkPark.Suffix", currentCulture);

            // open new window icon
            openNewWindowImageUrl = Global.tdResourceManager.GetString
                ("ExternalLinks.OpensNewWindowImage", currentCulture);

            //London Congestion Charge Additional Text added for CCN0602
            londonCCAdditionalText = Global.tdResourceManager.GetString
                ("RouteText.LondonCongestionChargeAdditionalText", currentCulture);

		}
		
		#endregion Private methods				
		
	}
}