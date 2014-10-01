// *********************************************** 
// NAME             : PageTimeout.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 22 Aug 2013
// DESCRIPTION  	: PageTimeout class to hold data used in session timeout processing
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.DataServices.TimeoutData
{
    /// <summary>
    /// PageTimeout class to hold data used in session timeout processing
    /// </summary>
    public class PageTimeout
    {
        #region Private members

        private PageId pageId = PageId.Empty;
        private List<string> showTimeoutControlIds = new List<string>();
        private List<string> hideTimeoutControlIds = new List<string>();
        private List<string> allowTimeoutEventControlIds = new List<string>();
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PageTimeout()
        {
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. PageId
        /// </summary>
        public PageId PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        /// <summary>
        /// Read/Write. ShowTimeoutControlIds
        /// </summary>
        public List<string> ShowTimeoutControlIds
        {
            get { return showTimeoutControlIds; }
            set { showTimeoutControlIds = value; }
        }

        /// <summary>
        /// Read/Write. HideTimeoutControlIds
        /// </summary>
        public List<string> HideTimeoutControlIds
        {
            get { return hideTimeoutControlIds; }
            set { hideTimeoutControlIds = value; }
        }

        /// <summary>
        /// Read/Write. AllowTimeoutEventControlIds
        /// </summary>
        public List<string> AllowTimeoutEventControlIds
        {
            get { return allowTimeoutEventControlIds; }
            set { allowTimeoutEventControlIds = value; }
        }

        #endregion
    }
}
