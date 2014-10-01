﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;

namespace TDP.TestProject.EventLogging
{
    /// <summary>
    /// A File Formatter for CustomEventOne
    /// </summary>
    public class MockCustomEventFileFormatter : IEventFormatter
    {
        public MockCustomEventFileFormatter()
        {
        }

        public string AsString(LogEvent logEvent)
        {
            string output = String.Empty;

            if (logEvent is CustomEventOne)
            {
                CustomEventOne customEventOne = (CustomEventOne)logEvent;
                string tab = "\t";

                output =
                    "TDP-CustomEventOne" + tab +
                    customEventOne.Time + tab +
                    customEventOne.Message + tab +
                    customEventOne.Category + tab +
                    customEventOne.Level + tab +
                    customEventOne.ReferenceNumber;
            }
            return output;
        }
    }
}
