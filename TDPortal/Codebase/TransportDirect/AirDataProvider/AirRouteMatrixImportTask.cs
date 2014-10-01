// *********************************************** 
// NAME			: AirRouteMatrixImportTask.cs
// AUTHOR		: Atos Origin
// DATE CREATED	: 18/05/2004
// DESCRIPTION	: Processes the air route summary information genereated as part
// of the air schedule feed.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirRouteMatrixImportTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:22   mturner
//Initial revision.
//
//   Rev 1.0   Jun 09 2004 17:41:48   CHosegood
//Initial revision.
//
//   Rev 1.0   May 18 2004 18:42:40   CHosegood
//Initial revision.

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Data Gateway import task invoked via the Data Gateway framework(AirScheduleImportTask).
	/// Processes the air route summary information genereated as part of the
	/// air schedule feed and store it in the database.
	/// </summary>
	public class AirRouteMatrixImportTask : DatasourceImportTask
	{
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">Parameters passed to the task</param>
        /// <param name="params2">Parameters passed to the task</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
        public AirRouteMatrixImportTask(string feed, string params1, string params2, string utility,
            string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
        {
            xmlschemaLocationKey = string.Format( xmlschemaLocationKey, "airroutes");
            xmlNamespaceKey = string.Format( xmlNamespaceKey, "airroutes");
            databaseKey = string.Format( databaseKey, "airroutes");
            storedProcedureKey = string.Format( storedProcedureKey, "airroutes");

            if ( !dataFeed.Equals( Properties.Current["datagateway.airbackend.routematrix.feedname"] ) ) 
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "AirRouteMatrixImport unexpected feed name: [" + dataFeed + "]"));
                throw new TDException("AirRouteMatrixImport unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.DGUnexpectedFeedName );
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
                "Air Schedule update begun for feed "+dataFeed));			
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
                    retCode.ToString()+ ": Air Schedule update failed for feed " +dataFeed));
            }
            else
            {
                // Log success
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                    "Air Schedule update succeeded for feed " +dataFeed));			
            }
        }
        #endregion
	}
}