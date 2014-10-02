// ***********************************************
// NAME			: IJourneyFareFilterFactory.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 25/10/2005
// DESCRIPTION	: IJourneyFareFilterFactory definition.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IJourneyFareFilterFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:46   mturner
//Initial revision.
//
//   Rev 1.1   Oct 26 2005 16:43:40   mguney
//Associated IR.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 16:42:30   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for IJourneyFareFilterFactory.
	/// </summary>
	public interface IJourneyFareFilterFactory
	{
		/// <summary>
		/// Gets the filter.
		/// </summary>
		/// <returns></returns>
		IJourneyFareFilter GetFilter();
	}
}
