// *********************************************** 
// NAME             : AirDataProvider.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 14 Oct 2013
// DESCRIPTION  	: Provides airport data to clients
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Logger = System.Diagnostics.Trace;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace TDP.Common.DataServices.AirportData
{
    /// <summary>
    /// Provides airport data to clients
    /// </summary>
    public class AirDataProvider
    {
        #region Private members

        private List<Airport> airports = new List<Airport>();

        #endregion

        #region Constructor

        /// <summary>
        /// Data should be loaded when the item is first created
        /// </summary>
        public AirDataProvider()
        {
            LoadData();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns all Airports
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<Airport> GetAirports()
        {
            return airports.AsReadOnly();
        }

        /// <summary>
        /// Retrieve an airport object given its IATA code.
        /// Null is returned if the airport is not found.
        /// </summary>
        /// <param name="airportCode"></param>
        /// <returns></returns>
        public Airport GetAirport(string airportCode)
        {
            if (!string.IsNullOrEmpty(airportCode))
            {
                Airport result = airports.Find(delegate(Airport airport) { return airport.IATACode == airportCode; });
                return result;
            }
                        
            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the airport data
        /// </summary>
        private void LoadData()
        {
            List<Airport> tmpAirports = new List<Airport>();

            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    // Initialise a SqlHelper and connect to the database.
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Info, "Loading Airports data started"));

                    helper.ConnOpen(SqlHelperDatabase.AirDataProviderDB);

                    #region Load airports
                    
                    // Execute the GetAirports stored procedure. This returns a list of airports
                    // containing code, name and no of terminals, in alphabetical order of name.
                    // At this point, we are relying on the ArrayList maintaining the list internally
                    // in order.
                    SqlDataReader reader = helper.GetReader("GetAirports", new List<SqlParameter>());

                    //Assign the column ordinals
                    int airportCodeColumnOrdinal = reader.GetOrdinal("Code");
                    int airportNameColumnOrdinal = reader.GetOrdinal("Name");
                    int airportTerminalsColumnOrdinal = reader.GetOrdinal("Terminals");

                    while (reader.Read())
                    {
                        tmpAirports.Add(new Airport(
                            reader.GetString(airportCodeColumnOrdinal),
                            reader.GetString(airportNameColumnOrdinal),
                            reader.GetInt32(airportTerminalsColumnOrdinal)));
                    }
                    reader.Close();

                    // Assign to class list
                    this.airports = tmpAirports;

                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, string.Format("Airports data loaded into cache: Airports[{0}]", airports.Count)));
                                        
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, "Loading Airports data completed"));

                    #endregion
                }
                catch (Exception e)
                {
                    // Log failed to load airports data
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "An error occurred whilst attempting to load the Airports data.", e));
                    
                }
                finally
                {
                    //close the database connection
                    if (helper.ConnIsOpen)
                        helper.ConnClose();
                }
            }
        }

        #endregion
    }
}
