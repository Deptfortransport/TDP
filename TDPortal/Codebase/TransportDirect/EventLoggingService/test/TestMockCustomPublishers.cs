// *********************************************** 
// NAME                 : TestMockCustomPublishers.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Custom Publisher classes used
// to support NUnit tests.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockCustomPublishers.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:18   mturner
//Initial revision.
//
//   Rev 1.2   Oct 07 2003 13:40:52   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.1   Jul 25 2003 14:14:58   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;

namespace TransportDirect.Common.Logging
{
	// VALID :  referenced in MockPropertiesGood
	public class TDPublisher1 : IEventPublisher
	{
		private string identifier;
		
		public string Identifier
		{
			get {return identifier;}
		}

		public void WriteEvent(LogEvent logEvent)
		{
			
		}

		public TDPublisher1(string identifier)
		{
			this.identifier = identifier;
		}
	}

	// VALID :  referenced in MockPropertiesGood
	public class TDPublisher2 : IEventPublisher
	{
		private string identifier;
		
		public string Identifier
		{
			get {return identifier;}
		}

		public void WriteEvent(LogEvent logEvent)
		{
			throw new TDException("mock write error", false, TDExceptionIdentifier.ELSCustomEmailPublisherWritingEvent);
		}

		public TDPublisher2(string identifier)
		{
			this.identifier = identifier;
		}
	}

	// Used to test errors conditions:
	// 1) Publisher not intialised with an id 
	// 2) Publisher not defined in properties is passed when initialising TDTraceListener
	public class TDPublisher3 : IEventPublisher
	{
		private string identifier;
		
		public string Identifier
		{
			get {return identifier;}
		}

		public void WriteEvent(LogEvent logEvent)
		{
			
		}

		public TDPublisher3(string identifier)
		{
			this.identifier = identifier;
		}
	}

}
