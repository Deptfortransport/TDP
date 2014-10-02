// *********************************************** 
// NAME                 : RetailerHandoffDetailControl.aspx.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 28/02/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RetailerHandoffDetailControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:24   mturner
//Initial revision.
//
//   Rev 1.6   Apr 24 2006 18:02:26   RPhilpott
//Correct child discount checking.
//Resolution for 3944: DD075: Find Cheaper: child discount not applied on Ticket Information page
//
//   Rev 1.5   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.1   Jan 30 2006 14:41:20   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4.1.0   Jan 10 2006 15:27:02   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Apr 16 2005 12:02:48   jgeorge
//Updated handling of discount fares
//Resolution for 2071: PT: Minimum train fares not flagged
//
//   Rev 1.3   Apr 12 2005 11:37:58   jgeorge
//Changed end location in journey description being displayed above ticket table.
//Resolution for 2102: PT: Incorrect destination displayed on retailer handoff page
//
//   Rev 1.2   Mar 30 2005 15:41:12   jgeorge
//Updates to keep in line with SelectedTicket class
//
//   Rev 1.1   Mar 21 2005 17:01:00   jgeorge
//FxCop changes
//
//   Rev 1.0   Mar 04 2005 11:51:22   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using System.Collections;

	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	Displays a list of selected tickets
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RetailerHandoffDetailControl : TDUserControl, ILanguageHandlerIndependent
	{
		#region Controls


		#endregion

		#region Private members

		private const string resourceKeyTransportMode = "TransportMode.{0}";
		private const string resourceKeyJourneyDetails = "RetailerHandoffDetailControl.JourneyDescription";
		private string resourceJourneyDetails;

		private const string formatDate = "dd MMM yy";
		private const string formatTime = "HH:mm";

		private SelectedTicket[] selectedTickets;
		private int adultsTravelling;
		private int childrenTravelling;
		private DiscountCard railCard;
		private DiscountCard coachCard;

		#endregion

		#region Constructor


		/// <summary>
		/// Default constructor. Sets the local resource manager
		/// </summary>
		public RetailerHandoffDetailControl()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadResources();
		}

		/// <summary>
		/// Loads resources that will be used more than once
		/// </summary>
		private void LoadResources()
		{
			resourceJourneyDetails = GetResource( resourceKeyJourneyDetails );
		}

		/// <summary>
		/// Handler for the prerender event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			repeaterTickets.DataSource = selectedTickets;
			repeaterTickets.DataBind();
		}

		/// <summary>
		/// Handler for the ItemDataBound event of the repeater. Sets properties of controls contained
		/// within the itemtemplate element
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void repeaterTickets_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			SelectedTicket currentTicket = (SelectedTicket)e.Item.DataItem;

			Label labelMode = (Label)e.Item.FindControl("labelMode");
			Label labelJourney = (Label)e.Item.FindControl("labelJourney");
			RequiredTicketsTableControl requiredTickets = (RequiredTicketsTableControl)e.Item.FindControl("requiredTickets");

			PublicJourneyDetail firstDetail = currentTicket.OutboundLegs[0];
			PublicJourneyDetail lastDetail = currentTicket.OutboundLegs[currentTicket.OutboundLegs.Length - 1];

			labelMode.Text = GetResource( String.Format( TDCultureInfo.InvariantCulture, resourceKeyTransportMode, firstDetail.Mode.ToString() ) );

			labelJourney.Text = String.Format( TDCultureInfo.InvariantCulture, resourceJourneyDetails, 
				firstDetail.LegStart.Location.Description,
				firstDetail.LegStart.DepartureDateTime.ToString(formatDate),
				firstDetail.LegStart.DepartureDateTime.ToString(formatTime),
				lastDetail.LegEnd.Location.Description,
				lastDetail.LegEnd.ArrivalDateTime.ToString(formatDate),
				lastDetail.LegEnd.ArrivalDateTime.ToString(formatTime));

			requiredTickets.TicketLines = BuildRequiredTicketTableLines(currentTicket);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The list of tickets that this control should display. This will come directly
		/// from a RetailUnit
		/// </summary>
		public SelectedTicket[] SelectedTickets
		{
			get { return selectedTickets; }
			set { selectedTickets = value; }
		}

		/// <summary>
		/// The number of adult tickets required
		/// </summary>
		public int AdultsTravelling
		{
			get { return adultsTravelling; }
			set { adultsTravelling = value; }
		}

		/// <summary>
		/// The number of child tickets required
		/// </summary>
		public int ChildrenTravelling
		{
			get { return childrenTravelling; }
			set { childrenTravelling = value; }
		}

		/// <summary>
		/// The rail card that the user holds
		/// </summary>
		public DiscountCard RailCard
		{
			get { return railCard; }
			set { railCard = value; }
		}

		/// <summary>
		/// The coach card that the user holds
		/// </summary>
		public DiscountCard CoachCard
		{
			get { return coachCard; }
			set { coachCard = value; }
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Builds an array of RequiredTicketsTableLine to display using the contained control
		/// </summary>
		/// <param name="ticket"></param>
		/// <returns></returns>
		private RequiredTicketTableLine[] BuildRequiredTicketTableLines(SelectedTicket ticket)
		{
			ArrayList results = new ArrayList(4);
			
			int adultsDiscounted = 0;
			int childrenDiscounted = 0;
			bool adultDiscountPresent = !float.IsNaN( ticket.Ticket.DiscountedAdultFare );
			bool childDiscountPresent = !float.IsNaN( ticket.Ticket.DiscountedChildFare );

			if (adultDiscountPresent || childDiscountPresent)
			{
				if ((railCard != null) && TicketRetailersHelper.DisplayDiscountedFares(ticket.Mode, true, false))
				{
					if (adultDiscountPresent)
						adultsDiscounted = Math.Min( adultsTravelling, railCard.MaxAdults );
					if (childDiscountPresent)
						childrenDiscounted = Math.Min( childrenTravelling, railCard.MaxChildren );
				}

			
				if ((coachCard != null) && TicketRetailersHelper.DisplayDiscountedFares(ticket.Mode, false, true))
				{
					if (adultDiscountPresent)
						adultsDiscounted = Math.Min( adultsTravelling, coachCard.MaxAdults );
					if (childDiscountPresent)
						childrenDiscounted = Math.Min( childrenTravelling, coachCard.MaxChildren );
				}
			}

			int adultsStandard = adultsTravelling - adultsDiscounted;
			int childrenStandard = childrenTravelling - childrenDiscounted;

			if (ItineraryAdapter.IsAdultTicket(ticket.Ticket))
			{
				// Discounted adult tickets
				if (adultsDiscounted != 0)
					results.Add( new RequiredTicketTableLine(ticket.Ticket, adultsDiscounted, false, true) );

				// Standard adult tickets
				if (adultsStandard != 0)
					results.Add( new RequiredTicketTableLine(ticket.Ticket, adultsStandard, false, false) );
			}

			if (ItineraryAdapter.IsChildTicket(ticket.Ticket))
			{
				// Discounted child tickets
				if (childrenDiscounted != 0)
					results.Add( new RequiredTicketTableLine(ticket.Ticket, childrenDiscounted, true, true) );

				// Standard child tickets
				if (childrenStandard != 0)
					results.Add( new RequiredTicketTableLine(ticket.Ticket, childrenStandard, true, false) );
			}

			return (RequiredTicketTableLine[])results.ToArray(typeof(RequiredTicketTableLine));
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.repeaterTickets.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.repeaterTickets_ItemDataBound);

		}
		#endregion

	}
}
