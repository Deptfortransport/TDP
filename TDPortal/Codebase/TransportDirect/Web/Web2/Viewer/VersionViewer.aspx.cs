// *********************************************** 
// NAME                 : VersionViewer.aspx.cs
// AUTHOR               : Atos Origin
// DATE CREATED         : 01/07/2004
// DESCRIPTION			: Page to view the version numbers of the portal components e.g. CJP.
//						  ** Access restricted to non-standard users **
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Viewer/VersionViewer.aspx.cs-arc  $
//
//   Rev 1.3   Jan 16 2009 08:12:12   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:27:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:32:00   mturner
//Initial revision.
//
//   Rev 1.3   Feb 24 2006 10:17:36   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.2   Feb 10 2006 15:09:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.1.1.0   Dec 06 2005 17:39:52   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.1   Jul 19 2004 14:53:04   AWindley
//Implementation of CCN098: CJP Logging changes
//
//   Rev 1.0   Jul 02 2004 13:35:54   AWindley
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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for VersionViewer.
	/// </summary>
	public partial class VersionViewer : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		#region Constructor
		/// <summary>
		/// Constructor, sets the PageId and calls base.
		/// </summary>
		public VersionViewer() : base()
		{
			pageId = PageId.VersionViewer;
		}
		#endregion
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Get version info
            // Get the ICJP instance from service discovery
            ICJP cjpObject = (ICJP)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
            string versionInfo = cjpObject.GetVersonInfo();

            if (!Page.IsPostBack)			
			{
				// Set Viewer properties
				textVersionInfo.ReadOnly = true;
				textVersionInfo.TextMode = TextBoxMode.MultiLine;
				textVersionInfo.Rows = 30;
			}

            // Display version info
            if (versionInfo != null)
                textVersionInfo.Text = versionInfo;
            else
                textVersionInfo.Text = Global.tdResourceManager.GetString("VersionViewer.NoVersionInfoAvailable", TDCultureInfo.CurrentUICulture);

            //Added for white labelling:
            ConfigureLeftMenu("VersionViewer.clientLink.BookmarkTitle", "VersionViewer.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextVersionViewer);
            expandableMenuControl.AddExpandedCategory("Related links");

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
			this.ID = "VersionViewer";
		}
		#endregion
	}
}
