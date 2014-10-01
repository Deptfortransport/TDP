// *********************************************** 
// NAME                 : BatchJourneyPlannerService.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Service class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/BatchJourneyPlannerService.cs-arc  $
//
//   Rev 1.2   Feb 28 2012 15:52:32   dlane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.ServiceProcess;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace BatchJourneyPlannerService
{
    public partial class BatchJourneyPlannerService : ServiceBase
    {
        private BatchProcessor batchProcessor;
        private const string PROCESSOR_ID = "BatchJourneyPlannerService_ProcessorId";

        /// <summary>
        /// Constructor
        /// </summary>
        public BatchJourneyPlannerService()
        {
            // Set up service name - this must match the AID used in the properties, and in ProjectInstaller.cs.
            this.ServiceName = "BatchJourneyPlanner" + ConfigurationManager.AppSettings[PROCESSOR_ID];
            InitializeComponent();
            batchProcessor = null;
        }

        /// <summary>
        /// Service start
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            bool tdServicesInitialised = false;

            // Initialise TD Services.
            try
            {
                TDServiceDiscovery.Init(new BatchJourneyPlannerServiceInitialisation());
                tdServicesInitialised = true;
            }
            catch (TDException tdException)
            {
                // Log error to default trace listener in case TD Listener failed to create.
                EventLog.WriteEntry(String.Format(Messages.Init_Failed, tdException.Message), EventLogEntryType.Error, 1);
            }

            if (tdServicesInitialised)
            {
                if (TDTraceSwitch.TraceVerbose)
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Init_Completed));
            }

            //Configure the hosted remoting objects to be a remote object
            if (File.Exists(ConfigurationManager.AppSettings["RemotingConfig.File"]))
            {
                RemotingConfiguration.Configure(ConfigurationManager.AppSettings["RemotingConfig.File"], false);
            }

            if (tdServicesInitialised)
            {
                // Start processing batches...
                try
                {
                    batchProcessor = new BatchProcessor();
                    batchProcessor.Run();
                }
                catch (TDException tdException)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailedRunning, tdException.Message)));
                }
            }
        }

        /// <summary>
        /// Service stop
        /// </summary>
        protected override void OnStop()
        {
            if (batchProcessor != null)
            {
                batchProcessor.Dispose(true);
                batchProcessor = null;
            }
        }

    		/// <summary>
		/// C# Destructor.
		/// </summary>
		~BatchJourneyPlannerService()
		{
			Dispose( false );
		}
    }
}
