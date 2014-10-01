// *********************************************** 
// NAME             : XmlGazetteerHandler.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Handler for Xml that is returned from ESRI Gazetteer
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;
using Logger = System.Diagnostics.Trace;
using System.Xml;
using System.IO;
using System.Globalization;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Handler for Xml that is returned from ESRI Gazetteer
    /// </summary>
    [Serializable()]
    public class XmlGazetteerHandler
    {
        #region Constants

        private const string operationResultName = "OPERATION_RESULT";
        private const string cacheIdName = "CACHE_ID";
        private const string addressMatchName = "ADDRESS_MATCH";
        private const string gazopsNorthingName = "GAZOPS_NORTHING";
        private const string gazopsEastingName = "GAZOPS_EASTING";
        private const string nodeNName = "N";
        private const string nodeVName = "V";
        private const string addLineName = "ADDLINE";
        private const string pickListEntryName = "PICK_LIST_ENTRY";
        private const string pickListName = "PICK_LIST";
        private const string criteriaFieldName = "CRITERIA_FIELD";
        private const string identityName = "IDENTITY";
        private const string descriptionName = "DESCRIPTION";
        private const string matchRecordsName = "MATCH_RECORDS";
        private const string scoreName = "SCORE";
        private const string finalName = "FINAL";
        private const string noMatchFound = "No Match Found";
        private const string success = "success";
        private const string matches = "matches";
        private const string unsupportedOperation = "Unsupported Operation";
        private const string gazopsScoreName = "GAZOPS_SCORE";
        private const string nationalGazetteerIdName = "NATIONAL_GAZETTEER_ID";
        private const string exchangePointIdName = "EXCHANGE_POINT_ID";
        private const string exchangePointTypeName = "EXCHANGE_POINT_TYPE";
        private const string naptanName = "NAPTAN";
        private const string eastingName = "EASTING";
        private const string northingName = "NORTHING";
        private const string postcodeName = "POSTCODE";
        private const string adminArea = "ADMINAREA";
        private const string atcoCode = "ATCO_CODE";
        private const string fields = "FIELDS";
        private const string maxX = "GAZOPS_MAXX";
        private const string minX = "GAZOPS_MINX";
        private const string maxY = "GAZOPS_MAXY";
        private const string minY = "GAZOPS_MINY";
        private const string gazopsUniqueId = "GAZOPS_UNIQUE_ID";
        private const string gazopsVagueName = "GAZOPS_VAGUE";

        // Extra constants for code Gazetteer
        private const string codeDescriptionName = "CODEDESCRIPTION";
        private const string naptanCodeName = "NAPTANCODE";
        private const string codeTypeName = "CODETYPE";
        private const string modeTypeName = "MODETYPE";
        private const string localityName = "LOCALITYNAME";
        private const string regionName = "REGIONNAME";
        private const string matchValueName = "MATCHVALUE";

        // Picklist entry Code Type values
        private const string IATAValue = "IATA";
        private const string CRSValue = "CRS";
        private const string SMSValue = "SMS";
        private const string postcodeValue = "Postcode";

        private const string railValue = "Rail";
        private const string busValue = "Bus";
        private const string coachValue = "Coach";
        private const string airValue = "Air";
        private const string metroValue = "Metro";
        private const string ferryValue = "Ferry";

        #endregion

        #region Private variables
        
        private bool userLoggedOn;
        private string sessionID;
        private int minScore;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public XmlGazetteerHandler()
        {
            this.userLoggedOn = false;
            this.sessionID = string.Empty;
            this.minScore = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userLoggedOn"></param>
        /// <param name="sessionID"></param>
        /// <param name="minScore"></param>
        public XmlGazetteerHandler(bool userLoggedOn, string sessionID, int minScore)
        {
            this.userLoggedOn = userLoggedOn;
            this.sessionID = sessionID;
            this.minScore = minScore;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Read Xml Result from DrillDownQuery Webmethod - Final call
        /// </summary>
        /// <param name="xml">Xml string containing the result</param>
        /// <param name="pickListUsed">pickList entry used representing the user choices</param>
        /// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        public LocationQueryResult ReadFinalDrillDownResult(string xml, string pickListUsed)
        {
            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    "Final DrillDownQuery Result = " + xml);
                Logger.Write(log);
            }

            LocationQueryResult queryResult = new LocationQueryResult(pickListUsed);

            using (StringReader sr = new StringReader(xml))
            {
                XmlTextReader reader = new XmlTextReader(sr);
                
                string operationResult = string.Empty;
                string cacheId = string.Empty;

                while (reader.Read())
                {
                    // if it's the beginning of a node
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case operationResultName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        operationResult = reader.Value;
                                    else if (operationResult != success)
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                                            "Operation result not valid");
                                        Logger.Write(oe);

                                        throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSFinalDrillDownInvalid);
                                    }
                                    break;
                                }
                            case cacheIdName:
                                reader.Read();
                                if (IsText(reader))
                                    queryResult.QueryReference = reader.Value;
                                break;
                            case addressMatchName:
                                {
                                    LocationChoice lc = ReadAddressMatch(reader);
                                    if (!lc.IsAdminArea)
                                        queryResult.LocationChoiceList.Add(lc);
                                    break;
                                }
                            case pickListEntryName:
                                {
                                    OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                                        "Final result must not have a PickList entry");
                                    Logger.Write(oe);

                                    throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSPickListExists);
                                }
                        }
                    }
                }
            }
            return queryResult;
        }
        
        /// <summary>
        /// Read Xml Result from DrillDownQuery Webmethod
        /// </summary>
        /// <param name="xml">Xml string containing the result</param>
        /// <param name="pickListUsed">pickList entry used representing the user choices</param>
        /// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        public LocationQueryResult ReadDrillDownResult(string xml, string pickListUsed)
        {
            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    "DrillDownQuery Result = " + xml);
                Logger.Write(log);
            }

            LocationQueryResult queryResult = new LocationQueryResult(pickListUsed);

            using (StringReader sr = new StringReader(xml))
            {
                XmlTextReader reader = new XmlTextReader(sr);

                string operationResult = string.Empty;
                string cacheId = string.Empty;
                bool noMatchWasFound = false;

                while (reader.Read())
                {
                    // if it's the beginning of a node
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case operationResultName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        operationResult = reader.Value;
                                    if (operationResult == noMatchFound)
                                        noMatchWasFound = true;
                                    else if (operationResult != success)
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                                            "Operation result not valid");
                                        Logger.Write(oe);

                                        throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSDrillDownInvalid);
                                    }
                                    break;
                                }
                            case gazopsVagueName:
                                {
                                    if (noMatchWasFound)
                                    {
                                        queryResult.IsVague = true;
                                        return queryResult;
                                    }
                                    break;
                                }
                            case cacheIdName:
                                reader.Read();
                                if (IsText(reader))
                                    queryResult.QueryReference = reader.Value;
                                break;
                            case addressMatchName:
                                {
                                    LocationChoice lc = ReadAddressMatch(reader);
                                    if (!lc.IsAdminArea)
                                        queryResult.LocationChoiceList.Add(lc);
                                    break;
                                }
                            case pickListEntryName:
                                {
                                    LocationChoice lc = ReadPickListEntry(reader, false);
                                    if (!lc.IsAdminArea)
                                        queryResult.LocationChoiceList.Add(lc);
                                    break;
                                }
                        }
                    }
                }

                if (!noMatchWasFound)
                {
                    LocationChoiceList list = queryResult.LocationChoiceList;
                    StandardizeLocationChoiceList(ref list);
                }
            }

            return queryResult;
        }
        
        /// <summary>
        /// Read Xml Result from PostcodeMatch Webmethod
        /// </summary>
        /// <param name="xml">Xml string containing the result</param>
        /// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        public LocationQueryResult ReadPostcodeMatchResult(string xml)
        {
            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    "PostcodeMatch Result = " + xml);
                Logger.Write(log);
            }

            LocationQueryResult queryResult = new LocationQueryResult(string.Empty);

            using (StringReader sr = new StringReader(xml))
            {
                XmlTextReader reader = new XmlTextReader(sr);

                string operationResult = string.Empty;
                int easting = -1;
                int northing = -1;
                string postcode = string.Empty;
                double partPostcodeMaxX = 0;
                double partPostcodeMaxY = 0;
                double partPostcodeMinX = 0;
                double partPostcodeMinY = 0;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case operationResultName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        operationResult = reader.Value;
                                    if (operationResult == noMatchFound)
                                        return queryResult;
                                    else if (operationResult != success)
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                                            "Operation result not valid");
                                        Logger.Write(oe);

                                        throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSPostCodeMatchInvalid);
                                    }
                                    break;
                                }
                            case postcodeName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        postcode = reader.Value;
                                    break;
                                }
                            case gazopsEastingName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        easting = Convert.ToInt32(TrimCoord(reader.Value), CultureInfo.CurrentCulture.NumberFormat);
                                    break;

                                }
                            case gazopsNorthingName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        northing = Convert.ToInt32(TrimCoord(reader.Value), CultureInfo.CurrentCulture.NumberFormat);
                                    break;

                                }
                            // For Partial Postcode area max and min coords:
                            case fields:
                                {
                                    if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
                                    {
                                        string strFields = reader.ReadOuterXml();
                                        partPostcodeMaxX = ReadCoordValue(maxX, strFields);
                                        partPostcodeMaxY = ReadCoordValue(maxY, strFields);
                                        partPostcodeMinX = ReadCoordValue(minX, strFields);
                                        partPostcodeMinY = ReadCoordValue(minY, strFields);
                                    }
                                    break;
                                }
                        }
                    }

                }
                OSGridReference gridReference = new OSGridReference(easting, northing);
                LocationChoice choice = new LocationChoice(
                    postcode,
                    false,
                    string.Empty,
                    string.Empty,
                    gridReference,
                    string.Empty,
                    0,
                    string.Empty,
                    string.Empty,
                    false
                    //partPostcodeMaxX, // Commented out until required
                    //partPostcodeMaxY,
                    //partPostcodeMinX,
                    //partPostcodeMinY
                    );

                queryResult.LocationChoiceList.Add(choice);
            }

            return queryResult;
        }

        /// <summary>
        /// Method that builds a CodeDetail[] from the xml string 
        /// returned by ESRI
        /// </summary>
        /// <param name="xml">string containing xml</param>
        /// <param name="modeTypes">Mode of transport array to select</param>
        /// <returns></returns>
        public CodeDetail[] ReadCodeGazResult(string xml, TDPModeType[] modeTypes)
        {
            bool noMatchWasFound = false;
            List<CodeDetail> listDetail = new List<CodeDetail>();

            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    "CodeGaz Result = " + xml);
                Logger.Write(log);
            }

            using (StringReader sr = new StringReader(xml))
            {
                XmlTextReader reader = new XmlTextReader(sr);
                
                string operationResult = string.Empty;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case operationResultName:
                                {
                                    reader.Read();
                                    if (IsText(reader))
                                        operationResult = reader.Value;
                                    if (operationResult == noMatchFound)
                                    {
                                        noMatchWasFound = true;
                                    }
                                    else if (operationResult != matches)
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                                            "Operation result not valid");
                                        Logger.Write(oe);

                                        throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSPlaceNameMatchInvalid);
                                    }
                                    break;
                                }

                            case pickListEntryName:
                                {
                                    CodeDetail detail = ReadCodePickListEntry(reader);
                                    List<TDPModeType> listModes = new List<TDPModeType>(modeTypes);

                                    // only add the detail to list if modeType is in requested list!
                                    if (listModes.Contains(detail.ModeType))
                                        listDetail.Add(detail);
                                    break;
                                }

                            case gazopsVagueName:
                                {
                                    if (noMatchWasFound)
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Verbose,
                                            "Place Name too vague");
                                        Logger.Write(oe);

                                        throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSPlaceNameMatchTooVague);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }

            if (noMatchWasFound)
            {
                return new CodeDetail[0];
            }

            return listDetail.ToArray();
        }

        #region Commented out methods until required

        ///// <summary>
        ///// Read Xml Result from PlacenameMatch Webmethod
        ///// </summary>
        ///// <param name="xml">Xml string containing the result</param>
        ///// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        //public LocationQueryResult ReadPlacenameMatchResult(string xml)
        //{
        //    if (TDPTraceSwitch.TraceVerbose)
        //    {
        //        OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
        //            "PlacenameMatch Result = " + xml);
        //        Logger.Write(log);
        //    }

        //    StringReader sr = new StringReader(xml);
        //    XmlTextReader reader = new XmlTextReader(sr);

        //    LocationQueryResult queryResult = new LocationQueryResult(string.Empty);

        //    string operationResult = string.Empty;

        //    while (reader.Read())
        //    {
        //        if (reader.NodeType == XmlNodeType.Element)
        //        {
        //            switch (reader.Name)
        //            {
        //                case operationResultName:
        //                    {
        //                        reader.Read();
        //                        if (IsText(reader))
        //                            operationResult = reader.Value;
        //                        if (operationResult == noMatchFound)
        //                            return queryResult;
        //                        else if (operationResult != matches)
        //                        {
        //                            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
        //                                "Operation result not valid");

        //                            Logger.Write(oe);
        //                            throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSPlaceNameMatchInvalid);
        //                        }
        //                        break;
        //                    }

        //                case pickListEntryName:
        //                    {
        //                        queryResult.LocationChoiceList.Add(ReadPickListEntry(reader, true));
        //                        break;
        //                    }
        //            }
        //        }

        //    }
        //    LocationChoiceList list = queryResult.LocationChoiceList;
        //    StandardizeLocationChoiceList(ref list);
        //    return queryResult;
        //}
        
        ///// <summary>
        ///// Read Xml Result from FetchRecord Webmethod
        ///// </summary>
        ///// <param name="xml">Xml string containing the result</param>
        ///// <returns>Returns an array of TDNaptan objects</returns>
        //public TDNaptan[] ReadFetchRecord(string xml)
        //{
        //    if (TDPTraceSwitch.TraceVerbose)
        //    {
        //        OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
        //            "FetchRecord Result = " + xml);
        //        Logger.Write(log);
        //    }

        //    StringReader sr = new StringReader(xml);
        //    XmlTextReader reader = new XmlTextReader(sr);

        //    ArrayList naptans = new ArrayList();
        //    string operationResult = string.Empty;

        //    bool isValid = false;

        //    while (reader.Read())
        //    {
        //        if (reader.NodeType == XmlNodeType.Element)
        //        {
        //            switch (reader.Name)
        //            {
        //                case operationResultName:
        //                    {
        //                        reader.Read();

        //                        if (IsText(reader))
        //                        {
        //                            operationResult = reader.Value;
        //                        }
        //                        else if (operationResult != unsupportedOperation)
        //                        {
        //                            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
        //                                "Operation result not valid");

        //                            Logger.Write(oe);
        //                            throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSFetchRecordInvalid);
        //                        }
        //                        break;
        //                    }

        //                case matchRecordsName:
        //                    {
        //                        isValid = true;
        //                        break;

        //                    }

        //                case naptanName:
        //                    {
        //                        reader.Read();

        //                        if (IsText(reader))
        //                        {
        //                            operationResult = reader.Value;
        //                        }

        //                        TDNaptan naptan = new TDNaptan();
        //                        naptan.Naptan = reader.Value;
        //                        naptans.Add(naptan);
        //                        break;
        //                    }

        //                case northingName:
        //                    {
        //                        reader.Read();

        //                        if (IsText(reader))
        //                        {
        //                            operationResult = reader.Value;
        //                        }

        //                        TDNaptan naptan = (TDNaptan)naptans[naptans.Count - 1];

        //                        if (naptan.GridReference == null)
        //                        {
        //                            naptan.GridReference = new OSGridReference();
        //                        }

        //                        naptan.GridReference.Northing = Int32.Parse(reader.Value);
        //                        break;
        //                    }

        //                case eastingName:
        //                    {
        //                        reader.Read();

        //                        if (IsText(reader))
        //                        {
        //                            operationResult = reader.Value;
        //                        }

        //                        TDNaptan naptan = (TDNaptan)naptans[naptans.Count - 1];

        //                        if (naptan.GridReference == null)
        //                        {
        //                            naptan.GridReference = new OSGridReference();
        //                        }

        //                        naptan.GridReference.Easting = Int32.Parse(reader.Value);
        //                        break;
        //                    }

        //            }
        //        }
        //    }

        //    if (!isValid)
        //    {
        //        OperationalEvent oe = new OperationalEvent(
        //            TDPEventCategory.Business,
        //            sessionID,
        //            TDPTraceLevel.Error,
        //            "Bad format Xml : Missing <MATCH_RECORDS> tag");

        //        Logger.Write(oe);
        //        throw new TDPException(
        //            "Bad format Xml : Missing <MATCH_RECORDS> tag",
        //            false,
        //            TDPExceptionIdentifier.LSFetchRecordNoMatch);

        //    }
        //    return (TDNaptan[])naptans.ToArray(typeof(TDNaptan));

        //}
        
        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Method that reads infor from a Picklist entry Xml node
        /// and creates either a LocationChoice or a TDCodeDetail object,
        /// depending on ginven GazetteerType
        /// </summary>
        /// <param name="type">Defines which type of object to get</param>
        /// <param name="reader">XmlTextReader object containing the next picklist entry to read</param>
        /// <param name="final">used to build a LocationChoice. Indicates if entry is final</param>
        /// <param name="target">reference of the object to build. </param>
        /// <returns>true if object built successfully, false otherwise</returns>
        private bool BuildGazObjectFromPickListEntry(Type type, XmlTextReader reader, bool final, ref object target)
        {
            string identity = string.Empty;
            string criteria = string.Empty;
            string description = string.Empty;
            string exchangePointType = string.Empty;
            string exchangePointId = string.Empty;
            string nationalGazetteerId = string.Empty;
            int easting = -1;
            int northing = -1;
            double score = 0;

            // special fields for code gaz
            string code = string.Empty;
            string codeType = string.Empty;
            string locality = string.Empty;
            string region = string.Empty;
            string modeType = string.Empty;
            string naptanCode = string.Empty;


            while (reader.Read() && !IsEndElement(reader, pickListEntryName))
            {
                if (!IsEndElement(reader, reader.Name))
                {

                    switch (reader.Name)
                    {
                        case criteriaFieldName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    criteria = reader.Value;
                                break;
                            }
                        case finalName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    final = Convert.ToBoolean(reader.Value);
                                break;
                            }
                        case identityName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                {
                                    identity = reader.Value;
                                }
                                break;
                            }
                        case scoreName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    score = Convert.ToDouble(reader.Value, CultureInfo.CurrentCulture.NumberFormat);
                                break;

                            }
                        case descriptionName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    description = reader.Value;
                                break;

                            }
                        case gazopsEastingName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    easting = Convert.ToInt32(reader.Value, CultureInfo.CurrentCulture.NumberFormat);
                                break;

                            }
                        case gazopsNorthingName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    northing = Convert.ToInt32(reader.Value, CultureInfo.CurrentCulture.NumberFormat);
                                break;

                            }
                        case nationalGazetteerIdName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    nationalGazetteerId = reader.Value;
                                break;

                            }
                        case exchangePointIdName:
                        case naptanName:
                        case atcoCode:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    exchangePointId = reader.Value;
                                break;

                            }
                        case exchangePointTypeName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    exchangePointType = reader.Value;
                                break;

                            }

                        // added for code gaz
                        case codeTypeName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    codeType = reader.Value;
                                break;

                            }
                        case naptanCodeName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    naptanCode = reader.Value;
                                break;

                            }
                        case modeTypeName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    modeType = reader.Value;
                                break;

                            }
                        case localityName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    locality = reader.Value;
                                break;

                            }
                        case regionName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    region = reader.Value;
                                break;

                            }
                        case matchValueName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                    code = reader.Value;
                                break;

                            }


                    }

                }
            }

            if (type == typeof(LocationChoice))
            {
                // Create new LocationChoice
                LocationChoice choice = new LocationChoice(
                    description,
                    !final,
                    criteria,
                    identity,
                    new OSGridReference(easting, northing),
                    exchangePointId,
                    score,
                    nationalGazetteerId,
                    exchangePointType,
                    (criteria == adminArea));

                target = choice;
                return true;
            }
            else if (type == typeof(CodeDetail))
            {
                CodeDetail detail = new CodeDetail();
                detail.Description = description;
                detail.Code = code;
                detail.CodeType = GetTDPCodeType(codeType);
                detail.Description = description;
                detail.Easting = easting;
                detail.Northing = northing;
                detail.Locality = locality;
                detail.Region = region;
                detail.ModeType = GetTDModeType(modeType);
                detail.NaptanId = naptanCode;

                target = detail;
                return true;
            }
            
            return false;

        }

        /// <summary>
        /// Method used to build a Location Choice need in TDGazetteer classes.
        /// </summary>
        /// <param name="reader">XmlTextReader reader containing the xml Picklist entryto analyse</param>
        /// <param name="final">specifies if the pickList entry is final</param>
        /// <returns></returns>
        private LocationChoice ReadPickListEntry(XmlTextReader reader, bool final)
        {
            object choice = null;

            BuildGazObjectFromPickListEntry(typeof(LocationChoice), reader, final, ref choice);

            if (TDPTraceSwitch.TraceVerbose)
            {
                try
                {
                    LocationChoice lc = (LocationChoice)choice;

                    OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        string.Format("ReadPickListEntry Location Desc[{0}] Value[{1}] Naptan[{2}] Locality[{3}] HasChilden[{4}] IsAdminArea[{5}]", lc.Description, lc.PicklistValue, lc.Naptan, lc.Locality, lc.HasChilden, lc.IsAdminArea));
                    Logger.Write(log);
                }
                catch
                {
                    // Ignore exception, just verbose logging 
                    OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        "ReadPickListEntry Location");
                    Logger.Write(log);
                }
            }

            return (LocationChoice)choice;

        }

        /// <summary>
        /// Method used to build a TDCodeDetail object, needed by TDCodeGazetteer class.
        /// </summary>
        /// <param name="reader">Reader containing the xml Picklist entry to analyse</param>
        /// <returns>new TDCodeDetail object</returns>
        private CodeDetail ReadCodePickListEntry(XmlTextReader reader)
        {
            object detail = null;

            BuildGazObjectFromPickListEntry(typeof(CodeDetail), reader, false, ref detail);

            if (TDPTraceSwitch.TraceVerbose)
            {
                try
                {
                    CodeDetail d = (CodeDetail)detail;

                    OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        string.Format("ReadPickListEntry Code Desc[{0}] Code[{1}] Naptan[{2}] Locality[{3}]", d.Description, d.Code, d.NaptanId, d.Locality));
                    Logger.Write(log);
                }
                catch
                {
                    // Ignore exception, just verbose logging 
                    OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        "ReadPickListEntry Code");
                    Logger.Write(log);
                }
            }

            return (CodeDetail)detail;
        }
        
        /// <summary>
        /// Method that builds a new LocationChoice object from an XmlTextReader containing xml AddressMatch.
        /// </summary>
        /// <param name="reader">Reader containing the xml address match information</param>
        /// <returns>new LocationChoice object</returns>
        private LocationChoice ReadAddressMatch(XmlTextReader reader)
        {
            string identity = string.Empty;
            string criteria = string.Empty;
            string exchangePointType = string.Empty;
            string exchangePointId = string.Empty;
            string exchangePointUniqueId = string.Empty;
            string nationalGazetteerId = string.Empty;
            double score = 0;

            string address = string.Empty;
            int northingValue = -1;
            int eastingValue = -1;

            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    "ReadAddressMatch");
                Logger.Write(log);
            }

            while (reader.Read() && !IsEndElement(reader, addressMatchName))
            {
                if (!IsEndElement(reader, reader.Name))
                {

                    switch (reader.Name)
                    {
                        case addLineName:
                            {
                                reader.Read();
                                if (IsText(reader))
                                {
                                    address += reader.Value + ",";
                                    reader.Read();
                                }
                                break;
                            }

                    }
                    switch (reader.Value)
                    {
                        case gazopsEastingName:
                            {
                                eastingValue = Convert.ToInt32(ReadValue(reader),
                                    CultureInfo.CurrentCulture.NumberFormat);
                                break;
                            }
                        case gazopsNorthingName:
                            {
                                northingValue = Convert.ToInt32(ReadValue(reader),
                                    CultureInfo.CurrentCulture.NumberFormat);
                                break;
                            }
                        case nationalGazetteerIdName:
                            {
                                nationalGazetteerId = ReadValue(reader);
                                break;
                            }
                        // Added for ESRI 5.2 change in Mainstation Gazetteer
                        case exchangePointIdName:
                        case naptanName:
                        case atcoCode:
                            {
                                exchangePointId = ReadValue(reader);
                                break;
                            }

                        case gazopsUniqueId:
                            {
                                exchangePointUniqueId = ReadValue(reader);
                                break;
                            }

                        case exchangePointTypeName:
                            {
                                exchangePointType = ReadValue(reader);
                                break;
                            }

                    }
                }
            }

            if (address[address.Length - 1] == ',')
            {
                address = address.Substring(0, address.Length - 1); // remove the coma at the end of the string
            }

            OSGridReference gridReference = new OSGridReference(eastingValue, northingValue);

            // FetchRecord returns a UniqueId that we want to use in preference to exchangepointId
            if (exchangePointUniqueId != null && exchangePointUniqueId.Length > 0)
            {
                exchangePointId = exchangePointUniqueId;
            }

            // Create new LocationChoice
            LocationChoice choice = new LocationChoice(
                address,
                false,
                criteria,
                identity,
                gridReference,
                exchangePointId,
                score,
                nationalGazetteerId,
                exchangePointType,
                (criteria == adminArea));
            return choice;
        }

        /// <summary>
        /// Trims any decimal places from returned coordinates to 
        /// ensure compatability with OSGridReference.
        /// </summary>
        private string TrimCoord(string coord)
        {
            while (coord.IndexOf(".") > 0)
            {
                coord = coord.Remove(coord.Length - 1, 1);
            }
            return coord;
        }

        /// <summary>
        /// Extracts max and min x and y values from xml string to 
        /// determine the best fit scale to use when displaying the map.
        /// </summary>
        private double ReadCoordValue(string strCoord, string xml)
        {
            bool setValue = false;
            double coord = 0;

            using (StringReader sr = new StringReader(xml))
            {
                XmlTextReader reader = new XmlTextReader(sr);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == nodeNName)
                    {
                        // Next element (contains the actual value)
                        reader.Read();
                        string data = reader.Value;
                        if (data == strCoord)
                        {
                            setValue = true;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.Element && reader.Name == nodeVName)
                    {
                        if (setValue)
                        {
                            // Next element (contains the actual value)
                            reader.Read();
                            setValue = false;
                            coord = Convert.ToDouble(reader.Value);
                            break;
                        }
                    }
                }
            }

            return coord;
        }

        /// <summary>
        /// Method that sorts in ascending order a given choice list.
        /// </summary>
        /// <param name="choiceList">given choice list as reference</param>
        private void StandardizeLocationChoiceList(ref LocationChoiceList choiceList)
        {
            // Don't remove any entries if there is only one ...

            if (choiceList.Count < 2)
            {
                return;
            }

            for (int i = 0; i < choiceList.Count; i++)
            {
                LocationChoice choice = (LocationChoice)choiceList[i];
                if (choice.Score < minScore)
                {
                    choiceList.RemoveRange(i, choiceList.Count - i);
                    break;
                }
            }

        }

        /// <summary>
        /// Method that converts a string into the associated enumeration TDPCodeType
        /// </summary>
        /// <param name="type">string value to identify</param>
        /// <returns>TDCodeType value of given string.</returns>
        private TDPCodeType GetTDPCodeType(string type)
        {
            switch (type)
            {
                case CRSValue:
                    return TDPCodeType.CRS;
                case SMSValue:
                    return TDPCodeType.SMS;
                case IATAValue:
                    return TDPCodeType.IATA;
                case postcodeValue:
                    return TDPCodeType.Postcode;
                default:
                    {
                        Logger.Write(new OperationalEvent(
                            TDPEventCategory.ThirdParty,
                            TDPTraceLevel.Error,
                            "Found Undefined CodeType in ESRI gazetteer")
                            );


                        throw new TDPException(
                            "Found Undefined CodeType in ESRI gazetteer",
                            true,
                            TDPExceptionIdentifier.LSCodeGazetteerUndefinedCodeType);

                    }
            }
        }
        
        /// <summary>
        /// Method that converts a string into the associated enumeration TDModeType
        /// </summary>
        /// <param name="type">string value to identify</param>
        /// <returns>TDModeType value of given string.</returns>
        private TDPModeType GetTDModeType(string type)
        {
            switch (type)
            {
                case railValue:
                    return TDPModeType.Rail;
                case busValue:
                    return TDPModeType.Bus;
                case coachValue:
                    return TDPModeType.Coach;
                case airValue:
                    return TDPModeType.Air;
                case metroValue:
                    return TDPModeType.Metro;
                case ferryValue:
                    return TDPModeType.Ferry;
                default:
                    return TDPModeType.Unknown;
            }
        }


        #endregion

        #region Xml Helper

        private bool IsEndElement(XmlTextReader reader, string name)
        {
            if (reader.Name == name
                && (reader.NodeType == XmlNodeType.EndElement
                    || reader.IsEmptyElement)) // detects if the current element is empty
                // equals to an end element!
                return true;
            else
                return false;
        }

        private bool IsText(XmlTextReader reader)
        {
            if (reader.NodeType == XmlNodeType.Text)
                return true;
            else
                return false;
        }

        private string ReadValue(XmlTextReader reader)
        {
            // find the next <V> node
            while (
                !(reader.Name == nodeVName && reader.NodeType == XmlNodeType.Element)
                )
            {
                reader.Read();
            }

            reader.Read();
            if (IsText(reader))
                return reader.Value;
            else
                return string.Empty;
        }

        #endregion
    }
}
