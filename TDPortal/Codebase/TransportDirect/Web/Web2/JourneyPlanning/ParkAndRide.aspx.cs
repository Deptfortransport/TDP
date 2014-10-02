// *********************************************** 
// NAME                 : ParkAndRide.aspx
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 21/07/2005 
// DESCRIPTION			: Displays Park and Ride Schemes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ParkAndRide.aspx.cs-arc  $ 
//
//   Rev 1.6   Feb 26 2010 16:14:44   PScott
//Meta tag and title changes on numerous pages
//RS71001 
//SCR 5408
//Resolution for 5408: Meta tags
//
//   Rev 1.5   Apr 24 2009 17:05:58   mmodi
//Added bookmark page link
//Resolution for 4720: Park and Ride schemes Page
//
//   Rev 1.4   Dec 17 2008 11:27:54   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 11 2008 11:58:24   mmodi
//Added icon
//Resolution for 4653: Firefox-Park and ride schemes
//
//   Rev 1.2   Mar 31 2008 13:25:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:24   mturner
//Initial revision.
//
//   Rev 1.6   Mar 23 2006 17:54:22   build
//Automatically merged from branch for stream0025
//
//   Rev 1.5.1.0   Mar 16 2006 16:10:56   halkatib
//Removed postback check on setregionspecifics to execute at every page load
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.5   Feb 23 2006 18:56:40   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.4   Feb 10 2006 15:09:18   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.2   Dec 14 2005 17:39:24   RGriffith
//Changes to use the ExpandableMenu control
//
//   Rev 1.3.1.1   Dec 08 2005 16:43:40   RGriffith
//Removal of comments
//
//   Rev 1.3.1.0   Dec 05 2005 16:06:04   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.3   Nov 03 2005 16:03:18   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.2.1.0   Oct 14 2005 11:32:42   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.2   Aug 25 2005 16:29:50   NMoorhouse
//Adding of suggestion link control
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 23 2005 10:19:44   NMoorhouse
//Park And Ride - Updated following UI review comments (fix problem with selecting the same region from the drop down a second time)
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 12 2005 13:19:22   NMoorhouse
//Initial revision.
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SuggestionLinkService;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for ParkAndRide.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ParkAndRide : TDPage
	{
		#region Instance variables
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.MapRegionSelectControl regionSelector;
		protected TransportDirect.UserPortal.Web.Controls.ParkAndRideTableControl parkAndRideTable;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
        //protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
        protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;
		ParkAndRideInfo[] parkAndRideData;
		IParkAndRideCatalogue parkAndRideLocations = (IParkAndRideCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.ParkAndRideCatalogue];
		#endregion

		#region Const declarations
		private const string RES_LEFTHEADING = "ParkAndRide.labelLeftHeading.Text";
		private const string RES_PAGEHEADING = "ParkAndRide.labelParkAndRideHeading.Text";
		private const string RES_PAGESUBHEADING = "ParkAndRide.labelParkAndRideSubheading.Text";
		private const string RES_HELPIMAGEURL = "HelpControl.Icon.ImageUrl";
		private const string RES_HELPURL = "ParkAndRide.UrlHelpParkAndRide";
		private const string RES_HELPALTTEXT = "ParkAndRide.AltTextHelpParkAndRide";
		private const string RES_PRINTABLEURL = "ParkAndRide.UrlPrintableParkAndRide";
		private const string RES_ALLREGIONID = "0";
        private const string RES_HEADINGIMAGEURL = "HomePlanAJourney.imageParkAndRide.ImageUrl";
        private const string RES_HEADINGIMAGEALTTEXT = "HomePlanAJourney.imageParkAndRide.AlternateText";
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public ParkAndRide() : base()
		{
			pageId = PageId.ParkAndRide;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Exposes the Print button control that links to a printer-friendly page.
		/// The Page ID of the printer-friendly page is the current page ID prefixed with "Printable".
		/// If no printer-friendly page is available then the Print button will not be shown.
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl PrinterFriendlyPageButton
		{
			get { return printerFriendlyPageButton; }
			set { printerFriendlyPageButton = value; }
		}
		#endregion

		#region Page event handlers
		/// <summary>
		/// Sets up session data, static page data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            PageTitle = GetResource("ParkAndRideSchemes.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");	

			//Set static labels, links and alternate text
			labelParkAndRideHeading.Text = GetResource(RES_PAGEHEADING);
			//labelLeftHeading.Text = GetResource(RES_LEFTHEADING);
			labelParkAndRideSubheading.Text = GetResource(RES_PAGESUBHEADING);

            imageFindPage.ImageUrl = GetResource(RES_HEADINGIMAGEURL);
            imageFindPage.AlternateText = GetResource(RES_HEADINGIMAGEALTTEXT);
			
			imageButtonHelp.AlternateText = GetResource(RES_HELPALTTEXT);

			//Tell user control that it's on a non-printerfriendly page
			parkAndRideTable.PrinterFriendly = false;

            //Left hand navigation menu set up
            //Added for white labelling:
            ConfigureLeftMenu("ParkAndRideInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextParkAndRide);
            expandableMenuControl.AddExpandedCategory("Related links");


			//Set up Park And Ride data on first entry to the screen
			SetRegionSpecifics();

		}

		/// <summary>
		/// PreRender event handler.
		/// Updates Park and Ride Data. This may have changed since Page Load if user has 
		/// changed selection.
		/// </summary>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			//Update Park and Ride Data
			SetRegionSpecifics();
		}

		/// <summary>
		/// Handles help button click event
		/// Puts this page on the stack then redirects to the Park and Ride help page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void imageButtonHelp_Click(object sender, EventArgs e)
		{
			string helpUrl = getBaseChannelURL(TDPage.SessionChannelName)
				+ resourceManager.GetString(RES_HELPURL);

			InputPageState pageState = TDSessionManager.Current.InputPageState;
			TDPage page = this.Page as TDPage;

			pageState.JourneyInputReturnStack.Push(page.PageId);
			// Need to ensure all data is properly saved away before exiting the page
			// This is needed because this redirection does not use the ScreenFlow framework.
			TDSessionManager.Current.OnPreUnload();
			page.Response.Redirect(helpUrl);

		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
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

		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.imageButtonHelp.Click += new EventHandler(this.imageButtonHelp_Click);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method used to set up the Regional specifics for the page
		/// </summary>
		private void SetRegionSpecifics()
		{
			if (regionSelector.SelectedRegionId == null)
			{
				parkAndRideData = parkAndRideLocations.GetAll();
			}
			else
			{
				string regionId = regionSelector.SelectedRegionId.Trim();
				if (regionId == RES_ALLREGIONID || regionId.Length == 0)
				{
					parkAndRideData = parkAndRideLocations.GetAll();
				}
				else
				{
					parkAndRideData = parkAndRideLocations.GetRegion(regionId);
					setPrinterFriendlyParameters(regionId);
				}
			}
			parkAndRideTable.Data = parkAndRideData;
		}

		/// <summary>
		/// Sets the printerfriendly parameters for specific regions
		/// </summary>
		/// <param name="regionId"></param>
		private void setPrinterFriendlyParameters(string regionId)
		{
			printerFriendlyPageButton.UrlParams = "region="+ regionId;
		}
		#endregion
	}
}
