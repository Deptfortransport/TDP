// *********************************************** 
// NAME                 : Defaut.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 15/07/2003 
// DESCRIPTION			: Webform containing the default
// links / graphics for each TD Portal page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Default.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:10:44   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 16:08:42   RWilby
//Merged stream3129
//
//   Rev 1.5   Feb 10 2006 15:04:36   build
//Automatically merged from branch for stream3180
//
//   Rev 1.4.1.0   Dec 06 2005 18:22:06   kjosling
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.5   Dec 06 2005 18:18:58   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.4   Jul 17 2003 16:46:26   asinclair
//Removed a using reference that wasn't needed.
//
//   Rev 1.3   Jul 15 2003 16:51:42   asinclair
//Added controls to web form

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
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.ResourceManager;


namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class Default : TDPage
	{
        protected System.Web.UI.WebControls.Panel placeHolderPanel;
        
        public Default() : base()
        {
            pageId = PageId.Home;
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");
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
