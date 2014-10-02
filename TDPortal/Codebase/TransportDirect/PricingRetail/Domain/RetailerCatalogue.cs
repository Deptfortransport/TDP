// *********************************************** 
// NAME			: RetailerCatalogue.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 26/09/03
// DESCRIPTION	: This class implements a catalogue that holds details of retailers capable of 
// selling tickets for a given operator and mode of transport. 
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RetailerCatalogue.cs-arc  $
//
//   Rev 1.3   Apr 15 2010 10:58:34   mturner
//Added logic that strips leading or trailing * and : from operator codes before performing a lookup.
//Resolution for 5513: No operator link appears for NX services in Trapeze regions
//
//   Rev 1.2   Feb 02 2009 16:48:48   mmodi
//Place theme calling code in try catch to prevent error when there is no HTTPContext
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.1   Mar 10 2008 15:22:24   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:36:54   mturner
//Initial revision.
//
//   Rev 1.17   Jan 20 2006 15:15:14   RPhilpott
//Return readonly ArrayList instead of IList
//Resolution for 3484: Del 8: Server error when Buy selected on Tickets/Costs
//
//   Rev 1.16   Oct 18 2005 16:10:46   mdambrine
//the partnerlookup is being done now through a stored procedure
//Resolution for 2875: Retailer catalougue does not use stored proc.
//
//   Rev 1.15   Aug 23 2005 14:29:40   mdambrine
//Undid rev 1.14 as it did not belong in the trunk
//
//   Rev 1.14   Aug 15 2005 12:13:10   mdambrine
//Removed all other ticket retailers from the list, only GNER
//
//   Rev 1.13   Mar 02 2005 16:14:42   jgeorge
//Added small icon url property to retailer
//
//   Rev 1.12   Feb 21 2005 13:52:38   jgeorge
//Removed singleton implementation, as the single instance behaviour is implemented by inclusion into the TDServiceDiscovery framework.
//
//   Rev 1.11   Dec 23 2004 11:57:22   jgeorge
//Modified to add new property to Retailer
//
//   Rev 1.10   Apr 19 2004 15:26:50   COwczarek
//Changes to make FindRetailers work with "rail replacement bus" mode
//Resolution for 697: Bus replacement change
//
//   Rev 1.9   Nov 18 2003 16:10:08   COwczarek
//SCR#247 :Add $Log: for PVCS history

using System;
using System.Collections;
using System.Data.SqlClient;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure.Content;

using Logger = System.Diagnostics.Trace;
using TD.ThemeInfrastructure;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// This class implements a catalogue that holds details of retailers capable of 
	/// selling tickets for a given operator and mode of transport. 
	/// </summary>
    public class RetailerCatalogue
    {
        #region Private members

        /// <summary>
		/// Table of retailers. Key is instance of RetailerKey, value is instance of Retailer.
		/// </summary>
        private IDictionary retailerLookUp;

		/// <summary>
		/// Table of retailers. Key is retailer id, value is instance of Retailer.
		/// </summary>
        private IDictionary retailers;

		/// <summary>
		/// Name of the stored procedure that returns the retailers table
		/// </summary>
		private const string retailerTableStoredProc = "GetRetailers";

		/// <summary>
		/// Name of the stored procedure that returns the retailers lookup and partner relations table
		/// </summary>
		private const string retailerLookupTableStoredProc = "GetRetailerLookup";

        /// <summary>
        /// Value used to identify test retailers and load if property switch set
        /// </summary>
        private const string TestRetailer_Prefix = "TEST";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Clients should not use this constructor to create instances.
        /// Instead the Current property should be used to obtain a singleton instance.
        /// </summary>
        public RetailerCatalogue()
        {
            retailerLookUp = new Hashtable();            
            retailers = new Hashtable();
            
            LoadData();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an immutable arraylist containing 1 or more Retailer instances that are capable
        /// of selling tickets for the suppplied operator code and travel mode.
        /// Null is returned if no retailers are found or an invalid combination of parameters
        /// is used.
        /// </summary>
        /// <param name="operatorCode">The operator code. A null value indicates the operator
        /// code is not significant, in which case the mode parameter must be a rail mode</param>
        /// <param name="mode">The mode of transport. If the value is a rail mode then the operator
        /// code is not used.</param>
        /// <return>An IList of Retailer instances</return>
        
        public ArrayList FindRetailers(string operatorCode, ModeType mode) 
        {
            if (operatorCode == null || PricingUnitMergePolicy.IsRailMode(mode)) 
            {
                operatorCode = RetailerKey.IgnoreOperatorCode;
            }
            if (PricingUnitMergePolicy.IsRailMode(mode)) 
            {
                // Force mode type to be rail since retailer lookup makes no distinction
                // between different rail modes
                mode = ModeType.Rail;
            }

            // In some cases the CJP returns operator codes with leading or 
            // trailing ":" and "*" characters. These are not included in the RetailerCatalogue
            // so need to be removed before we lookup the operator.
            if (operatorCode != null)
            {
                if (operatorCode.StartsWith("*") || operatorCode.StartsWith(":"))
                {
                    operatorCode = operatorCode.Substring(1);
                }
                else if (operatorCode.EndsWith("*") || operatorCode.EndsWith(":"))
                {
                    operatorCode = operatorCode.Substring(0, operatorCode.Length - 1);
                }
            }

            RetailerKey key = new RetailerKey(operatorCode,mode);
            return (ArrayList)retailerLookUp[key];						
        }

        /// <summary>
        /// Returns a retailer for the supplied id
        /// </summary>
        /// <param name="id">The id of the required retailer</param>
        /// <return>A retailer for the supplied id or null if not found</return>
        public Retailer FindRetailer(string id) 
        {
            return (Retailer)retailers[id];
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Loads the contents of the Retailers table and returns it as a dictionary.
        /// </summary>
        /// <param name="SqlHelper">Database connection object for open connection</param>
        private void LoadRetailers(SqlHelper sqlHelper, bool loadTestRetailers) 
        {
            SqlDataReader dataReader = null;
            try 
            {
				Hashtable emptyHash = new Hashtable();
				dataReader = sqlHelper.GetReader(retailerTableStoredProc, emptyHash, emptyHash);
                while (dataReader.Read()) 
                {
                    // Parse the retailers returned
                    string id = dataReader["RetailerId"].ToString().Trim();
                    string name = dataReader["Name"].ToString().Trim();
                    string websiteURL = string.Empty;
                    string handoffURL = string.Empty;
                    string displayURL = string.Empty;
                    string phoneNumber = string.Empty;
                    string phoneNumberDisplay = string.Empty;
                    string iconURL = string.Empty;
                    string smallIconUrl = string.Empty;
                    string resourceKey = string.Empty;
                    bool allowsMTH = dataReader["AllowsMTH"].ToString().Trim() == "Y";

                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("WebsiteURL")))
                    {
                        websiteURL = dataReader["WebsiteURL"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("HandoffURL")))
                    {
                        handoffURL = dataReader["HandoffURL"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("DisplayURL")))
                    {
                        displayURL = dataReader["DisplayURL"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("PhoneNumber")))
                    {
                        phoneNumber = dataReader["PhoneNumber"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("PhoneNumberDisplay")))
                    {
                        phoneNumberDisplay = dataReader["PhoneNumberDisplay"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("IconURL")))
                    {
                        iconURL = dataReader["IconURL"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("SmallIconUrl")))
                    {
                        smallIconUrl = dataReader["SmallIconUrl"].ToString().Trim();
                    }
                    if (!dataReader.IsDBNull(dataReader.GetOrdinal("ResourceKey")))
                    {
                        resourceKey = dataReader["ResourceKey"].ToString().Trim();
                    }

                    Retailer retailer = new Retailer(id, name,
                        websiteURL, handoffURL, displayURL, 
                        phoneNumber, phoneNumberDisplay,
                        iconURL, smallIconUrl, allowsMTH,
                        resourceKey);

                    // Only load the test retailers if switch set 
                    if ((!retailer.Id.ToUpper().StartsWith(TestRetailer_Prefix))
                        ||
                        (retailer.Id.ToUpper().StartsWith(TestRetailer_Prefix) && loadTestRetailers)
                       )
                    {
                        // Add retailer to the dictionary
                        retailers.Add(id, retailer);

                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format("RetailerCatalogue - Retailer cached: {0}", retailer.ToString())));
                    }
                }
            } 
            finally
            {
                if ( dataReader != null )
                {
                    dataReader.Close();
                }
            }
        }

        /// <summary>
        /// Loads the database tables that constitute the retailer catalogue and builds the
        /// retailerLookUp dictionary. 
        /// This method reads all rows from the Retailers and RetailerLookup tables and constructs
        /// a mapping in the retailerLookUp dictionary.
        /// Each unique operator code and mode combination from the RetailerLookup table represents
        /// an entry in retailerLookUp. The value for each key (instance of RetailerKey)
        /// is a list containing 1 or more Retailer instances. This represents the n:m relationship
        /// between a single key and many retailers. One instance of Retailer will exist for each
        /// row in the Retailer table and will potentially be referenced from 1 or more lists. 
        /// </summary>
        private void LoadData() 
        {
            SqlDataReader dataReader = null;
            SqlHelper sqlHelper = new SqlHelper();
            Hashtable localRetailerLookup = new Hashtable();

            // Place get theme in a try because exception can be thrown if attempting to get Retailers
            // for a non HTTPContext app, e.g. Nunit
            string themeName;
            try
            {
                themeName = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme().Name;
            }
            catch
            {
                // No need to log error, this should not happen for the web app
                themeName = TD.ThemeInfrastructure.ThemeProvider.Instance.GetDefaultTheme().Name;
            }

            try
            {
                sqlHelper.ConnOpen( SqlHelperDatabase.DefaultDB);

                // Determine whether to show test retailers
                bool loadTestRetailers = false;

                if (!bool.TryParse(Properties.Current["Retail.Retailers.ShowTestRetailers.Switch"], out loadTestRetailers))
                    loadTestRetailers = false;

                // Load all retailers
                LoadRetailers(sqlHelper, loadTestRetailers);

                // Load lookup table
                Hashtable htParameters = new Hashtable();
                htParameters.Add("@ThemeName", themeName);
                dataReader = sqlHelper.GetReader(retailerLookupTableStoredProc, htParameters);

                while (dataReader.Read()) 
                {
                    string operCode = dataReader.GetString(0).Trim();
                    string mode = dataReader.GetString(1).Trim();
                    string retailerId = dataReader.GetString(2).Trim();
                    ModeType modeType = (ModeType)Enum.Parse(ModeType.Air.GetType(),mode);
                    RetailerKey key = new RetailerKey(operCode.Trim(),modeType);

                    // Only load the test retailers if switch set 
                    if ((!retailerId.ToUpper().StartsWith(TestRetailer_Prefix))
                        ||
                        (retailerId.ToUpper().StartsWith(TestRetailer_Prefix) && loadTestRetailers)
                       )
                    {
                        // Get list of retailers for the key
                        ArrayList retailersList = (ArrayList)localRetailerLookup[key];

                        // If this is the first occurrence of this key, create a new
                        // list to hold the retailers
                        if (retailersList == null)
                        {
                            retailersList = new ArrayList();
                            localRetailerLookup.Add(key, retailersList);
                        }

                        // Create the retailer object and add to the list 
                        Retailer retailer = (Retailer)retailers[retailerId];
                        retailersList.Add(retailer);
                    }
                }
            }
            catch( SqlException sqlEx )
            {
                string message = "SqlException : "+ sqlEx.Message;
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Database,
                    TDTraceLevel.Error,
                    message,
                    sqlEx );
                Logger.Write ( oe );
                throw new TDException( 
                    message,
                    sqlEx,
                    true,
                    TDExceptionIdentifier.PRHRetailerCatalogueSQLCommandFailed);
            }
            finally
            {
                // Release database resources
                if ( dataReader != null )
                {
                    dataReader.Close();
                }
                if ( sqlHelper.ConnIsOpen )
                {
                    sqlHelper.ConnClose();
                }
            }

            // Convert all lists in lookup table to immutable lists
            foreach(RetailerKey retailerKey in localRetailerLookup.Keys) 
            {
                ArrayList retailerList = new ArrayList((IList)localRetailerLookup[retailerKey]);
                ArrayList readOnlyList = ArrayList.ReadOnly(retailerList);
                retailerLookUp.Add(retailerKey, readOnlyList);
            }

        }

        #endregion Private methods
    }
}