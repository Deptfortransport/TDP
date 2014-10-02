// ************************************************************** 
// NAME			: Leg.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents a route Leg
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/Leg.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:42   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:55:46   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 18:21:56   RWilby
//Updated to comply with FxCop
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:45:22   RWilby
//Added comments to properties
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:03:30   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents a specific route Leg, eg London to Oxford
	/// </summary>
	public class Leg
	{
		private string startNaPTAN;
		private string endNaPTAN;
		private string coachOperatorCode;
		private QuotaFareList quotaFareList = new QuotaFareList();

		/// <summary>
		/// Constructor
		/// </summary>
		public Leg()
		{
		}
		
		/// <summary>
		/// Read/Write Property. Start NaPTAN location
		/// </summary>
		public string StartNaPTAN
		{
			get{return startNaPTAN;}
			set{startNaPTAN = value;}
		}
		
		/// <summary>
		/// Read/Write Property. End NaPTAN location
		/// </summary>
		public string EndNaPTAN
		{
			get{return endNaPTAN;}
			set{endNaPTAN = value;}
		}

		/// <summary>
		/// Read/Write Property. Operator code of operator providing the service
		/// </summary>
		public string CoachOperatorCode
		{
			get{return coachOperatorCode;}
			set{coachOperatorCode = value;}
		}
		
		/// <summary>
		/// Read/Write Property. Collection of QuotaFare objects for specific route leg
		/// </summary>
		public QuotaFareList QuotaFareList
		{
			get{return quotaFareList;}
			set{quotaFareList = value;}
		}

	}

}
