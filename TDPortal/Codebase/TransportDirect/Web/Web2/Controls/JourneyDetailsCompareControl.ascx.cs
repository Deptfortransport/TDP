// *********************************************** 
// NAME                 : JourneyDetailsCompareControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 20/08/2003
// DESCRIPTION			: Control to display two journeys and compare
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyDetailsCompareControl.ascx.cs-arc  $
//
//   Rev 1.6   Feb 12 2010 11:13:30   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Nov 11 2009 16:42:48   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.4   Feb 09 2009 15:19:46   mmodi
//Updated code to allow Routing Guide Sections to be created for the adjusted Public Journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.3   Jan 14 2009 14:19:52   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:14   mturner
//Initial revision.
//
// Rev DevFactory Feb 14 2008 10:12:00 dgath
// All references to buttonBackToOriginal moved to CompareAdjustJourney.aspx page, due to 
//a requirement for this button to be moved outside of the JourneyDetailsCompareControl.
//
//   Rev 1.26   Apr 18 2006 17:37:02   mtillett
//the selected index of the result will only be set to the selected index of the adjusted journey when a CPJ error has not occurred
//Resolution for 3928: DN068 Adjust: Server error when viewing maps of an adjusted journey
//
//   Rev 1.25   Mar 13 2006 16:41:06   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.23.1.1   Mar 02 2006 15:03:56   pcross
//Updated screen flow for new adjust screens
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.23.1.0   Feb 16 2006 17:09:34   pcross
//Compare control now goes to the adjust results page on pressing Next.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Feb 23 2006 16:11:48   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.23   Nov 23 2005 16:48:06   ECHAN
//Code review comment #1 for UEE
//
//   Rev 1.22   Nov 03 2005 16:02:12   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.21.1.0   Oct 27 2005 14:21:42   kjosling
//TD089 ES020 Image Button Replacement
//
//   Rev 1.21   Nov 26 2003 10:38:40   kcheung
//Fixed bug that causes incorrect journey to be selected after adjusting.
//
//   Rev 1.20   Nov 25 2003 18:21:14   COwczarek
//SCR#145: CSS style does not appear correctly in Mozilla browsers  
//Resolution for 145: CSS style does not appear correctly in Mozilla browsers
//
//   Rev 1.19   Nov 19 2003 11:47:48   kcheung
//Updated to use new JourneyViewState properties.
//Resolution for 136: Properties of JourneyViewState to determine the selected outward and return journeys
//
//   Rev 1.18   Oct 30 2003 14:56:50   PNorell
//Updated for visual bugs.
//
//   Rev 1.17   Oct 22 2003 16:20:10   passuied
//bug fix : if no adjust journey found get last amended one!
//
//   Rev 1.16   Oct 22 2003 14:16:16   kcheung
//Fixed print width
//
//   Rev 1.15   Oct 22 2003 12:21:14   RPhilpott
//Improve handling of case when no journeys found
//
//   Rev 1.14   Oct 20 2003 11:10:36   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.13   Oct 06 2003 16:19:04   PNorell
//Updated when and how icons are shown.
//
//   Rev 1.12   Sep 29 2003 17:48:26   PNorell
//Arranged with a few printability issues.
//
//   Rev 1.11   Sep 29 2003 16:47:10   PNorell
//Printable compared journey added.
//
//   Rev 1.10   Sep 29 2003 10:25:12   PNorell
//Fixed the split view looking as in the visuals
//
//   Rev 1.9   Sep 25 2003 20:15:48   RPhilpott
//Add ESRI map handoff
//
//   Rev 1.8   Sep 25 2003 11:46:16   PNorell
//Fixed the compare control to have footer and small bugs in the handling of the adjusted journey.
//
//   Rev 1.7   Sep 23 2003 14:49:48   PNorell
//Updated page states and the wait page to function according to spec.
//Updated the different controls to ensure they have correct PageId and that they call the ValidateAndRun properly.
//Removed some 'warning' messages - a clean project is nice to see.
//
//   Rev 1.6   Sep 22 2003 20:55:34   kcheung
//Added missing using statements
//
//   Rev 1.5   Sep 22 2003 18:57:22   PNorell
//Updated all transition events and page ids and interaction events.
//
//   Rev 1.4   Sep 19 2003 19:58:00   PNorell
//Updated all journey details screens.
//Support for Adjusted journeys added and Validate And Run.

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Control to compare original journey with adjusted journey
	/// </summary>
	public partial  class JourneyDetailsCompareControl : TDUserControl
	{
		protected JourneyDetailsControl journeyDetailsControlOne;
		protected JourneyDetailsControl journeyDetailsControlTwo;
		protected System.Web.UI.WebControls.Label header;

		private PageId belongingPageId = PageId.Empty;

		#region PageId / Printable Properties
		
		/// <summary>
		/// Get/Set property for the pageId.
		/// </summary>
		public PageId MyPageId
		{
			get
			{
				return belongingPageId;
			}

			set
			{
				belongingPageId = value;
				journeyDetailsControlOne.MyPageId = value;
				journeyDetailsControlTwo.MyPageId = value;
			}
		}

		/// <summary>
		/// Get/Set property - get/set whether the control should be rendered
		/// in Printable mode or not.
		/// </summary>
		public bool Printable
		{
			get
			{
				return journeyDetailsControlOne.Printable && journeyDetailsControlTwo.Printable;
			}

			set
			{
				journeyDetailsControlOne.Printable = value;
				journeyDetailsControlTwo.Printable = value;
			}
		}

		#endregion

		#region Page Load Method

		/// <summary>
		/// Page Load Method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			TDJourneyViewState viewState = TDSessionManager.Current.JourneyViewState;
			TDCurrentAdjustState adjustState = TDSessionManager.Current.CurrentAdjustState;
			ITDJourneyResult journeyResult = TDSessionManager.Current.JourneyResult;
			
			// Determina if it is the outward or return that was the original
			bool outward = adjustState.CurrentAmendmentType == TDAmendmentType.OutwardJourney;

			int selectedIndex = -1;

			PublicJourney originalPublicJourney = null;

			if(outward)
			{
				selectedIndex = viewState.SelectedOutwardJourney;
		
				if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
				{
					// Get the journey index of the selected journey.
					int journeyIndex = viewState.SelectedOutwardJourneyID;
					// Get the public journey from journeyResult
					originalPublicJourney = journeyResult.OutwardPublicJourney(journeyIndex);
				}
				else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
				{
					// the amended journey has been selected
					originalPublicJourney = journeyResult.AmendedOutwardPublicJourney;
				}
			}
			else 
			{
				selectedIndex = viewState.SelectedReturnJourney;
				
				if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
				{
					// Get the journey index of the selected journey.
					int journeyIndex = viewState.SelectedReturnJourneyID;
					// Get the public journey from journeyResult
					originalPublicJourney = journeyResult.ReturnPublicJourney(journeyIndex);
				}
				else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
				{
					// the amended journey has been selected
					originalPublicJourney = journeyResult.AmendedReturnPublicJourney;
				}
			}

			// text load
			originalJourneyTitle.Text = Global.tdResourceManager.GetString(
				"JourneyDetailsCompareControl.originalJourneyTitle", TDCultureInfo.CurrentUICulture);

			// In the left hand corner we got original selected journey
			journeyDetailsControlOne.Initialise(originalPublicJourney,outward, true, false, false, TDItineraryManager.Current.JourneyRequest,TDSessionManager.Current.FindAMode);

			// In the right hand corner we got the amended journey but yet not saved
			ITDJourneyResult adjustedResults = TDSessionManager.Current.AmendedJourneyResult;
			
			// if no journey found, adjusted journey is set to the last amended journey.
 
			PublicJourney adjustedPublicJourney = null;

			if	(adjustedResults != null && adjustedResults.OutwardPublicJourneyCount > 0)
			{
				AdjustRoute adjustRoute = new AdjustRoute();
                adjustedPublicJourney = adjustRoute.BuildAmendedJourney(originalPublicJourney, adjustedResults, adjustState);
			}
			else
			{
				adjustedPublicJourney = adjustState.AmendedJourney;
			}

			
			adjustState.AmendedJourney = adjustedPublicJourney;

			journeyDetailsControlTwo.Initialise(adjustedPublicJourney,outward, true,true, true, TDItineraryManager.Current.JourneyRequest,TDSessionManager.Current.FindAMode);

			//*****

			buttonSaveJourney.Visible = !Printable;
			buttonSaveJourney.Text = Global.tdResourceManager.GetString(
				"JourneyDetailsCompareControl.buttonSaveJourney.Text", TDCultureInfo.CurrentUICulture);
			buttonSaveJourney.ToolTip = Global.tdResourceManager.GetString(
				"JourneyDetailsCompareContro1.buttonSaveJourney.AlternateText", TDCultureInfo.CurrentUICulture);

			adjustedJourneyTitle.Text = Global.tdResourceManager.GetString(
				"JourneyDetailsCompareControl.adjustedJourneyTitle", TDCultureInfo.CurrentUICulture);
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonSaveJourney.Click += new EventHandler(this.buttonSaveJourney_Click);

        }
		#endregion

		#region Event Handlers

		/// <summary>
		/// Handler for the Save button
		/// </summary>
		private void buttonSaveJourney_Click(object sender, EventArgs e)
		{
			AsyncCallState acs = TDSessionManager.Current.AsyncCallState;
			if (!( acs != null && acs.Status == AsyncCallStatus.NoResults ))
			{
				TDJourneyViewState viewState = TDSessionManager.Current.JourneyViewState;
				TDCurrentAdjustState adjustState = TDSessionManager.Current.CurrentAdjustState;
				ITDJourneyResult journeyResult = TDSessionManager.Current.JourneyResult;

				// Determine if it is the outward or return that was the original
				bool outward = adjustState.CurrentAmendmentType == TDAmendmentType.OutwardJourney;

				// Determine what the search type was.
				bool arriveBefore = viewState.JourneyLeavingTimeSearchType;

				ITDJourneyResult adjustedResults = TDSessionManager.Current.AmendedJourneyResult;

				PublicJourney adjustedPublicJourney = adjustState.AmendedJourney;

				if( outward )
				{
					journeyResult.AmendedOutwardPublicJourney = adjustedPublicJourney;
					// Select the amended journey

					// Select the journey that is amended in the summary line.
					JourneySummaryLine[] summaryLine = TDSessionManager.Current.JourneyResult.OutwardJourneySummary
						(TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType);

					int i = 0;
					for(i=0; i<summaryLine.Length; i++)
					{
						if(summaryLine[i].Type == TDJourneyType.PublicAmended)
							break;
					}

					viewState.SelectedOutwardJourney = i;
					viewState.SelectedOutwardJourneyID = adjustedPublicJourney.JourneyIndex;
					viewState.SelectedOutwardJourneyType = TDJourneyType.PublicAmended;
				}
				else
				{
					journeyResult.AmendedReturnPublicJourney = adjustedPublicJourney;
					// Select the amended journey
					JourneySummaryLine[] summaryLine = TDSessionManager.Current.JourneyResult.ReturnJourneySummary
						(TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType);

					int i=0;
					for(i=0; i<summaryLine.Length; i++)
					{
						if(summaryLine[i].Type == TDJourneyType.PublicAmended)
							break;
					}

					viewState.SelectedReturnJourney = i;
					viewState.SelectedReturnJourneyID = adjustedPublicJourney.JourneyIndex;
					viewState.SelectedReturnJourneyType = TDJourneyType.PublicAmended;
				}

				// Pass new journey details to ESRI database for subsequent map display.
				ITDMapHandoff handoff = (ITDMapHandoff) TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];
			
				handoff.SaveJourneyResult(TDSessionManager.Current.Session.SessionID, 
					(outward ? journeyResult.AmendedOutwardPublicJourney : journeyResult.AmendedReturnPublicJourney));

                // Ensure the fares option is set to calculate fares for the new journey
                if (TDSessionManager.Current.PricingRetailOptions != null)
                {
                    TDSessionManager.Current.PricingRetailOptions.CalculateFaresOverride = true;
                }
			}

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.AdjustFullItinerarySummary;
		}

		/////

		#endregion

	}
}
