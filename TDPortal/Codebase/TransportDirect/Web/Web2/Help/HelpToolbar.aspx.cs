// *********************************************** 
// NAME                 : HelpToolbar.aspx.cs 
// AUTHOR               : Russell Wilby
// DATE CREATED         : 09/12/2005 
// DESCRIPTION			: MCMS template for the toolbat help
// ************************************************ 
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
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.Common.PropertyService.Properties;
using CmsMockObject.Objects;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for HelpToolbar
	/// </summary>
	public partial class HelpToolbar : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;

		public HelpToolbar() : base()
		{
			pageId = PageId.HelpToolbar;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");
            else
            {
                // CCN 0427 Setting Printer friendly link
                string uri = "HelpFullJPrinter.aspx?" + Request.QueryString.ToString();


                // set the hyperlinks
                PrinterFriendlyLink.NavigateUrl = uri;
                hyperlinkText.Text = GetResource("PrinterFriendlyPageButton.Text"); ;
                PrinterFriendlyLink.ToolTip = GetResource("PrinterFriendlyButton.Tooltip");
            }

            //Get the path without the filename:
            Uri url = Request.Url;
            string fileName = url.Segments[url.Segments.Length - 1];
            string baseUrlText = url.ToString().Replace(fileName, "");

            basehelpliteral.Text = string.Format(@"<base href=""{0}"">", baseUrlText);
			
            /*string basehelpliteralURL = Properties.Current["HelpFullJP.baseurl"];
	
			int urlStart = basehelpliteralURL.IndexOf("http://")+7;
			int slash = basehelpliteralURL.IndexOf("/",urlStart);

			basehelpliteral.Text = basehelpliteralURL.Substring(0, urlStart) + Request.Url.Host 
				+ basehelpliteralURL.Substring(slash, basehelpliteralURL.Length - slash);

			// Set the URL of the Printer Friendly button
			// Gets the current Page URL and changes it to that of the printer friendly pages - in printer channel.
			string url= CmsHttpContext.Current.ChannelItem.Url.ToString();
			printerFriendlyPageButton.Url = url.Replace(@"/Help/", @"/printer/");*/

            //Added for white labelling:
            ConfigureLeftMenu("HelpToolbar.clientLink.BookmarkTitle", "HelpToolbar.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
		}

		override protected void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			//Reset the tab selection override
		}


		#region Properties
		/// <summary>
		/// Exposes the Print button control that links to a printer-friendly page.
		/// The Page ID of the printer-friendly page is the current page ID prefixed with "Printable".
		/// If no printer-friendly page is available then the Print button will not be shown.
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl PrinterFriendlyPageButton
		{
			get { return printerFriendlyPageButton; }
			set { printerFriendlyPageButton = value; }
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			//Ensure that the tab selection isn't changed because this page has each one of
			//the tab headers and when each one loads, it will overwrite the current one
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
