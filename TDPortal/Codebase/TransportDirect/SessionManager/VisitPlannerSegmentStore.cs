// *********************************************************
// NAME         : VisitPlannerSegmentStore.cs 
// AUTHOR       : Tim Mollart
// DATE CREATED : 18/08/2005
// DESCRIPTION	: Segment for visit planner functionality
//
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/VisitPlannerSegmentStore.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:48   mturner
//Initial revision.
//
//   Rev 1.2   Mar 14 2006 08:41:44   build
//Automatically merged from branch for stream3353
//
//   Rev 1.1.1.0   Dec 20 2005 14:58:46   rhopkins
//Define CopyJourneyFromSession() and CopyJourneyToSession() methods in correct place.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Nov 24 2005 16:28:34   tmollart
//Removed redundant methods.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:59:00   tmollart
//Initial revision.

using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// VisitPlannerSegmentStore class.
	/// </summary>
	[Serializable()][CLSCompliant(false)]
	public class VisitPlannerSegmentStore : TDSegmentStore
	{

		/// <summary>
		/// Private storage for extendEndOfSegment
		/// </summary>
		private bool extendEndOfSegment;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public VisitPlannerSegmentStore()
		{
		}

		/// <summary>
		/// Read/Write property. Controls if AddJourney should extend
		/// the end or the start of the segment. True for end and false
		/// for start.
		/// </summary>
		public bool ExtendEndOfSegment
		{
			get { return extendEndOfSegment; }
			set { extendEndOfSegment = value; }
		}
	}
}
