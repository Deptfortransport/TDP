// *********************************************** 
// NAME			: CycleAttributes.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/09/2008
// DESCRIPTION	: Class which provides the collection of CycleAttributes which can be displayed on a page.
//              : The UI will compare the attributes returned for a cycle journey against this collection,
//              : to determine if and what text it can display for the cycle journey attribute.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CycleAttributes.cs-arc  $
//
//   Rev 1.3   Oct 10 2012 14:26:10   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.2   Nov 05 2010 10:24:34   apatel
//updated to correct the cycle attributes information returned based on cycle attribute type and group
//Resolution for 5631: Cycle UI and EES web service shows wrong attribute description
//
//   Rev 1.1   Sep 29 2010 11:26:08   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.0   Oct 10 2008 15:31:34   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// CycleAttributes class
    /// </summary>
    public class CycleAttributes : ICycleAttributes
    {
        #region Private members 

        // Name of the DataChangeNotificationGroup used for detecting refreshing of Cycle Attributes data
        private const string dataChangeNotificationGroup = "CycleAttribute";

        // Boolean used to store whether an event handler with the data change notification service has been registered
        private bool receivingChangeNotifications;

        // Hashtable to store Cycle attribute data
        private Hashtable cycleAttributesCache = new Hashtable();

        // Hashtable to store Cycle infrastructure attribute data (used to control display of cycle path icon)
        private Hashtable cycleInfrastructureAttributesCache = new Hashtable();

        // Hashtable to store Cycle recommended route attribute data (used to control display of cycle route icon)
        private Hashtable cycleRecommendedAttributesCache = new Hashtable();
              
        // Variables to hold column headers for data
        private int cycleattributeIdColumnOrdinal;
        private int cycleattributeTypeColumnOrdinal;
        private int cycleattributeGroupColumnOrdinal;
        private int cycleattributeCategoryColumnOrdinal;
        private int cycleattributeResourceNameColumnOrdinal;
        private int cycleattributeMaskColumnOrdinal;
        private int cycleattributeCycleInfrastructureColumnOrdinal;
        private int cycleattributeCycleRecommendedColumnOrdinal;

        #endregion

        #region Constructor

		/// <summary>
        /// CycleAttributes Constructor
		/// </summary>
        public CycleAttributes()
		{
			LoadData();
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion

        #region Private methods
        /// <summary>
        /// Registers an event handler with the data change notification service
        /// </summary>
        private bool RegisterForChangeNotification()
        {
            IDataChangeNotification notificationService;
            try
            {
                notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
            }
            catch (TDException e)
            {
                // If the SDInvalidKey TDException is thrown, return false as the notification service
                // hasn't been initialised.
                // Otherwise, rethrow the exception that was received.
                if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising CycleAttributes"));
                    return false;
                }
                else
                    throw;
            }
            catch
            {
                // Non-CLS-compliant exception
                throw;
            }

            notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
            return true;
        }

        /// <summary>
        /// Loads the data and performs pre processing
        /// </summary>
        private void LoadData()
        {
            Hashtable localCycleAttributesCache = new Hashtable();
            Hashtable localCycleInfrastructureAttributesCache = new Hashtable();
            Hashtable localCycleRecommendedAttributesCache = new Hashtable();

            SqlHelper helper = new SqlHelper();

            try
            {
                // Initialise a SqlHelper and connect to the database.
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Verbose, "Loading CycleAttributes data started"));
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                #region Load cycle attributes into hashtables
                //Retrieve the reference data into a flat table of results
                SqlDataReader reader = helper.GetReader("GetCycleAttributes", localCycleAttributesCache, new Hashtable());

                //Assign the column ordinals
                cycleattributeIdColumnOrdinal = reader.GetOrdinal("CycleAttributeId");
                cycleattributeTypeColumnOrdinal = reader.GetOrdinal("Type");
                cycleattributeGroupColumnOrdinal = reader.GetOrdinal("Group");
                cycleattributeCategoryColumnOrdinal = reader.GetOrdinal("Category");
                cycleattributeResourceNameColumnOrdinal = reader.GetOrdinal("ResourceName");
                cycleattributeMaskColumnOrdinal = reader.GetOrdinal("Mask");
                cycleattributeCycleInfrastructureColumnOrdinal = reader.GetOrdinal("CycleInfrastructure");
                cycleattributeCycleRecommendedColumnOrdinal = reader.GetOrdinal("CycleRecommendedRoute");

                while (reader.Read())
                {
                    //Read in each cycle attribute
                    int attributeId = reader.GetInt32(cycleattributeIdColumnOrdinal);
                    string attributeType = reader.GetString(cycleattributeTypeColumnOrdinal);
                    string attributeGroup = reader.GetString(cycleattributeGroupColumnOrdinal);
                    string attributeCategory = reader.GetString(cycleattributeCategoryColumnOrdinal);
                    string attributeResourceName = reader.GetString(cycleattributeResourceNameColumnOrdinal);
                    Int64 attributeMask = Convert.ToInt64(reader.GetString(cycleattributeMaskColumnOrdinal), 16);
                    bool attributeCycleInfrastructure = reader.GetBoolean(cycleattributeCycleInfrastructureColumnOrdinal);
                    bool attributeCycleRecommended = reader.GetBoolean(cycleattributeCycleRecommendedColumnOrdinal);

                    CycleAttributeType cycleAttributeType = GetCycleAttributeType(attributeType);
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(attributeGroup);
                    CycleAttributeCategory cycleAttributeCategory = GetCycleAttributeCategory(attributeCategory);

                    CycleAttribute cycleAttribute = new CycleAttribute(attributeId, cycleAttributeType, cycleAttributeGroup, cycleAttributeCategory, attributeResourceName, attributeMask);

                    // Add to the local caches
                    localCycleAttributesCache.Add(attributeId, cycleAttribute);

                    if (attributeCycleInfrastructure)
                    {
                        localCycleInfrastructureAttributesCache.Add(attributeId, cycleAttribute);
                    }

                    if (attributeCycleRecommended)
                    {
                        localCycleRecommendedAttributesCache.Add(attributeId, cycleAttribute);
                    }
                }

                reader.Close();

                #region Update the hashtables
                Hashtable oldTable = new Hashtable();

                //Create reference to the existing cycleAttributesCache hashtable prior to replacing it				
                //This is for multi-threading purposes to ensure there is always data to reference
                if (cycleAttributesCache != null)
                {
                    oldTable = cycleAttributesCache;
                }

                //replace the cycleAttributesCache hashtable
                cycleAttributesCache = localCycleAttributesCache;

                //clear the old data storage hashtable
                oldTable.Clear();

                
                // CycleInfrastructure cache
                if (cycleInfrastructureAttributesCache != null)
                {
                    oldTable = cycleInfrastructureAttributesCache;
                }

                cycleInfrastructureAttributesCache = localCycleInfrastructureAttributesCache;

                oldTable.Clear();


                // CycleRecommended cache
                if (cycleRecommendedAttributesCache != null)
                {
                    oldTable = cycleRecommendedAttributesCache;
                }

                cycleRecommendedAttributesCache = localCycleRecommendedAttributesCache;

                oldTable.Clear();
                
                #endregion

                #endregion

                // Record the fact that the data was loaded.
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Loading CycleAttributes data completed"));
                }

            }
            catch (Exception e)
            {
                // Catching the base Exception class because we don't want any possibility
                // of this raising any errors outside of the class in case it causes the
                // application to fall over. As long as the exception doesn't occur in 
                // the final block of code, which copies the new data into the module-level
                // hashtables and arraylists, the object will still be internally consistant,
                // although the data will be inconsistant with that stored in the database.
                // One exception to this: if this is the first time LoadData has been run,
                // the exception should be raised.
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the CycleAttributes data.", e));
                if ((cycleAttributesCache == null) || (cycleAttributesCache.Count == 0))
                {
                    throw;
                }
            }
            finally
            {
                //close the database connection
                if (helper.ConnIsOpen)
                    helper.ConnClose();
            }
        }

        #region Helpers
        /// <summary>
        /// Method which parses a string into a CycleAttributeType.
        /// </summary>
        /// <param name="attributeGroup">string attributeType</param>
        /// <returns>CycleAttributeType.None if unable to parse</returns>
        private CycleAttributeType GetCycleAttributeType(string attributeType)
        {
            CycleAttributeType cycleAttributeType = CycleAttributeType.None;

            try
            {
                if (!string.IsNullOrEmpty(attributeType))
                {
                    cycleAttributeType = (CycleAttributeType)Enum.Parse(typeof(CycleAttributeType), attributeType);
                }
            }
            catch
            {
                // If the attribute group could be parsed, log exception

                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Error occurred attempting to parse a CycleAttributeType for string: " + attributeType));
            }

            return cycleAttributeType;
        }

        /// <summary>
        /// Method which parses a string into a CycleAttributeGroup.
        /// </summary>
        /// <param name="attributeGroup">string attributeGroup</param>
        /// <returns>CycleAttributeGroup.None if unable to parse</returns>
        private CycleAttributeGroup GetCycleAttributeGroup(string attributeGroup)
        {
            CycleAttributeGroup cycleAttributeGroup = CycleAttributeGroup.None;

            try
            {
                if (!string.IsNullOrEmpty(attributeGroup))
                {
                    cycleAttributeGroup = (CycleAttributeGroup)Enum.Parse(typeof(CycleAttributeGroup), attributeGroup);
                }
            }
            catch
            {
                // If the attribute group could be parsed, log exception

                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Error occurred attempting to parse a CycleAttributeGroup for string: " + attributeGroup));
            }

            return cycleAttributeGroup;
        }

        /// <summary>
        /// Method which parses a string into a CycleAttributeCategory.
        /// </summary>
        /// <param name="attributeCategory">string attributeCategory</param>
        /// <returns>CycleAttributeCategory.None if unable to parse</returns>
        private CycleAttributeCategory GetCycleAttributeCategory(string attributeCategory)
        {
            CycleAttributeCategory cycleAttributeCategory = CycleAttributeCategory.None;

            try
            {
                if (!string.IsNullOrEmpty(attributeCategory))
                {
                    cycleAttributeCategory = (CycleAttributeCategory)Enum.Parse(typeof(CycleAttributeCategory), attributeCategory);
                }
            }
            catch
            {
                // If the attribute group could be parsed, log exception

                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Error occurred attempting to parse a CycleAttributeCategory for string: " + attributeCategory));
            }

            return cycleAttributeCategory;
        }
        #endregion

        #endregion

        #region Event handler

        /// <summary>
        /// Used by the Data Change Notification service to reload the data if it is changed in the DB
        /// </summary>
        private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
        {
            if (e.GroupId == dataChangeNotificationGroup)
                LoadData();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an array of all CycleAttribute objects
        /// </summary>
        /// <returns>CycleAttribute[]</returns>
        public CycleAttribute[] GetCycleAttributes(CycleAttributeType cycleAttributeType, CycleAttributeGroup cycleAttributeGroup)
        {
            ArrayList cycleAttributeArray = new ArrayList();

            if ((cycleAttributesCache != null) && (cycleAttributesCache.Count > 0))
            {
                foreach (DictionaryEntry de in cycleAttributesCache)
                {
                    CycleAttribute cycleAttribute = (CycleAttribute)de.Value;
                    if ((cycleAttribute.CycleAttributeType == cycleAttributeType) &&
                            (cycleAttribute.CycleAttributeGroup == cycleAttributeGroup))
                    {
                        cycleAttributeArray.Add(cycleAttribute);
                    }
                    
                }
            }

            return (CycleAttribute[])cycleAttributeArray.ToArray(typeof(CycleAttribute));
        }

        /// <summary>
        /// Returns an array of all CycleAttributes 
        /// </summary>
        /// <returns>CycleAttribute[]</returns>
        public CycleAttribute[] GetCycleAttributes()
        {
            ArrayList cycleAttributeArray = new ArrayList();

            if ((cycleAttributesCache != null) && (cycleAttributesCache.Count > 0))
            {
                foreach (DictionaryEntry de in cycleAttributesCache)
                {
                    CycleAttribute cycleAttribute = (CycleAttribute)de.Value;

                    cycleAttributeArray.Add(cycleAttribute);
                    
                }
            }

            return (CycleAttribute[])cycleAttributeArray.ToArray(typeof(CycleAttribute));
        }

        /// <summary>
        /// Returns an array of CycleAttributes for the specified CycleAttributeGroup. Attributes returned will 
        /// be those attributes which have been defined as specific Cycle Infrastructure. 
        /// Can be used to determine when the Cycle (cycle path) icon is displayed
        /// </summary>
        /// <param name="cycleAttributeGroup">CycleAttributeGroup</param>
        /// <returns>CycleAttribute[]</returns>
        public CycleAttribute[] GetCycleInfrastructureAttributes(CycleAttributeGroup cycleAttributeGroup)
        {
            ArrayList cycleInfrastructureAttributeArray = new ArrayList();

            if ((cycleInfrastructureAttributesCache != null) && (cycleInfrastructureAttributesCache.Count > 0))
            {
                foreach (DictionaryEntry de in cycleInfrastructureAttributesCache)
                {
                    CycleAttribute cycleAttribute = (CycleAttribute)de.Value;

                    if (cycleAttribute.CycleAttributeGroup == cycleAttributeGroup)
                    {
                        cycleInfrastructureAttributeArray.Add(cycleAttribute);
                    }
                }
            }

            return (CycleAttribute[])cycleInfrastructureAttributeArray.ToArray(typeof(CycleAttribute));
        }

        /// <summary>
        /// Returns an array of CycleAttributes for the specified CycleAttributeGroup. Attributes returned will 
        /// be those attributes which have been defined as Recommended cycle route attributes. 
        /// Can be used to determine when the Cycle (recommended route) icon is displayed
        /// </summary>
        /// <param name="cycleAttributeGroup">CycleAttributeGroup</param>
        /// <returns>CycleAttribute[]</returns>
        public CycleAttribute[] GetCycleRecommendedAttributes(CycleAttributeGroup cycleAttributeGroup)
        {
            ArrayList cycleRecommendedAttributeArray = new ArrayList();

            if ((cycleRecommendedAttributesCache != null) && (cycleRecommendedAttributesCache.Count > 0))
            {
                foreach (DictionaryEntry de in cycleRecommendedAttributesCache)
                {
                    CycleAttribute cycleAttribute = (CycleAttribute)de.Value;

                    if (cycleAttribute.CycleAttributeGroup == cycleAttributeGroup)
                    {
                        cycleRecommendedAttributeArray.Add(cycleAttribute);
                    }
                }
            }

            return (CycleAttribute[])cycleRecommendedAttributeArray.ToArray(typeof(CycleAttribute));
        }

        #endregion
    }
}
