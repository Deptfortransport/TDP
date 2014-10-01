// ***********************************************
// NAME 		: SqlHelper.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 18-Jul-2003
// DESCRIPTION 	: Core DB Infrastructure.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/SqlHelper.cs-arc  $
//
//   Rev 1.11   Mar 15 2012 17:37:10   dlane
//Adding facility to call batch db with longer timeout
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.10   Feb 29 2012 14:26:40   dlane
//Added methods that use return parameters
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.9   Feb 07 2012 12:43:54   dlane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.8   Jun 10 2011 14:08:26   apatel
//Updated for SJP Databases
//Resolution for 5690: Update Database Infrastructure to cater for SJP databases for gateway
//
//   Rev 1.7   Jun 10 2011 14:01:46   apatel
//Updated to have SJPGazetteerDB in the database enumeration for SJP
//Resolution for 5690: Update Database Infrastructure to cater for SJP databases for gateway
//
//   Rev 1.6   Jul 01 2010 12:46:56   apatel
//Updated for CJP Config data import/export utility
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code
//
//   Rev 1.5   Jan 29 2010 12:10:24   mmodi
//Added InternationalPlanner database
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   May 08 2008 15:48:20   jfrank
//Adding reference to GAZ_Staging db needed for gateway import jmd265
//Resolution for 4957: Attraction Alias Importer failing
//
//   Rev 1.3   Mar 10 2008 15:15:38   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev Devfactory Jan 26 2008 08:09:00 apatel
//  new Database value CarParksDB added to SqlHelperDatabase
//
//   Rev 1.0   Nov 08 2007 12:19:54   mturner
//Initial revision.
//
//  Rev Devfactory Feb 18 2008 sbarker
//  Added alternative way to open the SqlHelper using a connection string. Note that this change is
//  marked as internal, so this alteration will not provide external users with a back door to opening
//  a database.
//
//    Rev Devfactory Jan 09 2008 16:43:00   sbarker
//    CCN 0427 - Extra item added to SqlHelperDatabase to support content database (ContentDB).
//
//   Rev 1.27   Aug 31 2007 14:27:28   rbroddle
//CCN393, (Coach Taxi Info) work, merged in from stream4468
//
//   Rev 1.26.1.0   Aug 21 2007 10:35:26   rbroddle
//Added new "AtosAdditionalDataDB" to SqlHelperDatabase enum
//Resolution for 4468: Coach Stop Taxi Enhancements
//
//   Rev 1.26   Dec 04 2006 13:34:04   tmollart
//Modified ExecuteCMD method's to provide an overridden method that allows passing of a command time out value for the command object.
//Resolution for 4282: Car Parking data importer timeouts after 30 seconds and does not complete.
//
//   Rev 1.25   Nov 09 2005 12:23:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.24.1.1   Oct 11 2005 11:03:42   schand
//Remove additional regional script which caused compilation error.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.24.1.0   Oct 10 2005 14:50:36   schand
//Added additional methods required by Search By Price Data Gateway.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.24   Apr 18 2005 11:22:46   jgeorge
//Updated Dispose method to call Dispose on SqlConnection object.
//
//   Rev 1.23   Feb 08 2005 10:22:50   jbroome
//Added ProductAvailabilityDB and extra overloads for query methods.
//
//   Rev 1.22   Nov 01 2004 15:51:50   jgeorge
//Added PlacesDB
//
//   Rev 1.21   Aug 03 2004 13:42:20   passuied
//added enum type
//Resolution for 1275: Move the ReportStagingDatabase to another machine - DEL6.0
//
//   Rev 1.20   May 27 2004 18:41:10   CHosegood
//Added AirRouteMatrixDB
//
//   Rev 1.19   Apr 27 2004 12:04:02   cshillan
//Inclusion of UserInfoDB that holds user profile information that is part of the removal of Commerce Server
//
//   Rev 1.18   Nov 18 2003 10:55:18   geaton
//Removed Report Staging Data id - staging data now stored in DefaultDB
//
//   Rev 1.17   Oct 14 2003 17:02:06   JHaydock
//Added GetDataSet methods for datagrid binding when connection has been closed
//
//   Rev 1.16   Oct 09 2003 15:32:56   JMorrissey
//Renamed TransientData to TransientPortal
//
//   Rev 1.15   Oct 06 2003 17:18:12   JMorrissey
//Added TransientDataDB to enum values for SqlHelperDatabase
//
//   Rev 1.14   Oct 06 2003 13:43:54   PNorell
//Added sql type on overloaded methods.
//
//   Rev 1.13   Sep 10 2003 14:44:16   ALole
//Added ReportDataDB to SqlHelperDatabase enum.
//
//   Rev 1.12   Sep 10 2003 10:17:44   jcotton
//Added 'ASPStateDB' to SqlHelperDatabase enum
//
//   Rev 1.11   Sep 04 2003 15:52:44   ALole
//Added '"ReportDataStagingDB' to SqlHelperDatabase enum
//
//   Rev 1.10   Aug 28 2003 15:29:58   TKarsan
//Inserted IsOpen connection method.
//
//   Rev 1.9   Aug 28 2003 12:53:52   jcotton
// 
//
//   Rev 1.8   Aug 28 2003 12:28:32   jcotton
//ESRI database Enum and Conn string added
//
//   Rev 1.7   Aug 11 2003 18:24:50   TKarsan
//Inserted Reference Number methods
//
//   Rev 1.6   Jul 25 2003 16:00:18   TKarsan
//Changes after 2nd code review
//
//   Rev 1.5   Jul 24 2003 16:00:18   TKarsan
//Update after review.
//
//   Rev 1.4   Jul 23 2003 10:22:48   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.3   Jul 21 2003 16:04:52   TKarsan
//FxCop fixes
//
//   Rev 1.2   Jul 18 2003 10:41:46   TKarsan
//Initial realease with comments on top of the file. The properties file is upated to use local server with trusted connection.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common.PropertyService.Properties;
using System.Xml;

namespace TransportDirect.Common.DatabaseInfrastructure
{
	/// <summary>
	/// Required by the Open() method to select a database.
	/// </summary>
	public enum SqlHelperDatabase
	{
		DefaultDB,
		EsriDB,
		ASPStateDB,
		ReportDataDB,
		TransientPortalDB,
		UserInfoDB,
        AirDataProviderDB,
		ReportStagingDB,
		ProductAvailabilityDB,
		PlacesDB,
		AtosAdditionalDataDB,
        CarParksDB, // CCN-426 New Car Parking Database
        GAZ_StagingDB,
        InternationalDataDB,
        SJPGazetteerDB,
        SJPTransientPortalDB,
        BatchJourneyPlannerDB,
        BatchJourneyPlannerDBLongTimeout,
		SqlHelperDatabaseEnd // Should ALWAYS be the last one, represents maximum.
	}

	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class SqlHelper : IDisposable
	{

		/// <summary>
		/// Used internally to maintain a connection. It needs to be opened
		/// explicitly and closed explicitly.
		/// </summary>
		private SqlConnection sqlConn = new SqlConnection();

		private static int refNumberCur, refNumberMax;
		private static string qryUpdate = "UPDATE ReferenceNum SET RefID = RefID + 100";
		private static string qrySelect = "SELECT MAX(RefID) AS RefID FROM ReferenceNum";

		#region Constructor and Destroy

		/// <summary>
		/// Constructor, does nothing yet.
		/// </summary>
		public SqlHelper()
		{
		}

		/// <summary>
		/// Ensures that the connection is closed.
		/// </summary>
		public void Dispose()
		{
			if (sqlConn != null)
			{
				if(sqlConn.State != ConnectionState.Closed)
					sqlConn.Close();

				sqlConn.Dispose();
				sqlConn = null;
			}
		}

		#endregion

		#region Reference Numbers

		/// <summary>
		/// Obtains the next number in sequence, updates the
		/// database if necessary.
		/// </summary>
		/// <returns></returns>
		public static int GetRefNumInt()
		{
			lock(typeof(SqlHelper))  // Lock SqlHelper Type at this stage.
			{
				// Read next batch if required...
				if((refNumberCur == 0) || (refNumberCur == refNumberMax))
				{
					SqlHelper sql = new SqlHelper();
					SqlTransaction tx = null;

					refNumberMax = refNumberCur = -2; // For completeness in case of exceptions.

					try
					{
						sql.ConnOpen(SqlHelperDatabase.DefaultDB);
						tx = sql.sqlConn.BeginTransaction(IsolationLevel.ReadCommitted);
						sql.Execute(qryUpdate, tx); // Update and lock then read the new value.
						refNumberMax = (int) sql.GetScalar(qrySelect, tx);
						refNumberCur = refNumberMax - 100;
						tx.Commit();
						tx = null;
					}
					finally
					{
						if(tx != null)
							tx.Rollback();
						sql.ConnClose();
					}
				} // if allocating a new seuquence block

				return refNumberCur++;
			} // lock(typeof(SqlHelper))
		}

		public static string GetRefNumStr()
		{
			return FormatRef(GetRefNumInt());
		}

		/// <summary>
		/// Converts the number, int, to the required string format.
		/// </summary>
		/// <param name="numberToFormat"></param>
		/// <returns></returns>
		public static string FormatRef(int numberToFormat)
		{
			string nullFormat = "0000-0000-0000-0000", refNumber, refLeft;
			NumberFormatInfo nfi = new NumberFormatInfo();

			nfi.NumberGroupSeparator = "-";
			nfi.NumberGroupSizes = new int[] {4};
			nfi.NumberDecimalDigits = 0;

			refNumber = numberToFormat.ToString("n", nfi);
			refLeft = nullFormat.Substring(0, nullFormat.Length - refNumber.Length);
			return  refLeft + refNumber;
		}

		/// <summary>
		/// Used internally by GetReference methods. This function was required
		/// to support database transactions..
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <param name="tx"></param>
		/// <returns></returns>
		private Object GetScalar(string sqlQuery, SqlTransaction tx)
		{
			SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
			sqlCmd.CommandType = CommandType.Text;
			sqlCmd.Transaction = tx;
			return sqlCmd.ExecuteScalar();
		}

		/// <summary>
		/// Used internally by GetReference methods. This function was required
		/// to support database transactions..
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <param name="tx"></param>
		/// <returns></returns>
		private int Execute(string sqlQuery, SqlTransaction tx)
		{
			SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
			sqlCmd.CommandType = CommandType.Text;
			sqlCmd.Transaction = tx;
			return sqlCmd.ExecuteNonQuery();
		}

		#endregion

		#region Connection Methods

		/// <summary>
		/// Opens a connection to the database.
		/// </summary>
		public void ConnOpen(SqlHelperDatabase dbToOpen)
		{
			// string strPropName = DBPropertyServiceNames[(int) dbToOpen];
			string strPropName = dbToOpen.ToString();
			IPropertyProvider currProps = Properties.Current;
            string connectionString = currProps[strPropName];
            ConnOpen(connectionString);
		}

        internal void ConnOpen(string connectionString)
        {
            sqlConn.ConnectionString = connectionString;
            sqlConn.Open();
        }

		/// <summary>
		/// Opens connection and returns the transaction object back to the caller.
		/// </summary>
		/// <param name="dbToOpen">Database to Open</param>
		/// <returns></returns>
		public SqlTransaction GetTransaction(SqlHelperDatabase dbToOpen)
		{
			ConnOpen(dbToOpen);
			return sqlConn.BeginTransaction(); 	
		}

		/// <summary>
		/// Closes the database connection.
		/// </summary>
		public void ConnClose()
		{
			sqlConn.Close();
		}

		public bool ConnIsOpen
		{
			get { return sqlConn.State != ConnectionState.Closed; }
		}

		#endregion

		#region Query Methods

		/// <summary>
		/// Executes an SQL statement.
		/// You must close the Reader when you have finished with it.
		/// </summary>
		/// <param name="sqlQuery">The query to execute.</param>
		/// <returns>SqlDataReader, could have zero rows in it.</returns>
		public SqlDataReader GetReader(string sqlQuery)
		{
			SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
			sqlCmd.CommandType = CommandType.Text;
			return sqlCmd.ExecuteReader(CommandBehavior.SingleResult);
		}

		/// <summary>
		/// Executes a stored procedure that returns rows.
		/// You must close the Reader when you have finished with it.
		/// </summary>
		/// <param name="storedProcName">The name of the store procedure to execute.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <returns>SqlDataReader, could have zero rows in it.</returns>
		public SqlDataReader GetReader(string storedProcName, Hashtable htParameters)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
				sqlCmd.Parameters.AddWithValue((string) enumPars.Key, enumPars.Value);
			}

			return sqlCmd.ExecuteReader();
		}

		/// <summary>
		/// Executes a stored procedure that returns rows.
		/// You must close the Reader when you have finished with it.
		/// </summary>
		/// <param name="storedProcName">The name of the store procedure to execute.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <param name="htTypes">A hashtable containing the correct types using the parameter name as key</param>
		/// <returns>SqlDataReader, could have zero rows in it.</returns>
		public SqlDataReader GetReader(string storedProcName, Hashtable htParameters, Hashtable htTypes)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
				SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
				param.Value = enumPars.Value;
				sqlCmd.Parameters.Add(param);
			}

			return sqlCmd.ExecuteReader();
		}

        /// <summary>
        /// Executes a stored procedure that returns rows.
        /// You must close the Reader when you have finished with it.
        /// </summary>
        /// <param name="storedProcName">The name of the store procedure to execute.</param>
        /// <param name="htParameters">Associative array of read-only parameters.</param>
        /// <param name="htTypes">A hashtable containing the correct types using the parameter name as key</param>
        /// <param name="timeout">Connection timeout in seconds</param>
        /// <returns>SqlDataReader, could have zero rows in it.</returns>
        public SqlDataReader GetReader(string storedProcName, Hashtable htParameters, Hashtable htTypes, int timeout)
        {
            SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
            sqlCmd.CommandTimeout = timeout;
            IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            while (enumPars.MoveNext())
            {
                SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
                param.Value = enumPars.Value;
                sqlCmd.Parameters.Add(param);
            }

            return sqlCmd.ExecuteReader();
        }

        /// <summary>
        /// Executes a stored procedure that returns rows.
        /// You must close the Reader when you have finished with it.
        /// </summary>
        /// <param name="storedProcName">The name of the store procedure to execute.</param>
        /// <param name="htParameters">Associative array of read-only parameters.</param>
        /// <param name="htTypes">A hashtable containing the correct types using the parameter name as key</param>
        /// <param name="paramReturnValue">The sql return parameter - will not give a value until the reader is closed</param>
        /// <returns>SqlDataReader, could have zero rows in it.</returns>
        public SqlDataReader GetReader(string storedProcName, Hashtable htParameters, Hashtable htTypes, SqlParameter paramReturnValue)
        {
            SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
            IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            while (enumPars.MoveNext())
            {
                SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
                param.Value = enumPars.Value;
                sqlCmd.Parameters.Add(param);
            }

            if (paramReturnValue != null)
            {
                sqlCmd.Parameters.Add(paramReturnValue);
            }

            return sqlCmd.ExecuteReader();
        }

        /// <summary>
		/// Executes a stored procedure and uses a data adapter to return a dataset.
		/// </summary>
		/// <param name="storedProcName">The name of the store procedure to execute.</param>
		/// <returns>DataSet</returns>
		public DataSet GetDataSet(string storedProcName)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			sqlCmd.CommandType = CommandType.StoredProcedure;

			//Create an SqlDataAdapter using the command object
			SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

			//create a new dataset
			DataSet dataSet = new DataSet();
			//fill the dataset
			dataAdapter.Fill(dataSet, storedProcName);

			//Return the dataset
			return dataSet;
		}


		/// <summary>
		/// Executes a stored procedure and uses a data adapter to return a dataset.
		/// </summary>
		/// <param name="storedProcName">The name of the store procedure to execute.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <returns>DataSet</returns>
		public DataSet GetDataSet(string storedProcName, Hashtable htParameters)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while (enumPars.MoveNext())
			{
                sqlCmd.Parameters.AddWithValue((string)enumPars.Key, enumPars.Value);
			}

			//Create an SqlDataAdapter using the command object
			SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

			//create a new dataset
			DataSet dataSet = new DataSet();
			//fill the dataset
			dataAdapter.Fill(dataSet, storedProcName);

			//Return the dataset
			return dataSet;
		}

		/// <summary>
		/// Executes a stored procedure and uses a data adapter to return a dataset.
		/// </summary>
		/// <param name="storedProcName">The name of the store procedure to execute.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <param name="htTypes">A hashtable containing the correct types using the parameter name as key</param>
		/// <returns>DataSet</returns>
		public DataSet GetDataSet(string storedProcName, Hashtable htParameters, Hashtable htTypes)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
				SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
				param.Value = enumPars.Value;
				sqlCmd.Parameters.Add(param);
			}

			//Create an SqlDataAdapter using the command object
			SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);

			//create a new dataset
			DataSet dataSet = new DataSet();
			//fill the dataset
			dataAdapter.Fill(dataSet, storedProcName);

			//Return the dataset
			return dataSet;
		}

		/// <summary>
		/// Executes an SQL statement that returns a scalar, eg COUNT(*)
		/// </summary>
		/// <param name="sqlQuery">The query to execute.</param>
		/// <returns>Object that will need to be cast to appropriate type.</returns>
		public Object GetScalar(string sqlQuery)
		{
			SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
			sqlCmd.CommandType = CommandType.Text;
			return sqlCmd.ExecuteScalar();
		}

		/// <summary>
		/// Executes an stored procedure that returns a scalar, eg COUNT(*)
		/// </summary>
		/// <param name="storedProcName">Stored procedure name.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <returns>Object that will need to be cast to appropriate type.</returns>
		public Object GetScalar(string storedProcName, Hashtable htParameters)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
                sqlCmd.Parameters.AddWithValue((string)enumPars.Key, enumPars.Value);
			}

			return sqlCmd.ExecuteScalar();
		}

		/// <summary>
		/// Executes an stored procedure that returns a scalar, eg COUNT(*)
		/// </summary>
		/// <param name="storedProcName">Stored procedure name.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
 		/// <param name="htTypes">An hashtable containing types mapped to keys</param>
		/// <returns>Object that will need to be cast to appropriate type.</returns>
		public Object GetScalar(string storedProcName, Hashtable htParameters, Hashtable htTypes)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
				SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
				param.Value = enumPars.Value;
				sqlCmd.Parameters.Add(param);
			}

			return sqlCmd.ExecuteScalar();
		}

        /// <summary>
        /// Executes an stored procedure that returns a xmlReader object
        /// </summary>
        /// <param name="storedProcName">Stored procedure name.</param>
        /// <param name="htParameters">Associative array of read-only parameters.</param>
        /// <param name="htTypes">An hashtable containing types mapped to keys</param>
        /// <returns>XmlReader Object</returns>
        public XmlReader GetXMLReader(string storedProcName, Hashtable htParameters, Hashtable htTypes)
        {
            SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
            IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            while (enumPars.MoveNext())
            {
                SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
                param.Value = enumPars.Value;
                sqlCmd.Parameters.Add(param);
            }

            return sqlCmd.ExecuteXmlReader();
        }

		/// <summary>
		/// Executes an update query.
		/// </summary>
		/// <param name="sqlQuery">A query that updates the database. Usually INSERT or UPDATE statement.</param>
		/// <returns>The number of rows affected.</returns>
		public int Execute(string sqlQuery)
		{
			SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
			sqlCmd.CommandType = CommandType.Text;
			return sqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes a stored procedure.
		/// </summary>
		/// <param name="storedProcName">Stored procedure name.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <returns>The number of rows affected.</returns>
		public int Execute(string storedProcName, Hashtable htParameters)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
                sqlCmd.Parameters.AddWithValue((string)enumPars.Key, enumPars.Value);
			}

			return sqlCmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Executes a stored procedure with given types.
		/// </summary>
		/// <param name="storedProcName">Stored procedure name.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <param name="htTypes">An hashtable containing types mapped to keys</param>
		/// <returns>The number of rows affected.</returns>
		public int Execute(string storedProcName, Hashtable htParameters, Hashtable htTypes)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			
			return ExecuteCmd(sqlCmd, htParameters, htTypes);
		}

        /// <summary>
        /// Executes a stored procedure with given types.
        /// </summary>
        /// <param name="storedProcName">Stored procedure name.</param>
        /// <param name="htParameters">Associative array of read-only parameters.</param>
        /// <param name="htTypes">An hashtable containing types mapped to keys</param>
        /// <param name="returnValue">The sql return parameter - will not give a value until the reader is closed</param>
        /// <returns>The number of rows affected.</returns>
        public int Execute(string storedProcName, Hashtable htParameters, Hashtable htTypes, SqlParameter paramReturnValue)
        {
            SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);

            return ExecuteCmd(sqlCmd, htParameters, htTypes, paramReturnValue);
        }

		/// <summary>
		/// Executes a stored procedure with given types.
		/// </summary>
		/// <param name="storedProcName">Stored procedure name.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <param name="htTypes">An hashtable containing types mapped to keys</param>
		/// <param name="commandTimeOut">Command time out value</param>
		/// <returns>The number of rows affected.</returns>
		public int Execute(string storedProcName, Hashtable htParameters, Hashtable htTypes, int commandTimeOut)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			sqlCmd.CommandTimeout = commandTimeOut;
			
			return ExecuteCmd(sqlCmd, htParameters, htTypes);
		}

		/// <summary>
		/// Executes SQL Command
		/// </summary>
		/// <param name="sqlCmd">SQL Command object to execute.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <param name="htTypes">An hashtable containing types mapped to keys.</param>
		/// <returns>The number of rows affected.</returns>
		private int ExecuteCmd(SqlCommand sqlCmd, Hashtable htParameters, Hashtable htTypes)
		{
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
				SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
				param.Value = enumPars.Value;
				sqlCmd.Parameters.Add(param);
			}

			return sqlCmd.ExecuteNonQuery();
		}

        /// <summary>
        /// Executes SQL Command
        /// </summary>
        /// <param name="sqlCmd">SQL Command object to execute.</param>
        /// <param name="htParameters">Associative array of read-only parameters.</param>
        /// <param name="htTypes">An hashtable containing types mapped to keys.</param>
        /// <param name="paramReturnValue">The return value.</param>
        /// <returns>The number of rows affected.</returns>
        private int ExecuteCmd(SqlCommand sqlCmd, Hashtable htParameters, Hashtable htTypes, SqlParameter paramReturnValue)
        {
            IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            while (enumPars.MoveNext())
            {
                SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
                param.Value = enumPars.Value;
                sqlCmd.Parameters.Add(param);
            }

            if (paramReturnValue != null)
            {
                sqlCmd.Parameters.Add(paramReturnValue);
            }

            return sqlCmd.ExecuteNonQuery();
        }

		/// <summary>
		/// Executes a stored procedure with given types and within the given transaction.
		/// </summary>
		/// <param name="storedProcName">Stored procedure name.</param>
		/// <param name="htParameters">Associative array of read-only parameters.</param>
		/// <param name="htTypes">An hashtable containing types mapped to keys</param>
		/// <param name="sqlTransaction">Transaction object</param>
		/// <returns>The number of rows affected/status.</returns>
		public int Execute(string storedProcName, Hashtable htParameters, Hashtable htTypes, SqlTransaction sqlTransaction)
		{
			SqlCommand sqlCmd = new SqlCommand(storedProcName, sqlConn);
			IDictionaryEnumerator enumPars = htParameters.GetEnumerator();
			sqlCmd.CommandType = CommandType.StoredProcedure;

			while( enumPars.MoveNext() )
			{
				SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
				param.Value = enumPars.Value;
				sqlCmd.Parameters.Add(param);
			}
			sqlCmd.Transaction = sqlTransaction;
			return sqlCmd.ExecuteNonQuery();
		}
		
		#endregion
	}
}
