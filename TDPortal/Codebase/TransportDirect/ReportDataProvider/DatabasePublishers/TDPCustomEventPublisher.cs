// *********************************************** 
// NAME                 : OperationalEventPublisher.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : A publisher that publishes OperationalEvents to the staging database
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/TDPCustomEventPublisher.cs-arc  $ 
//
//   Rev 1.16   Jan 22 2013 16:48:46   DLane
//Accessible events
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.15   Jan 20 2013 16:25:52   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.14   Jan 14 2013 14:41:56   mmodi
//Added GISQueryEvent
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.13   Sep 23 2011 10:50:52   mmodi
//Corrected mistakes in RealTimeRoadEvent publishing
//Resolution for 5742: Real Time In Car - MIS event publishing for RealTimeRoadEvent fails
//
//   Rev 1.12   Sep 01 2011 10:43:40   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.11   Jul 27 2010 10:19:42   PScott
//IR 5561 - add machine name to columns published into reference transacation event table
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.10   Feb 18 2010 15:50:34   mmodi
//Added International Planner events
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Jan 13 2010 11:28:44   PScott
//default useragent for repeatvisitor event to prevent exceptions
//Resolution for 5366: Database Publisher to solve ER failures
//
//   Rev 1.8   Nov 12 2009 13:53:08   apatel
//Updated to log a Map API event
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Oct 06 2009 14:04:28   mmodi
//Updated to log an EBC event
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Jan 19 2009 11:16:24   mmodi
//Log a Gradient profile event
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.5   Oct 13 2008 16:46:18   build
//Automatically merged from branch for stream5014
//
//   Rev 1.4.1.0   Aug 22 2008 10:13:48   mmodi
//Updated for Cycle planner events
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   May 14 2008 15:52:44   mmodi
//Limit UserAgent string to 200 chars
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.3   May 14 2008 15:39:14   mmodi
//Updated to log repeat visitor event
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.2   Apr 29 2008 16:37:54   mturner
//Fixes to allow the web log reader to process host names from IIS logs.  This is to resolve IR4904/USD2517876
//
//   Rev 1.1   Mar 25 2008 09:45:06   pscott
//IR 4621 CCN 427 - White Label Changes
//Changes to  add theme Id to page entry events staging database
//
//   Rev 1.0   Nov 08 2007 12:38:16   mturner
//Initial revision.
//
//   Rev 1.41   Feb 23 2006 11:56:46   RWilby
//Merged stream3129
//
//   Rev 1.40   Feb 20 2006 15:42:14   build
//Merged for stream 0017
//
//   Rev 1.39.2.0   Jan 30 2006 11:09:30   tolomolaiye
//Changes for RTTI Internal Event
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.39   Oct 05 2005 14:20:52   schand
//Merged changes for stream 2610 LandingPageEntryEvent
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.38   Aug 23 2005 14:24:42   mdambrine
//Undid rev 1.37 and 1.36 changes as they did not belong in the trunk
//
//   Rev 1.37   Aug 17 2005 16:11:34   mdambrine
//Added the partner ID to pageentryevent
//
//   Rev 1.36   Aug 17 2005 11:35:36   mdambrine
//Added Partner identification to the page entry event logger for GNER phase 2
//
//   Rev 1.35   Mar 08 2005 16:48:08   schand
//Changed elseif block from PageEventEntry to BasePageEventEntry
//
//   Rev 1.34   Feb 07 2005 14:37:54   passuied
//added code + storedproc + tables for ExposedServices Event
//
//   Rev 1.33   Feb 03 2005 11:22:42   passuied
//used RequestType string value (not int) + updated test
//
//   Rev 1.32   Feb 02 2005 18:51:06   passuied
//changes for StopEventRequestEvent
//
//   Rev 1.31   Jan 25 2005 18:46:34   schand
//Renamed RTTIEventCounters to RTTIEvent
//
//   Rev 1.30   Jan 25 2005 13:33:36   schand
//Changed to RTTIEvent from RTTICounterEvent
//
//   Rev 1.29   Jan 24 2005 18:14:42   schand
//Added elseif condition for RTTIEventCounter
//
//   Rev 1.28   Aug 16 2004 10:16:56   jgeorge
//Fix for vantive 3389081
//
//   Rev 1.27   Jul 20 2004 15:39:04   jmorrissey
//Added check in WriteEvent that handles UserFeedbackEvent
//
//   Rev 1.26   Jul 15 2004 18:04:42   acaunt
//Modified so that GazetteerEvent is now passed submitted time.
//
//   Rev 1.25   Jun 28 2004 16:36:30   passuied
//fix for eventreceiver
//
//   Rev 1.24   Apr 19 2004 20:21:08   geaton
//IR785. Updated to publish new property of WorkloadEvent.
//
//   Rev 1.23   Dec 02 2003 20:05:46   geaton
//Updates following addition of Successful column on ReferenceTransactionEvent table.
//
//   Rev 1.22   Nov 27 2003 19:27:48   geaton
//Removed functionality to selectively publish verbose journey data. (In the short-term) the Trace on/off switch will be used in the short-term to control logging of verbose journey data.
//
//   Rev 1.21   Nov 14 2003 10:43:46   geaton
//Removed redundant logging that is already being logged by the TD Trace Listener.
//
//   Rev 1.20   Nov 14 2003 09:08:28   geaton
//Store target database as a member variable.
//
//   Rev 1.19   Nov 13 2003 21:59:54   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.18   Nov 10 2003 12:32:16   geaton
//Change following removal of TDTransactionCategory
//
//   Rev 1.17   Oct 23 2003 08:54:12   pscott
//date time / string mismatch error corrected for workload event
//
//   Rev 1.16   Oct 10 2003 15:25:14   geaton
//Updated constructors to take target database property key. This enables validation at construction time.
//
//   Rev 1.15   Oct 07 2003 15:56:22   PScott
//publishers now called with id argument
//
//   Rev 1.14   Oct 06 2003 15:18:04   geaton
//Changed TDException references to use TDExceptionIdentifier identifier.
//
//   Rev 1.13   Oct 01 2003 15:45:48   PScott
//added UserPreferenceSaveEvent
//
//   Rev 1.12   Oct 01 2003 11:45:10   PScott
//Added Datagateway event
//
//   Rev 1.11   Sep 26 2003 11:53:24   geaton
//Serialise verbose event data to XML rather than use file formatter of event.
//
//   Rev 1.10   Sep 25 2003 12:00:34   ALole
//Updated Publishers to include code review comments.
//Added JourneyPlanRequestVerboseEvent, JourneyPlanResultsVerboseEvent and uncommented JourneyPlanResultsEvent.
//Added Properties service to TDPCUstomEventPublisher for VerboseEvent support.
//
//   Rev 1.9   Sep 24 2003 11:03:50   ALole
//Updated error codes from 2965 to 2695
//
//   Rev 1.8   Sep 16 2003 16:36:30   ALole
//Updated DatabasePublishers to use the new WorkloadEvent (minus URIStem and BytesSent).
//
//   Rev 1.7   Sep 15 2003 16:54:36   geaton
//ReferenceTransactionEvent takes DateTime instead of TDDateTime.
//
//   Rev 1.6   Sep 15 2003 16:41:52   geaton
//MapEvent changed to DateTime instead of TDDateTime.
//
//   Rev 1.5   Sep 10 2003 10:47:18   geaton
//Renamed JourneyRequestId to JourneyPlanRequestId so consistent with naming of JourneyWebRequestId.
//
//   Rev 1.4   Sep 09 2003 14:36:38   RPhilpott
//Add usng for JourneyControl namespace
//
//   Rev 1.3   Sep 08 2003 16:36:38   ALole
//Commented out JourneyPlanResultsEvent code aas this is not currently needed.
//Changed JourneyPlanRequestsEvent to use the new version of the event to include specific infor in the db.
//
//   Rev 1.2   Sep 05 2003 11:22:18   ALole
//Added TimeLogged field to the db table
//
//   Rev 1.1   Sep 05 2003 10:15:12   ALole
//Updated to Reflect changes to MapOverlayEvent and MapEvent
//
//   Rev 1.0   Sep 04 2003 17:03:12   ALole
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.EnvironmentalBenefits;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.EnhancedExposedServices.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.InternationalPlannerControl;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Summary description for OpertationalEventPublisher.
	/// </summary>
	public class TDPCustomEventPublisher : IEventPublisher
	{
		private string identifier;
		private SqlHelperDatabase targetDatabase;

		private string ConvertToXML(object obj)
		{
			XmlSerializer xmls = new XmlSerializer(obj.GetType());
			StringWriter sw = new StringWriter();
			xmls.Serialize(sw, obj);	
			sw.Close();
			return sw.ToString();
		}

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="identifier">Identifier for publisher. Must match identifier defined in logging service property key.</param>
		/// <param name="targetDatabase">Identifier of target database to which events are published.</param>
		public TDPCustomEventPublisher(string identifier, SqlHelperDatabase targetDatabase)
		{
			this.identifier = identifier;
			this.targetDatabase = targetDatabase;
			
			DatabasePublisherPropertyValidator validator = new DatabasePublisherPropertyValidator(Properties.Current);
			ArrayList errors = new ArrayList();
			validator.ValidateProperty(targetDatabase.ToString(), errors);
			validator.ValidateProperty(String.Format(TransportDirect.Common.Logging.Keys.CustomPublisherName, identifier), errors);

			if (errors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);

				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				throw new TDException(String.Format(Messages.ConstructorFailed, message.ToString()), false, TDExceptionIdentifier.RDPTDPCustomEventPublisherConstruction);
			}
		}

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}

		/// <summary>
		/// Executes the requested Stored Procedure with the parameters supplied.
		/// </summary>
		/// <param name="storedProcName">string containing the Stored Procedure Name</param>
		/// <param name="parameters">Hashtable containg the Stored Proc parameters</param>
		/// <exception cref="TDException">Thrown when the stored Procedure return code is incorrect</exception>
		/// <exception cref="TDException">Thrown when a SqlException is caught</exception>
		private void WriteToDB( string storedProcName, Hashtable parameters )
		{
			SqlHelper sqlHelper = new SqlHelper();

			try
			{
				sqlHelper.ConnOpen(this.targetDatabase);
				
				int returnCode = sqlHelper.Execute( storedProcName, parameters );

				if ( returnCode != 1 )
				{
					throw new TDException(String.Format(Messages.SQLHelperInsertFailed, 1, storedProcName, returnCode),
										  false,
										  TDExceptionIdentifier.RDPSQLHelperInsertFailed); 
				}

			}
			catch(SqlException sqlEx)
			{
				// SQLHelper does not catch SqlException so catch here.
				throw new TDException(String.Format(Messages.SQLHelperError, storedProcName, sqlEx.Message), sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
			}
			catch (SqlTypeException ste)
			{
				throw new TDException(String.Format(Messages.SQLHelperTypeError, storedProcName, ste.Message), ste, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
			}
			finally
			{
				sqlHelper.ConnClose();
			}
		}

		/// <summary>
		/// Publisher for Events that inherit from TDCustomEvent
		/// </summary>
		/// <param name="logEvent">Event that inherits from TDPCustomEvent</param>
		/// <exception cref="TDException">Thrown when the call to WriteToDB method fails</exception>
		/// <exception cref="TDException">Thrown if the Event passed in is not a TDPCustomEvent</exception>
		public void WriteEvent(LogEvent logEvent)
		{
			
			Hashtable parameters = null;

			if(logEvent is WorkloadEvent)
            {
                #region WorkloadEvent
                WorkloadEvent workloadEvent = (WorkloadEvent)logEvent;
				parameters = new Hashtable();

				parameters.Add( "@Requested", workloadEvent.Requested );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( workloadEvent.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddWorkloadEvent", workloadEvent.FileFormatter.AsString(workloadEvent))));
					return;

				}
				parameters.Add( "@TimeLogged", workloadEvent.Time );
				parameters.Add( "@NumberRequested", workloadEvent.NumberRequested );
                parameters.Add( "@PartnerId", workloadEvent.PartnerId);

				try
				{
					WriteToDB( "AddWorkloadEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "WorkloadEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingWorkloadEvent);
                }
                #endregion
            }
			else if( logEvent is RetailerHandoffEvent )
            {
                #region RetailerHandoffEvent
                RetailerHandoffEvent ce = (RetailerHandoffEvent)logEvent;
				parameters = new Hashtable();

				parameters.Add( "@RetailerId", ce.RetailerId );
				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddRetailerHandoffEvent", ce.FileFormatter.AsString(ce))));
					return;

				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddRetailerHandoffEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "RetailerHandoffEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingRetailerHandoffEvent);
                }
                #endregion
            }
			else if( logEvent is ReferenceTransactionEvent )
            {
                #region ReferenceTransactionEvent
                ReferenceTransactionEvent ce = (ReferenceTransactionEvent)logEvent;
				parameters = new Hashtable();

				parameters.Add( "@EventType", ce.EventType );
				parameters.Add( "@ServiceLevelAgreement", ce.ServiceLevelAgreement );
				
				
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Submitted))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddReferenceTransactionEvent", ce.FileFormatter.AsString(ce))));
					return;
				}
				parameters.Add( "@Submitted", ce.Submitted );

				parameters.Add( "@SessionId", ce.SessionId );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddReferenceTransactionEvent", ce.FileFormatter.AsString(ce))));
					return;
				}
				parameters.Add( "@TimeLogged", ce.Time );
				parameters.Add( "@Successful", ce.Successful );
                parameters.Add("@MachineName", ce.MachineName);
                
				try
				{
					WriteToDB( "AddReferenceTransactionEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "ReferenceTransactionEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingReferenceTransactionEvent);
                }
                #endregion
            }
			else if( logEvent is BasePageEntryEvent)
            {
                #region BasePageEntryEvent
                string pageName = string.Empty ;
				string sessionId = string.Empty ;
				bool userLoggedOn ;
				DateTime datetime ;
                int themeId;
				BasePageEntryEvent entryEvent = ((BasePageEntryEvent)logEvent);								
				pageName = entryEvent.PageName.ToString();
                
                if (entryEvent.PartnerName != string.Empty)
                    pageName = pageName + "_" + entryEvent.PartnerName;

				sessionId = entryEvent.SessionId.ToString();
				userLoggedOn = entryEvent.UserLoggedOn ;
				datetime = entryEvent.Time;
                themeId = entryEvent.ThemeId;


				parameters = new Hashtable();

				parameters.Add( "@Page", pageName );
				parameters.Add( "@SessionId", sessionId );
				parameters.Add( "@UserLoggedOn", userLoggedOn );

				if(!SqlTypeHelper.IsSqlDateTimeCompatible( datetime))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddPageEntryEvent", entryEvent.FileFormatter.AsString(entryEvent))));
					return;

				}
				parameters.Add( "@TimeLogged", datetime );
                
                parameters.Add("@ThemeID", themeId); 
				try
				{
					WriteToDB( "AddPageEntryEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "PageEntryEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingPageEntryEvent);
                }
                #endregion
            }
            else if (logEvent is RepeatVisitorEvent)
            {
                #region RepeatVisitorEvent
                string repeatVisitorType = string.Empty;
                string sessionIdOld = string.Empty;
                string sessionIdNew = string.Empty;
                string domain = string.Empty;
                string userAgent = string.Empty;
                int themeId = 0;
                DateTime lastVisitedDateTime = DateTime.MinValue;
                DateTime datetime;
                
                RepeatVisitorEvent repeatVisitorEvent = ((RepeatVisitorEvent)logEvent);
                
                repeatVisitorType = repeatVisitorEvent.RepeatVisitorType.ToString();
                sessionIdOld = repeatVisitorEvent.SessionIdOld;
                sessionIdNew = repeatVisitorEvent.SessionId;
                domain = repeatVisitorEvent.Domain;
                lastVisitedDateTime = repeatVisitorEvent.LastVisitedDateTime;
                datetime = repeatVisitorEvent.Time;
                themeId = repeatVisitorEvent.ThemeId;

                // limit user agent to first 200 chars or default to "AgentNotSupplied" if it doesnt exist
                if (string.IsNullOrEmpty(repeatVisitorEvent.UserAgent))
                {
                    userAgent = "AgentNotSupplied";
                }
                else
                {   
                    userAgent = (repeatVisitorEvent.UserAgent.Length > 200) ? repeatVisitorEvent.UserAgent.Substring(0, 200) : repeatVisitorEvent.UserAgent;
                }
                
                parameters = new Hashtable();

                parameters.Add("@RepeatVistorType", repeatVisitorType);
                parameters.Add("@SessionIdOld", sessionIdOld );
                parameters.Add("@SessionIdNew", sessionIdNew );
                parameters.Add("@DomainName", domain );
                parameters.Add("@UserAgent", userAgent );
                parameters.Add("@ThemeId", themeId);                

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(datetime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddRepeatVisitorEvent", repeatVisitorEvent.FileFormatter.AsString(repeatVisitorEvent))));
                    return;
                }

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(lastVisitedDateTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddRepeatVisitorEvent", repeatVisitorEvent.FileFormatter.AsString(repeatVisitorEvent))));
                    return;
                }

                parameters.Add("@TimeLogged", datetime);
                parameters.Add("@LastVisited", lastVisitedDateTime);

                try
                {
                    WriteToDB("AddRepeatVisitorEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "RepeatVisitorEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingRepeatVisitorEvent);
                }
                #endregion
            }
			else if( logEvent is MapEvent )
            {
                #region MapEvent
                MapEvent ce = (MapEvent)logEvent;
				parameters = new Hashtable();

				parameters.Add( "@CommandCategory", ce.CommandCategory.ToString() );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Submitted))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddMapEvent", ce.FileFormatter.AsString(ce))));
					return;
				}
				parameters.Add( "@Submitted", ce.Submitted );
				parameters.Add( "@DisplayCategory", ce.DisplayCategory.ToString() );
				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddMapEvent", ce.FileFormatter.AsString(ce))));
					return;

				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddMapEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "MapEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingMapEvent);
                }
                #endregion
            }
            else if (logEvent is MapAPIEvent)
            {
                #region MapEvent
                MapAPIEvent ce = (MapAPIEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@CommandCategory", ce.CommandCategory.ToString());
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddMapAPIEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add("@Submitted", ce.Submitted);
                parameters.Add("@SessionId", ce.SessionId);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddMapAPIEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddMapAPIEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "MapAPIEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingMapAPIEvent);
                }
                #endregion
            }
			else if( logEvent is LoginEvent )
            {
                #region LoginEvent
                LoginEvent ce = (LoginEvent)logEvent;
				parameters = new Hashtable();

				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddLoginEvent", ce.FileFormatter.AsString(ce))));
					return;
					
				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddLoginEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "LoginEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingLoginEvent);
                }
                #endregion
            }
			else if( logEvent is JourneyPlanRequestVerboseEvent )
            {
                #region JourneyPlanRequestVerboseEvent
                JourneyPlanRequestVerboseEvent ce = (JourneyPlanRequestVerboseEvent)logEvent;
					
				parameters = new Hashtable();

				parameters.Add( "@JourneyPlanRequestId", ce.JourneyPlanRequestId );
				parameters.Add( "@JourneyRequestData", ConvertToXML(ce.JourneyRequestData) );
				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddJourneyPlanRequestVerboseEvent", ce.FileFormatter.AsString(ce))));
					return;

				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddJourneyPlanRequestVerboseEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "JourneyPlanRequestVerboseEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingJourneyPlanRequestVerboseEvent);
                }
                #endregion
            }
			else if( logEvent is JourneyPlanResultsVerboseEvent )
            {
                #region JourneyPlanResultsVerboseEvent
                JourneyPlanResultsVerboseEvent ce = (JourneyPlanResultsVerboseEvent)logEvent;
					
				parameters = new Hashtable();

				parameters.Add( "@JourneyPlanRequestId", ce.JourneyPlanRequestId );
				parameters.Add( "@JourneyResultsData", ConvertToXML(ce.JourneyResultsData) );
				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddJourneyPlanResultsVerboseEvent", ce.FileFormatter.AsString(ce))));
					return;

				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddJourneyPlanResultsVerboseEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "JourneyPlanResultsVerboseEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingJourneyPlanResultsVerboseEvent);
                }
                #endregion
            }
			else if( logEvent is JourneyPlanResultsEvent )
            {
                #region JourneyPlanResultsEvent
                JourneyPlanResultsEvent ce = (JourneyPlanResultsEvent)logEvent;
				parameters = new Hashtable();

				parameters.Add( "@JourneyPlanRequestId", ce.JourneyPlanRequestId );
				parameters.Add( "@ResponseCategory", ce.ResponseCategory.ToString() );
				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddJourneyPlanResultsEvent", ce.FileFormatter.AsString(ce))));
					return;

				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddJourneyPlanResultsEvent", parameters );
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "JourneyPlanResultsEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingJourneyPlanResultsEvent);
                }
                #endregion
            }
			else if( logEvent is JourneyPlanRequestEvent )
            {
                #region JourneyPlanRequestEvent
                JourneyPlanRequestEvent ce = (JourneyPlanRequestEvent)logEvent;
				parameters = new Hashtable();

				bool air = false;
				bool bus = false;
				bool car = false;
				bool coach = false;
				bool cycle = false;
				bool drt = false;
				bool ferry = false;
				bool metro = false;
				bool rail = false;
				bool taxi = false;
                bool telecabine = false;
                bool tram = false;
				bool underground = false;
				bool walk = false;
				
				for ( int i = 0; i < ce.Modes.Length; i++ )
				{
					if ( ce.Modes[i].Equals( ModeType.Air ) )
					{
						air = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Bus ) )
					{	
						bus = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Car ) )
					{
						car = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Coach ) )
					{
						coach = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Cycle ) )
					{
						cycle = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Drt ) )
					{
						drt = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Ferry ) )
					{
						ferry = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Metro ) )
					{
						metro = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Rail ) )
					{
						rail = true;
					}
					else if ( ce.Modes[i].Equals( ModeType.Taxi ) )
					{
						taxi = true;
					}
                    else if (ce.Modes[i].Equals(ModeType.Telecabine))
                    {
                        telecabine = true;
                    }
                    else if (ce.Modes[i].Equals(ModeType.Tram))
                    {
                        tram = true;
                    }
                    else if (ce.Modes[i].Equals(ModeType.Underground))
                    {
                        underground = true;
                    }
                    else if (ce.Modes[i].Equals(ModeType.Walk))
                    {
                        walk = true;
                    }
				}

				parameters.Add( "@JourneyPlanRequestId", ce.JourneyPlanRequestId );
				parameters.Add( "@Air", air );
				parameters.Add( "@Bus", bus );
				parameters.Add( "@Car", car );
				parameters.Add( "@Coach", coach );
				parameters.Add( "@Cycle", cycle );
				parameters.Add( "@Drt", drt );
				parameters.Add( "@Ferry", ferry );
				parameters.Add( "@Metro", metro );
				parameters.Add( "@Rail", rail );
				parameters.Add( "@Taxi", taxi );
                parameters.Add( "@Telecabine", telecabine);
				parameters.Add( "@Tram", tram );
				parameters.Add( "@Underground", underground );
				parameters.Add( "@Walk", walk );
				parameters.Add( "@SessionId", ce.SessionId );
				parameters.Add( "@UserLoggedOn", ce.UserLoggedOn );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( ce.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddJourneyPlanRequestEvent", ce.FileFormatter.AsString(ce))));
					return;

				}
				parameters.Add( "@TimeLogged", ce.Time );

				try
				{
					WriteToDB( "AddJourneyPlanRequestEvent", parameters );
				}
				catch( TDException tdEx )
				{				
					throw new TDException(String.Format(Messages.EventPublishFailure, "JourneyPlanRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingJourneyPlanRequestEvent);
                }
                #endregion
            }
            else if (logEvent is CyclePlannerRequestEvent)
            {
                #region CyclePlannerRequestEvent
                CyclePlannerRequestEvent ce = (CyclePlannerRequestEvent)logEvent;
                parameters = new Hashtable();

                bool cycle = true;

                parameters.Add("@CyclePlannerRequestId", ce.CyclePlannerRequestId);
                parameters.Add("@Cycle", cycle);
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddCyclePlannerRequestEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddCyclePlannerRequestEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "CyclePlannerRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingCyclePlannerRequestEvent);
                }
                #endregion
            }
            else if (logEvent is CyclePlannerResultEvent)
            {
                #region CyclePlannerResultEvent
                CyclePlannerResultEvent ce = (CyclePlannerResultEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@CyclePlannerRequestId", ce.CyclePlannerRequestId);
                parameters.Add("@ResponseCategory", ce.ResponseCategory.ToString());
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddCyclePlannerResultEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddCyclePlannerResultEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "CyclePlannerResultEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingCyclePlannerResultEvent);
                }
                #endregion
            }
            else if (logEvent is GradientProfileEvent)
            {
                #region GradientProfileEvent
                GradientProfileEvent ce = (GradientProfileEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@DisplayCategory", ce.DisplayCategory.ToString());
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddGradientProfileEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add("@Submitted", ce.Submitted);
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddGradientProfileEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddGradientProfileEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "GradientProfileEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingGradientProfileEvent);
                }
                #endregion
            }
            else if (logEvent is GazetteerEvent)
            {
                #region GazetteerEvent
                GazetteerEvent ce = (GazetteerEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@EventCategory", ce.EventCategory.ToString());
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddGazetteerEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddGazetteerEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@Submitted", ce.Submitted);
                try
                {
                    WriteToDB("AddGazetteerEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "GazetteerEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingGazetteerEvent);
                }
                #endregion
            }
            else if (logEvent is DataGatewayEvent)
            {
                #region DataGatewayEvent
                DataGatewayEvent ce = (DataGatewayEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@FeedId", ce.FeedId);
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                parameters.Add("@FileName", ce.FileName);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.TimeStarted))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddDataGatewayEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeStarted", ce.TimeStarted);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.TimeFinished))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddDataGatewayEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeFinished", ce.TimeFinished);
                parameters.Add("@SuccessFlag", ce.SuccessFlag);
                parameters.Add("@ErrorCode", ce.ErrorCode);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddDataGatewayEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);
                try
                {
                    WriteToDB("AddDataGatewayEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "DataGatewayEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingDataGatewayEvent);
                }
                #endregion
            }
            else if (logEvent is UserPreferenceSaveEvent)
            {
                #region UserPreferenceSaveEvent
                UserPreferenceSaveEvent ce = (UserPreferenceSaveEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@EventCategory", ce.EventCategory.ToString());
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddUserPreferenceSaveEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddUserPreferenceSaveEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "UserPreferenceSaveEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingUserPreferenceSaveEvent);
                }
                #endregion
            }
            else if (logEvent is UserFeedbackEvent)
            {
                #region UserFeedbackEvent
                UserFeedbackEvent ce = (UserFeedbackEvent)logEvent;
                parameters = new Hashtable();

                //session ID
                parameters.Add("@SessionId", ce.SessionId);

                //submittedTime
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.SubmittedTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "UserFeedbackEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@SubmittedTime", ce.SubmittedTime);

                //FeedbackType
                parameters.Add("@FeedbackType", ce.FeedbackType.ToString());


                //AcknowledgedTime
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.AcknowledgedTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "UserFeedbackEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@AcknowledgedTime", ce.AcknowledgedTime);

                //AcknowledgmentSent
                parameters.Add("@AcknowledgmentSent", ce.AcknowledgmentSent);

                //UserLoggedOn
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);

                //TimeLogged
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "UserFeedbackEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddUserFeedbackEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "UserFeedbackEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingUserFeedbackEvent);
                }
                #endregion
            }
            else if (logEvent is RTTIEvent)
            {
                #region RTTIEvent
                // Added by SC on 24/01/2004 for RTTI Event request 
                // casting logEvent to RTTIEvent type
                RTTIEvent rEvent = (RTTIEvent)logEvent;
                // creating a hash table to populate sql params
                parameters = new Hashtable();

                // StartTime
                // checking it is compactible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(rEvent.StartTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "RTTIEvent", rEvent.FileFormatter.AsString(rEvent))));
                    return;
                }
                // Adding StartTime
                parameters.Add("@StartTime", rEvent.StartTime);



                // FinishTime
                // checking it is compactible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(rEvent.FinishTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "RTTIEvent", rEvent.FileFormatter.AsString(rEvent))));
                    return;
                }
                // Adding FinishTime
                parameters.Add("@FinishTime", rEvent.FinishTime);

                // Adding DataReceived
                if (rEvent.DataReceived)
                    parameters.Add("@DataReceived", 1);
                else
                    parameters.Add("@DataReceived", 0);

                // try writing to DB now 
                try
                {
                    WriteToDB("RTTIEventLog", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "RTTIEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RTTIEventFailed);
                }
                #endregion
            }
            else if (logEvent is StopEventRequestEvent)
            {
                #region StopEventRequestEvent
                // Added by PAs on 02/02/05 for Stop Event request 
                // casting logEvent to StopEventRequestEvent type
                StopEventRequestEvent sEvent = (StopEventRequestEvent)logEvent;
                // creating a hash table to populate sql params
                parameters = new Hashtable();

                // Submitted
                // checking it is compactible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(sEvent.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddStopEventRequestEvent", sEvent.FileFormatter.AsString(sEvent))));
                    return;
                }
                // Adding StartTime
                parameters.Add("@Submitted", sEvent.Submitted);

                parameters.Add("@RequestId", sEvent.RequestId);
                parameters.Add("@RequestType", sEvent.RequestType.ToString());
                parameters.Add("@Successful", sEvent.Success);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(sEvent.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddStopEventRequestEvent", sEvent.FileFormatter.AsString(sEvent))));
                    return;

                }
                parameters.Add("@TimeLogged", sEvent.Time);

                // try writing to DB now 
                try
                {
                    WriteToDB("AddStopEventRequestEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "AddStopEventRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.DBSAddStopEventRequestEventFailed);
                }
                #endregion
            }
            else if (logEvent is ExposedServicesEvent)
            {
                #region ExposedServicesEvent
                // Added by PAs on 02/02/05 for Exposed Services event

                ExposedServicesEvent eEvent = (ExposedServicesEvent)logEvent;
                // creating a hash table to populate sql params
                parameters = new Hashtable();

                // Submitted
                // checking it is compatible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(eEvent.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddExposedServicesEvent", eEvent.FileFormatter.AsString(eEvent))));
                    return;
                }
                // Adding StartTime
                parameters.Add("@Submitted", eEvent.Submitted);

                parameters.Add("@Token", eEvent.Token);
                parameters.Add("@Category", eEvent.Category.ToString());
                parameters.Add("@Successful", eEvent.Successful);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(eEvent.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddExposedServicesEvent", eEvent.FileFormatter.AsString(eEvent))));
                    return;

                }
                parameters.Add("@TimeLogged", eEvent.Time);

                // try writing to DB now 
                try
                {
                    WriteToDB("AddExposedServicesEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "AddExposedServicesEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.EXSAddExpposedServicesEventFailed);
                }
                #endregion
            }
            else if (logEvent is LandingPageEntryEvent)
            {
                #region LandingPageEntryEvent
                //Added by Tim Mollart on 28/07/2005 for Landing Page Events

                LandingPageEntryEvent lpe = (LandingPageEntryEvent)logEvent;

                parameters = new Hashtable();
                parameters.Add("@LPPCode", lpe.PartnerID);
                parameters.Add("@LPSCode", lpe.ServiceID.ToString());

                parameters.Add("@SessionId", lpe.SessionId);
                parameters.Add("@UserLoggedOn", lpe.UserLoggedOn);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(lpe.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddLoginEvent", lpe.FileFormatter.AsString(lpe))));
                    return;
                }

                parameters.Add("@TimeLogged", lpe.Time);

                try
                {
                    WriteToDB("AddLandingPageEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "LoginEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingLoginEvent);
                }
                #endregion
            }
            else if (logEvent is RTTIInternalEvent)
            {
                #region RTTIInternalEvent
                RTTIInternalEvent eventRTTI = (RTTIInternalEvent)logEvent;
                parameters = new Hashtable();

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(eventRTTI.StartDateTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "RTTIInternalEvent", eventRTTI.FileFormatter.AsString(eventRTTI))));
                    return;
                }

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(eventRTTI.EndDateTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "RTTIInternalEvent", eventRTTI.FileFormatter.AsString(eventRTTI))));
                    return;
                }

                parameters.Add("@StartTime", eventRTTI.StartDateTime);
                parameters.Add("@EndTime", eventRTTI.EndDateTime);
                parameters.Add("@NumberOfRetries", eventRTTI.NumberOfRetries);
                parameters.Add("@Successful", eventRTTI.LoggingSuccessful);

                WriteToDB("AddRTTIInternalEvent", parameters);
                #endregion
            }
            else if (logEvent is EnhancedExposedServiceEvent)
            {
                #region EnhancedExposedServiceEvent
                //TD106 Enhanced Exposed Web Service Framework
                bool isStartEvent;
                string internalTransactionId;
                string partnerId;
                bool successful;
                string externalTransactionId;
                DateTime eventTime;
                string serviceType;
                string operationType;

                int success;


                // Check which type of 	EnhancedExposedServicesEvent
                if (logEvent is EnhancedExposedServiceStartEvent)
                    isStartEvent = true;
                else
                    isStartEvent = false;


                // set the local variables
                EnhancedExposedServiceEvent eEvent = (EnhancedExposedServiceEvent)logEvent;

                ExposedServiceContext exContext = eEvent.ServiceReferenceContext;

                partnerId = exContext.PartnerId;
                externalTransactionId = exContext.ExternalTransactionId;
                internalTransactionId = exContext.InternalTransactionId;
                successful = eEvent.Successful;
                serviceType = exContext.ServiceType;
                operationType = exContext.OperationType;
                eventTime = eEvent.EventTime;
                success = Convert.ToInt32(eEvent.Successful);

                // creating a hash table to populate sql params
                parameters = new Hashtable();

                // checking it is compatible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(eventTime))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddEnhancedExposedServiceEvent", eEvent.FileFormatter.AsString(eEvent))));
                    return;
                }
                // Adding StartTime
                parameters.Add("@PartnerId", int.Parse(partnerId));
                parameters.Add("@ExternalTransactionId", externalTransactionId);
                parameters.Add("@InternalTransactionId", internalTransactionId);
                parameters.Add("@CallSuccessful", success);
                parameters.Add("@ServiceType", serviceType);
                parameters.Add("@OperationType", operationType);
                parameters.Add("@EventTime", eventTime);

                if (isStartEvent)
                    parameters.Add("@IsStartEvent", 1);
                else
                    parameters.Add("@IsStartEvent", 0);

                try
                {	// try writing to DB now 
                    WriteToDB("AddEnhancedExposedServiceEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "AddEnhancedExposedServiceEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.EESAddTDExpposedServicesEventFailed);
                }
                #endregion
            }
            else if (logEvent is EBCCalculationEvent)
            {
                #region EBCCalculationEvent
                parameters = new Hashtable();

                EBCCalculationEvent eventEBC = (EBCCalculationEvent)logEvent;

                parameters.Add("@Submitted", eventEBC.Submitted);
                parameters.Add("@SessionId", eventEBC.SessionId);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(eventEBC.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddEBCCalculationEvent", eventEBC.FileFormatter.AsString(eventEBC))));
                    return;
                }
                parameters.Add("@TimeLogged", eventEBC.Time);
                parameters.Add("@Successful", eventEBC.Success);
                
                try
                {
                    WriteToDB("AddEBCCalculationEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "EBCCalculationEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingEBCCalculationEvent);
                }
                #endregion
            }
            else if (logEvent is InternationalPlannerEvent)
            {
                #region InternationalPlannerEvent
                parameters = new Hashtable();

                InternationalPlannerEvent ce = (InternationalPlannerEvent)logEvent;

                parameters.Add("@InternationalPlannerType", ce.InternationalPlanner);
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddInternationalPlannerEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddInternationalPlannerEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "InternationalPlannerEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingInternationalPlannerEvent);
                }
                #endregion
            }
            else if (logEvent is InternationalPlannerResultEvent)
            {
                #region InternationalPlannerResultEvent
                InternationalPlannerResultEvent ce = (InternationalPlannerResultEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@InternationalPlannerRequestId", ce.InternationalPlannerRequestId);
                parameters.Add("@ResponseCategory", ce.ResponseCategory.ToString());
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddInternationalPlannerResultEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddInternationalPlannerResultEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "InternationalPlannerResultEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingInternationalPlannerResultEvent);
                }
                #endregion
            }
            else if (logEvent is InternationalPlannerRequestEvent)
            {
                #region InternationalPlannerRequestEvent
                InternationalPlannerRequestEvent ce = (InternationalPlannerRequestEvent)logEvent;
                parameters = new Hashtable();
                
                parameters.Add("@InternationalPlannerRequestId", ce.InternationalPlannerRequestId);
                parameters.Add("@SessionId", ce.SessionId);
                parameters.Add("@UserLoggedOn", ce.UserLoggedOn);
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddInternationalPlannerRequestEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add("@TimeLogged", ce.Time);

                try
                {
                    WriteToDB("AddInternationalPlannerRequestEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "InternationalPlannerRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingInternationalPlannerRequestEvent);
                }
                #endregion
            }
            else if (logEvent is RealTimeRoadEvent)
            {
                #region RealTimeRoadEvent
                RealTimeRoadEvent rtre = (RealTimeRoadEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@Submitted", rtre.Submitted);

                parameters.Add("@RealTimeRoadType", rtre.RealTimeRoadTypeOfEvent.ToString());
                parameters.Add("@RealTimeFound", rtre.RealTimeFound);
                parameters.Add("@SessionId", rtre.SessionId);
                
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(rtre.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddRealTimeRoadEvent", rtre.FileFormatter.AsString(rtre))));
                    return;

                }
                parameters.Add("@TimeLogged", rtre.Time);
                parameters.Add("@Successful", rtre.Success);
                try
                {
                    WriteToDB("AddRealTimeRoadEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "AddRealTimeRoadEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingInternationalPlannerRequestEvent);
                }
                #endregion
            }
            else if (logEvent is GISQueryEvent)
            {
                #region GISQueryEvent
                GISQueryEvent gqe = (GISQueryEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@Submitted", gqe.Submitted);

                parameters.Add("@GISQueryType", gqe.GISQueryType.ToString());
                
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(gqe.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddGISQueryEvent", gqe.FileFormatter.AsString(gqe))));
                    return;

                }
                parameters.Add("@TimeLogged", gqe.Time);
                try
                {
                    WriteToDB("AddGISQueryEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "AddGISQueryEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingGISQueryEvent);
                }
                #endregion
            }
            else if (logEvent is AccessibleEvent)
            {
                #region AccessibleEvent
                AccessibleEvent ae = (AccessibleEvent)logEvent;
                parameters = new Hashtable();

                parameters.Add("@Submitted", ae.Submitted);

                parameters.Add("@AccessibleEventType", ae.AccessibleEventType.ToString());

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ae.Time))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, "AddAccessibleEvent", ae.FileFormatter.AsString(ae))));
                    return;

                }
                parameters.Add("@TimeLogged", ae.Time);
                try
                {
                    WriteToDB("AddAccessibleEvent", parameters);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(String.Format(Messages.EventPublishFailure, "AddGISQueryEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingAccessibleEvent);
                }
                #endregion
            }
            else
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "TDPCustomEventPublisher")));
                throw new TDException(String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "TDPCustomEventPublisher"), false, TDExceptionIdentifier.RDPUnsupportedDatabasePublisherEvent);
            }
		}
	}
}