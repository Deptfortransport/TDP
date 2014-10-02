// *********************************************** 
// NAME                 : OperationalEventPublisher.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : A publisher that publishes OperationalEvents to the staging database
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/OperationalEventPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:16   mturner
//Initial revision.
//
//   Rev 1.12   Aug 16 2004 10:16:54   jgeorge
//Fix for vantive 3389081
//
//   Rev 1.11   Jun 28 2004 15:39:30   passuied
//Fix for the Event Receiver
//
//   Rev 1.10   Nov 14 2003 11:05:58   geaton
//Corrected logged flag value.
//
//   Rev 1.9   Nov 14 2003 10:43:26   geaton
//Removed redundant logging that is already being logged by the TDTraceListener.
//
//   Rev 1.8   Nov 14 2003 09:08:26   geaton
//Store target database as a member variable.
//
//   Rev 1.7   Nov 13 2003 22:00:00   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.6   Oct 30 2003 12:21:24   geaton
//Added support for publishing ReceivedOperationalEvents (which are logged by the Event Receiver)
//
//   Rev 1.5   Oct 10 2003 15:25:12   geaton
//Updated constructors to take target database property key. This enables validation at construction time.
//
//   Rev 1.4   Oct 07 2003 15:56:20   PScott
//publishers now called with id argument
//
//   Rev 1.3   Oct 06 2003 15:18:02   geaton
//Changed TDException references to use TDExceptionIdentifier identifier.
//
//   Rev 1.2   Sep 25 2003 12:00:34   ALole
//Updated Publishers to include code review comments.
//Added JourneyPlanRequestVerboseEvent, JourneyPlanResultsVerboseEvent and uncommented JourneyPlanResultsEvent.
//Added Properties service to TDPCUstomEventPublisher for VerboseEvent support.
//
//   Rev 1.1   Sep 05 2003 11:22:16   ALole
//Added TimeLogged field to the db table
//
//   Rev 1.0   Sep 04 2003 17:03:12   ALole
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Data.SqlTypes;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Database Publisher for OperationalEvents.
	/// </summary>
	public class OperationalEventPublisher : IEventPublisher
	{
		private Hashtable fieldLengths = null;

		private string identifier;
		private SqlHelperDatabase targetDatabase;
		private readonly string AddOperationalEventStoredProcedureName = "AddOperationalEvent";

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="identifier">Identifier for publisher. Must match identifier defined in logging service property key.</param>
		/// <param name="targetDatabase">Identifier of target database to which events are published.</param>
		public OperationalEventPublisher(string identifier, SqlHelperDatabase targetDatabase)
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

				throw new TDException(String.Format(Messages.ConstructorFailed, message.ToString()), false, TDExceptionIdentifier.RDPOperationalEventPublisherConstruction);
			}

			LoadFieldLengths(false);

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
		/// <param name="types">Hashtable containing the Stored Proc parameter types</param>
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
					throw new TDException(String.Format(Messages.SQLHelperInsertFailed, 1, storedProcName, returnCode), false, TDExceptionIdentifier.RDPSQLHelperInsertFailed); 
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
		/// Public method to publish an operational event to the database.
		/// </summary>
		/// <param name="logEvent"></param>
		/// <exception cref="TDException">Thrown when the call to WriteToDB method fails</exception>
		/// <exception cref="TDException">Thrown if the Event passed in is not a OperationalEvent</exception>
		/// <remarks>
		/// If the event passed is a ReceivedOperationalEvent then the 
		/// OperationalEvent wrapped within this event is published.
		/// </remarks>
		public void WriteEvent(LogEvent logEvent)
		{
			string formatString = String.Empty;

			if ((logEvent is OperationalEvent) || (logEvent is ReceivedOperationalEvent))
			{
				OperationalEvent operationalEvent = null;

				if (logEvent is ReceivedOperationalEvent)
				{
					ReceivedOperationalEvent receivedOperationalEvent = (ReceivedOperationalEvent)logEvent;
					operationalEvent = receivedOperationalEvent.WrappedOperationalEvent;
				}
				else
				{
					operationalEvent = (OperationalEvent)logEvent;
				}

				Hashtable parameters = new Hashtable();
				Hashtable types = new Hashtable();
			
				// Add all the required parameters for the Stored Procedure to the hashtable
				ArrayList truncatedFields = new ArrayList(8);

				parameters.Add( "@SessionId", TruncateField("SessionId", operationalEvent.SessionId, truncatedFields ));
				types.Add( "@SessionId", SqlDbType.VarChar );

				parameters.Add( "@Message", TruncateField("Message", operationalEvent.Message, truncatedFields ));
				types.Add( "@Message", SqlDbType.VarChar );
				
				parameters.Add( "@MachineName", TruncateField("MachineName", operationalEvent.MachineName, truncatedFields ));
				types.Add( "@MachineName", SqlDbType.VarChar );
				
				parameters.Add( "@AssemblyName", TruncateField("AssemblyName", operationalEvent.AssemblyName, truncatedFields ));
				types.Add( "@AssemblyName", SqlDbType.VarChar );
				
				parameters.Add( "@MethodName", TruncateField("MethodName", operationalEvent.MethodName, truncatedFields ));
				types.Add( "@MethodName", SqlDbType.VarChar );
				
				parameters.Add( "@TypeName", TruncateField("TypeName", operationalEvent.TypeName, truncatedFields ));
				types.Add( "@TypeName", SqlDbType.VarChar );
				
				parameters.Add( "@Level", TruncateField("Level", operationalEvent.Level.ToString(), truncatedFields ));
				types.Add( "@Level", SqlDbType.VarChar );
				
				parameters.Add( "@Category", TruncateField("Category", operationalEvent.Category.ToString(), truncatedFields ));
				types.Add( "@Category", SqlDbType.VarChar );
				
				
				// Catch null value before trying to cast it.
				if ( operationalEvent.Target != null )
				{
					parameters.Add( "@Target", TruncateField("Target", operationalEvent.Target.ToString(), truncatedFields ));
				}
				else
				{
					parameters.Add( "@Target", "" );
				}
				types.Add( "@Target", SqlDbType.VarChar );

				if(!SqlTypeHelper.IsSqlDateTimeCompatible( operationalEvent.Time))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, string.Format(Messages.SqlDateTimeOverflow, AddOperationalEventStoredProcedureName, operationalEvent.FileFormatter.AsString(operationalEvent))));
					return;

				}
				parameters.Add( "@TimeLogged", operationalEvent.Time );
				types.Add( "@TimeLogged", SqlDbType.DateTime );

				if (truncatedFields.Count > 0)
				{
					// At least one field has been truncated. Write a warning operational event
					string[] fields = (string[])truncatedFields.ToArray(typeof(string));
					Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Warning, String.Format(Messages.TruncatedField, string.Join(", ", fields), operationalEvent.FileFormatter.AsString(operationalEvent))));
				}

				try
				{
					WriteToDB(this.AddOperationalEventStoredProcedureName, parameters, types);
				}
				catch( TDException tdEx )
				{
					throw new TDException(String.Format(Messages.EventPublishFailure, logEvent.ClassName, tdEx.Message), tdEx, false, TDExceptionIdentifier.RDPFailedPublishingOperationalEvent);
				}
			}
			else
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "OperationalEventPublisher")));
				throw new TDException(String.Format(Messages.UnsupportedEventType, logEvent.ClassName, "OperationalEventPublisher"), true, TDExceptionIdentifier.RDPUnsupportedDatabasePublisherEvent);
			}
		}

		/// <summary>
		/// Attempts to retrieve the lengths of the text fields in the operational events
		/// table in the staging database. This information can then be used to trim the data.
		/// </summary>
		private bool LoadFieldLengths(bool forceReload)
		{
			if ( (fieldLengths != null) && !forceReload)
				return true;

			lock (this)
			{
				SqlHelper sqlHelper = new SqlHelper();

				try
				{
					sqlHelper.ConnOpen(this.targetDatabase);
					Hashtable param = new Hashtable(1);
					Hashtable types = new Hashtable(1);

                    param.Add("@TableName", "OperationalEvent");
					types.Add("@TableName", SqlDbType.NVarChar);
				
					SqlDataReader reader = sqlHelper.GetReader("GetTableColumnLengths", param, types);

					fieldLengths = new Hashtable();

					while (reader.Read())
						fieldLengths.Add(reader.GetString(0).ToLower(), reader.GetInt32(1));

					reader.Close();

				}
				catch(Exception ex)
				{
					// Write this as a warning then continue
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.FailedRetrievingFieldLengths, ex.Message), ex, string.Empty));
					fieldLengths = null;
					return false;
				}
				finally
				{
					if (sqlHelper.ConnIsOpen)
						sqlHelper.ConnClose();
				}
			}
			return true;
		}

		/// <summary>
		/// If there is a maximum length for the field specified in the hashtable, then
		/// this value is returned, otherwise 0 is returned
		/// </summary>
		/// <param name="fieldName"></param>
		/// <returns></returns>
		private int GetFieldLength(string fieldName)
		{
			string lowerFieldName = fieldName.ToLower();
			if (fieldLengths == null)
				return 0;
			if (fieldLengths.ContainsKey(lowerFieldName))
				return (int)fieldLengths[lowerFieldName];
			else
				return 0;
		}

		/// <summary>
		/// If the contents of fieldValue are greater than the maximum length for the field,
		/// as retrieved using GetFieldLength, a truncated copy of fieldValue will be returned,
		/// and fieldName will be added to fieldList. Otherwise fieldValue will be returned 
		/// unaltered.
		/// </summary>
		/// <param name="fieldName">Name of the field being processed</param>
		/// <param name="fieldValue">Value of the field being processed</param>
		/// <param name="fieldList">Array list to which the name of the field should be added if it is truncated</param>
		/// <returns></returns>
		private string TruncateField(string fieldName, string fieldValue, ArrayList fieldList)
		{
			int fieldLength = GetFieldLength(fieldName);
			if ((fieldLength != 0) && (fieldLength < fieldValue.Length))
			{
				fieldList.Add(fieldName);
				return fieldValue.Substring(0, fieldLength - 3) + "...";
			}
			else
				return fieldValue;
		}

	}
}
