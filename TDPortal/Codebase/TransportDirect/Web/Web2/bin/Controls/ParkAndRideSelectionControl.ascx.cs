// ****************************************************************************** 
// NAME                 : ParkAndRideSelectionControl.ascx.cs
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 3 MArch 2006
// DESCRIPTION			: Displays a drop down list of Park and Ride schemes
// ****************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ParkAndRideSelectionControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 28 2010 10:14:54   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:22:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:52   mturner
//Initial revision.
//
//   Rev 1.9   Apr 21 2006 15:07:00   jbroome
//Allowed refreshing of list for null location
//Resolution for 3960: DN058 Park and Ride Phase 2 - 'Clear' on Find a car not resetting Park and Ride scheme correctly
//
//   Rev 1.8   Apr 20 2006 10:31:52   esevern
//added property to obtain park and ride scheme drop down list
//Resolution for 3803: DN058 Park and Ride Phase 2 - Amend from journey results page does not retain values
//
//   Rev 1.7   Apr 18 2006 16:07:58   esevern
//Added RefreshParkAndRideSelectionControl method - uses the stored TDLocation value for the previously selected park and ride destination to set the initially selected 'to' location when the user chooses to amend a journey result
//Resolution for 3803: DN058 Park and Ride Phase 2 - Amend from journey results page does not retain values
//
//   Rev 1.6   Mar 22 2006 14:46:24   tolomolaiye
//Added select option to drop down list
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.5   Mar 20 2006 19:02:46   tolomolaiye
//Updated with code review comments
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4   Mar 16 2006 12:42:54   halkatib
//changes for park and ride phase 2 to get selected location.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.3   Mar 15 2006 11:59:04   halkatib
//Apllied changes required by td locatio contructor redesign
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Mar 10 2006 16:42:22   tolomolaiye
//Further updates for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.1   Mar 08 2006 14:27:24   tolomolaiye
//Work in progress
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.0   Mar 03 2006 13:17:36   tolomolaiye
//Initial revision.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Globalization;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	Display drop down list of park and ride schemes.
	/// </summary>
	public partial class ParkAndRideSelectionControl : TDUserControl
	{

		private ParkAndRideInfo[] parkAndRideData;
		private IParkAndRideCatalogue parkAndRideLocations = (IParkAndRideCatalogue) TDServiceDiscovery.Current[ServiceDiscoveryKey.ParkAndRideCatalogue];
		private const string listInstructionAmbiguousKey = "FindPlaceDropDownControl.listInstructionAmbiguous";

		/// <summary>
		/// Read-only property that returns a TDLocation object corresponding 
		/// to the Park and Ride scheme selected by the user. 
		/// </summary>
		public TDLocation Location
		{
			get
			{
				TDLocation location = null;

				//check if the user has selected an iten from the drop down
				//The first item in the list is "-- Please select --". This should be ignored
				if (listParkAndRideSchemes.SelectedIndex > 0)
				{
					//get the ParkAndRide associated with the current object
					ParkAndRideInfo parkInfo = parkAndRideLocations.GetScheme(Convert.ToInt32(listParkAndRideSchemes.SelectedItem.Value, CultureInfo.CurrentCulture.NumberFormat));

					if (parkInfo != null)
					{
						//initialise and set up the location object
						// -1 passed to constructor since the carparkid is not known.
						location = new TDLocation(parkInfo.ParkAndRideId, -1);
						location.Status = TDLocationStatus.Valid;
					}
				}
				else
				{
					//just set location to a new TDLocation object
					location = new TDLocation();
				}
				return location;
			}
		}

		/// <summary>
		/// Read-only property returning the DropDownList object  
		/// displaying available Park and Ride schemes. 
		/// </summary>
		public DropDownList ParkAndRideList 
		{
			get 
			{
				return listParkAndRideSchemes;
			}
		}

		/// <summary>
		/// Page load method for the control
		/// Initialise park and ride data
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelInstructionDropDown.Text = GetResource(listInstructionAmbiguousKey);

			// white labelling change - clear the items to prevent appedning to list on postback
            listParkAndRideSchemes.Items.Clear();
			listParkAndRideSchemes.Items.Add(new ListItem(GetResource("ParkAndRideSelectionControl.listInstructionFrom"), String.Empty));

			//load the park and ride data into the list box
			parkAndRideData = parkAndRideLocations.GetAll();
			foreach (ParkAndRideInfo parkInfo in parkAndRideData)
			{
				listParkAndRideSchemes.Items.Add(new ListItem(parkInfo.Location, parkInfo.ParkAndRideId.ToString(CultureInfo.CurrentCulture)));
			}
		}

		/// <summary>
		/// Refreshes the ParkAndRideSelectionControl to show the park and ride location
		/// previously selected by the user for journey planning - called from ParkAndRideInput page
		/// if the FindPageState.AmendMode is true (i.e. the user has planned a journey and selected 
		/// 'Amend' from the results page).
		/// </summary>
		/// <param name="location">TDLocation - the selected park and ride destination</param>
		public void RefreshParkAndRideSelectionControl(TDLocation location) 
		{
			if (location.ParkAndRideScheme == null)
			{
				listParkAndRideSchemes.SelectedIndex = -1;
			}
			else
			{
				// set selected item using the ParkAndRideId from the TDLocation
				for (int i= 0; i< listParkAndRideSchemes.Items.Count; i++)
				{
					ListItem item = listParkAndRideSchemes.Items[i];
					if (item.Value == (location.ParkAndRideScheme.ParkAndRideId).ToString() )
					{
						listParkAndRideSchemes.SelectedIndex = i;
						break;
					}
				}
			}
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
		}

		/// <summary>
		/// Event Handler for Page_PreRender event.
		/// </summary>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			// set the style of control when in ambiguity mode or not
			if (TDSessionManager.Current.FindPageState.AmbiguityMode)
			{
				divDropDown.Attributes.Clear();
				divDropDown.Attributes.Add("class", "FindPlace_Dropdown_area");
				labelInstructionDropDown.Visible = true;
			}
			else
			{
				divDropDown.Attributes.Clear();
				divDropDown.Attributes.Add("class", "");
				labelInstructionDropDown.Visible = false;
			}	
		}

		#endregion
	}
}