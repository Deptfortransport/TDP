// *********************************************** 
// NAME                 : ResultsFootnotesControl.aspx.cs
// AUTHOR               : James Brooome
// DATE CREATED         : 10/06/2005
// DESCRIPTION			: Control to display footnotes on journey results pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ResultsFootnotesControl.ascx.cs-arc  $
//
//   Rev 1.10   Mar 12 2010 13:54:32   mmodi
//Footnote label to about all times show being local added
//Resolution for 5451: TD Extra - Updates following testing by DfT
//
//   Rev 1.9   Feb 25 2010 11:04:10   mmodi
//Property to specify and set the accessability modes to show
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 21 2010 23:23:04   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 17 2010 15:13:46   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 27 2009 09:38:32   rbroddle
//Added "isTripPlannerJourney" property
//Resolution for 5258: Visit planner results footnotes include the note about planning a return using "amend date and time" tab
//
//   Rev 1.5   Nov 18 2008 15:17:28   mturner
//Updated to remove accesibility link for cycle journeys
//Resolution for 5174: Cycle Planner - Remove link to information for disabled users
//
//   Rev 1.4   Oct 13 2008 16:44:22   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.1   Sep 01 2008 13:26:46   jfrank
//Added air accessibility rights link
//Resolution for 5106: Add Air Accessibility Rights Link to portal
//
//   Rev 1.3.1.0   Jun 20 2008 14:25:38   mmodi
//Updated for cycle pages
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Apr 04 2008 15:41:18   mmodi
//Updated notes to also use pageid
//Resolution for 4829: Del 10 - City to city shows "Unable to find journeys" error message
//
//   Rev 1.2   Mar 31 2008 13:22:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:16   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to show an additional line for CO2 emissions in City to City
//
//   Rev 1.2   Jun 06 2007 12:11:32   sangle
//Added Footnote to journey results page for all journeys except returns and ParkAndRide.
//
//   Rev 1.1   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:27:00   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jun 29 2005 11:11:56   jbroome
//Initial revision.
//Resolution for 2556: DEL 8 Stream: Accessibility Links

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;

    using TransportDirect.Common;
    using TransportDirect.JourneyPlanning.CJPInterface;
    using TransportDirect.UserPortal.ExternalLinkService;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;
    
	/// <summary>
	///	Control to display footnotes on journey results pages
	///	Consists of a series of labels contained in a table structure.
	/// </summary>
	public partial class ResultsFootnotesControl : TDPrintableUserControl
	{
	
		private FindAMode findAMode = FindAMode.None;
		private bool costBasedResults = false;
		private bool returnExists = false;
		private bool isParkandRide = false;
        private bool isTripPlannerJourney = false;
        private PageId controlPageId = PageId.Empty;
        private ModeType[] accessabilityModeTypes = new ModeType[0];

		protected HyperlinkPostbackControl linkControl;
		
		/// <summary>
		/// Page Load sets up label text 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set up label text

            //rowFootnote - "Notes:"
			labelFootnote.Text = GetResource("ResultsFootnotes.labelFootnote.Text");

            //rowAccessibilltyNotes - "Travellers with disabilities can find out abou..."
            labelAccessibillityFootnoteLine1.Text = GetResource("ResultsFootnotes.labelAccessibillityFootnoteLine1.Text");
			linkControl.Text = GetResource("ResultsFootnotes.labelAccessibillityFootnoteLine1.LinkText");

            //rowAccessibilltyNotes1 - "More information about you accessibility rights when you fly..."
            labelAccessibilityFootnoteLine2.Text = GetResource("ResultsFootnotes.labelAccessibilityFootnoteLine2.Text");
            ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current["Accessibility.Rights"];

            if (link != null)
            {
                hyperlinkAccessibilityFootnoteLine2.NavigateUrl = link.Url;
                hyperlinkAccessibilityFootnoteLine2.Text = GetResource("ResultsFootnotes.labelAccessibilityFootnoteLine2.LinkText");
                hyperlinkAccessibilityFootnoteLine2.Target = "_blank";
            }
            else
            {
                hyperlinkAccessibilityFootnoteLine2.Visible = false;
            }
            
            //rowFootnoteLine1 - ""Change" means getting off one vehicle to board another."
			labelFootnoteLine1.Text = GetResource("ResultsFootnotes.labelFootnote1.Text");

            //rowReturnFootnoteLine1 - "You can plan a return for this journey by using the “Amend date and time” tab below."
            labelReturnFootnoteLine1.Text = GetResource("ResultsFootnotes.labelFootnote1.FindReturn.Text");
            

            //suppress note about planning a return using "amend date and time" tab for trip planner journeys
            //as they do not display that tab on the results page
            if (isTripPlannerJourney) 
            {
                rowReturnFootnoteLine1.Visible = false; 
                foreach (Control cntrl in rowReturnFootnoteLine1.Controls)
                {
                    cntrl.Visible = false;
                }
            }

            //rowFootnoteLine2 - "Also, please (re)check your journey details within ..."
            labelFootnoteLine2.Text = GetResource("ResultsFootnotes.labelFootnote2.Text");

            //rowFindFareTrainFootnoteLine1 - "Transport Direct only lists rail tickets if reservable seats are available on the service. You may..."
            labelFindFareTrainFootnoteLine1.Text = GetResource("ResultsFootnotes.labelFootnote1.FindAFare.Train.Text");

            //rowFlightFootnoteLine1 - "Flight times include check-in and an allowance for baggage reclaim."
            labelFlightFootnoteLine1.Text = GetResource("ResultsFootnotes.labelFootnote1.FindFlight.Text");

            //rowFlightFootnoteLine2 - "For flights "Leave" is the latest time you ... "
            labelFlightFootnoteLine2.Text =GetResource("ResultsFootnotes.labelFootnote2.FindFlight.Text");

            //rowFootnoteJourneyEmissionsLine1 - "CO2 emissions shows kg CO2 per traveller..."
            labelJourneyEmissionsFootnoteLine1.Text = string.Format(
                GetResource("ResultsFootnotes.labelFootnote1.JourneyEmissions.Text"),
                GetResource("ResultsFootnotes.labelFootnote1.JourneyEmissions.URL"));

            //rowJourneyTimesNote1 - "All times quoted are local."
            labelJourneyTimesNote1.Text = GetResource("ResultsFootnotes.labelJourneyTimesNote1.Text");

            //rowInternationalNotes1 - "All journey times include an estimate of city centre to airport/station transfer..."
            labelInternationalFootnote1.Text = GetResource("ResultsFootnotes.labelInternationalFootnote1.Text");

            //rowInternationalNotes2 - "Service times include check-in and an allowance for baggage reclaim."
            labelInternationalFootnote2.Text = GetResource("ResultsFootnotes.labelInternationalFootnote2.Text");

            //rowInternationalReturnFootnoteLine1 - "You can plan a return for this journey by using the return button above..."
            labelInternationalReturnFootnoteLine1.Text = GetResource("ResultsFootnotes.labelInternationalReturnFootnoteLine1.Text");

            //rowInternationalTransferFootnoteLine1 - "Using the link to plan the detail of the transfer within GB will"
            labelInternationalTransferFootnoteLine1.Text = GetResource("ResultsFootnotes.labelInternationalTransferFootnoteLine1.Text");

		}
		
		/// <summary>
		/// OnPreRender - set the visibility of the footnotes
		/// according to the FindAMode.
		/// </summary>
		protected override void OnPreRender(System.EventArgs e)
		{
			//Show/hide labels based on Single/Returns
			if (returnExists == false)
			{
				// Show/hide labels based on FindaMode
				switch (findAMode)
				{
					case FindAMode.Car :
						rowFootnoteLine1.Visible = false;
						rowFootnoteLine2.Visible = false;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowReturnFootnoteLine1.Visible = true;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
						break;
					case FindAMode.ParkAndRide :
						rowFootnoteLine1.Visible = false;
						rowFootnoteLine2.Visible = false;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowReturnFootnoteLine1.Visible = false;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
						break;
					case FindAMode.Train :
						rowFootnoteLine1.Visible = true;
						rowFootnoteLine2.Visible = true;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowReturnFootnoteLine1.Visible = true;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
						// Only show train fare footnote 
						// if come from Find A Fare
						rowFindFareTrainFootnoteLine1.Visible = costBasedResults;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
						break;
					case FindAMode.Flight :
                        rowFootnoteLine1.Visible = true;
                        rowFootnoteLine2.Visible = true;
                        rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = true;
                        rowFlightFootnoteLine1.Visible = true;
                        rowReturnFootnoteLine1.Visible = true;
                        rowFlightFootnoteLine2.Visible = true;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
                        break;

					case FindAMode.Trunk :
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
                        break;

					case FindAMode.TrunkStation :
						rowFootnoteLine1.Visible = true;
						rowFootnoteLine2.Visible = true;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = true;
						rowFlightFootnoteLine1.Visible = true;
						rowReturnFootnoteLine1.Visible = true;
						rowFlightFootnoteLine2.Visible = true;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
						break;
                    case FindAMode.Cycle :
                        rowFootnoteLine1.Visible = false;
                        rowFootnoteLine2.Visible = false;
                        rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilltyNotes.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
                        rowFlightFootnoteLine1.Visible = false;
                        rowReturnFootnoteLine1.Visible = true;
                        rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
                        break;
                    
                    case FindAMode.International:
                        rowFootnoteLine1.Visible = false;
                        rowReturnFootnoteLine1.Visible = false;
                        rowFootnoteLine2.Visible = true;
                        rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = true;
                        rowFlightFootnoteLine1.Visible = false;
                        rowFlightFootnoteLine2.Visible = true;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = true;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = true;
                        rowInternationalReturnFootnoteLine1.Visible = true;
                        rowInternationalTransferFootnoteLine1.Visible = true;
                        break;

					default :
						rowFootnoteLine1.Visible = true;
                        rowReturnFootnoteLine1.Visible = true;
						rowFootnoteLine2.Visible = true;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        rowInternationalNotes1.Visible = false;
                        rowInternationalNotes2.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = false;
                        rowInternationalTransferFootnoteLine1.Visible = false;
						break;
				}
			}
			else
			{
					// Show/hide labels based on FindaMode
				switch (findAMode)
				{
					case FindAMode.Car :
						rowFootnoteLine1.Visible = false;
						rowFootnoteLine2.Visible = false;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowReturnFootnoteLine1.Visible = false;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
						break;
					case FindAMode.Train :
						rowFootnoteLine1.Visible = true;
						rowFootnoteLine2.Visible = true;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowReturnFootnoteLine1.Visible = false;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
						// Only show train fare footnote 
						// if come from Find A Fare
                        rowFindFareTrainFootnoteLine1.Visible = costBasedResults;
                        rowJourneyTimesNote1.Visible = false;
						break;
					case FindAMode.Flight :
                        rowFootnoteLine1.Visible = true;
                        rowFootnoteLine2.Visible = true;
                        rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = true;
                        rowFlightFootnoteLine1.Visible = true;
                        rowReturnFootnoteLine1.Visible = false;
                        rowFlightFootnoteLine2.Visible = true;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        break;

					case FindAMode.Trunk :
                        rowFootnoteJourneyEmissionsLine1.Visible = true;
                        rowJourneyTimesNote1.Visible = false;
                        break;

					case FindAMode.TrunkStation :
						rowFootnoteLine1.Visible = true;
						rowFootnoteLine2.Visible = true;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = true;
						rowFlightFootnoteLine1.Visible = true;
						rowReturnFootnoteLine1.Visible = false;
						rowFlightFootnoteLine2.Visible = true;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
						break;

                    case FindAMode.Cycle:
                        rowFootnoteLine1.Visible = false;
                        rowFootnoteLine2.Visible = false;
                        rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilltyNotes.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
                        rowFlightFootnoteLine1.Visible = false;
                        rowReturnFootnoteLine1.Visible = false;
                        rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
                        break;

					default :
						rowFootnoteLine1.Visible = true;
						rowFootnoteLine2.Visible = true;
						rowReturnFootnoteLine1.Visible = false;
						rowFindFareTrainFootnoteLine1.Visible = false;
                        rowAccessibilityNotes1.Visible = false;
						rowFlightFootnoteLine1.Visible = false;
						rowFlightFootnoteLine2.Visible = false;
                        rowFootnoteJourneyEmissionsLine1.Visible = false;
                        rowJourneyTimesNote1.Visible = false;
						break;

				}

                // International journeys are only outwards
                rowInternationalReturnFootnoteLine1.Visible = false;
                rowInternationalNotes1.Visible = false;
                rowInternationalNotes2.Visible = false;
                rowInternationalTransferFootnoteLine1.Visible = false;
			}

            // Override for specific pages
            switch (controlPageId)
            {
                case PageId.JourneyOverview:
                    rowAccessibilltyNotes.Visible = false;
                    rowFootnoteLine1.Visible = false;
                    rowFindFareTrainFootnoteLine1.Visible = false;
                    rowAccessibilityNotes1.Visible = false;
                    rowFlightFootnoteLine2.Visible = false;
                    rowFootnoteJourneyEmissionsLine1.Visible = true;
                    rowJourneyTimesNote1.Visible = false;
                    rowInternationalTransferFootnoteLine1.Visible = false;
                    
                    if (findAMode == FindAMode.International)
                    {
                        rowJourneyTimesNote1.Visible = true;
                        rowInternationalNotes1.Visible = true;
                        rowInternationalNotes2.Visible = false;
                        rowReturnFootnoteLine1.Visible = false;
                        rowInternationalReturnFootnoteLine1.Visible = true;
                        rowFlightFootnoteLine1.Visible = false;
                        rowAccessibilltyNotes.Visible = true;
                    }
                    break;
                default:
                    break;
            }
		}
		
		/// <summary>
		/// Method handles the click event from the linkControl
		/// This could either come from the link button or the 
		/// image button, depending on what has been rendered.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkControl_link_Clicked(object sender, EventArgs e)
		{
			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

            // Set any modes we want displayed, rather than relying on the page to determine
            if (accessabilityModeTypes.Length > 0)
            {
                TDSessionManager.Current.InputPageState.AccessabilityModeTypes = accessabilityModeTypes;
            }
            else
            {
                // Don't specify any, let the Accessability page determine
                TDSessionManager.Current.InputPageState.AccessabilityModeTypes = new ModeType[0];
            }

			// Navigate to the Journey Accessibility page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyAccessibility;
		}

		/// <summary>
		/// Read-write property
		/// Find a mode influences the
		/// footnote info to display
		/// </summary>
		public FindAMode Mode
		{
			get { return findAMode; }
			set { findAMode = value; }
		}

		/// <summary>
		/// Read-write property
		/// Are the results from a cost-based
		/// search? Influences the
		/// footnote info to display
		/// </summary>
		public bool CostBasedResults
		{
			get { return costBasedResults; }
			set { costBasedResults = value; }
		}

		/// <summary>
		/// Read-write property
		/// Does a return exist?
		/// Influences the
		/// footnote info to display
		/// </summary>
		public bool ReturnExists
		{
			get { return returnExists; }
			set { returnExists = value; }
		}

		/// <summary>
		/// Read-write property
		/// Is this Find ParkandRide?
		/// Influences the
		/// footnote info to display
		/// </summary>
		public bool isParkRide
		{
			get { return isParkandRide; }
			set { isParkandRide = value; }
		}

        /// <summary>
        /// Read-write property
        /// Is this Trip Planner?
        /// Influences the
        /// footnote info to display
        /// </summary>
        public bool IsTripPlanner
        {
            get { return isTripPlannerJourney; }
            set { isTripPlannerJourney = value; }
        }

        /// <summary>
        /// Read-write property
        /// PageID to influence the footnote display
        /// </summary>
        public PageId controlPageID
        {
            get { return controlPageId; }
            set { controlPageId = value; }
        }

        /// <summary>
        /// Read/Write property to list the modes types to be shown on the Accessability page when 
        /// the accessability modes for journey link it clicked
        /// </summary>
        public ModeType[] AccessabilityModeTypes
        {
            get { return accessabilityModeTypes; }
            set { accessabilityModeTypes = value; }
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			linkControl.link_Clicked += new EventHandler(linkControl_link_Clicked);
		}
		#endregion

	}
}
