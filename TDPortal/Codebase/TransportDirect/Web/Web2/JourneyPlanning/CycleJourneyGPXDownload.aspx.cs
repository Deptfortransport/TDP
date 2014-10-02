// *********************************************** 
// NAME                 : CycleJourneyGPXDownload.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/06/2008
// DESCRIPTION          : A download page for a cycle journey GPX file
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/CycleJourneyGPXDownload.aspx.cs-arc  $ 
//
//   Rev 1.10   Jun 03 2009 11:28:42   mmodi
//Reworked to use latitude longitude values contained in the cycle journey, and updated logging/Page entry event
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.9   Apr 28 2009 11:03:10   mmodi
//Updated create filename logic
//Resolution for 5278: Cycle Planner - GPX file length too long
//Resolution for 5279: Cycle Planner - GPX File Failure when name contains a forward-slash
//
//   Rev 1.8   Jan 16 2009 10:18:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.7   Dec 11 2008 11:31:06   mmodi
//Capitalised the Dot symbol
//Resolution for 5203: Cycle Planner - GPX download should contrain Track points only
//
//   Rev 1.6   Dec 10 2008 14:36:06   mmodi
//Changed track to route to allow directions to be shown
//Resolution for 5203: Cycle Planner - GPX download should contrain Track points only
//
//   Rev 1.5   Dec 10 2008 11:25:54   mmodi
//Updated to support return journeys, and to only create a Track gpx download file
//Resolution for 5195: Cycle Planner - GPX download file is not generated for the Return journey
//Resolution for 5203: Cycle Planner - GPX download should contrain Track points only
//
//   Rev 1.4   Oct 31 2008 11:39:50   mturner
//Changed to allow the location of the GPX files to be somewhere other than a sub directory of Web2
//
//   Rev 1.3   Oct 23 2008 10:11:40   mmodi
//Improved efficiency of calculating latitude longitude
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Oct 22 2008 15:41:26   mmodi
//Updated to generate GPX file 
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Sep 08 2008 15:50:22   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 14:02:48   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Xsl;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    public partial class CycleJourneyGPXDownload : TDPage
    {
        #region Private members

        private ITDSessionManager sessionManager;
        
        private const string FILEEXTENSIONGPX = ".gpx";
        private const string FILENAMEDATETIMEFORMAT = "yyyyMMddHHmmss";

        private const string FILENAMEPATH = "CyclePlanner.GPXDownload.File.Path";
        private const string DOWNLOADPATH = "CyclePlanner.GPXDownload.Download.Path";

        private const string FILENAMEMAXLENGTH = "CyclePlanner.GPXDownload.File.MaxLength";

        private bool outward = true;

        #endregion

        #region Constructor
        /// <summary>
		/// Default Constructor
		/// </summary>
        public CycleJourneyGPXDownload()
		{
            pageId = PageId.CycleJourneyGPXDownload;
        }
        #endregion

        #region Page_Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            sessionManager = TDSessionManager.Current;

            ITDCyclePlannerRequest tdCyclePlannerRequest = sessionManager.CycleRequest;
            ITDCyclePlannerResult tdCyclePlannerResult = sessionManager.CycleResult;

            // Check if this is for the outward or return journey
            // Default to outward
            string outwardQueryString = Request.QueryString["outward"];
            if (!bool.TryParse(outwardQueryString, out outward))
            {
                outward = true;
            }
            
            // Prevent attempt to create GPX file when no cycle journeys exist
            if ((tdCyclePlannerResult != null) && 
                    (
                        (outward && (tdCyclePlannerResult.OutwardCycleJourneyCount > 0))
                        ||
                        (!outward && (tdCyclePlannerResult.ReturnCycleJourneyCount > 0))
                    )
               )
            {
                // Create the GPX file
                string gpxDownloadFile = GenerateGPXFile(tdCyclePlannerRequest, tdCyclePlannerResult);

                if (!string.IsNullOrEmpty(gpxDownloadFile))
                {
                    string downloadPath = Properties.Current[DOWNLOADPATH];

                    #region Log and Page Entry Event
                    // Log successfully created file
                    string message = string.Format("CycleJourneyGPXDownload page successfully created GPX file [{0}].", gpxDownloadFile);
                    OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, message);
                    Logger.Write(operationalEvent);

                    // Log the page activity to the TD Page Entry Event.
                    // Done here because we redirect to GPX file, so the PEE code is not hit in the base TDPage class
                    PageEntryEvent logPage = new PageEntryEvent(pageId, Session.SessionID, TDSessionManager.Current.Authenticated);
                    Logger.Write(logPage);
                    #endregion

                    // GPX file has been created so redirect user to the file
                    Response.Redirect(downloadPath + gpxDownloadFile, true);
                }
            }
            else
            {
                // Arrived to this page with an invalid cycle journey details (e.g. Null, or 0 journey count)
                string message = "CycleJourneyGPXDownload page accessed without a cycle journey in the session";
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Infrastructure, TDTraceLevel.Verbose, message);
                Logger.Write(operationalEvent);
            }

            // If we reach this point, then the GPX file was not successfully created, so we should 
            // display an error message to the user
            DisplayErrorMessage();

            // Navigation not needed
            headerControl.ShowNavigation = false;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generates the GPX file for a cycle journey.
        /// Saves the file on a file share and returns the full path to the file
        /// </summary>
        /// <returns></returns>
        private string GenerateGPXFile(ITDCyclePlannerRequest tdCyclePlannerRequest, ITDCyclePlannerResult tdCyclePlannerResult)
        {
            try
            {
                string message = "CycleJourneyGPXDownload page - generating GPX file for cycle journey.";
                OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, message);
                Logger.Write(operationalEvent);

                CycleJourney cycleJourney = (outward ? tdCyclePlannerResult.OutwardCycleJourney() : tdCyclePlannerResult.ReturnCycleJourney());

                // Set up the files 
                string fileName = GenerateFileName(tdCyclePlannerRequest);
                string filePath = Properties.Current[FILENAMEPATH];

                if (cycleJourney != null)
                {
                    // Check the latitude longitude coordinates were set correctly during the journey response processing from the Cycle Trip Planner.
                    if (cycleJourney.LatitudeLongitudesAreValid)
                    {
                        // local variables
                        string dateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
                        string journeyName = cycleJourney.OriginLocation.Description + " to " + cycleJourney.DestinationLocation.Description;

                        double lattitude = 0;
                        double longitude = 0;
                        double startLat = 0;
                        double startLon = 0;
                        double endLat = 0;
                        double endLon = 0;

                        // Start coordinates
                        startLat = cycleJourney.OriginLocation.LatitudeLongitudeCoordinate.Latitude;
                        startLon = cycleJourney.OriginLocation.LatitudeLongitudeCoordinate.Longitude;

                        // End coordinates
                        endLat = cycleJourney.DestinationLocation.LatitudeLongitudeCoordinate.Latitude;
                        endLon = cycleJourney.DestinationLocation.LatitudeLongitudeCoordinate.Longitude;

                        // Xml document settings
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Encoding = Encoding.UTF8;
                        settings.Indent = true;
                        settings.ConformanceLevel = ConformanceLevel.Document;
                        settings.OmitXmlDeclaration = false;

                        // Create and set up the GPX file
                        using (XmlWriter xmlWriter = XmlWriter.Create(filePath + fileName, settings))
                        {
                            xmlWriter.WriteStartDocument();

                            #region Header (MetaData)
                            // <gpx>
                            xmlWriter.WriteStartElement("gpx", "http://www.topografix.com/GPX/1/1");

                            xmlWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                            xmlWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                            xmlWriter.WriteAttributeString("xsi", "schemaLocation", null, "http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd");
                            xmlWriter.WriteAttributeString("version", "1.1");
                            xmlWriter.WriteAttributeString("creator", "TransportDirect.info");

                            // <metadata>
                            xmlWriter.WriteStartElement("metadata");

                            // <desc>
                            xmlWriter.WriteStartElement("desc");
                            xmlWriter.WriteString("GPX file for " + journeyName);
                            xmlWriter.WriteEndElement();

                            // <author>
                            xmlWriter.WriteStartElement("author");
                            xmlWriter.WriteStartElement("name");
                            xmlWriter.WriteString("TransportDirect.info");
                            xmlWriter.WriteEndElement(); //</name>
                            xmlWriter.WriteEndElement(); //</author>

                            // <time>
                            xmlWriter.WriteStartElement("time");
                            xmlWriter.WriteString(DateTime.Now.ToString(dateTimeFormat));
                            xmlWriter.WriteEndElement();

                            xmlWriter.WriteEndElement(); // </metadata>
                            #endregion

                            #region Locations (Waypoints)

                            // Start Location
                            xmlWriter.WriteStartElement("wpt");
                            xmlWriter.WriteAttributeString("lat", startLat.ToString());
                            xmlWriter.WriteAttributeString("lon", startLon.ToString());

                            xmlWriter.WriteStartElement("name");
                            xmlWriter.WriteValue("Start: " + cycleJourney.OriginLocation.Description);
                            xmlWriter.WriteEndElement(); // </name>                      

                            xmlWriter.WriteStartElement("desc");
                            xmlWriter.WriteString("Start of journey at " + cycleJourney.OriginLocation.Description);
                            xmlWriter.WriteEndElement(); // </desc>

                            xmlWriter.WriteStartElement("sym");
                            xmlWriter.WriteValue("Waypoint");
                            xmlWriter.WriteEndElement(); // </sym>

                            xmlWriter.WriteEndElement(); // </wpt>

                            // Via Location
                            if ((cycleJourney.RequestedViaLocation != null) &&
                                (!string.IsNullOrEmpty(cycleJourney.RequestedViaLocation.Description)))
                            {
                                // Via coordinates
                                lattitude = cycleJourney.RequestedViaLocation.LatitudeLongitudeCoordinate.Latitude;
                                longitude = cycleJourney.RequestedViaLocation.LatitudeLongitudeCoordinate.Longitude;

                                xmlWriter.WriteStartElement("wpt");
                                xmlWriter.WriteAttributeString("lat", lattitude.ToString());
                                xmlWriter.WriteAttributeString("lon", longitude.ToString());

                                xmlWriter.WriteStartElement("name");
                                xmlWriter.WriteValue("Via: " + cycleJourney.RequestedViaLocation.Description);
                                xmlWriter.WriteEndElement(); // </name>                      

                                xmlWriter.WriteStartElement("desc");
                                xmlWriter.WriteString("Via " + cycleJourney.RequestedViaLocation.Description);
                                xmlWriter.WriteEndElement(); // </desc>

                                xmlWriter.WriteStartElement("sym");
                                xmlWriter.WriteValue("Waypoint");
                                xmlWriter.WriteEndElement(); // </sym>

                                xmlWriter.WriteEndElement(); // </wpt>
                            }

                            // End Location
                            xmlWriter.WriteStartElement("wpt");
                            xmlWriter.WriteAttributeString("lat", endLat.ToString());
                            xmlWriter.WriteAttributeString("lon", endLon.ToString());

                            xmlWriter.WriteStartElement("name");
                            xmlWriter.WriteValue("End: " + cycleJourney.DestinationLocation.Description);
                            xmlWriter.WriteEndElement(); // </name>                      

                            xmlWriter.WriteStartElement("desc");
                            xmlWriter.WriteString("End of journey at " + cycleJourney.DestinationLocation.Description);
                            xmlWriter.WriteEndElement(); // </desc>

                            xmlWriter.WriteStartElement("sym");
                            xmlWriter.WriteValue("Waypoint");
                            xmlWriter.WriteEndElement(); // </sym>

                            xmlWriter.WriteEndElement(); // </wpt>

                            #endregion

                            // Get the directions (which are used for the Route points)
                            CycleJourneyDetailFormatter detailFormatter = new EmailCycleJourneyDetailFormatter(
                                cycleJourney,
                                sessionManager.JourneyViewState,
                                outward,
                                sessionManager.InputPageState.Units,
                                true,
                                false);

                            IList journeyDetails = detailFormatter.GetJourneyDetails();

                            // Get the actual cycle journey detail objects
                            CycleJourneyDetail[] cycleJourneyDetails = cycleJourney.Details;

                            // Get the coordinates for the entire journey
                            LatitudeLongitude[] gridReferences = cycleJourney.GetLatitudeLongitudes();

                            #region Route (including the journey directions)

                            // <rte>
                            xmlWriter.WriteStartElement("rte");

                            xmlWriter.WriteStartElement("name");
                            xmlWriter.WriteString(journeyName);
                            xmlWriter.WriteEndElement(); // </name>

                            string direction = string.Empty;
                            string symbol = "Dot";

                            // Loop through the directions (first and last are start and end locations)
                            for (int i = 0; i < journeyDetails.Count; i++)
                            {
                                // Get the direction instruction
                                direction = (string)((object[])journeyDetails[i])[2];

                                if (i == 0) // First direction
                                {
                                    #region Start
                                    // Set the lat/lon from First grid reference in journey
                                    lattitude = gridReferences[0].Latitude;
                                    longitude = gridReferences[0].Longitude;

                                    WriteRoutePoint(xmlWriter, lattitude.ToString(), longitude.ToString(), direction, symbol);
                                    #endregion
                                }
                                else if (i == journeyDetails.Count - 1) // Last direction
                                {
                                    #region End
                                    // Set the lat/lon from Last grid reference in journey
                                    lattitude = gridReferences[gridReferences.Length - 1].Latitude;
                                    longitude = gridReferences[gridReferences.Length - 1].Longitude;

                                    WriteRoutePoint(xmlWriter, lattitude.ToString(), longitude.ToString(), direction, symbol);
                                    #endregion
                                }
                                else
                                {
                                    #region Directions
                                    // Get the detail the instruction relates to
                                    CycleJourneyDetail cjd = cycleJourneyDetails[i - 1];
                                    bool writeDirection = true;

                                    // Write the points contained for this CycleJourneyDetail
                                    LatitudeLongitude[] coordinates = cjd.LatitudeLongitudes;

                                    if (coordinates != null && coordinates.Length > 0)
                                    {
                                        // loop through each coordinate there is for this detail, ignoring the last 
                                        // one in each array as this is used as the start for the next detail
                                        int items = (coordinates.Length == 1) ? 1 : (coordinates.Length - 1);

                                        for (int j = 0; j < items; j++)
                                        {
                                            lattitude = coordinates[j].Latitude;
                                            longitude = coordinates[j].Longitude;

                                            if (writeDirection)
                                            {
                                                // For the first osgr on this detail, add the Direction instruction
                                                WriteRoutePoint(xmlWriter, lattitude.ToString(), longitude.ToString(), direction, symbol);

                                                writeDirection = false;
                                            }
                                            else
                                            {
                                                WriteRoutePoint(xmlWriter, lattitude.ToString(), longitude.ToString(), string.Empty, string.Empty);
                                            }
                                        }
                                    }

                                    #endregion
                                }
                            }

                            xmlWriter.WriteEndElement(); //</rte>

                            #endregion

                            xmlWriter.WriteEndElement(); // </gpx>

                            xmlWriter.WriteEndDocument();

                            xmlWriter.Flush();

                            xmlWriter.Close();
                        }
                    }
                    else
                    {
                        message = string.Format("GPX file create failed  - CycleJourney [{0} to {1}]. The latitude and longitude valid flag was false. A problem must have occurred when processing the journey response from the Cycle Trip Planner.", 
                            cycleJourney.OriginLocation.Description, cycleJourney.DestinationLocation.Description);
                        TDException tdException = new TDException(message, true, TDExceptionIdentifier.CYXmlGPXFileTransformationFailed);
                        throw tdException;
                    }
                }
                else
                {
                    TDException tdException = new TDException("GPX file create failed  - CycleJourney was null", true, TDExceptionIdentifier.CYXmlGPXFileTransformationFailed);
                    throw tdException;
                }

                // No errors thrown during GPX conversion, so check file created exists before returning
                FileInfo fileInfo = new FileInfo(filePath + fileName);
                if (fileInfo.Exists)
                {
                    return fileName;
                }
                else
                {
                    TDException tdException = new TDException("GPX file create and save failed, filename: " + filePath + fileName, true, TDExceptionIdentifier.CYXmlGPXFileTransformationFailed);
                    throw tdException;
                }
            }
            catch (TDException tdEx)
            {
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Infrastructure, TDTraceLevel.Error, tdEx.Message);
                Logger.Write(operationalEvent);
            }
            catch (Exception ex)
            {
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Infrastructure, TDTraceLevel.Error, ex.Message, ex);
                Logger.Write(operationalEvent);
            }
            
            // Errors have occurred so return an empty file path
            return string.Empty;
        }

        /// <summary>
        /// Generates a gpx filename in the format "location_to_location_sessionID_yyyyMMddHHmmss.gpx";
        /// </summary>
        /// <param name="tdCyclePlannerRequest"></param>
        /// <returns></returns>
        private string GenerateFileName(ITDCyclePlannerRequest tdCyclePlannerRequest)
        {
            #region Get max file length property

            // Get the maximum file name length
            int maxLength = 0;
            int maxLengthUpperLimit = 225; // This is the largest value Windows currently will support 

            try
            {
                string strMaxLength = Properties.Current[FILENAMEMAXLENGTH];
                maxLength = Convert.ToInt32(strMaxLength);

                // If proverty value is 0, then assume user intentionally dosen't want to generate GPX file
                if ((maxLength > maxLengthUpperLimit) || (maxLength < 0))
                    throw new Exception();
            }
            catch
            {
                maxLength = maxLengthUpperLimit;
            }

            #endregion

            // Only if a valid max file length was specified, otherwise throw exception
            if (maxLength > 1)
            {
                string fileName = string.Empty;

                #region Set the parts needed for the filename
                               
                string fromLocation = string.Empty;
                string toLocation = string.Empty;

                if (outward)
                {
                    fromLocation = tdCyclePlannerRequest.OriginLocation.Description;
                    toLocation = tdCyclePlannerRequest.DestinationLocation.Description;
                }
                else
                {
                    fromLocation = tdCyclePlannerRequest.DestinationLocation.Description;
                    toLocation = tdCyclePlannerRequest.OriginLocation.Description;
                }

                // Remove any bad characters for a URL filename
                fromLocation = UpdateFileName(fromLocation);
                toLocation = UpdateFileName(toLocation);

                DateTime dt = DateTime.Now;
                string dateTimeStamp = " " + dt.ToString(FILENAMEDATETIMEFORMAT);
                string sessionID = " " + TDSessionManager.Current.Session.SessionID;
                string locationSeperator = " to ";

                #endregion

                #region Shorten location names if required
                // Recalculate the max length for the From and To location names.
                int lengthToSubtract = FILEEXTENSIONGPX.Length + dateTimeStamp.Length 
                    + sessionID.Length + locationSeperator.Length;
                
                // adjust the maxLength
                maxLength -= lengthToSubtract;

                // If maxLength is less than this value, then user specified to small a filename, so ignore their value!
                if (maxLength <= locationSeperator.Length)
                    maxLength = maxLengthUpperLimit - lengthToSubtract;

                // Check if we'll get a whole number when divided by 2
                if ( Decimal.Remainder(maxLength, 2) > 0 )
                {
                    maxLength -= 1;
                }

                // Set the max length of each location name
                int maxLengthFromLocation = maxLength / 2;
                int maxLengthToLocation = maxLength / 2;

                // Allow the location name to go over its max length if the other is shorter
                if (fromLocation.Length < maxLengthFromLocation)
                {
                    maxLengthToLocation += (maxLengthFromLocation - fromLocation.Length);
                }

                if (toLocation.Length < maxLengthToLocation)
                {
                    maxLengthFromLocation += (maxLengthToLocation - toLocation.Length);
                }

                // Shorten each location name to is allowed length
                if (fromLocation.Length > maxLengthFromLocation)
                {
                    fromLocation = fromLocation.Remove(maxLengthFromLocation);
                }

                if (toLocation.Length > maxLengthToLocation)
                {
                    toLocation = toLocation.Remove(maxLengthToLocation);
                }

                #endregion

                // Finally, put together the filename
                fileName = fromLocation + locationSeperator + toLocation
                    + sessionID
                    + dateTimeStamp
                    + FILEEXTENSIONGPX;

                fileName = UpdateFileName(fileName);

                return fileName;
            }
            else
            {
                TDException tdException = new TDException("GPX max file name length is set to 0 or less, unable to generate filename.", true, TDExceptionIdentifier.CYXmlGPXFileTransformationFailed);
                throw tdException;
            }
        }

        /// <summary>
        /// Replaces any characters in the filename string which could cause a browser URL address problems
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string UpdateFileName(string fileName)
        {
            fileName = fileName.Replace("&",  "and");
            fileName = fileName.Replace("\\", "-");
            fileName = fileName.Replace("/",  "-");
            fileName = fileName.Replace(' ',  '_');
            
            return fileName;
        }

        /// <summary>
        /// Populates and displays the error message on this page
        /// </summary>
        private void DisplayErrorMessage()
        {
            labelError.Text = GetResource("CyclePlanner.CycleJourneyGPXControl.labelError.Text");
            labelError.Visible = true;
        }

        /// <summary>
        /// Writes a rtept to the XmlWriter
        /// </summary>
        /// <param name="xmlWriter"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="name"></param>
        /// <param name="symbol"></param>
        private void WriteRoutePoint(XmlWriter xmlWriter, string lat, string lon, string name, string symbol)
        {
            xmlWriter.WriteStartElement("rtept");

            xmlWriter.WriteAttributeString("lat", lat);
            xmlWriter.WriteAttributeString("lon", lon);

            if (!string.IsNullOrEmpty(name))
            {
                xmlWriter.WriteStartElement("name");
                xmlWriter.WriteString(name);
                xmlWriter.WriteEndElement(); // </name>
            }

            if (!string.IsNullOrEmpty(symbol))
            {
                xmlWriter.WriteStartElement("sym");
                xmlWriter.WriteValue(symbol);
                xmlWriter.WriteEndElement(); // </sym>
            }

            xmlWriter.WriteEndElement(); // </rtept>
        }

        #endregion
    }
}
