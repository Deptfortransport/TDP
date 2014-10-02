// *********************************************** 
// NAME                 : WaitPage.aspx.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 22/09/2003
// DESCRIPTION			: The WaitPage
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/WaitPage.aspx.cs-arc  $
//
//   Rev 1.8   Oct 10 2012 14:29:46   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.7   Apr 08 2010 10:03:22   apatel
//Cycle planner white label changes - Comments
//Resolution for 5488: Cycle Planner white label changes
//
//   Rev 1.6   Mar 30 2010 09:13:54   apatel
//Cycle planner white label changes
//Resolution for 5488: Cycle Planner white label changes
//
//   Rev 1.5   Oct 24 2008 14:53:52   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.4   Apr 15 2008 12:08:22   mmodi
//Renamed label to remove warning
//
//   Rev 1.3   Apr 02 2008 11:20:28   mmodi
//Page landing from Word 2003 fix following Del 10
//
//   Rev DevFactory   Mar 31 2008 15:00:00  mmodi
//Removed old CmsHttpContext URL when handling page landing from Word 2003
//
//   Rev 1.2   Mar 31 2008 13:25:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:32:04   mturner
//Initial revision.
//
//   Rev 1.42   Mar 29 2007 15:34:10   jfrank
//Fix for Maps and Modify Journey when page landing from word 2003.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.41   Feb 28 2007 14:58:30   jfrank
//CCN0366 - Enhancement to enable Page Landing from Word 2003 and to ignore session timeouts for Page Landing due to future usage by National Trust.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.40   May 03 2006 16:45:54   mtillett
//Add title for Wait Page
//Resolution for 4068: WAI: Change wait page title
//
//   Rev 1.39   Apr 05 2006 15:43:02   build
//Automatically merged from branch for stream0030
//
//   Rev 1.38.1.0   Mar 29 2006 11:32:08   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.38   Feb 24 2006 10:17:26   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.37   Feb 10 2006 15:09:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.36.1.0   Nov 29 2005 18:47:26   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.36   Nov 09 2005 12:31:30   build
//Automatically merged from branch for stream2818
//
//   Rev 1.35.1.3   Nov 08 2005 17:24:52   RPhilpott
//Use property instead of hard-code "manual refresh hint".
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.35.1.2   Nov 02 2005 11:25:04   RPhilpott
//Clear deferred data from session before reading AsyncStatus.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.35.1.1   Oct 21 2005 18:25:36   jgeorge
//Amended to get refresh interval from AsyncCallState object.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.35.1.0   Oct 14 2005 15:29:52   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.35   Apr 04 2005 09:07:50   jmorrissey
//Updated CheckCostBasedSearchWaitStateData to check for null TDJourneyResult in the session before trying to update it.
//
//   Rev 1.34   Mar 15 2005 08:44:32   tmollart
//Made changes to use the CostSearchWaitControlData and CostSearchWaitStateData properies on TDSessionManager as opposed to in the FindCostBasedPageState object.
//
//   Rev 1.33   Mar 08 2005 14:49:00   rscott
//DEL 7 - updated method name to prevent confusion
//
//   Rev 1.32   Mar 08 2005 10:52:00   rscott
//DEL7 - page load event restructured to handle CostBasedSearchWaitStateData.
//
//   Rev 1.31   Jan 26 2005 15:53:20   PNorell
//Support for partitioning the session information.
//
//   Rev 1.30   Nov 08 2004 17:42:20   PAssuied
//Replaced labelMessage with place holder
//
//   Rev 1.29   Oct 15 2004 12:39:12   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.28   Aug 10 2004 15:45:04   JHaydock
//Update of display of correct header control for help pages.
//
//   Rev 1.27   Jun 25 2004 11:55:58   jgeorge
//Added code to switch between Journey Planner and Find Flight header controls depending on JourneyParameters type.
//
//   Rev 1.26   Mar 17 2004 14:28:42   CHosegood
//Uses jpstd.css instead of jp.css & fixed compile warning by removing unused private members
//
//   Rev 1.25   Mar 16 2004 16:31:28   asinclair
//Commented out the Logo Image Rotation for Del 5.2 
//
//   Rev 1.24   Mar 12 2004 17:44:48   asinclair
//Updated for Del 5.2
//
//   Rev 1.23   Feb 26 2004 16:15:52   asinclair
//Added AirLabel, latest Del 5.2 Update
//
//   Rev 1.22   Feb 25 2004 12:45:26   RPhilpott
//If all data provider logos have been used, begin loop again to avoid array out of bounds.
//
//   Rev 1.21   Feb 16 2004 13:46:04   asinclair
//Updated for Del 5.2 - Data Provider Logos displayed
//
//   Rev 1.20   Dec 30 2003 10:41:04   passuied
//hard coded the number of seconds to wait to 30 seconds (only displayed as such)
//Resolution for 568: Message in journey request refresh screen incorrect
//
//   Rev 1.19   Nov 24 2003 15:05:28   kcheung
//Added functionality for wait page to display an error message if it timeouts.
//
//   Rev 1.18   Nov 17 2003 18:00:06   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.17   Nov 07 2003 11:29:16   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.16   Nov 05 2003 13:46:56   kcheung
//Added summary headers.
//
//   Rev 1.15   Nov 04 2003 13:54:06   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.14   Oct 22 2003 12:20:18   RPhilpott
//Improve CJP error handling
//
//   Rev 1.13   Oct 21 2003 15:00:22   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.12   Oct 16 2003 12:19:20   kcheung
//Fixed so that alt text for the image loads automatically from langstrings
//
//   Rev 1.11   Oct 10 2003 11:33:02   PNorell
//Corrected the meta refresh.
//
//   Rev 1.10   Oct 09 2003 09:31:52   PNorell
//Added alt text to wait page.
//
//   Rev 1.9   Oct 09 2003 09:21:38   PNorell
//Updated outlook according closer to the wireframes.
//Incorporated time counter properly.
//
//   Rev 1.8   Oct 08 2003 19:47:54   PNorell
//Updated for the once-logging for pages that only wants to register once even though multiple tries has been accessed.
//
//   Rev 1.7   Oct 08 2003 15:27:06   PNorell
//Added 'undvik' keyword to make it able to be disregarded from SLA point of view.
//
//   Rev 1.6   Oct 03 2003 14:48:48   PNorell
//Added headers
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;

using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using CmsMockObject.Objects;

using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.Web.Templates
{

	/// <summary>
	/// Wait page - displayed when waiting for results.
	/// </summary>
	public partial class WaitPage : TDPage
	{

		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;


		bool switchingSession = false;

		/// <summary>
		/// Constructor - sets the Page Id.
		/// </summary>
		public WaitPage()
		{
			pageId = PageId.WaitPage;
		}

		/// <summary>
		/// Page Load Method. Sets up the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Used for JPLanding from Word 2003
			// If the session id in the url doesn't match the current session id.
			if (this.Request.QueryString["SID"] != null && TDSessionManager.Current.Session.SessionID != this.Request.QueryString["SID"])
			{
				//Call method to copy the data from the url's session to the current session.
				TDSessionSerializer tdSS= new TDSessionSerializer();
				tdSS.UpdateToDeferredSession(TDSessionManager.Current.Session.SessionID, this.Request.QueryString["SID"]);

				switchingSession = true;
			}
            litTitleTipofDay.Text = GetResource("WaitPage.TitleTipOfTheDay");
            litTipofDay.Text = TipOfTheDayProvider.Instance.GetTip();

			PageTitle = GetResource("WaitPage.PageTitle");

            // Set the Powered by logo mode
            PoweredByControl.PoweredByControlMode = TransportDirect.UserPortal.Web.Controls.PoweredByControl.PoweredByMode.LogoOnly;

			string strChn = TDPage.SessionChannelName.ToString();
			string language = GetChannelLanguage(strChn);
			ITDSessionManager sessionManager = TDSessionManager.Current;
		
			if( this.Request.Params["undvik"] != null && this.Request.Params["undvik"].Length != 0)
			{
				// No Page Logging
				IsReentrant = true;
			}
			
			CheckForTimeout();

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;

		}

		/// <summary>
		/// Checks the AsyncCallState object and updates it if the call has timed out
		/// </summary>
		private void CheckForTimeout()
		{
			TDSessionManager.Current.ClearDeferredData();
			
			AsyncCallState acs = TDSessionManager.Current.AsyncCallState;
			if( acs != null && acs.Status == AsyncCallStatus.InProgress && acs.IsTimeOutExpired )
			{
				acs.Status = AsyncCallStatus.TimedOut;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, "Wait page has timed out"));
			}
		}

		/// <summary>
		/// Override for the OnPreRender Method.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			// Call base.OnPreRender first as this might cause a Response.Redirect and there's no point
			// doing the rest of the processing below if that happens.

			base.OnPreRender(e);

			// Set the animated image alternate text and url
			AnimatedImage.AlternateText = GetResource("WaitPage.AnimatedImage.AlternateText");
			AnimatedImage.ImageUrl = GetResource("langStrings", "WaitPage.AnimatedImage.ImageUrl");

			// Used for JPLanding from Word 2003
			// If the session id in the url doesn't match the current session id.
			if (switchingSession)
			{

				refresh.Text = "<meta http-equiv=\"refresh\" content=\"1;URL="
                    + "?cacheparam=" + this.Request.QueryString["cacheparam"]
					+ "&SID=" + this.Request.QueryString["SID"] 
                    + "&repeatingloop=" + this.Request.QueryString["repeatingloop"] + "\" />";

				Response.Cookies["ASP.NET_SessionId"].Value = this.Request.QueryString["SID"];
			}
			else
			{
				AsyncCallState acs = TDSessionManager.Current.AsyncCallState;

				// Initialise the header message for the page
                headerLabel.Text = GetResource(acs.WaitPageMessageResourceFile, acs.WaitPageMessageResourceId);

				// Log if not defined to ensure new code sets this property
                if (string.IsNullOrEmpty(headerLabel.Text))
				{
                    Logger.Write(new OperationalEvent(
                        TDEventCategory.Infrastructure, TDTraceLevel.Error,
                        string.Format("Resource string not found for wait page header [{0}][{1}]", 
                            acs.WaitPageMessageResourceFile, acs.WaitPageMessageResourceId)));
				}

				// Put user code to initialize the page here
				refresh.Text = "<meta http-equiv=\"refresh\" content=\"" + acs.WaitPageRefreshInterval.ToString() + ";URL=WaitPage.aspx?undvik=1\" />";
			}
		
			string manualRefreshHint = Properties.Current["WaitPage.ManualRefreshHint.Seconds"];

			if	(manualRefreshHint == null || manualRefreshHint.Length == 0)
			{
				manualRefreshHint = "30";
			}

			InformationLabel.Text = string.Format(InformationLabel.Text, manualRefreshHint);

            // logic to make tipofday div show/hide based on the property value
            bool tipOfDayDivVisible = true;
            if (!bool.TryParse(Properties.Current["WaitPage.tipOfDay.Visible"], out tipOfDayDivVisible))
            {
                tipOfDayDivVisible = true;
            }
            tipofday.Visible = tipOfDayDivVisible;
                        
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

        
	}
}
