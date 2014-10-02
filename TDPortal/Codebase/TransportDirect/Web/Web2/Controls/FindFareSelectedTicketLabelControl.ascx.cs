// *********************************************** 
// NAME                 : FindFareSelectedTicketLabelControl.ascx.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 08/03/2005
// DESCRIPTION			: Used to display a label showing the currently selected Ticket(s).
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFareSelectedTicketLabelControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 27 2010 13:44:18   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:20:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:12   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:16:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:24:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   May 09 2005 14:56:04   rhopkins
//Corrected handling of Discount fares
//Resolution for 2113: PT - Discount cards not working on find a fare
//
//   Rev 1.5   Apr 26 2005 14:36:02   rgreenwood
//IR 2113: Fixed display of discount fares
//Resolution for 2113: PT - Discount cards not working on find a fare
//
//   Rev 1.4   Apr 15 2005 14:16:50   rhopkins
//When determining layout of text, use TicketType of selected TravelDate, rather than TicketType originally requested by User.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.3   Apr 08 2005 15:12:24   rhopkins
//Correct display of multiple tickets
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.2   Apr 01 2005 19:38:04   rhopkins
//Display multiple ticket names
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.1   Mar 14 2005 16:50:08   rhopkins
//Check in without name resolution to allow build
//
//   Rev 1.0   Mar 09 2005 18:11:02   rhopkins
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Text;
	using System.Web.UI.WebControls;
    using TransportDirect.Common;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
    /// Used to display page title on a journey
    /// planner output page. Text is determined based on the current
    /// output page id and the state of the itinerary manager.
	/// </summary>
	public partial  class FindFareSelectedTicketLabelControl : TDUserControl
	{


		FindCostBasedPageState pageState;

		/// <summary>
		/// Event handler for page PreRender event fired by page
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;

			// If we are not displaying the control, or it has not been set up correctly,
			// then ensure that it will not be displayed and do no further processing
			if (!this.Visible || (pageState == null))
			{
				this.Visible = false;
			}
			else
			{
                if ( pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles)
				{
					// Display separate single and return tickets

					DisplayableCostSearchTicket outwardDisplayableTicket = pageState.SelectedOutwardTicket;
					DisplayableCostSearchTicket inwardDisplayableTicket = pageState.SelectedInwardTicket;

					// Set up table layout and labels
					outwardTicketDirectionColumn.Visible = true;
                    outwardTicketRouteDirectionColumn.Visible = true;
					inwardTicketRow.Visible = true;
                    inwardRouteRow.Visible = true;
					outwardTicketLabelDirection.Text = GetResource("FindFareSelectedTicket.SinglesLabelOutward");
					inwardTicketLabelDirection.Text = GetResource("FindFareSelectedTicket.SinglesLabelReturn");

					// Display ticket names
					outwardTicketLabel.Text = FormatTicketNames(outwardDisplayableTicket.CostSearchTickets);
					inwardTicketLabel.Text = FormatTicketNames(inwardDisplayableTicket.CostSearchTickets);

					// Display ticket prices

					// Determine whether discount card has been selected that matches selected mode of travel
					if ( ( (pageState.SelectedTravelDate.TravelDate.TravelMode == TicketTravelMode.Rail) && (pageState.SearchRequest.RailDiscountedCard != String.Empty) )
					  || ( (pageState.SelectedTravelDate.TravelDate.TravelMode == TicketTravelMode.Coach) && (pageState.SearchRequest.CoachDiscountedCard != String.Empty) ) )
					{
						// User specified discount fare, show that price in the label
						outwardTicketPrice.Text = FormatFare(outwardDisplayableTicket, true);
						inwardTicketPrice.Text = FormatFare(inwardDisplayableTicket, true);
					}
					else
					{
						// User specified full fare, show full price in the label
						outwardTicketPrice.Text = FormatFare(outwardDisplayableTicket, false);
						inwardTicketPrice.Text = FormatFare(inwardDisplayableTicket, false);
					}

                    // Display route details
                    labelOutwardRoute.Text = GetRouteText(true, false);
                    labelInwardRoute.Text = GetRouteText(false, false);
                    outwardRouteRow.Visible = (!string.IsNullOrEmpty(labelOutwardRoute.Text));
                    inwardRouteRow.Visible = (!string.IsNullOrEmpty(labelInwardRoute.Text));
				}
				else
				{
					// Display single or return tickets

					DisplayableCostSearchTicket outwardDisplayableTicket = pageState.SelectedSingleOrReturnTicket;

					// Set up table layout (no labels required)
					outwardTicketDirectionColumn.Visible = false;
                    outwardTicketRouteDirectionColumn.Visible = false;
					inwardTicketRow.Visible = false;
                    inwardRouteRow.Visible = false;

					// Display ticket names
					outwardTicketLabel.Text = FormatTicketNames(outwardDisplayableTicket.CostSearchTickets);

					// Display ticket prices

					// Determine whether discount card has been selected that matches selected mode of travel
					if ( ( (pageState.SelectedTravelDate.TravelDate.TravelMode == TicketTravelMode.Rail) && (pageState.SearchRequest.RailDiscountedCard != String.Empty) )
					  || ( (pageState.SelectedTravelDate.TravelDate.TravelMode == TicketTravelMode.Coach) && (pageState.SearchRequest.CoachDiscountedCard != String.Empty) ) )
					{
						// User specified discount fare, show that price in the label
						outwardTicketPrice.Text = FormatFare(outwardDisplayableTicket, true);
					}
					else
					{
						// User specified full fare, show full price in the label
						outwardTicketPrice.Text = FormatFare(outwardDisplayableTicket, false);
					}

                    // Display route text
                    labelOutwardRoute.Text = GetRouteText(true, true);
                    outwardRouteRow.Visible = (!string.IsNullOrEmpty(labelOutwardRoute.Text));
				}
			}
		}

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
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		#endregion Web Form Designer generated code

		#region Public Properties

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

		#endregion Public Properties

		#region Private Methods

		/// <summary>
		/// Build up the names of all the required tickets (per direction of travel) into a single string
		/// </summary>
		/// <param name="tickets">CostSearchTicket array of the tickets required for this direction of travel</param>
		/// <returns>String of formatted ticket names</returns>
		private string FormatTicketNames(CostSearchTicket[] tickets)
		{
			StringBuilder ticketLabel = new StringBuilder();
			for (int i=0; i<tickets.Length; i++)
			{
				ticketLabel.Append(tickets[i].Code);
				ticketLabel.Append("<br />");
			}

			return ticketLabel.ToString();
		}

		/// <summary>
		/// Format the appropriate fare depending upon whether discount fare is required
		/// </summary>
		/// <param name="ticket">DisplayableCostSearchTicket that contains fare data</param>
		/// <param name="discount">Is discount fare required?</param>
		/// <returns>String containing formatted fare</returns>
		private string FormatFare(DisplayableCostSearchTicket ticket, bool discount)
		{
			if ( discount && (ticket.DiscountedFare != String.Empty) )
			{
				return "&pound;" + ticket.DiscountedFare + "<br />";
			}
			else
			{
				return "&pound;" + ticket.Fare + "<br />";
			}
		}

        /// <summary>
        /// Returns the Route restriction info forthe selected ticket
        /// </summary>
        /// <param name="outward"></param>
        /// <returns></returns>
        private string GetRouteText(bool outward, bool useSingleOrReturnTable)
        {
            string routeDetail = string.Empty;

            if (pageState != null)
            {
                if (outward)
                {
                    if (!useSingleOrReturnTable)
                    {
                        #region OutwardTicketTable
                        // Can only display route text if restriction info available
                        if ((pageState.OutwardTicketTable != null) &&
                            (pageState.OutwardTicketTable.RestrictionInfo.Count != 0))
                        {
                            // Get the selected ticket
                            CostSearchTicket costSearchTicket = pageState.SelectedOutwardTicket.CostSearchTickets[0];

                            // Make sure restriction code and text exists
                            if (pageState.OutwardTicketTable.RestrictionInfo.ContainsKey(costSearchTicket.TicketRailFareData.RouteCode))
                            {
                                routeDetail = pageState.OutwardTicketTable.RestrictionInfo[costSearchTicket.TicketRailFareData.RouteCode].ToString();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region SingleOrReturnTicketTable
                        if ((pageState.SingleOrReturnTicketTable != null) &&
                            (pageState.SingleOrReturnTicketTable.RestrictionInfo.Count != 0))
                        {
                            // Get the selected ticket
                            CostSearchTicket costSearchTicket = pageState.SelectedSingleOrReturnTicket.CostSearchTickets[0];

                            // Make sure restriction code and text exists
                            if (pageState.SingleOrReturnTicketTable.RestrictionInfo.ContainsKey(costSearchTicket.TicketRailFareData.RouteCode))
                            {
                                routeDetail = pageState.SingleOrReturnTicketTable.RestrictionInfo[costSearchTicket.TicketRailFareData.RouteCode].ToString();
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    #region InwardTicketTable
                    // Can only display route text if restriction info available
                    if ((pageState.InwardTicketTable != null) 
                        &&(pageState.InwardTicketTable.RestrictionInfo.Count != 0))
                    {
                        // Get the selected ticket
                        CostSearchTicket costSearchTicket = pageState.SelectedInwardTicket.CostSearchTickets[0];

                        // Make sure restriction code and text exists
                        if (pageState.InwardTicketTable.RestrictionInfo.ContainsKey(costSearchTicket.TicketRailFareData.RouteCode))
                        {
                            routeDetail = pageState.InwardTicketTable.RestrictionInfo[costSearchTicket.TicketRailFareData.RouteCode].ToString();
                        }
                    }
                    #endregion
                }

                if (!string.IsNullOrEmpty(routeDetail))
                {
                    // Append "Route: "
                    string route = GetResource("FindFareSelectedTicket.Route").Trim() + "&nbsp;";

                    return route + routeDetail;
                }
            }

            return routeDetail; // will return empty string here
        }

		#endregion Private Methods

	}
}
