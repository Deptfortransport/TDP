// *********************************************** 
// NAME                 : CJPCustomEventPublisher.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : A publisher that publishes CJPCustomEvents to the staging database
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/CJPCustomEventPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:12   mturner
//Initial revision.
//
//   Rev 1.15   Jan 24 2005 13:58:02   jgeorge
//Changes to InternalRequestEvent and JourneyWebRequestEvent. Also added DB types to WriteToDb method.
//
//   Rev 1.14   Jul 06 2004 16:18:02   jgeorge
//Bug fixes
//
//   Rev 1.13   Jul 02 2004 15:32:30   jgeorge
//Bug fixes
//
//   Rev 1.12   Jul 02 2004 13:53:40   jgeorge
//Added InternalRequestEvent
//
//   Rev 1.11   Jun 28 2004 15:39:32   passuied
//Fix for the Event Receiver
//
//   Rev 1.10   Nov 14 2003 10:43:48   geaton
//Removed redundant logging that is already being logged by the TD Trace Listener.
//
//   Rev 1.9   Nov 14 2003 09:08:30   geaton
//Store target database as a member variable.
//
//   Rev 1.8   Nov 13 2003 21:59:56   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.7   Oct 10 2003 15:25:08   geaton
//Updated constructors to take target database property key. This enables validation at construction time.
//
//   Rev 1.6   Oct 07 2003 15:56:24   PScott
//publishers now called with id argument
//
//   Rev 1.5   Oct 06 2003 15:17:42   geaton
//Changed TDException references to use TDExceptionIdentifier identifier.
//
//   Rev 1.4   Sep 25 2003 12:00:32   ALole
//Updated Publishers to include code review comments.
//Added JourneyPlanRequestVerboseEvent, JourneyPlanResultsVerboseEvent and uncommented JourneyPlanResultsEvent.
//Added Properties service to TDPCUstomEventPublisher for VerboseEvent support.
//
//   Rev 1.3   Sep 15 2003 15:11:20   geaton
//Change TDDateTime to DateTime.
//
//   Rev 1.2   Sep 10 2003 11:26:30   ALole
//Added SessionId to JourneyWebRequestEvent
//
//   Rev 1.1   Sep 05 2003 11:22:18   ALole
//Added TimeLogged field to the db table
//
//   Rev 1.0   Sep 04 2003 17:03:10   ALole
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.CJPCustomEvents;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Database Publisher for CJPCustomEvents.
	/// </summary>
	public class CJPCustomEventPublisher : IEventPublisher
	{
		private string identifier;
		private SqlHelperDatabase targetDatabase;

		private readonly string AddLocationRequestStoredProcedureName = "AddLocationRequestEvent";
		private readonly string AddJourneyWebRequestStoredProcedureName = "AddJourneyWebRequestEvent";
		private readonly string AddInternalRequestStoredProcedureName = "AddInternalRequestEvent";

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="identifier">Identifier for publisher. Must match identifier defined in logging service property key.</param>
		/// <param name="targetDatabase">Identifier of target database to which events are published.</param>
		public CJPCustomEventPublisher(string identifier, SqlHelperDatabase targetDatabase)
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

				throw new TDException(String.Format(Messages.ConstructorFailed, message.ToString()), false, TDExceptionIdentifier.RDPCJPCustomEventPublisherConstruction);
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
		/// Executes the requested Stored Procedure with the parameters supplied
		/// </summary>
		/// <param name="storedProcName">string containing the Stored Procedure Name</param>
		/// <param name="parameters">Hashtable containg the Stored Proc parameters</param>
		/// <param name="types">Hashtable containing the stored proc parameters types</param>
		/// <exception cref="TDException">Thrown when the stored Procedure return code is incorrect</exception>
		/// <exception cref="TDException">Thrown when a SqlException is caught</exception>
		private void WriteToDB( string storedProcName, Hashtable parameters, Hashtable types )
		{
			SqlHelper sqlHelper = new SqlHelper();

			try
			{
				sqlHelper.ConnOpen(this.targetDatabase);
				
				int returnCode = sqlHelper.Execute( storedProcName, parameters, types );

				if ( returnCode != 1 )
				{
					throw new TDException(String.Format(Messages.SQLHelperInsertFailed, 1, storedProcName, returnCode),
										  false,
										  TDExceptionIdentifier.RDPSQLHelperInsertFailed); 
				}

			}
			catch( SqlException sqlEx )
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
		/// Public method to publish an event to the database
		/// </summary>
		/// <param name="logEvent"></param>
		/// <exception cref="TDException">Thrown when the call to WriteToDB method fails</exception>
		/// <exception cref="TDException">Thrown if the Event passed in is not a CJPCustomEvent</exception>
		public void WriteEvent(LogEvent logEvent)
		{
			string formatString = String.Empty;

			if( logEvent is JourneyWebRequestEvent )
			{
				JourneyWebRequestEvent customEvent = (JourneyWebRequestEvent)logEvent;
				Hashtable parameters = new Hashtable();
				Hashtable types = new Hashtable();

				// Add all the required parameters for the Stored Procedure to the hashtable
				parameters.Add( "@JourneyWebRequestId", customEvent.JourneyWebRequestId );
				parameters.Add( "@SessionId", customEvent.SessionId );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( customEvent.Submitted))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, AddJourneyWebRequestStoredProcedureName, customEvent.FileFormatter.AsString(customEvent))));
					return;
				}
				parameters.Add( "@Submitted", customEvent.Submitted );
				parameters.Add( "@RequestType", customEvent.RequestType.ToString() );
				parameters.Add( "@RegionCode", customEvent.RegionCode );
				parameters.Add( "@Success", customEvent.Success );
				parameters.Add( "@RefTransaction", customEvent.RefTransaction );
				
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( customEvent.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, AddJourneyWebRequestStoredProcedureName, customEvent.FileFormatter.AsString(customEvent))));

					return;

				}
				parameters.Add( "@TimeLogged", customEvent.Time );
				
				types.Add( "@JourneyWebRequestId", SqlDbType.VarChar );
				types.Add( "@SessionId", SqlDbType.VarChar );
				types.Add( "@Submitted", SqlDbType.DateTime );
				types.Add( "@RequestType", SqlDbType.VarChar );
				types.Add( "@RegionCode", SqlDbType.VarChar );
				types.Add( "@Success", SqlDbType.Bit );
				types.Add( "@RefTransaction", SqlDbType.Bit );
				types.Add( "@TimeLogged", SqlDbType.DateTime );

				try
				{	
					WriteToDB(this.AddJourneyWebRequestStoredProcedureName, parameters, types);
				}
				catch( TDException tdEx )
				{ 
					throw new TDException(String.Format(Messages.EventPublishFailure, "AddJourneyWebRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingJourneyWebRequestEvent);
				}
			}
			else if( logEvent is LocationRequestEvent )
			{
				LocationRequestEvent customEvent = (LocationRequestEvent)logEvent;
				Hashtable parameters = new Hashtable();
				Hashtable types = new Hashtable();

				parameters.Add( "@JourneyPlanRequestId", customEvent.JourneyPlanRequestId );
				parameters.Add( "@PrepositionCategory", customEvent.PrepositionCategory.ToString() );
				parameters.Add( "@AdminAreaCode", customEvent.AdminAreaCode );
				parameters.Add( "@RegionCode", customEvent.RegionCode );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( customEvent.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, AddLocationRequestStoredProcedureName, customEvent.FileFormatter.AsString(customEvent))));
					return;

				}

				parameters.Add( "@TimeLogged", customEvent.Time );

				types.Add( "@JourneyPlanRequestId", SqlDbType.VarChar );
				types.Add( "@PrepositionCategory", SqlDbType.VarChar );
				types.Add( "@AdminAreaCode", SqlDbType.VarChar );
				types.Add( "@RegionCode", SqlDbType.VarChar );
				types.Add( "@TimeLogged", SqlDbType.DateTime );
				
				try
				{
					WriteToDB(this.AddLocationRequestStoredProcedureName, parameters, types);
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, "LocationRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingLocationRequestEvent);	
				}
			}
			else if (logEvent is InternalRequestEvent)
			{
				InternalRequestEvent customEvent = (InternalRequestEvent)logEvent;
				Hashtable parameters = new Hashtable();
				Hashtable types = new Hashtable();

				// Add all the required parameters for the Stored Procedure to the hashtable
				parameters.Add( "@InternalRequestId", customEvent.InternalRequestId );
				parameters.Add( "@SessionId", customEvent.SessionId );
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( customEvent.Submitted))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, AddInternalRequestStoredProcedureName, customEvent.FileFormatter.AsString(customEvent))));
					return;
				}
				parameters.Add( "@Submitted", customEvent.Submitted );
				parameters.Add( "@InternalRequestType", customEvent.RequestType.ToString() );
				parameters.Add( "@FunctionType", customEvent.FunctionType );
				parameters.Add( "@Success", customEvent.Success );
				parameters.Add( "@RefTransaction", customEvent.RefTransaction );
				
				if(!SqlTypeHelper.IsSqlDateTimeCompatible( customEvent.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, AddInternalRequestStoredProcedureName, customEvent.FileFormatter.AsString(customEvent))));

					return;

				}
				parameters.Add( "@TimeLogged", customEvent.Time );
				
				types.Add( "@InternalRequestId", SqlDbType.VarChar );
				types.Add( "@SessionId", SqlDbType.VarChar );
				types.Add( "@Submitted", SqlDbType.DateTime );
				types.Add( "@InternalRequestType", SqlDbType.VarChar );
				types.Add( "@FunctionType", SqlDbType.Char );
				types.Add( "@Success", SqlDbType.Bit );
				types.Add( "@RefTransaction", SqlDbType.Bit );
				types.Add( "@TimeLogged", SqlDbType.DateTime );

				try
				{	
					WriteToDB(this.AddInternalRequestStoredProcedureName, parameters, types);
				}
				catch( TDException tdEx )
				{ 
					throw new TDException(String.Format(Messages.EventPublishFailure, "InternalRequestEvent", tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingInternalRequestEvent);
				}
			}
			else
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "CJPCustomEventPublisher")));
				throw new TDException(String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "CJPCustomEventPublisher"), false, TDExceptionIdentifier.RDPUnsupportedDatabasePublisherEvent);
			}
		}
	}
}

