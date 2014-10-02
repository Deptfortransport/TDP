// *********************************************** 
// NAME                 : CarAllDetailsControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 25/09/2003 
// DESCRIPTION			: Wrapper for the CarJourneyDetails
// and CarSummaryControl. Used to control HTML formatting.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarAllDetailsControl.ascx.cs-arc  $
//
//   Rev 1.7   Sep 08 2011 13:10:20   apatel
//Updated to resolve the issues with printer friendly, padding for daily end date and daily end date adjustment issues
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.6   Sep 01 2011 10:44:40   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Mar 14 2011 15:12:04   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.4   Oct 26 2010 14:30:36   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.3   Dec 02 2009 12:17:24   mmodi
//Updated to display map direction number link on Car journey details table
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:19:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:30   mturner
//Initial revision.
//
//   Rev 1.11   Sep 05 2007 13:35:48   mmodi
//Added code to test for Via location
//Resolution for 4487: Car journey details: Via location is not shown
//
//   Rev 1.10   Jun 28 2007 16:25:48   mmodi
//Added code to control visibility of Journey Options and Type controls
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.9   Jun 28 2007 14:34:22   mmodi
//Code added for CarJourneyOptionsControl
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.8   Mar 14 2006 08:41:46   build
//Automatically merged from branch for stream3353
//
//   Rev 1.7.3.0   Feb 23 2006 18:59:26   NMoorhouse
//Updated for Extend, Replan & Adjust
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 18 2005 15:19:56   asinclair
//Added property to get the CarDetailsTableControl
//
//   Rev 1.6   Mar 01 2005 15:50:58   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.5   Oct 29 2003 09:43:36   kcheung
//Fixed Heading and and added "Directions" label as requested in QA.
//
//   Rev 1.4   Oct 20 2003 10:14:20   kcheung
//Fixed for FXCOP
//
//   Rev 1.3   Oct 15 2003 19:12:22   kcheung
//Fixed help label for car summary so that it appears properly on the details page rather than inside the DIV
//
//   Rev 1.2   Sep 29 2003 16:25:14   kcheung
//Added property to set visibility of the help control
//
//   Rev 1.1   Sep 26 2003 09:20:06   kcheung
//Fixed initialisation bug
//
//   Rev 1.0   Sep 25 2003 12:52:20   kcheung
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.Common;
    using TransportDirect.Common.PropertyService.Properties;
    using TransportDirect.UserPortal.Resource;
    using TransportDirect.UserPortal.Web.Adapters;

	/// <summary>
	///	Wrapper for the CarJourneyDetails and CarSummary controls
	/// </summary>
	public partial  class CarAllDetailsControl : TDUserControl
	{
		protected CarJourneyDetailsTableControl carJourneyDetailsTableControl;
		protected CarSummaryControl carSummaryControl;
		protected CarJourneyTypeControl carJourneyTypeControl;
		protected CarJourneyOptionsControl carJourneyOptionsControl;
		protected HelpCustomControl helpCustomControl;
		protected HelpLabelControl carSummaryHelpLabel;
		

		private RoadUnitsEnum roadUnits;

		private TDJourneyParametersMulti journeyParameters;

		private bool printable;

        private bool outward;

        private bool hasClosure = false;

        public event EventHandler ReplanAvoidClosures;

		/// <summary>
		/// Initialises this control.
		/// </summary>
		/// <param name="outward">Indicates if this control should render
		/// data for outward or return.</param>
        public void Initialise(bool outward, bool showTravelNews, bool hasClosure)
		{
           
			carJourneyDetailsTableControl.Initialise(outward);
			carJourneyDetailsTableControl.DirectionsLabelVisible = true;
			carSummaryControl.Initialise(outward);

			// No journey parameters, so don't display the Car Journey Type/Options controls
			ToggleJourneyTypeControl(false);
			ToggleJourneyOptionsControl(false);

            this.hasClosure = hasClosure;
		}

		/// <summary>
		/// Initialises this control with journey parameters
		/// </summary>
		/// <param name="outward">Indicates if this control should render
		/// data for outward or return.</param>
        public void Initialise(bool outward, TDJourneyParametersMulti journeyParameters, bool showTravelNews, bool hasClosure)
		{
            this.outward = outward;

           	carJourneyDetailsTableControl.Initialise(outward, journeyParameters);
			carJourneyDetailsTableControl.DirectionsLabelVisible = true;
			carSummaryControl.Initialise(outward);
			carJourneyTypeControl.Initialise(journeyParameters, outward);
			carJourneyOptionsControl.Initialise(journeyParameters);


            this.hasClosure = hasClosure;
            
			// Dont display the Journey Options control if the user has not selected any advanced Car journey options
			DisplayJourneyOptionsControl(journeyParameters);
		}

		/// <summary>
		/// Initialises this control with a specific road journey
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
        public void Initialise(RoadJourney roadJourney, TDJourneyViewState viewState, bool outward, bool showTravelNews)
		{
            this.outward = outward;

            carJourneyDetailsTableControl.Initialise(roadJourney, viewState, outward);
			carJourneyDetailsTableControl.DirectionsLabelVisible = true;
			carSummaryControl.Initialise(roadJourney, outward);

			// No journey parameters, so don't display the Car Journey Type/Options controls
			ToggleJourneyTypeControl(false);
			ToggleJourneyOptionsControl(false);
		}

		/// <summary>
		/// Initialises this control with a specific road journey
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
        public void Initialise(RoadJourney roadJourney, TDJourneyViewState viewState, bool outward, TDJourneyParametersMulti journeyParameters, bool showTravelNews)
		{
            this.outward = outward;

            carJourneyDetailsTableControl.Initialise(roadJourney, viewState, outward);
			carJourneyDetailsTableControl.DirectionsLabelVisible = true;
			carSummaryControl.Initialise(roadJourney, outward);
			carJourneyTypeControl.Initialise(journeyParameters, outward);
			carJourneyOptionsControl.Initialise(journeyParameters);

			// Dont display the Journey Options control if the user has not selected any advanced Car journey options
			DisplayJourneyOptionsControl(journeyParameters);
		}

        /// <summary>
        /// Method which sets the values needed to add map javascript to the direction links
        /// </summary>
        public void SetMapProperties(bool addMapJavascript, string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            carJourneyDetailsTableControl.SetMapProperties(addMapJavascript, mapId, 
                mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
        }

		/// <summary>
		/// Page Load Method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{			
            
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
            // Warning message about closure/blockages identified in the journey planned
            avoidClosedRoadsMessageContainer.Visible = hasClosure && printable;

            // Setup resources for the avoid closed road message
            if (avoidClosedRoadsMessageContainer.Visible)
            {
                imageErrorType.ImageUrl = resourceManager.GetString("ErrorDisplayControl.WarningImageUrl", TDCultureInfo.CurrentUICulture);
                imageErrorType.AlternateText = resourceManager.GetString("ErrorDisplayControl.Warning", TDCultureInfo.CurrentUICulture);
                labelErrorDisplayType.Text = resourceManager.GetString("ErrorDisplayControl.Warning", TDCultureInfo.CurrentUICulture);

                lblAvoidClosedRoadsMessage.Text = GetResource("CarAllDetailsControl.AvoidClosedRoadsMessage.Text");
                replanAvoidClosedRoads.Text = GetResource("CarAllDetailsControl.ReplanAvoidClosedRoads.Text");
                replanAvoidClosedRoads.ToolTip = GetResource("CarAllDetailsControl.ReplanAvoidClosedRoads.ToolTip");
            }

			//carJourneyDetailsTableControl.RoadUnits = carSummaryControl.RoadUnits;
			carJourneyDetailsTableControl.NonPrintable = this.Printable;
			carSummaryControl.NonPrintable = this.Printable;

			//If on the printable page then get RoadUnits from the URL string so the correct units 
			//are displayed. 
			if (printable)
			{
				carJourneyDetailsTableControl.RoadUnits = carSummaryControl.RoadUnits;
				
			}
			else
			{
				carJourneyDetailsTableControl.RoadUnits = this.roadUnits;
				carSummaryControl.RoadUnits = this.roadUnits;
			}

            
		}

		public bool Printable
		{
			get 
			{
				return printable;
			}

			set
			{
				printable = value;
			}
		}

		public RoadUnitsEnum RoadUnits
		{
			get 
			{
				return roadUnits;
			}

			set
			{
				roadUnits = value;
			}
		}

		public CarJourneyDetailsTableControl carjourneyDetailsTableControl
		{
			get
			{
				return carJourneyDetailsTableControl;
			}
		}

		// Read/write journey parameters to be used for displaying the journey options
		public TDJourneyParametersMulti JourneyParameters
		{
			get
			{
				return journeyParameters;
			}
			set
			{
				journeyParameters = value;
			}
		}

		/// <summary>
		/// Determines if the user has entered any adavanced options for Car Journey Options
		/// and sets the visibility of the control accordingly
		/// </summary>
		/// <param name="journeyParameters"></param>
		private void DisplayJourneyOptionsControl(TDJourneyParametersMulti journeyParameters)
		{
			bool visible = false;			

			if (journeyParameters != null)
			{
				IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

				if (journeyParameters.DrivingSpeed < Convert.ToInt32(populator.GetValue(DataServiceType.DrivingMaxSpeedDrop, "Max")))
					visible = true;
				else if (journeyParameters.DoNotUseMotorways)
					visible = true;
				else if (journeyParameters.AvoidFerries || journeyParameters.AvoidTolls || journeyParameters.AvoidMotorWays || journeyParameters.BanUnknownLimitedAccess)
					visible = true;
				else if (journeyParameters.UseRoadsList.Length > 0)
					visible = true;
				else if (journeyParameters.AvoidRoadsList.Length > 0)
					visible = true;
				else if ((journeyParameters.PrivateViaLocation != null) // We have a via location resolved
					&&		// and ensure the user has entered a via location
					((journeyParameters.PrivateVia != null) && (journeyParameters.PrivateVia.InputText != string.Empty)))
					visible = true;
				else
					visible = false;
			}
			
			ToggleJourneyOptionsControl(visible);
		}

		/// <summary>
		/// Shows/hides the journey type control
		/// </summary>
		/// <param name="visible">visible flag</param>
		private void ToggleJourneyTypeControl(bool visible)
		{
			carJourneyTypeControl.Visible = visible;
			panelJourneyTypeControl.Visible = visible;
		}

		/// <summary>
		/// Shows/hides the journey options control
		/// </summary>
		/// <param name="visible">visible flag</param>
		private void ToggleJourneyOptionsControl(bool visible)
		{
			carJourneyOptionsControl.Visible = visible;
			panelJourneyOptionsControl.Visible = visible;
		}
                

        /// <summary>
        /// raise event to replan the road journey to avoid closure/blockages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void replanAvoidClosedRoads_Click(object sender, EventArgs e)
        {
            if (ReplanAvoidClosures != null)
            {
                ReplanAvoidClosures(this, EventArgs.Empty);
            }
        }

		#region Web Form Designer generated code
		/// <summary>
		/// OnInit Method
		/// </summary?
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
            replanAvoidClosedRoads.Click += new EventHandler(replanAvoidClosedRoads_Click);
		}

		#endregion
	}
}
