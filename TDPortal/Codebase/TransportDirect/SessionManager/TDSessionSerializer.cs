// ***********************************************
// NAME         : TDSessionSerializer.cs
// AUTHOR       : J Cotton
// DATE CREATED : 11/09/2003
// DESCRIPTION  : Handles saving of deferrable objects to and from the database.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDSessionSerializer.cs-arc  $
//
//   Rev 1.4   Jul 19 2012 14:27:20   mmodi
//Updated to log session read write activity using a Properties switch
//
//   Rev 1.3   Nov 24 2009 09:33:22   mmodi
//Added extra verbose logging to help with debugging
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 12 2009 10:25:50   pscott
//Reintroduction of Session/Fares fix IR 5228
//
//   Rev 1.2   Mar 09 2009 14:36:08   apatel
//GetSessionLockStatus method added to check asp page session lock status.
//Resolution for 5228: Fares/Session Overwrite Problem
//
//   Rev 1.1   Jun 16 2008 11:17:58   mturner
//Fix for IR5022 - Word 2003 page landing hits logging as errors in operational events table.
//
//   Rev 1.0   Nov 08 2007 12:48:44   mturner
//Initial revision.
//
//   Rev 1.28   Feb 28 2007 14:59:10   jfrank
//CCN0366 - Enhancement to enable Page Landing from Word 2003 and to ignore session timeouts for Page Landing due to future usage by National Trust.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.27   Jan 17 2007 18:18:50   mmodi
//Added methods to retrieve session info for a Feedback record
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.26   Mar 14 2006 08:41:44   build
//Automatically merged from branch for stream3353
//
//   Rev 1.25.1.0   Feb 22 2006 12:00:34   RGriffith
//Changes made for multiple asynchronous ticket/costing
//
//   Rev 1.25   Nov 09 2005 12:31:50   build
//Automatically merged from branch for stream2818
//
//   Rev 1.24.2.0   Oct 14 2005 15:20:22   jgeorge
//Updated to store session paddings in static dictionary rather than looking up each time.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.24   Jul 05 2005 13:50:00   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.23.1.0   Jun 17 2005 09:54:36   jgeorge
//Added new constructor to allow AppDomain name to be specified. This allows the serializer to access session info from different AppDomains
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.23   May 25 2005 15:33:12   bflenk
//Padding now using Hex value for session padding when > 9 Web applications
//
//   Rev 1.22   May 17 2005 14:22:56   PNorell
//Updated for code-review
//Resolution for 1954: Dev Code Review: PT - Session Partitioning
//
//   Rev 1.21   Apr 23 2005 15:57:50   COwczarek
//Add methods to facilitate saving and retrieving array elements
//individually from deferred storage
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.20   Apr 16 2005 11:21:08   jgeorge
//Updated to create SqlHelper instance only as required
//Resolution for 2143: Del 7 - Code error causes intermittent SI Test hanging

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Handles saving of deferrable objects to and from the database.
	/// </summary>
	[CLSCompliant(false)]
	public class TDSessionSerializer
	{

		/// <summary>
		/// The session padding will remain the same for each app domain during runtime.
		/// This IDictionary stores the session padding for each appdomain so that it doesn't
		/// have to be looked up each an instance of the class is created.
		/// </summary>
		private static IDictionary SessionPadding = new System.Collections.Specialized.ListDictionary();

        private readonly string appDomainFriendlyName;
        private readonly bool logSession;

		/// <summary>
		/// Creates a session serializer for the current AppDomain
		/// </summary>
		public TDSessionSerializer() 
		{
			appDomainFriendlyName = System.AppDomain.CurrentDomain.FriendlyName;
            logSession = LogSessionActivity();
		}

		/// <summary>
		/// Creates a session serializer for the AppDomain with the given FriendlyName
		/// </summary>
		/// <param name="appDomainFriendlyName"></param>
		public TDSessionSerializer(string appDomainFriendlyName) 
		{
			this.appDomainFriendlyName = appDomainFriendlyName;
            this.logSession = LogSessionActivity();
		}

        /// <summary>
        /// Serialized and saves the supplied deferree object. If the object implements ITDDeferredArray,
        /// each element of the array is serialized individually so that it can be read individually.
        /// If the object implements ITDSessionAware, it will only be stored if it is declared as dirty.
        /// </summary>
        /// <param name="currentSessionId">The session id for the current session context</param>
        /// <param name="part">Session partition</param>
        /// <param name="deferrableKey">key identifying object</param>
        /// <param name="deferree">The object to save</param>
        public void SerializeSessionObjectAndSave( string currentSessionId , TDSessionPartition part, IKey deferrableKey, object deferree)
		{
			System.Array arr = deferree as System.Array;
            if (deferrableKey is ITDDeferredArray && arr != null) 
            {
                for (int i=0; i < arr.Length; i++)
                {
                    SerializeSessionObjectAndSave( currentSessionId, ((int)part) + deferrableKey.ID + i, arr.GetValue(i));
                }
            } 
            else 
            {
                SerializeSessionObjectAndSave( currentSessionId, ((int)part) + deferrableKey.ID, deferree);
            }
        }

		/// <summary>
		/// Serialized and saves the supplied array item in the deferree object. Does this by calling the 
		/// SerializeSessionObjectAndSave. If the object implements ITDDeferredArray,
		/// each element of the array is serialized individually so that it can be read individually.
		/// If the object implements ITDSessionAware, it will only be stored if it is declared as dirty.
		/// </summary>
		/// <param name="currentSessionId">The session id for the current session context</param>
		/// <param name="deferrableKey">key identifying object</param>
		/// <param name="deferree">The object to save</param>
		/// <param name="index">Index value to serialize and save the specific array item</param>
		public void SerializeSessionObjectAndSave(string currentSessionId, TDSessionPartition part , IKey deferrableKey, object deferree, int index)
		{
			SerializeSessionObjectAndSave( currentSessionId, ((int)part) + deferrableKey.ID + index, deferree );
		}
		
		/// <summary>
        /// Serialized and saves the supplied deferree object. If the object implements ITDDeferredArray,
        /// each element of the array is serialized individually so that it can be read individually.
        /// If the object implements ITDSessionAware, it will only be stored if it is declared as dirty.
        /// </summary>
        /// <param name="currentSessionId">The session id for the current session context</param>
        /// <param name="deferrableKey">key identifying object</param>
        /// <param name="deferree">The object to save</param>
        public void SerializeSessionObjectAndSave( string currentSessionId , IKey deferrableKey, object deferree)
		{
			System.Array arr = deferree as System.Array;
            if (deferrableKey is ITDDeferredArray && arr != null) 
            {
                for (int i=0; i < arr.Length; i++)
                {
                    SerializeSessionObjectAndSave( currentSessionId, deferrableKey.ID + i, arr.GetValue(i));
                }
            } 
            else 
            {
                SerializeSessionObjectAndSave( currentSessionId, deferrableKey.ID, deferree);
            }
        }

        /// <summary>
		/// Asks to have an object seralized and stored.
		/// If the object implements ITDSessionAware, it will only be stored if it is declared as dirty.
		/// </summary>
		/// <param name="currentSessionID">The current session id</param>
		/// <param name="part">The partition used</param>
		/// <param name="deferrableKey">The associated key to the object being saved</param>
		/// <param name="deferree">The object to save</param>
		private void SerializeSessionObjectAndSave( string currentSessionId , string key, object deferree)
		{
			string stringob = deferree as string;
			// do it if we have something
			if ( deferree != null && TDSessionManager.NULL_SAVED != stringob )
			{
				ITDSessionAware sesAw = deferree as ITDSessionAware;
				if( sesAw != null )
				{
					if( sesAw.IsDirty )
					{
						// change state of it as saved before saving it.
						sesAw.IsDirty = false;
					}
					else 
					{
						if( logSession )
						{
							// Nothing to save here - it has not changed
							OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
								TDTraceLevel.Verbose,
								string.Format(System.Globalization.CultureInfo.CurrentCulture,  "Not saving object {0}", key ) );					 
							Logger.Write(oe);
						}
						return;
					}
				}
				if( stringob == TDSessionManager.NULL_UNSAVED )
				{
					deferree = TDSessionManager.NULL_SAVED;
				}
				// instantiate a MemoryStream          
				using( MemoryStream memoryStream = new MemoryStream() )
				{
			
					// Do the serialisation and pass the object and byte array of data of to a method
					// that saves it to the deferred table in the session database.
					try 
					{
                        if (logSession)
                        {
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Verbose,
                                string.Format(System.Globalization.CultureInfo.CurrentCulture, 
                                "Starting save of object {0}", key));
                            Logger.Write(oe);
                        }
				
						// create a new BinaryFormatter instance 
						BinaryFormatter binaryFormatter = new BinaryFormatter();

                        if (logSession)
                        {
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Verbose,
                                string.Format(System.Globalization.CultureInfo.CurrentCulture,
                                "Serializing object {0}", deferree.ToString()));
                            Logger.Write(oe);
                        }

						// serialize the class into a MemoryStream 
						binaryFormatter.Serialize( memoryStream, deferree ); 
						memoryStream.Seek(0,0); 
				
						// Save the information 
						SaveToDeferredSession( key, memoryStream.ToArray(), currentSessionId );

                        if (logSession)
                        {
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Verbose,
                                string.Format(System.Globalization.CultureInfo.CurrentCulture,
                                "Completed save of object {0}", key));
                            Logger.Write(oe);
                        }
					} 
					catch(Exception ex) 
					{ 
						// TDEventLogging
						OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
							TDTraceLevel.Error,
							ex.Message );
					 
						Logger.Write(oe);
					} 
					finally 
					{ 
						//Clean up 
						memoryStream.Close(); 
					}
				}

			}
		}

		/// <summary>
		/// Copies the data from the old session to the new session.
		/// This is to enable JPLanding from Word 2003
		/// </summary>
		/// <param name="currentSessionId">The current session id</param>
		/// <param name="oldSessionId">The old session id</param>
		/// <returns></returns>
		public void UpdateToDeferredSession( string currentSessionId, string oldSessionId )
		{
			
			int sqlResult = 0;
			Hashtable sqlParam = new Hashtable();
			Hashtable types = new Hashtable();

			// Build the Hash table of parameters
			sqlParam.Add( "@CurrSessionID", currentSessionId+RetrieveSessionPadding() );
			types.Add( "@CurrSessionID", SqlDbType.VarChar );
			sqlParam.Add( "@OldSessionID", oldSessionId+RetrieveSessionPadding() );
			types.Add( "@OldSessionID", SqlDbType.VarChar );
			SqlHelper TDSessionSerializerSQL = null;
			try
			{
				// Open our connection to the ASPState Database
				TDSessionSerializerSQL = new SqlHelper();
				TDSessionSerializerSQL.ConnOpen( SqlHelperDatabase.ASPStateDB );
				sqlResult = TDSessionSerializerSQL.Execute( "usp_UpdateDeferredData", sqlParam, types); 

				// Log Word event
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Info,
					string.Format(System.Globalization.CultureInfo.CurrentCulture, "Word 2003 Page Landing Hit" ));
					 
				Logger.Write(oe);
			}
			
			catch( SqlException eSQL )
			{
				sqlResult = -1;
				
				// TDEventLogging
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error,
					string.Format(  System.Globalization.CultureInfo.CurrentCulture, "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message ));
					 
				Logger.Write(oe);

			}

			finally
			{
				if ( TDSessionSerializerSQL != null )
					TDSessionSerializerSQL.Dispose();
			}
		} 

		/// <summary>
		/// Internal function to save
		/// </summary>
		/// <param name="deferredSessionKey">The exact key to be used</param>
		/// <param name="binarySessionItem">The binary representation of the object to be saved</param>
		/// <param name="currentSessionID">The session id</param>
		/// <returns></returns>
		private int SaveToDeferredSession( string deferredSessionKey, byte[] binarySessionItem, string currentSessionId )
		{
			
			int sqlResult = 0;
			Hashtable sqlParam = new Hashtable();
			Hashtable types = new Hashtable();

			// Build the Hash table of parameters
			sqlParam.Add( "@SessionID", currentSessionId+RetrieveSessionPadding() );
			types.Add( "@SessionID", SqlDbType.VarChar );
			sqlParam.Add( "@KeyID", deferredSessionKey );
			types.Add( "@KeyID", SqlDbType.Char );
			sqlParam.Add( "@SessionItem", binarySessionItem );
			types.Add( "@SessionItem", SqlDbType.Image );
			SqlHelper TDSessionSerializerSQL = null;
			try
			{
                if (logSession)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Verbose,
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        "Opening connection to database {0}", SqlHelperDatabase.ASPStateDB.ToString()));
                    Logger.Write(oe);
                }
                // Open our connection to the ASPState Database
				TDSessionSerializerSQL = new SqlHelper();
				TDSessionSerializerSQL.ConnOpen( SqlHelperDatabase.ASPStateDB );

                if (logSession)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Verbose,
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        "Executing usp_SaveDeferredData Session[{0}] Key[{1}]", 
                        currentSessionId+RetrieveSessionPadding(), 
                        deferredSessionKey));
                    Logger.Write(oe);
                }

				sqlResult = TDSessionSerializerSQL.Execute( "usp_SaveDeferredData", sqlParam, types);

                if (logSession)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Verbose,
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        "Executing usp_SaveDeferredData completed with sqlResult [{0}]",
                        sqlResult));
                    Logger.Write(oe);
                }
			}
			
			catch( SqlException eSQL )
			{
				sqlResult = -1;
				
				// TDEventLogging
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
															TDTraceLevel.Error,
															string.Format(  System.Globalization.CultureInfo.CurrentCulture, "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message ));
					 
				Logger.Write(oe);

			}

			finally
			{
				if ( TDSessionSerializerSQL != null )
					TDSessionSerializerSQL.Dispose();
			}

			return sqlResult;
		} 

        /// <summary>
        /// Retrieves the object identified by the supplied key from deferred storage
        /// </summary>
        /// <param name="currentSessionID">The current session id</param>
        /// <param name="deferrableKey">The key of the object being retrieved</param>
        /// <returns>The object read from deferred storage</returns>
        public object RetrieveAndDeserializeSessionObject( string currentSessionId, IKey deferrableKey) 
		{
			return RetrieveAndDeserializeSessionObject( currentSessionId, deferrableKey.ID);
		}

        /// <summary>
        /// Retrieves the object identified by the supplied key from deferred storage. This method retrieves
        /// deferred arrays so the supplied index identifies the element of the array to retrieve. If the
        /// supplied key does not implement ITDDeferredArray, an operational event is logged.
        /// </summary>
        /// <param name="currentSessionID">The current session id</param>
        /// <param name="index">Index of deferred array element to retrieve</param>
        /// <param name="deferrableKey">The key of the object being retrieved</param>
        /// <returns>The object read from deferred storage</returns>
        public object RetrieveAndDeserializeSessionObject( string currentSessionId, IKey deferrableKey, int index) 
        {
            if (deferrableKey is ITDDeferredArray)
            {
                return RetrieveAndDeserializeSessionObject( currentSessionId, deferrableKey.ID +index);
            } 
            else 
            {
                OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Error,
                    string.Format("Attempt to read a deferred array element using a key that does not implement ITDDeferredArray, key id={0}",
                    deferrableKey.ID));	 
                Logger.Write(oe);
                return null;
            }
        }

        /// <summary>
        /// Retrieves the object identified by the supplied key from deferred storage.
        /// </summary>
        /// <param name="currentSessionID">The current session id</param>
        /// <param name="part">Session partition</param>
        /// <param name="deferrableKey">The key of the object being retrieved</param>
        /// <returns>The object read from deferred storage</returns>
        public object RetrieveAndDeserializeSessionObject( string currentSessionId, TDSessionPartition part, IKey deferrableKey) 
		{
			if (deferrableKey is ITDDeferredArray) 
			{
				ArrayList objectArray = new ArrayList();
				Object arrayElement;
				int i = 0;
				do
				{
					arrayElement = RetrieveAndDeserializeSessionObject( currentSessionId, ((int)part)+deferrableKey.ID + i.ToString());
					if (arrayElement != null)
					{
						objectArray.Add(arrayElement);
					}
					i++;
				} while (arrayElement != null);
				return objectArray;
			}
			else
			{
				return RetrieveAndDeserializeSessionObject( currentSessionId, ((int)part)+deferrableKey.ID);
			}
		}

        /// <summary>
        /// Retrieves the object identified by the supplied key from deferred storage. This method retrieves
        /// deferred arrays so the supplied index identifies the element of the array to retrieve. If the
        /// supplied key does not implement ITDDeferredArray, an operational event is logged.
        /// </summary>
        /// <param name="currentSessionID">The current session id</param>
        /// <param name="part">Session partition</param>
        /// <param name="index">Index of deferred array element to retrieve</param>
        /// <param name="deferrableKey">The key of the object being retrieved</param>
        /// <returns>The object read from deferred storage</returns>
        public object RetrieveAndDeserializeSessionObject( string currentSessionId, TDSessionPartition part, IKey deferrableKey, int index) 
        {
            if (deferrableKey is ITDDeferredArray) 
            {
                return RetrieveAndDeserializeSessionObject( currentSessionId, ((int)part)+deferrableKey.ID + index);
            } 
            else 
            {
                OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database, TDTraceLevel.Error,
                    string.Format("Attempt to read a deferred array element using a key that does not implement ITDDeferredArray, partition={0}, key id={1}",
                    part.ToString(),deferrableKey.ID));		 
                Logger.Write(oe);
                return null;
            }
        }

		/// <summary>
		/// Retrieves an object
		/// </summary>
		/// <param name="currentSessionID">The session id</param>
		/// <param name="part">The partition for the object</param>
		/// <param name="deferrableKey">The key associated with the object to retrieve</param>
		/// <returns></returns>
		private object RetrieveAndDeserializeSessionObject( string currentSessionId, string deferrableKey) 
		{
			// instantiate a MemoryStream          
			using( MemoryStream memoryStream = new MemoryStream() )
			{

				try
				{
                    if (logSession)
                    {
                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                            TDTraceLevel.Verbose,
                            string.Format(System.Globalization.CultureInfo.CurrentCulture,
                            "Start retrieve and deserialise of session object Session[{0}] Key[{1}]", 
                            currentSessionId, deferrableKey));
                        Logger.Write(oe);
                    }

					// get the object from the deferred table in the session database
					// and store it temporarilly into a buffer, while we write
					// it to the memory stream.
					byte[] temporaryBuffer = GetFromDeferredSession( deferrableKey, currentSessionId); 
					if( temporaryBuffer == null )
					{
                        if (logSession)
                        {
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Verbose,
                                string.Format(System.Globalization.CultureInfo.CurrentCulture,
                                "Get from deffered session returned a null object Session[{0}] Key[{1}]",
                                currentSessionId, deferrableKey));
                            Logger.Write(oe);
                        }

                        if (logSession)
                        {
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Verbose,
                                string.Format(System.Globalization.CultureInfo.CurrentCulture,
                                "Completed retrieve and deserialise of session object Session[{0}] Key[{1}]",
                                currentSessionId, deferrableKey));
                            Logger.Write(oe);
                        }

						// Nothing stored - register try within deferrableObjects?
						return null;
					}
				
					memoryStream.Write ( temporaryBuffer, 0, temporaryBuffer.Length );
					memoryStream.Seek(0,0); 
				
					// create a new BinaryFormatter instance 
					BinaryFormatter binaryFormatter = new BinaryFormatter();

                    if (logSession)
                    {
                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                            TDTraceLevel.Verbose,
                            string.Format(System.Globalization.CultureInfo.CurrentCulture,
                            "Completed retrieve and deserialise of session object Session[{0}] Key[{1}]",
                            currentSessionId, deferrableKey));
                        Logger.Write(oe);
                    }
					return binaryFormatter.Deserialize(memoryStream);
				}			
				catch(Exception ex) 
				{ 
					// TDEventLogging
					OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
						TDTraceLevel.Error,
						ex.Message );
					 
					Logger.Write(oe);
				} 
				finally 
				{
					//Clean up 
					memoryStream.Close(); 
				}
			}
			return null;
		}

		/// <summary>
		/// Get an object from the deferred storage
		/// </summary>
		/// <param name="deferredSessionKey">The complete key</param>
		/// <param name="currentSessionID">The session id</param>
		/// <returns>A binary representation of the object requested</returns>
		private byte[] GetFromDeferredSession( string deferredSessionKey, string currentSessionId )
		{ 
			Hashtable sqlParam = new Hashtable();
			Hashtable types = new Hashtable();

			// Build the Hash table of parameters
			sqlParam.Add( "@SessionID", currentSessionId+RetrieveSessionPadding() );
			types.Add("@SessionID", SqlDbType.VarChar );
			sqlParam.Add( "@KeyID", deferredSessionKey );
			types.Add("@KeyID", SqlDbType.Char );

			SqlHelper TDSessionSerializerSQL = null;
			byte[] resultData = null;

            try
            {
                if (logSession)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Verbose,
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        "Opening connection to database {0}", SqlHelperDatabase.ASPStateDB.ToString()));
                    Logger.Write(oe);
                }

                // Create a SqlHelper and open our connection to the ASPState Database
                TDSessionSerializerSQL = new SqlHelper();
                TDSessionSerializerSQL.ConnOpen(SqlHelperDatabase.ASPStateDB);

                if (logSession)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Verbose,
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        "Executing usp_GetDeferredData Session[{0}] Key[{1}]",
                        currentSessionId + RetrieveSessionPadding(),
                        deferredSessionKey));
                    Logger.Write(oe);
                }

                // Stream the serialized object into an SQLDataReader
                SqlDataReader drSql = TDSessionSerializerSQL.GetReader("usp_GetDeferredData", sqlParam, types);
                if (!drSql.Read())
                {
                    if (logSession)
                    {
                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                            TDTraceLevel.Verbose,
                            string.Format(System.Globalization.CultureInfo.CurrentCulture,
                            "Executing usp_GetDeferredData did not return an object from the database for Session[{0}] Key[{1}]",
                            currentSessionId + RetrieveSessionPadding(),
                            deferredSessionKey));
                        Logger.Write(oe);
                    }

                    // Nothing yet stored in the deferred session, ignore and return null
                    return null;
                }

                // Return as a byte array
                resultData = (byte[])drSql["SessionItemLong"];

                if (logSession)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Verbose,
                        string.Format(System.Globalization.CultureInfo.CurrentCulture,
                        "Executing usp_GetDeferredData completed, SessionItemLong data value has length of {0}.",
                        resultData.Length.ToString()));
                    Logger.Write(oe);
                }
            }

            catch (SqlException eSQL)
            {

                // TDEventLogging
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                                            TDTraceLevel.Error,
                                                            string.Format(System.Globalization.CultureInfo.CurrentCulture, "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message));

                Logger.Write(oe);

            }
			finally
			{
				// If the db connection is open, close it.
				if ( TDSessionSerializerSQL != null )
					TDSessionSerializerSQL.Dispose();
			}

			return resultData;

		}

		// Find the correct session padding
		/// <summary>
		/// This can technically happen more than once but as the session padding
		/// will be unmutable, read only string it does not matter more than initially
		/// being a bit of a performance hit if more than one thread access it at the same
		/// time.
		/// </summary>
		/// <returns></returns>
		private string RetrieveSessionPadding()
		{
			if( !SessionPadding.Contains(appDomainFriendlyName) )
			{
				// Need to look up the name and store it in the dictionary
				lock(this)
				{
					// Check again in case another thread has completed the lookup in the meantime
					if( !SessionPadding.Contains(appDomainFriendlyName) )
					{
						// Find AppName
						string appName = appDomainFriendlyName;
						// Outlook is: /LM/w3svc/1/Root/Web-14-127082061660609700
						// Where - is the separator for what we need.
						int index = appName.IndexOf('-');
						if( index == -1 ) 
						{
							OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
								TDTraceLevel.Error,"App Id can not be processed, friendly name is :"+appName+":");					 
							Logger.Write(oe);
							return null;
						}
						appName = appName.Substring(0,index);
						Hashtable deferredSessionSqlParameters = new Hashtable();

						// Build the Hash table of parameters
						deferredSessionSqlParameters.Add( "@appName", appName );
						SqlDataReader sqlResult = null;
						SqlHelper TDSessionSerializerSQL = null;
						try
						{
							// Create a SqlHelper and open our connection to the ASPState Database
							TDSessionSerializerSQL = new SqlHelper();
							TDSessionSerializerSQL.ConnOpen( SqlHelperDatabase.ASPStateDB );
							sqlResult = TDSessionSerializerSQL.GetReader( "usp_GetAppID", deferredSessionSqlParameters ); 
							if( !sqlResult.Read() )
							{
								OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
									TDTraceLevel.Error,"App id for "+appName+" can not be found");					 
								Logger.Write(oe);
								return null;
							}

							// Warning: This is actually a hexidecimal value and should be turned into the correct base
							// This is information has been gained from Microsoft internally, Peter Norell
							// Padding is using Hex value for session padding once we reach > 9 Web applications
							string padding = (""+sqlResult["AppId"]);
							int temppadding = Int32.Parse(padding);
							padding = temppadding.ToString("X");
					
							// The slow way - but it loops at maximum five times
							while(padding.Length < 8 ) { padding = "0"+padding; }
	
							SessionPadding.Add(appDomainFriendlyName, padding);

							if( TDTraceSwitch.TraceVerbose )
							{
								OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
									TDTraceLevel.Verbose,"Session Padding has been set to :"+padding+":");					 
								Logger.Write(oe);
							}
					
						}
						catch( SqlException eSQL )
						{
							OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
								TDTraceLevel.Error,
								string.Format( System.Globalization.CultureInfo.CurrentCulture, "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message ));
					 
							Logger.Write(oe);
						}
						finally
						{
							if( sqlResult != null )
								sqlResult.Close();

							if ( TDSessionSerializerSQL != null)
								TDSessionSerializerSQL.Dispose();
					
						}
					}
				}
			}
			return (string)SessionPadding[appDomainFriendlyName];
		}

        /// <summary>
        /// Returns flag if session activity should be logged
        /// </summary>
        /// <returns></returns>
        private bool LogSessionActivity()
        {
            try
            {
                bool log = false;

                if (bool.TryParse(Properties.Current["Session.Debug.Logging.Switch"], out log))
                {
                    return log;
                }
            }
            catch
            {
                // ignore exception if property missing or invalid
            }

            return false;
        }

		#region Properties and Methods used to retrieve session data for FeedbackViewer use

		#region Public properties

		/// <summary>
		/// Retrieves the object identified by the supplied key from UserFeedbackSessionData storage.
		/// A Feedback Id is required given a user can have submitted multiple Feedback requests in a
		/// session
		/// </summary>
		/// <param name="currentFeedbackID">The feedback id</param>
		/// <param name="currentSessionID">The current session id</param>
		/// <param name="deferrableKey">The key of the object being retrieved</param>
		/// <returns>The object read from deferred storage</returns>
		public object RetrieveAndDeserializeFeedbackSessionObject(int currentFeedbackId, string currentSessionId, IKey deferrableKey) 
		{
			return RetrieveAndDeserializeFeedbackSessionObject(currentFeedbackId, currentSessionId, deferrableKey.ID);
		}

		/// <summary>
		/// Retrieves the object identified by the supplied key from UserFeedbackSessionData storage.
		/// A Feedback Id is required given a user can have submitted multiple Feedback requests in a
		/// session
		/// </summary>
		/// <param name="currentFeedbackID">The feedback id</param>
		/// <param name="currentSessionID">The current session id</param>
		/// <param name="part">Session partition</param>
		/// <param name="deferrableKey">The key of the object being retrieved</param>
		/// <returns>The object read from deferred storage</returns>
		public object RetrieveAndDeserializeFeedbackSessionObject(int currentFeedbackId, string currentSessionId, TDSessionPartition part, IKey deferrableKey) 
		{
			if (deferrableKey is ITDDeferredArray) 
			{
				ArrayList objectArray = new ArrayList();
				Object arrayElement;
				int i = 0;
				do
				{
					arrayElement = RetrieveAndDeserializeFeedbackSessionObject(currentFeedbackId, currentSessionId, ((int)part)+deferrableKey.ID + i.ToString());
					if (arrayElement != null)
					{
						objectArray.Add(arrayElement);
					}
					i++;
				} while (arrayElement != null);
				return objectArray;
			}
			else
			{
				return RetrieveAndDeserializeFeedbackSessionObject(currentFeedbackId, currentSessionId, ((int)part)+deferrableKey.ID);
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Retrieves an object - from UserFeedbackSessionData.
		/// This should ONLY be used to view session information saved during a User Feedback submit
		/// </summary>
		/// <param name="currentSessionID">The session id</param>
		/// <param name="deferrableKey">The key associated with the object to retrieve</param>
		/// <returns></returns>
		private object RetrieveAndDeserializeFeedbackSessionObject(int currentFeedbackId, string currentSessionId, string deferrableKey) 
		{
			// instantiate a MemoryStream          
			using( MemoryStream memoryStream = new MemoryStream() )
			{

				try
				{
					// get the object from the deferred table in the session database
					// and store it temporarilly into a buffer, while we write
					// it to the memory stream.
					byte[] temporaryBuffer = GetFromFeedbackSessionData( currentFeedbackId, deferrableKey, currentSessionId); 
					if( temporaryBuffer == null )
					{
						// Nothing stored - register try within deferrableObjects?
						return null;
					}
				
					memoryStream.Write ( temporaryBuffer, 0, temporaryBuffer.Length );
					memoryStream.Seek(0,0); 
				
					// create a new BinaryFormatter instance 
					BinaryFormatter binaryFormatter = new BinaryFormatter(); 
					return binaryFormatter.Deserialize(memoryStream);
				}			
				catch(Exception ex) 
				{ 
					// TDEventLogging
					OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
						TDTraceLevel.Error,
						ex.Message );
					 
					Logger.Write(oe);
				} 
				finally 
				{
					//Clean up 
					memoryStream.Close(); 
				}
			}
			return null;
		}


		/// <summary>
		/// Get an object from the FeedbackSessionData storage - in TDUserInfo database
		/// </summary>
		/// <param name="currentFeedbackId">The feedback id</param>
		/// <param name="deferredSessionKey">The complete key</param>
		/// <param name="currentSessionID">The session id</param>
		/// <returns>A binary representation of the object requested</returns>
		private byte[] GetFromFeedbackSessionData(int currentFeedbackId, string deferredSessionKey, string currentSessionId )
		{ 
			Hashtable sqlParam = new Hashtable();
			Hashtable types = new Hashtable();

			// Build the Hash table of parameters
			sqlParam.Add( "@FeedbackID", currentFeedbackId );
			types.Add("@FeedbackID", SqlDbType.Int );
			sqlParam.Add( "@SessionID", currentSessionId+RetrieveSessionPadding() );
			types.Add("@SessionID", SqlDbType.VarChar );
			sqlParam.Add( "@KeyID", deferredSessionKey );
			types.Add("@KeyID", SqlDbType.Char );

			SqlHelper TDSessionSerializerSQL = null;
			byte[] resultData = null;

			try
			{
				// Create a SqlHelper and open our connection to the TDUserInfo Database
				TDSessionSerializerSQL = new SqlHelper();
				TDSessionSerializerSQL.ConnOpen( SqlHelperDatabase.UserInfoDB );
				
				// Stream the serialized object into an SQLDataReader
				SqlDataReader drSql = TDSessionSerializerSQL.GetReader( "GetSessionData", sqlParam, types ); 
				if( !drSql.Read() )
				{
					// Nothing yet stored in the session, ignore and return null
					return null;
				}
				
				// Return as a byte array
				resultData = (byte[]) drSql[ "SessionItemLong" ]; 
			}
			
			catch( SqlException eSQL )
			{
				
				// TDEventLogging
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error,
					string.Format( System.Globalization.CultureInfo.CurrentCulture, "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message ));
					 
				Logger.Write(oe);

			}

			finally
			{
				// If the db connection is open, close it.
				if ( TDSessionSerializerSQL != null )
					TDSessionSerializerSQL.Dispose();
			}

			return resultData;

		}

		#endregion
	
		#endregion

        #region method to check the webserver session lock down status
        /// <summary>
        /// looks for the asp session lock down status which gets written to database
        /// </summary>
        /// <param name="currentSessionId">current session Id</param>
        /// <returns>returns true if the asp session is locked down</returns>
        public bool GetSessionLockStatus(string currentSessionId)
        {
            bool sqlResult = false;
            Hashtable sqlParam = new Hashtable();
            Hashtable types = new Hashtable();

            // Build the Hash table of parameters
            sqlParam.Add("@SessionID", currentSessionId + RetrieveSessionPadding());
            types.Add("@SessionID", SqlDbType.VarChar);

            SqlHelper TDSessionSerializerSQL = null;
            try
            {
                // Open our connection to the ASPState Database
                TDSessionSerializerSQL = new SqlHelper();
                TDSessionSerializerSQL.ConnOpen(SqlHelperDatabase.ASPStateDB);
                sqlResult = (bool)TDSessionSerializerSQL.GetScalar("GetWebServerSessionLockState", sqlParam, types);
            }

            catch (SqlException eSQL)
            {
                sqlResult = false;

                // TDEventLogging
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                                            TDTraceLevel.Error,
                                                            string.Format(System.Globalization.CultureInfo.CurrentCulture, "SQL Exception {0:F0} - {1}", eSQL.Number, eSQL.Message));

                Logger.Write(oe);

            }

            finally
            {
                if (TDSessionSerializerSQL != null)
                    TDSessionSerializerSQL.Dispose();
            }

            return sqlResult;
        }
        #endregion

    }// end class 	
}