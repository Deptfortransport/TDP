// *********************************************** 
// NAME                 : ImportantPlaceGazetteer
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/10/2004
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ImportantPlaceGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:06   mturner
//Initial revision.
//
//   Rev 1.2   Apr 20 2006 15:51:18   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.1   Mar 23 2005 12:11:58   jgeorge
//Updated commenting
//
//   Rev 1.0   Nov 02 2004 14:05:54   jgeorge
//Initial revision.

using System;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.Common;
using System.Diagnostics;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Implementation of ITDGazetteer which retrieves data from the PlaceDataProvider
	/// </summary>
	[Serializable]
	public class ImportantPlaceGazetteer : ITDGazetteer
	{
		private PlaceType placeType;
		private bool userLoggedOn;
		private string sessionID;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="placeType">Used when querying the PlaceDataProvider</param>
		public ImportantPlaceGazetteer(string sessionID, bool userLoggedOn, PlaceType placeType)
		{
			this.placeType = placeType;
			this.userLoggedOn = userLoggedOn;
			this.sessionID = sessionID;
		}

		#region Implementation of ITDGazetteer

		/// <summary>
		/// Returns a list of places. Both parameters are ignored
		/// </summary>
		/// <param name="text"></param>
		/// <param name="fuzzy"></param>
		/// <returns></returns>
		public LocationQueryResult FindLocation(string text, bool fuzzy)
		{
			IPlaceDataProvider dataProvider = (IPlaceDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PlaceDataProvider];

			LocationQueryResult result = new LocationQueryResult(string.Empty);
			result.LocationChoiceList = dataProvider.GetPlaces(placeType);
			result.QueryReference = placeType.ToString();
			return result;
		}

		/// <summary>
		/// This method will always throw an error, as no LocationChoice objects returned
		/// from previous searches will have children
		/// </summary>
		/// <param name="text"></param>
		/// <param name="fuzzy"></param>
		/// <param name="pickList"></param>
		/// <param name="queryRef"></param>
		/// <param name="choice"></param>
		/// <returns></returns>
		public LocationQueryResult DrillDown (string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice)
		{
			// Throw an error. We can never drilldown using this gazetteer
			OperationalEvent oe = new OperationalEvent(
				TDEventCategory.Business,
				sessionID,
				TDTraceLevel.Error,
				"Unable to drill into a choice without children");

			Trace.Write(oe);
			throw new TDException("Unable to drill into a choice without children", true, TDExceptionIdentifier.LSAddressDrillLacksChildren);
		}

		/// <summary>
		/// Populates the TDLocation object. All parameters but choice are ignored
		/// </summary>
		/// <param name="location"></param>
		/// <param name="text"></param>
		/// <param name="fuzzy"></param>
		/// <param name="pickList"></param>
		/// <param name="queryRef"></param>
		/// <param name="choice"></param>
		/// <param name="maxDistance"></param>
		/// <param name="disableGisQuery"></param>
		/// <param name="localityRequired"></param>
		public void GetLocationDetails (ref TDLocation location, string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice, int maxDistance, bool disableGisQuery)
		{
			IPlaceDataProvider dataProvider = (IPlaceDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PlaceDataProvider];

			location = dataProvider.GetPlace(choice.PicklistValue);
		}

		/// <summary>
		/// Does nothing, as TDLocation objects retrieved using this Gazetteer will always
		/// be fully populated
		/// </summary>
		/// <param name="location"></param>
		public void PopulateLocality(ref TDLocation location)
		{
			return;
		}

		/// <summary>
		/// Does nothing, as TDLocation objects retrieved using this Gazetteer will always
		/// be fully populated
		/// </summary>
		/// <param name="location"></param>
		public void PopulateToids(ref TDLocation location)
		{
			return;
		}

		/// <summary>
		/// Returns false
		/// </summary>
		public bool SupportHierarchicSearch
		{
			get { return false; }
		}

		#endregion

	}
}
