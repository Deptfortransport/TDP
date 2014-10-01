// *********************************************** 
// NAME             : TDPCustomEventPublisher.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 Apr 2011
// DESCRIPTION  	: Publisher that publishes TDP Custom Events to the report staging database
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using TDP.Common;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using TDP.Reporting.Events;
using Logger = System.Diagnostics.Trace;

namespace TDP.Reporting.EventPublishers
{
    /// <summary>
    /// Publisher that publishes TDP Custom Events to the report staging database
    /// </summary>
    public class TDPCustomEventPublisher : IEventPublisher
    {
        #region Private members

        private string identifier;
        private SqlHelperDatabase targetDatabase;

        #endregion

        #region Constructor

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
            List<string> errors = new List<string>();
			validator.ValidateProperty(targetDatabase.ToString(), errors);
            validator.ValidateProperty(String.Format(TDP.Common.EventLogging.Keys.CustomPublisherName, identifier), errors);

			if (errors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);

				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				throw new TDPException(String.Format(Messages.ConstructorFailed, message.ToString()), false,
                    TDPExceptionIdentifier.RDPCustomEventPublisherConstruction);
			}
		}

        #endregion

        #region Publict properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Publisher for Events that inherit from TDCustomEvent
        /// </summary>
        /// <param name="logEvent">Event that inherits from TDPCustomEvent</param>
        /// <exception cref="TDPException">Thrown when the call to WriteToDB method fails</exception>
        /// <exception cref="TDPException">Thrown if the Event passed in is not a TDPCustomEvent</exception>
        public void WriteEvent(LogEvent logEvent)
        {
            List<SqlParameter> parameters = null;

            if (logEvent is WorkloadEvent)
            {
                #region WorkloadEvent

                WorkloadEvent workloadEvent = (WorkloadEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Requested", workloadEvent.Requested));
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(workloadEvent.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddWorkloadEvent", workloadEvent.FileFormatter.AsString(workloadEvent))));
                    return;

                }
                parameters.Add(new SqlParameter("@TimeLogged", workloadEvent.Time));
                parameters.Add(new SqlParameter("@NumberRequested", workloadEvent.NumberRequested));
                parameters.Add(new SqlParameter("@PartnerId", workloadEvent.PartnerId));

                try
                {
                    WriteToDB("AddWorkloadEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "WorkloadEvent", tdpEx.Message), 
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingWorkloadEvent);
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

                parameters = new List<SqlParameter>();

				parameters.Add(new SqlParameter("@Page", pageName ));
				parameters.Add(new SqlParameter("@SessionId", sessionId ));
				parameters.Add(new SqlParameter("@UserLoggedOn", userLoggedOn ));

				if(!SqlTypeHelper.IsSqlDateTimeCompatible( datetime))
				{
					Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddPageEntryEvent", entryEvent.FileFormatter.AsString(entryEvent))));
					return;
				}
				parameters.Add(new SqlParameter("@TimeLogged", datetime ));
                
                parameters.Add(new SqlParameter("@ThemeID", themeId)); 
				try
				{
					WriteToDB( "AddPageEntryEvent", parameters );
				}
				catch( TDPException tdpEx )
				{
					throw new TDPException(String.Format(Messages.EventPublishFailure, "PageEntryEvent", tdpEx.Message), 
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingPageEntryEvent);
                }
                #endregion
            }
            else if (logEvent is JourneyPlanRequestEvent)
            {
                #region JourneyPlanRequestEvent

                JourneyPlanRequestEvent jpre = (JourneyPlanRequestEvent)logEvent;

                parameters = new List<SqlParameter>();

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
                bool tram = false;
                bool underground = false;
                bool walk = false;
                bool telecabine = false;

                foreach (TDPModeType mode in jpre.Modes)
                {
                    if (mode.Equals(TDPModeType.Air))
                    {
                        air = true;
                    }
                    else if (mode.Equals(TDPModeType.Bus))
                    {
                        bus = true;
                    }
                    else if (mode.Equals(TDPModeType.Car))
                    {
                        car = true;
                    }
                    else if (mode.Equals(TDPModeType.Coach))
                    {
                        coach = true;
                    }
                    else if (mode.Equals(TDPModeType.Cycle))
                    {
                        cycle = true;
                    }
                    else if (mode.Equals(TDPModeType.Drt))
                    {
                        drt = true;
                    }
                    else if (mode.Equals(TDPModeType.Ferry))
                    {
                        ferry = true;
                    }
                    else if (mode.Equals(TDPModeType.Metro))
                    {
                        metro = true;
                    }
                    else if (mode.Equals(TDPModeType.Rail))
                    {
                        rail = true;
                    }
                    else if (mode.Equals(TDPModeType.Taxi))
                    {
                        taxi = true;
                    }
                    else if (mode.Equals(TDPModeType.Tram))
                    {
                        tram = true;
                    }
                    else if (mode.Equals(TDPModeType.Underground))
                    {
                        underground = true;
                    }
                    else if (mode.Equals(TDPModeType.Walk))
                    {
                        walk = true;
                    }
                }

                parameters.Add(new SqlParameter("@JourneyPlanRequestId", jpre.JourneyPlanRequestId));
                parameters.Add(new SqlParameter("@Air", air));
                parameters.Add(new SqlParameter("@Bus", bus));
                parameters.Add(new SqlParameter("@Car", car));
                parameters.Add(new SqlParameter("@Coach", coach));
                parameters.Add(new SqlParameter("@Cycle", cycle));
                parameters.Add(new SqlParameter("@Drt", drt));
                parameters.Add(new SqlParameter("@Ferry", ferry));
                parameters.Add(new SqlParameter("@Metro", metro));
                parameters.Add(new SqlParameter("@Rail", rail));
                parameters.Add(new SqlParameter("@Taxi", taxi));
                parameters.Add(new SqlParameter("@Tram", tram));
                parameters.Add(new SqlParameter("@Underground", underground));
                parameters.Add(new SqlParameter("@Walk", walk));
                parameters.Add(new SqlParameter("@Telecabine", telecabine));
                parameters.Add(new SqlParameter("@SessionId", jpre.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", jpre.UserLoggedOn));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(jpre.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddJourneyPlanRequestEvent", jpre.FileFormatter.AsString(jpre))));
                    return;
                }

                parameters.Add(new SqlParameter("@TimeLogged", jpre.Time));

                try
                {
                    WriteToDB("AddJourneyPlanRequestEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "JourneyPlanRequestEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingJourneyPlanRequestEvent);
                }
                #endregion
            }
            else if (logEvent is JourneyPlanResultsEvent)
            {
                #region JourneyPlanResultsEvent

                JourneyPlanResultsEvent jpre = (JourneyPlanResultsEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@JourneyPlanRequestId", jpre.JourneyPlanRequestId));
                parameters.Add(new SqlParameter("@ResponseCategory", jpre.ResponseCategory.ToString()));
                parameters.Add(new SqlParameter("@SessionId", jpre.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", jpre.UserLoggedOn));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(jpre.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddJourneyPlanResultsEvent", jpre.FileFormatter.AsString(jpre))));
                    return;
                }

                parameters.Add(new SqlParameter("@TimeLogged", jpre.Time));

                try
                {
                    WriteToDB("AddJourneyPlanResultsEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "JourneyPlanResultsEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingJourneyPlanResultsEvent);
                }
                #endregion
            }
            else if (logEvent is CyclePlannerRequestEvent)
            {
                #region CyclePlannerRequestEvent

                CyclePlannerRequestEvent cpre = (CyclePlannerRequestEvent)logEvent;

                parameters = new List<SqlParameter>();

                bool cycle = true;

                parameters.Add(new SqlParameter("@CyclePlannerRequestId", cpre.CyclePlannerRequestId));
                parameters.Add(new SqlParameter("@Cycle", cycle));
                parameters.Add(new SqlParameter("@SessionId", cpre.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", cpre.UserLoggedOn));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(cpre.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddCyclePlannerRequestEvent", cpre.FileFormatter.AsString(cpre))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeLogged", cpre.Time));

                try
                {
                    WriteToDB("AddCyclePlannerRequestEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "CyclePlannerRequestEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingCyclePlannerRequestEvent);
                }
                #endregion
            }
            else if (logEvent is CyclePlannerResultEvent)
            {
                #region CyclePlannerResultEvent

                CyclePlannerResultEvent cpre = (CyclePlannerResultEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@CyclePlannerRequestId", cpre.CyclePlannerRequestId));
                parameters.Add(new SqlParameter("@ResponseCategory", cpre.ResponseCategory.ToString()));
                parameters.Add(new SqlParameter("@SessionId", cpre.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", cpre.UserLoggedOn));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(cpre.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddCyclePlannerResultEvent", cpre.FileFormatter.AsString(cpre))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeLogged", cpre.Time));

                try
                {
                    WriteToDB("AddCyclePlannerResultEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "CyclePlannerResultEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingCyclePlannerResultEvent);
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

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@RepeatVistorType", repeatVisitorType));
                parameters.Add(new SqlParameter("@SessionIdOld", sessionIdOld));
                parameters.Add(new SqlParameter("@SessionIdNew", sessionIdNew));
                parameters.Add(new SqlParameter("@DomainName", domain));
                parameters.Add(new SqlParameter("@UserAgent", userAgent));
                parameters.Add(new SqlParameter("@ThemeId", themeId));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(datetime))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddRepeatVisitorEvent", repeatVisitorEvent.FileFormatter.AsString(repeatVisitorEvent))));
                    return;
                }

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(lastVisitedDateTime))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddRepeatVisitorEvent", repeatVisitorEvent.FileFormatter.AsString(repeatVisitorEvent))));
                    return;
                }

                parameters.Add(new SqlParameter("@TimeLogged", datetime));
                parameters.Add(new SqlParameter("@LastVisited", lastVisitedDateTime));

                try
                {
                    WriteToDB("AddRepeatVisitorEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "RepeatVisitorEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingRepeatVisitorEvent);
                }
                #endregion
            }
            else if (logEvent is RetailerHandoffEvent)
            {
                #region RetailerHandoffEvent

                RetailerHandoffEvent ce = (RetailerHandoffEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@RetailerId", ce.RetailerId));
                parameters.Add(new SqlParameter("@SessionId", ce.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", ce.UserLoggedOn));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddRetailerHandoffEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeLogged", ce.Time));

                try
                {
                    WriteToDB("AddRetailerHandoffEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "RetailerHandoffEvent", tdpEx.Message), 
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingRetailerHandoffEvent);
                }
                #endregion
            }
            else if (logEvent is LandingPageEntryEvent)
            {
                #region LandingPageEntryEvent

                LandingPageEntryEvent lpe = (LandingPageEntryEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@LPPCode", lpe.PartnerID));
                parameters.Add(new SqlParameter("@LPSCode", lpe.ServiceID.ToString()));

                parameters.Add(new SqlParameter("@SessionId", lpe.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", lpe.UserLoggedOn));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(lpe.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddLandingPageEvent", lpe.FileFormatter.AsString(lpe))));
                    return;
                }

                parameters.Add(new SqlParameter("@TimeLogged", lpe.Time));

                try
                {
                    WriteToDB("AddLandingPageEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "LandingPageEvent", tdpEx.Message), 
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingLandingPageEvent);
                }
                #endregion
            }
            else if (logEvent is DataGatewayEvent)
            {
                #region DataGatewayEvent

                DataGatewayEvent ce = (DataGatewayEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@FeedId", ce.FeedId));
                parameters.Add(new SqlParameter("@SessionId", ce.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", ce.UserLoggedOn));
                parameters.Add(new SqlParameter("@FileName", ce.FileName));
                
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.TimeStarted))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddDataGatewayEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeStarted", ce.TimeStarted));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.TimeFinished))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddDataGatewayEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeFinished", ce.TimeFinished));

                parameters.Add(new SqlParameter("@SuccessFlag", ce.SuccessFlag));
                parameters.Add(new SqlParameter("@ErrorCode", ce.ErrorCode));
                
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddDataGatewayEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeLogged", ce.Time));
                
                try
                {
                    WriteToDB("AddDataGatewayEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "DataGatewayEvent", tdpEx.Message), 
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingDataGatewayEvent);
                }
                #endregion
            }
            else if (logEvent is GazetteerEvent)
            {
                #region GazetteerEvent
                GazetteerEvent ce = (GazetteerEvent)logEvent;
                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@EventCategory", ce.EventCategory.ToString()));
                parameters.Add(new SqlParameter("@SessionId", ce.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", ce.UserLoggedOn));
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddGazetteerEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add(new SqlParameter("@TimeLogged", ce.Time));
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddGazetteerEvent", ce.FileFormatter.AsString(ce))));
                    return;

                }
                parameters.Add(new SqlParameter("@Submitted", ce.Submitted));
                try
                {
                    WriteToDB("AddGazetteerEvent", parameters);
                }
                catch (TDPException tdEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "GazetteerEvent", tdEx.Message), 
                        tdEx, false, TDPExceptionIdentifier.RDPFailedPublishingGazetteerEvent);
                }
                #endregion
            }
            else if (logEvent is GISQueryEvent)
            {
                #region GISQueryEvent
                GISQueryEvent gqe = (GISQueryEvent)logEvent;
                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@Submitted", gqe.Submitted));

                parameters.Add(new SqlParameter("@GISQueryType", gqe.GISQueryType.ToString()));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(gqe.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddGISQueryEvent", gqe.FileFormatter.AsString(gqe))));
                    return;

                }
                parameters.Add(new SqlParameter("@TimeLogged", gqe.Time));
                try
                {
                    WriteToDB("AddGISQueryEvent", parameters);
                }
                catch (TDPException tdEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "AddGISQueryEvent", tdEx.Message), 
                        tdEx, false, TDPExceptionIdentifier.RDPFailedPublishingGISQueryEvent);
                }
                #endregion
            }
            else if (logEvent is StopEventRequestEvent)
            {
                #region StopEventRequestEvent

                StopEventRequestEvent sEvent = (StopEventRequestEvent)logEvent;

                parameters = new List<SqlParameter>();

                // Submitted
                // checking it is compactible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(sEvent.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddStopEventRequestEvent", sEvent.FileFormatter.AsString(sEvent))));
                    return;
                }
                // Adding StartTime
                parameters.Add(new SqlParameter("@Submitted", sEvent.Submitted));

                parameters.Add(new SqlParameter("@RequestId", sEvent.RequestId));
                parameters.Add(new SqlParameter("@RequestType", sEvent.RequestType.ToString()));
                parameters.Add(new SqlParameter("@Successful", sEvent.Success));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(sEvent.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, 
                        string.Format(Messages.SqlDateTimeOverflow, "AddStopEventRequestEvent", sEvent.FileFormatter.AsString(sEvent))));
                    return;

                }
                parameters.Add(new SqlParameter("@TimeLogged", sEvent.Time));

                try
                {
                    WriteToDB("AddStopEventRequestEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "StopEventRequestEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingStopEventRequestEvent);
                }
                #endregion
            }
            else if (logEvent is NoResultsEvent)
            {
                #region NoResultsEvent

                NoResultsEvent sEvent = (NoResultsEvent)logEvent;

                parameters = new List<SqlParameter>();

                // Submitted
                // checking it is compactible
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(sEvent.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddNoResultsEvent", sEvent.FileFormatter.AsString(sEvent))));
                    return;
                }
                // Adding StartTime
                parameters.Add(new SqlParameter("@Submitted", sEvent.Submitted));
                parameters.Add(new SqlParameter("@SessionId", sEvent.SessionId));
                parameters.Add(new SqlParameter("@UserLoggedOn", sEvent.UserLoggedOn));
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(sEvent.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddNoResultsEvent", sEvent.FileFormatter.AsString(sEvent))));
                    return;

                }
                parameters.Add(new SqlParameter("@TimeLogged", sEvent.Time));

                try
                {
                    WriteToDB("AddNoResultsEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "NoResultsEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingStopEventRequestEvent);
                }
                #endregion

            }
            else if (logEvent is ReferenceTransactionEvent)
            {
                #region ReferenceTransactionEvent

                ReferenceTransactionEvent ce = (ReferenceTransactionEvent)logEvent;

                parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@EventType", ce.EventType));
                parameters.Add(new SqlParameter("@ServiceLevelAgreement", ce.ServiceLevelAgreement));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Submitted))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddReferenceTransactionEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add(new SqlParameter("@Submitted", ce.Submitted));

                parameters.Add(new SqlParameter("@SessionId", ce.SessionId));

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error,
                        string.Format(Messages.SqlDateTimeOverflow, "AddReferenceTransactionEvent", ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add(new SqlParameter("@TimeLogged", ce.Time));

                parameters.Add(new SqlParameter("@Successful", ce.Successful));
                parameters.Add(new SqlParameter("@MachineName", ce.MachineName));

                try
                {
                    WriteToDB("AddReferenceTransactionEvent", parameters);
                }
                catch (TDPException tdpEx)
                {
                    throw new TDPException(String.Format(Messages.EventPublishFailure, "ReferenceTransactionEvent", tdpEx.Message),
                        tdpEx, false, TDPExceptionIdentifier.RDPFailedPublishingReferenceTransactionEvent);
                }
                #endregion
            }
            else
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "TDPCustomEventPublisher")));
                throw new TDPException(String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "TDPCustomEventPublisher"),
                    true, TDPExceptionIdentifier.RDPUnsupportedCustomEventPublisherEvent);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Executes the requested Stored Procedure with the parameters supplied.
        /// </summary>
        /// <param name="storedProcName">string containing the Stored Procedure Name</param>
        /// <param name="parameters">Hashtable containg the Stored Proc parameters</param>
        /// <exception cref="TDPException">Thrown when the stored Procedure return code is incorrect</exception>
        /// <exception cref="TDPException">Thrown when a SqlException is caught</exception>
        private void WriteToDB(string storedProcName, List<SqlParameter> sqlParameters)
        {
            try
            {
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(this.targetDatabase);

                    int returnCode = sqlHelper.Execute(storedProcName, sqlParameters);

                    if (returnCode != 1)
                    {
                        throw new TDPException(String.Format(Messages.SQLHelperInsertFailed, 1, storedProcName, returnCode),
                                              false,
                                              TDPExceptionIdentifier.RDPSQLHelperInsertFailed);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // SQLHelper does not catch SqlException so catch here.
                throw new TDPException(String.Format(Messages.SQLHelperError, storedProcName, sqlEx.Message), sqlEx, false, 
                    TDPExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
            }
            catch (SqlTypeException ste)
            {
                throw new TDPException(String.Format(Messages.SQLHelperTypeError, storedProcName, ste.Message), ste, false, 
                    TDPExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
            }
        }

        #endregion
    }
}
