// *********************************************** 
// NAME                 : TicketMatrixControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 07/01/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TicketMatrixControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:12   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 16:14:04   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.8   Nov 24 2005 12:11:04   mguney
//ReturnJourneyDate property is introduced in TicketMatrix control and that property is set from the TicketRetailers.aspx.cs when there is a combined return journey. And in TicketMatrix .cs labelDate.Text setting is changed accordingly to display the appropriate dates.
//Resolution for 3140: DN040 (CG): Ticket retailers page date label
//
//   Rev 1.7   Nov 15 2005 14:43:02   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.6   Nov 03 2005 16:18:06   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.5.1.0   Oct 24 2005 16:59:22   RGriffith
//TD089 ES020 Image Button Replacement
//
//   Rev 1.5   Apr 16 2005 12:04:12   jgeorge
//Control should not be responsible for its own visibility
//Resolution for 2145: PT - Unable to go to printer friendly page from ticket retailer page
//
//   Rev 1.4   Mar 21 2005 17:03:36   jgeorge
//FxCop changes
//
//   Rev 1.3   Mar 08 2005 16:26:58   jgeorge
//Bug fixes and updates after QA
//
//   Rev 1.2   Mar 04 2005 09:32:22   jgeorge
//Updated after redesign
//
//   Rev 1.1   Feb 22 2005 17:31:48   jgeorge
//Interim check-in
//
//   Rev 1.0   Jan 18 2005 11:44:40   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Displays a list of tickets with ticks to indicate their availability
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class TicketMatrixControl :  TDPrintableUserControl, ILanguageHandlerIndependent
	{
		#region Control declaration


		protected PeopleTravellingControl peopleTravellingControl;

		#endregion

		#region Private members

		private const int NO_SELECTED_RETAILUNIT = -1;

		private string resourceTickURL;
		private string resourceTickAlt;
		private string resourceTicketURL;
		private string resourceTicketAlt;
		
		private string resourceLabelTo;
		
		private string resourceSingleJourneyTitle;
		private string resourceReturnJourneyTitle;
		private string resourceOutward;
		private string resourceInward;
		private string resourceFor;
		private string resourceSelectSelectedURL;
		private string resourceSelectSelectedAlt;
		private string resourceSelectText;

		private const string classUnitCellSelected = "unitcellselected";
		private const string classUnitCellUnselected = "unitcellunselected";
		private const string classUnitCellSelectedFirst = "unitcellselectedfirst";
		private const string classUnitCellUnselectedFirst = "unitcellunselectedfirst";
		private const string classSelectCellSelected = "selectcellselected";
		private const string classSelectCellUnselected = "selectcellunselected";

		private TicketRetailerInfo data;
		private Discounts discounts;
		private TDDateTime returnJourneyDate;
		private int selectedUnitIndex = NO_SELECTED_RETAILUNIT;
		
		private TicketRetailersHelper helper;

		#endregion

		#region Event

		/// <summary>
		/// Event that will be raised when the user changes their selection of
		/// RetailUnit
		/// </summary>
		public event EventHandler SelectedRetailUnitChanged;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets the local resource manager to FARES_AND_TICKETS_RM
		/// </summary>
		public TicketMatrixControl() : base()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the load event. Loads resources then rebinds the data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			helper = new TicketRetailersHelper(resourceManager);
			LoadResources();
			DisplayData();
		}

		/// <summary>
		/// Rebinds the data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			PopulateLabels();
			DisplayData();
		}

		/// <summary>
		/// Handler for the ItemDataBound event of the main repeater. Sets control values,
		/// create the columns and binds the fares repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ticketsRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ( (e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) )
			{
				// Populate the entries in the row
				SelectedTicket rowTicket = (SelectedTicket)e.Item.DataItem;

				// Populate the labels
				System.Web.UI.WebControls.Image imageTicket = (System.Web.UI.WebControls.Image)e.Item.FindControl("imageTicket");
				Label labelMode = (Label)e.Item.FindControl("labelMode");
				Label labelTicketType = (Label)e.Item.FindControl("labelTicketType");
				Label labelJourneyDescription = (Label)e.Item.FindControl("labelJourneyDescription");
				
				imageTicket.ImageUrl = resourceTicketURL;
				imageTicket.AlternateText = resourceTicketAlt;
				labelMode.Text = GetResource( "TransportMode." + rowTicket.Mode );
				labelTicketType.Text = rowTicket.Ticket.Code;
				labelJourneyDescription.Text = String.Format( TDCultureInfo.InvariantCulture, "{0} {1} {2}", rowTicket.OriginLocation.Description, resourceLabelTo, rowTicket.DestinationLocation.Description);

				// Bind the fares repeater
				Repeater faresRepeater = (Repeater)e.Item.FindControl("faresRepeater");
				faresRepeater.DataSource = helper.BuildFaresList(rowTicket, discounts);
				faresRepeater.DataBind();

				// Generate the columns
				GenerateColumns( rowTicket, (PlaceHolder)e.Item.FindControl("placeHolderColumns"), e.Item.ItemIndex == 0 );
			}
			else if ( e.Item.ItemType == ListItemType.Footer )
			{
				GenerateFooterColumns( (PlaceHolder)e.Item.FindControl("placeHolderColumns") );
			}
		}

		/// <summary>
		/// Handler for the ItemCommand event of the repeater. Will be executed when one of
		/// the Select buttons is clicked, and is used to set the SelectedRetailUnit property.
		/// Also causes the SelectedRetailUnitChanged event to be raised.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void ticketsRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			selectedUnitIndex = Convert.ToInt32( e.CommandArgument, TDCultureInfo.InvariantCulture );
			OnSelectedRetailUnitChanged(EventArgs.Empty);
		}

		#endregion

		#region Public properties and methods for data binding

		/// <summary>
		/// Property which returns the summary string for the matrix table. Used for data binding
		/// </summary>
		public string MatrixTableSummary
		{
			get { return GetResource( "TicketMatrixControl.TableSummary" ); }
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Sets/Gets the data being displayed by this control. Must be set before
		/// anything else
		/// </summary>
		public TicketRetailerInfo Data
		{
			get { return data; }
			set { this.data = value; }
		}

		/// <summary>
		/// Gets/sets the Discounts object used to determine which fares to display for 
		/// the ticket
		/// </summary>
		public Discounts Discounts
		{
			get { return discounts; }
			set { discounts = value; }
		}

		/// <summary>
		/// Gets/sets the return journey date if it exists so that it can be displayed when the itenerary type is return.
		/// </summary>
		public TDDateTime ReturnJourneyDate
		{
			get { return returnJourneyDate; }
			set { returnJourneyDate = value; }
		}

		/// <summary>
		/// Determines whether the PeopleTravellingControl is being shown
		/// </summary>
		public bool ShowPeopleTravellingControl
		{
			get { return peopleTravellingControl.Visible; }
			set { peopleTravellingControl.Visible = value; }
		}
		/// <summary>
		/// Gets the instance of the PeopleTravellingControl contained within this control.
		/// </summary>
		public PeopleTravellingControl PeopleTravellingControl
		{
			get { return peopleTravellingControl; }
		}

		/// <summary>
		/// Gets/sets the currently selected Retail Unit (null == none is selected).
		/// </summary>
		public RetailUnit SelectedRetailUnit
		{
			set 
			{ 
				// Find its index
				selectedUnitIndex = NO_SELECTED_RETAILUNIT;
				if (value != null)
				{
					for (int index = 0; index < data.RetailUnits.Length; index++)
						if ((RetailUnit)data.RetailUnits[index] == value)
						{
							selectedUnitIndex = index;
							continue;
						}
				}
			}
			get 
			{ 
				if (selectedUnitIndex == NO_SELECTED_RETAILUNIT)
					return null;
				else
                    return data.RetailUnits[selectedUnitIndex];
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Raises the SelectedRetailUnitChanged event
		/// </summary>
		/// <param name="e"></param>
		protected void OnSelectedRetailUnitChanged(EventArgs e)
		{
			EventHandler theHandler = SelectedRetailUnitChanged;
			if (theHandler != null)
				theHandler(this, e);
		}

		/// <summary>
		/// Generates the columns for the table. There is a column for each RetailUnit,
		/// and the column will either contain a tick (if the RetailUnit contains the 
		/// ticket for this row) or a non-breaking space (if it doesn't)
		/// </summary>
		/// <param name="rowTicket"></param>
		/// <param name="placeholder"></param>
		private void GenerateColumns(SelectedTicket rowTicket, PlaceHolder placeholder, bool isFirstLine)
		{
			foreach (RetailUnit unit in data.RetailUnits)
			{
				// Create the cell
				HtmlTableCell cell = new HtmlTableCell();
				cell.Align = "center";
				cell.VAlign = "middle";
					
				// Either populate it with an image or a non breaking space
				if (unit.ContainsTicket(rowTicket))
				{
					HtmlImage image = new HtmlImage();
					image.Alt = resourceTickAlt;
					image.Src = resourceTickURL;
					cell.Controls.Add( image );
				}
				else
				{
					cell.InnerHtml = "&nbsp;";
				}

				// Set its style
				if (SelectedRetailUnit == unit)
				{
					if (isFirstLine)
						cell.Attributes.Add("class", classUnitCellSelectedFirst);
					else
						cell.Attributes.Add("class", classUnitCellSelected);
				}
				else
				{
					if (isFirstLine)
						cell.Attributes.Add("class", classUnitCellUnselectedFirst);
					else
						cell.Attributes.Add("class", classUnitCellUnselected);
				}

				// Add the cell to the placeholder
				placeholder.Controls.Add( cell );
			}		
		}

		/// <summary>
		/// Generates the footer for the repeater, which contains the "Select" buttons
		/// </summary>
		/// <param name="rowTicket"></param>
		/// <param name="placeholder"></param>
		private void GenerateFooterColumns(PlaceHolder placeholder)
		{
			for (int index = 0; index < data.RetailUnits.Length; index++)
			{
				RetailUnit unit = data.RetailUnits[index];
				HtmlTableCell cell = new HtmlTableCell();
				cell.Align = "center";
				cell.VAlign = "middle";

				System.Web.UI.Control image = null;
				if (SelectedRetailUnit == unit)
				{
					cell.Attributes.Add("class", classSelectCellSelected);

					HtmlImage htmlImage = new HtmlImage();
					htmlImage.Src = resourceSelectSelectedURL;
					htmlImage.Alt = resourceSelectSelectedAlt;
					image = (System.Web.UI.Control)htmlImage;
				}
				else 
				{
					cell.Attributes.Add("class", classSelectCellUnselected);

					// If the control is in printer friendly mode, the 
					// Select button should be a standard image rather than
					// a TDbutton
					if (!PrinterFriendly)
					{
						TDButton selectButton = new TDButton();
						selectButton.Text = resourceSelectText;
						selectButton.CommandArgument = index.ToString(TDCultureInfo.InvariantCulture);
						image = (System.Web.UI.Control)selectButton;
					}
				}
				if (image != null)
					cell.Controls.Add( image );
				else
					cell.InnerHtml = "&nbsp;";
				placeholder.Controls.Add( cell );
			}
		}

		/// <summary>
		/// Retreives the resources and stores them in local variables. This is done because they are
		/// potentially used multiple times.
		/// </summary>
		private void LoadResources()
		{
			resourceTickURL = GetResource( "TicketMatrixControl.Tick.ImageUrl" );
			resourceTickAlt = GetResource( "TicketMatrixControl.Tick.ImageAlt" );
			resourceTicketURL = GetResource( "TicketMatrixControl.Ticket.ImageUrl" );
			resourceTicketAlt = GetResource( "TicketMatrixControl.Ticket.ImageAlt" );

			resourceLabelTo = GetResource( "TicketMatrixControl.labelTo" );

			resourceSingleJourneyTitle = GetResource( "TicketMatrixControl.SingleJourneyTitle" );
			resourceReturnJourneyTitle = GetResource( "TicketMatrixControl.ReturnJourneyTitle" );
			resourceOutward = GetResource( "TicketMatrixControl.SingleJourneyType.Outward" );
			resourceInward = GetResource( "TicketMatrixControl.SingleJourneyType.Inward" );
			resourceFor = GetResource( "TicketMatrixControl.For" );

			resourceSelectSelectedURL = GetResource( "TicketMatrixControl.Select.Selected.ImageUrl" );
			resourceSelectSelectedAlt = GetResource( "TicketMatrixControl.Select.Selected.ImageAlt" );
			resourceSelectText = GetResource( "TicketMatrixControl.Select.Text");
		}

		/// <summary>
		/// Populates all of the labels not on the repeater
		/// </summary>
		private void PopulateLabels()
		{
			// Populate the journey info labels
			if (data.ItineraryType == ItineraryType.Single)
			{
				if (data.IsForReturn)
					labelJourneyType.Text = String.Format( TDCultureInfo.InvariantCulture, resourceSingleJourneyTitle, resourceInward );
				else
					labelJourneyType.Text = String.Format( TDCultureInfo.InvariantCulture, resourceSingleJourneyTitle, resourceOutward );
				labelDate.Text = data.JourneyDate.ToString("ddd dd MMM yy");
			}
			else
			{
				labelJourneyType.Text = resourceReturnJourneyTitle;
				labelDate.Text = data.JourneyDate.ToString("ddd dd MMM yy") + 
					((returnJourneyDate != null) ? (" - " + returnJourneyDate.ToString("ddd dd MMM yy")) : string.Empty);
			}

			labelFor.Text = resourceFor;

		}

		/// <summary>
		/// Sets control visibility and binds the repeater
		/// </summary>
		private void DisplayData()
		{
			if ((data == null) || (this.Visible == false))
				return;
			else
			{
				// Bind the repeater.
				ticketsRepeater.DataSource = data.SelectedTickets;
				ticketsRepeater.DataBind();
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
			this.ticketsRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.ticketsRepeater_ItemDataBound);
			this.ticketsRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.ticketsRepeater_ItemCommand);

		}

		#endregion

	}
}
