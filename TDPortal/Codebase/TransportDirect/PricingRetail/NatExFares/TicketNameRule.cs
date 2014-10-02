//********************************************************************************
//NAME         : TicketNameRule.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 11/06/04
//DESCRIPTION  : TicketNameRule interface and implementation classes for NatEx and SCL
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/TicketNameRule.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:06   mturner
//Initial revision.
//
//   Rev 1.2   Mar 30 2005 17:07:26   jgeorge
//Stopped adding (Adult) or (Child) to end of nat ex ticket names as they are now merged together
//
//   Rev 1.1   Jun 15 2004 10:13:10   acaunt
//Serializable attribute added and comments

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;
using CJP =  TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Interface to be fullfilled by class that supply the names for coach tickets. 
	/// </summary>

	public interface TicketNameRule
	{
		/// <summary>
		/// Provide a ticket name based on the associated fare.
		/// </summary>
		/// <param name="fare"></param>
		/// <returns></returns>
		string GetName(CJP.Fare fare);
	}

	/// <summary>
	/// Implementation of TicketNameRule to generate ticket names for NatEx fares
	/// </summary>
	[Serializable]
	public class NatExTicketNameRule : TicketNameRule
	{	
		public string GetName(CJP.Fare fare)
		{
			return fare.fareType;
		}
	}

	/// <summary>
	/// Implementation of TicketNameRule to generate ticket names for SCL fares
	/// </summary>
	[Serializable]
	public class SCLTicketNameRule : TicketNameRule
	{
		public string GetName(CJP.Fare fare)
		{
			return fare.fareType;
		}
	}
}
