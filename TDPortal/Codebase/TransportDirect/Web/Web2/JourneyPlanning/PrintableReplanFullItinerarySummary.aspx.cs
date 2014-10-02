// *********************************************** 
// NAME                 : PrintableReplanFullItinerarySummary.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 20/01/2006
// DESCRIPTION			: A new page as part of the new Extension pages. Printable version displaying 
//							the full itinerary summary of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableReplanFullItinerarySummary.aspx.cs-arc  $
//
//   Rev 1.3   Jan 06 2009 14:22:36   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:02   mturner
//Initial revision.
//
//   Rev 1.10   Apr 28 2006 13:23:34   COwczarek
//Pass summary type in call to FormattedFullItinerarySummary method.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.9   Apr 04 2006 19:05:20   RGriffith
//IR3690 Fix: Addition of ErrorDisplayControl to PrintableReplanFullItinerary
//
//   Rev 1.8   Mar 28 2006 08:55:14   pcross
//Added Results Table Title control
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.7   Mar 17 2006 15:32:40   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.6   Mar 14 2006 11:39:56   RGriffith
//Addition of HeaderControl and HeadElementControl as well as inclusion of new Resource namespace
//
//   Rev 1.5   Mar 08 2006 16:53:10   RGriffith
//Update of page to use ReplanItinerary manager and remove the ExtendJourneyLineControl
//
//   Rev 1.4   Feb 24 2006 11:03:22   pcross
//Removed reference to deprecated option column
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 14 2006 09:56:06   RGriffith
//Addition of printer friendly labels and Date/Time label
//
//   Rev 1.2   Feb 07 2006 18:34:20   NMoorhouse
//Change resource manager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 30 2006 17:38:46   RGriffith
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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableReplanFullItinerarySummary.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableReplanFullItinerarySummary : TDPrintablePage, INewWindowPage
	{
		// HTML Controls

		// TD Custom Web Controls
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl combinedResultsSummaryControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableReplanFullItinerarySummary()
		{
			pageId = PageId.PrintableReplanFullItinerarySummary;
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
			ITDSessionManager sm = TDSessionManager.Current;

			if (sm.AmendedJourneyResult.CJPMessages.GetLength(0) == 0)
			{	
				ReplanItineraryManager im = (ReplanItineraryManager)sm.ItineraryManager;
				ReplanPageState pageState = (ReplanPageState)sm.InputPageState;

				AsyncCallState acs = sm.AsyncCallState;

				if ((acs != null) && (sm.AmendedJourneyResult != null) && sm.AmendedJourneyResult.IsValid)
				{
					im.InsertReplan(sm.AmendedJourneyResult, pageState.ReplanStartJourneyDetailIndex, pageState.ReplanEndJourneyDetailIndex, (pageState.CurrentAmendmentType == TDAmendmentType.OutwardJourney));
					sm.AsyncCallState = null;
				}

				im.InsertReplan(sm.AmendedJourneyResult, pageState.ReplanStartJourneyDetailIndex, pageState.ReplanEndJourneyDetailIndex, (pageState.CurrentAmendmentType == TDAmendmentType.OutwardJourney));
			
				// Call private methods to populate individual web controls
				PopulateResultsSummaryControl();
				
				// Initialise text and button properties
				PopulateControls();

				errorMessagePanel.Visible = false;
				errorDisplayControl.Visible = false ;
			}
			else
			{
				// Initialise text and button properties
				PopulateControls();

				ArrayList errorsList = new ArrayList();
				foreach (CJPMessage message in sm.AmendedJourneyResult.CJPMessages)
				{
					if(message.Type == ErrorsType.Warning)
					{
						errorDisplayControl.Type = ErrorsDisplayType.Warning;
					}

					string text = message.MessageText;

					if( text == null || text.Length == 0 )
					{
						string errResource = message.MessageResourceId;

						text = GetResource("langStrings", errResource);

						errorsList.Add( text );
					}

					errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));
					
					if (errorDisplayControl.ErrorStrings.Length > 0)
					{
						errorMessagePanel.Visible = true;
						errorDisplayControl.Visible = true;
						summaryPanel.Visible = false;
					}
					else
					{
						errorMessagePanel.Visible = false;
						errorDisplayControl.Visible = false ;
					}
				}
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Get correct resource strings for labels
			labelTitle.Text = GetResource("ReplanFullItinerarySummary.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("ReplanFullItinerarySummary.labelIntroductoryText.Text");

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
		/// Initialise the Results Summary Control
		/// </summary>
		private void PopulateResultsSummaryControl()
		{
			// Set display properties
			combinedResultsSummaryControl.ShowDeleteColumn = false;
			combinedResultsSummaryControl.ShowSelectColumn = false;
			combinedResultsSummaryControl.ShowEmptyDeleteColumn = false;
			combinedResultsSummaryControl.ShowEmptySelectColumn = false;

			// Use new results adpater to populate the control's summary lines
			ResultsAdapter journeyResults = new ResultsAdapter();
			combinedResultsSummaryControl.SummaryLines = journeyResults.FormattedFullItinerarySummary(ExtendItineraryManager.Current, FormattedSummaryType.Replan);
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
