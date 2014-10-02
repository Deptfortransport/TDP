// *********************************************** 
// NAME                 : PrintableJourneyEmissions.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 26/11/2006 
// DESCRIPTION			: Page displaying the printer friendly version of the Journey Emissions page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyEmissions.aspx.cs-arc  $ 
//
//   Rev 1.3   Dec 19 2008 15:07:28   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:46   mturner
//Initial revision.
//
//   Rev 1.3   Nov 30 2006 18:39:08   mmodi
//Added SaveFuel text
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 27 2006 12:28:18   mmodi
//Added version logging info
//Resolution for 4240: CO2 Emissions

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
	/// <summary>
	/// Summary description for PrintableJourneyEmissions.
	/// </summary>
	public partial class PrintableJourneyEmissions : TDPrintablePage, INewWindowPage
	{
		
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl resultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsControl journeyEmissionsControl;

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public PrintableJourneyEmissions()
		{
			pageId = PageId.PrintableJourneyEmissions;
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
			InitialiseResultsSummaryControl();
			PopulateJourneyEmissionsControl();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Page title
			labelTitle.Text = GetResource("JourneyEmissions.Title");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
			string labelTextOverride = GetResource("JourneyEmissions.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, false);

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

			labelSaveFuelTitle.Text = GetResource("JourneyEmissions.labelSaveFuel.Title");
			labelSaveFuelText.Text = GetResource("JourneyEmissions.labelSaveFuel.Text");
		}

		/// <summary>
		/// Initialise the Results Summary Control
		/// </summary>
		private void InitialiseResultsSummaryControl()
		{
			// Set display properties
			resultsSummaryControl.ShowDeleteColumn = false;
			resultsSummaryControl.ShowSelectColumn = false;
			resultsSummaryControl.ShowEmptyDeleteColumn = false;
			resultsSummaryControl.ShowEmptySelectColumn = false;

			// Use new results adpater to populate the control's summary lines
			ResultsAdapter helper = new ResultsAdapter();
			ITDSessionManager sessionManager = TDSessionManager.Current;
			resultsSummaryControl.SummaryLines = helper.FormattedSelectedJourneys(sessionManager, FormattedSummaryType.Replan);	
		}

		/// <summary>
		/// Populates the JourneyEmissionsControl
		/// </summary>
		private void PopulateJourneyEmissionsControl()
		{
			journeyEmissionsControl.Printable = true;
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
