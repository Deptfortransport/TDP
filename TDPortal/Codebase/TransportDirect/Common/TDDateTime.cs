// *********************************************** 
// NAME                 : TDDateTime.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 14/07/2003 
// DESCRIPTION  : 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDDateTime.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:06   mturner
//Initial revision.
//
//   Rev 1.18   Apr 19 2006 12:39:20   mtillett
//Correct the implicit conversion operator for DateTime to TDDateTime
//Resolution for 3923: DN091 Travel News Updates: Incident dates and times are incorrect in the table view
//
//   Rev 1.17   Aug 18 2005 11:29:04   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.16.1.0   Jul 01 2005 11:09:26   jmorrissey
//Added TDMonthYear property
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.16   Apr 01 2005 16:46:28   jbroome
//Added AreSameDate and GetDifferenceDates methods
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.15   Mar 15 2005 17:54:54   jmorrissey
//Added new methods to return the difference between 2 dates in hours, mins or seconds
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.14   Jun 18 2004 15:01:48   asinclair
//Fix for IR 1033
//
//   Rev 1.13   Oct 07 2003 11:33:00   JTOOR
//Modified properties Year, Month, Day, Hour, Minute, Second and Millisecond to support correct proxy generation with wsdl.
//
//   Rev 1.12   Sep 18 2003 12:04:12   passuied
//added implementations for AddDays and AddMinutes
//
//   Rev 1.11   Sep 11 2003 16:33:56   jcotton
//Made Class Serializable
//
//   Rev 1.10   Sep 11 2003 13:56:54   cshillan
//Included comparison operators for <, >, <=, >=
//
//   Rev 1.9   Sep 09 2003 14:51:14   PNorell
//Changed constructor to take into effect the changing of the reference date.
//
//   Rev 1.8   Sep 03 2003 10:14:32   jtoor
//Inserted default constructor.
//Required to enable reflection.
//
//   Rev 1.7   Aug 21 2003 16:42:56   adow
//Add implicit conversion docmentation
//
//   Rev 1.6   Aug 21 2003 13:02:10   adow
//implicit conversion and AddMonths methods added
//
//   Rev 1.5   Aug 20 2003 11:53:52   PNorell
//Week day will use the appropriate system enum as return value.
//
//   Rev 1.4   Aug 18 2003 17:55:00   geaton
//Added constructor to take milliseconds. (This was required for storing report data.)
//
//   Rev 1.3   Jul 23 2003 11:45:08   alole
//Updated to include a CultureInfo in the call to the Parse method
//
//   Rev 1.2   Jul 22 2003 16:39:48   ALole
//Changed all reference to Thread.CurrentThread.CurrentCulture to Thread.CurrentThread.CurrentUICulture
//
//   Rev 1.1   Jul 18 2003 11:40:10   ALole
//Added comments.
//Updated Fromat to take a CultureInfo

using System;
using System.Threading;
using System.Globalization;

namespace TransportDirect.Common
{
	/// <summary>
	/// TDDateTime class.
	/// </summary>
	[Serializable()]
	public class TDDateTime
	{
		// DateTime variable which provides most of the functionality for the class
		private DateTime dtDateTime;

		// TDDateTime variable to hold a 'fixed' Time/Date for the Now method
		static private TDDateTime tddtNow = null;

		// standard format for any requests based on CurrentCulture
		private CultureInfo sFormat = null;
		
		/// <summary>
		/// Default constructor.
		/// Sets the internal DateTime object to the current date time.
		/// </summary>
		public TDDateTime()
		{
			dtDateTime = TDDateTime.Now.GetDateTime();
		}		

		/// <summary>
		/// Constructor for Date only
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="day"></param>
		public TDDateTime(int year, int month, int day)
		{
			dtDateTime = new DateTime(year, month, day);
		}

		/// <summary>
		/// TDDateTime "less than" relational operator that returns true if the first operand is less than the second, false
		/// </summary>
		/// <param name="op1">First operand</param>
		/// <param name="op2">Second operand</param>
		/// <returns></returns>
		static public bool operator < (TDDateTime op1, TDDateTime op2)
		{
			// Use underlying DateTime's less than operator to determine if op1 < op2
			if( op1.dtDateTime < op2.dtDateTime )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// TDDateTime "greater than" relational operator that returns true if the first operand is greater than the second, false
		/// </summary>
		/// <param name="op1">First operand</param>
		/// <param name="op2">Second operand</param>
		/// <returns></returns>
		static public bool operator > (TDDateTime op1, TDDateTime op2)
		{
			// Use underlying DateTime's greater than operator to determine if op1 > op2
			if( op1.dtDateTime > op2.dtDateTime )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// TDDateTime "less than or equal" relational operator that returns true if the first operand is less than or equal the second, false
		/// </summary>
		/// <param name="op1">First operand</param>
		/// <param name="op2">Second operand</param>
		/// <returns></returns>
		static public bool operator <= (TDDateTime op1, TDDateTime op2)
		{
			// Use underlying DateTime's less than operator to determine if op1 <= op2
			if( op1.dtDateTime <= op2.dtDateTime )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// TDDateTime "greater than or equal" relational operator that returns true if the first operand is greater than or equal to the second, false
		/// </summary>
		/// <param name="op1">First operand</param>
		/// <param name="op2">Second operand</param>
		/// <returns></returns>
		static public bool operator >= (TDDateTime op1, TDDateTime op2)
		{
			// Use underlying DateTime's greater than operator to determine if op1 >= op2
			if( op1.dtDateTime >= op2.dtDateTime )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Implicit conversion to TDDateTime
		/// </summary>
		/// <param name="value"> the DateTime value to convert from</param>
		static public implicit operator TDDateTime(DateTime value)
		{
			return new TDDateTime(value);
		}

		
		/// <summary>
		/// Constructor for Date and Time down to a second
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		public TDDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			dtDateTime = new DateTime(year, month, day, hour, minute, second);
		}

		/// <summary>
		/// Constructor for Date and Time down to a millisecond
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <param name="minute"></param>
		/// <param name="second"></param>
		/// <param name="millisecond"></param>
		public TDDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			dtDateTime = new DateTime(year, month, day, hour, minute, second, millisecond);
		}

		/// <summary>
		/// Constructor to take an DateTime
		/// </summary>
		/// <param name="dt"></param>
		public TDDateTime(System.DateTime dt)
		{
			dtDateTime = dt;
		}

		/// <summary>
		/// Returns the Year
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Year
		{
			get { return dtDateTime.Year; }
			set { }
		}

		/// <summary>
		/// Returns the Month
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Month
		{
			get { return dtDateTime.Month; }
			set { }
		}

		/// <summary>
		/// Returns the Day
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Day
		{
			get { return dtDateTime.Day; }
			set { }
		}

		/// <summary>
		/// Returns the Hour
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Hour
		{
			get { return dtDateTime.Hour; }
			set { }
		}

		/// <summary>
		/// Returns the Minute
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Minute
		{
			get { return dtDateTime.Minute; }
			set { }
		}

		/// <summary>
		/// Returns the Second
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Second
		{
			get { return dtDateTime.Second; }
			set { }
		}

		/// <summary>
		/// Returns the Millisecond
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int Millisecond
		{
			get { return dtDateTime.Millisecond; }
			set { }
		}

		/// <summary>
		/// Returns the DayOfYear
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		public int DayOfYear
		{
			get { return dtDateTime.DayOfYear; }
		}

		/// <summary>
		/// Returns the DayOfWeek as an integer
		/// Sunday = 0 through to Saturday = 7
		/// </summary>
		public DayOfWeek DayOfWeek
		{
			get 
			{
				return dtDateTime.DayOfWeek;
			}
		}

		/// <summary>
		/// Returns Now
		/// If Now has been fixed the fixed value is returned
		/// Otherwise the real System value of Now is returned
		/// </summary>
		static public TDDateTime Now
		{
			get
			{
				if (tddtNow == null)
				{
					return new TDDateTime(System.DateTime.Now);
				}
				else
				{
					return tddtNow;
				}
			}
		}

		/// <summary>
		/// Adds a TimeSpan to the current TDDateTime
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="timeSpan">A TimeSpan</param>
		public TDDateTime Add(TimeSpan timeSpan)
		{
			return new TDDateTime(dtDateTime.Add(timeSpan));
		}
		
		/// <summary>
		/// Subtracts a TimeSpan from the current TDDateTime
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="timeSpan">A TimeSpan</param>
		public TDDateTime Subtract(TimeSpan timeSpan)
		{
			return new TDDateTime(dtDateTime.Subtract(timeSpan));
		}

		/// <summary>
		/// Compares the passed in Object to the TDDateTime
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="value">An Object</param>
		public int CompareTo(object value)
		{
			Type valueType = value.GetType();
			if (valueType.IsInstanceOfType(this))
			{
				// If value is a TDDateTime cast it to a TDDateTime and call the DateTime.CompareTo method
				TDDateTime locTDDT = (TDDateTime)value;
				return dtDateTime.CompareTo(locTDDT.GetDateTime());
			}
			else
			{
				// If value is not a TDDateTime throw an ArgumentException
				throw new ArgumentException("This object is not of Type TransportDirect.Common.TDDateTime");
			}
		}

		/// <summary>
		/// Compares an object with this. 
		/// If it is a TDDateTime and has the same value return true, otherwise return false
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="value">An Object</param>
		override public bool Equals(object value)
		{
			Type valueType = value.GetType();
			if (valueType.IsInstanceOfType(this))
			{
				TDDateTime locTDDT = (TDDateTime)value;
				return dtDateTime.Equals(locTDDT.GetDateTime());
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		override public int GetHashCode()
		{
			return dtDateTime.GetHashCode();
		}

		/// <summary>
		/// Stores the default formatting option in sFormat.
		/// </summary>
		/// <param name="format">A CultureInfo</param>
		public void SetFormat(CultureInfo format)
		{
			sFormat = format;
		}

		/// <summary>
		/// 		/// Returns the number of days in the month for the given year
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		static public int DaysInMonth(int year, int month)
		{
			return System.DateTime.DaysInMonth(year, month);
		}

		/// <summary>
		/// Returns true if the given year is a leap year, otherwise returns false.
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="year"></param>
		static public bool IsLeapYear(int year)
		{
			return System.DateTime.IsLeapYear(year);
		}

		/// <summary>
		/// Compares the dates (dd/MM/yyyy) part of two DateTimes to see if
		/// they are the same. The time is ignored. 
		/// </summary>
		/// <param name="date1">TDDateTime</param>
		/// <param name="date2">TDDateTime</param>
		/// <returns>True if same date</returns>
		static public bool AreSameDate(TDDateTime date1, TDDateTime date2)
		{
			if (date1.GetDateTime().Date.CompareTo(date2.GetDateTime().Date) == 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Returns the underlying Syste.DateTime object
		/// </summary>
		public DateTime GetDateTime()
		{
			return dtDateTime;
		}

		/// <summary>
		/// Parses the given string and returns a TDDateTime based on it, formatted according to the CultureInfo format.
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="dateText">A string</param>
		/// <param name="format">A CultureInfo object</param>
		static public TDDateTime Parse(string dateText, CultureInfo format)
		{
			TDDateTime tdNew = new TDDateTime(System.DateTime.Parse(dateText, format));
			tdNew.SetFormat(format);

			return tdNew;
		}

		/// <summary>
		/// Returns the TDDateTime object as a string.
		/// Uses the default format if sFormat has not been set.
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		override public string ToString()
		{
			if (sFormat != null)
			{
				return dtDateTime.ToString(sFormat);
			}
			else
			{
				if(Thread.CurrentThread.CurrentUICulture.Name.Equals("cy-GB"))
				{
					return dtDateTime.ToString();
				}
				else
				{
					return dtDateTime.ToString(Thread.CurrentThread.CurrentUICulture);
				}

			}
		}

		/// <summary>
		/// Returns the TDDateTime object as a string, formatted as per format.
		/// Pass through method invokes corresponding DateTimeFunctionality
		/// </summary>
		/// <param name="format"></param>
		public string ToString(string format)
		{
			return dtDateTime.ToString(format, Thread.CurrentThread.CurrentUICulture);
		}

		/// <summary>
		/// Fixes Now to the passed in vlaue.
		/// </summary>
		/// <param name="falseNow">A TDDateTime</param>
		static public void FixNow(TDDateTime falseNow)
		{
			tddtNow = falseNow;
		}

		/// <summary>
		/// Implements the AddMonths method from DateTime.
		/// </summary>
		/// <param name="falseNow">A TDDateTime</param>
		public TDDateTime AddMonths(int months)
		{
			return new TDDateTime(dtDateTime.AddMonths(months));
		}

		/// <summary>
		/// Implements the AddDays method from DateTime
		/// </summary>
		/// <param name="days">days</param>
		/// <returns>A TDDateTime</returns>
		public TDDateTime AddDays(int days)
		{
			return new TDDateTime(dtDateTime.AddDays(days));
		}

		/// <summary>
		/// Implements the AddMinutes method from DateTime
		/// </summary>
		/// <param name="minutes">minutes</param>
		/// <returns>a TDDateTime</returns>
		public TDDateTime AddMinutes (double minutes)
		{
			return new TDDateTime(dtDateTime.AddMinutes(minutes));
		}

		/// <summary>
		/// Returns the difference in whole days between this date and a date parameter
		/// Time of day is taken into account, i.e. a day is one 24 hour period
		/// </summary>
		/// <param name="date2"></param>
		/// <returns></returns>
		public int GetDifferenceDays(TDDateTime date2)
		{
			//get time span between the 2 dates
			TimeSpan timeSpan = (dtDateTime - date2.GetDateTime());			
			
			//returns the number of whole days in the timespan			
			return timeSpan.Days;
				
		}

		/// <summary>
		/// Returns the difference in calendar days between this date and a TDDateTime
		/// The time portion is ignored. 		
		/// </summary>
		/// <param name="date2"></param>
		/// <returns></returns>
		public int GetDifferenceDates(TDDateTime date2)
		{
			//get time span between the 2 dates
			TimeSpan timeSpan = (date2.GetDateTime().Date - dtDateTime.Date);			
			
			//returns the number of days in the timespan			
			return timeSpan.Days;
				
		}

		/// <summary>
		/// Returns the difference in hours between this date and a date parameter
		/// </summary>
		/// <param name="date2"></param>
		/// <returns></returns>
		public int GetDifferenceHours(TDDateTime date2)
		{
			//get time span between the 2 dates
			TimeSpan timeSpan = (dtDateTime - date2.GetDateTime());
			
			//returns the number of whole hours in the timespan
			return timeSpan.Hours;		
		}

		/// <summary>
		/// Returns the difference in seconds between this date and a date parameter
		/// </summary>
		/// <param name="date2"></param>
		/// <returns></returns>
		public int GetDifferenceSeconds(TDDateTime date2)
		{
			//get time span between the 2 dates
			TimeSpan timeSpan = (dtDateTime - date2.GetDateTime());
			
			//returns the number of whole seconds in the timespan
			return timeSpan.Seconds;			
		}

		/// <summary>
		/// returns the month and year formatted for use with
		/// the TriStateDateControl
		/// </summary>
		/// <returns></returns>
		public string TDMonthYear
		{
			get
			{
				return dtDateTime.ToString("MM/yyyy");		
			}
		}
	
	}
}
