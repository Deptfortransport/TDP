// *********************************************** 
// NAME                 : AttractionsGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Gazetteer for Attractions. Inherits from TDGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/AttractionsGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:00   mturner
//Initial revision.
//
//   Rev 1.26   Apr 30 2007 10:13:42   mmodi
//Reset the NaPTANs properrty for the location object
//Resolution for 2555: Del 9.6: Reversing journey direction in Find a Car
//
//   Rev 1.25   Apr 20 2006 15:51:08   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.24   Mar 31 2006 18:45:56   RPhilpott
//Pass address of attraction to FindNearestITN() method to get a single TOID.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.23   Jan 18 2005 17:43:32   passuied
//moved Xml handling into a separate class and added handling for code gaz
//
//   Rev 1.22   Sep 10 2004 15:35:42   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.21   Jul 15 2004 18:02:06   acaunt
//Logging method moved to TDGazetteer which now passes submitted time to Gazetteer event.
//
//   Rev 1.20   Jul 13 2004 12:56:58   passuied
//changes to compile....
//
//   Rev 1.19   Jul 13 2004 11:09:08   passuied
//fixed problem in FillToids
//
//   Rev 1.18   Jul 09 2004 13:09:10   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.17   May 24 2004 17:25:56   RPhilpott
//Replace FindStopsInRadius() with FindNeraestStopsAndITNs().
//Resolution for 928: Change "Find Nearest NaPTAN" function to speed up process for a particular GIS query
//
//   Rev 1.16   May 14 2004 15:29:10   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.15   Dec 11 2003 17:52:42   RPhilpott
//Make use of new FindStopsInRadius() method in GISQuery.
//Resolution for 281: Postcode journey doesn't return public transport journeys
//
//   Rev 1.14   Dec 03 2003 12:21:22   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.13   Nov 20 2003 11:02:28   kcheung
//Fixed so that naptan is correctly populated for attocode
//
//   Rev 1.12   Oct 14 2003 12:48:28   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.11   Oct 09 2003 18:46:00   RPhilpott
//Add verbose logging on all gazetteer calls
//
//   Rev 1.10   Oct 09 2003 10:31:54   passuied
//implementation of minScore LocationChoiceList tailing
//
//   Rev 1.9   Oct 07 2003 13:32:58   passuied
//fixed the bug of gisquery stored to session but not serialisable (now not stored in session)
//
//   Rev 1.8   Oct 03 2003 13:38:32   PNorell
//Updated the new exception identifier.
//
//   Rev 1.7   Oct 02 2003 14:41:10   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.6   Sep 23 2003 13:19:50   passuied
//corrected errors inserted from previous changes
//
//   Rev 1.5   Sep 22 2003 17:52:28   passuied
//corrected Serializable problem (thanks Peter!)
//
//   Rev 1.4   Sep 22 2003 17:31:16   passuied
//made all objects serializable
//
//   Rev 1.3   Sep 22 2003 13:39:30   RPhilpott
//Uncomment FindStopsInGroupForStops(schema) calls
//
//   Rev 1.2   Sep 20 2003 16:59:46   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:44   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:22   passuied
//Initial Revision

using System;
using System.Text;
using System.Collections;
using System.Globalization;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for AttractionsGazetteer.
	/// </summary>
	[Serializable()]
	public class AttractionsGazetteer : TDGazetteer
	{
		public AttractionsGazetteer(string sessionID, bool userLoggedOn):base(sessionID, userLoggedOn)
		{
			gazetteerId = Properties.Current["locationservice.attractionsgazetteerid"];
			if (gazetteerId== null || gazetteerId == string.Empty)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"locationservice.attractionsgazetteerid not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.attractionsgazetteerid not set or set to a wrong format", true,TDExceptionIdentifier.LSAttractionGazetteerIDInvalid);

			}

			minScore = Convert.ToInt32(Properties.Current["locationservice.attractions.minscore"], CultureInfo.CurrentCulture);

			OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
				"Using AttractionsGazetteer - minimum score = " + minScore);
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
				throw new TDException("locationservice.maxreturnedrecords not set or set to a wrong format", true, TDExceptionIdentifier.LSAttractionMaxReturnPropertyInvalid);
			}
			DateTime submitted = DateTime.Now;
			String result =				GazopsWeb.PlaceNameMatch (
							text,
							fuzzy,
							-1,
							-1,
							gazetteerId,
							string.Empty,
							maxReturnedRecords,
							string.Empty);
			Logging(GazetteerEventCategory.GazetteerPointOfInterest, submitted);
			return xmlHandler.ReadPlacenameMatchResult (result	);
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
			throw new TDException ("DrillDown cannot be called for this gazetteer", true, TDExceptionIdentifier.LSAttractionLacksChildren);
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
			location.RequestPlaceType = RequestPlaceType.Coordinate;
			location.Status = TDLocationStatus.Valid;
			location.SearchType = SearchType.POI;
			
			location.AddressToMatch = choice.Description;

			if  (!disableGisQuery)
			{
				PopulateLocality(ref location);				
				PopulateToids(ref location);
			}

			// Because the Attractions gaz doesnt return station NaPTANs, need to reset this property to 
			// avoid scenario where previous search NaPTANs are used
			location.NaPTANs = new TDNaptan[0];
		}


		public override void PopulateLocality(ref TDLocation location)
		{

			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
		
			location.Locality = gisQuery.FindNearestLocality(location.GridReference.Easting, 
				location.GridReference.Northing);
			
			if (TDTraceSwitch.TraceVerbose)
			{
				StringBuilder logMsg = new StringBuilder();
				logMsg.Append("GetLocationDetails : description = " + location.Description + " -- locality = " + location.Locality + " ");
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}
		}

		public override void PopulateToids(ref TDLocation location)
		{
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			QuerySchema gisResult = gisQuery.FindNearestITN(location.GridReference.Easting, 
															location.GridReference.Northing,
															location.AddressToMatch,
															true);
			
			StringBuilder logMsg = new StringBuilder();

			if (TDTraceSwitch.TraceVerbose)
			{
				logMsg.Append("GetLocationDetails : description = " + location.Description + " address to match = " + location.AddressToMatch + " -- ");
				logMsg.Append(gisResult.ITN.Rows.Count + " TOIDs: ");
			}
			
			location.Toid = new string[gisResult.ITN.Rows.Count];

			for (int i=0; i < gisResult.ITN.Rows.Count; i++)
			{
				QuerySchema.ITNRow row = (QuerySchema.ITNRow) gisResult.ITN.Rows[i];
				location.Toid[i] = row.toid;
				
				if (TDTraceSwitch.TraceVerbose)
				{
					logMsg.Append(row.toid + " (" + row.name + ")  ");
				}
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}
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
