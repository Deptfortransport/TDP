// *********************************************** 
// NAME                 : MajorStationsGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Gazetteer for Major stations. Inherits from TDGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/MajorStationsGazetteer.cs-arc  $ 
//
//   Rev 1.1   Mar 30 2012 16:21:00   MTurner
//Fixed bug in Find A Car
//
//   Rev 1.0   Nov 08 2007 12:25:14   mturner
//Initial revision.
//
//   Rev 1.24   Jul 13 2006 15:34:00   rphilpott
//Remove unnecessary Naptan check for "no locaility" error msg
//
//   Rev 1.23   Jul 13 2006 15:24:34   rphilpott
//Use NaptanCache to get locality instead of separate GIS Query calls, so that it works for Exchange Points, too, and to improve efficiency.
//Resolution for 4128: Foula Ferry Terminal gets stuck on ambiguity page.
//
//   Rev 1.22   Apr 27 2006 14:54:14   halkatib
//Resolution for IR3996: Changed the FindNearestITNs() method with the FindNearestITN() method so that only one TOID is sent in the journey request. 
//
//   Rev 1.21   Apr 20 2006 15:52:04   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.20   Jan 18 2005 17:43:42   passuied
//moved Xml handling into a separate class and added handling for code gaz
//
//   Rev 1.19   Sep 11 2004 14:20:32   RPhilpott
//Allow for no rows being returned by FindStopsInfoForStops
//Resolution for 1573: City to City - No flights are returned when planning journeys with flight expectations
//
//   Rev 1.18   Sep 11 2004 13:16:10   RPhilpott
//Move airport "puffing up" logic into TDLocation so that it is available in all three situations where it is needed.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1328: Find nearest stations/airports does not return any results
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.17   Sep 10 2004 15:35:52   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.16   Jul 15 2004 18:02:04   acaunt
//Logging method moved to TDGazetteer which now passes submitted time to Gazetteer event.
//
//   Rev 1.15   Jul 09 2004 13:09:14   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.14   May 14 2004 15:29:18   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.13   Mar 08 2004 16:56:18   PNorell
//Updated for doing drill downs.
//
//   Rev 1.12   Dec 11 2003 17:52:48   RPhilpott
//Make use of new FindStopsInRadius() method in GISQuery.
//Resolution for 281: Postcode journey doesn't return public transport journeys
//
//   Rev 1.11   Dec 03 2003 12:21:28   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.10   Oct 14 2003 12:48:22   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.9   Oct 09 2003 18:45:26   RPhilpott
//Change locality and naptan processing to match data coming back from gazetteer
//
//   Rev 1.8   Oct 09 2003 10:31:58   passuied
//implementation of minScore LocationChoiceList tailing
//
//   Rev 1.7   Oct 07 2003 13:33:00   passuied
//fixed the bug of gisquery stored to session but not serialisable (now not stored in session)
//
//   Rev 1.6   Oct 03 2003 13:38:32   PNorell
//Updated the new exception identifier.
//
//   Rev 1.5   Oct 02 2003 14:41:08   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.4   Sep 22 2003 17:52:30   passuied
//corrected Serializable problem (thanks Peter!)
//
//   Rev 1.3   Sep 22 2003 17:31:24   passuied
//made all objects serializable
//
//   Rev 1.2   Sep 20 2003 16:59:50   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:52   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:34   passuied
//Initial Revision

using System;
using System.Text;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Gazetteer for Major stations. Inherits from TDGazetteer
	/// </summary>
	[Serializable()]
	public class MajorStationsGazetteer : TDGazetteer
	{
		private static readonly string GExchangeType = "G";
		private static readonly string exchangePointByGroupName = "EXCHANGEPOINT_BY_GROUP:";
		private StationType stationType = StationType.Undetermined;
		
		public MajorStationsGazetteer(string sessionID, bool userLoggedOn, StationType stationType):base(sessionID, userLoggedOn)
		{
			this.stationType = stationType;
			// will retrieve the gazetteerId from properties according to the stationType param
			gazetteerId = Properties.Current[string.Format(LSKeys.MajorStationsGazetteerIdProperties, stationType.ToString())];
			
			if (gazetteerId== null || gazetteerId == string.Empty)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"locationservice.majorstationsgazetteerid not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.majorstationsgazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSMajorStationGazetteerIDInvalid);
			}

			minScore = Convert.ToInt32(Properties.Current["locationservice.majorstations.minscore"], CultureInfo.CurrentCulture);

			OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
				"Using MajorStationsGazetteer - minimum score = " + minScore);
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
				throw new TDException("locationservice.maxreturnedrecords not set or set to a wrong format", true, TDExceptionIdentifier.LSMajorStationMaxReturnPropertyInvalid);
			}
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
			Logging(GazetteerEventCategory.GazetteerMajorStations, submitted);
			return xmlHandler.ReadDrillDownResult(result,	string.Empty);
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
				throw new TDException("Unable to drill into a choice without children", true, TDExceptionIdentifier.LSAddressDrillLacksChildren);

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
			Logging(GazetteerEventCategory.GazetteerMajorStations, submitted);		
			return xmlHandler.ReadDrillDownResult(result, 	pickList.Current);			
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

			if (choice.HasChilden)
			{
				throw new TDException("Unable to get location details for a choice that still has children", false, TDExceptionIdentifier.LSAddressPostCodeGazetteerHasChildren);
			}

			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

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
			Logging(GazetteerEventCategory.GazetteerMajorStations, submitted);	
				LocationQueryResult result = xmlHandler.ReadFinalDrillDownResult(gazopsResult,	pickList);

				choice = (LocationChoice)result.LocationChoiceList[0];
			}

			location.Description = choice.Description;
			location.GridReference = choice.OSGridReference;
			location.Locality = choice.Locality;
			location.RequestPlaceType = RequestPlaceType.NaPTAN;
			location.SearchType = SearchType.MainStationAirport;
			location.Status = TDLocationStatus.Valid;

			StringBuilder logMsg = new StringBuilder();
			if( TDTraceSwitch.TraceVerbose )
			{
				logMsg.Append("GetLocationDetails : description = " + choice.Description + " locality = " + choice.Locality + " ");
			}
			QuerySchema gisResult = null;

			// Don't need to find toids if StationType is set to a value
			// different from Undetermined.
			// This is for performance issue, because it is impossible to 
			// have a car journey when you do a Find A 'coach' or 'train'
			// so we don't need the information
			
			if (stationType == StationType.Undetermined || stationType == StationType.UndeterminedNoGroup)
			{
				gisResult = gisQuery.FindNearestITN(choice.OSGridReference.Easting,
													choice.OSGridReference.Northing,
													string.Empty,
													true);

				if (TDTraceSwitch.TraceVerbose)
				{
					logMsg.Append(gisResult.ITN.Rows.Count + " TOIDs: ");
				}
				
				location.Toid = new string[gisResult.ITN.Rows.Count];

				for ( int i=0; i < gisResult.ITN.Rows.Count; i++)
				{
					QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
					location.Toid[i] = row.toid;
					
					if	(TDTraceSwitch.TraceVerbose)
					{
						logMsg.Append(row.toid + " ");
					}
				}
			}

			if (choice.ExchangePointType == GExchangeType)
			{
				location.NaPTANs = xmlHandler.ReadFetchRecord(GazopsWeb.FetchRecord(
					1, 
					exchangePointByGroupName + choice.Naptan, 
					gazetteerId, 
					string.Empty, 
					string.Empty));
			}
			else
			{
				location.NaPTANs = new TDNaptan[] {new TDNaptan(choice.Naptan, choice.OSGridReference)};
			}

			if( TDTraceSwitch.TraceVerbose )
			{
				logMsg.Append(" and " + location.NaPTANs.Length + " NaPTANs: ");
				foreach (TDNaptan naptan in location.NaPTANs)
				{
					logMsg.Append(naptan.Naptan + " ");
				}
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}

			// Locality will not be returned by MajorStations gazetteer 
			//  so derive it from associated NaPTANs, using the Naptan Cache ...

			if	((location.Locality == null || location.Locality == string.Empty) 
				&& (location.NaPTANs.Length > 0))
			{
				foreach (TDNaptan naptan in location.NaPTANs)
				{
					NaptanCacheEntry nce = NaptanLookup.Get(naptan.Naptan, location.Description);	

					if	(nce != null && nce.Found)
					{
						location.Locality = nce.Locality;

						if  (TDTraceSwitch.TraceVerbose) 
						{
							Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
								"MajorStationsGazetteer - got locality " + location.Locality
								+ " from " + naptan.Naptan));
						}
						break;
					}
				}
			}

			// Still no locality, so try to get it from the OSGR

			if	(location.Locality == null || location.Locality == string.Empty)
			{
				location.Locality = gisQuery.FindNearestLocality(location.GridReference.Easting, 
																 location.GridReference.Northing); 
				if	(TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
						"MajorStationsGazetteer - got locality " + location.Locality
						+ " from OSGR " + location.GridReference.Easting + "," + location.GridReference.Northing));
				}
			}

			// Still no locality ("this should never happen") -- give up, but log an error

			if	(location.Locality == null || location.Locality.Length == 0) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
					"MajorStationsGazetteer - unable to find any locality for " + location.Description));
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
			get { return false; }
		}
		#endregion
	}
}
