// ***********************************************
// NAME 			: TDMapHandoff.cs
// AUTHOR 			: James Cotton
// DATE CREATED		: 28-Aug-2003
// DESCRIPTION 		: The hand off to ESRI map component will provide two capabilities:
//							1.	Saving journey routes to the ESRI MasterMap database
//							2.	Removal of journey routes from the ESRI MasterMap database
// DESIGN DOCUMENT	: TDP-DV/DD010 Hand Off to ESRI Map
// 
// ************************************************
// 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDMapHandoff.cs-arc  $
//
//   Rev 1.3   Feb 04 2013 10:40:06   mmodi
//Updated for walk legs in public interchange detail legs
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.2   Oct 12 2009 09:10:58   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:42:54   apatel
//EBC Map and Printer Friendly pages related chages
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:39:44   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 13 2008 16:45:04   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Sep 08 2008 15:47:02   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:24:02   mturner
//Initial revision.
//
//   Rev 1.23   Aug 25 2005 08:58:52   rgreenwood
//IR 2662 - AppendPublicJourney() now performs co-ordinate substitution in the event of invalid Leg Start or Leg End co-ordinates.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.22   Aug 19 2005 14:04:26   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.21.1.1   Aug 02 2005 16:43:22   rgreenwood
//DD073 Map Details: changed AppendPublicJourney method from private to public for UT purposes
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.21.1.0   Jul 25 2005 16:54:12   rgreenwood
//DD073 Map Details: Amended AppendPublicJourney() method to skip greyed out legs.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.21   Mar 22 2005 10:26:54   jbroome
//Added new SaveJourneyResult overload method
//
//   Rev 1.20   Jul 26 2004 15:57:48   RPhilpott
//Use TOID prefix from Properties service to determine what and if to chop from the beginning of TOID's returned by the CJP. 
//Resolution for 1152: Technical errors in road requests to CJP
//
//   Rev 1.19   Apr 19 2004 13:26:50   CHosegood
//Added RailReplacement functionality
//Resolution for 663: Rail fares not being displayed
//Resolution for 697: Bus replacement change
//
//   Rev 1.18   Feb 11 2004 14:27:54   PNorell
//Updated for new xml structure and map data.
//
//   Rev 1.17   Feb 11 2004 12:08:38   PNorell
//Updated to support multiple journeys map handoff.
//Updated to support one stored proc call instead of many.
//
//   Rev 1.16   Feb 10 2004 16:18:40   PNorell
//Intermidary checkin. Map handoff updated for new stored procedure for road journeys. Public journeys unchanged.
//
//   Rev 1.15   Nov 21 2003 16:29:38   PNorell
//IR305 - Unadjusted journeys saved with correct congestion number.
//
//   Rev 1.14   Oct 22 2003 18:17:32   RPhilpott
//Fix exception logging
//
//   Rev 1.13   Oct 10 2003 19:54:10   RPhilpott
//Add more exception logging
//
//   Rev 1.12   Oct 09 2003 19:50:46   PNorell
//Ensured correct value is input for type of journey.
//
//   Rev 1.11   Oct 08 2003 18:37:26   PNorell
//Fixed TOID prefix of osbg gets removed before storing into database.
//
//   Rev 1.10   Sep 25 2003 11:44:40   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.9   Sep 19 2003 12:31:16   RPhilpott
//Remove spurious using for NUnit. 
//
//   Rev 1.8   Sep 08 2003 15:57:14   jcotton
//Final integration with TDEventLogging Service
//
//   Rev 1.7   Sep 05 2003 15:28:58   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.6   Sep 05 2003 08:57:48   jcotton
//Gave more discriptive names to variables and made inner 'Save' routines private.
//
//   Rev 1.5   Sep 02 2003 10:49:04   jcotton
//Deleted commented out unused code section
//
//   Rev 1.4   Sep 02 2003 09:55:38   jcotton
//Integration with TDEventLogging Service
//
//   Rev 1.3   Sep 02 2003 09:11:42   jcotton
//Minor modifications to meet FXCop requirements
//
//   Rev 1.2   Sep 01 2003 16:29:50   jcotton
//Post Nunit Testing
//
//   Rev 1.1   Aug 28 2003 17:00:56   jcotton
//Initial Code Release
//   

using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	///The hand off to ESRI map component will provide two capabilities:
	///	1.	Saving journey routes to the ESRI MasterMap database
	///	2.	Removal of journey routes from the ESRI MasterMap database
	/// DESIGN DOCUMENT	: TDP-DV/DD010 Hand Off to ESRI Map	
	/// </summary>
	public class TDMapHandoff : ITDMapHandoff
	{
	

		private const string TOID_PREFIX = "JourneyControl.ToidPrefix";

		/// <summary>
		/// Used by TDMapHandoff for database connectivity and interaction.
		/// Opened on each call to the database and immeadiately closed on retrun.
		/// </summary>
		private SqlHelper TDMapHandoffSQL = new SqlHelper();

        /// <summary>
        /// Congestion value of the road routes. Default is -1(no congestion)
        /// </summary>
        private int congestionValue = -1;
		
		/// <summary>
		/// TDMapHandoff class constructor.
		/// </summary>
		public TDMapHandoff() 
		{
		}

		/// <summary>
		/// Iterates through outward and return public transport journeys 
		/// within a TDJourneyResult and stores them on the ESRI database 
		/// for subsequent map display. 
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the signature for an ITDJourneyResult containing public transport routes.
		/// </summary>
		/// <param name="result">The returned journey results</param>
		/// <param name="sessionID">Session-id, used to key journeys on the database</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		public bool SaveJourneyResult(ITDJourneyResult result, string sessionID)
		{
			JourneySummaryLine[] jslOut = result.OutwardJourneySummary(true);
			JourneySummaryLine[] jslRet = result.ReturnJourneySummary(true);

			ArrayList al = new ArrayList();

			foreach (JourneySummaryLine line in jslOut )
			{
				if	(line.Type == TDJourneyType.PublicOriginal) 
				{
					al.Add( result.OutwardPublicJourney(line.JourneyIndex) );
				}
			}

			foreach (JourneySummaryLine line in jslRet )
			{
				if	(line.Type == TDJourneyType.PublicOriginal) 
				{
					al.Add( result.ReturnPublicJourney(line.JourneyIndex));
				}
			}

			TransportDirect.UserPortal.JourneyControl.PublicJourney[] pj = new TransportDirect.UserPortal.JourneyControl.PublicJourney[ al.Count ];
			al.CopyTo( pj );

			return this.SaveJourneyResult(sessionID, pj );
		}

        /// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the signature for public transport routes.
		/// </summary>
		/// <param name="sessionID">The users session ID.</param>
		/// <param name="publicJourney">A single public journey object</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		public bool SaveJourneyResult( string sessionID, PublicJourney publicJourney )
		{
			return this.SaveJourneyResult( sessionID, new PublicJourney[] { publicJourney } );
		}
		
		public bool SaveJourneyResult( string sessionID, PublicJourney[] publicJourneys )
		{
			DateTime dt = DateTime.Now;
			if( TDTraceSwitch.TraceVerbose )
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- Starting timing input"));
			}

			bool saveResult = true;

			Hashtable publicTransportSqlParameters = new Hashtable();
			Hashtable publicTransportSqlTypes = new Hashtable();
			publicTransportSqlTypes.Add( "@SESSIONID", SqlDbType.VarChar );
			publicTransportSqlTypes.Add( "@XML", SqlDbType.Text );

			// Initially set to a bigger size than 16 to avoid copying too much
			StringBuilder sb = new StringBuilder(255);

			sb.Append( ROOT_START );
			foreach( PublicJourney pj in publicJourneys )
			{
				AppendPublicJourney( sb, pj );
			}
			sb.Append( ROOT_END );
			publicTransportSqlParameters.Add( "@SESSIONID", sessionID );
			publicTransportSqlParameters.Add( "@XML", sb.ToString() );
			SavePublicJourney(publicTransportSqlParameters,publicTransportSqlTypes );

		
			if( TDTraceSwitch.TraceVerbose )
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- End time "+ ( DateTime.Now - dt ).ToString() ));
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- XML \n"+sb.ToString() +"\n-- /XML\n") );
			}
			return saveResult;
		}

		#region XML constant formatters
		private const string ROOT_START = "<ROOT>";
		private const string ROOT_END = "</ROOT>";

		private const string JOURNEY_START = "<journey routenum=\"{0}\">";
		private const string JOURNEY_END = "</journey>";

		private const string LEG_START = "<leg no=\"{0}\" mode=\"{1}\">";
		private const string LEG_END = "</leg>";

		private const string GEOMETRY = "<geometry order=\"{0}\" easting=\"{1}\" northing=\"{2}\" />";

		private const string TOID = "<toid id=\"{0}\" congestion=\"{1}\" />";

		private const string GREYEDOUT = "GREYEDOUT";

		#endregion 

		/// <summary>
		/// Appends a public journey to an existing string builder.
		/// </summary>
		/// <param name="xml">The stringbuilder where data should be appended</param>
		/// <param name="pj">The journey in question</param>
		public void AppendPublicJourney(StringBuilder xml, PublicJourney pj )
		{
			// Format : 
			// <journey routenum="">
			// <leg no="" mode="">
			// <geometry order="" easting="" northing="" />
			// </leg>
			// </journey>

			//Append Journey Start Node and Route Number info to XML
			xml.Append( string.Format( JOURNEY_START, pj.RouteNum ) );
			
			//Initialise greyedOutSection flag
			bool greyedOutSection = false;

			//Initialise Leg Number
			int legnumber = 1;

			//IR 2662 - Handle differing and invalid leg start or leg end co-ordinates
			for (int i = 0; i < pj.Details.Length; i++)
			{
				if	(i > 0 && !(pj.Details[i].LegStart.Location.GridReference.IsValid))
				{
					if	(pj.Details[i - 1].LegEnd.Location.GridReference.IsValid)
					{
						pj.Details[i].LegStart.Location.GridReference = pj.Details[i - 1].LegEnd.Location.GridReference;
					}
				}
	
				if	(i < (pj.Details.Length - 1) && !(pj.Details[i].LegEnd.Location.GridReference.IsValid))
				{
					if	(pj.Details[i + 1].LegStart.Location.GridReference.IsValid)
					{
						pj.Details[i].LegEnd.Location.GridReference = pj.Details[i + 1].LegStart.Location.GridReference;
					}	
				}
			}

			foreach( PublicJourneyDetail pjd in pj.Details )
			{
				if (pjd.HasInvalidCoordinates)
				{
					if (!greyedOutSection)
					{
						// The leg is invalid, so set the greyedOutSection flag to true
						greyedOutSection = true;

						//if leg start grid ref is inval, use last valid osgr

						// Append a new Leg Start in greyedOut mode to xml
						xml.Append( string.Format(LEG_START, legnumber, GREYEDOUT));
						// Append new geometry node with grid ref of startLocation to xml
						xml.Append(string.Format(GEOMETRY, 1, pjd.LegStart.Location.GridReference.Easting, pjd.LegStart.Location.GridReference.Northing));

					}
				}
				else //This IS in the greyedOutSection
				{
					if (greyedOutSection)
					{
						// Explicitly set this flag to false
						greyedOutSection = false;

						// Append new geometry node with grid ref of startLocation to xml
						xml.Append(string.Format(GEOMETRY, 2, pjd.LegStart.Location.GridReference.Easting, pjd.LegStart.Location.GridReference.Northing));

						// Append a new Leg End node to xml
						xml.Append( LEG_END );
						// Increment legNumber
						legnumber++;
					}
					// Append a new leg start node with relevant transport to xml.
					//If the mode is rail replacement then insert rail as we can not currently
					//display this mode type on the map.  This should be configurable in a 
					//later release of the ESRI mapping component
					xml.Append( string.Format( LEG_START, legnumber, pjd.Mode == ModeType.RailReplacementBus?ModeType.Rail.ToString():pjd.Mode.ToString() ) );
				
					int pointOrder = 1;

                    if (pjd is PublicJourneyInterchangeDetail && pjd.Mode == ModeType.Walk)
                    {
                        // PublicJourneyInterchangeDetail can have a navigation path which provides a more 
                        // accurate geometry for walking in the leg, e.g. from bus stop -> entrance -> platform.
                        // However this could result in a poor line display on the map, so show 
                        // direct line from start location to end of the leg
                        if (pjd.Geometry.Length > 0)
                        {
                            OSGridReference osgr = pjd.Geometry[0];
                            xml.Append(string.Format(GEOMETRY, pointOrder++, osgr.Easting, osgr.Northing));
                            osgr = pjd.Geometry[pjd.Geometry.Length - 1];
                            xml.Append(string.Format(GEOMETRY, pointOrder++, osgr.Easting, osgr.Northing));
                        }
                    }
                    else
                    {
                        foreach (OSGridReference osg in pjd.Geometry)
                        {
                            // Append a new geometry node using current grid ref and pointOrder from 1 to number of grid refs in geometry
                            xml.Append(string.Format(GEOMETRY, pointOrder++, osg.Easting, osg.Northing));
                        }
                    }

					// Increment leg number
					legnumber++;

					// Append new leg end node to xml
					xml.Append( LEG_END );
				}


			
			}
			//Covered by TEST CASE 1
			if (greyedOutSection)
			{
				// Append new geometry node with gridRef of EndLocation to xml
				OSGridReference endPoint = pj.Details[ pj.Details.Length - 1 ].LegEnd.Location.GridReference;
				xml.Append(string.Format(GEOMETRY, 2, endPoint.Easting, endPoint.Northing));
				// Append a new Leg End node to xml
				xml.Append( LEG_END );
			}
		
			// Append new Journey End node to xml
			xml.Append( JOURNEY_END );
		}

        /// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the signiture for road routes setting one congestion value for all the routes
		/// </summary>
		/// <param name="congestion">If the journey contains congestion or not</param>
		/// <param name="sessionID">The users session ID.</param>
		/// <param name="routeNum">A unique ID within the users session.</param>
		/// <param name="itnLinks">An array of TOIDS.</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
        public bool SaveJourneyResult(bool congestion, string sessionID, int routeNum, ITNLink[] itnLinks, int congestionValue)
        {
            this.congestionValue = congestionValue;

            return SaveJourneyResult(congestion, sessionID, routeNum, itnLinks);
        }

		/// <summary>
		/// This method is used to persist the journeyResult to the ESRI MasterMap database.  
		/// It is overloaded to facilitate saving public transport routes and road routes.
		/// This is the signiture for road routes.
		/// </summary>
		/// <param name="congestion">If the journey contains congestion or not</param>
		/// <param name="sessionID">The users session ID.</param>
		/// <param name="routeNum">A unique ID within the users session.</param>
		/// <param name="itnLinks">An array of TOIDS.</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		public bool SaveJourneyResult( bool congestion,string sessionID, int routeNum, ITNLink[] itnLinks )
		{
			DateTime dt = DateTime.Now;
			if( TDTraceSwitch.TraceVerbose )
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- Starting timing input"));
			}
			Hashtable roadTransportSqlParameters = new Hashtable();
			
			IEnumerator roadJourneyLinks;
			ITNLink roadJourneyLink;
			
			// 47 is the length of each TOID row
			// 15 is the length of the root elements plus a few characters
			StringBuilder sb = new StringBuilder( itnLinks.Length * 56 + 15);

			sb.Append( ROOT_START );

			string toidPrefix = Properties.Current[TOID_PREFIX];

			if	(toidPrefix == null) 
			{
				toidPrefix = string.Empty;
			}

			roadJourneyLinks = itnLinks.GetEnumerator();
			
			while( roadJourneyLinks.MoveNext() )
			{
				roadJourneyLink = (ITNLink) roadJourneyLinks.Current;
				
				string toid = roadJourneyLink.TOID;
			
				if	(toidPrefix.Length > 0 && toid.StartsWith(toidPrefix))
				{
					toid = toid.Substring(toidPrefix.Length);
				}

				sb.Append( string.Format( TOID, toid, (congestion ? roadJourneyLink.congestion : congestionValue)  ) );

			}
			sb.Append( ROOT_END );

			roadTransportSqlParameters.Add( "@SESSIONID", sessionID );
			roadTransportSqlParameters.Add( "@ROUTENUM", routeNum );
			roadTransportSqlParameters.Add( "@doc", sb.ToString() );

			Hashtable roadTransportSqlTypes = new Hashtable();

			roadTransportSqlTypes.Add( "@SESSIONID", SqlDbType.VarChar );
			roadTransportSqlTypes.Add( "@ROUTENUM", SqlDbType.Int );
			roadTransportSqlTypes.Add("@doc", SqlDbType.Text);

			SaveRoadJourney( roadTransportSqlParameters, roadTransportSqlTypes );

			if( TDTraceSwitch.TraceVerbose )
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- End time "+ ( DateTime.Now - dt ).ToString() ));
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- XML \n"+sb.ToString() +"\n-- /XML\n") );
			}			
			return true;
		}

        /// <summary>
        /// This overloaded method is used to persist the Cycle Journey route to the ESRI MasterMap database.  
        /// This is the signiture for cycle routes.
        /// </summary>
        /// <param name="sessionID">The users session ID.</param>
        /// <param name="routeNum">A unique ID within the users session.</param>
        /// <param name="cycleRoute">The binary formatter memory stream of the ESRI CycleRoute object to save</param>
        /// <returns>A boolean. True if no error, false for all other conditions</returns>
        public bool SaveJourneyResult(string sessionID, int routeNum, MemoryStream cycleRoute)
        {
            DateTime dt = DateTime.Now;
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- Starting timing input"));
            }
            
            Hashtable cycleRouteSqlParameters = new Hashtable();

            cycleRouteSqlParameters.Add("@SESSIONID", sessionID);
            cycleRouteSqlParameters.Add("@ROUTENUM", routeNum);
            cycleRouteSqlParameters.Add("@CYCLEROUTE", cycleRoute.GetBuffer());
            cycleRouteSqlParameters.Add("@DATALEN", cycleRoute.Length);

            Hashtable cycleRouteSqlTypes = new Hashtable();

            cycleRouteSqlTypes.Add("@SESSIONID", SqlDbType.VarChar);
            cycleRouteSqlTypes.Add("@ROUTENUM", SqlDbType.Int);
            cycleRouteSqlTypes.Add("@CYCLEROUTE", SqlDbType.Image);
            cycleRouteSqlTypes.Add("@DATALEN", SqlDbType.Int);

            SaveCycleJourney(cycleRouteSqlParameters, cycleRouteSqlTypes);

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "-- End time " + (DateTime.Now - dt).ToString()));
            }
            return true;
        }

        #region Private methods

        /// <summary>
		/// This method persist a single public journey to the ESRI MasterMap database.  
		/// </summary>
		/// <param name="publicTransportSqlParameters">A hash table of parameters for the stored proc.</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		private bool SavePublicJourney( Hashtable publicTransportSqlParameters, Hashtable publicTransportSqlTypes )
		{
			bool savePublicJourneyResult = true;

			try
			{
				// Open our connection to the ESRI Database
				TDMapHandoffSQL.ConnOpen( SqlHelperDatabase.EsriDB );
				int sqlResult = TDMapHandoffSQL.Execute( "usp_Save_To_PT_Table_Arr", publicTransportSqlParameters , publicTransportSqlTypes); 
			}
			
			catch( SqlException eSQL )
			{
				savePublicJourneyResult = false;
				
				// TDEventLogging
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
															TDTraceLevel.Error,
															string.Format( "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message ));
					 
				Logger.Write(oe);

			}
			catch (Exception e)
			{
				savePublicJourneyResult = false;

				OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error,
					string.Format("Exception saving public journey result - " + e.Message));
					 
				Logger.Write(oe);
			}

			finally
			{
				TDMapHandoffSQL.ConnClose();
			}
			
			return savePublicJourneyResult;
		}

		/// <summary>
		/// This method persist a number of TOIDs for a single road journey to the ESRI MasterMap database.  
		/// </summary>
		/// <param name="roadTransportSqlParameters">A hash table of parameters for the stored proc.</param>
		/// <returns>A boolean. True if no error, false for all other conditions</returns>
		private bool SaveRoadJourney( Hashtable roadTransportSqlParameters, Hashtable roadTransportSqlTypes )
		{
			bool saveRoadJourneyResult = true;

			try
			{
				// Open our connection to the ESRI Database
				TDMapHandoffSQL.ConnOpen( SqlHelperDatabase.EsriDB );
				int sqlResult = TDMapHandoffSQL.Execute( "usp_Save_To_RD_Table_Arr", roadTransportSqlParameters, roadTransportSqlTypes ); 
			}
			
			catch( SqlException eSQL )
			{
				saveRoadJourneyResult = false;
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error,
					string.Format( "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message ));
					 
				Logger.Write(oe);

			}
			catch (Exception e)
			{
				saveRoadJourneyResult = false;

				OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error,
					string.Format("Exception saving road journey result - " + e.Message));
					 
				Logger.Write(oe);
			}
			finally
			{
				TDMapHandoffSQL.ConnClose();
			}
			
			return saveRoadJourneyResult;
		}

        /// <summary>
        /// This method persist a cycle route xml for a single cycle journey to the ESRI MasterMap database.  
        /// </summary>
        /// <param name="roadTransportSqlParameters">A hash table of parameters for the stored proc.</param>
        /// <returns>A boolean. True if no error, false for all other conditions</returns>
        private bool SaveCycleJourney(Hashtable cycleRouteSqlParameters, Hashtable cycleRouteSqlTypes)
        {
            bool saveCycleJourneyResult = true;

            try
            {
                // Open our connection to the ESRI Database
                TDMapHandoffSQL.ConnOpen(SqlHelperDatabase.EsriDB);
                int sqlResult = TDMapHandoffSQL.Execute("usp_InsertCycleRoute", cycleRouteSqlParameters, cycleRouteSqlTypes);
            }

            catch (SqlException eSQL)
            {
                saveCycleJourneyResult = false;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                    TDTraceLevel.Error,
                    string.Format("SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message));

                Logger.Write(oe);

            }
            catch (Exception e)
            {
                saveCycleJourneyResult = false;

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                    TDTraceLevel.Error,
                    string.Format("Exception saving cycle journey xml - " + e.Message));

                Logger.Write(oe);
            }
            finally
            {
                TDMapHandoffSQL.ConnClose();
            }

            return saveCycleJourneyResult;
        }

        #endregion
    }
}
