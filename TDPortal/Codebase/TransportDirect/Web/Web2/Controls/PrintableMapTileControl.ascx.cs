// *********************************************** 
// NAME                 : PrintableMapTileControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/08/2008
// DESCRIPTION          : Printable map control, which displays a journey using tiled maps.
//                      : Currently, this is only implemented for Cycle journeys
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PrintableMapTileControl.ascx.cs-arc  $ 
//
//   Rev 1.8   Oct 26 2010 14:30:18   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.7   Mar 16 2010 10:39:50   apatel
//Updated to resolve Cycle printer friendly return map issue
//Resolution for 5457: Cycle Journey : Printer friendly issue
//
//   Rev 1.6   Feb 09 2010 13:05:26   apatel
//updated for cycle printer friendly map
//Resolution for 5399: Cycle Planner Printer Friendly page broken
//
//   Rev 1.5   Feb 02 2010 16:38:00   pghumra
//Fixed multiple page refresh issues for printer friendly maps.
//Resolution for 5395: CODE FIX - INITIAL - DEL 10.x - Del 10.9.1 Bug printer friendly
//
//   Rev 1.4   Nov 16 2009 17:07:02   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Jan 15 2009 13:22:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Oct 09 2008 16:25:50   mmodi
//Added printer friendly instruction text
//Resolution for 5143: Cycle Planner - Text not displayed on 'Printer Friendly' page
//
//   Rev 1.1   Oct 08 2008 11:30:26   mmodi
//Added map scale label
//Resolution for 5134: Cycle Planner - The 'scale of map' is not displayed on bottom right corner of 'Cycle Journey Map - Printer Friendly' page
//
//   Rev 1.0   Aug 22 2008 10:53:16   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Presentation.InteractiveMapping;
using System.Web.Script.Serialization;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class PrintableMapTileControl : TDUserControl
    {
        #region Private members
        
        private bool outward = false;
        private bool showMapEntireJourney = true;
        private bool showMapTile = true;
        private bool showDirectionsEntireJourney = true;
        private bool showDirectionsTile = true;
        private bool cycleMapHeaderControl = true;
        private int numberOfPages = 0;
        private double scaleMapTile = 0;

        private ITDSessionManager sessionManager;
        private TDItineraryManager itineraryManager;
        private TDJourneyViewState viewState;
        private InputPageState inputPageState;

        // journey to display
        private CycleJourney cycleJourney = null;

        // string containing the map image url of entire journey
        private string mapImageUrl = null;

        // object holding the tile maps
        private CyclePrintDetail[] cyclePrintDetails = null;

        // currently selected roadunits
        private RoadUnitsEnum roadUnits;

        #region State of results
        /// <summary>
        ///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
        /// </summary>
        private bool outwardExists = false;

        /// <summary>
        ///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
        /// </summary>
        private bool returnExists = false;

        /// <summary>
        /// True if the Itinerary exists, containing the Initial journey and zero or more extensions
        /// </summary>
        private bool itineraryExists = false;

        /// <summary>
        /// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
        /// </summary>
        private bool extendInProgress = false;

        /// <summary>
        /// True if the Itinerary exists and there are no extensions in the process of being planned
        /// </summary>
        private bool showItinerary = false;

        /// <summary>
        /// True if the results have been planned using FindA
        /// </summary>
        private bool showFindA = false;

        private bool returnArriveBefore = false;
        private bool outwardArriveBefore = false;
        #endregion
        
        #endregion
        
        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AddEventHandlers();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to populate the controls on the page.
        /// </summary>
        /// <param name="isOutward"></param>
        public void Populate(bool outward, Journey journey, ITDSessionManager sessionManager, 
            TDItineraryManager itineraryManager, TDJourneyViewState viewState)
        {
            if (journey is CycleJourney)
            {
                this.outward = outward;
                this.sessionManager = sessionManager;
                this.itineraryManager = itineraryManager;
                this.viewState = viewState;
                this.cycleJourney = journey as CycleJourney;
                this.inputPageState = sessionManager.InputPageState;

                DetermineStateOfResults();

                MapHelper mapHelper = new MapHelper();

                if (showMapEntireJourney)
                {
                    // Get the map of the entire journey
                    // This is shown in the Repeater header
                    mapImageUrl = this.outward ? inputPageState.MapUrlOutward : inputPageState.MapUrlReturn;
                }

                if (showMapTile)
                {
                    // Get the tile maps to display
                    // This is shown in the Repeater items
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    cyclePrintDetails = null;
                    if (this.outward && !string.IsNullOrEmpty(inputPageState.CycleMapTilesOutward))
                    {
                        cyclePrintDetails = serializer.Deserialize<CyclePrintDetail[]>(inputPageState.CycleMapTilesOutward);
                    }
                    else if (!this.outward && !string.IsNullOrEmpty(inputPageState.CycleMapTilesReturn))
                    {
                        cyclePrintDetails = serializer.Deserialize<CyclePrintDetail[]>(inputPageState.CycleMapTilesReturn);
                    }

                    scaleMapTile = this.outward ? inputPageState.MapTileScaleOutward : inputPageState.MapTileScaleReturn;
                }

                // If we dont have any cycle print details, then there has been an error/or no map loaded.
                if ((cyclePrintDetails == null) || (cyclePrintDetails.Length <= 0))
                {
                    // Initialise an empty array, this will stop any map tiles with directions being displayed
                    cyclePrintDetails = new CyclePrintDetail[0];
                }

                // Capture the number of maps/pages we're displaying on this control.
                // Used for placing "page break"s on all pages but last
                numberOfPages = cyclePrintDetails.Length;


                // Add the maps/directions to the repeater
                repeaterMapTile.DataSource = cyclePrintDetails;
                repeaterMapTile.DataBind();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Wires up any additional events needed on this control
        /// </summary>
        private void AddEventHandlers()
        {
            repeaterMapTile.ItemDataBound += new RepeaterItemEventHandler(repeaterMapTile_ItemDataBound);
        }

        /// <summary>
        /// Establish what mode the Itinerary Manager is in and whether we have any Return results
        /// </summary>
        private void DetermineStateOfResults()
        {
            itineraryExists = (itineraryManager.Length > 0);
            extendInProgress = itineraryManager.ExtendInProgress;
            showItinerary = (itineraryExists && !extendInProgress);
            showFindA = (!showItinerary && (sessionManager.IsFindAMode));

            if (showItinerary)
            {
                outwardExists = (itineraryManager.OutwardLength > 0);
                returnExists = (itineraryManager.ReturnLength > 0);
            }
            else
            {
                //check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);

                //check for normal result
                ITDJourneyResult result = sessionManager.JourneyResult;
                if (result != null)
                {
                    outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists;
                    returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;

                    // Get time types for journey.
                    outwardArriveBefore = sessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
                    returnArriveBefore = sessionManager.JourneyViewState.JourneyReturningTimeSearchType;
                }
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Repeater item databound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repeaterMapTile_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Populate controls common 
            if ((e.Item.ItemType == ListItemType.Header) || (e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                #region Populate journey title and date
                ResultsTableTitleControl resultsTableTitleControl = (ResultsTableTitleControl)e.Item.FindControl("resultsTableTitleControl");

                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
                plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControl, outward);

                #endregion

                #region Populate journey summary results table
                if (!showItinerary)
                {
                    FindSummaryResultControl findSummaryResultTableControl = (FindSummaryResultControl)e.Item.FindControl("findSummaryResultTableControl");

                    findSummaryResultTableControl.Visible = showFindA;

                    if (showFindA)
                    {
                        // Get the modes to display on the results
                        TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                        if (sessionManager.FindPageState != null)
                            modeTypes = sessionManager.FindPageState.ModeType;

                        bool arriveBefore = outward ? outwardArriveBefore : returnArriveBefore;

                        findSummaryResultTableControl.Initialise(true, outward, arriveBefore, modeTypes);
                    }
                }
                #endregion

                #region Display the new page break

                Literal literalNewPage = (Literal)e.Item.FindControl("literalNewPage");

                // Ensures page break is not shown for the last map
                if (e.Item.ItemIndex < (numberOfPages - 1))
                {
                    literalNewPage.Visible = true;
                }

                #endregion
            }

            // Populate controls only for header
            if (e.Item.ItemType == ListItemType.Header)
            {
                if ((outward && outwardExists) || (!outward && returnExists))
                {
                    HtmlGenericControl divHeaderControl = (HtmlGenericControl)e.Item.FindControl("divCycleMapHeaderControl");
                    
                    divHeaderControl.Visible = cycleMapHeaderControl;

                    #region Populate the map of entire journey

                    Image imageMap = (Image)e.Item.FindControl("imageMap");

                    // Only display the map of entire journey if we have one
                    if ((showMapEntireJourney) 
                        && (!string.IsNullOrEmpty(mapImageUrl)))
                    {
                        imageMap.ImageUrl = mapImageUrl;

                        // Set the scale labels
                        Label labelMapScaleTitle = (Label)e.Item.FindControl("labelMapScaleTitle");
                        labelMapScaleTitle.Text = GetResource("PrintableMapControl.labelMapScaleTitle");

                        Label labelMapScale = (Label)e.Item.FindControl("labelMapScale");
                        labelMapScale.Text = (outward) ?
                            "1:" + sessionManager.InputPageState.MapScaleOutward.ToString(TDCultureInfo.CurrentUICulture.NumberFormat) :
                            "1:" + sessionManager.InputPageState.MapScaleReturn.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
                    }
                    else
                    {
                        // no map, hide the map and scale divs
                        HtmlGenericControl divCycleImageMap = (HtmlGenericControl)e.Item.FindControl("divCycleImageMap");
                        HtmlGenericControl divCycleImageMapScale = (HtmlGenericControl)e.Item.FindControl("divCycleImageMapScale");
                        
                        divCycleImageMap.Visible = false;
                        imageMap.Visible = false;
                        divCycleImageMapScale.Visible = false;
                    }

                    #endregion

                    #region Populate journey directions
                    // Get the journey details table
                    CycleJourneyDetailsTableControl cycleJourneyDetailsTable = (CycleJourneyDetailsTableControl)e.Item.FindControl("cycleJourneyDetailsTableControl");

                    // Only display the directions if we're in journey directions are to be displayed, initialise details table
                    if (showDirectionsEntireJourney)
                    {
                        TDJourneyParametersMulti journeyParams = null;

                        if (sessionManager.JourneyParameters is TDJourneyParametersMulti)
                        {
                            journeyParams = sessionManager.JourneyParameters as TDJourneyParametersMulti;
                        }

                        cycleJourneyDetailsTable.Initialise(cycleJourney, outward, viewState,journeyParams);

                        cycleJourneyDetailsTable.RoadUnits = roadUnits;
                        cycleJourneyDetailsTable.Printable = true;
                    }
                    else
                    {
                        // no directions to display, hide the journey details div
                        HtmlGenericControl divCycleJourneyDetailsTable = (HtmlGenericControl)e.Item.FindControl("divCycleJourneyDetailsTable");
                        divCycleJourneyDetailsTable.Visible = false;
                        cycleJourneyDetailsTable.Visible = false;
                    }
                    #endregion
                }
            }

            // Populate controls only for repeating items
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if ((outward && outwardExists) || (!outward && returnExists))
                {
                    // Get the data item, so that we can populate directions and map
                    CyclePrintDetail cyclePrintDetail = (CyclePrintDetail)e.Item.DataItem;

                    #region Populate the journey map tiles

                    Image imageMap = (Image)e.Item.FindControl("imageMap");

                    if ((showMapTile)
                        && (!string.IsNullOrEmpty(cyclePrintDetail.MapImageURL)))
                    {
                        imageMap.ImageUrl = cyclePrintDetail.MapImageURL;

                        // Set the scale labels
                        Label labelMapScaleTitle = (Label)e.Item.FindControl("labelMapScaleTitle");
                        labelMapScaleTitle.Text = GetResource("PrintableMapControl.labelMapScaleTitle");

                        Label labelMapScale = (Label)e.Item.FindControl("labelMapScale");
                        labelMapScale.Text = "1:" + scaleMapTile.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
                    }
                    else
                    {
                        // no map, hide the map and scale divs
                        HtmlGenericControl divCycleImageMap = (HtmlGenericControl)e.Item.FindControl("divCycleImageMap");
                        HtmlGenericControl divCycleImageMapScale = (HtmlGenericControl)e.Item.FindControl("divCycleImageMapScale");

                        divCycleImageMap.Visible = false;
                        imageMap.Visible = false;
                        divCycleImageMapScale.Visible = false;
                    }

                    #endregion

                    #region Populate journey directions
                    // Get the journey details table
                    CycleJourneyDetailsTableControl cycleJourneyDetailsTable = (CycleJourneyDetailsTableControl)e.Item.FindControl("cycleJourneyDetailsTableControl");

                    // If we have journey directions to display, intialise details table
                    if ( (showDirectionsTile)
                        && (cyclePrintDetail.JourneyInstruction.Length > 0))
                    {
                        TDJourneyParametersMulti journeyParams = null;

                        if (sessionManager.JourneyParameters is TDJourneyParametersMulti)
                        {
                            journeyParams = sessionManager.JourneyParameters as TDJourneyParametersMulti;
                        }
                        cycleJourneyDetailsTable.Initialise(cycleJourney, outward, viewState,
                            cyclePrintDetail.JourneyInstruction[0], // start direction index
                            cyclePrintDetail.JourneyInstruction[cyclePrintDetail.JourneyInstruction.Length - 1],journeyParams); // end direction index

                        cycleJourneyDetailsTable.RoadUnits = roadUnits;
                        cycleJourneyDetailsTable.Printable = true;
                    }
                    else
                    {
                        // no directions to display, hide the journey details div
                        HtmlGenericControl divCycleJourneyDetailsTable = (HtmlGenericControl)e.Item.FindControl("divCycleJourneyDetailsTable");
                        divCycleJourneyDetailsTable.Visible = false;
                        cycleJourneyDetailsTable.Visible = false;
                    }
                    #endregion

                }
            }
        }

        #endregion

        #region Repeater properties

        /// <summary>
        /// Returns the Printer friendly text
        /// </summary>
        /// <returns></returns>
        public string GetlabelPrinterFriendlyText()
        {
            return GetResource("StaticPrinterFriendly.labelPrinterFriendly");
        }

        /// <summary>
        /// Returns the Printer friendly instruction text
        /// </summary>
        /// <returns></returns>
        public string GetlabelInstructionsText()
        {
            return GetResource("StaticPrinterFriendly.labelInstructions");
        }

        /// <summary>
        /// Returns the Page Header title text
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetlabelMapPageHeaderTitle(int index)
        {
            if ((showDirectionsEntireJourney) && (!showMapEntireJourney))
                return string.Format(GetResource("CyclePlanner.PrintableMapTileControl.labelMapPageHeaderTitle.DirectionsForJourney.Text"));
            else // Map is being shown, return map text
                return string.Format(GetResource("CyclePlanner.PrintableMapTileControl.labelMapPageHeaderTitle.MapOfEntireJourney.Text"));
        }

        /// <summary>
        /// Returns the Page Item title text
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetlabelMapPageItemTitle(int index)
        {
            if (showDirectionsTile)
                return string.Format(GetResource("CyclePlanner.PrintableMapTileControl.labelMapPageItemTitle.Directions.Text"), Convert.ToString(index + 1));
            else // Map only is being shown, return map specific text
                return string.Format(GetResource("CyclePlanner.PrintableMapTileControl.labelMapPageItemTitle.Maps.Text"), Convert.ToString(index + 1));
        }

        /// <summary>
        /// Returns the alternate text for tiled map images of cycle journey
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetImageMapAltText()
        {
            return GetResource("JourneyMapControl.imageMap.AlternateText");
           
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Visibility of cycle map header control
        /// </summary>
        public bool CycleMapHeaderControl
        {
            get
            {
                return cycleMapHeaderControl;
            }
            set
            {
                cycleMapHeaderControl = value;
            }
        }

        /// <summary>
        /// Read/write. The Road units to display the instructions in
        /// </summary>
        public RoadUnitsEnum RoadUnits
        {
            get { return roadUnits; }
            set { roadUnits = value; }
        }

        /// <summary>
        /// Read/write. Flag set to show map of entire journey
        /// </summary>
        public bool ShowMapEntireJourney
        {
            get { return showMapEntireJourney; }
            set { showMapEntireJourney = value; }
        }

        /// <summary>
        /// Read/write. Flag set to show map tiles on this control
        /// </summary>
        public bool ShowMapTile
        {
            get { return showMapTile; }
            set { showMapTile = value; }
        }

        /// <summary>
        /// Read/write. Flag set to show directions of entire journey
        /// </summary>
        public bool ShowDirectionsEntireJourney
        {
            get { return showDirectionsEntireJourney; }
            set { showDirectionsEntireJourney = value; }
        }

        /// <summary>
        /// Read/write. Flag set to show directions sectioned, with the map tiles
        /// </summary>
        public bool ShowDirectionsTile
        {
            get { return showDirectionsTile; }
            set { showDirectionsTile = value; }
        }
        #endregion
    }
}