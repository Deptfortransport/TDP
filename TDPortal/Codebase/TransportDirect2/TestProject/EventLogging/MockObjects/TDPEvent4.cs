using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;

namespace TDP.TestProject.EventLogging.MockObjects
{
    [Serializable]
    class TDPEvent4: CustomEvent
	{
		
		public TDPEvent4() : base()
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

