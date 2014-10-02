// *********************************************** 
// NAME                 : VehicleFeaturesControl.ascx.cs 
// AUTHOR               : Rob Greenwood 
// DATE CREATED         : 23/06/2004
// DESCRIPTION			: Control to display the series of on-board
//						  facilities for a given vehicle type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/VehicleFeaturesControl.ascx.cs-arc  $
//
//   Rev 1.5   Nov 18 2008 15:32:30   jfrank
//Update after code review
//Resolution for 5146: WAI AAA compliance work (CCN 474)
//
//   Rev 1.4   Oct 21 2008 14:25:32   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.3   Oct 13 2008 16:44:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Oct 07 2008 11:44:36   jfrank
//Update for XHTML compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:23:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:40   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:17:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:28:06   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jul 25 2005 18:06:56   RPhilpott
//Use OnPreRender override, not new event handler.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 18 2005 15:13:14   RPhilpott
//Changed RM constant for JourneyResults RM.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 10 2005 15:17:44   rgreenwood
//Moved control's Init into Pre-Render
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 08 2005 13:29:30   rgreenwood
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.AirDataProvider;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;

	/// <summary>
	///		Basic HTML Repeater which displays various icons to illustrate the variety of facilities available onboard a vehicle. Part of DN062 for Del8.
	///		
	/// </summary>
	public partial class VehicleFeaturesControl : TDUserControl
	{
		#region Variables

		private int[] features;
		private ArrayList vehicleFeatureIcons = new ArrayList();

		protected RailVehicleFeaturesIconMapper railVehicleFeaturesIconMapper;

		#endregion

		public VehicleFeaturesControl()
		{
			LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Event handler for the prerender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{

			// Put user code to initialize the page here
			// Instantiate RailVehicleFeaturesIconMapper
			railVehicleFeaturesIconMapper = new RailVehicleFeaturesIconMapper();

			vehicleFeatureIcons = railVehicleFeaturesIconMapper.GetIcons(features);

			//Check that there are icons for this vehicle
			if ( (vehicleFeatureIcons == null || vehicleFeatureIcons.Count == 0) )
			{
				//No icons, hide the entire repeater control and cease processing
				vehicleFeaturesRepeater.Visible = false;
			}
			else
			{
				// Set Repeater DataSource and DataBind to the array of icons
				vehicleFeaturesRepeater.DataSource = vehicleFeatureIcons;
				vehicleFeaturesRepeater.DataBind();
				//Show repeater control
				vehicleFeaturesRepeater.Visible = true;
			}


			//Check for null and empty array from GetIcons call
			if (Features == null || Features.Length <= 0)
			{
				this.Visible = false;
			}
			else
			{
				this.Visible = true;
			}
          
		}


		public string GetImageURL(VehicleFeatureIcon vehicleFeatures)
		{
			//Get ImageURL from vehicleFeatureIcons for a given item
			return GetResource(vehicleFeatures.ImageUrlResource);
			
		}

		public string GetAltText(VehicleFeatureIcon vehicleFeatures)
		{
			//Get AltText from vehicleFeatureIcons for a given item 
			return GetResource(vehicleFeatures.AltTextResource);
		}

		public string GetToolTip(VehicleFeatureIcon vehicleFeatures)
		{
			//Get ToolTip from vehicleFeatureIcons for a given item
			return GetResource(vehicleFeatures.ToolTipResource);
		}


		
		#region Properties

		public int[] Features
		{
			get { return features; }
			set { features = value; }
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
	}
}
