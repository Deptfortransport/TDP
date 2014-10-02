// *********************************************** 
// NAME                 : ITDGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Common interface for all gazetteers
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ITDGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:08   mturner
//Initial revision.
//
//   Rev 1.7   Apr 20 2006 15:51:28   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.6   Sep 10 2004 15:35:46   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.5   Jul 09 2004 13:09:10   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.4   May 14 2004 15:29:12   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.3   Dec 03 2003 12:21:24   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.2   Oct 14 2003 12:48:18   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.1   Sep 09 2003 17:23:46   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:26   passuied
//Initial Revision

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Common interface for all gazetteers
	/// </summary>
	public interface ITDGazetteer
	{
		LocationQueryResult FindLocation(string text, bool fuzzy);
		LocationQueryResult DrillDown (
			string text, 
			bool fuzzy, 
			string pickList, 
			string queryRef, 
			LocationChoice choice);

		void GetLocationDetails (
			ref TDLocation location,
			string text,
			bool fuzzy, 
			string pickList,
			string queryRef,
			LocationChoice choice,
			int maxDistance,
			bool disableGisQuery);

		void PopulateLocality(ref TDLocation location);

		void PopulateToids(ref TDLocation location);

		bool SupportHierarchicSearch { get;}
	}
}
