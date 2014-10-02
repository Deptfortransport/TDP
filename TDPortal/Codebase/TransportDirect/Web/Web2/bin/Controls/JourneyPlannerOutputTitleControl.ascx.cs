// *********************************************** 
// NAME                 : JourneyPlannerOutputTitle.aspx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 20.09.04
// DESCRIPTION			: Used to display page title on a journey
// planner output page. Text is determined based on the current
// output page id and the state of the itinerary manager.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyPlannerOutputTitleControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 27 2010 15:46:30   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.4   Mar 29 2010 18:19:40   mmodi
//Display title on cycle pages
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.3   Oct 13 2008 16:44:16   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Aug 22 2008 10:33:36   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:21:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:44   mturner
//Initial revision.
//
//   Rev Devfactory   Jan 20 2008 19:00:00   dgath
//CCN0382b City to City enhancements:
//Updated to show mode of transport as part of title of page, e.g. "Summary of Coach journey options"
//when used on City to City summary page
//
//   Rev 1.8   Nov 13 2006 12:49:26   dsawe
//added condition FindAMode.RailCost for displaying the label 
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.7   Feb 23 2006 19:16:54   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:25:54   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Aug 16 2005 14:55:54   jgeorge
//Automatically merged from branch for stream2556
//
//   Rev 1.5.1.0   Jun 29 2005 10:07:34   jbroome
//Updated for Journey Accessibility page
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.5   May 23 2005 17:18:16   rgreenwood
//IR2500: FindAFare JourneyDetails and JourneyMap now nav back to previous FindAFare step, as per JourneySummary
//Resolution for 2500: PT - Back Button Missing in Find-a-Fare
//
//   Rev 1.4   Apr 29 2005 15:01:34   rgeraghty
//Entries added for Ticket Retailers
//Resolution for 2345: PT: Cosmetic faults on Ticket Retailer page
//
//   Rev 1.3   Apr 08 2005 16:11:54   rhopkins
//Change PageId for PrintableJourneyMap so that it is compatible with the PrinterFriendlyPageButtonControl
//
//   Rev 1.2   Feb 25 2005 16:51:12   COwczarek
//Add remaining Find Fare ticket selection pages
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.1   Feb 25 2005 12:28:28   rhopkins
//Added handling for titles of output pages when in Find A Fare mode.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.0   Sep 20 2004 16:28:24   COwczarek
//Initial revision.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
    using TransportDirect.Common.ResourceManager;
	using System.Web.UI.WebControls;
    using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
    using TransportDirect.JourneyPlanning.CJPInterface;
    using System.Collections.Generic;

	/// <summary>
    /// Used to display page title on a journey
    /// planner output page. Text is determined based on the current
    /// output page id and the state of the itinerary manager.
	/// </summary>
	public partial  class JourneyPlannerOutputTitle : TDUserControl
	{
        private ModeType[] modeTypes;

        /// <summary>
        /// Read/write property. The Mode to be displayed in the title
        /// e.g. Summary of Rail journey options
        /// </summary>
        public ModeType[] ModeTypes
        {
            get { return modeTypes; }
            set { modeTypes = value; }
        }

        /// <summary>
        /// Returns text determined based on the current
        /// output page id and the state of the itinerary manager.
        /// </summary>
        /// <returns>Output page title text</returns>
        private string deriveTitleString() 
        {
            string titleString = string.Empty;

            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            if (itineraryManager.Length > 0) 
            {
                if (itineraryManager.ExtendInProgress) 
                {
                    switch (PageId) 
                    {
                        case (PageId.JourneySummary):
                        case (PageId.PrintableJourneySummary):
                            titleString = GetResource("JourneyPlannerOutputTitle.SummaryOfExtensionsText");
                            break;
                        case (PageId.JourneyDetails):
                        case (PageId.PrintableJourneyDetails):
                            titleString = GetResource("JourneyPlannerOutputTitle.DetailsOfExtensionsText");
                            break;
                        case (PageId.JourneyMap):
                        case (PageId.PrintableJourneyMap):
                            titleString = GetResource("JourneyPlannerOutputTitle.MapsOfExtensionsText");
                            break;
                        case (PageId.JourneyFares):
                        case (PageId.PrintableJourneyFares):
                            titleString = GetResource("JourneyPlannerOutputTitle.FaresForExtensionsText");
                            break;
                        case (PageId.TicketRetailers):
                        case (PageId.PrintableTicketRetailers):
                            titleString = GetResource("JourneyPlannerOutputTitle.RetailersForExtensionsText");
                            break;
						case (PageId.JourneyAccessibility):
							titleString = GetResource("JourneyPlannerOutputTitle.AccessibilityText");
							break;
                    }
                } 
                else 
                {
					if (PageId == PageId.JourneyAccessibility)
					{
						titleString = GetResource("JourneyPlannerOutputTitle.AccessibilityText");
					}
					else if (TDItineraryManager.Current.Length == 1) 
                    {
                        titleString = GetResource("JourneyPlannerOutputTitle.ExtendText");
                    } 
                    else 
                    {
                        titleString = GetResource("JourneyPlannerOutputTitle.ExtendedText");
                    }                    
                }
            } 
            else 
            {
				ITDSessionManager tdSessionManager = TDSessionManager.Current;
				if ((tdSessionManager.FindAMode == FindAMode.Fare) || (tdSessionManager.FindAMode == FindAMode.TrunkCostBased)
					|| tdSessionManager.FindAMode == FindAMode.RailCost)
				{
					switch (PageId)
					{
						case (PageId.FindFareDateSelection):
						case (PageId.PrintableFindFareDateSelection):
							titleString = GetResource("JourneyPlannerOutputTitle.FindFareDateSelectionText");
							break;
						case (PageId.FindFareTicketSelection):
                        case (PageId.FindFareTicketSelectionReturn):
                        case (PageId.FindFareTicketSelectionSingles):
                        case (PageId.PrintableFindFareTicketSelection):
                        case (PageId.PrintableFindFareTicketSelectionReturn):
                        case (PageId.PrintableFindFareTicketSelectionSingles):
                            titleString = GetResource("JourneyPlannerOutputTitle.FindFareTicketSelectionText");
							break;
						case (PageId.JourneySummary):
						case (PageId.PrintableJourneySummary):
						case (PageId.JourneyDetails):
						case (PageId.PrintableJourneyDetails):
						case (PageId.JourneyMap):
						case (PageId.PrintableJourneyMap):
							titleString = GetResource("JourneyPlannerOutputTitle.FindFareSummaryOfOptionsText");
							break;
						case (PageId.TicketRetailers):
						case (PageId.PrintableTicketRetailers):
							titleString = GetResource("JourneyPlannerOutputTitle.FindFareTicketRetailersText");
							break;
						case (PageId.JourneyAccessibility):
							titleString = GetResource("JourneyPlannerOutputTitle.AccessibilityText");
							break;
					}
				}
				else
				{
					switch (PageId) 
					{
						case (PageId.JourneySummary):
                        case (PageId.CycleJourneySummary):
						case (PageId.PrintableJourneySummary):
                        case (PageId.JourneyOverview):
                        case (PageId.PrintableJourneyOverview):
                           titleString = string.Format ( GetResource("JourneyPlannerOutputTitle.SummaryOfOptionsText"), determineTitleModeType());
							break;
						case (PageId.JourneyDetails):
                        case (PageId.CycleJourneyDetails):
						case (PageId.PrintableJourneyDetails):
                        case (PageId.PrintableCycleJourneyDetails):
							titleString = GetResource("JourneyPlannerOutputTitle.DetailsText");
							break;
						case (PageId.JourneyMap):
                        case (PageId.CycleJourneyMap):
						case (PageId.PrintableJourneyMap):
                        case (PageId.PrintableJourneyMapTile):
							titleString = GetResource("JourneyPlannerOutputTitle.MapsText");
							break;
						case (PageId.JourneyFares):
						case (PageId.PrintableJourneyFares):
							titleString = GetResource("JourneyPlannerOutputTitle.FaresText");
							break;
						case (PageId.TicketRetailers):
						case (PageId.PrintableTicketRetailers):
							titleString = GetResource("JourneyPlannerOutputTitle.TicketRetailersText");
							break;
						case (PageId.JourneyAccessibility):
							titleString = GetResource("JourneyPlannerOutputTitle.AccessibilityText");
							break;
					}
				}
            }

            return titleString;
        }

        /// <summary>
        /// Returns mode title text dependent on the ModesType property value
        /// </summary>
        /// <returns></returns>
        private string determineTitleModeType()
        {
            if ((modeTypes != null) && (modeTypes.Length > 0))
            {
                List<ModeType> listModeTypes = new List<ModeType>(modeTypes.Length);
                listModeTypes.AddRange(modeTypes);

                if (listModeTypes.Contains(ModeType.Air))
                    return GetResource("JourneyPlannerOutputTitle.AirJourneysText");
                
                else if (listModeTypes.Contains(ModeType.Coach))
                    return GetResource("JourneyPlannerOutputTitle.CoachJourneysText");
                
                else if (listModeTypes.Contains(ModeType.Rail))
                    return GetResource("JourneyPlannerOutputTitle.RailJourneysText");
                
                else if (listModeTypes.Contains(ModeType.Car))
                    return GetResource("JourneyPlannerOutputTitle.CarJourneysText");
                
            }
            
            return string.Empty;  //Default
        }

        /// <summary>
        /// Event handler for page PreRender event fired by page
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            labelTitle.Text = deriveTitleString();
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
        }

		#endregion

	}
}