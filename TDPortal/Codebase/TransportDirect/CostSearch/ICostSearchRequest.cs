// ************************************************************** 
// NAME			: ICostSearchRequest.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the ICostSearchRequest Interface
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/ICostSearchRequest.cs-arc  $
//
//   Rev 1.1   Feb 02 2009 16:20:56   mmodi
//Include Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:19:20   mturner
//Initial revision.
//
//   Rev 1.4   May 06 2005 16:22:40   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.3   Feb 22 2005 16:47:08   jmorrissey
//Added SessionInfo and RequestId properties
//
//   Rev 1.2   Jan 26 2005 10:33:58   jmorrissey
//Added properties to hold the start and end dates of searches
//
//   Rev 1.1   Jan 12 2005 13:52:30   jmorrissey
//Latest versions. Still in development.
//
//   Rev 1.0   Dec 22 2004 11:59:48   jmorrissey
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// The ICostSearchRequest interface is implemented by the CostSearchRequest class
	/// </summary>
	public interface ICostSearchRequest
	{
		TDLocation OriginLocation { get; set; }
		TDLocation DestinationLocation { get; set; }
		TDLocation ReturnOriginLocation { get; set; }
		TDLocation ReturnDestinationLocation { get; set; }		
		TDDateTime OutwardDateTime { get; set; }
		TDDateTime ReturnDateTime { get; set; }
		TDDateTime SearchOutwardStartDate { get; set; }
		TDDateTime SearchOutwardEndDate { get; set; }
		TDDateTime SearchReturnStartDate { get; set; }
		TDDateTime SearchReturnEndDate { get; set; }
		int OutwardFlexibilityDays { get; set; }
		int InwardFlexibilityDays { get; set; }
		TicketTravelMode [] TravelModes { get; set; }
		string RailDiscountedCard { get; set; }
		string CoachDiscountedCard { get; set; }
        bool RoutingGuideInfluenced { get; set; }
        bool RoutingGuideCompliantJourneysOnly { get; set; }
		CJPSessionInfo SessionInfo { get; set; }			
		Guid RequestId { get; set; }	
	
	}
}
