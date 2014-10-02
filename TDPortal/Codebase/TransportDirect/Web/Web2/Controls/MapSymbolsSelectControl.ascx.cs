// *********************************************** 
// NAME                 : MapSymbolsSelectControl.ascx
// AUTHOR               : Amit Patel
// DATE CREATED         : 02/11/2009 
// DESCRIPTION          : Control allowing map STOPS and POINTX symbols to be shown/toggled on the maps (using MapControl2)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapSymbolsSelectControl.ascx.cs-arc  $
//
//   Rev 1.10   Jun 14 2010 13:24:12   pghumra
//Added JavaScript validation of date selected to show travel news for on map on journey results page
//Resolution for 5552: CODEFIX - INITIAL - DEL 10.X - Issue with date in Travel News on journey planner results
//
//   Rev 1.9   Dec 11 2009 14:53:48   apatel
//Mapping enhancement for travelnews
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 30 2009 16:02:48   mmodi
//Updated to completely hide travel news toggle to correct display issue in IE
//
//   Rev 1.7   Nov 28 2009 11:26:52   apatel
//Travel News map enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 17 2009 18:01:08   mmodi
//New public methods to set seletced symbols
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 13 2009 09:40:52   apatel
//make all the additional symbol tables not to display on load
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 12 2009 08:58:00   mmodi
//Updated public method names
//
//   Rev 1.3   Nov 10 2009 10:16:30   apatel
//Updated control to set the additional symbols table to show and added public methods to control the transport symbols showing by default
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 05 2009 14:56:24   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 04 2009 11:03:00   mmodi
//Updated styles and tidy up
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 03 2009 09:48:08   apatel
//Initial revision.

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Code;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Control to allow selection of STOPS and POINTXs on the map.
	/// </summary>
    public partial class MapSymbolsSelectControl : TDUserControl
    {
        #region Private members

        // Script containing the javascript to do the toggle of map symbols
        private const string scriptName = "MapSymbolsSelectControl";

        // Flag used to determine whether the Transport symbols 
        // sub menu should be displayed
        bool transportPanelVisible;

        // Client ID of the map control to toggle the map symbols on
        private string mapId = "map";

        // Travel News Date Select control selected date
        private TDDateTime travelNewsDate = TDDateTime.Now;

        private bool showTravelNews = false;

        #region Image Urls of radio and checked box button

        // Checkbox
		private string imageUnchecked = String.Empty;
		private string imageChecked = String.Empty;

		// Radio button
		private string imageSelected = String.Empty;
		private string imageUnselected = String.Empty;

		#endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Write only. Sets the Client ID of the map control to perform the 
        /// map symbols toggle on
        /// </summary>
        public string MapId
        {
            set { mapId = value; }
        }

        /// <summary>
        /// Write only. Sets wether travel news option and toggle button should be visible or not
        /// </summary>
        public bool ShowTravelNews
        {
            set
            {
                showTravelNews = value;  
            }
        }

        /// <summary>
        /// Write only. Sets the date for which travel news incidents should be visible on the map
        /// </summary>
        public TDDateTime TravelNewsDate
        {
            set
            {
                travelNewsDate = value;
            }
        }
 
        #endregion
        
        #region Page Load, Page PreRender
        
        /// <summary>
		/// Page Load method - populates and initialise all controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
        {
            LoadResources();

            PopulateControls();

            if (!IsPostBack)
            {
                // Check all checkboxes in the Transport Table
                UpdateCheckBoxes(tableTransport, true);
              

            }
            
			// Set the visibility of the Transport types Table, 
			// based on the current image N.B. this is a dummy checkbox
            // CCN 0427 used GetAlteredImageUrl to get the correct image url
			if (commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
			{
				transportPanelVisible = true;
			}
			else
			{
				transportPanelVisible = false;
			}
		}

		/// <summary>
		/// Pre Render method
		/// Set visibilty of transport menu Table
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e ) 
		{
			// Udpate visibility of transport sub menu
			// This could have changed since page load
			tableTransport.Visible = transportPanelVisible;

            travelNewsContainer.Visible = showTravelNews;
            buttonToggleTravelNewsAndSymbols.Visible = showTravelNews;
            panelTravelNewsToggle.Visible = showTravelNews;

            dateSelect.Current = travelNewsDate;
 
            RegisterJavaScripts();

			base.OnPreRender( e );
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			
		}

		#endregion

        #region Private methods

        /// <summary>
        /// Loads the images and text strings
        /// </summary>
        private void LoadResources()
        {
            #region Images

            // Initialise Image Urls of radio and checked box image buttons
            imageUnchecked = Global.tdResourceManager.GetString(
                "JourneyPlanner.imageButtonCheckBoxUnchecked",
                TDCultureInfo.CurrentUICulture);

            imageChecked = Global.tdResourceManager.GetString(
                "JourneyPlanner.imageButtonCheckBoxChecked",
                TDCultureInfo.CurrentUICulture);

            imageSelected = Global.tdResourceManager.GetString(
                "JourneyPlanner.imageButtonBlueRadioButtonChecked",
                TDCultureInfo.CurrentUICulture);

            imageUnselected = Global.tdResourceManager.GetString(
                "JourneyPlanner.imageButtonBlueRadioButtonUnchecked",
                TDCultureInfo.CurrentUICulture);

            #endregion

            #region Alternate text

            // Set alternate texts
            commandTransport.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandTransport.AlternateText", TDCultureInfo.CurrentUICulture);

            commandAccommodation.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandAccomodation.AlternateText", TDCultureInfo.CurrentUICulture);

            commandAttractions.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandAttractions.AlternateText", TDCultureInfo.CurrentUICulture);

            commandSport.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandSport.AlternateText", TDCultureInfo.CurrentUICulture);

            commandEducation.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandEducation.AlternateText", TDCultureInfo.CurrentUICulture);

            commandInfrastructure.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandInfrastructure.AlternateText", TDCultureInfo.CurrentUICulture);

            commandHealth.AlternateText = Global.tdResourceManager.GetString(
                "JourneyMapControl.commandHealth.AlternateText", TDCultureInfo.CurrentUICulture);

            #endregion

            #region Labels

            lblDateError.Text = GetResource(TravelNewsHelper.ResourceKeyInvalidDate);

            labelMapSymbols.Text = 
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelLocationTitle",
                TDCultureInfo.CurrentUICulture);

            lblSelectedCategory.Text = 
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.lblSelectedCategory",
                TDCultureInfo.CurrentUICulture);

            labelOnlyView.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelOnlyView",
                TDCultureInfo.CurrentUICulture);

            labelSymbolsKey.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelSymbolsKey",
                TDCultureInfo.CurrentUICulture);

            labelTransport.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelTransport",
                TDCultureInfo.CurrentUICulture);

            labelAccommodation.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelAccommodationTitle",
                TDCultureInfo.CurrentUICulture);

            labelSport.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelSportTitle",
                TDCultureInfo.CurrentUICulture);

            labelAttractions.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelAttractionsTitle",
                TDCultureInfo.CurrentUICulture);

            labelHealth.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelHealthTitle",
                TDCultureInfo.CurrentUICulture);

            labelEducation.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelEducationTitle",
                TDCultureInfo.CurrentUICulture);

            labelPublicBuildings.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelInfrastructureTitle",
                TDCultureInfo.CurrentUICulture);
            
            labelTransportTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelTransportTitle",
                TDCultureInfo.CurrentUICulture);

            labelAccommodationTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelAccommodationTitle",
                TDCultureInfo.CurrentUICulture);

            labelAttractionsTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelAttractionsTitle",
                TDCultureInfo.CurrentUICulture);

            labelEducationTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelEducationTitle",
                TDCultureInfo.CurrentUICulture);

            labelInfrastructureTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelInfrastructureTitle",
                TDCultureInfo.CurrentUICulture);

            labelSportTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelSportTitle",
                TDCultureInfo.CurrentUICulture);

            labelHealthTitle.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelHealthTitle",
                TDCultureInfo.CurrentUICulture);

            labelTravelNews.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.labelTravelNews",
                TDCultureInfo.CurrentUICulture);
            #endregion

            #region TravelNews checkboxes
            publicIncidentsVisible.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.publicIncidentsVisible",
                TDCultureInfo.CurrentUICulture);

            roadIncidentsVisible.Text =
                Global.tdResourceManager.GetString(
                "MapLocationIconsSelectControl.roadIncidentsVisible",
                TDCultureInfo.CurrentUICulture);
            #endregion

            #region Buttons

            buttonOk.Text = GetResource("JourneyMapControl.JourneyPlanner.imageShowOnMap.Text");

            buttonShowNews.Text = GetResource("MapLocationIconsSelectControl.buttonShowNews.Text");

            buttonToggleTravelNewsAndSymbols.Text = GetResource("MapLocationIconsSelectControl.buttonToggleTravelNewsAndSymbols.TravelNews.Text");

            #endregion
        }

        private void PopulateControls()
        {
            #region Set Group command images

            // Set the Transport to the checked image (default)
            commandTransport.ImageUrl = imageChecked;

            // Set all other image radio buttons to unselected
            commandAccommodation.ImageUrl = imageUnselected;
            commandAttractions.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            #endregion

            #region Populate the checklists and keys

            // Hide the car park if needed
            Image31.Visible = FindCarParkHelper.CarParkingAvailable;
            CPK.Visible = FindCarParkHelper.CarParkingAvailable;

            // Handling for car parks check box if available
            if (FindCarParkHelper.CarParkingAvailable)
            {
                PopulateCheckList(tableTransport, "MapLocationIconsSelectControl.checklistTransport");

                PopulateKeyImages(
                    tableTransport,
                    "MapLocationIconsSelectControl.imageUrlListTransport",
                    "MapLocationIconsSelectControl.imageListTransportAlternateText");
            }
            else
            {
                PopulateCheckList(tableTransport, "MapLocationIconsSelectControl.checklistTransportAlt");

                PopulateKeyImages(
                    tableTransport,
                    "MapLocationIconsSelectControl.imageUrlListTransportAlt",
                    "MapLocationIconsSelectControl.imageListTransportAlternateTextAlt");
            }

            PopulateCheckList(tableAttractions, "MapLocationIconsSelectControl.checklistAttractions");
            PopulateCheckList(tableAccommodation, "MapLocationIconsSelectControl.checklistAccommodation");
            PopulateCheckList(tableSport, "MapLocationIconsSelectControl.checklistSport");
            PopulateCheckList(tableEducation, "MapLocationIconsSelectControl.checklistEducation");
            PopulateCheckList(tableInfrastructure, "MapLocationIconsSelectControl.checklistInfrastructure");
            PopulateCheckList(tableHealth, "MapLocationIconsSelectControl.checklistHealth");


            // Populate the key images				
            PopulateKeyImages(
                tableAttractions,
                "MapLocationIconsSelectControl.imageUrlListAttractions",
                "MapLocationIconsSelectControl.imageListAttractionsAlternateText");

            PopulateKeyImages(
                tableAccommodation,
                "MapLocationIconsSelectControl.imageUrlListAccomodation",
                "MapLocationIconsSelectControl.imageListAccomodationAlternateText");

            PopulateKeyImages(
                tableSport,
                "MapLocationIconsSelectControl.imageUrlListSport",
                "MapLocationIconsSelectControl.imageListSportAlternateText");

            PopulateKeyImages(
                tableEducation,
                "MapLocationIconsSelectControl.imageUrlListEducation",
                "MapLocationIconsSelectControl.imageListEducationAlternateText");

            PopulateKeyImages(
                tableInfrastructure,
                "MapLocationIconsSelectControl.imageUrlListInfrastructure",
                "MapLocationIconsSelectControl.imageListInfrastructureAlternateText");

            PopulateKeyImages(
                tableHealth,
                "MapLocationIconsSelectControl.imageUrlListHealth",
                "MapLocationIconsSelectControl.imageListHealthAlternateText");

            #endregion
        }

        /// <summary>
        /// Method which adds the javascript to the controls
        /// </summary>
        private void RegisterJavaScripts()
        {
            commandTransport.Attributes["onclick"] = "toggleTransportOptionsAll('" + this.ClientID + "', '" + mapId + "');";

            commandAccommodation.Attributes["onclick"] = "showOtherOption('" + this.ClientID + "', '" + tableAccommodation.ClientID + "');";
            commandAttractions.Attributes["onclick"] = "showOtherOption('" + this.ClientID + "', '" + tableAttractions.ClientID + "');";
            commandSport.Attributes["onclick"] = "showOtherOption('" + this.ClientID + "', '" + tableSport.ClientID + "');";
            commandEducation.Attributes["onclick"] = "showOtherOption('" + this.ClientID + "', '" + tableEducation.ClientID + "');";
            commandInfrastructure.Attributes["onclick"] = "showOtherOption('" + this.ClientID + "', '" + tableInfrastructure.ClientID + "');";
            commandHealth.Attributes["onclick"] = "showOtherOption('" + this.ClientID + "', '" + tableHealth.ClientID + "');";

            buttonOk.OnClientClick = "return setLayers('" + this.ClientID + "', '" + mapId + "');";

            buttonShowNews.OnClientClick = "return setLayers('" + this.ClientID + "', '" + mapId + "');";

            string toggleTravelNewsAndSymbolsScript = string.Format("toggleNewsAndPointX('{0}','{1}','{2}');return false;",
                this.ClientID,
                GetResource("MapLocationIconsSelectControl.buttonToggleTravelNewsAndSymbols.TravelNews.Text"),
                GetResource("MapLocationIconsSelectControl.buttonToggleTravelNewsAndSymbols.PointOfInterest.Text"));

            buttonToggleTravelNewsAndSymbols.OnClientClick = toggleTravelNewsAndSymbolsScript;

            string dojoScript = "dojo.subscribe(\"Map\",function(mapArgs){ setMapSymbolLayers('" + this.ClientID + "', '" + mapId + "', mapArgs); });";

            // Determine if javascript is support and determine the JavascriptDom value
            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
            
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoScript", dojoScript, true );

            // Register the javascript file to toggle the map symbols on the page
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Common", scriptRepository.GetScript("Common", javaScriptDom));
        }

        #region Methods to populate check lists key images in the table

        /// <summary>
        /// Checks of unchecks all the checkboxes in the given Table.
        /// </summary>
        /// <param name="Table">Table to update checkboxes for.</param>
        /// <param name="check">Check boxes in true, uncheck if false.</param>
        private void UpdateCheckBoxes(Table table, bool check)
        {
            foreach (System.Web.UI.Control control in table.Controls)
            {
                foreach (System.Web.UI.Control control1 in control.Controls)
                {
                    foreach (System.Web.UI.Control control2 in control1.Controls)
                    {

                        if (control2 is CheckBox)
                        {
                            CheckBox checkbox = (CheckBox)control2;
                            checkbox.Checked = check;
                        }
                    }
                }
            }
        }
        
		/// <summary>
		/// Takes a Table and a resource name (for Resourcing manager)
		/// and populates the images on that Table. (The images are the keys
		/// to the checkbox items). A 'pipe' seperate list of image urls
		/// must exist in the resource manager for the given resource.
		/// </summary>
		/// <param name="Table">Table to populate key images for.</param>
		/// <param name="resource">Name of the resource to populate from in
		/// Resource manager.</param>
		private void PopulateKeyImages(Table table, string resourceImages, string resourceAlternateText)
		{
			// Get the 'pipe' seperate list of image urls for this Table
			string[] imageUrls = Global.tdResourceManager.GetString(
				resourceImages, TDCultureInfo.CurrentUICulture).Split('|');

			string[] imageAlternateTexts = Global.tdResourceManager.GetString(
				resourceAlternateText, TDCultureInfo.CurrentUICulture).Split('|');

			// Find all the images in this Table and populate from the list
			int i = 0;
			foreach(System.Web.UI.Control control in table.Controls)
			{
				foreach(System.Web.UI.Control control1 in control.Controls)
				{
					foreach(System.Web.UI.Control control2 in control1.Controls)
					{

						// Check to see if the control is an image
						if(control2 is System.Web.UI.WebControls.Image)
						{
							// The control is an image so set the image url
                            // CCN 0427 change image control to tdimage control
							TDImage image =
								(TDImage)control2;
							if(i < imageUrls.Length)
							{
								image.ImageUrl = imageUrls[i];
								image.AlternateText = imageAlternateTexts[i++];
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Method to populate the checklist
		/// </summary>
		private void PopulateCheckList(Table table, string resource)
		{
			try
			{
				string[] labels = Global.tdResourceManager.GetString(
					resource,
					TDCultureInfo.CurrentUICulture).Split('|');

				int i=0;
				foreach (System.Web.UI.Control control in table.Controls)
				{
					foreach(System.Web.UI.Control control1 in control.Controls)
					{
						foreach(System.Web.UI.Control control2 in control1.Controls)
						{
							if (control2 is CheckBox)
							{
								CheckBox checkbox = (CheckBox) control2;
								if (i < labels.Length)
									checkbox.Text = labels[i++];
							}
						}
					}
				}
			}
			catch (NullReferenceException)
			{
				// If this occurs, then the checklist will not be populated.
			}
		}

		#endregion

        #endregion

        #region Public Methods
        /// <summary>
        /// Uncheck all transport section checkboxes. For FindStationMap page
        /// </summary>
        public void UncheckTransportSectionSymbols()
        {
            // UnCheck all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, false);
        }

        /// <summary>
        /// Sets the checked status of transport section symbols for Car.
        /// This sets airports, rail stations, ferry terminals, and car parks
        /// </summary>
        public void CheckTransportSectionSymbolsForCar()
        {
            // UnCheck all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, false);
            RLY.Checked = true;
            AIR.Checked = true;
            MET.Checked = true;
            FER.Checked = true;
            CPK.Checked = true;
        }

        /// <summary>
        /// Sets the checked status of transport section symbols for Car Parks.
        /// This sets the car parks symbols to visible only
        /// </summary>
        public void CheckTransportSectionSymbolsForCarPark()
        {
            // UnCheck all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, false);
            CPK.Checked = true;
        }

        /// <summary>
        /// Sets the checked status of transport section symbols for Stations.
        /// This sets the rail stations, bus/coach stops, and airport symbols to visible only
        /// </summary>
        public void CheckTransportSectionSymbolsForStations()
        {
            // UnCheck all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, false);
            RLY.Checked = true;
            AIR.Checked = true;
            BCX.Checked = true;
        }

        /// <summary>
        /// Check all transport section checkboxes
        /// Default for public journeys and Extension summaries. 
        /// </summary>
        public void CheckTransportSectionSymbols()
        {
            // Check all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, true);
        }
        #endregion
    }
}
