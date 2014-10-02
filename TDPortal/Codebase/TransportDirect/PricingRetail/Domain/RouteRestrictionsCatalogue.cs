// *********************************************** 
// NAME             : RouteRestrictionsCatalogue.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 16 Nov 2010
// DESCRIPTION  	: Route Restriction Information provider. The class is created to stop Fares UI page calling
//                    SQL server every time to get the route restriction information. 
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RouteRestrictionsCatalogue.cs-arc  $
//
//   Rev 1.1   Nov 18 2010 09:56:36   apatel
//Initial Revision - Added header and comments
//Resolution for 5639: Fares page breaks with connection time out errors
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Collections;
using System.Data.SqlClient;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
    /// <summary>
    /// Implements a catalogue that holds details for route restriction information
    /// </summary>
    public class RouteRestrictionsCatalogue : IServiceFactory
    {

        #region Private members

        /// <summary>
        /// Stored proc that will be used to load the route restrictions
        /// </summary>
        private const string storedProcName = "GetAllRouteRestrictions";

        /// <summary>
        /// Hashtable holding a hashtable of route restrictions. Keys are
        /// values of type RouteCode
        /// </summary>
        private Hashtable restrictions;

        #endregion

        #region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sourceDB">The database containing the route restriction table/stored proc</param>
        public RouteRestrictionsCatalogue(SqlHelperDatabase sourceDB)
		{
			LoadData(sourceDB);
        }

		#endregion

        #region IServiceFactory Members
        /// <summary>
        /// Implementation of IServiceFactory.Get. Returns a reference to this object
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            return this;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Returns route restriction description for route code
        /// </summary>
        /// <param name="routeCode"></param>
        /// <returns></returns>
        public string GetRouteRestrictionForCode(string routeCode)
        {
            if (restrictions.ContainsKey(routeCode))
            {
                return restrictions[routeCode].ToString();
            }

            return string.Empty;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads all of the route restrictions data into the hashtables
        /// </summary>
        /// <param name="sourceDB"></param>
        private void LoadData(SqlHelperDatabase sourceDB)
        {
            // Lock the object whilst loading
            lock (this)
            {
                // Create the helper and initialise objects
                SqlHelper helper = new SqlHelper();
                restrictions = new Hashtable();

                SqlDataReader reader = null;

                try
                {
                    // Open connection and get a DataReader
                    helper.ConnOpen(sourceDB);
                    
                    reader = helper.GetReader(storedProcName);
                    // Vars to hold the data from the datareader
                    string code;
                    string description;

                    // Loop through the data and create discount card object for each record
                    while (reader.Read())
                    {
                        code = reader.GetString(0);
                        description = reader.GetString(1);

                        restrictions.Add(code, description);
                    }
                }
                finally
                {
                    reader.Close();
                    helper.ConnClose();
                }
            }
        }
        #endregion
    }
}
