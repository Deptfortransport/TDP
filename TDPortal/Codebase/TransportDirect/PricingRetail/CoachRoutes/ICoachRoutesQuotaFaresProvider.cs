// ************************************************************** 
// NAME			: ICoachRoutesQuotaFaresProvider.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 03/10/2005
// DESCRIPTION	: Definition of the ICoachRoutesQuotaFaresProvider Interface
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachRoutes/ICoachRoutesQuotaFaresProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:40   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:55:44   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 19 2005 18:21:38   RWilby
//Updated to comply with FxCop
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 14:46:28   RWilby
//Corrected spelling mistake in "FindRoutesAndQuotaFares"
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 10 2005 16:58:06   RWilby
//Added method signature
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// This ICoachRoutesQuotaFaresProvider interface is implemented by the CoachRoutesQuotaFaresProvider
	/// </summary>
	public interface ICoachRoutesQuotaFaresProvider
	{
		/// <summary>
		/// Finds through ticket fare requests for operators between city to city.
		/// This enables the portal to determine when multiple operators can satisfy all or part of a journey between city to city locations
		/// </summary>
		/// <param name="OriginNaPTAN"></param>
		/// <param name="DestinationNaPTAN"></param>
		/// <returns>RouteList</returns>
		 RouteList FindRoutesAndQuotaFares(string originNaPTAN, string destinationNaPTAN);
	}
}
