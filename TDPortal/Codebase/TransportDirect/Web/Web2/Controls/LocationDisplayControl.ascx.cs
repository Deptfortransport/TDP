// ***********************************************
// NAME                 :  LocationDisplayControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/07/2003
// DESCRIPTION  : Control displaying a location.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocationDisplayControl.ascx.cs-arc  $
//
//   Rev 1.3   May 01 2008 17:31:12   mmodi
//No change.
//Resolution for 4923: Control alignments: Find a train
//
//   Rev 1.2   Mar 31 2008 13:21:48   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 14 2008 13:00:00   mmodi
//Updated check for Park and ride car park
//
//   Rev DevFactory   Feb 06 2008 17:00:00   mmodi
//Updated for Car parks to display the Location description and the Car park description
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:16:04   mturner
//Initial revision.
//
//   Rev 1.22   Apr 12 2006 17:57:38   rgreenwood
//IR3791: Enhanced Populate() method to check search type and append additional text to "To" location in case of park & ride
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//Resolution for 3791: DN058 Park and Ride Phase 2 - Plan to Park and ride To location does not have appended 'park & ride' text
//
//   Rev 1.21   Feb 23 2006 19:16:54   build
//Automatically merged from branch for stream3129
//
//   Rev 1.20.1.1   Jan 30 2006 14:41:14   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.20.1.0   Jan 10 2006 15:26:02   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.20   Nov 10 2005 12:41:36   halkatib
//Correction to New Location handling
//
//   Rev 1.19   Nov 04 2005 13:35:38   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.18   Nov 02 2005 17:28:58   halkatib
//Merged code as part of stream2877
//Resolution for 2937: Stream 2877 (Landing Page - Phase 2) Merge
//
//   Rev 1.17   Nov 01 2005 15:12:02   build
//Automatically merged from branch for stream2638
//
//   Rev 1.16.3.0   Oct 25 2005 14:39:30   rgreenwood
//TD089 ES020 Image Button Replacement
//
//   Rev 1.16.2.0   Oct 25 2005 09:59:28   tolomolaiye
//New Location button should not be visible for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.16   Mar 01 2005 18:21:34   tmollart
//Changed so that New Location button is shown when Page ID is FindFareInput.
//
//   Rev 1.15   Nov 02 2004 11:40:54   passuied
//fixed various display bugs
//
//   Rev 1.14   Sep 30 2004 13:13:08   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.13   Sep 22 2004 10:35:56   passuied
//Hide SearchType when in extended mode (LocationFixed)
//Resolution for 1615: Location type read only display should be removed when extending a journey
//
//   Rev 1.12   Sep 21 2004 13:40:44   jgeorge
//Hide Station Type line when there is nothing to display (as happens when used in Find a... pages). 
//Resolution for 1461: Find a train too much space on From and To
//
//   Rev 1.11   May 19 2004 15:00:34   acaunt
//commandNewLocation visibility is now determined by the LocationSearch's LocationFixed property:
//Visibility = !LocationFixed.
//
//   Rev 1.10   Apr 08 2004 14:31:40   COwczarek
//Pass through correct data services data set to use when
//generating text to show search type used e.g.
//"Address/Postcode"
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.9   Mar 12 2004 16:20:24   COwczarek
//Display "Selected from map" for locations selected from a map
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.8   Dec 18 2003 11:07:56   passuied
//added New Location functionality for valid locations
//Resolution for 557: JP Ambiguity: Formatting
//
//   Rev 1.7   Nov 18 2003 11:49:00   passuied
//missing comments
//
//   Rev 1.6   Nov 18 2003 10:44:08   passuied
//changes to hopefully pass code review
//
//   Rev 1.5   Oct 28 2003 10:39:04   passuied
//changes after fxcop
//
//   Rev 1.4   Sep 29 2003 16:14:20   passuied
//updated
//
//   Rev 1.3   Sep 26 2003 10:47:40   passuied
//latest working version
//
//   Rev 1.2   Sep 25 2003 18:36:24   passuied
//Added missing headers
//
//   Rev 1.1   Sep 20 2003 14:41:52   passuied
//updated for ambiguity screen
//
//   Rev 1.0   Jul 29 2003 16:00:04   passuied
//Initial Revision


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///		Summary description for LocationDisplayControl.
	/// </summary>
	public partial  class LocationDisplayControl : TDUserControl
	{

        private DataServiceType listType;
		private DataServices populator;
		public event EventHandler NewLocation;

		/// <summary>
		/// Constructor
		/// </summary>
		protected LocationDisplayControl()
		{
			populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}
        protected void Page_Load(object sender, System.EventArgs e)
        {
            commandNewLocation.Text = Global.tdResourceManager.GetString(
                    "AmbiguousLocationSelectControl2.commandNewLocation.Text",
                    TDCultureInfo.CurrentUICulture);
        }

		/// <summary>
		/// Populate the control with a location object
		/// </summary>
		/// <param name="listType">The data service set to use in displaying the search type</param>
		/// <param name="search">The search being performed</param>
		/// <param name="location">Location to display</param>
		/// <param name="differentReturnLocation">The description of a different return location</param>
		public void Populate(DataServiceType listType, LocationSearch search, TDLocation location, string differentReturnLocation)
		{
            this.listType = listType;

			if (location.SearchType == SearchType.ParkAndRide)
			{
				labelDescription.Text = HttpUtility.HtmlDecode( location.Description + 
					Global.tdResourceManager.GetString(
						"ParkAndRideInput.AppendToLocation.Text",
						TDCultureInfo.CurrentUICulture));
			}
			else
			{
				labelDescription.Text = HttpUtility.HtmlDecode( location.Description);
			}

			// Check if returning from a different location
			// If so, display this to the user
			if (differentReturnLocation != string.Empty)
			{
				labelReturnDescription.Visible = true;
				labelReturnDescription.Text = 
					string.Format(GetResource("LocationDisplayControl.labelReturnDescription.Text"), differentReturnLocation);
			}

			// Hide search type if location is fixed. For extended Journey
			if (!search.LocationFixed)
			{
				if (location.SearchType == SearchType.Map) 
				{
					labelLocationType.Text = Global.tdResourceManager.GetString(
						"LocationDisplayControl.LabelLocationType.Text",
						TDCultureInfo.CurrentUICulture);
				} 
				else 
				{
					labelLocationType.Text = populator.GetText(listType, Enum.GetName(typeof(SearchType),location.SearchType));
				}
			}
			else
				labelLocationType.Visible = false;

            // Specific handling if it is a car park location
            if ((location.SearchType == SearchType.CarPark)
                ||
                (location.CarParking != null))
            {
                // Display the Near "Searched for" location
                labelDescription.Text = GetResource("FindLocationControl.directionLabelTravelFrom") + "&nbsp;&nbsp;&nbsp;" + HttpUtility.HtmlDecode(location.Description);

                #region Get the Car Park name
                string carLocation = string.Empty;;
                string carParkName = string.Empty; ;

                // in case there is no location, then this prevents car name being displayed incorrectly
			    if (location.CarParking.Location != string.Empty)
                    carLocation = HttpUtility.HtmlDecode(location.CarParking.Location) + ", ";
			    else
				    carLocation = string.Empty;

                carParkName = carLocation + HttpUtility.HtmlDecode(location.CarParking.Name);

                // add on the car park end 
                if (location.CarParking.ParkAndRideIndicator.Trim().ToLower() == "true")
                    carParkName += GetResource("ParkAndRide.Suffix") + GetResource("ParkAndRide.CarkPark.Suffix");
                else
                    carParkName += GetResource("ParkAndRide.CarkPark.Suffix");
                #endregion

                // Display the Car park name
                labelLocationDescription.Text = carParkName;
                locationDescriptionPanel.Visible = true;
            }
            else
            {
                locationDescriptionPanel.Visible = false;
            }

			//commandNewLocation should not be visible when an Extend is in progress and the location is fixed,
			//it should be visible at all other times.
			commandNewLocation.Visible = !(search.LocationFixed && TDItineraryManager.Current.ExtendInProgress);
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
			this.commandNewLocation.Click += new EventHandler(this.CommandNewLocationClick);

		}
		#endregion

		private void CommandNewLocationClick(object sender, EventArgs e)
		{
			if (NewLocation != null)
				NewLocation(sender, e);
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			// If there is no location type, hide the panel it is displayed in to
			// remove the blank line that would otherwise be rendered.
			if (labelLocationType.Text.Length == 0)
			{
				panelLocationType.Visible = false;
			}
			else
			{
				panelLocationType.Visible = true;
			}
		}
	}
}
