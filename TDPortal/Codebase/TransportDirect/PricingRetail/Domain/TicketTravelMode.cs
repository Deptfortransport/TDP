// ************************************************************** 
// NAME			: TicketTravelMode.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the TicketTravelMode enumerator
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TicketTravelMode.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:58   mturner
//Initial revision.
//
//   Rev 1.1   Feb 16 2005 11:12:26   jmorrissey
//Made serializable
//
//   Rev 1.0   Dec 22 2004 12:25:34   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TicketTravelMode.
	/// </summary>
	[Serializable]
	public enum TicketTravelMode
	{
		Rail,
		Coach,
		Air

	}
}
