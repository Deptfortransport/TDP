// *********************************************** 
// NAME                 : PrintableJourneyEmissionsCompareJourney.aspx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/02/2007
// DESCRIPTION			: Printer friendly page to display the CO2 emissions for a Journey 
//						: and public transport modes 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyEmissionsCompareJourney.aspx.cs-arc  $
//
//   Rev 1.4   Jan 15 2009 09:59:28   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:44:32   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Oct 07 2008 12:01:38   mmodi
//Updated to check for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5126: Cycle Planner - "Server Error" is displayed when user clicks on 'Printer Friendly' button on 'Journey Emissions Compare Journey' page
//
//   Rev 1.2   Mar 31 2008 13:25:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:48   mturner
//Initial revision.
//
//   Rev 1.1   Feb 26 2007 11:56:58   mmodi
//Updated when page loaded from Visit planner
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 17:31:36   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
    // <summary>
	/// Summary description for PrintableJourneyEmissionsCompareJourney.
	/// </summary>
	public partial class PrintableJourneyEmissionsCompareJourney : TDPage
	{	
		#region Controls

		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCompareControl journeyEmissionsCompareControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl2 resultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public PrintableJourneyEmissionsCompareJourney()
		{
			this.pageId = PageId.PrintableJourneyEmissionsCompareJourney;
		}

		#endregion

		#region Page_Load

		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties
			PopulateControls();
			PopulateResultsSummaryControl();
			PopulateJourneyEmissionsCompareControl();			
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Page title
			labelTitle.Text = GetResource("JourneyEmissionsCompare.Title");

			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");

			labelDateTime.Text = TDDateTime.Now.ToString("G");
			labelDateTimeTitle.Text = GetResource("PrintableJourneySummary.labelDateTitle");
			labelUsernameTitle.Text = GetResource("PrintableJourneyMapInput.labelUsernameTitle");

			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}
		}
	
		/// <summary>
		/// Initialise the Results Summary Control
		/// </summary>
		private void PopulateResultsSummaryControl()
		{
			VisitPlannerItineraryManager vpim = TDItineraryManager.Current as VisitPlannerItineraryManager;
			if (vpim == null)
			{
				// Summary table title
				PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);

				string labelTextOverride = GetResource("JourneyEmissions.labelTitleControl");			
				plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, true, true);

				// Show the Transport row but not the column
				resultsSummaryControl.ShowTransportColumn = false;
				resultsSummaryControl.ShowTransportRow = true;

				// Do not show the dates on the leave/arrive time columns
				resultsSummaryControl.ShowLeaveArriveDate = false;

				// Use results adpater to populate the control's summary lines
				ResultsAdapter helper = new ResultsAdapter();
				ITDSessionManager sessionManager = TDSessionManager.Current;

                // Need to check where to get the selected journey from, Journey Result, Cycle Result, or Itinerary Manager
                if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.IsValid))
                    || ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.IsValid)))
					resultsSummaryControl.SummaryLines = helper.FormattedSelectedJourneys(sessionManager, FormattedSummaryType.Replan);	
				else
					resultsSummaryControl.SummaryLines = helper.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Replan);	
			}
			else
			{
				panelSummary.Visible = false;				
			}
		}

		/// <summary>
		/// Populates the JourneyEmissionsCompareControl
		/// </summary>
		private void PopulateJourneyEmissionsCompareControl()
		{
			journeyEmissionsCompareControl.NonPrintable = false;

            ITDSessionManager sessionManager = TDSessionManager.Current;

			// Determine where we get selected journey from: Journey Result or Itinerary Manager.
			// The Compare emissions control will then populate distances and calculate emissions
			// from the session
            if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.IsValid))
                || ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.IsValid)))
				journeyEmissionsCompareControl.UseSessionManager = true;
			else
				journeyEmissionsCompareControl.UseItineraryManager = true;

			// Set the control to CompareJourney state so we show the journey emissions
			journeyEmissionsCompareControl.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.JourneyCompare;

			//Sets the Units for Road jounreys on the Printable page
			string UrlQueryString = string.Empty;
			//The Query params is set using javascript on the non-printable page
			UrlQueryString = Request.Params["Units"];
			if (UrlQueryString =="kms")
			{
				journeyEmissionsCompareControl.RoadUnits = RoadUnitsEnum.Kms;
			}
			else
			{
				journeyEmissionsCompareControl.RoadUnits = RoadUnitsEnum.Miles;
			}
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

		}
		#endregion
	}
}
