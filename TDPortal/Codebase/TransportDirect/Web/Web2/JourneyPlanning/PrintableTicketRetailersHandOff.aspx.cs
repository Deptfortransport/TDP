// *********************************************** 
// NAME                 : PrintableTicketRetailersHandOff.aspx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 08/03/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableTicketRetailersHandOff.aspx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:25:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:06   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 18:25:14   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.4   Feb 10 2006 15:09:20   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.0   Dec 01 2005 11:58:46   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.3   Nov 18 2005 16:47:26   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.2   Mar 31 2005 17:40:28   jgeorge
//Removed instructional text
//
//   Rev 1.1   Mar 22 2005 08:53:30   jgeorge
//FxCop changes
//
//   Rev 1.0   Mar 08 2005 16:51:16   jgeorge
//Initial revision.

using System;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.Web.Controls;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable hand-off page to ticket retailers.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableTicketRetailersHandOff : TDPrintablePage, INewWindowPage
	{
		#region Controls and private members

		protected System.Web.UI.WebControls.Panel handoffForm;

		protected RetailerHandoffHeadingControl handoffHeading;
		protected RetailerHandoffDetailControl handoffDetail;
		protected RetailerInformationControl offlineRetailerInformation;

		private const string resourceKeyOnlineListHeading = "TicketRetailersHandOff.Online.List.{0}.Heading";
		private const string resourceKeyOnlineListItem = "TicketRetailersHandOff.Online.List.{0}.{1}";

		/// <summary>
		/// The retailer that was selected from the ticket retailers page
		/// </summary>
		private Retailer selectedRetailer;

		/// <summary>
		/// The retail unit that was selected from the ticket retailers page
		/// </summary>
		private RetailUnit selectedRetailUnit;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets PageId and LocalResourceManager
		/// </summary>
		public PrintableTicketRetailersHandOff() : base()
		{
			pageId = PageId.PrintableTicketRetailersHandOff;
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
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
            getRetailerDetails();

            initialiseControls();
		}

		/// <summary>
		/// Page unload event handler.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Unload(object sender, System.EventArgs e) 
		{
			//Reset the tab selection override
			TDSessionManager.Current.TabSectionChangeable = true;
		}

		#endregion 

		#region Private methods

        /// <summary>
        /// Gets details of retailer selected on ticket retailers page
        /// </summary>
        private void getRetailerDetails() 
        {
			PricingRetailOptionsState options = TDSessionManager.Current.PricingRetailOptions;
			
			selectedRetailer = options.LastRetailerSelection;
			
			bool isForReturn = options.LastRetailerSelectionIsForReturn;

			if (isForReturn)
				selectedRetailUnit = options.SelectedReturnRetailUnit;
			else
				selectedRetailUnit = options.SelectedOutwardRetailUnit;

        }

		/// <summary>
		/// Initialises all controls
		/// </summary>
		private void initialiseControls()
		{	
			LoadStaticLabels(selectedRetailer.isHandoffSupported);
			UpdateFaresHeader();
			UpdateData();

			if (selectedRetailer.isHandoffSupported)
			{
				panelHandoff.Visible = true;
				panelOfflineInformation.Visible = false;

			}
			else
			{
				panelHandoff.Visible = false;
				panelOfflineInformation.Visible = true;
				offlineRetailerInformation.RetailerDetails = selectedRetailer;
			}
		}

		/// <summary>
		/// Populates the journey details from the session
		/// </summary>
		private void UpdateFaresHeader()
		{
			PricingRetailOptionsState pricingOptions = TDSessionManager.Current.PricingRetailOptions;
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			handoffHeading.CoachCardName = ds.GetText(DataServiceType.DiscountCoachCardDrop, pricingOptions.Discounts.CoachDiscount);
			handoffHeading.RailCardName = ds.GetText(DataServiceType.DiscountRailCardDrop, pricingOptions.Discounts.RailDiscount);
		}

		/// <summary>
		/// Updates the detail control
		/// </summary>
		private void UpdateData()
		{
			PricingRetailOptionsState pricingOptions = TDSessionManager.Current.PricingRetailOptions;
			DiscountCardCatalogue discountCards = (DiscountCardCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.DiscountCardCatalogue];

			handoffDetail.AdultsTravelling = pricingOptions.AdultPassengers;
			handoffDetail.ChildrenTravelling = pricingOptions.ChildPassengers;
			handoffDetail.SelectedTickets = selectedRetailUnit.Tickets;
			
			if (pricingOptions.Discounts.RailDiscount.Length != 0)
				handoffDetail.RailCard = discountCards.GetDiscountCard(ModeType.Rail, pricingOptions.Discounts.RailDiscount);

			if (pricingOptions.Discounts.CoachDiscount.Length != 0)
				handoffDetail.CoachCard = discountCards.GetDiscountCard(ModeType.Coach, pricingOptions.Discounts.CoachDiscount);

			// Only show the discount disclaimer if we have discount cards specifed
            panelDiscountCardDisclaimer.Visible = (handoffDetail.RailCard != null) || (handoffDetail.CoachCard != null);
		}

		/// <summary>
		/// Loads data into the labels on the page. Different labels are populated for an online retailer than
		/// for an offline one.
		/// </summary>
		/// <param name="online"></param>
		private void LoadStaticLabels(bool online)
		{
			labelPageName.Text = GetResource("TicketRetailersHandOff.PageTitle");
			labelDiscountDisclaimer.Text = GetResource("TicketRetailersHandOff.DiscountNote");

			labelPrinterFriendly.Text= GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");

			if (online)
			{
				labelList3Heading.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListHeading, "3") );
				labelList3Item1.Text = string.Format( TDCultureInfo.InvariantCulture, GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "1") ), selectedRetailer.Name );
				labelList3Item2.Text = GetResource( string.Format(TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "2") );
				labelList3Item3.Text = GetResource( string.Format(TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "3") );
				labelList3Item4.Text = GetResource( string.Format(TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "4") );
				labelList3Item5.Text = GetResource( string.Format(TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "5") );
				labelList3Item6.Text = GetResource( string.Format(TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "6") );
			}
		}

        #endregion Private methods
        
   		#region Web Form Designer generated code
   		
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
            //TDSessionManager.Current.FormShift[ SessionKey.SkipScreenFlow ] = true;

			//Ensure that the tab selection isn't changed because this page has each one of
			//the tab headers and when each one loads, it will overwrite the current one
			TDSessionManager.Current.TabSectionChangeable = false;

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
