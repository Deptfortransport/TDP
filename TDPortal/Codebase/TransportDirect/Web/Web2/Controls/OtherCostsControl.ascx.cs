// *********************************************** 
// NAME                 : OtherCostsControl.ascx.cs 
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 21/12/2003
// DESCRIPTION          : Repeater control that displays a list of 
//						  other costs e.g. ferry and tolls on the costs page.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/OtherCostsControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Jul 28 2011 16:19:18   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.3   Oct 27 2010 17:09:40   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:22:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:50   mturner
//Initial revision.
//
//   Rev 1.29   Jan 12 2007 13:19:42   jfrank
//Changed for IR4277 - Adding and end instruction to a road journey ending in the congegestion zone at a time when a charge applies, if it entered at a time when a charge didn't apply.
//Resolution for 4277: Congestion Charge Addendum
//
//   Rev 1.28   Dec 07 2006 14:12:50   mturner
//Manual merge for stream4240
//
//   Rev 1.26.1.3   Dec 04 2006 14:15:56   mmodi
//Added alt text for icon
//Resolution for 4240: CO2 Emissions
//Resolution for 4288: CO2: Alt text not shown for icons on Tickets/costs page
//
//   Rev 1.26.1.2   Nov 27 2006 12:25:00   mmodi
//Amended code to align a £0 cost to the pounds column
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.26.1.1   Nov 22 2006 15:07:04   dsawe
//added image for plan & ride car park
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.26.1.0   Nov 20 2006 16:19:42   dsawe
//added othercost image
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.26   Oct 06 2006 16:01:54   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.25.1.3   Oct 02 2006 16:23:26   mmodi
//Removed setting of CarPark costs as this is now done seperately in the CarParkCosts control
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.25.1.2   Sep 27 2006 12:03:00   mmodi
//Updated check for park and ride car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4203: Car Parking: Car park shown as Park and ride in costs view
//
//   Rev 1.25.1.1   Sep 13 2006 16:27:36   mmodi
//Changed order of displaying Car parks cost
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4180: Car Parking: Alignment of car park costs
//
//   Rev 1.25.1.0   Aug 18 2006 17:19:22   mmodi
//Code to display Car Park cost
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.25   May 02 2006 10:25:18   mtillett
//Ensure that car park is looked up each time, as the cached copy in the location object can get out of sync
//Resolution for 4009: DN058 Park and Ride Phase 2 - Carp park costs are for a different car park to the one used in the journey details
//
//   Rev 1.24   Apr 20 2006 16:19:22   kjosling
//Displays alternative text if parking is free or costs are not known
//Resolution for 44: DEL 8.1 Workstream - Park and Ride Amendments
//
//   Rev 1.23   Apr 12 2006 14:45:40   kjosling
//Pence is no longer displayed if it is zero
//
//   Rev 1.22   Apr 11 2006 10:27:38   kjosling
//Added correct prefixes and suffixes to page display data
//Resolution for 3841: DN058 Park & Ride phase 2 - Tickets/costs page issues
//
//   Rev 1.21   Apr 06 2006 11:59:20   mdambrine
//Change the tdlocation variable that is used throughout the displaycharges() mehod to the roudjourney's desination instead.
//Resolution for 3816: DN68 Extend: Server error viewing tickets/costs for combined PT and car journey
//
//   Rev 1.20   Mar 23 2006 17:58:44   build
//Automatically merged from branch for stream0025
//
//   Rev 1.19.1.3   Mar 21 2006 12:20:14   halkatib
//Applied changes resluting from code review
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.19.1.2   Mar 20 2006 19:42:04   halkatib
//Added extra processing to make format the display the cost information for park and ride.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.19.1.1   Mar 10 2006 16:42:22   tolomolaiye
//Further updates for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.19.1.0   Mar 08 2006 14:34:38   tolomolaiye
//Changes for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.19   Feb 23 2006 19:17:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.18.1.0   Jan 10 2006 15:26:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.18   Aug 03 2005 21:07:34   asinclair
//fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.17   May 20 2005 11:12:28   tmollart
//Following changes made:
// - Added event handler for table row creation so that formatting of each row can be controlled.
// - Other general changes to fix IR2300.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.16   May 12 2005 17:36:48   tmollart
//Updated repeater control to adopt same styles as similar controls. 
//Added a column for pence to aid in formatting.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.15   May 12 2005 15:14:58   rgreenwood
//IR 2448 & 2487: Added another hyperlink in FormatChargeData() and also now set any -1 Ferry charge to "unknown".
//Resolution for 2487: Car Costing: Zero or -1 cost ferries should display "Unknown"
//
//   Rev 1.14   May 12 2005 12:11:56   Ralavi
//Changes made so that company name is not shown as hyperlink in printer friendly mode for Ticket/Costs page. This is now shown as label.
//
//   Rev 1.13   Apr 26 2005 09:30:50   rgeraghty
//Check added for ChargeItem Url being > 0 - if so display as hyperlink, if not display as text
//Resolution for 2233: DEl 7 - Car costing - identifying ferry
//
//   Rev 1.12   Apr 25 2005 17:03:58   jgeorge
//Modifications to cope with extend journey
//Resolution for 2297: Del 7 - Car Costing - Zero cost for car journey which is part of multi-mode extended journey
//
//   Rev 1.11   Apr 18 2005 15:02:38   esevern
//corrected setting of company url for tolls
//
//   Rev 1.10   Apr 12 2005 12:28:30   esevern
//added check for company name not being null
//
//   Rev 1.9   Apr 07 2005 14:24:38   rgreenwood
//IR1997: Added ChargeItemExists property, and a check to ensure that the repeater is databound only if there are chargeitems.
//Resolution for 1997: Del 7 - Display of 'Other costs' in Door-to-door
//
//   Rev 1.8   Apr 04 2005 19:19:22   esevern
//changes made as a result of FXCop (inc. adding CurrentUICulture when formatting currency string)
//
//   Rev 1.7   Mar 24 2005 17:07:50   esevern
//added check for charge value being less than 0.  CalMac charge should not be displayed and will be entered as -1
//
//   Rev 1.6   Mar 16 2005 12:55:06   esevern
//amended to display total cost as pounds and pence
//
//   Rev 1.5   Mar 14 2005 12:40:12   esevern
//added outward and road journey properties
//
//   Rev 1.4   Mar 04 2005 15:45:58   esevern
//car costing integration
//
//   Rev 1.3   Feb 11 2005 15:49:48   rgreenwood
//Work in progress
//
//   Rev 1.2   Feb 11 2005 15:39:10   rgreenwood
//Work in progress
//
//   Rev 1.1   Feb 10 2005 13:41:40   rgreenwood
//work in progress
//
//   Rev 1.0   Jan 11 2005 10:23:24   rgreenwood
//Initial revision.

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
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.UserPortal.LocationService;

	/// <summary>
	///	Repeater control allowing the display of additional car cost 
	///	charges (eg. road tolls, ferry charges etc.)
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class OtherCostsControl : TDUserControl
	{
		
		private RoadJourney roadJourney;

		/// <summary>
		/// Indicates if this control is for outward or return journey
		/// </summary>
		private bool outward;

		/// <summary>
		/// Contains the text for the word "Unknown"
		/// </summary>
		private string unknownText;

		/// <summary>
		/// Contains the text for the word "Free Parking"
		/// </summary>
		private string freeParkingText;

		/// <summary>
		/// Contains the text for the word "Free"
		/// Used for Car Parks change Phase 1 and 2
		/// </summary>
		private string freeCarParkText;

		/// <summary>
		/// Contains the URL for CarParkInformation page
		/// Used for Car Parks change Phase 1 and 2
		/// </summary>
		private string carParkInformationUrl;

		/// <summary>
		/// Indicates whether ChargeItems exist to display
		/// </summary>
		private bool chargeItemExists = false;

		#region Event handlers

		/// <summary>
		/// Calls DisplayCharges() to show the charge items (tolls etc) for this journey 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			DisplayCharges();
		}

		/// <summary>
		/// Handles page load. Assigns local variables.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			//Set value so its available through full page cycle.
			unknownText = GetResource("OtherCostsControl.UnknownCost");
			freeParkingText = GetResource("ParkAndRide.CarPark.FreeParking");
			freeCarParkText = GetResource("CarParking.Free");
			carParkInformationUrl = GetResource("CarParking.URLCarParkInformation");
		}

		/// <summary>
		/// Handles event for when repeater control creates an item. Determines require 
		/// table cell layout.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OtherCostsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//Get references to the table cells we need to control.
				TableCell poundsCell = (TableCell)e.Item.FindControl("PoundsCell");
				TableCell penceCell = (TableCell)e.Item.FindControl("PenceCell");
				TableCell unknownCell = (TableCell)e.Item.FindControl("UnknownCell");
				TableCell imageCell = (TableCell)e.Item.FindControl("ImageCell");
				TableCell blankCell = (TableCell)e.Item.FindControl("BlankCell");

				//If item[1] contains the unknown string them only show unknownCell. This cell
				//has a colspan of 2 so it makes sure the word "Unknown" is totally right aligned.
				//Otherwise display the pounds and pence cells.
				string[] item = (string[])e.Item.DataItem;

				if (item[1].ToString() != unknownText && 
					item[1].ToString() != freeParkingText )
				{
					poundsCell.Visible = true;
					penceCell.Visible = true;
				}
				else
				{
					unknownCell.Visible = true;
				}

				// Item 4 indicates if it is a Park and Ride location
				if (item[4].ToString() == "true")
				{
					imageCell.Visible = true;
				}
				else
				{
					blankCell.Visible = true;
				}
			}
		}
		#endregion

		#region Private methods

		/// <summary>
		/// Iterates through the list of road journey details to create an 
		/// array list of charge items for this journey (tolls etc.), used 
		/// to populate the repeater.
		/// </summary>
		private void DisplayCharges () 
		{
			//get the itinerary details
			TDItineraryManager itineraryManager = TDItineraryManager.Current;			
			string[] detailRow;

			TDLocation destinationLocation = roadJourney.DestinationLocation;			
			
			//Store results of loop in an Arraylist (DataSource for repeater)
			ArrayList details = new ArrayList();

			if(CarRoadJourney != null) 
			{	
				// Used to track first occurance of adding a Congestion charge
				bool congestionChargeAdded = false;

				// Loop through the array of RoadJourneyDetails and get any associated RoadJourneyChargeItems
				for (int journeyDetailIndex=0; journeyDetailIndex < roadJourney.Details.Length; journeyDetailIndex++)
				{
				
					if (roadJourney.Details[journeyDetailIndex].ChargeItem != null)
					{
						RoadJourneyChargeItem chargeItem = roadJourney.Details[journeyDetailIndex].ChargeItem;

						// For CongestionZones, we want to add the first occurance of a Congestion charge, 
						// and then only subsequent Congestion charges > £0

						// Check for CongestionZone type and first congestionChargeAdded
						if (
							((chargeItem.SectionType == StopoverSectionType.CongestionZoneEntry) ||
							(chargeItem.SectionType == StopoverSectionType.CongestionZoneEnd)||
							(chargeItem.SectionType == StopoverSectionType.CongestionZoneExit)) &&
							!congestionChargeAdded
							)
						{
							detailRow = FormatChargeData(journeyDetailIndex);
							congestionChargeAdded = true;
						}
							// Check for CongestionZone type and only add if its greater than £0
						else if (
							((chargeItem.SectionType == StopoverSectionType.CongestionZoneEntry) ||
							(chargeItem.SectionType == StopoverSectionType.CongestionZoneEnd) ||
							(chargeItem.SectionType == StopoverSectionType.CongestionZoneExit)) &&
							congestionChargeAdded
							)
						{
							if (chargeItem.Charge > 0)
								detailRow = FormatChargeData(journeyDetailIndex);
							else
								detailRow = null;
						}
							// For all other types of charges, just add the detail
						else
						{
							detailRow = FormatChargeData(journeyDetailIndex);
						}


						if (detailRow != null)
						{
							details.Add(detailRow);
						}
					}
				}

				//check if location contains a park and ride scheme
				if (destinationLocation.ParkAndRideScheme != null)
				{
					detailRow = FormatParkAndRide(destinationLocation, CarRoadJourney.Details[CarRoadJourney.Details.Length - 1].Toid);
					if (detailRow != null)
					{
						details.Add(detailRow);
					}
				}

				//If we have added details bind the repeater control and set property
				//to determine if we have charge items to true.
				if(details.Count > 0)
				{
					// Bind the repeater to the data
					OtherCostsRepeater.DataSource = details;
					OtherCostsRepeater.DataBind();
					chargeItemExists = true;
				}
			}
		}

		/// <summary>
		/// Returns a string array that has HTML for the Description 
		/// Hyperlink and the charge value
		/// </summary>
		/// <param name="journeyDetailIndex"></param>
		/// <returns>string[]</returns>
		private string[] FormatChargeData(int journeyDetailIndex) 
		{
			string[] chargeItemDetails = new string[5];

			decimal chargeItem;
			//The cost that is display is returned here

			int cost = roadJourney.Details[journeyDetailIndex].ChargeItem.Charge;
			
			chargeItemDetails[3] = "&nbsp;";
			chargeItemDetails[4] = "&nbsp;";

			//add a check for CalMac: charges should not be displayed
			if(cost < 0) 
			{
				if (roadJourney.Details[journeyDetailIndex].ChargeItem.Url.Length !=0)
				{
					if (PageId != PageId.PrintableJourneyFares)
					{
						chargeItemDetails[0] = "<a href=\"" +"http://"   
							+ roadJourney.Details[journeyDetailIndex].ChargeItem.Url.Trim() 
							+ "\" target=\"_blank\">" 
							+ roadJourney.Details[journeyDetailIndex].ChargeItem.CompanyName 
							+ "</a>"
							+ "  "
							+ "<a href=\"" +"http://"
							+ roadJourney.Details[journeyDetailIndex].ChargeItem.Url.Trim()
							+ "\" target=\"_blank\">"
							+ GetResource("OtherCostsControl.ChargeNotification");// charge notification
					}
					else
					{
						chargeItemDetails[0] = roadJourney.Details[journeyDetailIndex].ChargeItem.CompanyName;			
					}
				}
				else
				{
					chargeItemDetails[0] = roadJourney.Details[journeyDetailIndex].ChargeItem.CompanyName;
				}
				chargeItemDetails[1] = unknownText;
			}
			else 
			{
				// sanity check for company name entered !
				string companyName = roadJourney.Details[journeyDetailIndex].ChargeItem.CompanyName;
				
				if(companyName != null && !companyName.Equals(string.Empty)) 
				{
					if (roadJourney.Details[journeyDetailIndex].ChargeItem.Url.Length !=0)
					{
						if (PageId != PageId.PrintableJourneyFares)
						{
							chargeItemDetails[0] = "<a href=\"" +"http://"  + roadJourney.Details[journeyDetailIndex].ChargeItem.Url.Trim() 
								+ "\" target=\"_blank\">" + companyName	+ "</a>";
						}
						else
						{
							chargeItemDetails[0] = companyName;
						}
					}
					else
					{
						chargeItemDetails[0] = companyName;
					}

					//if we are on the return Journey and CongestionCostAdded is true, then Charge is zero
					if ((!outward && TDItineraryManager.Current.JourneyViewState.CongestionCostAdded)&& roadJourney.Details[journeyDetailIndex].CongestionEntry)
					{
						chargeItem = 0;
						string temp = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", chargeItem);
						chargeItemDetails[1] = temp.Substring(0,temp.Length - 3);
						chargeItemDetails[2] = temp.Substring(temp.Length - 3,3);
					}
					else
					{
						// convert the charge item (int) to pounds and pence
						chargeItem = (decimal)roadJourney.Details[journeyDetailIndex].ChargeItem.Charge / 100;
						string temp = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", chargeItem);
						chargeItemDetails[1] = temp.Substring(0,temp.Length - 3);
						chargeItemDetails[2] = temp.Substring(temp.Length - 3,3);
					}
					if(chargeItemDetails[2] == ".00")
					{
						chargeItemDetails[2] = string.Empty;
					}
				}
				else
				{
					chargeItemDetails = null;
				}
			}
			return chargeItemDetails;
		}
		
		/// <summary>
		/// Obtains the ParkAndRideInfo that corresponds to the required scheme
		/// </summary>
		/// <param name="parkInfo">The ParkAndRideInfo object</param>
		/// <param name="toids">An array of toids</param>
		/// <returns>A string array</returns>
		private string[] FormatParkAndRide(TDLocation location, string[] toids)
		{
			//create an array of three strings to hold the return value
			string[] parkAndRideText = new string[5];
			
			//update the car park using the park and ride scheme every time
			location.CarPark = location.ParkAndRideScheme.MatchCarPark(toids);
			
			if (location.CarPark != null)
			{
				CarParkInfo carPark = location.CarPark;
				
				string carParkDisplayName = carPark.CarParkName + 
					Global.tdResourceManager.GetString(
					"ParkAndRide.CarkPark.Suffix", TDCultureInfo.CurrentUICulture);

				//car park property is not null - create a link
				if (carPark.UrlLink != null)
				{
					parkAndRideText[0] = "<a href=\"" + carPark.UrlLink + "\" target=\"_blank\">" + carParkDisplayName + "</a>";
				}
				else if (location.ParkAndRideScheme.UrlLink != null)
				{
					parkAndRideText[0] = "<a href=\"" + location.ParkAndRideScheme.UrlLink + "\" target=\"_blank\">" + carParkDisplayName + "</a>";
				}
				else
				{
					parkAndRideText[0] = "<b>" + carParkDisplayName + "</b>";
				}

				switch(carPark.MinimumCost)
				{
					case -1:
						parkAndRideText[1] = unknownText;
						parkAndRideText[2] = string.Empty;
						break;
					case 0:
						parkAndRideText[1] = freeParkingText;
						parkAndRideText[2] = string.Empty;
						break;
					default:
						// convert the charge item (int) to pounds and pence
						decimal chargeItem = (decimal)carPark.MinimumCost / 100;
						string temp = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", chargeItem);
						parkAndRideText[1] = temp.Substring(0,temp.Length - 3);
						parkAndRideText[2] = temp.Substring(temp.Length - 3,3);
						if(parkAndRideText[2] == ".00")
						{
							parkAndRideText[2] = string.Empty;
						}
						break;
				}

				if(carPark.Comments.Trim() == String.Empty)
				{
					parkAndRideText[3] = String.Empty;
				}
				else
				{
					parkAndRideText[3] = 					
						Global.tdResourceManager.GetString(
						"ParkAndRide.Comments.Prefix", TDCultureInfo.CurrentUICulture)
						+ carPark.Comments;				
				}
				if(location.ParkAndRideScheme !=null)
				{
					parkAndRideText[4] = "true";
				}
				else
				{
					parkAndRideText[4] = "false";
				}
				return parkAndRideText;
			}

			// No details to return
			return null;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Read only Property for othercosts image URL
		/// </summary>
		public string imageOtherCostsURL
		{
			get
			{
				return GetResource("OtherCostsControl.imageOtherCosts");
			}
		}
		
		/// <summary>
		/// Read only Property for othercosts image alternate text
		/// </summary>
		public string imageOtherCostsAltText
		{
			get
			{
				return " ";
			}
		}

		/// <summary>
		/// Read only Property for CarParkCosts image URL
		/// </summary>
		public string imageCarParkCostsURL
		{
			get
			{
				return GetResource("CarParking.imageCarParkCosts");
			}
		}

		/// <summary>
		/// Read only Property for CarParkCosts image alternate text
		/// </summary>
		public string imageCarParkCostsAltText
		{
			get
			{
				return GetResource("CarParking.imageCarParkCosts.AlternateText");
			}
		}

		/// <summary>
		/// Read only property supplying the header text 
		/// for the control
		/// </summary>
		public string HeaderText
		{
			get
			{
				return GetResource("OtherCostsControl.labelHeader");
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
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.OtherCostsRepeater.ItemCreated +=new RepeaterItemEventHandler(OtherCostsRepeater_ItemCreated);
		}
		#endregion
	}
}