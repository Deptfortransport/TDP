//********************************************************************************
//NAME         : CoachFaresInterfaceType.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : CoachFaresInterfaceType enum whcich is used for initialising CoachFaresInterfaceFactory.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/CoachFaresInterfaceType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:28   mturner
//Initial revision.
//
//   Rev 1.2   Oct 13 2005 14:52:56   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:16   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:32   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Enumerator to be used for initialising CoachFaresInterfaceFactory.
	/// </summary>
	public enum CoachFaresInterfaceType
	{
		ForJourney,
		ForRoute
	}
}
