// *********************************************** 
// NAME                 : ReplanPageState.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: Page State for replanning		
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ReplanPageState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:34   mturner
//Initial revision.
//
//   Rev 1.2   Mar 10 2006 11:20:52   tmollart
//Updated after code review.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 15 2006 16:16:34   tmollart
//Work in progress.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 07 2006 19:40:26   tmollart
//Initial revision.

using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// State for replanning a journey route.
	/// </summary>
	[Serializable()]
	[CLSCompliant(false)]
	public class ReplanPageState : InputPageState
	{

		#region Private Members

		/// <summary>
		/// Private storage containing the journey the user intends
		/// to replan.
		/// </summary>
		private Journey journeySelectedForReplan;
	
		/// <summary>
		/// Private storage for the journey request used to plan the
		/// original journey.
		/// </summary>
		private ITDJourneyRequest originalRequest;

		/// <summary>
		/// Private storage for the current amendment type. This needs
		/// to be defaulted to the outward journey.
		/// </summary>
		private TDAmendmentType currentAmendmentType = TDAmendmentType.OutwardJourney;

		/// <summary>
		/// Private storage containing index of leg user wants to 
		/// start replanning the journey from.
		/// </summary>
		private int replanStartJourneyDetailIndex = -1;
		
		/// <summary>
		/// As above but for index of end leg.
		/// </summary>
		private int replanEndJourneyDetailIndex = -1;
		
		#endregion

		/// <summary>
		/// Default constuctor.
		/// </summary>
		public ReplanPageState()
		{
		}

		#region Properties

		/// <summary>
		/// Gets/sets the original public journey
		/// </summary>
		public Journey JourneySelectedForReplan
		{
			get { return journeySelectedForReplan; }
			set	{ journeySelectedForReplan = value; }
		}

		
		/// <summary>
		/// Gets / Sets the original journey request.
		/// </summary>
		public ITDJourneyRequest OriginalRequest
		{
			get { return originalRequest; }
			set { originalRequest = value; }
		}


		/// <summary>
		/// Gets/sets the start index of journey section that is to be replanned.
		/// </summary>
		public int ReplanStartJourneyDetailIndex
		{
			get { return replanStartJourneyDetailIndex;	}
			set	{ replanStartJourneyDetailIndex = value; }
		}


		/// <summary>
		/// Gets/sets the end iindex of journey section that is to be replanned.
		/// </summary>
		public int ReplanEndJourneyDetailIndex
		{
			get { return replanEndJourneyDetailIndex; }
			set	{ replanEndJourneyDetailIndex = value; }
		}


		/// <summary>
		/// Gets/sets the current replan type (default is OutwardJourney).
		/// </summary>
		public TDAmendmentType CurrentAmendmentType
		{
			get { return currentAmendmentType; }
			set	{ currentAmendmentType = value; }
		}
		
		#endregion
	}
}