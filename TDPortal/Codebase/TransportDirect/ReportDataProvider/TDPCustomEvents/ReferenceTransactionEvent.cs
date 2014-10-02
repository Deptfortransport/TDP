// *********************************************** 
// NAME                 : ReferenceTransactionEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// reference transaction data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/ReferenceTransactionEvent.cs-arc  $
//
//   Rev 1.1   Jun 28 2010 14:07:48   PScott
//SCR 5561 - write MachineName to reference transactions
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:39:30   mturner
//Initial revision.
//
//   Rev 1.9   Dec 02 2003 20:07:10   geaton
//Updates following addition of Successful column on ReferenceTransactionEvent table.
//
//   Rev 1.8   Nov 10 2003 12:31:22   geaton
//Removed TDTransactionCategory - a string will be used instead to allow new categories to be added at runtime.
//
//   Rev 1.7   Nov 06 2003 12:18:36   geaton
//Configured to use its own file formatter.
//
//   Rev 1.6   Sep 16 2003 11:09:30   geaton
//Added extra comments following code review.
//
//   Rev 1.5   Sep 15 2003 16:54:20   geaton
//ReferenceTransactionEvent takes DateTime instead of TDDateTime.
//
//   Rev 1.4   Sep 05 2003 08:51:38   geaton
//Requirements change - SLA flag needed.
//
//   Rev 1.3   Aug 28 2003 17:05:38   geaton
//Moved enumeration  category to separate file to follow standards.
//
//   Rev 1.2   Aug 27 2003 19:29:20   geaton
//Updated category type name.
//
//   Rev 1.1   Aug 20 2003 10:39:26   geaton
//Updated fields
//
//   Rev 1.0   Aug 18 2003 19:11:36   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{

	/// <summary>
	/// Defines the class for capturing Reference Transaction Event data.
	/// </summary>
	[Serializable]
	public class ReferenceTransactionEvent : TDPCustomEvent
	{
		private DateTime submitted;
		private string category;
		private bool serviceLevelAgreement;
		private bool successful;
        private string machineName;

		private static ReferenceTransactionEventFileFormatter fileFormatter = new ReferenceTransactionEventFileFormatter();

		
		/// <summary>
		/// Constructor for a <c>ReferenceTransactionEvent</c> class. 
		/// A <c>ReferenceTransactionEvent</c> is used
		/// to log reference transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="submitted">Date/Time that reference transaction was submitted, down to the millisecond.</param>
		/// <param name="sessionId">The session id used to perform the reference transaction.</param>
		/// <param name="category">The category of reference transaction.</param>
		/// <param name="successful">Indicates whether reference transaction returned the expected results.</param>
		/// <param name="serviceLevelAgreement">Indicates whether reference transaction is used to calculate if SLAs have been met.</param>
		public ReferenceTransactionEvent(string category,
										 bool serviceLevelAgreement,
										 DateTime submitted,
										 string sessionId,
                                         bool successful,
                                         string machineName)
            : base(sessionId, false)
		{
			this.submitted = submitted;
			this.category = category;
			this.serviceLevelAgreement = serviceLevelAgreement;
			this.successful = successful;
            this.machineName = machineName;
		}

		/// <summary>
		/// Gets the date/time at which the reference transaction was submitted.
		/// </summary>
		public DateTime Submitted
		{
			get{return submitted;}
		}

		/// <summary>
		/// Gets the event category.
		/// </summary>
		public string EventType
		{
			get{return category;}
		}

		/// <summary>
		/// Gets the SLA indicator. True if transaction is used for SLA calculations.
		/// </summary>
		public bool ServiceLevelAgreement
		{
			get{return serviceLevelAgreement;}
		}

		/// <summary>
		/// Gets the success indicator. True if transaction completed successfully.
		/// </summary>
		public bool Successful
		{
			get{return successful;}
		}



        /// <summary>
        /// Gets the MachineName of the injector service.
        /// </summary>
        public string MachineName
        {
            get { return machineName; }
        }

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	}
}


