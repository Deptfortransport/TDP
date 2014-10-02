// *********************************************** 
// NAME                 : PrintableExtendedFullItinerarySummary.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: A new page as part of the new Extension pages. Printable version displaying 
//							the full itinerary summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableExtendedFullItinerarySummary.aspx.cs-arc  $
//
//   Rev 1.3   Jan 09 2009 09:44:18   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:30   mturner
//Initial revision.
//
//   Rev 1.10   Apr 28 2006 13:18:22   COwczarek
//Pass summary type in call to FormattedFullItinerarySummary method.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.9   Mar 28 2006 08:47:58   pcross
//Added Results Table Title control
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.8   Mar 17 2006 15:32:38   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.7   Mar 14 2006 11:39:56   RGriffith
//Addition of HeaderControl and HeadElementControl as well as inclusion of new Resource namespace
//
//   Rev 1.6   Mar 10 2006 16:39:04   RGriffith
//Removal of Summary Title
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 08 2006 09:22:52   pcross
//Tidied layout
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 07 2006 19:22:28   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.3   Feb 24 2006 13:13:46   pcross
//Removed reference to deprecated option column
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 14 2006 09:56:06   RGriffith
//Addition of printer friendly labels and Date/Time label
//
//   Rev 1.1   Feb 07 2006 18:34:20   NMoorhouse
//Change resource manager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 30 2006 17:33:18   RGriffith
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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for ExtendedFullItinerarySummary.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableExtendedFullItinerarySummary : TDPrintablePage, INewWindowPage
	{
		// HTML Controls
		protected Label labelReferenceNumberTitle;
		protected Label labelJourneyReferenceNumber;

		// TD Custom Web Controls
		protected TransportDirect.UserPortal.Web.Controls.ExtendJourneyLineControl extendJourneyLineControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl combinedResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableExtendedFullItinerarySummary()
		{
			pageId = PageId.PrintableExtendedFullItinerarySummary;
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Event Handlers
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties
			PopulateControls();
		}

		/// <summary>
		/// Handles page initalise event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ExtendedFullItinerarySummary_Init(object sender, EventArgs e)
		{
			// Call private methods to populate individual web controls
			InitialiseJourneyLineControl();
			InitialiseResultsSummaryControl();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Get correct resource strings for labels
			labelTitle.Text = GetResource("ExtendedFullItinerarySummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("ExtendedFullItinerarySummary.labelIntroductoryText.Text");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
			string labelTextOverride = GetResource("RefineJourney.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, false);

			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");
			labelDateTitle.Text = GetResource("StaticPrinterFriendly.labelDateTitle");
			labelDate.Text = TDDateTime.Now.ToString("G");
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
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
			combinedResultsSummaryControl.SummaryLines = journeyResults.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Extend);
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
			this.Init += new System.EventHandler(this.ExtendedFullItinerarySummary_Init);

		}
		#endregion
	}
}
