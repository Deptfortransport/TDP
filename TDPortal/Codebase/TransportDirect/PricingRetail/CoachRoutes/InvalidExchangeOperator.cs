// ************************************************************** 
// NAME			: InvalidExchangeOperator.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents an InvalidExchangeOperator object in the Coach Routes context
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/InvalidExchangeOperator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:40   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2006 17:44:54   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.0   Oct 26 2005 09:55:44   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents an InvalidExchangeOperator
	/// </summary>
	public class InvalidExchangeOperator
	{

		private string coachOperatorCode;

		/// <summary>
		/// Constructor
		/// </summary>
		public InvalidExchangeOperator()
		{
		}
		
		/// <summary>
		/// Read/Write Property. Coach Operator code
		/// </summary>
		public string CoachOperatorCode
		{
			get{return coachOperatorCode;}
			set{coachOperatorCode = value;}
		}
		
		/// <summary>
		/// Overridden ToString() for InvalidExchangeOperator
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return coachOperatorCode;
		}

	}

}
