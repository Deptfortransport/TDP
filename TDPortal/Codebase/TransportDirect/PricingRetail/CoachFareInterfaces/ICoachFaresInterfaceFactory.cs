//********************************************************************************
//NAME         : ICoachFaresInterfaceFactory.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Interface for enabling unit tests via TDServiceDiscovery.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/ICoachFaresInterfaceFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:30   mturner
//Initial revision.
//
//   Rev 1.4   Nov 03 2005 13:59:54   RPhilpott
//Add new get method (for interface type).
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 22 2005 15:51:20   mguney
//Factories returned for the specified operator code.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:55:10   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:04:10   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:01:06   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Summary description for ICoachFaresInterfaceFactory.
	/// </summary>
	public interface ICoachFaresInterfaceFactory
	{
		IFaresInterface GetFaresInterface(string operatorCode);
		IFaresInterface GetFaresInterface(CoachFaresInterfaceType type);
	}
}
