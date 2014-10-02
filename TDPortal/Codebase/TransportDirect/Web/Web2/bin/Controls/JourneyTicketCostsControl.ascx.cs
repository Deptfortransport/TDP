// *********************************************** 
// NAME                 : JourneyTicketCostsControl.ascx.cs
// AUTHOR               : Robert Griffith
// DATE CREATED         : 07/01/2005
// DESCRIPTION			: Displays the costs associated with each individual journey segment
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyTicketCostsControl.ascx.cs-arc  $
//
//   Rev 1.3   Feb 02 2009 17:13:24   mmodi
//Added logic to setup the disply of the break of journey note for Adjusted journeys
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.2   Mar 31 2008 13:21:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:52   mturner
//Initial revision.
//
//   Rev 1.11   Mar 06 2007 13:43:58   build
//Automatically merged from branch for stream4358
//
//   Rev 1.10.1.0   Mar 02 2007 11:49:32   asinclair
//Added OtherFaresClicked method
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.10   Oct 06 2006 15:35:04   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.9.1.0   Oct 02 2006 16:20:14   mmodi
//Set the road journey property for CarParkCosts control
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.9   Mar 17 2006 15:21:16   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.8   Mar 13 2006 12:00:06   RGriffith
//Changes to make Car Fuel Costs drop down printer friendly
//
//   Rev 1.7   Mar 10 2006 16:36:36   RGriffith
//Avoid doing calculations if no Journey Array exists
//
//   Rev 1.6   Mar 08 2006 16:14:00   RGriffith
//FxCop Suggested Changes & Additional functionality for Info button & "Fares included above" message
//
//   Rev 1.5   Mar 06 2006 18:37:22   RGriffith
//Null check when populating start/end location labels
//
//   Rev 1.4   Mar 06 2006 17:31:36   RGriffith
//Changes made for tickets/costs
//
//   Rev 1.3   Mar 01 2006 13:26:18   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.2   Feb 22 2006 12:09:08   RGriffith
//Interim Version
//
//   Rev 1.1   Feb 15 2006 16:13:38   RGriffith
//Interim Version
//
//   Rev 1.0   Feb 06 2006 13:11:26   RGriffith
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.PricingRetail.Domain;
    using System.Collections;
    
    using CJP = TransportDirect.JourneyPlanning.CJPInterface;
    using TransportDirect.Common.PropertyService.Properties;
	
	/// <summary>
	///		Summary description for JourneyTicketCostsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyTicketCostsControl : TDUserControl
    {
        #region Private members
        // Web controls used in the page
		protected System.Web.UI.WebControls.Label segmentHeader;

		// Private data members
		private PricingRetailOptionsState[] priceOptionsArray;
		private Journey[] journeyArray;
		private bool isReturnJourney;
		private bool printerFriendly;
        private bool showBreakOfJourneyNote = false;
        private bool showBreakOfJourneyNoteOnFaresControl = false;
        
		// Private variables
		private PricingRetailOptionsState priceOptions;

        #endregion

        #region Properties
        /// <summary>
		/// Read/Write: Property to store the array of all journey PricingOptionRetailStates
		/// </summary>
		public PricingRetailOptionsState[] PriceOptionsArray
		{
			get
			{
				return priceOptionsArray;
			}
			set
			{
				priceOptionsArray = value;
			}
		}

		/// <summary>
		/// Read/Write: Property to store the array of all journeys to be displayed in the control
		/// </summary>
		public Journey[] JourneyArray
		{
			get
			{
				return journeyArray;
			}
			set
			{
				journeyArray = value;
			}
		}

		/// <summary>
		/// Read/Write: Property to determine if the control is being used to show outward or return journey details
		/// </summary>
		public bool IsReturnJourney
		{
			get
			{
				return isReturnJourney;
			}
			set
			{
				isReturnJourney = value;
			}
		}

		/// <summary>
		/// Read/Write: Property to determine if controls should be displayed in printer friendly mode
		/// </summary>
		public bool PrinterFriendly
		{
			get
			{
				return printerFriendly;
			}
			set
			{
				printerFriendly = value;
			}
		}

        /// <summary>
        /// Read/write. Property to allow the break of journey note to be shown on the fares control
        /// </summary>
        public bool ShowBreakOfJourneyNote
        {
            get { return showBreakOfJourneyNote; }
            set { showBreakOfJourneyNote = value; }

        }

		#endregion Properties

		#region Event Handlers
		/// <summary>
		/// Handles Page Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// If journey details available bind the PricingRetailOptionsState to the repeater control
			if ((journeyArray != null) && (journeyArray.Length > 0))
			{
				if ((priceOptionsArray != null) && (journeyArray != null) && (priceOptionsArray.Length == journeyArray.Length))
				{
                    SetupBreakOfJourneyNote();
                    
					ticketsCostsRepeater.DataSource = priceOptionsArray;
					ticketsCostsRepeater.DataBind();
				}
			}
		}

		/// <summary>
		/// Handles the Repeater DataBinding Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ticketsAndRetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					// Retrieve the individual PricingRetailOptionsState bound to this repeater item
					priceOptions = (PricingRetailOptionsState)e.Item.DataItem;

					// Find associated car/public costs controls
					CarJourneyCostsControl carCosts = (CarJourneyCostsControl)e.Item.FindControl("carJourneyCostsControl");
					JourneyFaresControl publicCosts = (JourneyFaresControl)e.Item.FindControl("journeyFaresControl");

					// Determine if the journey is a public or car journey
					bool isPublicJourney = journeyArray[e.Item.ItemIndex] is PublicJourney;
					
					if (!isPublicJourney)
					{
						// If car journey - hide public costs control and initialise car costs control
						publicCosts.Visible = false;
						ViewCarCostsControl(carCosts, (RoadJourney)journeyArray[e.Item.ItemIndex]);
					}
					else
					{
						// If public journey - hide car costs control and initialise public costs control
						carCosts.Visible = false;
						ViewPublicTransportCostsControl(publicCosts, priceOptions, journeyArray[e.Item.ItemIndex]);
					}

					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Handles the Ok button associated with the Car Costs Control (fuel/all costs) drop down list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OKButton_Click(object sender, EventArgs e)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			int previousSelectedValue;
			bool toggle = false;

			// Determine previously selected drop down value
			if (itineraryManager.JourneyViewState.ShowRunning)
				previousSelectedValue = 1;
			else
				previousSelectedValue = 0;

			// Determine if any drop down menu items have changed from the previously selected value
			foreach (RepeaterItem ri in ticketsCostsRepeater.Items)
			{
				switch (ri.ItemType)
				{
					case ListItemType.Item :
					case ListItemType.AlternatingItem : 
						
						// Retrieve the Drop Down control from the repeater items
						CarJourneyCostsControl carJourneyCosts = (CarJourneyCostsControl)ri.FindControl("carJourneyCostsControl");
						DropDownList dropDown = carJourneyCosts.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.DropDownFuelCosts;

						/*
						 * NOTE: This code block has to do a direct inquiry on the postback data as it seems impossible
						 * to view the viewstate information for this drop down list on this control 
						 * (whilst in a repeater) despite the viewstate being explicitly turned on
						*/
						int selectedIndex = previousSelectedValue;
						string postBackValue = Request.Form[dropDown.UniqueID];
						if (postBackValue == "Option1") // If "Option1" selected (Fuel Costs) set selected = 0
						{
							selectedIndex = 0;
						}
						else if (postBackValue == "Option2") // If "Option2" selected (All Costs) set selected = 1
						{
							selectedIndex = 1;
						}
					
						// If selected index has changed from previous selection then set the toggle flag
						if ((dropDown != null) && (selectedIndex != previousSelectedValue))
						{
							toggle = true;
						}
						break;

					default :
						break;
				}
			}
			// If toggle set then negate the itinerary flag for the type of fuel costs to display
			if (toggle)
			{
				itineraryManager.JourneyViewState.ShowRunning = !itineraryManager.JourneyViewState.ShowRunning;
			}

			// Force a page refresh by redirecting to the current page
			TDSessionManager.Current.FormShift[SessionKey.ForceRedirect]= true;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineTicketsView;
		}

		/// <summary>
		/// Event handler for when an info button is clicked on the tabledegementcontrol
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void InfoButtonClicked(object sender, EventArgs e)
		{
			// redirect to Ticket Upgrade page				
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketUpgrade;
		}
		#endregion Event Handlers

		#region Private Methods
		/// <summary>
		/// Method to set up the CarJourneyCostsControl
		/// </summary>
		/// <param name="carCosts">The CarJourneyCostsControl to initialise</param>
		/// <param name="journey">The journey it is associated with</param>
		private void ViewCarCostsControl(CarJourneyCostsControl carCosts, RoadJourney journey)
		{
			// Set up the Ok Button event handler
			carCosts.ItemisedCarCostsControl.CostPageTitleControl.OKButton.Click
				+= new EventHandler(this.OKButton_Click);

			// Set up car control properties
			carCosts.ItemisedCarCostsControl.CostPageTitleControl.HideHelpButton = true;
			carCosts.ItemisedCarCostsControl.IsOutward = !isReturnJourney;
			carCosts.ItemisedCarCostsControl.OtherCostsControl.IsOutward = !isReturnJourney;
			carCosts.ItemisedCarCostsControl.CarParkCostsControl.IsOutward = !isReturnJourney;
			carCosts.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.PrinterFriendly = printerFriendly;
			carCosts.ItemisedCarCostsControl.CarParkCostsControl.Printable = printerFriendly;

			// Set up properties for display types
			carCosts.ItemisedCarCostsControl.ShowRunning = TDItineraryManager.Current.JourneyViewState.ShowRunning;
			carCosts.ItemisedCarCostsControl.ReturnExists = false;

			// Set the Journey objects for the Car costs control
			carCosts.ItemisedCarCostsControl.CarRoadJourney = journey;
			carCosts.ItemisedCarCostsControl.OtherCostsControl.CarRoadJourney = journey;
			carCosts.ItemisedCarCostsControl.CarParkCostsControl.CarRoadJourney = journey;

			// Set up the start and end journey locations strings for display
			if (journey != null)
			{
				int journeyLength = journey.JourneyLegs.Length;
				carCosts.ItemisedCarCostsControl.CostPageTitleControl.StartLocation = journey.JourneyLegs[0].LegStart.Location.Description;
				carCosts.ItemisedCarCostsControl.CostPageTitleControl.EndLocation = journey.JourneyLegs[journeyLength - 1].LegEnd.Location.Description;
			}
			
			// Set Car controls as visible
			carCosts.TotalCarCostsControl.Visible = false;
			carCosts.Visible = true;

			// Calculate the car costs
			carCosts.ItemisedCarCostsControl.CalculateCosts();
		}

		/// <summary>
		/// Method to set up the JourneyFaresControl for public journeys
		/// </summary>
		/// <param name="publicCosts">JourneyFaresControl to initialise</param>
		/// <param name="priceOptions">Pricing information to associate with the JourneyFares control</param>
		/// <param name="journey">The journey it is associated with</param>
		private void ViewPublicTransportCostsControl(JourneyFaresControl publicCosts, PricingRetailOptionsState priceOptions, Journey journey)
		{
			// Set up event handler for ticket upgrade info button
			publicCosts.InfoButtonClicked += new EventHandler(this.InfoButtonClicked);

			publicCosts.OtherFaresClicked +=new EventHandler(this.publicCosts_OtherFaresClicked);

			// Set up an itinerary adapter
			ItineraryAdapter adapter = new ItineraryAdapter(priceOptions.JourneyItinerary);
			adapter.OverrideItineraryType = priceOptions.OverrideItineraryType;

			// Set variables to hide interactive parts of controls not required on this page
			publicCosts.HideTicketSelection = true;
			publicCosts.HideHelpAndHeaderLabels = true;

			// Set JourneyFares Control options
			publicCosts.ItineraryAdapter = adapter;			
			publicCosts.ShowChildFares = priceOptions.ShowChildFares;
			publicCosts.PrinterFriendly = printerFriendly;
			
			// Set results to be displayed in a table rather than a diagram
			publicCosts.InTableMode = true;
			
			// More JourneyFares options
			publicCosts.IsForReturn = isReturnJourney;
			publicCosts.FullItinerarySelected = false;
			publicCosts.ShowFindCheaper(false);

            // Set the message flags
            publicCosts.ShowBreakOfJourney = showBreakOfJourneyNoteOnFaresControl;
            
			// Set up the start and end journey locations strings for display
			int journeyLength = journey.JourneyLegs.Length;
			publicCosts.StartLocation = journey.JourneyLegs[0].LegStart.Location.Description;
			publicCosts.EndLocation = journey.JourneyLegs[journeyLength - 1].LegEnd.Location.Description;
			
			// Set up the discount options for the journey fares control
			if (priceOptions.Discounts != null)
			{
				publicCosts.RailDiscount = priceOptions.Discounts.RailDiscount;
				publicCosts.CoachDiscount = priceOptions.Discounts.CoachDiscount;
			}

			// Set as visible
			publicCosts.Visible = true;
		}

        /// <summary>
        /// Method which determines if the Break of journey note should be shown on the public journey Fares control.
        /// Currently only implemented for an Adjusted journey.
        /// </summary>
        private void SetupBreakOfJourneyNote()
        {
            // Set up the labels to show on the fares control
            if (showBreakOfJourneyNote)
            {
                #region Break of journey note
                // Assume we will only ever show the Break of journey note for Adjust journeys,
                // that involve Rail legs
                // see DN069 sec 8.2.5.

                // There should only be one journey, but placed in loop to avoid errors
                PublicJourney publicJourney = null;

                foreach (Journey journey in JourneyArray)
                {
                    if (journey is PublicJourney)
                    {
                        ArrayList modes = new ArrayList(journey.GetUsedModes());

                        if (modes.Contains(CJP.ModeType.Rail))
                        {
                            publicJourney = (PublicJourney)journey;
                        }
                    }
                }

                if (publicJourney != null)
                {
                    int startDay = publicJourney.JourneyLegs[0].LegStart.DepartureDateTime.Day;
                    int endDay = publicJourney.JourneyLegs[publicJourney.JourneyLegs.Length - 1].LegEnd.ArrivalDateTime.Day;

                    bool showMessage = false;

                    int maxInterchangeTime = Convert.ToInt32(Properties.Current["JourneyFaresControl.BreakOfJourney.InterchangeTime.Minutes"]);

                    // Only show the break of journey message when
                    //  a) Journey starts and ends on different days, and
                    //  b) There is an interchange time between two legs greater than the configured amount
                    if (startDay != endDay)
                    {
                        for (int i = 0; i < publicJourney.JourneyLegs.Length - 1; i++)
                        {
                            JourneyLeg currentleg = publicJourney.JourneyLegs[i];
                            JourneyLeg nextLeg = publicJourney.JourneyLegs[i + 1];

                            TDDateTime time = currentleg.LegEnd.ArrivalDateTime.AddMinutes(maxInterchangeTime);

                            // If the current leg time + max interchange minutes is still less than the next
                            // leg departure time, then its more than a max interchange time wait
                            int timeCompare = time.CompareTo(nextLeg.LegStart.DepartureDateTime);
                            if (timeCompare == -1)
                            {
                                showMessage = true;
                                break;
                            }
                        }
                    }

                    // Ok to show the message. Set the global flag so when the Repeater bind event fires,
                    // it'll set the property on the Fares control correctly
                    showBreakOfJourneyNoteOnFaresControl = showMessage;
                }
                #endregion
            }
        }

		#endregion Private Methods

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
			this.ticketsCostsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ticketsAndRetails_ItemDataBound);
		}
		#endregion Web Form Designer generated code

		private void publicCosts_OtherFaresClicked(object sender, EventArgs e)
		{
			PricingRetailOptionsState[] options;

			options = TDItineraryManager.Current.GetItineraryPricing();

			foreach (PricingRetailOptionsState optionsItem in options)
			{

				if(optionsItem.OverrideItineraryType == ItineraryType.Single)
				{
					optionsItem.OverrideItineraryType = ItineraryType.Return;
				}
				else
					if(optionsItem.OverrideItineraryType == ItineraryType.Return)
				{
					optionsItem.OverrideItineraryType = ItineraryType.Single;

				}

				optionsItem.SetProcessedJourneys();
			}


			TDSessionManager.Current.Session[SessionKey.Transferred] = false;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;

		}
	}
}
