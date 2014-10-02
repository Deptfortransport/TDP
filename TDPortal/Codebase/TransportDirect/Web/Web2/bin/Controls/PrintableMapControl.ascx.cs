// *********************************************** 
// NAME                 : PrintableMapControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 02/09/2003 
// DESCRIPTION  : Printable map control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PrintableMapControl.ascx.cs-arc  $ 
//
//   Rev 1.10   Jan 18 2010 12:14:20   mmodi
//Added debug logging if no map found in session
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.9   Nov 12 2009 13:39:52   mmodi
//Updated and tidy up
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 11 2009 18:34:18   mmodi
//Updated for new mapping changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Oct 12 2009 09:11:48   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.7   Oct 12 2009 08:40:08   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Jan 15 2009 11:09:18   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Jan 09 2009 13:36:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Dec 17 2008 11:27:02   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 13 2008 16:41:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Oct 07 2008 10:11:34   mmodi
//Updated for Cycle Journeys
//Resolution for 5122: Cycle Planner - "Server Error" is displayed when user clicks on 'Priner Friendly' button on 'Find on Map' page
//
//   Rev 1.2   Mar 31 2008 13:22:26   mturner
//Drop3 from Dev Factory
//  
//  Rev DevFactory Mar 07 2008 14:24:00 apatel
//  removed the map height and width get set up in codebehind. Used css in aspx page instead.
//
//   Rev 1.0   Nov 08 2007 13:17:00   mturner
//Initial revision.
//
//   Rev 1.31   Mar 13 2006 14:37:30   NMoorhouse
//Manual merge of stream3353 -> trunk
//
//   Rev 1.30   Feb 23 2006 16:13:14   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.29   Feb 21 2006 12:23:16   build
//Automatically merged from branch for stream0009
//
//   Rev 1.28.2.1   Feb 27 2006 17:55:58   NMoorhouse
//Changed the Refine Map Page label
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.2.0   Feb 09 2006 19:21:22   NMoorhouse
//Added new Map View description for use on Refine Map page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.28.1.1   Feb 09 2006 18:56:20   aviitanen
//Fxcop and review changes
//
//   Rev 1.28.1.0   Feb 03 2006 17:32:12   AViitanen
//TD114 PDF Print Bundles (phase 1) High resolution maps. Changed to use GetHighResolutionImageURL to set map image properties.
//
//   Rev 1.28   Nov 28 2005 15:21:16   jbroome
//Added new intialise method for Visit plan on Map key control
//Resolution for 3222: Visit Planner: Purple triangles should be in the map key for stopover locations
//
//   Rev 1.27   Nov 01 2005 09:33:18   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.26   Oct 18 2005 11:32:16   schand
//Fix for IR2874. Added a condition to check if the request is coming from locationMap.
//Resolution for 2874: Object reference error from Print Button
//
//   Rev 1.25.1.0   Oct 14 2005 09:43:56   jbroome
//Replaced checks for SelectedItinerarySegment with FullItinerarySelected
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.25   Aug 19 2005 14:08:42   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.24.1.0   Aug 04 2005 15:12:04   rgreenwood
//DD073 Map Details: Changed all mthod calls to MapHelper Initialise... methods to now pass extra parameter to initialise the GreyedOut map key if the journey contains an invalid leg
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.24   May 11 2005 09:23:00   rscott
//Changed Table definition for Vantive 3634582
//
//   Rev 1.23   Apr 13 2005 14:46:02   asinclair
//Fix for IR 2044
//
//   Rev 1.22   Apr 08 2005 16:10:20   rhopkins
//Changes to MapHelper methods for testing for presence of public/private results
//
//   Rev 1.21   Mar 11 2005 11:05:52   asinclair
//Fixed further bug issues
//
//   Rev 1.20   Mar 11 2005 10:15:22   rscott
//DEL7 - Code bug fix
//
//   Rev 1.19   Mar 01 2005 16:27:12   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.18   Sep 28 2004 10:05:56   esevern
//obtain find a mode from TDSessionManager.Current
//
//   Rev 1.17   Sep 04 2004 09:47:02   jbroome
//IR 1493 Call correct initialisation of MapKeyControl for FindACar mode.
//
//   Rev 1.16   Jul 12 2004 19:54:44   JHaydock
//DEL 5.4.7 Merge: IR 1132
//
//   Rev 1.15   Jun 22 2004 12:20:10   JHaydock
//FindMap page done. Corrections to printable map controls and pages. Various updates to Find pages.
//
//   Rev 1.14   Jun 09 2004 15:44:20   RPhilpott
//Add explicit support fore FindA options.
//
//   Rev 1.13   Jun 07 2004 15:12:00   jbroome
//ExtendJourney - added support for TDItineraryManager
//
//   Rev 1.12   Apr 05 2004 17:13:16   CHosegood
//Del 5.2 map QA fixes
//
//   Rev 1.11   Mar 25 2004 14:39:46   CHosegood
//DEL 5.2 Map QA changes
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.10   Nov 18 2003 11:11:54   passuied
//Added "1:" before scale
//Resolution for 246: Missing colon from Map Scale on PrintableJourneyMapInput
//
//   Rev 1.9   Nov 06 2003 12:26:08   kcheung
//Fixed alternate text for map and summary map
//
//   Rev 1.8   Nov 06 2003 11:39:10   kcheung
//Fixed printing for Netscape
//
//   Rev 1.7   Nov 05 2003 15:09:06   kcheung
//Updated commenting
//
//   Rev 1.6   Nov 05 2003 10:45:46   kcheung
//Fixed Initialise call to Summary Result Control and time as requested in QA
//
//   Rev 1.5   Oct 23 2003 13:57:08   passuied
//added images
//
//   Rev 1.4   Oct 23 2003 09:43:20   passuied
//display/hide mapkeys for road journeys
//
//   Rev 1.3   Oct 22 2003 19:23:14   passuied
//changes for printable output map page
//
//   Rev 1.2   Oct 13 2003 14:13:10   passuied
//minor changes
//
//   Rev 1.1   Oct 03 2003 11:26:30   passuied
//latest working of printable maps
//
//   Rev 1.0   Oct 02 2003 16:56:30   passuied
//Initial revision.


using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.UserSupport;
    using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common;
    using TransportDirect.Common.Logging;
	
	/// <summary>
	///	Printable map control
	/// </summary>
	public partial  class PrintableMapControl : TDUserControl
    {
        #region Private members

        protected MapLocationIconsDisplayControl iconsDisplay;
		protected CarJourneyDetailsTableControl carJourneyDetails;
		protected MapKeyControl mapKeys;

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

        #endregion

        #region Page Load

        /// <summary>
		/// Page Load Method. Sets-up the control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

			labelOverview.Text = GetResource("PrintableMapControl.labelOverview");
			labelMapScaleTitle.Text = GetResource("PrintableMapControl.labelMapScaleTitle");
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

        #region Public methods

        /// <summary>
		/// Method to populate the all the controls on the page.
		/// </summary>
		/// <param name="isOutward">Indicates if the rendering
		/// for outward or return journey.</param>
		/// <param name="isInput">Indicates if rendering for input
		/// or output map.</param>
		/// <param name="isFromFindA">Indicates that this control 
		/// is being used on the FindA results page.</param>
		public void Populate(bool isOutward, bool isInput, bool isFromFindA)
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

			InputPageState inputPageState = (InputPageState) tdSessionManager.InputPageState;
			TDJourneyViewState viewState = null;
			MapHelper helper = new MapHelper();

			bool itineraryExists = (itineraryManager.Length > 0);
			bool extendInProgress = itineraryManager.ExtendInProgress;

			if (itineraryExists)
				viewState = itineraryManager.JourneyViewState;
			else
				viewState = tdSessionManager.JourneyViewState;

			if (isOutward)
            {
                #region Outward map

                #region Initialise Map Keys

                // Only need to initialise map key for a journey map
                if (!isInput)
                {
                    if (!itineraryExists || extendInProgress)
                    {
                        if (tdSessionManager.IsFindAMode)
                        {
                            if (tdSessionManager.FindPageState.Mode == FindAMode.Car)
                                mapKeys.InitialisePrivate(false, true);
                            else if (tdSessionManager.FindPageState.Mode == FindAMode.Cycle)
                                mapKeys.InitialiseCycle(true);
                            else
                                mapKeys.InitialiseSpecificModes(helper.FindUsedModes(true, false), true, helper.HasJourneyGreyedOutMode(isOutward));
                        }
                        else
                        {
                            if (helper.PublicOutwardJourney)
                            {
                                if (isInput)
                                    mapKeys.InitialisePublic(true);
                                else
                                    mapKeys.InitialisePublic(true, helper.HasJourneyGreyedOutMode(isOutward));
                            }
                            else
                            {
                                mapKeys.InitialisePrivate(false, true);
                            }
                        }
                    }
                    else
                    {
                        // Check for Visit Planner Results - has its own map key
                        if (TDSessionManager.Current.ItineraryMode == ItineraryManagerMode.VisitPlanner)
                            mapKeys.InitialiseVisitPlan(true, helper.HasJourneyGreyedOutMode(isOutward));
                        else
                            mapKeys.InitialiseMixed(true, helper.HasJourneyGreyedOutMode(isOutward));
                    }

                    // Show map keys 
                    panelMapKeys.Visible = true;

                    // Set Keys panel style container for journey map
                    panelKeysContainer.CssClass = "mpMapPrintKeysContainerJourney";
                }
                else
                {
                    // Hide map keys
                    panelMapKeys.Visible = false;

                    // Set  Keys panel style container for input map
                    panelKeysContainer.CssClass = "mpMapPrintKeysContainerMap";
                }
                #endregion

                #region Initialise Map Symbols

                iconsDisplay.Populate(inputPageState.IconSelectionOutward);
                panelIconsDisplay.Visible = !iconsDisplay.IsEmpty;

                #endregion

                #region Initialise Map View Type label

                if (!isInput)
                {
                    if (this.PageId == PageId.PrintableRefineMap)
                    {
                        panelMapViewType.Visible = true;
                        labelMapViewType.Text = GetResource("RefineMap.PrintableMapControl.labelOutwardJourney");
                    }
                    else if (!string.IsNullOrEmpty(inputPageState.MapViewTypeOutward))
                    {
                        // Get display text from the session
                        panelMapViewType.Visible = true;
                        labelMapViewType.Text = inputPageState.MapViewTypeOutward;
                    }
                }
                else
                {
                    // Don't show Map view type label for input map
                    panelMapViewType.Visible = false;
                }

                #endregion

                #region Initialise Map Scale text

                // Get scale text from session
                labelMapScale.Text = "1:" + inputPageState.MapScaleOutward.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);

                // Always hide scale row, the scale is now generated as part of the image
                scaleRow.Visible = false;

                #endregion

                #region Initialise Map Overview image

                imageOverview.ImageUrl = inputPageState.OverviewMapUrlOutward;
                imageOverview.AlternateText = GetResource( "JourneyMapControl.imageSummaryMap.AlternateText");
                
                // Always hide the overview map, no longer need to display it
                panelMapOverview.Visible = false;

                #endregion

                #region Initialise Map Image

                // Printable map image URL is now set to the session by the page Printer Friendly button javascript
                if (string.IsNullOrEmpty(inputPageState.MapUrlOutward))
                {
                    imageMap.ImageUrl = string.Empty;

                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Outward Map image url not found in session inputPageState.MapUrlOutward for printer friendly page.");
                    Logger.Write(oe);
                }
                else
                {
                    imageMap.ImageUrl = inputPageState.MapUrlOutward;

                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Outward Map image url found in session inputPageState.MapUrlOutward for printer friendly page - " + inputPageState.MapUrlOutward);
                    Logger.Write(oe);
                }

                imageMap.AlternateText = GetResource("langStrings", "JourneyMapControl.imageMap.AlternateText");

                #endregion

                #region Initialise Car Journey Directions panel

                if (!isInput)
                {
                    if (itineraryExists && !extendInProgress)
                    {
                        if (!itineraryManager.FullItinerarySelected)
                        {
                            panelDirections.Visible = viewState.OutwardShowDirections;
                            carJourneyDetails.Initialise(isOutward);
                        }
                    }
                    else
                    {
                        panelDirections.Visible = viewState.OutwardShowDirections;
                        if (FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
                        {
                            carJourneyDetails.Visible = false;
                        }
                        else
                        {
                            carJourneyDetails.Initialise(isOutward);
                        }
                    }
                }
                else
                {
                    panelDirections.Visible = false;
                }

                #endregion
                
                #endregion
            }
			else //Return journey
            {
                #region Return map

                #region Initialise Map Keys

                // Only need to initialise map key for a journey map
                if (!isInput)
                {
                    if (!itineraryExists || extendInProgress)
                    {
                        if (isFromFindA)
                        {
                            if (tdSessionManager.FindPageState.Mode == FindAMode.Car)
                                mapKeys.InitialisePrivate(false, true);
                            else if (tdSessionManager.FindPageState.Mode == FindAMode.Cycle)
                                mapKeys.InitialiseCycle(true);
                            else
                                mapKeys.InitialiseSpecificModes(helper.FindUsedModes(false, false), true, helper.HasJourneyGreyedOutMode(isOutward));
                        }
                        else
                        {
                            if (helper.PublicReturnJourney)
                            {
                                mapKeys.InitialisePublic(true, helper.HasJourneyGreyedOutMode(isOutward));
                            }
                            else
                            {
                                mapKeys.InitialisePrivate(false, true);
                            }
                        }
                    }
                    else
                    {
                        mapKeys.InitialiseMixed(true, helper.HasJourneyGreyedOutMode(isOutward));
                    }

                    // Show map keys 
                    panelMapKeys.Visible = true;

                    // Set Keys panel style container for journey map
                    panelKeysContainer.CssClass = "mpMapPrintKeysContainerJourney";
                }
                else
                {
                    // Hide map keys
                    panelMapKeys.Visible = false;

                    // Set  Keys panel style container for input map
                    panelKeysContainer.CssClass = "mpMapPrintKeysContainerMap";
                }

                #endregion

                #region Initialise Map Symbols

                iconsDisplay.Populate(inputPageState.IconSelectionReturn);
                panelIconsDisplay.Visible = !iconsDisplay.IsEmpty;

                #endregion

                #region Initialise Map View Type label

                if (!isInput)
                {
                    if (this.PageId == PageId.PrintableRefineMap)
                    {
                        panelMapViewType.Visible = true;
                        labelMapViewType.Text = GetResource("RefineMap.PrintableMapControl.labelReturnJourney");
                    }
                    else if (!string.IsNullOrEmpty(inputPageState.MapViewTypeReturn))
                    {
                        // Get display text from the session
                        panelMapViewType.Visible = true;
                        labelMapViewType.Text = inputPageState.MapViewTypeReturn;
                    }
                }
                else
                {
                    // Don't show Map view type label for input map
                    panelMapViewType.Visible = false;
                }

                #endregion

                #region Initialise Map Scale text

                // Get scale text from session
                labelMapScale.Text = "1:" + inputPageState.MapScaleReturn.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);

                // Always hide scale row, the scale is now generated as part of the image
                scaleRow.Visible = false;

                #endregion

                #region Initialise Map Overview image

                imageOverview.ImageUrl = inputPageState.OverviewMapUrlReturn;
                imageOverview.AlternateText = GetResource("JourneyMapControl.imageSummaryMap.AlternateText");

                // Always hide the overview map, no longer need to display it
                panelMapOverview.Visible = false;

                #endregion

                #region Initialise Map Image

                // Printable map image URL is now set to the session by the page Printer Friendly button javascript
                if (string.IsNullOrEmpty(inputPageState.MapUrlReturn))
                {
                    imageMap.ImageUrl = string.Empty;

                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Return Map image url not found in session inputPageState.MapUrlReturn for printer friendly page.");
                    Logger.Write(oe);
                }
                else
                {
                    imageMap.ImageUrl = inputPageState.MapUrlReturn;

                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Return Map image url found in session inputPageState.MapUrlReturn for printer friendly page - " + inputPageState.MapUrlReturn);
                    Logger.Write(oe);
                }

                imageMap.AlternateText = GetResource("langStrings", "JourneyMapControl.imageMap.AlternateText");

                #endregion

                #region Initialise Car Journey Directions panel

                if (!isInput)
				{
					if (itineraryExists && !extendInProgress)
					{
						if (!itineraryManager.FullItinerarySelected)
							panelDirections.Visible = viewState.OutwardShowDirections;
					}
					else
					{
						panelDirections.Visible = viewState.ReturnShowDirections;
						if (FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
						{
							carJourneyDetails.Visible = false;
						}
						else
						{
							carJourneyDetails.Initialise(isOutward);
						}
					}
                }

                #endregion

                #endregion
            }

            // Overide control visibilities based on planner mode
            if (tdSessionManager.FindAMode == FindAMode.EnvironmentalBenefits)
            {
                keyRow.Visible = false;
                scaleRow.Visible = false;               
            }
        }

        #endregion

        #region Public properties

        /// <summary>
		/// Property to allow access to the CarJourneyDetailsTableControl
		/// </summary>
		/// <remarks></remarks>
		public CarJourneyDetailsTableControl CarJourneyDetails
		{
			get
			{
				return carJourneyDetails;
			}
			set
			{	
				carJourneyDetails = value;
			}
        }

        #endregion
    }

}
