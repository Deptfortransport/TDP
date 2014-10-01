using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;

namespace TDP.TestProject.EventLogging
{
    /// <summary>
    /// A custom event used for NUnit testing
    /// </summary>
    [Serializable]
    public class CustomEventTwo : CustomEvent
    {
        private TDPEventCategory category;
        private TDPTraceLevel level;
        private string message;
        private string user;
        private long referenceNumber;

       

        private static IEventFilter filter = new OperationalEventFilter();

        override public IEventFormatter FileFormatter
        {
            get { return DefaultFormatter; }
        }

        override public IEventFormatter EmailFormatter
        {
            get { return DefaultFormatter; }
        }

        override public IEventFormatter EventLogFormatter
        {
            get { return DefaultFormatter; }
        }

        override public IEventFormatter ConsoleFormatter
        {
            get { return DefaultFormatter; }
        }

        override public IEventFilter Filter
        {
            get { return filter; }
        }

        public TDPTraceLevel Level
        {
            get { return level; }
        }

        public TDPEventCategory Category
        {
            get { return category; }
        }

        public string Message
        {
            get { return message; }
        }

        public long ReferenceNumber
        {
            get { return referenceNumber; }
        }

        public string User
        {
            get { return user; }
        }

        public CustomEventTwo(TDPEventCategory category, TDPTraceLevel level, string message,
            string user, long referenceNumber)
            : base()
        {
            this.category = category;
            this.level = level;
            this.message = message;
            this.referenceNumber = referenceNumber;
            this.user = user;
        }


    }
}
