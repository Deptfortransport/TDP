//******************************************************************************
//NAME			: FindFareGotoTicketRetailerControl.cs
//AUTHOR		: Richard Hopkins
//DATE CREATED	: 18/02/2005
//DESCRIPTION	: Control used to display buttons to allow the User to buy tickets
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFareGotoTicketRetailerControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:08   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:16:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:24:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Nov 03 2005 16:55:44   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.2.1.1   Nov 02 2005 10:37:54   rgreenwood
//TD089 ES020 Explicitly set resource keys via reource manager for HTML Buttons
//
//   Rev 1.2.1.0   Oct 20 2005 12:53:04   rgreenwood
//TD089 ES020 Image Button Replacement
//
//   Rev 1.2   Apr 15 2005 14:18:12   rhopkins
//When determining which buttons to display, use TicketType of selected TravelDate, rather than TicketType originally requested by User.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.1   Mar 09 2005 20:03:14   rhopkins
//Change to button layout
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.0   Feb 25 2005 17:12:28   rhopkins
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Web.Support;

	/// <summary>
	///	Control used to display buttons to allow the User to buy tickets
	/// </summary>
	public partial  class FindFareGotoTicketRetailerControl : TDUserControl
	{

		FindCostBasedPageState pageState;

		#region PageLoad, Pre-Render, Initialise

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// OnPreRender method.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			// If we are not displaying the control, or it has not been set up correctly,
			// then ensure that it will not be displayed and do no further processing
			if (!this.Visible || (PageState == null))
			{
				this.Visible = false;
			}
			else
			{
				LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;

				// If the User is viewing separate singles for Outward and Return then
				// show three buttons to allow them to buy either Outward or Return or both
				if ( pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles)
				{
					ticketRetailersButton.Visible = false;
					ticketRetailersOutwardSingleButton.Visible = true;
					ticketRetailersInwardSingleButton.Visible = true;
					ticketRetailersBothSingleButton.Visible = true;

					ticketRetailersOutwardSingleButton.Text = resourceManager.GetString("FindFare.GotoBuyTicketsOutwardSingle.Text", TDCultureInfo.CurrentUICulture);

					ticketRetailersInwardSingleButton.Text = resourceManager.GetString("FindFare.GotoBuyTicketsInwardSingle.Text", TDCultureInfo.CurrentUICulture);

					ticketRetailersBothSingleButton.Text = resourceManager.GetString("FindFare.GotoBuyTicketsBothSingles.Text", TDCultureInfo.CurrentUICulture);
				}
				else
				{
					// The User is viewing single or combined tickets, so only one button is required
					ticketRetailersButton.Visible = true;
					ticketRetailersOutwardSingleButton.Visible = false;
					ticketRetailersInwardSingleButton.Visible = false;
					ticketRetailersBothSingleButton.Visible = false;

					ticketRetailersButton.Text = GetResource("FindFare.GotoBuyTickets.Text");
				}
			}
		}

		/// <summary>
		/// Initialises this control
		/// </summary>
		public void Initialise()
		{
		}

		#endregion

		public FindPageState PageState
		{
			get { return (FindPageState)pageState; }
			set 
			{
				try
				{
					pageState = (FindCostBasedPageState)value;
				}
				catch
				{
					// Supplied PageState cannot be cast to FindCostBasedPageState
					pageState = null;
				}
			}
		}

		#region button click event handlers

		/// <summary>
		/// Event Handler for the "Buy tickets" button that allows the User to buy a single or return ticket
		/// </summary>
		private void ticketRetailersButton_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.SetOneUseKey(SessionKey.FindFareBuySingleOrReturn,string.Empty);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
		}

		/// <summary>
		/// Event Handler for the "Buy tickets" button that allows the User to buy a single ticket for the Outward journey
		/// </summary>
		private void ticketRetailersOutwardSingleButton_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.SetOneUseKey(SessionKey.FindFareBuyOutwardSingle,string.Empty);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
		}

		/// <summary>
		/// Event Handler for the "Buy tickets" button that allows the User to buy a single ticket for the Inward journey
		/// </summary>
		private void ticketRetailersInwardSingleButton_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.SetOneUseKey(SessionKey.FindFareBuyReturnSingle,string.Empty);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
		}

		/// <summary>
		/// Event Handler for the "Buy tickets" button that allows the User to buy both single tickets
		/// for the Outward journey and Inward journeys
		/// </summary>
		private void ticketRetailersBothSingleButton_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.SetOneUseKey(SessionKey.FindFareBuyBothSingle,string.Empty);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
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
			this.ticketRetailersButton.Click += new EventHandler(this.ticketRetailersButton_Click);
			this.ticketRetailersOutwardSingleButton.Click += new EventHandler(this.ticketRetailersOutwardSingleButton_Click);
			this.ticketRetailersInwardSingleButton.Click += new EventHandler(this.ticketRetailersInwardSingleButton_Click);
			this.ticketRetailersBothSingleButton.Click += new EventHandler(this.ticketRetailersBothSingleButton_Click);

		}
		#endregion
	}
}
