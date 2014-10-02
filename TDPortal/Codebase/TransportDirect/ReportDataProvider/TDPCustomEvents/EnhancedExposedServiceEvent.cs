// *********************************************** 
// NAME                 : ExposedServicesEvent.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 17/11/2005
// DESCRIPTION  : Logging Event for Enhanced Exposed Web Services Framework
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/EnhancedExposedServiceEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:18   mturner
//Initial revision.
//
//   Rev 1.2   Nov 25 2005 14:22:56   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon, removed unnecessary assignment.
//
//   Rev 1.1   Nov 22 2005 16:23:12   rgreenwood
//Changed using statement to match changed namespace for EnhancedExposedServices
//
//   Rev 1.0   Nov 21 2005 13:13:34   rgreenwood
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

using TransportDirect.Common.Logging;
using TransportDirect.EnhancedExposedServices.Common;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for EnhancedExposedServiceEvent.
	/// </summary>
	/// 
	[Serializable]
	public class EnhancedExposedServiceEvent : CustomEvent
	{

		private string sToken = string.Empty;
		private DateTime eventTime;
		private bool successful;
		private string internalRefID;
		private ExposedServiceContext serviceReferenceContext;
		private IEventFormatter defaultFormatter = null;
		private static EnhancedExposedServiceEventFileFormatter fileFormatter = new EnhancedExposedServiceEventFileFormatter();
		

		public EnhancedExposedServiceEvent()
		{
			//Generate a unique internal reference ID
			internalRefID = System.Guid.NewGuid().ToString();;
		}


		#region Properties

		/// <summary>
		/// Read-only property. date and time event was submitted
		/// </summary>
		public DateTime EventTime
		{
			get { return eventTime; }
			set { eventTime = (DateTime)value; }
		}
	

		/// <summary>
		/// Read-only property. Was the call to the exposed services successful.
		/// </summary>
		public bool Successful
		{
			get { return successful; }
			set { successful = value; }
		}

		/// <summary>
		/// Read-only property. Was the call to the exposed services successful.
		/// </summary>
		public ExposedServiceContext ServiceReferenceContext
		{
			get { return serviceReferenceContext; }
			set { serviceReferenceContext = value; }
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}

		/// <summary>
		/// Provides an event formatting for publishing to email.
		/// </summary>
		///
		override public IEventFormatter EmailFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to event logs
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to console.
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			get {return defaultFormatter;}
		}

		#endregion
	}
}
