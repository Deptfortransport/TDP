// *********************************************** 
// NAME                 : CarJourneyOptionsControl.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 27/6/2007
// DESCRIPTION			: A custom control to display
//						  the car journey options selected information.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarJourneyOptionsControl.ascx.cs-arc  $
//
//   Rev 1.4   Mar 14 2011 15:12:04   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.3   Feb 19 2010 15:14:16   MTurner
//Added code to resolve issue with invlaid casts being perfomed.
//Resolution for 5406: Car Journey Options control can attempt to perform an invalid cast
//
//   Rev 1.2   Mar 31 2008 13:19:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:36   mturner
//Initial revision.
//
//   Rev 1.3   Sep 06 2007 11:22:06   mmodi
//Screen reader improvements
//Resolution for 4493: Car journey details: Screen reader improvements
//
//   Rev 1.2   Aug 29 2007 16:45:24   mmodi
//Updates mpg and lkm, Via location and Fuel cost
//Resolution for 4486: Car journey details: Fuel cost not shown
//Resolution for 4487: Car journey details: Via location is not shown
//Resolution for 4488: Car journey details: MPG value not changed when units dropdown changed
//
//   Rev 1.1   Jun 28 2007 14:35:18   mmodi
//Code updates
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.0   Jun 27 2007 09:46:04   mmodi
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
	///		Summary description for CarJourneyOptionsControl.
	/// </summary>
	public partial  class CarJourneyOptionsControl : TDUserControl
	{
		#region Controls
		

		
		#endregion

		#region Private members

		private IDataServices populator;
		private bool nonprintable;
		private TDJourneyParametersMulti journeyParameters = null;

		private bool displayMaxSpeed;
		private bool displayMotorways;
		private bool displayAvoid;
		private bool displayUseRoads;
		private bool displayAvoidRoads;
		private bool displayViaLocation;

		#endregion

		#region Initialise Method

		/// <summary>
		/// Initialise this control with the Journey Parameters
		/// </summary>
		public void Initialise(TDJourneyParametersMulti journeyParameters)
		{
			this.journeyParameters = journeyParameters;
		}

		#endregion
			
		#region Page_Load
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
			DetermineJourneyOptionsToDisplay();
			SetDynamicLabels();

			base.OnPreRender(e);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Determines the car journey options available and sets the flag to display or hide
		/// the label containing the information
		/// </summary>
		private void DetermineJourneyOptionsToDisplay()
		{
			if (journeyParameters != null)
			{	//Only display the max speed if its less than the default of Max
				if (journeyParameters.DrivingSpeed < Convert.ToInt32(populator.GetValue(DataServiceType.DrivingMaxSpeedDrop, "Max")))
					DisplayMaxSpeed = true;
				else
					DisplayMaxSpeed = false;

				if (journeyParameters.DoNotUseMotorways)
					DisplayMotorways = true;
				else
					DisplayMotorways = false;

				if (journeyParameters.AvoidFerries || journeyParameters.AvoidTolls || journeyParameters.AvoidMotorWays || journeyParameters.BanUnknownLimitedAccess)
					DisplayAvoid = true;
				else
					DisplayAvoid = false;

				if (journeyParameters.UseRoadsList.Length > 0)
					DisplayUseRoads = true;
				else
					DisplayUseRoads = false;

				if (journeyParameters.AvoidRoadsList.Length > 0)
					DisplayAvoidRoads = true;
				else
					DisplayAvoidRoads = false;

				if ((journeyParameters.PrivateViaLocation != null) 
					&& 
					((journeyParameters.PrivateVia != null) && (journeyParameters.PrivateVia.InputText != string.Empty)))
					DisplayViaLocation = true;
				else
					DisplayViaLocation = false;

				// show hide panels
				pnlRow1.Visible = DisplayMaxSpeed || DisplayMotorways || DisplayAvoid;
				pnlRow2.Visible = DisplayUseRoads;
				pnlRow3.Visible = DisplayAvoidRoads;
				pnlRow4.Visible = DisplayViaLocation;
			}
		}

		/// <summary>
		/// Sets the text for labels 
		/// </summary>
		private void SetDynamicLabels()
		{
			if (journeyParameters != null)
			{
				labelCarJourneyOptionsTitle.Text = GetResource("CarJourneyOptionsControl.Title.Text");

				#region MaxSpeed
				if (DisplayMaxSpeed)
				{
                    decimal speed;
                    if (Decimal.TryParse(MeasurementConversion.Convert((double)journeyParameters.DrivingSpeed, ConversionType.KilometresToMiles), out speed))
                    {
                        speed = Decimal.Round(speed, 0);

                        StringBuilder maxSpeed = new StringBuilder();
                        maxSpeed.Append(GetResource("CarJourneyOptionsControl.CarSpeed.Text"));
                        maxSpeed.Append(" ");
                        maxSpeed.Append(speed.ToString());
                        maxSpeed.Append(" ");
                        maxSpeed.Append(GetResource("CarJourneyOptionsControl.mph.Text"));
                        maxSpeed.Append(".&nbsp;");

                        labelMaxSpeed.Text = maxSpeed.ToString();
                    }
				}
				#endregion

				#region DoNotUseMotorways
				if (DisplayMotorways)
				{
					labelMotorways.Text = GetResource("CarJourneyOptionsControl.NotUsingMotorways.Text") + ".&nbsp;";
				}
				#endregion

				#region Avoid
				if (DisplayAvoid)
				{					
					string ferries = GetResource("FindCarJourneyOptionsControl.AvoidFerries");
					string tolls = GetResource("FindCarJourneyOptionsControl.AvoidTolls");
                    string motorways = GetResource("FindCarJourneyOptionsControl.AvoidMotorways");
                    string limitedaccess = GetResource("FindCarJourneyOptionsControl.LimitedAccess");

					string avoid = GetResource("CarJourneyOptionsControl.Avoid.Text") + " ";

					bool addComma = false;

					if (journeyParameters.AvoidFerries)
					{
						avoid += ferries;
						addComma = true;
					}

                    if (journeyParameters.AvoidTolls)
                    {
                        if (addComma)
                            avoid += ", ";
                        avoid += tolls;
                        addComma = true;
                    }

					if ((journeyParameters.AvoidMotorWays) && (!journeyParameters.DoNotUseMotorways))
					{
						if (addComma)
							avoid += ", ";
						avoid += motorways;
                        addComma = true;
					}

                    if (journeyParameters.BanUnknownLimitedAccess)
                    {
                        if (addComma)
                            avoid += ", ";
                        avoid += limitedaccess;
                    }

					// Replace the last comma with an "and"
					int replace = avoid.LastIndexOf(',');
					if (replace > 0)
					{
						avoid = avoid.Remove(replace, 1);
						avoid = avoid.Insert(replace, " " + GetResource("CarJourneyOptionsControl.AndLowerCase") );
					}

					labelAvoid.Text = avoid + ".";
				}
				else
				{
					labelAvoid.Visible = false;
				}
				#endregion

				#region UseRoads
				if (DisplayUseRoads)
				{
					StringBuilder useRoads = new StringBuilder();
					useRoads.Append( GetResource("CarJourneyOptionsControl.UseRoads.Text") );
					useRoads.Append( " " );

					foreach (TDRoad r in journeyParameters.UseRoadsList)
					{
						if (r.Status == TDRoadStatus.Valid)
						{
							useRoads.Append( r.RoadName );
							useRoads.Append( ", " );
						}
					}
					useRoads.Remove(useRoads.Length - 2, 2);

					string screenReaderPause = "<span class=\"screenreader\">. </span>";
					
					labelUseRoads.Text = useRoads.ToString() + screenReaderPause;
				}
				else
				{
					pnlRow2.Visible = false;
				}
				#endregion
                
				#region AvoidRoads
				if (DisplayAvoidRoads)
				{
					StringBuilder avoidRoads = new StringBuilder(); 
					avoidRoads.Append( GetResource("CarJourneyOptionsControl.AvoidRoads.Text") );
					avoidRoads.Append( " " );

					foreach (TDRoad r in journeyParameters.AvoidRoadsList)
					{
						if (r.Status == TDRoadStatus.Valid)
						{
							avoidRoads.Append( r.RoadName );
							avoidRoads.Append( ", " );
						}
					}
					avoidRoads.Remove(avoidRoads.Length - 2, 2);

					string screenReaderPause = "<span class=\"screenreader\">. </span>";

					labelAvoidRoads.Text = avoidRoads.ToString() + screenReaderPause;
				}
				else
				{
					pnlRow3.Visible = false;
				}
				#endregion

				#region ViaLocation

				if (DisplayViaLocation)
				{
					if (journeyParameters.PrivateViaLocation != null)
						labelViaLocation.Text = string.Format(GetResource("CarJourneyOptionsControl.TravellingVia.Text"), 
							journeyParameters.PrivateViaLocation.Description);
					else
						pnlRow4.Visible = false;
				}
				else
				{
					pnlRow4.Visible = false;
				}

				#endregion
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
		
		#region Public properties

		/// <summary>
		/// Set and get property if this component is in printable mode or not.
		/// </summary>
		public bool NonPrintable 
		{
			get {return nonprintable;}
			set {nonprintable = value;}
		}

		/// <summary>
		/// Set and get property if the label containing max speed should be displayed
		/// </summary>
		public bool DisplayMaxSpeed 
		{
			get {return displayMaxSpeed;}
			set {displayMaxSpeed = value;}
		}

		/// <summary>
		/// Set and get property if the label containing do not use motorways should be displayed
		/// </summary>
		public bool DisplayMotorways
		{
			get {return displayMotorways;}
			set {displayMotorways = value;}
		}

		/// <summary>
		/// Set and get property if the label containing avoid Ferry's, Tolls, and Motorways should be displayed
		/// </summary>
		public bool DisplayAvoid 
		{
			get {return displayAvoid;}
			set {displayAvoid = value;}
		}

		/// <summary>
		/// Set and get property if the label containing Use Roads should be displayed
		/// </summary>
		public bool DisplayUseRoads 
		{
			get {return displayUseRoads;}
			set {displayUseRoads = value;}
		}

		/// <summary>
		/// Set and get property if the label containing Avoid Roads should be displayed
		/// </summary>
		public bool DisplayAvoidRoads 
		{
			get {return displayAvoidRoads;}
			set {displayAvoidRoads = value;}
		}

		/// <summary>
		/// Set and get property if the label containing Via location should be displayed
		/// </summary>
		public bool DisplayViaLocation
		{
			get {return displayViaLocation;}
			set {displayViaLocation = value;}
		}
		#endregion
	}
}
