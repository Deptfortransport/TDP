// *********************************************** 
// NAME             : DateTimeHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 15 Aug 2013
// DESCRIPTION  	: DateTime helper methods class
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;

namespace TDP.Common.Web
{
    /// <summary>
    /// DateTime helper methods class
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// Sets the default start and end dates either using the Games date range 
        /// or the configured number of months
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public static void SetDefaultDateRange(ref DateTime startDate, ref DateTime endDate, bool setStartDayToday)
        {
            bool useGamesDateRange = Properties.Current["JourneyPlanner.Dates.UseGamesDateRange.Switch"].Parse(true);

            // Set the start to end dates to display for the calendar. This will be the period allowed on the site
            if (useGamesDateRange)
            {
                startDate = Properties.Current["JourneyPlanner.Validate.Games.StartDate"].Parse(DateTime.Now.Date);
                endDate = Properties.Current["JourneyPlanner.Validate.Games.EndDate"].Parse(DateTime.Now.Date);
            }
            else
            {
                // Otherwise determine how many months to show (including today's month)
                int numberOfMonths = Properties.Current["JourneyPlanner.Dates.NumberOfMonthsToShow.Count"].Parse(3);

                DateTime now = DateTime.Now;

                if (setStartDayToday)
                {
                    startDate = new DateTime(now.Year, now.Month, now.Day);
                }
                else
                {
                    startDate = new DateTime(now.Year, now.Month, 1);
                }

                // Go to the end day of the month
                DateTime end = startDate.AddMonths(numberOfMonths - 1);
                end = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));

                endDate = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
            }
        }

        /// <summary>
        /// Sets the default time for the provided date using the configured value
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dateTime"></param>
        public static void SetDefaultTimeForDate(DateTime date, ref DateTime dateTime, bool isReturn)
        {
            // Get the configured default selected time value
            string[] timeparts = isReturn ?
                Properties.Current["EventDateControl.DropDownTime.Return.Default"].Split(new char[] { ':' }) :
                Properties.Current["EventDateControl.DropDownTime.Outward.Default"].Split(new char[] { ':' });

            int minute = timeparts[1].Parse(0);
            int hour = timeparts[0].Parse(0);

            dateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        /// <summary>
        /// Gets the datetime rounded to the nearest minute
        /// </summary>
        public static DateTime GetRoundedTime(DateTime dateTime, int minuteInterval)
        {
            // Roundup to the nearest minute interval
            TimeSpan ts = TimeSpan.FromMinutes(minuteInterval);

            DateTime selectedTime = new DateTime(((dateTime.Ticks + ts.Ticks - 1) / ts.Ticks) * ts.Ticks);

            return selectedTime;
        }
        
        /// <summary>
        /// Gets the datetime rounded to the nearest minute
        /// </summary>
        public static DateTime GetRoundedTime(DateTime dateTime)
        {
            int minuteInterval = Properties.Current["EventDateControl.Time.Default.RoundedUpMinutes"].Parse(15);

            return GetRoundedTime(dateTime, minuteInterval);
        }
    }
}
