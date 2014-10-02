//********************************************************************************
//NAME         : IFaresInterface.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : IFaresInterface interface.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/IFaresInterface.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:32   mturner
//Initial revision.
//
//   Rev 1.2   Oct 13 2005 14:55:16   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:04:14   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:01:08   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Summary description for IFaresInterface.
	/// </summary>
	public interface IFaresInterface
	{
		/// <summary>
		/// The method for getting the coach fares from the providers.
		/// </summary>
		/// <param name="fareRequest">The request information</param>
		/// <returns>FareResult</returns>
		FareResult GetCoachFares(FareRequest fareRequest);	
	}
}
