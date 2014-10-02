// *********************************************** 
// NAME                 : SorryPage.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 24/05/2007 
// DESCRIPTION			: A sorry page to display to users instead of displaying
// the Portal Error page to users. Used in scenarios where an error is not thrown by the Portal but we want to
// show a Sorry "something went wrong" page, e.g. in a landing page scenario for CRS Codes not being available
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/SorryPage.aspx.cs-arc  $
//
//   Rev 1.3   Jan 16 2009 09:28:52   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:26:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:24   mturner
//Initial revision.
//
//   Rev 1.0   May 24 2007 15:07:26   mmodi
//Initial revision.
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//

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

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using System.Text;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for SorryPage.
	/// </summary>
	public partial class SorryPage : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		#region Constructor
		/// <summary>
		/// Constructor. Sets the PageId.
		/// </summary>
		public SorryPage() : base()
		{
			pageId = PageId.SorryPage;
		}
		#endregion

		#region Event handlers

		/// <summary>
		/// Handler for the load event. Sets up static text and abandons the session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// To allow the Error and Timeout pages 
			// to specify the redirect url for the TD logo on the header control.
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
		/// Used to hide/show the appropriate user instruction
		/// </summary>
		private void SetPanelVisibility()
		{
			panelMessage.Visible=true;
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
			Page.ClientScript.RegisterClientScriptBlock(typeof(SorryPage),"ErrorAndTimeoutLinkHandler", scriptRepository.GetScript("ErrorAndTimeoutLinkHandler", javaScriptDom));		
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
