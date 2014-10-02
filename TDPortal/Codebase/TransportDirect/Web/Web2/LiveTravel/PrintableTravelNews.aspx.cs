// ***********************************************
// NAME 		: PrintableTravelNews.aspx.cs
// AUTHOR 		: Joe Morrissey
// DATE CREATED : 16/10/2003
// DESCRIPTION 	: Printable version of the TravelNews web page
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/LiveTravel/PrintableTravelNews.aspx.cs-arc  $
//
//   Rev 1.6   Dec 11 2009 14:53:48   apatel
//Mapping enhancement for travelnews
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Dec 19 2008 15:49:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   May 12 2008 12:19:34   PScott
//SCR 4973 - add footer to the bottom of printable screen
//
//   Rev 1.3   Apr 18 2008 16:47:26   mmodi
//Added code to display single travel news incident
//Resolution for 4881: Del 10 - Printable Travel news issues
//
//   Rev 1.2   Mar 31 2008 13:25:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:08   mturner
//Initial revision.
//
//   Rev 1.22   Mar 28 2006 11:09:08   build
//Automatically merged from branch for stream0024
//
//   Rev 1.21.1.0   Mar 03 2006 16:26:10   AViitanen
//Removed lblDetailsDrop and lblDetailsDropValue. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.21   Feb 23 2006 19:03:30   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.20   Feb 21 2006 11:38:24   aviitanen
//Merge from stream0009 
//
//   Rev 1.19   Feb 10 2006 15:09:22   build
//Automatically merged from branch for stream3180
//
//   Rev 1.18.1.0   Dec 01 2005 12:03:06   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.18   Nov 18 2005 16:48:06   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.17   Sep 23 2005 11:43:36   jbroome
//Added map view
//Resolution for 2793: Travel News Printable Map View
//
//   Rev 1.16   Aug 18 2005 11:29:18   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.15.1.2   Aug 04 2005 11:28:42   jgeorge
//Minor updates for incident mapping
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.15.1.1   Jul 08 2005 14:57:40   jbroome
//Interim check in - work in progress
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.15.1.0   Jul 01 2005 10:11:28   jmorrissey
//Now uses new TravelNewsDetailsControl
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.15   Dec 16 2004 15:23:38   passuied
//refactoring the TravelNews component and changes to the clients
//
//   Rev 1.14   Sep 06 2004 21:09:26   JHaydock
//Major update to travel news
//
//   Rev 1.13   Jul 20 2004 09:26:58   CHosegood
//Added recent delays combo box option and removed sort by ability on travelnews
//Resolution for 1168: Add 'recent delays' pulldown to travel news and remove the ability to sort headings
//
//   Rev 1.12   May 26 2004 10:21:02   jgeorge
//IR954 fix
//
//   Rev 1.11   Mar 31 2004 09:57:06   AWindley
//DEL5.2 QA Changes: Resolution for 684
//
//   Rev 1.10   Mar 17 2004 14:23:18   CHosegood
//No change.
//
//   Rev 1.9   Mar 12 2004 14:02:48   rgreenwood
//Updated so when no travel news is available only a message is displayed
//
//   Rev 1.8   Mar 01 2004 14:41:36   asinclair
//Added Print instructions for Del 5.2
//
//   Rev 1.7   Nov 17 2003 17:29:36   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.6   Oct 30 2003 18:47:48   JMorrissey
//Updated to provide public properties to hold a logged in user's preferences
//
//   Rev 1.5   Oct 22 2003 18:32:24   JMorrissey
//updated after PVCS comments
//
//   Rev 1.4   Oct 22 2003 16:12:08   JMorrissey
//updated namespace to TransportDirect.UserPortal.Web.Templates
//
//   Rev 1.3   Oct 22 2003 13:22:20   JMorrissey
//fixed lblDetailsValue bug which was displaying the Delays value

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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;

using TransportDirect.UserPortal.TravelNewsInterface;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableTravelNews.
	/// </summary>
	public partial class PrintableTravelNews : TDPrintablePage, INewWindowPage
	{
		public static readonly string ZERO_MINUTES = "00";
		public static readonly int MINUTES = 10;


		protected TransportDirect.UserPortal.Web.Controls.TravelNewsDetailsControl TravelNewsDetails;

		private TravelNewsState travelNewsState;
		private TravelNewsHandler travelNewsHandler;

		/// <summary>
		/// default constructor
		/// </summary>
		public PrintableTravelNews()
		{
			pageId = PageId.PrintableTravelNews;
		}

		/// <summary>
		/// retrieves travel data to display from the session Manager
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			travelNewsState = TDSessionManager.Current.TravelNewsState;
			travelNewsHandler = (TravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];

			// Get printer friendly text
			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");

			//display correct time and date
            // CCN 0421 Removing the date displaying in the "Specific News Displayed:" box
            // and showing the selected date below the title instead of today's date
            lblDateTime.Text = DisplayFormatAdapter.StandardDateFormat(travelNewsState.SelectedDate);
            labelDate.Text = TDDateTime.Now.ToString("G");
            labelDateTitle.Visible = true;
            labelDate.Visible = true;

            labelUsername.Visible = TDSessionManager.Current.Authenticated;
            labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;

            if (TDSessionManager.Current.Authenticated)
            {
                labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
            }








            if (travelNewsHandler.IsTravelNewsAvaliable)
			{
				//Hide labelNoTravelNews
				labelNoTravelNews.Visible = false;

				//retrieve values selected by user from drop downs from the Session Manager
				lblTransportDropValue.Text = GetResource("DataServices.NewsTransportDrop." + Converting.ToString(travelNewsState.SelectedTransport));
				lblRegionDropValue.Text = GetResource("DataServices.NewsRegionDrop." + travelNewsState.SelectedRegion);
				lblDelaysDropValue.Text = GetResource("DataServices.NewsShowTypeDrop." + Converting.ToString(travelNewsState.SelectedDelays));
                lblTypeDropValue.Text = GetResource("DataServices.NewsIncidentTypeDrop." + Converting.ToString(travelNewsState.SelectedIncidentType));
                // CCN 0421 extra Date displaying in the "Specific News Displayed" removed
                //lblDateValue.Text = DisplayFormatAdapter.StandardDateFormat(travelNewsState.SelectedDate);

				// Show correct display according to Travel News State
				if (travelNewsState.SelectedView == TravelNewsViewType.Details)
				{
					// Show and populate the travel news details control
					TravelNewsDetails.Visible = true;
					PanelMapControls.Visible = false;
					TravelNewsDetails.PrinterFriendly = true;
					PopulateDetailsControl();		
				}
				else
				{
					// Show and set up the map controls
					TravelNewsDetails.Visible = false;
					PanelMapControls.Visible = true;

					// Retrieve and set map image from page state	
					InputPageState inputPageState = TDSessionManager.Current.InputPageState;

					//Get high resolution map image properties for setting image url
					ImageMap.ImageUrl = inputPageState.MapUrlOutward;
					

                    ImageMap.AlternateText = Global.tdResourceManager.GetString("langStrings", "PrintableTravelNews.imageMap.AlternateText");

					
				}
			}
			else
			{
				//Hide unnecessary labels
				lblSpecificNews.Visible = false;
				lblDelaysDrop.Visible = false;
				lblDelaysDropValue.Visible = false;
				lblRegionDrop.Visible = false;
				lblRegionDropValue.Visible = false;
				lblTransportDrop.Visible = false;
				lblTransportDropValue.Visible = false;
				
				

				//hide the travel news details control
				TravelNewsDetails.Visible = false;

				//retrieve travel headlines using the TravelNewsHandler 
				labelNoTravelNews.Text = travelNewsHandler.TravelNewsUnavailableText;
			}
		}

		/// <summary>
		/// Binds the data repeater to the appropriate data
		/// </summary>
		/// <param name="PostBack"></param>
		private void PopulateDetailsControl()
		{
			//update the details control with the correct data
            if (!string.IsNullOrEmpty(travelNewsState.SelectedIncident))
            {
                TravelNewsItem[] newsItem = new TravelNewsItem[1];
                newsItem[0] = travelNewsHandler.GetDetailsByUid(travelNewsState.SelectedIncident);
                TravelNewsDetails.NewsItems = newsItem;
            }
            else
            {
                TravelNewsDetails.NewsItems = travelNewsHandler.GetDetails(travelNewsState);
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
