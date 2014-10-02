// *********************************************** 
// NAME                 : CoordinateConvertor.asmx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: CoordinateConvertor web service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/CoordinateConvertorService/CoordinateConvertor.asmx.cs-arc  $
//
//   Rev 1.1   Oct 01 2009 10:54:24   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.0   Jun 03 2009 11:34:12   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

using GIQ60Lib;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.WebService.CoordinateConvertorService
{
    /// <summary>
    /// Coordinate Convertor web service which provides a number of methods to convert
    /// a coordinate
    /// </summary>
    [WebService(Namespace = "http://www.transportdirect.info/CoordinateConvertorService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CoordinateConvertor : System.Web.Services.WebService
    {
        #region Private members

        // Static objects
        private static OSTransformation osTransform = null;
        private static bool osTransformInitialised = false;  // Flag to indicate if Transformation object needs to be initialised
        private static bool osTransformInitialisedOK = true; // Flag used to prevent multiple attempts at initialising the Transformation object

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CoordinateConvertor()
		{
            // Only call the initialise if it the object hasn't already been created
            if ((!osTransformInitialised) && (osTransformInitialisedOK))
            {
                osTransformInitialised = InitialiseOSTransformation();
            }
		}
	    
        #endregion

        #region Private methods

        /// <summary>
        /// Initialises the Transformation object
        /// </summary>
        /// <returns></returns>
        private bool InitialiseOSTransformation()
        {
            // Flag to track initialisation
            bool initialisedOK = true;
            OperationalEvent operationalEvent;

            try
            {
                operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
                    Messages.Service_InitialisingTransformObject);
                Logger.Write(operationalEvent);

                // Used to capture any errors from the OSTransformationClass
                eErrorCode errorCode = eErrorCode.eFailure;

                string coordinateConvertorDataPath = Properties.Current["Coordinate.Convertor.DataPath"]; //@"C:\TDPortal\Codebase\build\Quest\";
                string initialiseString = Properties.Current["Coordinate.Convertor.InitialiseString"]; //"GIQ.6.0"; 

                // Set up the convertor    
                osTransform = new OSTransformationClass();

                // Set the data path for the convertor
                errorCode = osTransform.SetDataFilesPath(coordinateConvertorDataPath);

                if (errorCode == eErrorCode.eSuccess)
                {
                    // Initialise the convertor
                    errorCode = osTransform.SetArea(eArea.eAreaGreatBritain);
                    errorCode = osTransform.Initialise(initialiseString);

                }

                // Set the local initialise flag
                initialisedOK = (errorCode == eErrorCode.eSuccess);

                if (initialisedOK)
                {
                    operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, 
                        Messages.Service_InitialisedTransformObject);
                    Logger.Write(operationalEvent);
                }
                else
                {
                    throw new Exception(string.Format(Messages.Service_ErrorInitialisingTransformObjectCode, errorCode.ToString()));
                }

            }
            catch (Exception ex)
            {
                initialisedOK = false;

                operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    Messages.Service_ErrorInitialisingTransformObject, ex);
                Logger.Write(operationalEvent);
            }

            // Set the static variable indicating the Transformation object was initialised ok
            osTransformInitialisedOK = initialisedOK;
            
            // Return 
            return initialisedOK;
        }

        private OSGridReference ConvertToOSGR(LatitudeLongitude latlong)
        {
            double easting = 0;
            double northing = 0;

            if (osTransformInitialised)
            {
                if (osTransform != null)
                {
                    // Temp values
                    eVertDatum verticalHeightDataType; // Only used because Transform object asks for it
                    double height; // Only used because Transform object asks for it

                    // Insert the OSGR in to the object
                    eErrorCode errorCode = osTransform.SetETRS89Geodetic(latlong.Latitude, latlong.Longitude, 0);

                    if (errorCode == eErrorCode.eSuccess)
                    {
                        // Convert the OSGR to Lattitude/longitude
                        osTransform.GetOSGB36(out easting, out northing, out height, out verticalHeightDataType);

                    }
                    else
                    {
                        string message = string.Format(Messages.Service_ErrorPopulatingLatitudeLongitude,
                        latlong.Latitude,
                        latlong.Longitude,
                        errorCode.ToString());

                        throw new Exception(message);
                    }
                }
                else
                {
                    throw new Exception(Messages.Service_TransformObjectNull);
                }
            }
            else
            {
                throw new Exception(Messages.Service_TransformObjectNotInitialised);
            }

            // Will only reach here if all the above has worked
            OSGridReference osgr = new OSGridReference();
            osgr.Easting = (int)easting;
            osgr.Northing = (int)northing;

            return osgr;
        }

        /// <summary>
        /// Performs the conversion of an OSGR to an LatitudeLongitude
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private LatitudeLongitude ConvertToLatitudeLongitude(OSGridReference osgr)
        {
            double latitude = 0;
            double longitude = 0;

            if (osTransformInitialised)
            {
                if (osTransform != null)
                {
                    // Temp values
                    eVertDatum verticalHeightDataType; // Only used because Transform object asks for it
                    double height; // Only used because Transform object asks for it

                    // Insert the OSGR in to the object
                    eErrorCode errorCode = osTransform.SetOSGB36(osgr.Easting, osgr.Northing, 0, out verticalHeightDataType);

                    if (errorCode == eErrorCode.eSuccess)
                    {
                        // Convert the OSGR to Lattitude/longitude
                        osTransform.GetETRS89Geodetic(out latitude, out longitude, out height);
                    }
                    else
                    {
                        string message = string.Format(Messages.Service_ErrorPopulatingOSGR,
                            osgr.Easting.ToString(),
                            osgr.Northing.ToString(),
                            errorCode.ToString());

                        throw new Exception(message);
                    }
                }
                else
                {
                    throw new Exception(Messages.Service_TransformObjectNull);
                }
            }
            else
            {
                throw new Exception(Messages.Service_TransformObjectNotInitialised);
            }

            // Will only reach here if all the above has worked
            LatitudeLongitude latlong = new LatitudeLongitude();

            latlong.Latitude = latitude;
            latlong.Longitude = longitude;

            return latlong;
        }

        #endregion

        #region Web methods

        /// <summary>
        /// Performs the conversion from a LatitudeLongitude to an OSGR
        /// </summary>
        /// <param name="latlongs"></param>
        /// <returns></returns>
        [WebMethod(EnableSession=false)]
        public OSGridReference[] ConvertLatitudeLongitudetoOSGR(LatitudeLongitude[] latlongs)
        {
            ArrayList arrayosgr = new ArrayList();

            OperationalEvent operationalEvent;

            try
            {
                if ((latlongs != null) && (latlongs.Length > 0))
                {
                    foreach (LatitudeLongitude latlong in latlongs)
                    {
                        operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format(Messages.Service_ConvertLatitudeLongitudetoOSGR, latlong.Latitude, latlong.Longitude));
                        Logger.Write(operationalEvent);

                        OSGridReference osgr = ConvertToOSGR(latlong);
                        arrayosgr.Add(osgr);

                        operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format(Messages.Service_LatitudeLongitudeConverterdToOSGR, latlong.Latitude, latlong.Longitude, osgr.Easting, osgr.Northing));
                        Logger.Write(operationalEvent);
                    }
                }
                else
                {
                    #region Invalid Latitude Longitude array, throw error

                    string message;

                    if (latlongs == null)
                    {
                        message = string.Format(Messages.Service_LatitudeLongitudeInvalid, Messages.Service_LatitudeLongitudeIsNull);
                    }
                    else
                    {
                        message = string.Format(Messages.Service_LatitudeLongitudeInvalid, string.Format(Messages.Service_LatitudeLongitudeArrayLength, latlongs.Length));
                    }
                    throw new Exception(message);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                // Log exception
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ex.Message));

                // Throw the exception to the caller
                SoapException se = new SoapException(Messages.Service_InternalError, SoapException.ServerFaultCode, ex);

                throw se;
            }
            // Return array to caller
            return (OSGridReference[])arrayosgr.ToArray(typeof(OSGridReference));
        }

        /// <summary>
        /// Returns the Latitude/Longitude of the supplied OSGR
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        [WebMethod(EnableSession=false)]
        public LatitudeLongitude[] ConvertOSGRtoLatitudeLongitude(OSGridReference[] osgrs)
        {
            // The return object
            ArrayList arrayLatLong = new ArrayList();
            
            OperationalEvent operationalEvent;

            try
            {
                if ((osgrs != null) && (osgrs.Length > 0))
                {
                    foreach (OSGridReference osgr in osgrs)
                    {

                        operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format(Messages.Service_ConvertOSGRtoLatitudeLongitude, osgr.Easting, osgr.Northing));
                        Logger.Write(operationalEvent);

                        // Get the LatitudeLongitude
                        LatitudeLongitude latlong = ConvertToLatitudeLongitude(osgr);
                        arrayLatLong.Add(latlong);

                        operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format(Messages.Service_OSGRConverterdToLatitudeLongitude, osgr.Easting, osgr.Northing, latlong.Latitude, latlong.Longitude));
                        Logger.Write(operationalEvent);
                    }
                }
                else
                {
                    #region Invalid OSGR array, throw error
                    string message;

                    if (osgrs == null)
                    {
                        message = string.Format(Messages.Service_OSGRInvalid, Messages.Service_OSGRIsNull);
                    }
                    else 
                    {
                        message = string.Format(Messages.Service_OSGRInvalid, string.Format(Messages.Service_OSGRArrayLength, osgrs.Length));
                    }

                    throw new Exception(message);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                // Log exception
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ex.Message));

                // Throw the exception to the caller
                SoapException se = new SoapException(Messages.Service_InternalError, SoapException.ServerFaultCode, ex);

                throw se;
            }

            // Return array to caller
            return (LatitudeLongitude[])arrayLatLong.ToArray(typeof(LatitudeLongitude));
        }

        #region Test methods

        /// <summary>
        /// Used to test if the web service is running
        /// </summary>
        /// <returns>True if the web service is running</returns>
        [WebMethod]
        public bool TestActive()
        {
            return true;
        }

        /// <summary>
        /// Used to test an OSGRR can be converted to a LatitudeLongitude by passing in a test value 
        /// to the ConvertOSGRtoLatitudeLongitude web method
        /// </summary>
        /// <returns>True if coordinate converted without error</returns>
        [WebMethod]
        public LatitudeLongitude TestConvertOSGRToLatitudeLongitude(int easting, int northing)
        {
            try
            {
                string message = string.Format("TEST - Calling ConvertOSGRToLatitudeLongitude for OSGR [{0},{1}]", easting, northing);
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, message));

                OSGridReference[] osgrs = new OSGridReference[1];
                osgrs[0] = new OSGridReference();
                osgrs[0].Easting = easting;
                osgrs[0].Northing = northing;

                LatitudeLongitude[] latlongs = ConvertOSGRtoLatitudeLongitude(osgrs);
                                
                return latlongs[0];
            }
            catch (Exception ex)
            {
                // Log exception
                string message = "TEST - Call to ConvertOSGRToLatitudeLongitude web method threw an exception: " + ex.Message;
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, message));

                // Throw the exception to the caller
                SoapException se = new SoapException(Messages.Service_InternalError, SoapException.ServerFaultCode, ex);

                throw se;
            }
        }

        #endregion

        #endregion
    }
}
