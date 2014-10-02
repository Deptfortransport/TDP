// *********************************************** 
// NAME                 : PrintableTicketType.aspx.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 20/01/2008
// DESCRIPTION			: Printable Ticket Type page
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

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
    public partial class PrintableTicketType : TDPrintablePage, INewWindowPage
    {
        #region Private members

        private ITDSessionManager tdSessionManager;

        #endregion

        #region Constructor, PageLoad

        /// <summary>
		/// Constructor.
		/// </summary>
		public PrintableTicketType() : base()
		{
			pageId = PageId.PrintableTicketType;
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            tdSessionManager = TDSessionManager.Current;

            SetupControls();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises/displays controls on the page based on the journey results
        /// </summary>
        private void SetupControls()
        {
            labelPageTitle.Text = GetResource("PrintableTicketType.labelPageTitle");

            //Populate the Ticket on the screen
            TicketTypeAdapter ticketAdapter = new TicketTypeAdapter(tdSessionManager);
            ticketAdapter.PopulateTicketTypeTable(TicketTypeControl1, Request.QueryString["TicketTypeCode"].ToString());
        }

        #endregion
    }
}
