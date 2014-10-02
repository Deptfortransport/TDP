// *********************************************** 
// NAME                 : CarJourneyTypeControl.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 27/6/2007
// DESCRIPTION			: A custom control to display
//						  the car journey type information.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarJourneyTypeControl.ascx.cs-arc  $
//
//   Rev 1.4   Sep 01 2011 10:44:44   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Oct 13 2008 16:41:36   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.1   Oct 01 2008 15:48:38   jfrank
//Changed to be XHTML compliant
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Sep 26 2008 13:36:12   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:19:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:38   mturner
//Initial revision.
//
//   Rev 1.4   Sep 06 2007 11:22:06   mmodi
//Screen reader improvements
//Resolution for 4493: Car journey details: Screen reader improvements
//
//   Rev 1.3   Aug 29 2007 16:45:26   mmodi
//Updates mpg and lkm, Via location and Fuel cost
//Resolution for 4486: Car journey details: Fuel cost not shown
//Resolution for 4487: Car journey details: Via location is not shown
//Resolution for 4488: Car journey details: MPG value not changed when units dropdown changed
//
//   Rev 1.2   Jun 28 2007 17:12:46   mmodi
//Updated alt text of images
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.1   Jun 28 2007 14:35:18   mmodi
//Code updates
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.0   Jun 27 2007 09:45:36   mmodi
//Initial revision.
//Resolution for 4458: DEL 9.7 - Car journey details
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;	

	/// <summary>
	///		Summary description for CarJourneyTypeControl.
	/// </summary>
	public partial  class CarJourneyTypeControl : TDUserControl
	{
		#region Controls
		
		
		#endregion

		#region Constants
		// Image paths
		private const string ImageSmallCarURL = "CarJourneyTypeControl.ImageSmallCar.URL";
		private const string ImageMediumCarURL = "CarJourneyTypeControl.ImageMediumCar.URL";
		private const string ImageLargeCarURL = "CarJourneyTypeControl.ImageLargeCar.URL";
		private const string ImageSmallCarAltText = "CarJourneyTypeControl.ImageSmallCar.AlternateText";
		private const string ImageMediumCarAltText = "CarJourneyTypeControl.ImageMediumCar.AlternateText";
		private const string ImageLargeCarAltText = "CarJourneyTypeControl.ImageLargeCar.AlternateText";

		// sizes
		private const string SMALL = "small";
		private const string MEDIUM = "medium";
		private const string LARGE = "large";

		#endregion

		#region Private members

		private IDataServices populator;
		private bool nonprintable;
		private TDJourneyParametersMulti journeyParameters = null;

        private bool isOutward = true;

		#endregion

		#region Initialise Method

		/// <summary>
		/// Initialise this control with the Journey Parameters
		/// </summary>
		public void Initialise(TDJourneyParametersMulti journeyParameters, bool isOutward)
		{
			this.journeyParameters = journeyParameters;
            this.isOutward = isOutward;
		}

		#endregion

		#region Page_Load, PreRender
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//load page resources
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		/// <summary>
		/// Constructs all dynamic labels and calls base OnPreRender
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			SetDynamicLabels();
			SetDynamicImages();

			base.OnPreRender(e);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets the text for labels 
		/// </summary>
		private void SetDynamicLabels()
		{
			if (journeyParameters != null)
			{
				#region Journey type label

				// Get the text value for the journey type, e.g. "Quickest", "Most fuel economic"
				string typeResourceId = populator.GetResourceId(DataServiceType.DrivingFindDrop,
					Enum.GetName(typeof(PrivateAlgorithmType), journeyParameters.PrivateAlgorithmType));
				
				string typeValue = populator.GetValue(DataServiceType.DrivingFindDrop, typeResourceId);

				string journeyTypeText = populator.GetText(DataServiceType.DrivingFindDrop, typeValue);

				string carSize = populator.GetText(DataServiceType.ListCarSizeDrop, journeyParameters.CarSize);
				string fuelType = populator.GetText(DataServiceType.ListFuelTypeDrop, journeyParameters.CarFuelType);

				labelCarJourneyType.Text = string.Format(GetResource("CarJourneyTypeControl.JourneyType.Text"), 
					journeyTypeText,
					carSize.ToLower(),
					fuelType.ToLower());

                bool avoidClosedRoads = false;

                // display message about avoiding closed roads if the journeyparameters has been populated 
                // with list of toids to avoid
                if (isOutward)
                {
                    if (journeyParameters.AvoidToidsListOutward != null
                        && journeyParameters.AvoidToidsListOutward.Length > 0)
                    {
                        avoidClosedRoads = true;
                    }

                }
                else
                {
                    if (journeyParameters.AvoidToidsListReturn != null
                        && journeyParameters.AvoidToidsListReturn.Length > 0)
                    {
                        avoidClosedRoads = true;
                    }
                }

                if (avoidClosedRoads)
                {
                    labelCarJourneyType.Text = string.Format(GetResource("CarJourneyTypeControl.JourneyType.AvoidClosedRoads.Text"),
                    journeyTypeText,
                    carSize.ToLower(),
                    fuelType.ToLower());
                }

				#endregion

				#region Fuel consumption label

				StringBuilder sb2 = new StringBuilder();
				sb2.Append( GetResource("CarJourneyTypeControl.FuelConsumption.Text") );
				sb2.Append( " " );
				
				string strMPG = string.Empty;
				string strLKM = string.Empty;

				#region Calculate MPG and LKM values
				// user has not entered a specific consumption value, so we will need to convert the default value
				if (journeyParameters.FuelConsumptionOption)
				{
					double consumption = Convert.ToDouble( journeyParameters.FuelConsumptionEntered );
					strMPG = MeasurementConversion.Convert(consumption, ConversionType.MetersPerLitreToMilesPerGallon );
					strLKM = MeasurementConversion.Convert(consumption, ConversionType.MetersPerLitreToLitresPer100Kilometers );
				}
				else  // Output what user entered/selected
				{	
					if (journeyParameters.FuelConsumptionUnit == 1)// MPG
					{
						strMPG = journeyParameters.FuelConsumptionEntered;

						double consumption = Convert.ToDouble(journeyParameters.FuelConsumptionEntered);
						string metresPerLitre = MeasurementConversion.Convert( consumption, ConversionType.MilesPerGallonToMetersPerLitre);
						decimal decLKM = Convert.ToDecimal(
											MeasurementConversion.Convert(
												Convert.ToDouble(metresPerLitre), 
												ConversionType.MetersPerLitreToLitresPer100Kilometers )
										);
						decLKM = Decimal.Round(decLKM, 1);

						strLKM = decLKM.ToString().Replace(".0", string.Empty); // remove trailing zero
					}
					else // LKM
					{
						strLKM = journeyParameters.FuelConsumptionEntered;

						double consumption = Convert.ToDouble(journeyParameters.FuelConsumptionEntered);
						string metresPerLitre = MeasurementConversion.Convert( consumption, ConversionType.LitresPer100KilometersToMetersPerLitre);
						decimal decMPG = Convert.ToDecimal(
											MeasurementConversion.Convert(
												Convert.ToDouble(metresPerLitre), 
												ConversionType.MetersPerLitreToMilesPerGallon)
										);
						decMPG = Decimal.Round(decMPG, 1);

						strMPG = decMPG.ToString().Replace(".0", string.Empty); // remove trailing zero
					}
				}

				#endregion

				string unitMPG = " " + populator.GetText(DataServiceType.FuelConsumptionUnitDrop, "1");
				string unitLKM = " " + populator.GetText(DataServiceType.FuelConsumptionUnitDrop, "2");

				strMPG += unitMPG;
				strLKM += unitLKM;

				sb2.Append( "<span class=\"milesshow\">" + strMPG + "</span>" );
				sb2.Append( "<span class=\"kmshide\">" + strLKM + "</span>" );
				sb2.Append( ".");

				labelFuelConsumption.Text = sb2.ToString();

				#endregion

				#region FuelCost

					decimal fuelCost = Convert.ToDecimal(journeyParameters.FuelCostEntered);
					if (journeyParameters.FuelCostOption)
						fuelCost = Decimal.Round(fuelCost/10, 1);
					labelFuelCost.Text = string.Format(GetResource("CarJourneyTypeControl.JourneyFuelCost.Text"), 
						fuelCost);

				#endregion
			}
		}

		/// <summary>
		/// Sets the images to be displayed
		/// </summary>
		private void SetDynamicImages()
		{
			string carImageUrl = GetResource(ImageMediumCarURL);
			string alternateText = GetResource(ImageMediumCarAltText);

			if (journeyParameters != null)
			{
				switch (journeyParameters.CarSize.ToLower())				
				{
					case SMALL:
					{
						carImageUrl = GetResource(ImageSmallCarURL);
						alternateText = GetResource(ImageSmallCarAltText);
					}
						break;
					case MEDIUM:
					{
						carImageUrl = GetResource(ImageMediumCarURL);
						alternateText = GetResource(ImageMediumCarAltText);
					}
						break;
					case LARGE:
					{
						carImageUrl = GetResource(ImageLargeCarURL);
						alternateText = GetResource(ImageLargeCarAltText);
					}
						break;
					default:
					{
						carImageUrl = GetResource(ImageMediumCarURL);
						alternateText = GetResource(ImageMediumCarAltText);
					}
						break;
				}
			}
			imageCar.ImageUrl = carImageUrl;
			imageCar.AlternateText = alternateText;
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

		#region Public properties

		/// <summary>
		/// Set and get property if this component is in printable mode or not.
		/// </summary>
		public bool NonPrintable 
		{
			get {return nonprintable;}
			set {nonprintable = value;}
		}

		#endregion
	}
}
