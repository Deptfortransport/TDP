// ************************************************************** 
// NAME			: QuotaFare.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents a QuotaFare
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/QuotaFare.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:42   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:55:48   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 14:48:56   RWilby
//Added CultureInfo.CurrentCulture IFormatter to ToString method
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:45:22   RWilby
//Added comments to properties
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:04:16   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
using System;
using System.Globalization;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents a specific QuotaFare for a Route Leg
	/// </summary>
	public class QuotaFare
	{
		private string originNaPTAN;
		private string destinationNaPTAN;
		private string ticketType;
		private int fare;

		/// <summary>
		/// Constructor
		/// </summary>
		public QuotaFare()
		{
		}
		
		/// <summary>
		/// Read/Write Property. Origin NaPTAN
		/// </summary>
		public string OriginNaPTAN
		{
			get{return originNaPTAN;}
			set{originNaPTAN = value;}
		}
	
		/// <summary>
		/// Read/Write Property. Destination NaPTAN
		/// </summary>
		public string DestinationNaPTAN
		{
			get{return destinationNaPTAN;}
			set{destinationNaPTAN = value;}
		}
		
		/// <summary>
		/// Read/Write Property. Ticket Type e.g. "Fun Fare"
		/// </summary>
		public string TicketType
		{
			get{return ticketType;}
			set{ticketType = value;}
		}
		
		/// <summary>
		/// Read/Write Property. Monetary fare value stored in pence
		/// </summary>
		public int Fare
		{
			get{return fare;}
			set{fare = value;}
		}
		

		/// <summary>
		/// Overrides System.Object implementation
		/// </summary>
		/// <returns>Monetary fare value stored in pence</returns>
		public override string ToString()
		{
			return fare.ToString(CultureInfo.CurrentCulture);
		}


	}

}
