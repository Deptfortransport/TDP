// *********************************************************************** 
// NAME                 : JourneyFaresControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 24/01/2005
// DESCRIPTION			: Displays the journey fares in either diagrammatic 
//						or tabular format
// ************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyFaresControl.ascx.cs-arc  $
//
//   Rev 1.8   Mar 26 2010 11:37:48   mmodi
//Populate CJP user flag to allow debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.7   Feb 27 2009 11:21:22   mmodi
//Removed a todo: comment and unnecessary code
//
//   Rev 1.6   Feb 26 2009 17:26:22   mmodi
//Added a label when there are multiple legs with travelcards
//Resolution for 5266: ZPBO - Multiple Travelcards when journey has multiple legs
//
//   Rev 1.5   Feb 03 2009 12:34:12   apatel
//Local zonal fares hyperlinks are offered if the journey selected includes a rail leg, but they are not offered if the journey selected does not include a rail leg. Resolve the problem of local zonal fares hyperlinks to show even the journey does not include a rail leg.
//Resolution for 5230: Journey Fares page - Local zonal fares hyperlinks problem
//
//   Rev 1.4   Feb 02 2009 17:09:00   mmodi
//New Break of journey note for non routing guide compliant journeys
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.3   Jul 11 2008 11:17:34   pscott
//5051 find a flight errors
//
//   Rev 1.2   Mar 31 2008 13:21:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:38   mturner
//Initial revision.
//
//   Rev 1.37   Aug 06 2007 15:18:00   asinclair
//No longer display Return fares in Return Leg if no outward fares can be displayed.
//Resolution for 4455: 9.7 - Return coach fares - Return fares displayed in Return leg when no outward fare found.
//
//   Rev 1.36   Jun 06 2007 16:04:20   asinclair
//Updated SingleJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.35   Jun 06 2007 12:45:16   asinclair
//Added SingleJourneyNewFares
//Resolution for 4432: NX Fares: Outward only journey must not display Return fares
//
//   Rev 1.34   May 25 2007 16:22:20   build
//Automatically merged from branch for stream4401
//
//   Rev 1.33.1.0   May 22 2007 13:20:32   mmodi
//Added test for New and Old coach fares, setting the appropriate flag properties
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.33   May 03 2007 09:56:58   asinclair
//Added fix for train/coach fare issues
//
//   Rev 1.32   May 02 2007 13:51:30   asinclair
//labelAdditionalNote.visible = false removed
//
//   Rev 1.31   Apr 26 2007 15:41:44   asinclair
//if nothroughfares then do not display buttonChangeView
//Resolution for 4395: 9.5 - Improved Rail / Local Zonal - Cosmetic Issues
//
//   Rev 1.30   Apr 23 2007 14:55:14   asinclair
//Set returnfaresincluded for faresDetailsDiagram control
//
//   Rev 1.29   Apr 18 2007 16:05:44   dsawe
//updated to disable show in diagram button when no fares found
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.28   Apr 18 2007 12:04:24   asinclair
//Merge from branch
//
//   Rev 1.27.1.0   Apr 13 2007 08:59:52   asinclair
//Added code to set ItineraryAdapter on FaresDetailsTable
//Resolution for 4385: 9.5 - Improved Rail / Local Zonal - Issues with Return Displays
//
//   Rev 1.27   Apr 04 2007 21:53:30   asinclair
//Updated for local zonal services
//
//   Rev 1.26   Apr 04 2007 18:32:24   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.25   Apr 04 2007 14:06:44   asinclair
//Removed unused code
//
//   Rev 1.24   Mar 29 2007 22:20:58   asinclair
//Further fixes for integration of LocalZonal and Improved Rail Fares
//
//   Rev 1.23   Mar 28 2007 15:55:58   asinclair
//Fixes for integration of LocalZonalServices and ImprovedRailFares
//
//   Rev 1.22   Mar 21 2007 14:09:54   asinclair
//Updated to fix Improved Rail Fares bugs
//
//   Rev 1.21   Mar 19 2007 18:39:28   asinclair
//Updated for stream4362
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.20   Mar 06 2007 13:43:58   build
//Automatically merged from branch for stream4358
//
//   Rev 1.19.1.0   Mar 02 2007 11:51:44   asinclair
//Updated existing methods to allow the display of cheaper single/return fares as required
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.19   Apr 28 2006 16:59:32   RPhilpott
//Change Find Cheaper link from ImageButton to HyperlinkPostbackControl.
//Resolution for 4037: DD075: Find Cheaper: Cheaper hyperlink should use new style
//
//   Rev 1.18   Mar 17 2006 15:21:18   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.17   Mar 14 2006 10:30:16   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.16   Feb 23 2006 16:12:10   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.15.1.2   Mar 08 2006 16:12:40   RGriffith
//Changes to display "Fares included above" message correctly in RefineTickets page
//
//   Rev 1.15.1.1   Mar 01 2006 13:17:00   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.15.1.0   Feb 23 2006 11:27:46   RGriffith
//Initial Changes for new Tickets/Costs functionality
//
//   Rev 1.15   Nov 28 2005 14:36:06   NMoorhouse
//Set Cheaper Fares button to look like a hyperlink
//Resolution for 3138: UEE (CG): "Cheaper Fares" link is button, not link
//
//   Rev 1.14   Nov 03 2005 17:04:24   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.13.1.0   Oct 20 2005 16:24:38   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.13   Apr 25 2005 12:20:18   rgeraghty
//JourneyDirection text setting updated
//Resolution for 2295: Del 7 - PT - Server error selecting full extended journey from tickets/costs page
//
//   Rev 1.12   Apr 24 2005 17:03:32   rgeraghty
//updated comment on FullItinerarySelected property
//Resolution for 2295: Del 7 - PT - Server error selecting full extended journey from tickets/costs page
//
//   Rev 1.11   Apr 24 2005 17:01:48   rgeraghty
//Added FullItinerarySelected property to enable update of  labelJourneyDirection when full itinerary is selected
//Resolution for 2295: Del 7 - PT - Server error selecting full extended journey from tickets/costs page
//
//   Rev 1.10   Apr 05 2005 14:06:24   rgeraghty
//fx cop changes, plus commenting
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.9   Apr 04 2005 16:37:18   rgeraghty
//Added alt text to image
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.8   Mar 22 2005 08:35:28   rgeraghty
//Corrected visibility of controls
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.7   Mar 15 2005 18:16:32   esevern
//checks for itinerary adapter being null - it will be if car costs rather than fares are being displayed
//
//   Rev 1.6   Mar 14 2005 15:45:52   rgeraghty
//Added More Help functionality and event to handle info button click
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Mar 04 2005 11:18:50   rgeraghty
//Added additional image alt text
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 03 2005 17:57:52   rgeraghty
//FxCop changes made
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Mar 01 2005 15:04:44   rgeraghty
//First version
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.2   Feb 11 2005 14:58:18   rgeraghty
//Work in progress
//
//   Rev 1.1   Feb 09 2005 10:16:50   rgeraghty
//Work in progress
//
//   Rev 1.0   Jan 25 2005 17:19:02   rgeraghty
//Initial revision.


namespace TransportDirect.UserPortal.Web.Controls
{
	#region .NET namespaces
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.PricingRetail.Domain;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using CJPInterfaceAlias = TransportDirect.JourneyPlanning.CJPInterface;
	#endregion

	/// <summary>
	///	Displays journey fares in either diagrammatic or tabular format
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyFaresControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		#region controls

		protected TransportDirect.UserPortal.Web.Controls.FareDetailsTableControl fareDetailsTable;
		protected TransportDirect.UserPortal.Web.Controls.FareDetailsDiagramControl fareDetailsDiagram;
		protected System.Web.UI.WebControls.Label journeyLabel;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl findCheaperLinkControl;
		

		#endregion

		#region private members

		private bool inTableMode;
		private bool isForReturn;	
		private bool showChildFares;		
		private bool fullItinerarySelected;
        private bool showBreakOfJourney;
		private string railDiscount = string.Empty;
		private string coachDiscount =string.Empty;
		private ItineraryAdapter itineraryAdapter;
		private bool inErrorMode;		
		private bool hideTicketSelection;
		private bool hideHelpAndHeaderLabels;
		private string startLocation;
		private string endLocation;
		protected System.Web.UI.WebControls.ImageButton imageButtonFindCheaper;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl viewOtherFareLinkControl;

		/// <summary>
		/// Stores the selected tickets for a journey. PricingUnit objects are used for
		/// the keys and Ticket objects for the values
		/// </summary>
		private Hashtable selectedTicketsHash;

        /// <summary>
        /// Flag to allow additional debug info to be displayed on screen if logged on user has CJP status
        /// </summary>
        private bool cjpUser = false;

		#endregion

		#region Events
		/// <summary>
		/// Event raised when the user clicks an "Info" button on table segment control
		/// </summary>
		public event EventHandler InfoButtonClicked;

		/// <summary>
		/// Event raised when the user clicks to view single or return fares.
		/// </summary>
		public event EventHandler OtherFaresClicked;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public JourneyFaresControl()
		{
			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
			selectedTicketsHash = new Hashtable();
		}

		#endregion

		#region EventHandlers

		/// <summary>
		/// Event Handler for when info button of table segment control is clicked within table control
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsTable_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		/// <summary>
		/// Event Handler for when info button of table segment control is clicked within diagram control
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void fareDetailsDiagram_InfoButtonClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = InfoButtonClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		/// <summary>
		/// Page Load event
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// check itinerary adapter - will be null if we're displaying 
			// car costs, not fares, so don't continue with display
			if(itineraryAdapter != null) 
			{
				SetControlText();	
				InitialiseControls();					
				PopulateControl();
			}
		}

		/// <summary>
		/// PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{	
			// check itinerary adapter - will be null if we're displaying 
			// car costs, not fares, so don't continue with display
			if(itineraryAdapter != null) 
			{
				SetControlText();
				InitialiseControls();			
				PopulateControl();
			}
		}

		#endregion

		#region private methods

		/// <summary>
		/// Set the properties of the control's controls
		/// </summary>
		private void InitialiseControls()
		{					
			//set the properties of the table control
			fareDetailsTable.PrinterFriendly = this.PrinterFriendly;
			fareDetailsTable.ShowChildFares = this.ShowChildFares;
			fareDetailsTable.OverrideItineraryType = itineraryAdapter.OverrideItineraryType;
			fareDetailsDiagram.OverrideItineraryType = itineraryAdapter.OverrideItineraryType;

			fareDetailsTable.RailDiscount = this.RailDiscount;
			fareDetailsTable.CoachDiscount = this.CoachDiscount;
			fareDetailsTable.InfoButtonClicked +=new EventHandler(fareDetailsTable_InfoButtonClicked);
			fareDetailsTable.OtherFaresClicked +=new EventHandler(fareDetailsTable_OtherFaresClicked);

            fareDetailsTable.CJPUser = this.CJPUser;

			//set the properties of the diagram control			
			fareDetailsDiagram.PrinterFriendly = this.PrinterFriendly;
			fareDetailsDiagram.ShowChildFares = this.ShowChildFares;	

			fareDetailsDiagram.RailDiscount = this.RailDiscount;
			fareDetailsDiagram.CoachDiscount = this.CoachDiscount;
			fareDetailsDiagram.InfoButtonClicked +=new EventHandler(fareDetailsDiagram_InfoButtonClicked);
			fareDetailsDiagram.OtherFaresClicked += new EventHandler(fareDetailsDiagram_OtherFaresClicked);

			//Hide the diagram control if in tablemode
			if (inTableMode)
			{
				fareDetailsDiagram.Visible=false;
				fareDetailsTable.Visible =true;								
				
				//update the change view button text
				if (isForReturn)
					buttonChangeView.Text= GetResource("JourneyFaresControl.buttonShowDiagram.Return.Text");
				else
					buttonChangeView.Text= GetResource("JourneyFaresControl.buttonShowDiagram.Outward.Text");		

			}
			else //Show the diagram control if in diagram mode
			{
				fareDetailsDiagram.Visible=true;
				fareDetailsTable.Visible =false;
								
				//update the change view button text
				if (isForReturn)
					buttonChangeView.Text= GetResource("JourneyFaresControl.buttonShowTable.Return.Text");
				else
					buttonChangeView.Text= GetResource("JourneyFaresControl.buttonShowTable.Outward.Text");
				
			}

			//set controls visibility according to printer friendly property
			hyperlinkAmendFareDetails.Visible = (!this.PrinterFriendly && !this.hideHelpAndHeaderLabels);
			hyperlinkImageAmendFareDetails.Visible = (!this.PrinterFriendly && !this.hideHelpAndHeaderLabels);
			helpJourneyFaresControl.Visible = (!this.PrinterFriendly && !this.hideHelpAndHeaderLabels);
			buttonChangeView.Visible = (!this.PrinterFriendly && !this.hideHelpAndHeaderLabels);

			// Set location label
			if (this.hideHelpAndHeaderLabels)
			{
				labelLocations.Visible = true;
				labelLocations.Text = startLocation + GetResource("RefineTickets.LocationSeperator.Text") + endLocation;
			}
			else
			{
				labelLocations.Visible = false;
			}

			fareDetailsTable.HideTicketSelection = this.hideTicketSelection;

			//hide the find cheaper buttons if in printer friendly mode
			if (this.PrinterFriendly)
				ShowFindCheaper(false);						
		}

		/// <summary>
		/// Sets the control's labels' text
		/// </summary>
		private void SetControlText()
		{
			labelNoFareInformation.Text= GetResource("JourneyFaresControl.NoFaresInformationAvailable.Text");
			labelNoThroughFare.Text = GetResource("JourneyFaresControl.NoThroughFares");

			labelView.Text = GetResource("JourneyFaresControl.labelViewText");

			labelSeeOutwardFares.Text = GetResource("JourneyFaresControl.labelSeeOutwardFaresText");
			// Note: this label is a copy of LabelSeeOutwardFares but is displayed differently in RefineTickets.aspx
			labelFaresIncludedAbove.Text = GetResource("JourneyFaresControl.labelSeeOutwardFaresText");

			labelNoSelectionError.Text = GetResource("JourneyFaresControl.labelNoSelectionErrorText");			

			labelFindCheaperPrefix.Text = GetResource("JourneyFaresControl.labelFindCheaperPrefixText");
			labelFindCheaperSuffix.Text = GetResource("JourneyFaresControl.labelFindCheaperSuffixText");

            /// CCN 0427 setting text for the button retailer
            buttonRetailers.Text = GetResource("JourneyFaresControl.buttonRetailers.Text");

			findCheaperLinkControl.Text = GetResource("JourneyFaresControl.findCheaperLinkText");
		
			if (hideHelpAndHeaderLabels)
				labelJourneyDirection.Text = GetResource("JourneyFaresControl.labelJourneyDirection.Text");
			else
				if ((fullItinerarySelected) && (!isForReturn))
				labelJourneyDirection.Text = GetResource("JourneyFaresControl.labelFullItineraryTitle.Text");			
			else 
				if (isForReturn)
				labelJourneyDirection.Text = GetResource("JourneyFaresControl.labelReturnJourneyDirectionText");			
			else
				labelJourneyDirection.Text = GetResource("JourneyFaresControl.labelOutwardJourneyDirectionText");

			labelFaresExplanation.Text = GetResource("JourneyFaresControl.labelFaresExplanationText");
            labelNotePrefix.Text = GetResource("JourneyFaresControl.labelLabelNotePrefix");
			labelAdditionalNote.Text = GetResource(	"JourneyFaresControl.labelFaresAdditionalNoteText");
            labelSingleTicketsNote.Text = GetResource("JourneyFaresControl.labelSingleTicketsNote");
            labelMultipleTravelcardsNote.Text = GetResource("JourneyFaresControl.labelMultipleTravelcardsNote");
            labelBreakOfJourney.Text = GetResource("JourneyFaresControl.labelBreakOfJourney");

			hyperlinkAmendFareDetails.Text = GetResource("JourneyFaresControl.hyperlinkAmendFareDetailsText");
			hyperlinkImageAmendFareDetails.ImageUrl = GetResource("JourneyFaresControl.hyperlinkAmendFareDetails.ImageUrl");
			hyperlinkImageAmendFareDetails.AlternateText = GetResource("JourneyFaresControl.hyperlinkImageAmendFareDetails.AlternateText");
			
			helpJourneyFaresControl.AlternateText = GetResource("JourneyFaresControl.helpJourneyFaresControl.AlternateText");				
			helpJourneyFaresLabelControl.MoreButton.Visible = true;
			helpJourneyFaresLabelControl.MoreHelpUrl = GetResource("JourneyFaresControl.helpJourneyFaresLabelControl.moreURL");				
			
			if (inTableMode)
				helpJourneyFaresLabelControl.Text = GetResource("JourneyFaresControl.helpJourneyFaresLabelControl.TableText");				
			else
				helpJourneyFaresLabelControl.Text = GetResource("JourneyFaresControl.helpJourneyFaresLabelControl.DiagramText");				

		}
		
		/// <summary>
		/// Populate the fareDetailsTable or fareDetailsDiagram control
		/// </summary>
		private void PopulateControl()
		{

			//Set panel default displays
			
			//show the faresPanel by default
			faresPanel.Visible = true;

			//show the faresDisplay panel by default which contains the diagram and table controls
			faresDisplayPanel.Visible = true;

			//hide the matchingReturnFaresPanel by default
			matchingReturnFaresPanel.Visible = false;
			
			//show the explanation text by default
			labelFaresExplanation.Visible = !hideHelpAndHeaderLabels;
	
			labelNoThroughFare.Visible = false;

			//labelNoReturnFares.Visible = false;
			labelView.Visible = false;
			viewOtherFareLinkControl.Visible = false;

			//hide the no fare info label by default
			labelNoFareInformation.Visible = false;		

			//show the matching return text by default
			labelSeeOutwardFares.Visible= true;
			// Note: this label is a copy of LabelSeeOutwardFares but is displayed differently in RefineTickets.aspx
			labelFaresIncludedAbove.Visible = false;

            //Set div to visible first, so contained labels are updated correctly
            divNotes.Visible = true; 

			//Show the additional note only if there is a leg with missing fare information
			labelAdditionalNote.Visible = itineraryAdapter.IsFareMissing(isForReturn);

            //Show the single ticket is not valid for whole journey message
            labelSingleTicketsNote.Visible = itineraryAdapter.IsJourneyRoutingGuideCompliant(isForReturn) ? false : true;

            //Show the multiple travelcards message
            labelMultipleTravelcardsNote.Visible = itineraryAdapter.DoesJourneyContainMultipleTravelcards(isForReturn);

            //Set up the notes container visibility
            labelNotePrefix.Visible = (labelAdditionalNote.Visible || labelSingleTicketsNote.Visible || labelMultipleTravelcardsNote.Visible);
            divNotes.Visible = labelNotePrefix.Visible;
		
			//set the visibility of the validation message depending on whether currently in error mode
			labelNoSelectionError.Visible = inErrorMode;

            //Set the break of journey to be visible when the journey goes into the next day, or an
            //interchange time is greater than a parameter
            labelBreakOfJourney.Visible = showBreakOfJourney;

			//evaluate whether the journey contains pricing units
			bool isJourneyPriced = itineraryAdapter.IsJourneyPriced(isForReturn);

			bool noThroughFares = itineraryAdapter.NoThroughFares(false);

			if (!isJourneyPriced || noThroughFares)
			{
				labelFaresExplanation.Visible = false;
				buttonChangeView.Visible= false;
			}
			PopulateFaresControls();
			
		}

		/// <summary>
		/// Populate the fares controls dependant on the user's selected view
		/// </summary>
		private void PopulateFaresControls()
		{
			//table view
			switch (inTableMode)
			{									
				case true:
								
					PricingUnit[] pricingUnits = null;
				
					// if the return control is being dealt with, use the Return pricing units
					if (isForReturn)
					{	
						if (itineraryAdapter.Adaptee.ReturnJourney != null )
						{
							IList returnPricingUnits= itineraryAdapter.ReturnPricingUnits;
							pricingUnits =  new PricingUnit[returnPricingUnits.Count];

							for (int i =0; i< returnPricingUnits.Count;i++)							
							{
								pricingUnits[i] = (PricingUnit)returnPricingUnits[i];	
							}	

							bool isJourneyPriced = itineraryAdapter.IsJourneyPriced(isForReturn);

							//If the outward journey doesn't contain fares, then remove the return
							//fares from the ReturnPricingUnits (as these are not the correct A to B
							//fares, but the B to A fares - this causes issues for retail handoff)
							if(!itineraryAdapter.DoesJourneyContainFares(false) && isJourneyPriced)
							{
								for (int i =0; i< returnPricingUnits.Count;i++)							
								{
									if(pricingUnits[i].ReturnFares != null)
									{
										pricingUnits[i].ReturnFares.Tickets.Clear();
									}	
								}
							}
								
							fareDetailsTable.PricedJourney = itineraryAdapter.GetReturnPricedJourney();
					
							fareDetailsTable.ItineraryAdapter = this.itineraryAdapter;

							//If the Journey is a single Coach journey, returning new fares, then the Return Fare components
							//must not be displayed to the user.
							if(SingleJourneyNewFares(true))
							{
								fareDetailsTable.ReturnCoachJourneyNewFares = true;
							}


							//check if there is any fare information to display
							if (itineraryAdapter.DoesJourneyContainFares(isForReturn))
							{
								fareDetailsTable.ReturnFaresIncluded = true;
								fareDetailsDiagram.ReturnFaresIncluded = true;
							}
							else
							{
								fareDetailsTable.ReturnFaresIncluded = false;
								buttonChangeView.Visible = false;
								labelFaresExplanation.Visible = false;

							}
							if (itineraryAdapter.NoThroughFares(false))
							{
								fareDetailsTable.ReturnNoThroughFares = true;
								// disable show in diagram buttton when no throughfares
								buttonChangeView.Visible = false;
							}
						}
						
						else
						{
							faresPanel.Visible = false;
							this.Visible= false;
						}

						fareDetailsTable.PricingUnits  = pricingUnits;	
						fareDetailsTable.SelectedTickets = this.selectedTicketsHash;

					}
					else // outbound only journey
					{
						if (itineraryAdapter.Adaptee.OutwardJourney != null ||  pricingUnits == null )
						{
							IList outwardPricingUnits= itineraryAdapter.OutwardPricingUnits;
							pricingUnits =  new PricingUnit[outwardPricingUnits.Count];
							for (int i =0; i< outwardPricingUnits.Count;i++)							
							{
								pricingUnits[i] = (PricingUnit)outwardPricingUnits[i];	
							}																	
						}

						//If a train journey and the return journey does not contain fares then 
						//do not display the label or the button, as the user will need to switch to view
						//single fares.
                        if (pricingUnits.Length > 0)
                        {
                            if ((!itineraryAdapter.DoesJourneyContainFares(true)) && (pricingUnits[0].Mode == CJPInterfaceAlias.ModeType.Rail || pricingUnits[0].Mode == CJPInterfaceAlias.ModeType.RailReplacementBus))
                            {
                                buttonChangeView.Visible = false;
                                labelFaresExplanation.Visible = false;
                            }
                        }
                       
						
						if (!itineraryAdapter.DoesJourneyContainFares(isForReturn)) 
						{
							buttonChangeView.Visible = false;
							labelFaresExplanation.Visible = false;
						}

						//If the Journey is a single Coach journey, returning new fares, then the Return Fare components
						//must not be displayed to the user.
						if(SingleJourneyNewFares(false))
						{
							fareDetailsTable.SingleCoachJourneyNewFares = true;
						}
				
						fareDetailsTable.PricingUnits  = pricingUnits;	
						fareDetailsTable.SelectedTickets = this.selectedTicketsHash;
                        
                        fareDetailsTable.PricedJourney = itineraryAdapter.GetOutwardPricedJourney();
                        
					}

					break;

				case false: // diagram mode

					PricedJourneySegment[] pricedJourney = null;
				
					if (isForReturn)
					{					
						if (itineraryAdapter.Adaptee.ReturnJourney != null)
							pricedJourney = itineraryAdapter.GetReturnPricedJourney();
						
						IList returnPricingUnits= itineraryAdapter.ReturnPricingUnits;
						pricingUnits =  new PricingUnit[returnPricingUnits.Count];

						for (int i =0; i< returnPricingUnits.Count;i++)							
						{
							pricingUnits[i] = (PricingUnit)returnPricingUnits[i];	
						}	

						bool isJourneyPriced = itineraryAdapter.IsJourneyPriced(isForReturn);

						//If the outward journey doesn't contain fares, then remove the return
						//fares from the ReturnPricingUnits (as these are not the correct A to B
						//fares, but the B to A fares - this causes issues for retail handoff)
						if(!itineraryAdapter.DoesJourneyContainFares(false) && isJourneyPriced)
						{
							for (int i =0; i< returnPricingUnits.Count;i++)							
							{
								if(pricingUnits[i].ReturnFares != null)
								{
									pricingUnits[i].ReturnFares.Tickets.Clear();
								}	
							}
						}				
		
						//if returned list is empty hide the faresPanel and show the matchingReturnFaresPanel
						if ((pricedJourney != null) && (pricedJourney.Length==0))
						{
							faresPanel.Visible = false;
							matchingReturnFaresPanel.Visible = true;
						}
						else
							if (pricedJourney == null)							
							this.Visible = false;
						else						
						{
							if (itineraryAdapter.DoesJourneyContainFares(isForReturn))
							{
								fareDetailsDiagram.ReturnFaresIncluded = true;

							}
							fareDetailsDiagram.PricedJourney= pricedJourney;														
						}



						if(SingleJourneyNewFares(true))
						{

							//fareDetailsDiagram.SingleCoachJourneyNewFares = true;
							fareDetailsDiagram.ReturnCoachJourneyNewFares = true;

						}

						fareDetailsDiagram.SelectedTickets = this.selectedTicketsHash;
					
					}
					else //outbound journey only
						if (itineraryAdapter.Adaptee.OutwardJourney != null)
						fareDetailsDiagram.PricedJourney = itineraryAdapter.GetOutwardPricedJourney();
					
					//set the selected tickets property on the diagram control - this is needed for viewstate purposes
					fareDetailsDiagram.SelectedTickets = this.selectedTicketsHash;

					//If the Journey is a single Coach journey, returning nwe fares, then the Return Fare components
					//must not be displayed to the user.
					if(SingleJourneyNewFares(false))
					{

						fareDetailsDiagram.SingleCoachJourneyNewFares = true;

					}

					break;
			}

			// Test for the new and old coach fares and 
			fareDetailsTable.NewAndOldCoachFares = JourneyContainsNewAndOldCoachFares(isForReturn);
			fareDetailsDiagram.NewAndOldCoachFares = fareDetailsTable.NewAndOldCoachFares; 
		}
	

		/// <summary>
		/// Determines whether a journey uses New NX fares for the Outward or Return journey
		/// by examing the PricingUnit.CoachFaresReturnComponent property. 
		/// Performs XOR to only return True if Outward OR the Return journey contains new NX fares
		/// </summary>
		/// <returns></returns>
		private bool JourneyContainsNewAndOldCoachFares(bool isForReturn)
		{
			bool outwardFaresNew = false;
			bool returnFaresNew = false;

			// Only need to test if this control is for the Return journey
			if (isForReturn)
			{
				PricingUnit[] pricingUnitsOutward = null;
				PricingUnit[] pricingUnitsReturn = null;

				if (itineraryAdapter.Adaptee.OutwardJourney != null)
				{
					IList outwardPricingUnits = itineraryAdapter.OutwardPricingUnits;
					pricingUnitsOutward =  new PricingUnit[outwardPricingUnits.Count];
					for (int i =0; i< outwardPricingUnits.Count; i++)							
					{
						pricingUnitsOutward[i] = (PricingUnit)outwardPricingUnits[i];	
					}														
				}

				if (itineraryAdapter.Adaptee.ReturnJourney != null )
				{
					IList returnPricingUnits = itineraryAdapter.ReturnPricingUnits;
					pricingUnitsReturn =  new PricingUnit[returnPricingUnits.Count];
					for (int i =0; i< returnPricingUnits.Count; i++)							
					{
						pricingUnitsReturn[i] = (PricingUnit)returnPricingUnits[i];	
					}
				}

				// Test for the new fares
				if (pricingUnitsOutward != null)
				{
					foreach (PricingUnit pu in pricingUnitsOutward)
					{
						if (pu.CoachFaresReturnComponent)
							outwardFaresNew = true;
					}
				}
				
				if (pricingUnitsReturn != null)
				{
					foreach (PricingUnit pu in pricingUnitsReturn)
					{
						if (pu.CoachFaresReturnComponent)
							returnFaresNew = true;
					}
				}

			}
            
			return (outwardFaresNew ^ returnFaresNew); //XOR
		}

		/// <summary>
		/// Determines if a Single journey has been planned returning new fares.
		/// </summary>
		/// <returns></returns>
		private bool SingleJourneyNewFares(bool isForReturn)
		{ 
			bool outwardNew = false;
			bool returnNew = false;
						
			// first, are we dealing with a single journey
			if (itineraryAdapter.Adaptee.ReturnJourney == null )
			{
				PricingUnit[] pricingUnitsOutward = null;

				IList outwardPricingUnits = itineraryAdapter.OutwardPricingUnits;
				pricingUnitsOutward =  new PricingUnit[outwardPricingUnits.Count];
				for (int i =0; i< outwardPricingUnits.Count; i++)							
				{
					pricingUnitsOutward[i] = (PricingUnit)outwardPricingUnits[i];	
				}	

				if (pricingUnitsOutward != null)
				{
					foreach (PricingUnit pu in pricingUnitsOutward)
					{
						if (pu.CoachFaresReturnComponent)
							outwardNew = true;
					}
				}
			}
			else
			{
				// determine whether we're dealing with a journey with old and new fares

				bool mixedCoachFares = JourneyContainsNewAndOldCoachFares(true);

				if (mixedCoachFares)
				{
					// Check if outward journey has new fares
					if (!isForReturn)
					{
						PricingUnit[] pricingUnitsOutward = null;

						if(itineraryAdapter.OutwardPricingUnits != null)
						{


							IList outwardPricingUnits = itineraryAdapter.OutwardPricingUnits;
							pricingUnitsOutward =  new PricingUnit[outwardPricingUnits.Count];
							for (int i =0; i< outwardPricingUnits.Count; i++)							
							{
								pricingUnitsOutward[i] = (PricingUnit)outwardPricingUnits[i];	
							}	

							if (pricingUnitsOutward != null)
							{
								foreach (PricingUnit pu in pricingUnitsOutward)
								{
									if (pu.CoachFaresReturnComponent)
										outwardNew = true;
								}
							}
						}

					}
					else // Check if return journey has new fares
					{

						PricingUnit[] pricingUnitsReturn = null;

						if(itineraryAdapter.ReturnPricingUnits != null)
						{

							IList returnPricingUnits = itineraryAdapter.ReturnPricingUnits;
							pricingUnitsReturn =  new PricingUnit[returnPricingUnits.Count];
							for (int i =0; i< returnPricingUnits.Count; i++)							
							{
								pricingUnitsReturn[i] = (PricingUnit)returnPricingUnits[i];	
							}	

							if (pricingUnitsReturn != null)
							{
								foreach (PricingUnit pu in pricingUnitsReturn)
								{
									if (pu.CoachFaresReturnComponent)
										returnNew = true;
								}
							}
						}

					}

				}
				else
				{
					// this a return journey, but the fares are either both new, or both old
					return false;
				}
			}

			if (!isForReturn)
				return outwardNew;
			else
				return returnNew;

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{						

		}
		#endregion

		#region public properties

         public TDButton ButtonRetailers
         {
             get
             {
                 return this.buttonRetailers;
             }
         }

		/// <summary>
		/// Read/Write property used to display an error message 
		/// </summary>
		public bool InErrorMode
		{
			get {return inErrorMode;}
			set {inErrorMode = value;}
		}

		/// <summary>
		/// Read only property - Exposes the find cheaper image hyperlink control so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's find cheaper hyperlink control</returns>
		public HyperlinkPostbackControl FindCheaperLink
		{
			get{ return this.findCheaperLinkControl;}
		}

		/// <summary>
		/// Read only property - Exposes the display mode image button so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's display mode image button</returns>
		public TDButton ViewButton
		{
			get{ return this.buttonChangeView;}
		}

		
		/// <summary>
		/// Read/write property - Indicates whether control is to display rail discount fares
		/// </summary>
		public string RailDiscount
		{
			get {return railDiscount;}
			set {railDiscount=value;}
		}

		/// <summary>
		/// Read/write property - Indicates whether control is to display coach discount fares
		/// </summary>
		public string CoachDiscount
		{
			get {return coachDiscount;}
			set {coachDiscount=value;}
		}

		/// <summary>
		/// Read/write property - Gets/Sets the itinerary adapter to be used by the control
		/// </summary>
		public ItineraryAdapter ItineraryAdapter
		{
			get{return itineraryAdapter;}
			set{ itineraryAdapter = value;}
		}

		/// <summary>
		/// Read/write property - Determines if the control is displaying fares information for the
		/// return leg
		/// </summary>
		public bool IsForReturn
		{
			get{return isForReturn;}
			set{isForReturn= value;}
		}

		/// <summary>
		/// Read/write property - Determines whether adult or child fares are visible		
		/// </summary>
		public bool ShowChildFares
		{
			get{return showChildFares;}
			set{showChildFares= value;}
		}

		/// <summary>
		/// Read/write property - Determines whether Full Itinerary details are being displayed		
		/// </summary>
		public bool FullItinerarySelected
		{
			get{return fullItinerarySelected;}
			set{fullItinerarySelected= value;}
		}
        
		/// <summary>
		/// Read/write property - Determines whether the control is displayed in diagram or table mode
		/// </summary>
		public bool InTableMode
		{
			get{return inTableMode;}
			set{inTableMode= value;}

		}

		/// <summary>
		/// Read/write property to hide ticket selection check boxes and disable javascript highlighting
		/// </summary>
		public bool HideTicketSelection
		{
			get {return hideTicketSelection;}
			set {hideTicketSelection = value;}
		}

		/// <summary>
		/// Read/write property to hide the Help button and the control header labels
		/// </summary>
		public bool HideHelpAndHeaderLabels
		{
			get {return hideHelpAndHeaderLabels;}
			set {hideHelpAndHeaderLabels = value;}
		}

		/// <summary>
		/// Read/write property to set the start location label of the control
		/// </summary>
		public string StartLocation
		{
			get {return startLocation;}
			set {startLocation = value;}
		}

		/// <summary>
		/// Read/write property to set the end location label of the control
		/// </summary>
		public string EndLocation
		{
			get {return endLocation;}
			set {endLocation = value;}
		}

		/// <summary>
		/// Read only property - Exposes the find cheaper image hyperlink control so that the page containing the control can handle its events
		/// </summary>
		/// <returns>The control's find cheaper hyperlink control</returns>
		public HyperlinkPostbackControl ViewOtherFareLink
		{
			get{ return this.viewOtherFareLinkControl;}
		}
         
        /// <summary>
        /// Read only property - Returns the help label associated with the JourneyFaresControl
        /// </summary>
        public string HelpLabel
        {
            get { return helpJourneyFaresLabelControl.ID; }
        }

        /// <summary>
        /// Read/write. Sets if the Break of journey message can be shown. 
        /// If the journey involves a break over a configured amount, then the label will only be 
        /// shown if this property is true.
        /// </summary>
        public bool ShowBreakOfJourney
        {
            get { return showBreakOfJourney; }
            set { showBreakOfJourney = value; }
        }

        /// <summary>
        /// Read/write. Flag to allow additional debug info to be displayed on screen if 
        /// logged on user has CJP status
        /// </summary>
        public bool CJPUser
        {
            get { return cjpUser; }
            set { cjpUser = value; }
        }

		#endregion

		#region public methods

       

		/// <summary>
		/// Returns the fareDetailsTableSegmentControl for a given pricing unit
		/// </summary>
		/// <param name="pricingUnit">PricingUnit</param>
		/// <returns>FareDetailsTableSegmentControl</returns>
		public FareDetailsTableSegmentControl FaresTable(PricingUnit pricingUnit)
		{
			if (fareDetailsDiagram.Visible)			
				return fareDetailsDiagram.FaresTable(pricingUnit);					
			else if (fareDetailsTable.Visible)
				return fareDetailsTable.FaresTable(pricingUnit);
			else
				return null;

		}

		/// <summary>
		/// Hides/shows the find cheaper buttons and associated labels
		/// </summary>
		/// <param name="show">Sets visibility to true/false</param>
		public void ShowFindCheaper(bool show)
		{		
			labelFindCheaperPrefix.Visible = show;
			labelFindCheaperSuffix.Visible = show;
			findCheaperLinkControl.Visible = show;
		}

		/// <summary>
		/// Stores the selected ticket for a given pricing unit. Specify null
		/// for selectedTicket if no ticket is selected.
		/// </summary>
		/// <param name="pricingUnit">Pricing Unit</param>
		/// <param name="selectedTicket">Ticket for which selection information should be stored</param>
		public void SetSelectedTicket(PricingUnit pricingUnit, Ticket selectedTicket)
		{
			selectedTicketsHash[pricingUnit] = selectedTicket;
		}
				
		#endregion

		private void fareDetailsTable_OtherFaresClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);
		}

		private void fareDetailsDiagram_OtherFaresClicked(object sender, EventArgs e)
		{
			EventHandler eventHandler = OtherFaresClicked;
			if (eventHandler != null)
				eventHandler(this, e);

		}

	}
}
