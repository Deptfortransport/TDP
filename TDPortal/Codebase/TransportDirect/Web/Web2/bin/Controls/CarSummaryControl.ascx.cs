// *********************************************** 
// NAME                 : CarSummaryControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 9/9/2003
// DESCRIPTION			: A custom control to display
// summary of a car journey.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarSummaryControl.ascx.cs-arc  $
//
//   Rev 1.6   Oct 26 2009 10:08:38   mmodi
//Show EBC distance to 2dp
//
//   Rev 1.5   Oct 16 2009 14:04:30   mmodi
//Tidied up
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Sep 21 2009 14:57:14   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 13 2008 16:41:36   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 26 2008 13:42:14   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:19:40   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:44   mturner
//Initial revision.
//
//   Rev 1.29   Mar 13 2006 17:32:28   NMoorhouse
//Manual merge of stream3353 -> trunk
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28   Feb 23 2006 19:16:26   build
//Automatically merged from branch for stream3129
//
//   Rev 1.27.2.0   Feb 23 2006 19:01:26   NMoorhouse
//Updated for Extend, Replan & Adjust
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.27.1.1   Jan 31 2006 10:02:14   mdambrine
//assign td common namspace
//
//   Rev 1.27.1.0   Jan 30 2006 16:33:06   kjosling
//moved TDCulture to common project
//
//   Rev 1.27.1.0   Jan 10 2006 15:23:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.27   May 26 2005 10:46:24   rscott
//Updated to resolve IR2529
//
//   Rev 1.26   Apr 29 2005 14:26:02   rscott
//Further changes for IR1984
//
//   Rev 1.25   Apr 29 2005 12:13:22   rscott
//Updated fix for IR1984
//
//   Rev 1.24   Apr 22 2005 15:34:20   rscott
//Fix for JavaScript out of sync issue IR 1984
//
//   Rev 1.23   Apr 20 2005 15:02:34   asinclair
//Fix for IR 2235
//
//   Rev 1.22   Apr 13 2005 14:45:10   asinclair
//Fix for IRs 2044 and 1984
//
//   Rev 1.21   Mar 03 2005 14:12:50   rscott
//DEL 7 - updated spans for miles to include a name attribute - to enable getelementsByName within FireFox
//
//   Rev 1.20   Mar 01 2005 15:54:36   asinclair
//Updated for Del 7 car costing - work in progress
//
//   Rev 1.19   Sep 17 2004 15:13:44   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.18   Aug 17 2004 11:20:18   rgreenwood
//IR 1286 - Check TDItineraryManager for Journey Data
//
//   Rev 1.17   Nov 26 2003 16:46:50   kcheung
//Fixed time rounding errors.
//Previously using Math.Round - this always rounds to the  nearest EVEN number when the value to round is a point 5.  This means if a duration is 1:30, you get 2 minutes, and when it is 2:30 you also get 2 minutes - which creates inconsistency.
//Custom round method has been added to remove this problem.
//
//   Rev 1.16   Nov 26 2003 14:35:34   kcheung
//Fixed rounding error.
//Resolution for 409: Unadjusted route (car journey) duration is set to 0hrs 0 minutes
//
//   Rev 1.15   Nov 24 2003 17:09:06   kcheung
//Allows one journey to be null, and the other car journey summary will still be displayed.  This should never be the case however, as the CJP should return both car journeys.
//
//   Rev 1.14   Nov 06 2003 13:23:10   kcheung
//Updated so that summary only displays road numbers.
//
//   Rev 1.13   Nov 04 2003 12:34:16   kcheung
//Fixed design as requested in QA
//
//   Rev 1.12   Oct 27 2003 12:14:16   kcheung
//Fixed so that ; does not appear if a road name is empty
//
//   Rev 1.11   Oct 21 2003 16:51:04   kcheung
//Changes to string test to test for length empty for FXCOP
//
//   Rev 1.10   Oct 21 2003 16:45:04   kcheung
//Changes for the benefit of FXCOP
//
//   Rev 1.9   Oct 20 2003 10:31:54   kcheung
//Cosmetic corrections for FXCOP
//
//   Rev 1.8   Oct 15 2003 19:25:02   kcheung
//Removed redundant HelpLabelVisible property
//
//   Rev 1.7   Oct 15 2003 19:12:26   kcheung
//Fixed help label for car summary so that it appears properly on the details page rather than inside the DIV
//
//   Rev 1.6   Oct 15 2003 13:29:58   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.5   Oct 08 2003 10:37:04   PNorell
//Removed exceptions and errors when no journey results where found.
//
//   Rev 1.4   Sep 29 2003 16:25:10   kcheung
//Added property to set visibility of the help control
//
//   Rev 1.3   Sep 25 2003 13:05:20   kcheung
//Integrated HTML for Car Details Control
//
//   Rev 1.2   Sep 22 2003 17:20:42   PNorell
//Integrated help controls and associated resources.
//Fixed bug in event handling in JourneyDetails.
//
//   Rev 1.1   Sep 10 2003 15:24:56   kcheung
//Working version

/// TODO: Integrate with HTML

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common;

	/// <summary>
	///	A custom control to display summary of a car journey.
	/// </summary>
	public abstract class CarSummaryControl : TDUserControl
    {
        #region Private members

        protected System.Web.UI.WebControls.Label labelAdjustedTotalDistance;
		protected System.Web.UI.WebControls.Label labelAdjustedTotalDistanceNumber;
		protected System.Web.UI.WebControls.Label labelAdjustedTotalDuration;
		protected System.Web.UI.WebControls.Label labelAdjustedRoads;
		protected System.Web.UI.WebControls.Label labelSummaryOfDirections;
		protected System.Web.UI.HtmlControls.HtmlTableRow adjustedJourneyRow;

		// Inidicates if outward or return journey should be rendered.
		private bool outward = false;
		protected System.Web.UI.WebControls.Label labelAdjustedTotalDurationNumber;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected TransportDirect.UserPortal.Web.Controls.ScriptableDropDownList dropdownlistCarSummary;
		protected System.Web.UI.WebControls.Label lblUnits;

		// Indicates whether TDItineraryManager is being used.
		private bool usingItinerary = false;

		//Added for Del 7
		//Populator to load the strings for the DropDown list
		private DataServices populator;
		private RoadUnitsEnum roadUnits;
		private bool nonprintable;
		private RoadJourney adjustedRoadJourney = null;

        // int to determine how many decimal places should be used for the distance display
        private int distanceDecimalPlaces = 1;

        #endregion

        #region Initialise Method

        /// <summary>
		/// Initialise this control.
		/// </summary>
		/// <param name="outward">Renders outward car journey details if true,
		/// or return car journey details if false.</param>
		public void Initialise(bool outward)
		{
			this.outward = outward;
		}

		/// <summary>
		/// Initialise this control with a specific road journey
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="outward">Renders outward car journey details if true,
		/// or return car journey details if false.</param>
		public void Initialise(RoadJourney roadJourney, bool outward)
		{
			this.adjustedRoadJourney = roadJourney;
			this.outward = outward;
		}

        /// <summary>
        /// Initialise this control with a specific road journey
        /// </summary>
        /// <param name="roadJourney">The specific road journey to display</param>
        /// <param name="outward">Renders outward car journey details if true,
        /// or return car journey details if false.</param>
        /// <param name="distanceDecimalPlaces">The number of decimal places to show the distance in</param>
        public void Initialise(RoadJourney roadJourney, bool outward, int distanceDecimalPlaces)
        {
            this.adjustedRoadJourney = roadJourney;
            this.outward = outward;
            this.distanceDecimalPlaces = distanceDecimalPlaces;
        }

		#endregion

		#region ViewState Code
		/// <summary>
		/// Loads the View State.
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					outward = (bool)myState[1];
			}
		}

		public InputPageState pageState
		{
			get 
			{ 
				return TDSessionManager.Current.InputPageState;
			}
		}

	
		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[2];
			allStates[0] = baseState;
			allStates[1] = outward;

			return allStates;
		}
		#endregion

		#region Page Load / OnPreRender Methods

		/// <summary>
		/// Page Load method
		/// </summary>
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Initilaise static labels from resourcing manager
			labelSummaryOfDirections.Text = Global.tdResourceManager.GetString(
				"CarSummaryControl.labelSummaryOfDirections.Text", TDCultureInfo.CurrentUICulture);

			labelAdjustedTotalDistance.Text = Global.tdResourceManager.GetString(
				"CarSummaryControl.labelTotalDistance", TDCultureInfo.CurrentUICulture);

			labelAdjustedTotalDuration.Text = Global.tdResourceManager.GetString(
				"CarSummaryControl.labelTotalDuration", TDCultureInfo.CurrentUICulture);

            lblUnits.Text = Global.tdResourceManager.GetString
                    ("CarSummaryControl.lblUnits", TDCultureInfo.CurrentUICulture);
            
            if(!IsPostBack)
				SetUpResources();
			
			AlignServerWithClient();

		}

		/// <summary>
		/// Constructs all dynamic labels and calls base OnPreRender
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			this.DataBind();
			usingItinerary = TDItineraryManager.Current.Length > 0 && !TDItineraryManager.Current.ExtendInProgress;
			SetDynamicLabels();
			
			//Only want to do this if not on the nonprintable page.
			if (nonprintable)
			{
				EnableScriptableObjects();
			}

			SetupUnitsDropdown();

            base.OnPreRender(e);
		}

		#endregion

        #region Private methods

        #region Methods to set text for labels

        /// <summary>
		/// Sets the text in all the dynamic labels by reading data from
		/// TDSessionManager.
		/// </summary>
		private void SetDynamicLabels()
		{
			if (adjustedRoadJourney == null)
			{
				if(outward)
				{
					// Get car journey details for outward
					adjustedRoadJourney =
						TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
				}
				else
				{
					// Get car journey details for return
					adjustedRoadJourney =
						TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
				}
			}

			string miles = Global.tdResourceManager.GetString(
				"CarSummaryControl.labelMilesString", TDCultureInfo.CurrentUICulture);

			string hoursString = Global.tdResourceManager.GetString(
				"CarSummaryControl.labelHourString", TDCultureInfo.CurrentUICulture);

			string minutesString = Global.tdResourceManager.GetString(
				"CarSummaryControl.labelMinuteString", TDCultureInfo.CurrentUICulture);

			if( adjustedRoadJourney != null )
			{
				// Construct labels for Adjusted Route

				string distanceInMiles = ConvertMetresToMileage(adjustedRoadJourney.TotalDistance) + " " + miles;
				string distanceInKm = ConvertMetersToKm(adjustedRoadJourney.TotalDistance) + " " + "km";

				//switches the total distance between miles and kms
				if (roadUnits == RoadUnitsEnum.Miles)
				{
					labelAdjustedTotalDistanceNumber.Text = "<span class=\"milesshow\">" + distanceInMiles + "</span>" + "<span class=\"kmshide\">" + distanceInKm + "</span>";
				}
				else
				{
					labelAdjustedTotalDistanceNumber.Text = "<span class=\"mileshide\">" + distanceInMiles + "</span>" + "<span  class=\"kmsshow\">" + distanceInKm + "</span>";
				}
				
				
				double totalDuration = (double)adjustedRoadJourney.TotalDuration / 60.0;
				totalDuration = Round(totalDuration);
				long hours = (long)totalDuration / 60;
				long minutes = (long)totalDuration % 60;



				labelAdjustedTotalDurationNumber.Text = hours + " " + hoursString +
					" " + minutes + " " + minutesString;

				// Initialise label by clearing it (otherwise previous roads would be kept)
				labelAdjustedRoads.Text = String.Empty;

				string adjustedRoadName = String.Empty;

				// Used to temporarily hold all the road names so that
				// duplicates can be filtered out.
				ArrayList adjustedRoadNames = new ArrayList();

				// Get all the roads for the adjusted route
				foreach(RoadJourneyDetail roadJourneyDetails in adjustedRoadJourney.Details)
				{
					adjustedRoadName = GetRoadString(roadJourneyDetails);

					// Add the road name only if is not empty and it hasn't
					// already been added.
					if( adjustedRoadName != null &&
						adjustedRoadName.Length != 0 &&
						!adjustedRoadNames.Contains(adjustedRoadName) )
					{
						adjustedRoadNames.Add(adjustedRoadName);
					}
				}

				// Construct the adjusted roads label
				for(int i=0; i<adjustedRoadNames.Count; i++)
				{
					// Add the road name to the label
					labelAdjustedRoads.Text += adjustedRoadNames[i] + "; ";
				}
			}
			 
		}

		/// <summary>
		/// Return a formatted string from converting the given metres
		/// to a mileage
		/// </summary>
		/// <param name="metres">Metres to convert.</param>
		/// <returns>Milage string</returns>
		private string ConvertMetresToMileage(int metres)
		{
			// Retrieve the conversion factor from the Properties Service.
			double conversionFactor =
				Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

			double result = (double)metres / conversionFactor;

            string distanceFormat = GetDistanceFormat();

			return result.ToString(distanceFormat, TDCultureInfo.CurrentCulture.NumberFormat);
		}

        /// <summary>
        /// Return a formatted string by converting the given metres
        /// to km.
        /// </summary>
        /// <param name="metres"></param>
        /// <returns></returns>
		private string ConvertMetersToKm(int metres)
		{
			double result = (double)metres / 1000;

            string distanceFormat = GetDistanceFormat();

			// Return the result
			return result.ToString(distanceFormat, TDCultureInfo.CurrentUICulture.NumberFormat);
		}

        /// <summary>
        /// Method to return the text format of a distance to be converted to string.
        /// The distanceDecimalPlaces value is used to determine number decimal places
        /// </summary>
        /// <returns></returns>
        private string GetDistanceFormat()
        {
            string numberFormat = "F";

            // determine the format based on number of decimal places, 3 or less (this can be changed in future as its only set to avoid large numbers)
            if ((distanceDecimalPlaces >= 0) && (distanceDecimalPlaces <= 3))
            {
                numberFormat += distanceDecimalPlaces.ToString();
            }
            else
            {
                numberFormat += "1";
            }

            return numberFormat;
        }

		/// <summary>
		/// Returns a string of the road number for the given
		/// leg of the journey. If a road number does not exist
		/// then string.empty is returned.
		/// </summary>
		/// <param name="detail">Current section.</param>
		/// <returns>Road number or string.empty</returns>
		private string GetRoadString(RoadJourneyDetail detail)
		{
			string road = String.Empty;
			string roadNumber = detail.RoadNumber;

			if(roadNumber != null && roadNumber.Length != 0)
				return roadNumber;
			else
				return string.Empty;
		}

		#endregion

        #region Rounding method

        /// <summary>
        /// Rounds the given double to the nearest int.
        /// If double is 0.5, then rounds up.
        /// Using this instead of Math.Round because Math.Round
        /// ALWAYS returns the even number when rounding a .5 -
        /// this is not behaviour we want.
        /// </summary>
        /// <param name="valueToRound">Value to round.</param>
        /// <returns>Nearest integer</returns>
        private static int Round(double valueToRound)
        {
            // Get the decimal point
            double valueFloored = Math.Floor(valueToRound);
            double remain = valueToRound - valueFloored;

            if (remain >= 0.5)
                return (int)Math.Ceiling(valueToRound);
            else
                return (int)Math.Floor(valueToRound);
        }

        #endregion

        #region Javascript related methods

        /// <summary>
        /// Sets up the javascript action for the Units drop down list
        /// </summary>
        private void SetupUnitsDropdown()
        {
            string PageName = this.PageId.ToString();

            // Relies on the control having an "outward" or "return" in the parent control name
            // e.g. "carAllDetailsControlOutward_carSummaryControl_dropdownlistCarSummary"
            //      "carAllDetailsControlReturn_carSummaryControl_dropdownlistCarSummary"
            string outwardDropDownId = string.Empty;
            string returnDropDownId = string.Empty;
            string outwardHiddenInputId = string.Empty;
            string returnHiddenInputId = string.Empty;

            // Do the replacement
            if (outward)
            {
                outwardDropDownId = dropdownlistCarSummary.ClientID;
                returnDropDownId = outwardDropDownId.Replace("Outward", "Return");
            }
            else
            {
                returnDropDownId = dropdownlistCarSummary.ClientID;
                outwardDropDownId = returnDropDownId.Replace("Return", "Outward");
            }

            // Do the replacement
            if (outward)
            {
                outwardHiddenInputId = GetHiddenInputId;
                returnHiddenInputId = outwardHiddenInputId.Replace("Outward", "Return");
            }
            else
            {
                returnHiddenInputId = GetHiddenInputId;
                outwardHiddenInputId = returnHiddenInputId.Replace("Return", "Outward");
            }


            dropdownlistCarSummary.Action =
            "ChangeUnits('" +
                outwardDropDownId + "', '" +
                returnDropDownId + "', '" +
                outwardHiddenInputId + "', '" +
                returnHiddenInputId + "', '" +
                PageName + "', this)";
        }

        ///<summary>
        /// The EnableClientScript property of a scriptable control is set so that they
        /// output an action attribute when appropriate.
        /// If JavaScript is enabled then appropriate script blocks are added to the page.
        ///</summary>
        protected void EnableScriptableObjects()
        {
            bool javaScriptSupported = bool.Parse((string)Session[((TDPage)Page).Javascript_Support]);
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            if (javaScriptSupported)
            {
                dropdownlistCarSummary.Visible = true;
                lblUnits.Visible = true;
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                dropdownlistCarSummary.EnableClientScript = true;
                Page.ClientScript.RegisterStartupScript(typeof(CarSummaryControl), dropdownlistCarSummary.ScriptName, scriptRepository.GetScript(dropdownlistCarSummary.ScriptName, javaScriptDom));
            }
            else
            {
                dropdownlistCarSummary.Visible = false;
                lblUnits.Visible = false;

            }

        }

        ///<summary>
        /// The client may have changed things through JavaScript so need to update server state.  Used to set the units for road journeys.
        ///</summary>
        private void AlignServerWithClient()
        {

            string HiddenUnitsField = "";
            if (this.ClientID != null && this.ClientID != "carAllDetailsControlReturn_carSummaryControl")
            {
                HiddenUnitsField = this.ClientID;
            }
            else
            {
                HiddenUnitsField = "carAllDetailsControlReturn_carSummaryControl";
            }

            if (Request.Params[HiddenUnitsField + "_hdnUnitsState"] != null)
            {
                roadUnits = (RoadUnitsEnum)Enum.Parse(typeof(RoadUnitsEnum), Request.Params[HiddenUnitsField + "_hdnUnitsState"], true);
            }
            else
            {
                if (HiddenUnitsField == "carAllDetailsControlReturn_carSummaryControl")
                {
                    if (Request.Params["carAllDetailsControlOutward_carSummaryControl_hdnUnitsState"] != null)
                    {
                        roadUnits = (RoadUnitsEnum)Enum.Parse(typeof(RoadUnitsEnum), Request.Params["carAllDetailsControlOutward_carSummaryControl_hdnUnitsState"], true);
                    }
                }
                else
                {
                    if (Request.Params["carAllDetailsControlReturn_carSummaryControl_hdnUnitsState"] != null)
                    {
                        roadUnits = (RoadUnitsEnum)Enum.Parse(typeof(RoadUnitsEnum), Request.Params["carAllDetailsControlReturn_carSummaryControl_hdnUnitsState"], true);
                    }
                }
            }

            RoadUnitsEnum serverUnits = roadUnits;
            TDSessionManager.Current.InputPageState.Units = serverUnits;

            if (roadUnits == RoadUnitsEnum.Miles)
            {
                dropdownlistCarSummary.SelectedIndex = 0;
            }
            else
            {
                dropdownlistCarSummary.SelectedIndex = 1;
            }
        }

        #endregion
        
        /// <summary>
        /// Populates the Units selection drop down from DataServices and switches the language if required
        /// </summary>
        /// <returns></returns>
        private void SetUpResources()
        {
            //Populates the drop down list control with the allowed values from DataServices
            populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            populator.LoadListControl(DataServiceType.UnitsDrop, dropdownlistCarSummary, Global.tdResourceManager);

        }

        #endregion

        #region Web Form Designer generated code
        /// <summary>
		/// OnInit Method
		/// </summary>
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        #region Public properties

        public string UnitsState
		{
			get 
			{
				return TDSessionManager.Current.InputPageState.Units.ToString();
			}
		}

		public string GetHiddenInputId
		{
			get
			{
				return this.ClientID + "_hdnUnitsState";
			}
		}

		public RoadUnitsEnum RoadUnits
		{
			get { return roadUnits; }
			set { roadUnits = value; }
		}

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
