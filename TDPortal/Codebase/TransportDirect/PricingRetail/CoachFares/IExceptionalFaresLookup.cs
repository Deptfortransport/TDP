//********************************************************************************
//NAME         : IExceptionalFaresLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 28/11/2005
//DESCRIPTION  : IExceptionalFaresLookup interface.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/IExceptionalFaresLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:36   mturner
//Initial revision.
//
//   Rev 1.0   Nov 28 2005 16:17:12   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Enum for the action to be taken against the exceptional fare.
	/// </summary>
	public enum ExceptionalFaresAction
	{
		NotFound,
		Exclude,
		DayReturn
	}


	/// <summary>
	/// Summary description for IExceptionalFaresLookup.
	/// </summary>
	public interface IExceptionalFaresLookup
	{
		/// <summary>
		/// Method for getting the exceptional fares using the given fare type (fare name or code).
		/// </summary>
		/// <param name="fareType">Fare type</param>
		/// <returns>ExceptionalFaresAction</returns>
		ExceptionalFaresAction GetExceptionalFaresAction(string fareType);
	}
}
