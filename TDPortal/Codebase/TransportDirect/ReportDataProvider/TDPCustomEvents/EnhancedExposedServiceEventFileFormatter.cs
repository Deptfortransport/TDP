// *********************************************** 
// NAME                 : EnhancedExposedServiceEventFileFormatter.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 17/11/2005
// DESCRIPTION  : Fileformatter for Enhanced Exposed Web Services Framework Events
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/EnhancedExposedServiceEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:20   mturner
//Initial revision.
//
//   Rev 1.6   Feb 02 2006 11:55:22   schand
//Code Review Changes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Jan 10 2006 16:07:58   schand
//printing operation type as well
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Nov 28 2005 14:09:00   rgreenwood
//TD106 FX Cop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Nov 25 2005 14:42:16   rgreenwood
//Typo correction
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Nov 25 2005 14:36:52   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon
//
//   Rev 1.1   Nov 22 2005 16:23:12   rgreenwood
//Changed using statement to match changed namespace for EnhancedExposedServices
//
//   Rev 1.0   Nov 21 2005 13:13:34   rgreenwood
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.EnhancedExposedServices.Common;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Fileformatter for Enhanced Exposed Web Services Framework Events
	/// </summary>
	public class EnhancedExposedServiceEventFileFormatter :IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
	

		public EnhancedExposedServiceEventFileFormatter()
		{
		}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			string output = string.Empty;

			ExposedServiceContext sRefContext;

			if(logEvent is EnhancedExposedServiceStartEvent)
			{
				EnhancedExposedServiceStartEvent e = (EnhancedExposedServiceStartEvent)logEvent;
				
				 sRefContext = e.ServiceReferenceContext;

				output = String.Format(Messages.EnhancedExposedServicesStartEventFileFormat,
					e.EventTime.ToString(dateTimeFormat),
					sRefContext.PartnerId.ToString(),
					sRefContext.InternalTransactionId.ToString(),
					sRefContext.ExternalTransactionId.ToString(),
					sRefContext.ServiceType.ToString(),
					sRefContext.OperationType.ToString(),
					e.Successful.ToString());
									  
			}

			else if (logEvent is EnhancedExposedServiceFinishEvent)
			{
				EnhancedExposedServiceFinishEvent e = (EnhancedExposedServiceFinishEvent)logEvent;

				sRefContext = e.ServiceReferenceContext;

				output = String.Format(Messages.EnhancedExposedServicesFinishEventFileFormat,
					e.EventTime.ToString(dateTimeFormat),
					sRefContext.PartnerId.ToString(),
					sRefContext.InternalTransactionId.ToString(),
					sRefContext.ExternalTransactionId.ToString(),
					sRefContext.ServiceType.ToString(),
					sRefContext.OperationType.ToString(),
					e.Successful.ToString());

			}

			return output;
		}
	}
}
