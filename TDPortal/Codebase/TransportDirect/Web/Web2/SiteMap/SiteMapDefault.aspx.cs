// *********************************************** 
// NAME                 : SiteMapDefaut.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 13/08/2003 
// DESCRIPTION			: The template for the SiteMap
// page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/SiteMap/SiteMapDefault.aspx.cs-arc  $:
//
//   Rev 1.2   Mar 31 2008 13:26:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:24   mturner
//Initial revision.
//
//   Rev 1.11   Aug 06 2007 14:25:44   jfrank
//Fix for problem if Host other than transportdirect.info is used e.g. transportdirect.co.uk
//Resolution for 4471: URL domain issue - if .co.uk, .com, .org.uk are used.
//
//   Rev 1.10   Feb 23 2006 18:34:04   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.9   Feb 15 2006 10:05:26   aviitanen
//Added code to get baseliteral value from the properties database. 
//Resolution for 3577: DEL 8.0 : Related Sites and Site Map - Headerclick with no javascript causes error
//
//   Rev 1.8   Feb 10 2006 15:09:24   build
//Automatically merged from branch for stream3180
//
//   Rev 1.7.2.0   Dec 06 2005 18:56:34   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.7   Mar 21 2005 10:11:58   rscott
//NoWrap adde to header div to prevent TDOnTheMove Tab Image Button from wrappping
//
//   Rev 1.6   Sep 18 2004 16:18:16   COwczarek
//Remove superfluous statements to set Find A mode to
//Station/Airport. Additional query string parameter added to
//Find A Station/Airport input page URL now takes care of this.
//Resolution for 1561: Find A sub navigation does not use real hyperlinks
//
//   Rev 1.5   Sep 06 2004 17:03:06   asinclair
//Changed to add the Quick planners for Del 6 and to set the FindaPageState
//
//   Rev 1.4   Nov 18 2003 13:56:54   asinclair
//Added comments

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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for SiteMapDefault.
	/// </summary>
	public partial class SiteMapDefault : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl HeaderControl1;

		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public SiteMapDefault() : base()
		{
			pageId = PageId.SiteMap;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string baseliteralURL = Properties.Current["StaticNoPrint.baseurl"];

			int urlStart = baseliteralURL.IndexOf("http://")+7;
			int slash = baseliteralURL.IndexOf("/",urlStart);

			baseliteral.Text = baseliteralURL.Substring(0, urlStart) + Request.Url.Host 
				+ baseliteralURL.Substring(slash, baseliteralURL.Length - slash);
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
