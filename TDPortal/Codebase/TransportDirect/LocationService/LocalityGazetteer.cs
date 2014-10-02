// *********************************************** 
// NAME                 : LocalityGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Gazetteer for Localities. Inherits from TDGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/LocalityGazetteer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:10   mturner
//Initial revision.
//
//   Rev 1.28   Apr 30 2007 10:13:22   mmodi
//Reset the NaPTANs properrty for the location object
//Resolution for 2555: Del 9.6: Reversing journey direction in Find a Car
//
//   Rev 1.27   Apr 20 2006 15:51:40   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.26   Aug 19 2005 14:04:40   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.25.1.1   Aug 10 2005 16:36:44   RPhilpott
//Can now use RequestPlaceType.Locality -- CJP now working.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.25.1.0   Aug 09 2005 18:53:40   RPhilpott
//Support for RequestPlaceType of Locality, removing now redundant code for switching dynamically to type of Naptan.
//
//New type commented out until CJP works properly with it ...
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//Resolution for 2644: Unable to find journeys with RequestPlaceType of Locality
//
//   Rev 1.25   Feb 14 2005 15:39:28   PNorell
//Updated with the latest requirements for IR1905
//Resolution for 1905: Cumbria causes error when issued as a locality
//
//   Rev 1.24   Jan 20 2005 12:12:48   PNorell
//Fix for IR: 1905
//Resolution for 1905: Open  Cumbria causes error when issued as a locality
//
//   Rev 1.23   Jan 18 2005 17:43:36   passuied
//moved Xml handling into a separate class and added handling for code gaz
//
//   Rev 1.22   Sep 11 2004 13:13:56   RPhilpott
//Restore ability to switch back to NaPTAN-based searching via property setting, if necessary. 
//
//   Rev 1.21   Sep 10 2004 15:35:48   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.20   Jul 15 2004 18:02:04   acaunt
//Logging method moved to TDGazetteer which now passes submitted time to Gazetteer event.
//
//   Rev 1.19   Jul 09 2004 13:09:12   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.18   Jul 07 2004 15:01:38   RPhilpott
//Use RequestPlaceType.Coordinate instaed of NaPTAN, but make it configurable via property in case of reversion ...
//Resolution for 1134: Search type used for Locality-based requests
//
//   Rev 1.17   May 24 2004 17:26:00   RPhilpott
//Replace FindStopsInRadius() with FindNeraestStopsAndITNs().
//Resolution for 928: Change "Find Nearest NaPTAN" function to speed up process for a particular GIS query
//
//   Rev 1.16   May 14 2004 15:29:14   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.15   Dec 11 2003 17:52:46   RPhilpott
//Make use of new FindStopsInRadius() method in GISQuery.
//Resolution for 281: Postcode journey doesn't return public transport journeys
//
//   Rev 1.14   Dec 03 2003 12:21:24   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.13   Oct 14 2003 12:48:26   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.12   Oct 09 2003 18:46:00   RPhilpott
//Add verbose logging on all gazetteer calls
//
//   Rev 1.11   Oct 09 2003 10:31:56   passuied
//implementation of minScore LocationChoiceList tailing
//
//   Rev 1.10   Oct 07 2003 13:33:00   passuied
//fixed the bug of gisquery stored to session but not serialisable (now not stored in session)
//
//   Rev 1.9   Oct 03 2003 13:38:34   PNorell
//Updated the new exception identifier.
//
//   Rev 1.8   Oct 02 2003 14:41:04   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.7   Sep 25 2003 12:38:20   passuied
//Added property and allowed locality to get location details when the location has children
//
//   Rev 1.6   Sep 23 2003 13:19:52   passuied
//corrected errors inserted from previous changes
//
//   Rev 1.5   Sep 22 2003 17:52:28   passuied
//corrected Serializable problem (thanks Peter!)
//
//   Rev 1.4   Sep 22 2003 17:31:20   passuied
//made all objects serializable
//
//   Rev 1.3   Sep 22 2003 13:39:32   RPhilpott
//Uncomment FindStopsInGroupForStops(schema) calls
//
//   Rev 1.2   Sep 20 2003 16:59:48   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:48   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:28   passuied
//Initial Revision

using System;
using System.Text;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Gazetteer for Localities. Inherits from TDGazetteer
	/// </summary>
	[Serializable()]
	public class LocalityGazetteer : TDGazetteer
	{

		public LocalityGazetteer(string sessionID, bool userLoggedOn):base(sessionID, userLoggedOn)
		{
			gazetteerId = Properties.Current["locationservice.localitygazetteerid"];
			
			if (gazetteerId == null || gazetteerId == string.Empty)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"locationservice.localitygazetteerid not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.localitygazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSLocalityGazetteerIDInvalid);
			}

			minScore = Convert.ToInt32(Properties.Current["locationservice.locality.minscore"], CultureInfo.CurrentCulture);

			OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
				"Using LocalityGazetteer - minimum score = " + minScore);

			Logger.Write(log);

			xmlHandler = new XmlGazetteerHandler(userLoggedOn, sessionID, minScore);

		}

		#region ITDGazetteer implementation
		/// <summary>
		/// Method searching a location from a string.
		/// </summary>
		/// <param name="text">input text string</param>
		/// <param name="fuzzy">indicates if search should be fuzzy</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public override LocationQueryResult FindLocation(string text, bool fuzzy)
		{
			DateTime submitted = DateTime.Now;
			String result = GazopsWeb.DrillDownAddressQuery(
				text,
				string.Empty,
				string.Empty,
				string.Empty,
				fuzzy,
				gazetteerId,
				string.Empty,
				string.Empty
				);
			Logging(GazetteerEventCategory.GazetteerLocality, submitted);
			
			LocationQueryResult lqr = xmlHandler.ReadDrillDownResult(result,string.Empty);
			return lqr;
		}

		/// <summary>
		/// Calls the gazetteer drillDown method
		/// </summary>
		/// <param name="text">Input text string</param>
		/// <param name="fuzzy">Indicates if search is fuzzy</param>
		/// <param name="sPickList">gives the list entry to drill down with</param>
		/// <param name="queryRef">Gives the query reference</param>
		/// <param name="choice">choice</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public override LocationQueryResult DrillDown (
			string text, 
			bool fuzzy, 
			string sPickList, 
			string queryRef, 
			LocationChoice choice)
		{
			if (!choice.HasChilden)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"Unable to drill into a choice without children");

				Logger.Write(oe);
				throw new TDException("Unable to drill into a choice without children", true, TDExceptionIdentifier.LSLocalityLacksChildren);

			}
			PickList pickList;
			if (sPickList == string.Empty)
				pickList = new PickList();
			else
				pickList = new PickList(sPickList);

			pickList.Add(choice.PicklistCriteria, choice.PicklistValue);
			DateTime submitted = DateTime.Now;
			String result = GazopsWeb.DrillDownAddressQuery(
				text,
				pickList.Current,
				string.Empty,
				queryRef,
				fuzzy,
				gazetteerId,
				string.Empty,
				string.Empty);

			Logging(GazetteerEventCategory.GazetteerLocality, submitted);
			return xmlHandler.ReadDrillDownResult(result,	pickList.Current);
		}

		/// <summary>
		/// Retrieves the Location details from a given choice
		/// </summary>
		/// <param name="text">Input text string</param>
		/// <param name="fuzzy">Indicates if search is fuzzy</param>
		/// <param name="pickList">gives the list entry to drill down with</param>
		/// <param name="queryRef">Gives the query reference</param>
		/// <param name="choice">choice</param>
		/// <param name="maxDistance">Max walking distance</param>
		public override void GetLocationDetails ( 
			ref TDLocation location,
			string text,
			bool fuzzy, 
			string pickList,
			string queryRef,
			LocationChoice choice, 
			int maxDistance,
			bool disableGisQuery)
		{
			// Because it supports hierarchical searches, it can drill down whether it has children or not

			// If Easting or Northing have not been populated
			if (choice.OSGridReference.Easting ==-1 
				|| choice.OSGridReference.Northing == -1)
			{
				DateTime submitted = DateTime.Now;
				String gazopsResult = GazopsWeb.DrillDownAddressQuery(
					text,
					pickList,
					choice.PicklistValue,
					queryRef,
					fuzzy,
					gazetteerId,
					string.Empty,
					string.Empty);
				
				Logging(GazetteerEventCategory.GazetteerLocality, submitted);

				LocationQueryResult result = xmlHandler.ReadFinalDrillDownResult(gazopsResult,	pickList);

				choice = (LocationChoice)result.LocationChoiceList[0];

			}

			location.Description = choice.Description;
			location.GridReference = choice.OSGridReference;
			location.Locality = choice.Locality;
			location.RequestPlaceType = RequestPlaceType.Locality;
			location.SearchType = SearchType.Locality;
			location.Status = TDLocationStatus.Valid;

			if	(!disableGisQuery)
			{
				PopulateToids(ref location);
			}

			// Because a Locality doesnt return station NaPTANs, need to reset this property to 
			// avoid scenario where previous search NaPTANs are used
			location.NaPTANs = new TDNaptan[0];
		}

		public override void PopulateToids(ref TDLocation location)
		{
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];


			QuerySchema gisResultToids = gisQuery.FindNearestITNs(location.GridReference.Easting, 
																  location.GridReference.Northing); 

			StringBuilder logMsg = new StringBuilder();

			if (TDTraceSwitch.TraceVerbose)
			{
				logMsg.Append("GetLocationDetails : description = " + location.Description + " locality = " + location.Locality + " ");
				logMsg.Append(gisResultToids.ITN.Rows.Count + " TOIDs: ");
			}

			location.Toid = new string[gisResultToids.ITN.Rows.Count];

			for ( int i=0; i< gisResultToids.ITN.Rows.Count; i++)
			{
				QuerySchema.ITNRow row = (QuerySchema.ITNRow) gisResultToids.ITN.Rows[i];
				location.Toid[i] = row.toid;
				
				if (TDTraceSwitch.TraceVerbose)
				{
					logMsg.Append(row.toid + " ");
				}
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}

		}

		public override void PopulateLocality(ref TDLocation location)
		{
			// nothing to do ...
		}

		/// <summary>
		/// Returns if the gazetteer supports hierarchic search
		/// </summary>
		public override bool SupportHierarchicSearch 
		{
			get
			{
				return true;
			}
		}
		#endregion
	}
}
