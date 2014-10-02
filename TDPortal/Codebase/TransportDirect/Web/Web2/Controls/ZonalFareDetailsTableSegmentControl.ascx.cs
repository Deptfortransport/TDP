// *********************************************** 
// NAME                 : ZonalFareDetailsTableSegmentControl.ascx.cs 
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 14/03/2007
// DESCRIPTION			: A custom control to display  
//						  Zonal Fare Hyperlink
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ZonalFareDetailsTableSegmentControl.ascx.cs-arc  $
//
//   Rev 1.4   Jan 23 2013 17:21:40   mmodi
//Hide buy fares button for accessible journeys
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.3   Mar 26 2010 11:38:16   mmodi
//Populate CJP user flag to allow debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.2   Mar 31 2008 13:23:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:48   mturner
//Initial revision.
//
//   Rev 1.11   Jun 06 2007 16:13:44   asinclair
//Updated
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.11   Jun 06 2007 16:05:16   asinclair
//Added returnCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.10   Jun 06 2007 12:40:54   asinclair
//Added singleCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.9   May 25 2007 16:22:22   build
//Automatically merged from branch for stream4401
//
//   Rev 1.8.1.0   May 22 2007 13:21:16   mmodi
//Added NewAndOldCoachFares flag for new NX fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.8   May 03 2007 09:55:32   asinclair
//Added constructor and set label text correctly
//
//   Rev 1.7   May 02 2007 13:52:32   asinclair
//Added FareNoFareInformationControl for coach only
//
//   Rev 1.6   Apr 26 2007 15:43:42   asinclair
//Removed FareNoFareInformationControl as no longer required
//Resolution for 4395: 9.5 - Improved Rail / Local Zonal - Cosmetic Issues
//
//   Rev 1.5   Apr 18 2007 12:05:18   asinclair
//Merge from branch
//
//   Rev 1.4.1.1   Apr 13 2007 13:38:52   mmodi
//Added event to cascade Other fares click
//
//   Rev 1.4.1.0   Apr 13 2007 09:00:50   asinclair
//Added new FareNoFareInformationControl
//Resolution for 4385: 9.5 - Improved Rail / Local Zonal - Issues with Return Displays
//
//   Rev 1.4   Apr 04 2007 21:54:48   asinclair
//added returnnothroughfares
//
//   Rev 1.3   Apr 04 2007 18:33:14   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.2   Apr 04 2007 14:13:18   asinclair
//Added method to return the fares table on this control
//
//   Rev 1.1   Mar 21 2007 14:40:52   asinclair
//Added click events
//
//   Rev 1.0   Mar 19 2007 18:58:14   asinclair
//Initial revision.
 
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.Web.Adapters;

	/// <summary>
	///	Summary description for ZonalFareDetailsTableSegmentControl.
	/// </summary>
	public partial class ZonalFareDetailsTableSegmentControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		protected System.Web.UI.WebControls.Label faresLabel;
		protected System.Web.UI.WebControls.Label journeySegmentDescription;
		protected System.Web.UI.WebControls.Label noFaresFound;
		protected System.Web.UI.HtmlControls.HtmlTableCell journeyDescriptionCell;
		protected TransportDirect.UserPortal.Web.Controls.LocalZonalFaresControl LocalZonalFaresControl1;
		protected TransportDirect.UserPortal.Web.Controls.LocalZonalOpertatorFaresControl LocalZonalOpertatorFaresControl1;
		protected TransportDirect.UserPortal.Web.Controls.LegDetailsControl legdetailscontrol;
		private FareDetailsTableSegmentControl fareDetailsTableSegmentControl;
		private FareNoFareInformationControl fareNoFareInformationControl;
		
		private PublicJourneyDetail publicJourneyDetail;
		private PricedJourneySegment[] pricedJourney;
		private PricedJourneySegment pricedSegment;	

		private PublicJourneyDetail firstLeg;

		private bool showChildFares;
		private string railDiscount = string.Empty;
		private string coachDiscount = string.Empty;
		private ItineraryType overrideItineraryType;

		private IList journeyDetailList; 

		private bool returnfaresincluded;
		private bool returnNoThroughFares;
		private bool newAndOldCoachFares;
		private bool singleCoachJourneyNewFares;
		private bool returnCoachJourneyNewFares;

        /// <summary>
        /// Flag to allow additional debug info to be displayed on screen if logged on user has CJP status
        /// </summary>
        private bool cjpUser = false;

		private ItineraryAdapter itineraryAdapter;

        private bool hideTicketSelection;

		/// <summary>
		/// Event raised when the user clicks an "Info" button.
		/// </summary>
		public event EventHandler InfoButtonClicked;

		/// <summary>
		/// Event raised when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the FareDetailsTableSegmentControl
		/// </summary>
		public event EventHandler OtherFaresClicked;

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public ZonalFareDetailsTableSegmentControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;

		}

		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			GetData();

		}

		/// <summary>		
		/// sets the datasource of the repeater and binds.
		/// </summary>
		private void GetData()
		{
			if(pricedSegment != null)
			{
				journeyDetailList = pricedSegment.OutboundLegs;							

				if ((journeyDetailList != null) && (journeyDetailList.Count !=0 ))
				{	
					journeyDetailList = pricedSegment.OutboundLegs;
					firstLeg = (PublicJourneyDetail) journeyDetailList[0];
					zonalfares.DataSource = journeyDetailList;
					zonalfares.DataBind();
				}
			}

			
			DataBind();
		}


		/// <summary>
		/// Runs before the control is rendered to the client
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnPreRender(EventArgs e)
		{
			GetData();
			
			if (publicJourneyDetail == null) //hide the control if there is no Public Journey Detail
				this.Visible=false;
			else
			{
				GetData();
			}

		}

		public Label JourneySegmentDescription
		{
			get {return journeySegmentDescription;}
			set {journeySegmentDescription = value;}
		}

		/// <summary>
		/// The PricedJourneySegment object to be displayed
		/// </summary>
		public PricedJourneySegment PricedSegment
		{
			get {return pricedSegment;}
			set {pricedSegment = value;}			
		}

		/// <summary>
		/// Gets/Sets the itinerarytype to ensure single/return fares are
		/// displayed
		/// </summary>
		public ItineraryType OverrideItineraryType
		{
			get {return overrideItineraryType;}
			set {overrideItineraryType = value;}
		}

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

		/// <summary>
		/// Read/Write property. Set to indicate the Outward and Return journeys have a mixed set of 
		/// new and old coach fares.
		/// </summary>
		public bool NewAndOldCoachFares
		{
			get {return newAndOldCoachFares;}
			set {newAndOldCoachFares = value;}
		}

		public ItineraryAdapter ItineraryAdapter
		{
			get {return itineraryAdapter;}
			set {itineraryAdapter = value;}
		}

        /// <summary>
        /// Read/write property to hide ticket selection check boxes and disable javascript highlighting
        /// </summary>
        public bool HideTicketSelection
        {
            get { return hideTicketSelection; }
            set { hideTicketSelection = value; }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.zonalfares.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.zonalfares_ItemCreated);
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




		/// <summary>
		/// Event Handler for when info button of table segment control is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void fareDetailsTableSegmentControl_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		private void zonalfares_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				publicJourneyDetail = (PublicJourneyDetail) e.Item.DataItem;
				

				//The journey is priced, so use the fareDetailsTableSegmentControl
				 if (pricedSegment.UnitIsPriced && publicJourneyDetail.Equals(firstLeg))
				{					
					PlaceHolder placeholder = e.Item.FindControl("faresTablePlaceholder") as PlaceHolder;
					fareDetailsTableSegmentControl = ( FareDetailsTableSegmentControl )this.LoadControl( "FareDetailsTableSegmentControl.ascx" );										
					fareDetailsTableSegmentControl.ShowChildFares = this.ShowChildFares;
					fareDetailsTableSegmentControl.RailDiscount = this.RailDiscount;
					fareDetailsTableSegmentControl.CoachDiscount = this.CoachDiscount;	
					fareDetailsTableSegmentControl.PriceUnit = pricedSegment.PricingUnit;					
					fareDetailsTableSegmentControl.OverrideItineraryType = this.OverrideItineraryType;
					fareDetailsTableSegmentControl.PrinterFriendly = this.PrinterFriendly;
                    fareDetailsTableSegmentControl.HideTicketSelection = this.hideTicketSelection;
					fareDetailsTableSegmentControl.ReturnFaresIncluded = this.returnfaresincluded;
					fareDetailsTableSegmentControl.SingleCoachJourneyNewFares = this.singleCoachJourneyNewFares;
					fareDetailsTableSegmentControl.ReturnCoachJourneyNewFares = this.returnCoachJourneyNewFares;
					fareDetailsTableSegmentControl.NewAndOldCoachFares = this.newAndOldCoachFares;
                    fareDetailsTableSegmentControl.CJPUser = this.CJPUser;
					fareDetailsTableSegmentControl.InfoButtonClicked += new EventHandler(fareDetailsTableSegmentControl_InfoButtonClicked);
					placeholder.Controls.Add(fareDetailsTableSegmentControl);
					fareDetailsTableSegmentControl.OtherFaresClicked +=new EventHandler(fareDetailsTableSegmentControl_OtherFaresClicked);
					
				}

				//The journey is not priced, and is not a Coach Journey, then display the Leg Details Control, which
				//may contain a Local Zonal Services Control if required.
				else if(publicJourneyDetail.Mode != TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach)
				{
					LegDetailsControl legDetailsControl = e.Item.FindControl("legDetails") as LegDetailsControl;
					legDetailsControl.IsPriced = pricedSegment.UnitIsPriced;
					legDetailsControl.TableView = true;
					legDetailsControl.LegDetail = publicJourneyDetail;
					
					if(this.returnNoThroughFares && (publicJourneyDetail.Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail))
					{
						legDetailsControl.ReturnNoThroughFares = true;
					}
					if(this.returnfaresincluded && (publicJourneyDetail.Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail || publicJourneyDetail.Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach))
					{
						legDetailsControl.ReturnFaresIncluded = true;
					}
					else
					{
						legDetailsControl.ReturnFaresIncluded = false;
					}
				}

				//The journey is not priced, and is a coach journey.  Use the fareNoFareInformationControl to display 
				// a correct message to the user. This displays in a control with a similar format as if fares had been returned.
				else if(!pricedSegment.UnitIsPriced && (publicJourneyDetail.Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach))
				{			
					PlaceHolder placeholder = e.Item.FindControl("faresTablePlaceholder") as PlaceHolder;
					fareNoFareInformationControl = ( FareNoFareInformationControl )this.LoadControl( "FareNoFareInformationControl.ascx" );
					fareNoFareInformationControl.Visible=true;
					fareNoFareInformationControl.NoFareJourneyDetail = this.publicJourneyDetail;
					fareNoFareInformationControl.ItineraryAdapter = this.ItineraryAdapter;
					fareNoFareInformationControl.OtherFaresClicked +=new EventHandler(fareDetailsTableSegmentControl_OtherFaresClicked);
					fareNoFareInformationControl.LabelFareInformation.Text = GetResource("FareDetailsTableSegmentControl.NoFaresInformationAvailable.Text");
					placeholder.Controls.Add(fareNoFareInformationControl);
				}		
			}
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
		/// Indicates whether adult or child fares are visible 
		/// </summary>
		public bool ShowChildFares
		{
			get {return showChildFares;}
			set {showChildFares=value;}
		}


		/// <summary>
		/// Indicates whether control is to display rail discount fares
		/// </summary>
		public string RailDiscount
		{
			get {return railDiscount;}
			set {railDiscount=value;}
		}

		/// <summary>
		/// Indicates whether control is to display coach discount fares
		/// </summary>
		public string CoachDiscount
		{
			get {return coachDiscount;}
			set {coachDiscount=value;}
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


		/// <summary>
		/// Occurs when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the ZonalFareDetailsTableSegmentControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsTableSegmentControl_OtherFaresClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);

		}


		/// <summary>
		/// Returns the FareDetailsTableSegmentControl for the segment
		/// </summary>
		public FareDetailsTableSegmentControl FaresTable
		{
			get
			{ 
				return fareDetailsTableSegmentControl; // will be null if the segment is for an unpriced leg
			}
		}

		private void zonalfares_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);	
		}
	}
}
