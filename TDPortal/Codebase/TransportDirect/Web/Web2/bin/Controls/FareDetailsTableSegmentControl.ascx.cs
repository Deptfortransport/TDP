// *********************************************** 
// NAME                 : FareDetailsTableSegmentControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 18/01/2005
// DESCRIPTION			: A custom control to display a list of 
//						  possible fares for a PricingUnit
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FareDetailsTableSegmentControl.ascx.cs-arc  $
//
//   Rev 1.16   Jan 24 2013 14:00:16   mmodi
//Corrected width when no return fares and select column hidden
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.15   Jan 23 2013 17:21:32   mmodi
//Hide buy fares button for accessible journeys
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.14   Jan 23 2013 14:27:36   mmodi
//Null checks added to fix error on journey containing coach and rail modes
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.13   Oct 11 2012 14:05:52   mmodi
//Updated cjp user output information styling
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.12   Nov 18 2010 09:37:16   apatel
//Updated to implement cached route restriction information provider
//Resolution for 5639: Fares page breaks with connection time out errors
//
//   Rev 1.11   Oct 26 2010 14:30:30   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.10   Oct 26 2010 13:39:10   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.9   Mar 26 2010 11:36:24   mmodi
//Added CJP user flag to allow debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.8   Sep 21 2009 17:09:26   nrankin
//Accessibility - Opens in new window (CCN0535)
//Resolution for 5320: Accessibility - Opens in new window
//
//   Rev 1.7   Feb 26 2009 17:24:04   mmodi
//Show the "Do not buy ticket" option when there is at least one fare
//Resolution for 5265: Do not buy this fare option is not shown when only One ticket is available
//
//   Rev 1.6   Oct 17 2008 11:54:50   build
//Automatically merged from branch for stream0093
//
//   Rev 1.5.1.1   Oct 13 2008 15:58:10   jfrank
//Updated for XHTML compliance
//Resolution for 93: Stream IR for Del 10.4 maintenance fixes
//
//   Rev 1.5.1.0   Aug 14 2008 16:15:36   pscott
//SCR 5100 usd 1898063
//Amend problems with disallowed discount fares
//If no discount fare to display then display the normal fare
//Resolution for 5100: Fare display problem when selecting child with adult discount card
//
//   Rev 1.5   Jul 08 2008 10:59:24   apatel
//updated for xml ticket type feed
//Resolution for 5034: CCN 0400 - NRE Ticket Type XML Feed
//
//   Rev 1.4   Jul 08 2008 09:25:26   apatel
//Accessibility link CCN 458 updates
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//Resolution for 5034: CCN 0400 - NRE Ticket Type XML Feed
//
//   Rev 1.3   Jun 27 2008 14:18:44   apatel
//CCN 0400 Ticket type feed files
//Resolution for 5034: CCN 0400 - NRE Ticket Type XML Feed
//
//   Rev 1.2   Mar 31 2008 13:20:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:22   mturner
//Initial revision.
//
//   Rev 1.38   Aug 06 2007 15:16:00   asinclair
//Cosmetic fix for Coach Fare display issue
//Resolution for 4446: NX Fares: Display issue for Coach fares
//
//   Rev 1.37   Jun 13 2007 15:29:18   mmodi
//Updated display of Return fares message above to only display when its a matching return, and added additional condition when both Single and Return fares are not available
//Resolution for 4427: 9.6 - Return Coach Fares for different operators displayed incorrectly
//
//   Rev 1.36   Jun 07 2007 15:19:58   asinclair
//Fixed cosmetic issues with spaces in Fares display
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.35   Jun 06 2007 16:02:24   asinclair
//Added returnCoachJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.34   Jun 06 2007 12:44:32   asinclair
//Added code for singleCoachJourneyNewFares = true
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.33   May 31 2007 14:57:12   asinclair
//Tidy code and allow coach journeys to display the 'View single fares from' link
//
//   Rev 1.32   May 25 2007 16:22:20   build
//Automatically merged from branch for stream4401
//
//   Rev 1.31.1.0   May 22 2007 13:18:42   mmodi
//Added NewAndOldCoachFares flag for new NX fares, which is used in detemining the value for nocoachfares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.31   May 15 2007 13:06:52   mmodi
//Corrected colspan value to resolve cosmetic issue when no (return) coach fares available
//
//   Rev 1.30   May 02 2007 14:09:38   asinclair
//Added check and display options for when there are no coach fares
//
//   Rev 1.29   Apr 23 2007 18:29:02   asinclair
//Set col spans to be correct when in Printer Friendly mode
//
//   Rev 1.28   Apr 23 2007 17:55:48   mmodi
//Updated to correct Coach mode column spacing issue
//
//   Rev 1.27   Apr 18 2007 12:02:48   asinclair
//Updated to fix Improved Rail Fares issues
//
//Rev 1.26   Apr 16 2007 18:03:24   mmodi
//Added test for mode RailReplacementBus when creating TicketType cell
//Resolution for 4387: Improved Rail Fares: Return RailReplacementBus fares not shown
//
//   Rev 1.25   Apr 04 2007 13:56:58   asinclair
//Check for restriction info to be null to enable it to work with coach fares
//
//   Rev 1.24   Mar 29 2007 21:56:12   asinclair
//Changed the value of journeyDescriptionCell.colspan
//
//   Rev 1.23   Mar 28 2007 15:54:34   asinclair
//Added fixes for the integration of ImprovedRailFares and LocalZonal Services
//
//   Rev 1.22   Mar 02 2007 12:22:58   asinclair
//Added code to get the fare route data from the database and display it on the portal, and events for when cheaper fare is clicked.
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.21   Mar 14 2006 10:30:16   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.20   Feb 23 2006 16:10:42   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.19.1.0   Mar 01 2006 13:21:00   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.19   Nov 26 2005 14:29:10   mguney
//In the BindData() method of FareDetailsTableSegmentControl, null check was applied for priceUnit.
//Resolution for 3193: DN040: find a coach - server error on selecting child fares
//
//   Rev 1.18   Nov 17 2005 16:17:52   jgeorge
//Minor correction to logic for determining which label to display when no fares are present.
//Resolution for 3108: DN40: Journey Fares page displayes empty ticket table when no tickets found
//
//   Rev 1.17   Nov 14 2005 18:10:40   mguney
//null check is done for ticketList and pricingResult in BindData and SetLabelVisibility methods.
//Resolution for 3015: DN040: Null reference error on SBT when multi operator journeys selected
//
//   Rev 1.16   Nov 03 2005 17:08:38   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.15.1.0   Oct 24 2005 17:00:40   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.15   May 09 2005 15:22:28   rgeraghty
//Updated SetSelectedTicket method to check for ticket being non-null
//
//   Rev 1.14   May 09 2005 14:42:58   jgeorge
//Use overridden .Equals method on ticket object instead of comparison with ==
//
//   Rev 1.13   Apr 30 2005 13:46:24   jgeorge
//Added "No tickets available" label and code to show/hide/populate as required.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.12   Apr 29 2005 09:24:00   jgeorge
//Make sure Upgrades column is never displayed when in Printer Friendly mode.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.11   Apr 12 2005 14:10:16   pcross
//IR1710 (vantive 3319715)
//Don't display child fare age range of (0 to 0 years)
//
//   Rev 1.10   Apr 05 2005 14:06:18   rgeraghty
//fx cop changes, plus commenting
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.9   Apr 04 2005 16:38:58   rgeraghty
//Changes made to accommodate Welsh translation
//
//   Rev 1.8   Mar 30 2005 18:14:42   rgeraghty
//Updated to reflect changes to Ticket class for upgrades
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.7   Mar 15 2005 16:53:54   rgeraghty
//ticketTypeLink.NavigateUrl updated to reflect correct control name
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.6   Mar 14 2005 16:13:30   rgeraghty
//Changed ticket upgrade code for new DFt changes
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Mar 03 2005 17:57:48   rgeraghty
//FxCop changes made
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 01 2005 15:03:00   rgeraghty
//First version
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Feb 11 2005 14:58:14   rgeraghty
//Work in progress
//
//   Rev 1.2   Feb 09 2005 10:16:46   rgeraghty
//Work in progress
//
//   Rev 1.1   Jan 19 2005 14:42:46   rgeraghty
//Work in progress
//
//   Rev 1.0   Jan 18 2005 17:18:40   rgeraghty
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{

	#region using
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;

	using System.Data.SqlClient;

	using TransportDirect.UserPortal.JourneyControl;
	using PricingRetailDomain = TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.SessionManager;
	using CJPInterfaceAlias = TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.Common.DatabaseInfrastructure;
    using TransportDirect.UserPortal.ScreenFlow;
    using TransportDirect.Common;
    using TransportDirect.UserPortal.PricingRetail.Domain;

    using Logger = System.Diagnostics.Trace;
    using TransportDirect.Common.Logging;
	

	#endregion

	/// <summary>
	///	A custom control which displays a list of fares for a PricingUnit
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class FareDetailsTableSegmentControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{

		#region controls

		
		
		protected TransportDirect.UserPortal.Web.Controls.ScriptableGroupRadioButton ticketRadioButton;
		
		#endregion controls
		
		#region constants
		private const string javaScriptFileName = "RowHighlighter";
		private const string rowPrefixCssClass = "fdtcrow";
		private const string rowhighlightedPrefixCssClass = "fdtcrowy";
		private const string rowAlternatePrefixCssClass = "fdtcrowg";
		private const string ticketTypeCssClass = "fdtcbody1";
		private const string flexibilityCssClass = "fdtcbody2";
		private const string faresCssClass = "fdtcbody3";
		private const string discountsCssClass = "fdtcbody4";
		private const string upgradesCssClass = "fdtcbody5";
		private const string ticketRadioButtonCssClass = "fdtcbody6";
		private const string footerRowCssClass = "fdtcbody7";
		private const string noTicketRadioButtonCssClass = "fdtcbody8";
		private const string routesCssClass = "fdtcRoute";
		private const string scriptableRadioButtonId = "ticketRadioButton";
		private const string segmentRowId = "segmentRow";
		private const string infoButtonId = "infoButton";
		private const string nonbreakingSpace = "&nbsp;";
		

		private const int noSelection = -1;
		
		#endregion

		#region private members

		private PricingRetailDomain.PricingUnit priceUnit; 
		private PricingRetailDomain.Ticket selectedTicket;
		private int selectedTicketIndex;		
		private bool showChildFares;
		private string railDiscount = string.Empty;
		private string coachDiscount = string.Empty;
		private PricingRetailDomain.ItineraryType overrideItineraryType;
		private IList ticketList;
		protected System.Web.UI.WebControls.Label routeLabel; 
		private bool hideTicketSelection;
		private IList otherTicketList;
		private IList filteredTicketList;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl otherFaresLinkControl;
		private bool coachNoFare;
		private bool newAndOldCoachFares;
		private bool singleCoachJourneyNewFares;
		private bool returnCoachJourneyNewFares;

		/// <summary>
		/// Collection of Ticket Codes belonging to a displayableCostSearchTicket object
		/// </summary>
		private ArrayList listTicketTypeCodes;

		/// <summary>
		/// Collection of Route Codes
		/// </summary>
		private ArrayList listRouteCodes;

		private ArrayList routesList;

		/// <summary>
		/// Hashtable containing the RouteCode and Restriction information text
		/// </summary>
		private Hashtable restrictionInfo;

		private bool returnfaresincluded;

        /// <summary>
        /// Flag to allow additional debug info to be displayed on screen if logged on user has CJP status
        /// </summary>
        private bool cjpUser = false;

		#endregion

		#region Events
		/// <summary>
		/// Event raised when the user clicks an "Info" button.
		/// </summary>
		public event EventHandler InfoButtonClicked;

		/// <summary>
		/// Event raised when the user clicks the single/return HyperlinkPostbackControl.
		/// </summary>
		public event EventHandler OtherFaresClicked;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public FareDetailsTableSegmentControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//									
			InitializeComponent();
			this.fareTicketRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.item_Created);
			this.fareTicketRepeater.ItemCommand += new RepeaterCommandEventHandler(fareTicketRepeater_ItemCommand);
			this.otherFaresLinkControl.link_Clicked +=new EventHandler(otherFaresLinkControl_link_Clicked);
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
		/// Initialise the control
		/// </summary>
		private void Initialise()
		{					
			InitialiseControls();
			BindData();						
		}

		/// <summary>
		/// Bind the ticket data to the control
		/// </summary>
		private void BindData()
		{
			PricingRetailDomain.PricingResult pricingResultReturn = priceUnit.ReturnFares;
			PricingRetailDomain.PricingResult pricingResultSingle = priceUnit.SingleFares;
			
			//This means it is a single outward journey with new fares
            if ((overrideItineraryType.Equals(PricingRetailDomain.ItineraryType.Return)) && (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach) && singleCoachJourneyNewFares)
            {
                otherTicketList = CreateTicketList(pricingResultSingle);
                ticketList = CreateTicketList(pricingResultReturn);

                if (ticketList != null)
                    ticketList.Clear();
            }
            //Return Journey with the return segment as new fares
            else if ((overrideItineraryType.Equals(PricingRetailDomain.ItineraryType.Return)) && (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach) && returnCoachJourneyNewFares)
            {
                otherTicketList = CreateTicketList(pricingResultSingle);
                ticketList = CreateTicketList(pricingResultReturn);

                if (ticketList != null)
                    ticketList.Clear();
            }
            else if (overrideItineraryType.Equals(PricingRetailDomain.ItineraryType.Return))
            {
                ticketList = CreateTicketList(pricingResultReturn);
                otherTicketList = CreateTicketList(pricingResultSingle);
            }
            else
            {
                ticketList = CreateTicketList(pricingResultSingle);
                otherTicketList = CreateTicketList(pricingResultReturn);
                
                if (singleCoachJourneyNewFares && otherTicketList != null)
                    otherTicketList.Clear();
                if (returnCoachJourneyNewFares && otherTicketList != null)
                    otherTicketList.Clear();
            }

		
			if (priceUnit.Mode != CJPInterfaceAlias.ModeType.Coach)
			{
				if(ticketList != null)
				{
					restrictionInfo = FareRoutes(ticketList);
				}
			}

			if (ticketList != null) 
			{			
				fareTicketRepeater.DataSource = new int[ticketList.Count + 1];
				fareTicketRepeater.DataBind();
			}	
									
			SetLabelVisibility();

			if(!this.PrinterFriendly)
			{
				string cheaperFare = string.Empty;
				if ((otherTicketList != null) && ( otherTicketList.Count > 0))
				{
					faresViewLabel.Visible = true;
					faresViewLabel.Text =  GetResource("FareDetailsTableSegmentControl.ViewFares");

					PricingRetailDomain.Ticket ticket = (PricingRetailDomain.Ticket)otherTicketList[0];
					if(!DisplayDiscountFares())
					{
						if(!showChildFares)
						{
							cheaperFare = ticket.AdultFare.Equals( float.NaN )?string.Empty : string.Format("{0:N}",ticket.AdultFare );
		
						}
						else
						{
							cheaperFare = ticket.ChildFare.Equals( float.NaN )?string.Empty : string.Format("{0:N}",ticket.ChildFare );
						}
					}
					else
					{
                        if (!showChildFares)
                        {
                            cheaperFare = ticket.DiscountedAdultFare.Equals(float.NaN) ? string.Empty : string.Format("{0:N}", ticket.DiscountedAdultFare);
                            if (cheaperFare.Length == 0)
                            {
                                cheaperFare = ticket.AdultFare.Equals(float.NaN) ? string.Empty : string.Format("{0:N}", ticket.AdultFare);
                            }
                        }
                        else
                        {
                            cheaperFare = ticket.DiscountedChildFare.Equals(float.NaN) ? string.Empty : string.Format("{0:N}", ticket.DiscountedChildFare);
                            if (cheaperFare.Length == 0)
                            {
                                cheaperFare = ticket.ChildFare.Equals(float.NaN) ? string.Empty : string.Format("{0:N}", ticket.ChildFare);
                            }
                        }
					}

						if (overrideItineraryType.Equals( PricingRetailDomain.ItineraryType.Return )) 
						{
							otherFaresLinkControl.Text = GetResource("FareDetailsTableSegmentControl.SingleFrom")+ " £" + cheaperFare;
							faresViewLabel.Visible = true;
							//need to reduce by 1 if no fares or on pf page
							if ((ticketList.Count > 0) && (!priceUnit.MatchingReturn))
							{
								if(DisplayDiscountFares())
								{
									journeyDescriptionCell.ColSpan=(GetNumberOfColumns())-2;
								}
								else
								{
									journeyDescriptionCell.ColSpan=(GetNumberOfColumns())-3;
								}
							
							}
							else
							{
								journeyDescriptionCell.ColSpan=(hideTicketSelection ? GetNumberOfColumns() - 1 : GetNumberOfColumns() );
							}
						}
						else
						{
							otherFaresLinkControl.Text = GetResource("FareDetailsTableSegmentControl.ReturnFrom")+ " £" +  cheaperFare;
							faresViewLabel.Visible = true;
							if ((ticketList.Count > 0) && (!priceUnit.MatchingReturn))
							{
								if(DisplayDiscountFares())
								{
									journeyDescriptionCell.ColSpan=(GetNumberOfColumns())-2;
								}
								else
								{
									journeyDescriptionCell.ColSpan=(GetNumberOfColumns())-3;
								}
							}
							else
							{
								journeyDescriptionCell.ColSpan=(GetNumberOfColumns())-1;
							}
						}
					
				}
				else
				{
					if (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach)
					{
						journeyDescriptionCell.ColSpan=(GetNumberOfColumns());
						otherFaresLinkControl.Visible = false;
					}
					else
					{
						journeyDescriptionCell.ColSpan=(GetNumberOfColumns() -1);
						otherFaresLinkControl.Visible = false;
					}
				}
			}

			DataBind(); //this ensure that the table id gets set
		}

		/// <summary>
		/// Bind the ticket data to the control
		/// </summary>
		private IList CreateTicketList(PricingRetailDomain.PricingResult pricingResult )
		{
			
			//filter the ticket list according to whether adult or child tickets are to be displayed
			if (pricingResult != null)
			{
				filteredTicketList = ItineraryAdapter.FilterTickets( pricingResult.Tickets,!showChildFares);			

				//set the child label age ranges
				if(showChildFares)
				{
					int MinChildAge = pricingResult.MinChildAge;
					int MaxChildAge = pricingResult.MaxChildAge;

					if(MinChildAge == 0 && MaxChildAge == 0)
					{
						// Avoid heading of (0 - 0 years) from return of poor data giving these values
						// by not setting any text.
						// Note that I not checked for scenario where we get age range of (5 to 0 years) say. This is probably
						// validated against in the CJP return anyhow but if not at least it shows you a minimum age.
					}
					else
					{
						childAgeLabel.Text = string.Format(GetResource("FareDetailsTableSegmentControl.labelChildYears.Text"),
							MinChildAge.ToString(), MaxChildAge.ToString());
					}
				}
			}

			return filteredTicketList;

		}

        /// <summary>
        /// Gets the fare route restriction information
        /// </summary>
        /// <param name="ticketlist"></param>
        /// <returns></returns>
		public Hashtable FareRoutes(IList ticketlist)
		{
            RouteRestrictionsCatalogue routeRestrictions = (RouteRestrictionsCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.RouteRestrictionsCatalogue];

            Hashtable restrictionInfo = new Hashtable();

            try
            {
                //Go through the pricingResult and identify any fares that have the same ticket type.
                listTicketTypeCodes = new ArrayList();
                listRouteCodes = new ArrayList();
                routesList = new ArrayList();
                foreach (PricingRetailDomain.Ticket ticket in ticketList)
                {
                    listRouteCodes.Add(ticket.TicketRailFareData.RouteCode);
                }

                foreach (string ticketRoute in listRouteCodes)
                {
                    // Only add route restriction information if they are not in the hashtable already
                    if (!restrictionInfo.ContainsKey(ticketRoute))
                    {
                        string restrictionDesc = routeRestrictions.GetRouteRestrictionForCode(ticketRoute);

                        if (!string.IsNullOrEmpty(restrictionDesc))
                        {
                            restrictionInfo.Add(ticketRoute, restrictionDesc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                restrictionInfo = new Hashtable();
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Fare Routes Route Restriction Error - " + ex.Message);

                Logger.Write(operationalEvent);
            }
			

			return restrictionInfo;		
		}


		/// <summary>
		/// Sets up the label and table summary text
		/// </summary>
		private void InitialiseControls()
		{
			//Initialise labels			
			ticketTypeLabel.Text = GetResource("FareDetailsTableSegmentControl.labelTicketTypeHeadingText");
			flexibilityLabel.Text = GetResource("FareDetailsTableSegmentControl.labelFlexibilityHeadingText");
			adultFareLabel.Text = GetResource("FareDetailsTableSegmentControl.labelAdultFareHeadingText");
			childFareLabel.Text = GetResource("FareDetailsTableSegmentControl.labelChildFareHeadingText");
			noFaresFound.Text = GetResource("FareDetailsTableSegmentControl.NoFaresInformationAvailable.Text");
			noTicketsAvailable.Text = GetResource("FareDetailsTableSegmentControl.NoTicketsAvailable");
			noThroughFares.Text = GetResource("FareDetailsTableSegmentControl.NoThroughFares");
									
			//set the pricing unit description
			journeySegmentDescription.Text = ItineraryAdapter.FromLocationName(priceUnit) + " to " + 
				ItineraryAdapter.ToLocationName(priceUnit);		

			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			coachNoFare = ((returnfaresincluded && 
				priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach && 
				options.OverrideItineraryType == PricingRetail.Domain.ItineraryType.Return && 
				priceUnit.MatchingReturn));

			// The following is done to ensure e.g. if outward has New NX coach fares, and return has Old coach fares,
			// we show the return fares for each, rather than the Return journey showing "Return fares included above".			
			// newAndOldCoachFares value is set within the JourneyFaresControl
			if (newAndOldCoachFares)  
			{
				coachNoFare = coachNoFare && !newAndOldCoachFares;
			}

			GetModeFaresLabelText();
		}


		/// <summary>
		/// Sets the fares label text defpending on the pricing unit mode
		/// Currently there are only two modes which have fares, Rail and coach
		/// </summary>
		private void GetModeFaresLabelText()
		{
			switch(priceUnit.Mode)
			{
				case CJPInterfaceAlias.ModeType.Rail:
					faresLabel.Text = GetResource("FareDetailsTableSegmentControl.labelFaresText.Rail");
					break;
				case CJPInterfaceAlias.ModeType.Coach:
					faresLabel.Text = GetResource("FareDetailsTableSegmentControl.labelFaresText.Coach");
					break;
				default:
					faresLabel.Text = GetResource("FareDetailsTableSegmentControl.labelFaresText");
					break;
			}
		}
		
		/// <summary>
		/// Sets visibility of the control's labels
		/// Visibility depends on whether there is any ticket information to display
		/// </summary>
		private void SetLabelVisibility ()
		{
			//hide the nofares found label by default
			noFaresFound.Visible = false;
			noTicketsAvailable.Visible = false;
			noThroughFares.Visible = false;

			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			if(coachNoFare)
			{
				noFaresFound.Text = GetResource("FareDetailsTableSegmentControl.FaresAbove");
				noFaresFound.Visible = true;
				ticketTypeLabel.Visible = false;
				flexibilityLabel.Visible = false;
				adultFareLabel.Visible = false;
				childFareLabel.Visible = false;
				childAgeLabel.Visible = false;	
				journeyDescriptionCell.ColSpan= 5;
			}

			else if((returnfaresincluded) && (priceUnit.MatchingReturn) && (overrideItineraryType.Equals( PricingRetailDomain.ItineraryType.Return )) && (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach))
			{
				journeyDescriptionCell.ColSpan= 5;
			}

			else
				if(priceUnit.SingleFares == null && priceUnit.ReturnFares == null)
			{
				noFaresFound.Visible = true;
				ticketTypeLabel.Visible = false;
				flexibilityLabel.Visible = false;
				adultFareLabel.Visible = false;
				childFareLabel.Visible = false;
				childAgeLabel.Visible = false;	
				journeyDescriptionCell.ColSpan= 5;

                SetVariableColumns();
			}
			else
			{
				//hide all the label headers if no fares are found
				if ((ticketList == null) || (ticketList.Count == 0))
				{
					ticketTypeLabel.Visible = false;
					flexibilityLabel.Visible = false;
					adultFareLabel.Visible = false;
					childFareLabel.Visible = false;
					childAgeLabel.Visible = false;	
				
					if((priceUnit.Mode!= CJPInterfaceAlias.ModeType.Coach) &&(priceUnit.SingleFares.NoThroughFaresAvailable))
					{
						noTicketsAvailable.Visible = false;
						noFaresFound.Visible = false;
						noThroughFares.Visible = true;
					}
					else
					{
						// Check to see if there are no tickets because they were all unavailable
						if ( ( ticketList != null ) && (overrideItineraryType == PricingRetail.Domain.ItineraryType.Single) && ( priceUnit.SingleFares.NoPlacesAvailableForSingles ) )
						{
							noTicketsAvailable.Visible = true;
							noFaresFound.Visible = false;
						}
						else if ( ( ticketList != null ) && (overrideItineraryType == PricingRetail.Domain.ItineraryType.Return ) && ( priceUnit.ReturnFares.NoPlacesAvailableForReturns ) )
						{
							noTicketsAvailable.Visible = true;
							noFaresFound.Visible = false;
						}
						else
						{
							if((returnfaresincluded) && (priceUnit.MatchingReturn)) 
							{	// Only want to display this message if its a matching return, otherwise we 
								// may incorrectly tell the user the fare is valid on a different route (non-matching return)
								noFaresFound.Text = GetResource("FareDetailsTableSegmentControl.FaresAbove");

							}
							else if(priceUnit.SingleFares == null && priceUnit.ReturnFares == null)
							{
								noTicketsAvailable.Visible = false;

							}
							else if(priceUnit.SingleFares == null && priceUnit.ReturnFares != null)
							{
								noFaresFound.Text = GetResource("FareDetailsTableSegmentControl.NoSingles");
							
							}
							else if (priceUnit.SingleFares.Tickets.Count == 0 && priceUnit.ReturnFares.Tickets.Count == 0)
							{	// Ensures we tell user no fare information was found, rather than the message in the next else if
								noFaresFound.Text = GetResource("FareDetailsTableSegmentControl.NoFaresInformationAvailable.Text");
							
							}
							else if (priceUnit.SingleFares !=null && priceUnit.ReturnFares.Tickets.Count == 0)
							{
								noFaresFound.Text = GetResource("FareDetailsTableSegmentControl.NoReturns");
							
							}
							//The text of the noFaresFound label needs to change depending on if
							//there are fares or not.
							noFaresFound.Visible = true;
						}
					}

                    //ensure the journeydescription cell spans the maximum number of columns
                    SetVariableColumns();
				}
				else
				{			
					childFareLabel.Visible = showChildFares;
					childAgeLabel.Visible = showChildFares;
					adultFareLabel.Visible = !showChildFares;	
					SetVariableColumns();
				}
			}

		}
		
		/// <summary>
		/// Sets visibility of Header cells for discounts and upgrades 
		/// </summary>
		private void SetVariableColumns()
		{
			bool showDiscounts = DisplayDiscountFares();
			bool showUpgrades = DisplayUpgrades();
			bool showRadioButtons = (!this.PrinterFriendly && !this.hideTicketSelection);

			//set the visibility of the discounts cells
			if (!showDiscounts)
				headerCellDiscounts.Visible= false;
			else
			{
				headerCellDiscounts.InnerText= GetResource("FareDetailsTableSegmentControl.labelDiscountFareHeadingText");
				headerCellDiscounts.Visible=true;
			}
			
			//set the visibility of the upgrades cell
			if (!showUpgrades)			
				headerCellUpgrades.Visible= false;
			else
			{
				headerCellUpgrades.InnerText=GetResource("FareDetailsTableSegmentControl.labelUpgradesHeadingText");			
				headerCellUpgrades.Visible= true;
			}

			//set the visibility of the radio button cell
			if (!showRadioButtons)
				headerCellSelect.Visible = false;				
			else
			{
				headerCellSelect.InnerText = GetResource("FareDetailsTableSegmentControl.labelRadioButtonHeadingText");				
				headerCellSelect.Visible = true;	
			}

			//ensure the journeydescription cell spans the correct number of columns
			journeyDescriptionCell.ColSpan= (GetNumberOfColumns());

			if(this.PrinterFriendly)
				journeyDescriptionCell.ColSpan = ((GetNumberOfColumns())-1);
			
		}

		/// <summary>
		/// Creates the footer row for the control
		/// </summary>		
		/// <param name="index">The index of the footer row</param>
		/// <returns>A table row populated with the details of the footer</returns>
		private TableRow CreateFooterRow(int index ) 
		{
			TableRow row = new TableRow();						
			row.CssClass = rowPrefixCssClass + GetCssClassSuffix(index); 			
			row.ID = segmentRowId;

			TableHeaderCell headerCell;	
			//add the no ticket description option
			headerCell = CreateFooterNoTicketCell();
			row.Cells.Add( headerCell );
			
			TableCell cell;			
			//add the radio button for the no ticket option
			cell = CreateFooterNoTicketSelectorCell();
			row.Cells.Add( cell );			
			return row;

		}

		/// <summary>
		/// Creates a table cell for the TicketType
		/// </summary>
		/// <param name="ticket">The ticket to display</param>
		/// <param name="index">The index of the ticket</param>
		/// <param name="printerFriendlyLastRow">Indicates whether ticket type cell is for the last row of the printer friendly table</param>
		/// <returns>A table cell populated with the ticket type details of the provided ticket</returns>
		private TableHeaderCell CreateTicketTypeCell( PricingRetailDomain.Ticket ticket,int index, bool printerFriendlyLastRow ) 
		{
			TableHeaderCell cell= new TableHeaderCell(); 		
			if (printerFriendlyLastRow)
				cell.CssClass = footerRowCssClass; 
			else
				cell.CssClass = ticketTypeCssClass; 
			
			//for printer friendly version do not want to display ticket type as a hyperlink
			if (this.PrinterFriendly) 
			{
				Label ticketLabel = new Label();
				ticketLabel.Text = ticket.Code;
				cell.Controls.Add(ticketLabel);
			}
			else
			{	// Add the hyperlink to the ticket type information
				if ((ticket.Code.Length != 0) && 
					((priceUnit.Mode == CJPInterfaceAlias.ModeType.Rail) ||
					(priceUnit.Mode == CJPInterfaceAlias.ModeType.RailReplacementBus))
					) 
				{
                    // Get the PageController from Service Discovery
                    IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

                    // Get the PageTransferDataCache from the pageController
                    IPageTransferDataCache pageTransferDataCache = pageController.PageTransferDataCache;

                    // Get the PageTransferDetails object to which holds the Url
                    PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.PrintableTicketType);

                    string openNewWindowImageUrl = GetResource("langStrings", "ExternalLinks.OpensNewWindowImage");

                    HyperLink ticketTypeLink = new HyperLink();
                    ticketTypeLink.Text = ticket.Code + " " + openNewWindowImageUrl;
                    ticketTypeLink.NavigateUrl = "../" + pageTransferDetails.PageUrl + "?TicketTypeCode=" + ticket.ShortCode; // "../JourneyPlanning/printableTicketType.aspx?TicketTypeCode="   Properties.Current["TicketType.PrintableTicketTypePage.URL"] + 
                    if (ticket.Code != null)
                        ticketTypeLink.ToolTip = string.Format(GetResource("langStrings", "FareDetailsTableSegmentControl.ticketTypeLinkToolTip"), ticket.Code);
                    cell.Controls.Add(ticketTypeLink);
                    ticketTypeLink.Target = "_blank";
                    ticketTypeLink.CssClass = ticketTypeCssClass;
				}
				else
					cell.Text = ticket.Code;
			}

			if(restrictionInfo != null)
			{

				if (restrictionInfo.ContainsKey(ticket.TicketRailFareData.RouteCode))
				{ 
						
					string text = restrictionInfo[ticket.TicketRailFareData.RouteCode].ToString();
						
					Label routeLabel = new Label();
					routeLabel.Text= "<br />Route: " + text;
					cell.Controls.Add(routeLabel);
					routeLabel.CssClass= routesCssClass;
				}
			}

            // Add the debug info for a cjp logged on user
            if (cjpUser && CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.FareDetail))
            {
                if (ticket.TicketRailFareData != null)
                {
                    Label debugLabel1 = new Label();
                    debugLabel1.CssClass = "cjperror";
                    debugLabel1.Text = string.Format(
                        "<br />ShortCode[{0}] Route[{1}] Restriction[{2}]",
                        ticket.TicketRailFareData.ShortTicketCode,
                        ticket.TicketRailFareData.RouteCode,
                        ticket.TicketRailFareData.RestrictionCode);

                    cell.Controls.Add(debugLabel1);

                    Label debugLabel2 = new Label();
                    debugLabel2.CssClass = "cjperror";
                    debugLabel2.Text = string.Format(
                        "<br />OrigNLC/Actual[{0}/{1}] OrigName[{2}] DestNLC/Actual[{3}/{4}] DestName[{5}]",
                        ticket.TicketRailFareData.OriginNlc,
                        ticket.TicketRailFareData.OriginNlcActual,
                        ticket.TicketRailFareData.OriginName,
                        ticket.TicketRailFareData.DestinationNlc,
                        ticket.TicketRailFareData.DestinationNlcActual,
                        ticket.TicketRailFareData.DestinationName);

                    cell.Controls.Add(debugLabel2);
                }
            }

			return cell;
		}

		/// <summary>
		/// Creates a table cell for the Footer item
		/// </summary>
		/// <returns>A table cell populated with the footer details for no ticket selection</returns>
		private TableHeaderCell CreateFooterNoTicketCell()
		{						
			TableHeaderCell headerCell = new TableHeaderCell();
			headerCell.CssClass = footerRowCssClass;

			if (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach)
				headerCell.ColumnSpan= GetNumberOfColumns();
			else
				headerCell.ColumnSpan= GetNumberOfColumns()-1;

			headerCell.Text = GetResource("FareDetailsTableSegmentControl.footerText");
			return headerCell;
		}

		/// <summary>
		/// Creates a table cell for the Flexibility column
		/// </summary>
		/// <param name="ticket">The ticket to display</param>		
		/// <param name="printerFriendlyLastRow">Indicates whether flexibility cell is for the last row of the printer friendly table</param>
		/// <returns>A table cell populated with the flexibility details of the provided ticket</returns>
		private TableCell CreateFlexibilityCell( PricingRetailDomain.Ticket ticket, bool printerFriendlyLastRow )
		{
			TableCell cell= new TableCell(); 
			if (printerFriendlyLastRow)
				cell.CssClass = footerRowCssClass;
			else
				cell.CssClass = flexibilityCssClass;
			
			cell.Text = GetResource("FareDetailsTableSegmentControl." + ticket.Flexibility.ToString()) + " ";

			return cell;
		}

		/// <summary>
		/// Creates a table cell for the Discounts column
		/// </summary>
		/// <param name="ticket">The ticket to display</param>
		/// <param name="printerFriendlyLastRow">Indicates whether discounts cell is for the last row of the printer friendly table</param>
		/// <returns>A table cell populated with the Discounts details of the provided ticket</returns>
		private TableCell CreateDiscountsCell( PricingRetailDomain.Ticket ticket, bool printerFriendlyLastRow)
		{
			TableCell cell= new TableCell(); 			
			if (printerFriendlyLastRow)
				cell.CssClass = footerRowCssClass;
			else
				cell.CssClass=discountsCssClass;

			//set child discount fares display
			if (showChildFares)
			{				
				string discFareText = ticket.DiscountedChildFare.Equals( float.NaN )? string.Empty : string.Format("{0:N}",ticket.DiscountedChildFare );
				if (discFareText.Length !=0)
				{
					Label labelPound = new Label();				
					labelPound.Text = "£";
					labelPound.CssClass = "pound";					
					cell.Controls.Add(labelPound);				

					Label labelFare = new Label();
					labelFare.Text = discFareText;
					cell.Controls.Add(labelFare);	
				}
			}
			else //set adult discount fares display
			{
				string discFareText = ticket.DiscountedAdultFare.Equals( float.NaN )? string.Empty : string.Format("{0:N}",ticket.DiscountedAdultFare );
				if (discFareText.Length !=0)
				{
					Label labelPound = new Label();				
					labelPound.Text = "£";
					labelPound.CssClass = "pound";					
					cell.Controls.Add(labelPound);				

					Label labelFare = new Label();				
					labelFare.Text = discFareText;
					cell.Controls.Add(labelFare);	
				}
			}			

			if (cell.Controls.Count == 0) // then no discounts has been added - need to add an empty label for formatting purposes
			{
				Label labelEmpty = new Label();				
				labelEmpty.Text = nonbreakingSpace;				
				cell.Controls.Add(labelEmpty);
			}
			return cell;
		}

		/// <summary>
		/// Creates a table cell for the Upgrades column
		/// </summary>
		/// <param name="ticket">The ticket to display</param>
		/// <param name="index">The index of the ticket</param>
		/// <param name="printerFriendlyLastRow">Indicates whether upgrades cell is for the last row of the printer friendly table</param>
		/// <returns>A table cell populated with the upgrade details of the provided ticket</returns>
		private TableCell CreateUpgradesCell(PricingRetailDomain.Ticket ticket,int index, bool printerFriendlyLastRow)
		{
			TableCell cell= new TableCell(); 			
			if (printerFriendlyLastRow)
				cell.CssClass = footerRowCssClass; 
			else
				cell.CssClass=upgradesCssClass; 
			
			if (this.PrinterFriendly) 
				cell.Text = nonbreakingSpace; // don't display any upgrade text if in printer friendly mode
			else
			{	
				// Add the link to the upgrade info page
				if ((ticket.Upgrades != null) && (ticket.Upgrades.Length != 0))
				{										
					TDButton infoButton = new TDButton();
					infoButton.ID = infoButtonId;
					infoButton.Text = GetResource("FareDetailsTableSegmentControl.upgradeInfo.Text");

                    // add a secondary button style in addition to the default
                    infoButton.CssClass = Properties.Current["WebControlLibrary.TDButton.DefaultStyle"].ToString();
                    infoButton.CssClass += " TDButtonSecondary";

                    infoButton.CssClassMouseOver = Properties.Current["WebControlLibrary.TDButton.DefaultMouseOverStyle"].ToString();
                    infoButton.CssClassMouseOver += " TDButtonSecondaryMouseOver";
				
					cell.Controls.Add(infoButton);
				}

				if (cell.Controls.Count == 0) // then no ticket type has been added - need to add an empty label for formatting purposes
				{
					Label labelEmpty = new Label();				
					labelEmpty.Text = nonbreakingSpace;				
					cell.Controls.Add(labelEmpty);
				}
				
			}
			return cell;

		}

		/// <summary>
		/// Creates a table cell for the Fares column
		/// </summary>
		/// <param name="ticket">The ticket to display</param>
		/// <param name="printerFriendlyLastRow">Indicates whether fares cell is for the last row of the printer friendly table</param>
		/// <returns>A table cell populated with the fare details of the provided ticket</returns>
		private TableCell CreateFaresCell( PricingRetailDomain.Ticket ticket, bool printerFriendlyLastRow)
		{
			TableCell cell = new TableCell(); 
			
			//set the css style for the cell
			if (printerFriendlyLastRow)
				cell.CssClass = footerRowCssClass;
			else			
				cell.CssClass=faresCssClass;			

			if (showChildFares)
			{
				if ( ticket != null  ) 
				{
					//set the child fare text
					string faretext = ticket.ChildFare.Equals( float.NaN )?string.Empty : string.Format("{0:N}",ticket.ChildFare );;					
					if (faretext.Length !=0)
					{
						Label labelPound = new Label();				
						labelPound.Text = "£";
						labelPound.CssClass = "pound";					
						cell.Controls.Add(labelPound);				

						Label labelFare = new Label();				
						labelFare.Text = faretext;
						cell.Controls.Add(labelFare);											
					}
				}
			}
			else
			{
				if ( ticket != null  ) 
				{
					//set the adult fare text
					string faretext = ticket.AdultFare.Equals( float.NaN )?string.Empty : string.Format("{0:N}",ticket.AdultFare );
					if (faretext.Length !=0)
					{
						Label labelPound = new Label();				
						labelPound.Text = "£";
						labelPound.CssClass = "pound";					
						cell.Controls.Add(labelPound);

						Label labelFare = new Label();				
						labelFare.Text =  faretext;
						cell.Controls.Add(labelFare);					
					}
				}
			}		
			
			// if no fares have been added then add an empty label for formatting purposes
			if (cell.Controls.Count == 0) 
			{
				Label labelEmpty = new Label();				
				labelEmpty.Text = nonbreakingSpace;				
				cell.Controls.Add(labelEmpty);
			}
			return cell;
		}


		/// <summary>
		/// Creates a table cell for the RadioButton column
		/// </summary>		
		/// <returns>A table cell populated with the no ticket selector radiobutton</returns>
		private TableCell CreateFooterNoTicketSelectorCell ()
		{
			TableCell cell = new TableCell();  
			cell.CssClass = noTicketRadioButtonCssClass;

			//cell for radio select button
			ScriptableGroupRadioButton sgr = new ScriptableGroupRadioButton();									
			sgr.ID= scriptableRadioButtonId;
			sgr.GroupName = GroupName();
			sgr.EnableClientScript = ((TDPage)Page).IsJavascriptEnabled;
			
			// If JavaScript currently supported
			if (sgr.EnableClientScript)
			{																						
				sgr.Action= "return highlightSelectedItem('" + GetTableId + "', '" + rowPrefixCssClass + "', '" + rowAlternatePrefixCssClass  + "', '" + rowhighlightedPrefixCssClass  + "');";				
				sgr.ScriptName = javaScriptFileName;			

				string javaScriptDom = ((TDPage)Page).JavascriptDom;
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			
				// Output reference to necessary JavaScript file from the ScriptRepository
				Page.ClientScript.RegisterClientScriptBlock(typeof(FareDetailsTableSegmentControl), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));
							
			}
			cell.Controls.Add(sgr);			
			return cell;
		}


		/// <summary>
		/// Creates a table cell for the RadioButton column
		/// </summary>		
		/// <param name="index">The index of the ticket</param>
		/// <param name="printerFriendlyLastRow">Indicates whether ticket cell is for the last row of the printer friendly table</param>
		/// <returns>A table cell populated with the ticket selector radiobutton for the provided ticket</returns>
		private TableCell CreateTicketSelectorCell ( bool printerFriendlyLastRow)
		{
			TableCell cell = new TableCell();  			
			if (printerFriendlyLastRow)
				cell.CssClass = footerRowCssClass;
			else
				cell.CssClass = ticketRadioButtonCssClass;

			//cell for radio select button
			ScriptableGroupRadioButton sgr = new ScriptableGroupRadioButton();									
			sgr.ID=scriptableRadioButtonId;
			sgr.GroupName = GroupName();				
			sgr.EnableClientScript = ((TDPage)Page).IsJavascriptEnabled;

			// If JavaScript currently supported
			if (sgr.EnableClientScript)
			{														
				sgr.Action= "return highlightSelectedItem('" + GetTableId + "', '" + rowPrefixCssClass + "', '" + rowAlternatePrefixCssClass  + "', '" + rowhighlightedPrefixCssClass  + "');";					

				sgr.ScriptName = javaScriptFileName;			

				string javaScriptDom = ((TDPage)Page).JavascriptDom;
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

				// Output reference to necessary JavaScript file from the ScriptRepository
                Page.ClientScript.RegisterClientScriptBlock(typeof(FareDetailsTableSegmentControl), javaScriptFileName, scriptRepository.GetScript(javaScriptFileName, javaScriptDom));				
			}

			cell.Controls.Add(sgr);
			
			return cell;

		}

		/// <summary>
		/// Creates a table row for the provided ticket
		/// </summary>
		/// <param name="ticket">The ticket to display</param>
		/// <param name="index">The index of the ticket</param>
		/// <param name="printerFriendlyLastRow">used for displaying printer friendly last row
		/// correctly as in printer friendly mode do not need to show the footer row</param>
		/// <returns>A table row populated with the details of the provided ticket</returns>
		private TableRow CreateItemRow( PricingRetailDomain.Ticket ticket, int index, bool printerFriendlyLastRow ) 
		{			
			TableRow row = new TableRow();						
			row.CssClass = rowPrefixCssClass + GetCssClassSuffix(index); 
			row.ID = segmentRowId;

			TableHeaderCell headerCell;
			headerCell = CreateTicketTypeCell ( ticket,index,printerFriendlyLastRow );
			row.Cells.Add( headerCell );
			
			TableCell cell;
			cell = CreateFlexibilityCell(ticket, printerFriendlyLastRow);
			row.Cells.Add( cell );
			
			cell = CreateFaresCell(ticket, printerFriendlyLastRow);
			row.Cells.Add( cell);

			if (DisplayDiscountFares())
			{
				cell = CreateDiscountsCell(ticket, printerFriendlyLastRow);
				row.Cells.Add( cell);
			}

			if (DisplayUpgrades())
			{
				cell = CreateUpgradesCell(ticket, index, printerFriendlyLastRow);
				row.Cells.Add(cell);
			}
			
			if ((!this.PrinterFriendly) && (!this.hideTicketSelection))
			{
				cell =CreateTicketSelectorCell(printerFriendlyLastRow);
				row.Cells.Add( cell );
			}
						
			return row;
		}

		/// <summary>
		/// Returns the number of columns required by the control
		/// </summary>
		/// <returns></returns>
		private int GetNumberOfColumns()
		{
			int minColumns = 3;

			if((otherTicketList != null) && (otherTicketList.Count == 0 && (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach))) 
                minColumns = 2;
			
			if(!singleCoachJourneyNewFares && (priceUnit.Mode == CJPInterfaceAlias.ModeType.Coach))
				minColumns = 2;

			if (DisplayDiscountFares())
				minColumns ++;
		
			if (DisplayUpgrades())
				minColumns ++;

			if (!this.PrinterFriendly && !this.hideTicketSelection) //for the radio button selection column
				minColumns ++;

			return minColumns;
		}

		/// <summary>
		/// Indicates whether control is to display discount fares
		/// </summary>
		private bool DisplayDiscountFares()
		{			
			bool display = false;
			switch (priceUnit.Mode) 
			{
				case CJPInterfaceAlias.ModeType.Coach:
					if ( coachDiscount.Length !=0 ) 
						display = true;
					break;
				case CJPInterfaceAlias.ModeType.RailReplacementBus:
				case CJPInterfaceAlias.ModeType.Rail:
					if ( railDiscount.Length !=0 ) 
						display = true;
					break;
				default:
					break;
			}
			return display;
			
		}

		/// <summary>
		/// Indicates whether control is to display ticket upgrade information
		/// </summary>
		private bool DisplayUpgrades()
		{			
			if (PrinterFriendly || (priceUnit.SingleFares == null && priceUnit.ReturnFares == null))
				// If page is in printer friendly mode, never display the upgrades column
				return false;
			else
			{
				// Otherwise display is dependent on mode and presence of an upgrade in at 
				// least one of the tickets
				bool display = false;
				switch (priceUnit.Mode) 
				{
					case CJPInterfaceAlias.ModeType.Rail:
					
						foreach (PricingRetailDomain.Ticket ticket in ticketList)
						{
							if ((ticket.Upgrades != null) && (ticket.Upgrades.Length > 0))
								display = true;	 // return true if there are any tickets with ticket upgrade info
						}
						break;
					default: // all other modes have no upgrades
						break;
				}
				return display;
			}
		}

		/// <summary>
		/// Sets the selected ticket index
		/// </summary>
		private void SetSelectedTicket(PricingRetailDomain.Ticket ticket)
		{
			//if ticket is the 'empty' ticket then the 'I don't want to buy option needs to be selected'
			if ((ticket !=null) && (ticket.Equals( PricingRetailOptionsState.NoTicket) ))

				selectedTicketIndex = ticketList.Count; //set to last one in the list which is always the no ticket option

			else if (ticket == null)
			{			
				selectedTicketIndex = 0; //default to first ticket in the list 
			}
			else
			{
				foreach(PricingRetailDomain.Ticket item in ticketList)
				{
					//update the selected ticket index
					if (ticket.Equals(item))	
						selectedTicketIndex = ticketList.IndexOf(item);					
				}					
			}		
			SetSelectedTicketIndex();
		}

		/// <summary>
		/// Returns the currently selected ticket
		/// </summary>
		private PricingRetailDomain.Ticket GetSelectedTicket()
		{
			int index = GetSelectedTicketIndex();
			if (index == noSelection)
				if ((ticketList==null) || (ticketList.Count==0))
					return null;
				else
					return (PricingRetailDomain.Ticket) ticketList[0];	//default to first ticket					
			else if (index == ticketList.Count) //no ticket is selected, return an empty ticket				
				return PricingRetailOptionsState.NoTicket;
			else
				return (PricingRetailDomain.Ticket) ticketList[index];						
		}

		/// <summary>
		/// Returns the currently selected ticket index
		/// </summary>
		private int GetSelectedTicketIndex()
		{			
			foreach(RepeaterItem item in fareTicketRepeater.Items)
			{
				ScriptableGroupRadioButton ticketRadioButton = item.FindControl(scriptableRadioButtonId) as ScriptableGroupRadioButton;

				if (ticketRadioButton!=null && ticketRadioButton.Checked)
					//update the selected ticket index
					return item.ItemIndex;
			}
				
			// nothing is selected
			return noSelection;
		} 
		

		/// <summary>
		/// Checks the selected radio button and highlights associated row
		/// </summary>
		private void SetSelectedTicketIndex()
		{
			foreach(RepeaterItem item in fareTicketRepeater.Items)
			{
				ScriptableGroupRadioButton ticketRadioButton = item.FindControl(scriptableRadioButtonId) as ScriptableGroupRadioButton;			

				if (ticketRadioButton!=null)
					ticketRadioButton.Checked = (item.ItemIndex == selectedTicketIndex);
			}
		}

		/// <summary>
		/// Fired when the other fares button is clicked
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void otherFaresLinkControl_link_Clicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);	

		}


		#endregion

		#region public properties


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


		/// <summary>
		/// Gets/Sets the itinerarytype to ensure single/return fares are
		/// displayed
		/// </summary>
		public PricingRetailDomain.ItineraryType OverrideItineraryType
		{
			get {return overrideItineraryType;}
			set {overrideItineraryType = value;}
		}

		/// <summary>
		/// The pricing unit for which the control is displaying fares
		/// </summary>
		public PricingRetailDomain.PricingUnit PriceUnit
		{
			get {return priceUnit;}
			set {priceUnit = value;}			
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
		/// Returns the table summary
		/// </summary>
		/// <returns></returns>
		public string GetTableSummary
		{
			get {return GetResource("FareDetailsTableSegmentControl.tableSummaryText");}			
		}
	
		/// <summary>
		/// Used to set the Id attribute of the Html table in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		public string GetTableId
		{
			get{return this.ClientID;}
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
		/// Read/write property for the currently selected ticket 
		/// </summary>
		public PricingRetailDomain.Ticket SelectedTicket
		{
			get 
			{
				selectedTicket = GetSelectedTicket();					
				return selectedTicket;				
			}
			set 
			{
				SetSelectedTicket(value);
				selectedTicket = value;
			}
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
        /// Read/write. Flag to allow additional debug info to be displayed on screen if 
        /// logged on user has CJP status
        /// </summary>
        public bool CJPUser
        {
            get { return cjpUser; }
            set { cjpUser = value; }
        }

		#endregion		

		#region Public methods
			
		/// <summary>
		/// Returns the Css class that the text should be rendered with.
		/// </summary>
		/// <param name="index">item being rendered.</param>
		/// <returns>Css class string.</returns>
		public string GetCssClassSuffix(int index)
		{			
			//highlight selected row if javascript supported and ticket selection is not hidden			
			if( index == selectedTicketIndex && ((TDPage)Page).IsJavascriptEnabled 
					&& !this.PrinterFriendly && !this.hideTicketSelection)
				return "y";

			return (index % 2) == 0 ? "":"g";			
		}

		/// <summary>
		/// Used to set the radio group name of the radio buttons in the 
		/// item and footer template of the repeater control.
		/// </summary>
		/// <returns>Radio (group) name string</returns>
		public string GroupName()
		{
			return fareTicketRepeater.ClientID;			
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
			Initialise();		
		}


		/// <summary>
		/// Item Created event handler for the repeaters
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void item_Created(object sender, RepeaterItemEventArgs  e) 
		{	PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;					
			if (ticketList.Count != 0)
			{
				//ensure that a row is added only for item types and not for the header or footer
				if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType==ListItemType.AlternatingItem))
				{
					//for the footer item add the footer row which is not a ticket item, only if in non-printer friendly mode
					if (e.Item.ItemIndex == ticketList.Count) 
					{
                        //if there is at least one ticket offered then display option to not buy this part of journey
                        if (ticketList.Count > 0)
                        {
                            if ((!this.PrinterFriendly) & (!this.hideTicketSelection) & (!coachNoFare))
                            {
                                TableRow row = CreateFooterRow(e.Item.ItemIndex);
                                e.Item.Controls.Add(row);
                            }
                        }
					}
					else //add a ticket item
					{
						PricingRetailDomain.Ticket ticket = (PricingRetailDomain.Ticket)ticketList[e.Item.ItemIndex];	
																
						bool printerFriendlyLastRow = false;
						//if we are dealing with the second to last item and in printerfriendly mode
						if ((e.Item.ItemIndex == ticketList.Count -1) && ((this.PrinterFriendly) || (this.hideTicketSelection)))
							printerFriendlyLastRow = true;
						if(!coachNoFare)
						{
							TableRow row = CreateItemRow( ticket, e.Item.ItemIndex, printerFriendlyLastRow );
							e.Item.Controls.Add( row );
						}
					}
				}			
			}
		}

		/// <summary>
		/// PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{					
			GetModeFaresLabelText();
			BindData();			
			SetSelectedTicketIndex();
		}

		/// <summary>
		/// Fired when a button is clicked in the repeater control
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareTicketRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);		
		}

		#endregion


	}

}
