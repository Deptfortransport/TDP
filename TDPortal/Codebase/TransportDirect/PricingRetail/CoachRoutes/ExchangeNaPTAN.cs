// ************************************************************** 
// NAME			: ExchangeNaPTAN.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents an ExchangeNaPTAN object in the Coach Routes context
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/ExchangeNaPTAN.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:40   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:55:42   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:44:14   RWilby
//Added comments to properties
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:02:46   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents an ExchangeNaPTAN
	/// </summary>
	public class ExchangeNaPTAN
	{
		private string naptanID;

		/// <summary>
		/// Constructor
		/// </summary>
		public ExchangeNaPTAN()
		{
		}

		/// <summary>
		/// Read/Write Property. Exchange NaPTAN location
		/// </summary>
		public string NaPTANID
		{
			get{return naptanID;}
			set{naptanID = value;}
		}
		
		/// <summary>
		/// Overridden ToString() for ExchangeNaPTAN
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return naptanID;
		}

	}

}
