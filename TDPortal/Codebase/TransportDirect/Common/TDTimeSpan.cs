//***********************************************
//NAME			: TDTimeSpan.cs
//AUTHOR		: Andy Lole
//DATE CREATED	: 15/07/2003
//DESCRIPTION	: Serializable implementation of TimeSpan.
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDTimeSpan.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:08   mturner
//Initial revision.
//
//   Rev 1.3   Nov 06 2003 11:07:08   geaton
//Reordered properties to improve serialisation readability.
//
//   Rev 1.2   Nov 06 2003 10:39:00   geaton
//Initial version.

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// This wrapper has been implemented,
	/// to support serialization of the class - 
	/// problems were encountered when serializing TimeSpan.
	/// </summary>
	[Serializable]
	public class TDTimeSpan 
	{
		public TDTimeSpan()
		{

		}
		
		public TDTimeSpan( int hours, int minutes, int seconds )
		{		
			this.days = 0;
			this.hours	 = hours;
			this.minutes = minutes;
			this.seconds = seconds;
		}

		public TDTimeSpan( int days, int hours, int minutes, int seconds )
		{		
			this.days = days;
			this.hours	 = hours;
			this.minutes = minutes;
			this.seconds = seconds;
		}

		private int days;
		private int hours;
		private int minutes;
		private int seconds;	
		
		public int Days
		{
			get { return days;  }
			set { days = value; }
		}

		public int Hours
		{
			get { return hours;  }
			set { hours = value; }
		}

		public int Minutes
		{
			get { return minutes;  }
			set { minutes = value; }
		}

		public int Seconds
		{
			get { return seconds;  }
			set { seconds = value; }
		}
		
		public TimeSpan ToTimeSpan()
		{
			return new TimeSpan( this.days, this.hours, this.minutes, this.seconds );
		}		
		
	}
}
