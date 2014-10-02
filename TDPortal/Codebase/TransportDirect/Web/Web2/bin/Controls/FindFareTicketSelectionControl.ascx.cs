// *********************************************** 
// NAME                 : FindFareTicketSelectionControl.ascx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 07/02/2005
// DESCRIPTION			: This control is responsible for displaying tickets for a
//                        particular date and travel mode.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFareTicketSelectionControl.ascx.cs-arc  $
//
//   Rev 1.7   Oct 27 2010 13:54:56   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.6   Mar 26 2010 11:37:16   mmodi
//Addded CJP user flag to allow debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.5   Jan 26 2009 12:55:36   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Jul 08 2008 09:25:34   apatel
//Accessibility link CCN 458 updates
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//Resolution for 5034: CCN 0400 - NRE Ticket Type XML Feed
//
//   Rev 1.3   Jun 27 2008 14:18:46   apatel
//CCN 0400 Ticket type feed files
//Resolution for 5034: CCN 0400 - NRE Ticket Type XML Feed
//
//   Rev 1.2   Mar 31 2008 13:20:40   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:16   mturner
//Initial revision.
//
//   Rev 1.19   Jul 17 2007 10:56:36   asinclair
//Fixed missing char in langsting resource id
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.18   Jul 16 2007 18:23:24   asinclair
//Added code to set css class and text for discount col
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.17   Jul 10 2007 14:00:44   asinclair
//Changed code to set headers value in cells and id values in Headers
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.16   Jun 26 2007 11:40:28   asinclair
//Added code to link table containing headers with table containing data
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.15   Mar 21 2007 14:29:28   asinclair
//Correctly set the row limit value
//
//   Rev 1.14   Mar 06 2007 13:43:56   build
//Automatically merged from branch for stream4358
//
//   Rev 1.13.1.0   Mar 02 2007 11:55:46   asinclair
//Added code to display route restriction information if it exists.
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.13   Feb 23 2006 19:16:38   build
//Automatically merged from branch for stream3129
//
//   Rev 1.12.1.0   Jan 10 2006 15:24:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12   Apr 22 2005 14:48:52   COwczarek
//Correct condition tested to display partial results error
//Resolution for 2247: PT - Error handling by Retail Business Objects
//
//   Rev 1.11   Apr 22 2005 12:28:30   COwczarek
//Add comments
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.10   Apr 13 2005 12:08:40   COwczarek
//Use correct database property to obtain national rail website URL
//Resolution for 2121: PT: On the select a fare page, the ticket type hyperlinks do not link to anything
//
//   Rev 1.9   Apr 12 2005 08:47:24   rgeraghty
//Add pound symbol to discount fares
//Resolution for 2067: PT: £ symbol missing from Find Fare Ticket Selection
//
//   Rev 1.8   Apr 08 2005 08:57:28   jgeorge
//Corrected formatting issue with combined tickets and added $Log$ to header block.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Text;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.PricingRetail.Domain;
    using TransportDirect.UserPortal.ScreenFlow;

	/// <summary>
	/// This control is responsible for displaying tickets for a particular date and
	///	travel mode.
	/// </summary>
	public partial class FindFareTicketSelectionControl : TDPrintableUserControl, ILanguageHandlerIndependent
    {
        #region Private members

        /// <summary>
		/// Name of script (in script repository) used for highlighting selected row
		/// </summary>
		private const string ROW_HIGHLIGHTER_SCRIPT = "RowHighlighter";

		/// <summary>
		/// Repeater used to display list of tickets
		/// </summary>
		protected System.Web.UI.WebControls.Repeater repeaterResultTable;

		/// <summary>
		/// Radio button that invokes javascript to highlight selected row
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.ScriptableGroupRadioButton ticketRadioButton;
		
		/// <summary>
		/// Maximum number of row to show before allowing scrolling
		/// </summary>
		private const int ROW_LIMIT = 10;

		/// <summary>
		/// URL of national rail website. When appended with query string containing ticket code, is used
		/// to display web page containing ticket type description
		/// </summary>
		protected string nationalRailUrl;

		/// <summary>
		/// Index of currently selected row
		/// </summary>
		private int selectedIndex = 0;

		/// <summary>
		/// Row counter used when populating repeater to determine switching of background colour for 
		/// alternating rows
		/// </summary>
		private int index = 0;

		/// <summary>
		/// True if this table is displaying data for a cost search which return partial results, false
		/// otherwise
		/// </summary>
		private bool partialResults;

		/// <summary>
		/// True if showing open return tickets only, false otherwise
		/// </summary>
		private bool openReturnOnly;

		/// <summary>
		/// True if control being used to show a single, singles or open return ticket details, false for return
		/// </summary>
		private bool single;

		/// <summary>
		/// True if control being used to show the outward tickets when showing singles, false otherwise
		/// </summary>
		private bool outward;

		/// <summary>
		/// The outward date to which the tickets apply
		/// </summary>
		private TDDateTime currentOutwardDate;

        
		/// <summary>
		/// The return date to which the tickets apply
		/// </summary>
		private TDDateTime currentReturnDate;

		/// <summary>
		/// Panel containing label that indicates no tickets available for date/mode
		/// </summary>
		protected System.Web.UI.WebControls.Panel noTicketsPanel;

		/// <summary>
		/// Label that indicates no tickets available for date/mode
		/// </summary>        
		protected System.Web.UI.WebControls.Label noTicketsLabel;

		/// <summary>
		/// Panel containing ticket details
		/// </summary>
		protected System.Web.UI.WebControls.Panel ticketsPanel;

		/// <summary>
		/// Label that indicates the type of ticket being shown
		/// </summary>
		protected System.Web.UI.WebControls.Label headerTitleLabel;

		/// <summary>
		/// Label that indicates the outward and possibly return dates for the tickets being shown
		/// </summary>
		protected System.Web.UI.WebControls.Label headerDatesLabel;

		/// <summary>
		/// Date format used in header
		/// </summary>
		private const string TIME_FORMAT = "ddd dd MMM yy";

		/// <summary>
		/// The data source for this control
		/// </summary>
		private DisplayableCostSearchTickets tickets;

        /// <summary>
        /// Flag to allow additional debug info to be displayed on screen if logged on user has CJP status
        /// </summary>
        private bool cjpUser = false;

        #endregion

        /// <summary>
		/// Constructor.
		/// </summary>
		public FindFareTicketSelectionControl()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
		}

		#region Private methods

		/// <summary>
		/// Returns the currently selected ticket index
		/// </summary>
		/// <returns>The index of the currently selected ticket, or -1 if no selection</returns>
		private int GetSelectedTicketIndex()
		{
			
			foreach(RepeaterItem item in repeaterResultTable.Items)
			{
				ScriptableGroupRadioButton ticketRadioButton = item.FindControl("ticketRadioButton") as ScriptableGroupRadioButton;

				if (ticketRadioButton!=null && ticketRadioButton.Checked)
				{
					//update the selected ticket index
					return item.ItemIndex;
				}					
			}
				
			// no ticket has been selected 
			return -1;
		} 

		/// <summary>
		/// Highlights the currently selected ticket, setting the table row background to yellow and
		/// selecting the corresponding radio button. If this control is in printer friendly mode, no
		/// processing is performed. If Javascript is disabled, the row background is not changed but
		/// the radio button is set.
		/// </summary>
		private void SetSelectedTicketIndex()
		{
			if (!PrinterFriendly)
			{
				foreach(RepeaterItem item in repeaterResultTable.Items)
				{
					ScriptableGroupRadioButton ticketRadioButton = item.FindControl("ticketRadioButton") as ScriptableGroupRadioButton;

					if (ticketRadioButton!=null)
					{
						ticketRadioButton.Checked = (item.ItemIndex == selectedIndex);
						if (((TDPage)Page).IsJavascriptEnabled) 
						{
							HtmlTableRow row = (HtmlTableRow)item.FindControl("ticketTableRow");
							if (item.ItemIndex == selectedIndex ) 
							{
								row.Attributes["class"] = "tdtitemrowyellow";
								if (Scrollable()) 
								{
									((TDPage)Page).ScrollManager.ScrollElementToView(row.ClientID);
								}
							} 
							else 
							{
								if (item.ItemType == ListItemType.Item) 
								{
									row.Attributes["class"] = "tdtitemrow";
								} 
								else if (item.ItemType == ListItemType.AlternatingItem) 
								{
									row.Attributes["class"] = "tdtitemrowalt";
								}
							}
						}
					}
				}
			}
		} 

		/// <summary>
		/// Creates the HTML for the table cell heading the discount column
		/// </summary>
		/// <returns>HTML for the table cell heading the discount column</returns>
		private HtmlTableCell CreateDiscountCellHeader()
		{
			HtmlTableCell cell = new HtmlTableCell("th");
			cell.InnerText = GetResource("FindFareTicketSelectionControl.DiscountFareColumnHeader");
			cell.Attributes["id"]= "discountHeader";
			cell.Attributes["class"] = "tdtdiscountfarecolumnheader"; 
			return cell;
		}

		/// <summary>
		/// Creates the HTML for the table cell containing discounted ticket price
		/// </summary>
		/// <returns>HTML for the table cell containing discounted ticket price</returns>
		private HtmlTableCell CreateDiscountCellItem(DisplayableCostSearchTicket ticket) 
		{
			HtmlTableCell cell = new HtmlTableCell();
			if (ticket.DiscountedFare.Length !=0)
			{
				cell.InnerHtml = "<span class=\"pound\">&pound;</span>" + ticket.DiscountedFare;
			}
			cell.Attributes["class"] = "tdtdiscountfareitem";
			cell.Attributes["headers"] = "discountHeader";
			return cell;
		}

		/// <summary>
		/// Creates the HTML for the table cell containing the ticket names. This maybe the name of
		/// one or more tickets in the case of combined tickets. In the case of rail tickets, the
		/// name of each ticket is rendered as a hyperlink that redirects the user to the national rail
		/// site ticket information page.
		/// </summary>
		/// <param name="ticket">The displayble ticket that contains one or more tickets</param>
		/// <param name="cell">The cell to update</param>
		private void addTicketNames(DisplayableCostSearchTicket ticket, HtmlTableCell cell) 
		{
			if (tickets.Mode == TicketTravelMode.Coach) 
			{
				IList costSearchTickets = ticket.CostSearchTickets;
				foreach(CostSearchTicket costSearchTicket in costSearchTickets) 
				{
					if (cell.InnerHtml.Length == 0 ) 
					{
						cell.InnerHtml = Server.HtmlEncode( costSearchTicket.Code );
					} 
					else 
					{
						cell.InnerHtml += "<br/>" + Server.HtmlEncode( costSearchTicket.Code );
					}
				}
			} 
			else if (tickets.Mode == TicketTravelMode.Rail) 
			{
				CostSearchTicket costSearchTicket = ticket.CostSearchTickets[0];
				if (PrinterFriendly) 
				{
					Label ticketCode = new Label();
					ticketCode.Text = costSearchTicket.Code;
					cell.Controls.Add(ticketCode);
					//cell.InnerText = costSearchTicket.Code;
				} 
				else 
				{
                    // Get the PageController from Service Discovery
                    IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

                    // Get the PageTransferDataCache from the pageController
                    IPageTransferDataCache pageTransferDataCache = pageController.PageTransferDataCache;

                    // Get the PageTransferDetails object to which holds the Url
                    PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.PrintableTicketType);

					HyperLink link = new HyperLink();
					link.Target = "_blank";
                    link.NavigateUrl = "../" + pageTransferDetails.PageUrl + "?TicketTypeCode=" + costSearchTicket.ShortCode;
					link.Text = costSearchTicket.Code;
                    link.ToolTip = string.Format(GetResource("FindFareTicketSelectionControl.ticketTypeLinkToolTip"), costSearchTicket.Code);
					cell.Controls.Add(link);
				}

				//add code here to check hashtable for route restrictions
				if (tickets.RestrictionInfo.Count != 0) 
				{
					if (tickets.RestrictionInfo.ContainsKey(costSearchTicket.TicketRailFareData.RouteCode))
					{
						string text = tickets.RestrictionInfo[costSearchTicket.TicketRailFareData.RouteCode].ToString();
							
						Label routeLabel = new Label();
						routeLabel.Text= "<br/>Route: " + text;
						cell.Controls.Add(routeLabel);
						//routeLabel.CssClass= routesCssClass;
					}
				}

                // Add the debug info for a cjp logged on user
                if (cjpUser)
                {
                    if (costSearchTicket.TicketRailFareData != null)
                    {
                        Label debugLabel1 = new Label();
                        debugLabel1.CssClass = "txterror";
                        debugLabel1.Text = string.Format(
                            "ShortCode[{0}] Route[{1}] Restriction[{2}]",
                            costSearchTicket.TicketRailFareData.ShortTicketCode,
                            costSearchTicket.TicketRailFareData.RouteCode,
                            costSearchTicket.TicketRailFareData.RestrictionCode);

                        cell.Controls.Add(debugLabel1);

                        Label debugLabel2 = new Label();
                        debugLabel2.CssClass = "txterror";
                        debugLabel2.Text = string.Format(
                            "<br />OrigNLC/Actual[{0}/{1}] OrigName[{2}] DestNLC/Actual[{3}/{4}] DestName[{5}]",
                            costSearchTicket.TicketRailFareData.OriginNlc,
                            costSearchTicket.TicketRailFareData.OriginNlcActual,
                            costSearchTicket.TicketRailFareData.OriginName,
                            costSearchTicket.TicketRailFareData.DestinationNlc,
                            costSearchTicket.TicketRailFareData.DestinationNlcActual,
                            costSearchTicket.TicketRailFareData.DestinationName);

                        cell.Controls.Add(debugLabel2);
                    }
                }
			}

                
		}

		/// <summary>
		/// Returns value indicating whether the current table has a scrollbar.
		/// </summary>
		/// <returns>True if scrollbar available, false otherwise</returns>
		private bool Scrollable() 
		{
			return tickets.Length > ROW_LIMIT && !PrinterFriendly;
		}

		/// <summary>
		/// Set the title of the table to indicate the ticket type, travel mode and travel dates.
		/// </summary>
		private void SetTitle() 
		{
			string mode;
			switch (tickets.Mode) 
			{
				case TicketTravelMode.Rail:
					mode = "Rail";
					break;
				case TicketTravelMode.Coach:
					mode = "Coach";
					break;
				case TicketTravelMode.Air:
					mode = "Air";
					break;
				default:
					mode = string.Empty;
					break;
			}

			if (single) 
			{
				if (openReturnOnly) 
				{
					headerTitleLabel.Text = GetResource(string.Format("FindFareTicketSelectionControl.HeaderTitleOpenReturn{0}.Text",mode));
				} 
				else 
				{
					if (outward) 
					{
						headerTitleLabel.Text = GetResource(string.Format("FindFareTicketSelectionControl.HeaderTitleSingle{0}Outward.Text",mode));
					} 
					else 
					{
						headerTitleLabel.Text = GetResource(string.Format("FindFareTicketSelectionControl.HeaderTitleSingle{0}Return.Text",mode));
					}
				}
			} 
			else 
			{
				headerTitleLabel.Text = GetResource(string.Format("FindFareTicketSelectionControl.HeaderTitleReturn{0}.Text",mode));
			}

			if (single) 
			{
				headerDatesLabel.Text = GetResource("FindFareTicketSelectionControl.HeaderTitleFor.Text") +
					" " + currentOutwardDate.ToString(TIME_FORMAT);
			} 
			else 
			{
				headerDatesLabel.Text = GetResource("FindFareTicketSelectionControl.HeaderTitleFor.Text") + 
					" " + currentOutwardDate.ToString(TIME_FORMAT) + 
					", " + GetResource("FindFareTicketSelectionControl.HeaderTitleReturning.Text") + 
					" " + currentReturnDate.ToString(TIME_FORMAT);
			}
  
		}

		#endregion Private methods

		#region Properties

		/// <summary>
		/// Read/write property which is the data source for this control
		/// </summary>
		public DisplayableCostSearchTickets Tickets 
		{
			set 
			{
				tickets = value;
				index = 0;
				repeaterResultTable.DataSource = tickets;
				repeaterResultTable.DataBind();			
			}
			get 
			{
				return tickets;
			}
		}


		/// <summary>
		/// Read/write property that is the index of the currently selected row
		/// </summary>
		public int SelectedIndex 
		{
			get
			{
				selectedIndex = GetSelectedTicketIndex();
				return selectedIndex;
			}
			set
			{
				selectedIndex = value;
				SetSelectedTicketIndex();
			}
		}

		/// <summary>
		/// Read/write property. True if control being used to show a single, singles or open return ticket details, false for return
		/// </summary>
		public bool Single 
		{
			get {return single;}
			set {single = value;}
		}

		/// <summary>
		/// Read/write property. True if control being used to show the outward tickets when showing singles, false otherwise
		/// </summary>
		public bool Outward 
		{
			get {return outward;}
			set {outward = value;}
		}

		/// <summary>
		/// Read/write property. The outward date to which the tickets apply
		/// </summary>
		public TDDateTime CurrentOutwardDate
		{
			get {return currentOutwardDate;}
			set {currentOutwardDate = value;}
		}

		/// <summary>
		/// Read/write property. The return date to which the tickets apply
		/// </summary>
		public TDDateTime CurrentReturnDate
		{
			get {return currentReturnDate;}
			set {currentReturnDate = value;}
		}

		/// <summary>
		/// Read/write property. True if this table is displaying data for a cost search which return partial results, false
		/// otherwise
		/// </summary>
		public bool PartialResults
		{
			get {return partialResults;}
			set {partialResults = value;}
		}

		/// <summary>
		/// Read/write property. True if showing open return tickets only, false otherwise
		/// </summary>
		public bool OpenReturnOnly
		{
			get {return openReturnOnly;}
			set {openReturnOnly = value;}
		}

		/// <summary>
		/// read-only property used to hide table cell using CSS
		/// </summary>
		/// <param name="visible"></param>
		/// <returns></returns>
		protected string CellCss()
		{
			if (!tickets.HasDiscountedFares)
			{
				return "hide";
			}
			else
			{
				return "screenreader";
			}
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

        #endregion Properties

		#region Protected methods

        /// <summary>
        /// Returns value indicating if client has Javascript enabled.
        /// </summary>
        /// <returns>True if enabled, false otherwise</returns>
		protected bool GetEnableClientScript() 
		{
			return ((TDPage)Page).IsJavascriptEnabled;
		}

        /// <summary>
        /// Returns value indicating whether selection of supplied ticket should be enabled or disabled.
        /// </summary>
        /// <param name="ticket">Ticket to test</param>
        /// <returns>Return if enabled, false otherwise</returns>
        protected bool GetEnabled(DisplayableCostSearchTicket ticket) 
        {
            return ticket.CostSearchTickets[0].Probability != Probability.None;
        }

        /// <summary>
        /// Returns the Javascript function to execute when the radio button is clicked
        /// </summary>
        /// <returns>Javascript to execute when the radio button is clicked</returns>
        protected string GetAction()
		{
			return
				"return highlightSelectedItem('" + 
				GetTableId() + 
				"', 'tdtitemrow','tdtitemrowalt','tdtitemrowyellow');";
		}

        /// <summary>
        /// Returns the name of the Javascript file containing the code to execute when the radio button is clicked
        /// </summary>
        /// <returns></returns>
		protected string GetScriptName() 
		{
			return ROW_HIGHLIGHTER_SCRIPT;
		}

		/// <summary>
		/// Used to set the radio group name of the radio buttons in the 
		/// item and footer template of the repeater control.
		/// </summary>
		/// <returns>Radio (group) name string</returns>
		protected string GetGroupName()
		{
			return repeaterResultTable.ClientID;			
		}

		/// <summary>
		/// Used to set the Id attribute of the Html table in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetTableId()
		{
			return repeaterResultTable.ClientID;			
		}

		/// <summary>
		/// Used to set the  headers attribute of the Type Column in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetTypeHeaderId()
		{
			return repeaterResultTable.ClientID + "__ctl0_HeaderType";			
		}

		/// <summary>
		/// Used to set the headers attribute of the Flexibility Column in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetFlexHeaderId()
		{
			return repeaterResultTable.ClientID + "__ctl0_flexHeader";			
		}

		/// <summary>
		/// Used to set the headers attribute of the Fare Column in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetFareHeaderId()
		{
			return repeaterResultTable.ClientID + "__ctl0_fareHeader";			
		}

		/// <summary>
		/// Used to set the headers attribute of the Probability Column in the control	
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetProbHeaderId()
		{
			return repeaterResultTable.ClientID + "__ctl0_probHeader";			
		}

		/// <summary>
		/// Used to set the  headers attribute of the Select Column in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetSelectHeaderId()
		{
			return repeaterResultTable.ClientID + "__ctl0_selectHeader";			
		}

        
        /// <summary>
        /// Used to set the id attribute for headings
        /// </summary>
        /// <param name="headerId">header column </param>
        /// <returns>table column header id</returns>

        protected string GetHeaderId(int headerId)
        {
            return repeaterResultTable.ClientID + "__ctl0_Header" + headerId;
        }

        /// <summary>
        /// Used to set the discount header column id		
        /// </summary>
        /// <returns>table discount column header id</returns>
        protected string GetDiscountHeaderId()
        {
            return repeaterResultTable.ClientID + "__ctl0_DiscountHeader";
        }

		/// <summary>
		/// Used to set the Id attribute of the Html table in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string SetHeaderId()
		{   
			return repeaterResultTable.ClientID + "__ctl0_HeaderType";			
		}

        /// <summary>
        /// Returns the CSS class name to use for the ticket row currently being rendered
        /// </summary>
        /// <returns>CSS class name to use for the ticket row currently being rendered</returns>
		protected string GetRowClass() 
		{
			return (index++ % 2) == 0 ? "tdtitemrow":"tdtitemrowalt";
		}

        /// <summary>
        /// Returns the CSS class name to use for the column containing the scroll bar (the column
        /// width varies to accommodate the scroll bar). The class returned will vary depending on whether
        /// the ticket table is scrollable.
        /// </summary>
        /// <returns>CSS class name to use for the column containing the scroll bar</returns>
		protected string GetScrollBarColumnClass()
		{
			if (Scrollable())
			{
				return "tdtselectitemscroll";
			} 
			else 
			{
				return "tdtselectitem";
			}
		}

        /// <summary>
        /// Returns the CSS class name to use for ticket table box body. This will vary depending on whether 
        /// the table is scrollable and whether or not the discounted fare column is showing (both factors
        /// affect box width).
        /// </summary>
        /// <returns>CSS class name to use for ticket table box</returns>
		protected string GetTicketBoxClass()
		{
			if (Scrollable())
			{
				return tickets.HasDiscountedFares ?
					"boxtypetdtitemsscroll" : "boxtypetdtitemsnodiscountscroll";
			} 
			else 
			{
				return tickets.HasDiscountedFares ?
					"boxtypetdtitems" : "boxtypetdtitemsnodiscount";
			}
		}

        /// <summary>
        /// Returns the CSS class name to use for ticket table box header. This will vary depending on whether 
        /// or not the discounted fare column is showing (affects box width).
        /// </summary>
        /// <returns>CSS class name to use for ticket table box header</returns>
        protected string GetTicketBoxHeaderClass()
		{
			return tickets.HasDiscountedFares ?
				"boxtypetdtheader" : "boxtypetdtheadernodiscount";
		}

        /// <summary>
        /// Returns the CSS class name to use for box containing the table title. This will vary depending on
        /// whether the discounted fare column is showing (affects box width).
        /// </summary>
        /// <returns>CSS class name to use for box containing the table title</returns>
        protected string GetTicketBoxHeaderTitleClass()
        {
            return tickets.HasDiscountedFares ?
                "boxtypetdtheadertitle" : "boxtypetdtheadertitlenodiscount";
        }

        /// <summary>
        /// Returns the internationlised text for the ticket type column header
        /// </summary>
        /// <returns>internationlised text for the ticket type column header</returns>
		protected string GetTicketTypeColumnHeaderText() 
		{
			return GetResource("FindFareTicketSelectionControl.TicketTypeColumnHeader");
		}

        /// <summary>
        /// Returns the internationlised text for the flexibility column header
        /// </summary>
        /// <returns>internationlised text for the flexibility column header</returns>
        protected string GetFlexibilityColumnHeaderText() 
		{
			return GetResource("FindFareTicketSelectionControl.FlexibilityColumnHeader");
		}

        /// <summary>
        /// Returns the internationlised text for the fare column header
        /// </summary>
        /// <returns>internationlised text for the fare column header</returns>
        protected string GetFareColumnHeaderText() 
		{
			if (tickets.AdultTickets) 
			{
				return GetResource("FindFareTicketSelectionControl.AdultFareColumnHeader");
			} 
			else 
			{
				return GetResource("FindFareTicketSelectionControl.ChildFareColumnHeader");
			}
		}

        /// <summary>
        /// Returns the internationlised text for the probability column header
        /// </summary>
        /// <returns>internationlised text for the probability column header</returns>
        protected string GetProbabilityColumnHeaderText() 
		{
			return GetResource("FindFareTicketSelectionControl.ProbabilityColumnHeader");
		}

		/// <summary>
		/// Returns the internationlised text for the discount fare column header
		/// </summary>
		/// <returns>internationlised text for the discount column header</returns>
		protected string GetDiscountColumnHeaderText() 
		{
			return GetResource("FindFareTicketSelectionControl.DiscountFareColumnHeader");
		}

        /// <summary>
        /// Returns the internationlised text for the radio button column header
        /// </summary>
        /// <returns>internationlised text for the radio button column header</returns>
        protected string GetSelectColumnHeaderText() 
		{
			return GetResource("FindFareTicketSelectionControl.SelectColumnHeader");
		}

		#endregion Protected methods

		#region Event handlers

        /// <summary>
        /// Event handler for page PreRender event fired by page
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, System.EventArgs e)
		{
            if (tickets.Length == 0) 
            {
                noTicketsPanel.Visible = true;
                if (partialResults) 
                {
                    noTicketsLabel.Text = GetResource("FindFareTicketSelectionControl.NoSingleTicketsPartialResults");
                } 
                else if (single) 
                {
                    noTicketsLabel.Text = GetResource("FindFareTicketSelectionControl.NoSingleTickets");
                } 
                else 
                {
                    noTicketsLabel.Text = GetResource("FindFareTicketSelectionControl.NoReturnTickets");
                }
            } 
            else 
            {
                SetSelectedTicketIndex();
                SetTitle();
                noTicketsPanel.Visible = false;
            }
            ticketsPanel.Visible = !noTicketsPanel.Visible;

		}

        /// <summary>
        /// Event handler for page Load event fired by page
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if (((TDPage)Page).IsJavascriptEnabled && !PrinterFriendly) 
			{
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];				
				// Output reference to necessary JavaScript file from the ScriptRepository
				Page.ClientScript.RegisterClientScriptBlock(typeof(FindFareTicketSelectionControl), ROW_HIGHLIGHTER_SCRIPT, scriptRepository.GetScript(ROW_HIGHLIGHTER_SCRIPT, ((TDPage)Page).JavascriptDom));
			}

		}		
		
		/// <summary>
		/// Item Created event handler for the repeater generating ticket detail rows
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void item_Created(object sender, RepeaterItemEventArgs  e) 
		{		
			HtmlTableRow tableRow = null;
	        ListItemType itemType = e.Item.ItemType;

            if ((itemType == ListItemType.Item) || (itemType == ListItemType.AlternatingItem)) 
            {
                tableRow = (HtmlTableRow)e.Item.FindControl("ticketTableRow");
			
            }
            else if (itemType == ListItemType.Header)
            {
                tableRow = (HtmlTableRow)e.Item.FindControl("ticketTableHeader");
            }        
    		

            
            // if discounted fares exist, add a discounted fares column
            if (tickets.HasDiscountedFares) 
			{
				if ((itemType == ListItemType.Item) || (itemType == ListItemType.AlternatingItem))
				{
					tableRow.Cells.Insert(3,
						CreateDiscountCellItem((DisplayableCostSearchTicket)e.Item.DataItem));
					
 
				} 
				else if (itemType == ListItemType.Header)
				{
					tableRow.Cells.Insert(3,CreateDiscountCellHeader());
				}
	
			}

            // if in printer friendly mode, remove radion button column
			if (PrinterFriendly) 
			{
                if ((itemType == ListItemType.Header) || (itemType == ListItemType.Item) || (itemType == ListItemType.AlternatingItem))
                    tableRow.Cells.RemoveAt(tableRow.Cells.Count-1);
			}

            if ((itemType == ListItemType.Item) || (itemType == ListItemType.AlternatingItem)) 
            {
                HtmlTableCell tableCell = (HtmlTableCell)tableRow.FindControl("ticketName");
                addTicketNames((DisplayableCostSearchTicket)e.Item.DataItem, tableCell);
            }

            if (itemType != ListItemType.Header && itemType != ListItemType.Footer)
            {
                for (int i = 0; i < tableRow.Cells.Count; i++)
                {
                    tableRow.Cells[i].Attributes["headers"] = repeaterResultTable.ClientID + "__ctl0_Header" + (i+1);
                }
            }

		}

		#endregion Event handlers

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			repeaterResultTable.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.item_Created);
            nationalRailUrl = ((IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService])["FareDetailsTableSegmentControl.NationalRailTicketInfoUrl"];
        }
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

        }
		#endregion

	}
}
