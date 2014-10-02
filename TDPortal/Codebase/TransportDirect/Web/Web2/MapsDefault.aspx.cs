// *********************************************** 
// NAME                 : MapsDefaut.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 17/07/2003 
// DESCRIPTION			: Webform containing the default
// template for the Maps pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/MapsDefault.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:26:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:22   mturner
//Initial revision.
//
//DEVFACTORY FEB 22 2008 sbarker
//white labelling changes
//
//   Rev 1.16   Sep 03 2007 15:25:34   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.15   Aug 06 2007 14:24:56   jfrank
//Fix for problem if Host other than transportdirect.info is used e.g. transportdirect.co.uk
//Resolution for 4471: URL domain issue - if .co.uk, .com, .org.uk are used.
//
//   Rev 1.14   Feb 23 2006 18:08:08   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.13   Feb 10 2006 15:09:18   build
//Automatically merged from branch for stream3180
//
//   Rev 1.12.1.0   Dec 02 2005 14:48:38   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.12   Mar 21 2005 09:17:06   rscott
//NoWrap added to header div to prevent TDOnTheMove tab image button from wrapping.
//
//   Rev 1.11   Jan 05 2004 16:19:44   asinclair
//Added a ref to the base url for this page in the properties database, to fix IR 574
//
//   Rev 1.10   Nov 17 2003 16:26:46   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.9   Sep 30 2003 20:02:44   asinclair
//Initial Version
//
//   Rev 1.8   Sep 22 2003 18:28:20   asinclair
//Added Page Id
//Resolution for 2: Session Manager - Deferred storage
//
//   Rev 1.7   Sep 18 2003 11:48:22   asinclair
//Updated Placeholders
//
//   Rev 1.6   Sep 04 2003 10:43:30   asinclair
//Added HTML placeholder
//
//   Rev 1.5   Sep 01 2003 14:39:28   asinclair
//Removed the reference to 'RobotMetaTag'
//
//   Rev 1.4   Aug 15 2003 15:34:10   asinclair
//Changed Header control
//
//   Rev 1.3   Jul 24 2003 12:13:42   asinclair
//Removed the reference to the the RobotMetaTag control
//
//   Rev 1.2   Jul 24 2003 11:35:16   asinclair
//Changed name
//
//   Rev 1.1   Jul 22 2003 10:53:10   asinclair
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
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Base class for the default template for the Map pages.
	/// </summary>
	public partial class MapsDefault : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		
		public MapsDefault() : base()
		{
			pageId = PageId.NetworkMaps;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");


            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
            expandableMenuControl.AddExpandedCategory("Find a place");
            expandableMenuControl.AddExpandedCategory("Network maps");
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
