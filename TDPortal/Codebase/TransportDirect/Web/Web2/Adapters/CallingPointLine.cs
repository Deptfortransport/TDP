// *********************************************** 
// NAME			: CallingPointLine.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-07-11
// DESCRIPTION	: Encapsulates all the displayable characteristics
//				  of a single line of a public transport schedule 
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CallingPointLine.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:58:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:12   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.1   Jan 30 2006 12:15:14   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2.1.0   Jan 10 2006 15:17:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Aug 16 2005 17:53:32   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 19:57:56   RPhilpott
//Development of ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:09:48   RPhilpott
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Encapsulates all the displayable characteristics
	///	of a single line of a public transport schedule 	
	///	</summary>
	public class CallingPointLine
	{

		private string stationName;
		private string arrivalTime;
		private string departureTime;
		private bool significant;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CallingPointLine()
		{
		}

		/// <summary>
		/// Constructor that sets all properties of the CallingPointLine.
		/// </summary>
		/// <param name="stationName">Station name</param>
		/// <param name="arrivalTime">May be empty</param>
		/// <param name="departureTime">May be empty</param>
		/// <param name="significant">True is board/alight location</param>
		public CallingPointLine(string stationName, string arrivalTime, string departureTime, bool significant)
		{
			this.stationName = stationName;
			this.arrivalTime = arrivalTime;
			this.departureTime = departureTime;
			this.significant = significant;
		}

		/// <summary>
		/// Description of the location.
		/// </summary>
		public string StationName
		{
			get { return stationName; }
			set { stationName = value; }
		}

		/// <summary>
		/// Arrival time at this location (may be empty).
		/// </summary>
		public string ArrivalTime
		{
			get { return arrivalTime; }
			set { arrivalTime = value; }
		}

		/// <summary>
		/// Departure time from this location (may be empty).
		/// </summary>
		public string DepartureTime
		{
			get { return departureTime; }
			set { departureTime = value; }
		}

		/// <summary>
		/// True if this is the boarding or alighting point.
		/// </summary>
		public bool SignificantStation
		{
			get { return significant; }
			set { significant = value; }
		}
	}
}
