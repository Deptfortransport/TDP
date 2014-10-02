// *********************************************** 
// NAME                 : TravelNewsImport.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 29/09/2003 
// DESCRIPTION  : Class that receives and processes
// an XML travel news update file via the Data Gateway
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsImport.cs-arc  $
//
//   Rev 1.1   Dec 29 2008 17:01:32   RBRODDLE
//Added line to make this feed pick up the sproc timeout to allow this to be configured in the properties DB
//Resolution for 5214: Timeout values not working for some data feeds
//
//   Rev 1.0   Nov 08 2007 12:50:30   mturner
//Initial revision.
//
//   Rev 1.11   Feb 10 2005 15:04:08   jmorrissey
//Corrected error handling message that referred to Air Route Matrix import
//
//   Rev 1.10   Dec 16 2004 15:27:04   passuied
//Refactoring the TravelNews component
//
//   Rev 1.9   Jun 07 2004 14:17:02   CHosegood
//Functionality moved into new base class DatasourceImportTask.
//
//   Rev 1.8   Nov 19 2003 09:43:16   kcheung
//Added Properties namespace.
//
//   Rev 1.7   Nov 18 2003 19:17:26   JMorrissey
//Fixed clean up issues with data gateway, fixed travel news to use properties for xsd, fixed command line importer, and updated resources for data gateway.
//
//   Rev 1.6   Nov 17 2003 19:29:08   JMorrissey
//Serco feed fixed and integrated with Gateway.
//
//   Rev 1.5   Nov 17 2003 15:47:50   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.4   Oct 28 2003 12:59:52   JHaydock
//Update to use new TravelNews import file and schema. Success code also set to be 0.
//
//   Rev 1.3   Oct 09 2003 15:36:50   JMorrissey
//Updated all references to TransientData database to use TransientPortal instead
//
//   Rev 1.2   Oct 08 2003 17:21:00   JHaydock
//Completed first cut of travel news import class and test
//
//   Rev 1.1   Oct 06 2003 19:11:50   JHaydock
//Added Test class and files
//
//   Rev 1.0   Sep 29 2003 17:49:18   JMorrissey
//Initial Revision

using System;
using System.Collections;
using System.Data;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// This class performs an import for travel news data.
	/// </summary>
	public class TravelNewsImport : DatasourceImportTask
	{

        #region Constructor
        /// <summary>
        /// Constructor calls base contructor with the only required
        /// value: feed = "Travel News Import" (which is used for logging purposes)
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">Parameters passed to the task</param>
        /// <param name="params2">Parameters passed to the task</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
        public TravelNewsImport(string feed, string params1, string params2, string utility,
            string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
        {
            xmlschemaLocationKey = string.Format( xmlschemaLocationKey, "travelnews");
            xmlNamespaceKey = string.Format( xmlNamespaceKey, "travelnews");
            databaseKey = string.Format( databaseKey, "travelnews");
            storedProcedureKey = string.Format( storedProcedureKey, "travelnews");
            commandTimeOutKey = string.Format(commandTimeOutKey, "travelnews");


            if ( (dataFeed == null) || (!dataFeed.Equals( Properties.Current["datagateway.airbackend.travelnews.feedname"] ) ) ) 
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Travel News Import unexpected feed name: [" + dataFeed + "]"));
                throw new TDException("Travel News Import unexpected feed name: [" + dataFeed + "]", true, TDExceptionIdentifier.DGUnexpectedFeedName );
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
                "TravelNewsImport update begun for feed "+dataFeed));			
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
                    retCode.ToString()+ ": TravelNewsImport update failed for feed " +dataFeed));
            }
            else
            {
                // Log success
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                    "TravelNewsImport update succeeded for feed " +dataFeed));			
            }
        }
        #endregion
	}
}
