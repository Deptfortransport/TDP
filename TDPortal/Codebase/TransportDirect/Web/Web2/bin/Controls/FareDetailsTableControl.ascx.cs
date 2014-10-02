// *********************************************************************** 
// NAME                 : FareDetailsTableControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 19/01/2005
// DESCRIPTION			: Displays the pricing units for a journey in tables
// ************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FareDetailsTableControl.ascx.cs-arc  $
//
//   Rev 1.7   Jan 23 2013 17:21:26   mmodi
//Hide buy fares button for accessible journeys
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.6   Oct 27 2010 10:34:38   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Mar 26 2010 11:35:52   mmodi
//Populate CJP user flag to allow debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.4   Nov 13 2008 15:07:06   jfrank
//Removed commented out code based on code review
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.3   Oct 17 2008 11:54:50   build
//Automatically merged from branch for stream0093
//
//   Rev 1.2.1.0   Oct 14 2008 11:40:18   jfrank
//Updated for XHTML compliance
//Resolution for 93: Stream IR for Del 10.4 maintenance fixes
//
//   Rev 1.2   Mar 31 2008 13:20:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:20   mturner
//Initial revision.
//
//   Rev 1.19   Jun 06 2007 16:00:46   asinclair
//Added returnCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.18   Jun 06 2007 12:39:50   asinclair
//Added singleCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.17   May 25 2007 16:22:18   build
//Automatically merged from branch for stream4401
//
//   Rev 1.16.1.0   May 22 2007 13:17:42   mmodi
//Added NewAndOldCoachFares flag for new NX fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.16   Apr 23 2007 17:53:46   asinclair
//Set the zonalfaredetailstablesegmentcontrol's printerfriendly property
//
//   Rev 1.15   Apr 18 2007 12:31:26   asinclair
//Merge from trunk
//
//   Rev 1.14.1.0   Apr 13 2007 08:58:40   asinclair
//Added property to access ItineraryAdapter
//
//   Rev 1.14   Apr 04 2007 21:52:02   asinclair
//Updated for local zonal services
//
//   Rev 1.13   Apr 04 2007 18:35:00   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.12   Apr 04 2007 13:54:16   asinclair
//Now returns the fare table for the local zonal control
//
//   Rev 1.11   Mar 21 2007 14:38:36   asinclair
//Added click event for zonalFaresControl
//
//   Rev 1.10   Mar 19 2007 18:46:34   asinclair
//Updated with outstanding stream4362 work
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.9   Mar 06 2007 13:43:54   build
//Automatically merged from branch for stream4358
//
//   Rev 1.8.1.0   Mar 02 2007 11:43:52   asinclair
//Added OtherFaresButtonClicked event handler
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.8   Mar 14 2006 10:30:14   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.7   Feb 23 2006 19:16:30   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.2.0   Mar 01 2006 13:19:54   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.6.1.0   Jan 10 2006 15:24:08   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Apr 05 2005 14:06:14   rgeraghty
//fx cop changes, plus commenting
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Mar 14 2005 15:42:10   rgeraghty
//Added event to handle upgrade info button click
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 03 2005 17:57:46   rgeraghty
//FxCop changes made
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Mar 01 2005 15:02:06   rgeraghty
//First version
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.2   Feb 11 2005 14:58:12   rgeraghty
//Work in progress
//
//   Rev 1.1   Feb 09 2005 10:16:44   rgeraghty
//Work in progress
//
//   Rev 1.0   Jan 19 2005 14:43:36   rgeraghty
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region .NET namespaces
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;
	using System.Collections;
	#endregion

	/// <summary>
	///	Displays the pricing units for a journey in a series of tables
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class FareDetailsTableControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		protected TransportDirect.UserPortal.Web.Controls.FareDetailsTableSegmentControl detailsSegment;
		protected TransportDirect.UserPortal.Web.Controls.ZonalFareDetailsTableSegmentControl zonalSegment;

		#region instance members

		private bool showChildFares;
		private string railDiscount = string.Empty;
		private string coachDiscount = string.Empty;
		private ItineraryType overrideItineraryType;
		private PricingUnit[] pricingUnits;
		private PricedJourneySegment[] pricedJourney;
		private Hashtable selectedTicketsHash;
		private bool hideTicketSelection;
		private bool returnfaresincluded;
		private bool returnNoThroughFares;
		private bool newAndOldCoachFares;
		private bool singleCoachJourneyNewFares;
		private bool returnCoachJourneyNewFares;
		private ItineraryAdapter itineraryAdapter;

        /// <summary>
        /// Flag to allow additional debug info to be displayed on screen if logged on user has CJP status
        /// </summary>
        private bool cjpUser = false;

		#endregion

		#region Events
		/// <summary>
		/// Event raised when the user clicks an "Info" button on table segment control
		/// </summary>
		public event EventHandler InfoButtonClicked;

		/// <summary>
		/// Event raised when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the FareDetailsTableSegmentControl
		/// </summary>
		public event EventHandler OtherFaresClicked;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public FareDetailsTableControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
			selectedTicketsHash = new Hashtable();
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();		
			this.fareDetailsTableSegmentControlRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.item_Created);
			this.fareDetailsZonalSegmentControlRepeater.ItemCreated +=new RepeaterItemEventHandler(fareDetailsZonalSegmentControlRepeater_ItemCreated);
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region private methods

		/// <summary>		
		/// Sets the datasource of the repeater and binds.
		/// </summary>
		private void GetData()
		{

			fareDetailsZonalSegmentControlRepeater.DataSource = pricedJourney;
			fareDetailsZonalSegmentControlRepeater.DataBind();
			
			DataBind(); 
		}

		#endregion

		#region public properties

		public bool ReturnFaresIncluded
		{
			get {return returnfaresincluded;}
			set {returnfaresincluded = value;}
		}

		public bool ReturnNoThroughFares
		{
			get {return returnNoThroughFares;}
			set {returnNoThroughFares = value;}
		}

		/// <summary>
		/// Read/Write property. Set to indicate the Outward and Return journeys have a mixed set of 
		/// new and old coach fares.
		/// </summary>
		public bool NewAndOldCoachFares
		{
			get {return newAndOldCoachFares;}
			set {newAndOldCoachFares = value;}
		}

		/// <summary>
		/// Read/Write property. Set to indicate that the outward leg of coach journey has
		/// been planned returning new fares, but that the return leg returns old fares, or that 
		/// this is a Single journey.
		/// In these cases we don't want to display return fares for this journey.
		/// </summary>
		public bool SingleCoachJourneyNewFares
		{
			get {return singleCoachJourneyNewFares;}
			set {singleCoachJourneyNewFares = value;}
		}

		/// <summary>
		/// Read/Write property. Set to indicate that the return leg of single coach journey has
		/// been planned returning new fares, but that the outward leg returns old fares.
		/// Therefore, we don't want to display return fares for this journey.
		/// </summary>
		public bool ReturnCoachJourneyNewFares
		{
			get {return returnCoachJourneyNewFares;}
			set {returnCoachJourneyNewFares = value;}
		}

		public ItineraryAdapter ItineraryAdapter
		{
			get {return itineraryAdapter;}
			set {itineraryAdapter = value;}
		}


		/// <summary>
		/// Read/Write property - Indicates whether control is to display rail discount fares
		/// </summary>
		public string RailDiscount
		{
			get {return railDiscount;}
			set {railDiscount=value;}
		}

		/// <summary>
		/// Read/Write property - Indicates whether control is to display coach discount fares
		/// </summary>
		public string CoachDiscount
		{
			get {return coachDiscount;}
			set {coachDiscount=value;}
		}


		/// <summary>
		/// Read/Write property - Gets/Sets the itinerarytype to ensure single/return fares are
		/// displayed
		/// </summary>
		public ItineraryType OverrideItineraryType
		{
			get {return overrideItineraryType;}
			set {overrideItineraryType = value;}
		}

		/// <summary>
		/// Read/Write property - Indicates whether adult or child fares are visible 
		/// </summary>
		public bool ShowChildFares
		{
			get {return showChildFares;}
			set {showChildFares=value;}
		}

		/// <summary>
		/// Read/Write property -  The array of PricedJourneySegment objects to be displayed
		/// </summary>
		public PricedJourneySegment[] PricedJourney
		{
			get {return pricedJourney;}
			set {pricedJourney = value;}			
		}

		/// <summary>
		/// Read/Write property - The array of PricingUnits which is to be displayed
		/// </summary>
		public PricingUnit[] PricingUnits
		{
			get {return pricingUnits;}
			set {pricingUnits = value;}			
		}

		/// <summary>
		/// Read/Write property - Contains the selected ticket for a pricing unit. 
		/// </summary>		
		public Hashtable SelectedTickets
		{
			get {return selectedTicketsHash;}
			set {selectedTicketsHash = value;}
		}

		public ZonalFareDetailsTableSegmentControl ZonalSegment
		{
			get { return zonalSegment;}
			set {zonalSegment = value;}
	
		}

		/// <summary>
		/// Read/write property to hide ticket selection check boxes and disable javascript highlighting
		/// </summary>
		public bool HideTicketSelection
		{
			get {return hideTicketSelection;}
			set {hideTicketSelection = value;}
		}

        /// <summary>
        /// Read/write. Flag to allow additional debug info to be displayed on screen if 
        /// logged on user has CJP status
        /// </summary>
        public bool CJPUser
        {
            get { return cjpUser; }
            set { cjpUser = value; }
        }

		#endregion

		#region public methods
		/// <summary>
		/// Returns the FareDetailsTableSegmentControl for the given pricing unit
		/// </summary>
		/// <param name="pricingUnit">PricingUnit</param>
		/// <returns>FareDetailsTableSegmentControl</returns>
		public FareDetailsTableSegmentControl FaresTable(PricingUnit pricingUnit)
		{			
			for(int i=0; i<fareDetailsTableSegmentControlRepeater.Items.Count; i++)
			{					
				if((FareDetailsTableSegmentControl)fareDetailsTableSegmentControlRepeater.Items[i].FindControl("detailsSegment") != null)
				{
					FareDetailsTableSegmentControl item = fareDetailsTableSegmentControlRepeater.Items[i].FindControl("detailsSegment") as FareDetailsTableSegmentControl;
		               
					//return the FareDetailsTableSegmentControl
					if (pricingUnit.Equals(item.PriceUnit))	
						return item;	
				}				
			}

//			for(int i=0; i<fareDetailsZonalSegmentControlRepeater.Items.Count; i++)
//			{					
//				if((ZonalFareDetailsTableSegmentControl)fareDetailsZonalSegmentControlRepeater.Items[i].FindControl("zonalSegment") != null)
//				{
//					ZonalFareDetailsTableSegmentControl item = fareDetailsZonalSegmentControlRepeater.Items[i].FindControl("zonalSegment") as ZonalFareDetailsTableSegmentControl;
//		               
//					//return the FareDetailsTableSegmentControl
//					if (pricingUnit.Equals(item.PriceUnit))	
//						return item;	
//				}				
//			}

			for(int i=0; i<fareDetailsZonalSegmentControlRepeater.Items.Count; i++)
			{					
				if((ZonalFareDetailsTableSegmentControl)fareDetailsZonalSegmentControlRepeater.Items[i].FindControl("zonalSegment") != null)
				{
					ZonalFareDetailsTableSegmentControl item = fareDetailsZonalSegmentControlRepeater.Items[i].FindControl("zonalSegment") as ZonalFareDetailsTableSegmentControl;
					
					//if the pricing unit matches the diagramsegment control's pricing unit then return the
					//tablesegmentcontrol of the diagramsegmentcontrol
					if (pricingUnit.Equals(item.PricedSegment.PricingUnit))	
				
						//need to return the tablesegment part
						if (item.PricedSegment.UnitIsPriced)				
							return item.FaresTable;
				}
			}



			return null;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			GetData();
		}

		/// <summary>
		/// Event Handler for when info button of table segment control is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsTableSegmentControl_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		/// <summary>
		/// PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{		
			GetData();

			//need to set the selected tickets for each table segment control
			foreach(RepeaterItem item in fareDetailsTableSegmentControlRepeater.Items)
			{
				FareDetailsTableSegmentControl fareDetailsTableSegmentControl = item.FindControl("detailsSegment") as FareDetailsTableSegmentControl;									

				if(fareDetailsTableSegmentControl!= null)
				{
					if (selectedTicketsHash.ContainsKey(fareDetailsTableSegmentControl.PriceUnit))
						fareDetailsTableSegmentControl.SelectedTicket = (Ticket) selectedTicketsHash[fareDetailsTableSegmentControl.PriceUnit];				
				}
			}
		}

		///<summary>
		/// Sets the properties of the control's fareDetailsDiagramSegment control 
		///</summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void item_Created(object sender, RepeaterItemEventArgs  e) 
		{						
			FareDetailsTableSegmentControl fareDetailsTableSegmentControl = e.Item.FindControl("detailsSegment") as FareDetailsTableSegmentControl;									
			fareDetailsTableSegmentControl.HideTicketSelection = this.hideTicketSelection;
			fareDetailsTableSegmentControl.PrinterFriendly= this.PrinterFriendly;
			fareDetailsTableSegmentControl.ShowChildFares =this.ShowChildFares;
			fareDetailsTableSegmentControl.RailDiscount = this.RailDiscount;
			fareDetailsTableSegmentControl.CoachDiscount = this.CoachDiscount;
			fareDetailsTableSegmentControl.OverrideItineraryType = this.OverrideItineraryType;
			fareDetailsTableSegmentControl.PriceUnit = (PricingUnit) e.Item.DataItem;	
			fareDetailsTableSegmentControl.InfoButtonClicked +=new EventHandler(fareDetailsTableSegmentControl_InfoButtonClicked);
			fareDetailsTableSegmentControl.OtherFaresClicked +=new EventHandler(fareDetailsTableSegmentControl_OtherFaresButtonClicked);
            fareDetailsTableSegmentControl.CJPUser = this.CJPUser;
		}

		/// <summary>
		/// Occurs when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the FareDetailsTableSegmentControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsTableSegmentControl_OtherFaresButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}


		

		private void fareDetailsZonalSegmentControlRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ZonalFareDetailsTableSegmentControl zonalFareDetailsTableSegmentControl = e.Item.FindControl("zonalSegment") as ZonalFareDetailsTableSegmentControl;
                zonalFareDetailsTableSegmentControl.PricedSegment = (PricedJourneySegment)e.Item.DataItem;
                zonalFareDetailsTableSegmentControl.ShowChildFares = this.showChildFares;
                zonalFareDetailsTableSegmentControl.RailDiscount = this.railDiscount;
                zonalFareDetailsTableSegmentControl.CoachDiscount = this.coachDiscount;
                zonalFareDetailsTableSegmentControl.OverrideItineraryType = this.overrideItineraryType;
                zonalFareDetailsTableSegmentControl.OtherFaresClicked += new EventHandler(zonalFareDetailsTableSegmentControl_OtherFaresClicked);
                zonalFareDetailsTableSegmentControl.InfoButtonClicked += new EventHandler(zonalFareDetailsTableSegmentControl_InfoButtonClicked);
                zonalFareDetailsTableSegmentControl.ReturnFaresIncluded = this.returnfaresincluded;
                zonalFareDetailsTableSegmentControl.NewAndOldCoachFares = this.newAndOldCoachFares;
                zonalFareDetailsTableSegmentControl.SingleCoachJourneyNewFares = this.singleCoachJourneyNewFares;
                zonalFareDetailsTableSegmentControl.ReturnCoachJourneyNewFares = this.returnCoachJourneyNewFares;
                zonalFareDetailsTableSegmentControl.ReturnNoThroughFares = this.returnNoThroughFares;
                zonalFareDetailsTableSegmentControl.ItineraryAdapter = this.itineraryAdapter;
                zonalFareDetailsTableSegmentControl.PrinterFriendly = this.PrinterFriendly;
                zonalFareDetailsTableSegmentControl.HideTicketSelection = this.hideTicketSelection;
                zonalFareDetailsTableSegmentControl.CJPUser = this.CJPUser;
            }
		}

		/// Occurs when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the zonalFareDetailsTableSegmentControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void zonalFareDetailsTableSegmentControl_OtherFaresClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);

		}

		/// <summary>
		/// Event Handler for when info button of zonalFareDetailsTableSegmentControl is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void zonalFareDetailsTableSegmentControl_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);

		}

		#endregion
	}
}
