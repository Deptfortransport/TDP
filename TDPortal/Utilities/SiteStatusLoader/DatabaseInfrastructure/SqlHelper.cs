// *********************************************** 
// NAME                 : SqlHelper.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Database helper class for Opening, Closing DB connections, and exposes Execute methods
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/DatabaseInfrastructure/SqlHelper.cs-arc  $
//
//   Rev 1.1   Apr 09 2009 15:06:06   mmodi
//Public method to allow connection string to be passed in for database connection
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:26:04   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.DatabaseInfrastructure
{
    /// <summary>
    /// Required by the Open() method to select a database.
    /// </summary>
    public enum SqlHelperDatabase
    {
        DefaultDB,
        ReportStagingDB
    }

    public class SqlHelper : IDisposable
    {
        #region Private members

        /// <summary>
        /// Used internally to maintain a connection. It needs to be opened
        /// explicitly and closed explicitly.
        /// </summary>
        private SqlConnection sqlConn = new SqlConnection();

        #endregion

        #region Constructor and Destroy

        /// <summary>
        /// Default Constructor
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
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();

                sqlConn.Dispose();
                sqlConn = null;
            }
        }

        #endregion

        #region Connection Methods

        /// <summary>
        /// Opens a connection to the database.
        /// </summary>
        public void ConnOpen(SqlHelperDatabase dbToOpen)
        {
            string propertyDatabase = dbToOpen.ToString();
            string connectionString = PropertyService.Instance[propertyDatabase];
            ConnOpen(connectionString);
        }

        /// <summary>
        /// Opens a connection to the database (using a connection string)
        /// </summary>
        /// <param name="connectionString"></param>
        public void ConnOpen(string connectionString)
        {
            sqlConn.ConnectionString = connectionString;
            sqlConn.Open();
        }

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        public void ConnClose()
        {
            sqlConn.Close();
        }

        /// <summary>
        /// Read only. Returns true if the database connection is open
        /// </summary>
        public bool ConnIsOpen
        {
            get { return sqlConn.State != ConnectionState.Closed; }
        }

        #endregion

        #region Execute methods

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

            while (enumPars.MoveNext())
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

            while (enumPars.MoveNext())
            {
                SqlParameter param = new SqlParameter(enumPars.Key.ToString(), (SqlDbType)htTypes[enumPars.Key]);
                param.Value = enumPars.Value;
                sqlCmd.Parameters.Add(param);
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

            while (enumPars.MoveNext())
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
