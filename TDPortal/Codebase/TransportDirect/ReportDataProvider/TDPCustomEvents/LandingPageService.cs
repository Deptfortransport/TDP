// **********************************************************************
// NAME                 : LandingPageService.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 01/08/2005
// DESCRIPTION			: Enumeration holding all landing pages that exist
// ***********************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/LandingPageService.cs-arc  $
//
//   Rev 1.5   Jun 14 2010 15:06:16   pghumra
//Added FindACycle entry into enum
//Resolution for 5553: CODEFIX - REQUIRED - 10.12 - Betterbybike.info page landing to CTP is being reported as Find A Coach
//
//   Rev 1.4   Sep 14 2009 10:55:08   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Mar 10 2008 15:23:38   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.1   Nov 29 2007 10:54:56   mturner
//Updated for Del 9.8
//
//   Rev 1.5   Nov 08 2007 14:23:04   mmodi
//Added FindACarPark enum
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.4   Jan 09 2007 16:22:56   dsawe
//added enum for iframes Page landing
//Resolution for 4331: iFrames for LastMinute.com
//
//   Rev 1.3   Mar 22 2006 16:33:30   build
//Automatically merged from branch for stream3152
//
//   Rev 1.2.1.0   Nov 25 2005 09:16:02   halkatib
//IR3152 Changes asscotiated with Landing page phase 3
//Resolution for 3152: DEL 8 stream: Landing Page Phase 3
//
//   Rev 1.2   Nov 03 2005 11:33:44   kjosling
//Added TravelNews to enum to fix build
//
//   Rev 1.1   Sep 14 2005 16:40:26   halkatib
//MIS Landing Page functionality
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.0   Aug 02 2005 09:45:02   tmollart
//Initial revision.


using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Enumeration of Landing Event Services
	/// </summary>
	public enum LandingPageService
	{
		TestLandingPageService = -1,
		DoorToDoor,
		TravelNews, 
		FindATrain, 
		FindACar,
		FindAFlight,
		FindACoach,
		iFrameJourneyPlanning,
		iFrameFindAPlace,
		iFrameJourneyLandingPage,
		iFrameLocationLandingpage,
		FindACarPark,
        CO2LandingPage,
        StopInformation,
        FindACycle
	};
}
