// *********************************************** 
// NAME                 : TestMockCustomEventTwo.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : A mock custom event used
// for NUnit testing.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockCustomEventTwo.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:18   mturner
//Initial revision.
//
//   Rev 1.3   Oct 29 2003 20:20:52   geaton
//Provided default formatter rather than null!
//
//   Rev 1.2   Jul 29 2003 17:31:56   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.1   Jul 25 2003 14:14:58   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// A custom event used for NUnit testing
	/// </summary>
	[Serializable]
	public class CustomEventTwo : CustomEvent
	{
		private TDEventCategory category;
		private TDTraceLevel level;
		private string message;
		private string user;
		private long referenceNumber;

		private static DefaultFormatter defaultFormatter = new DefaultFormatter();


		private static IEventFilter filter = new OperationalEventFilter();

		override public IEventFormatter FileFormatter
		{
			get {return defaultFormatter;}
		}

		override public IEventFormatter EmailFormatter
		{
			get {return defaultFormatter;}
		}

		override public IEventFormatter EventLogFormatter
		{
			get {return defaultFormatter;}
		}

		override public IEventFormatter ConsoleFormatter
		{
			get {return defaultFormatter;}
		}

		override public IEventFilter Filter
		{
			get {return filter;}
		}

		public TDTraceLevel Level
		{
			get{return level;}
		}

		public TDEventCategory Category
		{
			get{return category;}
		}
		
		public string Message
		{
			get{return message;}
		}

		public long ReferenceNumber
		{
			get{return referenceNumber;}
		}

		public string User
		{
			get{return user;}
		}

		public CustomEventTwo(TDEventCategory category, TDTraceLevel level, string message,
			string user, long referenceNumber) : base()
		{
			this.category = category;
			this.level = level;
			this.message = message;
			this.referenceNumber = referenceNumber;
			this.user = user;
		}


	}
}
