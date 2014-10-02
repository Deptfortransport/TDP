//********************************************************************************
//NAME         : ICoachOperatorLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 12/10/2005
//DESCRIPTION  : ICoachOperatorLookup interface.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/ICoachOperatorLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:34   mturner
//Initial revision.
//
//   Rev 1.0   Oct 13 2005 09:35:24   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for ICoachOperatorLookup.
	/// </summary>
	public interface ICoachOperatorLookup
	{
		/// <summary>
		/// Method for getting the coach operator details using the given operator code.
		/// </summary>
		/// <param name="operatorCode">Operator code</param>
		/// <returns>CoachOperator object.</returns>
		CoachOperator GetOperatorDetails(string operatorCode);
	}
}
