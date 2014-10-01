using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;

namespace TDP.TestProject.EventLogging.MockObjects
{
    [Serializable]
    class TDPEvent2: CustomEvent
	{
		private string customParameter;
		private string user;
		private long referenceNumber;
		
        public TDPEvent2(string user, long referenceNumber, string customParameter)
            : base()
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
            get { return TDP.Common.EventLogging.DefaultFormatter.Instance;}
		}

		override public IEventFormatter EmailFormatter
		{
            get { return TDP.Common.EventLogging.DefaultFormatter.Instance; }
		}

		override public IEventFormatter EventLogFormatter
		{
            get { return TDP.Common.EventLogging.DefaultFormatter.Instance; }
		}

		override public IEventFormatter ConsoleFormatter
		{
            get { return TDP.Common.EventLogging.DefaultFormatter.Instance; }
		}
	}
}
