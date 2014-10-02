// *********************************************** 
// NAME                 : MapControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 11/09/2003 
// DESCRIPTION  : Map Web user control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Oct 13 2008 16:44:18   build
//Automatically merged from branch for stream5014
//
//   Rev 1.4.1.0   Sep 16 2008 10:57:26   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Jul 24 2008 13:45:48   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.3   Apr 15 2008 11:40:58   mmodi
//Removed unused variable
//
//   Rev 1.2   Mar 31 2008 13:21:54   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Jan 31 2008 23:48:00 apatel
//  added zoom-in and zoom-out buttons and added a property to turn them on and off.
//
//   Rev 1.0   Nov 08 2007 13:16:18   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.1.0   Jan 10 2006 15:26:12   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16   Aug 18 2005 11:29:40   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.15.1.0   Jul 07 2005 09:40:08   jgeorge
//Added PreRender handler to hide tooltip when map is displaying travel incidents
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.15   Dec 17 2003 16:46:02   kcheung
//Removed most of the code and inserted into TDMap so that correct map logging can be performed.
//Resolution for 551: Map Events are being logged with incorrect submission time
//
//   Rev 1.14   Dec 11 2003 10:21:34   kcheung
//Journey Planner Location Map Del 5.1 Update
//
//   Rev 1.13   Nov 26 2003 14:59:22   PNorell
//Added properties HasStopsChanged and HasPointXChanged
//
//   Rev 1.12   Nov 17 2003 15:21:54   kcheung
//Updated cultureinfo for fxcop.  Added lang="en".
//
//   Rev 1.11   Nov 06 2003 12:26:10   kcheung
//Fixed alternate text for map and summary map
//
//   Rev 1.10   Nov 05 2003 14:13:54   kcheung
//Fixed commenting 
//
//   Rev 1.9   Oct 15 2003 11:38:04   kcheung
//Removed hard-coded alt tags - they are now ready from Langstrings
//
//   Rev 1.8   Oct 14 2003 17:34:18   kcheung
//Fixed bug caused by Peter
//
//   Rev 1.7   Oct 14 2003 11:29:36   PNorell
//Updated Pointx flag to correspond to the latest IF.
//
//   Rev 1.6   Oct 14 2003 10:36:30   PNorell
//Arranged for the map logging to work according to the spec.
//
//   Rev 1.5   Oct 09 2003 19:14:22   kcheung
//Moved property load onto the OnInit otherwise null exception is thrown by the mapping component
//
//   Rev 1.4   Oct 09 2003 18:29:56   kcheung
//Updated to ensure that Map does not display any initial icons and the Map ServerName and ServiceName properties are loaded from Properties service.
//
//   Rev 1.3   Oct 02 2003 16:59:46   kcheung
//Updated to make pan buttons work.
//
//   Rev 1.2   Sep 24 2003 15:03:26   passuied
//last working version
//
//   Rev 1.1   Sep 24 2003 11:52:22   asinclair
//Applied the BBCT HTML code
//
//   Rev 1.0   Sep 11 2003 15:00:02   passuied
//Initial Revision

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///	Map Web user control
	/// </summary>
	public partial  class MapControl : TDUserControl
	{
		#region Web controls


		#endregion

		#region Local variables

		private DateTime operationStartedDateTime;
        

		#endregion

        #region Definitions for zoom control
        private const string RES_URL_SCALESELECTED = "MapToolsControl.imageScaleSelected";
        private const string RES_URL_SCALEUNSELECTED = "MapToolsControl.imageScale";
        private const string RES_ALT_SCALE = "MapToolsControl.imageButtonZoom{0}.AlternateText";

        private const string RES_TXT_ZOOMPLUS = "MapToolsControl.buttonMapPlus.Text";
        private const string RES_TOOLTIP_ZOOMPLUS = "MapToolsControl.buttonZoomIn.ToolTip";
        private const string RES_TXT_ZOOMMINUS = "MapToolsControl.buttonMapMinus.Text";
        private const string RES_TOOLTIP_ZOOMMINUS = "MapToolsControl.buttonZoomOut.ToolTip";

        private const string RES_TOOLTIP_ZOOMIN = "MapToolsControl.buttonZoomIn.ToolTip";
        private const string RES_TXT_ZOOMIN = "MapToolsControl.buttonZoomIn.Text";
        private const string RES_TXT_ZOOMOUT = "MapToolsControl.buttonZoomOut.Text";
        private const string RES_TOOLTIP_ZOOMOUT = "MapToolsControl.buttonZoomOut.ToolTip";

        private const string RES_TXT_INST_QUERY = "MapToolsControl.labelZoomControlInstructions.Query.Text";
        private const string RES_TXT_INST_ZOOM = "MapToolsControl.labelZoomControlInstructions.ZoomIn.Text";

        private const string DEFAULT_ZOOM_DEFINITION = "MappingComponent";

        private readonly string[] zoomDefinitions = {
														"Web.{0}.ZoomLevelOne",
														"Web.{0}.ZoomLevelTwo",
														"Web.{0}.ZoomLevelThree",
														"Web.{0}.ZoomLevelFour",
														"Web.{0}.ZoomLevelFive",
														"Web.{0}.ZoomLevelSix",
														"Web.{0}.ZoomLevelSeven",
														"Web.{0}.ZoomLevelEight",
														"Web.{0}.ZoomLevelNine",
														"Web.{0}.ZoomLevelTen",
														"Web.{0}.ZoomLevelEleven",
														"Web.{0}.ZoomLevelTwelve",
														"Web.{0}.ZoomLevelThirteen"
													};
        private const string UNSELECTED = "U";
        private const string SELECTED = "S";
        private const string SKIP = "Z";

        private bool displayLowZoomLevelBox = true;

        private ImageButton[] imbCache;
        private string zoomSetting = DEFAULT_ZOOM_DEFINITION;

        private bool isZoomControlVisible = true;

        private bool isOverviewLinkVisible = false;

        #endregion

		#region Constructor

		/// <summary>
		/// Page Load method. Sets-up the control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //  CCN 0427 Set the image urls for the pan buttons
            this.PanNorthWestButton.ImageUrl = GetResource("MapToolsControl.PanNorthWestButton");
            this.PanNorthButton.ImageUrl = GetResource("MapToolsControl.PanNorthButton");
            this.PanNorthEastButton.ImageUrl = GetResource("MapToolsControl.PanNorthEastButton");
            this.PanWestButton.ImageUrl = GetResource("MapToolsControl.PanWestButton");
            this.PanEastButton.ImageUrl = GetResource("MapToolsControl.PanEastButton");
            this.PanSouthWestButton.ImageUrl = GetResource("MapToolsControl.PanSouthWestButton");
            this.PanSouthButton.ImageUrl = GetResource("MapToolsControl.PanSouthButton");
            this.PanSouthEastButton.ImageUrl = GetResource("MapToolsControl.PanSouthEastButton");

            // CCN 0427 setting the text for the mapoverview link
            hyperLinkOverviewMap.Text = GetResource("MapControl.hyperLinkMapKey.Text");
            

			// Set the alternate text for all the pan buttons
			PanSouthButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanSouth.AlternateText", TDCultureInfo.CurrentUICulture);
	
			PanSouthEastButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanSouthEast.AlternateText", TDCultureInfo.CurrentUICulture);

			PanSouthWestButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanSouthWest.AlternateText", TDCultureInfo.CurrentUICulture);
	
			PanEastButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanEast.AlternateText", TDCultureInfo.CurrentUICulture);
	
			PanWestButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanWest.AlternateText", TDCultureInfo.CurrentUICulture);
		
			PanNorthEastButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanNorthEast.AlternateText", TDCultureInfo.CurrentUICulture);
		
			PanNorthButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanNorth.AlternateText", TDCultureInfo.CurrentUICulture);
	
			PanNorthWestButton.AlternateText = Global.tdResourceManager.GetString(
				"MapControl.PanNorthWest.AlternateText", TDCultureInfo.CurrentUICulture);
			
            //CCN 0427 Changes to map - Setup the map legend inside map at top left corner
            hyperLinkMapKey.Text = string.Format(Global.tdResourceManager.GetString
               ("JourneyMapControl.hyperLinkMapKey.Text", TDCultureInfo.CurrentUICulture),
               Global.tdResourceManager.GetString("ExternalLinks.OpensNewWindowText", TDCultureInfo.CurrentUICulture));
           
            hyperLinkMapKey.Target = "_blank";

            //CCN 0427 - Set the URL for the map legend at the current scale if one exists
            MapHelper helper = new MapHelper();
            hyperLinkMapKey.NavigateUrl = helper.getLegandUrl(theMap.Scale);
            hyperLinkMapKey.Visible = !(hyperLinkMapKey.NavigateUrl == string.Empty);

            //CCN 0427 Changed made to map control to have zoom control on map
            if (IsZoomControlVisible)
            {
                InitializeZoomControl();
               
            }
		}

        

        

		/// <summary>
		/// Prerender handler. Sets the alternate text for the image
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
            hyperLinkOverviewMap.Visible = isOverviewLinkVisible;

            

			if (theMap.RoadIncidentsVisible || theMap.PublicIncidentsVisible)
				theMap.AlternateText = string.Empty;
			else
				theMap.AlternateText = Global.tdResourceManager.GetString("MapControl.imageMap.AlternateText", TDCultureInfo.CurrentUICulture);

		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			theMap.ServiceName = Properties.Current["InteractiveMapping.Map.ServiceName"];
			theMap.ServerName = Properties.Current["InteractiveMapping.Map.ServerName"];
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
           

			this.PanNorthWestButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanNorthWestButton_Click);
			this.PanNorthButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanNorthButton_Click);
			this.PanNorthEastButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanNorthEastButton_Click);
			this.PanWestButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanWestButton_Click);
			this.PanEastButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanEastButton_Click);
			this.PanSouthWestButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanSouthWestButton_Click);
			this.PanSouthButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanSouthButton_Click);
			this.PanSouthEastButton.Click += new System.Web.UI.ImageClickEventHandler(this.PanSouthEastButton_Click);
			this.PreRender +=new EventHandler(Page_PreRender);
            

		}

       

        
		#endregion

		#region Pan event handling

		/// <summary>
		/// Handler for the Pan North West Button.
		/// </summary>
		private void PanNorthWestButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.NorthWest);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the Pan North Button.
		/// </summary>
		private void PanNorthButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.North);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the Pan North East Button.
		/// </summary>
		private void PanNorthEastButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.NorthEast);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the Pan East Button.
		/// </summary>
		private void PanEastButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.East);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the South East Button.
		/// </summary>
		private void PanSouthEastButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.SouthEast);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the Pan South Button.
		/// </summary>
		private void PanSouthButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.South);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the Pan South West Button.
		/// </summary>
		private void PanSouthWestButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.SouthWest);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}

		/// <summary>
		/// Handler for the West Button.
		/// </summary>
		private void PanWestButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.Map.Direction.West);
			MapLogging.Write(MapEventCommandCategory.MapPan, theMap.Scale, operationStartedDateTime);
		}
		#endregion

		#region Public properties

		/// <summary>
		/// Get property - returns the mapping component.
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.TDMap Map
		{
			get
			{
				return theMap;
			}
		}

        /// <summary>
        /// Get/Set visiblity of the zoom control in mapControl
        /// </summary>
        public bool IsZoomControlVisible
        {
            get { return isZoomControlVisible; }
            set { isZoomControlVisible = value; }
        }

        /// <summary>
        /// Get/Set property to enable/disable the visibility of the div box round the zoom panel lower zoom levels.
        /// property added to mapcontrol for zoom control - CCN 0427
        /// </summary>
        public bool DisplayLowZoomLevelBox
        {
            get
            {
                return displayLowZoomLevelBox;
            }
            set
            {
                displayLowZoomLevelBox = value;
            }
        }

        /// <summary>
        /// Get/Set property to enable/disable the visibility of the Overview map - CCN 0427
        /// </summary>
        public bool OverviewLinkVisible
        {
            get { return isOverviewLinkVisible; }
            set { isOverviewLinkVisible = value; }
        }

        public TDButton OverviewMapLink
        {
            get { return hyperLinkOverviewMap; }
        }

		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the start time of the operation.
		/// </summary>
		private void SetStartTime()
		{
			operationStartedDateTime = DateTime.Now;
		}

        

        

		#endregion

        #region Zoom Control private methods and events
        /// <summary>
        /// this method initializes zoom control buttons in the map and binds zoom-in
        /// and zoom-out events to it
        /// </summary>
        private void InitializeZoomControl()
        {
            panelZoomButtons.Controls.Clear();        //CWH

            HtmlGenericControl zoomPlusDiv = new HtmlGenericControl("div");
            zoomPlusDiv.Attributes.Add("class", "zoombuttons");

            TDButton zoomPlus = new TDButton();
            zoomPlus.ID = "ZoomPlus";
            zoomPlus.CommandName = SKIP;
            zoomPlus.Click += new EventHandler(ZoomPlus);
            zoomPlus.Text = GetResource(RES_TXT_ZOOMPLUS);
            zoomPlus.ToolTip = GetResource(RES_TOOLTIP_ZOOMPLUS);

            zoomPlusDiv.Controls.Add(zoomPlus);


            panelZoomButtons.Controls.Add(zoomPlusDiv);

            imbCache = new ImageButton[zoomDefinitions.Length];

            //Create div border
            HtmlGenericControl borderDiv = new HtmlGenericControl("div");
            borderDiv.Attributes.Add("class", "mapzoomiconshighlight");
            if (displayLowZoomLevelBox)
            {
                panelZoomButtons.Controls.Add(borderDiv);
            }

            //Create the non div border
            HtmlGenericControl nonBorderDiv = new HtmlGenericControl("div");
            nonBorderDiv.Attributes.Add("class", "mapzoomicons");
            panelZoomButtons.Controls.Add(nonBorderDiv);


            for (int i = 0; i < zoomDefinitions.Length; i++)
            {
                string zoomDef = string.Format(zoomDefinitions[i], zoomSetting);
                ImageButton im = new ImageButton();
                im.ImageUrl = GetResource(RES_URL_SCALEUNSELECTED);
                im.ID = zoomDef;
                im.CommandName = Properties.Current[zoomDef];
                im.CommandArgument = UNSELECTED;
                im.AlternateText = GetResource(string.Format(RES_ALT_SCALE, (i + 1)));
                im.Click += new System.Web.UI.ImageClickEventHandler(ZoomByLevel);
                if (displayLowZoomLevelBox && i <= 4)
                {
                    borderDiv.Controls.Add(im);

                }
                else
                {
                    nonBorderDiv.Controls.Add(im);

                }
                // For caching purposes
                imbCache[i] = im;
            }


            TDButton zoomMinus = new TDButton();
            zoomMinus.ID = "ZoomMinus";
            zoomMinus.CommandName = SKIP;
            zoomMinus.Click += new EventHandler(ZoomMinus);
            zoomMinus.Text = GetResource(RES_TXT_ZOOMMINUS);
            zoomMinus.ToolTip = GetResource(RES_TOOLTIP_ZOOMMINUS);


            HtmlGenericControl zoomMinusDiv = new HtmlGenericControl("div");
            zoomMinusDiv.Attributes.Add("class", "zoombuttons");

            zoomMinusDiv.Controls.Add(zoomMinus);

            panelZoomButtons.Controls.Add(zoomMinusDiv);
        }

        /// <summary>
        /// Conveince method for turning an object into an int in a FXCop friendly way
        /// </summary>
        /// <param name="val">The value as object</param>
        /// <returns>The value as int</returns>
        private int ConvertToInt(object val)
        {
            // Presume it is string
            return Convert.ToInt32(val, TDCultureInfo.CurrentCulture.NumberFormat);
        }

        /// <summary>
        /// Event handler for the zoom buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomByLevel(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // Find correct zoom level
            string zoomLevel = ((ImageButton)sender).CommandName;

            SetZoomScale(ConvertToInt(zoomLevel));
        }

        /// <summary>
        /// Event handler for the zoom +
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomPlus(object sender, EventArgs e)
        {
            ImageButton previous = null;
            int zoomLevel = 0;
            for (int i = 0; i < imbCache.Length; i++)
            {
                ImageButton im = imbCache[i];

                if (im.CommandArgument == SELECTED && previous != null)
                {
                    im.ImageUrl = GetResource(RES_URL_SCALEUNSELECTED);
                    im.CommandArgument = UNSELECTED;
                    zoomLevel = ConvertToInt(previous.CommandName);
                    break;
                }
                previous = im;
            }
            if (previous == null)
            {
                // No update needed already on correct zoom level (ie first)
                return;
            }

            int scale = ConvertToInt(zoomLevel);

            SetZoomScale(scale);
            
        }

        /// <summary>
        /// Event handler for the zoom -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomMinus(object sender, EventArgs e)
        {
           double x = theMap.XMax;

            double y = theMap.YMax;
            
            ImageButton next;
            int zoomLevel = 0;
            for (int i = 0; i < imbCache.Length; i++)
            {
                ImageButton im = imbCache[i];

                if (im.CommandArgument == SELECTED)
                {
                    // Next is two steps away as control
                    int j = i + 1;
                    if (j == imbCache.Length)
                    {
                        // Last zoom icon selected - no more up to go
                        return;
                    }

                    im.CommandArgument = UNSELECTED;
                    im.ImageUrl = GetResource(RES_URL_SCALEUNSELECTED);

                    // Should always be correct
                    next = imbCache[j];
                    zoomLevel = ConvertToInt(next.CommandName);
                    break;
                }
            }
            SetZoomScale(ConvertToInt(zoomLevel));
        }


        #endregion

        #region Scale Change methode to be called when map change CCN 0427
       /// <summary>
       /// CCN- 0427 Sets Map legend and zoom level. Also changes the selected zoom level button.
       /// </summary>
       /// <param name="newscale">new scale of the map</param>
       /// <param name="naptanInRange">naptan in range</param>
        public void ScaleChange(int newscale, bool naptanInRange)
        {
            MapHelper helper = new MapHelper();
            hyperLinkMapKey.NavigateUrl = helper.getLegandUrl(newscale);
            hyperLinkMapKey.Visible = !(hyperLinkMapKey.NavigateUrl == string.Empty);

            labelZoomLevel.Text = "1:" + newscale;

            if (imbCache != null)
            {
                ImageButton selectedButton = null;
                int max = imbCache.Length;
                // Start on 1 and only look towards the lower scale
                for (int i = 0; i < max; i++)
                {
                    ImageButton selBut = imbCache[i];

                    selBut.CommandArgument = UNSELECTED;
                    selBut.ImageUrl = GetResource(RES_URL_SCALEUNSELECTED);

                    if (selectedButton == null)
                    {
                        int currentZoom = ConvertToInt(selBut.CommandName);
                        if (newscale == currentZoom)
                        {
                            selectedButton = selBut;
                            continue;
                        }
                        if (newscale > currentZoom)
                        {
                            continue;
                        }
                        ImageButton prevBut = imbCache[i - 1];

                        int previousZoom = ConvertToInt(prevBut.CommandName);
                        // If somehow previous zoom is larger than the scale -> first button to be selected
                        if (previousZoom > newscale)
                        {
                            selectedButton = prevBut;
                        }
                        else if (i > 0)
                        {

                            // Advanced calculation
                            int treshold = (currentZoom - previousZoom) / 2;
                            int compare = newscale - previousZoom;
                            if (treshold > compare)
                            {
                                // IR861 If previous button is zoom level 5, care should be taken.
                                // Only select this if naptanInRange, so can display Map Symbols.
                                string zoomDef = string.Format(zoomDefinitions[4], zoomSetting);
                                if (prevBut.ID == zoomDef && !naptanInRange)
                                {
                                    selectedButton = selBut;
                                }
                                else
                                {
                                    selectedButton = prevBut;
                                }
                            }
                            else
                            {
                                selectedButton = selBut;
                            }
                        }
                    }
                }

                if (selectedButton == null)
                {
                    // Last button to be selected
                    selectedButton = imbCache[max - 1];
                }

                selectedButton.CommandArgument = SELECTED;
                selectedButton.ImageUrl = GetResource(RES_URL_SCALESELECTED);
            }

        }

        private void SetZoomScale(int scale)
        {
            string zoomDef = string.Format("Web.{0}.ZoomLevelThirteen", zoomSetting);

            if (scale > 0)
            {
                if (scale < ConvertToInt(Properties.Current[zoomDef]))
                    theMap.SetScale(scale);
                else
                    theMap.ZoomFull();
            }
        }
        #endregion


    }
}
