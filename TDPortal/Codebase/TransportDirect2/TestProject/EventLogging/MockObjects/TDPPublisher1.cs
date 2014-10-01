using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;

namespace TDP.TestProject.EventLogging.MockObjects
{
    /// <summary>
    /// Test custom event publisher 
    /// used in testing TDPTraceSwitch/listener
    /// </summary>
    class TDPPublisher1: IEventPublisher
	{
		private string identifier;
		
		public string Identifier
		{
			get {return identifier;}
		}

		public void WriteEvent(LogEvent logEvent)
		{
			
		}

        public TDPPublisher1(string identifier)
		{
			this.identifier = identifier;
		}
	}
}
