// *********************************************** 
// NAME                 : CarDetails.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 22/02/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Page displays the details of selected car journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/CarDetails.aspx.cs-arc  $
//
//   Rev 1.5   Sep 01 2011 10:44:44   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.4   Jan 02 2009 15:47:38   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jan 02 2009 15:21:54   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:58   mturner
//Initial revision.
//
//   Rev 1.9   Jun 28 2007 16:27:16   mmodi
//Added code to populate the Car details control with journey parameters
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.8   Jun 01 2007 11:19:26   mmodi
//Added Check CO2 button code 
//Resolution for 4438: CO2: Add Check CO2 button to Car details page in Modify journey
//
//   Rev 1.7   Mar 22 2006 20:27:52   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Mar 16 2006 10:34:16   NMoorhouse
//Applied review comments (CR064)
//
//   Rev 1.5   Mar 14 2006 11:42:46   NMoorhouse
//Post stream3353 merge: added new HeadElementControl, HeaderControls and reference new ResourceManager
//
//   Rev 1.4   Mar 10 2006 11:47:18   NMoorhouse
//Ensure correct journey is selected from front end selected segment
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 06 2006 18:17:32   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 02 2006 19:31:44   NMoorhouse
//Added Help Page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Mar 01 2006 14:33:22   NMoorhouse
//Updated during Dev
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 24 2006 14:39:28   NMoorhouse
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
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for CarDetails.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class CarDetails : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.CarAllDetailsControl carAllDetailsControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		private bool outward = true;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public CarDetails() : base()
		{
			// Set page Id
			pageId = PageId.CarDetails;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyParametersMulti journeyParams = TDItineraryManager.Current.JourneyParameters as TDJourneyParametersMulti;

			carAllDetailsControlOutward.Printable = true;
			RoadJourney roadJourney = GetJourneyDetail();
			
			// Because there is only the ability to store one set of JourneyParameters in the session, 
			// we cannot allow the carAllDetailsControl to display the CarJourneyType/Options controls 
			// because the user may have two car segments with different parameters.
			if((itineraryManager.ItineraryMode == ItineraryManagerMode.ExtendJourney) 
				&&
				(!itineraryManager.ExtendInProgress) )
			{
				carAllDetailsControlOutward.Initialise(roadJourney, viewState, outward, !IsPostBack);
			}
			else
			{
				carAllDetailsControlOutward.Initialise(roadJourney, viewState, outward, journeyParams, !IsPostBack);
			}

			labelTitle.Text = GetResource("CarDetails.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("CarDetails.labelIntroductoryText.Text");
			backButton.Text = GetResource("CarDetails.backButton.Text");

			helpButton.HelpUrl = GetResource("CarDetails.helpButton.HelpUrl");

			// Only display the CompareCO2 buttons if switch is turned on
			buttonCompareEmissions.Visible = JourneyEmissionsHelper.JourneyEmissionsPTAvailable;
			buttonCompareEmissions.Text = GetResource("CarDetails.CompareEmissions.Text");

            //Added for white labelling:
            ConfigureLeftMenu("CarDetails.clientLink.BookmarkTitle", "CarDetails.clientLink.LinkText", null, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextCarDetails);
		}

		/// <summary>
		/// Returns back to the calling page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backButtonClick(object sender, EventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ServiceDetailsBack;
		}

		/// <summary>
		/// Handler for the Compare emissions button
		/// Navigates to the Compare emissions page for Car
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCompareEmissions_Click(object sender, EventArgs e)
		{
			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			// Reset the journey emissions page state, to clear it of any previous values
			TDSessionManager.Current.JourneyEmissionsPageState.Initialise();
			
			// Journey is always a road journey on this page therefore, navigate to the car emissions page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;				
		}

		/// <summary>
		/// Gets the RoadJourney that was selected from the session data.  
		/// </summary>
		/// <returns>RoadJourney</returns>
		private RoadJourney GetJourneyDetail()
		{
			Journey journey;

			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			bool outward = TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward];  

			if (outward)
			{
				journey = TDItineraryManager.Current.SelectedOutwardJourney;
			}
			else
			{
				journey = TDItineraryManager.Current.SelectedReturnJourney;
			}

			if (viewState == null)
			{
				return null;
			}
			else if (viewState.SelectedIntermediateItinerarySegment >= 0)
			{
				if (outward)
				{
					labelDirection.Text = GetResource("CarDetails.labelOutwardDirection.Text");
					journey = itineraryManager.GetOutwardJourney(viewState.SelectedIntermediateItinerarySegment);
				}
				else
				{
					labelDirection.Text = GetResource("CarDetails.labelReturnDirection.Text");
					journey = itineraryManager.GetReturnJourney(viewState.SelectedIntermediateItinerarySegment);
				}
			}

			if	(journey != null) 
			{
				RoadJourney roadJourney = journey as RoadJourney;
				
				if (roadJourney != null)
					labelSummary.Text = String.Concat(GetResource("CarDetails.JourneySummary.TextElement1"), roadJourney.OriginLocation.Description, GetResource("CarDetails.JourneySummary.TextElement2"), roadJourney.DepartDateTime.ToString("HH:mm"));
				
				return roadJourney;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			backButton.Click += new EventHandler(this.backButtonClick);	
			buttonCompareEmissions.Click += new EventHandler(this.buttonCompareEmissions_Click);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
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
