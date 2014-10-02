// ******************************************************************
// NAME                 : PrintableFindFareDateSelection.aspx.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 23/03/2005
// DESCRIPTION			: Printable version of FindFareDateSelection 
//                        page
// ******************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableFindFareTicketSelection.aspx.cs-arc  $
//
//   Rev 1.3   Jan 26 2009 12:55:40   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:38   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 18:12:16   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.3   Feb 10 2006 15:04:48   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.0   Dec 02 2005 11:18:24   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.2   Nov 18 2005 16:43:00   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.1   Apr 27 2005 10:20:36   COwczarek
//Fix compiler warnings
//
//   Rev 1.0   Mar 30 2005 17:09:28   tmollart
//Initial revision.
//
//   Rev 1.0   Mar 29 2005 15:20:14   tmollart
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
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Web.Support;

namespace Web.Templates
{
	/// <summary>
	/// Summary description for PrintableFindFareDateSelection.
	/// </summary>
	public partial class PrintableFindFareTicketSelection : TDPrintablePage, INewWindowPage
	{
		//Label controls
		protected System.Web.UI.WebControls.Label instructionLabel;

		//User controls
		protected JourneysSearchedForControl theJourneysSearchedForControl;
		protected PrintablePageInfoControl printablePageInfoControl;
		protected FindFareTicketSelectionControl ticketSelectionControl;

		public PrintableFindFareTicketSelection() : base()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
			pageId = PageId.PrintableFindFareTicketSelection;
		}
				 
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Set up page labels etc
			PageTitle = GetResource("FindFare.DefaultPageTitle");
			labelPrinterFriendly.Text = Global.tdResourceManager.GetString("StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);
			labelInstructions.Text = Global.tdResourceManager.GetString("StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			//Set up ticket controls
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

			ticketSelectionControl.Tickets = pageState.SingleOrReturnTicketTable;
			ticketSelectionControl.Single = true;
			ticketSelectionControl.SelectedIndex = pageState.SelectedSingleOrReturnTicketIndex;
			ticketSelectionControl.Outward = true;
			ticketSelectionControl.CurrentOutwardDate = pageState.SelectedSingleDate;

            findFareStepsControl.Printable = true;
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep2;

			//Set up printable page info object
			printablePageInfoControl.Date = TDDateTime.Now.ToString("G");
			printablePageInfoControl.UserNameVisible = TDSessionManager.Current.Authenticated;
			if (TDSessionManager.Current.Authenticated)
			{
				printablePageInfoControl.UserName = TDSessionManager.Current.CurrentUser.Username;
			}
			printablePageInfoControl.JourneyReferenceNumberVisible = false;
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
