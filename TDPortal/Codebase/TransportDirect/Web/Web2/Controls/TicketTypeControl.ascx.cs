// *********************************************** 
// NAME                 : TicketTypeControl.aspx.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 11/08/2005 
// DESCRIPTION			: A custom user control to display Rail Ticket Types
// ************************************************

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TransportDirect.Common.RailTicketType;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
   /// <summary>
    /// Used to display page title on a journey
    /// planner output page. Text is determined based on the current
    /// output page id and the state of the itinerary manager.
	/// </summary>
    public partial class TicketTypeControl : TDUserControl
    {
        public bool TicketFound
        {
            set
            {
                TicketTypeTable.Visible = value;
                ticketNotFound.Visible = !value;
            }
        
       }

        public string Packages
        {
            set
            {
                if (value == "")
                {
                    ticketTypePackagesRow.Visible = false;
                }
                else
                {
                    PackagesDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Conditions
        {
            set
            {
                if (value == "")
                {
                    ticketTypeConditionsRow.Visible = false;
                }
                else
                {
                    ConditionsDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

         public string ChangesToTravelPlans
        {
            set
            {
                if (value == "")
                {
                    ticketTypeChangeToTravelPlansRow.Visible = false;
                }
                else
                {
                    ChangeToTravelPlansDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string InternetOnly
        {
            set
            {
                if (value == "")
                {
                    ticketTypeInternetOnlyRow.Visible = false;
                }
                else
                {
                    InternetOnlyDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Retailing
        {
            set
            {
                if (value == "")
                {
                    ticketTypeRetailingRow.Visible = false;
                }
                else
                {
                    RetailingDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string BreakOfJourney
        {
            set
            {
                if (value == "")
                {
                    ticketTypeBreakofJourneyRow.Visible = false;
                }
                else
                {
                    breakOfJourneyDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Description
        {
            set
            {
                if (value == "")
                {
                    ticketTypeDescriptionRow.Visible = false;
                }
                else
                {
                    descriptionDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Refunds
        {
            set
            {
                if (value == "")
                {
                    ticketTypeRefundsRow.Visible = false;
                }
                else
                {
                    refundsDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Sleepers
        {
            set
            {
                if (value == "")
                {
                    ticketTypeSleeperRow.Visible = false;
                }
                else
                {
                    sleeperDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Discounts
        {
            set
            {
                if (value == "")
                {
                    ticketTypeDiscountRow.Visible = false;
                }
                else
                {
                    discountDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Availability
        {
            set
            {
                if (value == "")
                {
                    ticketTypeAvailabilityRow.Visible = false;
                }
                else
                {
                    availabilityDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string BookingDeadlines
        {
            set
            {
                if (value == "")
                {
                    ticketTypeBookingDeadlineRow.Visible = false;
                }
                else
                {
                    bookingDeadlineDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string TicketTypeName
        {
            set
            {
                if (value == "")
                {
                    this.ticketTypeTitle.Visible = false;
                }
                else
                {
                    this.ticketTypeTitle.Text = Server.HtmlDecode(value);
                }
            }
        }

        public string Validity
        {
            set
            {
                if (value == "")
                {
                    ticketTypeValidityRow.Visible = false;
                }
                else
                {
                    this.validityDesc.Text = Server.HtmlDecode(value);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            descriptionTitle.Text = GetResource("PrintableTicketType.descriptionTitle");

            validityTitle.Text = GetResource("PrintableTicketType.validityTitle");

            sleepersTitle.Text = GetResource("PrintableTicketType.sleepersTitle");

            discountTitle.Text = GetResource("PrintableTicketType.discountTitle");

            availabilityTitle.Text = GetResource("PrintableTicketType.availabilityTitle");

            bookingDeadlinesTitle.Text = GetResource("PrintableTicketType.bookingDeadlinesTitle");

            refundsTitle.Text = GetResource("PrintableTicketType.refundsTitle");

            ticketNotFound.Text = GetResource("PrintableTicketType.ticketNotFound");

            ConditionsTitle.Text = GetResource("PrintableTicketType.conditions");
            breakOfJourneyTitle.Text = GetResource("PrintableTicketType.breakOfJourney");
            InternetOnlyTitle.Text = GetResource("PrintableTicketType.internetOnly");
            ChangeToTravelPlansTitle.Text = GetResource("PrintableTicketType.changesToTravelPlans");
            RetailingTitle.Text = GetResource("PrintableTicketType.retailing");
            PackagesTitle.Text = GetResource("PrintableTicketType.packages");


        }


    }
}