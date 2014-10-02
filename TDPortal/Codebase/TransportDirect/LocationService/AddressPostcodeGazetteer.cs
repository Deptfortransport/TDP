// *********************************************** 
// NAME                 : AddressPostcodeGazetteer
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Gazetteer for addesses and Postcode. Inherits from TDGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/AddressPostcodeGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:24:58   mturner
//Initial revision.
//
//   Rev 1.34   Apr 30 2007 10:14:00   mmodi
//Reset the NaPTANs property for the location object
//Resolution for 2555: Del 9.6: Reversing journey direction in Find a Car
//
//   Rev 1.33   Apr 20 2006 15:50:44   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.32   Mar 31 2006 18:45:14   RPhilpott
//Use new "GetStreetsForPostcode()" method when getting TOIDs for a postcode.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.31   Mar 20 2006 18:02:20   RWilby
//Merged stream0027: Start/End TOIDs changes.
//
//   Rev 1.30.1.0   Mar 14 2006 13:51:22   RWilby
//Changed PopulateToids method to use new method FindNearestITN on GISQuery class
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.30   May 05 2005 16:31:44   tmollart
//Modified methods to trim spaces of entered text.
//Resolution for 2209: DEL 7.1: Space before number and postcode does not expand the address correctly
//
//   Rev 1.29   Apr 13 2005 09:10:04   rscott
//DEL 7 Additional Tasks - IR1978 enhancements added - reject single word address.
//
//   Rev 1.28   Jan 18 2005 17:43:26   passuied
//moved Xml handling into a separate class and added handling for code gaz
//
//   Rev 1.27   Sep 10 2004 15:35:36   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.26   Jul 15 2004 18:02:06   acaunt
//Logging method moved to TDGazetteer which now passes submitted time to Gazetteer event.
//
//   Rev 1.25   Jul 13 2004 12:57:00   passuied
//changes to compile....
//
//   Rev 1.24   Jul 13 2004 11:09:10   passuied
//fixed problem in FillToids
//
//   Rev 1.23   Jul 09 2004 13:09:08   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.22   May 24 2004 17:25:58   RPhilpott
//Replace FindStopsInRadius() with FindNeraestStopsAndITNs().
//Resolution for 928: Change "Find Nearest NaPTAN" function to speed up process for a particular GIS query
//
//   Rev 1.21   May 14 2004 15:29:06   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.20   Apr 27 2004 13:44:36   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.19   Dec 11 2003 17:52:40   RPhilpott
//Make use of new FindStopsInRadius() method in GISQuery.
//Resolution for 281: Postcode journey doesn't return public transport journeys
//
//   Rev 1.18   Dec 03 2003 12:21:18   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.17   Oct 14 2003 12:48:26   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.16   Oct 11 2003 19:40:04   RPhilpott
//Correct locality determination 
//
//   Rev 1.15   Oct 09 2003 18:46:02   RPhilpott
//Add verbose logging on all gazetteer calls
//
//   Rev 1.14   Oct 09 2003 10:32:00   passuied
//implementation of minScore LocationChoiceList tailing
//
//   Rev 1.13   Oct 08 2003 12:02:42   passuied
//Only divide easting and northing by 10 for addresses (not for postcodes!!!)
//
//   Rev 1.12   Oct 07 2003 15:30:40   passuied
//corrected the error so the osgridreference for addressPostcode gets divided by 10
//
//   Rev 1.11   Oct 07 2003 13:33:02   passuied
//fixed the bug of gisquery stored to session but not serialisable (now not stored in session)
//
//   Rev 1.10   Oct 07 2003 08:44:28   PNorell
//Added new exception identifier for AddressPostCodeGazetteer.
//
//   Rev 1.9   Oct 06 2003 16:31:20   PNorell
//Changed class to use updated TDExceptionIdentifier.
//
//   Rev 1.8   Oct 03 2003 15:29:04   passuied
//working version saved just in case
//
//   Rev 1.7   Oct 02 2003 14:41:08   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.6   Sep 23 2003 13:19:54   passuied
//corrected errors inserted from previous changes
//
//   Rev 1.5   Sep 22 2003 17:52:32   passuied
//corrected Serializable problem (thanks Peter!)
//
//   Rev 1.4   Sep 22 2003 17:31:14   passuied
//made all objects serializable
//
//   Rev 1.3   Sep 22 2003 13:39:34   RPhilpott
//Uncomment FindStopsInGroupForStops(schema) calls
//
//   Rev 1.2   Sep 20 2003 16:59:42   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:42   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:20   passuied
//Initial Revision

using System;
using System.Text;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.LocationService
{

	/// <summary>
	/// Gazetteer for addesses and Postcode. Inherits from TDGazetteer
	/// </summary>
	[Serializable()]
	public  class AddressPostcodeGazetteer : TDGazetteer
	{
		#region Constructor
		public AddressPostcodeGazetteer(string sessionID, bool userLoggedOn): base(sessionID, userLoggedOn)
		{
			gazetteerId = Properties.Current["locationservice.addressgazetteerid"];
			if (gazetteerId == string.Empty)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					sessionID,
					TDTraceLevel.Error,
					"locationservice.addressgazetteerid not set or set to a wrong format");

				Logger.Write(oe);
				throw new TDException("locationservice.addresspostcodegazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSAddressPostCodeGazetteerIDInvalid);
			}

			minScore = Convert.ToInt32(Properties.Current["locationservice.addresspostcode.minscore"], CultureInfo.CurrentCulture);

			OperationalEvent log = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
				"Using AddressPostcodeGazetteer - minimum score = " + minScore);
			Logger.Write(log);

			xmlHandler = new XmlGazetteerHandler(userLoggedOn, sessionID, minScore);


		}
		#endregion

		#region ITDGazetteer implementation
		/// <summary>
		/// Method searching a location from a string.
		/// </summary>
		/// <param name="text">input text string</param>
		/// <param name="fuzzy">indicates if search should be fuzzy</param>
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
		public override LocationQueryResult FindLocation(string text, bool fuzzy)
		{
			// if the input text consists of just a postcode : Call the PostcodeMatch() method
			if (PostcodeSyntaxChecker.IsPostCode(text.Trim()))
			{	
				gazetteerId = Properties.Current["locationservice.postcodegazetteerid"];
				if (gazetteerId== null || gazetteerId == string.Empty)
				{
					OperationalEvent oe = new OperationalEvent(
						TDEventCategory.Business,
						sessionID,
						TDTraceLevel.Error,
						"locationservice.addresspostcodegazetteerid not set or set to a wrong format");

					Logger.Write(oe);
					throw new TDException("locationservice.addresspostcodegazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSAddressPostCodeGazetteerIDInvalid);
				}
				DateTime submitted = DateTime.Now;
				string result = GazopsWeb.PostcodeMatch(
									text.Trim(),
									gazetteerId,
									string.Empty,
									string.Empty);
				Logging(submitted, true);

				return xmlHandler.ReadPostcodeMatchResult(result);
			}
			// else if the input text consists of just a partial postcode : Call the PartPostcodeMatch() method
			else if(PostcodeSyntaxChecker.IsPartPostCode(text.Trim()))
			{
				gazetteerId = Properties.Current["locationservice.partpostcodegazetteerid"];
				if (gazetteerId == string.Empty)
				{
					OperationalEvent oe = new OperationalEvent(
						TDEventCategory.Business,
						sessionID,
						TDTraceLevel.Error,
						"locationservice.addresspostcodegazetteerid not set or set to a wrong format");

					Logger.Write(oe);
					throw new TDException("locationservice.addresspostcodegazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSAddressPostCodeGazetteerIDInvalid);
				}
				DateTime submitted = DateTime.Now;
				string result = GazopsWeb.PostcodeMatch(
					text.Trim(),
					gazetteerId,
					string.Empty,
					string.Empty);
				Logging(submitted, true);
				return xmlHandler.ReadPostcodeMatchResult(result);
				
			}
			else
			{			
				if (AddressSyntaxChecker.IsNotSingleWord(text))
				{

					gazetteerId = Properties.Current["locationservice.addressgazetteerid"];
					if (gazetteerId == string.Empty)
					{
						OperationalEvent oe = new OperationalEvent(
							TDEventCategory.Business,
							sessionID,
							TDTraceLevel.Error,
							"locationservice.addressgazetteerid not set or set to a wrong format");

						Logger.Write(oe);
						throw new TDException("locationservice.addresspostcodegazetteerid not set or set to a wrong format", true, TDExceptionIdentifier.LSAddressPostCodeGazetteerIDInvalid);
					}

					DateTime submitted = DateTime.Now;
					String result = GazopsWeb.DrillDownAddressQuery(
						text.Trim(),
						string.Empty,
						string.Empty,
						string.Empty,
						fuzzy,
						gazetteerId,
						string.Empty,
						string.Empty
						);
					Logging(submitted, false);
					return xmlHandler.ReadDrillDownResult(result, String.Empty);
				}
				else 
				{
					//The query text is a single word address hence set return to "too vague"
					LocationQueryResult lqr = new LocationQueryResult("");
					lqr.IsSingleWordAddress = true;
					return lqr;
				}
			}
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
														text.Trim(),
														pickList.Current,
														string.Empty,
														queryRef,
														fuzzy,
														gazetteerId,
														string.Empty,
														string.Empty);
			Logging(submitted,false);
			return xmlHandler.ReadDrillDownResult(result, pickList.Current);
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
		/// <returns>Returns a LocationQueryResult representing the result of the query</returns>
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
				throw new TDException("Unable to get location details for a choice that still has children", false, TDExceptionIdentifier.LSAddressPostCodeGazetteerHasChildren);

			// If Easting or Northing have not been populated
			if (choice.OSGridReference.Easting ==-1 
				|| choice.OSGridReference.Northing == -1)
			{
				DateTime submitted = DateTime.Now;
				String gazopsResult = 	GazopsWeb.DrillDownAddressQuery(
										text.Trim(),
										pickList,
										choice.PicklistValue,
										queryRef,
										fuzzy,
										gazetteerId,
										string.Empty,
										string.Empty);
				Logging(submitted, false);
				LocationQueryResult result = xmlHandler.ReadFinalDrillDownResult(gazopsResult, pickList);
				choice = (LocationChoice)result.LocationChoiceList[0];
			}

			// For address gazetteer : divide easting and northing by 10
			if (gazetteerId == Properties.Current["locationservice.addressgazetteerid"])
			{
				choice.OSGridReference.Easting /= 10;
				choice.OSGridReference.Northing /= 10;
				location.AddressToMatch = choice.Description;
			}

			if (PostcodeSyntaxChecker.IsPostCode(text.Trim()))
			{
				IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
		
				string[] streets = gisQuery.GetStreetsFromPostCode(text.Trim());

				StringBuilder streetList = new StringBuilder(200);

				foreach (string street in streets)
				{
					if	(streetList.Length > 0)
					{
						streetList.Append(',');
					}
					streetList.Append(street);
				}

				location.AddressToMatch = streetList.ToString();
			}

			location.Description = choice.Description;
			location.GridReference = choice.OSGridReference;
			location.RequestPlaceType = RequestPlaceType.Coordinate;
			location.SearchType = SearchType.AddressPostCode;
			location.Status = TDLocationStatus.Valid;

			location.PartPostcodeMaxX = choice.PartPostcodeMaxX;
			location.PartPostcodeMaxY = choice.PartPostcodeMaxY;
			location.PartPostcodeMinX = choice.PartPostcodeMinX;
			location.PartPostcodeMinY = choice.PartPostcodeMinY;
			
			if	(!disableGisQuery)
			{
				PopulateLocality(ref location);
				PopulateToids(ref location);
			}

			// Because an Addresspostcode doesnt return station NaPTANs, need to reset this property to 
			// avoid scenario where previous search NaPTANs are used
			location.NaPTANs = new TDNaptan[0];
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
				QuerySchema.ITNRow row= (QuerySchema.ITNRow) gisResult.ITN.Rows[i];
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

		#region Private methods
		/// <summary>
		/// Method called for logging purpose
		/// </summary>
		/// <param name="isPostcodeMatch">indicates if the method PostcodeMatch is called</param>
		private void Logging(DateTime submitted, bool isPostcodeMatch)
		{
			if (!isPostcodeMatch)
			{
				Logging(GazetteerEventCategory.GazetteerAddress, submitted);
			}
			else
			{
				Logging(GazetteerEventCategory.GazetteerPostCode, submitted);
			}
		}
		#endregion
	}
}
