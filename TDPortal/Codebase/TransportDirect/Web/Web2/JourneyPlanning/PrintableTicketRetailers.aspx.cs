// *********************************************** 
// NAME                 : PrintableTicketRetailers.aspx.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 04/11/2003
// DESCRIPTION			: Printable ticket retailers page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableTicketRetailers.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:25:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:04   mturner
//Initial revision.
//
//   Rev 1.23   Feb 23 2006 18:24:20   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.22   Feb 10 2006 15:09:20   build
//Automatically merged from branch for stream3180
//
//   Rev 1.21.1.2   Jan 09 2006 16:55:50   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.21.1.1   Dec 12 2005 17:04:28   tmollart
//Removed code to reinstate journey results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.21.1.0   Dec 01 2005 11:58:46   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.21   Nov 18 2005 16:47:04   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.20   Apr 29 2005 13:56:48   rgeraghty
//Browser page title added
//Resolution for 2345: PT: Cosmetic faults on Ticket Retailer page
//
//   Rev 1.19   Apr 16 2005 12:05:18   jgeorge
//Changed code that populates header row to deal with both Time and Cost based search results. Also removed redundant code.
//Resolution for 2145: PT - Unable to go to printer friendly page from ticket retailer page
//
//   Rev 1.18   Mar 22 2005 08:53:28   jgeorge
//FxCop changes
//
//   Rev 1.17   Mar 08 2005 16:47:42   jgeorge
//Rework for Del 7
//
//   Rev 1.16   Mar 01 2005 16:30:02   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.15   Oct 22 2004 15:29:22   jmorrissey
//Added javascript to open a user survey form on Page_Init - if the user survey form should be shown
//
//   Rev 1.14   Oct 13 2004 12:05:42   jmorrissey
//Updated Page_Init to add page Id to the return stack
//
//   Rev 1.13   Oct 08 2004 12:36:34   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.12   Sep 20 2004 16:51:46   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.11   Aug 31 2004 15:06:50   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.10   Jul 23 2004 16:29:04   jgeorge
//Updates for Del 6.1 Find a...
//
//   Rev 1.9   Jul 02 2004 15:07:28   RHopkins
//Corrected to work with ItineraryManager (including printable pages)
//
//   Rev 1.8   Jun 22 2004 17:27:46   jmorrissey
//Added support for Extend Journey
//
//   Rev 1.7   Apr 28 2004 16:20:24   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.6   Feb 19 2004 10:00:58   asinclair
//Added print instructions labels for Del 5.2
//
//   Rev 1.5   Nov 27 2003 16:30:32   CHosegood
//Retailers and Fares pages now have identical behaviour for single, matching & non-matching returns.
//Resolution for 307: Return retailers control appears when single journey selected
//
//   Rev 1.4   Nov 18 2003 16:00:08   COwczarek
//SCR#247 : Complete adding comments to existing code and add $Log: for PVCS history

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
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable ticket retailers page
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableTicketRetailers : TDPrintablePage, INewWindowPage
	{
		#region Control declaration

		protected System.Web.UI.WebControls.Button forcePostback;

		protected JourneyFareHeadingControl journeyFareHeadingControl;
		protected TicketMatrixControl outwardTickets;
		protected RetailerMatrixControl outwardRetailers;
		protected TicketMatrixControl inwardTickets;
		protected RetailerMatrixControl inwardRetailers;
		
		#endregion

		#region Private members

		private TicketRetailersHelper helper;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Sets PageId and LocalResourceManager
		/// </summary>
		public PrintableTicketRetailers() : base()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
			pageId = PageId.PrintableTicketRetailers;

		}
        
		#endregion

		#region Event handlers

		/// <summary>
		/// Page load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			helper = new TicketRetailersHelper(resourceManager);
		}

		/// <summary>
		/// Pre-render event handler. This method is responsible for displaying retailers
		/// for a new journey selection. It is only at this point in the page processing
		/// that we know if a new journey selection has been made. This is because event 
		/// handlers of the journey selection control will have been called by the time this
		/// method is called and will have updated the necessary session data.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, System.EventArgs e) 
		{
			UpdateFaresHeader();

			UpdateTicketMatrixControls();
			UpdatePeopleTravellingControl();

            SetupControls();
		}

		#endregion 

		#region Private Methods

		/// <summary>
		/// Populates the ticket matrix controls with data and ensures only the correct ones are visible
		/// </summary>
		private void UpdateTicketMatrixControls()
		{
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();

			if ((options.OutwardTicketRetailerInfo != null) && (options.OutwardTicketRetailerInfo.RetailUnits.Length != 0))
			{
				panelOutward.Visible = true;
				outwardTickets.Data = options.OutwardTicketRetailerInfo;
				outwardTickets.Discounts = options.Discounts;
				outwardTickets.ShowPeopleTravellingControl = true;
				outwardTickets.SelectedRetailUnit = options.SelectedOutwardRetailUnit;

				outwardRetailerPanel.Visible = options.SelectedOutwardRetailUnit != null;
				outwardRetailers.RetailUnit = options.SelectedOutwardRetailUnit;
			}
			else
				panelOutward.Visible = false;

			if ((options.ReturnTicketRetailerInfo != null) && (options.ReturnTicketRetailerInfo.RetailUnits.Length != 0))
			{
				panelInward.Visible = true;
				inwardTickets.Data = options.ReturnTicketRetailerInfo;
				inwardTickets.Discounts = options.Discounts;
				inwardTickets.ShowPeopleTravellingControl = !panelOutward.Visible;
				inwardTickets.SelectedRetailUnit = options.SelectedReturnRetailUnit;

				inwardRetailerPanel.Visible = options.SelectedReturnRetailUnit != null;
				inwardRetailers.RetailUnit = options.SelectedReturnRetailUnit;
			}
			else
				panelInward.Visible = false;

		}

		/// <summary>
		/// Update the PeopleTravellingControl with the numbers of adults/children travelling
		/// </summary>
		private void UpdatePeopleTravellingControl()
		{
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
			PeopleTravellingControl control = ActivePeopleTravellingControl;

			if (control != null)
			{
				control.Adults = options.AdultPassengers;
				control.Children = options.ChildPassengers;
			}
		}

		/// <summary>
		/// Returns the current PeopleTravellingControl - only one of the outward or inward
		/// Matrix controls will be displaying it.
		/// </summary>
		private PeopleTravellingControl ActivePeopleTravellingControl
		{
			get 
			{
				if (panelOutward.Visible)
					return outwardTickets.PeopleTravellingControl;
				else
					return inwardTickets.PeopleTravellingControl;
			}
		}

		/// <summary>
		/// Populates static labels with data
		/// </summary>
		private void SetupControls()
		{
			labelPrinterFriendly.Text= GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");

            SetupStepsControl();
		}

        /// <summary>
        /// Populates the Fare steps control
        /// </summary>
        private void SetupStepsControl()
        {
            if (FindInputAdapter.IsCostBasedSearchMode(TDSessionManager.Current.FindAMode))
            {
                panelFindFareSteps.Visible = true;
                findFareStepsControl.Printable = true;
                findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep4;
            }
        }

		/// <summary>
		/// Populates the journey details from the session
		/// </summary>
		private void UpdateFaresHeader()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			
			// Get the current journey
			PublicJourney journey = sessionManager.JourneyResult.OutwardPublicJourney(sessionManager.JourneyViewState.SelectedOutwardJourneyID);

			journeyFareHeadingControl.OriginLocation = journey.Details[0].LegStart.Location.Description;
			journeyFareHeadingControl.DestinationLocation = journey.Details[ journey.Details.Length - 1 ].LegEnd.Location.Description;

			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			PricingRetailOptionsState pricingOptions = helper.GetPricingRetailOptionsState();
			journeyFareHeadingControl.CoachCardName = ds.GetText(DataServiceType.DiscountCoachCardDrop, pricingOptions.Discounts.CoachDiscount);
			journeyFareHeadingControl.RailCardName = ds.GetText(DataServiceType.DiscountRailCardDrop, pricingOptions.Discounts.RailDiscount);
		}

		#endregion Private Methods        

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
