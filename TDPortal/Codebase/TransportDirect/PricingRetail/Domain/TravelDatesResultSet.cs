// ************************************************************** 
// NAME			: TravelDatesResultSet.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Implementation of the TravelDatesResultSet class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TravelDatesResultSet.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:00   mturner
//Initial revision.
//
//   Rev 1.4   Jan 21 2005 11:04:22   jmorrissey
//Reset the properties after Rob's changes
//
//   Rev 1.3   Jan 18 2005 11:15:22   rgreenwood
//temporary fix of properties in this class to correct build failure
//
//   Rev 1.2   Jan 14 2005 15:30:24   jmorrissey
//Now only one property of ContainsSingleTickets after change to designs
//
//   Rev 1.1   Jan 12 2005 13:56:28   jmorrissey
//Changed methods to properties
//
//   Rev 1.0   Dec 22 2004 15:22:18   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TravelDatesResultSet.
	/// </summary>
	public class TravelDatesResultSet
	{
		//private fields
		private bool containsSinglesTickets;
		private TravelDate[] travelDates;			

		/// <summary>
		/// default constructor
		/// </summary>
		public TravelDatesResultSet()
		{
			
		}		

		/// <summary>
		/// Read/Write property that contains the subset of travel dates for this results set
		/// </summary>
		public TravelDate[] TravelDates
		{
			get
			{
				return travelDates;
			}
			set
			{
				travelDates = value;
			}
		}

		/// <summary>
		/// property returns true if any singles tickets exist for the 
		/// travel dates in this results set. This may return true if TicketType is Return.
		/// </summary>
		/// <returns>bool</returns>
		public bool ContainsSinglesTickets
		{
			get
			{
				return containsSinglesTickets;
			}		
			set
			{
				containsSinglesTickets = value;
			}
		}		
		
	}
}
