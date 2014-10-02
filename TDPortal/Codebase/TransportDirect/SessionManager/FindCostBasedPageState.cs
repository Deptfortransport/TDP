// *********************************************** 
// NAME         : FindCostBasedPageState.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 22/12/2004
// DESCRIPTION  : Base class for cost based page states.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindCostBasedPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:24   mturner
//Initial revision.
//
//   Rev 1.13   Nov 15 2005 16:36:24   mguney
//Null check implemented for fareDateTable in SelectedTravelDate property. fareDateTable becomes null after extending a journey.
//Resolution for 3072: DN040: SBP Unhandled error when New Search clicked after Extend
//
//   Rev 1.12   Apr 13 2005 14:02:38   rhopkins
//Ensure that when the User chooses to view a Return travel date tickets as Singles, this should not adversely impact the viewing of tickets for other travel dates.
//
//   Rev 1.11   Apr 04 2005 15:32:06   tmollart
//Modifed initialise method so that showChild is initialised to false. 
//
//   Rev 1.10   Mar 24 2005 16:07:54   COwczarek
//Modifications to SelectedTravelDate property to allow the
//TravelDate object to be set/read independantly of the 
//SetSelectedTravelDateIndex property.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.9   Mar 15 2005 08:40:20   tmollart
//Removed CostBasedSearchWaitControlData and CostBasedSearchWaitStateData properties. Now on TDSessionManager.
//
//   Rev 1.8   Mar 11 2005 11:01:28   tmollart
//Updated intitialise method to set traveldetailsvisible to false.
//
//   Rev 1.7   Mar 09 2005 14:59:40   tmollart
//Removed implementation of SaveJourneyParamters. This overriden implementaiton is not required as nothing is stored at this level with is not stored in the base class.
//
//   Rev 1.6   Feb 25 2005 16:41:00   COwczarek
//Add properties for ticket selection
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.5   Feb 16 2005 14:10:16   tmollart
//Work in progress. Added Initialise method.
//
//   Rev 1.4   Feb 10 2005 16:14:24   rhopkins
//Added properties required for FindFareDateSelection
//
//   Rev 1.3   Jan 31 2005 16:59:02   tmollart
//Work in progress for Del 7 Find A Fare.
//
//   Rev 1.2   Jan 17 2005 15:10:32   tmollart
//Fixed error. Changed constructor to public from protected.
//
//   Rev 1.1   Jan 07 2005 16:04:52   jmorrissey
//Added CostSearchRequest and CostSearchResult properties
//
//   Rev 1.0   Dec 22 2004 15:29:24   tmollart
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.CostSearch;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Base class for cost based search page states.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindCostBasedPageState : FindPageState
	{
		#region Private Member Declaration

		private bool showChild;
		private TicketType selectedTicketType;
		private TDDateTime selectedSinglesOutwardDate;
		private TDDateTime selectedSinglesInwardDate;
		private TDDateTime selectedReturnOutwardDate;
		private TDDateTime selectedReturnInwardDate;
		private TDDateTime selectedSingleDate;
		private int selectedTravelDateIndex;
		private DisplayableTravelDates fareDateTable;
		private CostSearchRequest searchRequest;
		private CostSearchResult searchResult;
        private DisplayableTravelDate overriddenTravelDate;
		private int overriddenTravelDateIndex;

		/// <summary>
		/// Formatted single or return tickets used in a Find Fare Ticket table
		/// </summary>
		private DisplayableCostSearchTickets singleOrReturnTicketTable;

		/// <summary>
		/// Formatted inward only tickets used in a Find Fare Ticket table
		/// </summary>
		private DisplayableCostSearchTickets inwardTicketTable;

		/// <summary>
		/// Formatted outward only tickets used in a Find Fare Ticket table
		/// </summary>
		private DisplayableCostSearchTickets outwardTicketTable;

		/// <summary>
		/// Index of the selected single or return ticket in a Find Fare Ticket table
		/// </summary>
		private int selectedSingleOrReturnTicketIndex;

        /// <summary>
        /// Index of the selected outward ticket in a Find Fare Ticket table
        /// </summary>
        private int selectedOutwardTicketIndex;

        /// <summary>
        /// Index of the selected inward ticket in a Find Fare Ticket table
        /// </summary>
        private int selectedInwardTicketIndex;


		#endregion

		#region Constructor

		public FindCostBasedPageState()
		{
		}

		#endregion Constructor

		#region Public Properties

		/// <summary>
		/// Read/Write property. Gets/Sets ShowChild
		/// </summary>
		public bool ShowChild
		{
			get {return showChild;}
			set {showChild = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets SelectedTicketType
		/// </summary>
		public TicketType SelectedTicketType
		{
			get {return selectedTicketType;}
			set {selectedTicketType = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets SelectedSinglesOutwardDate
		/// </summary>
		public TDDateTime SelectedSinglesOutwardDate
		{
			get {return selectedSinglesOutwardDate;}
			set {selectedSinglesOutwardDate = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets SelectedSinglesInwardDate
		/// </summary>
		public TDDateTime SelectedSinglesInwardDate
		{
			get {return selectedSinglesInwardDate;}
			set {selectedSinglesInwardDate = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets SelectedReturnOutwardDate
		/// </summary>
		public TDDateTime SelectedReturnOutwardDate
		{
			get {return selectedReturnOutwardDate;}
			set {selectedReturnOutwardDate = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets SelectedReturnInwardDate
		/// </summary>
		public TDDateTime SelectedReturnInwardDate
		{
			get {return selectedReturnInwardDate;}
			set {selectedReturnInwardDate = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets SelectedSingleDate
		/// </summary>
		public TDDateTime SelectedSingleDate
		{
			get {return selectedSingleDate;}
			set {selectedSingleDate = value;}
		}

        /// <summary>
        /// Read/Write property. Gets/Sets the currently selected TravelDate object. 
        /// If the TravelDate property has not been set, the value of the
        /// SelectedTravelDateIndex property is used to derive the TravelDate from the 
        /// FareDateTable property. Setting the TravelDate property does not change
        /// the SelectedTravelDateIndex property.
        /// </summary>
		public DisplayableTravelDate SelectedTravelDate
		{
			get
            { 
                if ((overriddenTravelDate != null) && (overriddenTravelDateIndex == selectedTravelDateIndex))
                {
					return overriddenTravelDate;
				}
                else 
                {
					if (fareDateTable == null)
						return null;
					else return fareDateTable[selectedTravelDateIndex];
                }
            }
            set
			{
				overriddenTravelDate = value;
				overriddenTravelDateIndex = selectedTravelDateIndex;
			}
		}

        /// <summary>
        /// Read property. Returns the currently selected single or return ticket
        /// using the value of the SelectedSingleOrReturnTicketIndex property
        /// to index the SingleOrReturnTicketTable property.
        /// </summary>
		public DisplayableCostSearchTicket SelectedSingleOrReturnTicket 
		{
			get {return singleOrReturnTicketTable[selectedSingleOrReturnTicketIndex];}
		}

        /// <summary>
        /// Read property. Returns the currently selected outward ticket
        /// using the value of the SelectedOutwardTicketIndex property
        /// to index the OutwardTicketTable property.
        /// </summary>
        public DisplayableCostSearchTicket SelectedOutwardTicket 
		{
			get {return outwardTicketTable[selectedOutwardTicketIndex];}
		}

        /// <summary>
        /// Read property. Returns the currently selected inward ticket
        /// using the value of the SelectedInwardTicketIndex property
        /// to index the InwardTicketTable property.
        /// </summary>
        public DisplayableCostSearchTicket SelectedInwardTicket 
		{
			get {return inwardTicketTable[selectedInwardTicketIndex];}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets CostSearchRequest
		/// </summary>
		public CostSearchRequest SearchRequest
		{
			get {return searchRequest;}
			set {searchRequest = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets CostSearchRequest
		/// </summary>
		public CostSearchResult SearchResult
		{
			get {return searchResult;}
			set {searchResult = value;}
		}

		/// <summary>
		/// Index of the travel date that is currently selected in Find Fare Date Selection
		/// </summary>
		public int SelectedTravelDateIndex
		{
			get { return selectedTravelDateIndex; }
            set {selectedTravelDateIndex = value; }
		}

		/// <summary>
		/// Read/Write property. Gets/Sets formatted travel dates used in Find Fare Date Selection
		/// </summary>
		public DisplayableTravelDates FareDateTable
		{
			get { return fareDateTable; }
			set { fareDateTable = value; }
		}

		/// <summary>
		/// Read/Write property. Gets/Sets formatted outward only tickets used in a Find Fare Ticket table
		/// </summary>
		public DisplayableCostSearchTickets OutwardTicketTable
		{
			get { return outwardTicketTable; }
			set { outwardTicketTable = value; }
		}

		/// <summary>
		/// Read/Write property. Gets/Sets formatted inward only tickets used in a Find Fare Ticket table
		/// </summary>
		public DisplayableCostSearchTickets InwardTicketTable
		{
			get { return inwardTicketTable; }
			set { inwardTicketTable = value; }
		}

		/// <summary>
		/// Read/Write property. Gets/Sets formatted single or return tickets used in a Find Fare Ticket table
		/// </summary>
		public DisplayableCostSearchTickets SingleOrReturnTicketTable
		{
			get { return singleOrReturnTicketTable; }
			set { singleOrReturnTicketTable = value; }
		}

        /// <summary>
        /// Read/Write property. Gets/Sets index of the selected single or return ticket in a Find Fare Ticket table
        /// </summary>
        public int SelectedSingleOrReturnTicketIndex 
        {
            get { return selectedSingleOrReturnTicketIndex; }
            set { selectedSingleOrReturnTicketIndex = value; }
        }

        /// <summary>
        /// Read/Write property. Gets/Sets index of the selected outward ticket in a Find Fare Ticket table
        /// </summary>
        public int SelectedOutwardTicketIndex 
        {
            get { return selectedOutwardTicketIndex; }
            set { selectedOutwardTicketIndex = value; }
        }

        /// <summary>
        /// Read/Write property. Gets/Sets index of the selected inward ticket in a Find Fare Ticket table
        /// </summary>
        public int SelectedInwardTicketIndex 
        {
            get { return selectedInwardTicketIndex; }
            set { selectedInwardTicketIndex = value; }
        }

		#endregion

		#region Public Methods
        
		/// <summary>
		/// Initialise the cost based page state.
		/// </summary>
		public override void Initialise()
		{
			showChild = false;
			TravelDetailsVisible = false;
			base.bAmbiguityMode = false;
			base.Initialise();
		}

		#endregion
	}
}
