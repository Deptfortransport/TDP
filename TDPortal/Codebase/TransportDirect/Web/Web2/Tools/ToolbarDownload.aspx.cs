// ************************************************
// NAME                 : ToolbarDownload.aspx
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 01/12/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Tools/ToolbarDownload.aspx.cs-arc  $
//
//   Rev 1.5   Jul 28 2011 16:21:28   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.4   Jan 30 2009 10:44:32   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.3   Jan 15 2009 14:55:48   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:27:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:54   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.8   Feb 24 2006 14:46:56   RWilby
//Fix for merge stream3129.
//
//   Rev 1.7   Feb 24 2006 12:46:20   RWilby
//Fix for merge stream3129. Added using reference to TransportDirect.Common.ResourceManager namespace.
//
//   Rev 1.6   Feb 24 2006 12:37:28   AViitanen
//Merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.5   Feb 10 2006 16:56:42   kjosling
//Removed redundant variable
//
//   Rev 1.4   Jan 30 2006 14:01:38   jbroome
//Ensure response stream ends after toolbar.exe is served.
//Resolution for 3524: Toolbar (Gigasoft): Downloading the toolbar aborts with an Invalid Publisher error
//
//   Rev 1.3   Dec 14 2005 14:42:22   rgreenwood
//TD109: Corrected HeaderControl declaration
//
//   Rev 1.2   Dec 13 2005 11:50:48   rgreenwood
//TD109: Code review actions - completed documentation and removed extraneous error message clauses.
//
//   Rev 1.1   Dec 12 2005 14:54:58   rgreenwood
//TD109: Design changes - removed ToolbarDownloadControl and refactored
//
//   Rev 1.0   Dec 06 2005 12:03:18   rgreenwood
//Initial revision.

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
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.DatabaseInfrastructure.Content;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for ToolbarDownload.
	/// </summary>
	public partial class ToolbarDownload : TDPage, ILanguageHandlerIndependent
	{


		protected System.Web.UI.WebControls.Label labelControlHeader;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl;

		/// <summary>
		/// Constructor. Set page ID here.
		/// </summary>
		public ToolbarDownload()
		{
			pageId = PageId.ToolbarDownload;
		}
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//Ensure error message controls are hidden on first page viewing
				errorMessagePanel.Visible = false;
				errorDisplayControl.Visible = false;

                PageTitle = GetResource("ToolbarDownload.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
			}

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextToolbarDownload);
            imageToolbarDownload.ImageUrl = GetResource("HomeTipsTools.imageToolbarDownload.ImageUrl");
            imageToolbarDownload.AlternateText = " ";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

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

		protected void Page_Init(object sender, System.EventArgs e)
		{
			//Set resources
			labelPageTitle.Text = Global.tdResourceManager.GetString(
				"ToolbarDownload.labelPageTitle.Text", TDCultureInfo.CurrentUICulture);
			buttonToolbarDownload.Text = Global.tdResourceManager.GetString(
				"ToolbarDownload.buttonToolbarDownload.Text", TDCultureInfo.CurrentUICulture);
			buttonToolbarDownload.ToolTip = Global.tdResourceManager.GetString(
				"ToolbarDownload.buttonToolbarDownload.ToolTip", TDCultureInfo.CurrentUICulture);
			labelDownloadInfo.Text = Global.tdResourceManager.GetString(
				"ToolbarDownload.labelControlHeader.Text", TDCultureInfo.CurrentUICulture);
			labelSystemRequirements.Text = Global.tdResourceManager.GetString(
				"ToolbarDownload.labelSystemRequirements.Text", TDCultureInfo.CurrentUICulture);
			

			//Call to wire up button click event
			ExtraWiringEvents();
		}

		/// <summary>
		/// Event wire-up for tdbutton
		/// </summary>
		private void ExtraWiringEvents()
		{
			//Wire up the download button
			this.buttonToolbarDownload.Click += new EventHandler(this.buttonToolbarDownload_Click);
		}


		/// <summary>
		/// Method to trigger toolbar download.
		/// Obtains channel-dependent Toolbar installation filename, checks file is available in the Web/Downloads
		/// folder and commences download. If not available, display an error.
		/// </summary>
		private void DownloadToolbar()
		{
			//Determine channel and get appropriate filename from properties table
            Language pageLanguage = CurrentLanguage.Value;
			string fileName = string.Empty;

			if (pageLanguage == Language.English)
			{
				//Download English toolbar
				fileName = Properties.Current["Web.ToolbarDownloadFileName.English"].ToString(); 
			}
			
			else
			{
				//Download Welsh toolbar
				fileName = Properties.Current["Web.ToolbarDownloadFileName.Welsh"].ToString();
			}


			string fullPath = Server.MapPath(fileName); 

			//File not found at specified location so display error message
			if (!System.IO.File.Exists(fullPath)) 
			{
				ArrayList errorsList = new ArrayList();

				errorsList.Add(Global.tdResourceManager.GetString(
				"ToolbarDownload.FileNotFoundError.Text", TDCultureInfo.CurrentUICulture));

				errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

				errorDisplayControl.Type = ErrorsDisplayType.Error;

				errorDisplayControl.Visible = true;
				errorMessagePanel.Visible = true;
			
			}

			else
			{
				//File exists in specified path, commence download
				Response.Clear();
				Response.ContentType="application";
				Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(fileName));
				Response.WriteFile(fullPath);
				Response.Flush();
				Response.Close();

				//Log the download activity
				PageEntryEvent logpage = new PageEntryEvent(PageId.ToolbarDownloadButtonClick, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				errorDisplayControl.Visible = false;
				errorMessagePanel.Visible = false;
			}


		}

		/// <summary>
		/// Click event for tdbutton. Calls Download toolbar method to initiate download of English or Welsh toolbar,
		/// or display an error if the relevant file is not found.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonToolbarDownload_Click(object sender, System.EventArgs e)
		{
			DownloadToolbar();
		
		}
	}
}
