// *********************************************** 
// NAME                 : PrintableAdjustFullItinerarySummary.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: A new page as part of the new Extension pages. Printable version displaying 
//							the full itinerary summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableAdjustFullItinerarySummary.aspx.cs-arc  $
//
//   Rev 1.3   Jan 14 2009 13:35:52   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:26   mturner
//Initial revision.
//
//   Rev 1.10   Apr 28 2006 13:17:32   COwczarek
//Pass summary type in call to FormattedSelectedJourneys method.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.9   Mar 28 2006 08:40:58   pcross
//Removed Summary title
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.8   Mar 17 2006 15:32:36   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.7   Mar 14 2006 13:19:20   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Mar 08 2006 16:29:14   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.5   Mar 03 2006 16:05:40   pcross
//Layout changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 24 2006 11:00:36   pcross
//Removed reference to deprecated option column
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 14 2006 09:56:04   RGriffith
//Addition of printer friendly labels and Date/Time label
//
//   Rev 1.2   Feb 07 2006 18:34:18   NMoorhouse
//Change resource manager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 30 2006 17:38:48   RGriffith
//Changes for renaming of Extended Summary pages
//
//   Rev 1.0   Jan 30 2006 13:02:22   RGriffith
//Initial revision.
//
//   Rev 1.0   Jan 26 2006 12:17:58   RGriffith
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

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableAdjustFullItinerarySummary.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableAdjustFullItinerarySummary : TDPrintablePage, INewWindowPage
	{
		// HTML Controls

		// TD Custom Web Controls
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl combinedResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;


		// Printable footer

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableAdjustFullItinerarySummary() : base()
		{
			// Set page Id
			pageId = PageId.PrintableAdjustFullItinerarySummary;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Private member variables
		
		ITDSessionManager sessionManager;

		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			sessionManager = TDSessionManager.Current;

			// Call private methods to populate individual web controls
			InitialiseResultsSummaryControl();

			// Initialise text and button properties
			PopulateControls();
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Standard printer friendly controls
			labelPrinterFriendly.Text = GetResource("langStrings", "StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("langStrings", "StaticPrinterFriendly.labelInstructions");
			labelDate.Text = TDDateTime.Now.ToString("G");
			labelUsername.Visible = sessionManager.Authenticated;
			labelUsernameTitle.Visible = sessionManager.Authenticated;

			if( sessionManager.Authenticated )
			{
				labelUsername.Text = sessionManager.CurrentUser.Username;
			}

			// Set the journey reference number from the result
			labelReferenceNumberTitle.Visible = true;
			labelJourneyReferenceNumber.Text =
				sessionManager.JourneyResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("AdjustFullItinerarySummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("AdjustFullItinerarySummary.labelIntroductoryText.Text");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
			string labelTextOverride = GetResource("RefineJourney.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, false);
		}

		/// <summary>
		/// Initialise the Results Summary Control
		/// </summary>
		private void InitialiseResultsSummaryControl()
		{
			// Set display properties
			combinedResultsSummaryControl.ShowDeleteColumn = false;
			combinedResultsSummaryControl.ShowSelectColumn = false;
			combinedResultsSummaryControl.ShowEmptyDeleteColumn = false;
			combinedResultsSummaryControl.ShowEmptySelectColumn = false;

			// Use new results adpater to populate the control's summary lines
			ResultsAdapter journeyResults = new ResultsAdapter();
			combinedResultsSummaryControl.SummaryLines = journeyResults.FormattedSelectedJourneys(sessionManager, FormattedSummaryType.Adjust);
		}

		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Web Form Designer generated code
		/// </summary>
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
