// *********************************************** 
// NAME                 : SSCustomEventPublisher.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Database publisher for all SSCustomEvents
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/DatabasePublisher/SSCustomEventPublisher.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:46   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

using AO.Common;
using AO.DatabaseInfrastructure;

using Logger = System.Diagnostics.Trace;
using PropertyService = AO.Properties.Properties;

namespace AO.EventLogging
{
    public class SSCustomEventPublisher : IEventPublisher
    {
        private string identifier;
        private SqlHelperDatabase targetDatabase;

        #region Constructor

        /// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="identifier">Identifier for publisher. Must match identifier defined in logging service property key.</param>
		/// <param name="targetDatabase">Identifier of target database to which events are published.</param>
		public SSCustomEventPublisher(string identifier, SqlHelperDatabase targetDatabase)
		{
			this.identifier = identifier;
			this.targetDatabase = targetDatabase;
			
			DatabasePublisherPropertyValidator validator = new DatabasePublisherPropertyValidator(PropertyService.Instance);
			ArrayList errors = new ArrayList();
			validator.ValidateProperty(targetDatabase.ToString(), errors);
			validator.ValidateProperty(String.Format(Keys.CustomPublisherName, identifier), errors);

			if (errors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);

				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

                throw new SSException(String.Format(Messages.SSTDatabasePublisherConstructorFailed, message.ToString()), false, SSExceptionIdentifier.ELSDatabasePublisherConstructor);
			}
		}

        #endregion

        #region Public properties

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
		/// <exception cref="TDException">Thrown when the call to WriteToDB method fails</exception>
		/// <exception cref="TDException">Thrown if the Event passed in is not a TDPCustomEvent</exception>
        public void WriteEvent(LogEvent logEvent)
        {
            Hashtable parameters = null;

            if (logEvent is ReferenceTransactionEvent)
            {
                #region ReferenceTransactionEvent

                ReferenceTransactionEvent ce = (ReferenceTransactionEvent)logEvent;

                string storedProcedure = "AddReferenceTransactionEventSiteStatus";

                parameters = new Hashtable();

                parameters.Add("@EventType", ce.EventType);
                parameters.Add("@ServiceLevelAgreement", ce.ServiceLevelAgreement);
                
                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.TimeSubmitted))
                {
                    Logger.Write(new OperationalEvent(SSEventCategory.Database, SSTraceLevel.Error, 
                        string.Format(Messages.DBSQLDateTimeOverflow, storedProcedure, ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add("@Submitted", ce.TimeSubmitted);

                parameters.Add("@SessionId", ce.SessionId);

                if (!SqlTypeHelper.IsSqlDateTimeCompatible(ce.Time))
                {
                    Logger.Write(new OperationalEvent(SSEventCategory.Database, SSTraceLevel.Error, 
                        string.Format(Messages.DBSQLDateTimeOverflow, storedProcedure, ce.FileFormatter.AsString(ce))));
                    return;
                }
                parameters.Add("@TimeLogged", ce.TimeCompleted); // Reporting uses the difference between Submitted and Completed to calculate duration
                parameters.Add("@Successful", ce.Successful);

                try
                {
                    WriteToDB(storedProcedure, parameters);

                    if (ce.LogSuccessWriteEvent)
                    {
                        Logger.Write(new OperationalEvent(SSEventCategory.Database, SSTraceLevel.Info,
                        string.Format(Messages.SSLSLoggedSiteStatusEvent, ce.FileFormatter.AsString(ce))));
                    }
                }
                catch (SSException ssEx)
                {
                    if (ssEx.Identifier == SSExceptionIdentifier.DBSQLHelperPrimaryKeyViolation)
                    {
                        // No need to throw error, because where it is a primary key violation, 
                        // this RTE has already been logged so attempting to add again isnt a problem
                    }
                    else
                    {
                        // Add on the Database error for the Real time site status service monitoring.
                        // This prefix is needed by the TNG monitoring to detect a DB error and raise its alerts as required
                        string message = String.Format(Messages.SSLSDatabaseCurrentError, PropertyService.Instance["SiteStatusLoaderService.Messages.DatabaseError.Current"]);
                        message += String.Format(Messages.DBEventPublishFailure, "ReferenceTransactionEvent", ssEx.Message);

                        throw new SSException(message, ssEx, false, SSExceptionIdentifier.DBFailedPublishingReferenceTransactionEvent);
                    }
                }
                #endregion
            }
            else
            {
                string message = String.Format(Messages.SSTDatabasePublisherUnsupportedEventType, logEvent.ClassName, "SSCustomEventPublisher");

                Logger.Write(new OperationalEvent(SSEventCategory.Database, SSTraceLevel.Error, message));

                throw new SSException(message, false, SSExceptionIdentifier.DBUnsupportedDatabasePublisherEvent);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Executes the requested Stored Procedure with the parameters supplied.
        /// </summary>
        /// <param name="storedProcName">string containing the Stored Procedure Name</param>
        /// <param name="parameters">Hashtable containg the Stored Proc parameters</param>
        private void WriteToDB(string storedProcName, Hashtable parameters)
        {
            SqlHelper sqlHelper = new SqlHelper();

            try
            {
                sqlHelper.ConnOpen(this.targetDatabase);

                int returnCode = sqlHelper.Execute(storedProcName, parameters);

                if (returnCode != 1)
                {
                    throw new SSException(String.Format(Messages.DBSQLHelperInsertFailed, 1, storedProcName, returnCode),
                                          false,
                                          SSExceptionIdentifier.DBSQLHelperInsertFailed);
                }

            }
            catch (SqlException sqlEx) // SQLHelper does not catch SqlException so catch here.
            {
                // If this is a primary key violation, then throw the relevant exception
                if (sqlEx.Number == 2627)
                {
                    throw new SSException(String.Format(Messages.DBSQLHelperPrimaryKeyError, storedProcName, sqlEx.Message), sqlEx, false, SSExceptionIdentifier.DBSQLHelperPrimaryKeyViolation);
                }
                else
                {
                    throw new SSException(String.Format(Messages.DBSQLHelperError, storedProcName, sqlEx.Message), sqlEx, false, SSExceptionIdentifier.DBSQLHelperStoredProcedureFailure);
                }
            }
            catch (SqlTypeException ste)
            {
                throw new SSException(String.Format(Messages.DBSQLHelperTypeError, storedProcName, ste.Message), ste, false, SSExceptionIdentifier.DBSQLHelperStoredProcedureFailure);
            }
            catch (Exception ex)
            {
                throw new SSException(String.Format(Messages.DBError, ex.Message), ex, false, SSExceptionIdentifier.DBError);
            }
            finally
            {
                sqlHelper.ConnClose();
            }
        }

        #endregion
    }
}
