// *********************************************** 
// NAME                 : PrintableRefineDetails.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 02/01/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Printable version displaying the maps of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableRefineDetails.aspx.cs-arc  $
//
//   Rev 1.6   Feb 16 2010 11:16:14   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 12 2010 11:14:06   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Nov 11 2009 16:43:18   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Jan 06 2009 11:09:58   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:58   mturner
//Initial revision.
//
//   Rev 1.4   Mar 17 2006 15:32:38   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.3   Mar 14 2006 13:18:54   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 09 2006 11:39:26   NMoorhouse
//Made consistent with RefineDetails
//
//   Rev 1.1   Mar 06 2006 18:17:34   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 09 2006 16:21:54   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 07 2006 18:47:24   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableRefineDetails.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableRefineDetails : TDPrintablePage, INewWindowPage
	{
		// HTML Controls

		// TD Web Controls
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsControl journeyDetailsControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsControl journeyDetailsControlReturn;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableControl journeyDetailsTableControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableControl journeyDetailsTableControlReturn;

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress;

		/// <summary>
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary;

		private bool returnArriveBefore;
		private bool outwardArriveBefore;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableRefineDetails() : base()
		{
			// Set page Id
			pageId = PageId.PrintableRefineDetails;

			// Initialise Resource Manager
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
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

			// Initialise text and button properties
			PopulateControls();

			// Initialise journey details controls
			InitialiseJourneyDetailsControls();
		}

		/// <summary>
		/// Handles the page prerender event. This performs any last-minute updates to the controls
		/// that are displayed to the user
		/// </summary>
		protected void OnPreRender(object sender, System.EventArgs e)
		{
			itineraryManager = TDItineraryManager.Current;
			SetControlVisibility();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Get correct resource strings for labels
			labelTitle.Text = GetResource("RefineDetails.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("RefineDetails.labelIntroductoryText.Text");
			labelOutwardDirection.Text = GetResource("RefineDetails.labelOutwardDirection.Text");
			labelReturnDirection.Text = GetResource("RefineDetails.labelReturnDirection.Text");
			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");
			labelDateTitle.Text = GetResource("StaticPrinterFriendly.labelDateTitle");
			labelDate.Text = TDDateTime.Now.ToString("G");
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsernameTitle.Text = GetResource("StaticPrinterFriendly.labelUsernameTitle");
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}
		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;

					// Get time types for journey.
					outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		/// <summary>
		/// Initialise the Journey Details Controls
		/// </summary>
		private void InitialiseJourneyDetailsControls()
		{
			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			DetermineStateOfResults();

			// Initialise those controls required for displaying an outward journey or extension
			if (outwardExists)
			{
				if (!itineraryManager.FullItinerarySelected)
				{
					// An individual journey is selected

					// Determine the journey details object to use in initialising the
					// details controls

					Journey outJourney = null;
		
					if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						// the original journey has been selected
						outJourney = journeyResult.OutwardPublicJourney(
							viewState.SelectedOutwardJourneyID);
					}
					else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						outJourney = journeyResult.AmendedOutwardPublicJourney;
					} 
					else if (viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested) 
					{
						//private journey has been selected
						outJourney = journeyResult.OutwardRoadJourney();
					}

					if (outJourney != null) 
					{
						journeyDetailsControlOutward.Initialise(outJourney, true, false,false,true, itineraryManager.JourneyRequest,tdSessionManager.FindAMode);
						journeyDetailsControlOutward.MyPageId = pageId;
						journeyDetailsControlOutward.Printable = true;
						journeyDetailsTableControlOutward.Initialise(outJourney, true, tdSessionManager.FindAMode);
						journeyDetailsTableControlOutward.MyPageId = pageId;
						journeyDetailsTableControlOutward.PrinterFriendly = true;
					}
				}
				else
				{
					// Full journey summary selected
                    journeyDetailsControlOutward.Initialise(true, false, false, true, tdSessionManager.FindAMode);
					journeyDetailsControlOutward.MyPageId = pageId;	
					journeyDetailsControlOutward.Printable = true;
					journeyDetailsTableControlOutward.Initialise(true, tdSessionManager.FindAMode);
					journeyDetailsTableControlOutward.MyPageId = pageId;
					journeyDetailsTableControlOutward.PrinterFriendly = true;
					labelOutwardSummary.Text = String.Concat(GetResource("RefineDetails.JourneySummary.TextElement1"), itineraryManager.OutwardDepartLocation().Description, GetResource("RefineDetails.JourneySummary.TextElement2"), itineraryManager.OutwardDepartDateTime().ToString("HH:mm"));
				}
			}

			if (returnExists)
			{
				if (!itineraryManager.FullItinerarySelected)
				{
					// An individual journey is selected

					// Determine the journey details object to use in initialising the
					// details controls

					Journey retJourney = null;

					if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						// the original journey has been selected
						retJourney = journeyResult.ReturnPublicJourney(
							viewState.SelectedReturnJourneyID);
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						retJourney = journeyResult.AmendedReturnPublicJourney;
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
					{
						//private journey has been selected
						retJourney = journeyResult.ReturnRoadJourney();
					}

					if (retJourney != null) 
					{
						journeyDetailsControlReturn.Initialise(retJourney, false, false, false, true, itineraryManager.JourneyRequest,tdSessionManager.FindAMode);
						journeyDetailsControlReturn.MyPageId = pageId;
						journeyDetailsControlReturn.Printable = true;
						journeyDetailsTableControlReturn.Initialise(retJourney, false, tdSessionManager.FindAMode);
						journeyDetailsTableControlReturn.MyPageId = pageId;	
						journeyDetailsTableControlReturn.PrinterFriendly = true;
					}
			
				}
				else
				{
					// Full journey summary selected
					journeyDetailsControlReturn.Initialise(false, false,false,true, tdSessionManager.FindAMode);			
					journeyDetailsControlReturn.MyPageId = pageId;
					journeyDetailsControlReturn.Printable = true;
					journeyDetailsTableControlReturn.Initialise(false, tdSessionManager.FindAMode);
					journeyDetailsTableControlReturn.MyPageId = pageId;
					journeyDetailsTableControlReturn.PrinterFriendly = true;
					labelReturnSummary.Text = String.Concat(GetResource("RefineDetails.JourneySummary.TextElement1"), itineraryManager.ReturnDepartLocation().Description, GetResource("RefineDetails.JourneySummary.TextElement2"), itineraryManager.ReturnDepartDateTime().ToString("HH:mm"));
				}

			}

		}

		/// <summary>
		/// Determines which controls should be visible
		/// </summary>
		private void SetControlVisibility()
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			if (outwardExists)
			{
				outwardPanel.Visible = true;

				// Journey Details control is visible if diagram mode
				journeyDetailsControlOutward.Visible = viewState.ShowOutwardJourneyDetailsDiagramMode;

				// Journey Details table control is visible if not diagram mode.
				journeyDetailsTableControlOutward.Visible = !viewState.ShowOutwardJourneyDetailsDiagramMode;
			}
			else
			{
				outwardPanel.Visible = false;
			}

			if (returnExists)
			{
				returnPanel.Visible = true;

				// Journey Details Control is visible if diagram mode
				journeyDetailsControlReturn.Visible = viewState.ShowReturnJourneyDetailsDiagramMode;
		
				// Journey Details table control is visible if table mode
				journeyDetailsTableControlReturn.Visible = !viewState.ShowReturnJourneyDetailsDiagramMode;
			}
			else
			{
				returnPanel.Visible = false;
			}
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
			this.PreRender += new System.EventHandler(this.OnPreRender);

		}
		#endregion
	}
}

