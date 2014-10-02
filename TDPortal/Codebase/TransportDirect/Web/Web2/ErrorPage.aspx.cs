// *********************************************** 
// NAME                 : ErrorPage.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 22/10/2003 
// DESCRIPTION			: A default error page to display to users instead of displaying
// the standard .net error page to users.  This page needs to be enabled in the Web.config file
// before it can be used.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/ErrorPage.aspx.cs-arc  $
//
//   Rev 1.4   Apr 20 2010 11:35:10   apatel
//Updated for Cycle white label custom home page link
//Resolution for 5488: Cycle Planner white label changes
//
//   Rev 1.3   Jan 16 2009 08:46:02   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:23:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:10   mturner
//Initial revision.
//
// DEVFACTORY Feb 28 2008 sbarker
// Corrected link for white labelling
//
//   Rev 1.12   Jan 24 2007 12:39:16   mmodi
//Changed Contact us url to point to new Feedback page
//Resolution for 4343: Contact Us: Portal error page takes user to old Feedback page
//
//   Rev 1.11   Apr 20 2006 11:38:24   rwilby
//1)Updated to allow the page to specify the redirect url for the TD logo on the header control. 2) Add functionality to link to feedback page
//Resolution for 3851: Error and Timeout pages - new wording and layout
//
//   Rev 1.10   Feb 23 2006 17:48:26   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.9   Feb 10 2006 15:08:50   build
//Automatically merged from branch for stream3180
//
//   Rev 1.8.1.0   Dec 16 2005 18:14:34   AViitanen
//Changed to use new HeaderControl and HeadElementControl.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.8   May 26 2005 11:20:04   rgeraghty
//Panels added to hide hyperlink if error occurs in UserSurvey page
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.7   May 20 2005 11:56:34   rgeraghty
//Changes made to display/handling of page
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.6   Apr 19 2005 16:29:24   jgeorge
//Added code to abandon session, and link to home page
//Resolution for 2211: Session should be invalidated after an exception has occurred
//
//   Rev 1.5   Mar 17 2004 19:17:38   RPhilpott
//Move error logging code to global.asax
//Resolution for 664: Error logging
//
//   Rev 1.4   Dec 04 2003 15:32:22   asinclair
//Corrected spelling mistake
//
//   Rev 1.3   Dec 04 2003 11:02:42   asinclair
//Updated to included blue header and  changed font of text.
//
//   Rev 1.2   Nov 17 2003 15:43:14   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.1   Nov 14 2003 14:01:14   asinclair
//Updated to allow it to write to the log when an error occurs
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
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Default error page 
	/// </summary>
	public partial class Error : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		/// <summary>
		/// Constructor. Sets the PageId.
		/// </summary>
		public Error() : base()
		{
			pageId = PageId.Error;
		}

		#region Event handlers

		/// <summary>
		/// Handler for the load event. Sets up static text and abandons the session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		
			//IR3851 - Enhancement IR: To allow the Error and Timeout pages 
			//to specify the redirect url for the TD logo on the header control.
			headerControl.UseTDPLogoURL = true;
			headerControl.TDLogoURL = GetHRefValue();

            // Don't show the Navigation tabs
            headerControl.ShowNavigation = false;

			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				RegisterJavaScriptFile();

				SetPanelVisibility();
				
				// Clear the session of all data, as it is potentially in an inconsistent state
				Session.Abandon();
			}
		}

		#endregion

		#region private methods

		/// <summary>
		/// Used to hide/show the appropriate user instruction for dependant on whether the 
		/// error occurred when the UserSurvey page was accessed
		/// </summary>
		private void SetPanelVisibility()
		{
			//IR2493 if an error has occurred in the user Survey page, need to prevent the hyperlink
			//being displayed to avoid complications
			if ((Request.Url.Query.Length !=0) && (Request.Url.Query.IndexOf("UserSurvey.aspx")>0))
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

		/// <summary>
		/// Registers the ErrorAndTimeoutLinkHandler javascript file on the page		
		/// </summary>
		private void RegisterJavaScriptFile()
		{
			/// Note that this code is not dependant on javascript being enabled as it is not 
			/// possible to evaluate whether javascript is enabled if the page is accessed from a hyperlink
			/// e.g. printer friendly when no postback occurs
			string javaScriptDom = ((TDPage)Page).JavascriptDom;			
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			
			// Output reference to necessary JavaScript file from the ScriptRepository
			Page.ClientScript.RegisterClientScriptBlock(typeof(Error),"ErrorAndTimeoutLinkHandler", scriptRepository.GetScript("ErrorAndTimeoutLinkHandler", javaScriptDom));		
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

            try
            {
                // Check if the custom home page requred
                bool customHomePage = false;
                if (!bool.TryParse(Properties.Current["ErrorPage.IsCustomHomePage"], out customHomePage))
                {
                    customHomePage = false;
                }
                // if custom home page required get custom home page url from the properties and 
                // set the home page link in error message.
                if (customHomePage)
                {
                    string customHomePageUrl = Properties.Current["ErrorPage.CustomHomePage.Url"];

                    if (!string.IsNullOrEmpty(customHomePageUrl))
                    {
                        href.Append(customHomePageUrl);
                    }
                }

            }
            catch
            {
                // Error raised due to possible Properties not available
                // In this case let user to navigate to default application root
            }
            

            //the abandon keyword is used by TDPage to prevent the TimeOut page from re-displaying
            href.Append("?abandon=true");

            return href.ToString();
		}

		/// <summary>
		/// Returns the hyperlink reference for feedback page
		/// </summary>
		/// <returns>string</returns>
		public string GetHRefValueForFeedback()
		{		
			//IR3851 - Enhancement IR: To add a link to the feedback page from the error page.
			//The link is hardcoded as the error page cannot depend on a database or resource file.

			//the abandon keyword is used by TDPage to prevent the TimeOut page from re-displaying
			//note that the default (english) channel is used as for the error page, don't know the culture			
            StringBuilder href = new StringBuilder();

            href.Append(Request.ApplicationPath);
            //the abandon keyword is used by TDPage to prevent the TimeOut page from re-displaying
            href.Append("/ContactUs/FeedbackPage.aspx?abandon=true");

            return href.ToString();
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
	}
}
