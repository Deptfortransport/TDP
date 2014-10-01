// *********************************************** 
// NAME                 : TDEventCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Enumeration that defines the
// event categories.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/TDEventCategory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:10   mturner
//Initial revision.
//
//   Rev 1.6   Mar 14 2006 08:41:34   build
//Automatically merged from branch for stream3353
//
//   Rev 1.5.1.0   Feb 22 2006 14:53:46   RGriffith
//Addition of Event Logging item for PricingCall items
//
//   Rev 1.5   Nov 14 2005 12:08:44   jgeorge
//Added FaresProvider category
//Resolution for 3028: DN40: New Event Category for FaresProvider Component (Atkins)
//
//   Rev 1.4   Oct 30 2003 14:18:02   geaton
//Set first element value to 1.
//
//   Rev 1.3   Aug 22 2003 09:00:40   geaton
//Added category for use by infrastructure components
//
//   Rev 1.2   Jul 24 2003 18:27:52   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Enumeration containing the categories that can be assicated with
	/// an <c>OperationalEvent</c>.
	/// The first element is given a value of 1 so that a non-default
	/// value appears when publishing to Windows Event Logs.
	/// </summary>
	public enum TDEventCategory : int
	{
		Business=1,
		CJP,
		Database,
		ThirdParty,
		Infrastructure,
		FaresProvider,
		PricingCall
	}
}
