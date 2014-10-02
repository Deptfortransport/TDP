// ****************************************************************** 
// NAME			: TicketTravelModeParams.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Implementation of the TicketTravelModeParams class
// ***************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TicketTravelModeParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:58   mturner
//Initial revision.
//
//   Rev 1.1   Jan 12 2005 13:54:42   jmorrissey
//Added Discounts member variable
//
//   Rev 1.0   Dec 22 2004 12:25:32   jmorrissey
//Initial revision.

using System;


namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TicketTravelMode.
	/// </summary>
	public class TicketTravelModeParams
	{
		private Discounts[] discountDetails;
		/// <summary>
		/// default constructor
		/// </summary>
		public TicketTravelModeParams()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public Discounts[] DiscountDetails
		{
			get
			{
				return discountDetails;
			}
			set
			{
				discountDetails = value;
			}
		}
	}
}
