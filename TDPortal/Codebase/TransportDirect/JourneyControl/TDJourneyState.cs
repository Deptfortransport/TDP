// *********************************************** 
// NAME                 : TDJourneyState.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 19/04/2004
// DESCRIPTION			: The TDJourneyState maintains the state information for the
//                        current Request and Result.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDJourneyState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:00   mturner
//Initial revision.
//
//   Rev 1.1   May 19 2004 14:27:06   RHopkins
//Added extra data that is required for prepopulating Request from Itinerary Manager.

using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Maintains the User's selection within a set of TDJourneyResults.
	/// </summary>
	[Serializable()][CLSCompliant(false)]
	public class TDJourneyState
	{
		private ITDJourneyRequest originalJourneyRequest = null;
		private int selectedOutwardJourney;
		private int selectedOutwardJourneyID;
		private TDJourneyType selectedOutwardJourneyType = TDJourneyType.PublicOriginal;
		private int selectedReturnJourney;
		private int selectedReturnJourneyID;
		private TDJourneyType selectedReturnJourneyType = TDJourneyType.PublicOriginal;

		public TDJourneyState()
		{
		}

		/// <summary>
		/// Gets/Sets the original journey request
		/// </summary>
		public ITDJourneyRequest OriginalJourneyRequest
		{
			get { return originalJourneyRequest; }
			set { originalJourneyRequest = value; }
		}

		/// <summary>
		/// Gets/sets the selected outward journey index
		/// </summary>
		public int SelectedOutwardJourney 
		{
			get { return selectedOutwardJourney; }
			set { selectedOutwardJourney = value; }
		}

		/// <summary>
		/// Gets/sets the selected outward journey identifier
		/// </summary>
		public int SelectedOutwardJourneyID 
		{
			get { return selectedOutwardJourneyID; }
			set { selectedOutwardJourneyID = value; }
		}

		/// <summary>
		/// Gets/sets the selected outward journey type
		/// </summary>
		public TDJourneyType SelectedOutwardJourneyType 
		{
			get { return selectedOutwardJourneyType; }
			set { selectedOutwardJourneyType = value; }
		}

		/// <summary>
		/// Gets/sets the selected return journey index
		/// </summary>
		public int SelectedReturnJourney 
		{
			get { return selectedReturnJourney; }
			set { selectedReturnJourney = value; }
		}

		/// <summary>
		/// Gets/sets the selected return journey identifier
		/// </summary>
		public int SelectedReturnJourneyID 
		{
			get { return selectedReturnJourneyID; }
			set { selectedReturnJourneyID = value; }
		}

		/// <summary>
		/// Gets/sets the selected return journey type
		/// </summary>
		public TDJourneyType SelectedReturnJourneyType 
		{
			get { return selectedReturnJourneyType; }
			set { selectedReturnJourneyType = value; }
		}

	}
}
