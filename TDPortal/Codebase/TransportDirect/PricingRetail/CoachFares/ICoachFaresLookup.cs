//********************************************************************************
//NAME         : ICoachFaresLookup.cs
//AUTHOR       : Mitesh Modi
//DATE CREATED : 08/05/2007
//DESCRIPTION  : ICoachFaresLookup interface
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/ICoachFaresLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:34   mturner
//Initial revision.
//
//   Rev 1.0   May 09 2007 14:33:40   mmodi
//Initial revision.
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Enum for the action to be taken against the coach fare.
	/// </summary>
	public enum CoachFaresAction
	{
		NotFound,
		Exclude,
		Include
	}

	/// <summary>
	/// Summary description for ICoachFaresLookup.
	/// </summary>
	public interface ICoachFaresLookup
	{
		/// <summary>
		/// Method for getting the coach fares using the given fare type (fare type code).
		/// </summary>
		/// <param name="fareType">Fare type</param>
		/// <returns>CoachFaresAction</returns>
		CoachFaresAction GetCoachFaresAction(string fareType);

		/// <summary>
		/// Method for getting the coach fares restriction priority using the given fare type (fare type code).
		/// </summary>
		/// <param name="fareType">Fare type code</param>
		/// <returns>Restriction priority as int</returns>
		int GetCoachFaresRestriction(string fareTypeCode);
	}
}
