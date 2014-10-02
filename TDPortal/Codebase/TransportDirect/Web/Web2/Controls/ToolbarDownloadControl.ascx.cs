// *********************************************** 
// NAME                 : ToolbarDownloadControl.ascx.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 01/12/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ToolbarDownloadControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:14   mturner
//Initial revision.
//
//   Rev 1.4   Feb 24 2006 14:45:54   RWilby
//Fix for merge stream3129.
//
//   Rev 1.3   Feb 24 2006 12:31:16   RWilby
//Fix for merge stream3129. Added using reference to TransportDirect.Common.ResourceManager namespace.
//
//   Rev 1.2   Dec 06 2005 12:12:08   rgreenwood
//Changed pageEvent to ToolbarDownload to fix build
//
//   Rev 1.1   Dec 05 2005 17:15:48   rgreenwood
//TD109: Added labels
//
//   Rev 1.0   Dec 05 2005 15:48:48   rgreenwood
//Initial revision.


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.Logging;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using Logger = System.Diagnostics.Trace;



	/// <summary>
	///	Control that encapsulates functionality allowing users to download files e.g. TD Toolbar
	/// </summary>
	public partial class ToolbarDownloadControl : TDUserControl, ILanguageHandlerIndependent
	{
		protected System.Web.UI.WebControls.Label labelControlHeader;
		protected System.Web.UI.WebControls.Label labelSystemRequirements;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			//Set button & label text
			buttonToolbarDownload.Text = Global.tdResourceManager.GetString(
				"ToolbarDownloadControl.buttonToolbarDownload.Text", TDCultureInfo.CurrentUICulture);
			labelControlHeader.Text = Global.tdResourceManager.GetString(
				"ToolbarDownloadControl.labelControlHeader.Text", TDCultureInfo.CurrentUICulture);
			labelSystemRequirements.Text = Global.tdResourceManager.GetString(
				"ToolbarDownloadControl.labelSystemRequirements.Text", TDCultureInfo.CurrentUICulture);

		}


		/// <summary>
		/// Method to trigger toolbar download.
		/// Obtains channel-dependent Toolbar installation filename, checks file is available in the Web/Doenloads
		/// folder and commence download. If not available, raise an event to display an error.
		/// </summary>
		private void DownloadToolbar()
		{
			//Determine channel and get appropriate filename from properties table
			
			string pageLanguage = TDPage.GetChannelLanguage(TDPage.SessionChannelName.ToString());             
			string fileName = string.Empty;

			if (pageLanguage == TDPage.EnglishLanguage)
			{ 
				fileName = Properties.Current["Web.ToolbarDownloadFileName.English"].ToString(); 
			}
			
			else if (pageLanguage == TDPage.WelshLanguage)
			{
				fileName = Properties.Current["Web.ToolbarDownloadFileName.Welsh"].ToString();
			}

			else
			{
				// No match with either English or Welsh, throw an exception
				throw new TDException("Invalid Culture Channel", null, false, TDExceptionIdentifier.TBDInvalidCultureChannel);
			}

			string fullPath = Server.MapPath(fileName); 

			//File not found at specified location so raise an exception to the page for error display
			if (!System.IO.File.Exists(fullPath)) 
				throw new TDException("File not found", null, false, TDExceptionIdentifier.TBDFileNotFound);

			else
			{
				//File exists in web/downloads folder, commence download
				Response.Clear();
				Response.ContentType="application";
				Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
				Response.WriteFile(fullPath);
				Response.Flush();
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            buttonToolbarDownload.Click += new EventHandler(buttonToolbarDownload_Click);
		}
		#endregion

		protected void buttonToolbarDownload_Click(object sender, System.EventArgs e)
		{
			DownloadToolbar();
			
			//Raise PageEntryEvent to log this download attempt
			PageEntryEvent logPage =  new PageEntryEvent(PageId.ToolbarDownload,
				Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(logPage);
		}
	}
}
