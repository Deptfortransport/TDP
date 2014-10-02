// *********************************************** 
// NAME                 : SimpleMapControl.ascx
// AUTHOR               : Andy Lole
// DATE CREATED         : 09/10/2003 
// DESCRIPTION  : SimpleMap Web user control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/SimpleMapControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 07 2009 11:28:18   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:23:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:58   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:52   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:27:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Dec 18 2003 15:12:36   kcheung
//Updated logic to perform map event logging.
//Resolution for 551: Map Events are being logged with incorrect submission time
//
//   Rev 1.1   Nov 26 2003 13:38:58   alole
//Updated to draw Congestion Service Name from Properties DB.
//Added MapEvent Logging for Map_Changed and Map_click events.
//
//   Rev 1.0   Oct 21 2003 09:23:14   ALole
//Initial Revision


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.Presentation.InteractiveMapping;
	using TransportDirect.Common.PropertyService.Properties;
    using TransportDirect.Common;
    using TransportDirect.UserPortal.Web.Adapters;

	/// <summary>
	///	SimpleMap Web user control
	/// </summary>
	public partial  class SimpleMapControl : TDUserControl
	{
		private DateTime operationStartedDateTime; // used for logging purposes

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

        private const string DEFAULT_ZOOM_DEFINITION = "SimpleMappingComponent";

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

        #endregion

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

            //CCN 0427 Changed made to map control to have zoom control on map
            if (IsZoomControlVisible)
            {
                InitializeZoomControl();
                
                
            }
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			theMap.ServiceName = Properties.Current["InteractiveMapping.SimpleMap.ServiceName"];
			theMap.ServerName = Properties.Current["InteractiveMapping.SimpleMap.ServerName"];
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

		}
		#endregion

		#region Pan Event Handling

		private void PanNorthWestButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.NorthWest);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanNorthButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.North);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanNorthEastButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.NorthEast);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanEastButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.East);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanSouthEastButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.SouthEast);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanSouthButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.South);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanSouthWestButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.SouthWest);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		private void PanWestButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SetStartTime();
			theMap.Pan(TransportDirect.Presentation.InteractiveMapping.SimpleMap.Direction.West);
			MapLogging.Write(MapEventCommandCategory.MapPan, MapEventDisplayCategory.MiniScale, operationStartedDateTime);
		}

		#endregion

		#region Public properties

		public TransportDirect.UserPortal.Web.Controls.TDSimpleMapControl Map
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

            if (ConvertToInt(zoomLevel) > 0)
                theMap.SetScale(ConvertToInt(zoomLevel));
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

            if (ConvertToInt(zoomLevel) > 0)
                theMap.SetScale(ConvertToInt(zoomLevel));

        }

        /// <summary>
        /// Event handler for the zoom -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomMinus(object sender, EventArgs e)
        {
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
            if (ConvertToInt(zoomLevel) > 0)
                theMap.SetScale(ConvertToInt(zoomLevel));
        }


        #endregion

        #region handling map change event CCN 0427
        

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
                                if (prevBut.ID == zoomDefinitions[4] && !naptanInRange)
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
        #endregion
	}
}

