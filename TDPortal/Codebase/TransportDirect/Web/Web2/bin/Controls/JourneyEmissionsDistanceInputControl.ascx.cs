// *********************************************** 
// NAME                 : JourneyEmissionsDistanceInputControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/02/2007
// DESCRIPTION			: Control displaying the Journey Distance input used to calculate PT Emissions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionsDistanceInputControl.ascx.cs-arc  $ 
//
//   Rev 1.4   May 20 2009 13:40:22   mmodi
//Updated code to limit length of distance to prevent overflow error.
//
//   Rev 1.3   Dec 19 2008 14:33:02   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:28   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 07 2008 13:15:32   mmodi
//Corrected error when invalid distance entered following a landing page non-autoplan request
//
//   Rev 1.0   Nov 08 2007 13:15:32   mturner
//Initial revision.
//
//   Rev 1.3   Apr 12 2007 11:19:28   mmodi
//Updated to populate input box with value previously entered to correct loss of value when returning from Help page
//Resolution for 4383: CO2: Rounding of Distance on CO2 emissions compare panel
//
//   Rev 1.2   Feb 28 2007 15:48:42   mmodi
//Updated distance error message
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.1   Feb 27 2007 10:39:04   mmodi
//Updates from code review checklist
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 17:06:40   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		Summary description for JourneyEmissionsDistanceInputControl.
	/// </summary>
	public partial class JourneyEmissionsDistanceInputControl : TDUserControl
	{

		#region Controls



		#endregion

		#region Private Variables

		private IDataServices populator;		
		private bool journeyDistanceValid = true;

		#endregion

		#region Constants

		private const string ImageCarURL = "JourneyEmissionsDistanceInputControl.ImageCar.URL";
		private const string ImageCarAltText = "JourneyEmissionsDistanceInputControl.ImageCar.AlternateText";
		private const string ImageTrainURL = "JourneyEmissionsDistanceInputControl.ImageTrain.URL";
		private const string ImageTrainAltText = "JourneyEmissionsDistanceInputControl.ImageTrain.AlternateText";
		private const string ImagePlaneURL = "JourneyEmissionsDistanceInputControl.ImagePlane.URL";
		private const string ImagePlaneAltText = "JourneyEmissionsDistanceInputControl.ImagePlane.AlternateText";
		private const string ImageBusCoachURL = "JourneyEmissionsDistanceInputControl.ImageBusCoach.URL";
		private const string ImageBusCoachAltText = "JourneyEmissionsDistanceInputControl.ImageBusCoach.AlternateText";

		private const string TxtSeven = "txtseven";
		private const string TxtSevenB = "txtsevenb";
		private const string TxtSevenR = "txtsevenr";

		#endregion

		#region Page_Init, Page_Load, Page_PreRender

		/// <summary>
		/// Handler for the Init event. Sets up global variables and additional event handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			resourceManager = Global.tdResourceManager;
		}

		/// <summary>
		/// Populates drop down lists and sets label text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if ((!Page.IsPostBack))
			{
				// Load dropdown info
				populator.LoadListControl(DataServiceType.UnitsDrop, listJourneyDistanceUnit);

				JourneyDistanceText = TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceToDisplay;
				JourneyDistanceUnit = TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceUnit;
			}

			// Load strings from the languages file
			labelTitle.Text = GetResource("JourneyEmissionsDistanceInputControl.Title");
			labelJourneyDistance.Text = GetResource("JourneyEmissionsDistanceInputControl.JourneyDistance");

			textJourneyDistance.CssClass ="";

            // Limit the number of characters that can be entered. 8 allows a reasonable sized number 
            // with decimals if required. Shouldn't be more than 11 or 12 to prevent convert to int/dec overflow error
            textJourneyDistance.MaxLength = 8;
		
			PopulateImages();			
		}

		/// <summary>
		/// Sets the visibility/style of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{			
			// Determine is the error message needs to be displayed
			if ((!JourneyDistanceValid) ||
				(!TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceValid))
			{
				textJourneyDistance.CssClass = "alertboxerror";
				labelDisplayJourneyDistanceError.Visible = true;

				string minDistance = Properties.Current[ "JourneyEmissions.MinDistance" ];
				string maxDistance = Properties.Current["JourneyEmissions.MaxDistance"];
				
				// Because Min/Max distance properties are in Kms, convert them to miles if user
				// has selected the miles unit
				if (listJourneyDistanceUnit.SelectedIndex == 0)
				{
					minDistance = Convert.ToDouble(
						MeasurementConversion.Convert(
						Convert.ToDouble(minDistance), ConversionType.KilometresToMiles))
						.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat);;
					maxDistance = Convert.ToDouble(
						MeasurementConversion.Convert(
						Convert.ToDouble(maxDistance), ConversionType.KilometresToMiles))
						.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat);;
				}

				labelDisplayJourneyDistanceError.Text = string.Format(
					GetResource("JourneyEmissionsDistanceInputControl.JourneyDistanceErrorKey"),
					minDistance, 
					maxDistance,
					listJourneyDistanceUnit.SelectedItem.ToString());

                // Determine if we need to display the landing page Plane distance error message
                try
                {
                    Double journeyDistance = Convert.ToDouble(TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistance);

                    if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModePlane
                            && ((journeyDistance) < Convert.ToDouble(maxDistance)))
                    {
                        labelDisplayJourneyDistanceError.Text = GetResource("JourneyEmissionsDistanceInputControl.JourneyDistanceAirErrorKey");
                    }
                }
                catch
                {
                    // Unble to convert the distance to a value, therefore invalid distance entered. 
                    // Therefore do not need to do anything as we've already set the error message to display above
                }
			}
			else
			{
				textJourneyDistance.CssClass = "";
				labelDisplayJourneyDistanceError.Visible = false;
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Populates the images and alt text
		/// </summary>
		private void PopulateImages()
		{
			// Assign images to image controls
			imageCar.ImageUrl = GetResource(ImageCarURL);
			imageTrain.ImageUrl = GetResource(ImageTrainURL);
			imagePlane.ImageUrl = GetResource(ImagePlaneURL);
			imageBusCoach.ImageUrl = GetResource(ImageBusCoachURL);

			// Assign alternate text
			imageCar.AlternateText = GetResource(ImageCarAltText);
			imageTrain.AlternateText = GetResource(ImageTrainAltText);
			imagePlane.AlternateText = GetResource(ImagePlaneAltText);
			imageBusCoach.AlternateText = GetResource(ImageBusCoachAltText);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets/Sets Journey distance entered text
		/// </summary>
		public string JourneyDistanceText
		{
			get{ return HttpUtility.HtmlDecode( textJourneyDistance.Text ); }
			set{ textJourneyDistance.Text = HttpUtility.HtmlEncode(value); }
		}

		/// <summary>
		/// Get/Set property that returns true if the Journey Distance is between
		/// the valid distance range
		/// </summary>
		public bool JourneyDistanceValid
		{
			get{ return journeyDistanceValid; }
			set{ journeyDistanceValid = value; }
		}

		/// <summary>
		/// Gets/Sets the Journey Distance unit in dropdown list
		/// </summary>
		public int JourneyDistanceUnit
		{
			get
			{
				string itemValue = populator.GetValue(DataServiceType.UnitsDrop, listJourneyDistanceUnit.SelectedItem.Value);
				return Convert.ToInt32(itemValue);
			}
			set
			{
				string journeyDistanceUnitId = populator.GetResourceId(DataServiceType.UnitsDrop, value.ToString());
			
				populator.Select(listJourneyDistanceUnit, journeyDistanceUnitId);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
