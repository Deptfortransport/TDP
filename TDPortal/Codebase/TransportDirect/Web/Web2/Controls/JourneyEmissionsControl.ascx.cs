// *********************************************** 
// NAME                 : JourneyEmissionsControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2006 
// DESCRIPTION			: Control displaying the Emissions for a journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionsControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Dec 19 2008 15:06:18   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   May 06 2008 11:06:12   apatel
//moved CommandSwitchView button to the top of the journey emissions page.
//Resolution for 4906: Carbon Dials - input page icons and table view button
//
//   Rev 1.2   Mar 31 2008 13:21:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:28   mturner
//Initial revision.
//
//   Rev 1.11   Jun 07 2007 16:08:10   mmodi
//Added note for dials within this control
//
//   Rev 1.10   Jan 03 2007 14:25:14   mmodi
//Updated to populate the Your Car parameters
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.9   Dec 15 2006 11:08:32   mmodi
//Removed rounding of Fuel Cost up to £1 to store actual value to session
//Resolution for 4321: CO2: Use accurate Scales when calculating angle
//
//   Rev 1.8   Dec 14 2006 11:01:10   mmodi
//Added Panel to control positioning of Next button
//Resolution for 4319: CO2: Next button to be made inline with Car size drop downs
//
//   Rev 1.7   Dec 05 2006 15:52:30   mmodi
//Updated fuel cost value following helper changes
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.6   Nov 29 2006 15:10:56   mmodi
//Rounded fuel cost to £1 when less than 1
//Resolution for 4240: CO2 Emissions
//Resolution for 4279: CO2: Fuel costs less than £1 not shown correctly
//
//   Rev 1.5   Nov 27 2006 10:37:16   dsawe
//changed totalFuelCost += TotalFuelCost;
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Nov 26 2006 18:40:32   mmodi
//Added TotalFuelCost property
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.3   Nov 26 2006 15:44:40   mmodi
//Set visibility of controls dependent on Printable
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 25 2006 14:09:50   mmodi
//Removed redundant code
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.1   Nov 24 2006 11:17:00   mmodi
//Code changes and updates as part of workstream
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Nov 19 2006 10:38:10   mmodi
//Initial revision.
//Resolution for 4240: CO2 Emissions
//

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
using System.Web.UI;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		JourneyEmissionsControl containing Dashboard and Input details controls.
	/// </summary>
	public partial class JourneyEmissionsControl : TDUserControl
	{

		#region Controls

		protected TransportDirect.UserPortal.Web.Controls.HelpCustomControl helpJourneyEmissions;

		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCarInputControl journeyEmissionsCarInput;
		protected TransportDirect.UserPortal.Web.Controls.JourneyEmissionsDashboardControl journeyEmissionsDashboard;



		#endregion

		#region Variables

		private JourneyEmissionsState journeyEmissionsState;
		private bool printable;
		private JourneyEmissionsPageState pageState;
		private RoadJourney roadJourneyOutward;
		private RoadJourney roadJourneyReturn;

		/// <summary>
		/// Hold user's current journey parameters
		/// </summary>
		private TDJourneyParametersMulti journeyParams;

		#endregion

		#region Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// retrieve page state from session
			pageState = TDSessionManager.Current.JourneyEmissionsPageState;		
			journeyEmissionsState = pageState.JourneyEmissionsState;
	
			// Set the printable values
			journeyEmissionsDashboard.Printable = this.printable;
			journeyEmissionsCarInput.Printable = this.printable;

			PopulateLabels();
		}

		/// <summary>
		/// Page Prerender
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{	
			SetControlVisibility();

			// Panel used to position the Next button to be inline with dropdowns on the input panel
			// Not a nice way to do this but the car images on the Car Input page are not always shown
			// so we need to adjust the panel height in order to raise or lower the next button.
			if ( (pageState.JourneyEmissionsState == JourneyEmissionsState.Input)
				|| pageState.JourneyEmissionsState == JourneyEmissionsState.InputDetails)
				panelNextButton.Height = 73;
			else
				panelNextButton.Height = 38;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets label text
		/// </summary>
		private void SetControlVisibility()
		{
			// Set Details control visibility
			switch (journeyEmissionsState)
			{
				case JourneyEmissionsState.Input:
				{
					journeyEmissionsCarInput.CarImagesVisible = true;
					journeyEmissionsCarInput.CarMoreDetailsVisible = false;
					commandMoreDetails.Visible = true;
					panelDashboard.Visible = false;
					panelNotes.Visible = false;
				}
					break;
				case JourneyEmissionsState.Output:
				case JourneyEmissionsState.Compare:
				{
					journeyEmissionsCarInput.CarImagesVisible = false;
					journeyEmissionsCarInput.CarMoreDetailsVisible = false;
					commandMoreDetails.Visible = true;
					panelDashboard.Visible = true;
					panelNotes.Visible = true;
				}
					break;
				case JourneyEmissionsState.InputDetails:
				{
					journeyEmissionsCarInput.CarImagesVisible = true;
					journeyEmissionsCarInput.CarMoreDetailsVisible = true;
					commandMoreDetails.Visible = false;
					panelDashboard.Visible = false;
					panelNotes.Visible = false;
				}
					break;
				case JourneyEmissionsState.OutputDetails:
				case JourneyEmissionsState.CompareDetails:
				{
					journeyEmissionsCarInput.CarImagesVisible = false;
					journeyEmissionsCarInput.CarMoreDetailsVisible = true;
					commandMoreDetails.Visible = false;
					panelDashboard.Visible = true;
					panelNotes.Visible = true;
				}
					break;
			}

			// Do not show the the Car preferences input panel if we're in Printable mode
			journeyEmissionsCarInput.Visible = !printable;

			// As More details is set above, hide if in printable mode
			if (printable)
				commandMoreDetails.Visible = !printable;
		}

		/// <summary>
		/// Sets label text
		/// </summary>
		private void PopulateLabels()
		{
			// Set text
			commandMoreDetails.Text = GetResource("JourneyEmissionsControl.commandMoreDetails.Text");
			commandMoreDetails.ToolTip = GetResource("JourneyEmissionsControl.commandMoreDetails.Text");

			commandNext.Text = GetResource("JourneyEmissionsControl.commandNext.Text");
			commandNext.ToolTip = GetResource("JourneyEmissionsControl.commandNext.Text");

			// Do not show the command buttons if in printable mode
			commandMoreDetails.Visible = !printable;
			commandNext.Visible = !printable;

			// Do not show the horizontal rule if in printable mode, doesnt render correctly
			panelHorizontalRuleOne.Visible =!printable;

			// labels
			notes.Text = GetResource("JourneyEmissionsControl.Notes");
			dialColours.Text = GetResource("JourneyEmissionsControl.DialColours");						
		}

		/// <summary>
		/// Validates the user inpout details
		/// </summary>
		private bool ValidatePreferences()
		{
			CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();
				
			// Validate fuel cost entered by the user
			if (journeyEmissionsCarInput.FuelCostOption)
				pageState.FuelCostValid = true;
			else
				pageState.FuelCostValid = 
					FuelValidation.ValidateFuelCost( journeyEmissionsCarInput.FuelCostText );

			// Validate fuel consumption eneterd by the user
			if (journeyEmissionsCarInput.FuelConsumptionOption)
				pageState.FuelConsumptionValid = true;
			else
				pageState.FuelConsumptionValid = 
					FuelValidation.ValidateFuelConsumption( journeyEmissionsCarInput.FuelConsumptionText, journeyEmissionsCarInput.FuelConsumptionUnit);

			return (pageState.FuelCostValid && pageState.FuelConsumptionValid);
		}

		/// <summary>
		/// Updates the Journey Emissions page state when Next is clicked
		/// </summary>
		private void UpdatePageState()
		{

			switch (journeyEmissionsState)
			{
				case JourneyEmissionsState.Input:
				{
					journeyEmissionsState = JourneyEmissionsState.Output;
				}
					break;
				case JourneyEmissionsState.Output:
				{
					journeyEmissionsState = JourneyEmissionsState.Compare;
				}
					break;
				case JourneyEmissionsState.Compare:
				{
					journeyEmissionsState = JourneyEmissionsState.Compare;
				}
					break;
				case JourneyEmissionsState.InputDetails:
				{
					journeyEmissionsState = JourneyEmissionsState.OutputDetails;
				}
					break;
				case JourneyEmissionsState.OutputDetails:
				{
					journeyEmissionsState = JourneyEmissionsState.CompareDetails;
				}
					break;
				case JourneyEmissionsState.CompareDetails:
				{
					journeyEmissionsState = JourneyEmissionsState.CompareDetails;
				}
					break;
				default:
				{
					journeyEmissionsState = JourneyEmissionsState.Input;
				}
					break;
			}

			pageState.JourneyEmissionsState = journeyEmissionsState;
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
            commandNext.Click += new EventHandler(this.CommandNextClick);
            commandMoreDetails.Click += new EventHandler(this.CommandMoreDetailsClick);
		}

		#endregion

		#region Event handlers

		/// <summary>
		/// Click event for next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void CommandNextClick(object sender, EventArgs e)
		{
            // Only calculate values and move to the Output or Compare state if user input preferences are valid
			if (ValidatePreferences())
			{
				# region Set PageState values
				// Set the values of the Input details to the Session
				pageState.CarSize = journeyEmissionsCarInput.CarSize;
				pageState.CarFuelType = journeyEmissionsCarInput.FuelType;
				pageState.FuelConsumptionOption = journeyEmissionsCarInput.FuelConsumptionOption;
				pageState.FuelConsumptionEntered = journeyEmissionsCarInput.FuelConsumptionValue;
				pageState.FuelConsumptionUnit = journeyEmissionsCarInput.FuelConsumptionUnit;
				pageState.FuelCostOption = journeyEmissionsCarInput.FuelCostOption;
				pageState.FuelCostEntered = journeyEmissionsCarInput.FuelCostValue;
				
				// Need to set the YourCar values incase the user selects the link which replans journey
				// This link should replan the Your Car and not the Compare Car
				if ((pageState.JourneyEmissionsState == JourneyEmissionsState.Input)
					||  (pageState.JourneyEmissionsState == JourneyEmissionsState.InputDetails) )
				{
					pageState.YourCarSize = journeyEmissionsCarInput.CarSize;
					pageState.YourCarFuelType = journeyEmissionsCarInput.FuelType;
					pageState.YourCarFuelConsumptionOption = journeyEmissionsCarInput.FuelConsumptionOption;
					pageState.YourCarFuelConsumptionEntered = journeyEmissionsCarInput.FuelConsumptionValue;
					pageState.YourCarFuelConsumptionUnit = journeyEmissionsCarInput.FuelConsumptionUnit;
					pageState.YourCarFuelCostOption = journeyEmissionsCarInput.FuelCostOption;
					pageState.YourCarFuelCostEntered = journeyEmissionsCarInput.FuelCostValue;
				}
				#endregion
					
				// Calculate Emissions values			
                journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;

				// Obtain the fuel cost from the original journey planned
				decimal totalFuelCost = 0;

				if (roadJourneyOutward != null)
					totalFuelCost = totalFuelCost + roadJourneyOutward.TotalFuelCost;

				if (roadJourneyReturn != null)
					totalFuelCost = totalFuelCost + roadJourneyReturn.TotalFuelCost;

				// Add the totalfuelcost from the property
				totalFuelCost += TotalFuelCost;

				// Set up the emissions helper used to calculate values
				JourneyEmissionsHelper emissionsHelper = new JourneyEmissionsHelper(journeyParams, totalFuelCost);

				decimal fuelCost = emissionsHelper.GetFuelCost(pageState);

				if ((pageState.JourneyEmissionsState == JourneyEmissionsState.Input) ||
					(pageState.JourneyEmissionsState == JourneyEmissionsState.InputDetails))
				{
					pageState.FuelCostValue = fuelCost.ToString();
					pageState.EmissionsValue = emissionsHelper.GetEmissions(pageState).ToString();
				}
				else
				{	
					pageState.FuelCostCompareValue = fuelCost.ToString();
					pageState.EmissionsCompareValue = emissionsHelper.GetEmissions(pageState).ToString();
				}
				
				// Set the scale values
				pageState.FuelCostScaleMax = emissionsHelper.GetFuelCostScaleMax();
				pageState.FuelCostScaleMin = emissionsHelper.GetFuelCostScaleMin();
				pageState.EmissionsScaleMax = emissionsHelper.GetEmissionsScaleMax();
				pageState.EmissionsScaleMin = emissionsHelper.GetEmissionsScaleMin();

				// To ensure when page is reloaded the controls are in the correct state
				UpdatePageState();
			}

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;
		}

		/// <summary>
		/// Click event for next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void CommandMoreDetailsClick(object sender, EventArgs e)
		{
            journeyEmissionsCarInput.CarMoreDetailsVisible = true;
			commandMoreDetails.Visible = false;

			switch (journeyEmissionsState)
			{
				case JourneyEmissionsState.Input:
				{
					journeyEmissionsState = JourneyEmissionsState.InputDetails;
				}
					break;
				case JourneyEmissionsState.Output:
				{
					journeyEmissionsState = JourneyEmissionsState.OutputDetails;
				}
					break;
				case JourneyEmissionsState.Compare:
				{
					journeyEmissionsState = JourneyEmissionsState.CompareDetails;
				}
					break;
			}

			pageState.JourneyEmissionsState = journeyEmissionsState;

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Set and get property of the state the Journey Emissions control is in
		/// </summary>
		public JourneyEmissionsState JourneyEmissionsState
		{
			get { return journeyEmissionsState; }
			set { journeyEmissionsState = value; }
		}
		
		/// <summary>
		/// Set and get property if this component is in printable mode or not.
		/// </summary>
		public bool Printable
		{
			get { return printable;	}
			set	{ printable = value; }
		}

		/// <summary>
		/// Set and get property for the car journey - outward
		/// </summary>
		public RoadJourney CarRoadJourneyOutward
		{
			get { return roadJourneyOutward; }
			set { roadJourneyOutward = value; }
		}

		/// <summary>
		/// Set and get property for the car journey - return
		/// </summary>
		public RoadJourney CarRoadJourneyReturn
		{
			get { return roadJourneyReturn; }
			set { roadJourneyReturn = value; }
		}

		private decimal totalFuelCost = 0;
		/// <summary>
		/// Get and Set the TotalFuelCost 
		/// </summary>
		public decimal TotalFuelCost
		{
			get { return totalFuelCost; }
			set { totalFuelCost = value; }
		}

        /// <summary>
        /// Get the value if showing table view 
        /// </summary>
        public bool IsTableView
        {
            get
            {
                return journeyEmissionsDashboard.IsTableView;
            }
        }

        /// <summary>
        /// Returns the Dashboard view change buttons text depending on wether
        /// dashboard is in diagram view or table view.
        /// </summary>
        public string CommandSwitchDashboardViewText
        {
            get
            {
                if (IsTableView)
                {
                    return GetResource("JourneyEmissionsDashboardControl.CommandSwitchGraphic.Text");
                }
                else
                {
                    return GetResource("JourneyEmissionsDashboardControl.CommandSwitchTable.Text");

                }
            }

        }

        /// <summary>
        /// Hides the panelText if showing table view
        /// </summary>
        public void hideTextPanel()
        {
            foreach (Control control in panelNotes.Controls)
            {
                control.Visible = false;
            }
        }

        /// <summary>
        /// Switches Journey emissions dashboad from diagram to table view and vice versa.
        /// </summary>
        public void SwitchDashboardView()
        {
            this.journeyEmissionsDashboard.CommandSwitchClick();
        }

        /// <summary>
        /// Gets the spacer img alt text from content database.
        /// </summary>
        /// <returns>string - alt text for spacer img</returns>
        protected string GetSpacerText()
        {
            return GetResource("JourneyEmissions.imageSpacer.AlternateText");
        }

        
        #endregion

	}
}
