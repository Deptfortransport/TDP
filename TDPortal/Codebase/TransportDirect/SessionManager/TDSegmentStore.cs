// ************************************************************************
// NAME         : TDSegmentStore.cs 
// AUTHOR       : Tim Mollart
// DATE CREATED : 18/08/2005
// DESCRIPTION	: Groups all of the data related to a request and its
//                results.
//                NOTE: This used to be TDJourneyStore but was renamed to
//                segment store to reflect the fact it stores data for a
//				  particular segment not journey.
// ************************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDSegmentStore.cs-arc  $
//
//   Rev 1.1   Apr 08 2008 16:58:36   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
//   Rev 1.0   Nov 08 2007 12:48:42   mturner
//Initial revision.
//
//   Rev 1.5   Mar 14 2006 08:41:44   build
//Automatically merged from branch for stream3353
//
//   Rev 1.4.1.1   Jan 26 2006 20:25:44   rhopkins
//Added properties OutwardSelectedJourney and ReturnSelectedJourney to return the selected journey, regardless of whether Road or PT.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4.1.0   Dec 20 2005 14:58:02   rhopkins
//Define CopyJourneyFromSession() and CopyJourneyToSession() methods in correct place.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Nov 09 2005 18:58:00   RPhilpott
//Merge with stream2818
//
//   Rev 1.3   Oct 29 2005 14:35:12   tmollart
//Code Review: Changes to this code as some properties etc have been moved into ExtendSegmentStore and vice versa. Attempt to make better inheritence.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 10 2005 18:07:48   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 17:21:38   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:59:00   tmollart
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDJourneyStore.
	/// </summary>
	[Serializable()][CLSCompliant(false)]
	public abstract class TDSegmentStore
	{

		#region Private Members

		/// <summary>
		/// Private storage for journey request for this segment.
		/// </summary>
		private ITDJourneyRequest journeyRequest;
		
		/// <summary>
		/// Private storage for journey result for this segment.
		/// </summary>
		private ITDJourneyResult journeyResult;
		
		/// <summary>
		/// Private storage for journey state for this segment.
		/// </summary>
		private TDJourneyState journeyState;

		/// <summary>
		/// Private storage for the journey parameters for this segment.
		/// </summary>
		private TDJourneyParameters journeyParameters;

		/// <summary>
		/// Private storage for pricing retail options state.
		/// </summary>
		private PricingRetailOptionsState pricingRetailOptions = new PricingRetailOptionsState();

		/// <summary>
		/// Private storage for the journey plan state data for this
		/// segment.
		/// </summary>
		private AsyncCallState asyncCallState;

		#endregion

		#region Public Properties

		/// <summary>
		///  Read/Write. The JourneyRequest that was used to generate this Result set
		/// </summary>
		public ITDJourneyRequest JourneyRequest 
		{
			get { return journeyRequest; }
			set { journeyRequest = value; }
		}


		/// <summary>
		///  Read/Write. The JourneyResult that contains this Result set
		/// </summary>
		public ITDJourneyResult JourneyResult 
		{
			get { return journeyResult; }
			set { journeyResult = value; }
		}


		/// <summary>
		///  Read/Write. The JourneyViewState properties that identify User selections from this Result set
		/// </summary>
		public TDJourneyState JourneyState 
		{
			get { return journeyState; }
			set { journeyState = value; }
		}


		/// <summary>
		///  Read/Write. The JourneyParameters that were used to generate this Result set
		/// </summary>
		public TDJourneyParameters JourneyParameters
		{
			get { return journeyParameters; }
			set { journeyParameters = value; }
		}


		/// <summary>
		/// Read/Write. PricingRetailOptions that were used to view this Result set
		/// </summary>
		public PricingRetailOptionsState PricingRetailOptions
		{
			get { return pricingRetailOptions; }
			set { pricingRetailOptions = value; }
		}


		/// <summary>
		/// Read/Write. AsyncCallState that were used to 
		/// generate this Result set
		/// </summary>
		public AsyncCallState AsyncCallState
		{
			get { return asyncCallState; }
			set { asyncCallState = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Read Only. Returns if the selected outward journey is
		/// a public transport journey.
		/// </summary>
		public bool SelectedOutwardJourneyIsPublic
		{
			get
			{
				switch (this.JourneyState.SelectedOutwardJourneyType)
				{
					case TDJourneyType.PublicAmended:
					case TDJourneyType.PublicOriginal:
						return true;
					default:
						return false;
				}
			}
		}


		/// <summary>
		/// Read Only. Returns if the selected return journey is
		/// a public transport journey.
		/// </summary>
		public bool SelectedReturnJourneyIsPublic
		{
			get
			{
				switch (this.JourneyState.SelectedReturnJourneyType)
				{
					case TDJourneyType.PublicAmended:
					case TDJourneyType.PublicOriginal:
						return true;
					default:
						return false;
				}
			}
		}


		/// <summary>
		/// Gets the selected Outward journey, whether Road or Public Transport
		/// </summary>
		public Journey OutwardSelectedJourney
		{
			get
			{
				if (this.JourneyState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
				{
					return OutwardRoadJourney();
				}
				else
				{
					return OutwardPublicJourney();
				}
			}
		}


		/// <summary>
		/// Gets the selected Return journey, whether Road or Public Transport
		/// </summary>
		public Journey ReturnSelectedJourney
		{
			get
			{
				if (this.JourneyState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
				{
					return ReturnRoadJourney();
				}
				else
				{
					return ReturnPublicJourney();
				}
			}
		}


		/// <summary>
		/// Returns the selected outward public journey.
		/// </summary>
		/// <returns>Public Journey</returns>
		public PublicJourney OutwardPublicJourney()
		{
			int amendedJourneyIndex = -1;
			if (JourneyResult.AmendedOutwardPublicJourney != null)
			{
				amendedJourneyIndex = JourneyResult.AmendedOutwardPublicJourney.JourneyIndex;
			}

            //The first clause in the if statement was added to prevent a bug
            //with the publicJourney variable being null.
			PublicJourney publicJourney = this.JourneyResult.OutwardPublicJourney(this.JourneyState.SelectedOutwardJourneyID);
			if (publicJourney != null && publicJourney.JourneyIndex == amendedJourneyIndex)
			{
				return JourneyResult.AmendedOutwardPublicJourney;
			}
			else
			{
				return publicJourney;
			}
		}


		/// <summary>
		/// Returns the selected return public journey.
		/// </summary>
		/// <returns>Public Journey</returns>
		public PublicJourney ReturnPublicJourney()
		{
			int amendedJourneyIndex = -1;
			if (JourneyResult.AmendedOutwardPublicJourney != null)
			{
				amendedJourneyIndex = JourneyResult.AmendedOutwardPublicJourney.JourneyIndex;
			}

			PublicJourney publicJourney = this.JourneyResult.ReturnPublicJourney(this.JourneyState.SelectedReturnJourneyID);
			if (publicJourney.JourneyIndex == amendedJourneyIndex)
			{
				return JourneyResult.AmendedReturnPublicJourney;
			}
			else
			{
				return publicJourney;
			}
		}


		/// <summary>
		/// Returns the outward road journey
		/// </summary>
		/// <returns>Road Journey</returns>
		public RoadJourney OutwardRoadJourney()
		{
			return this.JourneyResult.OutwardRoadJourney();
		}


		/// <summary>
		/// Returns the return road journey
		/// </summary>
		/// <returns>Road Journey</returns>
		public RoadJourney ReturnRoadJourney()
		{
			return this.JourneyResult.ReturnRoadJourney();
		}

		#endregion
	}
}
