// ************************************************************** 
// NAME			: TicketType.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of the TicketType enumerator
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TicketType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:58   mturner
//Initial revision.
//
//   Rev 1.3   Nov 09 2005 12:31:34   build
//Automatically merged from branch for stream2818
//
//   Rev 1.2.1.0   Nov 07 2005 18:22:14   RPhilpott
//Add TicketType.None.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Feb 16 2005 11:11:04   jmorrissey
//Made serializable
//
//   Rev 1.1   Jan 12 2005 13:55:02   jmorrissey
//Added Single enumerator
//
//   Rev 1.0   Dec 22 2004 12:25:32   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TicketType.
	/// </summary>	
	[Serializable]
	public enum TicketType
	{
		None,
		Single,
		OpenReturn,	
		Singles,
		Return			
	}
}
