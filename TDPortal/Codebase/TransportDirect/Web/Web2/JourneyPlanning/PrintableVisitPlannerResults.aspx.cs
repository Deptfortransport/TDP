// *********************************************** 
// NAME				 : PrintableVisitPlannerResults.aspx.cs
// AUTHOR			 : James Broome
// DATE CREATED		 : 12 October 2005
// DESCRIPTION		 : Printable Journey Results Page for VisitPlanner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableVisitPlannerResults.aspx.cs-arc  $ 
//
//   Rev 1.7   Feb 16 2010 11:16:16   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 12 2010 11:14:08   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Jan 18 2010 12:16:58   mmodi
//Add an auto refresh page if map image url not detected in session
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.4   Nov 23 2009 10:34:02   mmodi
//Updated styles
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Dec 17 2008 11:27:56   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:10   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 18:27:14   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.5   Feb 22 2006 10:36:26   aviitanen
//Removed references to the logo and strapline images.
//
//   Rev 1.4   Feb 10 2006 15:09:22   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.0   Dec 01 2005 12:03:02   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.3   Nov 18 2005 16:48:26   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.2   Oct 28 2005 17:12:54   tolomolaiye
//Updates following code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 15 2005 14:48:56   jbroome
//Completed formatting and functionality of page
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Oct 13 2005 19:17:32   jbroome
//Initial revision.
//Resolution for 2638: DEL 8 Stream: Visit Planner

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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{

	/// <summary>
	/// Page displays printer friendly view of Visit Planner Results page
	/// </summary>
	public partial class PrintableVisitPlannerResults : TDPrintablePage, INewWindowPage
	{
		protected TransportDirect.UserPortal.Web.Controls.VisitPlannerRequestDetailsControl visitPlannerRequestDetailsControl;

		protected System.Web.UI.WebControls.Image imageLogo;
		protected System.Web.UI.WebControls.Image imageStrapline;

		protected TransportDirect.UserPortal.Web.Controls.RouteSelectionControl routeSelectionJourney1;
		protected TransportDirect.UserPortal.Web.Controls.RouteSelectionControl routeSelectionJourney2;
		protected TransportDirect.UserPortal.Web.Controls.RouteSelectionControl routeSelectionJourney3;


		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsControl journeyDetailsControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableControl journeyDetailsTableControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyMapControl journeyMapControl;
		protected TransportDirect.UserPortal.Web.Controls.PrintableMapControl mapControl;


		// Adapter is used to populate the controls used
		private VisitPlannerAdapter visitPlanner = new VisitPlannerAdapter();

		/// <summary>
		/// Constructor sets local resource manager
		/// </summary>
		public PrintableVisitPlannerResults() : base()
		{
			LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
			pageId = PageId.PrintableVisitPlannerResults;
		}

		/// <summary>
		/// Page load method - sets up resource strings
		/// and calls methods to show/populate controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Set up page labels etc		

			PageTitle = GetResource("PrintableVisitPlannerResults.DefaultPageTitle");
			labelPrinterFriendly.Text= GetResource("PrintableVisitPlannerResults.labelPrinterFriendly");
			labelInstructions.Text = GetResource("PrintableVisitPlannerResults.labelInstructions");

			labelDetails.Text = GetResource("PrintableVisitPlannerResults.labelDetails");
			labelMap.Text = GetResource("PrintableVisitPlannerResults.labelMap");

			labelDateTitle.Text = GetResource("PrintableVisitPlannerResults.labelDateTitle");
            labelDate.Text = DisplayFormatAdapter.StandardDateAndTimeFormat(TDDateTime.Now);
			
			labelUsername.Visible =TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			
			// If user logged in, show username labels
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
				labelUsernameTitle.Text = GetResource("PrintableVisitPlannerResults.labelUsernameTitle");
			}

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDJourneyParametersVisitPlan parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
			
			// Hide all controls to begin with
			HideControls();

			// Populate controls based on ResultsMode
			visitPlanner.PopulateVisitPlannerRequestDetailsControl(visitPlannerRequestDetailsControl, parameters);
			visitPlannerRequestDetailsControl.ShowJourneyDetails();
            visitPlannerRequestDetailsControl.AmendButtonVisible = false;
			
			switch (sessionManager.ResultsPageState.ResultsMode)
			{
				case ResultsModes.Summary:
					ShowAndPopulateRouteSelectionControls(parameters.ReturnToOrigin);
					break;

				case ResultsModes.SchematicDetailsView:
					ShowAndPopulateJourneyDetailsControl();
					break;

				case ResultsModes.TabularDetailsView:
					ShowAndPopulateJourneyDetailsTableControl();
					break;

				case ResultsModes.MapView:
					ShowAndPopulateMapControls();
                    SetupRefreshPage();
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Method populates and shows RouteSelectionControls when in summary view
		/// </summary>
		/// <param name="returnToOrigin">bool indicates if is return to origin journey</param>
		private void ShowAndPopulateRouteSelectionControls(bool returnToOrigin)
		{
			VisitPlannerItineraryManager itineraryManager = (VisitPlannerItineraryManager)TDSessionManager.Current.ItineraryManager;

			visitPlanner.PopulateRouteSelectionControl(routeSelectionJourney1, itineraryManager, 0);
			visitPlanner.PopulateRouteSelectionControl(routeSelectionJourney2, itineraryManager, 1);

			rowJourney1.Visible = true;
			rowJourney2.Visible = true;

			labelJourneyDetails1.Text = GetResource("PrintableVisitPlannerResults.labelJourneyDetails1");
			labelJourneyDetails2.Text = GetResource("PrintableVisitPlannerResults.labelJourneyDetails2");

			// If returning to origin, need to poulate and show final control
			if (returnToOrigin)
			{
				visitPlanner.PopulateRouteSelectionControl(routeSelectionJourney3, itineraryManager, 2);
				rowJourney3.Visible = true;
				labelJourneyDetails3.Text = GetResource("PrintableVisitPlannerResults.labelJourneyDetails3");
			}
		}

		/// <summary>
		/// Hides all controls and their parent table rows
		/// </summary>
		private void HideControls()
		{
			rowJourney1.Visible = false;
			rowJourney2.Visible = false;
			rowJourney3.Visible = false;

			rowDetails.Visible = false;
			rowMap.Visible = false;
			journeyDetailsControl.Visible = false;
			journeyDetailsTableControl.Visible = false;
		}

		/// <summary>
		/// Show and populate the journey details control
		/// </summary>
		private void ShowAndPopulateJourneyDetailsControl()
		{
			journeyDetailsControl.Initialise(true, false, false, false, TDSessionManager.Current.FindAMode);
			journeyDetailsControl.MyPageId = pageId;
			journeyDetailsControl.Printable = true;
			rowDetails.Visible = true;
			journeyDetailsControl.Visible = true;
		}

		/// <summary>
		/// Show and populate the JourneyDetailsTableControl control
		/// </summary>
		private void ShowAndPopulateJourneyDetailsTableControl()
		{
            journeyDetailsTableControl.Initialise(true, TDSessionManager.Current.FindAMode);
			journeyDetailsTableControl.MyPageId = pageId;
			rowDetails.Visible = true;
			journeyDetailsTableControl.Visible = true;
		}

		/// <summary>
		/// Show and populate the map control
		/// </summary>
		private void ShowAndPopulateMapControls()
		{
            rowMap.Visible = true;
			mapControl.Populate(true, false, false);
		}

        /// <summary>
        /// Method which checks if page needs to be automatically refreshed, 
        /// e.g. when the map image url is missing
        /// </summary>
        private void SetupRefreshPage()
        {
            #region Refresh page if map image not found

            // Add a refresh to itself if no map image url is found, because the session
            // could still be updating (printable map url is passed by the client browser through the 
            // TD web service).
            if (string.IsNullOrEmpty(TDSessionManager.Current.InputPageState.MapUrlOutward))
            {
                // Check if refresh attempted before
                string refreshAttempt = (string)Request.QueryString["refresh"];

                // Get the URL to redirect to
                string url = Request.RawUrl;

                // First refresh attempt
                if (string.IsNullOrEmpty(refreshAttempt))
                {
                    url += "&refresh=1";

                    headElementControl.AddAutoRedirect(1, url);
                }
                else
                {
                    try
                    {
                        // Check attempt number
                        int attemptNumber = Convert.ToInt32(refreshAttempt);

                        // Update url
                        url = url.Replace("&refresh=" + attemptNumber, "&refresh=" + (attemptNumber + 1));

                        if ((attemptNumber >= 0) && (attemptNumber <= 5))
                        {
                            headElementControl.AddAutoRedirect(1, url);
                        }
                    }
                    catch
                    {
                        // Any exceptions, then someone may have tampered with the refresh value, 
                        // so cancel and dont add the refresh
                    }
                }
            }
            #endregion
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
