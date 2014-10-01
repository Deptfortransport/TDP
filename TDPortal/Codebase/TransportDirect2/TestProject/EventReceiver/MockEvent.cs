using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;
using TDP.TestProject.EventLogging.MockObjects;

namespace TDP.TestProject.EventReceiver
{
    [Serializable]
    public class MockEvent: CustomEvent
	{
		
		public MockEvent() : base()
		{
			
		}

		override public IEventFormatter FileFormatter
		{
			get {return TDP.Common.EventLogging.DefaultFormatter.Instance;}
		}

		override public IEventFormatter EmailFormatter
		{
			get {return TDP.Common.EventLogging.DefaultFormatter.Instance;}
		}

		override public IEventFormatter EventLogFormatter
		{
			get {return TDP.Common.EventLogging.DefaultFormatter.Instance;}
		}


		override public IEventFormatter ConsoleFormatter
		{
			get {return TDP.Common.EventLogging.DefaultFormatter.Instance;}
		}

	}
}
