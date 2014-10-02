// *********************************************** 
// NAME                 : JourneyPlannerDefaut.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 17/07/2003 
// DESCRIPTION			: Webform containing the default
// template for the Journey Planner pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlannerDefault.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:24:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:08   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 18:04:40   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.6   Feb 10 2006 15:08:44   build
//Automatically merged from branch for stream3180
//
//   Rev 1.5.1.0   Nov 30 2005 14:55:42   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.5   Dec 03 2003 14:38:40   esevern
//fix for IR78 - added full doctype tag
//Resolution for 78: DOCTYPE tag
//
//   Rev 1.4   Nov 17 2003 16:12:12   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.3   Sep 01 2003 14:38:34   asinclair
//Removed the reference to 'RobotMetaTag'
//
//   Rev 1.2   Aug 04 2003 13:48:00   asinclair
//Added graphics
//
//   Rev 1.1   Jul 24 2003 12:13:56   asinclair
//Removed the reference to the the RobotMetaTag control
//
//   Rev 1.0   Jul 24 2003 11:35:54   asinclair
//Initial Revision
//
//   Rev 1.1   Jul 22 2003 10:53:46   asinclair
//Changed comment block

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
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Base class for the default template for the Journey Planner pages.
	/// </summary>
	public partial class JourneyPlannerDefault : TDPage
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            //Added for white labelling:
            //(This code doesn't work, although this page is a template.)
            ConfigureLeftMenu("***.clientLink.BookmarkTitle", "***.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.CONTEXT1);
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
