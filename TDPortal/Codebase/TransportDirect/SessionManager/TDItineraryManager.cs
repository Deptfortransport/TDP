// *********************************************** 
// NAME                 : TDItineraryManager.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 19/04/2004
// DESCRIPTION			: The TDItineraryManager maintains an Itinerary consisting
// of multiple journey Results.  It also maintains the Request data that were used
// to generate each Result so that Itinerary journeys can be restored into the normal
// JourneyRequest/JourneyResult areas to allow the User to make changes.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDItineraryManager.cs-arc  $
//
//   Rev 1.1   Oct 09 2008 15:22:06   mmodi
//Updated to check for null result
//Resolution for 5141: Cycle Planner - "Server Error" is displayed when user clicks on 'accessibility issues relating to the types of vehicles used in this journey' link at the bottom of details, map and summary page in 'Notes' section
//
//   Rev 1.0   Nov 08 2007 12:48:38   mturner
//Initial revision.
//
//   Rev 1.68   Oct 06 2006 13:39:14   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.67.1.0   Sep 19 2006 14:31:02   mmodi
//Updated extend to itinerary start routine to include copying car park object
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4177: Car Parking: Car costs not shown for extended journey
//
//   Rev 1.67   Jun 06 2006 18:06:22   rphilpott
//Changes to prevent unnecessary creation of new PricingRetailOptionsState objects when pricing extended/replanned journeys that include car sections.
//Resolution for 4053: DN068: Amend tool when used on Extend pages loses selected values
//
//   Rev 1.66   May 03 2006 15:39:10   asinclair
//Added fromParkAndRide bool to check if the user has come from the Park and Ride planner
//Resolution for 4060: DN058 Park & Ride Phase 2: After Extend/Replan etc - New Search button displays Find a Car
//
//   Rev 1.65   May 03 2006 13:28:56   mtillett
//Updated the ExtendFromItineraryEndPoint and ExtendToItineraryStartPoint methods to clear down the ParkAndRideScheme property
//Resolution for 4061: DN058: Map Error when Extending From a Park and Ride location
//
//   Rev 1.64   Apr 20 2006 17:17:32   RGriffith
//Fix to prevent GetItineraryPricing() from crashing if only dealing with road journey info.
//
//   Rev 1.63   Apr 20 2006 13:29:40   RGriffith
//IR3838 - Reverse of order of tranport modes returned to the ResultsSummaryControl
//Resolution for 3838: DN068 Extend: Extended to start Return journey shows icons in the wrong order
//
//   Rev 1.62   Apr 19 2006 15:44:22   RGriffith
//IR3936 &3937 - Fixes to allow fares to work with single journeys (following fixes to IR3825)
//Resolution for 3936: Error on Ticket and Costs option
//Resolution for 3937: DN068 Extend:  Server Error on Extend Tickets and costs results page
//
//   Rev 1.61   Apr 18 2006 11:09:46   mtillett
//Move setting of Base FindA Mode before it is reset by creaing a new itinerary.
//Resolution for 3911: DN068: New Search displays City-to-City instead of Find Nearest input screen
//
//   Rev 1.60   Apr 12 2006 13:16:18   RGriffith
//IR3709, IR3710, IR3825  Fixes: Changes to how matching outward and return fares are calculated
//
//   Rev 1.59   Apr 04 2006 16:26:56   rgreenwood
//IR3761: Enhanced JourneyReferenceNumber property to check  for valid JourneyResult object and also to return either the latest added extension or the initial journey as appropriate
//Resolution for 3761: DN068 Extend: Server error instead of usual timeout page
//
//   Rev 1.58   Mar 31 2006 16:18:52   RGriffith
//Addition of new methods to allow varying default type of ticket displayed when calculating fares
//
//   Rev 1.57   Mar 22 2006 20:27:42   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.56   Mar 22 2006 16:31:50   rhopkins
//Minor code-review fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55   Mar 14 2006 17:17:00   tmollart
//Post merge fixes. Stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.54   Mar 14 2006 11:25:24   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.53   Feb 17 2006 16:18:28   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.52   Feb 10 2006 15:04:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.51.2.1   Dec 22 2005 09:34:34   tmollart
//Removed calls to now redudant SaveCurrentFindaMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.51.2.0   Dec 12 2005 17:38:26   tmollart
//Removed code to reinstate journey parameters. Removed references to OldFindAMode - changed to FindAMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.51   Nov 09 2005 15:59:40   RPhilpott
//Merge for stream2818
//
//   Rev 1.50   Nov 01 2005 09:40:32   tmollart
//Merge with stream 2638.
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.49   Oct 28 2005 15:02:26   schand
//Modified the function GetLastAddedSegment() so that in case of exception it should return null.
//Resolution for 2886: Having Planned a Find a Flight Journey and Extended it clicking on the Door to Door tab opens a Door to Door page with the input fields populated from the Find a Flight journey
//
//   Rev 1.48   Oct 27 2005 14:58:06   schand
//Added new method GetLastAddedSegment() for IR2886.
//Resolution for 2886: Having Planned a Find a Flight Journey and Extended it clicking on the Door to Door tab opens a Door to Door page with the input fields populated from the Find a Flight journey
//
//   Rev 1.47   Sep 01 2005 14:48:18   asinclair
//Removed InitialiseSearch from AddExtensionToItinerary
//Resolution for 2744: DN062 - There is no 'Add last Journey Details' button when a journey has been extended
//
//   Rev 1.46   Aug 19 2005 19:10:14   RPhilpott
//Do not use a REquestPlaceType of Locality when extending -- use Coordinate instead.
//Resolution for 2654: Extending journey back to original origin (locality) - Between Cardiff and Cardiff Central
//
//   Rev 1.45   Apr 20 2005 11:22:46   tmollart
//Added method CostBasedBaseJourneyFindAMode.
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.44   Apr 15 2005 13:14:36   COwczarek
//Add comments
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.43   Apr 15 2005 13:11:50   COwczarek
//Fix to previous check-in.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.42   Apr 15 2005 12:52:28   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.41   Apr 11 2005 15:54:06   COwczarek
//Changes to support extending cost based searches.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.40   Mar 01 2005 16:45:42   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.39   Jan 26 2005 15:53:10   PNorell
//Support for partitioning the session information.
//
//   Rev 1.38   Oct 19 2004 10:34:18   jgeorge
//Added code to transfer JourneyPlanStateData to JourneyStore.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.37   Oct 15 2004 12:31:20   jgeorge
//Added JourneyPlanStateData and modifications to the serialization process.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.36   Sep 30 2004 11:55:44   rhopkins
//IR1648 Use ReturnOriginLocation and ReturnDestinationLocation when producing summary lines for Car segments
//
//   Rev 1.35   Sep 23 2004 13:35:02   passuied
//Called SaveCurrentFindAMode in None mode to save the JourneyParameters that will be reinstated later if user flicks between the different pages.
//Resolution for 1626: Extend Journey: inconsistency when using header tabs and coming back
//
//   Rev 1.34   Sep 22 2004 15:00:42   passuied
//Make sure that the naptans are not overwritten by an empty array. Also call FindNearestLocality if locality is empty
//Resolution for 1611: Find a coach - Extend a coach journey to a PT journey asks for travel preference changes
//
//   Rev 1.33   Sep 21 2004 17:27:30   jbroome
//IR 1596 - Obtain ExtendEndOfItinerary value from TDJourneyParameters.
//
//   Rev 1.32   Sep 14 2004 12:13:32   RHopkins
//Check that NaPTAN arrays are populated before trying to use them.
//
//   Rev 1.31   Sep 13 2004 16:15:40   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.30   Sep 09 2004 17:17:40   RHopkins
//IR1559 Ensure that ReinstateParametersForResults() is called in every method that can result in the mode being reverted to FindA.
//
//   Rev 1.29   Sep 09 2004 13:31:46   RHopkins
//IR1559 Keep track of whether the User initiated the Extension from the Details page, so that we can go back to the correct results set.
//
//   Rev 1.28   Sep 08 2004 18:52:10   RHopkins
//IR 1517  Partial fix to close gaps between extensions
//
//   Rev 1.27   Sep 03 2004 12:32:10   RHopkins
//IR1463 Use the check-in and exit times when planning extensions.  Also, when generating the SummaryLines for the Itinerary, show the check-in and exit times.
//
//   Rev 1.26   Aug 27 2004 14:34:46   RHopkins
//IR1449 When the latest extension is removed, if the resulting Itinerary contains just the Initial journey then the SelectedItinerarySegment is set to 0, to indicate that the one and only segment is selected.
//
//   Rev 1.25   Aug 25 2004 16:14:32   RHopkins
//IR1407 Changed code that populates the data (NaPTANs and TOIDs) that are missing from the location that is used for the fixed end of an extension.
//
//   Rev 1.24   Aug 19 2004 13:12:30   COwczarek
//CopyJourneyToSession and CopyJourneyFromSession
//methods now retrieve/store current FindPageState object
//
//   Rev 1.23   Aug 17 2004 09:04:06   COwczarek
//Add ConvertJourneyParametersType and ResetItinerary methods. Save/restore old Find A mode and old journey parameters in CopyJourneyFromSession and CopyJourneyToSession methods respectively.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.22   Jul 14 2004 15:04:24   RHopkins
//IR1093 Fixed "Amend this extension" so that it works when the Extension has been added to the Itinerary.
//
//   Rev 1.21   Jul 13 2004 19:37:18   RHopkins
//IR1119 Eliminated gaps between extensions
//
//   Rev 1.20   Jul 01 2004 14:30:02   RHopkins
//Add JourneyPlanControlData to Itinerary.  Also improve handling of InitialJourney
//
//   Rev 1.19   Jun 23 2004 15:59:40   RHopkins
//Corrected JourneyViewState handling
//
//   Rev 1.18   Jun 22 2004 19:08:10   RHopkins
//Changed code for maintaining ItineraryManagerModeChanged
//
//   Rev 1.17   Jun 17 2004 14:09:34   RHopkins
//More fixes to JourneyViewState handling
//
//   Rev 1.16   Jun 17 2004 10:46:28   RHopkins
//Corrected JourneyViewState handling
//
//   Rev 1.15   Jun 16 2004 12:10:30   RHopkins
//Added new method NewEXtensionSearch();  Also corrected some SelectedItinerarySegment handling.
//
//   Rev 1.14   Jun 10 2004 16:11:16   RHopkins
//Changes to data used with SummaryItineraryTable
//
//   Rev 1.13   Jun 08 2004 18:03:06   RHopkins
//FullItinerarySelected now returns false if Itinerary length is zero
//
//   Rev 1.12   Jun 08 2004 16:05:48   RHopkins
//Change of Selected Segment is now flagged as a "mode" change because it may require different controls to be displayed.
//
//   Rev 1.11   Jun 07 2004 16:17:32   RHopkins
//Corrected behaviour for JourneyViewState and SelectedItinerarySegment;  Added property to identify that the Full Itinerary has been selected.
//
//   Rev 1.10   Jun 04 2004 19:12:32   RHopkins
//Added property ItineraryManagerModeChanged to assist with decisions about User Control rendering;  Also improved handling of JourneyViewState and SelectedItinerarySegment
//
//   Rev 1.9   Jun 03 2004 19:25:54   RHopkins
//Correction to DeleteLastExtension()
//
//   Rev 1.8   Jun 02 2004 16:23:26   RHopkins
//Corrected methods for resetting/deleting last extension
//
//   Rev 1.7   May 26 2004 15:57:30   RHopkins
//Additional properties/methods to expose JourneyViewState for selected Segment.  Additional Property to indicate whether we can restore latest Extension.  Improved array bounds tolerance.
//
//   Rev 1.5   May 21 2004 14:01:50   RHopkins
//Added methods to allow specific JourneyRequest and JourneyResults data to be retrieved without the client having to modify SelectedItinerarySegment.
//
//   Rev 1.4   May 20 2004 18:03:24   JHaydock
//Intermediary check-in for FindSummary with FindSummaryResultControl operational
//
//   Rev 1.3   May 19 2004 14:45:26   RHopkins
//Corrected Serialisation of ItineraryManager.  Also fixed the pre/re-population of the Request/Results data.

using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Web;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// This class manages an array of data that is used to represent an Itinerary consisting of multiple Journeys.
	/// The class allows the members of the Itinerary to be manipulated and also allows the properties of the Itinerary to be interrogated.
	/// </summary>
	[Serializable(), CLSCompliant(false)]
	public abstract class TDItineraryManager
	{
		#region Constants and private attributes

		/// <summary>
		/// The maximum number of segments. Set up by constructer of inherited class.
		/// </summary>
		protected int maxItinerarySegments;

		/// <summary>
		/// Array of journey segments.
		/// </summary>
		protected TDSegmentStore[] itineraryArray;

		/// <summary>
		/// Current journey view state
		/// </summary>
		protected TDJourneyViewState currentJourneyViewState = new TDJourneyViewState();

		/// <summary>
		/// The Itinerary segment currently selected by the User
		/// </summary>
		protected int selectedItinerarySegment = -1;

		/// <summary>
		/// The Itinerary segment that was most recently added by the User
		/// </summary>
		protected int latestItinerarySegment = -1;

		/// <summary>
		/// The current length of the itinerary.
		/// </summary>
		protected int itineraryLength = 0;

		/// <summary>
		/// The users initial journey.
		/// </summary>
		protected int initialJourney = 0;
		
		/// <summary>
		/// The first outward journey.
		/// </summary>
		protected int outwardFirst = 0;
		
		/// <summary>
		/// The last outward journey.
		/// </summary>
		protected int outwardLast = -1;
		
		/// <summary>
		/// The first return journey.
		/// </summary>
		protected int returnFirst = 0;
		
		/// <summary>
		/// The last return journey.
		/// </summary>
		protected int returnLast = -1;

        /// <summary>
        /// The find a mode used to plan the initial journey (prior to extending)
        /// </summary>
        private FindAMode baseJourneyFindAMode;

		/// <summary>
		/// CJP Session information.
		/// </summary>
		protected CJPSessionInfo sessionInfo;

		/// <summary>
		/// 
		/// </summary>
		private bool extendedFromInitialResultsPage = false;

		/// <summary>
		/// Private storage for ExtendInProgress property.
		/// </summary>
		protected bool extendInProgress = false;

		/// <summary>
		/// Private storage for ExtendEndOfItinerary property.
		/// </summary>
		private bool extendEndOfItinerary = true;

		/// <summary>
		/// Private storage for OutwardExtendPermitted property.
		/// </summary>
		private bool outwardExtendPermitted = false;

		/// <summary>
		/// Private storage for ReturnExtendPermitted property.
		/// </summary>
		private bool returnExtendPermitted = false;

		/// <summary>
		/// Private storage to determine if calculated tickets should be forced to display single tickets
		/// </summary>
		private bool forceDisplayOfSingleTickets = false;

		/// <summary>
		/// Private storage for whether the pricing data is populated correctly.
		/// </summary>
		protected bool pricingDataComplete = false;

		/// <summary>
		/// Private storage to indicate we have come from the Park and Ride Input page.
		/// </summary>
		private bool fromParkAndRideInput = false;

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor.
		/// </summary>
		protected TDItineraryManager()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Read Only. Provides direct access to the User's Itinerary Manager
		/// </summary>
		public static TDItineraryManager Current
		{
			get { return TDSessionManager.Current.ItineraryManager; }
		}

		/// <summary>
		/// Read Only. Indicates that the ItineraryManager mode has changed between No-Itinerary, 
		/// Itinerary-only, Itinerary-with-ExtendInProgress or SelectedItinerarySegment
		/// </summary>
		public virtual bool ItineraryManagerModeChanged
		{
			get 
			{
				return TDSessionManager.Current.FormShift[ SessionKey.ItineraryManagerModeChanged ];
			}
		}

		/// <summary>
		/// Read Only. Gets the number of segments (JourneyResults) in the Itinerary
		/// </summary>
		public virtual int Length
		{
			get { return Math.Max(OutwardLength, ReturnLength); }
		}

		/// <summary>
		/// Read Only. Gets the number of segments in the Itinerary that have Outward journeys.
		/// </summary>
		public virtual int OutwardLength
		{
			get { return (outwardLast - outwardFirst) + 1; }
		}

		/// <summary>
		/// Read Only. Gets the number of segments in the Itinerary that have Return journeys.
		/// </summary>
		public virtual int ReturnLength
		{
			get { return (returnLast - returnFirst) + 1; }
		}

		/// <summary>
		/// Read Only. Gets whether the currect view of the Results set contains an Outward trip, 
		/// for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public bool OutwardExists
		{
			get
			{
				if (FullItinerarySelected)
				{
					return (OutwardLength > 0);
				}
				else
				{
					ITDJourneyResult result = JourneyResult;

                    if (result != null)
                        return (result.OutwardPublicJourneyCount > 0) || (result.OutwardRoadJourneyCount > 0);
                    else
                        return false;
				}
			}
		}

		/// <summary>
		/// Read Only. Gets whether the currect view of the Results set contains a Return trip, 
		/// for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public bool ReturnExists
		{
			get
			{
				if (FullItinerarySelected)
				{
					return (ReturnLength > 0);
				}
				else
				{
					ITDJourneyResult result = JourneyResult;

                    if (result != null)
                        return (result.ReturnPublicJourneyCount > 0) || (result.ReturnRoadJourneyCount > 0);
                    else
                        return false;
				}
			}
		}

		/// <summary>
		/// Read Only. Gets the display number for the selected Outward journey, for normal Journeys, Extensions 
		/// or the selected Itinerary segment.If the full Itinerary is selected then -1 will be returned.
		/// </summary>
		public string OutwardDisplayNumber
		{
			get
			{
				if ((itineraryLength > 0) && (!ExtendInProgress))
				{
					if (FullItinerarySelected)
					{
						return "-1";
					}
					else
					{
						return (SelectedItinerarySegment + 1).ToString(CultureInfo.CurrentCulture.NumberFormat);
					}
				}
				else
				{
					string displayNumber = TDSessionManager.Current.JourneyResult.OutwardDisplayNumber(SelectedOutwardJourneyID);
					if (SelectedOutwardJourneyType == TDJourneyType.PublicAmended)
					{
						return displayNumber + "a";
					}
					else
					{
						return displayNumber;
					}
				}
			}
		}

		/// <summary>
		/// Read Only. Gets the display number for the selected Return journey, for normal Journeys, Extensions or 
		/// the selected Itinerary segment. If the full Itinerary is selected then -1 will be returned.
		/// </summary>
		public string ReturnDisplayNumber
		{
			get
			{
				if ((itineraryLength > 0) && (!ExtendInProgress))
				{
					if (FullItinerarySelected)
					{
						return "-1";
					}
					else
					{
						return (itineraryLength - SelectedItinerarySegment).ToString(CultureInfo.CurrentCulture.NumberFormat);
					}
				}
				else
				{
					string displayNumber = TDSessionManager.Current.JourneyResult.ReturnDisplayNumber(SelectedReturnJourneyID);
					if (SelectedReturnJourneyType == TDJourneyType.PublicAmended)
					{
						return displayNumber + "a";
					}
					else
					{
						return displayNumber;
					}
				}
			}
		}

		/// <summary>
		/// Read/Write. The journey selected from the itinerary.
		/// </summary>
		public int SelectedItinerarySegment 
		{
			get
			{
				if (selectedItinerarySegment >= itineraryLength)
				{
					SelectedItinerarySegment = -1;
				}
				return selectedItinerarySegment;
			}
			set
			{
				// Setting the Changed flag and the JourneyViewState should only be done if the selection has changed
				if (value != selectedItinerarySegment)
				{
					selectedItinerarySegment = value;

					// The new row may require different controls to be displayed, so flag that this change has occurred.
					TDSessionManager.Current.FormShift[ SessionKey.ItineraryManagerModeChanged ] = true;
				}
			}
		}

		/// <summary>
		/// Read Only. Gets whether the Outward Itinerary spans multiple dates.
		/// </summary>
		public bool OutwardMultipleDates
		{
			get
			{
				if ((this.OutwardDepartDateTime().Day != this.OutwardArriveDateTime().Day)
					|| (this.OutwardDepartDateTime().Month != this.OutwardArriveDateTime().Month))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Read Only. Gets whether the Return Itinerary spans multiple dates.
		/// </summary>
		public bool ReturnMultipleDates
		{
			get
			{
				if ((this.ReturnDepartDateTime().Day != this.ReturnArriveDateTime().Day)
					|| (this.ReturnDepartDateTime().Month != this.ReturnArriveDateTime().Month))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		
		/// <summary>
		/// Read/Write. Returns true if the User initiated the Extension from the Details page
		/// </summary>
		public bool ExtendedFromInitialResultsPage
		{
			get { return extendedFromInitialResultsPage; }
			set { extendedFromInitialResultsPage = value; }
		}

		
		/// <summary>
		/// Read/Write. Returns true if an extension is currently being requested or reviewed.
		/// </summary>
		public bool ExtendInProgress
		{
			set { extendInProgress = value; }
			get { return extendInProgress; }
		}

		
		/// <summary>
		/// Read/Write. Returns true if the Itinerary is being extended from the End or false 
		/// if it is being extended to the Start.
		/// </summary>
		public bool ExtendEndOfItinerary
		{
			set { extendEndOfItinerary = value; }
			get { return extendEndOfItinerary; }
		}

		/// <summary>
		/// Read Only. Determines whether it is possible to add any more extensions to the Itinerary
		/// </summary>
		public bool ExtendPermitted
		{
			get { return (this.Length < maxItinerarySegments); }
		}

		
		/// <summary>
		/// Read Only. Determines whether the current extension is able to include Outward Journeys
		/// </summary>
		public bool OutwardExtendPermitted
		{
			get { return outwardExtendPermitted; }
		}

		
		/// <summary>
		/// Read Only. Determines whether the current extension is able to include Return Journeys
		/// </summary>
		public bool ReturnExtendPermitted
		{
			get { return returnExtendPermitted; }
		}

		/// <summary>
		/// Read Only. Returns true if the Itinerary Manager is capable of restoring the Results for the last Extension that was added.
		/// </summary>
		public bool LatestExtensionAvailable
		{
			get { return (latestItinerarySegment != -1); }
		}

		/// <summary>
		/// Read Only. Returns true if the SelectedItinerarySegment indicates that the Full Itinerary has been selected
		/// </summary>
		public virtual bool FullItinerarySelected
		{
			get
			{
				return ((itineraryLength > 0) && !ExtendInProgress && (SelectedItinerarySegment == -1));
			}
		}

		/// <summary>
		/// Read/Write. The selected outward journey index, for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public virtual int SelectedOutwardJourneyIndex
		{
			get 
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState.SelectedOutwardJourney;
				}
				else if (FullItinerarySelected)
				{
					return 0;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourney;
				}
			}
			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState.SelectedOutwardJourney = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourney = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. The selected outward journey identifier, for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public virtual int SelectedOutwardJourneyID 
		{
			get 
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyID;
				}
				else if (FullItinerarySelected)
				{
					return 0;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourneyID;
				}
			}
			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyID = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourneyID = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. The selected outward journey type, for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public TDJourneyType SelectedOutwardJourneyType 
		{
			get 
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType;
				}
				else if (FullItinerarySelected)
				{
					return TDJourneyType.Itinerary;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourneyType;
				}
			}
			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourneyType = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. the selected return journey index, for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public int SelectedReturnJourneyIndex
		{
			get 
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState.SelectedReturnJourney;
				}
				else if (FullItinerarySelected)
				{
					return 0;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyState.SelectedReturnJourney;
				}
			}
			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourney = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyState.SelectedReturnJourney = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. The selected return journey identifier, for normal Journeys, Extensions or the selected Itinerary segment
		/// </summary>
		public int SelectedReturnJourneyID
		{
			get 
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyID;
				}
				else if (FullItinerarySelected)
				{
					return 0;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyState.SelectedReturnJourneyID;
				}
			}
			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyID = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyState.SelectedReturnJourneyID = value;
				}

				pricingDataComplete = false;
			}
		}

        /// <summary>
        /// Read Only. Gets the Find A mode used to plan the initial journey. If there is no itinerary, retrieves 
        /// the Find A mode used to obtain the latests journey results.
        /// </summary>
        public FindAMode BaseJourneyFindAMode 
        {
            get
            {
                if (itineraryLength == 0)
                {
                    return TDSessionManager.Current.FindAMode;
                }
                else 
                {
                    return baseJourneyFindAMode;
                }
            }
        }

        /// <summary>
        /// Read Only. Gets the Find A mode used to plan the initial journey. If there is no itinerary, retrieves 
        /// the Find A mode used to obtain the latests journey results but from the time based partition.
        /// </summary>
        public FindAMode TimeBasedBaseJourneyFindAMode 
        {
            get
            {
                if (itineraryLength == 0)
                {
                    TDSessionSerializer ser = new TDSessionSerializer();
                    object findAMode = ser.RetrieveAndDeserializeSessionObject(TDSessionManager.Current.Session.SessionID, TDSessionPartition.TimeBased , TDSessionManager.KeyFindAMode);
                    if (findAMode == null)
                        return FindAMode.None;
                    else
                        return (FindAMode)findAMode;
                }
                else 
                {
                    return baseJourneyFindAMode;
                }
            }
        }

		/// <summary>
		/// Read Only. Gets the Find A mode used to plan the initial journey. If there is no itinerary, 
		/// retrieves the Find A mode used to obtain the latests journey results but from the cost based partition.
		/// </summary>
		public FindAMode CostBasedBaseJourneyFindAMode 
		{
			get
			{
				if (itineraryLength == 0)
				{
					TDSessionSerializer ser = new TDSessionSerializer();
					object findAMode = ser.RetrieveAndDeserializeSessionObject(TDSessionManager.Current.Session.SessionID, TDSessionPartition.CostBased , TDSessionManager.KeyFindAMode);
					if (findAMode == null)
						return FindAMode.None;
					else
						return (FindAMode)findAMode;
				}
				else 
				{
					return baseJourneyFindAMode;
				}
			}
		}


		/// <summary>
		/// Read/Write. The selected return journey type, for normal Journeys, Extensions or 
		/// the selected Itinerary segment.
		/// </summary>
		public TDJourneyType SelectedReturnJourneyType
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType;
				}
				else if (FullItinerarySelected)
				{
					return TDJourneyType.Itinerary;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyState.SelectedReturnJourneyType;
				}
			}
			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyState.SelectedReturnJourneyType = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. the appropriate journey request, for normal Journeys, Extensions or the selected Itinerary 
		/// segment Will return null if full Itinerary is selected.
		/// </summary>
		public ITDJourneyRequest JourneyRequest
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyRequest;
				}
				else if (FullItinerarySelected)
				{
					return null;
				} 
				else 
				{
					return itineraryArray[selectedItinerarySegment].JourneyRequest;
				}
			}

			set
			{ 
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyRequest = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyRequest = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. The appropriate journey result, for normal Journeys, Extensions or the selected Itinerary 
		/// segment. Returns null if full Itinerary is selected.
		/// </summary>
		public ITDJourneyResult JourneyResult
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyResult;
				}
				else if (FullItinerarySelected)
				{
					return null;
				} 
				else 
				{
					return itineraryArray[selectedItinerarySegment].JourneyResult;
				}
			}

			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyResult = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyResult = value;
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. The appropriate journey view state, for normal Journeys, Extensions or the selected 
		/// Itinerary segment. The data that is used for screen navigation is common across all segments; only 
		/// the JourneyState is segment-specific.
		/// </summary>
		public  TDJourneyViewState JourneyViewState
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyViewState;
				}
				else 
				{
					// Return the JourneyViewState based on the current selection
					if (FullItinerarySelected)
					{
						TDJourneyState newJourneyState = new TDJourneyState();
						currentJourneyViewState.JourneyState = newJourneyState;
					}
					else
					{
						currentJourneyViewState.JourneyState = itineraryArray[selectedItinerarySegment].JourneyState;
					}
					return currentJourneyViewState;
				}
			}

			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyViewState = value;
				}
				else
				{
					currentJourneyViewState = value;
					if (!FullItinerarySelected)
					{
						itineraryArray[selectedItinerarySegment].JourneyState = value.JourneyState;
					}
				}

				pricingDataComplete = false;
			}
		}

		/// <summary>
		/// Read/Write. the appropriate journey parameters, for normal Journeys, Extensions or the selected 
		/// Itinerary segment.
		/// </summary>
		public  TDJourneyParameters JourneyParameters
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					return TDSessionManager.Current.JourneyParameters;
				}
				else 
				{
					// Return the JourneyParameters based on the current selection
					if (FullItinerarySelected)
					{
						TDJourneyParameters newJourneyParameters = new TDJourneyParametersMulti();
						return newJourneyParameters;
					}
					else
					{
						return itineraryArray[selectedItinerarySegment].JourneyParameters;
					}
				}
			}

			set
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					TDSessionManager.Current.JourneyParameters = value;
				}
				else if (!FullItinerarySelected)
				{
					itineraryArray[selectedItinerarySegment].JourneyParameters = value;
				}

				pricingDataComplete = false;
			}
		}


		/// <summary>
		/// Read/Write. The appropriate Pricing Retail Options, for normal Journeys, Extensions or the selected 
		/// Itinerary segment.
		/// </summary>
		public virtual PricingRetailOptionsState PricingRetailOptions
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress || FullItinerarySelected)
				{
					return TDSessionManager.Current.PricingRetailOptions;
				}
				else
				{
					// Return the PricingRetailOptions based on the current selection
					return itineraryArray[selectedItinerarySegment].PricingRetailOptions;
				}
			}

			set
			{
				if ((itineraryLength == 0) || ExtendInProgress || FullItinerarySelected)
				{
					TDSessionManager.Current.PricingRetailOptions = value;
				}
				else
				{
					// Update the PricingRetailOptions for the current selection
					itineraryArray[selectedItinerarySegment].PricingRetailOptions = value;
				}
			}
		}

		/// <summary>
		/// Read/Only. Returns CJPMessages on all journeys in all segments.
		/// </summary>
		public CJPMessage[] CJPMessages
		{
			get 
			{
				ArrayList messages = new ArrayList();
				foreach(TDSegmentStore segment in itineraryArray)
				{
					if (segment != null)
					{
						messages.AddRange(segment.JourneyResult.CJPMessages);
					}
				}
				return (CJPMessage[])messages.ToArray(typeof(CJPMessage));
			}
		}


		/// <summary>
		/// Read/Write. Sets the CJP Session Information.
		/// </summary>
		public CJPSessionInfo CJPSessionInfo
		{
			get { return sessionInfo; }
			set { sessionInfo = value; }
		}


		/// <summary>
		/// Read Only. Returns the journey reference number for the last journey
		/// result object in the segment store.
		/// </summary>
		public int JourneyReferenceNumber
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress )
				{
					if (TDSessionManager.Current.JourneyResult == null)
					{
						return 0;
					}
					else
					{
						return TDSessionManager.Current.JourneyResult.JourneyReferenceNumber;
					}		
				}
				else if (FullItinerarySelected)
				{
					if (itineraryLength > 0)
					{
						if (latestItinerarySegment != -1)
						{
							return itineraryArray[latestItinerarySegment].JourneyResult.JourneyReferenceNumber;
						}
						else
						{
							return itineraryArray[initialJourney].JourneyResult.JourneyReferenceNumber;
						}			
					}
					else
					{
						return 0;
					}
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].JourneyResult.JourneyReferenceNumber;
				}
			}
		}


		/// <summary>
		/// Read Only. Returns array of journeys that make up the outward itinerary.
		/// If none exist then an empty array is returned.
		/// </summary>
		public virtual Journey[] OutwardJourneyItinerary
		{
			get
			{
				//Call private method to return ourward journeys
				return GetJourneyItinerary(true, false);
			}	
		}


		/// <summary>
		/// Read Only. Returns array of journeys that make up the return itinerary.
		/// If none exist then an empty array is returned.
		/// </summary>
		public virtual Journey[] ReturnJourneyItinerary
		{
			get
			{
				//Call private method to return return journeys
				return GetJourneyItinerary(false, false);
			}
		}


		/// <summary>
		/// Get Property to determine if current outward journey is public
		/// </summary>
		public virtual bool OutwardIsPublic
		{
			get
			{
				bool isPublic = false;

				// Iterate through itinerary Array and stop if a public journey is found
				for (int i=outwardFirst; i <= outwardLast; i++)
				{
					if ((itineraryArray[i].JourneyState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal)
						|| (itineraryArray[i].JourneyState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended))
					{
						isPublic = true;
						break;
					}
				}
				return isPublic;
			}
		}


		/// <summary>
		/// Get Property to determine if current return journey is public
		/// </summary>
		public virtual bool ReturnIsPublic
		{
			get
			{
				bool isPublic = false;
				
				// Iterate through itinerary Array and stop if a public journey is found
				for (int i=returnFirst; i <= returnLast; i++)
				{
					if ((itineraryArray[i].JourneyState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal)
						|| (itineraryArray[i].JourneyState.SelectedReturnJourneyType == TDJourneyType.PublicAmended))
					{
						isPublic = true;
						break;
					}
				}
				return isPublic;
			}
		}


		/// <summary>
		/// Property to determine if PricingData is complete.
		/// </summary>
		public bool PricingDataComplete
		{
			get { return pricingDataComplete; }
			set { pricingDataComplete = value; }
		}

		/// <summary>
		/// Read/Write. Returns true if we are Extending a journey that was planned using  
		/// the Park and Ride input pages.
		/// </summary>
		public bool FromParkAndRideInput
		{
			set { fromParkAndRideInput = value; }
			get { return fromParkAndRideInput; }
		}

		#endregion

		#region Public/Private Methods
		
		/// <summary>
		/// Returns a specific journey request from the itinerary without setting
		/// selected Itinerary Segment.
		/// </summary>
		/// <param name="segmentIndex">Index of segment to return journey request from.</param>
		/// <returns>Journey request object.</returns>
		public ITDJourneyRequest SpecificJourneyRequest(int segmentIndex)
		{
			if ((segmentIndex >= 0) && (segmentIndex < this.Length))
			{
				return itineraryArray[segmentIndex].JourneyRequest;
			}
			else
			{
				return null;
			}
		}

		
		/// <summary>
		/// Returns a specific journey result from the itinerary without setting 
		/// selectedItinerarySegment.
		/// </summary>
		/// <param name="segmentIndex">Index of segment to return journey result object from.</param>
		/// <returns>Journey Result Object</returns>
		public virtual ITDJourneyResult SpecificJourneyResult(int segmentIndex)
		{
			if ((segmentIndex >= 0) && (segmentIndex < this.Length))
			{
				return itineraryArray[segmentIndex].JourneyResult;
			}
			else
			{
				return null;
			}
		}

		
		/// <summary>
		/// Returns a specific journey state from the itinerary without setting selectedItinerarySegment.
		/// The data that is used for screen navigation is common across all segments. Only the JourneyState
		/// is segment specific.
		/// </summary>
		/// <param name="segmentIndex">Index of segment to return journey view state from.</param>
		/// <returns>Journey view state object.</returns>
		public virtual TDJourneyViewState SpecificJourneyViewState(int segmentIndex)
		{
			if ((segmentIndex >= 0) && (segmentIndex < this.Length))
			{
				currentJourneyViewState.JourneyState = itineraryArray[segmentIndex].JourneyState;
				return currentJourneyViewState;
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Returns the requested Origin Location for the outward itinerary.
		/// </summary>
		/// <returns>Location Object.</returns>
		public TDLocation OutwardOriginLocation()
		{
			if (this.OutwardLength > 0)
			{
				return itineraryArray[outwardFirst].JourneyRequest.OriginLocation;
			}
			else
			{
				return null;
			}
		}

		
		/// <summary>
		/// Returns the requested Origin Location for the Return Itinerary.
		/// </summary>
		/// <returns>Location Object</returns>
		public TDLocation ReturnOriginLocation()
		{
			if (this.ReturnLength > 0)
			{
				return itineraryArray[returnLast].JourneyRequest.ReturnOriginLocation;
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Returns the requested Destination Location for the Outward Itinerary.
		/// </summary>
		/// <returns>Location Object</returns>
		public TDLocation OutwardDestinationLocation()
		{
			if (this.OutwardLength > 0)
			{
				return itineraryArray[outwardLast].JourneyRequest.DestinationLocation;
			}
			else
			{
				return null;
			}
		}

		
		/// <summary>
		/// Returns the requested Destination Location for the Return Itinerary.
		/// </summary>
		/// <returns>Location Object.</returns>
		public TDLocation ReturnDestinationLocation()
		{
			if (this.ReturnLength > 0)
			{
				return itineraryArray[returnFirst].JourneyRequest.ReturnDestinationLocation;
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Returns the actual Departure Location for the Outward Itinerary.
		/// </summary>
		/// <returns>Location Object.</returns>
		public virtual TDLocation OutwardDepartLocation()
		{
			if (this.OutwardLength > 0)
			{
				if (itineraryArray[outwardFirst].SelectedOutwardJourneyIsPublic)
				{
					return itineraryArray[outwardFirst].OutwardPublicJourney().Details[0].LegStart.Location;
				}
				else
				{
					return itineraryArray[outwardFirst].JourneyRequest.OriginLocation;
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns the actual Departure Location for the Return Itinerary.
		/// </summary>
		/// <returns>Location Object.</returns>
		public virtual TDLocation ReturnDepartLocation()
		{
			if (this.ReturnLength > 0)
			{
				if (itineraryArray[returnLast].SelectedReturnJourneyIsPublic)
				{
					return itineraryArray[returnLast].ReturnPublicJourney().Details[0].LegStart.Location;
				}
				else
				{
					return itineraryArray[returnLast].JourneyRequest.ReturnOriginLocation;
				}
			}
			else
			{
				return null;
			}
		}

		
		/// <summary>
		/// Returns the actual Arrival Location for the Outward Itinerary
		/// </summary>
		/// <returns>Location Object.</returns>
		public virtual TDLocation OutwardArriveLocation()
		{
			if (this.OutwardLength > 0)
			{
				if (itineraryArray[outwardLast].SelectedOutwardJourneyIsPublic)
				{
					PublicJourneyDetail[] _publicDetails = itineraryArray[outwardLast].OutwardPublicJourney().Details;
					return _publicDetails[_publicDetails.Length-1].LegEnd.Location;
				}
				else
				{
					return itineraryArray[outwardLast].JourneyRequest.DestinationLocation;
				}
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Returns the actual Arrival Location for the Return Itinerary.
		/// </summary>
		/// <returns>Location Object.</returns>
		public TDLocation ReturnArriveLocation()
		{
			if (this.ReturnLength > 0)
			{
				if (itineraryArray[returnFirst].SelectedReturnJourneyIsPublic)
				{
					PublicJourneyDetail[] _publicDetails = itineraryArray[returnFirst].ReturnPublicJourney().Details;
					return _publicDetails[_publicDetails.Length-1].LegEnd.Location;
				}
				else
				{
					return itineraryArray[returnFirst].JourneyRequest.ReturnDestinationLocation;
				}
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Returns the Depart Date/Time for the Outward itinerary.
		/// </summary>
		/// <returns>Date/Time object.</returns>
		public virtual TDDateTime OutwardDepartDateTime()
		{
			if (this.OutwardLength > 0)
			{
				if (itineraryArray[outwardFirst].SelectedOutwardJourneyIsPublic)
				{
					PublicJourneyDetail detail = itineraryArray[outwardFirst].OutwardPublicJourney().Details[0];

					if (detail.CheckInTime != null)
					{
						return detail.CheckInTime;
					}
					else
					{
						return detail.LegStart.DepartureDateTime;
					}
				}
				else
				{
					//The first index value of the OutwardDateTime[] is used 
					//as for normal requests there will be only one value.
					if (itineraryArray[outwardFirst].JourneyRequest.OutwardArriveBefore)
					{
						TimeSpan duration = new TimeSpan( 0,0, (int)itineraryArray[outwardFirst].OutwardRoadJourney().TotalDuration);

						return itineraryArray[outwardFirst].JourneyRequest.OutwardDateTime[0].Subtract( duration );
					}
					else
					{
						return itineraryArray[outwardFirst].JourneyRequest.OutwardDateTime[0];
					}
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns the Return Depart Date/Time for the Return Itinerary.
		/// </summary>
		/// <returns>Date/Time object.</returns>
		public virtual TDDateTime ReturnDepartDateTime()
		{
			if (this.ReturnLength > 0)
			{
				if (itineraryArray[returnLast].SelectedReturnJourneyIsPublic)
				{
					PublicJourneyDetail detail = itineraryArray[returnLast].ReturnPublicJourney().Details[0];

					if (detail.CheckInTime != null)
					{
						return detail.CheckInTime;
					}
					else
					{
						return detail.LegStart.DepartureDateTime;
					}
				}
				else
				{
					//The first index value of the ReturnDateTime[] is used 
					//as for normal requests there will be only one value.
					if (itineraryArray[returnLast].JourneyRequest.ReturnArriveBefore)
					{
						TimeSpan duration = new TimeSpan( 0,0, (int)itineraryArray[returnLast].ReturnRoadJourney().TotalDuration);
						return itineraryArray[returnLast].JourneyRequest.ReturnDateTime[0].Subtract( duration );
					}
					else
					{
						return itineraryArray[returnLast].JourneyRequest.ReturnDateTime[0];
					}
				}
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Returns the Outward Arrive Date/Time for the Return Itinerary.
		/// </summary>
		/// <returns>Date/Time object.</returns>
		public virtual TDDateTime OutwardArriveDateTime()
		{
			if (this.OutwardLength > 0)
			{
				if (itineraryArray[outwardLast].SelectedOutwardJourneyIsPublic)
				{
					PublicJourneyDetail[] _publicDetails = itineraryArray[outwardLast].OutwardPublicJourney().Details;
					PublicJourneyDetail detail = _publicDetails[_publicDetails.Length-1];

					if (detail.ExitTime != null)
					{
						return detail.ExitTime;
					}
					else
					{
						return detail.LegEnd.ArrivalDateTime;
					}
				}
				else
				{
					if (itineraryArray[outwardLast].JourneyRequest.OutwardArriveBefore)
					{
						return itineraryArray[outwardLast].JourneyRequest.OutwardDateTime[0];
					}
					else
					{
						TimeSpan duration = new TimeSpan( 0,0, (int)itineraryArray[outwardLast].OutwardRoadJourney().TotalDuration);
						//The first index value of the OutwardDateTime[] is used 
						//as for normal requests there will be only one value.
						return itineraryArray[outwardLast].JourneyRequest.OutwardDateTime[0].Add( duration );
					}
				}
			}
			else
			{
				return null;
			}
		}

		
		/// <summary>
		/// Returns the Return Arrive Date/Time for the Return Itinerary.
		/// </summary>
		/// <returns>Date/Time object.</returns>
		public virtual TDDateTime ReturnArriveDateTime()
		{
			if (this.ReturnLength > 0)
			{
				if (itineraryArray[returnFirst].SelectedReturnJourneyIsPublic)
				{
					PublicJourneyDetail[] _publicDetails = itineraryArray[returnFirst].ReturnPublicJourney().Details;
					PublicJourneyDetail detail = _publicDetails[_publicDetails.Length-1];

					if (detail.ExitTime != null)
					{
						return detail.ExitTime;
					}
					else
					{
						return detail.LegEnd.ArrivalDateTime;
					}
				}
				else
				{
					if (itineraryArray[returnFirst].JourneyRequest.ReturnArriveBefore)
					{
						return itineraryArray[returnFirst].JourneyRequest.ReturnDateTime[0];
					}
					else
					{
						TimeSpan duration = new TimeSpan( 0,0, (int)itineraryArray[returnFirst].ReturnRoadJourney().TotalDuration);
						
						//The first index value of the ReturnDateTime[] is used 
						//as for normal requests there will be only one value.
						return itineraryArray[returnFirst].JourneyRequest.ReturnDateTime[0].Add( duration );
					}
				}
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Initialises the Itinerary, with a single segment that is the current Journey.
		/// </summary>
		public void CreateItinerary()
		{
			//Check to see if we are coming from the Park and Ride Input page.  
			if (TDItineraryManager.Current.JourneyParameters.DestinationLocation.ParkAndRideScheme != null)
			{
				fromParkAndRideInput = true;
			}

			ClearItinerary();

			// Store the Find A mode used to plan the intial journey
			baseJourneyFindAMode = TDSessionManager.Current.FindAMode;

			extendEndOfItinerary = true;
			AddExtensionToItinerary();

			initialJourney = 0;

			// Default to display of first (i.e. only) row in Itinerary
			SelectedItinerarySegment = 0;
		}


		/// <summary>
		/// Identify that the itinerary is to be extended from the current Itinerary end point 
		/// and initialise the new JourneyRequest to have its Origin equal to the current Itinerary Destination
		/// </summary>
		/// <returns>True if Extension was initiated successfully; False if Itinerary is full;</returns>
		public virtual bool ExtendFromItineraryEndPoint()
		{
			if (itineraryLength < maxItinerarySegments)
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				bool returnSameAsOutward = true;

				// Create new request objects
				AsyncCallState newAsyncCallState = new JourneyPlanState();

				TDJourneyRequest newJourneyRequest = new TDJourneyRequest();
				// Ensure the Find A mode is none before instantiating TDJourneyParametersMulti
				// otherwise it will be initialised for a Find A mode
				sessionManager.FindPageState = null;
				TDJourneyParameters newJourneyParameters = new TDJourneyParametersMulti();
				newJourneyParameters.Initialise();


				TDSegmentStore itineraryLastElement = itineraryArray[itineraryLength-1];

				TDLocation newOutwardLocation = OutwardArriveLocation();
				TDLocation newReturnLocation = new TDLocation();
				if (this.ReturnExists)
				{
					newReturnLocation = ReturnDepartLocation();

					returnSameAsOutward = ((newReturnLocation.NaPTANs != null)
						&& (newOutwardLocation.NaPTANs != null)
						&& (newReturnLocation.NaPTANs.Length == 1)
						&& (newOutwardLocation.NaPTANs.Length == 1)
						&& (newReturnLocation.NaPTANs[0].Naptan == newOutwardLocation.NaPTANs[0].Naptan));
				}

				// To reduce gaps in Extensions, force the location to be of the type that was requested.
				newOutwardLocation.SearchType = itineraryLastElement.JourneyRequest.DestinationLocation.SearchType;

				// Do not use a RequestPlaceType of Locality when extending - use Coordinate instead.
				if	(itineraryLastElement.JourneyRequest.DestinationLocation.RequestPlaceType == RequestPlaceType.Locality)
				{
					newOutwardLocation.RequestPlaceType = RequestPlaceType.Coordinate;
				}
				else
				{
					newOutwardLocation.RequestPlaceType = itineraryLastElement.JourneyRequest.DestinationLocation.RequestPlaceType;
				}

				// Ensure that locations contain all necessary data that are required for planning
				PopulateNaptansAndToids(ref newOutwardLocation,
						itineraryLastElement.JourneyRequest.MaxWalkingTime * itineraryLastElement.JourneyRequest.WalkingSpeed);

				newJourneyParameters.OriginLocation = newOutwardLocation;

				// Populate location for Return if different
				if ( !returnSameAsOutward )
				{
					newReturnLocation.SearchType = newOutwardLocation.SearchType;
					newReturnLocation.RequestPlaceType = newOutwardLocation.RequestPlaceType;

					PopulateNaptansAndToids(ref newReturnLocation,
						itineraryLastElement.JourneyRequest.MaxWalkingTime * itineraryLastElement.JourneyRequest.WalkingSpeed);

					newJourneyParameters.ReturnDestinationLocation = newReturnLocation;
				}

				newJourneyParameters.OutwardArriveBefore = false;
				newJourneyParameters.ReturnArriveBefore = true;
				newJourneyParameters.Origin.LocationFixed = true;
				newJourneyParameters.Destination.LocationFixed = false;
				newJourneyParameters.OriginLocation.Status = TDLocationStatus.Valid;
				newJourneyParameters.OriginLocation.ParkAndRideScheme = null;
				
				sessionManager.JourneyRequest = newJourneyRequest;
				sessionManager.JourneyParameters = newJourneyParameters;
				sessionManager.PricingRetailOptions = new PricingRetailOptionsState();
				sessionManager.AsyncCallState = newAsyncCallState;

				// Invalidate the current journey result so we can display the input page and do new search
				sessionManager.JourneyResult.IsValid = false;

				outwardExtendPermitted = ( this.OutwardExists && (outwardLast == itineraryLength-1));
				returnExtendPermitted = ( this.ReturnExists && (returnLast == itineraryLength-1));
				extendInProgress = true;
				extendEndOfItinerary = true;

				SerializeAndSetModechange();

				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// Identify that the itinerary is to be extended to the current Itinerary start point
		/// and initialise the new JourneyRequest to have its Destination equal to the current Itinerary Origin
		/// </summary>
		/// <returns>True if Extension was initiated successfully; False if Itinerary is full;</returns>
		public virtual bool ExtendToItineraryStartPoint()
		{
			if (itineraryLength < maxItinerarySegments)
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				bool returnSameAsOutward = true;

				// Create new request objects
				AsyncCallState newAsyncCallState = new JourneyPlanState();

				TDJourneyRequest newJourneyRequest = new TDJourneyRequest();
				// Ensure the Find A mode is none before instantiating TDJourneyParametersMulti
				// otherwise it will be initialised for a Find A mode
				sessionManager.FindPageState = null;
				TDJourneyParameters newJourneyParameters = new TDJourneyParametersMulti();
				newJourneyParameters.Initialise();


				TDSegmentStore itineraryFirstElement = itineraryArray[0];

				TDLocation newOutwardLocation = OutwardDepartLocation();
				TDLocation newReturnLocation = new TDLocation();
				if (this.ReturnExists)
				{
					newReturnLocation = ReturnArriveLocation();

					returnSameAsOutward = ((newReturnLocation.NaPTANs != null)
						&& (newOutwardLocation.NaPTANs != null)
						&& (newReturnLocation.NaPTANs.Length == 1)
						&& (newOutwardLocation.NaPTANs.Length == 1)
						&& (newReturnLocation.NaPTANs[0].Naptan == newOutwardLocation.NaPTANs[0].Naptan));
				}

				// To reduce gaps in Extensions, force the location to be of the type that was requested.
				newOutwardLocation.SearchType = itineraryFirstElement.JourneyRequest.OriginLocation.SearchType;
				newOutwardLocation.RequestPlaceType = itineraryFirstElement.JourneyRequest.OriginLocation.RequestPlaceType;

				// Ensure that locations contain all necessary data that are required for planning
				PopulateNaptansAndToids(ref newOutwardLocation,
					itineraryFirstElement.JourneyRequest.MaxWalkingTime * itineraryFirstElement.JourneyRequest.WalkingSpeed);

				// Ensure car parking object is also copied 
				// (to ensure location retains car park details used in displaying extended journey costs)
                newOutwardLocation.CarParking = itineraryFirstElement.JourneyParameters.OriginLocation.CarParking;

				newJourneyParameters.DestinationLocation = newOutwardLocation;

				// Populate location for Return if different
				if ( !returnSameAsOutward )
				{
					newReturnLocation.SearchType = newOutwardLocation.SearchType;
					newReturnLocation.RequestPlaceType = newOutwardLocation.RequestPlaceType;

					PopulateNaptansAndToids(ref newReturnLocation,
						itineraryFirstElement.JourneyRequest.MaxWalkingTime * itineraryFirstElement.JourneyRequest.WalkingSpeed);

					newJourneyParameters.ReturnOriginLocation = newReturnLocation;
				}

				newJourneyParameters.OutwardArriveBefore = true;
				newJourneyParameters.ReturnArriveBefore = false;
				newJourneyParameters.Origin.LocationFixed = false;
				newJourneyParameters.Destination.LocationFixed = true;
				newJourneyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
				newJourneyParameters.DestinationLocation.ParkAndRideScheme = null;

				// Put new objects into Session
				sessionManager.JourneyRequest = newJourneyRequest;
				sessionManager.JourneyParameters = newJourneyParameters;
				sessionManager.PricingRetailOptions = new PricingRetailOptionsState();
				sessionManager.AsyncCallState = newAsyncCallState;

				// Invalidate the current journey result so we can display the input page and do new search
				sessionManager.JourneyResult.IsValid = false;

				outwardExtendPermitted = ( this.OutwardExists && (outwardFirst == 0));
				returnExtendPermitted = ( this.ReturnExists && (returnFirst == 0));
				extendInProgress = true;
				extendEndOfItinerary = false;

				SerializeAndSetModechange();

				return true;
			}
			else
			{
				return false;
			}
		}


        /// <summary>
        /// If an extension is in progress then the extension is cancelled. If no extension is in progress, this
        /// method does nothing.
        /// </summary>
        public void CancelExtension()
        {
            if (extendInProgress) 
            {
                InitialiseSearch();

                extendInProgress = false;

                SerializeAndSetModechange();
            }
        }


        /// <summary>
		/// If an Extension is in progress then this is deleted, otherwise the most recently added Extension is removed from the Itinerary
		/// </summary>
		public void DeleteLastExtension()
		{
			InitialiseSearch();
			if (!extendInProgress)
			{
				DeleteSegmentWithoutCleanup(latestItinerarySegment);
			}
			extendInProgress = false;

			SerializeAndSetModechange();
		}


		/// <summary>
		/// Delete the Segment at position 0 in the Itinerary
		/// </summary>
		public void DeleteFirstSegment()
		{
			DeleteSegmentWithoutCleanup(0);
			SerializeAndSetModechange();
		}


		/// <summary>
		/// Delete the Segment at the last position in the Itinerary
		/// </summary>
		public void DeleteLastSegment()
		{
			DeleteSegmentWithoutCleanup(itineraryLength - 1);
			SerializeAndSetModechange();
		}


		/// <summary>
		/// Provides functionality to remove the current itinerary and reset session
		/// manager back to the users original journey before an itinerary was created.
		/// Virtual method overridden by sub classes.
		/// </summary>
		public abstract void ResetToInitialJourney();


		/// <summary>
		/// Resets all of the Request/Result data to their initial values, in both the normal and itinerary areas, ready for a new search.
		/// </summary>
		public void NewSearch()
		{
			InitialiseSearch();
			ClearItinerary();

			SerializeAndSetModechange();
		}


        /// <summary>
        /// Resets itinerary so that it contains no journey segments. This method does not reset request/result data.
        /// </summary>
        public void ResetItinerary()
        {
            ClearItinerary();
			SerializeAndSetModechange();
        }


		/// <summary>
		/// Removes the most recent Extension from the Itinerary, without resetting the Request/Result 
		/// data or serialising the ItineraryManager.
		/// </summary>
		private void DeleteLastExtensionWithoutCleanup()
		{
			if ((itineraryLength > 0) && (latestItinerarySegment != -1))
			{
				if (latestItinerarySegment == 0)
				{
					Array.Copy(itineraryArray, 1, itineraryArray, 0, itineraryLength-1);

					initialJourney--;

					if (!FullItinerarySelected)
					{
						SelectedItinerarySegment--;
					}
				}
				else if (selectedItinerarySegment == latestItinerarySegment)
				{
					SelectedItinerarySegment = -1;
				}


				itineraryLength--;
				itineraryArray[itineraryLength] = null;

				if (this.OutwardLength > 0)
				{
					if (latestItinerarySegment == 0)
					{
						if (outwardLast == 0)
						{
							outwardFirst = 0;
							outwardLast = -1;
						}
						else
						{
							if (outwardFirst > 0)
							{
								outwardFirst--;
							}
							outwardLast--;
						}
					}
					else
					{
						if (outwardFirst == itineraryLength)
						{
							outwardFirst = 0;
							outwardLast = -1;
						}
						else
						{
							outwardLast--;
						}
					}
				}

				if (this.ReturnLength > 0)
				{
					if (latestItinerarySegment == 0)
					{
						if (returnLast == 0)
						{
							returnFirst = 0;
							returnLast = -1;
						}
						else
						{
							if (returnFirst > 0)
							{
								returnFirst--;
							}
							returnLast--;
						}
					}
					else
					{
						if (returnFirst == itineraryLength)
						{
							returnFirst = 0;
							returnLast = -1;
						}
						else
						{
							returnLast--;
						}
					}
				}

				// If Itinerary now only contains the Initial Journey then set this as the selected segment.
				if (itineraryLength == 1)
				{
					SelectedItinerarySegment = 0;
				}

				latestItinerarySegment = -1;
			}
		}


		/// <summary>
		/// Reset the Request and Result data to their default values
		/// </summary>
		protected virtual void InitialiseSearch()
		{
			// New objects are created to avoid deleting data referenced from elsewhere
			ITDSessionManager sessionManager = TDSessionManager.Current;

			AsyncCallState newAsyncCallState = new JourneyPlanState();
			sessionManager.AsyncCallState = newAsyncCallState;

			InputPageState newInputPageState = new InputPageState();
			sessionManager.InputPageState = newInputPageState;

			TDJourneyRequest newJourneyRequest = new TDJourneyRequest();
			sessionManager.JourneyRequest = newJourneyRequest;

			TDJourneyResult newJourneyResult = new TDJourneyResult();
			sessionManager.JourneyResult = newJourneyResult;
			sessionManager.JourneyResult.IsValid = false;

			TDJourneyViewState newJourneyViewState = new TDJourneyViewState();
			sessionManager.JourneyViewState = newJourneyViewState;

			sessionManager.JourneyMapState.Initialise();
			sessionManager.ReturnJourneyMapState.Initialise();

			pricingDataComplete = false;
		}


		/// <summary>
		/// Clears the Itinerary array and resets all attributes to their initial values
		/// </summary>
		protected virtual void ClearItinerary()
		{
			SelectedItinerarySegment = -1;
			latestItinerarySegment = -1;

			extendInProgress = false;
			extendEndOfItinerary = true;
			outwardExtendPermitted = false;
			returnExtendPermitted = false;

			pricingDataComplete = false;

			itineraryLength = 0;

			initialJourney = 0;
			outwardFirst = 0;
			outwardLast = -1;
			returnFirst = 0;
			returnLast = -1;
		}


		/// <summary>
		/// Finds all the transport modes used in the Outward Itinerary.
		/// </summary>
		/// <returns>An array containing all the modes found.</returns>
		private ModeType[] GetAllOutwardModes()
		{
			ArrayList modeTypes = new ArrayList();
			PublicJourneyDetail[] publicDetails;

			for (int i=outwardFirst; i<=outwardLast; i++)
			{
				if (itineraryArray[i].SelectedOutwardJourneyIsPublic)
				{
					publicDetails = itineraryArray[i].OutwardPublicJourney().Details;
					if (publicDetails != null)
					{
						foreach (PublicJourneyDetail detail in publicDetails)
						{
							modeTypes.Add( detail.Mode );
						}
					}
				}
				else
				{
					if( !modeTypes.Contains( ModeType.Car ) )
						modeTypes.Add( ModeType.Car );
				}
			}

			return (ModeType[])modeTypes.ToArray(typeof(ModeType));
		}


		/// <summary>
		/// Finds all the transport modes used in the Return Itinerary.
		/// </summary>
		/// <returns>An array containing all the modes found.</returns>
		private ModeType[] GetAllReturnModes()
		{
			ArrayList modeTypes = new ArrayList();
			PublicJourneyDetail[] publicDetails;

			for (int i=returnFirst; i<=returnLast; i++)
			{
				if (itineraryArray[returnLast - i].SelectedReturnJourneyIsPublic)
				{
					publicDetails = itineraryArray[returnLast - i].ReturnPublicJourney().Details;
					if (publicDetails != null)
					{
						foreach (PublicJourneyDetail detail in publicDetails)
						{
							modeTypes.Add( detail.Mode );
						}
					}
				}
				else
				{
					if( !modeTypes.Contains( ModeType.Car ) )
						modeTypes.Add( ModeType.Car );
				}
			}

			return (ModeType[])modeTypes.ToArray(typeof(ModeType));
		}


		/// <summary>
		/// Finds all the different operator names used in the Outward Itinerary.
		/// </summary>
		/// <returns>An array containing all the distinct operator names found.</returns>
		protected virtual string[] GetOutwardOperatorNames()
		{
			ArrayList operatorNames = new ArrayList();
			PublicJourneyDetail[] publicDetails;

			for (int i = outwardFirst; i <= outwardLast; i++)
			{
				if (itineraryArray[i].SelectedOutwardJourneyIsPublic)
				{
					publicDetails = itineraryArray[i].OutwardPublicJourney().Details;
					if (publicDetails != null)
					{
						foreach (PublicJourneyDetail detail in publicDetails)
						{
							if (detail.Services != null)
							{
								foreach (ServiceDetails service in detail.Services)
								{
									if (!operatorNames.Contains(service.OperatorName))
										operatorNames.Add(service.OperatorName);
								}
							}
						}
					}
				}
			}

			return (string[])operatorNames.ToArray(typeof(string));
		}


		/// <summary>
		/// Finds all the different operator names used in the Return Itinerary.
		/// </summary>
		/// <returns>An array containing all the distinct operator names found.</returns>
		protected virtual string[] GetReturnOperatorNames()
		{
			ArrayList operatorNames = new ArrayList();
			PublicJourneyDetail[] publicDetails;

			for (int i = returnFirst; i <= returnLast; i++)
			{
				if (itineraryArray[i].SelectedReturnJourneyIsPublic)
				{
					publicDetails = itineraryArray[i].ReturnPublicJourney().Details;
					if (publicDetails != null)
					{
						foreach (PublicJourneyDetail detail in publicDetails)
						{
							if (detail.Services != null)
							{
								foreach (ServiceDetails service in detail.Services)
								{
									if (!operatorNames.Contains(service.OperatorName))
										operatorNames.Add(service.OperatorName);
								}
							}
						}
					}
				}
			}

			return (string[])operatorNames.ToArray(typeof(string));
		}


		/// <summary>
		/// Returns the number of interchanges in the Outward Itinerary.
		/// This includes the interchanges that occur between segments, as well as those that occur between journey legs.
		/// </summary>
		/// <returns>Number of interchanges.</returns>
		protected virtual int GetOutwardInterchangeCount()
		{
			// Interchange count is calculated as:
			// Total number of interchange counts in each segment plus (number of segments - 1)

			int numberOfInterchanges = 0;

			for (int i=outwardFirst; i<=outwardLast; i++, numberOfInterchanges++)
			{
				if (itineraryArray[i].SelectedOutwardJourneyIsPublic)
				{
					numberOfInterchanges += TDJourneyResult.GetInterchangeCount(itineraryArray[i].OutwardPublicJourney());
				}
			}

			return numberOfInterchanges - 1;
		}


		/// <summary>
		/// Returns the number of interchanges in the Return Itinerary.
		/// This includes the interchanges that occur between segments, as well as those that occur between journey legs.
		/// </summary>
		/// <returns>Number of interchanges.</returns>
		protected virtual int GetReturnInterchangeCount()
		{
			// Interchange count is calculated as:
			// Total number of interchange counts in each segment plus (number of segments - 1)

			int numberOfInterchanges = 0;

			for (int i=returnFirst; i<=returnLast; i++, numberOfInterchanges++)
			{
				if (itineraryArray[i].SelectedReturnJourneyIsPublic)
				{
					numberOfInterchanges += TDJourneyResult.GetInterchangeCount(itineraryArray[i].ReturnPublicJourney());
				}
			}

			return numberOfInterchanges - 1;
		}


		/// <summary>
		/// Populate the missing NaPTAN and TOID data that is required for locations used in the Request
		/// </summary>
		/// <param name="location">The TDLocation that needs to be completed</param>
		/// <param name="maxDistance">The maximum distance from the supplied grid reference, within which to look for matches</param>
		private void PopulateNaptansAndToids( ref TDLocation location, int maxDistance)
		{
			// The TDLocation objects that are created for the start and end locations of the results do no contain
			// enough data to be used in a request.  Therefore, the missing data must be populated.

			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			QuerySchema gisResult = gisQuery.FindNearestStopsAndITNs(location.GridReference.Easting, 
				location.GridReference.Northing,
				maxDistance);

			StringBuilder naptansMsg = new StringBuilder();
			StringBuilder toidsMsg = new StringBuilder();

			// Create searchable list of existing NaPTANs
			ArrayList naptanList = new ArrayList();
			for (int i=0; i < location.NaPTANs.Length; i++)
			{
				naptanList.Add(location.NaPTANs[i].Naptan);
			}

			bool naptansMissing = (location.NaPTANs.Length == 0);

			// Populate missing NaPTANS

			ArrayList newNaPTANs = new ArrayList();
			ArrayList natgazids = new ArrayList();

			QuerySchema.StopsRow stopsRow;
			for ( int i=0; i < gisResult.Stops.Rows.Count; i++)
			{
				stopsRow = (QuerySchema.StopsRow) gisResult.Stops.Rows[i];

				if ( naptansMissing || (naptanList.IndexOf(stopsRow.atcocode) != -1))
				{
					newNaPTANs.Add(new TDNaptan(stopsRow.atcocode, new OSGridReference((int)stopsRow.X, (int)stopsRow.Y)));

					natgazids.Add(stopsRow.natgazid);

					if (TDTraceSwitch.TraceVerbose)
					{
						naptansMsg.Append(stopsRow.atcocode + " ");
					}
				}
			}

			if (newNaPTANs.Count != 0)
			{
				location.NaPTANs = (TDNaptan[])newNaPTANs.ToArray(typeof(TDNaptan));
				location.Locality = StringHelper.GetMoreFrequentWord((string[])natgazids.ToArray(typeof(String)));
			}
			else
				if (location.Locality == null || location.Locality.Length == 0)
					location.Locality = gisQuery.FindNearestLocality(location.NaPTANs[0].GridReference.Easting, location.NaPTANs[0].GridReference.Northing);

			// Populate TOIDs

			location.Toid = new string[gisResult.ITN.Rows.Count];

			QuerySchema.ITNRow itnRow;
			for ( int i=0; i< gisResult.ITN.Rows.Count; i++)
			{
				itnRow = (QuerySchema.ITNRow) gisResult.ITN.Rows[i];
				location.Toid[i] = itnRow.toid;
				
				if (TDTraceSwitch.TraceVerbose)
				{
					toidsMsg.Append(itnRow.toid + " ");
				}
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				StringBuilder logMsg = new StringBuilder();
				logMsg.Append("DefaultExtensionLocationDetails : description = " + location.Description + " locality = " + location.Locality + " ");
				logMsg.Append(gisResult.ITN.Rows.Count + " TOIDs: " + toidsMsg.ToString());
				logMsg.Append(" and " + gisResult.Stops.Rows.Count + " NaPTANs: " + naptansMsg.ToString());
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}
		}


		/// <summary>
		/// The data stored in the Itinerary Manager has been changed, so serialize the data
		/// and set a flag to indicate to other classes that a change has occurred.
		/// </summary>
		protected virtual void SerializeAndSetModechange()
		{
			pricingDataComplete = false;
			
			TDSessionManager.Current.SaveData();
			TDSessionManager.Current.FormShift[ SessionKey.ItineraryManagerModeChanged ] = true;
		}


		/// <summary>
		/// Private method that recurses the itinerary array and then returns either the
		/// outward or return journeys dependent on how the supplied bool is set.
		/// </summary>
		/// <param name="outward">True to return outward journeys, false for return.</param>
		/// <param name="useExtendInProgressResults">Used to get info from session manager if extend in progress results are required</param>
		/// <returns>Array (possibly zero length) of journeys</returns>
		private Journey[] GetJourneyItinerary(bool outward, bool useExtendInProgressResults)
		{
			Journey journey;
			ArrayList journeys = new ArrayList();

			// If ItineraryManagerMode = None then use session manager to find JourneyArray
			if ((itineraryLength == 0) || (extendInProgress && useExtendInProgressResults))
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				TDJourneyViewState viewState = sessionManager.JourneyViewState;

				if (outward)
				{
					// Populate Journey item appropriately from sessionManager depending on type of outward Journey
					if ( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						journey = sessionManager.JourneyResult.OutwardPublicJourney(viewState.SelectedOutwardJourneyID);
					}
					else if (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended)
					{
						journey = sessionManager.JourneyResult.AmendedOutwardPublicJourney;
					}
					else
					{
						journey = sessionManager.JourneyResult.OutwardRoadJourney();
					}
				}
				else
				{
					// Populate Journey item appropriately from sessionManager depending on type of return Journey
					if ( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						journey = sessionManager.JourneyResult.ReturnPublicJourney(viewState.SelectedReturnJourneyID);
					}
					else if (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended)
					{
						journey = sessionManager.JourneyResult.AmendedReturnPublicJourney;
					}
					else
					{
						journey = sessionManager.JourneyResult.ReturnRoadJourney();
					}
				}

				// If Journey found - add to the Journey[]
				if (journey != null)
				{
					journeys.Add(journey);
				}
			} 
			else
			{
				if (outward)
				{
					// Iterate forwards through the array, adding all valid journeys from the ItineraryArray
					for (int i=outwardFirst; i <= outwardLast; i++)
					{
						journey = itineraryArray[i].OutwardSelectedJourney;

						if (journey != null)
						{
							journeys.Add(journey);
						}
					}
				}
				else
				{
					// Iterate forwards through the array, adding all valid journeys from the ItineraryArray
					for (int i=returnLast; i >= returnFirst; i--)
					{
						journey = itineraryArray[i].ReturnSelectedJourney;

						if (journey != null)
						{
							journeys.Add(journey);
						}
					}
				}
			}

			// Check to see if the arraylist has been populated with anything.
			if (journeys.Count == 0)
			{
				return new Journey[0];
			}
			else
			{	
				return (Journey[])journeys.ToArray(typeof(Journey));
			}
		}

		/// <summary>
		/// Returns the last added itinerary segment store object.
		/// Added for fix IR2744 by SC.			
		/// </summary>
		/// <returns>Last added itinerary as TDSegmentStore </returns>
		public TDSegmentStore GetLastAddedSegment()
		{	
			if (latestItinerarySegment >=0 && latestItinerarySegment < this.Length)
			{
				try
				{
					return itineraryArray[latestItinerarySegment];
				}
				catch(Exception)
				{	// if exception is thrown at this point then return null
					return null;
				}
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// Read Only. Returns selected Outward journey from the Itinerary or the Session Manager.
		/// </summary>
		public virtual Journey SelectedOutwardJourney
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					ITDSessionManager sm = TDSessionManager.Current;
					if (sm.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
					{
						return sm.JourneyResult.OutwardRoadJourney();
					}
					else
					{
						int amendedJourneyIndex = -1;
						if (sm.JourneyResult.AmendedOutwardPublicJourney != null)
						{
							amendedJourneyIndex = sm.JourneyResult.AmendedOutwardPublicJourney.JourneyIndex;
						}

						JourneyControl.PublicJourney publicJourney = sm.JourneyResult.OutwardPublicJourney(sm.JourneyViewState.SelectedOutwardJourneyID);
						if (publicJourney.JourneyIndex == amendedJourneyIndex)
						{
							return sm.JourneyResult.AmendedOutwardPublicJourney;
						}
						else
						{
							return publicJourney;
						}
					}
				}
				else if (FullItinerarySelected)
				{
					return null;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].OutwardSelectedJourney;
				}
			}
		}


		/// <summary>
		/// Read Only. Returns selected Return journey from the Itinerary or the Session Manager.
		/// </summary>
		public virtual Journey SelectedReturnJourney
		{
			get
			{
				if ((itineraryLength == 0) || ExtendInProgress)
				{
					ITDSessionManager sm = TDSessionManager.Current;
					if (sm.JourneyViewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
					{
						return sm.JourneyResult.ReturnRoadJourney();
					}
					else
					{
						int amendedJourneyIndex = -1;
						if (sm.JourneyResult.AmendedReturnPublicJourney != null)
						{
							amendedJourneyIndex = sm.JourneyResult.AmendedReturnPublicJourney.JourneyIndex;
						}

						JourneyControl.PublicJourney publicJourney = sm.JourneyResult.ReturnPublicJourney(sm.JourneyViewState.SelectedReturnJourneyID);
						if (publicJourney != null)
						{
							if (publicJourney.JourneyIndex == amendedJourneyIndex)
							{
								return sm.JourneyResult.AmendedReturnPublicJourney;
							}
							else
							{
								return publicJourney;
							}
						} 
						else
						{
							return null;
						}
					}
				}
				else if (FullItinerarySelected)
				{
					return null;
				}
				else
				{
					return itineraryArray[selectedItinerarySegment].ReturnSelectedJourney;
				}
			}
		}


		/// <summary>
		/// Returns selected Outward journey for the specified segment
		/// </summary>
		public virtual Journey GetOutwardJourney(int segmentIndex)
		{
			return itineraryArray[segmentIndex].OutwardSelectedJourney;
		}


		/// <summary>
		/// Returns selected Return journey for the specified segment
		/// </summary>
		public virtual Journey GetReturnJourney(int segmentIndex)
		{
			return itineraryArray[segmentIndex].ReturnSelectedJourney;
		}


		/// <summary>
		/// Returns the reference number for the specified journey
		/// </summary>
		/// <param name="segmentIndex"></param>
		/// <returns></returns>
		public virtual int SpecificJourneyReferenceNumber(int segmentIndex)
		{
			if ((segmentIndex >= 0) && (segmentIndex < this.Length) && (itineraryArray != null))
			{
				return itineraryArray[segmentIndex].JourneyResult.JourneyReferenceNumber;
			}
			else
			{
				return 0;
			}
		}


		/// <summary>
		/// Gets Journey Summary Lines for the Full Outward and Return Itinerary.
		/// </summary>
		/// <returns>A Journey Summary Line array</returns>
		public virtual JourneySummaryLine[] FullItinerarySummary()
		{
			JourneySummaryLine[] result;
			ModeType[] car = new ModeType[] { ModeType.Car };
			int index = 0;


			// Create a Journey Summary Line array of the appropriate length
			if (itineraryLength == 0)
			{
				// No journeys, so return an empty array
				return new JourneySummaryLine[0];
			}
			else if ((this.OutwardLength == 0) || (this.ReturnLength == 0))
			{
				// Journeys only exist for one direction
				result = new JourneySummaryLine[1];
			}
			else
			{
				// Journeys exist for both directions
				result = new JourneySummaryLine[2];
			}

			if (this.OutwardLength != 0)
			{
				result[index++] = new JourneySummaryLine(
					-1,
					this.OutwardDepartLocation().Description,
					this.OutwardArriveLocation().Description,
					TDJourneyType.Itinerary,
					GetAllOutwardModes(),
					GetOutwardInterchangeCount(),
					this.OutwardDepartDateTime(),
					this.OutwardArriveDateTime(),
					0,
					"-1",
					GetOutwardOperatorNames());
			}

			if (this.ReturnLength != 0)
			{
				result[index++] = new JourneySummaryLine(
					-2,
					this.ReturnDepartLocation().Description,
					this.ReturnArriveLocation().Description,
					TDJourneyType.Itinerary,
					GetAllReturnModes(),
					GetReturnInterchangeCount(),
					this.ReturnDepartDateTime(),
					this.ReturnArriveDateTime(),
					0,
					"-2",
					GetReturnOperatorNames());
			}

			return result;
		}


		/// <summary>
		/// Gets Journey Summary Lines for all of the segments of the Outward Itinerary.
		/// </summary>
		/// <returns>A Journey Summary Line array</returns>
		public virtual JourneySummaryLine[] OutwardSegmentsSummary()
		{
			if (this.OutwardLength == 0)
			{
				// No Outward journeys, so return an empty array
				return new JourneySummaryLine[0];
			}

			TDSegmentStore itinerarySegment;
			TransportDirect.UserPortal.JourneyControl.PublicJourney publicJourney;
			RoadJourney roadJourney;

			TimeSpan duration;
			TDDateTime departTime;
			TDDateTime arriveTime;

			ModeType[] car = new ModeType[] { ModeType.Car };
			int index = 0;
			int displayNumber = 0;

			// Create the Journey Summary Line array

			JourneySummaryLine[] result = new JourneySummaryLine[this.OutwardLength];

			// Produce a summary line for each segment
			for (int i=outwardFirst; i<=outwardLast; i++, displayNumber++)
			{
				itinerarySegment = itineraryArray[i];

				if (itinerarySegment.SelectedOutwardJourneyIsPublic)
				{
					publicJourney = itinerarySegment.OutwardPublicJourney();

					// Get the depart and arrival times for this journey
					// Use CheckInTime or ExitTime over DepartDateTime and ArriveDateTime
					// respectively.
					if (publicJourney.Details[0].CheckInTime != null)
					{
						departTime = publicJourney.Details[0].CheckInTime;
					}
					else
					{
						departTime = publicJourney.Details[0].LegStart.DepartureDateTime;
					}

					if (publicJourney.Details[publicJourney.Details.Length-1].ExitTime != null)
					{
						arriveTime = publicJourney.Details[publicJourney.Details.Length-1].ExitTime;
					}
					else
					{
						arriveTime = publicJourney.Details[publicJourney.Details.Length-1].LegEnd.ArrivalDateTime;
					}

					result[index++] = new JourneySummaryLine(
						displayNumber,
						publicJourney.Details[0].LegStart.Location.Description,
						publicJourney.Details[publicJourney.Details.Length-1].LegEnd.Location.Description,
						publicJourney.Type,
						TDJourneyResult.GetAllModes(publicJourney),
						TDJourneyResult.GetInterchangeCount(publicJourney),
						departTime,
						arriveTime,
						0,
						(displayNumber + 1).ToString(CultureInfo.CurrentCulture.NumberFormat),
						TDJourneyResult.GetOperatorNames(publicJourney));
				}
				else
				{
					roadJourney = itinerarySegment.OutwardRoadJourney();

					duration = new TimeSpan( 0,0, (int)roadJourney.TotalDuration );
					//The first index value of the OutwardDateTime[] is used 
					//as for normal requests there will be only one value.
					if (itinerarySegment.JourneyRequest.OutwardArriveBefore)
					{
						departTime = itinerarySegment.JourneyRequest.OutwardDateTime[0].Subtract( duration );
						arriveTime = itinerarySegment.JourneyRequest.OutwardDateTime[0];
					}
					else
					{
						departTime = itinerarySegment.JourneyRequest.OutwardDateTime[0];
						arriveTime = itinerarySegment.JourneyRequest.OutwardDateTime[0].Add( duration );
					}

					result[index++] = new JourneySummaryLine(
						displayNumber,
						itinerarySegment.JourneyRequest.OriginLocation.Description,
						itinerarySegment.JourneyRequest.DestinationLocation.Description,
						roadJourney.Type,
						car,
						0,
						departTime,
						arriveTime,
						roadJourney.TotalDistance,
						(displayNumber + 1).ToString(CultureInfo.CurrentCulture.NumberFormat),
						null);
				}
			}

			return result;
		}


		/// <summary>
		/// Gets Journey Summary Lines for all of the segments of the Return Itinerary.
		/// </summary>
		/// <returns>A Journey Summary Line array</returns>
		public virtual JourneySummaryLine[] ReturnSegmentsSummary()
		{
			if (this.ReturnLength == 0)
			{
				// No Return journeys, so return an empty array
				return new JourneySummaryLine[0];
			}

			TDSegmentStore itinerarySegment;
			TransportDirect.UserPortal.JourneyControl.PublicJourney publicJourney;
			RoadJourney roadJourney;

			TimeSpan duration;
			TDDateTime departTime;
			TDDateTime arriveTime;


			ModeType[] car = new ModeType[] { ModeType.Car };
			int index = 0;
			int displayNumber = 0;

			JourneySummaryLine[] result = new JourneySummaryLine[this.ReturnLength];

			// Produce a summary line for each segment.
			// Loop throught Itinerary in reverse order because start of return itinerary is in last return element
			for (int i=returnLast; i>=returnFirst; i--, displayNumber++)
			{
				itinerarySegment = itineraryArray[i];

				if (itinerarySegment.SelectedReturnJourneyIsPublic)
				{
					publicJourney = itinerarySegment.ReturnPublicJourney();

					// Get the depart and arrival times for this journey
					// Use CheckInTime or ExitTime over DepartDateTime and ArriveDateTime
					// respectively.
					if (publicJourney.Details[0].CheckInTime != null)
					{
						departTime = publicJourney.Details[0].CheckInTime;
					}
					else
					{
						departTime = publicJourney.Details[0].LegStart.DepartureDateTime;
					}

					if (publicJourney.Details[publicJourney.Details.Length-1].ExitTime != null)
					{
						arriveTime = publicJourney.Details[publicJourney.Details.Length-1].ExitTime;
					}
					else
					{
						arriveTime = publicJourney.Details[publicJourney.Details.Length-1].LegEnd.ArrivalDateTime;
					}

					result[index++] = new JourneySummaryLine(
						displayNumber,
						publicJourney.Details[0].LegStart.Location.Description,
						publicJourney.Details[publicJourney.Details.Length-1].LegEnd.Location.Description,
						publicJourney.Type,
						TDJourneyResult.GetAllModes(publicJourney),
						TDJourneyResult.GetInterchangeCount(publicJourney),
						departTime,
						arriveTime,
						0,
						(displayNumber + 1).ToString(CultureInfo.CurrentCulture.NumberFormat),
						TDJourneyResult.GetOperatorNames(publicJourney));
				}
				else
				{
					roadJourney = itinerarySegment.ReturnRoadJourney();

					duration = new TimeSpan( 0,0, (int)roadJourney.TotalDuration );
					//The first index value of the ReturnDateTime[] is used 
					//as for normal requests there will be only one value.
					if (itinerarySegment.JourneyRequest.ReturnArriveBefore)
					{
						departTime = itinerarySegment.JourneyRequest.ReturnDateTime[0].Subtract( duration );
						arriveTime = itinerarySegment.JourneyRequest.ReturnDateTime[0];
					}
					else
					{
						departTime = itinerarySegment.JourneyRequest.ReturnDateTime[0];
						arriveTime = itinerarySegment.JourneyRequest.ReturnDateTime[0].Add( duration );
					}

					result[index++] = new JourneySummaryLine(
						displayNumber,
						itinerarySegment.JourneyRequest.ReturnOriginLocation.Description,
						itinerarySegment.JourneyRequest.ReturnDestinationLocation.Description,
						roadJourney.Type,
						car,
						0,
						departTime,
						arriveTime,
						roadJourney.TotalDistance,
						(displayNumber + 1).ToString(CultureInfo.CurrentCulture.NumberFormat),
						null);
				}
			}

			return result;
		}


		/// <summary>
		/// Removes the most recent Extension from the Itinerary, without resetting the Request/Result 
		/// data or serialising the ItineraryManager.
		/// </summary>
		protected void DeleteSegmentWithoutCleanup(int segmentIndex)
		{
			if ((itineraryLength > 0) && (segmentIndex != -1))
			{
				int lastSegment = itineraryLength - 1;

				if (segmentIndex != lastSegment)
				{
					Array.Copy(itineraryArray, segmentIndex + 1, itineraryArray, segmentIndex, (itineraryLength-1)-segmentIndex);
				}

				if (initialJourney == segmentIndex)
				{
					initialJourney = -1;
				}
				else if (initialJourney > segmentIndex)
				{
					initialJourney--;
				}

				if (selectedItinerarySegment == segmentIndex)
				{
					SelectedItinerarySegment = -1;
				}
				else if (selectedItinerarySegment > segmentIndex)
				{
					SelectedItinerarySegment--;
				}

				if (latestItinerarySegment == segmentIndex)
				{
					latestItinerarySegment = -1;
				}
				else if (latestItinerarySegment > segmentIndex)
				{
					latestItinerarySegment--;
				}

				itineraryLength--;
				itineraryArray[itineraryLength] = null;

				if (this.OutwardLength > 0)
				{
					if (outwardFirst > segmentIndex)
					{
						outwardFirst--;
					}

					if (outwardLast >= segmentIndex)
					{
						outwardLast--;
					}

					if (outwardLast < outwardFirst)
					{
						outwardFirst = 0;
						outwardLast = -1;
					}
				}

				if (this.ReturnLength > 0)
				{
					if (returnFirst > segmentIndex)
					{
						returnFirst--;
					}

					if (returnLast >= segmentIndex)
					{
						returnLast--;
					}


					if (returnLast < returnFirst)
					{
						returnFirst = 0;
						returnLast = -1;
					}
				}

				pricingDataComplete = false;
			}
		}


		/// <summary>
		/// Public method that recurses the itinerary array and then returns the
		/// pricing data for the whole Itinerary.
		/// </summary>
		/// <returns>Array (possibly zero length) of journey pricings</returns>
		public virtual PricingRetailOptionsState[] GetItineraryPricing()
		{
			// Initialise variables required for retrieving the ItineraryPricing
			ITDSessionManager sessionManager = TDSessionManager.Current;
			ArrayList pricings = new ArrayList();
			// Retrive Journey Arrays for outward and return journeys
			Journey[] outwardJourneys = GetJourneyItinerary(true, true);
			Journey[] returnJourneys = GetJourneyItinerary(false, true);
			bool createNewOptions = false;

			// Determine if itinerary should be for single or return journey
			ItineraryType itineraryType = (ReturnLength > 0) ? ItineraryType.Return : ItineraryType.Single;

			// If in Adjust Mode (ie: no itinerary manger is being used) - determine if need to create new PricingRetailOptionsState
			if ((itineraryLength == 0) || extendInProgress)
			{
				if (sessionManager.PricingRetailOptions == null)
				{
					createNewOptions = true;
				} 
				else
					if (sessionManager.PricingRetailOptions.JourneyItinerary != null)
				{
                    if (sessionManager.PricingRetailOptions.IsPublic())
                    {
                        createNewOptions = !PricingRetailOptions.JourneyItinerary.FaresInitialised;
                    }
				}
				else
				{
					createNewOptions = true;
				}

				// If new PricingRetailOptionsState required - create new object 
				// in session manager PricingRetailOptionsState and set appropriate values
				if (createNewOptions)
				{
					sessionManager.PricingRetailOptions = new PricingRetailOptionsState();
					sessionManager.PricingRetailOptions.OverrideItineraryType = itineraryType;
					sessionManager.PricingRetailOptions.ItinerarySegment = -1;
					sessionManager.PricingRetailOptions.GetPricingRetailOptionsState(
						FindFareBuyOption.SingleOrReturn,
						((outwardJourneys.Length > 0) && (outwardJourneys[0].Type != TDJourneyType.RoadCongested)) ? outwardJourneys[0] : null, 
						((returnJourneys.Length > 0) && (returnJourneys[0].Type != TDJourneyType.RoadCongested)) ? returnJourneys[0] : null, 
						true); // Indicate that the pricing is being done from the RefineTickets page 
				}
				else
				{
					// Else if object doesn't have pricing data yet - set the correct itinerary type
					if (!pricingDataComplete)
					{
						sessionManager.PricingRetailOptions.OverrideItineraryType = itineraryType;
					}
				}
				// Add result from the sessionManager to the return Array
				pricings.Add(sessionManager.PricingRetailOptions);
			} 
			else
			{
				// Determine the type of tickets to display (Single/Return) based on forceDisplayOfSingleTickets
				FindFareBuyOption fareBuyOption;
				if (forceDisplayOfSingleTickets)
				{
					fareBuyOption = FindFareBuyOption.BothSingle;
				}
				else
				{
					fareBuyOption = FindFareBuyOption.SingleOrReturn;
				}

				// Iterate forwards through the array, creating new pricings/adding existing pricings 
				for (int i=0; i < itineraryLength; i++)
				{
					// Determine if need to create new pricing options object
					if (itineraryArray[i].PricingRetailOptions == null)
					{
						createNewOptions = true;
					}
					else
						if (itineraryArray[i].PricingRetailOptions.JourneyItinerary != null)
					{
                        if (itineraryArray[i].PricingRetailOptions.IsPublic()){
                            createNewOptions = !itineraryArray[i].PricingRetailOptions.JourneyItinerary.FaresInitialised;
                        }
					}
					else
					{
						createNewOptions = true;
					}

					// If new PricingRetailOptionsState required - create new object 
					// in itineraryArray PricingRetailOptionsState and set appropriate values
					if (createNewOptions)
					{
						itineraryArray[i].PricingRetailOptions = new PricingRetailOptionsState();
						if (forceDisplayOfSingleTickets)
						{
							itineraryArray[i].PricingRetailOptions.OverrideItineraryType = ItineraryType.Single;
						}
						else
						{
							itineraryArray[i].PricingRetailOptions.OverrideItineraryType = itineraryType;
						}
						itineraryArray[i].PricingRetailOptions.ItinerarySegment = i;
						itineraryArray[i].PricingRetailOptions.GetPricingRetailOptionsState(
							fareBuyOption,
							((outwardJourneys.Length > 0) && (outwardJourneys[i].Type != TDJourneyType.RoadCongested)) ? outwardJourneys[i] : null, 
							((returnJourneys.Length > 0) && (returnJourneys[(itineraryLength - i) - 1].Type != TDJourneyType.RoadCongested)) ? returnJourneys[(itineraryLength - i) - 1] : null, 
							true); // Indicate that the pricing is being done from the RefineTickets page 
						
						// Determine if the priceable units of the return journey match the priceable units
						// of the outward journey (using PricingUnit.MatchingReturn property) and if not use
						// single tickets. Also set any segments previously set with return tickets as singles.
						// Note: if there is no return journey, the OverrideItineraryType will = Single
						if ((!forceDisplayOfSingleTickets) && (returnJourneys.Length > 0))
						{
							if ((itineraryArray[i].PricingRetailOptions != null) &&
								(itineraryArray[i].PricingRetailOptions.JourneyItinerary != null) &&
								(itineraryArray[i].PricingRetailOptions.JourneyItinerary.ReturnUnits.Count > 0))
							{
								PricingRetail.Domain.PricingUnit pRet = 
									(PricingRetail.Domain.PricingUnit)itineraryArray[i].PricingRetailOptions.JourneyItinerary.ReturnUnits[0];
								if (!pRet.MatchingReturn)
								{
									forceDisplayOfSingleTickets = true;

									// If forcing use of single tickets - go back and set any Pricing items previously set to show return prices
									for (int j=0; j <= i; j++)
									{
										itineraryArray[j].PricingRetailOptions.OverrideItineraryType = ItineraryType.Single;
									}
								}
							}
						}
					}
					else 
					{
						// Else if object doesn't have pricing data yet - set the correct itinerary type
						if (!pricingDataComplete)
						{
							itineraryArray[i].PricingRetailOptions.OverrideItineraryType = itineraryType;
						}
					}

					// Add result from the itineraryArray to the return Array
					pricings.Add(itineraryArray[i].PricingRetailOptions);
				}
			}

			//Check to see if the arraylist has been populated with anything and return array as appropriate.
			if (pricings.Count == 0)
			{
				return new PricingRetailOptionsState[0];
			}
			else
			{
				return (PricingRetailOptionsState[])pricings.ToArray(typeof(PricingRetailOptionsState));
			}
		}

		/// <summary>
		/// Method to set the pricing options to be recalculated
		/// </summary>
		public void ResetFares()
		{
			if (extendInProgress)
			{
				// Code for resetting fares for just a currently selected extension page
				ITDSessionManager sessionManager = TDSessionManager.Current;
				if ((sessionManager.PricingRetailOptions != null)
					&& (sessionManager.PricingRetailOptions.JourneyItinerary != null))
				sessionManager.PricingRetailOptions.JourneyItinerary.FaresInitialised = false;
			}

			if (itineraryLength != 0)
			{
				// Code for resetting fares for all journeys in the itinerary
				// Repeat the resetting of "FaresInitialised" for all pricingRetailOptions items in the itinerary
				for (int i=0; i < itineraryLength; i++)
				{
					// If pricing options items exist then reset "FaresInitialised" to false
					if ((itineraryArray[i].PricingRetailOptions != null) 
						&& (itineraryArray[i].PricingRetailOptions.JourneyItinerary != null))
					{
						itineraryArray[i].PricingRetailOptions.JourneyItinerary.FaresInitialised = false;
					} 
				}
			}
		}

		/// <summary>
		/// Public method that recurses the itinerary array and 
		/// copies in the corresponding pricing data.
		/// </summary>
		public virtual void SetItineraryPricing(PricingRetailOptionsState[] pricings, bool complete)
		{
			// Ensure passed in pricingOptions array is of same length as the current itinerary
			if (pricings.Length == itineraryLength)
			{
				// Iterate forwards through the itinerary, adding supplied pricings
				for (int i=0, j=0; i < itineraryLength; i++, j++)
				{
					itineraryArray[i].PricingRetailOptions = pricings[j];
				}
			}

			// Determine if pricingData is complete
			pricingDataComplete = complete;
		}


		/// <summary>
		/// Add the current Journey Request/Results to the Itinerary	
		/// </summary>
		public abstract void AddExtensionToItinerary();


		/// <summary>
		/// ItineraryManagerMode 
		/// </summary>
		public abstract ItineraryManagerMode ItineraryMode{ get; }

		#endregion
	}
}
