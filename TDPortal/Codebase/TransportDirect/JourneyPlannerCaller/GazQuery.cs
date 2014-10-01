// *********************************************** 
// NAME             : GazQuery.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Nov 2010
// DESCRIPTION  	: Object containing the paths for the GAZ query request/result files 
//                  : and the Gaz function to be called
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/GazQuery.cs-arc  $
//
//   Rev 1.0   Nov 30 2010 13:33:46   apatel
//Initial revision.
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// GazQuery class containing GAZ request and response file paths
    /// </summary>
    class GazQuery
    {
        #region Private Fields
        private string requestPath = string.Empty;
        private string resultPath = string.Empty;
        private string query = string.Empty;
        private string soapAction = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="query">GAZ function to call</param>
        /// <param name="requestPath">GAZ request path</param>
        /// <param name="resultPath">GAZ result file path</param>
        /// <param name="soapActionUrl">Stop action url</param>
        public GazQuery(string query, string requestPath, string resultPath, string soapActionUrl)
        {
            this.requestPath = requestPath;
            this.resultPath = resultPath;
            this.query = query;
            this.soapAction = soapActionUrl;

        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Read only. Gaz request file path
        /// </summary>
        public string RequestPath
        {
            get { return requestPath; }
        }

        /// <summary>
        /// Read only. Gaz result file path
        /// </summary>
        public string ResultPath
        {
            get { return resultPath; }
        }

        /// <summary>
        /// Read only. Gaz function to call
        /// </summary>
        public string GazFunction
        {
            get { return query; }
        }

        /// <summary>
        /// Read only. SoapAction to be amended in the Gaz web service call
        /// </summary>
        public string SoapAction
        {
            get
            {
                if (!string.IsNullOrEmpty(soapAction))
                    return soapAction;
                else
                    return string.Empty;
            }
        }

        #endregion
    }

}
