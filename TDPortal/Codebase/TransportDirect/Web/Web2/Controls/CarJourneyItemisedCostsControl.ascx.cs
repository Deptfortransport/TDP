// *********************************************** 
// NAME                 : CarJourneyItemisedCostsControl.ascx.cs 
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 06/01/2003
// DESCRIPTION          : Control that displays costs for car journeys
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarJourneyItemisedCostsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jul 28 2011 16:18:46   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.2   Mar 31 2008 13:19:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:34   mturner
//Initial revision.
//
//   Rev 1.42   Jan 25 2007 12:37:08   mmodi
//Added check for CongestionZoneEnd to determine if charge added
//Resolution for 4346: Congestion Charge: Return journey which ends in zone is shown charge twice
//
//   Rev 1.41   Dec 07 2006 13:55:22   mturner
//Manual merge fro stream4240
//
//   Rev 1.39.1.5   Dec 04 2006 14:11:04   mmodi
//Added alt text for icon
//Resolution for 4288: CO2: Alt text not shown for icons on Tickets/costs page
//
//   Rev 1.39.1.4   Dec 04 2006 13:41:26   dsawe
//added property for accessing SummaryC02EmissionsControl
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.39.1.3   Nov 25 2006 10:24:54   mmodi
//Updated FuelCost calculation for CJP change which returns cost to 10000th p
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.39.1.2   Nov 24 2006 15:07:00   mmodi
//Updated assign of fuel cost value to SummaryCO2 control
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.39.1.1   Nov 24 2006 13:21:10   mmodi
//Updated use of SummaryCO2 control
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.39.1.0   Nov 20 2006 16:15:30   dsawe
//added summaryCO2EmissionsControl
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.39   Oct 06 2006 14:26:52   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.38.1.2   Oct 02 2006 16:18:38   mmodi
//Added CarParkCosts control
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.38.1.1   Sep 12 2006 17:03:40   mmodi
//Set Decimal property to true when Car park costs shown
//Resolution for 4161: Car Parking: Total cost for return car journey incorrect
//
//   Rev 1.38.1.0   Aug 18 2006 17:16:14   mmodi
//Code to add Car Park cost to total
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.38   May 16 2006 17:32:54   CRees
//Fixed bug in total costs related to Park and Ride car park TOID matching. Similar to IR4009.
//
//   Rev 1.37   May 02 2006 16:45:58   jbroome
//IR 4046
//Resolution for 4046: DN068: Netscape/Firefox/IE display issues on the Replan Tickets/Costs page
//
//   Rev 1.36   Apr 27 2006 16:35:56   RPhilpott
//Don't try to use the JourneyParameters to get car size and fuel type if we are in cost-based partition -- use defaults instead.
//Resolution for 4012: DD075: Server error viewing costs for PT journey replaced by car
//
//   Rev 1.35   Apr 20 2006 16:18:08   kjosling
//Will not tally up parking costs if zero or unknown
//Resolution for 44: DEL 8.1 Workstream - Park and Ride Amendments
//
//   Rev 1.34   Apr 19 2006 11:23:54   RGriffith
//Fix: Viewing fares for car journeys in Extend, Adjust and Replan was crashing here.
//Resolution for 3893: Park+Ride: Ambiguity yellow crash screen if ' all costs' selected on car results screen
//
//   Rev 1.33   Apr 11 2006 17:51:18   kjosling
//Costs now include car park fees if applicable. Changed currency display. 
//Resolution for 3841: DN058 Park & Ride phase 2 - Tickets/costs page issues
//
//   Rev 1.32   Mar 23 2006 13:35:48   RGriffith
//Removal of redundant brackets
//
//   Rev 1.31   Mar 23 2006 13:32:52   RGriffith
//Fix: IR3675 - Congestion charge & tolls now correctly calculated for single car journeys
//
//   Rev 1.30   Mar 22 2006 20:27:50   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.29   Mar 14 2006 10:30:14   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.28   Mar 08 2006 17:04:44   CRees
//Fix for road journey pricing for same-day return road tolls. Vantive 4050754
//
//   Rev 1.27   Feb 23 2006 19:16:26   build
//Automatically merged from branch for stream3129
//
//   Rev 1.26.2.1   Mar 06 2006 19:58:08   RGriffith
//Fix to only check return toll costs if a return journey exists
//
//   Rev 1.26.2.0   Mar 01 2006 13:12:12   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.26.1.0   Jan 10 2006 15:23:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.26   Aug 04 2005 13:04:24   asinclair
//No longer initialising the array list in PageLoad
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.25   Aug 03 2005 21:07:26   asinclair
//fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.24   May 20 2005 11:07:02   tmollart
//Changed the following:
// - Split method into two to allow one half to be run on page load and the other on pre-render so no longer needs to be run twice.
// - Changed code that works out minimum and rounded fuel/running costs values to remove non-required conditions and to simplify code.
// - Changed code that works out total cost to remove non-required conditions and to simply code.
// - Removed commented out code that was no longer used and performed general code tidy up.
// - Changed code to control when we display pence.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.23   May 12 2005 17:39:06   tmollart
//Modifed page layout and code behind to aid formatting of Pounds and Pence. Modified code behind so that decimal points are aligned correctly.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.22   May 12 2005 14:37:20   tmollart
//Modified formatting to that decimal places displayed when required.
//Partial fix for 2300.
//
//   Rev 1.21   May 12 2005 12:38:12   Ralavi
//See comment below.
//
//   Rev 1.20   May 12 2005 12:24:30   Ralavi
//Ignore the last comment. This has been changed so that "AA" and "RAC" do not appear as hyperlinks in printer friendly mode.
//
//   Rev 1.19   May 12 2005 12:10:06   Ralavi
//Changes made so that company hyper link is not shown in printer friendly mode on Ticket/costs page. Company name will be displayed as a label.
//
//   Rev 1.18   May 10 2005 17:10:12   rgreenwood
//IR 2300: Refactored DisplayCarCosts() and CalculateCarCosts() so that all values affected by IR2300 are now calculated and stored as properties, and then rounded according to conditions defined in IR.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.17   Apr 25 2005 17:03:38   jgeorge
//Modifications to cope with extend journey
//Resolution for 2297: Del 7 - Car Costing - Zero cost for car journey which is part of multi-mode extended journey
//
//   Rev 1.16   Apr 25 2005 10:24:10   esevern
//added check for road journey (outward or return) not being null in pageload
//
//   Rev 1.15   Apr 22 2005 17:59:44   esevern
//for return car journeys, when either outward or return car cost are change (between fuel/all costs) the other controls display is also changed
//Resolution for 2236: Del 7 - Car Costing - Fuel Costs/Total Costs toggles
//
//   Rev 1.14   Apr 19 2005 19:25:10   esevern
//fix for running costs not appearing on printable page when selected
//
//   Rev 1.13   Apr 13 2005 12:35:20   esevern
//corrected display of fuel and running costs (was displaying tenths of pence as pounds value)
//Resolution for 2030: Del 7  - Defaults for running costs are too high
//Resolution for 2083: Fuel costs are not calculated correctly
//
//   Rev 1.12   Apr 07 2005 16:14:22   rgreenwood
//IR 1997: Added CalMacFerryTollExists property and added checks to CalculateTotalCosts() and CalculateTotalOtherCosts  so that the OtherCostsControl is correctly displayed
//Resolution for 1997: Del 7 - Display of 'Other costs' in Door-to-door
//
//   Rev 1.11   Apr 07 2005 16:01:06   esevern
//fix for incorrect display of fuel costs
//
//   Rev 1.10   Apr 04 2005 19:19:18   esevern
//changes made as a result of FXCop (inc. adding CurrentUICulture when formatting currency string)
//
//   Rev 1.9   Mar 29 2005 17:22:28   esevern
//added '£' to cost display
//
//   Rev 1.8   Mar 24 2005 17:14:40   esevern
//added check for charge value being less than 0.  CalMac charge should not be displayed and will be entered as -1
//
//   Rev 1.7   Mar 16 2005 12:54:28   esevern
//amended to display total cost as pounds and pence
//
//   Rev 1.6   Mar 15 2005 18:34:14   esevern
//set running charge value text
//
//   Rev 1.5   Mar 14 2005 12:37:14   esevern
//Added commenting and calculation of other costs total
//
//   Rev 1.4   Mar 04 2005 15:47:08   esevern
//car costing integration
//
//   Rev 1.3   Feb 11 2005 15:39:08   rgreenwood
//Work in progress
//
//   Rev 1.2   Feb 10 2005 13:42:32   rgreenwood
//Work in Progress
//
//   Rev 1.1   Jan 14 2005 14:04:26   rgreenwood
//Work in Progress
//
//   Rev 1.0   Jan 11 2005 10:25:52   rgreenwood
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Text;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Web.Support;
	using TransportDirect.JourneyPlanning.CJPInterface;

	/// <summary>
	///	Control allowing the display of car journey costs, including
	///	fuel, running, tolls and ferrys.  
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class CarJourneyItemisedCostsControl : TDUserControl
	{
		#region Private members

		// Labels

		// UserControls
		protected TransportDirect.UserPortal.Web.Controls.OtherCostsControl OtherCosts;
		protected TransportDirect.UserPortal.Web.Controls.CostsPageTitleControl CostPageTitle;
		protected TransportDirect.UserPortal.Web.Controls.CarParkCostsControl CarParkCosts;
		protected TransportDirect.UserPortal.Web.Controls.SummaryC02EmissionsControl summaryCO2EmissionsControl;

		// Variables
		protected RoadJourney roadJourney;
		protected TDJourneyViewState viewState;
		private decimal fuelCost;
		private decimal roundedFuelCost;
		private decimal runningCost;
		private decimal roundedRunningCost;
		private decimal totalCost = 0;
		private decimal roundedTotalCost;
		private decimal totalOtherCosts;
		private decimal tollsCost;
		private bool decimaliseTotalCost = false;
		
		/// <summary>
		/// Indicates if this control is for outward or return journey
		/// </summary>
		private bool outward; 

		/// <summary>
		/// Indicates if return journey has been planned
		/// </summary>
		private bool returnExists;  

		/// <summary>
		/// Indicates whether a ferry charge exists
		/// </summary>
		private bool calMacFerryTollExists;
		
		/// <summary>
		/// Indicates whether the running costs should be displayeds
		/// </summary>
		bool showRunning = false; 

		#endregion

		#region Event handlers/overridden methods

		/// <summary>
		/// PreRender event handler.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			//Initialise static controls and display journey costs.
			InitialiseStaticControls();
			DisplayCosts();
		}

		/// <summary>
		/// Page load event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//When the page loads calculate the total costs so that they
			//are available for the PreRender of the parent page.
			CalculateCosts();

			// Set CO2 control property with fuel cost so it can calculate CO2 value
			if (roadJourney != null)
				summaryCO2EmissionsControl.FuelCost = roadJourney.TotalFuelCost;
		}
		#endregion

		#region Public methods

		/// <summary>
		/// Calculates costs for the current journey.
		/// </summary>
		public void CalculateCosts()
		{
			TDItineraryManager.Current.JourneyViewState.VisitedCongestionCompany.Clear();
			
			//Work out the other costs of the journey including if we need to
			//decimalise the total cost.
			CalculateTotalOtherCosts();

			//Get unrounded decimal fuel cost
			FuelCost = CalculateFuelCost();

			//Minimum fuel cost is 1 - then round it to nearest decimal point.
			if (FuelCost < 1)
			{
				FuelCost = 1;
			}
			RoundedFuelCost = Decimal.Round(FuelCost, 0);

			//Get unrounded running costs
			RunningCost = CalculateRunningCost();

			//Minimum running cost is 1 - then round it to nearest decimal point.
			if (runningCost < 1)
			{
				runningCost = 1;
			}
			RoundedRunningCost = Decimal.Round(runningCost, 0);

			//Add up totals depending on what user wants to see. Fuel cost is always
			//included but we selectively add on the running costs.
			TotalCost = 0;
			TotalCost = TotalCost + RoundedFuelCost;

			//If the running costs are visible these need to be added to
			//the total costs.
			if (showRunning)
			{
				TotalCost = TotalCost + RoundedRunningCost;
			}

			//Add on the tolls cost if there is a toll cost.
			if (TollsCost > 0)
			{
				TotalCost = TotalCost + TollsCost;
			}
		}

		public void DisplayCosts()
		{
			//From DisplayRunningCosts() method, set visibility of running cost related text
			labelRunningCharge.Visible = ShowRunning;
			labelRunningCost.Visible = ShowRunning;
			labelRunningInstruction.Visible = ShowRunning;
			imageRunningCost.Visible = ShowRunning;

			this.CostPageTitleControl.ShowCostTypeControl.SetSelectedCostItem(ShowRunning);

			labelFuelCharge.Text = TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol 
				+ RoundedFuelCost.ToString();

			labelRunningCharge.Text = TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol
				+ RoundedRunningCost.ToString();
			
			//Set Total Cost Label	
			string totalCost = TotalCost.ToString();
			if(totalCost.Length > 0)
			{
				int index = totalCost.IndexOf('.');
				if(index > -1)
				{
					labelTotalValuePounds.Text = 
						TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol + 
						totalCost.Substring(0, index);
					labelTotalValuePence.Text = 
						totalCost.Substring(index).PadRight(3, '0');
				}
				else
				{
					labelTotalValuePounds.Text = 
						TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol + totalCost.ToString();
				}
			}
		}

		/// <summary>
		/// Calculates the sum of the other costs (tolls etc), fuel and running costs.
		/// The running cost is only included if the running costs value is displayed
		/// (includeRunning = true)
		/// </summary>
		/// <param name="includeRunning"></param>
		/// <returns>the total journey cost value (pounds and pence) as decimal</returns>
		public decimal CalculateTotalCost()
		{
			decimal journeyCost = CalculateTotalOtherCosts();

			if(journeyCost == 0 && !OtherCosts.ChargeItemExists && !CalMacFerryTollExists)
			{
				OtherCosts.Visible = false;
			}
			else
			{
				OtherCosts.Visible = true;
			}

			if(!CarParkCosts.ChargeItemExists)
			{
				CarParkCosts.Visible = false;
				CarParkCosts.ShowHeaderText = false;
			}
			else
			{
				CarParkCosts.Visible = true;
				if (OtherCosts.ChargeItemExists)
				{
					CarParkCosts.ShowHeaderText = false;
				}
				else
				{
					CarParkCosts.ShowHeaderText = true;
				}
			}

			if (ShowRunning) 
			{
				// add the fuel, running and other costs
				journeyCost = Decimal.Add(journeyCost, CalculateRunningCost());
				journeyCost = Decimal.Add(journeyCost, CalculateFuelCost());
			}
			else 
			{
				journeyCost = Decimal.Add(journeyCost, CalculateFuelCost());
			}

			return journeyCost;
		}

		/// <summary>
		/// Method to set the visiblity of the running cost labels.
		/// </summary>
		/// <param name="showCosts">bool true if running costs should be visible</param>
		public void DisplayRunningCosts() 
		{
			// run cost only requires currency symbol as pence are not displayed
			labelRunningCharge.Text = TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol + (Decimal.Floor(CalculateRunningCost())).ToString();

			labelRunningCharge.Visible = ShowRunning;
			labelRunningCost.Visible = ShowRunning;
			labelRunningInstruction.Visible = ShowRunning;
			imageRunningCost.Visible = ShowRunning;
		}

		/// <summary>
		/// Creation of the total value text done here as this method will need to be
		/// called from the container page for this control when the user switches
		/// between 'fuel' or 'all' costs.
		/// </summary>
		public void SetTotalValueText()
		{
			labelTotalValuePounds.Text = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", 
				CalculateTotalCost());
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

		#region Private Methods

		/// <summary>
		/// Uses the CarCostCalculator to calculate the running cost of 
		/// this journey, based on the fuel type and car size provided
		/// in the OriginalJourneyRequest, and the distance obtained
		/// from the RoadJourney
		/// </summary>
		/// <returns>running cost value in pounds as decimal</returns>
		private decimal CalculateRunningCost()
		{
			if(CarRoadJourney != null) 
			{
				TDJourneyParametersMulti journeyParametersMulti = TDItineraryManager.Current.JourneyParameters as TDJourneyParametersMulti;
				
				string carFuelType	= string.Empty;
				string carSize		= string.Empty;

				if	(journeyParametersMulti == null) 
				{
					IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					carFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
					carSize		= populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);    
				}
				else
				{
					carFuelType = journeyParametersMulti.CarFuelType;
					carSize		= journeyParametersMulti.CarSize;
				}

				CarCostCalculator calculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];
				
				// total distance is in metres, so convert to kilometres for carcostcalculator
				double distance = Convert.ToDouble(CarRoadJourney.TotalDistance/1000);
				
				// returned value from cost calculator is in tenths of pence, so convert to pounds
				decimal cost = Convert.ToDecimal(calculator.CalcRunningCost(carSize, carFuelType, distance));

				return cost/1000;
			}
			else 
			{
				return 0;
			}
		}

		/// <summary>
		/// Uses the CarCostCalculator to calculate the fuel cost of this 
		/// journey, based on the fuel type provided in the OriginalJourneyRequest 
		/// </summary>
		/// <returns>fuel cost value in pounds as decimal</returns>
		private decimal CalculateFuelCost()
		{
			// obtain sum of costs for each drive section from the road journey (value in pounds)
			if(CarRoadJourney != null) 
			{
				decimal pence = Convert.ToDecimal(roadJourney.TotalFuelCost);

				// 2006/11/24 - CJP change means cost is returned in 10,000
				// Therefore divide by 10000 to return it to pence
				pence = pence/10000;
								
				// value is returned in pence, so convert to pounds 
				return pence/100;
			}
			else 
			{
				return 0;
			}
		}

		/// <summary>
		///  Runs through the array list of charge items and totals the 
		///  charges for other costs
		/// </summary>
		/// <param name="chargesList">list of RoadJourneyChargeItems</param>
		/// <returns>sum of charges for other costs</returns>
		private decimal CalculateTotalOtherCosts()
		{
			//Reset TollsCost property to prevent doubling of the TollsCosts figure
			TollsCost = 0;
			
			// sanity check - road journey may be null if there is no return journey
			if(CarRoadJourney != null) 
			{
				// Loop through the array of RoadJourneyDetails and get any associated RoadJourneyChargeItems
				for (int journeyDetailIndex=0; journeyDetailIndex < CarRoadJourney.Details.Length; journeyDetailIndex++)
				{

					//If there is a charge item then we need to decide if this should be added
					//to the tolls total or not.
					if (CarRoadJourney.Details[journeyDetailIndex].ChargeItem != null)
					{
						//NOTE: IR2300 means that we now display pence on the total
						//if we have charge items, even if the charge item is 0.00
						//or is the text "Unknown".
						DecimaliseTotalCost = true;

						//Means it is the outward journey, congestion entry, and there is a charge,
						//therefore for the return we do not want to display a Congestion Entry Charge
						//so set a bool to indicate this.

						// IR 3518 - amended if statement to test for toll being congestion related. This doesn't
						// handle the case where traveling between two different congestion zones.
						
						if (returnExists)
						{
							Journey returnJourney = TDItineraryManager.Current.SelectedReturnJourney;
							TDDateTime outwardTime = roadJourney.JourneyLegs[0].LegStart.DepartureDateTime;
							TDDateTime returnTime = returnJourney.JourneyLegs[0].LegStart.DepartureDateTime;

							if ((outward && returnJourney != null) 
								&& (outwardTime.GetDifferenceDates(returnTime) == 0)
								&& (CarRoadJourney.Details[journeyDetailIndex].TollCost > 0)
								&& ((CarRoadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == StopoverSectionType.CongestionZoneEntry) || 
								(CarRoadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == StopoverSectionType.CongestionZoneEnd) ||
								(CarRoadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == StopoverSectionType.CongestionZoneExit)) )
							{
								TDItineraryManager.Current.JourneyViewState.CongestionChargeAdded = true;
								TDItineraryManager.Current.JourneyViewState.CongestionCostAdded = true;
							}
						}
						
						// IR 3518 - amended if statement to test for toll being congestion related. This doesn't
						// handle the case where traveling between two different congestion zones.
						if ( (!outward && TDItineraryManager.Current.JourneyViewState.CongestionChargeAdded)
							&& ((CarRoadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == StopoverSectionType.CongestionZoneEntry) ||
							(CarRoadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == StopoverSectionType.CongestionZoneEnd) ||
							(CarRoadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == StopoverSectionType.CongestionZoneExit)) )
						{
							// IR 3518 - changed from = 0 to adding 0.
							//then there must be an outward journey with a charge, so we add 0 for this stage
							TollsCost = Decimal.Add(TollsCost, 0);
						} 
						else
						{
							if (CarRoadJourney.Details[journeyDetailIndex].ChargeItem.Charge == -1)
							{
								//A zero cost CalMac ferry exists. Set property
								CalMacFerryTollExists = true;
							}
							else
							{
								//Add the cost of this charge item to the tolls total.
								TollsCost = Decimal.Add(TollsCost, (decimal)roadJourney.Details[journeyDetailIndex].ChargeItem.Charge / 100); 
							}
						}					
					}
				}
				//Check the last leg of the destination for an associated car park. If there is 
				//a match, add the cost of the car park to the total journey costs.
				TDLocation location = CarRoadJourney.DestinationLocation;
				if (location.ParkAndRideScheme != null)
				{
					// Below amended CR 16/05/06 to use destination -1 toid to get car park costs.
					CarParkInfo info = location.CarPark = location.ParkAndRideScheme.MatchCarPark(CarRoadJourney.Details[CarRoadJourney.Details.Length - 1].Toid);
					if(info != null)
					{
						if(info.MinimumCost > 0)
						{
							decimal chargeItem = (decimal)info.MinimumCost / 100;
							TollsCost = Decimal.Add(TollsCost, chargeItem);
						}
					}
				}
				//Check if its a Car park (not a Park and Ride Scheme car park) and add the cost.
				//Only add if its the Destination of the Outward segment
				if (outward)
				{
					if (location.CarParking != null)
					{
						if (location.CarParking.MinimumCost > 0)
						{
							decimal chargeItem = (decimal)location.CarParking.MinimumCost / 100;
							TollsCost = Decimal.Add(TollsCost, chargeItem);

							// Set here because may not get executed in above loop if there are no tolls
							DecimaliseTotalCost = true;
						}
					}
				}
			}
			return TollsCost;
		}

		/// <summary>
		/// Sets up control labels.
		/// </summary>
		private void InitialiseStaticControls()
		{
			labelTotalCost.Text = GetResource("CarJourneyItemisedCostsControl.labelTotalCost");

			if(IsOutward) 
			{
				if (CostPageTitle.HideHelpButton)
					CostPageTitle.Title.Text = GetResource("CostPageTitle.labelCostPageTitleCarCosts");
				else
					CostPageTitle.Title.Text = GetResource("CostPageTitle.labelCostPageTitle");
				labelTotalCost.Text += GetResource("CarJourneyItemisedCostsControl.outward");
			}
			else 
			{
				if (CostPageTitle.HideHelpButton)
					CostPageTitle.Title.Text = GetResource("CostPageTitle.labelCostPageTitleCarCosts");
				else
					CostPageTitle.Title.Text = GetResource("CostPageTitle.returnLabelCostPageTitle");
				labelTotalCost.Text += GetResource("CarJourneyItemisedCostsControl.return");
			}

			//Set labels
			imageFuelCost.ImageUrl = GetResource("CarJourneyItemisedCostsControl.imageFuelCost");
			imageFuelCost.AlternateText = " ";
			labelFuelCost.Text = GetResource("CarJourneyItemisedCostsControl.labelFuelCost");
			labelFuelInstruction.Text = GetResource("CarJourneyItemisedCostsControl.labelFuelInstruction");
			labelReturnInstruction.Text = GetResource("CarJourneyItemisedCostsControl.labelReturnInstruction");
			//labelParkingCost.Text = GetResource("CarJourneyItemisedCostsControl.labelParkingCost");
			
			if ((PageId == PageId.PrintableJourneyFares) || PageId == PageId.PrintableRefineTickets)
			{
				labelRunningInstruction.Text = GetResource("PrintableJourneyFares.labelRunningInstruction");
			}
			else
			{
				labelRunningInstruction.Text = GetResource("CarJourneyItemisedCostsControl.labelRunningInstruction");
			}
			
			labelRunningCost.Text = GetResource("CarJourneyItemisedCostsControl.labelRunningCost");
			imageRunningCost.ImageUrl = GetResource("CarJourneyItemisedCostsControl.imageRunningCost");
			imageRunningCost.AlternateText = " ";
		}
		#endregion

		#region Public Properties

		/// <summary>
		/// Read only property returning reference to SummaryC02EmissionsControl
		/// </summary>
		public SummaryC02EmissionsControl SummaryC02EmissionsControl
		{
			get { return summaryCO2EmissionsControl;}
		}
			/// <summary>
			///	Read only property returning reference to the OtherCostsControl
			/// </summary>
			public OtherCostsControl OtherCostsControl
		{
			get { return OtherCosts; }
		}

		/// <summary>
		///	Read only property returning reference to the CarParkCostsControl
		/// </summary>
		public CarParkCostsControl CarParkCostsControl
		{
			get { return CarParkCosts; }
		}

		/// <summary>
		///	Read only property returning reference to the CostPageTitleControl
		/// </summary>
		public CostsPageTitleControl CostPageTitleControl
		{
			get { return CostPageTitle; }
		}

		/// <summary>
		/// Read only property returning reference to the returnInstruction label
		/// </summary>
		public Label ReturnInstructionLabel 
		{
			get { return labelReturnInstruction; }
		}

		/// <summary>
		/// Read/write property returning the 
		/// road journey for this journey result
		/// (may be outward or return), and calls CalculateCosts()
		/// in order to set cost properties in this control so that
		/// JourneyFares page can access them and set the Total Cost label
		/// </summary>
		public RoadJourney CarRoadJourney
		{
			get { return roadJourney; }
			set { roadJourney = value; }
		}

		/// <summary>
		/// Read/write property returning true if this
		/// control is for the outward portion of the 
		/// car journey, false if return.
		/// </summary>
		public bool IsOutward
		{
			get { return outward; }
			set { outward = value; }
		}

		/// <summary>
		/// Read/write property returing true if the 
		/// journey has a return portion
		/// </summary>
		public bool ReturnExists
		{
			get { return returnExists; }
			set { returnExists = value; }
		}

		/// <summary>
		/// Read/write property returing true if the 
		/// running costs are visible
		/// </summary>
		public bool ShowRunning
		{
			get { return showRunning; }
			set { showRunning = value; }
		}

		/// <summary>
		/// Read/write property returing true if the 
		/// journey has a ferry charge item, regardless
		/// of it's cost
		/// </summary>
		public bool CalMacFerryTollExists
		{
			get { return calMacFerryTollExists; }
			set { calMacFerryTollExists = value; }
		}

		///<summary>
		///Read/Write property to set/get fuel cost. Fuel cost
		///is the calculated fuel cost for the current journey.
		/// </summary>
		public decimal FuelCost
		{
			get { return fuelCost; }
			set { fuelCost = value; }
		}

		/// <summary>
		///Read/Write property to set/get rounded fuel cost.
		///This value is a rounded version of the actual calculated fuel cost.
		/// </summary>
		public decimal RoundedFuelCost
		{
			get { return roundedFuelCost; }
			set { roundedFuelCost = value; }
		}

		/// <summary>
		///Read/Write property to set/get running cost.
		///Value is the calculated running cost for the journey.
		/// </summary>
		public decimal RunningCost
		{
			get { return runningCost; }
			set { runningCost = value; }
		}

		/// <summary>
		///Read/Write property to set/get the rounded running cost.
		///Value is the rounded version of the total running cost.
		/// </summary>
		public decimal RoundedRunningCost
		{
			get { return roundedRunningCost; }
			set { roundedRunningCost = value; }
		}
		
		/// <summary>
		///Read/Write total journey cost.
		///Total calculated cost of the journey which takes into account if
		///the user is showing running costs. This will affect the value.
		/// </summary>
		public decimal TotalCost
		{
			get { return totalCost; }
			set { totalCost = value; }
		}

		/// <summary>
		/// Read/Write rounded version of the total cost.
		/// </summary>
		public decimal RoundedTotalCost
		{
			get { return roundedTotalCost; }
			set { roundedTotalCost = value; }
		}

		/// <summary>
		/// Read/Write total other journey costs e.g. tolls/ferries etc.
		/// </summary>
		public decimal TotalOtherCosts
		{
			get { return totalOtherCosts; }
			set { totalOtherCosts = value; }
		}

		/// <summary>
		/// Read/Write tolls cost.
		/// </summary>
		public decimal TollsCost
		{
			get { return tollsCost; }
			set { tollsCost = value; }
		}
		
		/// <summary>
		/// Read/Write. Is the total cost displayed decimalised (with pence).
		/// </summary>
		public	bool DecimaliseTotalCost
		{
			get { return decimaliseTotalCost; }
			set { decimaliseTotalCost = value; }
		}
		#endregion
	}
}
