// *********************************************** 
// NAME                 : JourneyEmissionsSpeedoDial.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/11/2006 
// DESCRIPTION			: Control displaying the Speedo dial used on Journey Emissions Dashboad
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionsSpeedoDial.ascx.cs-arc  $ 
//
//   Rev 1.3   Dec 19 2008 15:06:24   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:34   mturner
//Initial revision.
//
//   Rev 1.5   May 22 2007 17:05:06   mmodi
//Updated speedo alt text
//Resolution for 4412: 9.6 - WAI / Accessibility Issues
//
//   Rev 1.4   Feb 26 2007 16:22:46   mmodi
//Moved Process outside of try so it can be disposed in the finally
//Resolution for 4360: Del 9: Portal performance - memory leakage problem
//
//   Rev 1.3   Feb 21 2007 18:06:12   mmodi
//Moved position of newimage.Dispose and added a p.Dispose that was missing
//Resolution for 4360: Del 9: Portal performance - memory leakage problem
//
//   Rev 1.2   Jan 08 2007 15:09:58   PScott
//IR 4325/ vantive 4517240
//issue net start and retry save when initial save fails
//
//   Rev 1.1   Dec 15 2006 11:06:00   mmodi
//Updated code to use decimal values for Scale values
//Resolution for 4321: CO2: Use accurate Scales when calculating angle
//
//   Rev 1.0   Dec 14 2006 15:52:56   mmodi
//Initial revision.
//
//   Rev 1.5   Nov 27 2006 17:54:14   mmodi
//Changed save image format to jpeg
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Nov 27 2006 15:56:34   mmodi
//Changed save format to PNG as GIF were not sharp enough
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.3   Nov 27 2006 13:16:56   mmodi
//Added code to load correct Pointer file when in compare mode
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 26 2006 15:47:26   mmodi
//Added OperationalEvent for creating and saving image
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.1   Nov 24 2006 11:17:06   mmodi
//Code changes and updates as part of workstream
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Nov 19 2006 10:40:02   mmodi
//Initial revision.
//Resolution for 4240: CO2 Emissions
//

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Text;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;

using Logger=System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
	public enum SpeedoDialType
	{
		FuelCost,
		CO2Emission
	}

	/// <summary>
	///		Summary description for JourneyEmissionsSpeedoDial.
	/// </summary>
	public partial class JourneyEmissionsSpeedoDial : TDUserControl
	{

		#region Constants

		private const string GAUGESCALE = "JourneyEmissionsSpeedoDial.Gauge.ImageURL";
		private const string POINTER = "JourneyEmissionsSpeedoDial.Pointer.ImageURL";
		private const string POINTERNOYELLOWTIP = "JourneyEmissionsSpeedoDial.PointerNoYellowTip.ImageURL";
		private const string POINTERCOMPARE = "JourneyEmissionsSpeedoDial.PointerCompare.ImageURL";

		private const int ROTATE = 180;

		private const string SPEEDOCOLOUR = "JourneyEmissions.SpeedoColour";
		private const string SAVESPEEDOFILELOCATION = "JourneyEmissions.SpeedoDialImageSaveLocation";
		private const string SPEEDOREFERRALURL = "JourneyEmissions.SpeedoDialReferralURL";
		private const string SPEEDOTEXT = "speedo";
		private const string FILEEXTENSION = ".jpg";
		private const string DIALTYPEFUELCOST = "FC";
		private const string DIALTYPEEMISSION = "CE";

		private const string COSTTITLE = "JourneyEmissionsDashboard.costTitle.Text";
		private const string EMISSIONSTITLE = "JourneyEmissionsDashboard.emissionsTitle.Text";

		private const string SMALL = "small";
		private const string MEDIUM = "medium";
		private const string LARGE = "large";

		private const string GREEN = "JourneyEmissionsSpeedoDial.Green";
		private const string AMBER = "JourneyEmissionsSpeedoDial.Amber";
		private const string RED = "JourneyEmissionsSpeedoDial.Red";

		#endregion

		#region Private members

		private decimal angle;
		private decimal angleCompare;
		private decimal ANGLEMIN;
		private decimal ANGLEMAX;

		private decimal pointerValue;
		private decimal pointerCompareValue;
		private decimal scaleValueMin;
		private decimal scaleValueMax;

		private string speedoFilename;
		private string altText;

		private JourneyEmissionsPageState pageState;

		private SpeedoDialType speedoDialType;

		new protected TDResourceManager resourceManager = Global.tdResourceManager;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyEmissionsSpeedoDial()
		{
		}

		#endregion

		#region Page_load, Page_PreRender

		protected void Page_Load(object sender, System.EventArgs e)
		{			
			pageState = TDSessionManager.Current.JourneyEmissionsPageState;

			InitialiseValues();			
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			imageSpeedoDial.ImageUrl = Properties.Current[SPEEDOREFERRALURL] + speedoFilename;
			imageSpeedoDial.AlternateText = this.altText; //CreateAltText();
			imageSpeedoDial.Visible = true;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets all private member variables to defaults
		/// </summary>
		private void InitialiseValues()
		{
			angle = 0;
			angleCompare = 0;
			ANGLEMIN = 50;
			ANGLEMAX = 130;

			pointerValue = 0;
			pointerCompareValue = 0;
			scaleValueMin = 0;
			scaleValueMax = 0;

			speedoFilename = string.Empty;
		}

		/// <summary>
		/// Returns the Alt text for the speedo
		/// </summary>
		/// <returns></returns>
		private string CreateAltText(string scaleMin, string scaleMax, bool compare)
		{
			string DIAGRAMSHOWING = resourceManager.GetString("JourneyEmissionsSpeedoDial.DiagramShowing");
			string DIALISSPLIT = resourceManager.GetString("JourneyEmissionsSpeedoDial.DialIsSplit");
			string DIALISPOINTING = resourceManager.GetString("JourneyEmissionsSpeedoDial.DialIsPointing");
			string DIALISPOINTINGCOMPARE = resourceManager.GetString("JourneyEmissionsSpeedoDial.DialIsPointing.Compare");

			string altText;
			string colourPointer;
			string colourComparePointer;
			string pointerAmount;
			string pointerAmountCompare;

			// determine what colour the pointer is in
			if (angle <= 254)
				colourPointer = resourceManager.GetString(GREEN, TDCultureInfo.CurrentUICulture);
			else if (angle <= 282)
				colourPointer = resourceManager.GetString(AMBER, TDCultureInfo.CurrentUICulture);
			else
				colourPointer = resourceManager.GetString(RED, TDCultureInfo.CurrentUICulture);

			if (angleCompare <= 254)
				colourComparePointer = resourceManager.GetString(GREEN, TDCultureInfo.CurrentUICulture);
			else if (angleCompare <= 282)
				colourComparePointer = resourceManager.GetString(AMBER, TDCultureInfo.CurrentUICulture);
			else
				colourComparePointer = resourceManager.GetString(RED, TDCultureInfo.CurrentUICulture);

			// Add the first part of the alt text - dial type and scale values
			StringBuilder sb = new StringBuilder();
			if (speedoDialType == SpeedoDialType.FuelCost)
			{
				pointerAmount = resourceManager.GetString(COSTTITLE) + Decimal.Round( Convert.ToDecimal(pageState.FuelCostValue), 0).ToString();
				pointerAmountCompare = resourceManager.GetString(COSTTITLE) + Decimal.Round( Convert.ToDecimal(pageState.FuelCostCompareValue), 0).ToString();

				sb.Append( string.Format(DIAGRAMSHOWING, "A", resourceManager.GetString("JourneyEmissionsDashboard.fuelCost.Text").ToLower(), scaleMin, scaleMax));
			}
			else
			{
				pointerAmount = pointerValue + resourceManager.GetString(EMISSIONSTITLE);
				pointerAmountCompare = pointerCompareValue + resourceManager.GetString(EMISSIONSTITLE);

				sb.Append( string.Format(DIAGRAMSHOWING, "B", resourceManager.GetString("JourneyEmissionsDashboard.emissions.Text"), scaleMin, scaleMax));
			}

			// Add the dial colour ranges - e.g. green, yellow, amber
			sb.Append( DIALISSPLIT );

			// Add the pointer values and colour range it points to. Slightly different text if this is the Compare dial
			if (!compare)
			{
				sb.Append( string.Format(DIALISPOINTING, pointerAmount, colourPointer ));
			}
			else
			{
				if (speedoDialType == SpeedoDialType.FuelCost)
                    sb.Append( string.Format(DIALISPOINTINGCOMPARE, pointerAmount, colourComparePointer, pointerAmountCompare, colourPointer ));
				else
					sb.Append( string.Format(DIALISPOINTINGCOMPARE, pointerAmountCompare, colourComparePointer, pointerAmount, colourPointer));
			}

			altText = sb.ToString();

			return altText;
		}

		#region Angles
		/// <summary>
		/// Calculates the angle used to rotate the pointer on the speedo
		/// </summary>
		private void CalculateAngle()
		{

			angle = PointerAngle(pointerValue, scaleValueMin, scaleValueMax);

			// Need to Rotate the angle by 180 degrees so we're starting on the 9 o'clock
			angle += ROTATE;
		}

		/// <summary>
		/// Calculates two angles used to rotate the pointers on the speedo, one for the original value,
		/// and one for the compare value
		/// </summary>
		private void CalculateCompareAngle()
		{
			if (speedoDialType == SpeedoDialType.FuelCost)
			{
				CalculateFuelAngle();
			}
			else
			{
				CalculateAngle();
			}
			
			angleCompare = PointerAngle( pointerCompareValue, scaleValueMin, scaleValueMax);

			// Need to Rotate the angle by 180 degrees so we're starting on the 9 o'clock
			angleCompare += ROTATE;
		}

		/// <summary>
		/// This is a hack due to the strange decisions for displaying the Speedo dial for Fuel Cost. 
		/// In effect, the Fuel Cost is the same as the CO2 emissions dial, but with the Compare pointer
		/// angle adjusted dependent on the difference between the Your Car fuel cost and the Compare Car
		/// fuel cost angles
		/// </summary>
		private void CalculateFuelAngle()
		{
			// To calculate the Compare Car Fuel Cost angle we want to perform the following calculation:
			// Compare car fuel cost angle = Your car CO2 angle + (Compare Car fuel cost angle - Your car fuel cost angle)

			decimal yourCarFuelCostAngle;
			decimal compareCarFuelCostAngle;
			decimal yourCarEmissionsAngle;

			decimal fuelCost = Convert.ToDecimal(pageState.FuelCostValue);
			decimal fuelCostCompare = Convert.ToDecimal(pageState.FuelCostCompareValue);
			decimal fuelCostScaleValueMin = pageState.FuelCostScaleMin;
			decimal fuelCostScaleValueMax = pageState.FuelCostScaleMax;

			decimal emissions = Convert.ToDecimal(pageState.EmissionsValue);
			decimal emissionsScaleValueMin = pageState.EmissionsScaleMin;
			decimal emissionsScaleValueMax = pageState.EmissionsScaleMax;

			// Calculate the angles
			yourCarFuelCostAngle = PointerAngle(fuelCost, fuelCostScaleValueMin, fuelCostScaleValueMax);
			compareCarFuelCostAngle = PointerAngle(fuelCostCompare, fuelCostScaleValueMin, fuelCostScaleValueMax);
			yourCarEmissionsAngle = PointerAngle(emissions, emissionsScaleValueMin, emissionsScaleValueMax);

			angle = yourCarEmissionsAngle + (compareCarFuelCostAngle - yourCarFuelCostAngle);

			// Check if the new angle is above or below the max/min and adjust accordingly
			if ((angle < ANGLEMIN) || (fuelCostCompare < fuelCostScaleValueMin))
				angle = ANGLEMIN;

			if ((angle > ANGLEMAX) || (fuelCostCompare > fuelCostScaleValueMax))
				angle = ANGLEMAX;

			// Need to Rotate the angle by 180 degrees so we're starting on the 9 o'clock
			angle += ROTATE;			
		}

		/// <summary>
		/// This is a helper method to calculate the angle, without adding on the ROTATE needed
		/// </summary>
		/// <param name="pointerValue"></param>
		/// <param name="scaleMin"></param>
		/// <param name="scaleMax"></param>
		/// <returns></returns>
		private decimal PointerAngle(decimal pointerVal, decimal scaleMin, decimal scaleMax)
		{
			decimal myAngle = 0;

			if (pointerVal <= scaleMin)
				myAngle = ANGLEMIN;

			if (pointerVal >= scaleMax)
				myAngle = ANGLEMAX;
			
			if ((pointerVal > scaleMin) && (pointerVal < scaleMax))
			{
				decimal temp = (pointerVal - scaleMin) / (scaleMax - scaleMin);
				myAngle = temp * (ANGLEMAX - ANGLEMIN) + ANGLEMIN;
				
				// set to max if we've gone over max scale
				if (myAngle > ANGLEMAX)
					myAngle = ANGLEMAX;
			}

			return myAngle;
		}

		#endregion

		/// <summary>
		/// Returns a path/filename to save the speedo to
		/// </summary>
		/// <returns>String filename</returns>
		private string SpeedoFilename()
		{
			string filename;
			DateTime dt = DateTime.Now;
			dt.AddMilliseconds(2);
			string datetime = dt.ToString("yyyyMMddHHmmssfff");

			if (speedoDialType == SpeedoDialType.FuelCost)
			{
				filename = SPEEDOTEXT + 
					DIALTYPEFUELCOST + 
					datetime +
					TDSessionManager.Current.Session.SessionID + 
					FILEEXTENSION;
			}
			else
			{
				filename = SPEEDOTEXT + 
					DIALTYPEEMISSION + 
					datetime +
					TDSessionManager.Current.Session.SessionID + 
					FILEEXTENSION;
			}
			
			return filename;			
		}

		/// <summary>
		/// Returns a string for the value supplied adding the Cost or Emissions title
		/// </summary>
		/// <param name="scaleValue">Value to convert</param>
		/// <returns>Speedo value as a string</returns>
		private string SetSpeedoValue(decimal scaleValue)
		{	
			string speedoValue = Convert.ToInt32(scaleValue).ToString();
			if (speedoDialType == SpeedoDialType.FuelCost)
				speedoValue = resourceManager.GetString(COSTTITLE, TDCultureInfo.CurrentUICulture) + speedoValue;
			else
				speedoValue = speedoValue + resourceManager.GetString(EMISSIONSTITLE, TDCultureInfo.CurrentUICulture);

			return speedoValue;
		}

		/// <summary>
		/// Draws the speedo pointer and the compare pointer on the scale with the angle supplied
		/// </summary>
		/// <param name="drawCompare">Bool to draw the compare pointer</param>
		private void DrawSpeedoGauge(bool drawCompare)
		{
			# region Image filepaths

			string gaugeFilePath = Page.Server.MapPath(resourceManager.GetString(GAUGESCALE,TDCultureInfo.CurrentUICulture));
			string pointerCompareFilePath = Page.Server.MapPath(resourceManager.GetString(POINTERCOMPARE,TDCultureInfo.CurrentUICulture));
			string pointerFilePath;
			// load the appropriate pointer
			if (drawCompare)
			{
				pointerFilePath = Page.Server.MapPath(resourceManager.GetString(POINTERNOYELLOWTIP, TDCultureInfo.CurrentUICulture));
			}
			else
			{
				pointerFilePath = Page.Server.MapPath(resourceManager.GetString(POINTER, TDCultureInfo.CurrentUICulture));
			}
			string petrolPumpFilePath = Page.Server.MapPath(resourceManager.GetString("JourneyEmissionsDashboard.imageFuel.ImageUrl", TDCultureInfo.CurrentUICulture));
			// load the appropriate CO2 car
			string carCO2FilePath;
			switch (pageState.CarSize.ToLower())
			{
				case SMALL:
				{
					carCO2FilePath = Page.Server.MapPath(resourceManager.GetString("JourneyEmissionsDashboard.imageEmissions.SmallCO2.ImageUrl", TDCultureInfo.CurrentUICulture));
				}
					break;
				case MEDIUM:
				{
					carCO2FilePath = Page.Server.MapPath(resourceManager.GetString("JourneyEmissionsDashboard.imageEmissions.MediumCO2.ImageUrl", TDCultureInfo.CurrentUICulture));
				}
					break;
				case LARGE:
				{
					carCO2FilePath = Page.Server.MapPath(resourceManager.GetString("JourneyEmissionsDashboard.imageEmissions.LargeCO2.ImageUrl", TDCultureInfo.CurrentUICulture));
				}
					break;
				default:
				{
					carCO2FilePath = Page.Server.MapPath(resourceManager.GetString("JourneyEmissionsDashboard.imageEmissions.ImageUrl", TDCultureInfo.CurrentUICulture));
				}
					break;
			}
           
			#endregion
			
			OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, "Speedo image file : STARTED");
			Logger.Write(oe);

			// Load images
			Bitmap gauge = new Bitmap(gaugeFilePath);
			Bitmap pointer = new Bitmap(pointerFilePath);

			// use gauge and pointer lengths to determine new image size
			// and add some padding for the new image
			int imageWidth = gauge.Width + 30;
			int imageHeight = pointer.Width + 0;
			int rotatePointX = (imageWidth / 2); 
			int rotatePointY = imageHeight - 18;
			int pointerStartX = (imageWidth / 2) - (pointer.Height / 2); 
			int pointerStartY = imageHeight - 28;

			// rgb value used for the background colour of the new image
			// Ensure colour used here is the same as that used in the stylesheet ("boxtypetwentyseven")
			// Foe easier maintenance, obtain RGB property from database
			string rgb = Properties.Current[SPEEDOCOLOUR];
			char[] seperator = new Char[1];
			seperator[0] = Convert.ToChar(",");
			string[] rgbArray = rgb.Split(seperator, 3);

			int r = Convert.ToInt32(rgbArray[0]);
			int g = Convert.ToInt32(rgbArray[1]);
			int b = Convert.ToInt32(rgbArray[2]);

			Bitmap newImage = new Bitmap(imageWidth, imageHeight);
			Graphics graphics = Graphics.FromImage(newImage);

			// Set background colour
			Brush brush = new SolidBrush(Color.FromArgb(r,g,b));
			graphics.FillRectangle(brush, 0, 0, imageWidth,imageHeight);
			brush.Dispose();

			// Add the Gauge
			// positioning it slightly in because of the padding added to the new image
			graphics.DrawImage(gauge, 15, 0);
			
			// used for the scale text positioning
			int gaugeHeight = gauge.Height;
			int gaugeWidth = gauge.Width;
			gauge.Dispose();

			// Add the Scale labels
			string scaleMin; 
			string scaleMax; 
			
			//Fix for IR4292 - replicate pointers to match Emissions speedo
			if (speedoDialType == SpeedoDialType.FuelCost)
			{
				scaleMin = SetSpeedoValue(
								Convert.ToDecimal(System.Math.Floor(Convert.ToDouble(pageState.FuelCostScaleMin)))
										); 
				scaleMax = SetSpeedoValue(
								Convert.ToDecimal(System.Math.Ceiling(Convert.ToDouble(pageState.FuelCostScaleMax)))
										); 
				if (pageState.FuelCostScaleMax <= 1)
					scaleMax = "<" + scaleMax;
			}
			else
			{
				scaleMin = SetSpeedoValue(
								Convert.ToDecimal(System.Math.Floor(Convert.ToDouble(scaleValueMin)))
										); 
				scaleMax = SetSpeedoValue(
								Convert.ToDecimal(System.Math.Ceiling(Convert.ToDouble(scaleValueMax)))
										); 
			}

			

			// Adjust positioning of ScaleValueMax text for CO2 Emissions Speedo
			// otherwise the Kg gets cut off	
			int scaleMaxPos = imageWidth-30;
			if ((scaleValueMax >= 100) && (speedoDialType == SpeedoDialType.CO2Emission))
				scaleMaxPos = scaleMaxPos - 5;

			brush = new SolidBrush(Color.Black);
			Font font = new Font("Arial", 8);
			graphics.DrawString(scaleMin, font, brush, 5, 25);
			graphics.DrawString(scaleMax, font, brush, scaleMaxPos, 25);
			brush.Dispose();
			font.Dispose();
	
			// add the petrol pump or CO2 Car icon in bottom left corner of image
			Bitmap icon;
			if (speedoDialType == SpeedoDialType.CO2Emission)
				icon = new Bitmap(carCO2FilePath);
			else
				icon = new Bitmap(petrolPumpFilePath);
			
			graphics.DrawImage(icon, 10, imageHeight-40);
			icon.Dispose();

			if (drawCompare)
			{
				// Rotate for Compare pointer
				Matrix matrixCompare = new Matrix();
				matrixCompare.RotateAt(Convert.ToInt32(angleCompare), new PointF(rotatePointX, rotatePointY));
				graphics.Transform = matrixCompare;
				matrixCompare.Dispose();
			
				// Load compare arrow pointer
				Bitmap pointerCompare = new Bitmap(pointerCompareFilePath);
				graphics.DrawImage(pointerCompare, pointerStartX, pointerStartY);
				pointerCompare.Dispose();
			}

			// Rotate
			Matrix matrix = new Matrix();
			matrix.RotateAt(Convert.ToInt32(angle), new PointF(rotatePointX, rotatePointY));
			graphics.Transform = matrix;
			matrix.Dispose();

			// Load arrow pointer
			graphics.DrawImage(pointer, pointerStartX, pointerStartY);
			pointer.Dispose();

			// Dispose
			graphics.Dispose();

			#region Save speedo

			// Write the image file - Attempt 2 times
			// placing in a try catch for sanity
			for ( int writeAttempt = 1 ; writeAttempt<=2;writeAttempt++)
			{
				try
				{
					// Establish file to save to
					speedoFilename = SpeedoFilename();
					// Save the dynamically created speedo
					newImage.Save(Properties.Current[SAVESPEEDOFILELOCATION] + speedoFilename, ImageFormat.Jpeg);					
					oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, "Speedo image file saved to output stream ");
					Logger.Write(oe);
					writeAttempt = 3; //written so no need to make 2nd attempt
				}
				catch
				{
					if (writeAttempt == 1)
					{
						//phils code below
						int statusCode = 0;
						StringBuilder tempString = new StringBuilder();
						tempString.Append(" USE "+Properties.Current[SAVESPEEDOFILELOCATION]);
						tempString.Remove(tempString.Length-1,1);
						Process p = new Process();
						try
						{
							// Set up process attributes.
							p.StartInfo.UseShellExecute = false;
							p.StartInfo.CreateNoWindow = false; 
							p.StartInfo.FileName = "NET";
							p.StartInfo.WorkingDirectory = ".";
							p.StartInfo.Arguments = tempString.ToString();
							p.Start();
							p.WaitForExit();
							statusCode = p.ExitCode;
							if (statusCode!=0)
							{
								oe = new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Error, "StatusCode "+statusCode.ToString()+ " has occurred when executing NET " +tempString);
								Logger.Write(oe);
							}
						}
						catch(Exception e)
						{
							statusCode = (int)TDExceptionIdentifier.DGCommandLineImportError;
							oe = new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Error, statusCode.ToString()+ ":" +e.Message+ " has occurred for NET " +tempString);
							Logger.Write(oe);
							writeAttempt = 3; //Net use has failed so dont bother with 2nd attempt
						}
						finally
						{
							p.Dispose();
						}
					}
					else
					{
						//phils code above
						oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Failure streaming Speedo image file to outputstream : ");
						Logger.Write(oe);
					}
				}
			}
			#endregion

			// Finally, ensure image is cleared from memory
			newImage.Dispose();

			// Set the alt text string
			altText = CreateAltText(scaleMin, scaleMax, drawCompare);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Creates the speedo, first populating the control values
		/// </summary>
		/// <param name="pointerValue">Value for the pointer</param>
		/// <param name="scaleValueMin">Minimum value used for the scale</param>
		/// <param name="scaleValueMax">Maximum value used for the scale</param>
		/// <param name="speedoDialType">SpeedoDialType</param>
		public void CreateSpeedo(decimal pointerValue, decimal scaleValueMin, decimal scaleValueMax, SpeedoDialType speedoDialType)
		{
			this.pointerValue = pointerValue;
			this.scaleValueMin = scaleValueMin;
			this.scaleValueMax = scaleValueMax;
			this.speedoDialType = speedoDialType;

			CalculateAngle();
			DrawSpeedoGauge(false);
		}

		/// <summary>
		/// Creates the speedo, first populating the control values
		/// </summary>
		/// <param name="pointerValue">Value for the pointer</param>
		/// <param name="pointerCompareValue">Value for the compare pointer</param>
		/// <param name="scaleValueMin">Minimum value used for the scale</param>
		/// <param name="scaleValueMax">Maximum value used for the scale</param>
		/// <param name="speedoDialType">SpeedoDialType</param>
		public void CreateCompareSpeedo(decimal pointerValue, decimal pointerCompareValue, decimal scaleValueMin, decimal scaleValueMax, SpeedoDialType speedoDialType)
		{
			// switch the pointer values so we have the compare value as the solid pointer,
			// and the original value as the semi transparent pointer
			this.pointerValue = pointerCompareValue;
			this.pointerCompareValue = pointerValue;
			this.scaleValueMin = scaleValueMin;
			this.scaleValueMax = scaleValueMax;
			this.speedoDialType = speedoDialType;

			CalculateCompareAngle();
			DrawSpeedoGauge(true);
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
