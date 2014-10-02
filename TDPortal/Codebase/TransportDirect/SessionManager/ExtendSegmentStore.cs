// *********************************************************
// NAME                 : ExtendSegmentStore.cs 
// AUTHOR               : Tim Mollart
// DATE CREATED         : 22/08/2005
// DESCRIPTION			: Segment for extend functionality
//
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ExtendSegmentStore.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:22   mturner
//Initial revision.
//
//   Rev 1.6   Mar 14 2006 11:25:22   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 10 2006 15:04:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.4.1.1   Dec 22 2005 09:29:38   tmollart
//Removed reference to OldJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.4.1.0   Dec 12 2005 17:33:12   tmollart
//Changed OldFindAMode to FindAMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.4   Nov 09 2005 18:58:00   RPhilpott
//Merge with stream2818
//
//   Rev 1.3   Oct 29 2005 14:36:08   tmollart
//Code review rework to move some methods and properties into the base class.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 10 2005 18:07:48   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 17:12:24   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:58:58   tmollart
//Initial revision.

using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// VisitPlannerSegmentStore class.
	/// </summary>
	[Serializable()][CLSCompliant(false)]
	public class ExtendSegmentStore : TDSegmentStore
	{

		#region Private Members
		
		/// <summary>
		/// Private storage for old find a mode.
		/// </summary>
		private FindAMode findAMode;

		/// <summary>
		/// Private storage for find page state.
		/// </summary>
		private FindPageState findPageState;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ExtendSegmentStore()
		{
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Read/Write. Find A mode related to the current journey results
		/// </summary>
		public FindAMode FindAMode
		{
			get { return findAMode; }
			set { findAMode = value; }
		}

		/// <summary>
		/// Read/Write. Current Find A mode page state
		/// </summary>
		public FindPageState FindPageState
		{
			get { return findPageState; }
			set { findPageState = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Copies data from session into this journey segment.
		/// </summary>
		public void CopyJourneyFromSession()
		{
			JourneyRequest = TDSessionManager.Current.JourneyRequest;
			JourneyResult = TDSessionManager.Current.JourneyResult;
			JourneyState = TDSessionManager.Current.JourneyViewState.JourneyState;
			JourneyParameters = TDSessionManager.Current.JourneyParameters;
			AsyncCallState = TDSessionManager.Current.AsyncCallState;
			FindAMode = TDSessionManager.Current.FindAMode;
			FindPageState = TDSessionManager.Current.FindPageState;
		}


		/// <summary>
		/// Copes data from the current segment into the session.
		/// </summary>
		public void CopyJourneyToSession()
		{
			TDSessionManager.Current.JourneyRequest = JourneyRequest;
			TDSessionManager.Current.JourneyResult = JourneyResult;
			TDSessionManager.Current.JourneyViewState.JourneyState = JourneyState;
			TDSessionManager.Current.JourneyParameters = JourneyParameters;
			TDSessionManager.Current.AsyncCallState = AsyncCallState;
			TDSessionManager.Current.PricingRetailOptions = PricingRetailOptions;
			TDSessionManager.Current.FindPageState = FindPageState;
		}

		#endregion

	}
}
