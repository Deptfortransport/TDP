// *********************************************** 
// NAME                 : ClientLinkControl.ascx.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 21/11/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ClientLinkControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 01 2009 16:25:06   apatel
//Updates for Social Bookmark links
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.3   Oct 08 2008 13:37:48   mturner
//Updated for XHTML compliance
//
//   Rev 1.2.1.0   Sep 15 2008 10:52:30   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:19:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:54   mturner
//Initial revision.
//
//   Rev 1.1   Feb 10 2006 15:04:46   build
//Automatically merged from branch for stream3180
//
//   Rev 1.0.1.1   Jan 03 2006 11:04:30   RGriffith
//Changed Img control to a TDImage
//
//   Rev 1.0.1.0   Dec 23 2005 15:29:04   RGriffith
//Changes for using clientlinks on the homepages
//
//   Rev 1.1   Dec 20 2005 14:14:44   RGriffith
//Changes for using clientlinks on the homepages
//
//   Rev 1.0   Nov 23 2005 11:21:00   jgeorge
//Initial revision.

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Displays a link to the user that allows them to add a bookmark to the current journey result.
	/// </summary>
	/// <remarks>
	/// The main purpose of the control is to encapsulate the logic that is used to control the display 
	/// of the link and the actual creation of the bookmark.  However, the control will have basic 
	/// functionality that allows it to be used to make a simple bookmark of the current page without 
	/// requiring additional coding.
	/// The control will only work when placed on a page derived from TDPage.
	/// </remarks>
	public partial class ClientLinkControl : TDUserControl, ILanguageHandlerIndependent
	{

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

		}
		#endregion

		// String used for the hyperlink onclick handler.
		const string createBookmark = "return CreateBookmark(this, '{0}', '{1}');";

		// String added to the page to render the client link.
		const string showBookmark = "<script language=\"javascript\" type=\"text/javascript\">MakeClientLinkVisible('{0}');</script>";

		string bookmarkTitle;
        string linkAltText;

		#region Public properties

		/// <summary>
		/// Gets or sets the Url that will be used for the bookmark.
		/// </summary>
		public string BookmarkUrl
		{
			get { return clientLink.NavigateUrl; }
			set { clientLink.NavigateUrl = value; }
		}

		/// <summary>
		/// Gets or sets the name that will be given to the bookmark.
		/// </summary>
		public string BookmarkTitle
		{
			get { return bookmarkTitle; }
			set { bookmarkTitle = value; }
		}

		/// <summary>
		/// Gets or sets the text that will be displayed as the link the user clicks to 
		/// add the bookmark.
		/// </summary>
		public string LinkText
		{
			get { return clientLink.Text; }
			set { clientLink.Text = value; }
		}

        public string LinkAltText
        {
            get 
            {
                if (string.IsNullOrEmpty(linkAltText))
                {
                    return LinkText;
                }
                else
                {
                    return linkAltText;
                }
            }
            set { linkAltText = value; }
        }

		#endregion

		#region Page lifecycle methods

		/// <summary>
		/// Overridden method. Uses the property values to set up the control, and adds required JavaScript
		/// to the containing page.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			// Build the JavaScript strings.
			clientLink.Action = string.Format(createBookmark, BookmarkUrl, BookmarkTitle);
			string startupAction = string.Format(showBookmark, linkContainer.ClientID);

			// Determine whether or not JavaScript is enabled.
			TDPage thePage = (TDPage)this.Page;
		
			clientLink.EnableClientScript = thePage.IsJavascriptEnabled;

			// Add the script to the page.
			if (clientLink.EnableClientScript)
			{
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
				thePage.ClientScript.RegisterClientScriptBlock(typeof(ClientLinkControl), clientLink.ScriptName, scriptRepository.GetScript(clientLink.ScriptName, thePage.JavascriptDom ));
                thePage.ClientScript.RegisterStartupScript(typeof(ClientLinkControl), this.UniqueID, startupAction);
			}

			linkStar.ImageUrl = resourceManager.GetString("ClientLinkControl.linkStar.ImageUrl");
			linkStar.AlternateText = LinkAltText;
			base.OnPreRender (e);
		}


		#endregion
	}
}
