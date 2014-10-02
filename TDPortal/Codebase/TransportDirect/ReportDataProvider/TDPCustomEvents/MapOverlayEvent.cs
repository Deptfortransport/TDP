// *********************************************** 
// NAME                 : MapOverlayEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// map overlay event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapOverlayEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:28   mturner
//Initial revision.
//
//   Rev 1.0   Sep 09 2003 13:45:14   TKarsan
//Initial Revision
//
//   Rev 1.1   Aug 28 2003 17:05:36   geaton
//Moved enumeration  category to separate file to follow standards.
//
//   Rev 1.0   Aug 20 2003 10:41:58   geaton
//Initial Revision

using System;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	
	[Serializable]
	public class MapOverlayEvent : TDPCustomEvent
	{
		private TDDateTime submitted;
		private MapOverlayCategory overlayCategory;
		private string scale;

		/// <summary>
		/// Constructor for a <c>MapOverlayEvent</c> class. 
		/// A <c>MapOverlayEvent</c> is used
		/// to log map overlay transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="submitted">Date/Time that map overlay event was submitted, down to the millisecond.</param>
		/// <param name="sessionId">The session id used to perform map overlay event.</param>
		/// <param name="overlayCategory">The category of object that was overlayed on the map.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="scale">The scale of the map on which the map overlay event occurred.</param>
		public MapOverlayEvent(MapOverlayCategory overlayCategory,
							   TDDateTime submitted,
							   string scale,
							   string sessionId, 
							   bool userLoggedOn) : base(sessionId, userLoggedOn)
		{
			this.submitted = submitted;
			this.overlayCategory = overlayCategory;
			this.scale = scale;
		}

		/// <summary>
		/// Gets the date/time at which the reference transaction was submitted.
		/// </summary>
		public TDDateTime Submitted
		{
			get{return submitted;}
		}

		/// <summary>
		/// Gets the overlay object category
		/// </summary>
		public MapOverlayCategory OverlayCategory
		{
			get{return overlayCategory;}
		}

		/// <summary>
		/// Gets the map scale
		/// </summary>
		public string Scale
		{
			get{return scale;}
		}
	}
}

