// *********************************************** 
// NAME                 : TimeSpanParser.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Customer TimeSpan parser used to parse a time value read in from the Third Party status data
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/TimeSpanParser.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:23:42   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.Common
{
    /// <summary>
    /// Parses a string of time in seconds.fractions to a TimeSpan using parsing rules.
    /// Expects the string to be like "99.99 s";
    /// </summary>
    public static class TimeSpanParser
    {
        /// <summary>
        /// Get a Timespan from fractional seconds. 
        /// </summary>
        /// <param name="value">e.g. "99.99 s"</param>
        public static TimeSpan GetValue(string value)
        {
            return ParseTimeSpan(value);
        }

        private static TimeSpan ParseTimeSpan(string value)
        {
            TimeSpan timeSpan = new TimeSpan();

            try
            {
                string timeSpanString = value;

                // remove the "s" value
                timeSpanString = timeSpanString.Replace("s", "");

                // remove any spaces
                timeSpanString = timeSpanString.Replace(" ", "");

                // convert the string into fractional seconds
                double timeSpanSeconds = Convert.ToDouble(timeSpanString);

                // convert into a timespan
                timeSpan = TimeSpan.FromSeconds(timeSpanSeconds);

                return timeSpan;
            }
            catch
            {
                return timeSpan;
            }
        }
    }
}
