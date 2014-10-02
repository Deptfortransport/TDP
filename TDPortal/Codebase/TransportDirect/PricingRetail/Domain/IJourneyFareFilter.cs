// ***********************************************
// NAME			: IJourneyFareFilter.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 25/10/2005
// DESCRIPTION	: IJourneyFareFilter definition.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IJourneyFareFilter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:46   mturner
//Initial revision.
//
//   Rev 1.1   Oct 26 2005 15:55:28   mguney
//Associated IR
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 15:54:26   mguney
//Initial revision.

using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// IJourneyFareFilter interface.
	/// </summary>
	public interface IJourneyFareFilter
	{
		/// <summary>
		/// Filters the journeys given in the TDJourneyResult using the information in the provided tickets.
		/// </summary>
		/// <param name="originalJourney"></param>
		/// <param name="tickets"></param>
		/// <returns>The changed journey result.</returns>
		TDJourneyResult FilterJourneys(TDJourneyResult originalJourney,CostSearchTicket[] tickets);
	}
}
