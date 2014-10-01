// *********************************************** 
// NAME             : GisRequestCaller.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 30 Nov 2010
// DESCRIPTION  	: Class which provides core GIS request functionality 
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/GisRequestCaller.cs-arc  $
//
//   Rev 1.1   Dec 05 2012 14:12:26   mmodi
//Fixed compiler warnings
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 30 2010 13:33:46   apatel
//Initial revision.
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using JourneyPlannerCaller.GisQueryObjects;
using TransportDirect.Presentation.InteractiveMapping;
using System.Xml.Serialization;
using System.Configuration;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// GisRequestCaller class
    /// </summary>
    class GisRequestCaller
    {
        private const string LOG_DATETIME_FORMAT = "dd-MM-yyyy HH:mm:ss";
        private Query gisQueryService;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GisRequestCaller()
        {
            // get ServiceName and ServerName properties from PropertyService
            string serviceName =ConfigurationManager.AppSettings["locationservice.servicename"];
            string serverName = ConfigurationManager.AppSettings["locationservice.servername"];

            if (serviceName == string.Empty || serverName == string.Empty)
                throw new Exception("Unable to access the GisQuery Properties");
            gisQueryService = new Query(serverName, serviceName);	
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Calls the GIS functions as defined int the query dictionary object.
        /// </summary>
        /// <param name="gisQueryDictionary">Dictionary containing the GIS requests</param>
        /// <returns></returns>
        public int PerformGisQueries(Dictionary<string, IGisQuery> gisQueryDictionary)
        {
            int gisStatus = StatusCodes.JPC_SUCCESS;

            foreach (string requestId in gisQueryDictionary.Keys)
            {
                FileLogger.LogMessage(string.Format("Calling GIS function : {0} with requestId : {1}", gisQueryDictionary[requestId].QueryFunction, requestId));
                int status = CallGis(gisQueryDictionary[requestId]);

                if (status != StatusCodes.JPC_SUCCESS)
                {
                    gisStatus = status;
                }

                FileLogger.LogMessage(string.Format("GIS function : {0} request exited with status: {1}", gisQueryDictionary[requestId].QueryFunction, status)); 
            }

            return gisStatus;

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Calls the GIS function for the query request provided
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int CallGis(IGisQuery gisQuery)
        {
            int gisStatus = StatusCodes.JPC_SUCCESS;

            #region Gis request
            try
            {
                switch (gisQuery.QueryFunction)
                {
                    case GisQueryFunction.FindExchangePointsInRadius:
                        gisStatus = FindExchangePointsInRadius(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestCarParks:
                        gisStatus = FindNearestCarParks(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestITN:
                        gisStatus = FindNearestITN(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestITNs:
                        gisStatus = FindNearestITNs(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestLocality:
                        gisStatus = FindNearestLocality(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestPointOnTOID:
                        gisStatus = FindNearestPointOnTOID(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestStops:
                        gisStatus = FindNearestStops(gisQuery);
                        break;

                    case GisQueryFunction.FindNearestStopsAndITNs:
                        gisStatus = FindNearestStopsAndITNs(gisQuery);
                        break;

                    case GisQueryFunction.FindStopsInfoForStops:
                        gisStatus = FindStopsInfoForStops(gisQuery);
                        break;

                    case GisQueryFunction.FindStopsInGroupForStops:
                        gisStatus = FindStopsInGroupForStops(gisQuery);
                        break;

                    case GisQueryFunction.FindStopsInRadius:
                        gisStatus = FindStopsInRadius(gisQuery);
                        break;

                    case GisQueryFunction.GetStreetsFromPostCode:
                        gisStatus = GetStreetsFromPostCode(gisQuery);
                        break;

                    case GisQueryFunction.IsPointsInCycleDataArea:
                        gisStatus = IsPointsInCycleDataArea(gisQuery);
                        break;

                    case GisQueryFunction.IsPointsInWalkDataArea:
                        gisStatus = IsPointsInWalkDataArea(gisQuery);
                        break;

                    default:
                        gisStatus = StatusCodes.JPC_REQUESTFAILURE;
                        break;

                }

                

                
            }
            catch (Exception ex)
            {
                gisStatus = StatusCodes.JPC_EXCEPTION;

                FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function" + gisQuery.QueryFunction,ex.StackTrace));
            }

            #endregion
            
            return gisStatus;
            
        }

        /// <summary>
        /// Wrapper for FindExchangePointsInRadius GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindExchangePointsInRadius(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is ExchangePointQuery)
            {
                try
                {
                    ExchangePointQuery query = (ExchangePointQuery)gisQuery;

                    ExchangePointSchema resultObj = gisQueryService.FindExchangePointsInRadius(query.X, query.Y, query.Radius, query.Mode, query.Maximum);
                    GisResult result = new GisResult();

                    result.ExchangePointSchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for IsPointsInWalkDataArea GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int IsPointsInWalkDataArea(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is GisInfoQuery)
            {
                try
                {
                    GisInfoQuery query = (GisInfoQuery)gisQuery;

                    string[] resultObjs = new string[3];
                    string city = string.Empty;
                    int walkitId = 0;

                    resultObjs[0] = gisQueryService.IsPointsInWalkDataArea(query.Points, query.SameAreaOnly, out walkitId, out city).ToString();

                    resultObjs[1] = "walkitid : " + walkitId;

                    resultObjs[2] = "city : " + city;

                    GisResult result = new GisResult();

                    result.OutputResults = resultObjs;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for IsPointsInCycleDataArea GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int IsPointsInCycleDataArea(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is GisInfoQuery)
            {
                try
                {
                    GisInfoQuery query = (GisInfoQuery)gisQuery;

                    string resultObj = gisQueryService.IsPointsInCycleDataArea(query.Points, query.SameAreaOnly).ToString();
                    GisResult result = new GisResult();

                    result.OutputResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for GetStreetsFromPostCode GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int GetStreetsFromPostCode(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is GisInfoQuery)
            {
                try
                {
                    GisInfoQuery query = (GisInfoQuery)gisQuery;

                    string[] resultObj = gisQueryService.GetStreetsFromPostcode(query.Postcode);
                    GisResult result = new GisResult();

                    result.OutputResults = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindStopsInRadius GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindStopsInRadius(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = null;

                    if (query.StopTypes != null && query.StopTypes.Length > 0)
                    {
                        resultObj = gisQueryService.FindStopsInRadius(query.X, query.Y, query.SearchDistance, query.StopTypes);

                    }
                    else 
                    {
                        resultObj = gisQueryService.FindStopsInRadius(query.X, query.Y, query.SearchDistance);
                    }
                   

                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindStopsInGroupForStops GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindStopsInGroupForStops(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = null;

                    if(query.NaptanIds != null && query.NaptanIds.Length > 0)
                    {
                        resultObj = gisQueryService.FindStopsInGroupForStops(query.NaptanIds);
                    }
                    else if (query.Query != null)
                    {
                        resultObj = gisQueryService.FindStopsInGroupForStops(query.Query);
                    }
                    else
                    {
                        returnCode = StatusCodes.JPC_REQUESTFAILURE;
                    }

                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindStopsInfoForStops GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindStopsInfoForStops(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = gisQueryService.FindStopsInfoForStops(query.NaptanIds);
                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestStopsAndITNs GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestStopsAndITNs(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = gisQueryService.FindNearestStopsAndITNs(query.X, query.Y, query.MaxDistance);
                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestStops GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestStops(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = gisQueryService.FindNearestStops(query.X, query.Y, query.MaxDistance);
                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestPointOnTOID GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestPointOnTOID(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    Point resultObj = gisQueryService.FindNearestPointOnTOID(query.X, query.Y, query.ToId);
                    GisResult result = new GisResult();

                    result.ResultAsPoint = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestLocality GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestLocality(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    string resultObj = gisQueryService.FindNearestLocality(query.X, query.Y);
                    GisResult result = new GisResult();

                    result.OutputResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestITNs GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestITNs(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = gisQueryService.FindNearestITNs(query.X, query.Y);
                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestITN GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestITN(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestQuery)
            {
                try
                {
                    FindNearestQuery query = (FindNearestQuery)gisQuery;

                    QuerySchema resultObj = gisQueryService.FindNearestITN(query.X, query.Y, query.Address, query.IgnoreMotorways);
                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Wrapper for FindNearestCarParks GIS function
        /// </summary>
        /// <param name="gisQuery">GIS query request</param>
        /// <returns></returns>
        private int FindNearestCarParks(IGisQuery gisQuery)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            if (gisQuery is FindNearestCarParksQuery)
            {
                try
                {
                    FindNearestCarParksQuery carParkQuery = (FindNearestCarParksQuery)gisQuery;

                    QuerySchema resultObj = gisQueryService.FindNearestCarParks(carParkQuery.Easting, carParkQuery.Northing,
                        carParkQuery.InitialRadius, carParkQuery.MaxRadius, carParkQuery.MaxNoCarParks);

                    GisResult result = new GisResult();

                    result.QuerySchemaResult = resultObj;

                    WriteGisResult(result, gisQuery.GisResultPath);
                }
                catch (Exception ex)
                {
                    returnCode = StatusCodes.JPC_EXCEPTION;

                    FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GIS Response for function " + gisQuery.QueryFunction, ex.StackTrace));
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_REQUESTFAILURE;
            }

            return returnCode;
        }

        /// <summary>
        /// Writes a GISResult object to the supplied file path, appending to the end of the file
        /// </summary>
        public void WriteGisResult(GisResult gisr, string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                StreamWriter writer = new StreamWriter(filepath, true);

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(GisResult));

                    if (gisr != null)
                    {
                        serializer.Serialize(writer, gisr);

                        // New line for next entry if there is one
                        writer.WriteLine(string.Empty);
                    }
                    else
                    {
                        throw new Exception("GISResult object was null, unable to create result XML file");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(
                        string.Format("Error occurred writing GISResult to an Xml file[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                        filepath,
                        ex.Message,
                        ex.StackTrace));
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
        }

        #endregion
    }
}
