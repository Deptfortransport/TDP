// *********************************************** 
// NAME             : WMIMonitoringItem.cs      
// AUTHOR           : Rich Broddle
// DATE CREATED     : 11th April 2011
// DESCRIPTION  	: Implementation of WMIMonitoringItem class
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using TDP.Common;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using System.Data.SqlClient;
using Logger = System.Diagnostics.Trace;
using System.Security;
using System.Security.Permissions;

namespace TDP.UserPortal.CCAgent
{
    public class WMIMonitoringItem : MonitoringItem
    {


        private string wQLQuery;
        private string wqlFieldToUse;


        /// <summary>
        /// Read/Write.The WMI Query Language (WQL) statement to run to obtain the required WMI information
        /// </summary>
        /// <remarks></remarks>
        public string WQLQuery
        {
            get
            {
                return wQLQuery;
            }
            set
            {
                wQLQuery = value;
            }
        }

        public WMIMonitoringItem(int MonitoringItemID, int CheckInterval, string WQLQuery, string Description, string RedCondition)
        {
            monitoringItemID = MonitoringItemID;
            checkInterval = CheckInterval;
            wQLQuery = WQLQuery;
            description = Description;
            redCondition = RedCondition;

            //Grab the bit between "Select" and "From" - that's the field name we'll need to extract for the output
            wqlFieldToUse = wQLQuery.Substring(6, wQLQuery.IndexOf("FROM") - 7).Trim();

        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.ControlThread)]
        public override void ReCheck(SqlHelper helper)
        {
            try
            {
                //Get the WMI measure
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wQLQuery))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        this.ValueAtLastCheck = queryObj[wqlFieldToUse].ToString();
                    }
                }
                //Capture the datetime for this check
                lastCheckTime = DateTime.Now;

                //Write it to the DB
                #region AddWMIResult
                List<SqlParameter> paramList = new List<SqlParameter>();
                SqlParameter MonitoringItemID = new SqlParameter("MonitoringItemID", (System.Data.SqlTypes.SqlInt32)monitoringItemID);
                SqlParameter TDP_Server = new SqlParameter("TDP_Server", (System.Data.SqlTypes.SqlString)System.Environment.MachineName);
                SqlParameter Description = new SqlParameter("Description", (System.Data.SqlTypes.SqlString)description);
                SqlParameter CheckTime = new SqlParameter("CheckTime", (System.Data.SqlTypes.SqlDateTime)lastCheckTime);
                SqlParameter ValueAtCheck = new SqlParameter("ValueAtCheck", (System.Data.SqlTypes.SqlString)valueAtLastCheck);
                SqlParameter ItemIsInRed = new SqlParameter("IsInRed", (System.Data.SqlTypes.SqlBoolean)IsInRed);
                paramList.Add(MonitoringItemID);
                paramList.Add(TDP_Server);
                paramList.Add(Description);
                paramList.Add(CheckTime);
                paramList.Add(ValueAtCheck);
                paramList.Add(ItemIsInRed);

                if (helper.Execute("InsertWMIMonitoringResult", paramList) != 1)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                        "CCAgent - Could not add WMI monitoring item result with description " + this.Description));
                }
            }
            catch (ManagementException e)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                "An error occurred while querying for WMI measure: " + this.Description + " - " + e.Message));
            }
            #endregion
        }
    }
}
