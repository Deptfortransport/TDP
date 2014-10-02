// *********************************************** 
// NAME                 : TimeOut.aspx.cs 
// AUTHOR               : Ben Flenk 
// DATE CREATED         : 01/03/2005 
// DESCRIPTION			: Webform containing the template
// for the Time Out page.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/TimeOut.aspx.cs-arc  $
//
//   Rev 1.4   Jan 16 2009 09:16:36   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 01 2008 17:14:30   mmodi
//Updated for session timeout changes
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 13:27:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:52   mturner
//Initial revision.
//
// DEVFACTORY Feb 28 2008 sbarker
// Sorted links to white labelled home page
//
//   Rev 1.5   Apr 20 2006 11:41:02   rwilby
//Updated to allow the page to specify the redirect url for the TD logo on the header control.
//Resolution for 3851: Error and Timeout pages - new wording and layout
//
//   Rev 1.4   Feb 24 2006 10:17:36   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.3   Feb 10 2006 15:09:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.1   Jan 09 2006 16:38:36   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using System.Threading;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page displayed when the user session has expired
	/// </summary>
	public partial class TimeOut : TDPage
	{			
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

        int redirectSeconds = 0;
        bool addAutoRedirect = false; 
	
		#region constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public TimeOut(): base ()
		{
			pageId = PageId.TimeOut;
		}

		#endregion

		#region private methods

		/// <summary>
		/// Registers the ErrorAndTimeoutLinkHandler javascript file on the page		
		/// </summary>
		private void RegisterJavaScriptFile()
		{
			/// Note that this code is not dependant on javascript being enabled as it is not 
			/// possible to evaluate properly whether javascript is enabled if the page is accessed from a hyperlink
			/// e.g. printer friendly when no postback occurs
			string javaScriptDom = ((TDPage)Page).JavascriptDom;			
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			
			// Output reference to necessary JavaScript file from the ScriptRepository
			Page.ClientScript.RegisterClientScriptBlock(typeof(TimeOut), "ErrorAndTimeoutLinkHandler", scriptRepository.GetScript("ErrorAndTimeoutLinkHandler", javaScriptDom));		
		}

		#endregion

		#region public methods

		/// <summary>
		/// Returns the current language channel
		/// </summary>
		/// <returns>string</returns>
		public string GetChannelLanguage()
		{
			string strChn = TDPage.SessionChannelName.ToString();
			return GetChannelLanguage(strChn);			
		}

		/// <summary>
		/// Returns the hyperlink reference of the html anchor
		/// </summary>
		/// <returns>string</returns>
		public string GetHRefValue()
		{
            StringBuilder href = new StringBuilder();

			href.Append(Request.ApplicationPath);
            //the abandon keyword is used by TDPage to prevent the TimeOut page from re-displaying
			href.Append("?abandon=true");
		
			return href.ToString();
		}

		/// <summary>
		/// Returns the text used to instruct the user to close the browser window
		/// </summary>
		/// <returns>string</returns>
		public string GetCloseBrowserText()
		{
            return GetResource("TimeOut.CloseBrowserText");
        }

		/// <summary>
		/// Returns the anchor text 
		/// </summary>
		/// <returns>string</returns>
		public string GetAnchorText()
		{
			return GetResource("TimeOut.GetAnchorText");
		}

		/// <summary>
		/// Returns the text used to prefix the anchor text
		/// </summary>
		/// <returns>string</returns>
		public string GetInstructionPrefix()
		{
            if (addAutoRedirect)
            {
                return string.Format(GetResource("TimeOut.GetInstructionPrefix.Redirect"), redirectSeconds);
            }
            else
            {
                return GetResource("TimeOut.GetInstructionPrefix");
            }
		}

		/// <summary>
		/// Returns the text used to suffix the anchor text
		/// </summary>
		/// <returns>string</returns>
		public string GetInstructionSuffix()
		{
            if (addAutoRedirect)
            {
                return GetResource("TimeOut.GetInstructionSuffix.Redirect");
            }
            else
            {
                return GetResource("TimeOut.GetInstructionSuffix");
            }
		}

		#endregion

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

		#region EventHandlers

		/// <summary>
		/// Page load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//IR3851 - Enhancement IR: To allow the Error and Timeout pages 
			//to specify the redirect url for the TD logo on the header control.
			headerControl.UseTDPLogoURL = true;
			headerControl.TDLogoURL = GetHRefValue();

            // Don't show the Navigation tabs
            headerControl.ShowNavigation = false;

            #region Add the autoredirect for this page
            try
            {
                // Set global flag so that we can display the correct message on page
                addAutoRedirect = bool.Parse(Properties.Current["TimeoutPage.AutoRedirect"]);
                if ((addAutoRedirect) && (this.IsJavascriptEnabled))
                {
                    redirectSeconds = int.Parse(Properties.Current["TimeoutPage.AutoRedirect.Seconds"]);
                    string redirectUrl = Properties.Current["TimeoutPage.AutoRedirect.URL"];

                    headElementControl.AddAutoRedirect(redirectSeconds, redirectUrl);
                }
            }
            catch
            {
                // Unable to setup autoredirect, so default is to not add
                addAutoRedirect = false;
            }
            #endregion

            RegisterJavaScriptFile();

			SetPanelVisibility();
														
			//invalidate the session
			Session.Abandon();
		}

		/// <summary>
		/// Used to hide/show the appropriate user instruction for dependant on whether the 
		/// time out occurred when the UserSurvey page was accessed
		/// </summary>
		private void SetPanelVisibility()
		{
			
			if (Request.QueryString["UserSurvey"] != null)
			{
				panelMessage.Visible=false;
				panelUserSurvey.Visible = true;
			}
			else
			{
				panelMessage.Visible=true;
				panelUserSurvey.Visible = false;
			}
		}
		#endregion
	}
}
