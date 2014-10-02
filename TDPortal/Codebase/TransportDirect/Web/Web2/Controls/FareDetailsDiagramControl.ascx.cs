// *********************************************************************** 
// NAME                 : FareDetailsDiagramControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 24/01/2005
// DESCRIPTION			: Displays a journey as a diagram with fare information
// ************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FareDetailsDiagramControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:16   mturner
//Initial revision.
//
//   Rev 1.12   Jun 06 2007 15:58:58   asinclair
//Added returnCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.11   Jun 06 2007 12:37:54   asinclair
//Added singleCoachJourneyNewFares bool
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.10   May 25 2007 16:22:18   build
//Automatically merged from branch for stream4401
//
//   Rev 1.9.1.0   May 22 2007 13:16:10   mmodi
//Added NewAndOldCoachFares flag for new NX fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9   Apr 23 2007 14:51:18   asinclair
//Added returnfaresincluded bool
//
//   Rev 1.8   Mar 06 2007 13:43:54   build
//Automatically merged from branch for stream4358
//
//   Rev 1.7.1.0   Mar 02 2007 11:40:50   asinclair
//Added OtherFaresClicked Event Handler
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.7   Feb 23 2006 19:16:30   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.1   Jan 30 2006 14:41:02   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6.1.0   Jan 10 2006 15:24:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Apr 05 2005 14:06:10   rgeraghty
//fx cop changes, plus commenting
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Mar 14 2005 15:41:58   rgeraghty
//Added event to handle upgrade info button click
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 03 2005 17:57:38   rgeraghty
//FxCop changes made
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Mar 01 2005 15:01:34   rgeraghty
//First version
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.2   Feb 11 2005 14:58:06   rgeraghty
//Work in progress
//
//   Rev 1.1   Feb 09 2005 10:16:38   rgeraghty
//Work in progress
//
//   Rev 1.0   Jan 24 2005 18:24:36   rgeraghty
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region .NET namespaces
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;	
	using TransportDirect.Common.ResourceManager;
	
	#endregion

	/// <summary>
	///	Displays a journey as a diagram with fare information
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class FareDetailsDiagramControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{

		#region private members
		private PricedJourneySegment[] pricedJourney;
		private bool showChildFares;
		private string railDiscount = string.Empty;
		private string coachDiscount = string.Empty;
		private ItineraryType overrideItineraryType;
		private Hashtable selectedTicketsHash;
		private bool returnfaresincluded;
		private bool newAndOldCoachFares;
		private bool singleCoachJourneyNewFares;
		private bool returnCoachJourneyNewFares;
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
		public FareDetailsDiagramControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
			selectedTicketsHash = new Hashtable();
		}
		#endregion

		#region EventHandlers

		/// <summary>
		/// Page Load event code
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			GetData();
		}


		/// <summary>
		/// Adds a fareDetailsDiagramSegment control to the control
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsDiagramItemCreated(object sender, RepeaterItemEventArgs e)
		{			
			switch( e.Item.ItemType)
			{
				case ListItemType.Item:	
				case ListItemType.AlternatingItem:	

					FareDetailsDiagramSegmentControl fareDetailsDiagramSegmentControl = e.Item.FindControl("detailSegment") as FareDetailsDiagramSegmentControl;
					fareDetailsDiagramSegmentControl.PrinterFriendly = this.PrinterFriendly;
					fareDetailsDiagramSegmentControl.ShowChildFares = this.ShowChildFares;				
					fareDetailsDiagramSegmentControl.RailDiscount = this.RailDiscount;
					fareDetailsDiagramSegmentControl.CoachDiscount = this.CoachDiscount;
					fareDetailsDiagramSegmentControl.OverrideItineraryType = this.OverrideItineraryType;
					fareDetailsDiagramSegmentControl.ReturnFaresIncluded = this.ReturnFaresIncluded;
					fareDetailsDiagramSegmentControl.NewAndOldCoachFares = this.NewAndOldCoachFares;
					fareDetailsDiagramSegmentControl.PricedSegment = (PricedJourneySegment) e.Item.DataItem;
					fareDetailsDiagramSegmentControl.SelectedTickets = this.selectedTicketsHash;
					fareDetailsDiagramSegmentControl.SingleCoachJourneyNewFares = this.singleCoachJourneyNewFares;
					fareDetailsDiagramSegmentControl.ReturnCoachJourneyNewFares = this.returnCoachJourneyNewFares;
					fareDetailsDiagramSegmentControl.InfoButtonClicked +=new EventHandler(fareDetailsDiagramSegmentControl_InfoButtonClicked);
					fareDetailsDiagramSegmentControl.OtherFaresClicked +=new EventHandler(fareDetailsDiagramSegmentControl_OtherFaresClicked);
					break;
	
				default:
					break;
			}
		}

		/// <summary>
		/// PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{		
			GetData();
		}

		/// <summary>
		/// Event Handler for when info button of table segment control is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsDiagramSegmentControl_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.fareDetailsDiagramRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.fareDetailsDiagramItemCreated);			
			this.PreRender += new System.EventHandler(this.Page_PreRender);
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

		#region public properties

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
		/// Read only property - Returns the longest instruction text to be used by a dummy cell in the 
		/// repeater table. This is only to ensure correct cell widths.
		/// </summary>
		/// <returns>Instruction text</returns>
		public string GetLongestInstruction
		{
			get{return GetResource("FareDetailsDiagramControl.Checkin");}

		}

		/// <summary>
		/// Read only Property StartText.
		/// Internationalised text for "start"
		/// </summary>
		public string StartText
		{
			get 
			{
				return GetResource("FareDetailsDiagramControl.StartText");
			}
		}

		/// <summary>
		/// Read only property EndText.
		/// Internationalised text for "end"
		/// </summary>
		public string EndText
		{
			get
			{
				return GetResource("FareDetailsDiagramControl.EndText");
			}
		}

		/// <summary>
		/// Read/Write property - Contains the selected tickets for a pricing unit. 
		/// </summary>		
		public Hashtable SelectedTickets
		{
			get{return selectedTicketsHash;}
			set {selectedTicketsHash = value;}
		}

		/// <summary>
		/// Read/Write property -  Indicates whether control is to display rail discount fares
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
		/// Read/Write property - indicates whether adult or child fares are visible 
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

		public bool ReturnFaresIncluded
		{
			get {return returnfaresincluded;}
			set {returnfaresincluded = value;}
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
		#endregion

		#region public methods
		/// <summary>
		/// Returns the FareDetailsTableSegmentControl for the given pricing unit
		/// </summary>
		/// <param name="pricingUnit">PricingUnit</param>
		/// <returns>FareDetailsTableSegmentControl</returns>
		public FareDetailsTableSegmentControl FaresTable(PricingUnit pricingUnit)
		{	               
			for(int i=0; i<fareDetailsDiagramRepeater.Items.Count; i++)
			{					
				if((FareDetailsDiagramSegmentControl)fareDetailsDiagramRepeater.Items[i].FindControl("detailSegment") != null)
				{
					FareDetailsDiagramSegmentControl item = fareDetailsDiagramRepeater.Items[i].FindControl("detailSegment") as FareDetailsDiagramSegmentControl;
					
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

		#region private methods
		
		/// <summary>		
		/// sets the datasource of the repeater and binds.
		/// </summary>
		private void GetData()
		{
			if (pricedJourney != null)
			{				
				fareDetailsDiagramRepeater.DataSource = pricedJourney;
				fareDetailsDiagramRepeater.DataBind();			
			}
			
			DataBind();
		}

		/// <summary>
		/// Occurs when the user clicks the otherFaresLinkControl HyperbackPostbackControl
		/// on the FareDetailsTableSegmentControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsDiagramSegmentControl_OtherFaresClicked(object sender, EventArgs e)
		{

			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);

		}
		
		#endregion
	}

}
