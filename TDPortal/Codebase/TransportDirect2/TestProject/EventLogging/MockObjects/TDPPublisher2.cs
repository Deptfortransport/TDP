using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;
using TDP.Common;

namespace TDP.TestProject.EventLogging.MockObjects
{
    /// <summary>
    /// Test custom event publisher 
    /// used in testing TDPTraceSwitch/listener
    /// </summary>
    class TDPPublisher2: IEventPublisher
	{
		private string identifier;
		
		public string Identifier
		{
			get {return identifier;}
		}

		public void WriteEvent(LogEvent logEvent)
		{
			throw new TDPException("mock write error", false, TDPExceptionIdentifier.ELSCustomEmailPublisherWritingEvent);
		}

        public TDPPublisher2(string identifier)
		{
			this.identifier = identifier;
		}
	}
}
