// ******************************************************************
// NAME                 : PrintableFindFareDateSelection.aspx.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 23/03/2005
// DESCRIPTION			: Printable version of FindFareDateSelection 
//                        page
// ******************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableFindFareDateSelection.aspx.cs-arc  $
//
//   Rev 1.3   Jan 08 2009 10:49:54   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:36   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 18:11:46   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.3   Feb 10 2006 15:04:46   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.0   Dec 02 2005 11:18:24   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.2   Nov 18 2005 16:42:38   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.1   Apr 28 2005 09:17:56   rscott
//Fix for IR2137 - include notes at the bottom of the printer page.
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
	public partial class PrintableFindFareDateSelection : TDPrintablePage, INewWindowPage
	{
		//Label controls
		protected System.Web.UI.WebControls.Label instructionLabel;

		//User controls
		protected JourneysSearchedForControl theJourneysSearchedForControl;
		protected FindFareSingleTravelDatesControl findFareSingleTravelDatesControl;
		protected FindFareReturnTravelDatesControl findFareReturnTravelDatesControl;
		protected PrintablePageInfoControl printablePageInfoControl;

		// Private members
		private ITDSessionManager tdSessionManager;
		private FindCostBasedPageState pageState;
		private CostSearchParams searchParams;

		public PrintableFindFareDateSelection() : base()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
			pageId = PageId.PrintableFindFareDateSelection;
		}
				 
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Set up page labels etc
			PageTitle = GetResource("FindFare.DefaultPageTitle");
			labelPrinterFriendly.Text = Global.tdResourceManager.GetString("StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);
			labelInstructions.Text = Global.tdResourceManager.GetString("StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			//Set up ticket controls
			tdSessionManager = TDSessionManager.Current;
			pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
			searchParams = (CostSearchParams)tdSessionManager.JourneyParameters;

			if ((pageState.SelectedTicketType == TicketType.Return) || (pageState.SelectedTicketType == TicketType.Singles))
			{
				findFareSingleTravelDatesControl.Visible = false;
				findFareReturnTravelDatesControl.Visible = true;
				findFareReturnTravelDatesControl.PageState = pageState;
				findFareReturnTravelDatesControl.SearchParams = searchParams;
			}
			else
			{
				findFareReturnTravelDatesControl.Visible = false;
				findFareSingleTravelDatesControl.Visible = true;
				findFareSingleTravelDatesControl.PageState = pageState;
				findFareSingleTravelDatesControl.SearchParams = searchParams;
			}

            findFareStepsControl.Printable = true;
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep1;

			//Set up printable page info object
			printablePageInfoControl.Date = TDDateTime.Now.ToString("G");
			printablePageInfoControl.UserNameVisible = tdSessionManager.Authenticated;
			if (tdSessionManager.Authenticated)
			{
				printablePageInfoControl.UserName = tdSessionManager.CurrentUser.Username;
			}
			printablePageInfoControl.JourneyReferenceNumberVisible = false;


			//IR2137 Include notes at bottom of printable page
			if (pageState.SelectedTicketType == TicketType.Return)
			{
				noteLabel.Text = GetResource("FindFareDateSelection.ReturnSinglesNote");
			}
			else
			{
				noteLabel.Text = GetResource("FindFareDateSelection.SingleNote");
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
