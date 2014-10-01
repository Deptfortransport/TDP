 // *********************************************** 
// NAME			: IBayTextFilter.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 14/07/2005
// DESCRIPTION	: Definition of the IBayTextFilter interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/IBayTextFilter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:40   mturner
//Initial revision.
//
//   Rev 1.1   Jul 25 2005 14:18:16   RWilby
//Module association added
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 25 2005 13:17:16   RWilby
//Initial revision.
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for IBayTextFilter.
	/// </summary>
	public interface IBayTextFilter
	{
		/// <summary>
		/// Tests if the Bay Text is displayable for a given Traveline
		/// </summary>
		/// <param name="travelineRegion">The Traveline Region</param>
		/// <returns>True if Bay Text is displayable for the Traveline
		/// or false otherwise</returns>
		bool FilterText(string travelineRegion);
	}
}
