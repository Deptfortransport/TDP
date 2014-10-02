namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using TransportDirect.UserPortal.Web.Adapters;
    using TransportDirect.UserPortal.Web;
    using TransportDirect.Web.Support;
    using TransportDirect.Common.ResourceManager;
    using TransportDirect.Common;
    using TransportDirect.UserPortal.Web.Code;
    using TransportDirect.Common.DatabaseInfrastructure.Content;

    /// <summary>
    ///	Control to allow selection of STOPS and POINTXs on the map.
    /// </summary>
    public partial class MapLocationIconSelectControlTransportOnly : TDUserControl
    {
        #region Labels

        #endregion

        #region Image Buttons

        #endregion

        #region Tables
        #endregion

        #region Checkboxes











        #endregion

        #region Images (used for the keys)
        #endregion

        #region Image Urls of radio and checked box button

        // Checkbox
        private string imageUnchecked = String.Empty;
        private string imageChecked = String.Empty;

        // Radio button
        private string imageSelected = String.Empty;
        private string imageUnselected = String.Empty;
        #endregion

        // Flag used to determine whether the Transport symbols 
        // sub menu should be displayed
        bool transportPanelVisible;

        public event EventHandler FindAMapBoxSelectionChanged;

        #region Page Load
        /// <summary>
        /// Page Load method - populates and initialise all controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
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

            Image31.Visible = FindCarParkHelper.CarParkingAvailable;
            CPK.Visible = FindCarParkHelper.CarParkingAvailable;

            if (!IsPostBack)
            {

                // Set the Transport to the checked image (default)
                commandTransport.ImageUrl = imageChecked;
            }
            // Check all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, true);

            // Set the image radio button for Accomodation to selected
            commandAccommodation.ImageUrl = imageUnselected; ;

            // Set the Table of accomodation to be false
            //tableAccommodation.Visible = false;

            // Set all other image radio buttons to unselected
            commandAttractions.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            // Initialise the OK image button URL
            buttonOk.Text = GetResource("JourneyMapControl.JourneyPlanner.imageShowOnMap.Text");

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


            // Populate controls

            
            


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

            // Populate the checklists
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
            //PopulateCheckList(tableAttractions, "MapLocationIconsSelectControl.checklistAttractions");
            //PopulateCheckList(tableAccommodation, "MapLocationIconsSelectControl.checklistAccommodation");
            //PopulateCheckList(tableSport, "MapLocationIconsSelectControl.checklistSport");
            //PopulateCheckList(tableEducation, "MapLocationIconsSelectControl.checklistEducation");
            //PopulateCheckList(tableInfrastructure, "MapLocationIconsSelectControl.checklistInfrastructure");
            //PopulateCheckList(tableHealth, "MapLocationIconsSelectControl.checklistHealth");

            // Populate the key images				
            //PopulateKeyImages(
            //    tableAttractions,
            //    "MapLocationIconsSelectControl.imageUrlListAttractions",
            //    "MapLocationIconsSelectControl.imageListAttractionsAlternateText");

            //PopulateKeyImages(
            //    tableAccommodation,
            //    "MapLocationIconsSelectControl.imageUrlListAccomodation",
            //    "MapLocationIconsSelectControl.imageListAccomodationAlternateText");

            //PopulateKeyImages(
            //    tableSport,
            //    "MapLocationIconsSelectControl.imageUrlListSport",
            //    "MapLocationIconsSelectControl.imageListSportAlternateText");

            //PopulateKeyImages(
            //    tableEducation,
            //    "MapLocationIconsSelectControl.imageUrlListEducation",
            //    "MapLocationIconsSelectControl.imageListEducationAlternateText");

            //PopulateKeyImages(
            //    tableInfrastructure,
            //    "MapLocationIconsSelectControl.imageUrlListInfrastructure",
            //    "MapLocationIconsSelectControl.imageListInfrastructureAlternateText");

            //PopulateKeyImages(
            //    tableHealth,
            //    "MapLocationIconsSelectControl.imageUrlListHealth",
            //    "MapLocationIconsSelectControl.imageListHealthAlternateText");


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
        #endregion

        /// <summary>
        /// Pre Render method
        /// Set visibilty of transport menu Table
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            // Udpate visibility of transport sub menu
            // This could have changed since page load
            tableTransport.Visible = transportPanelVisible;
            base.OnPreRender(e);
        }


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
            this.commandTransport.Click += new System.Web.UI.ImageClickEventHandler(this.commandTransport_Click);
            this.commandAccommodation.Click += new System.Web.UI.ImageClickEventHandler(this.commandAccommodation_Click);
            this.commandSport.Click += new System.Web.UI.ImageClickEventHandler(this.commandSport_Click);
            this.commandAttractions.Click += new System.Web.UI.ImageClickEventHandler(this.commandAttractions_Click);
            this.commandHealth.Click += new System.Web.UI.ImageClickEventHandler(this.commandHealth_Click);
            this.commandEducation.Click += new System.Web.UI.ImageClickEventHandler(this.commandEducation_Click);
            this.commandInfrastructure.Click += new System.Web.UI.ImageClickEventHandler(this.commandInfrastructure_Click);

        }
        #endregion

        #region Event Handlers to handle image button clicks to expand panels

        /// <summary>
        /// Handler for the transport button
        /// </summary>
        private void commandTransport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // Update dummy check box image		
            // CCN 0427 used GetAlteredImageUrl to get the correct image url
            if (commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
            {
                commandTransport.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(imageUnchecked);
                transportPanelVisible = false;
            }
            else
            {
                commandTransport.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(imageChecked);
                transportPanelVisible = true;
            }
        }

        /// <summary>
        /// Handler for the accomodation button
        /// </summary>
        private void commandAccommodation_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {


            commandAccommodation.ImageUrl = imageSelected;

            

            // unselect all other buttons
            commandAttractions.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            if (FindAMapBoxSelectionChanged != null)
                FindAMapBoxSelectionChanged(this, EventArgs.Empty);

        }

        /// <summary>
        /// Handler for the accomodation button
        /// </summary>
        private void commandAttractions_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            commandAttractions.ImageUrl = imageSelected;
           
            // unselect all other buttons
            commandAccommodation.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            if (FindAMapBoxSelectionChanged != null)
                FindAMapBoxSelectionChanged(this, EventArgs.Empty);
           
        }

        /// <summary>
        /// Handler for the accomodation button
        /// </summary>
        private void commandSport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            commandSport.ImageUrl = imageSelected;

            
            // unselect all other buttons
            commandAccommodation.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;
            commandAttractions.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            if (FindAMapBoxSelectionChanged != null)
                FindAMapBoxSelectionChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handler for the education button
        /// </summary>
        private void commandEducation_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            commandEducation.ImageUrl = imageSelected;

            

            // unselect all other buttons
            commandAccommodation.ImageUrl = imageUnselected;
            commandAttractions.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            if (FindAMapBoxSelectionChanged != null)
                FindAMapBoxSelectionChanged(this, EventArgs.Empty);
            
        }

        /// <summary>
        /// Handler for the infrastructure button
        /// </summary>
        private void commandInfrastructure_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            commandInfrastructure.ImageUrl = imageSelected;

            
            // unselect all other buttons
            commandAccommodation.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandAttractions.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandHealth.ImageUrl = imageUnselected;

            if (FindAMapBoxSelectionChanged != null)
                FindAMapBoxSelectionChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handler for the health button
        /// </summary>
        private void commandHealth_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            commandHealth.ImageUrl = imageSelected;

           
            // unselect all other buttons
            commandAccommodation.ImageUrl = imageUnselected;
            commandEducation.ImageUrl = imageUnselected;
            commandAttractions.ImageUrl = imageUnselected;
            commandSport.ImageUrl = imageUnselected;
            commandInfrastructure.ImageUrl = imageUnselected;

            if (FindAMapBoxSelectionChanged != null)
                FindAMapBoxSelectionChanged(this, EventArgs.Empty);
           
        }

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

        #endregion

        #region Methods to populate check lists key images in the table

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
            foreach (System.Web.UI.Control control in table.Controls)
            {
                foreach (System.Web.UI.Control control1 in control.Controls)
                {
                    foreach (System.Web.UI.Control control2 in control1.Controls)
                    {

                        // Check to see if the control is an image
                        if (control2 is System.Web.UI.WebControls.Image)
                        {
                            // The control is an image so set the image url
                            // CCN 0427 change image control to tdimage control
                            TDImage image =
                                (TDImage)control2;
                            if (i < imageUrls.Length)
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

                int i = 0;
                foreach (System.Web.UI.Control control in table.Controls)
                {
                    foreach (System.Web.UI.Control control1 in control.Controls)
                    {
                        foreach (System.Web.UI.Control control2 in control1.Controls)
                        {
                            if (control2 is CheckBox)
                            {
                                CheckBox checkbox = (CheckBox)control2;
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

        #region Property to return OK button

        /// <summary>
        /// Get Property, returns the OK image button
        /// </summary>
        public TDButton OK
        {
            get { return buttonOk; }
        }

        #endregion

        #region Methods to get selected Hashtable keys

        /// <summary>
        /// Returns an arraylist of all the Stop keys that have been selected.
        /// These are determined depending on what has been selected in
        /// the Transport checkboxes.
        /// </summary>
        /// <returns>ArrayList containing all selected keys</returns>
        public ArrayList GetSelectedStopKeys()
        {
            // Determine if the ImageButton checkbox for Transport has been selected.
            // If the Table is open then get the keys, otherwise return an empty
            // string array.
            // CCN 0427 used GetAlteredImageUrl to get the correct image url
            if (commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
                return GetHashtableKeys(tableTransport);
            else
                return new ArrayList(0);
        }

        /// <summary>
        /// Returns true if the car park check box is selected.
        /// </summary>
        /// <returns>boolean</returns>
        public bool CarParksSelected
        {
            get
            {
                // CCN 0427 used GetAlteredImageUrl to get the correct image url
                if (commandTransport.ImageUrl.ToLower() == ImageUrlHelper.GetAlteredImageUrl(imageChecked).ToLower())
                    return CPK.Checked;
                else
                    return false;
            }
        }

        /// <summary>
        /// Returns an arraylist of all the Stop keys that have been selected.
        /// These are determined depending on what has been selected in
        /// the Accomodation, Sport, Attractions, Education, Health, Public Checkboxes.
        /// The keys returned will belong to only one category (e.g. - this method
        /// will never return keys that belong both to Accomodation and Sports.)
        /// </summary>
        /// <returns>ArrayList containing all selected keys</returns>
        public ArrayList GetSelectedPointXKeys()
        {
            
                return new ArrayList(0); // no Tables open
        }

        /// <summary>
        /// Returns all the hashtable keys of all selected checkboxes in the given Table.
        /// </summary>
        /// <param name="Table">Table to get hashtable keys for.</param>
        /// <returns>ArrayList of selected keys.</returns>
        private ArrayList GetHashtableKeys(Table table)
        {
            ArrayList keys = new ArrayList();
            bool selectAll = false;

            // Loop through the controls in the given Table
            foreach (System.Web.UI.Control control in table.Controls)
            {
                foreach (System.Web.UI.Control control1 in control.Controls)
                {
                    foreach (System.Web.UI.Control control2 in control1.Controls)
                    {

                        // Check to see if the control is a checkbox
                        if (control2 is CheckBox)
                        {
                            CheckBox checkBox = (CheckBox)control2;

                            // Get the id of the checkbox
                            string id = checkBox.ID;

                            // Check to see if the id is a "Select All" option
                            if (id.StartsWith("checkboxSelectAll"))
                            {
                                // Determine if "Select All" has been checked
                                selectAll = checkBox.Checked;
                            }
                            else
                            {
                                // Check box is not "Select All". Determine if the associated
                                // key should be added.
                                if (selectAll || checkBox.Checked)
                                    keys.Add(id);
                            }
                        }
                    }
                }
            }

            // Return all selected keys
            return keys;
        }

        #endregion

        /// <summary>
        /// Enable/Disables the OK button
        /// </summary>
        /// <param name="enable">True to enable, false to disable.</param>
        public void EnableOKButton(bool enable)
        {
            // Set both to false initially - otherwise it would be possible for
            // both buttons to show if there visibilities were not initialised correctly.
            buttonOk.Visible = false;
            //imageOK.Visible = false;
            //panelOnlyView.Visible = false;

            buttonOk.Visible = enable;
            //imageOK.Visible = !enable;

            //panelOnlyView.Visible = !enable;

            commandTransport.Visible = enable;

            labelTransportTitle.Visible = enable;
            commandAccommodation.Visible = enable;
            labelAccommodationTitle.Visible = enable;
            commandSport.Visible = enable;
            labelSportTitle.Visible = enable;
            commandAttractions.Visible = enable;
            labelAttractionsTitle.Visible = enable;
            commandHealth.Visible = enable;
            labelHealthTitle.Visible = enable;
            commandEducation.Visible = enable;
            labelEducationTitle.Visible = enable;
            commandInfrastructure.Visible = enable;
            labelInfrastructureTitle.Visible = enable;
            panelKeys.Visible = enable;

        }

        /// <summary>
        /// Get property - indicates if the OK button is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return buttonOk.Visible;
            }
        }

        public MapAdditionalIconSelected MapAdditionalIconsSelected
        {
            get
            {
                MapAdditionalIconSelected selected = MapAdditionalIconSelected.None;

                if (commandHealth.ImageUrl == imageSelected)
                    selected = MapAdditionalIconSelected.Health;
                if (commandAccommodation.ImageUrl == imageSelected)
                    selected = MapAdditionalIconSelected.Accomodation;
                if (commandEducation.ImageUrl == imageSelected)
                    selected = MapAdditionalIconSelected.Education;
                if (commandAttractions.ImageUrl == imageSelected)
                    selected = MapAdditionalIconSelected.Attractions;
                if (commandSport.ImageUrl == imageSelected)
                    selected = MapAdditionalIconSelected.Sport;
                if (commandInfrastructure.ImageUrl == imageSelected)
                    selected = MapAdditionalIconSelected.Infrastructure;

                return selected;
            }
        }

        /// <summary>
        /// Uncheck all transport section checkboxes. For FindStationMap page
        /// </summary>
        public void UncheckTransportSection()
        {

            // UnCheck all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, false);

        }

        /// <summary>
        /// Uncheck all transport section checkboxes, then check airports, rail stations, and car parks
        /// This is the default map symbol selection for car journeys
        /// </summary>
        public void TransportSectionCar()
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
        /// Check all transport section checkboxes, default for public journeys and Extension summaries. 
        /// </summary>
        public void CheckTransportSection()
        {

            // Check all checkboxes in the Transport Table
            UpdateCheckBoxes(tableTransport, true);

        }

        #region Methods to control icon selection for printer friendly pages

        /// <summary>
        /// Clears the current icon selection
        /// </summary>
        private void ClearIconSelection(ref bool[][] iconSelection)
        {
            for (int i = 0; i < iconSelection.Length; i++)
            {
                for (int j = 0; j < iconSelection[i].Length; j++)
                {
                    iconSelection[i][j] = false;
                }
            }
        }

        /// <summary>
        /// Updates the current icon selection
        /// </summary>
        public void UpdateIconSelection(ref bool[][] iconSelection)
        {
            ClearIconSelection(ref iconSelection);

            if (tableTransport.Visible)
                RetrieveSelectionFromTable(tableTransport, ref iconSelection[0]);
           


        }

        /// <summary>
        /// Retrieves the current icon selection
        /// </summary>
        private void RetrieveSelectionFromTable(Table table, ref bool[] iconSelection)
        {
            int i = 0;
            foreach (System.Web.UI.Control control in table.Controls)
            {
                foreach (System.Web.UI.Control control1 in control.Controls)
                {
                    foreach (System.Web.UI.Control control2 in control1.Controls)
                    {
                        if (control2 is CheckBox)
                        {

                            CheckBox checkBox = (CheckBox)control2;

                            iconSelection[i++] = checkBox.Checked;

                        }
                    }
                }
            }
        }

        #endregion




    }
}
