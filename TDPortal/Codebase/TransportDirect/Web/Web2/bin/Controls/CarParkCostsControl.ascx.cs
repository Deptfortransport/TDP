// *********************************************** 
// NAME                 : CarParkCostsControl.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 29/09/2006
// DESCRIPTION          : Repeater control that displays a list of Car Park costs
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarParkCostsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jul 28 2011 16:18:48   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.2   Mar 31 2008 13:19:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:40   mturner
//Initial revision.
//
//   Rev 1.1   Dec 07 2006 14:37:50   build
//Automatically merged from branch for stream4240
//
//   Rev 1.0.1.1   Dec 04 2006 14:11:52   mmodi
//Added alt text for icon
//Resolution for 4240: CO2 Emissions
//Resolution for 4288: CO2: Alt text not shown for icons on Tickets/costs page
//
//   Rev 1.0.1.0   Nov 21 2006 15:54:46   dsawe
//aded logo for CarParkCosts & otherimage costs
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Oct 02 2006 16:12:50   mmodi
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.LocationService;

	/// <summary>
	///		Summary description for CarParkCostsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class CarParkCostsControl : TDUserControl
	{
		private RoadJourney roadJourney;
		private bool chargeItemExists = false;
		private bool boolPrintablePage = false;
		private bool outward;
		private bool showHeaderText = false;

		private string linkText = string.Empty;
		private string linkCommand = string.Empty;
		private string costPounds = string.Empty;
		private string costPence = string.Empty;
		private string costFree = string.Empty;
		private string costUnknown = string.Empty;
		private string note = string.Empty;
		private string headerText = string.Empty;
		private string otherCostsImageURL = string.Empty;
		private string carParkCostsImageURL = string.Empty;

		protected System.Web.UI.WebControls.Label labelCostUnknown;
		protected HyperlinkPostbackControl carParkInfoLinkControl;

		#region Public Properties

		/// <summary>
		/// Read only property supplying the header text 
		/// for the control
		/// </summary>
		public bool ShowHeaderText
		{
			get
			{
				return showHeaderText;
			}
			set
			{
				showHeaderText = value;
			}

		}

		/// <summary>
		/// Read/write property returning the 
		/// road journey for this journey result
		/// (may be outward or return)
		/// </summary>
		public RoadJourney CarRoadJourney
		{
			get 
			{
				return roadJourney;
			}
			set 
			{
				roadJourney = value;
			}
		}

		/// <summary>
		/// Read/write property returning true if this
		/// control is for the outward portion of the 
		/// car journey, false if return.
		/// </summary>
		public bool IsOutward
		{
			get
			{
				return outward;
			}
			set
			{
				outward = value;
			}
		}

		/// <summary>
		/// Read/write property returning true 
		/// if there are chargeitems to display
		/// in the othercostscontrol
		/// </summary>
		public bool ChargeItemExists
		{
			get 
			{
				return chargeItemExists;
			}
			set 
			{
				chargeItemExists = value;
			}
		}

		/// <summary>
		/// Writeable only property setting the printable status of the page - true if control 
		/// is on a printable page 
		/// </summary>
		public bool Printable
		{
			set
			{
				boolPrintablePage = value;
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Iterates through the list of road journey details to find a 
		/// Car park charge item for this journey
		/// </summary>
		private void DisplayCharges () 
		{
			TDLocation destinationLocation = roadJourney.DestinationLocation;

			if(CarRoadJourney != null) 
			{
				if (outward)
				{
					if (destinationLocation.CarParking != null)
					{
						DisplayCarParkCost(destinationLocation);
						chargeItemExists = true;

						// set visibility
						carParkInfoLinkControl.Visible = this.Visible;
						labelCarParkPound.Visible = this.Visible;
						labelCarParkPence.Visible = this.Visible;
						labelCarParkNote.Visible = this.Visible;
						imageCarParkCost.Visible = this.Visible;

						// set printer friendly
						carParkInfoLinkControl.PrinterFriendly = boolPrintablePage;

					}
				}

				// Check if there are any OtherCosts so that we can set visibility of
				// the OtherCosts title on this control. Prevents it being displayed twice
				// Not good code but can't get it to set any other way.
				
				bool otherCostItemExists = false;
				// Loop through the array of RoadJourneyDetails and get any associated RoadJourneyChargeItems
				for (int journeyDetailIndex=0; journeyDetailIndex < roadJourney.Details.Length; journeyDetailIndex++)
				{
					if (roadJourney.Details[journeyDetailIndex].ChargeItem != null)
					{
						otherCostItemExists = true;
					}
				}
				//check for a park and ride scheme
				if (destinationLocation.ParkAndRideScheme != null)
				{
					otherCostItemExists = true;
				}

				if ((chargeItemExists) && (!otherCostItemExists))
					showHeaderText = true;
				else
					showHeaderText = false;

				// display header if needed
				otherCostsHeaderLabel.Visible = showHeaderText;
				imageOtherCost.Visible = showHeaderText;
			}
		}

		private void DisplayCarParkCost(TDLocation location)
		{
			CarPark carPark = location.CarParking;

			bool isParkAndRide = (carPark.ParkAndRideIndicator.Trim().ToUpper() == "TRUE");
			string carParkDisplayName;
			
			if (isParkAndRide)
			{
				carParkDisplayName = carPark.Location + ", " + carPark.Name + 
					Global.tdResourceManager.GetString("CarParking.ParkAndRide.Suffix", TDCultureInfo.CurrentUICulture);
			}
			else
			{
				carParkDisplayName = carPark.Location + ", " + carPark.Name + 
					Global.tdResourceManager.GetString("CarParking.Suffix", TDCultureInfo.CurrentUICulture);
			}
			
			string minimumCostPound;
			string minimumCostPence;
			switch(carPark.MinimumCost)
			{
				case -1:
					minimumCostPound = costUnknown;
					minimumCostPence = string.Empty;
					break;
				case 0:
					minimumCostPound = costFree;
					minimumCostPence = string.Empty;
					break;
				default:
					// convert the charge item (int) to pounds and pence
					decimal chargeItem = (decimal)carPark.MinimumCost / 100;
					string temp = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", chargeItem);
					minimumCostPound = temp.Substring(0,temp.Length - 3);
					minimumCostPence = temp.Substring(temp.Length - 3,3);
					if(minimumCostPence == ".00")
					{
						minimumCostPence = string.Empty;
					}
					break;
			}

			string carParkNote;
			// only display the car park note if there is a known charge
			if (carPark.MinimumCost <= -1)
			{
				carParkNote = string.Empty;
			}
			else
			{
				carParkNote = Global.tdResourceManager.GetString(
					"CarParking.MinimumCostNote", TDCultureInfo.CurrentUICulture);				
			}

			// populate the private variables
			linkCommand = carPark.CarParkReference;
			linkText= carParkDisplayName;
			costPounds = minimumCostPound;
			costPence = minimumCostPence;
			note = carParkNote;

			SetTableRowDetails();
		}

		private void SetTableRowDetails()
		{
			carParkInfoLinkControl.CommandArgument = linkCommand;
			carParkInfoLinkControl.CommandName = linkCommand;
			carParkInfoLinkControl.Text = linkText;
			carParkInfoLinkControl.ToolTipText = linkText;

			labelCarParkPound.Text = costPounds;
			labelCarParkPence.Text = costPence;
			labelCarParkNote.Text = note;
			
		}

		#endregion

		#region Event handlers

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Set value so its available through full page cycle.
			costUnknown = GetResource("OtherCostsControl.UnknownCost");
			costFree = GetResource("CarParking.Free");
			headerText = GetResource("OtherCostsControl.labelHeader");
			otherCostsImageURL = GetResource("OtherCostsControl.imageOtherCosts");
			carParkCostsImageURL = GetResource("CarParking.imageCarParkCosts");
			imageOtherCost.ImageUrl = otherCostsImageURL;
			imageOtherCost.AlternateText = " ";
			otherCostsHeaderLabel.Text = headerText;
			imageCarParkCost.ImageUrl = carParkCostsImageURL;
			imageCarParkCost.AlternateText = " ";
		}

		/// <summary>
		/// Calls DisplayCharges() to show the car park items for this journey 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			DisplayCharges();
		}
		/// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void CarParkInformationLinkClick(object sender, EventArgs e)
		{
			// Set the car park reference to session
			TDLocation destinationLocation = roadJourney.DestinationLocation;
			TDSessionManager.Current.InputPageState.CarParkReference = destinationLocation.CarParking.CarParkReference;

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

			// Show the information page for the selected location.
			// Write the Transition Event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.CarParkInformation;
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
			this.carParkInfoLinkControl.link_Clicked += new System.EventHandler(this.CarParkInformationLinkClick);
		}

		#endregion

	}
}
