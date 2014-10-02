// ***********************************************
// NAME 		: TravelNews.aspx.cs
// AUTHOR 		: Joe Morrissey
// DATE CREATED : 07/10/2003
// DESCRIPTION 	: Page displaying latest travel news
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/LiveTravel/TravelNews.aspx.cs-arc  $
//
//   Rev 1.31   Nov 09 2012 15:58:28   mmodi
//Added check for null news item for a travel news page request to show an incident which is no longer available
//Resolution for 5871: Problem URLS reported by SEO testing company
//
//   Rev 1.30   Oct 24 2011 10:47:04   mmodi
//Updated to display travel news toids for CJP user
//Resolution for 5758: Real Time in Car - Display TOIDs on incident popup for CJP user
//
//   Rev 1.29   Jul 28 2011 16:21:06   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.28   Sep 02 2010 13:27:22   apatel
//Changed travel news view change buttons from 2 button to single toggle button.
//Resolution for 5598: Travel news - Change two buttons to one button
//
//   Rev 1.27   Aug 25 2010 12:01:30   apatel
//Updated to initialise the map when the map view shown first on page when come from landing page.
//Resolution for 5595: Travel news map issues
//
//   Rev 1.26   Aug 24 2010 16:23:34   apatel
//Updated to initialise map completely on client side. Following is considered while making changes :
// -- Keep the existing control and hide it as it would be more error prone removing the control and its references
// -- Added divs to create the same map effect as with the old map control
//Resolution for 5595: Travel news map issues
//
//   Rev 1.25   Aug 11 2010 10:18:04   apatel
//Updated to show map to selected region when the map of specific region needs showing for logged on user
//Resolution for 5591: TravelNews preferences not saved for logged in user
//
//   Rev 1.24   Aug 02 2010 08:45:10   apatel
//Travel News page updated to show two buttons 'Show Map' and 'Show Table' outside travel news filter control instead of single toggle button to switch travel news views
//Resolution for 5578: Travel news issue with single incident  clicked from home page
//
//   Rev 1.23   Jul 20 2010 11:39:10   apatel
//updated to set the map to zoom to incident when single incident selected from home page
//Resolution for 5578: Travel news issue with single incident  clicked from home page
//
//   Rev 1.22   Jun 23 2010 09:06:12   apatel
//Updated to resolve the issue of error page display when user navigates away from travel news page and no region is selected
//Resolution for 5558: Travel News Navigation issue
//
//   Rev 1.21   Jun 01 2010 14:15:00   apatel
//Updated to implement help button functionality as the current help button doesn't do anything.
//Resolution for 5542: TravelNews help button does not work
//
//   Rev 1.20   Apr 14 2010 08:32:46   apatel
//Resolve the issue with map not centering when double click on map 
//Resolution for 5506: Travel News double click center issue
//
//   Rev 1.19   Mar 16 2010 10:41:36   apatel
//Updated to show error when invalid date input by user
//Resolution for 5458: Travel News : Invalid date issue
//
//   Rev 1.18   Feb 26 2010 16:14:52   PScott
//Meta tag and title changes on numerous pages
//RS71001 
//SCR 5408
//Resolution for 5408: Meta tags
//
//   Rev 1.17   Dec 11 2009 14:53:50   apatel
//Mapping enhancement for travelnews
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.16   Dec 11 2009 07:05:42   apatel
//updated for search phrase logic of showing error when show map button clicked
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.15   Nov 30 2009 09:58:28   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Nov 28 2009 11:27:00   apatel
//Travel News map enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Nov 26 2009 15:47:32   apatel
//TravelNews page and controls updated for new mapping functionality
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Nov 23 2009 10:29:42   mmodi
//Updated styles
//
//   Rev 1.11   Nov 20 2009 09:28:28   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 11 2009 11:53:58   MTurner
//Fixed bug with zooming to regions when using new mapping control.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 10 2009 15:07:52   MTurner
//Updated to use new mapping controls
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Sep 29 2009 11:48:26   rbroddle
//CCN 485a Travel News Parts 3 and 4 Hierarchy & Roadworks.
//Resolution for 5321: Travel News Parts 3 and 4 Hierarchy & Roadworks
//
//   Rev 1.7   Jan 30 2009 11:53:40   apatel
//SEO Change - Updated page header title to include regional text. for example "London Live Travel News Fri 30 Jan 2009 11:51 " - CCN624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.6   Jan 21 2009 09:34:18   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Jan 16 2009 10:22:20   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Jan 02 2009 15:21:58   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 30 2008 11:07:56   apatel
//Corrected Date time in heading on postback
//Resolution for 4905: Live travel news page - map is not aligned and horizontal scroll on the table
//
//   Rev 1.2   Mar 31 2008 13:26:00   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Mar 23 2008 13:50:00 mmodi
//  Updated to show single incident map view after switching view selected (when arriving from click through on homepage).
//  Updated logic with Back button clicks
//  
//  Rev Devfactory Feb 20 2008 13:50:00 apatel
//  mapregionselectcontrol is replaced by mapregioncontrol as mapregionselectcontrol being used by other pages.
//  And there is a need to remove controls from mapregionselectcontrol.
//
//  Rev Devfactory Feb 20 2008 11:11:00 apatel
//  changed the layout of control, moved the shownewscontrol and mapregionselect controls. Also, stretch the controls
//  to cover all right hand area. put a title image for page. 
//
//  Rev Devfactory Feb 04 2008 17:00:00 mmodi
//Updated display of errors logic for Search text and Region combinations
//
//   Rev Devfactory Jan 26 2008 08:33 apatel
//   CCN 0421 modified UpdateMapDisplay method for ESRI dll related work for setting 
//   map's IncidentType.
//
//   Rev Devfactory Jan 12 2008 22:49 apatel
//   CCN 0421 TravelNewsStateNotChanged() method added to varify if TravelNewsState is changed 
//   and UserSelectionChanged method changed to make back button invisible if the region error is displayed.
//
//   Rev Devfactory Jan 12 2008 08:04 apatel
//   CCN 0421 Page_Load and UserSelectionChanged() methods modified to set ShowNewsControl's ShowSearchInputError property
//
//   Rev Devfactory Jan 09 2008 10:26 aahmed
//   CCN 0421// CCN 0421 shoeSingleIncident flag is only reset when another button is selected
//
//   Rev Devfactory Jan 09 2008 09:40 aahmed
//   CCN 0421 in UpDateGridDisplay() to allow filters to be applied to the table and map removed following code travelNewsState.SelectedRegion = "All"; from the if statement shownewscontrol.searchphrase
//   show only one incident if the showSingleIncident flag is true 
//
//   Rev Devfactory Jan 09 2008 10:33 aahmed
//   CCN 0421 in UserSelectionChangedHandler() set showSingleIncident flag is false 
//
//   Rev Devfactory Jan 09 2008 09:40 aahmed
//   CCN 0421 in UpDateGridDisplay() to allow filters to be applied to the table and map removed following code travelNewsState.SelectedRegion = "All"; from the if statement shownewscontrol.searchphrase
//
//   Rev 1.0   Nov 08 2007 13:31:58   mturner
//Initial revision.
//
//   Rev Devfactory Jan 07 2008 12:00:00 apatel
//   Adding back button functionality to TravelNew page.
//
//   Rev Devfactory Jan 07 2008 09:05:56 apatel
//   CCN 0421 - Removed the duplicating Travel News page Title. Added new resource string in resorece file 
//   TravelNews.lblCurrentViewHoverMessage to show Hover on symbols message.
//
//   Rev Devfactory Jan 04 2008 15:26:34 apatel
//   CCN 0421 - Removed the duplicating date showing in the box below pagetitle and 
//   made the selected filter date showing next to title 
//
//   Rev Devfactory Jan 04 2008 14:09:23 apatel
//   CCN 0421 - Modifications made to improve left hand box visibility. Removed top left hand title and change
//   the positiong of mapregion and filter search boxes.
//
//   Rev 1.74   Aug 20 2007 11:05:18   pscott
//IR 4479 - Travel News freeform text search changes
//
//   Rev 1.73   Jan 19 2007 13:41:32   build
//Automatically merged from branch for stream4329
//
//   Rev 1.72.1.0   Jan 12 2007 14:50:34   tmollart
//Modifications for travel news search. Added code to populate or de-populate the search state object.
//Resolution for 4329: Travel News Updates and Search
//
//   Rev 1.72   Aug 09 2006 11:52:58   mmodi
//Added checks for page language changed to prevent re-intialisation
//Resolution for 4146: Selecting Welsh link on Travel news map reverts view back to table
//
//   Rev 1.71   May 26 2006 15:19:20   mmodi
//IR4099 Change to remove the Previous map button
//Resolution for 4099: Del 8.2 - Live Travel Previous map button does nothing
//
//   Rev 1.70   Apr 21 2006 10:18:08   mtillett
//Remove restirtcion on display of serious incidents only when filters selected by user valid
//Resolution for 3956: DN091 Travel News:  Non-serious incidents not displayed after critical incident selected from the home page
//
//   Rev 1.69   Apr 20 2006 16:04:16   mtillett
//Move logic to select region and transport type so that not run for critical incidents
//Resolution for 3954: DN091 Travel News Updates:  Not all serious incidents displayed when critical incident is clicked from Homepage
//
//   Rev 1.68   Apr 05 2006 17:38:14   AViitanen
//Set SelectedDelays to Major when coming from the homepage.
//Resolution for 3793: DN091 Travel News Updates:  Strange behaiour when user is logged in with saved preferences and clicks on incident on Home Page
//
//   Rev 1.67   Mar 31 2006 15:59:56   AViitanen
//Amended to set dropdown to region of the incident when clicked through. 
//
//   Rev 1.66   Mar 28 2006 10:20:34   AViitanen
//Manual merge for travel news (stream0024)
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.65   Mar 22 2006 12:57:00   rgreenwood
//IR 3623 Live Travel News zoom control fix
//Resolution for 3623: Homepage phase 2: Zoom level not highlighted when clicking through to travel incident map
//
//   Rev 1.64   Feb 24 2006 10:13:20   rgreenwood
//Stream 3129: Manual merge changes
//
//   Rev 1.63   Feb 21 2006 11:37:36   aviitanen
//Merge from stream0009 
//
//   Rev 1.62   Feb 10 2006 11:16:40   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.61   Jan 24 2006 14:41:42   rhopkins
//Added call to method to use clientside JavaScript refresh if server thinks JavaScript not enabled.
//Resolution for 3322: DN077 -  Missing button on initial landing on Travel news page
//
//   Rev 1.60   Dec 20 2005 11:15:06   jgeorge
//Page landing modifications.
//Resolution for 3320: DN077 - Travel News Table Map not shown on first landing on page
//
//   Rev 1.59   Nov 16 2005 09:52:54   AViitanen
//Changed to show 'show map' button text.
//Resolution for 3031: UEE: Untitled button on Live Travel News.
//
//   Rev 1.58   Nov 09 2005 15:16:18   ECHAN
//update to code review comments
//
//   Rev 1.57   Nov 09 2005 15:09:40   ECHAN
//Fix for code review comments #4
//
//   Rev 1.56   Nov 04 2005 15:29:10   ECHAN
//merged stream2816 for DEL8
//
//   Rev 1.55   Oct 06 2005 15:13:00   kjosling
//Merged with stream 2817. 
//Resolution for 2817: DEL 7.1.4 Stream Label
//
//   Rev 1.54.1.0   Oct 03 2005 14:21:38   CRees
//Updated for new Esri dll, and to add warning for recent delay selected for date <> today.
//
//   Rev 1.54   Sep 23 2005 17:59:06   CRees
//Added Selected Date display to title bar of Table and Map. Hid Switch to Map/Table image when no travel news available. 
//
//   Rev 1.53   Sep 23 2005 11:44:50   jbroome
//Updated to use new TravelNewMapKeyControl
//Resolution for 2793: Travel News Printable Map View
//
//   Rev 1.52   Sep 14 2005 11:32:02   asinclair
//Code tidy up
//
//   Rev 1.51   Sep 13 2005 11:34:44   asinclair
//Added Help box and instructional text
//Resolution for 2723: DN018: Help Buttons and Instructional Text on Live Travel page
//
//   Rev 1.50   Sep 02 2005 17:26:28   RWilby
//Fix for IR2687 to handle the non-javascript implementation of the ImageMapcontrol
//Resolution for 2687: DN018 - When map region is selected the date is re-set (IE 5.5 only?)
//
//   Rev 1.49   Sep 02 2005 11:23:48   mguney
//If javascript is not enabled, the switch (table/map) button is not going to be displayed.
//Resolution for 2746: DN018 - "Show map" button is displayed when JavaScript is disabled
//
//   Rev 1.48   Sep 01 2005 09:04:34   rgreenwood
//IR2716: Added LoadTravelNewsState() call to Page_PreRender to ensure User Prefs for MapRegionSelectControl are loaded when the page is accessed.
//Resolution for 2716: DN018 - User selection criteria is not saved for a registered user
//
//   Rev 1.47   Aug 31 2005 15:40:52   jgeorge
//Moved call to StartMap so that it only takes place if in map view.
//Resolution for 2734: DN018 - Poor performance of the Travel News page
//
//   Rev 1.46   Aug 24 2005 14:45:20   jgeorge
//Correctly set IncidentDisplayType on detail control
//Resolution for 2692: DN018 - Summary details are not displayed for an incident
//
//   Rev 1.45   Aug 18 2005 15:37:42   jgeorge
//Implemented TODO comment
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.44   Aug 18 2005 11:29:10   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.43.1.4   Aug 04 2005 11:30:10   jgeorge
//Additional changes for incident mapping
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.43.1.3   Jul 08 2005 15:39:04   jbroome
//Work in progress
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.43.1.2   Jul 08 2005 14:59:38   jbroome
//Added mapping controls and wired up events
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.43.1.1   Jul 01 2005 17:49:08   jmorrissey
//Update to layout
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.43.1.0   Jul 01 2005 11:13:28   jmorrissey
//New version of page that adds incident mapping
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.43   Apr 15 2005 12:48:30   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.42   Dec 16 2004 15:23:38   passuied
//refactoring the TravelNews component and changes to the clients
//
//   Rev 1.41   Sep 29 2004 16:42:14   jmcallister
//Completed. Return to Traffic News screen now returns traffic news to default location of "All".
//
//   Rev 1.40   Sep 06 2004 21:09:28   JHaydock
//Major update to travel news
//
//   Rev 1.39   Aug 18 2004 11:58:00   JHaydock
//Removed explicit table definition from order by clause for new Travel News DB
//
//   Rev 1.38   Jul 20 2004 09:27:04   CHosegood
//Added recent delays combo box option and removed sort by ability on travelnews
//Resolution for 1168: Add 'recent delays' pulldown to travel news and remove the ability to sort headings
//
//   Rev 1.37   Jul 12 2004 19:06:28   JHaydock
//DEL 5.4.7 Merge: IR 1120
//
//   Rev 1.36   Jun 11 2004 15:24:06   jgeorge
//IR966
//
//   Rev 1.35   May 26 2004 10:21:04   jgeorge
//IR954 fix
//
//   Rev 1.34   May 18 2004 13:30:26   jbroome
//IR864 Retaining values when Help is displayed.
//
//   Rev 1.33   Apr 15 2004 10:37:30   ESevern
//if current time minutes less than 10, add a leading zero to display
//Resolution for 750: Live Travel news - time - Leading zero for minutes not shown
//
//   Rev 1.32   Mar 24 2004 15:59:06   ESevern
//DEL5.2 QA changes
//Resolution for 670: DEL5.2 QA Changes for Live Travel News
//
//   Rev 1.31   Mar 23 2004 15:35:24   AWindley
//DEL5.2 Updates for Alt text
//
//   Rev 1.30   Mar 17 2004 14:26:44   CHosegood
//Now uses jpstd.css instead of jp.css
//Resolution for 635: Page corruption on Travel News page when using Netscape
//
//   Rev 1.29   Mar 12 2004 16:40:54   jmorrissey
//moved call to SaveData() to end of PageLoad method so that screen labels and values are always saved;
//
//   Rev 1.28   Mar 12 2004 14:06:32   rgreenwood
//Updated so that when no travel news is available, Travel News grid is hidden and a message is displayed.
//
//   Rev 1.27   Mar 01 2004 14:02:08   asinclair
//Removed printer hyperlink for Del 5.2
//
//   Rev 1.26   Jan 13 2004 09:06:34   asinclair
//Added fix for IR328, 4 - Message now displayed on Welsh pages stating news only in English
//
//   Rev 1.25   Dec 18 2003 14:11:40   asinclair
//Fix for IRs 530 and 560
//
//   Rev 1.24   Dec 15 2003 12:16:02   JHaydock
//Populate method added to ShowNewsControl for TravelNews page to call after language has switched to Welsh
//
//   Rev 1.23   Dec 11 2003 18:11:16   asinclair
//Added alt tags to the drop down buttons
//
//   Rev 1.22   Dec 02 2003 10:24:40   JHaydock
//Update to welsh language access to travel news grid
//
//   Rev 1.21   Dec 01 2003 19:54:02   JHaydock
//Welsh support for travel news grid
//
//   Rev 1.20   Nov 19 2003 16:21:14   passuied
//created a new style for help on travel news
//Resolution for 248: Help Label on Travel News
//
//   Rev 1.19   Nov 16 2003 13:15:28   hahad
//Change Title Tag so that it reads value from asp:literal rather then value being hardcoded within the page
//
//   Rev 1.18   Nov 07 2003 10:07:34   JMorrissey
//updated how the Navigate URLS are set for the printer friendly hyperlinks
//
//   Rev 1.17   Oct 30 2003 18:43:44   JMorrissey
//Updated to show preferred data correctly when a user is logged 
//
//   Rev 1.16   Oct 23 2003 16:19:36   MTurner
//Regional Image names changed to reflect new images
//
//   Rev 1.15   Oct 22 2003 18:30:46   JMorrissey
//updated after PVCS comments
//
//   Rev 1.14   Oct 22 2003 13:21:08   JMorrissey
//Updated PopulateNewsControl to use IsPostBack to determine course of action

using System;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;

using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

using TransportDirect.UserPortal.TravelNewsInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Text;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Page displaying latest travel news
    /// </summary>
    public partial class TravelNews : TDPage
    {
        #region Instance Members

        // HTML Controls
        //CR Added for IR2799
        // End CR IR2799
        protected System.Web.UI.WebControls.Label labelOverviewMap;
        protected System.Web.UI.WebControls.Label lblWelshOnly;
        protected System.Web.UI.WebControls.Image imgRegionMap;

        // User controls
        protected TransportDirect.UserPortal.Web.Controls.ShowNewsControl ShowNewsControl;
        protected HeaderControl headerControl;
        protected TransportDirect.UserPortal.Web.Controls.TravelNewsDetailsControl TravelNewsDetails;
        protected TransportDirect.UserPortal.Web.Controls.MapControl TravelNewsMap;
        protected TransportDirect.UserPortal.Web.Controls.MapTravelNewsControl MapTravelNews;
        protected TransportDirect.UserPortal.Web.Controls.MapZoomControl TravelNewsMapZoom;
        protected TransportDirect.UserPortal.Web.Controls.HelpCustomControl HelpControl;
        protected TransportDirect.UserPortal.Web.Controls.MapRegionControl regionSelector;
        protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorControl;
        protected TransportDirect.UserPortal.Web.Controls.TravelNewsMapKeyControl MapKeyControl;

        // Flags used for initialising map
        private bool switchingView;
        private bool zoomToRegion = true;
        private bool zoomToIncident;
        private bool mapToRefresh = false;

        private bool regionValid = true;
        private bool regionAndSearchTextValid = true;
        private bool searchTextValid = true;
        protected TransportDirect.UserPortal.Web.Controls.HelpLabelControl Helplabelcontrol1;
        private bool dateValid = true;
        private bool delayValid = true;
        private bool incidentPopulated = false;
        private bool incidentSelected = false;
        private string incidentID;
        private bool showSingleIncident = false;

        private const string scriptCommonAPI = "Common";

        #endregion

        #region Page event handlers

        /// <summary>
        /// Sets the PageId and wires up additional events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            pageId = PageId.TravelNews;

            this.ShowNewsControl.Click += new System.EventHandler(this.UserSelectionChangedHandler);
            this.TravelNewsDetails.IncidentClicked += new CommandEventHandler(this.TravelNewsDetails_IncidentClicked);
            this.regionSelector.SelectedRegionChanged += new EventHandler(this.UserSelectionChangedHandler);

            this.ButtonSwitchView.Click += new EventHandler(ButtonSwitchView_Click);
            
            // CCN 0427 back button moved to shownewscontrol 
            this.ShowNewsControl.BackButtonClicked += new EventHandler(regionSelector_BackButtonClicked);

            //add the javascript resources for translation of the popups in the map
            SetupResourcesInClientScript();
        }

        /// <summary>
        /// Reads/setup session data, populates data grid, initialises controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string newsUid = Request.QueryString["uid"];
            PageTitle = GetResource("TravelNews.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");	

            TravelNewsItem newsItem = TravelNewsHelper.GetTravelNewsItem(newsUid);
            lblLiveTravelNews.Text = GetResource("TravelNews.lblLiveTravelNews");

            // Setting ShowNewsControl ShowSearchInputError false - clearing the warning
            ShowNewsControl.ShowSearchInputError = false;

            // Prevent initialise of page if the user has switched language while on this page.
            // This ensures users current view and selection is retained
            if (!TravelNewsHelper.HasLanguageChanged())
            {
                // Set up initial page state
                if (!IsPostBack && !IsReentrant)
                {
                    bool isForPageLanding = TravelNewsHelper.InitialiseTravelNewsState();

                    if (newsUid != null && newsItem != null)
                    {
                        if (newsItem.SeverityLevel == SeverityLevel.Critical)
                        {
                            TravelNewsHelper.InitialiseTravelNewsState();
                            TravelNewsHelper.CurrentTravelNewsState.SelectedSeverityFilter = SeverityFilter.CriticalIncidents;
                        }
                        else
                        {
                            if (newsItem.ModeOfTransport == "Road")
                            {
                                TravelNewsHelper.CurrentTravelNewsState.SelectedTransport = TransportType.Road;
                            }
                            else
                            {
                                TravelNewsHelper.CurrentTravelNewsState.SelectedTransport = TransportType.PublicTransport;
                            }
                            //set the region
                            string[] regions = newsItem.Regions.Split(',');
                            TravelNewsHelper.CurrentTravelNewsState.SelectedRegion = regions[0];
                            incidentPopulated = true;

                            // CCN 0421 remove ShowIncident(newsUid); and mapToRefresh = false and add call to newly created method ShowIncidentInTable 
                            ShowIncidentInTable(newsUid);
                        }
                        mapToRefresh = false;

                        TravelNewsHelper.CurrentTravelNewsState.SelectedDelays = DelayType.Major;
                    }

                    // If coming in from landing page on map view, set zoomToRegion to ensure that the map
                    // is displayed properly
                    if (isForPageLanding && TravelNewsHelper.CurrentTravelNewsState.SelectedView == TravelNewsViewType.Map)
                        zoomToRegion = true;
                }

                // Prevents incident map being shown when language is switched, as user
                // by this point would have performed another action on the page.
                // Only run if newsUid is null, this retains the Incident selected from Homepage
                if (newsUid == null)
                    TravelNewsHelper.CurrentTravelNewsState.SelectedIncident = "";
            }
            else
            {
                // If user was on a Map when language changed, zoom back to the region
                if (TravelNewsHelper.CurrentTravelNewsState.SelectedView == TravelNewsViewType.Map)
                    zoomToRegion = true;

                // If user was viewing a map of incident, set flag to zoom back to selected incident
                if (TravelNewsHelper.CurrentTravelNewsState.SelectedIncident != "")
                {
                    zoomToRegion = false;
                    incidentSelected = true;
                    incidentID = TravelNewsHelper.CurrentTravelNewsState.SelectedIncident;
                }
            }

            //save current page language
            TravelNewsHelper.SavePageLanguageToSession();

            LoadTravelNewsState();

            // Set up controls
            SetUpControls();

            // Zoom to incident if user was viewing incident when language changed
            if (incidentSelected)
            {
                ShowIncident(incidentID);
                incidentSelected = false;
            }

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLiveTravel);


            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextTravelNews);
            expandableMenuControl.AddExpandedCategory("Related links");
        }



        /// <summary>
        /// PreRender event handler
        /// Updates the travelNewsState for the ShowNewsControl
        /// This may have changed since Page Load if user has 
        /// changed selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetStaticLabels();

            LoadTravelNewsState();

            // Ensure region dropdown equals map region. When arriving in single incident view default is to show All UK
            SynchronizeRegion(false);

            // Display error messages
            ArrayList errors = new ArrayList(3);

            #region Set Control display modes and get error messages
            if (dateValid)
                ShowNewsControl.DateDisplayMode = GenericDisplayMode.Normal;
            else
            {
                ShowNewsControl.DateDisplayMode = GenericDisplayMode.Ambiguity;
                errors.Add(GetResource(TravelNewsHelper.ResourceKeyInvalidDate));
            }

            if ((regionValid) && (regionAndSearchTextValid))
            {
                regionSelector.DisplayMode = GenericDisplayMode.Normal;
                ShowNewsControl.SearchPhraseDisplayMode = GenericDisplayMode.Normal;
            }
            else
            {
                regionSelector.DisplayMode = GenericDisplayMode.Ambiguity;

                // If user has attempted to switch to Map view with no region selected
                if (!regionValid)
                {
                    if (string.IsNullOrEmpty(ShowNewsControl.SearchPhrase))
                        errors.Add(GetResource(TravelNewsHelper.ResourceKeyInvalidRegion));
                    else
                    {
                        // If user has attempted to switch to Map view with no region selected and search text entered
                        ShowNewsControl.SearchPhraseDisplayMode = GenericDisplayMode.Ambiguity;
                        errors.Add(GetResource(TravelNewsHelper.InvalidRegionAndSearchPhrase));
                    }
                }

                // If user has selected a region AND search text entered display error
                if (!regionAndSearchTextValid)
                {
                    ShowNewsControl.SearchPhraseDisplayMode = GenericDisplayMode.Ambiguity;
                    errors.Add(GetResource(TravelNewsHelper.RegionSelectWithSearchPhraseEntered));
                }
            }

            if ((!searchTextValid) && (regionValid) && (regionAndSearchTextValid))
            {
                ShowNewsControl.SearchPhraseDisplayMode = GenericDisplayMode.Ambiguity;
                errors.Add(GetResource(TravelNewsHelper.SwitchToMapWithSearchPhraseEntered));
            }

            if (delayValid)
            {
                ShowNewsControl.DelayDisplayMode = GenericDisplayMode.Normal;
            }
            else
            {
                ShowNewsControl.DateDisplayMode = GenericDisplayMode.Ambiguity;
                ShowNewsControl.DelayDisplayMode = GenericDisplayMode.Ambiguity;
                errors.Add(GetResource(TravelNewsHelper.ResourceKeyInvalidDelay));
            }

            ShowNewsControl.TransportDisplayMode = GenericDisplayMode.Normal;
            #endregion

            #region SearchPhraseError Panel
            searchPhraseErrorControl.Type = ErrorsDisplayType.Error;
            searchPhraseErrorControl.ErrorStrings = new string[1] { GetResource(TravelNewsHelper.SwitchToMapWithSearchPhraseEntered) };
            #endregion

            #region Date Error Panel
            dateErrorControl.Type = ErrorsDisplayType.Error;
            dateErrorControl.ErrorStrings = new string[1] { GetResource(TravelNewsHelper.ResourceKeyInvalidDate) };
            #endregion

            #region Show/Hide errors

            if (TravelNewsHelper.CurrentTravelNewsState.SelectedView == TravelNewsViewType.Map)
            {
                CheckJavascriptEnabled();
            }

            if (errors.Count > 0)
            {
                errorPanel.Visible = true;
                errorControl.Type = ErrorsDisplayType.Error;
                errorControl.ErrorStrings = (string[])errors.ToArray(typeof(string));
                errorControl.ReferenceNumber = string.Empty;
            }
            else if (!this.IsJavascriptEnabled)
            {
                errorControl.Visible = errorControl.ErrorStrings.Length > 0;
            }
            else
            {
                errorPanel.Visible = false;
            }
            #endregion

            #region Show/Hide help panel

            //HelpPanel.Visible = helpLabelTravelNews.Visible || helpLabelTravelNewsNonMap.Visible;
            if (!this.IsJavascriptEnabled)
            {
                helpPanelTravelNews.Visible = helpLabelTravelNews.Visible;
                helpPanelTravelNewsNonMap.Visible = helpLabelTravelNewsNonMap.Visible;
            }

            #endregion

            SetPrintableControl();

            if (mapToRefresh && TravelNewsHelper.ShowMap)
            {
                TravelNewsMap.Map.Refresh();
            }

            if (!TravelNewsHelper.ShowMap)
            {
                InitializeMap();
                // Refresh the map display
                MapTravelNews.RefreshMap();

            }
            

            RegisterJavascript();

        }

        #endregion

        #region Private methods

        /// <summary>
        /// Updates the text on labels from the resource file.
        /// </summary>
        private void SetStaticLabels()
        {
            // Display correct time and date
            // CCN 0421 showing date selected in filter or default today's date next to page title
            // and removed the duplicating date showing below in a box
            lblDateTime.Text = DisplayFormatAdapter.StandardDateAndTimeFormat(TDDateTime.Now);
            // CR IR2799 - New date display on page from Selected Date.
            // END CR IR2799

            // CCN 0421 - Removed the top left hand title

            // CCN 0421 - Removed the travel news title

            //CCN 0427 Travel News page title image
            imageTravelNews.ImageUrl = GetResource("HomeTravelInfo.imageTravelNews.ImageUrl");
            imageTravelNews.AlternateText = " ";

            lblTopInstruction.Text = GetResource("TravelNewsInstructional");

            lblCurrentView.Text = GetResource("TravelNews.lblCurrentView");

        }

        /// <summary>
        /// Validates and stores user selection
        /// </summary>
        private void UserSelectionChanged()
        {
            TravelNewsState travelNewsState = TDSessionManager.Current.TravelNewsState;

            bool targetModeIsMap = false;



            if (switchingView)
            //    targetModeIsMap = travelNewsState.SelectedView == TravelNewsViewType.Details;
            //else
                targetModeIsMap = travelNewsState.SelectedView == TravelNewsViewType.Map;
            
            
                // CCN 0421 if the user has clicked through a news incident from the homepage, only show the single incident
                // until they change any of the filter options 
            
                if (showSingleIncident)
                {
                    string uid = travelNewsState.SelectedIncident;

                    // Retrieve the Id of the incident
                    TravelNewsItem incident = TravelNewsHelper.GetTravelNewsItem(uid);

                    if (targetModeIsMap)
                        ShowIncident(uid);
                    else
                        ShowIncidentInTable(uid);

                    if (incident != null)
                    {
                        MapTravelNews.ZoomToPoint(incident.Easting, incident.Northing, TravelNewsHelper.DefaultIncidentZoomLevel);
                    }
                }
                else
                {
                    if (!switchingView)
                    {
                        // Selected region Id comes from the control as an ID and needs to be converted to the
                        // corresponding resource ID for travel news purposes
                        string newsRegionId = TravelNewsHelper.RegionIdToNewsValue(regionSelector.SelectedRegionId);

                        // setting the page title to show selected region - CCN 0624

                        lblLiveTravelNews.Text = String.Format("{0} {1}", newsRegionId, GetResource("TravelNews.lblLiveTravelNews"));


                        // If user has entered search criteria and also selected a region, flag error to be shown
                        // indicating this
                        if ((TravelNewsHelper.IsRegionValidForMapView(newsRegionId))
                            && (!string.IsNullOrEmpty(ShowNewsControl.SearchPhrase))
                            && !switchingView)
                        {
                            // Don't need to show error if user was in region selected mode, and then entered search text
                            if ((string.IsNullOrEmpty(travelNewsState.LastSearchPhrase))
                                &&
                                (TravelNewsHelper.IsRegionValidForMapView(travelNewsState.LastSelectedRegion)))
                            {
                                regionAndSearchTextValid = true;
                            }
                            else
                            {
                                regionAndSearchTextValid = false;
                            }
                        }

                        if (targetModeIsMap)
                        {
                            // Validate the region and transport options

                            //regionValid = TravelNewsHelper.IsRegionValidForMapView(newsRegionId);

                            // User first presented with travel News in Detail mode
                            // if user clicks Show Map button without selecting region
                            // region selection error is displayed. if the travelnewsstate filter
                            // variables not changed before it shouldn't show the back button yet.
                            if (!regionValid && TravelNewsStateNotChanged())
                                ShowNewsControl.IsBackButtonVisible = false;
                        }
                        else
                        {
                            regionValid = true;
                        }

                        if (regionValid && (travelNewsState.SelectedRegion != newsRegionId))
                        {
                            travelNewsState.SelectedRegion = newsRegionId;
                            zoomToRegion = true;
                        }


                        //IR2687 - Don't reload data into TravelNewsState 
                        //		   for the non-javascript implementation of the ImageMapcontrol
                        if (!IsReentrant)
                        {
                            // Validate the supplied date.
                            dateValid = TravelNewsHelper.IsDateValid(ShowNewsControl.SelectedDate);

                            if (dateValid)
                                travelNewsState.SelectedDate = ShowNewsControl.SelectedDate;

                            travelNewsState.SelectedTransport = ShowNewsControl.SelectedTransport;

                            // CCN 0421 setting IncidentType propery of travelNewsState
                            travelNewsState.SelectedIncidentType = ShowNewsControl.SelectedIncidentType;

                            //if (switchingView)
                            //{
                            //    if (targetModeIsMap && regionValid)
                            //        travelNewsState.SelectedView = TravelNewsViewType.Map;
                            //    else
                            //        travelNewsState.SelectedView = TravelNewsViewType.Details;
                            //}

                            //CR 
                            delayValid = TravelNewsHelper.IsDelayValid(ShowNewsControl.SelectedDate, ShowNewsControl.SelectedDelays);
                            if (delayValid)
                            {
                                travelNewsState.SelectedDelays = ShowNewsControl.SelectedDelays;
                            }

                            //if data valid reset the severity filter back 
                            //to default to allow all incidents to be filtered
                            if (dateValid && delayValid && regionValid)
                            {
                                travelNewsState.SelectedSeverityFilter = SeverityFilter.Default;
                            }

                            // Search phrase
                            if (ShowNewsControl.SearchPhrase.Length > 0)
                            {
                                travelNewsState.SearchPhrase = ShowNewsControl.SearchPhrase;

                            }
                            else
                            {
                                travelNewsState.SearchPhrase = string.Empty;
                            }



                        }
                        // Update the session with the new user preferences if necessary
                        if (ShowNewsControl.SavePreferences && TDSessionManager.Current.Authenticated)
                            TravelNewsHelper.SavePreferences();

                        if ((travelNewsState.SelectedView == TravelNewsViewType.Map)
                            &&
                             (!string.IsNullOrEmpty(ShowNewsControl.SearchPhrase)))
                        {
                            switchingView = true;
                        }
                    }
                    
                    if(switchingView)
                    {
                        
                        if (targetModeIsMap)
                        {
                           regionAndSearchTextValid = string.IsNullOrEmpty(travelNewsState.SearchPhrase);
                           if (regionAndSearchTextValid)
                           {
                               travelNewsState.SelectedView = TravelNewsViewType.Map;
                           }
                           else
                           {
                               travelNewsState.SelectedView = TravelNewsViewType.Details;
                           }
                        }
                        else
                            travelNewsState.SelectedView = TravelNewsViewType.Details;
                    }


                    
                        
                    

                // Update controls accordingly
                SetUpControls();
            }
        }

        /// <summary>
        /// varifies if the previous and current travel news states are same
        /// this means travel news state not changed 
        /// </summary>
        /// <returns>return true if travel news state is changed</returns>
        private bool TravelNewsStateNotChanged()
        {
            bool ischanged = false;
            TravelNewsState travelNewsState = TravelNewsHelper.CurrentTravelNewsState;

            if (travelNewsState.LastSearchPhrase != travelNewsState.SearchPhrase)
                ischanged = true;

            if (travelNewsState.LastSelectedDate != travelNewsState.SelectedDate)
                ischanged = true;

            if (travelNewsState.LastSelectedDelays != travelNewsState.SelectedDelays)
                ischanged = true;

            if (travelNewsState.LastSelectedIncident != travelNewsState.SelectedIncident)
                ischanged = true;

            if (travelNewsState.LastSelectedIncidentType != travelNewsState.SelectedIncidentType)
                ischanged = true;

            if (travelNewsState.LastSelectedRegion != travelNewsState.SelectedRegion)
                ischanged = true;

            if (travelNewsState.LastSelectedTransport != travelNewsState.SelectedTransport)
                ischanged = true;

            if (travelNewsState.LastSelectedView != travelNewsState.SelectedView)
                ischanged = true;

            return ischanged;

        }

        /// <summary>
        /// Sets up the controls used in the page, according to the 
        /// state of the travel news data and the travelNewsState
        /// </summary>
        private void SetUpControls()
        {
            // Obtain TravelNewsState from session
            TravelNewsState travelNewsState = TravelNewsHelper.CurrentTravelNewsState;

            ITravelNewsHandler travelNewsHandler = (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];

            // Check if travel news data is available in the database 
            if (travelNewsHandler.IsTravelNewsAvaliable)
            {
                labelNoTravelNews.Visible = false;

                // Set up ShowNewsControl
                ShowNewsControl.Authenticated = TDSessionManager.Current.Authenticated;


                // CCN 0421 Set up MapRegionSelectControl's Back button visibility
                // For the first time back button shouldn't be visible 
                // Once user clicks ok button back button shoul be visible

                // CCN 0427 back button moved to shownewscontrol
                if (!Page.IsPostBack)
                    ShowNewsControl.IsBackButtonVisible = false;



                // If user is logged in then retrieve user preferences for the ShowNewsControl
                if (((!Page.IsPostBack) && (TDSessionManager.Current.Authenticated)) && !incidentPopulated)
                {
                    // Load user's preferences
                    TravelNewsHelper.LoadPreferences();
                }

                // Set up labels, urls etc.
                //TravelNewsMapZoom.HelpLabel = this.helpLabelMapTools.ID;





                lblHoverMsg.Visible = false;

                // Show appropriate label/button according to current view
                if (travelNewsState.SelectedView == TravelNewsViewType.Details)
                {
                    travelNewsHelp.HelpLabelControl = helpLabelTravelNewsNonMap;
                    ButtonSwitchView.Text = GetResource("TravelNews.buttonSwitchToMap.Text");
                    
                }
                else // Map view
                {
                    travelNewsHelp.HelpLabelControl = helpLabelTravelNews;
                    // CCN 0421 - made hover on symbol message to show in map view
                    lblHoverMsg.Text = GetResource("TravelNews.lblCurrentViewHoverMessage");
                    lblHoverMsg.Visible = true;
                    ButtonSwitchView.Text = GetResource("TravelNews.ImageButtonSwitchToTable.Text");
                   

                }

               
                // Update the display of the data
                UpdateDisplay();
            }
            else // If no travel news is available
            {
                // Hide/disable controls
                TravelNewsDetails.Visible = false;
                mapTable.Visible = false;
                
                // end CR IR2799
                ShowNewsControl.DisableAllControls();

                // Show the unavailable message
                labelNoTravelNews.Text = travelNewsHandler.TravelNewsUnavailableText;
                labelNoTravelNews.Visible = true;

                ButtonSwitchView.Visible = false;
                
            }
        }

        /// <summary>
        /// This method will add the resources to the page to make the popups on the map translate in welsh
        /// </summary>
        private void SetupResourcesInClientScript()
        {
            //all the messages within the popups
            scriptResources.Add("TravelNews.MapPopup.Severity", GetResource("TravelNews.MapPopup.Severity"));
            scriptResources.Add("TravelNews.MapPopup.Mode", GetResource("TravelNews.MapPopup.Mode"));
            scriptResources.Add("TravelNews.MapPopup.Type", GetResource("TravelNews.MapPopup.Type"));
            scriptResources.Add("TravelNews.MapPopup.Location", GetResource("TravelNews.MapPopup.Location"));
            scriptResources.Add("TravelNews.MapPopup.Detail", GetResource("TravelNews.MapPopup.Detail"));
            scriptResources.Add("TravelNews.MapPopup.StartDate", GetResource("TravelNews.MapPopup.StartDate"));
            scriptResources.Add("TravelNews.MapPopup.EndDate", GetResource("TravelNews.MapPopup.EndDate"));
            scriptResources.Add("TravelNews.MapPopup.LastUpdated", GetResource("TravelNews.MapPopup.LastUpdated"));

            //headers of the popups
            scriptResources.Add("IncidentMapping.Roadworks.Alt", GetResource("IncidentMapping.Roadworks.Alt"));
            scriptResources.Add("IncidentMapping.RoadIncident.Alt", GetResource("IncidentMapping.RoadIncident.Alt"));
            scriptResources.Add("IncidentMapping.PlannedRoadworks.Alt", GetResource("IncidentMapping.PlannedRoadworks.Alt"));
            scriptResources.Add("IncidentMapping.PTIncident.Alt", GetResource("IncidentMapping.PTIncident.Alt"));
        }

        /// <summary>
        /// Updates the display of travel news items
        /// Displays the appropriate controls according to view type and reapplies any filters
        /// </summary>
        private void UpdateDisplay()
        {
            if (TravelNewsHelper.ShowMap)
                UpdateMapDisplay();
            else
                UpdateGridDisplay();
        }

        /// <summary>
        /// Sets up the map and hides the grid
        /// </summary>
        private void UpdateMapDisplay()
        {
            // Update visibility of controls
            travelNewsDetailsContainer.Style[HtmlTextWriterStyle.Visibility] = "hidden";
            mapTable.Style[HtmlTextWriterStyle.Visibility] = "";

            InitializeMap();

            // If switching to map view, need to zoom to correct level
            if ((switchingView || zoomToRegion) && !zoomToIncident && (TravelNewsHelper.CurrentTravelNewsState.SelectedRegion != "All"))
            {
                // Obtain coordinates of selected region to zoom map
                int[] regionCoords = TravelNewsHelper.GetRegionCoordinates();
                if (regionCoords.Length == 4)
                {
                    MapTravelNews.SetEnvelope(regionCoords[0], regionCoords[1], regionCoords[2], regionCoords[3]);
                }
            }

            // Refresh the map display
            MapTravelNews.RefreshMap();
        }

        private void InitializeMap()
        {
            
            if (TravelNewsHelper.MapDisplayRecent)
            {
                // set incidents to All, and call setdisplaydate - date should be today
                MapTravelNews.MapSeverity = MapSeverity.All;
                MapTravelNews.MapTimePeriod = MapTimePeriod.Recent;
            }
            else
            {
                // set incident to selected type, and set date filter to selected date.
                // we are using the Date type, to show all incidents on a given date.
                // could also use DateTime type, to show all incidents at a given time.
                if (TravelNewsHelper.MapIncidentSeverityLevel == Map.IncidentSeverityLevel.Severe)
                {
                    MapTravelNews.MapSeverity = MapSeverity.Major;
                }
                else
                {
                    MapTravelNews.MapSeverity = MapSeverity.All;
                }

                MapTravelNews.SetIncidentsTimeFilter(TravelNewsHelper.CurrentTravelNewsState.SelectedDate.GetDateTime());
               
            }

            switch (TravelNewsHelper.CurrentTravelNewsState.SelectedTransport)
            {
                case TransportType.All:
                    MapTravelNews.MapTransportType = MapTransportType.All;
                    break;
                case TransportType.PublicTransport:
                    MapTravelNews.MapTransportType = MapTransportType.Public;
                    break;
                case TransportType.Road:
                    MapTravelNews.MapTransportType = MapTransportType.Road;
                    break;
                default:
                    MapTravelNews.MapTransportType = MapTransportType.All;
                    break;
            }

            // CCN 421 - Set the Incident Type Filter in the ESRI Map object
            switch (TravelNewsHelper.CurrentTravelNewsState.SelectedIncidentType)
            {
                case IncidentType.All:
                    MapTravelNews.MapIncidentType = MapIncidentType.All;
                    break;
                case IncidentType.Planned:
                    MapTravelNews.MapIncidentType = MapIncidentType.Planned;
                    break;
                case IncidentType.Unplanned:
                    MapTravelNews.MapIncidentType = MapIncidentType.Unplanned;
                    break;
                default:
                    MapTravelNews.MapIncidentType = MapIncidentType.All;
                    break;
            }

           
        }

        /// <summary>
        /// Sets up the grid and hides the map
        /// </summary>
        private void UpdateGridDisplay()
        {
            // Update the details control with the correct data
            TravelNewsDetails.NewsItems = TravelNewsHelper.GetNewsItems();

            // Set to render additional CJP User information
            TravelNewsDetails.CJPUser = CJPUserInfoHelper.IsCJPInformationAvailable();

            if (switchingView || !Page.IsPostBack)
            {
                // Update visibility of controls
                travelNewsDetailsContainer.Style[HtmlTextWriterStyle.Visibility] = "";
                mapTable.Style[HtmlTextWriterStyle.Visibility] = "hidden";

            }

            // User has arrived passing an incident ID, so show only one incident - CCN 0421
            if ((showSingleIncident) || (!string.IsNullOrEmpty(TravelNewsHelper.CurrentTravelNewsState.SelectedIncident)))
            {
                TravelNewsItem[] newsItem = new TravelNewsItem[1];
                newsItem[0] = TravelNewsHelper.GetTravelNewsItem(TravelNewsHelper.CurrentTravelNewsState.SelectedIncident);
                
                //check to see if any children to display too - if the selected incident has children they 
                //must also be displayed - CCN 0485a Hierarchy functionality
                TravelNewsItem[] newsItemChildren = TravelNewsHelper.GetChildrenTravelNewsItems(TravelNewsHelper.CurrentTravelNewsState.SelectedIncident);
                if (newsItemChildren.Length > 0)
                {
                    //Size return array to take selected incident and all it's children
                    TravelNewsItem[] returnArray = new TravelNewsItem[newsItemChildren.Length + 1];
                    //Add selected incident to return array...
                    returnArray[0] = newsItem[0];
                    //...and add all it's children after it starting at index 1
                    newsItemChildren.CopyTo(returnArray,1);
                    TravelNewsDetails.NewsItems = returnArray;
                }
                else
                {
                    //Simply return selected item
                    TravelNewsDetails.NewsItems = newsItem;
                }
            }
            else
            {
                TravelNewsDetails.NewsItems = TravelNewsHelper.GetNewsItems();

                // zoom map to correct level
                if (!zoomToIncident && (TravelNewsHelper.CurrentTravelNewsState.SelectedRegion != "All"))
                {
                    // Obtain coordinates of selected region to zoom map
                    int[] regionCoords = TravelNewsHelper.GetRegionCoordinates();
                    if (regionCoords.Length == 4)
                    {
                        MapTravelNews.SetEnvelope(regionCoords[0], regionCoords[1], regionCoords[2], regionCoords[3]);
                    }
                }
            }


        }

        /// <summary>
        /// Loads the current values from TravelNewsState into the ShowNewsControl and
        /// MapRegionSelectControl
        /// </summary>
        private void LoadTravelNewsState()
        {
            // Set travelNewsState for ShowNewsControl
            ShowNewsControl.ShowNewsState = TravelNewsHelper.CurrentTravelNewsState;
            regionSelector.SelectedRegionId = TravelNewsHelper.RegionNewsValueToId(TravelNewsHelper.CurrentTravelNewsState.SelectedRegion);
        }

        /// <summary>
        /// Synchronizes the Map Region control/ShowNewsControl region dropdown.
        /// Set true to make region.SelectedRegionID = ShowNewsControl.SelectedRegionID
        /// Set false to make ShowNewsControl.SelectedRegionID = region.SelectedRegionID
        /// </summary>
        /// <param name="mapregion"></param>
        private void SynchronizeRegion(bool mapregion)
        {
            if (mapregion)
            {
                regionSelector.SelectedRegionId = ShowNewsControl.SelectedRegionId; 
            }
            else
            {
                ShowNewsControl.SelectedRegionId = regionSelector.SelectedRegionId;   
            }
        }

        /// <summary>
        /// Retrieves the incident in question, switches to map view and zooms to the incident
        /// </summary>
        /// <param name="uid"></param>
        private string ShowIncident(string uid)
        {
            // Retrieve the Id of the incident
            TravelNewsItem incident = TravelNewsHelper.GetTravelNewsItem(uid);

            // Update to map view in session
            TravelNewsHelper.CurrentTravelNewsState.SelectedView = TravelNewsViewType.Map;

            // Set flags for map initialisation
            switchingView = true;
            zoomToIncident = true;
            showSingleIncident = true;

            // Set up controls accordingly
            SetUpControls();

            // Zoom map to the specific incident
            if (incident != null)
              MapTravelNews.ZoomToPoint(incident.Easting, incident.Northing, TravelNewsHelper.DefaultIncidentZoomLevel);

            // Save the incident id to session
            TravelNewsHelper.CurrentTravelNewsState.SelectedIncident = uid;
                

            return incident.ModeOfTransport;
        }

        //CCN 0421 add new method ShowIncidentTable
        private void ShowIncidentInTable(string uid)
        {
            // Retrieve the Id of the incident
            TravelNewsItem incident = TravelNewsHelper.GetTravelNewsItem(uid);

            // Update to table view in session
            TravelNewsHelper.CurrentTravelNewsState.SelectedView = TravelNewsViewType.Details;

            // Save the incident id to session
            TravelNewsHelper.CurrentTravelNewsState.SelectedIncident = uid;

            showSingleIncident = true;

            // Zoom map to the specific incident
            if (incident != null)
                MapTravelNews.ZoomToPoint(incident.Easting, incident.Northing, TravelNewsHelper.DefaultIncidentZoomLevel);


            // Set up controls accordingly
            SetUpControls();
        }

        /// <summary>
        /// Checks if javascript is diabled and shows info message if disabled for map
        /// </summary>
        private void CheckJavascriptEnabled()
        {
            if (!this.IsJavascriptEnabled)
            {
                errorControl.Type = ErrorsDisplayType.Custom;
                errorControl.ErrorsDisplayTypeText = GetResource("MapControl.JavaScriptDisabled.Heading.Text");

                List<string> errors = new List<string>();

                errors.Add(GetResource("MapControl.JavaScriptDisabled.Description.Text"));
                errors.Add(GetResource("MapControl.JavaScriptDisabled.FindNearest.Text"));
                errorControl.ErrorStrings = errors.ToArray();

                errorPanel.Visible = true;
                errorControl.Visible = true;
                
            }
        }

        /// <summary>
        /// Registers page and page controls javascript
        /// </summary>
        private void RegisterJavascript()
        {
            // Register the scripts needed only if user has Javascript enabled
            TDPage thePage = this.Page as TDPage;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (thePage != null && thePage.IsJavascriptEnabled)
            {
                MapTravelNews.Visible = false;

               // To resolve the issue with no grid data displayed when map is displayed first on postback set grid data as well
                if (TravelNewsHelper.ShowMap)
                    UpdateGridDisplay();

                // Get the global script repository
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                string regionCoordinates = serializer.Serialize(TravelNewsHelper.GetAllRegionCoordinates());

                StringBuilder toggleScript = new StringBuilder();


                ButtonSwitchView.OnClientClick = string.Format("changeTNView('dummy');return false;");


                string travelNewsStateScript = string.Format("setupMapRegionSelector({0});travelNewsHelper.SaveState({0});setupRegionChangeResources('{1}');setupViewChangeResources('{2}','{3}',{0});", regionCoordinates, GetResource("TravelNews.lblLiveTravelNews"), GetResource("TravelNews.buttonSwitchToMap.Text"), GetResource("TravelNews.ImageButtonSwitchToTable.Text"));

                if (TravelNewsHelper.ShowMap)
                {
                    travelNewsStateScript += travelNewsStateScript + string.Format("SetTravelNewsInMap({0});", regionCoordinates);
                }

                if (showSingleIncident)
                {
                    travelNewsStateScript += "travelNewsHelper.showSingleIncident(true);";

                    if (!string.IsNullOrEmpty(TravelNewsHelper.CurrentTravelNewsState.SelectedIncident))
                    {
                        TravelNewsItem incident = TravelNewsHelper.GetTravelNewsItem(TravelNewsHelper.CurrentTravelNewsState.SelectedIncident);
                        // Set up hidden fields
                        singleIncidentEasting.Value = incident.Easting.ToString();
                        singleIncidentId.Value = TravelNewsHelper.CurrentTravelNewsState.SelectedIncident;
                        singleIncidentNorthing.Value = incident.Northing.ToString();
                        singleIncidentScale.Value = TravelNewsHelper.DefaultIncidentZoomLevel.ToString();
                    }
                }

                travelNewsStateScript += string.Format("setupTNViewSwitchButtons('{0}');", TravelNewsHelper.CurrentTravelNewsState.SelectedView);

                thePage.ClientScript.RegisterStartupScript(this.GetType(), "TravelNewsStateScript", travelNewsStateScript, true);

                // Register the mapping api call script
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptCommonAPI, repository.GetScript(scriptCommonAPI, thePage.JavascriptDom));

                helpPanelTravelNews.Style[HtmlTextWriterStyle.Display] = "none";
                helpPanelTravelNewsNonMap.Style[HtmlTextWriterStyle.Display] = "none";

                helpLabelTravelNewsNonMap.Visible = helpLabelTravelNews.Visible = true;

                helpLabelTravelNews.CloseButton.OnClientClick = "return hideTravelNewsHelp();";
                helpLabelTravelNewsNonMap.CloseButton.OnClientClick = "return hideTravelNewsHelp();";
                travelNewsHelp.OnClientClick = "return showTravelNewsHelp();";
            }
            else
            {
                // Javascript disabled keep the map travel news control visible
                MapTravelNews.Visible = true;
                newsContainer.Visible = false;
            }
            
        }

        /// <summary>
        /// Sets up the printable control
        /// </summary>
        private void SetPrintableControl()
        {
            // Add the javascript to set the map viewstate on client side
            PrintableButtonHelper printHelper = null;

            printHelper = new PrintableButtonHelper(MapTravelNews.MapID);
            

            if (printHelper != null)
            {
                PrinterFriendlyPageButtonControl1.PrintButton.OnClientClick = string.Format("setTravelNewsView();{0}",printHelper.GetClientScript());
                
            }
        }

        #endregion

        #region Private Event Handlers

        /// <summary>
        /// Handles click event of ShowNewsControl
        /// Updates session values with selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserSelectionChangedHandler(object sender, System.EventArgs e)
        {
            // CCN 0421 showSingleIncident flag is only reset when another button is selected
            showSingleIncident = false;

            #region Synchronize Selected Region
            //CCN 0427 syncronizing shownewscontrols selected region id with the mapregionselect control
            //in the case of OK button clicked on shownewscontrol
            if (sender is ShowNewsControl)
            {
                SynchronizeRegion(true);
            }

            //CCN 0427 syncronizing mapregionselect control selected region id with the shownewscontrol
            //in the case of OK button clicked on shownewscontrol
            if (sender is MapRegionControl)
            {
                SynchronizeRegion(false);
            }
            #endregion

            // back button moved to shownewscontrol
            ShowNewsControl.IsBackButtonVisible = true;

            #region Handle search text entered in Map view
            // If user is in Map view and has entered search text, then we need to switch to Table view
            // because the search text has no effect in the Map view
            TravelNewsState travelNewsState = TravelNewsHelper.CurrentTravelNewsState;
            
            #endregion

            // copy current TravelNewsState values as previous news State Values
            CopyCurrentTravelNewsValuestoPrevious();

            #region Clear Selected Incident
            // Because user has selected the OK button or changed Region, then need to remove the 
            // incident from the travelnewsstate to ensure we dont show the single incident again
            if (!string.IsNullOrEmpty(travelNewsState.SelectedIncident))
            {
                travelNewsState.SelectedIncident = "";

                // Ensures if user was in single incident view, and region changed, the map rescales
                zoomToRegion = true;
            }
            #endregion

            UserSelectionChanged();
        }

        /// <summary>
        /// this methode copies current TravelNewsState values as previous news state values
        /// </summary>
        private void CopyCurrentTravelNewsValuestoPrevious()
        {
            //assigning current filter selections as previously selected filter
            TravelNewsState currentTravelNewsState = TravelNewsHelper.CurrentTravelNewsState;
            currentTravelNewsState.LastSearchPhrase = currentTravelNewsState.SearchPhrase;
            currentTravelNewsState.LastSelectedRegion = currentTravelNewsState.SelectedRegion;
            currentTravelNewsState.LastSelectedDate = currentTravelNewsState.SelectedDate;
            currentTravelNewsState.LastSelectedTransport = currentTravelNewsState.SelectedTransport;
            currentTravelNewsState.LastSelectedDelays = currentTravelNewsState.SelectedDelays;
            currentTravelNewsState.LastSelectedIncident = currentTravelNewsState.SelectedIncident;
            currentTravelNewsState.LastSelectedIncidentType = currentTravelNewsState.SelectedIncidentType;
            currentTravelNewsState.LastSelectedView = currentTravelNewsState.SelectedView;
        }


        /// <summary>
        /// Event handler for ButtonSwitchView click
        /// Update labels and images accordingly and switch views
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSwitchView_Click(object sender, EventArgs e)
        {
            //Make sure the map control matches the drop down list:
            SynchronizeRegion(true);

            #region Handle search text entered in Table view
            // If user is in Table Details view and has entered search text, then need to show error stating the
            // user cannot switch to Map if search text entered
            TravelNewsState travelNewsState = TravelNewsHelper.CurrentTravelNewsState;
            if ((travelNewsState.SelectedView == TravelNewsViewType.Details)
                &&
                 (!string.IsNullOrEmpty(ShowNewsControl.SearchPhrase)))
            {
                searchTextValid = false;
            }
            else
            {
                switchingView = true;

                CopyCurrentTravelNewsValuestoPrevious();
            }
            #endregion

            #region Handle single incident view
            // Determine if user is in single incident view
            if (!string.IsNullOrEmpty(travelNewsState.SelectedIncident))
            {
                showSingleIncident = true;
            }
            #endregion



            UserSelectionChanged();

        }

       
        /// <summary>
        /// Handles the command event of the link buttons within the TravelNewsDetailsControl
        /// Retrieves the incident in question, switches to map view and zooms to the incident
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TravelNewsDetails_IncidentClicked(object sender, CommandEventArgs e)
        {
            // Retrieve the Id of the incident
            string uid = (string)e.CommandArgument;

            CopyCurrentTravelNewsValuestoPrevious();

            ShowIncident(uid);
        }

        private void regionSelector_BackButtonClicked(object sender, EventArgs e)
        {
            TravelNewsState currentTravelNewsState = TravelNewsHelper.CurrentTravelNewsState;

            string prvSearchPhrase = currentTravelNewsState.LastSearchPhrase;
            string prvSelectedRegion = currentTravelNewsState.LastSelectedRegion;
            string prvSelectedIncident = currentTravelNewsState.LastSelectedIncident;
            TDDateTime prvSelectedDate = currentTravelNewsState.LastSelectedDate;
            TransportType prvSelectedTransport = currentTravelNewsState.LastSelectedTransport;
            DelayType prvSelectedDelays = currentTravelNewsState.LastSelectedDelays;
            IncidentType prvSelectedIncidentType = currentTravelNewsState.LastSelectedIncidentType;
            TravelNewsViewType prvSelectedViewType = currentTravelNewsState.LastSelectedView;

            ShowNewsControl.SelectedDate = prvSelectedDate;
            ShowNewsControl.SelectedTransport = prvSelectedTransport;
            ShowNewsControl.SelectedDelays = prvSelectedDelays;
            ShowNewsControl.SelectedIncidentType = prvSelectedIncidentType;

            ShowNewsControl.SearchPhrase = prvSearchPhrase;
            regionSelector.SelectedRegionId = TravelNewsHelper.RegionNewsValueToId(prvSelectedRegion);
            ShowNewsControl.SelectedRegionId = regionSelector.SelectedRegionId;

            CopyCurrentTravelNewsValuestoPrevious();

            switchingView = currentTravelNewsState.SelectedView != prvSelectedViewType;
            //currentTravelNewsState.SelectedView = prvSelectedViewType;

            if (prvSelectedIncident != string.Empty)
            {
                currentTravelNewsState.SearchPhrase = prvSearchPhrase;
                currentTravelNewsState.SelectedRegion = prvSelectedRegion;
                currentTravelNewsState.SelectedDate = prvSelectedDate;
                currentTravelNewsState.SelectedTransport = prvSelectedTransport;
                currentTravelNewsState.SelectedDelays = prvSelectedDelays;
                currentTravelNewsState.SelectedIncidentType = prvSelectedIncidentType;
                currentTravelNewsState.SelectedIncident = prvSelectedIncident;

                showSingleIncident = true;

                if (prvSelectedViewType == TravelNewsViewType.Map)
                    ShowIncident(prvSelectedIncident);
                else
                    ShowIncidentInTable(prvSelectedIncident);
            }
            else
            {
                // Force clear of selected incident in current state
                currentTravelNewsState.SelectedIncident = "";
                showSingleIncident = false;

                UserSelectionChanged();
            }
        }
        #endregion

        #region Map event handlers

        /// <summary>
        /// Event handler for Zoom event of MapZoomControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomMap(object sender, ZoomLevelEventArgs e)
        {
            if (e.ZoomLevel > 0)
                TravelNewsMap.Map.SetScale(e.ZoomLevel);
        }

        /// <summary>
        /// Event handler for ZoomIn event of MapZoomControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomMapIn(object sender, System.EventArgs e)
        {
            // Get the current map click mode
            Map.ClickModeType currentClickMode = TravelNewsMap.Map.ClickMode;

            // Set the click mode to zoom in
            TravelNewsMap.Map.ClickMode = Map.ClickModeType.ZoomIn;

            // Raise the click event
            TravelNewsMap.Map.FireClickEvent();

            // Restore the click mode
            TravelNewsMap.Map.ClickMode = currentClickMode;
        }

        /// <summary>
        /// Event handler for ZoomOut event
        /// of MapZoomControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomMapOut(object sender, System.EventArgs e)
        {
            // Get the current map click mode
            Map.ClickModeType currentClickMode = TravelNewsMap.Map.ClickMode;

            // Set the click mode to zoom in
            TravelNewsMap.Map.ClickMode = Map.ClickModeType.ZoomOut;

            // Raise the click event
            TravelNewsMap.Map.FireClickEvent();

            // Restore the click mode
            TravelNewsMap.Map.ClickMode = currentClickMode;
        }

        /// <summary>
        /// Event handler for PreviuosView event
        /// of MapZoomControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //		private void MapPreviousView (object sender, System.EventArgs e)
        //		{
        //			// Call previous view on the map
        //			TravelNewsMap.Map.ZoomPrevious();
        //		}

        /// <summary>
        /// Event handler for the map changed event.
        /// </summary>
        private void Map_Changed(object sender, MapChangedEventArgs e)
        {
            // Get the map scale from the MapChangeEventArgs
            TravelNewsMap.ScaleChange(e.MapScale, e.NaptanInRange);

            // Update the map URLS and scales in InputPageState for printer friendly maps
            TDSessionManager.Current.InputPageState.MapUrlOutward = TravelNewsMap.Map.ImageUrl;
            TDSessionManager.Current.InputPageState.OverviewMapUrlOutward = e.OvURL;
            TDSessionManager.Current.InputPageState.MapScaleOutward = e.MapScale;

            // Save viewstate so map is available when printer friendly page is displayed
            TDSessionManager.Current.StoredMapViewState[TDSessionManager.OUTWARDMAP] = TravelNewsMap.Map.ExtractViewState();

            // Update the hyperlink on the key control
            MapHelper helper = new MapHelper();
            MapKeyControl.HyperlinkUrl = helper.getLegandUrl(e.MapScale);
        }

        /// <summary>
        /// Event handler for the MoreHelpEvent. Saves
        /// the map to view state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMapStore(object sender, EventArgs e)
        {
            if (TDTraceSwitch.TraceVerbose)
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Storing map into session"));

            // Store the map viewstate
            TDSessionManager.Current.StoredMapViewState[TDSessionManager.OUTWARDMAP] = TravelNewsMap.Map.ExtractViewState();
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }

        #endregion

    }
}
