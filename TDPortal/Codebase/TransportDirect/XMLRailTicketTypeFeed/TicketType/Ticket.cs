// *********************************************** 
// NAME                 : Ticket.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Representation of the Rail Ticket Type
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.RailTicketType
{
    public class Ticket
    {
        private string ticketTypeCode = String.Empty;
        private string ticketTypeName = String.Empty;
        private string description = String.Empty;
        private ApplicableTOCs applicableTocs = null;
        private string validity = String.Empty;
        private string sleepers = String.Empty;
        private string fareCategory = String.Empty;
        private bool groupSave;
        private string discounts = String.Empty;
        private string availability = String.Empty;
        private string retailing = String.Empty;
        private string bookingDeadlines = String.Empty;
        private string refunds = String.Empty;
        private ValidityCodes validityCodes = null;
        private string breaksOfJourney = String.Empty;
        private string changesToTravelPlans = String.Empty;
        private string packages = String.Empty;
        private string conditions = String.Empty;
        private string easements = String.Empty;
        private string internetOnly = String.Empty;

        /// <summary>
        /// Ticket Type Constructor
        /// </summary>
        /// <param name="TicketTypeCode"></param>
        /// <param name="TicketTypeName"></param>
        /// <param name="Description"></param>
        /// <param name="ApplicableTocs"></param>
        /// <param name="ValidityCodes"></param>
        /// <param name="Validity"></param>
        /// <param name="Sleepers"></param>
        /// <param name="FareCategory"></param>
        /// <param name="GroupSave"></param>
        /// <param name="Discounts"></param>
        /// <param name="Availability"></param>
        /// <param name="Retailing"></param>
        /// <param name="BookingDeadlines"></param>
        /// <param name="Refunds"></param>
        /// <param name="BreaksOfJourney"></param>
        /// <param name="ChangesToTravelPlans"></param>
        /// <param name="Packages"></param>
        /// <param name="Conditions"></param>
        /// <param name="Easements"></param>
        /// <param name="InternetOnly"></param>
        public Ticket(string TicketTypeCode, string TicketTypeName, string Description, 
            ApplicableTOCs ApplicableTocs, ValidityCodes ValidityCodes, 
            string Validity, string Sleepers, string FareCategory, 
            bool GroupSave, string Discounts, string Availability, 
            string Retailing, string BookingDeadlines, string Refunds, 
            string BreaksOfJourney, string ChangesToTravelPlans, 
            string Packages, string Conditions, string Easements, string InternetOnly)
        {
            ticketTypeCode = TicketTypeCode;
            ticketTypeName = TicketTypeName;
            description = Description;
            applicableTocs = ApplicableTocs;
            validityCodes = ValidityCodes;
            validity = Validity;
            sleepers = Sleepers;
            fareCategory = FareCategory;
            groupSave = GroupSave;
            discounts = Discounts;
            availability = Availability;
            retailing = Retailing;
            bookingDeadlines = BookingDeadlines;
            refunds = Refunds;
            breaksOfJourney = BreaksOfJourney;
            changesToTravelPlans = ChangesToTravelPlans;
            packages = Packages;
            conditions = Conditions;
            easements = Easements;
            internetOnly = InternetOnly;
        }

        public string TicketTypeCode
        {
            get
            {
                return ticketTypeCode;
            }
        }

        public string TicketTypeName
        {
            get
            {
                return ticketTypeName;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public ApplicableTOCs ApplicableTocs
        {
            get
            {
                return applicableTocs;
            }
        }

        public string Validity
        {
            get
            {
                return validity;
            }
        }

        public string Sleepers
        {
            get
            {
                return sleepers;
            }
        }

        public string FareCategory
        {
            get
            {
                return fareCategory;
            }
        }

        public bool GroupSave
        {
            get
            {
                return groupSave;
            }
        }

        public string Discounts
        {
            get
            {
                return discounts;
            }
        }

        public string Availability
        {
            get
            {
                return availability;
            }
        }

        public string Retailing
        {
            get
            {
                return retailing;
            }
            set
            {
            }
        }

        public string BookingDeadlines
        {
            get
            {
                return bookingDeadlines;
            }
        }

        public string Refunds
        {
            get
            {
                return refunds;
            }
        }

        public ValidityCodes ValidityCodes
        {
            get
            {
                return validityCodes;
            }
        }

        public string BreaksOfJourney
        {
            get
            {
                return breaksOfJourney;
            }
        }

        public string ChangesToTravelPlans
        {
            get
            {
                return changesToTravelPlans;
            }
        }

        public string Packages
        {
            get
            {
                return packages;
            }
        }

        public string Conditions
        {
            get
            {
                return conditions;
            }
        }

        public string Easements
        {
            get
            {
                return easements;
            }
        }

        public string InternetOnly
        {
            get
            {
                return internetOnly;
            }
        }

        /// <summary>
        /// Saves the Ticket to the Database
        /// </summary>
        public void SaveTicket()
        {
            DataAccess.Instance.SaveTicketToDB(this);
        }
    }
}
