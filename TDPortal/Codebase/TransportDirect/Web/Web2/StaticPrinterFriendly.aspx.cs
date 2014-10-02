// *************************************************************** 
// NAME                 : StaticPrinterFriendly.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 19/08/2003 
// DESCRIPTION			: Webform containing the printer friendly
// template for the 'Static' pages - (the non-functional pages,
// Help, About TD, Feedback and Contact Us, and Site Map) 
// *************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/StaticPrinterFriendly.aspx.cs-arc  $
//
//   Rev 1.4   Jan 26 2009 13:48:14   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 06 2008 16:50:04   mmodi
//Corrected logged pages for FAQ
//Resolution for 4948: Incorrect page event for FAQ pages, as raised by Peter Armstrong
//
//   Rev 1.2   Mar 31 2008 13:26:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:30   mturner
//Initial revision.
//
//   Rev 1.7   Feb 24 2006 10:17:38   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.6   Feb 10 2006 15:09:26   build
//Automatically merged from branch for stream3180
//
//   Rev 1.5.1.0   Dec 06 2005 16:08:04   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.5   Feb 19 2004 10:01:26   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.4   Feb 16 2004 13:56:32   asinclair
//Updated for Del 5.2
//
//   Rev 1.3   Feb 03 2004 17:02:42   asinclair
//Updated for Del 5.2
//
//   Rev 1.2   Nov 17 2003 17:56:54   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.1   Aug 19 2003 17:32:58   asinclair
//Updated with changes to layout
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

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for StaticPrinterFriendly.
	/// </summary>
	public partial class StaticPrinterFriendly : TDPrintablePage
	{
		
		public StaticPrinterFriendly() : base()
		{
			pageId = PageId.Links;
		}
		
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");

            //Here we change the page id to fix MIS, if required:
            ChangePageIdIfRequired();

            if (!Page.IsPostBack)			
			{
//				labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
//					"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

//				labelInstructions.Text = Global.tdResourceManager.GetString(
//					"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			}
			
		}

        private void ChangePageIdIfRequired()
        {
            string queryStringId = Request.QueryString["id"].ToLower();

            if (queryStringId.StartsWith("_web2_help_"))
            {
                pageId = PageId.Help;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
