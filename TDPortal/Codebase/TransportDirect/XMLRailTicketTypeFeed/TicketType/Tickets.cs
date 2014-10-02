// *********************************************** 
// NAME                 : Tickets.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Class containing a list of Ticket objects
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.Common.RailTicketType
{
    /// <summary>
    /// Tickets business objects class
    /// </summary>
    public class Tickets
    {
        private List<Ticket> allTickets;

        public Tickets()
        {
            allTickets = new List<Ticket>();
        }

        /// <summary>
        /// List of all ticket types
        /// </summary>
        public List<Ticket> AllTickets
        {
            get
            {
                return allTickets;
            }
        }

        /// <summary>
        /// Saves ticket to Database
        /// </summary>
        public void SaveToDB()
        {
            foreach (Ticket ticket in allTickets)
            {
                ticket.SaveTicket();
            }
        }
    }
}
