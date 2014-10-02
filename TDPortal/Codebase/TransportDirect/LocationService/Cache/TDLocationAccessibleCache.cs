// *********************************************** 
// NAME                 : TDLocationAccessibleCache.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2012 
// DESCRIPTION          : Helper class to provide methods to obtain 
//                      : Accessible Location Information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/Cache/TDLocationAccessibleCache.cs-arc  $ 
//
//   Rev 1.4   Apr 24 2013 11:19:30   mmodi
//Code review comments
//
//   Rev 1.3   Jan 04 2013 15:33:00   mmodi
//Backed out populating locality and gridreference for accessible location
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Dec 18 2012 16:53:36   DLane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 06 2012 09:10:14   mmodi
//Updated to not use static accessible locations cache class
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 05 2012 14:27:56   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using System.Data.SqlClient;
using System.Collections;

namespace TransportDirect.UserPortal.LocationService.Cache
{
    /// <summary>
    /// TDLocationAccessibleCache helper class to provide methods to obtain 
    /// Accessible Location Information
    /// </summary>
    internal class TDLocationAccessibleCache
    {
        #region Private Constants

        private const string DISTRICT_ALL = "ALL";
        
        #endregion

        #region Private members

        private readonly object dataInitialisedLock = new object();
        private bool dataInitialised = false;

        // Accessible Locations
        private List<TDLocationAccessible> accessibleList = new List<TDLocationAccessible>();

        // Accessible Admin Areas
        private List<TDAccessibleAdminArea> adminAreaList = new List<TDAccessibleAdminArea>();

        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        internal TDLocationAccessibleCache()
        {
            LoadAccessibleData();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Populates the Accessible locations cache by retrieveing the source data from the database.
        /// </summary>
        private void PopulateAccessibleLocations()
        {
            // Build Accessible Locations List
            using (SqlHelper helper = new SqlHelper())
            {
                // Temp lists before assigning to the static lists
                List<TDLocationAccessible> tmpAccessibleList = new List<TDLocationAccessible>();

                try
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Accessible stations location data"));

                    #region Load Accessible stations

                    helper.ConnOpen(SqlHelperDatabase.AtosAdditionalDataDB);

                    Hashtable paramList = new Hashtable();
                    
                    using (SqlDataReader listDR = helper.GetReader("GetAccessibleLocations", paramList))
                    {
                        while (listDR.Read())
                        {
                            tmpAccessibleList.Add(
                                new TDLocationAccessible(
                                        listDR["StopName"].ToString(),
                                        listDR["StopNaptan"].ToString().ToUpper(),
                                        TDStopTypeHelper.GetTDStopType(listDR["StopType"].ToString()),
                                        Convert.ToBoolean(listDR["WheelchairAccess"]),
                                        Convert.ToBoolean(listDR["AssistanceService"]),
                                        listDR["StopOperator"].ToString(),
                                        listDR["StopCountry"].ToString(),
                                        listDR["AdministrativeAreaCode"].ToString(),
                                        listDR["NPTGDistrictCode"].ToString(),
                                        0
                                    ));
                        }
                    }

                    // Assign to static list
                    accessibleList = tmpAccessibleList;

                    #endregion

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, string.Format("Accessible stations location in cache [{0}]", accessibleList.Count)));
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Accessible stations location data completed"));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Accessible stations location data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                }
            }
        }

        /// <summary>
        /// Populates the Accessible admin areas cache by retrieving the source data from the database.
        /// </summary>
        private void PopulateAccessibleAdminAreas()
        {
            // Build Accessible Admin Areas List
            using (SqlHelper helper = new SqlHelper())
            {
                // Temp lists before assigning to the static lists
                List<TDAccessibleAdminArea> tmpAccessibleList = new List<TDAccessibleAdminArea>();

                try
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Accessible admin areas data"));

                    #region Load Accessible stations

                    helper.ConnOpen(SqlHelperDatabase.AtosAdditionalDataDB);

                    Hashtable paramList = new Hashtable();

                    using (SqlDataReader listDR = helper.GetReader("GetAccessibleAdminAreas", paramList))
                    {
                        while (listDR.Read())
                        {
                            string adminAreaCode = listDR["AdministrativeAreaCode"].ToString();
                            string districtCode = listDR["DistrictCode"].ToString();

                            if (!string.IsNullOrEmpty(districtCode))
                            {
                                if (districtCode.ToUpper().Trim().Equals(DISTRICT_ALL))
                                {
                                    districtCode = DISTRICT_ALL;
                                }
                            }

                            // Add to the temp list
                            tmpAccessibleList.Add(
                                new TDAccessibleAdminArea(
                                    adminAreaCode.Trim(),
                                    districtCode.Trim(),
                                    Convert.ToBoolean(listDR["StepFree"]),
                                    Convert.ToBoolean(listDR["Assistance"])
                                    ));
                        }
                    }

                    // Assign to static list
                    adminAreaList = tmpAccessibleList;

                    #endregion

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, string.Format("Accessible Admin Areas in cache [{0}]", adminAreaList.Count)));
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Accessible admin areas data completed"));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Accessible admin areas data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                }
            }
        }

        #endregion
        
        #region Query Methods

        /// <summary>
        /// Returns true is the location NaPTAN is found in the Accessible stations list and has the required Accessible attributes
        /// </summary>
        /// <param name="naptan"></param>
        /// <returns></returns>
        internal bool IsAccessible(string naptan, bool stepFreeAccess, bool assistanceAvailable)
        {
            TDLocationAccessible result = accessibleList.Find(delegate(TDLocationAccessible loc) { return loc.ID == naptan; });
            if (result != null)
            {
                if (stepFreeAccess && assistanceAvailable)
                {
                    return result.StepFreeAccess && result.SpecialAssistance; 
                }
                else if (stepFreeAccess)
                {
                    return result.StepFreeAccess;
                }
                else if (assistanceAvailable)
                {
                    return result.SpecialAssistance;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true is the admin area and district code are found in the Accessible admin areas list 
        /// and has the required Accessible attributes
        /// </summary>
        /// <param name="adminAreaCode">Admin Area code</param>
        /// <param name="districtCode">District code</param>
        /// <param name="stepFreeAccess">Check step free access available in admin area/district</param>
        /// <param name="assistanceAvailable">Check assistance available in admin area/district</param>
        /// <returns></returns>
        internal bool IsAccessibleAdminArea(string adminAreaCode, string districtCode, bool stepFreeAccess, bool assistanceAvailable)
        {
            // Find matching on both admin area and district code
            TDAccessibleAdminArea result = adminAreaList.Find(delegate(TDAccessibleAdminArea AccessibleAdminArea) 
                { return (AccessibleAdminArea.AdministrativeAreaCode == adminAreaCode)
                          && (AccessibleAdminArea.DistrictCode == districtCode); });

            // If no result found, check for admin area and the "All" district code
            if (result == null)
            {
                result = adminAreaList.Find(delegate(TDAccessibleAdminArea AccessibleAdminArea)
                {
                    return (AccessibleAdminArea.AdministrativeAreaCode == adminAreaCode)
                            && (AccessibleAdminArea.DistrictCode == DISTRICT_ALL);
                });
            }

            // Check for Accessible attributes
            if (result != null)
            {
                if (stepFreeAccess && assistanceAvailable)
                {
                    return result.StepFreeAccess && result.AssistanceAvailable;
                }
                else if (stepFreeAccess)
                {
                    return result.StepFreeAccess;
                }
                else if (assistanceAvailable)
                {
                    return result.AssistanceAvailable;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a list of all accessible locations
        /// </summary>
        /// <returns>SJPAccessibleLocation list for Accessible stations</returns>
        internal List<TDLocationAccessible> GetAccessibleList()
        {
            return accessibleList;
        }

        /// <summary>
        /// Loads all Accessible data
        /// </summary>
        internal void LoadAccessibleData()
        {
            // Make load threadsafe
            if (!dataInitialised)
            {
                lock (dataInitialisedLock)
                {
                    if (!dataInitialised)
                    {
                        // Load data
                        PopulateAccessibleLocations();
                        PopulateAccessibleAdminAreas();

                        // Set to true here (prevents repeated attempts to load if it fails)
                        dataInitialised = true;
                    }
                }
            }
        }

        #endregion
    }
}
