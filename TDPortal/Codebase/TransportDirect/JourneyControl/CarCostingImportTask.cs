// ***********************************************
// NAME 		: CarCostingImportTask.cs
// AUTHOR 		: Rich Broddle
// DATE CREATED : 11/05/2010
// DESCRIPTION 	: Provides the necessary overrides of DatasourceImportTask methods
//              : to run an import of the car cost and fuel consumption data provided
//              : by MDS
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CarCostingImportTask.cs-arc  $  
//
//   Rev 1.1   May 11 2010 15:38:26   RBroddle
//Added header information
//

using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Xml;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Summary description for CarCostingImportTask class.
    /// </summary>
    public class CarCostingImportTask : DatasourceImportTask
    {
        private const string importName = "carcosting";

        #region Constructor
        /// <summary>
        /// Constructor calls base contructor 
        /// </summary>
        public CarCostingImportTask(string feed, string params1, string params2, string utility,
            string processingDirectory)
            : base(feed, params1, params2, utility, processingDirectory)
        {
            xmlschemaLocationKey = string.Format(CultureInfo.CurrentCulture, xmlschemaLocationKey, importName);
            xmlNamespaceKey = string.Format(CultureInfo.CurrentCulture, xmlNamespaceKey, importName);
            databaseKey = string.Format(CultureInfo.CurrentCulture, databaseKey, importName);
            storedProcedureKey = string.Format(CultureInfo.CurrentCulture, storedProcedureKey, importName);
            commandTimeOutKey = string.Format(commandTimeOutKey, importName);

            if ((dataFeed == null) || (!dataFeed.Equals(Properties.Current["datagateway.sqlimport.carcosting.feedname"])))
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "CarCostingImportTask unexpected feed name: [" + dataFeed + "]"));
                throw new TDException("CarCostingImportTask unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.DGUnexpectedFeedName);
            }
        }
        #endregion

        #region Overridden protected methods
        /// <summary>
        /// Override of LogStart method
        /// </summary>
        protected override void LogStart()
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
                "Car costing update begun for feed " + dataFeed));
        }
        /// <summary>
        /// Override of LogFinish method
        /// </summary>
        protected override void LogFinish(int retCode)
        {
            if (retCode != 0)
            {
                // Log failure
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    retCode.ToString() + ": Car costing update failed for feed " + dataFeed));
            }
            else
            {
                // Log success
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
                    "Car costing Import update succeeded for feed " + dataFeed));
            }
        }

        #endregion
    }
}
