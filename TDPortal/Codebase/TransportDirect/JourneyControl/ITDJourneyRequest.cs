// *********************************************** 
// NAME			: ITDJourneyRequest.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Definition of the ITDJourneyRequest Interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ITDJourneyRequest.cs-arc  $
//
//   Rev 1.8   Dec 05 2012 14:14:02   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Nov 13 2012 13:13:28   mmodi
//Added TDAccessiblePreferences
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Sep 01 2011 10:43:16   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Mar 14 2011 15:11:52   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.4   Dec 21 2010 14:05:02   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.3   Nov 24 2010 10:38:36   apatel
//Resolve the issue with SBP planner when planning for today get services from 22:00 yesterday.  For all other dates the period is 00:00 onwards. Make the SBP planner to return journeys planned 00:00 onwards for all days requested
//Resolution for 5644: SBP planner when planning for today you get services from 22:00 yesterday
//
//   Rev 1.2   Oct 12 2009 09:10:56   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:42:52   apatel
//EBC Map and Printer Friendly pages related chages
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:39:42   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Feb 02 2009 16:30:30   mmodi
//Added properties for Routing Guide
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:23:42   mturner
//Initial revision.
//
//   Rev 1.16   Nov 01 2005 15:12:58   build
//Automatically merged from branch for stream2638
//
//   Rev 1.15.1.1   Oct 10 2005 18:00:54   tmollart
//Added Clone method to enable class cloning for VisitPlanner.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.15.1.0   Sep 13 2005 11:52:26   tmollart
//Added property for Sequence.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.15   Mar 15 2005 15:52:58   RAlavi
//Change fuel consumption and fuel cost types to string
//
//   Rev 1.14   Mar 14 2005 11:30:16   esevern
//Added FuelType and CarSize properties
//
//   Rev 1.13   Mar 09 2005 09:38:28   RAlavi
//Added code for car costing
//
//   Rev 1.12   Feb 23 2005 16:40:34   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.11   Feb 23 2005 15:07:48   rscott
//DEL 7 Update - New Properties Added
//
//   Rev 1.10   Jan 27 2005 17:20:04   ralavi
//Added car costing properties
//
//   Rev 1.9   Jan 19 2005 14:45:22   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.8   Sep 13 2004 16:15:38   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.7   Jul 28 2004 11:00:02   RPhilpott
//Remove now-redundant FlightPlan flag (replaced by IsTrunkRequest for 6.1).
//
//   Rev 1.6   Jul 23 2004 18:12:38   RPhilpott
//Add isTrunkRequest flag
//
//   Rev 1.5   Jun 16 2004 10:13:04   RPhilpott
//More Find-A-Flight additions
//
//   Rev 1.4   Jun 15 2004 13:25:08   RPhilpott
//Find-A-Flight additions
//
//   Rev 1.3   Sep 05 2003 15:28:54   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.2   Aug 20 2003 17:55:44   AToner
//Work in progress
using System;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for ITDJourneyRequest.
	/// </summary>
	public interface ITDJourneyRequest
	{
		
		ModeType[] Modes { get; set;}
        bool IsOutwardRequired { get; set; }
		bool IsReturnRequired { get; set; }
		bool OutwardArriveBefore { get; set; }
		bool ReturnArriveBefore { get; set; }

		TDDateTime[] OutwardDateTime { get; set; }
		TDDateTime[] ReturnDateTime { get; set; }

		int InterchangeSpeed { get; set; }
		int WalkingSpeed { get; set; }
		int MaxWalkingTime { get; set; }
        int MaxWalkingDistance { get; set; }
		int DrivingSpeed { get; set; }
		bool AvoidMotorways { get; set; }
        bool BanUnknownLimitedAccess { get; set; }
		TDLocation OriginLocation { get; set; }
		TDLocation DestinationLocation { get; set; }
		TDLocation ReturnOriginLocation { get; set; }
		TDLocation ReturnDestinationLocation { get; set; }
		
		string[] TrainUidFilter { get; set; }
		bool TrainUidFilterIsInclude { get; set; }
		TDLocation[] PublicViaLocations { get; set;}
		TDLocation[] PublicSoftViaLocations { get; set;}
		TDLocation[] PublicNotViaLocations { get; set;}

		string[] RoutingPointNaptans { get; set; }

        bool RoutingGuideInfluenced { get; set;}
        bool RoutingGuideCompliantJourneysOnly { get; set;}
        string RouteCodes { get; set;}

		TDLocation PrivateViaLocation { get; set; }
		string[] AvoidRoads { get; set; }
        string[] AvoidToidsOutward { get; set; } // Array of Toids to be avoided when planning the outward road journey
        string[] AvoidToidsReturn { get; set; } // Array of Toids to be avoided when planning the return road journey
		TDLocation[] AlternateLocations { get; set; }
		bool AlternateLocationsFrom { get; set; }
		PrivateAlgorithmType PrivateAlgorithm { get; set; }
		PublicAlgorithmType PublicAlgorithm { get; set; }

		bool IsTrunkRequest { get; set; }
		
		// following only used for Find-A 
		//  (isTrunkRequest == true) ...

		TDDateTime ViaLocationOutwardStopoverTime { get; set; }
		TDDateTime ViaLocationReturnStopoverTime { get; set; }
		TDDateTime ExtraCheckinTime { get; set; }
		bool UseOnlySpecifiedOperators { get; set; }
		string[] SelectedOperators { get; set; }
		bool DirectFlightsOnly { get; set; }
		bool OutwardAnyTime { get; set; }
		bool ReturnAnyTime { get; set; }

		//following properties are added for use 
		//in car costing
		bool AvoidTolls { get; set; }
		string FuelPrice { get; set; }
		bool AvoidFerries { get; set; }
		string FuelConsumption { get; set; }
		string[] IncludeRoads { get; set; }
		bool DoNotUseMotorways { get; set; }
		VehicleType VehicleType { get; set; }
		string FuelType { get; set; }
		string CarSize { get; set; }

		//Added for use with Visit Planner
		int Sequence { get; set; }

        //Added for use with Environment benefit calculator
        bool IgnoreCongestion { get; set; }
        int CongestionValue { get; set; }

        /// <summary>
        /// During CJP call process, by default journey trunk request date times are adjusted 
        /// with interval time applied before and after the journey date specified.
        /// To allow perticular request to override this behaviour and to now allow interval time applied
        /// before the journey date time set this property as false.
        /// </summary>
        bool AdjustTimeWithIntervalBefore { get; set; }

        /// <summary>
        /// Read/Write property. Determines the find a planner mode i.e. Rail, Flight, City to City etc.
        /// </summary>
        FindAPlannerMode FindAMode { get; set; }

        // Added for accessible journey planner
        TDAccessiblePreferences AccessiblePreferences { get; set; }
        
        /// <summary>
        /// Flag for the CJP to perform an "olympic" request which will ensure the CJP
        /// - uses the single traveline region for journeys
        /// - not use the TTBO/Coach planner for air/rail/coach only journeys
        /// - not validate the results 
        /// - add TTBO service information where available
        /// </summary>
        bool AccessibleRequest { get; set; }

        /// <summary>
        /// Flag to override the CJP don't force coach rules. 
        /// Default is false to allow CJP to determine how to apply force coach rule. 
        /// True will prevent CJP from applying the force coach rules
        /// </summary>
        bool DontForceCoach { get; set; }

        /// <summary>
        /// Flag to remove awkward overnight journeys. 
        /// This removes journeys that breach overnight journey rules, in particular for journeys that arrive for
        /// early morning having long waits in the middle of the night.
        /// </summary>
        bool RemoveAwkwardOvernight { get; set; }

		//ICloneable Interface Members
		object Clone();
	}
}
