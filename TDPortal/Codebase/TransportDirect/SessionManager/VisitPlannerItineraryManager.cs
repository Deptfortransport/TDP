// *********************************************************
// NAME			: VisitPlannerItineraryManager.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 17/08/2005
// DESCRIPTION	: Itinerary Manager for Visit Planner
//				  functionality
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/VisitPlannerItineraryManager.cs-arc  $
//
//   Rev 1.1   Dec 05 2012 13:57:58   mmodi
//Supress unnecessary warnings during compile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:48:48   mturner
//Initial revision.
//
//   Rev 1.18   Mar 22 2006 20:27:46   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.17   Mar 22 2006 16:31:18   rhopkins
//Minor code-review fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.16   Mar 14 2006 11:25:32   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.15   Feb 17 2006 16:18:24   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.14   Jan 24 2006 11:13:24   RPhilpott
//When finding eariler services, remove the worst journey returned rather than the best one.
//Resolution for 3477: Visit Planner: Selecting Earlier fails to return any train journeys when they do exist
//
//   Rev 1.13   Dec 06 2005 18:29:42   pcross
//Updated the AddJourneysToSegment method to update the index that tracks the chronological order of journeys
//Resolution for 3263: Visit Planner: Selecting Earlier or Later leaves the Journey selected different to the Details displayed
//
//   Rev 1.12   Nov 24 2005 16:16:02   tmollart
//Removed redundant methods.
//Fixed fault that meant "Plan Earlier" journeys would have failed as the new request time could have been based on a null time.
//Added code so that when we add new segments to the itinerary we automatically select the 1st journey.
//Updated InitialiseSearch method so that it creates a new JourneyParamsMulti on session manager.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//Resolution for 3002: UEE - Find A Flight: Input page not refreshed on selecting new search from results page
//Resolution for 3054: UEE - Selecting New Search for Find a Train results does not show blank input screen
//Resolution for 3061: UEE -  Door to door - new search does not clear input page
//
//   Rev 1.11   Nov 17 2005 16:51:36   tmollart
//Added facility to change search time itnerval dependant on location type used.
//Changed behaviour of of the earlier/later functions so they work on correct journey times.
//Change behaviour of force coach functionality so that correct journeys are removed.
//Resolution for 2946: Visit Planner (CG): duplicate transport from locality
//Resolution for 2950: Visit Planner (CG): Force Coach option is not rejected but should have been
//
//   Rev 1.10   Nov 10 2005 11:48:20   jgeorge
//Removed code which creates new VisitPlanState object.
//
//   Rev 1.9   Nov 10 2005 10:29:18   jbroome
//Ensure journeys have unique indexes when adding additional journeys to a result.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   Nov 10 2005 10:19:08   jgeorge
//Changed to use AsyncCallState class instead of JourneyPlanStateData/JourneyPlanControlData
//
//   Rev 1.7   Nov 09 2005 15:04:36   tmollart
//Updates from code review. Fixed bug which meant that journeys were not getting planned from the correct time (baed on the previous journey arrive time).
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Nov 03 2005 17:58:52   tmollart
//Modified code that builds requests to use correct dates and times.
//
//   Rev 1.5   Oct 29 2005 14:25:36   tmollart
//Actioned code review comments. Removed redundant code. Fixed bug which meant error messages from CJP would not get to the UI (adding erroneous result to segment). Modified the way requests are built to not parse a date from a string.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 18 2005 18:00:50   jbroome
//Added overridden methods
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 10 2005 18:09:32   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Sep 27 2005 08:50:30   pcross
//Updates from unit testing
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 17:24:38   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:59:00   tmollart
//Initial revision.

using System;
using System.Text;
using System.Collections;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using ModeType = TransportDirect.JourneyPlanning.CJPInterface.ModeType;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Itinerary Manager for visit planner implementation
	/// </summary>
	[Serializable(), CLSCompliant(false)]

	public class VisitPlannerItineraryManager : TDItineraryManager, IVisitPlannerItineraryManagerRemote
	{

		#region Private Members
		
		/// <summary>
		/// Private storage specifying maximum size of a segment.
		/// </summary>
		private int maxSegmentSize;

		/// <summary>
		/// Private storage specifying the initial request size.
		/// </summary>
		private int initialRequestSize;

		/// <summary>
		/// Private storage specifying the extend request size. 
		/// </summary>
		private int extendRequestSize;
		
		/// <summary>
		/// Private storage specifying the initial request discard size.
		/// </summary>
		private int initialRequestDiscardSize;
		
		/// <summary>
		/// Private storage specifying the extend request discard size.
		/// </summary>
		private int extendRequestDiscardSize;
		
		/// <summary>
		/// Private storage specifying which segment should be used for extensions.
		/// </summary>
		private int selectedSegmentForExtension;

		/// <summary>
		/// Private storage specifying the cjp locality walk marking.
		/// </summary>
		private int cjpLocalityWalkMargin;

		/// <summary>
		/// Private constant. Error message used in event of journey times overlapping.
		/// </summary>
		private const string JOURNEY_TIMES_OVERLAP = "VisitPlannerItineraryManager.JourneyTimesOverlap";

		/// <summary>
		/// Private constasnt. Standard amount of minutes we will add/subtract from 
		/// request time for an earlier/later journey.
		/// </summary>
		private const int STANDARD_TIME_ADJUST_MINUTES = 1;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. 
		/// </summary>
		public VisitPlannerItineraryManager()
		{
			//Load parameters from the properties service to be used
			//as instance variables.
			maxSegmentSize            = int.Parse(Properties.Current["VisitPlan.MaxSegmentSize"]);
			initialRequestSize        = int.Parse(Properties.Current["VisitPlan.InitialRequestSize"]);
			extendRequestSize		  = int.Parse(Properties.Current["VisitPlan.ExtendRequestSize"]);
			initialRequestDiscardSize = int.Parse(Properties.Current["VisitPlan.InitialRequestDiscardSize"]);
			extendRequestDiscardSize  = int.Parse(Properties.Current["VisitPlan.ExtendRequestDiscardSize"]);
			cjpLocalityWalkMargin	  = int.Parse(Properties.Current["CJP.LocalityWalkMargin"]);

			//Set up maximum number of itinerary segments. This is differnet from maxSegementSize
			//above which controls the max size of segments and NOT the maximum number of segments.
			maxItinerarySegments = 3;

			//Create array of Segment Store objects up to maximum for this mode.
			itineraryArray = new VisitPlannerSegmentStore[maxItinerarySegments];
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Read Only. The number of journeys requested from the CJP
		/// per segment when building initial intinerary.
		/// </summary>
		public int InitialRequestSize
		{
			get {return initialRequestSize;}
		}

		/// <summary>
		/// Read Only. The number of journeys to discard from the journeys
		/// returned for each segment during initial creation of itinerary.
		/// </summary>
		public int InitialRequestDiscardSize
		{
			get {return initialRequestDiscardSize;}
		}
		
		/// <summary>
		/// Read-only property
		/// Overridden as always dealing with Full Itinerary 
		/// in Visit Planner, regardless of segment selected 
		/// </summary>
		public override bool FullItinerarySelected
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Read-only property
		/// Overridden as in Visit Planner, either 
		/// always have an itinerary, or don't. In base
		/// class, selecting different segments sets this to 
		/// true. This is incorrect for Visit Planner.
		/// </summary>
		public override bool ItineraryManagerModeChanged
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Overridden read/write property.
		/// Base class checks if FullItinerarySelected before processing, 
		/// which is always true for VisitPlanner. This override will 
		/// get/set values regardless of FullItinerarySelected
		/// </summary>
		public override int SelectedOutwardJourneyID
		{
			get
			{
				return itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourneyID;
			}
			set
			{
				itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourneyID = value;
			}
		}

		/// <summary>
		/// Overridden read/write property.
		/// Base class checks if FullItinerarySelected before processing, 
		/// which is always true for VisitPlanner. This override will 
		/// get/set values regardless of FullItinerarySelected
		/// </summary>
		public override int SelectedOutwardJourneyIndex
		{
			get
			{
				return itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourney;
			}
			set
			{
				itineraryArray[selectedItinerarySegment].JourneyState.SelectedOutwardJourney = value;
			}
		}

		#endregion

		#region Public Methods
			
		/// <summary>
		/// Creates a new journey request that will plan journeys that arrive
		/// before the first journey in specified segment.
		/// </summary>
		/// <param name="segmentIndex">Specific segment to add journeys to</param>
		public bool ExtendJourneyResultEarlier(int segmentIndex)
		{
			if (SpecificJourneyResult(segmentIndex).OutwardPublicJourneyCount < maxSegmentSize)
			{
				ITDJourneyRequest previousRequest = itineraryArray[segmentIndex].JourneyRequest;

				// NOTE: To create the new journey request we perform a shallow clone on
				// the previous request. This will create a new request that contains references
				// to the previous request. This is acceptable for a journey request as in
				// this scenario the locations/modes etc remain the same. We then create new
				// TDDateTime objects to give this request new times to previousrequest. 
				// PreviousRequest will be references for all other data.
				TDJourneyRequest newRequest = (TDJourneyRequest)previousRequest.Clone();
				
				newRequest.OutwardArriveBefore = true;
				newRequest.Sequence = extendRequestSize;

				int journeyIndex;	// Index of the last details element on the current journey.
				int earliestIndex;	// Index of the last details element on the earliest journey.

				PublicJourney earliestJourney = ((PublicJourney)(itineraryArray[segmentIndex].JourneyResult.OutwardPublicJourneys[0]));
				earliestIndex = earliestJourney.Details.Length - 1;

				foreach(PublicJourney journey in itineraryArray[segmentIndex].JourneyResult.OutwardPublicJourneys)
				{
					journeyIndex = journey.Details.Length - 1;
					
					if (journey.Details[journeyIndex].LegEnd.ArrivalDateTime < earliestJourney.Details[earliestIndex].LegEnd.ArrivalDateTime)
					{
						earliestJourney = journey;
						earliestIndex = earliestJourney.Details.Length - 1;
					}
				}

				newRequest.OutwardDateTime = new TDDateTime[1];
				newRequest.OutwardDateTime[0] = earliestJourney.Details[earliestIndex].LegEnd.ArrivalDateTime.AddMinutes(-(GetTimeAdjustMinutes(segmentIndex)));

				selectedSegmentForExtension = segmentIndex;
				((VisitPlannerSegmentStore)itineraryArray[segmentIndex]).ExtendEndOfSegment = false;
			
				//Assign journey request and plan state data to the session. Then
				//Serialized the data.
				TDSessionManager.Current.JourneyRequest = newRequest;
				SerializeAndSetModechange();
				
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Creates a new journey request that will plan journeys that arrive
		/// after the last journey in the specified segment.
		/// </summary>
		/// <param name="segmentIndex">Specific segment to add journeys to</param>
		public bool ExtendJourneyResultLater(int segmentIndex)
		{
			if (SpecificJourneyResult(segmentIndex).OutwardPublicJourneyCount < maxSegmentSize)
			{
				ITDJourneyRequest previousRequest = itineraryArray[segmentIndex].JourneyRequest;

				// NOTE: To create the new journey request we perform a shallow clone on
				// the previous request. This will create a new request that contains references
				// to the previous request. This is acceptable for a journey request as in
				// this scenario the locations/modes etc remain the same. We then create new
				// TDDateTime objects to give this request new times to previousrequest. 
				// PreviousRequest will be references for all other data.
				TDJourneyRequest newRequest = (TDJourneyRequest)previousRequest.Clone();
				
				newRequest.OutwardArriveBefore = false;
				newRequest.Sequence = extendRequestSize;

				PublicJourney latestJourney = ((PublicJourney)(itineraryArray[segmentIndex].JourneyResult.OutwardPublicJourneys[0]));

				foreach(PublicJourney journey in itineraryArray[segmentIndex].JourneyResult.OutwardPublicJourneys)
				{
					if (journey.Details[0].LegStart.DepartureDateTime > latestJourney.Details[0].LegStart.DepartureDateTime)
					{
						latestJourney = journey;
					}
				}

				newRequest.OutwardDateTime = new TDDateTime[1];
				newRequest.OutwardDateTime[0] = latestJourney.Details[0].LegStart.DepartureDateTime.AddMinutes(GetTimeAdjustMinutes(segmentIndex));
								
				selectedSegmentForExtension = segmentIndex;
				((VisitPlannerSegmentStore)itineraryArray[segmentIndex]).ExtendEndOfSegment = true;
			
				//Assign journey request and plan state data to the session. Then
				//Serialized the data.
				TDSessionManager.Current.JourneyRequest = newRequest;
				SerializeAndSetModechange();
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// Tests if the specified segment contains less that the max journeys.
		/// </summary>
		/// <returns>True if specified segment contains less than maximum</returns>
		/// <param name="segmentIndex">Segment to test</param>
		public bool ExtendSegmentPermitted(int segmentIndex)
		{
			//Test if the number of segments is less than the maximum.
			if (SpecificJourneyResult(segmentIndex).OutwardPublicJourneyCount < maxSegmentSize)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// Adds journeys to an existing itinerary segment. Extend...Earlier/Later needs
		/// to be called first and therefore a segment is not required in the call to
		/// this method.
		/// </summary>
		public void AddJourneysToSegment(ITDJourneyResult result)
		{
			// Check that selectedSegmentForExtension is in required range.
			if (selectedSegmentForExtension >=0 || selectedSegmentForExtension <= itineraryLength)
			{
				// Get references to required objects.
				ArrayList journeys = result.OutwardPublicJourneys;
				VisitPlannerSegmentStore segment = (VisitPlannerSegmentStore)itineraryArray[selectedSegmentForExtension];

				// Work out if its possible to remove the extend discard from the array
				if ((journeys.Count - extendRequestDiscardSize) >= 1)
				{
					// If after removing the extend request discard size we are left with
					// more than one journey we can afford to remove the required discard
					// size from the journeys array.

					// Dependant on if the user selected to extend earlier or later decides
					// on how we sort the joureys and which one we remove.
					if (segment.ExtendEndOfSegment)
					{
						result.OutwardPublicJourneys.Sort((IComparer)(new DepartAfterComparer()));
					}
					else
					{
						result.OutwardPublicJourneys.Sort((IComparer)(new ArriveBeforeComparer()));
					}

					// having sorted so the best journeys are at the best, remove the worst one 
					result.OutwardPublicJourneys.RemoveRange(result.OutwardPublicJourneys.Count - 1, 1);
				}

				//Work out how many journeys can be added before we over fill the segment.
				//maxJourneysToAdd 
				int maxJourneysToAdd = maxSegmentSize - segment.JourneyResult.OutwardPublicJourneys.Count;
				
				//The number we will actually add
				int journeysToAdd;

				if (journeys.Count > maxJourneysToAdd)
				{
					journeysToAdd = maxJourneysToAdd;
				}
				else
				{
					journeysToAdd = journeys.Count;
				}

				// Overwrite JourneyIndex values of journeys so that 
				// they are all unique. As we are adding journeys from a new result
				// to an existing result, there will be duplicate journey indices.

				// Get last used index from original result
				int newIndex = segment.JourneyResult.OutwardJourneyIndex;
				
				foreach (PublicJourney journey in journeys)
				{
					newIndex++;
					journey.JourneyIndex = newIndex;
				}
				
				// Update index on original result in case we add more journeys
				segment.JourneyResult.OutwardJourneyIndex = newIndex;

				//Add journeys to the segment.
				if (segment.ExtendEndOfSegment)
				{
					//Add the required number of journeys to the end of the segment.
					segment.JourneyResult.OutwardPublicJourneys.AddRange(journeys.GetRange(0, journeysToAdd));
				}
				else
				{
					//If we are adding to the start of the segment we need to reverse
					//the array list first and then insert it at the start.
					journeys.Reverse();
					segment.JourneyResult.OutwardPublicJourneys.InsertRange(0, journeys.GetRange(0, journeysToAdd));
				}
				
				// Update the viewstate with the new default journey index for the journeys in the new result
				// (there are 2 indexes: one which tracks order journey added to result and one which tracks
				// order journey is chronologically (therefore order on screen))
				// We are missing the index for the order journey is chronologically so we use the other index
				// to find that
				int selectedIndex = 0;
				int selectedJourneyIndex = 0;
				
				// get the current segment index
				selectedItinerarySegment = selectedSegmentForExtension;
				
				// get the ID for the default journey in this segment (the one which tracks order journey added to result)
				selectedJourneyIndex = SelectedOutwardJourneyID;

				// get the selected journey index which represents the chronological order of journeys
				selectedIndex = segment.JourneyResult.GetSelectedOutwardJourneyIndex(selectedJourneyIndex);
			
				// update the viewstate with the new index
				SelectedOutwardJourneyIndex = selectedIndex;

			}
		}


		/// <summary>
		/// Checks each segment for overlapping journey times. Returns true/false
		/// dependent on validation and adds an error to the ValidationError 
		/// property of Session Manager if overlap exists.
		/// </summary>
		/// <returns>True if no overlap and false if overlap exists.</returns>
		public bool ValidateSelectedJourneyTimes()
		{
			int currentSegment = 0;
			int maxSegments = itineraryLength;

			while (currentSegment < maxSegments - 1)
			{
				//Set up local (easy to manage!) references to required objects.
				VisitPlannerSegmentStore segment1 = (VisitPlannerSegmentStore)itineraryArray[currentSegment];
				VisitPlannerSegmentStore segment2 = (VisitPlannerSegmentStore)itineraryArray[currentSegment + 1];
				PublicJourney journey1 = (PublicJourney)segment1.JourneyResult.OutwardPublicJourneys[segment1.JourneyState.SelectedOutwardJourney];
				PublicJourney journey2 = (PublicJourney)segment2.JourneyResult.OutwardPublicJourneys[segment2.JourneyState.SelectedOutwardJourney];

				//Check the times are as expected.
				if (journey1.Details[journey1.Details.Length -1].LegEnd.ArrivalDateTime > journey2.Details[0].LegStart.DepartureDateTime)
				{
					//Create a validation error on session manager. Done by creating
					//local objects which are they assigned onto session manager.
					TDSessionManager.Current.ValidationError = new ValidationError();
					ArrayList listErrors = new ArrayList();
					Hashtable errorMessages = new Hashtable();

					//Populate the local objects with details of the error.
					listErrors.Add(ValidationErrorID.JourneyTimesOverlap);
					errorMessages.Add(ValidationErrorID.JourneyTimesOverlap, JOURNEY_TIMES_OVERLAP);

					//Assign them to the validation error object on session manager. Note that the 
					//array list is converted to an array.
					TDSessionManager.Current.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
					TDSessionManager.Current.ValidationError.MessageIDs = errorMessages;

					//Return false from this method to indicate there was an error
					return false;
				}

				//Increment counter to assess the next segments.
				currentSegment ++;
			}
			//If code get to this point then validation has been successful so return true
			return true;
		}


		/// <summary>
		/// Adds an segment to the end of the itinerary.
		/// </summary>
		/// <param name="parameters">Journey parameters</param>
		/// <param name="request">Journey request</param>
		/// <param name="result">Journey result</param>
		/// <param name="journeyState">Journey state</param>
		public void AddSegmentToItinerary(TDJourneyParameters parameters, ITDJourneyRequest request, ITDJourneyResult result, TDJourneyState journeyState)
		{
			// NOTE: Visit Planner only ever adds an segment to the end of an itinerary
			// so there is no need to check if we are extending the start or the end of 
			// itinerary manager. For this to be reinstated the extendEndOfItinerary property
			// would need to be set (as required) and this would need to extend either the 
			// start of the end of the itinerary array.
			
			//Check that there is room to add the extension.
			if (Length < maxItinerarySegments)
			{
				// We are working with an initial itinerary so check if we need
				// to force the removal of a coach journey.
				if ((result.OutwardPublicJourneys.Count - InitialRequestDiscardSize) >= 1)
				{
					// If after removing the extend request discard size we are left with
					// more than one journey we can afford to remove the required discard
					// size from the journeys array. We need to remove the worst journey.

					// Sort the journeys into order.
					result.OutwardPublicJourneys.Sort((IComparer)(new DepartAfterComparer()));

					// And set the SelectedOutwardJourneyID based on the first one
					journeyState.SelectedOutwardJourneyID = ((PublicJourney)(result.OutwardPublicJourneys[0])).JourneyIndex;

					// Remove the last journey.
					result.OutwardPublicJourneys.RemoveRange(result.OutwardPublicJourneys.Count - 1, 1);
				}
					
				// There is a journey result to put on the segment store so create a 
				// visit planner segment object and place on end of array.
				// NOTE: We even put a journey result containing no errors onto
				// the segment as this will contain error messages needed on the UI
				itineraryArray[itineraryLength] = new VisitPlannerSegmentStore();
				itineraryArray[itineraryLength].JourneyParameters = parameters;
				itineraryArray[itineraryLength].JourneyResult = result;
				itineraryArray[itineraryLength].JourneyRequest = request;
				itineraryArray[itineraryLength].JourneyState = journeyState;
				outwardLast ++;

				//Increase itinerary length and initialise search objects.
				itineraryLength ++;
				ExtendInProgress = false;			
			}
		}


		/// <summary>
		/// Builds the visit plan journey request. Looks at current itinerary length
		/// and works out how to build the request. First journey will take date and
		/// time from parameters where as subsequent journeys will base their departure
		/// times on the previous journey.
		/// </summary>
		/// <param name="parameters">Visit Plan Paramters</param>
		/// <returns>Journey request object containing first journey request</returns>
		public ITDJourneyRequest BuildNextRequest(TDJourneyParametersVisitPlan parameters)
		{	

			// Typical Journey Plan

			// Idx  Location	Journey							Itin. Length	Max Length	Stay Duration Index
			//				
			//					[Prior to any planning]				0				3
			//	0	   A			
			//					1st Journey (A to B) [0 to 1]		1				3				0
			//  1      B
			//					2nd Journey (B to C) [1 to 2]		2				3				1
			//	2      C
			//					3rd Journey (C to A) [2 to 0]		3				3				2
			// (0)    (A)

			//Declarations
			ITDJourneyRequest request = new TDJourneyRequest();

			// The from location is always from itinerary length
			request.OriginLocation = parameters.GetLocation(itineraryLength);

			// The journey destination can be worked out from the current itinerary length.
			// If the itinerary length is one away from the maximum and planning this request
			// will mean we have populated all the segments then then this request must
			// be the return to origin request. 
			if ( (itineraryLength + 1) < maxItinerarySegments)
			{
				// Destination location is itineraryLength + 1;
				request.DestinationLocation = parameters.GetLocation(itineraryLength + 1);
			}
			else
			{
				request.DestinationLocation = parameters.GetLocation(0);
			}


			// The journey time can be worked out from the itineraryLength as well.
			// If the itineraryLength is zero the date and time for the 1st journey comes from
			// the date specified on the parameters. Otherwise it comes from the previous
			// journey plus the stop over time.
			if (itineraryLength == 0)
			{
				int day = int.Parse(parameters.OutwardDayOfMonth);
				int month = int.Parse(parameters.OutwardMonthYear.Substring(0,2));
				int year = int.Parse(parameters.OutwardMonthYear.Substring(3,4));
				int hour = int.Parse(parameters.OutwardHour);
				int minutes = int.Parse(parameters.OutwardMinute);

				TDDateTime newDateTime = new TDDateTime(year, month, day, hour, minutes, 0, 0);
				request.OutwardDateTime = new TDDateTime[1]{newDateTime};
			}
			else
			{

				// If we are building a request that is not the first request we need to work out
				// how to generate the departure date and time. This will be based upon the previous
				// journey with the added stop over time. We must also work out which journey on the
				// previous segment arrives first. It will be this journey that we use to base our 
				// new request on.

				// Declare some temporary variables for this processing
				int resultPos = itineraryLength - 1;	// Points to last segement.
				int earliestDetailPos;					// Points to last journey detail on earliest journey
				int journeyDetailPos;					// Points to last journey detail on current journey

				// We need to work out which is the earliest ARRIVING journey on the segment. The way
				// this will be done is to set local variable to the first journey, then check if the 
				// next arrives earlier. If it does set that to the local var and keep recursing.
				PublicJourney earliestJourney = ((PublicJourney)(itineraryArray[resultPos].JourneyResult.OutwardPublicJourneys[0]));

				foreach(PublicJourney journey in itineraryArray[resultPos].JourneyResult.OutwardPublicJourneys)
				{
					// Set points to point to last leg of the journey on the earliest journey and also
					// on the journey we are comparing. The last legs are what we have to compare.
					earliestDetailPos = earliestJourney.Details.Length - 1;
					journeyDetailPos = journey.Details.Length - 1;

					// Compare the two journeys
					if (journey.Details[journeyDetailPos].LegEnd.ArrivalDateTime < earliestJourney.Details[earliestDetailPos].LegEnd.ArrivalDateTime)
					{
						earliestJourney = journey;
					}
				}
				
				// Create date object on the request and assign to it. Rework out the last detail leg
				// and assign the correct date to the request.
				request.OutwardDateTime = new TDDateTime[1];					
				earliestDetailPos = earliestJourney.Details.Length - 1;
				request.OutwardDateTime[0] = earliestJourney.Details[earliestDetailPos].LegEnd.ArrivalDateTime.AddMinutes(Convert.ToDouble( parameters.GetStayDuration(itineraryLength - 1)));
			}

			//Build the body of the request
			request.Sequence = InitialRequestSize;
			request.IsReturnRequired = false;
			request.Modes = parameters.PublicModes;
			request.OutwardArriveBefore = false;
			request.InterchangeSpeed = parameters.InterchangeSpeed;
			request.WalkingSpeed = parameters.WalkingSpeed;
			request.MaxWalkingTime = parameters.MaxWalkingTime;
			request.IsTrunkRequest = false;
			request.PublicAlgorithm = parameters.PublicAlgorithmType;
			request.Sequence = initialRequestSize;
		
			return request;
		}
	

		/// <summary>
		/// Clears the current itinerary and creates new blank segements on
		/// the itinerary.
		/// </summary>
		protected override void ClearItinerary()
		{
			//Create array of Segment Store objects up to maximum for this mode.
			itineraryArray = new VisitPlannerSegmentStore[maxItinerarySegments];
			base.ClearItinerary ();
		}
		

		/// <summary>
		/// Blank class to override base class.
		/// </summary>
		protected override void InitialiseSearch()
		{
			TDJourneyParameters newJourneyParameters = new TDJourneyParametersVisitPlan();
			TDSessionManager.Current.JourneyParameters = newJourneyParameters;

			base.InitialiseSearch ();
		}


		/// <summary>
		/// Provides functionality to remove the current itinerary and reset session
		/// manager back to the users original journey before an itinerary was created.
		/// No processing required for VisitPlannerItineraryManager.
		/// </summary>
		public override void ResetToInitialJourney()
		{
			// Do nothing for VisitPlannerItineraryManager
		}


		/// <summary>
		/// Gets the value (in minutes) we will add/subtract from an earlier/later
		/// search based on the locality type of the search.
		/// </summary>
		/// <param name="segmentIndex">Segment which we are extending.</param>
		/// <returns>Minutes to add/subtract from the time.</returns>
		private int GetTimeAdjustMinutes(int segmentIndex)
		{
			int returnValue = STANDARD_TIME_ADJUST_MINUTES;

			if (itineraryArray[segmentIndex].JourneyRequest.OriginLocation.SearchType == SearchType.Locality)
			{
				returnValue = cjpLocalityWalkMargin;
			}

			return returnValue;
		}

		/// <summary>
		/// Read only property - get the ItineraryManagerMode
		/// </summary>
		public override ItineraryManagerMode ItineraryMode
		{
			get {return ItineraryManagerMode.VisitPlanner;}
		}

		#endregion

		#region Obsolete Overridden Methods

		/// <summary>
		/// Obsolete as we do not want to serialize data back in visit planner.
		/// As it is working over the remoting boundary this may inadvertently
		/// overwrite data.
        /// </summary>
#pragma warning disable 809
        [Obsolete]
		public override void AddExtensionToItinerary()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		
		[Obsolete]
		public override bool ExtendToItineraryStartPoint()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override bool ExtendFromItineraryEndPoint()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete("Method OutwardDepartDateTime not available in VisitPlanner implementation of IntineraryManager")]
		public override TDDateTime OutwardDepartDateTime()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		protected override int GetOutwardInterchangeCount()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override TDLocation OutwardArriveLocation()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override TDLocation OutwardDepartLocation()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override PricingRetailOptionsState PricingRetailOptions 
		{ 
			get{throw new NotImplementedException("Method obsolete in implementation");} 
			set{throw new NotImplementedException("Method obsolete in implementation");} 
		}

		[Obsolete]
		public override TDDateTime ReturnArriveDateTime()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override TDDateTime OutwardArriveDateTime()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		protected override string[] GetOutwardOperatorNames()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override TDDateTime ReturnDepartDateTime()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		protected override string[] GetReturnOperatorNames()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		public override TDLocation ReturnDepartLocation()
		{
			throw new NotImplementedException("Method obsolete in implementation");
		}

		[Obsolete]
		protected override int GetReturnInterchangeCount()
		{
			throw new NotImplementedException("Method obsolete in implementation");
        }
#pragma warning restore 809

        #endregion
    }
}
