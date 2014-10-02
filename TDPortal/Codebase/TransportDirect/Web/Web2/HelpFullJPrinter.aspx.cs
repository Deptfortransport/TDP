// *********************************************** 
// NAME                 : HelpFullJPrinter.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 02/02/2004 
// DESCRIPTION			: Connected MCMS template for the printer
//  version of the full help pages.  This is a connected template, and is 
// contected to HelpFullJP.aspx
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/HelpFullJPrinter.aspx.cs-arc  $:
//
//   Rev 1.5   Dec 16 2008 10:17:12   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Oct 14 2008 10:08:18   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jun 26 2008 14:03:48   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2.1.0   Jul 31 2008 11:23:32   mmodi
//Updated for cycle planner, and corrected Help text loading for PrinterFriendly page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:23:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:46   mturner
//Initial revision.
//
//   Rev 1.5   Aug 06 2007 14:23:26   jfrank
//Fix for problem if Host other than transportdirect.info is used e.g. transportdirect.co.uk
//Resolution for 4471: URL domain issue - if .co.uk, .com, .org.uk are used.
//
//   Rev 1.4   Feb 23 2006 17:52:32   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.3   Feb 10 2006 15:09:14   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.0   Dec 06 2005 15:02:08   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.2   Nov 03 2005 16:02:02   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.1.1.0   Oct 26 2005 10:13:22   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.1   Feb 19 2004 09:57:46   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.0   Feb 16 2004 13:53:02   asinclair
//Initial Revision

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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for HelpFullJP.
	/// </summary>
	public partial class HelpFullJPrinter : TDPrintablePage
	{

		public HelpFullJPrinter() : base()
		{
			pageId = PageId.PrintableHelpFullJP;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");

			if (!Page.IsPostBack)			
			{
                //Get the path without the filename:
                Uri url = Request.Url;
                string fileName = url.Segments[url.Segments.Length - 1];
                string baseUrlText = url.ToString().Replace(fileName, "");

                basehelpliteral.Text = string.Format(@"<base href=""{0}""/>", baseUrlText);

				labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
					"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

				labelInstructions.Text = Global.tdResourceManager.GetString(
					"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);
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

		private void ImageButtonBack1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.HelpFullJPBack; // default one

		}
	}
}
