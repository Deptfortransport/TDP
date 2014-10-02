// *********************************************** 
// NAME                 :WalkitURLHandoffHelper.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 16/09/2009 
// DESCRIPTION  		: Helper class to build walkit.com handoff url dynamically 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/WalkitURLHandoffHelper.cs-arc  $
//
//   Rev 1.24   Apr 15 2013 11:55:18   mmodi
//Check for > 2 naptans coordinates in navigation path to prevent walk only journeys issue in walkit link locations
//Resolution for 5923: Walkit - Leicester Square locality to the Gielgud theatre
//
//   Rev 1.23   Apr 02 2013 11:05:58   mmodi
//Correction for naptans count check
//Resolution for 5909: Walkit link for accessible journey not from Entrance naptan
//
//   Rev 1.22   Mar 27 2013 13:00:46   mmodi
//Use naptan for walkit link in accessible journey with navigation path leg
//Resolution for 5909: Walkit link for accessible journey not from Entrance naptan
//
//   Rev 1.21   Mar 27 2013 09:28:56   mmodi
//Updated to take a copy of the location object and work with that rather than the journey location object ref
//Resolution for 5908: Mapping walk leg shows intermediate change count icon in different place to actual start of leg
//
//   Rev 1.20   Feb 04 2013 10:40:50   mmodi
//Corrected coordinates for public journey interchange leg navigation path geometry
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.19   Dec 05 2012 13:54:40   mmodi
//Refactored code to improve re-use and readability, and updated to use detail navigation paths if available
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.18   Aug 20 2012 12:23:28   DLane
//Updating to use refer id specific to the walk link
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.17   Aug 17 2012 10:55:24   dlane
//Cycle walk links
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.16   Nov 30 2011 11:36:08   MTurner
//Added logic to allow checks for points in seperate walk areas.
//Resolution for 5767: New WalkIT data has seperate shapes for London Boroughs
//
//   Rev 1.15   Sep 14 2010 09:49:14   apatel
//Updated to correct the walkit link for return journey
//Resolution for 5603: Return Journey - Walkit Links Wrong
//
//   Rev 1.14   May 05 2010 10:34:36   apatel
//Updated to resolve the issue with the walkit url shouldn't be shown for walk to/from airport.
//Resolution for 5530: Incorrectly showing walkit link to/from airport
//
//   Rev 1.13   Mar 12 2010 09:12:54   apatel
//Refactor Walkit control to put most of the walkit logic in to helper class
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.12   Feb 25 2010 15:57:00   apatel
//Updated for GB location having invalid naptan but valid OSGR
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 24 2010 12:07:40   apatel
//Updated to handle invalid characters in label passed in to walkit url
//Resolution for 5407: Walkit Link - Invalid characters
//
//   Rev 1.10   Feb 21 2010 23:24:20   apatel
//Removed the invalid characters from label text passed in to walkit url
//Resolution for 5407: Walkit Link - Invalid characters
//
//   Rev 1.9   Jan 05 2010 13:38:48   apatel
//Resolve the issue with ':' in label. Change code to correctly convert PLT NaPTAN to MET NaPTAN.
//Resolution for 5353: Walkit Link issues
//
//   Rev 1.8   Dec 15 2009 09:52:10   apatel
//make walkit url fields name to come from properties and not hard coded.
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.7   Dec 14 2009 14:57:40   apatel
//resolve the issue with walkit parameter come with 'naptanDescription:postcode:' when description contains postcode
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.6   Dec 11 2009 11:57:34   apatel
//updated code so naptan locations don't have street names with it and for the points seleced on map/coordinates
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.5   Dec 09 2009 07:46:12   apatel
//updated for label trimming code
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.4   Dec 08 2009 15:59:22   apatel
//Walkit link code
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Dec 08 2009 11:07:52   pghumra
//Corrected walkit URL parameters
//
//   Rev 1.2   Dec 04 2009 11:54:04   apatel
//removed the "naptan" string at start when link built using naptanid
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.1   Dec 04 2009 09:20:44   pghumra
//walkit control files
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.0   Nov 11 2009 16:52:42   pghumra
//Initial revision.
//Resolution for 5334: CCN0538 Page Land on Walkit.com


using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using CCP = TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// WalkitURLHandoffHelper class
    /// </summary>
    public class WalkitURLHandoffHelper
    {
        #region Private Fields
        private TDLocation origin;
        private TDLocation destination;
        private bool isWalkAtStartOrEnd;
        private string invalidCharactersRegex = string.Empty;
        private string walkitCity = string.Empty;

        private bool useCycleWalkReferrer = false;

        private string WALKIT_FROM = "walkit_from";
        private string WALKIT_TO = "walkit_to";
        private string WALKIT_CITY = "walkit_city";
        #endregion

        #region Constructors

        /// <summary>
        /// WalkitURLHandoffHelper Constructor
        /// </summary>
        public WalkitURLHandoffHelper(Journey journey, JourneyLeg journeyLeg, int journeyLegIndex, ITDJourneyRequest journeyRequest, bool outward)
        {
            InitResources();

            bool validWalkitJourney = false;
            
            #region Check leg duration

            // Determine if leg detail is at least the mininum duration otherwise it shouldnt be shown
            int minJourneyDuration = 0;

            int journeyDurationInMins = journeyLeg.Duration / 60;
            int.TryParse(Properties.Current["WalkitLinkControl.MinJourneyDetailDuration"], out minJourneyDuration);

            int minJourneyStartEndDuration = 5;
            int.TryParse(Properties.Current["WalkitLinkControl.MinJourneyStartEndDuration"], out minJourneyStartEndDuration);

            // Is leg minimum duration, 
            // or if start/end leg then is that at the minimum start/end leg duration
            if (((journeyLegIndex > 0 && journeyLegIndex < journey.JourneyLegs.Length - 1) && (journeyDurationInMins >= minJourneyDuration))
                || (((journeyLegIndex == 0) || (journeyLegIndex == journey.JourneyLegs.Length - 1)) && (journeyDurationInMins >= minJourneyStartEndDuration))
                || (WalkOnlyJourney(journey)))
            {
                validWalkitJourney = true;
            }
            else
            {
                validWalkitJourney = false;
            }

            #endregion

            TDLocation startLocation = GetJourneyLegLocation(true, journeyLeg, journeyLegIndex, journey, outward, journeyRequest);
            TDLocation endLocation = GetJourneyLegLocation(false, journeyLeg, journeyLegIndex, journey, outward, journeyRequest);
            
            #region Check in Walkit data area

            //Determine whether walk data is available to Walkit. Proceed only in the affirmative
            int walkit_ID = 0;

            validWalkitJourney = validWalkitJourney && IsPointsInWalkDataArea(
                startLocation.OSGRAsPoint(),
                endLocation.OSGRAsPoint(),
                out walkit_ID, out walkitCity);

            if ((validWalkitJourney) && (!string.IsNullOrEmpty(walkitCity)) && (walkit_ID != -1))
            {
                // Update latitude/longitude coords
                validWalkitJourney = SetLatitudeLongitude(ref startLocation);
                validWalkitJourney = validWalkitJourney && SetLatitudeLongitude(ref endLocation);

                if (validWalkitJourney)
                {
                    // Assign to class variables for use when the GetWalkitHandoffURL method is called
                    origin = startLocation;
                    destination = endLocation;
                }
            }

            #endregion
        }

        /// <summary>
        /// WalkitURLHandoffHelper Constructor taking origin and destination only
        /// </summary>
        public WalkitURLHandoffHelper(TDLocation origin, TDLocation destination)
        {
            InitResources();

            bool validWalkitJourney = false;

            SetLatitudeLongitude(ref origin);
            SetLatitudeLongitude(ref destination);

            this.origin = origin;
            this.destination = destination;

            useCycleWalkReferrer = true;

            #region Check in Walkit data area

            //Determine whether walk data is available to Walkit. Proceed only in the affirmative
            int walkit_ID = 0;

            validWalkitJourney = IsPointsInWalkDataArea(origin.OSGRAsPoint(), destination.OSGRAsPoint(), out walkit_ID, out walkitCity);

            if ((validWalkitJourney) && (!string.IsNullOrEmpty(walkitCity)) && (walkit_ID != -1))
            {
                isWalkAtStartOrEnd = true;
            }

            #endregion
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Constructs Walkit.com handoff url for walk leg of journey
        /// </summary>
        /// <returns>Walkit.com handoff url</returns>
        public string GetWalkitHandoffURL()
        {
            if (origin == null || destination == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(walkitCity))
            {
                return string.Empty;
            }

            // if the location is an airport don't generate walkit landing url
            if (IsAirportLocation(origin) || IsAirportLocation(destination))
            {
                return string.Empty;
            }

            string fromParamVal = BuildWalkitParameter(origin);

            string toParamVal = BuildWalkitParameter(destination);

            string baseUrl = string.Empty;
            if (useCycleWalkReferrer)
            {
                baseUrl = Properties.Current["CycleWalkLinks.WalkitControlBaseUrl"];
            }
            else
            {
                baseUrl = Properties.Current["WalkitControl.BaseUrl"];
            }

            //Build the string of parameters
            StringBuilder parameterString = new StringBuilder(string.Empty);

            parameterString.Append(WALKIT_FROM);
            parameterString.Append("=");
            parameterString.Append(HttpUtility.UrlEncode(fromParamVal));
            parameterString.Append("&");

            parameterString.Append(WALKIT_TO);
            parameterString.Append("=");
            parameterString.Append(HttpUtility.UrlEncode(toParamVal));
            parameterString.Append("&");

            parameterString.Append(WALKIT_CITY);
            parameterString.Append("=");
            parameterString.Append(HttpUtility.UrlEncode(walkitCity));

            return string.Format("{0}{1}",baseUrl,parameterString.ToString());
        }

        #endregion

        #region Private Methods

        #region Build Walkit

        /// <summary>
        /// Build walkit location paramater from the TDLocation object provided
        /// </summary>
        /// <param name="location">TDLocation object</param>
        /// <returns>string representing location in walkit location parameter structure</returns>
        private string BuildWalkitParameter(TDLocation location)
        {
            string param = string.Empty;
            string label = string.Empty;
            string walkitlocation = string.Empty;
            string streetName = string.Empty;
            bool validNaptan = false;

            if (location.NaPTANs.Length > 0)
            {
                validNaptan = IsValidNaptan(location.NaPTANs[0].Naptan,location.Description);
            }

            if (isWalkAtStartOrEnd)
            {
                #region Start/End of journey location

                if ((PostcodeSyntaxChecker.IsPartPostCode(location.Description)) || (PostcodeSyntaxChecker.IsPostCode(location.Description)))
                {
                    walkitlocation = location.Description;
                }
                else if (location.RequestPlaceType == RequestPlaceType.NaPTAN)
                {
                    if (location.NaPTANs.Length > 0)
                    {
                        TDNaptan tdNaptan = location.NaPTANs[0];

                        walkitlocation = "naptan" + CheckAndConvertMETNaPTAN(tdNaptan.Naptan);

                        if (tdNaptan.Naptan.StartsWith("9000") || !validNaptan)
                        {
                            walkitlocation = string.Format("{0},{1}", location.LatitudeLongitudeCoordinate.Latitude, location.LatitudeLongitudeCoordinate.Longitude);
                        }
                    }
                }
                else if (location.RequestPlaceType == RequestPlaceType.Locality)
                {
                    walkitlocation = string.Format("{0},{1}", location.LatitudeLongitudeCoordinate.Latitude, location.LatitudeLongitudeCoordinate.Longitude);
                }
                else if (location.RequestPlaceType == RequestPlaceType.Coordinate)
                {
                    walkitlocation = string.Format("{0},{1}", location.LatitudeLongitudeCoordinate.Latitude, location.LatitudeLongitudeCoordinate.Longitude);
                }
                else
                {
                    //if location contains an address get the street name
                    streetName = GetStreetName(location);
                    if (!string.IsNullOrEmpty(streetName))
                    {
                        streetName = "@" + streetName;
                    }
                    walkitlocation = string.Format("{0},{1}", location.LatitudeLongitudeCoordinate.Latitude, location.LatitudeLongitudeCoordinate.Longitude);
                }

                #endregion
            }
            else
            {
                if (location.NaPTANs.Length > 0)
                {
                    TDNaptan tdNaptan = location.NaPTANs[0];

                    walkitlocation = "naptan" + CheckAndConvertMETNaPTAN(tdNaptan.Naptan);

                    if (tdNaptan.Naptan.StartsWith("9000") || !validNaptan)
                    {
                        walkitlocation = string.Format("{0},{1}", location.LatitudeLongitudeCoordinate.Latitude, location.LatitudeLongitudeCoordinate.Longitude);
                    }
                }
            }

            label = BuildWalkitLocationLabel(location);
            param = string.Format("{0}{1}{2}", walkitlocation, streetName, label);

            return param;
        }

        /// <summary>
        /// Builds a walkit location label to pass with walkit location parameter.
        /// Location label starts and ends with ':'
        /// </summary>
        /// <param name="location">TD location object</param>
        /// <returns>string giving location label to pass in walkit url</returns>
        private string BuildWalkitLocationLabel(TDLocation location)
        {
            string label = string.Empty;

            
            if (location.NaPTANs.Length > 0)
            {
                label = string.Format("{0}", location.NaPTANs[0].Name);
            }

            
            if (isWalkAtStartOrEnd)
            {
                if ((PostcodeSyntaxChecker.IsPartPostCode(location.Description)) || (PostcodeSyntaxChecker.IsPostCode(location.Description)))
                {
                    label = string.Empty;
                }
                //if the description is point-x only the first element in the text will be retained in the label
                else if (PostcodeSyntaxChecker.ContainsPostcode(location.Description))
                {
                    if (!string.IsNullOrEmpty(label))
                    {
                        string[] labelSubStrings = label.Split(new char[] { ',' });

                        if (labelSubStrings.Length > 1)
                        {
                            label = string.Format("{0},{1}", labelSubStrings[0], labelSubStrings[1]);
                        }
                        else
                        {
                            label = labelSubStrings[0];
                        }
                    }
                } 
                else if (location.RequestPlaceType == RequestPlaceType.NaPTAN)
                {
                    if (location.NaPTANs.Length > 0)
                    {
                        TDStopType stopType = GetStopType(location.NaPTANs[0].Naptan);

                        //if the description is a string with no commas use the whole string.
                        //if not the name will either be clipped at the first closed bracket after the first comma or
                        //the third comma, which ever occurs first.
                        if(stopType != TDStopType.Air && stopType != TDStopType.Rail 
                            && stopType != TDStopType.Ferry && stopType != TDStopType.LightRail)
                        {
                            if (!location.NaPTANs[0].Naptan.StartsWith("9000"))
                            {
                               
                                if (label.IndexOf(',') > -1)
                                {
                                    string[] labelSubStrings = label.Split(new char[] { ',' });

                                    
                                    if (labelSubStrings.Length > 2)
                                    {
                                        if (labelSubStrings[2].IndexOf(')') > 0)
                                        {
                                            label = string.Format("{0}, {1}, {2}", labelSubStrings[0], labelSubStrings[1], labelSubStrings[2].Substring(0, labelSubStrings[2].IndexOf(')')+1));
                                        }
                                        else
                                        {
                                            label = string.Format("{0}, {1}, {2}", labelSubStrings[0], labelSubStrings[1], labelSubStrings[2]);
                                        }
                                    }
                                    else
                                    {
                                        if (labelSubStrings[1].IndexOf(')') > 0)
                                        {
                                            label = string.Format("{0}, {1}", labelSubStrings[0], labelSubStrings[1].Substring(0, labelSubStrings[1].IndexOf(')')+1));
                                        }
                                        else
                                        {
                                            label = string.Format("{0}, {1}", labelSubStrings[0], labelSubStrings[1]);
                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                }
                else if (location.RequestPlaceType == RequestPlaceType.Locality)
                {
                    //No trimming to do
                }
                else
                {
                    if (!string.IsNullOrEmpty(label))
                    {
                        if (label.IndexOf(",") > -1)
                        {
                            label = label.Substring(0, label.IndexOf(","));
                        }
                        
                    }
                }
            }
            else
            {
                if (location.NaPTANs.Length > 0)
                {
                    //No trimming to do
                }
            }
            if (!string.IsNullOrEmpty(label))
            {
                if (!string.IsNullOrEmpty(invalidCharactersRegex))
                {
                    label = Regex.Replace(label, invalidCharactersRegex, string.Empty);
                }
                

                label = ":" + label + ":";
            }
            return label;
        }
        
        #region Build Walkit helper methods

        /// <summary>
        /// Gets the street name hint for the location
        /// </summary>
        /// <param name="location">Td location object</param>
        /// <returns>string giving street name hint for walkit</returns>
        private string GetStreetName(TDLocation location)
        {
            string streetName = string.Empty;
            string[] streetNames;
            try
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                if ((PostcodeSyntaxChecker.IsPartPostCode(location.Description)) || (PostcodeSyntaxChecker.IsPostCode(location.Description)))
                {
                    streetNames = gisQuery.GetStreetsFromPostCode(location.Description);
                    if (streetNames.Length > 0)
                    {
                        streetName = streetNames[0];
                    }
                }
                // if location description contains address get the postcode out of it to get the street name hint
                else if (PostcodeSyntaxChecker.ContainsPostcode(location.Description))
                {
                    string postcode = location.Description.Substring(location.Description.LastIndexOf(',') + 1);

                    if (!string.IsNullOrEmpty(postcode))
                    {
                        streetNames = gisQuery.GetStreetsFromPostCode(postcode);
                        if (streetNames.Length > 0)
                        {
                            streetName = streetNames[0];
                        }
                    }
                }
                else if (location.NaPTANs.Length > 0)
                {
                    QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { location.NaPTANs[0].Naptan });

                    for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                    {
                        QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                        streetName = row.street;
                    }
                }

            }
            catch (Exception exception)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Walkit Link Exception." + exception.Message);

                Logger.Write(operationalEvent);

            }
            return streetName;
        }

        /// <summary>
        /// Check for naptan and if the naptan is 'PLT' converts it to MLT naptan
        /// </summary>
        /// <param name="naptan">naptan</param>
        /// <returns>string representing MLT naptan</returns>
        private string CheckAndConvertMETNaPTAN(string naptan)
        {
            string stopType = string.Empty;

             try
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { naptan });


                for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                {
                    QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                    stopType = row.stoptype;
                }

                if (stopType.Trim() == "PLT")
                {
                    //convert it to MLT by removing the final numeric character
                    // The structure of a 9400 NaPTAN is as follows: 
                    //9400ZZssccc[p] – where ss is the light rail system, for example 
                    //London Underground, South Yorkshire tram, ccc is the station code 
                    //and p, which is optional is the platform number. 

                    if (naptan.Length > 11)
                    {
                        naptan = naptan.Substring(0, 11);
                    }

                }

            }
             catch (Exception exception)
             {
                 
                 // Log the exception
                 OperationalEvent operationalEvent = new OperationalEvent
                     (TDEventCategory.Business, TDTraceLevel.Error, "Walkit Url Handoff Exception." + exception.Message);

                 Logger.Write(operationalEvent);

             }

             return naptan;
        }

        /// <summary>
        /// Gets the stop type using naptan of the stop
        /// </summary>
        /// <param name="naptan">Naptan of the stop</param>
        /// <returns></returns>
        private TDStopType GetStopType(string naptan)
        {
            string stopType = string.Empty;
            TDStopType tdStopType = TDStopType.Unknown;
           
            try
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { naptan });


                for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                {
                    QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                    stopType = row.stoptype;
                }

                #region Determining stop type
                switch (stopType)
                {
                    case "AIR":	// Air
                    case "GAT":
                        tdStopType = TDStopType.Air;
                        break;
                    case "BCE":	// coach
                    case "BST":
                        tdStopType = TDStopType.Coach;
                        break;
                    case "BCQ":
                    case "BCT":	// Bus
                    case "BCS":
                        tdStopType = TDStopType.Bus;
                        break;
                    case "RLY":	// Rail
                    case "RPL":
                    case "RSE":
                        tdStopType = TDStopType.Rail;
                        break;
                    case "MET":	// Light/rail
                    case "PLT":
                    case "TMU":
                        tdStopType = TDStopType.LightRail;
                        break;
                    case "TXR":	// Taxi
                    case "STR":
                        tdStopType = TDStopType.Taxi;
                        break;
                    case "FER":	// Ferry
                    case "FTD":
                        tdStopType = TDStopType.Ferry;
                        break;
                    default:
                        tdStopType = TDStopType.Unknown;
                        break;
                }
                #endregion


            }
            catch (Exception exception)
            {
                tdStopType = TDStopType.Unknown;

                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Walkit Url Handoff Exception." + exception.Message);

                Logger.Write(operationalEvent);

            }

           
            return tdStopType;
        }

        /// <summary>
        /// Checks the naptan cache to find the naptan, returns true if found
        /// </summary>
        /// <param name="naptan"></param>
        /// <returns></returns>
        private bool IsValidNaptan(string naptan, string description)
        {
            // Lookup naptan in cache and/or GIS query ...
            NaptanCacheEntry naptanCacheEntry = NaptanLookup.Get(naptan, description);

            return naptanCacheEntry.Found;
        }

        /// <summary>
        /// Checks if the location is an airport
        /// </summary>
        /// <param name="location">TDLocation object</param>
        /// <returns>true if the location is an airport</returns>
        private bool IsAirportLocation(TDLocation location)
        {
            bool isAirport = false;
            if (location.ContainsNaptansForStationType(StationType.Airport)
                    || location.ContainsNaptansForStationType(StationType.AirportNoGroup))
            {
                isAirport = true;
            }
            return isAirport;
        }

        #endregion

        #endregion

        /// <summary>
        /// Initialises resources
        /// </summary>
        private void InitResources()
        {
            WALKIT_FROM = Properties.Current["WalkitControl.URL.From"];
            WALKIT_TO = Properties.Current["WalkitControl.URL.To"];
            WALKIT_CITY = Properties.Current["WalkitControl.URL.City"];
            invalidCharactersRegex = Properties.Current["WalkitLinkControl.InvalidCharacters.Regex"];
        }

        /// <summary>
        /// Returns the location to use for the walkit location
        /// </summary>
        /// <param name="isStartLoc">Start or end of leg</param>
        /// <param name="journeyLeg">Journey leg to select locations from</param>
        /// <param name="journeyLegIndex">Journey leg index used to identify first/last leg of journey</param>
        /// <param name="journey">Journey containing all journey legs</param>
        /// <param name="outward">For outward journey</param>
        /// <param name="journeyRequest">Journey request containing origin and destination of the request</param>
        /// <returns></returns>
        private TDLocation GetJourneyLegLocation(bool isStartLoc,
            JourneyLeg journeyLeg, int journeyLegIndex, Journey journey,
            bool outward, ITDJourneyRequest journeyRequest)
        {
            // Clone to prevent updating the location in the journey result
            TDLocation location = isStartLoc ? journeyLeg.LegStart.Location.Clone() : journeyLeg.LegEnd.Location.Clone();

            #region Use journey request origin/destination locations if the leg is at the start/end of journey

            // The following updates the location to be that of the journey request.
            // Updates for the following scenarios
            // - Start of walkit should use the JourneyRequest OriginLocation if first leg of journey.
            // - End of walkit should use the JourneyRequest DestinationLocation if last leg of journey.
            // (and switched around if return journey)

            // Check for null JourneyRequest
            if (journeyRequest != null)
            {
                // Start location at the beginning of the journey
                if ((isStartLoc) && (journeyLegIndex == 0))
                {
                    if (journeyRequest.OriginLocation.SearchType != SearchType.MainStationAirport)
                    {
                        // start location
                        location = outward ? journeyRequest.OriginLocation : journeyRequest.DestinationLocation;
                        location.NaPTANs = journeyLeg.LegStart.Location.NaPTANs;
                    }
                    isWalkAtStartOrEnd = true;
                }
                
                // End location at the end of the journey
                if ((!isStartLoc) && (journeyLegIndex == journey.JourneyLegs.Length - 1))
                {
                    if (journeyRequest.DestinationLocation.SearchType != SearchType.MainStationAirport)
                    {
                        // end location
                        location = outward ? journeyRequest.DestinationLocation : journeyRequest.OriginLocation;
                        location.NaPTANs = journeyLeg.LegEnd.Location.NaPTANs;
                    }
                    isWalkAtStartOrEnd = true;
                }
            }

            #endregion

            #region Use Navigation path coordinate for Interchange journey leg

            // The following updates the coordinate to be that of the Navigation path returned for an interchange leg.
            // The navigation path provides more accurate coordinates, e.g. the entrance to an underground 
            // station, rather than the platform. This is stored in the legs geometry. 
            // Therefore in the coords below, A would represent the leg start (e.g. bus stop), 
            // G the leg end (e.g. platform), and B to F the path (e.g. from stop to platform including entrance).
            // A -> B -> C -> D -> E -> F -> G
            //
            // Updates for the following scenarios:
            // - End of walkit leg for the first leg of the journey (i.e. the entrance, the start coord will be the journey leg origin)
            // - Start of walkit leg for the last leg of the journey (i.e. the entrance, the end will be the journey leg destination)

            if (journeyLeg is PublicJourneyInterchangeDetail)
            {
                PublicJourneyInterchangeDetail pjid = (PublicJourneyInterchangeDetail)journeyLeg;

                // End location at the beginning of the journey
                if ((!isStartLoc) && (journeyLegIndex == 0))
                {
                    // Check for more than 2, any less than it just contains the leg start and end 
                    // so do not change as its ok as set in location above
                    if (pjid.Geometry != null && pjid.Geometry.Length > 2)
                    {
                        // Find first coord in the navigation path (e.g. the entrance to underground station),
                        // this will be the second in the geometry as the first will be the leg start location
                        location.GridReference = pjid.Geometry[1];
                    }

                    if (location.RequestPlaceType == RequestPlaceType.NaPTAN && pjid.NaPTANsPath.Count > 2)
                    {
                        // Find first naptan in the navigation path (e.g. the entrance to underground station),
                        // this will be the second in the naptans path as the first will be the leg start location
                        string naptan = pjid.NaPTANsPath[1];

                        location.NaPTANs = new TDNaptan[1];
                        location.NaPTANs[0] = new TDNaptan(naptan, location.GridReference, location.Description);
                    }
                }

                // Start location at the end of the journey
                if ((isStartLoc) && (journeyLegIndex == journey.JourneyLegs.Length - 1))
                {
                    // Check for more than 2, any less than it just contains the leg start and end 
                    // so do not change as its ok as set in location above
                    if (pjid.Geometry != null && pjid.Geometry.Length > 2)
                    {
                        // Find the last coord in the navigation path (e.g. the exit from underground station),
                        // this will be the second from last in the geometry as the last will be the leg end location
                        location.GridReference = pjid.Geometry[pjid.Geometry.Length - 2];
                    }

                    if (location.RequestPlaceType == RequestPlaceType.NaPTAN && pjid.NaPTANsPath.Count > 2)
                    {
                        // Find the last naptan in the navigation path (e.g. the exit from underground station),
                        // this will be the second from last in the naptans path as the last will be the leg end location
                        string naptan = pjid.NaPTANsPath[pjid.NaPTANsPath.Count - 2];

                        location.NaPTANs = new TDNaptan[1];
                        location.NaPTANs[0] = new TDNaptan(naptan, location.GridReference, location.Description);
                    }
                }

            }         

            #endregion

            return location;
        }

        /// <summary>
        /// determines if journey leg's start and end points are in walk data area of walkit.com
        /// </summary>
        /// <param name="journeyLeg">walk leg of the journey</param>
        /// <returns>true if the journey leg's start and end point are in walkit.com walk area</returns>
        private bool IsPointsInWalkDataArea(Point startPoint, Point endPoint, out int walkit_ID, out string walkit_city)
        {
            bool pointsInWalkArea = false;
            try
            {
                bool planSameAreaOnly;
                bool.TryParse(Properties.Current["WalkitControl.PlanSameAreaOnly"], out planSameAreaOnly);
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                //Determine whether walk data is available
                int walkitID = 0;
                string city = string.Empty;
                if (planSameAreaOnly)
                {
                    pointsInWalkArea = gisQuery.IsPointsInWalkDataArea(new Point[] { startPoint, endPoint }, true, out walkitID, out city);
                }
                else
                {
                    pointsInWalkArea = gisQuery.IsPointsInWalkDataArea(new Point[] { endPoint }, true, out walkitID, out city);
                    if (pointsInWalkArea)
                    {
                        pointsInWalkArea = gisQuery.IsPointsInWalkDataArea(new Point[] { startPoint }, true, out walkitID, out city);
                    }
                }
                walkit_ID = walkitID;
                walkit_city = city;
            }
            catch (Exception ex)
            {
                string message = "WalkitLinkControl - Quering GIS for points in walk data area threw an exception: " + ex.Message;

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));
                walkit_ID = 0;
                walkit_city = string.Empty;
            }

            return pointsInWalkArea;
        }

        /// <summary>
        /// Determines if the current journey is walk only journey as a whole or is a mix mode journey
        /// </summary>
        /// <param name="journey">Current journey</param>
        /// <returns>true if the journey is walk only journey</returns>
        private bool WalkOnlyJourney(Journey journey)
        {
            bool walkOnly = true;

            foreach (ModeType mode in journey.GetUsedModes())
            {
                if (mode != ModeType.Walk)
                {
                    walkOnly = false;
                }
            }

            return walkOnly;
        }

        #region Set LatitudeLongitude coordinates

        /// <summary>
        /// Converts OSGridReference data to Longitude and Latitude and set in to the TDLocation
        /// </summary>
        /// <param name="location">TDLocation</param>
        /// <returns>true if the conversion to LatitudeLongitude is successful</returns>
        private bool SetLatitudeLongitude(ref TDLocation location)
        {
            bool success = false;

            try
            {
                ICoordinateConvertor coordinateConvertor = (ICoordinateConvertor)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoordinateConvertorFactory];

                CCP.OSGridReference gridRef = GetCoordinateConvertorGridReference(location);

                CCP.LatitudeLongitude latitudeLongitude = coordinateConvertor.GetLatitudeLongitude(gridRef);

                if (latitudeLongitude != null)
                {
                    location.LatitudeLongitudeCoordinate.Latitude = latitudeLongitude.Latitude;
                    location.LatitudeLongitudeCoordinate.Longitude = latitudeLongitude.Longitude;

                    success = true;

                }
            }
            catch (Exception ex)
            {
                string message = "WalkitLinkControl - Populating the LatitudeLongitude coordinates using the CoordinateConvertor threw an exception: " + ex.Message;

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));
            }

            return success;

        }

        /// <summary>
        /// Converts location service OsGridReference to Coordinate converter web service OSGridReference
        /// </summary>
        /// <param name="location">TD location object</param>
        /// <returns>Coordinate converter web service OSGridReference</returns>
        private CCP.OSGridReference GetCoordinateConvertorGridReference(TDLocation location)
        {
            CCP.OSGridReference gridReference = new CCP.OSGridReference();

            gridReference.Easting = location.GridReference.Easting;

            gridReference.Northing = location.GridReference.Northing;

            return gridReference;
        }

        #endregion
                
        #endregion
    }
}
