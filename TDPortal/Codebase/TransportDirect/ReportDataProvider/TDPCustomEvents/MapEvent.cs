// *********************************************** 
// NAME                 : MapEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// map event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapEvent.cs-arc  $
//
//   Rev 1.1   Oct 23 2008 15:58:26   pscott
//5117 - check submitted time when writing map events to prevent un-set default dates being used as these cause errors in queue processing.
//Resolution for 5117: Map events being incorecctly recorded
//
//   Rev 1.0   Nov 08 2007 12:39:26   mturner
//Initial revision.
//
//   Rev 1.5   Dec 18 2003 15:44:30   kcheung
//Added a File Formatter for a map event.
//Resolution for 551: Map Events are being logged with incorrect submission time
//
//   Rev 1.4   Sep 16 2003 11:09:28   geaton
//Added extra comments following code review.
//
//   Rev 1.3   Sep 15 2003 16:42:12   geaton
//Changed to DateTime instead of TDDateTime.
//
//   Rev 1.2   Sep 04 2003 19:23:50   geaton
//Updates due to report requirements changes.
//
//   Rev 1.1   Aug 28 2003 17:05:34   geaton
//Moved enumeration  category to separate file to follow standards.
//
//   Rev 1.0   Aug 20 2003 10:41:58   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{

	/// <summary>
	/// Defines the class for capturing Map Event data.
	/// </summary>
	[Serializable]
	public class MapEvent : TDPCustomEvent
	{
		private MapEventDisplayCategory displayCategory;
		private DateTime submitted;
		private MapEventCommandCategory commandCategory;
		private static MapEventFileFormatter fileFormatter = new MapEventFileFormatter();

		/// <summary>
		/// Constructor for a <c>MapEvent</c> class. 
		/// A <c>MapTransactionEvent</c> is used
		/// to log map transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="submitted">Date and Time that map event was submitted, down to the millisecond.</param>
		/// <param name="sessionId">The session id used to perform map event.</param>
		/// <param name="commandCategory">The command category that uniquely identifies the type of map event.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="displayCategory">The scale of the map on which the map event occurred.</param>
		public MapEvent(MapEventCommandCategory commandCategory,
						DateTime submitted,
						MapEventDisplayCategory displayCategory,
						string sessionId, 
						bool userLoggedOn) : base(sessionId, userLoggedOn)
		{
			this.submitted = submitted;
            if (this.submitted.Year < 2000)
            {
                this.submitted = DateTime.Now;
            }
			this.commandCategory = commandCategory;
			this.displayCategory = displayCategory;
		}

		/// <summary>
		/// Gets the date/time at which the reference transaction was submitted.
		/// </summary>
		public DateTime Submitted
		{
			get{return submitted;}
		}

		/// <summary>
		/// Gets the command category.
		/// </summary>
		public MapEventCommandCategory CommandCategory
		{
			get{return commandCategory;}
		}

		/// <summary>
		/// Gets the map scale
		/// </summary>
		public MapEventDisplayCategory DisplayCategory
		{
			get{return displayCategory;}
		}
	
		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	}
}
