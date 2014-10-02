// *********************************************** 
// NAME                 : LocationRequestEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Defines a custom event class
// for capturing Location Request event data in
// the CJP.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/LocationRequestEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:08   mturner
//Initial revision.
//
//   Rev 1.5   Oct 29 2003 20:14:28   geaton
//Provided a DefaultFormatter instance for formatting rather than returning null which is bad.
//
//   Rev 1.4   Sep 16 2003 10:59:20   geaton
//Added serializable attribute to class to allow logging to MSMQ.
//
//   Rev 1.3   Sep 16 2003 10:55:24   geaton
//Updated comments following code review.
//
//   Rev 1.2   Sep 12 2003 11:34:32   geaton
//Create file formatter.
//
//   Rev 1.1   Sep 03 2003 19:23:56   geaton
//Aligned type names with those used in reporting database tables.
//
//   Rev 1.0   Sep 03 2003 16:56:46   geaton
//Initial Revision

using System;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{

	/// <summary>
	/// Defines the LocationRequestEvent class used for capturing
	/// Location Request event data within the CJP.
	/// </summary>
	[Serializable]
	public class LocationRequestEvent : CustomEvent
	{
		private string journeyPlanRequestId;
		private string adminAreaCode;
		private string regionCode;
		private JourneyPrepositionCategory prepositionCategory;

		/// <summary>
		/// Defines the formatter class that formats event data for use by a file publisher.
		/// Used by all instances of the LocationRequestEvent class.
		/// </summary>
		private static IEventFormatter fileFormatter = new LocationRequestEventFileFormatter();
				
		/// <summary>
		/// Standard formatter for use where specialised formatters are not required.
		/// </summary>
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();


		/// <summary>
		/// Gets the journey plan request identifier.
		/// </summary>
		public string JourneyPlanRequestId
		{
			get{return journeyPlanRequestId;}
		}

		/// <summary>
		/// Gets the administrative area code.
		/// </summary>
		public string AdminAreaCode
		{
			get{return adminAreaCode;}
		}
		
		/// <summary>
		/// Gets the region code.
		/// </summary>
		public string RegionCode
		{
			get{return regionCode;}
		}
		
		/// <summary>
		/// Gets the request preposition category.
		/// </summary>
		public JourneyPrepositionCategory PrepositionCategory
		{
			get{return prepositionCategory;}
		}

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <remarks>
		/// The constructor does NOT perfrom any validation on arguments.
		/// </remarks>
		/// <param name="journeyPlanRequestId">Identifier to uniquely identify this request.</param>
		/// <param name="prepositionCategory">The category of preposition for this request.</param>
		/// <param name="adminAreaCode">The administrative area code relating to the location request.</param>
		/// <param name="regionCode">The region code relating to the location request.</param>
		public LocationRequestEvent(string journeyPlanRequestId,
									JourneyPrepositionCategory prepositionCategory,						
									string adminAreaCode,
									string regionCode) : base()
		{
			this.journeyPlanRequestId = journeyPlanRequestId;
			this.prepositionCategory = prepositionCategory;
			this.adminAreaCode = adminAreaCode;
			this.regionCode = regionCode;
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}

		/// <summary>
		/// Provides an event formatting for publishing to email.
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to event logs
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to console.
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			get {return defaultFormatter;}
		}
	}
}


