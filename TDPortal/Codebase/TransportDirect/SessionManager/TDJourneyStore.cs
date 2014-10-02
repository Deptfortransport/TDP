// *********************************************** 
// NAME                 : TDJourneyStore.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 19/04/2004
// DESCRIPTION			: The TDJourneyStore groups together all of the data that relates to a Request and its Results
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDJourneyStore.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:40   mturner
//Initial revision.
//
//   Rev 1.9   Feb 10 2006 15:04:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.8.1.1   Dec 22 2005 09:38:04   tmollart
//Removed references to OldJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.8.1.0   Dec 12 2005 17:39:08   tmollart
//Changed OldFindAMode to FindAMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.8   Nov 09 2005 12:31:50   build
//Automatically merged from branch for stream2818
//
//   Rev 1.7.1.0   Oct 17 2005 15:43:58   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.7   Oct 15 2004 12:31:18   jgeorge
//Added JourneyPlanStateData and modifications to the serialization process.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.6   Sep 17 2004 15:13:06   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.5   Aug 19 2004 13:10:36   COwczarek
//Add FindPageState property
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.4   Aug 17 2004 08:53:26   COwczarek
//Add new properties OldFindAMode and OldJourneyParameters
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.3   Jul 01 2004 14:34:40   RHopkins
//Added PricingRetailOptions and JourneyPlanControlData
//
//   Rev 1.2   Jun 10 2004 16:13:54   RHopkins
//Return Amended Journey when requesting selected Public Journey, if it is selected.
//
//   Rev 1.1   May 20 2004 14:57:28   RHopkins
//Fixed ReturnPublicJourney() to return the correct journey.

using System;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDJourneyStore.
	/// </summary>
	[Serializable()][CLSCompliant(false)]
	public class TDJourneyStore
	{

		private ITDJourneyRequest journeyRequest;
		private ITDJourneyResult journeyResult;
		private TDJourneyState journeyState;
		private TDJourneyParameters journeyParameters;
		private PricingRetailOptionsState pricingRetailOptions = new PricingRetailOptionsState();
        private FindAMode findAMode;
        private FindPageState findPageState;


		public TDJourneyStore()
		{
		}

		/// <summary>
		/// The JourneyRequest that was used to generate this Result set
		/// </summary>
		public ITDJourneyRequest JourneyRequest 
		{
			get { return journeyRequest; }
			set { journeyRequest = value; }
		}

		/// <summary>
		/// The JourneyResult that contains this Result set
		/// </summary>
		public ITDJourneyResult JourneyResult 
		{
			get { return journeyResult; }
			set { journeyResult = value; }
		}

		/// <summary>
		/// The JourneyViewState properties that identify User selections from this Result set
		/// </summary>
		public TDJourneyState JourneyState 
		{
			get { return journeyState; }
			set { journeyState = value; }
		}

		/// <summary>
		/// The JourneyParameters that were used to generate this Result set
		/// </summary>
		public TDJourneyParameters JourneyParameters
		{
			get { return journeyParameters; }
			set { journeyParameters = value; }
		}

        /// <summary>
        /// Get/Set the Find A mode related to the current journey results
        /// </summary>
        public FindAMode FindAMode
        {
            get { return findAMode; }
            set { findAMode = value; }
        }

        /// <summary>
        /// Get/Set the current Find A mode page state
        /// </summary>
        public FindPageState FindPageState
        {
            get { return findPageState; }
            set { findPageState = value; }
        }

		/// <summary>
		/// The PricingRetailOptions that were used to view this Result set
		/// </summary>
		public PricingRetailOptionsState PricingRetailOptions
		{
			get { return pricingRetailOptions; }
			set { pricingRetailOptions = value; }
		}

		/// <summary>
		/// Returns true if the selected outward journey uses public transport
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
		/// Returns the selected outward public journey
		/// </summary>
		/// <returns>Selected outward public journey</returns>
		public PublicJourney OutwardPublicJourney()
		{
			int amendedJourneyIndex = -1;
			if (JourneyResult.AmendedOutwardPublicJourney != null)
			{
				amendedJourneyIndex = JourneyResult.AmendedOutwardPublicJourney.JourneyIndex;
			}

			PublicJourney publicJourney = this.JourneyResult.OutwardPublicJourney(this.JourneyState.SelectedOutwardJourneyID);
			if (publicJourney.JourneyIndex == amendedJourneyIndex)
			{
				return JourneyResult.AmendedOutwardPublicJourney;
			}
			else
			{
				return publicJourney;
			}
		}

		/// <summary>
		/// Returns the outward road journey
		/// </summary>
		/// <returns>Outward road journey</returns>
		public RoadJourney OutwardRoadJourney()
		{
			return this.JourneyResult.OutwardRoadJourney();
		}

		/// <summary>
		/// Returns true if the selected return journey uses public transport
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
		/// Returns the selected return public journey
		/// </summary>
		/// <returns>Selected return public journey</returns>
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
		/// Returns the return road journey
		/// </summary>
		/// <returns>Return road journey</returns>
		public RoadJourney ReturnRoadJourney()
		{
			return this.JourneyResult.ReturnRoadJourney();
		}
	}
}
