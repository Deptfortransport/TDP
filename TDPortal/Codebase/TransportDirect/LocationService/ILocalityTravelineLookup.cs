// *********************************************** 
// NAME			: ILocalityTravelineLookup.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 14/07/2005
// DESCRIPTION	: Definition of the ILocalityTravelineLookup interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ILocalityTravelineLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:06   mturner
//Initial revision.
//
//   Rev 1.1   Aug 09 2005 16:13:54   RWilby
//Added //$Log: comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for ILocalityTravelineLookup.
	/// </summary>
	public interface ILocalityTravelineLookup
	{
		/// <summary>
		/// Returns appropriate Traveline code for a given locality
		/// </summary>
		/// <param name="locality">The Locality</param>
		/// <returns>Returns the Traveline code or an empty 
		/// string if the Traveline code cannot be found for locality</returns>
		string GetTraveline (string locality);
	}
}
