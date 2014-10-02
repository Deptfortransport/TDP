// *********************************************** 
// NAME                 : DepartureBoards.aspx.cs
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 07/10/2003
// DESCRIPTION			: Page to display the Departure Boards links.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/LiveTravel/DepartureBoards.aspx.cs-arc  $:
//
//   Rev 1.6   Feb 26 2010 16:14:48   PScott
//Meta tag and title changes on numerous pages
//RS71001 
//SCR 5408
//Resolution for 5408: Meta tags
//
//   Rev 1.5   Jan 09 2009 15:25:36   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Jul 24 2008 10:44:36   apatel
//Removed External Links tooltip and added (opens new window) text to the links
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.3   Jun 26 2008 14:04:32   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:25:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.3   Nov 29 2007 14:07:48   mturner
//Removed redundant variable declarations
//
//Rev DevFatory Feb 20th 13:40:00 dgath
//Some lines in LoadResources() and SetupHyperlinks() methods commented out due to removal of 
//menu on this page which will now be incorporated into the left hand menu as a submenu of this page
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.2   Nov 29 2007 13:07:54   mturner
//Declared as partial class to make Del 9.8 cde .Net2 compliant
//
//   Rev 1.1   Nov 29 2007 11:44:40   build
//Updated for Del 9.8
//
//   Rev 1.8   Nov 06 2007 14:16:54   mmodi
//Only display airport if airport name is available
//Resolution for 4528: Departure Boards: Airport name not being displayed
//
//   Rev 1.7   Oct 26 2007 15:07:10   mmodi
//Corrected error when no airport links are returned
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.6   Oct 25 2007 16:08:18   mmodi
//Removed CMS content and retrieve departure boards from database
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.5   Feb 23 2006 17:56:30   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.4   Feb 10 2006 15:08:44   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.0   Dec 06 2005 11:10:48   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.3   Nov 18 2003 11:59:04   asinclair
//Added comments

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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.LocationInformationService;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for DepartureBoards.
	/// </summary>
	public partial class DepartureBoards : TDPage
	{
		#region Controls
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		#region Labels
		protected System.Web.UI.WebControls.Label labelDepartureBoardsTitle;
		protected System.Web.UI.WebControls.Label labelTrainTitle;
		protected System.Web.UI.WebControls.Label labelLondonTitle;
		protected System.Web.UI.WebControls.Label labelAirportTitle;
		protected System.Web.UI.WebControls.Label labelBusTitle;

		protected System.Web.UI.WebControls.Label labelAirportSummary;

		protected System.Web.UI.WebControls.Label labelHyperlinkTrain;
		protected System.Web.UI.WebControls.Label labelHyperlinkLondon;
		protected System.Web.UI.WebControls.Label labelHyperlinkAirport;
		protected System.Web.UI.WebControls.Label labelHyperlinkBus;

		protected System.Web.UI.WebControls.Label labelHyperlinkTrainTop;
		protected System.Web.UI.WebControls.Label labelHyperlinkLondonTop;
		protected System.Web.UI.WebControls.Label labelHyperlinkAirportTop;
		protected System.Web.UI.WebControls.Label labelHyperlinkBusTop;
		#endregion

		protected System.Web.UI.WebControls.Panel panelTrain;
		protected System.Web.UI.WebControls.Panel panelLondon;
		protected System.Web.UI.WebControls.Panel panelAirport;
		protected System.Web.UI.WebControls.Panel panelBus;

		#region Links
		protected System.Web.UI.WebControls.HyperLink hyperlinkTrain;
		protected System.Web.UI.WebControls.HyperLink hyperlinkLondon;
		protected System.Web.UI.WebControls.HyperLink hyperlinkAirport;
		protected System.Web.UI.WebControls.HyperLink hyperlinkBus;

		protected System.Web.UI.WebControls.HyperLink hyperlinkTrainTop;
		protected System.Web.UI.WebControls.HyperLink hyperlinkLondonTop;
		protected System.Web.UI.WebControls.HyperLink hyperlinkAirportTop;
		protected System.Web.UI.WebControls.HyperLink hyperlinkBusTop;
		#endregion

		protected System.Web.UI.WebControls.Image imageHyperlinkTrainTop;
		protected System.Web.UI.WebControls.Image imageHyperlinkLondonTop;
		protected System.Web.UI.WebControls.Image imageHyperlinkAirportTop;
		protected System.Web.UI.WebControls.Image imageHyperlinkBusTop;

		protected System.Web.UI.WebControls.DataList datalistAirportLinks;
		protected System.Web.UI.WebControls.Repeater repeaterTrainLinks;
		protected System.Web.UI.WebControls.Repeater repeaterLondonLinks;
		protected System.Web.UI.WebControls.Repeater repeaterBusLinks;

		#endregion

		#region Struct
		/// <summary>
		/// Used to hold a departure board link for use in a repeater
		/// </summary>
		public struct DepartureBoardLink
		{
			public string url, urlText, title, description;

			public DepartureBoardLink(string url, string urlText, string title, string description)
			{
				this.url = url;
				this.urlText = urlText;
				this.title = title;
				this.description = description;
			}
		}
		#endregion
		
		// While we don't have a fully database driven departure boards page, we need to specify
		// the external link id's to include. Ensure there is a Resource string entry for each link
		// if text is to be displayed alongside, in name format "externallinkid.Title", and 
		// "externallinkid.Description"
		string[] stringTrainLinks = new string[1]{"DepartureBoard.Train.1"};

		string[] stringLondonLinks = new string[8]{"DepartureBoard.London.1", "DepartureBoard.London.2", 
													  "DepartureBoard.London.3", "DepartureBoard.London.4", "DepartureBoard.London.5",
													  "DepartureBoard.London.6", "DepartureBoard.London.7", "DepartureBoard.London.8"};

		string[] stringBusLinks = new string[5]{"DepartureBoard.Bus.1", "DepartureBoard.Bus.2", "DepartureBoard.Bus.3", 
														"DepartureBoard.Bus.4", "DepartureBoard.Bus.5"};
	
		#region Constructor
		public DepartureBoards() : base()
		{
			pageId = PageId.DepartureBoards;
		}
		#endregion
		
		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
            PageTitle = GetResource("DepartureBoards.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");	

			LoadResources();

			LoadTrainLinks();
			LoadLondonLinks();
			LoadAirportLinks();
			LoadBusLinks();

			SetupHyperlinks();

            //Added for white labelling:
            ConfigureLeftMenu("DepartureBoards.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLiveTravel);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextDepartureBoards);
            expandableMenuControl.AddExpandedCategory("Related links");

		}
		#endregion

		#region Private methods

		/// <summary>
		/// Loads the page text using resource strings
		/// </summary>
		private void LoadResources()
		{
            imageDepartureBoards.ImageUrl = GetResource("HomeTravelInfo.imageDepartureBoards.ImageUrl");
            imageDepartureBoards.AlternateText = GetResource("HomeTravelInfo.imageDepartureBoards.AlternateText");

			labelDepartureBoardsTitle.Text = GetResource("DepartureBoards.DepartureBoardsTitle.Text");
			
			// Section headings
			labelTrainTitle.Text = GetResource("DepartureBoards.TrainTitle.Text");
			labelLondonTitle.Text = GetResource("DepartureBoards.LondonTitle.Text");
			labelAirportTitle.Text = GetResource("DepartureBoards.AirportTitle.Text");
			labelBusTitle.Text = GetResource("DepartureBoards.BusTitle.Text");

			labelAirportSummary.Text = GetResource("DepartureBoards.AirportSummary.Text");

			// Left hand navigation links
			/*labelHyperlinkTrain.Text = GetResource("DepartureBoards.HyperlinkTrain.Text");
			labelHyperlinkLondon.Text = GetResource("DepartureBoards.HyperlinkLondon.Text");
			labelHyperlinkAirport.Text = GetResource("DepartureBoards.HyperlinkAirport.Text");
			labelHyperlinkBus.Text = GetResource("DepartureBoards.HyperlinkBus.Text");*/

			// Top of page links and images
			labelHyperlinkTrainTop.Text = GetResource("DepartureBoards.TopOfPage.Text");
			labelHyperlinkLondonTop.Text = GetResource("DepartureBoards.TopOfPage.Text");
			labelHyperlinkAirportTop.Text = GetResource("DepartureBoards.TopOfPage.Text");
			labelHyperlinkBusTop.Text = GetResource("DepartureBoards.TopOfPage.Text");

			imageHyperlinkTrainTop.ImageUrl = GetResource("DepartureBoards.TopOfPage.HyperlinkImage");
			imageHyperlinkLondonTop.ImageUrl = GetResource("DepartureBoards.TopOfPage.HyperlinkImage");
			imageHyperlinkAirportTop.ImageUrl = GetResource("DepartureBoards.TopOfPage.HyperlinkImage");
			imageHyperlinkBusTop.ImageUrl = GetResource("DepartureBoards.TopOfPage.HyperlinkImage");

            imageHyperlinkTrainTop.AlternateText = GetResource("DepartureBoards.TopOfPage.HyperlinkImage.AlternateText");
            imageHyperlinkLondonTop.AlternateText = GetResource("DepartureBoards.TopOfPage.HyperlinkImage.AlternateText");
            imageHyperlinkAirportTop.AlternateText = GetResource("DepartureBoards.TopOfPage.HyperlinkImage.AlternateText");
            imageHyperlinkBusTop.AlternateText = GetResource("DepartureBoards.TopOfPage.HyperlinkImage.AlternateText");

		}

		/// <summary>
		/// Sets up the page navigation hyperlinks
		/// </summary>
		private void SetupHyperlinks()
		{
			/*hyperlinkTrain.NavigateUrl = "#train";
			hyperlinkLondon.NavigateUrl = "#london";
			hyperlinkAirport.NavigateUrl = "#airport";
			hyperlinkBus.NavigateUrl = "#bus";*/

			hyperlinkTrainTop.NavigateUrl = "#pagetop";
			hyperlinkLondonTop.NavigateUrl = "#pagetop";
			hyperlinkAirportTop.NavigateUrl = "#pagetop";
			hyperlinkBusTop.NavigateUrl = "#pagetop";
	
			// No need to display link if panel is hidden
			/*hyperlinkTrain.Visible = panelTrain.Visible;
			hyperlinkLondon.Visible = panelLondon.Visible;
			hyperlinkAirport.Visible = panelAirport.Visible;
			hyperlinkBus.Visible = panelBus.Visible;*/
		}

		#endregion

		#region Load Links
		/// <summary>
		/// Loads the Train departure/arrival board links
		/// </summary>
		private void LoadTrainLinks()
		{
			// Get Links
			ArrayList trainLinks = new ArrayList();
			foreach (string s in stringTrainLinks)
			{
				ExternalLinkDetail link = ExternalLinks.Current[s];
				if (link != null)
				{
					DepartureBoardLink departureBoardLink = new DepartureBoardLink(
						link.Url,
						link.LinkText,
						GetResource( s + ".Title" ),
						GetResource( s + ".Description") );
					trainLinks.Add(departureBoardLink);
				}
			}

			// Only show panel if links available
			if (trainLinks.Count > 0)
			{
				repeaterTrainLinks.DataSource = trainLinks.ToArray();
				repeaterTrainLinks.DataBind();
			}
			else
				panelTrain.Visible = false;
		}

		/// <summary>
		/// Loads the London departure/arrival board links
		/// </summary>
		private void LoadLondonLinks()
		{
			// Get Links
			ArrayList londonLinks = new ArrayList();
			foreach (string s in stringLondonLinks)
			{
				ExternalLinkDetail link = ExternalLinks.Current[s];
				if (link != null)
				{
					DepartureBoardLink departureBoardLink = new DepartureBoardLink(
						link.Url,
						link.LinkText,
						GetResource( s + ".Title" ),
						GetResource( s + ".Description") );
					londonLinks.Add(departureBoardLink);
				}
			}

			// Only show panel if links available
			if (londonLinks.Count > 0)
			{
				repeaterLondonLinks.DataSource = londonLinks.ToArray();
				repeaterLondonLinks.DataBind();
			}
			else
				panelLondon.Visible = false;
		}

		/// <summary>
		/// Loads the Aiport departure/arrival board links
		/// </summary>
		private void LoadAirportLinks()
		{
			LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];

			LocationInformationService.LocationInformation[] arrayLocationInformation = refData.GetAirportDepartureBoardLinks();
		
			// Used to hold the filtered links
			ArrayList arrayListLinks = new ArrayList();

			if (arrayLocationInformation != null)
			{
				#region Filter and sort airport links
				// Modify items to ensure we output the required links for departures AND arrival boards				
				foreach (LocationInformationService.LocationInformation li in arrayLocationInformation)
				{
					// If we have both a Departure and Arrival link, then need to append appropriate text on to the display name.					
					if ((li.DepartureLink != null) && (li.ArrivalLink != null))
					{
						// Only display the link if it has an Airport name associated. Prevents displaying "(departures)" only on the page
						if (li.DepartureLink.LinkText.Length > 0)
						{
							ExternalLinkDetail departureLink = li.DepartureLink;
							// Ensure we only add the "departures" text once
							if (departureLink.LinkText.IndexOf(GetResource("DepartureBoards.Airport.Departures.Text")) < 0)
								departureLink.LinkText += " " + GetResource("DepartureBoards.Airport.Departures.Text");

							arrayListLinks.Add(departureLink);
						}

						if (li.ArrivalLink.LinkText.Length > 0)
						{
							// Ensure we only add the "arrivals" text once
							ExternalLinkDetail arrivalLink = li.ArrivalLink;
							if (arrivalLink.LinkText.IndexOf(GetResource("DepartureBoards.Airport.Arrivals.Text")) < 0)
								arrivalLink.LinkText += " " + GetResource("DepartureBoards.Airport.Arrivals.Text");

							arrayListLinks.Add(arrivalLink);
						}
					}
						// Otherwise add the link to the array as it is, and only if it has an Airport name to display
					else if ((li.DepartureLink != null) && (li.DepartureLink.LinkText.Length > 0))
					{
						ExternalLinkDetail departureLink = li.DepartureLink;
						arrayListLinks.Add(departureLink);
					}
					else if ((li.ArrivalLink != null) && (li.ArrivalLink.LinkText.Length > 0))
					{
						ExternalLinkDetail arrivalLink = li.ArrivalLink;
						arrayListLinks.Add(arrivalLink);
					}
				}

				// Sort links array in to alphabetical order
				arrayListLinks.Sort();
				#endregion
			}

			// Only display airport departure boards if we have links from the filter
			if (arrayListLinks.Count > 0)
			{
				datalistAirportLinks.DataSource = arrayListLinks.ToArray();
				datalistAirportLinks.DataBind();
			}
			else 
				panelAirport.Visible = false;
		}

		/// <summary>
		/// Loads the Bus departure/arrival board links
		/// </summary>
		private void LoadBusLinks()
		{
			// Get Links
			ArrayList busLinks = new ArrayList();
			foreach (string s in stringBusLinks)
			{
				ExternalLinkDetail link = ExternalLinks.Current[s];
				if (link != null)
				{
					DepartureBoardLink departureBoardLink = new DepartureBoardLink(
						link.Url,
						link.LinkText,
						GetResource( s + ".Title" ),
						GetResource( s + ".Description") );
					busLinks.Add(departureBoardLink);
				}
			}

			// Only show panel if links available
			if (busLinks.Count > 0)
			{
				repeaterBusLinks.DataSource = busLinks.ToArray();
				repeaterBusLinks.DataBind();
			}
			else
				panelBus.Visible = false;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Gets the airport link text
		/// </summary>
		/// <param name="containerDataItem"></param>
		public string GetAirportLinkText(object containerDataItem)
		{
			ExternalLinkDetail item = (ExternalLinkDetail)containerDataItem;

            StringBuilder sb = new StringBuilder();
            sb.Append("<a href=\"");
            sb.Append(Server.HtmlEncode(item.Url));
            sb.Append("\" target=\"_blank\" >");
            sb.Append(string.Format("{0} {1}", item.LinkText, GetResource("ExternalLinks.OpensNewWindowText")));
            sb.Append("</a>");

			return sb.ToString();
		}

		/// <summary>
		/// Gets the link title
		/// </summary>
		/// <param name="containerDataItem"></param>
		public string GetLinkTitle(object containerDataItem)
		{
			DepartureBoardLink item = (DepartureBoardLink)containerDataItem;
			
			StringBuilder sb = new StringBuilder();
			sb.Append( item.title );

			return sb.ToString();
		}

		/// <summary>
		/// Gets the link url
		/// </summary>
		/// <param name="containerDataItem"></param>
		public string GetLinkUrl(object containerDataItem)
		{
			DepartureBoardLink item = (DepartureBoardLink)containerDataItem;

            StringBuilder sb = new StringBuilder();
            sb.Append("<a href=\"");
            sb.Append(item.url);
            sb.Append("\" target=\"_blank\" >");
            sb.Append(string.Format("{0} {1}", item.urlText, GetResource("ExternalLinks.OpensNewWindowText")));
            sb.Append("</a>");

			return sb.ToString();
		}

		/// <summary>
		/// Gets the link description
		/// </summary>
		/// <param name="containerDataItem"></param>
		public string GetLinkDescription(object containerDataItem)
		{
			DepartureBoardLink item = (DepartureBoardLink)containerDataItem;
			
			StringBuilder sb = new StringBuilder();
			sb.Append( item.description );

			return sb.ToString();
		}
		#endregion

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
