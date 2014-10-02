// *********************************************** 
// NAME                 : PrintableCompareAdjustedJourney.aspx.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 29/09/2003
// DESCRIPTION			: The printable version of compare adjusted journey.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableCompareAdjustedJourney.aspx.cs-arc  $
//
//   Rev 1.3   Jan 14 2009 14:19:54   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:30   mturner
//Initial revision.
//
//   Rev 1.14   Feb 23 2006 18:10:54   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.13   Feb 10 2006 15:09:18   build
//Automatically merged from branch for stream3180
//
//   Rev 1.12.1.0   Dec 02 2005 11:18:24   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.12   Nov 18 2005 16:42:18   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.11   Apr 28 2004 16:20:18   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.10   Apr 03 2004 14:53:34   AWindley
//DEL5.2 QA Changes: Resolution for 692
//
//   Rev 1.9   Nov 17 2003 16:34:04   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.8   Nov 07 2003 11:29:24   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.7   Nov 05 2003 13:12:06   kcheung
//Updated comments and summary headers.
//
//   Rev 1.6   Nov 04 2003 13:54:00   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.5   Oct 10 2003 12:52:28   kcheung
//Page Title read from lang strings.
//
//   Rev 1.4   Oct 06 2003 11:44:24   PNorell
//Updated to conform to new wireframes.
//Updated to exclude them from the ScreenFlow.
//
//   Rev 1.3   Oct 03 2003 14:55:16   PNorell
//Added File header
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
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printer friendly version of the Compare Adjusted Journey page.
	/// </summary>
	/// 
	public partial class PrintableCompareAdjustedJourney : TDPrintablePage, INewWindowPage
	{

		protected JourneyDetailsCompareControl JourneyDetailsCompareControl1;

		/// <summary>
		/// Constructor - sets the page Id.
		/// </summary>
		public PrintableCompareAdjustedJourney()
		{
			this.pageId = PageId.PrintableCompareAdjustedJourney;
		}
	
		/// <summary>
		/// Page Load Method. Sets up the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// Set Printer Friendly instructions
			labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

			labelInstructions.Text = Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			labelDate.Text = TDDateTime.Now.ToString("G");
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}

			// Set the journey reference number from the result
			labelJourneyReferenceNumber.Text =
				TDSessionManager.Current.JourneyResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			JourneyDetailsCompareControl1.MyPageId = pageId;
			JourneyDetailsCompareControl1.Printable = true;
			TDSessionManager.Current.FormShift[ SessionKey.SkipScreenFlow ] = true;
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
