// ***********************************************
// NAME 		: DuplicateTiplocProvider.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 30-Jun-2010
// DESCRIPTION 	: DuplicateTiplocProvider class provides duplicate tiploc data
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DuplicateTiplocProvider.cs-arc  $
//
//   Rev 1.0   Jul 01 2010 12:49:04   apatel
//Initial revision.
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Data.SqlClient;
using System.Collections;

namespace TransportDirect.UserPortal.LocationService
{
    public class DuplicateTiplocProvider: IDuplicateTiplocProvider
    {
        #region Private Fields
        //Dictionary to store all the duplicate tip locs
        private List<string> duplicateTiplocs = new List<string>();

        private string storedProcName = "GetDuplicateTiplocs";
        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sourceDB">The database containing the Duplicate Tiploc data/stored proc</param>
        public DuplicateTiplocProvider(SqlHelperDatabase sourceDB)
		{
			LoadData(sourceDB);
		}
		#endregion
        
        #region IDuplicateTiplocProvider Members
        /// <summary>
        /// Checks if the naptan specified is in each duplicate tip loc comma delimited string
        /// if the matching string found the method splits the strings into an array an returns as
        /// out parameter and returns true.
        /// if matching string not exist returs false
        /// </summary>
        /// <param name="naptan">Naptan of the tiploc for which duplicate tiploc needs checking</param>
        /// <param name="duplicateTiplocsData">Out parameter returning duplicate tiploc data in the event of naptan specified matches</param>
        /// <returns>True if specified naptan exist in one of the duplicate tiploc data</returns>
        public bool HasDuplicateTiploc(string naptan, out string[] duplicateTiplocsData)
        {
            bool duplicateFound = false;

            duplicateTiplocsData = null;

            foreach (string tiplocs in duplicateTiplocs)
            {
                if (tiplocs.Contains(naptan))
                {
                    duplicateFound = true;
                    duplicateTiplocsData = tiplocs.Split(new char[]{','});
                }
            }

            return duplicateFound;
        }

        #endregion

        #region Private Methods

        // <summary>
        /// Loads Duplicate tip loc data from the data base and add in to a list
        /// </summary>
        /// <param name="sourceDB">Source Database</param>
        private void LoadData(SqlHelperDatabase sourceDB)
        {
            // Create the helper and initialise objects
            SqlHelper helper = new SqlHelper();

            duplicateTiplocs.Clear();

            // Open connection and get a DataReader
            helper.ConnOpen(sourceDB);

            SqlDataReader reader = helper.GetReader(storedProcName, new Hashtable(), new Hashtable());

            // Vars to hold the data from the datareader
            string tiplocData = string.Empty;
            
            // Assign Column Ordinals 
            int tiplocDataOrdinal = reader.GetOrdinal("tiplocData");


            // Loop through the data and add in to duplicateTiplocs list
            while (reader.Read())
            {
                tiplocData = reader.GetString(tiplocDataOrdinal);

                if (!string.IsNullOrEmpty(tiplocData))
                {
                    duplicateTiplocs.Add(tiplocData);
                }
            }

            reader.Close();
            helper.ConnClose();
        }
        #endregion
    }
}
