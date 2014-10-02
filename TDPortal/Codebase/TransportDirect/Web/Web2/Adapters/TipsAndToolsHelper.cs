// ***********************************************
// NAME 		: FindAPlaceAdapter.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 15/02/2008
// ************************************************
//
//   Rev 1.0   Feb 15 2008 11:23:30   apatel
//Initial revision.
// CCN 0427 Added properties to Configure Functional Areas of Tips and Tools pages.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class TipsAndToolsHelper
    {
        #region Static methods

        /// <summary>
        /// Static read-only property indicating if Tips And Tools is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool TipsAndToolsAvailable
        {
            get
            {
                string property = Properties.Current["TipsAndToolsAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Link To Website is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool LinkToWebsiteAvailable
        {
            get
            {
                string property = Properties.Current["LinkToWebsiteAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Toolbar Download is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool ToolbarDownloadAvailable
        {
            get
            {
                string property = Properties.Current["ToolbarDownloadAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Mobile Demonstrator is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool MobileDemonstratorAvailable
        {
            get
            {
                string property = Properties.Current["MobileDemonstratorAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Check Journey CO2 is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool CheckJourneyCO2Available
        {
            get
            {
                string property = Properties.Current["CheckJourneyCO2Available"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Send Feedback is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool FeedbackAvailable
        {
            get
            {
                string property = Properties.Current["FeedbackAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if BatchJourney Planner is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool BatchJourneyPlannerAvailable
        {
            get
            {
                string property = Properties.Current["BatchJourneyPlannerAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Related Sites is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool RelatedSitesAvailable
        {
            get
            {
                string property = Properties.Current["RelatedSitesAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Frequently Asked Questions is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool FAQAvailable
        {
            get
            {
                string property = Properties.Current["FAQAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Digital TV Info is 
        /// to be made available
        /// </summary>
        /// <returns>boolean</returns>
        public static bool DigitalTVInfoAvailable
        {
            get
            {
                string property = Properties.Current["DigitalTVInfoAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        #endregion

    }
}
