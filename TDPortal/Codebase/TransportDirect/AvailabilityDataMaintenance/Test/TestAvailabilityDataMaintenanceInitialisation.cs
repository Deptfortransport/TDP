// *********************************************** 
// NAME			: TestAvailabilityDataMaintenanceInitialisation.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityDataMaintenanceInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/Test/TestAvailabilityDataMaintenanceInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:58   mturner
//Initial revision.
//
//   Rev 1.2   Apr 21 2005 14:38:30   jbroome
//Updated to use FilePropertyProvider
//
//   Rev 1.1   Mar 21 2005 10:56:00   jbroome
//Minor updates after code review
//
//   Rev 1.0   Feb 08 2005 10:39:32   jbroome
//Initial revision.

using System;
using System.Diagnostics;
using System.Collections;
using System.Globalization;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Class which initialises necessary services for the NUnit tests
	/// to run successfully.
	/// </summary>
	public class TestAvailabilityDataMaintenanceInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Initialises mock TD properties.
		/// </summary>
		/// <param name="serviceCache">Service cache.</param>
		public void Populate(Hashtable serviceCache)
		{
			// Enable Mock PropertyService
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// Enable the Event Logging Service
			ArrayList loggingErrors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, loggingErrors));
			}
			catch (TDException)
			{	
				// Create error message to log to default listener.
				Trace.WriteLine(String.Format(CultureInfo.CurrentCulture, Messages.Init_TDTraceListenerFailed, "Reasons follow."));
				StringBuilder message = new StringBuilder(100);
				foreach (string error in loggingErrors)
					message.Append(error + ",");
				Trace.WriteLine(message.ToString());				
				
				throw new TDException(String.Format(CultureInfo.CurrentCulture, Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.AETDTraceInitFailed);	
			}
		}

	}
}
