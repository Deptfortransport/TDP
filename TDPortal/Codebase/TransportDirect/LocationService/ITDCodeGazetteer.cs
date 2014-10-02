// *********************************************** 
// NAME                 : ITDCodeGazetteer.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Interface for Code Gazetteer classes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ITDCodeGazetteer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:08   mturner
//Initial revision.
//
//   Rev 1.0   Jan 18 2005 17:38:24   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:33:50   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Interface for Code Gazetteer classes
	/// </summary>
	public interface ITDCodeGazetteer
	{
		/// <summary>
		/// Method that identifies a given code as a valid code or not. 
		/// </summary>
		/// <param name="code"> code to identify.</param>
		/// <returns>If valid, all associated codes will be returned in a an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty</returns>
		TDCodeDetail[] FindCode( 
			string code);

		/// <summary>
		/// method that takes a given text entry and searches for associated codes.
		/// </summary>
		/// <param name="text">text to find</param>
		/// <param name="fuzzy">indicate if user is unsure of spelling</param>
		/// <param name="modeTypes">gives requested mode types-associated codes to be returned.</param>
		/// <returns>If codes are found, all matching ones will be returned in an array of TDCodeDetail objects.
		/// Otherwise, the array will be empty.</returns>
		TDCodeDetail[] FindText(
			string text,
			bool fuzzy,
			TDModeType[] modeTypes);

	}
}
