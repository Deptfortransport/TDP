// *********************************************** 
// NAME         : PublicJourneyDetail.cs
// AUTHOR       : Andrew Toner
// DATE CREATED : 10/08/2003 
// DESCRIPTION  : Implementation of the PublicJourneyDetail class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourneyDetail.cs-arc  $
//
//   Rev 1.16   Mar 21 2013 10:12:58   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.15   Mar 19 2013 12:03:22   mmodi
//Updates to accessible icons logic
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.14   Jan 30 2013 15:45:12   mmodi
//Updated accessible service icon for Tram mode
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.13   Jan 24 2013 13:23:18   mmodi
//Deleted commented out accessible code
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.12   Jan 21 2013 12:59:00   dlane
//Clarifying classes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.11   Dec 11 2012 10:20:20   mmodi
//Commented out dummy accessible features
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.10   Dec 05 2012 14:16:30   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.9   Mar 17 2010 15:25:22   mmodi
//Overloaded TidyStationName method to not remove the "Station" suffix
//Resolution for 5465: TD Extra - "Station" should not be removed from the stop names displayed
//
//   Rev 1.8   Feb 24 2010 14:45:06   mmodi
//Exposed Vehicle features as a property
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 19 2010 10:39:06   RBroddle
//Added Distance property
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 16 2010 17:50:46   mmodi
//Updated to add intermediate legs
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Jan 07 2010 14:19:52   mmodi
//Store Via location grid reference so they can be used to add via point on a map
//Resolution for 5358: Maps - Via location icons not shown on journey map
//
//   Rev 1.4   Apr 30 2009 16:28:40   jfrank
//Changes made for trapeze journeyWeb notes CCN0502
//Resolution for 5287: Trapeze Journey Web Notes
//
//   Rev 1.3   Feb 18 2009 18:14:02   mmodi
//Hold the fare route code
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.2   Feb 02 2009 16:35:46   mmodi
//Added properties to indicate if this detail is Routing Guide compliant
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.1   Mar 20 2008 10:11:46   mturner
//Del10 patch1 from Dev factory
//
//   Rev 1.0   Nov 08 2007 12:23:56   mturner
//Initial revision.
//
//   Rev 1.49   Oct 06 2006 10:43:38   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.48.1.0   Aug 14 2006 10:53:56   esevern
//Find nearest car park amendments
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.48   Apr 25 2006 17:31:12   COwczarek
//PopulateLocation checks if naptan associated with location
//has invalid OSGR and if so, assigns it the same OSGR as that
//assigned to the location itself (obtained from naptan cache).
//Resolution for 3853: DN068 Adjust: Location name lost after adjust (causes map button issue)
//
//   Rev 1.47   Mar 14 2006 08:41:36   build
//Automatically merged from branch for stream3353
//
//   Rev 1.46.1.2   Feb 17 2006 14:11:46   NMoorhouse
//Updates (by Richard Hopkins) to support Replan Maps
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.46.1.1   Feb 15 2006 15:35:26   tmollart
//Added two new properties to return the required start/end times.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.46.1.0   Jan 26 2006 20:10:50   rhopkins
//Inherit from JourneyLeg class and move some properties and methods to that class.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.46   Dec 07 2005 12:02:40   jgeorge
//Add initialisation code to default constructor to prevent XML deserialization issues.
//Resolution for 3336: Pricing Transactions not working
//
//   Rev 1.45   Aug 25 2005 14:41:14   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.44   Aug 24 2005 15:48:58   RPhilpott
//Do not create Geometry array until it is required.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.43   Aug 19 2005 14:04:18   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.42.1.15   Aug 16 2005 14:32:28   RPhilpott
//FxCop fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.14   Aug 11 2005 15:51:56   RPhilpott
//Minor correction to Bay Text.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.13   Aug 09 2005 18:13:22   rgreenwood
//DD073 Map Details: Added check to drop invalid intermediate locations from the geometry array, and removed an over-engineered test to check geometry validity
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.12   Aug 08 2005 14:27:46   RWilby
//Changes for Bay Text / CJP Interface 7.3.0.7
//
//   Rev 1.42.1.11   Aug 04 2005 17:03:20   rgreenwood
//DD073 Map Details: Changed back HasInvalidCoordinates property. Previous chaneg was a stream merge mistake.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.10   Aug 04 2005 16:58:44   rgreenwood
//DD073 Map Details: Changed HasInvalidCoordinates property and added checks for Start, End and geometry points in each publicjourneydetail object to trigger map key display.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.9   Aug 02 2005 19:41:58   RPhilpott
//Correct handling of passing points and pickup/setdown only points.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.8   Jul 28 2005 11:48:18   RPhilpott
//Minor change to HasInvalidCoordinates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.7   Jul 28 2005 10:31:50   RWilby
//Changed implementation of DisplayNotes property to comply with FxCop
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.6   Jul 27 2005 14:07:06   RWilby
//Fixed issue with BAY_TEXT_TEST constant
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.5   Jul 27 2005 10:56:00   RWilby
//Changes to comply with FxCop
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.4   Jul 25 2005 15:49:18   rgreenwood
//DD073 Map Details: Added HasInvalidCoordinates property, set just before databind.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.3   Jul 25 2005 13:02:02   RWilby
//IR 2572.DN062 DRT Notes and Bay Numbers. Added DisplayNotes property. Changed constructor to iterate through Leg.notes adding each note to displayNotes arraylist. Changed constructor to check for ModeType.Drt, if so, mode field is overwrittern with ModeType.Bus. Changed PopulateLocation method to take bayText parameter and to append the Bay Text to TDLocation.Description.
//
//   Rev 1.42.1.2   Jul 14 2005 13:31:42   RPhilpott
//Correct backward compatibility of legacy properties.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.1   Jul 13 2005 16:56:00   RPhilpott
//Use PublicJourneyCallingPoints for schedule locations.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42.1.0   Jul 07 2005 15:50:10   rgreenwood
//DN062 OnBoard Facilities: Removed some properties and code from PublicJourneyDetail() method that populates the vehicles features (now in Adapters\VehicleFeatureToDtoConvertor.cs class). In same method, added check for vehicleFeatures.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.42   Apr 05 2005 16:40:08   RPhilpott
//Unit test fix.
//
//   Rev 1.41   Apr 05 2005 15:02:14   RPhilpott
//Default reservations, etc, to single space instead of empty string. 
//
//   Rev 1.40   Mar 22 2005 16:13:32   RPhilpott
//Move shared Naptan lookup and caching code to new NaptanLookup class.
//
//   Rev 1.39   Mar 02 2005 14:35:34   rscott
//DEL 7 - updated to include new properties originLocation and originDateTime
//
//   Rev 1.38   Feb 23 2005 15:07:48   rscott
//DEL 7 Update - New Properties Added
//
//   Rev 1.37   Feb 14 2005 11:01:34   jgeorge
//Modified error message to include location name when no stops found for a naptan
//Resolution for 1910: Change logging level for location containing no Naptans DEL 7
//
//   Rev 1.36   Jan 26 2005 10:13:14   jgeorge
//Changed error level from Info to Error for OE when no stops corresponding to a Naptan are found.
//
//   Rev 1.35   Jan 20 2005 11:43:00   RScott
//Updated vehicleFeatures in PublicJourneyDetail
//
//   Rev 1.34   Jan 20 2005 10:07:50   RScott
//DEL 7 Updated to include vehicle Features (Reservation,SeatingClass,SleeperClass,Catering)
//
//   Rev 1.33   Oct 13 2004 09:42:30   RPhilpott
//Get airport/terminal name from terminal's NaPTAN description from GIS Query.
//Resolution for 1703: Airport names in Find-A-Flight results should include terminal number.
//
//   Rev 1.32   Oct 08 2004 17:59:56   RPhilpott
//Look up OSGR's using GIS Query for NaPTAN's when Travelines return OSGR's of (0,0) (as opposed to no OSGR at all).
//Resolution for 1699: Not correctly looking up (0,0) OSGR's
//
//   Rev 1.31   Sep 21 2004 20:08:02   RPhilpott
//Make handling of cached naptans a bit cleverer to prevent unnecessary searches when we have no name and cannot find naptan.
//
//   Rev 1.30   Sep 21 2004 17:25:44   RPhilpott
//Use location name obtained from GIS Query if no name provided by CJP/travelines, and include this name in the cache.  
//Resolution for 1612: Find-A-Train - initial walk leg can cause Null Reference Exception
//
//   Rev 1.29   Sep 09 2004 17:48:56   RPhilpott
//Change Find-A-Flight via location to pass new "group" NaPTAN instead of individual terminals.
//Resolution for 1402: Find a Flight STN to BEB via 9200GLA gives no results
//Resolution for 1455: Air stopovers returns no journeys.
//
//   Rev 1.28   Sep 02 2004 20:27:28   RPhilpott
//Translate Naptan to Airport name using AirDataProvider instead of getting it from previous/next leg, so that we can use it for final destination as well as alighting point.
//Resolution for 1354: Find A Flight results - wrong origin/dest location on output
//
//   Rev 1.27   Aug 20 2004 12:37:40   RPhilpott
//Convert naptans in result from 920F* to 9200* before doing OSGR lookup.
//Resolution for 1376: Find a flight journey map not displayed
//
//   Rev 1.26   Aug 20 2004 12:15:44   jgeorge
//IR1354
//
//   Rev 1.25   Jun 25 2004 09:36:34   RPhilpott
//Integrate Check-in, Check-out and transfer details into main Air flight leg.
//
//   Rev 1.24   May 14 2004 14:55:18   jmorrissey
//Updated properties
//
//   Rev 1.23   May 14 2004 12:04:48   jmorrissey
//Added CheckinDateTime and ExitDateTime properties
//
//   Rev 1.22   May 10 2004 15:04:22   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.21   Mar 03 2004 12:41:46   geaton
//Added XML attribute tag to allow deserialization of subclasses of PublicJourneyDetail. This is required to support the Transaction Injector component.
//
//   Rev 1.20   Feb 19 2004 17:05:36   COwczarek
//Refactored PublicJourneyDetail into new class hierarchy representing different journey leg types (timed, continuous and frequency based)
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.19   Nov 18 2003 09:20:18   PNorell
//Fixed the compile error I introduced.
//
//   Rev 1.18   Nov 17 2003 17:43:50   Pnorell
//Fixed cachine error.
//
//   Rev 1.17   Nov 12 2003 15:05:46   geaton
//Provided setters for all properties to allow serialisation/deserialisation by injector.
//
//   Rev 1.16   Nov 06 2003 17:05:20   PNorell
//Updated the property key to have a conformant name.
//
//   Rev 1.15   Nov 06 2003 16:58:08   PNorell
//Updated property key to indicate which type is used for the setting.
//
//   Rev 1.14   Nov 06 2003 16:27:06   PNorell
//Updated the journey details to cache the results.
//
//   Rev 1.13   Oct 21 2003 15:12:26   kcheung
//Added try block to catch exception thrown by the Query object of the GIS Interface. Also added test for verbose before adding.
//
//   Rev 1.12   Oct 17 2003 19:17:02   RPhilpott
//Don't try to get time of non-existent destinations.
//
//   Rev 1.11   Oct 17 2003 13:54:46   kcheung
//Fixed so that the duration of a leg of a journey is now in seconds rather than minutes
//
//   Rev 1.10   Oct 15 2003 21:52:32   acaunt
//destinationiDateTime and intermediatLocations attributes added
//
//   Rev 1.9   Oct 13 2003 12:43:48   RPhilpott
//Tidy up OSGR getting for NAPTANs
//
//   Rev 1.8   Oct 09 2003 19:52:16   PNorell
//Fixed os gridreferences.
//
//   Rev 1.7   Sep 26 2003 11:47:14   geaton
//Added default constructor to allow XML serialisation of class for use by Event Logging Service publishers.
//
//   Rev 1.6   Sep 12 2003 16:30:08   PNorell
//Fixed buggy geometry collection algorithm.
//
//   Rev 1.5   Sep 11 2003 16:34:12   jcotton
//Made Class Serializable
//
//   Rev 1.4   Sep 05 2003 15:28:50   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.3   Aug 29 2003 14:14:56   jcotton
//Corrections to AddCoordinates
//
//A parameter passed as a 'ref' must exist to be passed and this is not always the case for its parameter 'grid'. Using the 'out' keyword is not appropriate either, so the method was change to return the required updated array.
//          
//In addition, casting of OSGridReference objects in an ArrayList to a Coordinate objects in an array is required and this could not be achieved implicitly and hence was an issue using the original ArrayList.CopyTo approach.
//
//   Rev 1.2   Aug 20 2003 17:55:50   AToner
//Work in progress
using System;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;

using ICJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Summary description for PublicJourneyDetail.
    /// </summary>
    [Serializable()]
	[XmlInclude(typeof(PublicJourneyTimedDetail)), 
    XmlInclude(typeof(PublicJourneyFrequencyDetail)), 
    XmlInclude(typeof(PublicJourneyContinuousDetail)),
    XmlInclude(typeof(PublicJourneyInterchangeDetail))]
    public abstract class PublicJourneyDetail : JourneyLeg
    {
    
        #region Constants and definitions
		static private string airPrefix     = Properties.Current[string.Format(CultureInfo.InvariantCulture, LSKeys.NaptanPrefixProperties, StationType.Airport.ToString())];
		static private string railPrefix    = Properties.Current[string.Format(CultureInfo.InvariantCulture, LSKeys.NaptanPrefixProperties, StationType.Rail.ToString())];
		static private string airViaPrefix  = Properties.Current[LSKeys.NaptanPrefixAirportVia];

        static private ArrayList trapezeRegions = null; // This will be initialised in the static Create method

        #endregion

        #region Private members

        private bool isValidated;
  
		private PublicJourneyCallingPoint origin;					// where service started (may be same as legStart)
		private PublicJourneyCallingPoint destination;				// where service terminates (may be same as legEnd)

		private PublicJourneyCallingPoint[] intermediatesBefore;	// between origin & legStart (excl)
		private PublicJourneyCallingPoint[] intermediatesLeg;		// between legStart & legEnd (excl)
		private PublicJourneyCallingPoint[] intermediatesAfter;		// between legEnd & destination (excl)

        private string region = string.Empty;
		private ServiceDetails[] services;
        private OSGridReference[] geometry;
        private bool includesVia;
        private OSGridReference viaLocationOSGR = null;

		private int[] vehicleFeatures;

        private bool isRoutingGuideCompliant = false;
        private int routingGuideSectionIndex = -1;
        private string[] fareRouteCodes = new string[0];

		// following are specific to Find-A-Flight results ...
		private TDDateTime flightDepartTime;
		private TDDateTime flightArriveTime;		
		private TDDateTime checkInTime;
		private TDDateTime exitTime;		
		private string transferDescription;

		/// <summary>
		/// Used to stored display notes
		/// </summary>
		private ArrayList displayNotes = new ArrayList();
        private bool isTrapezeRegion = false;

        //Used for international journeys
        private int distance = -1;

        // Accessible information
        private List<AccessibilityType> serviceAccessibility = new List<AccessibilityType>();
        private List<AccessibilityType> stopBoardAccessibility = new List<AccessibilityType>();
        private List<AccessibilityType> stopAlightAccessibility = new List<AccessibilityType>();

        // For displaying debug info in UI
        private List<string> debugInfo = new List<string>();

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor - defined to allow XML serialisation.
        /// </summary>
        protected PublicJourneyDetail()
        {
			legStart = new PublicJourneyCallingPoint();
			legEnd = new PublicJourneyCallingPoint();
			vehicleFeatures = new int[0];
		}

		/// <summary>
		/// Takes a CJP leg and creates a new instance of this 
		/// class populated with the leg information
		/// </summary>
		/// <param name="leg">A journey leg passed back from the CJP</param>
		/// <param name="publicVia">A journey via location</param>
		/// <param name="previousLeg">The previous leg. Necessary only for air journeys</param>
		/// <param name="subsequentLeg">The subsequent leg. Necessary only for air journeys</param>
        protected PublicJourneyDetail(Leg leg, TDLocation publicVia, Leg previousLeg, Leg subsequentLeg)
        {
            #region Mode

            mode = leg.mode;

            //Overide ModeType.Drt with Bus
            if (mode == ModeType.Drt)
                mode = ModeType.Bus;

            isValidated = leg.validated;

            #endregion

            #region CheckIn time

            // If previousLeg is provided and is of the correct type, then use it for check in time
            if ((previousLeg != null) && (previousLeg.mode == ModeType.CheckIn))
            {
                checkInTime = new TDDateTime(previousLeg.board.departTime);
            }

            #endregion

            #region PublicJourneyCallingPoints for Leg: origin -> start -> end -> destination

            #region PublicJourneyCallingPoint Leg origin and start

            PublicJourneyCallingPointType startType;

            if (leg.origin == null)
            {
                startType = PublicJourneyCallingPointType.OriginAndBoard;
            }
            else
            {
                startType = ((leg.board.stop.NaPTANID == leg.origin.stop.NaPTANID)
                    ? PublicJourneyCallingPointType.OriginAndBoard
                    : PublicJourneyCallingPointType.Board);
            }

            TDLocation startLocation = new TDLocation(leg.board);
            PopulateLocation(startLocation, leg.board.stop.bay);

            legStart = new PublicJourneyCallingPoint(startLocation,
                                                        new TDDateTime(leg.board.arriveTime),
                                                        new TDDateTime(leg.board.departTime),
                                                        startType);

            if (startType == PublicJourneyCallingPointType.OriginAndBoard)
            {
                origin = legStart;
            }
            else if (leg.origin != null)
            {
                origin = new PublicJourneyCallingPoint(new TDLocation(leg.origin), null, new TDDateTime(leg.origin.departTime),
                                                        PublicJourneyCallingPointType.Origin);
            }

            #endregion

            #region PublicJourneyCallingPoint Leg destination and end

            PublicJourneyCallingPointType endType;

            if (leg.destination == null)
            {
                endType = PublicJourneyCallingPointType.DestinationAndAlight;
            }
            else
            {
                endType = ((leg.alight.stop.NaPTANID == leg.destination.stop.NaPTANID)
                    ? PublicJourneyCallingPointType.DestinationAndAlight
                    : PublicJourneyCallingPointType.Alight);
            }

            TDLocation endLocation = new TDLocation(leg.alight);
            PopulateLocation(endLocation, leg.alight.stop.bay);

            legEnd = new PublicJourneyCallingPoint(endLocation,
                new TDDateTime(leg.alight.arriveTime),
                new TDDateTime(leg.alight.departTime),
                endType);

            if (endType == PublicJourneyCallingPointType.DestinationAndAlight)
            {
                destination = legEnd;
            }
            else if (leg.destination != null)
            {
                destination = new PublicJourneyCallingPoint(new TDLocation(leg.destination), new TDDateTime(leg.destination.arriveTime), null,
                    PublicJourneyCallingPointType.Destination);
            }

            #endregion

            #endregion

            #region CheckOut exit time
            // If subsequentLeg is provided and is of the correct type, then use it for exit time
            if ((subsequentLeg != null) && ((subsequentLeg.mode == ModeType.CheckOut) || (subsequentLeg.mode == ModeType.Transfer)))
            {
                exitTime = new TDDateTime(subsequentLeg.alight.arriveTime);

                if (subsequentLeg.mode == ModeType.Transfer)
                {
                    transferDescription = subsequentLeg.description;
                }
            }

            #endregion

            #region Flight depart and arrive time

            if (mode == ModeType.Air)
            {
                flightDepartTime = legStart.DepartureDateTime;
                flightArriveTime = legEnd.ArrivalDateTime;
            }

            #endregion

            #region Vehicle features

            if (leg.vehicleFeatures != null && leg.vehicleFeatures.Length > 0)
            {
                vehicleFeatures = new int[leg.vehicleFeatures.Length];

                int i = 0;

                foreach (VehicleFeature vf in leg.vehicleFeatures)
                {
                    vehicleFeatures[i] = vf.id;
                    i++;
                }
            }
            else
            {
                vehicleFeatures = new int[0];
            }

            #endregion

            #region Region

            // Set the region
            if (!string.IsNullOrEmpty(leg.region))
                region = leg.region;

            // Set the trapeze region flag, to ensure the Display notes get formatted correctly when accessed
            if (trapezeRegions != null)
            {
                isTrapezeRegion = (trapezeRegions.Contains(Region));
            }

            #endregion

            #region Service details

            // Populate the services property
            if (leg.services != null)
            {
                services = new ServiceDetails[leg.services.Length];
                for (int i = 0; i < leg.services.Length; i++)
                {
                    services[i] = new ServiceDetails(leg.services[i].operatorCode,
                        leg.services[i].operatorName,
                        leg.services[i].serviceNumber,
                        leg.services[i].destinationBoard,
                        leg.services[i].direction,
                        leg.services[i].privateID,
                        leg.services[i].retailId);
                }
            }

            #endregion

            #region Intermediate stops

            TDDateTime arriveTime = DateTime.MinValue;
            TDDateTime departTime = DateTime.MinValue;

            #region Before Leg

            if (leg.intermediatesA != null)
            {
                intermediatesBefore = new PublicJourneyCallingPoint[leg.intermediatesA.Length];

                for (int i = 0; i < leg.intermediatesA.Length; i++)
                {
                    PublicJourneyCallingPointType type = ((leg.intermediatesA[i].activity == ActivityType.Pass)
                                                            ? PublicJourneyCallingPointType.PassingPoint
                                                            : PublicJourneyCallingPointType.CallingPoint);

                    if (leg.intermediatesA[i].activity == ActivityType.Pass || leg.intermediatesA[i].activity == ActivityType.Depart)
                    {
                        arriveTime = DateTime.MinValue;
                        departTime = new TDDateTime(leg.intermediatesA[i].departTime);
                    }
                    else if (leg.intermediatesA[i].activity == ActivityType.Arrive)
                    {
                        arriveTime = new TDDateTime(leg.intermediatesA[i].arriveTime);
                        departTime = DateTime.MinValue;
                    }
                    else
                    {
                        arriveTime = new TDDateTime(leg.intermediatesA[i].arriveTime);
                        departTime = new TDDateTime(leg.intermediatesA[i].departTime);
                    }

                    intermediatesBefore[i] = new PublicJourneyCallingPoint(new TDLocation(leg.intermediatesA[i]),
                                                                            arriveTime, departTime, type);
                }
            }
            else
            {
                intermediatesBefore = new PublicJourneyCallingPoint[0];
            }

            #endregion

            #region During Leg

            if (leg.intermediatesB != null)
            {
                intermediatesLeg = new PublicJourneyCallingPoint[leg.intermediatesB.Length];

                for (int i = 0; i < leg.intermediatesB.Length; i++)
                {
                    PublicJourneyCallingPointType type = ((leg.intermediatesB[i].activity == ActivityType.Pass)
                        ? PublicJourneyCallingPointType.PassingPoint
                        : PublicJourneyCallingPointType.CallingPoint);

                    TDLocation loc = new TDLocation(leg.intermediatesB[i]);

                    PopulateLocation(loc, string.Empty);

                    if (leg.intermediatesB[i].activity == ActivityType.Pass || leg.intermediatesB[i].activity == ActivityType.Depart)
                    {
                        arriveTime = DateTime.MinValue;
                        departTime = new TDDateTime(leg.intermediatesB[i].departTime);
                    }
                    else if (leg.intermediatesB[i].activity == ActivityType.Arrive)
                    {
                        arriveTime = new TDDateTime(leg.intermediatesB[i].arriveTime);
                        departTime = DateTime.MinValue;
                    }
                    else
                    {
                        arriveTime = new TDDateTime(leg.intermediatesB[i].arriveTime);
                        departTime = new TDDateTime(leg.intermediatesB[i].departTime);
                    }

                    intermediatesLeg[i] = new PublicJourneyCallingPoint(loc, arriveTime, departTime, type);
                }
            }
            else
            {
                intermediatesLeg = new PublicJourneyCallingPoint[0];
            }

            #endregion

            #region After Leg

            if (leg.intermediatesC != null)
            {
                intermediatesAfter = new PublicJourneyCallingPoint[leg.intermediatesC.Length];

                for (int i = 0; i < leg.intermediatesC.Length; i++)
                {
                    PublicJourneyCallingPointType type = ((leg.intermediatesC[i].activity == ActivityType.Pass)
                        ? PublicJourneyCallingPointType.PassingPoint
                        : PublicJourneyCallingPointType.CallingPoint);

                    if (leg.intermediatesC[i].activity == ActivityType.Pass || leg.intermediatesC[i].activity == ActivityType.Depart)
                    {
                        arriveTime = DateTime.MinValue;
                        departTime = new TDDateTime(leg.intermediatesC[i].departTime);
                    }
                    else if (leg.intermediatesC[i].activity == ActivityType.Arrive)
                    {
                        arriveTime = new TDDateTime(leg.intermediatesC[i].arriveTime);
                        departTime = DateTime.MinValue;
                    }
                    else
                    {
                        arriveTime = new TDDateTime(leg.intermediatesC[i].arriveTime);
                        departTime = new TDDateTime(leg.intermediatesC[i].departTime);
                    }

                    intermediatesAfter[i] = new PublicJourneyCallingPoint(new TDLocation(leg.intermediatesC[i]),
                                                                                arriveTime, departTime, type);
                }
            }
            else
            {
                intermediatesAfter = new PublicJourneyCallingPoint[0];
            }

            #endregion

            #endregion

            #region Includes Via

            // Check that to see if the start, end or any of the intermediate locations are the via point

            if ((legStart.Location.Intersects(publicVia, StationType.Undetermined)) || (legEnd.Location.Intersects(publicVia, StationType.Undetermined)))
            {
                includesVia = true;
                viaLocationOSGR = publicVia.GridReference;
            }
            else
            {
                // Check the intermediate location to see if any of them is the via point
                for (int i = 0; i < intermediatesLeg.Length; i++)
                {
                    if (intermediatesLeg[i].Location.Intersects(publicVia, StationType.Undetermined))
                    {
                        includesVia = true;
                        viaLocationOSGR = publicVia.GridReference;
                        break;
                    }
                }
            }

            #endregion

            #region Display notes

            //Load leg Display Notes into displayNotes arraylist
            if (leg.notes != null)
            {
                for (int i = 0; i < leg.notes.Length; i++)
                {
                    displayNotes.Add(leg.notes[i].message);
                }
            }

            #endregion

            #region Accessibility details

            // Leg accessibility details for board and alight stops
            if ((leg.board != null) && (leg.board.stop != null) && (leg.board.stop.accessibility != null))
            {
                TransportDirect.JourneyPlanning.CJPInterface.Accessibility cjpAccessibilityBoard = leg.board.stop.accessibility;

                StringBuilder debug = new StringBuilder();

                stopBoardAccessibility = PopulateStopAccessibility(cjpAccessibilityBoard, ref debug);

                if (debug.Length > 0)
                    debugInfo.Add(string.Concat("Board", debug.ToString()));
            }

            if ((leg.alight != null) && (leg.alight.stop != null) && (leg.alight.stop.accessibility != null))
            {
                TransportDirect.JourneyPlanning.CJPInterface.Accessibility cjpAccessibilityAlight = leg.alight.stop.accessibility;

                StringBuilder debug = new StringBuilder();
                
                stopAlightAccessibility = PopulateStopAccessibility(cjpAccessibilityAlight, ref debug);

                if (debug.Length > 0)
                    debugInfo.Add(string.Concat("Alight", debug.ToString()));
            }

            // Leg accessibility details for service
            if (leg.serviceAccessibility != null)
            {
                TransportDirect.JourneyPlanning.CJPInterface.ServiceAccessibility cjpServiceAccessibility = leg.serviceAccessibility;

                serviceAccessibility = PopulateServiceAccessibility(cjpServiceAccessibility);
            }

            #endregion
        }

        /// <summary>
        /// Takes a CJP leg and creates a new intance of this class populated with
        /// the leg information
        /// </summary>
        /// <param name="leg">A journey leg passed back from the CJP</param>
        /// <param name="publicVia">A journey via location</param>
        protected PublicJourneyDetail( Leg leg, TDLocation publicVia) : this( leg, publicVia, null, null)
        { }

		#endregion

		#region Properties

		/// <summary>
		/// Gets the Display Notes
		/// </summary>
        public override string[] GetDisplayNotes()
        {

            if (isTrapezeRegion)
            {
                for (int i = 0; i < displayNotes.Count; i++)
                {
                    displayNotes[i] = displayNotes[i].ToString().Replace("O:", string.Empty);
                    displayNotes[i] = displayNotes[i].ToString().Replace("D:", string.Empty);
                    displayNotes[i] = displayNotes[i].ToString().Replace(";", "\n");
                }
            }

            return (string[])displayNotes.ToArray(typeof(string));
        }

        /// <summary>
        /// Gets and sets the is validated flag.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public bool IsValidated
        {
            get { return isValidated; }
            set { isValidated = value; }
        }

		/// <summary>
		/// Gets the origin calling point.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public PublicJourneyCallingPoint Origin
		{
			get { return origin; }
			set { origin = value; }
		}

		/// <summary>
		/// Gets the destination calling point.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public PublicJourneyCallingPoint Destination
		{
			get { return destination; }
			set { destination = value; }
		}


        /// <summary>
        /// Read/Write. The intermediate calling points between origin and leg start
        /// </summary>
        public PublicJourneyCallingPoint[] IntermediatesBefore
        {
            get { return intermediatesBefore; }
            set { intermediatesBefore = value; }
        }

        /// <summary>
        /// Read/Write. The intermediate calling points between leg start and leg end
        /// </summary>
        public PublicJourneyCallingPoint[] IntermediatesLeg
        {
            get { return intermediatesLeg; }
            set { intermediatesLeg = value; }
        }

        /// <summary>
        /// Read/Write. The intermediate calling points between leg end and destination
        /// </summary>
        public PublicJourneyCallingPoint[] IntermediatesAfter
        {
            get { return intermediatesAfter; }
            set { intermediatesAfter = value; }
        }

		/// <summary>
		/// Gets the intermediate calling points between origin and leg start (exclusive)
		/// </summary>
		public PublicJourneyCallingPoint[] GetIntermediatesBefore()
		{
			return intermediatesBefore; 
		}

		/// <summary>
		/// Gets the intermediate calling points between origin and leg start (exclusive)
		/// </summary>
		public PublicJourneyCallingPoint[] GetIntermediatesLeg()
		{
			return intermediatesLeg;
		}
		
		/// <summary>
		/// Gets the intermediate calling points between origin and leg start (exclusive)
		/// </summary>
		public PublicJourneyCallingPoint[] GetIntermediatesAfter()
		{
			return intermediatesAfter;
		}


        /// <summary>
        /// Gets/Sets the distance (in metres) for the leg.
        /// </summary>
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }


		/// <summary>
		/// Gets and sets the destination datetime.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		[Obsolete("Deprecated - use of Destination.ArrivalDateTime is now preferred", false)]
		public TDDateTime DestinationDateTime
		{
			get { return ((destination == null) ? DateTime.MinValue : destination.ArrivalDateTime); }
			set 
			{
				if	(destination != null) 
				{
					destination.ArrivalDateTime = value; 
				}
			}
		}

		/// <summary>
		/// Gets and sets the origin datetime.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		[Obsolete("Deprecated - use of Origin.DepartureDateTime is now preferred", false)]
		public TDDateTime OriginDateTime
		{
			get { return ((origin == null) ? DateTime.MinValue : origin.DepartureDateTime); }
			set 
			{
				if	(origin != null) 
				{
					origin.DepartureDateTime = value; 
				}
			}
		}

		/// <summary>
        /// Gets and sets the start location.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
		[Obsolete("Deprecated - use of LegStart.Location is now preferred", false)]
		public TDLocation StartLocation
        {
            get { return legStart.Location; }
            set { legStart.Location = value; }
        }

        /// <summary>
        /// Gets and sets the end location.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
		[Obsolete("Deprecated - use of LegEnd.Location is now preferred", false)]
		public TDLocation EndLocation
        {
			get { return legEnd.Location; }
			set { legEnd.Location = value; }
		}

        /// <summary>
        /// Gets and sets the destination location.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
		[Obsolete("Deprecated - use of Destination.Location is now preferred", false)]
		public TDLocation DestinationLocation
        {
			get { Event leg = null; return ((destination == null) ? new TDLocation(leg) : destination.Location); }
			set 
			{ 
				if	(destination != null)
				{
					destination.Location = value; 
				}
				else
				{
					destination = new PublicJourneyCallingPoint(value, DateTime.MinValue, DateTime.MinValue, PublicJourneyCallingPointType.Destination);
				}
			}
        }

		/// <summary>
		/// Gets and sets the origin location.
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		[Obsolete("Deprecated - use of Origin.Location is now preferred", false)]
		public TDLocation OriginLocation
		{
			get { Event leg = null; return ((origin == null) ? new TDLocation(leg) : origin.Location); }
			set 
			{ 
				if	(origin != null)
				{
					origin.Location = value; 
				}
				else
				{
					origin = new PublicJourneyCallingPoint(value, DateTime.MinValue, DateTime.MinValue, PublicJourneyCallingPointType.Origin);
				}
			}
		}

        /// <summary>
        /// Gets the intermediate locations.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
		[Obsolete("Deprecated - use of GetIntermediatesLeg()[].Location is now preferred", false)]
		public TDLocation[] IntermediateLocations
        {
            get 
			{ 
				TDLocation[] tempLocations = new TDLocation[intermediatesLeg.Length];
				
				for (int i = 0; i < intermediatesLeg.Length; i++)
				{
					tempLocations[i] = intermediatesLeg[i].Location;
				}

				return tempLocations; 
			}

			set 
			{
				intermediatesLeg = new PublicJourneyCallingPoint[value.Length];

				for (int i = 0; i < value.Length; i++)
				{
					intermediatesLeg[i] = new PublicJourneyCallingPoint(value[i], null, null, PublicJourneyCallingPointType.CallingPoint);
				}
			}
        }

        /// <summary>
        /// Gets and sets the departure datetime.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
		[Obsolete("Deprecated - use of LegStart.DepartureDateTime is now preferred", false)]
		public TDDateTime DepartDateTime
        {
            get { return legStart.DepartureDateTime; }
            set { legStart.DepartureDateTime = value; }
        }

		/// <summary>
        /// Gets and sets the arrival datetime.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
		[Obsolete("Deprecated - use of LegEnd.ArrivalDateTime is now preferred", false)]
		public TDDateTime ArriveDateTime
        {
            get { return legEnd.ArrivalDateTime; }
            set { legEnd.ArrivalDateTime = value; }
        }

		/// <summary>
		/// Gets the check-in datetime (the time a user 
		/// must arrive at the airport for an Air leg).
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public TDDateTime CheckInTime
		{
			get { return checkInTime; }
			set { checkInTime = value; }
		}

		/// <summary>
		/// Gets exit datetime (the time a user 
		/// can actually leave the airport for an Air leg).
		/// </summary>
		/// <remarks>
		/// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
		/// </remarks>
		public TDDateTime ExitTime
		{
			get { return exitTime; }
			set { exitTime = value; }
		}

		/// <summary>
		/// Gets and sets the flight departure datetime.
		/// </summary>
		/// <remarks>
		/// This time is the same as departure datetime unless checkin time has been set.
		/// </remarks>
		public TDDateTime FlightDepartDateTime
		{
			get { return flightDepartTime; }
			set { flightDepartTime = value; }
		}

		/// <summary>
		/// Gets and sets the flight arrival datetime.
		/// </summary>
		/// <remarks>
		/// This time is the same as arrival datetime unless checkin time has been set.
		/// </remarks>
		public TDDateTime FlightArriveDateTime
		{
			get { return flightArriveTime; }
			set { flightArriveTime = value; }
		}

		/// <summary>
		/// Gets the time for the start of this leg, including any checkin time
		/// </summary>
		public override TDDateTime StartTime
		{
			get
			{
				if (checkInTime != null)
				{
					return checkInTime;
				}
				else
				{
					return legStart.DepartureDateTime;
				}
			}
		}

		/// <summary>
		/// Gets the time for the end of this leg, including any exit time
		/// </summary>
		public override TDDateTime EndTime
		{
			get
			{
				if (exitTime != null)
				{
					return exitTime;
				}
				else
				{
					return legEnd.ArrivalDateTime;
				}
			}
		}

		/// <summary>
		/// Gets and sets the transfer description (text describing the 
		/// airport transfer stage that immediately follows this leg).  
		/// </summary>
		public string TransferDescription
		{
			get { return transferDescription; }
			set { transferDescription = value; }
		}

        /// <summary>
        /// Gets and set the region for this leg
        /// </summary>
        public string Region
        {
            get { return region; }
            set { region = value; }
        }

		/// <summary>
        /// Gets and sets the services.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public ServiceDetails[] Services
        {
            get { return services; }
            set { services = value; }
        }

        /// <summary>
        /// Gets and sets the geometry.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public OSGridReference[] Geometry
        {
			get 
			{ 
				if	(geometry == null)
				{
					ArrayList al = new ArrayList();
	            
					al.Add(legStart.Location.GridReference);
	            
					for  (int i = 0; i < intermediatesLeg.Length; i++)
					{
						if	(intermediatesLeg[i].Location.GridReference.IsValid)
						{
							al.Add(intermediatesLeg[i].Location.GridReference);
						}
					}
	            
					al.Add(legEnd.Location.GridReference);

					geometry = new OSGridReference[al.Count];
					al.CopyTo(geometry); 
				}				
				
				return geometry;
			}
 
			set { geometry = value; }
        
		}

        /// <summary>
        /// Gets and sets the includes via flag.
        /// </summary>
        /// <remarks>
        /// Setter is provided so that TD Transaction Injector can serialize/deserialize the property.
        /// </remarks>
        public bool IncludesVia
        {
            get { return includesVia; }
            set { includesVia = value; }
        }

        /// <summary>
        /// Returns the via location OSGR if the detail goes through the request via location
        /// </summary>
        public OSGridReference ViaLocationOSGR
        {
            get { return viaLocationOSGR; }
            set { viaLocationOSGR = value; }
        }

		/// <summary>
		/// Returns the array of vehicle feature indicators  
		/// </summary>
		public override int[] GetVehicleFeatures()
		{
			return vehicleFeatures; 
		}

        /// <summary>
        /// Read/Write. Allows setting of the vehicle features if needed
        /// </summary>
        public int[] VehicleFeatures
        {
            get { return vehicleFeatures; }
            set { vehicleFeatures = value; }
        }

        /// <summary>
        /// Returns if this public journey detail is routing guide compliant.
        /// </summary>
        public bool RoutingGuideCompliant
        {
            get { return isRoutingGuideCompliant; }
            set { isRoutingGuideCompliant = value; }
        }

        /// <summary>
        /// Returns the routing guide section this public journey detail belongs to.
        /// </summary>
        public int RoutingGuideSectionIndex
        {
            get { return routingGuideSectionIndex; }
            set { routingGuideSectionIndex = value; }
        }

        /// <summary>
        /// Gets/sets the fare route codes applicable to this leg
        /// </summary>
        public string[] FareRouteCodes
        {
            get { return fareRouteCodes; }
            set { fareRouteCodes = value; }
        }

        /// <summary>
        /// Read/Write. Overall accessibility details for the services used in this journey detail
        /// </summary>
        public List<AccessibilityType> ServiceAccessibility
        {
            get { return serviceAccessibility; }
            set { serviceAccessibility = value; }
        }

        /// <summary>
        /// Read/Write. Accessibility details for the Board stop of this journey detail
        /// </summary>
        public List<AccessibilityType> BoardAccessibility
        {
            get { return stopBoardAccessibility; }
            set { stopBoardAccessibility = value; }
        }

        /// <summary>
        /// Read/Write. Accessibility details for the Alight stop of this journey detail
        /// </summary>
        public List<AccessibilityType> AlightAccessibility
        {
            get { return stopAlightAccessibility; }
            set { stopAlightAccessibility = value; }
        }

        /// <summary>
        /// Read/Write. Debug information to display
        /// </summary>
        public List<string> Debug
        {
            get { return debugInfo; }
            set { debugInfo = value; }
        }

		#endregion

        #region Private methods

        #region TDLocation methods

        private void PopulateLocation( TDLocation tdl,string bayText)
        {
            if( tdl.NaPTANs.Length < 1 )
            {
                // Log the exception
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error, "Location " + tdl.Description + " contains no NaPTANs"));               
                return;
            }
            
			// Grab the main naptan - this should really be the only naptan as well for intermediate locations
            TDNaptan mainNaptan = tdl.NaPTANs[0];
            
			if  (mainNaptan.Naptan == null || mainNaptan.Naptan.Length == 0 || mainNaptan.Naptan == "Origin" || mainNaptan.Naptan == "Destination")
            {
                // This is for unknown location and origin/end locations if they end in a non-naptan place
                // for instance last walking leg
                return;
            }

			// If the naptan in this location is an internal airport naptan it will represent
			// a specific airline arrival/departure desk, so we need to substitute something more 
			// meaningful.
			
			string[] naptans = new String[tdl.NaPTANs.Length];

            for( int i = 0; i < naptans.Length; i++)
            {
				if  ((mode == ModeType.Air || mode == ModeType.CheckIn || mode == ModeType.CheckOut || mode == ModeType.Transfer)
					  && (tdl.NaPTANs[i].Naptan.StartsWith(airViaPrefix)))
				{
					naptans[i] = airPrefix + tdl.NaPTANs[i].Naptan.Substring(airViaPrefix.Length);
				}
				else
				{
					naptans[i] = tdl.NaPTANs[i].Naptan;
				}
            }

			
			if	(tdl.Locality != null && tdl.Locality.Length > 0
					&& (mainNaptan.GridReference.Easting > 0 && mainNaptan.GridReference.Northing > 0)
					&& (tdl.Description.Length > 0))
			{
				// Is already populated with enough data - no need to progress further.
				return;
			}
            
			// Lookup naptan in cache and/or GIS query ...

			NaptanCacheEntry naptanCacheEntry = NaptanLookup.Get(mainNaptan.Naptan, tdl.Description);
			
			if	(naptanCacheEntry.Found)
			{
				tdl.Locality = naptanCacheEntry.Locality;
	
				// only use returned OSGR if it has non-default and sensible values ... 	
				
				if	(naptanCacheEntry.OSGR.Easting > 0 && naptanCacheEntry.OSGR.Northing > 0)
				{
					tdl.GridReference = naptanCacheEntry.OSGR;

                    if (!mainNaptan.GridReference.IsValid) 
                    {
                        mainNaptan.GridReference = naptanCacheEntry.OSGR;
                    }
				}
						
				// At present, we only want to use the naptan name from the 
				//  Stops database if the CJP hasn't returned a name, or if 
				//  this is an airport (and the CJP has therefore returned 
				//  the air-side coded name) ...
				
				if (naptans[0].StartsWith(airPrefix)) 
				{
					tdl.Description = naptanCacheEntry.Description;								
				}
				else if	(tdl.Description.Length < 1)
				{
					if	(naptans[0].StartsWith(railPrefix)) 
					{
						tdl.Description = TidyStationName(naptanCacheEntry.Description);								
					}
					else
					{
						tdl.Description = naptanCacheEntry.Description;
					}
				}

			}

			//Append Bay numbers
			if(bayText != null && bayText.Length != 0)
			{
				if(tdl.Locality != null && tdl.Locality.Length != 0)
				{
					
					//Get Traveline for Locality using ILocalityLookup
					ILocalityTravelineLookup LocalityTravelineLookup = 
						(ILocalityTravelineLookup) TDServiceDiscovery.Current[ServiceDiscoveryKey.LocalityTravelineLookup];

					string Traveline  = LocalityTravelineLookup.GetTraveline(tdl.Locality);

					if (Traveline.Length != 0)
					{
						//Test if bay Text is displayable for Traveline
						IBayTextFilter BayTextFilter = (IBayTextFilter)	TDServiceDiscovery.Current[ServiceDiscoveryKey.BayTextFilter];
						bool AppendBayText =  BayTextFilter.FilterText(Traveline);

						if (AppendBayText)
						{
							StringBuilder sb = new StringBuilder();
							sb.Append(tdl.Description);
							sb.Append(" (");
							sb.Append(bayText);
							sb.Append(")");
							tdl.Description = sb.ToString(); 
						}
					}
				}
			}
        }

        #endregion

        /// <summary>
        /// Populates the service accessibility list using the CJP ServiceAccessibility detail
        /// </summary>
        /// <param name="cjpServiceAccessibility"></param>
        private List<AccessibilityType> PopulateServiceAccessibility(ICJP.ServiceAccessibility cjpServiceAccessibility)
        {
            List<AccessibilityType> accessibility = new List<AccessibilityType>();

            // Display service accessible icon based on the the Mode and the accessible flag returned
            // See DN11802 for rules to explain the display logic below

            switch (mode)
            {
                case ModeType.Rail:
                case ModeType.Coach:
                    if (cjpServiceAccessibility.wheelchairBookingRequired)
                        accessibility.Add(AccessibilityType.ServiceWheelchairBookingRequired);
                    else if (cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True)
                        accessibility.Add(AccessibilityType.ServiceMobilityImpairedAccess);
                    break;

                case ModeType.Ferry:
                case ModeType.Underground:
                case ModeType.Telecabine:
                    if (cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True)
                        accessibility.Add(AccessibilityType.ServiceMobilityImpairedAccessService);
                    break;

                case ModeType.Bus:
                    if (cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True
                        && cjpServiceAccessibility.lowFloor)
                        accessibility.Add(AccessibilityType.ServiceLowFloor);
                    else if (cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True)
                        accessibility.Add(AccessibilityType.ServiceMobilityImpairedAccessBus);
                    break;

                case ModeType.Tram:
                case ModeType.Metro:
                    if (cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True
                        && cjpServiceAccessibility.lowFloor)
                        accessibility.Add(AccessibilityType.ServiceLowFloorTram);
                    else if (cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True)
                        accessibility.Add(AccessibilityType.ServiceMobilityImpairedAccess);
                    break;
            }

            #region Debug

            // Debug info
            StringBuilder debug = new StringBuilder();
            debug.Append("ServiceAccessibility[");
            debug.Append(cjpServiceAccessibility.wheelchairBookingRequired ? "wheelchairBookingRequired " : string.Empty);
            debug.Append(cjpServiceAccessibility.lowFloor ? "lowFloor " : string.Empty);
            debug.Append(cjpServiceAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True ? "mobilityImpairedAccess" : string.Empty);
            debug.Append("]");
            
            debugInfo.Add(debug.ToString());
            
            #endregion

            // Assistance available for the service
            if (cjpServiceAccessibility.assistanceServices != null)
            {
                foreach (ICJP.AssistanceServiceType ast in cjpServiceAccessibility.assistanceServices)
                {
                    accessibility.Add(AccessibilityTypeHelper.GetAccessibilityType(ast));
                }
                
                #region Debug

                debug = new StringBuilder();

                debug.Append("AssistanceServices[");

                foreach (ICJP.AssistanceServiceType ast in cjpServiceAccessibility.assistanceServices)
                {
                    debug.Append(ast.ToString() + " ");
                }

                debug.Append("]");

                if (cjpServiceAccessibility.assistanceServices.Length > 0)
                    debugInfo.Add(debug.ToString());

                #endregion
            }

            return accessibility;
        }

        /// <summary>
        /// Populates the stop accessibility list using the CJP Accessibility detail,
        /// and filters out accessibility types not required for output
        /// </summary>
        /// <param name="cjpAccessibility"></param>
        protected List<AccessibilityType> PopulateStopAccessibility(ICJP.Accessibility cjpAccessibility, ref StringBuilder debug)
        {
            // NOTE: Commented out lines are not required for output

            List<AccessibilityType> accessibility = new List<AccessibilityType>();

            debug.Append("StopAccessibility[");
            bool accessible = false;

            if (cjpAccessibility.escalatorFreeAccess == ICJP.AccessibilityType.True)
            {
                //accessibility.Add(AccessibilityType.EscalatorFreeAccess);
                debug.Append("escalatorFreeAccess ");
                accessible = true;
            }
            if (cjpAccessibility.liftFreeAccess == ICJP.AccessibilityType.True)
            {
                //accessibility.Add(AccessibilityType.LiftFreeAccess);
                debug.Append("liftFreeAccess ");
                accessible = true;
            }
            if (cjpAccessibility.mobilityImpairedAccess == ICJP.AccessibilityType.True)
            {
                //accessibility.Add(AccessibilityType.MobilityImpairedAccess);
                debug.Append("mobilityImpairedAccess ");
                accessible = true;
            }
            if (cjpAccessibility.stepFreeAccess == ICJP.AccessibilityType.True)
            {
                //accessibility.Add(AccessibilityType.StepFreeAccess);
                debug.Append("stepFreeAccess ");
                accessible = true;
            }
            if (cjpAccessibility.wheelchairAccess == ICJP.AccessibilityType.True)
            {
                accessibility.Add(AccessibilityType.WheelchairAccess);
                debug.Append("wheelchairAccess");
                accessible = true;
            }
            debug.Append("]");

            if (!accessible)
                debug = new StringBuilder(); // No debug info to display

            return accessibility;
        }

        /// <summary>
        /// Populates the leg accessibility list using the CJP Accessibility detail,
        /// and filters out accessibility types not required for output
        /// </summary>
        protected List<AccessibilityType> PopulateLegAccessibility(ICJP.Accessibility cjpAccessibility, ref StringBuilder debug)
        {
            List<AccessibilityType> accessibility = new List<AccessibilityType>();

            if (cjpAccessibility.accessSummary != null)
            {
                foreach (AccessSummary accessSummary in cjpAccessibility.accessSummary)
                {
                    AccessibilityType sat = AccessibilityTypeHelper.GetAccessibilityType(accessSummary);

                    // Only keep those wanted for UI output
                    if (sat == AccessibilityType.AccessLiftDown
                        || sat == AccessibilityType.AccessLiftUp
                        || sat == AccessibilityType.AccessEscalatorDown
                        || sat == AccessibilityType.AccessEscalatorUp
                        || sat == AccessibilityType.AccessStairsDown
                        || sat == AccessibilityType.AccessStairsUp
                        || sat == AccessibilityType.AccessRampDown
                        || sat == AccessibilityType.AccessRampUp)
                    {
                        // Display all even if there are duplicates
                        accessibility.Add(sat);
                    }
                }

                #region Debug

                debug.Append("LegAccessibility AccessSummary[");

                foreach (AccessSummary accessSummary in cjpAccessibility.accessSummary)
                {
                    try
                    {
                        debug.Append(string.Format("{0}|{1}|{2} ", accessSummary.accessFeature.ToString(), accessSummary.transition.ToString(), accessSummary.count));
                    }
                    catch
                    {
                        // Ignore exceptions
                    }
                }

                debug.Append("]");

                if (cjpAccessibility.accessSummary.Length <= 0)
                    debug = new StringBuilder(); // No AccessSummary so clear

                #endregion
            }

            return accessibility;
        }

        #endregion

        #region Public Static methods - Create and Tidy

        /// <summary>
        /// Method to tidy up a Station name, with the name having "Station" at the end of it removed 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string TidyStationName(string inputString)
        {
            return TidyStationName(inputString, true);
        }

        /// <summary>
        /// Method to tidy up a Station name
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="removeStationSuffix">True will remove "Station" from the end of the name</param>
        /// <returns></returns>
        public static string TidyStationName(string inputString, bool removeStationSuffix)
		{
		
			// strip off "Station" suffix, if necessary ...
			if	(removeStationSuffix && (inputString.EndsWith("Station")))
			{
				inputString = inputString.Substring(0, (inputString.Length - 8));
			}
			
			// now convert the rest into prettier mixed case ...

			StringBuilder outputString = new StringBuilder(inputString.Length);	
				
			bool wordBreak = true; 
			bool firstHyphen = true; 
				
			for (int i = 0; i < inputString.Length; i++) 
			{
				if	(wordBreak)
				{
					outputString.Append(Char.ToUpper(inputString[i], CultureInfo.InvariantCulture));
					
					if	(!Char.IsPunctuation(inputString[i]) && !Char.IsWhiteSpace(inputString[i]))
					{
						wordBreak = false;
					}
				}
				else if	(Char.IsLetterOrDigit(inputString[i]))
				{
					outputString.Append(Char.ToLower(inputString[i], CultureInfo.InvariantCulture));
				}
				else
				{
					outputString.Append(inputString[i]);
						
					if	(Char.IsWhiteSpace(inputString[i]))
					{
						wordBreak = true;
						firstHyphen = true;
						continue;
						
					}

					if	(Char.IsPunctuation(inputString[i]))
					{
						if	(inputString[i] == '-')
						{
							if	(!firstHyphen)
							{
								wordBreak = true;
							}
							else
							{
								firstHyphen = false;
							}
						}
						else
						{
							wordBreak = true;
						}
					}
				}
			}
			
			return outputString.ToString();
		}
        
        /// <summary>
        /// Factory method that creates an appropriate subclass of PublicJourneyDetail
        /// based on the type of leg supplied.
        /// </summary>
        /// <param name="leg">The journey leg details passed back from the CJP</param>
        /// <param name="publicVia">Journey via location</param>
        /// <returns></returns>
        public static PublicJourneyDetail Create(Leg leg, TDLocation publicVia) 
        {
            // Do the setup stuff
            SetupTrapezeRegions();

			return Create(leg, publicVia, null, null);
        }
        
		/// <summary>
		/// Factory method that creates an appropriate subclass of PublicJourneyDetail
		/// based on the type of leg supplied.
		/// </summary>
		/// <param name="leg">The journey leg details passed back from the CJP</param>
		/// <param name="publicVia">Journey via location</param>
		/// <param name="previousLeg">The previous leg in the list. This is necessary for flights only.</param>
		/// <param name="subsequentLeg">The subsequent leg in the list. This is necessary for flights only.</param>
		/// <returns></returns>
		public static PublicJourneyDetail Create(Leg leg, TDLocation publicVia, Leg previousLeg, Leg subsequentLeg) 
		{
            // Do the setup stuff
            SetupTrapezeRegions();

			if( leg is FrequencyLeg)
			{
				return new PublicJourneyFrequencyDetail(leg,publicVia);
			} 
            // InterchangeLeg inherits from ContinuousLeg, so check before
            else if (leg is InterchangeLeg)
            {
                return new PublicJourneyInterchangeDetail(leg, publicVia);
            }
            else if (leg is ContinuousLeg)
            {
                return new PublicJourneyContinuousDetail(leg, publicVia);
            }
            else if (leg is TimedLeg)
            {
                return new PublicJourneyTimedDetail(leg, publicVia, previousLeg, subsequentLeg);
            }
            else
            {
                return null;
            }
		}

        private static void SetupTrapezeRegions()
        {
            if (trapezeRegions == null)
            {
                //Load Trapeze region list from the properties DB
                try
                {
                    // initialise in case this errors
                    trapezeRegions = new ArrayList();

                    string strTrapezeRegions = Properties.Current["JourneyControl.Notes.TrapezeRegions"];
                    if (!string.IsNullOrEmpty(strTrapezeRegions))
                    {
                        trapezeRegions.AddRange(strTrapezeRegions.Split(','));
                    }
                }
                catch
                {
                    // Log the error when in verbose mode
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Verbose, "Property [JourneyControl.Notes.TrapezeRegions] failed to parse into trapezeRegions array"));
                }
            }
        }

        #endregion

        #region Clone

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
