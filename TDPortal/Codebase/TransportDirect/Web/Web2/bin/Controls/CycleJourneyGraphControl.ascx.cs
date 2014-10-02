// *********************************************** 
// NAME                 : CycleJourneyGraphControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/06/2008
// DESCRIPTION          : Control to display a gradient graph of a Cycle journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CycleJourneyGraphControl.ascx.cs-arc  $ 
//
//   Rev 1.14   Mar 29 2010 11:54:00   mmodi
//Added an anchor link to allow Tabbing to the div in IE
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.13   Sep 25 2009 11:37:00   apatel
//code updated so in km units mode when switch to the table view from gradients view all of the values show in correct units(kms)  USD UK5647417
//Resolution for 5327: Miles and Km get muddled in Cycle Planner
//
//   Rev 1.12   Jan 19 2009 11:20:06   mmodi
//Updated to log a gradient profile event for the table view
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.11   Dec 09 2008 16:20:02   mmodi
//Updated chart colour setting to correct xhtml issue on page
//Resolution for 5206: Cycle Planner - Cycle journey details page is not xhtml valid for return journey
//
//   Rev 1.10   Oct 28 2008 17:05:56   mmodi
//Check for null geometry, which can occur with Via location journeys
//
//   Rev 1.9   Oct 28 2008 10:33:44   mmodi
//Added screen reader label
//
//   Rev 1.8   Oct 08 2008 09:44:10   mmodi
//Updated to call gradient profiler in table mode. Hide table button when in print mode
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.7   Sep 25 2008 11:28:26   mmodi
//Updated for table version of gradient profile
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Sep 17 2008 16:23:46   mmodi
//Updated for printer friendly chart 
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Sep 17 2008 13:00:46   mturner
//Changes to show error message when GradientProfiler call fails rather than an empty chart.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Sep 15 2008 10:54:16   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 08 2008 12:09:16   mmodi
//Updated to populate statistic labels
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 06 2008 14:50:54   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 01 2008 16:37:16   mmodi
//Added message when javascript disabled
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:26:58   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class CycleJourneyGraphControl : TDUserControl
    {
        #region Private members

        // Javascript files needed for the chart
        private const string scriptIECanvas = "IECanvas";
        private const string scriptCanvasChartPainter = "CanvasChartPainter";
        private const string scriptChart = "Chart";
        private const string scriptExcanvas = "Excanvas";
        private const string scriptGradientProfileChart = "GradientProfileChart";
        private const string scriptWZ_Jsgraphics = "WZ_Jsgraphics";
        private const string scriptWindowOnLoadManager = "WindowOnLoadManager";

        // Script name of the dynamically generated script
        private string scriptName = string.Empty; 
        
        // Appended to the script name, to ensure control can distinguish between outward and return
        string journeyDirection = string.Empty;

        // Flag to indicate chart or table mode
        private bool showChart = true;

        // Cycle journey to display - only used when switching to Table view
        private CycleJourney cycleJourney = null;

        private bool printable = false;
        private bool outward = false;

        private bool callPopulateTable = false;

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control. 
        /// CycleJourney is only needed for the table view.
        /// When in Chart view, the CycleJourney is passed in when GetChartData AJAX method is called
        /// </summary>
        public void Initialise(CycleJourney cycleJourney, bool outward)
        {
            this.cycleJourney = cycleJourney;
            this.outward = outward;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender
        
        /// <summary>
        /// Page_OnInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            buttonShowTable.Click += new EventHandler(buttonShowTable_Click);
            repeaterGradientProfileTable.ItemDataBound += new RepeaterItemEventHandler(repeaterGradientProfileTable_ItemDataBound);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            LoadResources();

            if (callPopulateTable)
            {
                PopulateTable();
            }

            SetControlVisibility();

            RegisterJavascriptFiles();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load the text and images
        /// </summary>
        private void LoadResources()
        {
            labelTitle.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelTitle.Text");
            labelWait.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelWait.Text");

            string mileCssClass = TDSessionManager.Current.InputPageState.Units == RoadUnitsEnum.Miles ? "milesshow" : "mileshide";
            string kmsCssClass = TDSessionManager.Current.InputPageState.Units == RoadUnitsEnum.Kms ? "kmsshow" : "kmshide";

            labelDistanceInMiles.Text =
                string.Format("<span class=\"{0}\">", mileCssClass) +
                GetResource("CyclePlanner.CycleJourneyGraphControl.labelDistanceInMiles.Text")
                + "</span>" + string.Format("<span class=\"{0}\">", kmsCssClass) +
                GetResource("CyclePlanner.CycleJourneyGraphControl.labelDistanceInKms.Text") + "</span>"
                ;

            labelHighestPoint.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelHighestPoint.Text");
            labelLowestPoint.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelLowestPoint.Text");
            labelTotalClimb.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelTotalClimb.Text");
            labelTotalDescent.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelTotalDescent.Text");

            imageWait.AlternateText = GetResource("CyclePlanner.CycleJourneyGraphControl.imageWait.AltText");
            imageWait.ImageUrl = GetResource("CyclePlanner.CycleJourneyGraphControl.imageWait.URL");

            // javascript disabled
            labelNoScript.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelNoScript.Text");

            // Gradient Profiler Web Service call failed
            labelError.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelError.Text");
            labelTableError.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelError.Text");

            if (showChart)
            {
                buttonShowTable.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.buttonShowTable.Text");
            }
            else
            {
                buttonShowTable.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.buttonShowGraph.Text");
            }

            labelSRGradientProfile.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelSRGradientProfile.Text");

            // Setup the name to allow a page skiplink to navigate to
            gradientProfileTableAnchor.Name = "gradientProfileTableAnchor" + (outward ? "Outward" : "Return");
        }

        /// <summary>
        /// Method which sets the visibility of the Chart or Table controls
        /// </summary>
        private void SetControlVisibility()
        {
            panelChart.Visible = showChart;
            panelTable.Visible = !showChart;

            if (printable)
                buttonShowTable.Visible = false;
        }

        #region Javascript
        /// <summary>
        /// Method which registers all the javascript files needed by the control to render a chart
        /// </summary>
        private void RegisterJavascriptFiles()
        {
            // Only want to add javascript files if displaying chart
            if (showChart)
            {
                // Register the scripts needed only if user has Javascript enabled
                TDPage thePage = this.Page as TDPage;

                if (thePage != null && thePage.IsJavascriptEnabled)
                {
                    // Get the global script repository
                    ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                    // Register the WindowOnLoadManager script, so we can add our necessary Chart functions to fire
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptWindowOnLoadManager, repository.GetScript(scriptWindowOnLoadManager, thePage.JavascriptDom));

                    // Scripts needed to draw the chart object (leave in this order as their are dependencies on functions in scripts)
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptIECanvas, repository.GetScript(scriptIECanvas, thePage.JavascriptDom));
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptChart, repository.GetScript(scriptChart, thePage.JavascriptDom));
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptCanvasChartPainter, repository.GetScript(scriptCanvasChartPainter, thePage.JavascriptDom));
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptExcanvas, repository.GetScript(scriptExcanvas, thePage.JavascriptDom));
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptWZ_Jsgraphics, repository.GetScript(scriptWZ_Jsgraphics, thePage.JavascriptDom));

                    // Script which draws the chart object (by calling methods in above scripts
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptGradientProfileChart, repository.GetScript(scriptGradientProfileChart, thePage.JavascriptDom));

                    // text used to identify the outward or return div's 
                    journeyDirection = (outward ? "Outward" : "Return");

                    // Generate a dynamic script for the outward/return chart to setup the items needed for the chart
                    // The functions created in here will be added to the WindowOnLoadManager
                    scriptName = "GenerateChartJS" + journeyDirection;
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptName, GenerateChartJS(), false);

                    // Generate a dynamic script to populate the statistic labels
                    // The function is called by the scriptGradientProfileChart registered above, therefore the 
                    // scriptname defined here must be the same as the function call in the scriptGradientProfileChart
                    scriptName = "PopulateStatisticLabels" + journeyDirection;
                    thePage.ClientScript.RegisterStartupScript(this.GetType(), scriptName, GenerateStatisticsLabelsJS(), false);
                }
            }
        }

        /// <summary>
        /// Method which generates the javascript to call the draw chart functions 
        /// </summary>
        /// <returns></returns>
        private string GenerateChartJS()
        {
            StringBuilder generateChartJS = new StringBuilder();

            generateChartJS.Append(" <script type=\"text/javascript\"> \n");

            // Function which calls the method to generate the chart, immediatly when the page loads
            generateChartJS.Append(" function " + scriptName + "() { \n\n");

            // Set up the divs needed
            generateChartJS.Append(" divChartWait" + journeyDirection + " = document.getElementById('" + divGradientProfileChartWait.ClientID + "'); \n");
            generateChartJS.Append(" divChartContainer" + journeyDirection + " = document.getElementById('" + divGradientProfileChartContainer.ClientID + "'); \n");
            generateChartJS.Append(" divChart" + journeyDirection + " = document.getElementById('" + divGradientProfileChart.ClientID + "'); \n");
            generateChartJS.Append(" divChartError" + journeyDirection + " = document.getElementById('" + divGradientProfileError.ClientID + "'); \n");
            generateChartJS.Append("  \n");

            // Set up the chart colour (getStyle method is defined in script Chart_GradientProfileChart)
            generateChartJS.Append(" colour" + journeyDirection + " = getStyle(document.getElementById('" + divChartColour.ClientID + "'), 'background-color'); \n");
            generateChartJS.Append("  \n");

            // Show/Hide appropriate div
            generateChartJS.Append(" ShowElement(divChartWait" + journeyDirection + ", true); \n");
            generateChartJS.Append(" ShowElement(divChartContainer" + journeyDirection + ", false); \n");
            generateChartJS.Append(" ShowElement(divChartError" + journeyDirection + ", false); \n");
            generateChartJS.Append("  \n");

            // Call method to draw the chart
            generateChartJS.Append(" GenerateChart( " + outward.ToString().ToLower() + " ); \n");
            generateChartJS.Append("  \n");
            generateChartJS.Append(" } \n");
            generateChartJS.Append("  \n");
            generateChartJS.Append("  \n");

            // This registers the above script in to the WindowOnloadManager to ensure it gets run on the window load
            generateChartJS.Append(" womAdd('" + scriptName + "()'); \n");
            generateChartJS.Append(" womOn(); \n\n");
            generateChartJS.Append(" </script> \n\n");

            return generateChartJS.ToString();
        }

        /// <summary>
        /// Method to generate the javascript to populate the statistic labels
        /// </summary>
        /// <returns></returns>
        private string GenerateStatisticsLabelsJS()
        {
            StringBuilder populateStatisticLabelsJS = new StringBuilder();

            // Open script tag
            populateStatisticLabelsJS.Append(" <script type=\"text/javascript\"> \n");

            // Function which calls the method to generate the chart, immediatly when the page loads
            populateStatisticLabelsJS.Append(" function " + scriptName + "(chartData) { \n\n");

            // Get the labels and populate with the statistics
            populateStatisticLabelsJS.Append(" document.getElementById('" + labelHighestPointValue.ClientID + "').appendChild(document.createTextNode(chartData.StatisticHighestPoint)); \n");
            populateStatisticLabelsJS.Append(" document.getElementById('" + labelLowestPointValue.ClientID + "').appendChild(document.createTextNode(chartData.StatisticLowestPoint)); \n");
            populateStatisticLabelsJS.Append(" document.getElementById('" + labelTotalClimbValue.ClientID + "').appendChild(document.createTextNode(chartData.StatisticTotalClimb)); \n");
            populateStatisticLabelsJS.Append(" document.getElementById('" + labelTotalDescentValue.ClientID + "').appendChild(document.createTextNode(chartData.StatisticTotalDescent)); \n");
            populateStatisticLabelsJS.Append("  \n");
            populateStatisticLabelsJS.Append(" } \n");
            populateStatisticLabelsJS.Append("  \n");

            // Close script tag
            populateStatisticLabelsJS.Append(" </script> \n\n");

            return populateStatisticLabelsJS.ToString();
        }

        #endregion

        /// <summary>
        /// Method which populates the table with chart data
        /// </summary>
        private void PopulateTable()
        {

            // Display error message by default, unable to display gradient profile table
            repeaterGradientProfileTable.Visible = false;
            labelTableError.Visible = true;

            if (cycleJourney != null)
            {
                CJPSessionInfo sessionInfo = TDSessionManager.Current.GetSessionInformation();

                CallGradientProfiler(cycleJourney, sessionInfo);

                if ((cycleJourney.GradientProfileResult != null) && (cycleJourney.GradientProfileResult.IsValid))
                {
                    DateTime gradientProfileStarted = DateTime.Now;

                    #region Get data values

                    // statistic values
                    int highestPoint;
                    int lowestPoint;
                    int totalClimb;
                    int totalDescent;

                    // get the values
                    int[] chartHeight = GetGradientProfileData(cycleJourney, out highestPoint, out lowestPoint, out totalClimb, out totalDescent);

                    ArrayList chartDataValues = new ArrayList();

                    double distance = 0;
                    double distanceInterval = Convert.ToDouble(Properties.Current["GradientProfiler.PlannerControl.Resolution.Metres"]);
                    string distanceKm = string.Empty;
                    string distanceMiles = string.Empty;

                    string mileCssClass = TDSessionManager.Current.InputPageState.Units == RoadUnitsEnum.Miles ? "milesshow" : "mileshide";
                    string kmsCssClass = TDSessionManager.Current.InputPageState.Units == RoadUnitsEnum.Kms ? "kmsshow" : "kmshide";

                    foreach (int height in chartHeight)
                    {
                        string[] distanceHeight = new string[2];

                        distanceHeight[0] =
                            string.Format("<span class=\"{0}\">", mileCssClass) + ConvertMetresToMileage(distance) + "</span>"
                          + string.Format("<span class=\"{0}\">", kmsCssClass) + ConvertMetresToKm(distance) + "</span>";

                        distanceHeight[1] = height.ToString();

                        chartDataValues.Add(distanceHeight);

                        distance += distanceInterval;
                    }


                    #endregion

                    // Bind to the repeater
                    repeaterGradientProfileTable.DataSource = chartDataValues;
                    repeaterGradientProfileTable.DataBind();

                    repeaterGradientProfileTable.Visible = true;
                    labelTableError.Visible = false;

                    // Log an event
                    if (!printable)
                    {
                        // As Populate table is only ever called when displaying the table, the category is table
                        CyclePlannerHelper.LogGradientProfileEvent(GradientProfileEventDisplayCategory.Table,
                                                                   gradientProfileStarted,
                                                                   TDSessionManager.Current.Authenticated,
                                                                   TDSessionManager.Current.Session.SessionID);
                    }
                }
            } // end if cycleJourney
        }

        /// <summary>
        /// Return a formatted string by converting the given metres
        /// to a mileage (only 1 decimal place will be returned in the string).
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        /// <returns>Formatted string</returns>
        private string ConvertMetresToMileage(double metres)
        {
            string resultMileage = MeasurementConversion.Convert(metres, ConversionType.MetresToMileage);
            if (string.IsNullOrEmpty(resultMileage))
                resultMileage = "0";

            double result = Convert.ToDouble(resultMileage);

            return result.ToString("F2", TDCultureInfo.CurrentUICulture.NumberFormat);
        }

        /// <summary>
        /// Return a formatted string by converting the given metres
        /// to a mileage (only 1 decimal place will be returned in the string).
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        /// <returns>Formatted string</returns>
        private string ConvertMetresToKm(double metres)
        {
            double result = metres / 1000;

            return result.ToString("F2", TDCultureInfo.CurrentUICulture.NumberFormat);
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for the button show table click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowTable_Click(object sender, EventArgs e)
        {
            if (panelChart.Visible)
            {
                showChart = false;

                panelChart.Visible = false;
                panelTable.Visible = true;

                // Generate the gradient profile table by setting the flag. PopulateTable method will be called onPreRender
                callPopulateTable = true;
            }
            else
            {
                showChart = true;

                panelChart.Visible = true;
                panelTable.Visible = false;

                callPopulateTable = false;
            }
        }

        /// <summary>
        /// Event handler for the repeater item databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repeaterGradientProfileTable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                string mileCssClass = TDSessionManager.Current.InputPageState.Units == RoadUnitsEnum.Miles ? "milesshow" : "mileshide";
                string kmsCssClass = TDSessionManager.Current.InputPageState.Units == RoadUnitsEnum.Kms ? "kmsshow" : "kmshide";

                // Populate the table headings
                Label labelTableHeaderDistance = (Label)e.Item.FindControl("labelTableHeaderDistance");
                if (labelTableHeaderDistance != null)
                    labelTableHeaderDistance.Text =
                    string.Format("<span class=\"{0}\">", mileCssClass) +
                    GetResource("CyclePlanner.CycleJourneyGraphControl.labelTableHeaderDistanceInMiles.Text")
                    + "</span>" + string.Format("<span class=\"{0}\">", kmsCssClass) +
                    GetResource("CyclePlanner.CycleJourneyGraphControl.labelTableHeaderDistanceInKms.Text") + "</span>"
                ;

                Label labelTableHeaderHeight = (Label)e.Item.FindControl("labelTableHeaderHeight");
                if (labelTableHeaderHeight != null)
                    labelTableHeaderHeight.Text = GetResource("CyclePlanner.CycleJourneyGraphControl.labelTableHeaderHeight.Text");
            }
        }


        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Printable mode of control
        /// </summary>
        public bool Printable
        {
            get { return printable; }
            set { printable = value; }
        }

        /// <summary>
        /// Read/write. Display the control in Graph or Table mode
        /// </summary>
        public bool ShowChart
        {
            get { return showChart; }
            set { showChart = value; }
        }

        #endregion

        #region AJAX Private Helper methods

        /// <summary>
        /// Genereates a TDGradientProfileRequest for the supplied CycleJourney
        /// </summary>
        /// <param name="cycleJourney"></param>
        /// <returns></returns>
        private static ITDGradientProfileRequest CreateGradientProfileRequest(CycleJourney cycleJourney)
        {
            ITDGradientProfileRequest tdGradientProfileRequest = new TDGradientProfileRequest();

            #region Generate the array of polylines needed for the request

            int polylineID = 0;
            int polylineGroupID = 0;
            
            // Dictionary to hold all of our polyline groups
            Dictionary<int, TDPolyline[]> polylineGroups = new Dictionary<int,TDPolyline[]>();

            // Temp array used to group together polylines
            ArrayList group = new ArrayList();

            foreach (CycleJourneyDetail detail in cycleJourney.Details)
            {
                // Get the geometry values for this detail
                Dictionary<int, OSGridReference[]> geometrys = detail.Geometry;

                if ((geometrys != null) && (geometrys.Count > 0))
                {
                    polylineGroupID++;

                    group.Clear(); // reset the current group

                    // Each geometry gets its own Polyline, to enable more accurate gradient profiles 
                    foreach (KeyValuePair<int, OSGridReference[]> geometry in geometrys)
                    {
                        polylineID++;
                        
                        // Get the interpolateGradient flag from the complement dictionary
                        bool interpolateGradient = detail.InterpolateGradient[geometry.Key];

                        // Create and add the polyline to the current group
                        group.Add(new TDPolyline(polylineID, geometry.Value, interpolateGradient));
                    }

                    // Add the current group to the dictionary
                    polylineGroups.Add(polylineGroupID, (TDPolyline[])group.ToArray(typeof(TDPolyline)));
                }
            }

            #endregion

            tdGradientProfileRequest.TDPolylines = polylineGroups;
            
            return tdGradientProfileRequest;
        }      

        /// <summary>
        /// Method which returns the gradient profile data for the current journey
        /// </summary>
        /// <returns></returns>
        private static int[] GetGradientProfileData(CycleJourney cycleJourney, out int highestPoint,
            out int lowestPoint, out int totalClimb, out int totalDescent)
        {
            highestPoint = -5000; // there are some locations in the uk below sea level
            lowestPoint = 5000;
            totalClimb = 0;
            totalDescent = 0;
            int previousHeight = 0; // used to track climb/descent totals

            ArrayList gradientData = new ArrayList();

            if ((cycleJourney.GradientProfileResult != null) && (cycleJourney.GradientProfileResult.IsValid))
            {
                Dictionary<int, TDHeightPoint[]> tdHeightPoints = cycleJourney.GradientProfileResult.TDHeightPoints;

                // Add all gradient height points to an int array
                foreach (KeyValuePair<int, TDHeightPoint[]> tdHeightPointsKey in tdHeightPoints)
                {
                    if ((tdHeightPointsKey.Value != null) && (tdHeightPointsKey.Value.Length > 0))
                    {
                        foreach (TDHeightPoint height in tdHeightPointsKey.Value)
                        {
                            gradientData.Add(height.Height);

                            // set statistic values
                            if (height.Height > highestPoint)
                                highestPoint = height.Height;

                            if (height.Height < lowestPoint)
                                lowestPoint = height.Height;
                        }
                    }
                }
            }
            else
            {
                // No or invalid gradient data, return empty array
                gradientData.Add( (int)0 );
            }

            // All gradient data points have been collected.
            int[] gradientDataInt = (int[])gradientData.ToArray(typeof(int));

            // Set the previous height to be the first height in the list.
            // Total climb and descent is based from this starting height
            previousHeight = gradientDataInt[0];

            // Calculate total climb and total descent
            foreach (int height in gradientDataInt)
            {
                if (height > previousHeight)
                    totalClimb += height - previousHeight;

                if (height < previousHeight)
                    totalDescent += previousHeight - height;

                previousHeight = height;
            }

            return gradientDataInt;
        }

        #endregion

        #region AJAX Public methods - does the work when called by WebMethod in parent Page
        /// <summary>
        /// Method which invokes the call to the Gradient Profiler for the supplied CycleJourney and SessionInfo
        /// </summary>
        /// <returns>Returns the CycleJourney populated with the GradientProfile request and result</returns>
        public static CycleJourney CallGradientProfiler(CycleJourney cycleJourney, CJPSessionInfo sessionInfo)
        {
            if ((cycleJourney.GradientProfileResult != null) && (cycleJourney.GradientProfileResult.IsValid))
            {
                // The GradientProfiler result for this cycle journey exists and is valid,
                // so no need to make the call
            }
            else
            {
                // Get a GradientProfilerManager from the service discovery
                IGradientProfilerManager gradientProfilerManager = (IGradientProfilerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.GradientProfilerManager];

                // Create a new request
                ITDGradientProfileRequest tdGradientProfilerRequest = CreateGradientProfileRequest(cycleJourney);

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;
                string lang = Thread.CurrentThread.CurrentUICulture.ToString();

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "GradientProfiler - Call to GradientProfilerManager has started. SessionId = " + sessionInfo.SessionId));
                }

                // Make the call
                ITDGradientProfileResult tdGradientProfilerResult = gradientProfilerManager.CallGradientProfiler(
                        tdGradientProfilerRequest,
                        sessionInfo.SessionId,
                        sessionInfo.UserType,
                        referenceTransation,
                        sessionInfo.IsLoggedOn,
                        lang);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "GradientProfiler - Call to GradientProfilerManager has completed. SessionId = " + sessionInfo.SessionId));
                }

                // Add the request and result objects to the CycleJourney for future use
                cycleJourney.GradientProfileRequest = (TDGradientProfileRequest)tdGradientProfilerRequest;
                cycleJourney.GradientProfileResult = (TDGradientProfileResult)tdGradientProfilerResult;
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "GradientProfiler - Cycle journey has been populated with GradientProfileResult object. SessionId = " + sessionInfo.SessionId));
            }

            // The cycle journey will now have the gradient profile added to it
            return cycleJourney;
        }

        /// <summary>
        /// Method which is called by the parent Page AJAX webmethod.
        /// Constructs a chart data object to return back to the Client from the CycleJourney supplied. 
        /// </summary>
        /// <returns>Returns a TDChartData object populated with the Gradient Profile data</returns>
        public static TDChartData GetChartData(CycleJourney cycleJourney, CJPSessionInfo sessionInfo)
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "GradientProfiler - Chart data object being constructed for cycle journey. SessionId = " + sessionInfo.SessionId));
            }

            if ((cycleJourney.GradientProfileResult != null) && (cycleJourney.GradientProfileResult.IsValid))
            {
                TDChartData chartData = new TDChartData();

                #region Get data values

                // statistic values
                int highestPoint;
                int lowestPoint;
                int totalClimb;
                int totalDescent;
                string metres = "m";

                // set the values
                chartData.Values = GetGradientProfileData(cycleJourney, out highestPoint, out lowestPoint, out totalClimb, out totalDescent);

                // set the values "series" name
                chartData.Series = Properties.Current["GradientProfiler.Chart.Data.SeriesName"];

                // set the show legend flag
                chartData.ShowLegend = bool.Parse(Properties.Current["GradientProfiler.Chart.ShowLegend"]);

                // Populate the statistics in the chartData object sent back to user
                chartData.StatisticHighestPoint = highestPoint.ToString() + metres;
                chartData.StatisticLowestPoint = lowestPoint.ToString() + metres;
                chartData.StatisticTotalClimb = totalClimb.ToString() + metres;
                chartData.StatisticTotalDescent = totalDescent.ToString() + metres;

                #endregion

                #region Set y axis scale values
                // Calculate the vertical axis values
                chartData.GridDensityVertical = 5;
                chartData.VerticalValueMin = Convert.ToInt32(Properties.Current["GradientProfiler.Chart.ScaleValue.Metres.Min.Default"]);
                chartData.VerticalValueMax = Convert.ToInt32(Properties.Current["GradientProfiler.Chart.ScaleValue.Metres.Max.Default"]);

                // If the highest point is greater than our default height, round up to the next hundred value
                if (highestPoint > chartData.VerticalValueMax)
                {
                    chartData.VerticalValueMax = Convert.ToInt32(System.Math.Ceiling(Convert.ToDecimal(highestPoint) / 100) * 100);
                }

                // If the lowest point is less than our default lowest height, round down to the next hundred value
                if (lowestPoint < chartData.VerticalValueMin)
                {
                    chartData.VerticalValueMin = Convert.ToInt32(System.Math.Floor(Convert.ToDecimal(lowestPoint) / 100) * 100);
                }

                // Check we've not exceeded the configured max/min values
                if (chartData.VerticalValueMax > Convert.ToInt32(Properties.Current["GradientProfiler.Chart.ScaleValue.Metres.Max"]))
                {
                    chartData.VerticalValueMax = Convert.ToInt32(Properties.Current["GradientProfiler.Chart.ScaleValue.Metres.Max"]);
                }

                if (chartData.VerticalValueMin < Convert.ToInt32(Properties.Current["GradientProfiler.Chart.ScaleValue.Metres.Min"]))
                {
                    chartData.VerticalValueMin = Convert.ToInt32(Properties.Current["GradientProfiler.Chart.ScaleValue.Metres.Min"]);
                }

                #endregion

                #region Set x axis labels
                // Work out the labels shown on the horizontal bar
                chartData.GridDensityHorizontal = 5;
                decimal distanceMiles1 = decimal.Round(Convert.ToDecimal(MeasurementConversion.Convert((double)cycleJourney.TotalDistance * 0.25, ConversionType.MetresToMileage)), 1);
                decimal distanceMiles2 = decimal.Round(Convert.ToDecimal(MeasurementConversion.Convert((double)cycleJourney.TotalDistance * 0.50, ConversionType.MetresToMileage)), 1);
                decimal distanceMiles3 = decimal.Round(Convert.ToDecimal(MeasurementConversion.Convert((double)cycleJourney.TotalDistance * 0.75, ConversionType.MetresToMileage)), 1);
                decimal distanceMiles4 = decimal.Round(Convert.ToDecimal(MeasurementConversion.Convert((double)cycleJourney.TotalDistance, ConversionType.MetresToMileage)), 1);

                decimal distanceKm1 = decimal.Round(Convert.ToDecimal((double)cycleJourney.TotalDistance * 0.25) / 1000, 1);
                decimal distanceKm2 = decimal.Round(Convert.ToDecimal((double)cycleJourney.TotalDistance * 0.50) / 1000, 1);
                decimal distanceKm3 = decimal.Round(Convert.ToDecimal((double)cycleJourney.TotalDistance * 0.75) / 1000, 1);
                decimal distanceKm4 = decimal.Round(Convert.ToDecimal((double)cycleJourney.TotalDistance) / 1000, 1);

                chartData.HorizontalLabels = new string[] { 
                    "0.0",
                    distanceMiles1.ToString(),                  
                    distanceMiles2.ToString(),  
                    distanceMiles3.ToString(), 
                    distanceMiles4.ToString(),
                    "0.0",
                    distanceKm1.ToString(),
                    distanceKm2.ToString(),
                    distanceKm3.ToString(),
                    distanceKm4.ToString()
                };

                #endregion

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "GradientProfiler - Chart data object for cycle journey has been created. SessionId = " + sessionInfo.SessionId));
                }
                                    
                return chartData;
            }
            else
            {
                string message = "GradientProfiler - GradientProfilerResult is invalid for cycle journey, unable to construct chart data. SessionId = " + sessionInfo.SessionId;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, message));

                throw new TDException(message, true, TDExceptionIdentifier.GRInvalidGradientProfileResult);
            }
        }

        #endregion

        #region AJAX Public objects

        /// <summary>
        /// Class which contrains the chart data object to sent to the client by the AJAX method
        /// </summary>
        public class TDChartData
        {
            // Name of the data series
            public string Series = "Gradient";

            // Show/hide the chart legend
            public bool ShowLegend = false;

            // Vertical axis Min value
            public int VerticalValueMin = 0;

            // Vertical axis Max value
            public int VerticalValueMax = 500;

            // Number of grid lines shown on the vertical axis
            public int GridDensityVertical = 5;

            // Number of grid lines shown on the horizontal axis
            public int GridDensityHorizontal = 5;

            // Values shown on the horizontal axis
            public string[] HorizontalLabels = new string[] { "0", "0", "0", "0", "0" };

            // Values for the chart
            public int[] Values = new int[] { 0, 0, 0, 0, 0 };

            // Statistic for the values array - highest point
            public string StatisticHighestPoint = "0m";

            // Statistic for the values array - lowest point
            public string StatisticLowestPoint = "0m";

            // Statistic for the values array - total climb
            public string StatisticTotalClimb = "0m";

            // Statistic for the values array - total descent
            public string StatisticTotalDescent = "0m";

            // Flag to indicate an error occured when calling the GradientProfile Web Service
            public bool ErrorOccured = false;
        }

        #endregion
    }
}