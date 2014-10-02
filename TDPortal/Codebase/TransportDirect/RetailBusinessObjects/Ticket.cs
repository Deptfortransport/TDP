//********************************************************************************
//NAME         : Ticket.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Ticket.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:26   mturner
//Initial revision.
//
//   Rev 1.2   Oct 17 2003 10:12:02   CHosegood
//Added class and Type is now a JourneyType
//
//   Rev 1.1   Oct 14 2003 12:28:06   CHosegood
//No change.
//
//   Rev 1.0   Oct 14 2003 11:25:28   CHosegood
//Initial Revision

using System;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Details of a ticket.
	/// </summary>
	public class Ticket
	{
        private string code;
        private string name;
        private TicketClass ticketClass;
        private JourneyType type;

        /// <summary>
        /// The 3 digit code for the ticket
        /// </summary>
        public string Code 
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// The name of the ticket
        /// </summary>
        public string Name 
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// The type of ticket
        /// </summary>
        public JourneyType Type 
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public TicketClass TicketClass 
        {
            get { return this.ticketClass; }
            set { this.ticketClass = value; }
        }

        /// <summary>
        /// Details of a ticket
        /// </summary>
        /// <param name="name">The name of the ticket</param>
        /// <param name="type"></param>
		public Ticket( string code)
		{
            this.code = code;
		}
	}
}
