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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printer friendly version of the Extension Results Summary page.
	/// </summary>
	/// 
	public partial class PrintableExtensionResultsSummary : TDPrintablePage, INewWindowPage
	{

		#region Control Declarations

		// HTML controls

		// Web controls

		// TD custom web controls
		protected TransportDirect.UserPortal.Web.Controls.ExtendJourneyLineControl extendJourneyLineControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl outwardResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl returnResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControlReturn;

		// Footer controls

		#endregion

		#region Private Member Variables
		
		/// <summary>
		/// To hold reference to session manager
		/// </summary>
		ITDSessionManager sessionManager;

		/// <summary>
		/// To hold reference to session ItineraryManager
		/// </summary>
		ExtendItineraryManager itineraryManager;

		/// <summary>
		/// To hold reference to session view state
		/// </summary>
		TDJourneyViewState journeyViewState;
		
		/// <summary>
		/// To hold reference to current search request
		/// </summary>
		ITDJourneyRequest request;

		/// <summary>
		/// To hold reference to current search result
		/// </summary>
		ITDJourneyResult result;

		/// <summary>
		/// Summary lines for outward journey options
		/// </summary>
		FormattedJourneySummaryLines outwardSummaryLines;

		/// <summary>
		/// Summary lines for return journey options
		/// </summary>
		FormattedJourneySummaryLines returnSummaryLines;

		/// <summary>
		///  True if there is a return trip for the current journey
		/// </summary>
		private bool returnJourneySelected = false;

		#endregion

		#region Initialisation

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableExtensionResultsSummary() : base()
		{
			// Set page Id
			pageId = PageId.PrintableExtensionResultsSummary;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Event Handlers
		
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties

			if (!IsPostBack)
			{
				labelPrinterFriendly.Text= GetResource("langStrings", "StaticPrinterFriendly.labelPrinterFriendly");
				labelInstructions.Text = GetResource("langStrings", "StaticPrinterFriendly.labelInstructions");
				labelDate.Text = TDDateTime.Now.ToString("G");
			}

			PopulateControls();

		}

		/// <summary>
		/// Handles page initalise event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, EventArgs e)
		{
			// Set up current session references
			sessionManager = TDSessionManager.Current;
			itineraryManager = (ExtendItineraryManager)TDItineraryManager.Current;
			journeyViewState = sessionManager.JourneyViewState;
			result = sessionManager.JourneyResult;
			request = sessionManager.JourneyRequest;

			// See if we need to handle outward AND return journeys
			returnJourneySelected = (itineraryManager.ReturnLength > 0);

			// Call private methods to populate individual web controls
			InitialiseJourneyLineControl();
			InitialiseResultsSummaryControls();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Get correct resource strings for labels
			labelTitle.Text = GetResource("ExtensionResultsSummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("ExtensionResultsSummary.labelIntroductoryText.Text");

			// Summary table titles
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
			plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);

			if (!returnJourneySelected)
			{
				// Return results DO NOT exist. Set visibility of all return controls to false.
				SetReturnVisible(false);
			}

		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		private void SetReturnVisible(bool visible)
		{
			returnSummaryPanel.Visible = visible;
			resultsTableTitleControlReturn.Visible = visible;
			returnResultsSummaryControl.Visible = visible;
		}

		/// <summary>
		/// Initialise the Extend Journey Line Control
		/// </summary>
		private void InitialiseJourneyLineControl()
		{
			// Use the ExtendJourneyAdapter to populate the journey line control
			ExtendJourneyAdapter journeyExtension = new ExtendJourneyAdapter();

			journeyExtension.PopulateExtendJourneyLineControl(extendJourneyLineControl);
		}

		/// <summary>
		/// Initialise the Results Summary Controls (outward and return)
		/// </summary>
		private void InitialiseResultsSummaryControls()
		{
			// Get the formatted journey lines to display in the results summary control
			ResultsAdapter resultsAdapter = new ResultsAdapter();

			// Handle display of outward journeys
			outwardSummaryLines = resultsAdapter.FormattedOutwardJourneyLines(sessionManager,FormattedSummaryType.Extend);

			outwardResultsSummaryControl.SummaryLines = outwardSummaryLines;
			outwardResultsSummaryControl.ShowSelectColumn = false;
			outwardResultsSummaryControl.PrinterFriendly = true;

			if (journeyViewState != null)
			{
				outwardResultsSummaryControl.SelectedItemJourneyType = journeyViewState.SelectedOutwardJourneyType;
				outwardResultsSummaryControl.SelectedItemJourneyIndex = journeyViewState.SelectedOutwardJourneyID;
			}

			// Handle display of return journeys
			if (returnJourneySelected)
			{
				returnSummaryLines = resultsAdapter.FormattedReturnJourneyLines(sessionManager, FormattedSummaryType.Extend);

				returnResultsSummaryControl.SummaryLines = returnSummaryLines;
				returnResultsSummaryControl.ShowSelectColumn = false;
				returnResultsSummaryControl.PrinterFriendly = true;

				if (journeyViewState != null)
				{
					returnResultsSummaryControl.SelectedItemJourneyType = journeyViewState.SelectedReturnJourneyType;
					returnResultsSummaryControl.SelectedItemJourneyIndex = journeyViewState.SelectedReturnJourneyID;
				}
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
