// ************************************************************** 
// NAME			: NaPTAN.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Represents a location NaPTAN object in the Coach Routes context
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/NaPTAN.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:42   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:55:46   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:45:22   RWilby
//Added comments to properties
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 17:03:46   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Represents a specific location NaPTAN
	/// </summary>
	public class NaPTAN
	{
		private string naptanID;
		private bool isServed;
		private ExchangeNaPTANList exchangeNaPTANList = new ExchangeNaPTANList();
		private InvalidExchangeOperatorList invalidExchangeOperatorList = new InvalidExchangeOperatorList();

		/// <summary>
		/// Constructor
		/// </summary>
		public NaPTAN()
		{
		}

		/// <summary>
		/// Read/Write Property. NaPTAN location
		/// </summary>
		public string NaPTANID
		{
			get{return naptanID;}
			set{naptanID = value;}
		}

		/// <summary>
		///Read/Write Property. Boolean value to indicate if the NaPTAN is served by the parent Operator 
		/// </summary>
		public bool IsServed
		{
			get{return isServed;}
			set{isServed = value;}
		}

		/// <summary>
		/// Read/Write Property. Collection of exchange NaPTAN's for specific NaPTAN
		/// </summary>
		public ExchangeNaPTANList ExchangeNaPTANList
		{
			get{return exchangeNaPTANList;}
			set{exchangeNaPTANList = value;}
		}

		/// <summary>
		/// Read/Write Property. Collection of invalid operators for specific NaPTAN
		/// </summary>
		public InvalidExchangeOperatorList InvalidExchangeOperatorList
		{
			get{return invalidExchangeOperatorList;}
			set{invalidExchangeOperatorList = value;}
		}
		
		/// <summary>
		/// Overridden ToString() for NaPTAN
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return naptanID;
		}

	}

}
