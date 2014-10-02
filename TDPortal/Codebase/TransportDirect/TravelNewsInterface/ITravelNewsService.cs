// *********************************************** 
// NAME                 : ITravelNewsService.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 09/12/2004 
// DESCRIPTION  : Interface for TravelNews service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/ITravelNewsService.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:50:38   mturner
//Initial revision.
//
//   Rev 1.2   Jan 31 2005 18:31:08   passuied
//latest changes to return needed info by Mobile UI
//
//   Rev 1.1   Jan 07 2005 13:40:52   passuied
//added new webmethod to return all available regions...
//
//   Rev 1.0   Dec 16 2004 15:17:00   passuied
//Initial revision.


using System;

namespace TransportDirect.UserPortal.TravelNewsInterface
{
	/// <summary>
	/// Interface for TravelNewsService
	/// </summary>
	public interface ITravelNewsService
	{
		
		/// <summary>
		/// Method returning available Travel news details according to filtering requirements
		/// </summary>
		TravelNewsItem[] GetDetails(string token, TransportType transportType, DelayType delayType, string region);

		/// <summary>
		/// Method returning available Travel news headlines.
		/// </summary>
		HeadlineItem[] GetHeadlines(string token, TransportType transportType, DelayType delayType, string region);
		
		/// <summary>
		/// Method returning detail for a Travel News for the given uid.
		/// </summary>
		TravelNewsItem GetDetailsByUid (string token, string uid);

		/// <summary>
		/// Method returning all available regions within the TravelNews service
		/// </summary>
		/// <returns>array of region resourceIDs</returns>
		/// <param name="token">authentication token</param>
		string[] GetAvailableRegions(string token);
		
	}
}
