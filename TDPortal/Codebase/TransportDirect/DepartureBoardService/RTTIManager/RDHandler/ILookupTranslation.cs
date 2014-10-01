// *********************************************** 
// NAME                 : ILookupTranslation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: Interface for LookupTranslation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/ILookupTranslation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:34   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:04   passuied
//Initial revision.
//
//   Rev 1.4   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added
using System;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade ;   
namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for ILookupTranslation.
	/// </summary>
	public interface ILookupTranslation
	{	
		/// <summary>
		/// Gets the operator name from the specified opeartor code.
		/// </summary>
		/// <param name="operatorCode">Operator code</param>		
		/// <returns>operator name from the specified opeartor code </returns>
		string GetOperatorName(string operatorCode);

		/// <summary>
		/// Gets the reason description from the specified reason code.
		/// </summary>
		/// <param name="operatorCode">Reason code</param>		
		/// <returns>reason description from the specified reason code</returns>
		string GetReasonDescription(string reasonCode);
	}
}
