// *********************************************** 
// NAME                 : JourneyEmissionsDashboardControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2006 
// DESCRIPTION			: Control displaying the Journey Emissions Dashboard
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionsDashboardControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Dec 19 2008 15:06:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 06 2008 11:06:00   apatel
//moved CommandSwitchView button to the top of the journey emissions page.
//Resolution for 4906: Carbon Dials - input page icons and table view button
//
//   Rev 1.2   Mar 31 2008 13:21:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Feb 27 2008 14:58:24   sjohal
//Replaced Hyperlinks with buttons for Journey Costs and Compare CO2
//
//
//   Rev 1.0   Nov 08 2007 13:15:30   mturner
//Initial revision.
//
//   Rev 1.22   Aug 31 2007 16:21:56   build
//Automatically merged from branch for stream4474
//
//   Rev 1.21.1.0   Aug 30 2007 17:56:52   asinclair
//Set the JourneyEmissionsPageState value
//Resolution for 4474: DEL 9.7 Stream : Public Transport C02
//
//   Rev 1.21   Apr 03 2007 17:48:12   mmodi
//Removed Page.Reset and redundant code when Compare hyperlink selected
//Resolution for 4374: CO2: Mpg value is not used when viewing PT Emissions from Car Emissions
//
//   Rev 1.20   Mar 21 2007 14:15:52   asinclair
//When Compare hyperlink is clicked, no set Journey parameters to use what user has selected
//
//   Rev 1.19   Mar 06 2007 12:29:56   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.18.1.1   Feb 26 2007 11:42:08   mmodi
//Updated to test for CO2 PT switch before showing link to Journeyemissionscomparejourney
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.18.1.0   Feb 16 2007 11:43:54   mmodi
//Added Compare emissions hyperlink
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.18   Jan 30 2007 16:31:50   mmodi
//Updated check when viewing page for a modified journey, and hide hyperlinks if modifying the journey
//Resolution for 4351: CO2: Issue when view emissions for connecting Car journey
//
//   Rev 1.17   Jan 04 2007 14:04:52   mmodi
//Costs link no longer replans journey, but updates car journey fuel cost value in session
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.16   Jan 03 2007 14:26:12   mmodi
//Updated to use the Your Car parameters when replaning the journey using the Journey Costs link
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.15   Dec 15 2006 15:49:46   mmodi
//Journey costs link now replans journey based on values entered on this page
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.14   Dec 15 2006 11:12:24   mmodi
//Readded compare note text changes, and updated code to round fuel cost when <£1
//Resolution for 4321: CO2: Use accurate Scales when calculating angle
//
//   Rev 1.13   Dec 14 2006 16:26:48   mmodi
//Changed to use JourneyEmissionsSpeedoDials following multiserver problem
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.11   Dec 12 2006 12:34:24   mmodi
//Removed Journey costs link
//Resolution for 4312: CO2: Remove Find detailed journey costs link
//
//   Rev 1.10   Dec 05 2006 15:54:38   mmodi
//Rounded fuel cost values following Helper changes
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.9   Dec 05 2006 12:37:22   dsawe
//changed the title journeyEmissionsControlTitle
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.8   Dec 01 2006 14:37:12   mmodi
//Updated Compare note text 
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.7   Dec 01 2006 11:10:20   mmodi
//Removed petrol and car icons
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.6   Nov 30 2006 18:34:50   mmodi
//Corrected ForYourJourney label not being populated
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.5   Nov 30 2006 16:54:12   mmodi
//Updated code following DfT changes
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Nov 29 2006 14:54:26   dsawe
//replaced journeyemissionsspeedodial control with  image web control
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.3   Nov 26 2006 15:46:30   mmodi
//Added SaveFuel link code
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 25 2006 14:10:50   mmodi
//Updated compare note text
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.1   Nov 24 2006 11:17:04   mmodi
//Code changes and updates as part of workstream
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Nov 19 2006 10:39:08   mmodi
//Initial revision.
//Resolution for 4240: CO2 Emissions
//

using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

using MeasureConvert = System.Convert;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		Summary description for JourneyEmissionsDashboardControl.
	/// </summary>
	public partial class JourneyEmissionsDashboardControl : TDUserControl
	{
		#region Controls


		// speedo dials
		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsSpeedoDial fuelCostSpeedoDial;
		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsSpeedoDial emissionsSpeedoDial;

		// icons
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageFuel;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageEmissions;

		// labels


		// hyperlinks
		//protected HyperlinkPostbackControl journeyCostsHyperlink;
		//protected HyperlinkPostbackControl compareEmissionsHyperlink;

        

		#endregion

		#region Private variables

		private JourneyEmissionsPageState pageState;
		private bool printable;

		private string compareCar;
        private string compareYourCar;

     

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyEmissionsDashboardControl()
		{
		}

		#endregion

		#region Page_Load, PreRender

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Get the journeyemissions state
			pageState = TDSessionManager.Current.JourneyEmissionsPageState;

			SetUpLabels();
			//SetUpHyperlinks();

            fuelUsedTitle.Text = GetResource("JourneyEmissionsDashboardControl.FuelUsedTitle.Text");
		}

		/// <summary>
		/// Sets dashboard pointers for Fuel cost and CO2 emissions
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			UpdateDashboard();
			SetUpImages();
            yourCarRow.Visible = displayYourcarRow();
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

           // Buttons
            journeyCosts.Click += new EventHandler(this.JourneyCostsClick);
            compareEmissions.Click += new EventHandler(this.CompareEmissionsClick);
        }
		#endregion

		#region Event handlers

		/// <summary>
		/// Event Handler for the Journey cost hyperlink.
		/// </summary>
        public void JourneyCostsClick(object sender, EventArgs e)
        {
            // We want to replan the journey based on the Your Car parameters specified

            TDJourneyParametersMulti journeyParameters;

            // Update the Session JourneyParameters with those specified on the JourneyEmissions page
            journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            journeyParameters.CarSize = pageState.YourCarSize;
            journeyParameters.CarFuelType = pageState.YourCarFuelType;
            journeyParameters.FuelCostOption = pageState.YourCarFuelCostOption;
            journeyParameters.FuelCostEntered = pageState.YourCarFuelCostEntered;

            // Indicates they entered a CO2 value, so must convert to allow the Journey Replan
            if ((!pageState.YourCarFuelConsumptionOption) && (pageState.YourCarFuelConsumptionUnit == 3))
            {
                journeyParameters.FuelConsumptionOption = false;
                journeyParameters.FuelConsumptionEntered = ConvertCO2toMPG(pageState.YourCarFuelConsumptionEntered, pageState.YourCarFuelType);
                journeyParameters.FuelConsumptionUnit = 1;
            }
            else
            {
                journeyParameters.FuelConsumptionOption = pageState.YourCarFuelConsumptionOption;
                journeyParameters.FuelConsumptionEntered = pageState.YourCarFuelConsumptionEntered;
                journeyParameters.FuelConsumptionUnit = pageState.YourCarFuelConsumptionUnit;
            }

            TDSessionManager.Current.JourneyParameters = journeyParameters as TDJourneyParameters;

            // This overwrites the TotalFuelCost in the original RoadJourney in the session
            UpdateFuelCostToSession();
				
            // Reset the page state
            pageState.Initialise();

            TransitionEvent te = TransitionEvent.GoJourneyFares;
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
        }

		/// <summary>
		/// Handler for the Compare emissions hyperlink.
		/// Navigates to the Compare emissions page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void CompareEmissionsClick(object sender, EventArgs e)
        {
            // Set page id in stack so we know where to come back to
            TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(PageId);

            // Navigate to the Journey Emissions Compare Journey page
            TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsCompareState = JourneyEmissionsCompareState.JourneyCompare;
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissionsCompareJourney;
        }

        /// <summary>
        /// Handler for the switch to table/diagram view hyperlink.
        /// </summary>
        public void CommandSwitchClick()
        {
            if (graphicalDisplayFuelConsumptionPanel.Visible == false)
            {
                graphicalDisplayFuelConsumptionPanel.Visible = true;
                tableDisplayFuelConsumptionPanel.Visible = false;
            }
            else
            {
                graphicalDisplayFuelConsumptionPanel.Visible = false;
                tableDisplayFuelConsumptionPanel.Visible = true;
            }
            
        }

		#endregion

		#region Private methods
		
		/// <summary>
		/// Translates from CO2 g per km to MPG
		/// </summary>
		/// <param name="CO2Value">CO2 value as string</param>
		/// <returns>CO2 value converted to MPG</returns>
		private string ConvertCO2toMPG(string CO2Value, string fuelType )
		{
			CarCostCalculator calculator = new CarCostCalculator();

			// fuel factor is stored as 234 (for petrol), but we need it as 2.34 kg/l
			double fuelFactor = calculator.GetFuelFactor(fuelType);
			fuelFactor = fuelFactor / 100;			

			// Convert grams entered to Kg
			double fuelConsumption = MeasureConvert.ToDouble(CO2Value) / 1000; 
					
			// Convert Kg/L to Km/L by dividing by Kg/Km
			fuelConsumption = fuelFactor / fuelConsumption;

			// Convert Km/L to MPG 
			fuelConsumption = fuelConsumption * 2.8248105347;

			// Round so that if user returns to Journey emissions page we dont show a value with 10 dp 
			// in the MPG input text box
			decimal roundedFuelConsuption = decimal.Round(Convert.ToDecimal(fuelConsumption),2);

			return roundedFuelConsuption.ToString();
		}

		/// <summary>
		/// Assigns the Fuel Cost value calculated on Journey Emissions to the Road journeys in the Session
		/// </summary>
		private void UpdateFuelCostToSession()
		{
			// Determine if there was a return journey or just single and then update Fuel Cost value
			if ( (TDItineraryManager.Current.JourneyResult.OutwardRoadJourney() != null) 
				&& (TDItineraryManager.Current.JourneyResult.ReturnRoadJourney() != null))
			{
				// We have a return journey so need to split the Fuel cost calculated on the 
				// Journey Emissions page between the outward and return

				RoadJourney outwardRJ =  TDSessionManager.Current.JourneyResult.OutwardRoadJourney();
				RoadJourney returnRJ =  TDSessionManager.Current.JourneyResult.ReturnRoadJourney();
				
				decimal originalTotalFuelCost = Convert.ToDecimal(outwardRJ.TotalFuelCost + returnRJ.TotalFuelCost);
				decimal newTotalFuelCost = Convert.ToDecimal(pageState.FuelCostValue) * 1000000;
				
				decimal outwardFuelCost = Convert.ToDecimal(outwardRJ.TotalFuelCost);
				decimal returnFuelCost =  Convert.ToDecimal(returnRJ.TotalFuelCost);

				// Update fuel cost as a ratio of the original and new
				outwardFuelCost = outwardFuelCost * (newTotalFuelCost / originalTotalFuelCost);
				returnFuelCost = returnFuelCost * (newTotalFuelCost / originalTotalFuelCost);

				// Set values to RoadJourney objects
				outwardRJ.TotalFuelCost = Convert.ToInt32(outwardFuelCost);
				returnRJ.TotalFuelCost = Convert.ToInt32(returnFuelCost);

				// Update to session
				TDSessionManager.Current.JourneyResult.UpdateOutwardRoadJourney(outwardRJ);
				TDSessionManager.Current.JourneyResult.UpdateReturnRoadJourney(returnRJ);
			}
			else if (TDItineraryManager.Current.JourneyResult.ReturnRoadJourney() != null)
			{
				// Only the Return journey was available
				RoadJourney rj =  TDSessionManager.Current.JourneyResult.ReturnRoadJourney();
				rj.TotalFuelCost = Convert.ToInt32((Convert.ToDecimal(pageState.FuelCostValue) * 1000000));			
				TDSessionManager.Current.JourneyResult.UpdateReturnRoadJourney(rj);
			}
			else
			{
				// Otherwise only the Outward journey was used
				RoadJourney rj =  TDSessionManager.Current.JourneyResult.OutwardRoadJourney();
				rj.TotalFuelCost = Convert.ToInt32((Convert.ToDecimal(pageState.FuelCostValue) * 1000000));			
				TDSessionManager.Current.JourneyResult.UpdateOutwardRoadJourney(rj);
			}
		}

		/// <summary>
		/// Populates the icons
		/// </summary>
		private void SetUpImages()
		{
			// assign alternate text for speedo dial images
			//fuelCostSpeedoDial..AlternateText = GetResource("JourneyEmissionsSpeedoDial.imageSpeedoDial.AlternateText");
			//emissionsSpeedoDial.AlternateText = GetResource("JourneyEmissionsSpeedoDial.imageSpeedoDial.AlternateText");
		}
		
		/// <summary>
		/// Populates the labels
		/// </summary>
		private void SetUpLabels()
		{
			// Assign text to labels	
			//journeyEmissionsControlTitle.Text = GetResource("JourneyEmissionsControl.Caption.Text");

			fuelCostText.Text = GetResource("JourneyEmissionsDashboard.fuelCost.Text");
			emissionsText.Text = GetResource("JourneyEmissionsDashboard.emissions.Text");
			costTitle.Text = GetResource("JourneyEmissionsDashboard.costTitle.Text");
			emissionsTitle.Text = GetResource("JourneyEmissionsDashboard.emissionsTitle.Text");
            costTitle2.Text = GetResource("JourneyEmissionsDashboard.costTitle.Text");
            emissionsTitle2.Text = GetResource("JourneyEmissionsDashboard.emissionsTitle.Text");

            emissionsTitle3.Text = GetResource("JourneyEmissionsDashboard.emissionsTitle.Text");
            costTitle3.Text = GetResource("JourneyEmissionsDashboard.costTitle.Text");
            
            journeyCosts.Text = GetResource("JourneyEmissionsDashboard.journeyCosts.Text");
            compareEmissions.Text = GetResource("JourneyEmissionsDashboard.compareEmissions.Text");

            labelTransportHeader.Text = GetResource("JourneyEmissionsCompareControl.Transport");
            labelEmissionsHeader.Text = GetResource("JourneyEmissionsCompareControl.CO2Emissions");
            labelCostHeader.Text = GetResource("JourneyEmissionsCompareControl.Cost");

		}

		/// <summary>
		/// Populates the journey costs hyperlink
		/// </summary>
        //private void SetUpHyperlinks()
        //{
        //    // Don't display the hyperlinks if in Printable mode

        //    // Only display the links if not in Modify journey mode
        //    if (TDSessionManager.Current.ItineraryMode == ItineraryManagerMode.None)
        //    {
        //        journeyCostsHyperlink.Visible = !printable;
        //        compareEmissionsHyperlink.Visible = !printable && JourneyEmissionsHelper.JourneyEmissionsPTAvailable;
        //        saveFuelLabelLink.Visible = !printable;
        //    }
        //    else
        //    {
        //        journeyCostsHyperlink.Visible = false;
        //        compareEmissionsHyperlink.Visible = false;
        //        saveFuelLabelLink.Visible = false;
        //    }

        //    journeyCostsHyperlink.PrinterFriendly = printable;
        //    journeyCostsHyperlink.Text = GetResource("JourneyEmissionsControl.journeyCostsHyperlink.Text");
        //    journeyCostsHyperlink.ToolTipText = GetResource("JourneyEmissionsControl.journeyCostsHyperlink.Text");

        //    compareEmissionsHyperlink.PrinterFriendly = printable;
        //    compareEmissionsHyperlink.Text = GetResource("JourneyEmissionsControl.compareEmissionsHyperlink.Text");
        //    compareEmissionsHyperlink.ToolTipText = GetResource("JourneyEmissionsControl.compareEmissionsHyperlink.Text");

        //    StringBuilder link = new StringBuilder();
        //    link.Append("<a href=\"");
        //    link.Append( GetResource("JourneyEmissionsControl.saveFuelHyperlink.Link") );
        //    link.Append("\" >");
        //    link.Append( GetResource("JourneyEmissionsControl.saveFuelHyperlink.Text") );
        //    link.Append("</a>");
			
        //    saveFuelLabelLink.Text = link.ToString();
        //    saveFuelLabelLink.ToolTip = GetResource("JourneyEmissionsControl.saveFuelHyperlink.Text");
        //}

		/// <summary>
		/// Creates the compare note text
		/// </summary>
		/// <returns></returns>
		private string CompareNoteText()
		{
			StringBuilder compareNoteText = new StringBuilder();
			StringBuilder compareCarText = new StringBuilder();
            StringBuilder compareYourCarText = new StringBuilder();

			decimal fuelCost = Decimal.Round(Convert.ToDecimal(pageState.FuelCostValue), 0);
			decimal emissions = Convert.ToDecimal(pageState.EmissionsValue);
			decimal fuelCostCompare = 0;
			decimal emissionsCompare = 0;
			// Prevent conversion if compare values are null
			if (pageState.FuelCostCompareValue != null)
			{
				fuelCostCompare = Decimal.Round(Convert.ToDecimal(pageState.FuelCostCompareValue), 0); 
				emissionsCompare = Convert.ToDecimal(pageState.EmissionsCompareValue);
			}

			string space = " ";
			string a = GetResource("JourneyEmissionsDashboard.A.Text");
			string car = GetResource("JourneyEmissionsDashboard.Car.Text");
			string with = GetResource("JourneyEmissionsDashboard.With.Text");
			string ratedAt = GetResource("JourneyEmissionsDashboard.RatedAt.Text");
			string at = GetResource("JourneyEmissionsDashboard.At.Text");
			string pencelitre = GetResource("FindCarPreferencesControl.PencePerLitre");

            // 1) compareYourCarText formation code 
            
            if (pageState.YourCarFuelConsumptionOption)
            {	// e.g. medium petrol car
                compareYourCarText.Append(space);
                compareYourCarText.Append(pageState.YourCarSize.ToLower());
                compareYourCarText.Append(space);
                compareYourCarText.Append(pageState.YourCarFuelType.ToLower());
                compareYourCarText.Append(space);
                compareYourCarText.Append(car);
                compareYourCarText.Append(space);
            }
            else
            {  // e.g. petrol car with xx mpg

                compareYourCarText.Append(space);
                compareYourCarText.Append(pageState.YourCarSize.ToLower());
                compareYourCarText.Append(space);
                compareYourCarText.Append(pageState.YourCarFuelType.ToLower());
                compareYourCarText.Append(space);
                compareYourCarText.Append(car);
                compareYourCarText.Append(space);
                compareYourCarText.Append(with);
                compareYourCarText.Append(space);
                compareYourCarText.Append(pageState.YourCarFuelConsumptionEntered);
                compareYourCarText.Append(space);

                switch (pageState.YourCarFuelConsumptionUnit)
                {
                    case 1:
                        {
                            compareYourCarText.Append(GetResource("JourneyEmissionsDashboard.mpg.Text"));
                        }
                        break;
                    case 2:
                        {
                            compareYourCarText.Append(GetResource("JourneyEmissionsDashboard.l100km.Text"));
                        }
                        break;
                    case 3:
                        {
                            compareYourCarText.Append(GetResource("JourneyEmissionsDashboard.gKm.Text"));
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
                compareYourCarText.Append(space);
            }

            // Add the fuel cost per litre if entered by user
            if (!pageState.YourCarFuelCostOption)
            {
                compareYourCarText.Append(at);
                compareYourCarText.Append(space);
                compareYourCarText.Append(pageState.YourCarFuelCostEntered);
                compareYourCarText.Append(space);
                compareYourCarText.Append(pencelitre);
                compareYourCarText.Append(space);
            }

            compareYourCar = compareYourCarText.ToString();

            // 2) compareCarText formation code
			
			if  (pageState.FuelConsumptionOption)
			{	// e.g. medium petrol car
				compareCarText.Append( space );
				compareCarText.Append( pageState.CarSize.ToLower() );
				compareCarText.Append( space );
				compareCarText.Append( pageState.CarFuelType.ToLower() );
				compareCarText.Append( space );
				compareCarText.Append( car );
				compareCarText.Append( space );
			}
			else
			{  // e.g. petrol car with xx mpg

				compareCarText.Append( space );
				compareCarText.Append( pageState.CarSize.ToLower() );
				compareCarText.Append( space );
				compareCarText.Append( pageState.CarFuelType.ToLower() );
				compareCarText.Append( space );
				compareCarText.Append( car );
				compareCarText.Append( space );
				compareCarText.Append( with );
				compareCarText.Append( space );
				compareCarText.Append( pageState.FuelConsumptionEntered );
				compareCarText.Append( space );
				
				switch (pageState.FuelConsumptionUnit)
				{
					case 1:
					{
						compareCarText.Append( GetResource("JourneyEmissionsDashboard.mpg.Text") );
					}
						break;
					case 2:
					{
						compareCarText.Append( GetResource("JourneyEmissionsDashboard.l100km.Text") );
					}
						break;
					case 3:
					{
						compareCarText.Append( GetResource("JourneyEmissionsDashboard.gKm.Text") );
					}
						break;
					default:
					{
					}
						break;
				}			
				compareCarText.Append( space );
			}

			// Add the fuel cost per litre if entered by user
			if  (!pageState.FuelCostOption)
			{
				compareCarText.Append( at );
				compareCarText.Append( space );
				compareCarText.Append( pageState.FuelCostEntered );
				compareCarText.Append( space );
				compareCarText.Append( pencelitre );
				compareCarText.Append( space );
			}

			compareCar = compareCarText.ToString();

			// Various scenarios
			if ((fuelCost < fuelCostCompare) && (emissions < emissionsCompare))
			{
				// "A <compare car> costs £x mpre and emits yKg Co2 more on this journey than your car."
				compareNoteText.Append( a );
				compareNoteText.Append( compareCar );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.Costs.Text"));
				compareNoteText.Append( fuelCostCompare-fuelCost );
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.MoreAndEmits.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append( emissionsCompare-emissions );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.KgOfCO2.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.MoreThanYourCar.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.ForThisJourney.Text"));
			}
			else if ((fuelCost < fuelCostCompare) && (emissions >= emissionsCompare))
			{
				// "A <compare car> costs £xx more than your car for this journey
				compareNoteText.Append( a );
				compareNoteText.Append( compareCar );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.Costs.Text"));
				compareNoteText.Append( fuelCostCompare-fuelCost );
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.MoreThanYourCar.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.ForThisJourney.Text"));
			}
			else if ((fuelCost >= fuelCostCompare) && (emissions < emissionsCompare))
			{
				// "A <compare car> emits yyKg of CO2 more than your car for this journey
				compareNoteText.Append( a );
				compareNoteText.Append( compareCar );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.Emits.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append( emissionsCompare-emissions );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.KgOfCO2.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.MoreThanYourCar.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.ForThisJourney.Text"));
			}
			else if((fuelCost > fuelCostCompare) && (emissions > emissionsCompare))
			{
				// "A <compare car> costs £x less and emits yKg Co2 less on this journey than your car."
				compareNoteText.Append( a );
				compareNoteText.Append( compareCar );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.Costs.Text"));
				compareNoteText.Append( fuelCost-fuelCostCompare );
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.LessAndEmits.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append( emissions-emissionsCompare );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.KgOfCO2.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.LessThanYourCar.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.ForThisJourney.Text"));
			}
			else if ((fuelCost > fuelCostCompare) && (emissions <= emissionsCompare))
			{
				// "A <compare car> costs £xx less than your car for this journey
				compareNoteText.Append( a );
				compareNoteText.Append( compareCar );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.Costs.Text"));
				compareNoteText.Append( fuelCost-fuelCostCompare );
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.LessThanYourCar.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.ForThisJourney.Text"));
			}
			else if ((fuelCost <= fuelCostCompare) && (emissions > emissionsCompare))
			{
				// "A <compare car> emits yyKg of CO2 less than your car for this journey
				compareNoteText.Append( a );
				compareNoteText.Append( compareCar );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.Emits.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append( emissions-emissionsCompare );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.KgOfCO2.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.LessThanYourCar.Text"));
				compareNoteText.Append( space );
				compareNoteText.Append(GetResource("JourneyEmissionsDashboard.ForThisJourney.Text"));
			}
			else
			{// Dont return anything
				compareNoteText.Append( space );
			}

            //Popluate for your compare Car labels
            string forYourCompareJourney = GetResource("JourneyEmissionsDashboard.forYourJourney.Text")
                + "<br/>"
                + with
                + space
                + a.ToLower()
                + compareYourCar;

			// Populate the For your journey labels
			string forYourJourney = GetResource("JourneyEmissionsDashboard.forYourJourney.Text") 
				+ "<br/>"
				+ with
				+ space
				+ a.ToLower()
				+ compareCar;

			forYourJourneyText1.Text = forYourJourney;
			forYourJourneyText2.Text = forYourJourney;
            forYourJourneyText3.Text = forYourCompareJourney;
            forYourJourneyText4.Text = forYourJourney;

			return compareNoteText.ToString();
		}

		/// <summary>
		/// Populates labels with values displayed on the dashboard
		/// </summary>
		private void UpdateDashboard()
		{
			// Set up the speedo dials
			decimal fuelCost = Convert.ToDecimal(pageState.FuelCostValue); // Your car
			decimal fuelCostCompare = Convert.ToDecimal(pageState.FuelCostCompareValue); // Compare car
			decimal emissions = Convert.ToDecimal(pageState.EmissionsValue); // Your car
			decimal emissionsCompare = Convert.ToDecimal(pageState.EmissionsCompareValue); // Compare car

			if ((pageState.JourneyEmissionsState == JourneyEmissionsState.Output) ||
				(pageState.JourneyEmissionsState == JourneyEmissionsState.OutputDetails))
			{
				// Create the output speedo
				fuelCostSpeedoDial.CreateSpeedo(emissions, pageState.EmissionsScaleMin, pageState.EmissionsScaleMax, SpeedoDialType.FuelCost);
				emissionsSpeedoDial.CreateSpeedo(emissions, pageState.EmissionsScaleMin, pageState.EmissionsScaleMax, SpeedoDialType.CO2Emission);

				// always ensure fuel cost is at least £1
				if (fuelCost < 1)
					fuelCost = 1;

				// Display values on label
				fuelCostValue.Text = Decimal.Round(fuelCost, 0).ToString(); 
				emissionsValue.Text = pageState.EmissionsValue;
                fuelCostValueTable.Text = Decimal.Round(fuelCost, 0).ToString();
                emissionsValueTable.Text = pageState.EmissionsValue;

                emissionsValueTable3.Text = pageState.EmissionsCompareValue;
                fuelCostValueTable3.Text = Decimal.Round(fuelCostCompare, 0).ToString();

			}
			else if ((pageState.JourneyEmissionsState == JourneyEmissionsState.Compare) ||
				(pageState.JourneyEmissionsState == JourneyEmissionsState.CompareDetails))
			{
				// Create the compare speedo
				fuelCostSpeedoDial.CreateCompareSpeedo(emissions, emissionsCompare, pageState.EmissionsScaleMin, pageState.EmissionsScaleMax, SpeedoDialType.FuelCost);
				emissionsSpeedoDial.CreateCompareSpeedo(emissions, emissionsCompare, pageState.EmissionsScaleMin, pageState.EmissionsScaleMax, SpeedoDialType.CO2Emission);

				// always ensure fuel cost is at least £1
				if (fuelCostCompare < 1)
					fuelCostCompare = 1;

				// Display values on label
				fuelCostValue.Text = Decimal.Round(fuelCostCompare, 0).ToString(); 
				emissionsValue.Text = pageState.EmissionsCompareValue;
                fuelCostValueTable.Text = Decimal.Round(fuelCost, 0).ToString();
                emissionsValueTable.Text = pageState.EmissionsValue;

                emissionsValueTable3.Text = pageState.EmissionsCompareValue;
                fuelCostValueTable3.Text = Decimal.Round(fuelCostCompare, 0).ToString();
			}
			
			string compareText = CompareNoteText();

			// Display the compare car journey note
			if ((pageState.JourneyEmissionsState == JourneyEmissionsState.Compare)
				|| (pageState.JourneyEmissionsState == JourneyEmissionsState.CompareDetails))
			{
				// Dont display the compare note if the values are the same
				if ((fuelCost == fuelCostCompare) &&
					(emissions == emissionsCompare))
				{
					compareNotePanel.Visible = false;
				}
				else
				{	// Set compare text
					compareNote.Text = compareText;  
					compareNotePanel.Visible = true;
				}
			}
			else
			{
				compareNotePanel.Visible = false;
			}
		}


        /// <summary>
        /// Returns the string containing the text for the switch button
        /// </summary>
        /// <returns>string</returns>
        protected string GetCommandSwitchText()
        {
            if (graphicalDisplayFuelConsumptionPanel.Visible)
            {
                return GetResource("JourneyEmissionsDashboardControl.CommandSwitchTable.Text");
            }
            else
            {
                return GetResource("JourneyEmissionsDashboardControl.CommandSwitchGraphic.Text");
            }
        }

        public bool displayYourcarRow()
        {
            if (pageState.EmissionsCompareValue == null)
                return false;
            else return true;
        }

		#endregion
		
		#region Public properties

		/// <summary>
		/// Set and get property if this component is in printable mode or not.
		/// </summary>
		public bool Printable
		{
			get { return printable;	}
			set	{ printable = value; }
		}

        /// <summary>
		/// Get property to show whether table view is displayed
		/// </summary>
        public bool IsTableView
        {
            get
            {
                return tableDisplayFuelConsumptionPanel.Visible;
            }
        }



		#endregion
	}
}
