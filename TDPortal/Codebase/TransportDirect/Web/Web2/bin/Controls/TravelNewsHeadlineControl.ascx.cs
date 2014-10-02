// *********************************************** 
// NAME                 : TravelNewsHeadlineControl.ascx
// AUTHOR               : Jonathan George
// DATE CREATED         : 24/05/2004
// DESCRIPTION  : Displays travel news headlines
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelNewsHeadlineControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Mar 23 2011 11:05:18   PScott
//IR 5683 - 10.16 fix.  Relative paths resolved so can navigate from travel headline from anywhere.
//
//Resolution for 5683: 10.16 Fixes from initial testing on SITest
//
//   Rev 1.2   Mar 31 2008 13:23:28   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Mar 23 2008 13:50:00 mmodi
//  Removed all references to AjaxPro
//  Corrected incident url
//  
//  Rev Devfactory Feb 14 2008 17:00:00 mmodi
//Added screen reader and amended javascript hide method call to not pass a parameter
//
//  Rev Devfactory Jan 11 2008 09:15:15 apatel
//  Implemented AJAX hover functionality for travel news headline control
//  Added control prerender method to register new travelnewsheadlinepopup javascript
//
//   Rev 1.0   Nov 08 2007 13:18:28   mturner
//Initial revision.
//
//   Rev 1.8   Mar 28 2006 11:09:06   build
//Automatically merged from branch for stream0024
//
//   Rev 1.7.1.2   Mar 14 2006 16:01:38   AViitanen
//Updated following code review. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.7.1.1   Mar 10 2006 16:34:50   AViitanen
//Fxcop change. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.7.1.0   Mar 03 2006 16:36:22   AViitanen
//Updated to enable pt incidents to be displayed as hyperlinks also. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.7   Feb 24 2006 14:46:08   RWilby
//Fix for merge stream3129.
//
//   Rev 1.6   Feb 23 2006 16:14:18   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.5   Feb 10 2006 15:04:44   build
//Automatically merged from branch for stream3180
//
//   Rev 1.4.1.0   Nov 25 2005 17:49:38   AViitanen
//Updated GetHeadlineText for showing car incidents as hyperlinks.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.4   Nov 17 2005 13:50:16   kjosling
//Automatically merged from branch for stream2880
//
//   Rev 1.3.1.1   Nov 01 2005 20:31:26   pcross
//Using <strong> tag for bold (severe headline) text instead of css class. (More accessible)
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.3.1.0   Oct 21 2005 15:12:22   pcross
//Alternating items highlighted differently
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.3   Dec 16 2004 15:23:38   passuied
//refactoring the TravelNews component and changes to the clients
//
//   Rev 1.2   Sep 16 2004 15:59:10   jmorrissey
//part of fix for IR1587 - tarvel news dummy file not working
//
//   Rev 1.1   Sep 06 2004 21:09:24   JHaydock
//Major update to travel news
//
//   Rev 1.0   May 26 2004 10:18:30   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using TransportDirect.Common.ResourceManager;
    using System.Collections;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using TransportDirect.Common.Logging;
    using TransportDirect.Common.PropertyService.Properties;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.TravelNews;
    using Logger = System.Diagnostics.Trace;

    using TransportDirect.Common;
    using TransportDirect.UserPortal.ScreenFlow;
    using TransportDirect.UserPortal.TravelNewsInterface;
    using TransportDirect.UserPortal.Web.Adapters;


    /// <summary>
    ///		Summary description for TravelNewsHeadlineControl.
    /// </summary>
    public partial class TravelNewsHeadlineControl : TDUserControl
    {
        protected System.Web.UI.WebControls.Label labelNote;
        private TravelNewsHandler travelNewsHandler;


        protected void Page_Load(object sender, System.EventArgs e)
        {

            travelNewsHandler = (TravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];

            if (travelNewsHandler.IsTravelNewsAvaliable)
            {
                panelNoHeadlines.Visible = false;
                panelHeadlines.Visible = true;

                rptTravelNews.DataSource = travelNewsHandler.GetHeadlines();
                rptTravelNews.DataBind();

                labelHeadlineTextScreenReader.Text = GetResource("TravelNewsHeadlineControl.labelHeadlineTextScreenReader.Text");
            }
            else
            {
                panelNoHeadlines.Visible = true;
                panelHeadlines.Visible = false;

                //part of fix for IR1587 - tarvel news dummy file not working
                labelNoHeadlines.Text = travelNewsHandler.TravelNewsUnavailableText;
            }
        }

        /// <summary>
        /// Implements the javascript registration to page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }



        /// <summary>
        /// Gets the current headline text
        /// </summary>
        /// <param name="containerDataItem"></param>
        /// <returns>headline text string</returns>
        /// <remarks>Examines severity and adds bold tag as necessary. Note: doesn't use style for accessibility reasons</remarks>
        public string GetHeadlineText(object containerDataItem)
        {
            string newsItem;
            HeadlineItem item = (HeadlineItem)containerDataItem;

            string baseChannel = String.Empty;
            string url = String.Empty;
            if (TDPage.SessionChannelName != null)
                baseChannel = TDPage.getBaseChannelURL(TDPage.SessionChannelName);
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.TravelNews);
            url = baseChannel + pageTransferDetails.PageUrl;
            StringBuilder newsString = new StringBuilder(100);
            newsString.Append("<a href=\"");
            newsString.Append(ResolveClientUrl(url));
            newsString.Append("?uid=");
            newsString.Append(item.Uid);
            newsString.Append("\" ");

            newsString.Append(">");
            newsString.Append(item.HeadlineText);
            newsString.Append("</a>");
            newsItem = newsString.ToString();

            switch (item.SeverityLevel)
            {
                case (SeverityLevel.Critical):
                case (SeverityLevel.VerySevere):
                    return "<strong>" + newsItem + "</strong>";
                default:
                    return newsItem;
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}