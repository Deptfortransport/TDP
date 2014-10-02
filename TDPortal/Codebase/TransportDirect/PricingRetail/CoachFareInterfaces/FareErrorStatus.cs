//********************************************************************************
//NAME         : FareErrorStatus.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FareErrorStatus enumeration.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FareErrorStatus.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:28   mturner
//Initial revision.
//
//   Rev 1.2   Oct 13 2005 14:53:14   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:26   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:48   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Enumeration for FareErrorStatus. To be used by the remote coach fare providers and by the portal.
	/// </summary>
	public enum FareErrorStatus
	{
		Success,
		Error
	}
}
