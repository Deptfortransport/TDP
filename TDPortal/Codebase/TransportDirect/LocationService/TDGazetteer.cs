// *********************************************** 
// NAME                 : TDGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Base class for all gazetteers. Implements ITDGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:22   mturner
//Initial revision.
//
//   Rev 1.26   Apr 20 2006 15:52:16   tmollart
//Updated method signature for GetLocationDetails so that locality search required argument is no longer passed in. Updated methods where required so that parameter no longer has an effect.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.25   Jan 18 2005 17:43:46   passuied
//moved Xml handling into a separate class and added handling for code gaz
//
//   Rev 1.24   Nov 03 2004 14:21:48   passuied
//Fix : used parameter in Logging method instead of fixed gazetteereventcategory Postcode
//Resolution for 1728: GazetteerEvent type Postcode is the only gazetteer event raised.
//
//   Rev 1.23   Sep 10 2004 15:35:54   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.22   Jul 15 2004 18:02:04   acaunt
//Logging method moved to TDGazetteer which now passes submitted time to Gazetteer event.
//
//   Rev 1.21   Jul 09 2004 13:09:16   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.20   May 24 2004 16:55:24   RPhilpott
//Use GAZOPS_UNIQUE_ID instead of NAPTAN to pass to FetchRecord call, if appropriate.
//
//   Rev 1.19   May 14 2004 15:29:20   passuied
//Changes for FindAiports functionality. Change of GetLocationDetails interface to introduce disableGisQuery. avoid calling PopulateNaptanAndToids before searching for airports
//
//   Rev 1.18   Apr 27 2004 13:44:38   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.17   Mar 08 2004 16:56:34   PNorell
//Updated for supporting drill downs for Major station gazetteer.
//
//   Rev 1.16   Dec 03 2003 12:21:30   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.15   Nov 13 2003 20:33:16   RPhilpott
//Do not remove score==0 entry from choice list if it is the only entry. 
//Resolution for 125: Address Specification
//
//   Rev 1.14   Oct 14 2003 12:48:24   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.13   Oct 11 2003 19:35:34   RPhilpott
//Add extra XML tag for NAPTAN's fro AllStationsGazetteer
//
//   Rev 1.12   Oct 09 2003 18:45:26   RPhilpott
//Change locality and naptan processing to match data coming back from gazetteer
//
//   Rev 1.11   Oct 09 2003 10:32:00   passuied
//implementation of minScore LocationChoiceList tailing
//
//   Rev 1.10   Oct 08 2003 10:44:48   passuied
//implemented detection of ADMINAREA choice to avoid exception when trying to get location details
//
//   Rev 1.9   Oct 07 2003 13:33:02   passuied
//fixed the bug of gisquery stored to session but not serialisable (now not stored in session)
//
//   Rev 1.8   Oct 03 2003 15:29:16   passuied
//working version	
//
//   Rev 1.7   Oct 03 2003 13:38:36   PNorell
//Updated the new exception identifier.
//
//   Rev 1.6   Sep 23 2003 13:26:32   passuied
//Fixed to get GISQuery from ServiceDiscovery
//
//   Rev 1.5   Sep 23 2003 13:19:52   passuied
//corrected errors inserted from previous changes
//
//   Rev 1.4   Sep 22 2003 17:52:30   passuied
//corrected Serializable problem (thanks Peter!)
//
//   Rev 1.3   Sep 22 2003 17:31:30   passuied
//made all objects serializable
//
//   Rev 1.2   Sep 20 2003 16:59:52   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:58   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:42   passuied
//Initial Revision

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Base class for all gazetteers. Implements ITDGazetteer
	/// </summary>
	[Serializable()]
	public abstract class TDGazetteer : ITDGazetteer
	{


		#region Member variables
		protected string gazetteerId = string.Empty;

		[NonSerialized()]
		private GazopsWeb.GazopsWeb gazopsWeb;

	
		protected string sessionID;
		protected bool userLoggedOn;
		protected int minScore;

		protected XmlGazetteerHandler xmlHandler;

		#endregion

		protected GazopsWeb.GazopsWeb GazopsWeb
		{
			get 
			{
				if( gazopsWeb == null )
				{
					gazopsWeb = new GazopsWeb.GazopsWeb();
					gazopsWeb.Url = Properties.Current["locationservice.gazopsweburl"];
					if (gazopsWeb.Url == string.Empty)
					{
						OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,sessionID,  TDTraceLevel.Error, "Unable to access the GazopsWeb Url property");
						Logger.Write(oe);
						throw new TDException ("Unable to access the GazopsWeb Url property", true, TDExceptionIdentifier.LSGazopURLUnavailable);
					}
				}
				return gazopsWeb;
			}
		}



		/// <summary>
		/// Method called for logging purpose
		/// </summary>
		/// <param name="gazetterEventCategory">The type of gazetteer that is being called</param>
		protected void Logging(GazetteerEventCategory gazetterEventCategory, DateTime submitted)
		{
			GazetteerEvent ge =  new GazetteerEvent(gazetterEventCategory, submitted, sessionID, userLoggedOn);
			Logger.Write(ge);
		}
	

		#region Constructor
		public TDGazetteer(string sessionID, bool userLoggedOn)
		{
			this.sessionID = sessionID;
			this.userLoggedOn = userLoggedOn;
		}
		#endregion

		#region ITDGazetteer methods implementations
		public abstract LocationQueryResult FindLocation(string text, bool fuzzy);
		public abstract LocationQueryResult DrillDown (
							string text, 
							bool fuzzy, 
							string picklist, 
							string queryRef, 
							LocationChoice choice);

		public abstract void GetLocationDetails ( 
							ref TDLocation location,
							string text,
							bool fuzzy, 
							string pickList,
							string queryRef,
							LocationChoice choice, 
							int maxDistance,
							bool disableGisQuery);

		public abstract bool SupportHierarchicSearch { get;}
		public abstract void PopulateLocality(ref TDLocation location);
		public abstract void PopulateToids(ref TDLocation location);

		#endregion
	
	}
}