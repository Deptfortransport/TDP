// ****************************************************************************************** 
// NAME                 : ShowNewsControl.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 23/09/2003 
// DESCRIPTION          : UI providing user with number of drop down live travel news options 
//						  that may be saved to their user profile 
// ****************************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ShowNewsControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Dec 11 2009 07:05:38   apatel
//updated for search phrase logic of showing error when show map button clicked
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 26 2009 15:47:22   apatel
//TravelNews page and controls updated for new mapping functionality
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:23:06   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 04 2008 17:00:00 mmodi
//Updated to add error display style around search input text box
//
//  Rev Devfactory Jan 12 2008 07:31:18 apatel
//  New Property ShowSearchInputError and warning lable aded near the seach box
//
//  Rev Devfactory Jan 10 2008 11:58:58 apatel
//  Page_PreRender updated to ensure shiwnewscontrol.IncidentType 
//  dropdown property is correctly populated with the value held in travelnewsstate
//
//  Rev Devfactory Jan 10 2008 11:38:38 apatel
//  Added a new table filterTitleLabel to show title "Filter travel news by"
//
//   Rev 1.0   Nov 08 2007 13:17:56   mturner
//Initial revision.
//
//   Rev Devfactory Jan 08 2008 12:40:40 apatel
//Rearranging the controls to match new design, added new IncidentType filter and Labels
//
//   Rev 1.40   Jan 19 2007 13:41:32   build
//Automatically merged from branch for stream4329
//
//   Rev 1.39.1.0   Jan 12 2007 13:32:26   tmollart
//Modifications for travel news search. Added require code for the travel news search text box.
//
//   Rev 1.39   Mar 28 2006 11:09:04   build
//Automatically merged from branch for stream0024
//
//   Rev 1.38.1.0   Mar 03 2006 16:46:04   AViitanen
//Removed details label and dropdown list and SelectedDetails. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.38   Feb 23 2006 16:13:52   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.37   Dec 20 2005 11:14:08   jgeorge
//Removed page landing functionality (copied to helper class)
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.36   Dec 01 2005 16:20:24   jmcallister
//ir3265 Set session landing page flag to false after first load.
//Resolution for 3265: DN077: Changing Region in Travel News
//
//   Rev 1.35   Nov 15 2005 14:11:54   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.34   Nov 08 2005 18:03:34   ralonso
//commandCalendar imagebutton removed
//
//   Rev 1.33   Nov 08 2005 13:11:32   jmcallister
//Changed default display type to table for travel news landing due to changed requirements.
//
//   Rev 1.32   Nov 04 2005 12:48:44   ralonso
//Manual merge stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.31   Nov 03 2005 13:47:22   jmcallister
//Revised due to unit test results after merge. (Missing parameters were no longer being handled gracefully)
//
//   Rev 1.30   Nov 02 2005 18:34:30   kjosling
//Automatically merged from branch for stream2877
//
//   Rev 1.29.1.1   Nov 02 2005 16:12:40   jmcallister
//Code review comments enacted
//
//   Rev 1.29.1.0   Nov 01 2005 17:23:26   jmcallister
//Landing Page Functionality
//
//
//   Rev 1.29   Oct 06 2005 14:59:54   kjosling
//Merged stream 2817 with trunk
//Resolution for 2817: DEL 7.1.4 Stream Label

//   Rev 1.28.2.0   Oct 18 2005 11:24:36   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.28.1.0   Oct 03 2005 14:30:36   CRees
//Error handling for Recent Delays selected for date <> today.
//
//   Rev 1.28   Sep 02 2005 17:16:24   RWilby
//Fix for IR2687 to detect postbacks for the non-JavaScript implementation of the ImageMapControl  
//Resolution for 2687: DN018 - When map region is selected the date is re-set (IE 5.5 only?)
//
//   Rev 1.27   Aug 18 2005 14:28:30   jgeorge
//Added missing commenting
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.26   Aug 18 2005 11:29:26   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.25.1.2   Aug 04 2005 11:25:36   jgeorge
//Rework due to change in page layout
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.25.1.1   Jul 08 2005 14:50:32   jbroome
//Work in progress
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.25.1.0   Jul 01 2005 10:18:04   jmorrissey
//Redesigned layout. Moved most of the code to the TravelNewsPage to increase this control's reusability. Removed all direct references to the  SessionManager. Show button click now just raises an event which is handled by the parent page.
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.25   Dec 16 2004 15:23:36   passuied
//refactoring the TravelNews component and changes to the clients
//
//   Rev 1.24   Oct 04 2004 10:57:18   esevern
//added check for null travel news state and corrected setting of travel news state values from retrieved user preferences
//
//   Rev 1.23   Sep 28 2004 10:36:38   RGeraghty
//Fix for IR1657 - check for TravelNewsState being null
//
//   Rev 1.22   Sep 06 2004 21:09:24   JHaydock
//Major update to travel news
//
//   Rev 1.21   Aug 02 2004 13:38:36   COwczarek
//Replace hardcoded key values with constants defined in ProfileKeys class.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.20   Jul 12 2004 19:01:42   JHaydock
//DEL 5.4.7 Merge: IR 1116
//
//   Rev 1.19   May 18 2004 13:31:22   jbroome
//IR864 Retaining values when Help is displayed.
//
//   Rev 1.18   Apr 28 2004 16:20:08   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.17   Mar 17 2004 14:26:40   CHosegood
//Now uses jpstd.css instead of jp.css
//Resolution for 635: Page corruption on Travel News page when using Netscape
//
//   Rev 1.16   Mar 12 2004 14:29:18   rgreenwood
//Added public method DisableAllControls to enable controls to be disabled when Serco Dummy file is present in TransientPortal DB (IR626)
//
//   Rev 1.15   Dec 16 2003 17:41:04   asinclair
//Added Alt tags
//
//   Rev 1.14   Dec 15 2003 12:16:00   JHaydock
//Populate method added to ShowNewsControl for TravelNews page to call after language has switched to Welsh
//
//   Rev 1.13   Nov 19 2003 16:23:48   passuied
//added public properties to acces drop down lists and applied new style to help label
//Resolution for 248: Help Label on Travel News
//
//   Rev 1.12   Nov 18 2003 10:59:20   esevern
//fix for IR249 - added check for existing travel news preferences before using them
//Resolution for 249: Null Reference Exception when user has no travel news preferences
//
//   Rev 1.11   Oct 30 2003 18:33:14   JMorrissey
//Updated after FxCop run
//
//   Rev 1.10   Oct 30 2003 16:59:32   JMorrissey
//Added public properties to hold user's preferences for drop downs. Moved code from OnLoad to InitialiseComponent method (called via OnInit) to ensure that the control is set up before it is referenced by any page that uses it. 
//
//   Rev 1.9   Oct 27 2003 18:03:24   esevern
//bug fix
//
//   Rev 1.8   Oct 23 2003 11:25:34   esevern
//correction of user prefs property names
//
//   Rev 1.7   Oct 17 2003 17:17:02   JMorrissey
//Improved layout
//
//   Rev 1.6   Oct 16 2003 10:32:38   JMorrissey
//Updated html
//
//   Rev 1.5   Oct 14 2003 14:39:22   JMorrissey
//Layout updated. Styles need to be corrected.
//
//   Rev 1.4   Oct 08 2003 14:15:20   esevern
//added check for isPostBack on Page_Load
//
//   Rev 1.3   Oct 07 2003 19:12:40   esevern
//added setting of text with lang. res. man.
//
//   Rev 1.2   Sep 25 2003 09:52:56   esevern
//added first cut save/load news preferences
//
//   Rev 1.1   Sep 24 2003 15:55:24   esevern
//added ui elements
//
//   Rev 1.0   Sep 23 2003 17:08:28   esevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using TransportDirect.Common.ResourceManager;
    using System.Data;
    using System.Drawing;
    using System.Collections;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using TransportDirect.Common;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.SessionManager;
    using TransportDirect.UserPortal.DataServices;
    using TransportDirect.UserPortal.TravelNews;
    using TransportDirect.UserPortal.Web.Events;
    using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.TravelNewsInterface;

    using TransportDirect.UserPortal.Resource;
    using TransportDirect.UserPortal.Web.Adapters;
    using System.Web.Script.Serialization;

    /// <summary>
    ///	TDUserControl responsible for the display and retrieval 
    ///	of a logged in user's travel news preferences.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class ShowNewsControl : TDUserControl, ILanguageHandlerIndependent
    {
        #region Instance Members

        protected System.Web.UI.WebControls.Panel transportPanel;
        protected System.Web.UI.HtmlControls.HtmlTableRow searchPhraseRow;
        protected SimpleDateControl dateSelect;

        private IDataServices populator;
        private TravelNewsState travelNewsState;
        private bool userAuthenticated;
        private GenericDisplayMode transportDisplayMode;
        private GenericDisplayMode delayDisplayMode;
        private GenericDisplayMode searchPhraseDisplayMode;
        private GenericDisplayMode regionDisplayMode;
        private bool showSearchInputError = false;
        // CCN 0427 moving Region selector drop down to show news control
        private string selectedRegionId;

        // CCN 0427 All uk option for Region selector drop down
        private bool allowAllUKOption = true;

        // CCN 0427 back button moved from map region select control to shownewscontrol
        private bool isBackButtonVisible = true;

        private const string scriptTravelNew = "TravelNews";

        /// <summary>
        /// CCN 0421 new back button event
        /// CCN 0427 back button moved to shownewscontrol
        /// </summary>
        public event EventHandler BackButtonClicked;

        /// <summary>
        /// Event raised when the user clicks the Ok button.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Event raised when the selected region is changed using either the dropdown or the map
        /// </summary>
        public event EventHandler SelectedRegionChanged;

        #endregion

        #region Page event handlers

        protected void Page_Init(object sender, System.EventArgs e)
        {
            //Fix for IR2687 to detect postbacks for the Non-JavaScript implementation of the ImageMapControl   
            if (this.Request.QueryString["SelectedRegion"] != null)
                ((TDPage)this.Page).IsReentrant = true;

        }
        /// <summary>		
        /// Sets all instruction/label/error message text in the current language. 
        /// Checks if the user is authenticated and displays ui elements accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            //if (!Page.IsPostBack)
                PopulateDropDowns();
        }

        /// <summary>
        /// Handler for the prerender method. Sets control visibility and values
        /// </summary>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            if (allowAllUKOption)
                AddAllUKItem();
            else
                RemoveAllUKItem();

            // Re-populate drop downs as items could change
            SetStaticText();

            // Set values in each drop down list, according to TravelNewsState
            if (travelNewsState != null)
            {
                if (TransportDisplayMode != GenericDisplayMode.Ambiguity)
                    populator.Select(transportDropList, Converting.ToString(travelNewsState.SelectedTransport));

                populator.Select(delaysDropList, Converting.ToString(travelNewsState.SelectedDelays));

                //ccn 0421 selecting incidentTypeDropList value with the Incident Type value in travel news state
                populator.Select(incidentTypeDropList, Converting.ToString(travelNewsState.SelectedIncidentType));

                // CCN 0421 setting backbutton visible property 
                // CCN 0427 back button moved to shownewscontrol
                //backButton.Visible = IsBackButtonVisible;

                IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
                ds.Select(regionsList, ds.GetResourceId(DataServiceType.NewsRegionDrop, selectedRegionId));

                if ((dateSelect.Current != null) && (DateDisplayMode != GenericDisplayMode.Ambiguity))
                    dateSelect.Current = travelNewsState.SelectedDate;
            }
            else
            {
                transportDropList.SelectedIndex = 0;
                delaysDropList.SelectedIndex = 0;
                backButton.Visible = false;

                dateSelect.Current = TDDateTime.Now;
            }

            // showing/hiding warning label row depending on the value of ShowSearchInputError property value
            warningLabelRow.Visible = ShowSearchInputError;

            switch (transportDisplayMode)
            {
                case GenericDisplayMode.Normal:
                    transportRow.Attributes.Remove("class");
                    break;
                case GenericDisplayMode.Ambiguity:
                    transportRow.Attributes.Add("class", "alerterror");
                    break;
                case GenericDisplayMode.ReadOnly:
                    transportRow.Attributes.Remove("class");
                    break;
            }
            switch (delayDisplayMode)
            {
                case GenericDisplayMode.Normal:
                    delayRow.Attributes.Remove("class");
                    break;
                case GenericDisplayMode.Ambiguity:
                    delayRow.Attributes.Add("class", "alerterror");
                    break;
                case GenericDisplayMode.ReadOnly:
                    delayRow.Attributes.Remove("class");
                    break;
            }

            switch (searchPhraseDisplayMode)
            {
                case GenericDisplayMode.Normal:
                    searchPhraseRow.Attributes.Remove("class");
                    break;
                case GenericDisplayMode.Ambiguity:
                    searchPhraseRow.Attributes.Add("class", "alerterror");
                    break;
                case GenericDisplayMode.ReadOnly:
                    searchPhraseRow.Attributes.Remove("class");
                    break;
            }

            switch (regionDisplayMode)
            {
                case GenericDisplayMode.Normal:
                    regionsCell.Attributes.Remove("class");
                    break;
                case GenericDisplayMode.Ambiguity:
                    regionsCell.Attributes.Add("class", "alerterror");
                    break;
                case GenericDisplayMode.ReadOnly:
                    regionsCell.Attributes.Remove("class");
                    break;
            }

            // Show the 'Save these details' check box if user is logged in
            saveCheckBox.Visible = userAuthenticated;

            #region Landing Functionality
            if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck] == true)
            {
                this.dateSelect.Current = TDDateTime.Now;
                //Commit Landing Values by initiating OnClick event.
                OnClick();
            }
            #endregion

            RegisterJavascript();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Populates labels from the resource file
        /// </summary>
        private void SetStaticText()
        {
            transportLabel.Text = GetResource("ShowNewsControl.transportLabel");
            delaysLabel.Text = GetResource("ShowNewsControl.delaysLabel");
            futureIncidentsLabel.Text = GetResource("ShowNewsControl.futureIncidentsLabel");
            showButton.Text = GetResource("ShowNewsControl.ShowButton.Text");
            saveCheckBox.Text = GetResource("ShowNewsControl.saveCheckBox");
            dateSelect.HeadingLabel.Text = GetResource("DateSelectControl.dateLabel");
            searchTitleLabel.Text = GetResource("ShowNewsControl.TravelNewsSearchTitle");
            searchExampleLabel.Text = GetResource("ShowNewsControl.TravelNewsSearchExample");
            incidentTypeLabel.Text = GetResource("ShowNewsControl.incidentTypeLabel");
            //CCN 0427 White labelling moved reagion selector dropdown to show news control.
            headingRegion.Text = GetResource("ShowNewsControl.headingRegion");
            // new lable to show title "Filter travel news by:" CCN 0421
            filterTitleLabel.Text = GetResource("ShowNewsControl.filterTitleLabel");

            // new lable for warning when switching to map view
            warningLabel.Text = GetResource("ShowNewsControl.warningLabel");

            filterTravelNewsLabel.Text = GetResource("ShowNewsControl.filterTravelNewsLabel");

            // CCN 0427 back button moved from mapregionselectcontrol to shownewscontrol
            backButton.Text = GetResource("MapRegionSelectControl.backButton.Text");
        }

        /// <summary>
        /// Populates drop down lists from data services
        /// </summary>
        private void PopulateDropDowns()
        {
            int indextransportDropList = transportDropList.SelectedIndex;
            int indexdelaysDropList = delaysDropList.SelectedIndex;
            int indexincidentTypeDropList = incidentTypeDropList.SelectedIndex;
            int indexregionsList = regionsList.SelectedIndex;

            // Populate the drop down lists using DataServices			
            populator.LoadListControl(DataServiceType.NewsTransportDrop, transportDropList);
            populator.LoadListControl(DataServiceType.NewsShowTypeDrop, delaysDropList);
            populator.LoadListControl(DataServiceType.NewsIncidentTypeDrop, incidentTypeDropList);

            // Load dropdown
            IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            ds.LoadListControl(DataServiceType.NewsRegionDrop, regionsList);

            //assigning back the selected index of dropdownlist
            transportDropList.SelectedIndex = indextransportDropList;
            delaysDropList.SelectedIndex = indexdelaysDropList;
            incidentTypeDropList.SelectedIndex = indexincidentTypeDropList;
            regionsList.SelectedIndex = indexregionsList;
        }

        /// <summary>
        /// Event handler for ShowButton's click event
        /// Updates internal values and raises public event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showButton_Click(object sender, EventArgs e)
        {
            OnClick();
        }

        /// <summary>
        /// Used to raise the Click event.
        /// </summary>
        protected void OnClick()
        {
            // Raise public event.
            EventHandler e = Click;
            if (e != null)
            {
                e(this, EventArgs.Empty);    // raise external event
            }
        }

        /// <summary>
        /// Adds the "All UK" item to the list
        /// </summary>
        private void AddAllUKItem()
        {
            DSDropItem allUKItem = GetDataServicesAllUKItem();

            // Verify that the dropdown list contains this option
            ListItem all = regionsList.Items.FindByValue(allUKItem.ResourceID);
            if (all == null)
                regionsList.Items.Insert(0, new ListItem(GetResource(allUKItem.ResourceID), allUKItem.ResourceID));
        }

        /// <summary>
        /// Removes the "All UK" item from the list
        /// </summary>
        private void RemoveAllUKItem()
        {
            DSDropItem allUKItem = GetDataServicesAllUKItem();

            // Verify that the dropdown list contains this option
            ListItem all = regionsList.Items.FindByValue(allUKItem.ResourceID);
            if (all != null)
            {
                if (all.Selected == true)
                {
                    // Change the selected item to the first region in the list
                    IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
                    selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.Items[1].Value);
                }
                regionsList.Items.Remove(all);
            }
        }

        /// <summary>
        /// Retrieves the "All UK" item from Data Services
        /// </summary>
        /// <returns></returns>
        private DSDropItem GetDataServicesAllUKItem()
        {
            IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            ArrayList items = ds.GetList(DataServiceType.NewsRegionDrop);
            foreach (DSDropItem current in items)
            {
                if (current.ItemValue == "0")
                    return current;
            }
            return null;
        }

        /// <summary>
        /// Registers page and page controls javascript
        /// </summary>
        private void RegisterJavascript()
        {
            // Register the scripts needed only if user has Javascript enabled
            TDPage thePage = this.Page as TDPage;

            if (thePage != null && thePage.IsJavascriptEnabled)
            {
                // Get the global script repository
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string okScript = string.Format("travelNewsHelper.showSingleIncident(false);SetTravelNews({0});return false;", serializer.Serialize(TravelNewsHelper.GetAllRegionCoordinates()));

                showButton.OnClientClick = okScript;

                string backButtonScript = "ShowPreviousNews(); return false;";

                backButton.OnClientClick = backButtonScript;

                // Register the mapping api call script
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptTravelNew, repository.GetScript(scriptTravelNew, thePage.JavascriptDom));
            }

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method disables all controls
        /// </summary>
        public void DisableAllControls()
        {
            transportDropList.Enabled = false;
            delaysDropList.Enabled = false;
            showButton.Visible = false;
            dateSelect.Visible = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/write property allows session data 
        /// to be set outside this control
        /// </summary>
        public TravelNewsState ShowNewsState
        {
            get { return travelNewsState; }
            set { travelNewsState = value; }
        }

        /// <summary>
        /// Read/write property allows user authentication 
        /// to be evaluated and set outside this control
        /// </summary>
        public bool Authenticated
        {
            get { return userAuthenticated; }
            set { userAuthenticated = value; }
        }

        /// <summary>
        /// Read/write property to show/hide warning label CCN 0421
        /// </summary>
        public bool ShowSearchInputError
        {
            get { return showSearchInputError; }
            set { showSearchInputError = value; }
        }

        /// <summary>
        /// Read/write property that exposes the checked 
        /// status of the saveCheckedBox control
        /// </summary>
        public bool SavePreferences
        {
            get { return saveCheckBox.Checked; }
        }

        /// <summary>
        /// Read/write property that exposes 
        /// the selected value of the transportDropList 
        /// </summary>
        public TransportType SelectedTransport
        {
            get { return Parsing.ParseTransportType(transportDropList.SelectedValue); }
            set { populator.Select(transportDropList, Converting.ToString(value)); }
        }

        /// <summary>
        /// Read/write property that exposes 
        /// the selected value of the delaysDropList 
        /// </summary>
        public DelayType SelectedDelays
        {
            get { return Parsing.ParseDelayType(delaysDropList.SelectedValue); }
            set { populator.Select(delaysDropList, Converting.ToString(value)); }
        }

        /// <summary>
        /// Read/write property that exposes 
        /// the selected value of the incidentTypeDropList 
        /// </summary>
        public IncidentType SelectedIncidentType
        {
            get { return Parsing.ParseIncidentType(incidentTypeDropList.SelectedValue); }
            set { populator.Select(incidentTypeDropList, Converting.ToString(value)); }
        }

        /// <summary>
        /// Read/write property that exposes the selected value 
        /// of the TriStateDateControl as TDDateTime
        /// </summary>
        public TDDateTime SelectedDate
        {
            get { return dateSelect.Current; }
            //Fix for IR2687
            set { dateSelect.Current = value; }
        }

        /// <summary>
        /// Sets the display mode for the date control. Note that GenericDisplayMode.ReadOnly is 
        /// treated as GenericDisplayMode.Normal
        /// </summary>
        public GenericDisplayMode DateDisplayMode
        {
            set { dateSelect.DisplayMode = value; }
            get { return dateSelect.DisplayMode; }
        }

        /// <summary>
        /// Sets the display mode for the transport control. Note that GenericDisplayMode.ReadOnly is 
        /// treated as GenericDisplayMode.Normal
        /// </summary>
        public GenericDisplayMode TransportDisplayMode
        {
            set { transportDisplayMode = value; }
            get { return transportDisplayMode; }
        }

        /// <summary>
        /// Sets the display mode for the Delay control. Note that GenericDisplayMode.ReadOnly is 
        /// treated as GenericDisplayMode.Normal
        /// </summary>
        public GenericDisplayMode DelayDisplayMode
        {
            set { delayDisplayMode = value; }
            get { return delayDisplayMode; }
        }

        /// <summary>
        /// Read/write property controlling how the dropdown and imagemap are displayed
        /// </summary>
        public GenericDisplayMode RegionDisplayMode
        {
            get { return regionDisplayMode; }
            set { regionDisplayMode = value; }
        }

        /// <summary>
        /// Sets the display mode for the Search Phrase control. Note that GenericDisplayMode.ReadOnly is 
        /// treated as GenericDisplayMode.Normal
        /// </summary>
        public GenericDisplayMode SearchPhraseDisplayMode
        {
            set { searchPhraseDisplayMode = value; }
            get { return searchPhraseDisplayMode; }
        }

        /// <summary>
        /// Read/Write. Sets search phrase text.
        /// </summary>
        public string SearchPhrase
        {
            set { searchInputText.Text = value; }
            get { return searchInputText.Text; }
        }

        /// <summary>
        /// Read/write property holding the ID of the selected region
        /// </summary>
        public string SelectedRegionId
        {
            get
            {
                if (selectedRegionId == null)
                {
                    IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
                    selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.SelectedValue);
                }
                return selectedRegionId;
            }
            set { selectedRegionId = value; }
        }

        /// <summary>
        /// Read/write property setting the visiblity of back button
        /// </summary>
        public bool IsBackButtonVisible
        {
            get { return isBackButtonVisible; }
            set { isBackButtonVisible = value; }

        }

        /// <summary>
        /// Read/write property controlling whether or not the "All UK" option is available in the 
        /// dropdown
        /// </summary>
        public bool AllowAllUKOption
        {
            get { return allowAllUKOption; }
            set { allowAllUKOption = value; }
        }

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //			
            ExtraWiringEvents();
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }

        /// <summary>
        /// Extra event subscription
        /// </summary>
        private void ExtraWiringEvents()
        {
            showButton.Click += new EventHandler(this.showButton_Click);
            regionsList.SelectedIndexChanged += new EventHandler(regionsList_SelectedIndexChanged);
            backButton.Click += new EventHandler(backButton_Click);
        }
        #endregion

        #region Control even handlers
        /// <summary>
        /// Handles the SelectedIndexChanged event for the regions list. The new selected region id
        /// is stored in the selectedRegionId variable, and the SelectedRegionChanged event is raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void regionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ActionSelectedRegionChanged();
        }

        /// <summary>		
        /// Commit a change to the Region value, and force appropriate change in appearance by raising changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActionSelectedRegionChanged()
        {
            IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            selectedRegionId = ds.GetValue(DataServiceType.NewsRegionDrop, regionsList.SelectedValue);
            OnSelectedRegionChanged();
        }

        /// <summary>
        /// Raises the SelectedRegionChanged event
        /// </summary>
        protected void OnSelectedRegionChanged()
        {
            EventHandler h = SelectedRegionChanged;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        /// <summary>
        /// handles the onclick event for the back button and raises BackButtonClicked event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked();
        }

        protected void OnBackButtonClicked()
        {
            EventHandler h = BackButtonClicked;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        #endregion

    }
}
