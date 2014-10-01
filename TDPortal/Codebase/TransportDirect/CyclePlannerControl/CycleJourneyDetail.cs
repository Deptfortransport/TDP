// *********************************************** 
// NAME			: CycleJourneyDetail.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which takes a Cycle Planner section and turns its into a CycleJourneyDetail
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CycleJourneyDetail.cs-arc  $
//
//   Rev 1.16   Oct 11 2010 12:19:58   apatel
//Updated to check if the default pointseparator and eastingnorthingseparator used in journey result come back from the cycle webservice
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.15   Sep 29 2010 11:26:10   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.14   Apr 09 2010 15:51:14   mmodi
//Added check for a positive Complex Manoeuvre attribute
//Resolution for 5501: Cycle Planner - <SectionID>0  returned in Cycle cjp response crashes journey
//
//   Rev 1.13   Jun 03 2009 11:12:08   mmodi
//Updated to allow Latitude Longitude to be associated with the detail
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.12   Nov 28 2008 11:00:14   mmodi
//Check for null cycle route number
//Resolution for 5190: Cycle Planner - Cycle route name no longer contains a number
//
//   Rev 1.11   Oct 28 2008 17:08:46   mmodi
//Updated to display complex manoeuvres
//
//   Rev 1.10   Oct 23 2008 10:12:50   mmodi
//Updated setting of Start OSGR
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.9   Oct 22 2008 15:46:12   mmodi
//Added Start OSGR property, and trimmed road name
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.8   Oct 20 2008 14:31:50   mmodi
//Updated attribute values to be uint
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.7   Oct 15 2008 11:25:54   mmodi
//Corrected to read in geometry coordinates as doubles
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Oct 10 2008 16:54:48   mmodi
//Updated to include cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Sep 16 2008 16:44:04   mmodi
//Updated to use Point value and not pass Toids in request
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 22 2008 10:10:04   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 08 2008 12:05:46   mmodi
//Updated as part of workstream
//
//   Rev 1.2   Aug 06 2008 14:49:48   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:37:24   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:41:56   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class CycleJourneyDetail
    {
        #region Private members
        // static/constants
        private static readonly string ROAD_NUMBER_FERRY = "FERRY";
        
        private const string CONGESTION_LEVEL = "JourneyDetailsCongestionWarning.Value";
        private const string JOURNEYRESULTSETTING_POINTSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.PointSeperator";
        private const string JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.EastingNorthingSeperator";

        private int congestionlevel;
        private char coordinateSeperator;
        private char eastingNorthingSeperator;


        // private members
        private bool driveSection = false;
        private bool stopoverSection = false;
        private bool junctionSection = false;

        private int sectionId;

        private int distance;
        private long duration; // in seconds

        private string roadName;
        private string roadNumber;
        private string placeName;

        private string junctionNumber;
        private JunctionType junctionType;
        private bool junctionDriveSectionWithoutJunctionNo;

        private int cost; // in 10000th pence
        private int congestionZoneCost;
        private int tollCost;
        private int ferryCost;
        
        private bool roadSplits = false;
        private bool slipRoad = false;
        private bool ferry = false;
        private bool congestion = false;

        private int turnCount;
        private TurnDirection turnDirection;
        private TurnAngle turnAngle;

        private Dictionary<int, OSGridReference[]> geometry;
        private Dictionary<int, bool> interpolateGradient;
        private LatitudeLongitude[] latlongCoordinates;
        private OSGridReference startOSGR;
        private string[] toid;
        private string nodeToid;

        private string instructionText;
        private string manoeuvreText;
        private string namedAccessRestrictionText;
        private string namedTimeText;

        private Dictionary<string, string> cycleRoutes; // the cycle routes this section applies to
        private bool cycleRoute = false;

        private uint[] joiningSignificantLinkAttributes = new uint[0]; // cycle specific attributes
        private uint[] leavingSignificantLinkAttributes = new uint[0];
        private uint[] interestingLinkAttributes = new uint[0];
        private uint[] significantNodeAttributes = new uint[0];
        private uint[] sectionFeatureAttributes = new uint[0];

        private string companyUrl;
        
        private string congestionZoneEntry;
        private string congestionZoneExit;
        private string congestionZoneEnd;
        private bool congestionEntry = false;
        private bool congestionExit = false;
        private bool congestionEnd = false;

        private string ferryCheckInName;
        private bool ferryCheckIn = false;
        private bool ferryCheckOut = false;
        private bool displayFerryIcon = false;

        private string tollEntryName;
        private string tollExitName;
        private bool tollEntry = false;
        private bool tollExit = false;
        private bool displayTollIcon = false;

        private bool viaLocation = false;

        private bool waitSection = false;
        private bool undefinedWaitSection = false;

        private bool complexManoeuvre = false;
        private bool namedAccessRestriction = false;
        private bool namedTime = false;

        // Reuse as it does what a cycle details needs
        private RoadJourneyDetailMapInfo roadJourneyDetailMapInfo;

        //private RoadJourneyChargeItem roadJourneyChargeItem;
        #endregion

        /// <summary>
        /// Take a Cycle Planner Web Service "section" and create a CycleJourneyDetail
        /// </summary>
        /// <param name="section">TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.Section The section to use
        /// for the CycleJourneyDetail information</param>
        public CycleJourneyDetail(Section cpSection, TDCycleJourneyResultSettings resultSettings)
        {
            // populate helper values
            congestionlevel = Convert.ToInt32(Properties.Current[CONGESTION_LEVEL]);
            coordinateSeperator = Convert.ToChar(Properties.Current[JOURNEYRESULTSETTING_POINTSEPERATOR]);
            eastingNorthingSeperator = Convert.ToChar(Properties.Current[JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR]);

            if (resultSettings != null)
            {
                coordinateSeperator = resultSettings.PointSeparator;
                eastingNorthingSeperator = resultSettings.EastingNorthingSeparator;

            }

            
            sectionId = cpSection.sectionID;
            duration = cpSection.time.Ticks / TimeSpan.TicksPerSecond;

            // Is this a stop over section, drive section or a junction drive section?
            Type sectionType = cpSection.GetType();
            
            if (sectionType == typeof(StopoverSection))
            {
                #region StopoverSection

                stopoverSection = true;
                driveSection = false;
                junctionSection = false;

                // Stop Over section information
                StopoverSection thisSection = cpSection as StopoverSection;

                roadName = (thisSection.name != null ? thisSection.name.Trim() : string.Empty);

                // Get the toid and geometry
                if (thisSection.node == null || thisSection.node.TOID == null)
                {
                    nodeToid = string.Empty;

                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Error, String.Format("CyclePlanner - CycleJourneyDetail. No Node or Node TOID was found for a StopoverSection of name: " + thisSection.name + "and type: " + thisSection.type)));
                }
                else
                {
                    nodeToid = thisSection.node.TOID;

                    // Populate the geometry for this stopover section
                    if (!string.IsNullOrEmpty(thisSection.node.geometry))
                    {
                        PopulateGeometry(0, thisSection.node.geometry, false);
                    }
                }
                
                tollCost = thisSection.toll;

                //Creates a RoadJourneyDetailMapInfo object using first and last toids. 
                //Used for cycle section maps.
                roadJourneyDetailMapInfo = new RoadJourneyDetailMapInfo(string.Empty, string.Empty);

                // Need to establish what type of Stopover it is and handle accordingly

                #region ComplexManoeuvre
                if (thisSection.type == StopoverSectionType.ComplexManoeuvre)
                {
                    complexManoeuvre = true;

                    manoeuvreText = thisSection.name.Trim();

                    #region Populate the Section feature attributes
                    // Section Features (e.g. information on details of an operator, contact details for a ferry...).
                    // For a stopover section type of ComplexManoeuvre, there will be a SectionFeatureAttribute defined
                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        // Initialise the attributes array, assuming we will only ever have upto 4 attribute integers
                        if (sectionFeatureAttributes.Length == 0)
                        {
                            sectionFeatureAttributes = new uint[4] { 0, 0, 0, 0 };
                        }

                        foreach (SectionFeature feature in thisSection.sectionFeatures)
                        {
                            // The feature.id indicates whether the value is for ITN attribute (id = 0), or User attribuutes (id > 0)
                            if (feature.GetType() == typeof(SectionFeatureAttributes))
                            {
                                SectionFeatureAttributes featureAttribute = feature as SectionFeatureAttributes;

                                if (featureAttribute.id < 4) // Assume only ever get up to 4 attribute integers
                                {
                                    if (featureAttribute.value >= 0)
                                    {
                                        sectionFeatureAttributes[featureAttribute.id] = Convert.ToUInt32(featureAttribute.value);
                                    }
                                    else
                                    {
                                        // Should never reach here
                                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                                            TDTraceLevel.Error,
                                            String.Format("CyclePlanner - CycleJourneyDetail. ComplexManoeuvre contains an unsupported UInt32 feature attribute value, {0}, for the section {1}, {2}",
                                                featureAttribute.value, thisSection.name, thisSection.type)));

                                        sectionFeatureAttributes[featureAttribute.id] = 0;
                                    }
                                }
                            }
                            
                        }
                    }
                    #endregion
                }

                #endregion

                #region NamedAccessRestriction
                if (thisSection.type == StopoverSectionType.NamedAccessRestriction)
                {
                    namedAccessRestriction = true;

                    namedAccessRestrictionText = thisSection.name;
                }

                #endregion

                #region CongestionZoneEntry
                if (thisSection.type == StopoverSectionType.CongestionZoneEntry)
                {
                    congestionEntry = true;
                    congestionZoneEntry = thisSection.name;
                    congestionZoneCost = thisSection.toll;

                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        //need to extract company name and URL from stopoversection section features	
                        foreach (SectionFeature sec in thisSection.sectionFeatures)
                        {
                            TextSectionFeature tsec = (TextSectionFeature)sec;
                            switch (tsec.id)
                            {
                                case 0:		//Extract company name
                                    break;
                                case 13:	//Extract url of website
                                    companyUrl = tsec.value;
                                    break;

                            }
                        }

                        //Create a RoadJourneyChargeItem for this section - used for Costs page
                        //roadJourneyChargeItem = new RoadJourneyChargeItem(congestionZoneEntry, companyUrl, congestionZoneCost, thisSection.type);
                    }
                }
                #endregion

                #region CongestionZoneExit
                if (thisSection.type == StopoverSectionType.CongestionZoneExit)
                {
                    congestionExit = true;
                    congestionZoneExit = thisSection.name;
                    congestionZoneCost = thisSection.toll;

                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        //need to extract company name and URL from stopoversection section features	
                        foreach (SectionFeature sec in thisSection.sectionFeatures)
                        {
                            TextSectionFeature tsec = (TextSectionFeature)sec;
                            switch (tsec.id)
                            {
                                case 0:		//Extract company name
                                    break;
                                case 13:		//Extract url of website
                                    companyUrl = tsec.value;
                                    break;
                            }
                        }

                        //roadJourneyChargeItem = new RoadJourneyChargeItem(congestionZoneExit, companyUrl, congestionZoneCost, thisSection.type);
                    }
                }
                #endregion

                #region CongestionZoneEnd
                if (thisSection.type == StopoverSectionType.CongestionZoneEnd)
                {
                    congestionEnd = true;
                    congestionZoneEnd = thisSection.name;
                    congestionZoneCost = thisSection.toll;

                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        //need to extract company name and URL from stopoversection section features	
                        foreach (SectionFeature sec in thisSection.sectionFeatures)
                        {
                            TextSectionFeature tsec = (TextSectionFeature)sec;
                            switch (tsec.id)
                            {
                                case 0:		//Extract company name
                                    break;
                                case 13:		//Extract url of website
                                    companyUrl = tsec.value;
                                    break;
                            }
                        }

                        //roadJourneyChargeItem = new RoadJourneyChargeItem(congestionZoneEnd, companyUrl, congestionZoneCost, thisSection.type);
                    }
                }
                #endregion

                #region FerryCheckIn
                if (thisSection.type == StopoverSectionType.FerryCheckIn)
                {
                    ferryCost = thisSection.toll;
                    ferryCheckInName = thisSection.name;
                    ferryCheckIn = true;
                    displayFerryIcon = true;
                    
                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        string ferryRouteUrl = string.Empty;
                        //need to extract company name and URL from stopoversection section features	
                        foreach (SectionFeature sec in thisSection.sectionFeatures)
                        {
                            TextSectionFeature tsec = (TextSectionFeature)sec;
                            switch (tsec.id)
                            {
                                case 0:		//Extract company name
                                    break;
                                case 13:		//Extract url of website
                                    companyUrl = tsec.value;
                                    break;
                                case 14:
                                    ferryRouteUrl = tsec.value;
                                    break;
                            }

                            // The companyUrl property actually exposes either the ferry route Url or the company Url.
                            // The ferry route Url takes precedence.
                            if (ferryRouteUrl != null && ferryRouteUrl.Length > 0)
                                companyUrl = ferryRouteUrl;
                        }

                        //roadJourneyChargeItem = new RoadJourneyChargeItem(ferryCheckIn, companyUrl, ferryCost, thisSection.type);
                    }
                }
                #endregion

                #region FerryCheckOut
                if (thisSection.type == StopoverSectionType.FerryCheckOut)
                {
                    //Cost not displayed on exiting a ferry
                    ferryCheckOut = true;
                    displayFerryIcon = true;
                }
                #endregion

                #region TollEntry
                if (thisSection.type == StopoverSectionType.TollEntry)
                {
                    tollCost = thisSection.toll;
                    tollEntryName = thisSection.name;
                    tollEntry = true;
                    displayTollIcon = true;

                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        //need to extract company name and URL from stopoversection section features	
                        foreach (SectionFeature sec in thisSection.sectionFeatures)
                        {
                            TextSectionFeature tsec = (TextSectionFeature)sec;
                            switch (tsec.id)
                            {
                                case 0:		//Extract company name
                                    break;
                                case 13:		//Extract url
                                    companyUrl = tsec.value;
                                    break;
                            }
                        }

                        //roadJourneyChargeItem = new RoadJourneyChargeItem(tollEntry, companyUrl, tollCost, thisSection.type);
                    }
                }
                #endregion

                #region TollExit
                if (thisSection.type == StopoverSectionType.TollExit)
                {
                    tollCost = thisSection.toll;
                    tollExitName = thisSection.name;
                    tollExit = true;
                    displayTollIcon = true;

                    if ((thisSection.sectionFeatures != null) && (thisSection.sectionFeatures.Length != 0))
                    {
                        //need to extract company name and URL from stopoversection section features	
                        foreach (SectionFeature sec in thisSection.sectionFeatures)
                        {
                            TextSectionFeature tsec = (TextSectionFeature)sec;
                            switch (tsec.id)
                            {
                                case 0:		//Extract company name
                                    break;
                                case 13:		//Extract url
                                    companyUrl = tsec.value;
                                    break;
                            }

                        }

                        //roadJourneyChargeItem = new RoadJourneyChargeItem(tollExit, companyUrl, tollCost, thisSection.type);
                    }
                }
                #endregion

                #region Via Section
                if (thisSection.type == StopoverSectionType.Via)
                {
                    roadName = "VIALOCAION";
                    viaLocation = true;
                }
                #endregion

                #region Wait Section
                if (thisSection.type == StopoverSectionType.Wait)
                {
                    //If it is a wait type, we need to add the time to the journey duration
                    //but we don't want to display any journey directions for the wait.
                    roadName = "WAIT";
                    waitSection = true;
                }
                #endregion

                #region Undefined Wait
                if (thisSection.type == StopoverSectionType.UndefinedWait)
                {
                    undefinedWaitSection = true;
                }
                #endregion

                #region Named Time
                if (thisSection.type == StopoverSectionType.NamedTime)
                {
                    namedTime = true;

                    namedTimeText = thisSection.name;
                }
                #endregion
                #endregion
            }

            else if (sectionType == typeof(DriveSection))
            {
                #region DriveSection

                driveSection = true;
                junctionSection = false;
                stopoverSection = false;

                // Drive section
                DriveSection thisSection = cpSection as DriveSection;

                roadName = (thisSection.name != null ? thisSection.name.Trim() : string.Empty);
                roadNumber = (thisSection.number != null ? thisSection.number.Trim() : string.Empty);
                placeName = (thisSection.heading != null ? thisSection.heading.Trim() : string.Empty);

                instructionText = (thisSection.instructionText != null ? thisSection.instructionText : string.Empty);

                cost = thisSection.cost;

                // Don't include Ferry Journeys in the total distance, only in
                // total time. Updated to include check for road number in addition to road name. 
                if (roadNumber != ROAD_NUMBER_FERRY)
                {
                    distance = thisSection.distance;
                    ferry = false;
                }
                else
                {
                    // Section is a ferry section - set flag for formatters
                    ferry = true;
                }

                turnCount = thisSection.turnCount;
                turnDirection = thisSection.turnDirection;
                turnAngle = thisSection.turnAngle;
                
                roadSplits = thisSection.roadSplits;
                slipRoad = false;

                // Need to loop through every link to check for congestion levels
                toid = new string[thisSection.links.Length];
                for (int i = 0; i < thisSection.links.Length; i++)
                {
                    toid[i] = thisSection.links[i].TOID;

                    // congestion
                    if (thisSection.links[i].congestion > congestionlevel)
                    {
                        congestion = true;
                    }

                    // geometry
                    if (!string.IsNullOrEmpty(thisSection.links[i].geometry))
                    {
                        // Add the geometry to the list for this link
                        PopulateGeometry(i, thisSection.links[i].geometry, thisSection.links[i].interpolateGradient);
                    }
                }

                // Add the cycle routes this section applies to
                if ((thisSection.cycleRoutes != null) && (thisSection.cycleRoutes.Length > 0))
                {
                    if (cycleRoutes == null)
                    {
                        cycleRoutes = new Dictionary<string, string>();
                    }
 
                    // Cycle route may not have a number, so we need to assign a temporary key/value for the dictionary
                    int tempNumber = -100;

                    foreach (CycleRoute cr in thisSection.cycleRoutes)
                    {                        
                        string cycleRouteNumber = string.Empty;

                        // if null, give it a temp key value
                        if (string.IsNullOrEmpty(cr.number))
                        {
                            cycleRouteNumber = tempNumber.ToString();
                            tempNumber++;
                        }
                        else
                        {
                            cycleRouteNumber = cr.number.Trim();
                        }

                        // Add the cycle route to the dictionary
                        cycleRoutes.Add(cycleRouteNumber, cr.name.Trim());
                    }

                    cycleRoute = true;
                }

                // Add the cycle specific attributes. These are converted into actual instructions by the UI
                joiningSignificantLinkAttributes = thisSection.joiningSignificantLinkAttributes;
                leavingSignificantLinkAttributes = thisSection.leavingSignificantLinkAttributes;
                interestingLinkAttributes = thisSection.interestingLinkAttributes;
                significantNodeAttributes = thisSection.nodeAttributes;

                //Creates a RoadJourneyDetailMapInfo object using first and last toids. 
                string first = thisSection.links[0].TOID.ToString();
                string last = thisSection.links[(thisSection.links.Length - 1)].TOID.ToString();
                roadJourneyDetailMapInfo = new RoadJourneyDetailMapInfo(first, last);

                #endregion
            }

            else if (sectionType == typeof(JunctionDriveSection))
            {
                #region JunctionDriveSection

                // A JunctionDriveSection is used to describe turns on motorway junctions, so it should
                // not be used by the results returned by the cycle planner

                junctionSection = true;
                driveSection = false;
                stopoverSection = false;

                // Junction Drive Section
                JunctionDriveSection thisSection = cpSection as JunctionDriveSection;

                roadName = (thisSection.name != null ? thisSection.name.Trim() : string.Empty);
                roadNumber = (thisSection.number != null ? thisSection.number.Trim() : string.Empty);
                placeName = (thisSection.heading != null ? thisSection.heading.Trim() : string.Empty);

                instructionText = (thisSection.instructionText != null ? thisSection.instructionText : string.Empty);

                cost = thisSection.cost;

                if (roadNumber != ROAD_NUMBER_FERRY)
                {
                    distance = thisSection.distance;
                    ferry = false;
                }
                else
                {
                    // Section is a ferry section - set flag for formatters
                    ferry = true;
                }

                // Treat all junction sections as slip roads
                slipRoad = true;

                turnCount = thisSection.turnCount;
                turnDirection = thisSection.turnDirection;
                turnAngle = thisSection.turnAngle;
                roadSplits = thisSection.roadSplits;

                //Need to loop through every link to check for congestion levels
                
                toid = new string[thisSection.links.Length];
                for (int i = 0; i < thisSection.links.Length; i++)
                {
                    toid[i] = thisSection.links[i].TOID;

                    // congestion
                    if (thisSection.links[i].congestion > congestionlevel)
                    {
                        congestion = true;
                    }

                    // geometry
                    if (!string.IsNullOrEmpty(thisSection.links[i].geometry))
                    {
                        // Add the geometry to the list for this link
                        PopulateGeometry(i, thisSection.links[i].geometry, thisSection.links[i].interpolateGradient);
                    }
                }
                                
                // Although dealing with JunctionDriveSection, if no junction number 
                // present then need to override and treat as normal DriveSection.
                junctionNumber = thisSection.junctionNumber;
                junctionSection = ((junctionNumber != null) && (junctionNumber.Length != 0));
                driveSection = !junctionSection;

                junctionType = thisSection.type;

                junctionDriveSectionWithoutJunctionNo = (junctionNumber == null) || (junctionNumber.Length == 0);

                // Add the cycle routes this section applies to
                if ((thisSection.cycleRoutes != null) && (thisSection.cycleRoutes.Length > 0))
                {
                    if (cycleRoutes == null)
                    {
                        cycleRoutes = new Dictionary<string, string>();
                    }

                    foreach (CycleRoute cr in thisSection.cycleRoutes)
                    {
                        cycleRoutes.Add(cr.number, cr.name);
                    }

                    cycleRoute = true;
                }

                // Add the cycle specific attributes. These are converted into actual instructions by the UI
                joiningSignificantLinkAttributes = thisSection.joiningSignificantLinkAttributes;
                leavingSignificantLinkAttributes = thisSection.leavingSignificantLinkAttributes;
                interestingLinkAttributes = thisSection.interestingLinkAttributes;
                significantNodeAttributes = thisSection.nodeAttributes;

                //Creates a RoadJourneyDetailMapInfo object using first and last toids. 
                string first = thisSection.links[0].TOID.ToString();
                string last = thisSection.links[(thisSection.links.Length - 1)].TOID.ToString();
                roadJourneyDetailMapInfo = new RoadJourneyDetailMapInfo(first, last);

                #endregion
            }
        }

        #region Private methods

        /// <summary>
        /// Takes a string of coordinates, converts them into an array of OSGRs, and 
        /// then adds it to this objects geometry dictionary at the specified index.
        /// A second dictionary is also populated with the flag of whether the coordinate should be 
        /// interpolated by the gradient profiler
        /// </summary>
        private void PopulateGeometry(int index, string geometryvalue, bool interpolateGradient)
        {
            #region Logic to check if the seperators provided by User used in builded result
            char coordinateSeperatorUsed = Convert.ToChar(Properties.Current[JOURNEYRESULTSETTING_POINTSEPERATOR]);
            char eastingNorthingSeperatorUsed = Convert.ToChar(Properties.Current[JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR]);

            if (coordinateSeperator != coordinateSeperatorUsed)
            {
                if (geometryvalue.IndexOf(coordinateSeperator) > -1)
                {
                    coordinateSeperatorUsed = coordinateSeperator;
                }
            }

            if (eastingNorthingSeperator != eastingNorthingSeperatorUsed)
            {
                if (geometryvalue.IndexOf(eastingNorthingSeperator) > -1)
                {
                    eastingNorthingSeperatorUsed = eastingNorthingSeperator;
                }
            }
            #endregion

            // Get all the coordinate pairs from the geometry
            string[] coordinates = geometryvalue.Split(coordinateSeperatorUsed);

            ArrayList osgrs = new ArrayList();

            // Convert each coordinate pair into an OSGRGridReference
            foreach (string coordinate in coordinates)
            {
                // Make sure there is a coordinate before converting
                string coordinateTrimmed = coordinate.Trim();
              
                if (!string.IsNullOrEmpty(coordinateTrimmed))
                {
                    string[] eastingNorthing = coordinateTrimmed.Split(eastingNorthingSeperatorUsed);

                    // Assume coordinates returned are Points (i.e. with decimals)
                    double easting = Math.Round(Convert.ToDouble(eastingNorthing[0]), 0);
                    double northing = Math.Round(Convert.ToDouble(eastingNorthing[1]), 0);

                    // Create the OSGR
                    OSGridReference osgr = new OSGridReference(
                        Convert.ToInt32(easting),
                        Convert.ToInt32(northing));

                    // Add the OSGR to the array
                    osgrs.Add(osgr);

                    // Set up the OSGR for the start of this detail
                    if ((startOSGR == null) || (!startOSGR.IsValid))
                    {
                        startOSGR = osgr;
                    }                   
                }
            }

            // Finally, add all the OSGRs for this link to the geometry list
            if (this.geometry == null)
            {
                this.geometry = new Dictionary<int, OSGridReference[]>();
            }

            if (this.interpolateGradient == null)
            {
                this.interpolateGradient = new Dictionary<int, bool>();
            }

            this.geometry.Add(index, (OSGridReference[])osgrs.ToArray(typeof(OSGridReference)));
            this.interpolateGradient.Add(index, interpolateGradient);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read only. DriveSection flag for this detail
        /// </summary>
        public bool DriveSection
        {
            get { return driveSection; }
        }

        /// <summary>
        /// Read only. StopoverSection flag for this detail
        /// </summary>
        public bool StopoverSection
        {
            get { return stopoverSection; }
        }

        /// <summary>
        /// Read only. JunctionSection flag for this detail
        /// </summary>
        public bool JunctionSection
        {
            get { return junctionSection; }
        }

        /// <summary>
        /// Read only. SectionId for this detail
        /// </summary>
        public int SectionId
        {
            get { return sectionId; }
        }

        /// <summary>
        /// Read only. Distance for this detail
        /// </summary>
        public int Distance
        {
            get { return distance; }
        }

        /// <summary>
        /// Read only. Duration for this detail, in seconds
        /// </summary>
        public long Duration
        {
            get { return duration; }
        }

        /// <summary>
        /// Read only. RoadName for this detail
        /// </summary>
        public string RoadName
        {
            get { return roadName; }
        }

        /// <summary>
        /// Read only. RoadNumber for this detail
        /// </summary>
        public string RoadNumber
        {
            get { return roadNumber; }
        }

        /// <summary>
        /// Read only. PlaceName for this detail
        /// </summary>
        public string PlaceName
        {
            get { return placeName; }
        }

        /// <summary>
        /// Read only. JunctionNumber for this detail
        /// </summary>
        public string JunctionNumber
        {
            get { return junctionNumber; }
        }

        /// <summary>
        /// Read only. JunctionType for this detail
        /// </summary>
        public JunctionType JunctionType
        {
            get { return junctionType; }
        }

        /// <summary>
        /// Read only. JunctionDriveSectionWithoutJunctionNo flag for this detail
        /// </summary>
        public bool JunctionDriveSectionWithoutJunctionNo
        {
            get { return junctionDriveSectionWithoutJunctionNo; }
        }

        /// <summary>
        /// Read only. Cost for this detail, in 10000th pence
        /// </summary>
        public int Cost
        {
            get { return cost; }
        }

        /// <summary>
        /// Read only. CongestionZoneCost for this detail
        /// </summary>
        public int CongestionZoneCost
        {
            get { return congestionZoneCost; }
        }

        /// <summary>
        /// Read only. TollCost for this detail
        /// </summary>
        public int TollCost
        {
            get { return tollCost; }
        }

        /// <summary>
        /// Read only. FerryCost for this detail
        /// </summary>
        public int FerryCost
        {
            get { return ferryCost; }
        }

        /// <summary>
        /// Read only. RoadSplits flag for this detail
        /// </summary>
        public bool RoadSplits
        {
            get { return roadSplits; }
        }

        /// <summary>
        /// Read only. SlipRoad flag for this detail
        /// </summary>
        public bool SlipRoad
        {
            get { return slipRoad; }
        }

        /// <summary>
        /// Read only. Ferry flag for this detail
        /// </summary>
        public bool Ferry
        {
            get { return ferry; }
        }

        /// <summary>
        /// Read only. Congestion flag for this detail
        /// </summary>
        public bool Congestion
        {
            get { return congestion; }
        }

        /// <summary>
        /// Read only. TurnCount for this detail
        /// </summary>
        public int TurnCount
        {
            get { return turnCount; }
        }

        /// <summary>
        /// Read only. TurnDirection for this detail
        /// </summary>
        public TurnDirection TurnDirection
        {
            get { return turnDirection; }
        }

        /// <summary>
        /// Read only. TurnAngle for this detail
        /// </summary>
        public TurnAngle TurnAngle
        {
            get { return turnAngle; }
        }

        /// <summary>
        /// Read only. The geometry in OSGR format
        /// </summary>
        public Dictionary<int, OSGridReference[]> Geometry
        {
            get { return geometry; }
        }
                
        /// <summary>
        /// Read only. The interpolateGradient flag dictionary. This should be read in conjunction
        /// with the Geometry dictionary
        /// </summary>
        public Dictionary<int, bool> InterpolateGradient
        {
            get { return interpolateGradient; }
        }

        /// <summary>
        /// Read only. The latitude longitude coordinates for this detail.
        /// Assumes the TDCyclePlannerResult object has populated this array.
        /// </summary>
        public LatitudeLongitude[] LatitudeLongitudes
        {
            get { return latlongCoordinates; }
        }

        /// <summary>
        /// Read only. The OSGR for the start of this detail
        /// </summary>
        public OSGridReference StartOSGR
        {
            get { return startOSGR; }
        }

        /// <summary>
        /// Read only. Toid array for this detail
        /// </summary>
        public string[] Toid
        {
            get { return toid; }
        }

        /// <summary>
        /// Read only. NodeToid for this detail
        /// </summary>
        public string NodeToid
        {
            get { return nodeToid; }
        }

        /// <summary>
        /// Read only. InstructionText for this detail
        /// </summary>
        public string InstructionText
        {
            get { return instructionText; }
        }

        /// <summary>
        /// Read only. ManoeuvreText for this detail
        /// </summary>
        public string ManoeuvreText
        {
            get { return manoeuvreText; }
        }

        /// <summary>
        /// Read only. NamedAccessRestrictionText for this detail
        /// </summary>
        public string NamedAccessRestrictionText
        {
            get { return namedAccessRestrictionText; }
        }

        /// <summary>
        /// Read only. NamedTimeText for this detail
        /// </summary>
        public string NamedTimeText
        {
            get { return namedTimeText; }
        }

        /// <summary>
        /// Read only. The Cycle Routes this detail applies to
        /// </summary>
        public Dictionary<string, string> CycleRoutes
        {
            get { return cycleRoutes; }
        }

        /// <summary>
        /// Read only. Flag indicating this section is on a Cycle route
        /// </summary>
        public bool CycleRoute
        {
            get { return cycleRoute; }
        }

        /// <summary>
        /// Read only. Array of uint representing cycle attributes (Joining) applying to this section
        /// </summary>
        public uint[] JoiningSignificantLinkAttributes
        {
            get { return joiningSignificantLinkAttributes; }
        }

        /// <summary>
        /// Read only. Array of uint representing cycle attributes (Leaving) applying to this section
        /// </summary>
        public uint[] LeavingSignificantLinkAttributes
        {
            get { return leavingSignificantLinkAttributes; }
        }

        /// <summary>
        /// Read only. Array of uint representing cycle attributes (Interesting) applying to this section
        /// </summary>
        public uint[] InterestingLinkAttributes
        {
            get { return interestingLinkAttributes; }
        }

        /// <summary>
        /// Read only. Array of uint representing cycle attributes (Node) applying to this section
        /// </summary>
        public uint[] SignificantNodeAttributes
        {
            get { return significantNodeAttributes; }
        }

        /// <summary>
        /// Read only. Array of uint representing cycle attributes (SectionFeatures) applying to this section
        /// </summary>
        public uint[] SectionFeatureAttributes
        {
            get { return sectionFeatureAttributes; }
        }

        /// <summary>
        /// Read only. CompanyUrl for this detail
        /// </summary>
        public string CompanyUrl
        {
            get { return companyUrl; }
        }

        /// <summary>
        /// Read only. CongestionZoneEntry for this detail
        /// </summary>
        public string CongestionZoneEntry
        {
            get { return congestionZoneEntry; }
        }

        /// <summary>
        /// Read only. CongestionZoneExit for this detail
        /// </summary>
        public string CongestionZoneExit
        {
            get { return congestionZoneExit; }
        }

        /// <summary>
        /// Read only. CongestionZoneEnd for this detail
        /// </summary>
        public string CongestionZoneEnd
        {
            get { return congestionZoneEnd; }
        }

        /// <summary>
        /// Read only. CongestionEntry flag for this detail
        /// </summary>
        public bool CongestionEntry
        {
            get { return congestionEntry; }
        }

        /// <summary>
        /// Read only. CongestionExit flag for this detail
        /// </summary>
        public bool CongestionExit
        {
            get { return congestionExit; }
        }

        /// <summary>
        /// Read only. CongestionEnd flag for this detail
        /// </summary>
        public bool CongestionEnd
        {
            get { return congestionEnd; }
        }

        /// <summary>
        /// Read only. FerryCheckInName for this detail
        /// </summary>
        public string FerryCheckInName
        {
            get { return ferryCheckInName; }
        }

        /// <summary>
        /// Read only. FerryCheckIn flag for this detail
        /// </summary>
        public bool FerryCheckIn
        {
            get { return ferryCheckIn; }
        }

        /// <summary>
        /// Read only. FerryCheckOut flag for this detail
        /// </summary>
        public bool FerryCheckOut
        {
            get { return ferryCheckOut; }
        }

        /// <summary>
        /// Read only. DisplayFerryIcon flag for this detail
        /// </summary>
        public bool DisplayFerryIcon
        {
            get { return displayFerryIcon; }
        }

        /// <summary>
        /// Read only. TollEntryName for this detail
        /// </summary>
        public string TollEntryName
        {
            get { return tollEntryName; }
        }

        /// <summary>
        /// Read only. TollExitName for this detail
        /// </summary>
        public string TollExitName
        {
            get { return tollExitName; }
        }

        /// <summary>
        /// Read only. TollEntry flag for this detail
        /// </summary>
        public bool TollEntry
        {
            get { return tollEntry; }
        }

        /// <summary>
        /// Read only. TollExit flag for this detail
        /// </summary>
        public bool TollExit
        {
            get { return tollExit; }
        }

        /// <summary>
        /// Read only. DisplayTollIcon flag for this detail
        /// </summary>
        public bool DisplayTollIcon
        {
            get { return displayTollIcon; }
        }

        /// <summary>
        /// Read only. ViaLocation flag for this detail
        /// </summary>
        public bool ViaLocation
        {
            get { return viaLocation; }
        }

        /// <summary>
        /// Read only. WaitSection flag for this detail
        /// </summary>
        public bool WaitSection
        {
            get { return waitSection; }
        }

        /// <summary>
        /// Read only. UndefinedWaitSection flag for this detail
        /// </summary>
        public bool UndefinedWaitSection
        {
            get { return undefinedWaitSection; }
        }

        /// <summary>
        /// Read only. ComplexManoeuvre flag for this detail
        /// </summary>
        public bool ComplexManoeuvre
        {
            get { return complexManoeuvre; }
        }

        /// <summary>
        /// Read only. NamedAccessRestriction flag for this detail
        /// </summary>
        public bool NamedAccessRestriction
        {
            get { return namedAccessRestriction; }
        }

        /// <summary>
        /// Read only. NamedTimen flag for this detail
        /// </summary>
        public bool NamedTime
        {
            get { return namedTime; }
        }
        /// <summary>
        /// Read only. RoadJourneyDetailMapInfo for this detail
        /// </summary>
        public RoadJourneyDetailMapInfo RoadJourneyDetailMapInfo
        {
            get { return roadJourneyDetailMapInfo; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method returns the OSGR coordinates (the Geometry) of this detail, removing all the 
        /// grouping of the coordiantes and returning one array of OSGRs
        /// </summary>
        /// <returns></returns>
        public OSGridReference[] GetAllOSGRGridReferences()
        {
            // Temp array used to group together the coordinates
            ArrayList tempGridReferences = new ArrayList();

            if (geometry != null && geometry.Count > 0)
            {
                // Loop through each geometry there is for this detail
                foreach (KeyValuePair<int, OSGridReference[]> kvpGeometry in geometry)
                {                   
                    for (int i = 0; i < (kvpGeometry.Value.Length); i++)
                    {
                        OSGridReference osgr = kvpGeometry.Value[i];

                        if (osgr.IsValid)
                        {
                            tempGridReferences.Add(osgr);
                        }
                    }
                }
            }

            // Filter the grid references to remove adjacent OSGR duplicates. This is needed because each OSGR array
            // in the Geometry dictionary Starts with the previous's End OSGR.
            ArrayList gridReferences = new ArrayList();

            OSGridReference current = new OSGridReference();
            OSGridReference previous = new OSGridReference();

            for (int j = 0; j < tempGridReferences.Count; j++)
            {
                current = (OSGridReference)tempGridReferences[j];

                // Is this a duplicate?
                if ((current.Easting == previous.Easting) && (current.Northing == previous.Northing))
                {
                    // The current OSGR matches the previous OSGR so do not add.
                }
                else
                {
                    gridReferences.Add(current);
                }

                // Assign the previous OSGR ready for the next loop
                previous = current;
            }

            return (OSGridReference[])gridReferences.ToArray(typeof(OSGridReference));
        }

        /// <summary>
        /// Method which populates the Latitude Longitude coordiantes for this detail with coordinates provided
        /// </summary>
        /// <param name="coordinates"></param>
        public void UpdateLatitudeLongitudeCoordinates(LatitudeLongitude[] coordinates)
        {
            if (coordinates != null)
            {
                latlongCoordinates = coordinates;
            }
        }

        #endregion
    }
}
