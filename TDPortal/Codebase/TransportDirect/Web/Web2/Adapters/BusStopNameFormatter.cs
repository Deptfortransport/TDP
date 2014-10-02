// *********************************************** 
// NAME			: BusStopNameFormatter.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 2010-07-20
// DESCRIPTION	: Encapsulates the formatting of a bus stop name
//                into a common format for display in the UI 
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/BusStopNameFormatter.cs-arc  $
//
//   Rev 1.0   Jul 20 2010 15:02:12   mturner
//Initial revision.
//Resolution for 5549: Two issues with stop names on station information page

using System;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Data;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    ///Encapsulates the formatting of a bus stop name
    ///into a common format for display in the UI 	
    ///	</summary>
    public class BusStopNameFormatter
    {
        private string NaPTAN;
        Dictionary<string, string> stopsIdentifiers = new Dictionary<string, string>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BusStopNameFormatter()
        {
        }

        /// <summary>
        /// Constructor that sets all properties of the CallingPointLine.
        /// </summary>
        /// <param name="NaPTAN">The NaPTAN of the bus stop</param>
        public BusStopNameFormatter(string NaPTAN)
        {
            this.NaPTAN = NaPTAN;
        }

        public string Format()
        {
            bool prependIdentifierFound = false;
            string tempIdentifier = string.Empty;
            string tempString = string.Empty;

            string[] stopNaptanArray = { NaPTAN };

            //Obtain GisQuery service from service discovery
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            if (stopsIdentifiers.Count == 0)
            {
                try
                {
                    SqlHelper sqlHelper = new SqlHelper();
                    sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
                    DataSet ds = sqlHelper.GetDataSet("GetIdentifiers", new Hashtable());
                    DataTable dt = ds.Tables[0];
                    DataRow[] rows = dt.Select();
                    foreach (DataRow row in rows)
                    {
                        stopsIdentifiers[row[0].ToString()] = row[1].ToString();
                    }
                }
                catch (System.Data.SqlClient.SqlException exception)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Database, TDTraceLevel.Error, "Bus Stop Name Formatter Exception." + exception.Message);

                    Logger.Write(operationalEvent);
                }
            }

            QuerySchema gisQueryResult = gisQuery.FindStopsInfoForStops(stopNaptanArray);
            QuerySchema.StopsRow stopsRow = (QuerySchema.StopsRow)gisQueryResult.Stops.Rows[0];

            LocalityNameInfo locInfo = gisQuery.GetLocalityInfoForNatGazID(stopsRow.natgazid);

            string locName = locInfo.LocalityName;
            
            if (!string.IsNullOrEmpty(locName))
            {
                locName = locName + ", ";
            }

            // Check if identifier is one that should be placed before the streetname
            switch (stopsRow.identifier)
            {
                case ("opposite"):
                case ("Opp"):
                case ("outside"):
                case ("o/s"):
                case ("adjacent"):
                case ("adj"):
                case ("near"):
                case ("nr"):
                case ("behind"):
                case ("inside"):
                case ("in"):
                case ("by"):
                case ("just before"):
                case ("just after"):
                case ("corner"):
                case ("cnr"):
                case ("corner of"):
                case ("at"):
                    {
                        prependIdentifierFound = true;
                        break;
                    }
                default:
                    {
                        prependIdentifierFound = false;
                        break;
                    }
            }

            // Check if identifier is in list of idenitifiers that need to be replaced 
            // with a more user friendly string. If it is perform the replacement.
            if (stopsIdentifiers != null && stopsIdentifiers.ContainsKey(stopsRow.identifier))
            {
                tempIdentifier = stopsIdentifiers[stopsRow.identifier];
            }
            else
            {
                // use original identifier
                tempIdentifier = stopsRow.identifier;
            }

            if (prependIdentifierFound)
            {
                tempString = locName + tempIdentifier + " " + stopsRow.commonname + " (On " + stopsRow.street + ")";
            }
            else
            {
                tempString = locName + stopsRow.commonname + ", " + tempIdentifier + " (On " + stopsRow.street + ")";
            }

            // Remove certain banned sub strings frrom the text displayed to an end user.
            StringBuilder sb = new StringBuilder(tempString);
            sb.Replace("-", null);
            sb.Replace("*", null);
            sb.Replace("_", null);
            sb.Replace("N/A", null);
            sb.Replace("TBA", null);
            sb.Replace("TBC", null);
            sb.Replace("?", null);
            sb.Replace("/", null);
            sb.Replace("/\\", null);
            sb.Replace("No Name", null);
            //If the entire first field was made up of banned chars remove the comma delimiter
            sb.Replace(", ", null, 0, 2);
            //If the entire last field was made up of banned chars remove the delimiter
            sb.Replace(" (On )", null, sb.Length - 6, 6);
            return sb.ToString();
         }
    }
}