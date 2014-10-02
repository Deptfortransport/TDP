// *********************************************** 
// NAME                 : DatatGatewayEvent.cs 
// AUTHOR               : Phil Scott
// DATE CREATED         : 01/10/2003 
// DESCRIPTION  : Defines a custom event for logging
// data gateway event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/DataGatewayEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:18   mturner
//Initial revision.
//
//   Rev 1.2   Oct 29 2003 19:59:48   geaton
//Provided file formatter
//
//   Rev 1.1   Oct 03 2003 12:30:54   geaton
//Removed LoggedOn parameter from DataGatewayEvent constructor.
//
//   Rev 1.0   Oct 01 2003 11:45:00   PScott
//Initial Revision
//
using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines the class for capturing data gateway Event data.
	/// </summary>
	[Serializable]
	public class DataGatewayEvent: TDPCustomEvent
	{
		
		private string feedId;
		private string fileName;
		private DateTime timeStarted;
		private DateTime timeFinished;
		private bool successFlag;
		private int errorCode;

		private static DataGatewayEventFileFormatter fileFormatter = new DataGatewayEventFileFormatter();

		/// <summary>
		/// Constructor for a <c>DataGatewayEvent</c> class. 
		/// A <c>DataGatewayEvent</c> is used
		/// to log file movements in the data gateway 
		/// using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id on which the page was entered.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		public DataGatewayEvent(string feedId,
									string sessionId, 
									string fileName,
									DateTime timeStarted,
									DateTime timeFinished,
									bool successFlag,
									int errorCode): base(sessionId, false)
		{
			this.feedId = feedId;
			this.fileName = fileName;
			this.timeStarted = timeStarted;
			this.timeFinished = timeFinished;
			this.successFlag = successFlag;
			this.errorCode = errorCode;
		}

		
		/// <summary>
		/// Gets the feed id
		/// </summary>
		public string FeedId
		{
			get{return feedId;}
		}
		/// <summary>
		/// Gets the fileName
		/// </summary>
		public string FileName
		{
			get{return fileName;}
		}
		/// <summary>
		/// Gets the timeStarted
		/// </summary>
		public DateTime TimeStarted
		{
			get{return timeStarted;}
		}
		/// <summary>
		/// Gets the timeFinished
		/// </summary>
		public DateTime TimeFinished
		{
			get{return timeFinished;}
		}
		/// <summary>
		/// Gets the successFlag
		/// </summary>
		public bool SuccessFlag
		{
			get{return successFlag;}
		}
		/// <summary>
		/// Gets the ErrorCode
		/// </summary>
		public int ErrorCode
		{
			get{return errorCode;}
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



