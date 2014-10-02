// *********************************************** 
// NAME                 : AllStationsGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Gazetteer for All stations. Inherits from TDGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/AllStationsGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:24:58   mturner
//Initial revision.
//
//   Rev 1.21   Oct 15 2007 15:31:06   JFrank
//Change ESRI call so that only a single stop naptan is returned instead of all of the stops in the group.
//Resolution for 4516: Turn off Multiple Stop groups
//
//   Rev 1.20   Apr 27 2006 14:52:20   halkatib
//Resolution for IR3996: Changed the FindNearestITNs() method with the FindNearestITN() method so that only one TOID is sent in the journey request. 
//
//   Rev 1.19   Apr 20 2006 15:50:54   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.18   Jan 18 2005 17:43:28   passuied
//moved Xml handling into a separate class and added handling for code gaz
//
//   Rev 1.17   Sep 10 2004 15:35:40   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.16   Jul 15 2004 18:01:40   acaunt
//Logging method moved to TDGazetteer and now passes submitted time to Gazetteer event
//
//   Rev 1.15   Jul 09 2004 13:09:10   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.14   May 24 2004 17:25:58   RPhilpott
//Replace FindStopsInRadius() with FindNeraestStopsAndITNs().
//Resolution for 928: Change "Find Nearest NaPTAN" function to speed up process for a particular GIS query
//
//   Rev 1.13   May 14 2004 15:29:08   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.12   Dec 03 2003 12:21:20   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.11   Oct 14 2003 12:48:20   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.10   Oct 09 2003 18:45:58   RPhilpott
//Add verbose logging on all gazetteer calls
//
//   Rev 1.9   Oct 09 2003 10:31:52   passuied
//implementation of minScore LocationChoiceList tailing
//
//   Rev 1.8   Oct 07 2003 13:32:58   passuied
//fixed the bug of gisquery stored to session but not serialisable (now not stored in session)
//
//   Rev 1.7   Oct 03 2003 13:38:36   PNorell
//Updated the new exception identifier.
//
//   Rev 1.6   Oct 02 2003 14:41:06   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.5   Sep 23 2003 13:19:50   passuied
//corrected errors inserted from previous changes
//
//   Rev 1.4   Sep 22 2003 17:52:26   passuied
//corrected Serializable problem (thanks Peter!)
//
//   Rev 1.3   Sep 22 2003 17:31:16   passuied
//made all objects serializable
//
//   Rev 1.2   Sep 20 2003 16:59:44   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:44   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:20   passuied
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
	/// Gazetteer for All stations. Inherits from TDGazetteer
	/// </summary>
	[Serializable()]
	public class AllStationsGazetteer : TDGazetteer
	{
		public AllStationsGazetteer(string sessionID, bool userLoggedOn): base(sessionID, userLoggedOn)
		{
			gazetteerId = Properties.Current["locationservice.allstationsgazetteerid"];
			if (gazetteerId== null || gazetteerId == string.Empty)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"locationservice.allsstationsgazetteerid not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.allsstationsgazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSAllStationGazetteerIDInvalid);
			}

			minScore = Convert.ToInt32(Properties.Current["locationservice.allstations.minscore"], CultureInfo.CurrentCulture);

			OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
				"Using AllStationsGazetteer - minimum score = " + minScore);
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
			// Get MaxReturnedRecords Property from PropertyService
			int maxReturnedRecords;
			try
			{

				maxReturnedRecords = Convert.ToInt32(
					Properties.Current["locationservice.maxreturnedrecords"],
					CultureInfo.CurrentCulture.NumberFormat);
			}
			catch (FormatException)
			{
			
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"locationservice.maxreturnedrecords not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.maxreturnedrecords not set or set to a wrong format", true, TDExceptionIdentifier.LSAllStationsMaxReturnedInvalid);
			
				
			}
			DateTime submitted = DateTime.Now;
			String result = GazopsWeb.PlaceNameMatch(
							text,
							fuzzy,
							-1,
							-1,
							gazetteerId,
							string.Empty,
							maxReturnedRecords,
							string.Empty);
			Logging(GazetteerEventCategory.GazetteerAllStations, submitted);
			return xmlHandler.ReadPlacenameMatchResult(result);

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
			string picklist, 
			string queryRef, 
			LocationChoice choice)
		{
			OperationalEvent oe = new OperationalEvent(
				TDEventCategory.Business,
				sessionID,
				TDTraceLevel.Error,
				"DrillDown cannot be called for this gazetteer");

			Logger.Write(oe);
			throw new TDException ("DrillDown cannot be called for this gazetteer", true, TDExceptionIdentifier.LSAllStationsDrillLacksChildren);
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
			location.Description = choice.Description;
			location.GridReference = choice.OSGridReference;
			location.Locality = choice.Locality;
			location.RequestPlaceType = RequestPlaceType.NaPTAN;
			location.SearchType = SearchType.AllStationStops;
			location.Status = TDLocationStatus.Valid;

			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
			
			QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[]{choice.Naptan});
		
			location.NaPTANs = new TDNaptan[gisResult.Stops.Rows.Count];

			StringBuilder logMsg = new StringBuilder();

			if (TDTraceSwitch.TraceVerbose)
			{
				logMsg.Append("GetLocationDetails : description = " + choice.Description + " locality = " + choice.Locality + " ");
				logMsg.Append(gisResult.Stops.Rows.Count + " NaPTANs: ");
			}

			for ( int i=0; i < gisResult.Stops.Rows.Count; i++)
			{
				QuerySchema.StopsRow row= (QuerySchema.StopsRow)gisResult.Stops.Rows[i];
				location.NaPTANs[i] = new TDNaptan();				
				location.NaPTANs[i].Naptan = row.atcocode;
				location.NaPTANs[i].GridReference = new OSGridReference((int)row.X, (int)row.Y);
				location.NaPTANs[i].Name = row.commonname;

				if (TDTraceSwitch.TraceVerbose)
				{
					logMsg.Append(row.atcocode + " ");
				}
			}

			gisResult = gisQuery.FindNearestITN(choice.OSGridReference.Easting,
												choice.OSGridReference.Northing,
												string.Empty,
												true);

			location.Toid = new string[gisResult.ITN.Rows.Count];

			if (TDTraceSwitch.TraceVerbose)
			{
				logMsg.Append(" and " + gisResult.ITN.Rows.Count + " TOIDs: ");
			}

			for ( int i=0; i < gisResult.ITN.Rows.Count; i++)
			{
				QuerySchema.ITNRow row= (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
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

		public override void PopulateToids(ref TDLocation location)
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
				return false;
			}
		}
		#endregion

	}
}
