// *********************************************** 
// NAME                 : TestMockCustomEvents.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Custom Event classes used to
// support NUnit tests.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockCustomEvents.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:18   mturner
//Initial revision.
//
//   Rev 1.4   Oct 29 2003 20:41:00   geaton
//Provided defaultformatters to mock events.
//
//   Rev 1.3   Oct 09 2003 13:11:42   geaton
//Support for error handling should an unknown event class be logged.
//
//   Rev 1.2   Jul 29 2003 17:31:54   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.1   Jul 25 2003 14:14:56   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;

namespace TransportDirect.Common.Logging
{
	[Serializable]
	public class TDEvent1 : CustomEvent
	{
		private int customParameter;
		private string user;
		private long referenceNumber;
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();

		public TDEvent1(string user, long referenceNumber, int customParameter) : base()
		{
			this.customParameter = customParameter;
			this.referenceNumber = referenceNumber;
			this.user = user;
		}

		public long ReferenceNumber
		{
			get{return referenceNumber;}
		}

		public string User
		{
			get{return user;}
		}

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

	}

	[Serializable]
	public class TDEvent2 : CustomEvent
	{
		private string customParameter;
		private string user;
		private long referenceNumber;
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();

		public TDEvent2(string user, long referenceNumber, string customParameter) : base()
		{
			this.customParameter = customParameter;
			this.referenceNumber = referenceNumber;
			this.user = user;
		}

		public long ReferenceNumber
		{
			get{return referenceNumber;}
		}

		public string User
		{
			get{return user;}
		}

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
	}

	[Serializable]
	public class TDEvent3
	{
	}

	[Serializable]
	public class TDEvent4 : CustomEvent
	{
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();
		
		public TDEvent4() : base()
		{
			
		}

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

	}
}
