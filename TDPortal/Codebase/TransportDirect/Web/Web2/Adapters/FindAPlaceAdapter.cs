// ***********************************************
// NAME 		: FindAPlaceAdapter.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 15/02/2008
// ************************************************
//
//   Rev 1.0   Feb 15 2008 10:11:30   apatel
//Initial revision.
// CCN 0427 Added properties to Configure Functional Areas of FindAPlace pages.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Responsible for common functionality required by Find A Place pages.
    /// </summary>
    public class FindAPlaceAdapter
    {
        #region Static methods

        /// <summary>
        /// Static read-only property indicating if Journey Planner Location Map (Find A Map) is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool JourneyPlannerLocationMapAvailable
        {
            get 
            {
                string property = Properties.Current["JourneyPlannerLocationMapAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Traffic Map is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool TrafficMapsAvailable
        {
            get 
            {
                string property = Properties.Current["TrafficMapsAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Network Map is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool NetworkMapsAvailable
        {
            get 
            {
                string property = Properties.Current["NetworkMapsAvailable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }

        #endregion
    }
}
