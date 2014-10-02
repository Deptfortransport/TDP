// *********************************************** 
// NAME                 : TicketTypeAdapter.cs 
// AUTHOR               : DEV FACTORY
// DATE CREATED         : 04/07/2008
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/TicketTypeAdapter.cs-arc  $
//
//   Rev 1.1   Jul 04 2008 11:15:42   mturner
//Put PVCS headers in code and removed duplicate using statement


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.Common.RailTicketType;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class TicketTypeAdapter : TDWebAdapter
    {
        private ITDSessionManager tdSessionManager;
        private TDItineraryManager itineraryManager;

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tdSessionManager">SessionManager being used by parent page</param>
        public TicketTypeAdapter(ITDSessionManager tdSessionManager)
		{
            this.tdSessionManager = tdSessionManager;
			this.itineraryManager = tdSessionManager.ItineraryManager;
		}

        /// <summary>
        /// Populates the TicketTypeControl with the corresponding Ticket
        /// </summary>
                /// <param name="ticketTypeControl">TicketTypeControl to populate</param>
        /// <param name="ticketTypeCode">Type of ticket</param>
        public void PopulateTicketTypeTable(TicketTypeControl ticketTypeControl, string ticketTypeCode)
        {
            try
            {
                if(DataAccess.Instance.DoesTicketExist(ticketTypeCode))
                {
                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                    "Loading Ticket from Database for TicketTypeCode '" + ticketTypeCode + "'"));
                Ticket myTicket = DataAccess.Instance.LoadTicketFromDB(ticketTypeCode);

                ticketTypeControl.TicketFound = true;
                ticketTypeControl.Refunds = myTicket.Refunds;
                ticketTypeControl.Description = myTicket.Description;
                ticketTypeControl.Discounts = myTicket.Discounts;
                ticketTypeControl.Availability = myTicket.Availability;
                ticketTypeControl.BookingDeadlines = myTicket.BookingDeadlines;
                ticketTypeControl.Sleepers = myTicket.Sleepers;
                ticketTypeControl.Validity = myTicket.Validity;
                ticketTypeControl.TicketTypeName = myTicket.TicketTypeName;

                ticketTypeControl.Conditions = myTicket.Conditions;
                ticketTypeControl.BreakOfJourney = myTicket.BreaksOfJourney;
                ticketTypeControl.InternetOnly = myTicket.InternetOnly;
                ticketTypeControl.ChangesToTravelPlans = myTicket.ChangesToTravelPlans;
                ticketTypeControl.Retailing = myTicket.Retailing;
                ticketTypeControl.Packages = myTicket.Packages;

                }
                else
                {
                    ticketTypeControl.TicketFound = false;

                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                    "Failed to load Ticket from Database for TicketTypeCode '" + ticketTypeCode + "' - ticket type does not exist"));
                }
                   
            }
            catch (Exception tdex)
            {

                //log error and throw exception
                string message = "Error TicketTypeAdapter.PopulateTicketTypeTable method : " + tdex.Message;
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));

                throw new TDException(message, tdex, false, TDExceptionIdentifier.XMLRTTPopulateTicketTypeTable);
            }
        }
    }
}
