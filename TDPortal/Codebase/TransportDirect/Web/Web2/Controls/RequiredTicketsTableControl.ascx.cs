// *********************************************** 
// NAME                 : RequiredTicketsTableControl.aspx.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/03/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RequiredTicketsTableControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:16   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.1   Jan 30 2006 14:41:20   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2.1.0   Jan 10 2006 15:26:58   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Mar 21 2005 17:01:00   jgeorge
//FxCop changes
//
//   Rev 1.1   Mar 08 2005 15:57:50   jgeorge
//Modifications afer initial QA
//
//   Rev 1.0   Mar 04 2005 11:51:20   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///	Displays details for the tickets a user is buying
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RequiredTicketsTableControl : TDUserControl, ILanguageHandlerIndependent
	{
		#region Private members and controls


		private string resourceYes;
		private string resourceNo;

		private RequiredTicketTableLine[] data;
		private bool showColumnAdult;
		private bool showColumnChild;
		private bool showColumnDiscounts;
		private float totalCost;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets the local resource manager
		/// </summary>
		public RequiredTicketsTableControl()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the repeater itemdatabound event. Depending on the item type, one of three private 
		/// methods is used to do the work
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void requiredTicketTable_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					HeaderItemCreated(e);
					break;

				case ListItemType.Footer:
					FooterItemCreated(e);
					break;

				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					BodyItemCreated(e);
					break;
			}
		}

		/// <summary>
		/// Handler for the prerender event. Binds the data to the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			ProcessData();

			// The only resources loaded by LoadResources are the yes and no values for the
			// discounts column, so there's no need to bother if we aren't showing that column.
			if (showColumnDiscounts)
				LoadResources();
			
			requiredTicketTable.DataSource = data;
			requiredTicketTable.DataBind();
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The data to be displayed
		/// </summary>
		public RequiredTicketTableLine[] TicketLines
		{
			get { return data; }
			set { data = value; }
		}

		/// <summary>
		/// Returns the table summary. Data bound.
		/// </summary>
		public string RequiredTicketsTableSummary
		{
			get { return GetResource( "RequiredTicketsTableControl.TableSummary" ); }
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets up the labels in the header item
		/// </summary>
		/// <param name="e"></param>
		private void HeaderItemCreated(System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			Label labelTicket = (Label)e.Item.FindControl("labelTicket");
			Label labelDiscounted = (Label)e.Item.FindControl("labelDiscounted");
			Label labelAdultFare = (Label)e.Item.FindControl("labelAdultFare");
			Label labelChildFare = (Label)e.Item.FindControl("labelChildFare");
			Label labelNoOfTickets = (Label)e.Item.FindControl("labelNoOfTickets");
			Label labelTotal = (Label)e.Item.FindControl("labelTotal");

			labelTicket.Text = resourceManager.GetString( "RequiredTicketsTableControl.HeadingTickets", TDCultureInfo.CurrentUICulture );
			
			if (showColumnDiscounts)
				labelDiscounted.Text = resourceManager.GetString( "RequiredTicketsTableControl.HeadingDiscounted", TDCultureInfo.CurrentUICulture );
			else
				labelDiscounted.Visible = false;

			if (showColumnAdult)
				labelAdultFare.Text = resourceManager.GetString( "RequiredTicketsTableControl.HeadingAdultFare", TDCultureInfo.CurrentUICulture );
			else
				labelAdultFare.Visible = false;

			if (showColumnChild)
				labelChildFare.Text = resourceManager.GetString( "RequiredTicketsTableControl.HeadingChildFare", TDCultureInfo.CurrentUICulture );
			else
				labelChildFare.Visible = false;

			labelNoOfTickets.Text = resourceManager.GetString( "RequiredTicketsTableControl.HeadingNoOfTickets", TDCultureInfo.CurrentUICulture );
			labelTotal.Text = resourceManager.GetString( "RequiredTicketsTableControl.HeadingTotal", TDCultureInfo.CurrentUICulture );
		}

		/// <summary>
		/// Populates a repeater item with data
		/// </summary>
		/// <param name="e"></param>
		private void BodyItemCreated(System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			Label labelTicket = (Label)e.Item.FindControl("labelTicket");
			Label labelDiscounted = (Label)e.Item.FindControl("labelDiscounted");
			Label labelAdultFare = (Label)e.Item.FindControl("labelAdultFare");
			Label labelChildFare = (Label)e.Item.FindControl("labelChildFare");
			Label labelNoOfTickets = (Label)e.Item.FindControl("labelNoOfTickets");
			Label labelTotal = (Label)e.Item.FindControl("labelTotal");

			RequiredTicketTableLine line = (RequiredTicketTableLine)e.Item.DataItem;
			//RequiredTicketTableLine line = data[e.Item.ItemIndex];

			labelTicket.Text = line.TicketName;
			
			if (showColumnDiscounts)
				labelDiscounted.Text = line.Discounted ? resourceYes : resourceNo;
			else
				labelDiscounted.Visible = false;

			if (showColumnAdult && !float.IsNaN(line.AdultFare))
				labelAdultFare.Text = String.Format( TDCultureInfo.InvariantCulture, TicketRetailersHelper.MoneyFormat, line.AdultFare );
			else
				labelAdultFare.Visible = false;

			if (showColumnChild && !float.IsNaN(line.ChildFare))
				labelChildFare.Text = String.Format( TDCultureInfo.InvariantCulture, TicketRetailersHelper.MoneyFormat, line.ChildFare );
			else
				labelChildFare.Visible = false;

			labelNoOfTickets.Text = line.NoPeople.ToString(TDCultureInfo.InvariantCulture);
			labelTotal.Text = String.Format( TDCultureInfo.InvariantCulture, TicketRetailersHelper.MoneyFormat, line.Total );
		}

		/// <summary>
		/// Sets up the repeater footer
		/// </summary>
		/// <param name="e"></param>
		private void FooterItemCreated(System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			Label labelTotal = (Label)e.Item.FindControl("labelTotal");
			labelTotal.Text = String.Format( TDCultureInfo.InvariantCulture, TicketRetailersHelper.MoneyFormat, totalCost );
		}

		/// <summary>
		/// Loads resources that are used repeatedly
		/// </summary>
		private void LoadResources()
		{
			resourceYes = resourceManager.GetString( "RequiredTicketsTableControl.Yes", TDCultureInfo.CurrentUICulture );
			resourceNo = resourceManager.GetString( "RequiredTicketsTableControl.No", TDCultureInfo.CurrentUICulture );
		}

		/// <summary>
		/// Goes through the data provided and populates variables for total and column visibility
		/// </summary>
		private void ProcessData()
		{
			showColumnAdult = false;
			showColumnChild = false;
			showColumnDiscounts = false;
			totalCost = 0;

			foreach (RequiredTicketTableLine line in data)
			{
				showColumnAdult = showColumnAdult || !float.IsNaN(line.AdultFare);
				showColumnChild = showColumnChild || !float.IsNaN(line.ChildFare);
				showColumnDiscounts = showColumnDiscounts || line.Discounted;
				totalCost += line.Total;
			}
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
			this.requiredTicketTable.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.requiredTicketTable_ItemDataBound);

		}
		#endregion

	}
}
