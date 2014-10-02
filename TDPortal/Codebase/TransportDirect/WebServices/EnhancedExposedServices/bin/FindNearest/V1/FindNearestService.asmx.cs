// *********************************************** 
// NAME                 : FindNearestService.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: FindNearest web service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/FindNearest/V1/FindNearestService.asmx.cs-arc  $ 
//
//   Rev 1.4   Jan 12 2012 15:33:58   PScott
//Add fuel Genie find nearest methods to EES
//Resolution for 5781: Fuel Genie EES
//
//   Rev 1.3   Feb 24 2010 14:36:54   MTurner
//Changes to allow TI to call this service.
//Resolution for 5411: EES Security improvements led to TI injections failing
//
//   Rev 1.2   Feb 11 2010 14:52:00   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:16   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.0   Nov 08 2007 13:51:58   mturner
//Initial revision.
//
//   Rev 1.8   Mar 15 2006 16:23:08   RWilby
//Changed GetGridReference web method to throw TDException instead of SoapException when the postcode cannot be found.
//Resolution for 3634: FindNearestService.GetGridReference error response issue
//
//   Rev 1.7   Mar 13 2006 13:53:36   RWilby
//FindNearestLocality web method
//Resolution for 3624: EES FindNearestLocality web method
//
//   Rev 1.6   Feb 21 2006 16:16:32   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Feb 08 2006 15:18:16   RWilby
//Changed LSUniqueOSGridReferenceNotFoundForPostcode TDExceptionIdentifier to
//ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.4   Feb 03 2006 10:48:04   RWilby
//Updated after Code Review
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.3   Jan 19 2006 15:53:58   RWilby
//Corrected spelling mistake.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.2   Jan 19 2006 15:45:12   RWilby
//Completed implementation of class
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.1   Jan 04 2006 12:39:58   mtillett
//Create stub for the FindNearest web services
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Jan 04 2006 12:17:46   mtillett
//Initial revision.

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging; 
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using CommonV1 = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using System.Collections.Generic;


namespace TransportDirect.EnhancedExposedServices.FindNearest.V1
{
	/// <summary>
	///Web service for find nearest functionality
	/// </summary>
	[WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1")
	,ValidationSchemaCache("./schemas")]
	public class FindNearestService : TDEnhancedExposedWebService
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public FindNearestService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		/// <summary>
		/// Provides OS grid reference co-ordinates for full UK postcode
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language">EN(English) or CY(Welsh)</param>
		/// <param name="postcode">Full UK postcode</param>
		/// <returns>OSGridReference</returns>
		[WebMethod (EnableSession=false),Validation]
		public CommonV1.OSGridReference GetGridReference(string transactionId, string language, string postcode)
		{
			try
			{
                bool OKToProceed = false;
                if (String.Compare(transactionId, "TransactionInjector", true) != 0)
                {
                    int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                    if (tokenCount < 1)
                    {
                        OKToProceed = false;
                        Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                        throw e;
                    }
                    else
                    {
                        OKToProceed = true;
                    }
                }
                else
                {
                    //this is a TWS request from the local machine so should proceed
                    OKToProceed = true;
                }

                if (OKToProceed)
                {
                    CreateExposedServiceContext(transactionId, language);

                    // logging the start event
                    LogStartEvent(true);

                    //Perform operation to return grid reference for postcode
                    LocationChoiceList locationChoiceList = LocationSearch.FindPostcode(postcode, transactionId, false);

                    //If the locationChoiceList has 0 or more than 1 result throw a TDException
                    //We excepted 1 result otherwise the postcode is ambiguous 
                    if (locationChoiceList.Count != 1)
                    {
                        //throw TDException using TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode code
                        throw new TDException("Postcode not found", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                    }

                    CommonV1.OSGridReference osGridReference
                        = CommonAssembler.CreateOSGridReferenceDT((locationChoiceList[0] as LocationChoice).OSGridReference);

                    // log the finish event 		
                    LogFinishEvent(true);

                    return osGridReference;
                }
                else
                {
                    //should never go here but all code paths need to return a value
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
			}
			catch(SoapException soapException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				//log the error 				
				LogError(soapException.Message);  
				// throw the exception without wrapping it
				throw;
			}
			catch(TDException tdException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(tdException.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);
			
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
			}
		}

		/// <summary>
		/// Finds nearest Airports to OSGridReference
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language">EN(English) or CY(Welsh)</param>
		/// <param name="gridReference">OSGridReference</param>
		/// <param name="maxResults">Maximum number of results to return</param>
		/// <returns>NaptanProximity array</returns>
		[WebMethod (EnableSession=false),Validation]
		public NaptanProximity[] FindNearestAirports(string transactionId, string language, CommonV1.OSGridReference gridReference, int maxResults)
		{
			try
			{
                int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                if (tokenCount < 1)
                {
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
                else
                {
				    CreateExposedServiceContext(transactionId,language);

				    // logging the start event
				    LogStartEvent(true);

				    TDLocation tdLocation = CommonAssembler.CreateTDLocation(gridReference);
    				
				    LocationSearch.FindStations
					    (ref tdLocation ,new StationType[] {StationType.Airport},maxResults);

				    NaptanProximity[] naptanProximityArray =  FindNearestAssembler.CreateNaptanPromimityArrayDT(gridReference,tdLocation.NaPTANs);
    				
				    // log the finish event 		
				    LogFinishEvent(true);  

				    return naptanProximityArray;
                 }
			}
			catch(SoapException soapException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				//log the error 				
				LogError(soapException.Message);  
				// throw the exception without wrapping it
				throw;
			}
			catch(TDException tdException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(tdException.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);
			
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
			}
		}

		/// <summary>
		/// Finds nearest Rail Stations to OSGridReference
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language">EN(English) or CY(Welsh)</param>
		/// <param name="gridReference">OSGridReference</param>
		/// <param name="maxResults">Maximum number of results to return</param>
		/// <returns>NaptanProximity array</returns>
		[WebMethod (EnableSession=false),Validation]
		public NaptanProximity[] FindNearestStations(string transactionId, string language, CommonV1.OSGridReference gridReference, int maxResults)
		{
			try
			{
                bool OKToProceed = false;
                if (String.Compare(transactionId, "TransactionInjector", true) != 0)
                {
                    int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                    if (tokenCount < 1)
                    {
                        OKToProceed = false;
                        Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                        throw e;
                    }
                    else
                    {
                        OKToProceed = true;
                    }
                }
                else
                {
                    //this is a TWS request from the local machine so should proceed
                    OKToProceed = true;
                }

                if (OKToProceed)
                {
                    CreateExposedServiceContext(transactionId, language);

                    // logging the start event
                    LogStartEvent(true);

                    TDLocation tdLocation = CommonAssembler.CreateTDLocation(gridReference);

                    LocationSearch.FindStations(ref tdLocation, new StationType[] { StationType.Rail }, maxResults);

                    NaptanProximity[] naptanProximityArray = FindNearestAssembler.CreateNaptanPromimityArrayDT(gridReference, tdLocation.NaPTANs);

                    // log the finish event 		
                    LogFinishEvent(true);

                    return naptanProximityArray;
                }
                else
                {
                    //should never go here but all code paths need to return a value
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
			}
			catch(SoapException soapException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				//log the error 				
				LogError(soapException.Message);  
				// throw the exception without wrapping it
				throw;
			}
			catch(TDException tdException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(tdException.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);
			
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
			}
		}

		/// <summary>
		/// Finds nearest Bus Stops to OSGridReference
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language">EN(English) or CY(Welsh</param>
		/// <param name="gridReference">OSGridReference</param>
		/// <param name="maxResults">Maximum number of results to return</param>
		/// <returns>NaptanProximity array</returns>
		[WebMethod (EnableSession=false),Validation]
		public NaptanProximity[] FindNearestBusStops(string transactionId, string language, CommonV1.OSGridReference gridReference, int maxResults)
		{
			try
			{
                int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                if (tokenCount < 1)
                {
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
                else
                {
				    CreateExposedServiceContext(transactionId,language);

				    // logging the start event
				    LogStartEvent(true);

				    TDLocation tdLocation = CommonAssembler.CreateTDLocation(gridReference);
    				
				    LocationSearch.FindBusStops(ref tdLocation,maxResults);

				    NaptanProximity[] naptanProximityArray =  FindNearestAssembler.CreateNaptanPromimityArrayDT(gridReference,tdLocation.NaPTANs);

				    // log the finish event 		
				    LogFinishEvent(true);  

				    return naptanProximityArray;
                 }
            }
			catch(SoapException soapException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				//log the error 				
				LogError(soapException.Message);  
				// throw the exception without wrapping it
				throw;
			}
			catch(TDException tdException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(tdException.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);
			
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
			}

		}
		/// <summary>
		/// Finds nearest Locality to OSGridReference
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language">EN(English) or CY(Welsh</param>
		/// <param name="gridReference">OSGridReference</param>
		/// <returns>Locality</returns>
		[WebMethod (EnableSession=false),Validation]
		public string FindNearestLocality(string transactionId, string language, CommonV1.OSGridReference gridReference)
		{
			try
			{
                int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                if (tokenCount < 1)
                {
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
                else
                {
                    CreateExposedServiceContext(transactionId, language);

                    // logging the start event
                    LogStartEvent(true);

                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    double easting = Convert.ToDouble(gridReference.Easting);
                    double northing = Convert.ToDouble(gridReference.Northing);

                    string Locality = gisQuery.FindNearestLocality(easting, northing);

                    // log the finish event 		
                    LogFinishEvent(true);

                    return Locality;
                }
			}
			catch(SoapException soapException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				//log the error 				
				LogError(soapException.Message);  
				// throw the exception without wrapping it
				throw;
			}
			catch(TDException tdException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(tdException.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);
			
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
			}
		}

        /// <summary>
        /// Provides Nearest Fuel Sites functionality
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="language">EN(English) or CY(Welsh)</param>
        /// <param name="postcode">Full UK postcode</param>
        /// <param name="easting">Easting</param>
        /// <param name="northing">Northing</param>
        /// <param name="northing">SearchType</param>
        /// <param name="northing">SearchFlag</param>
        /// <param name="northing">SearchRadius</param>
        /// <returns>FuelRetailer</returns>
        [WebMethod(EnableSession = false), Validation]
        public CommonV1.FuelRetailer [] FindFuelGenieSites(string transactionId, string language, string postcode, int easting, int northing, String searchType, String searchFlag, float searchRadius)
        {
            try
            {
                bool OKToProceed = false;
                if (String.Compare(transactionId, "TransactionInjector", true) != 0)
                {
                    int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                    if (tokenCount < 1)
                    {
                        OKToProceed = false;
                        Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                        throw e;
                    }
                    else
                    {
                        OKToProceed = true;
                    }
                }
                else
                {
                    //this is a TWS request from the local machine so should proceed
                    OKToProceed = true;
                }
             
                if (OKToProceed)
                {
                    CreateExposedServiceContext(transactionId, language);

                    // logging the start event
                    LogStartEvent(true);


                    //is the correct searchType input
                    if (searchType!="F" && searchType!="N" && searchType!="A" )
                    {
                         //throw TDException using 
                           throw new TDException("Invalid Search Type - use F,N or A", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                    }
                    //is the correct searchFlag input
                    if (searchFlag != "I" && searchFlag != "E")
                    {
                        //throw TDException using 
                        throw new TDException("Invalid Search Flag - use I or E", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                    }

                    //is the correct searchRadius input
                    if (searchRadius < 0 || searchRadius > 999)
                    {
                        //throw TDException using 
                        throw new TDException("Invalid Search Radius use 0 to 999 miles", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                    }

                    //formulate sql query based on parameters supplied
                    if (postcode.Length >= 5)
                    {
                        //Post code supplied

                        //Perform operation to return grid reference for postcode
                        LocationChoiceList initialLlocationChoiceList = LocationSearch.FindPostcode(postcode, transactionId, false);

                        //If the locationChoiceList has 0 or more than 1 result throw a TDException
                        //We excepted 1 result otherwise the postcode is ambiguous 
                        if (initialLlocationChoiceList.Count != 1)
                        {
                            //throw TDException using TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode code
                            throw new TDException("Postcode not found", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                        }

                        CommonV1.OSGridReference osGridReference
                            = CommonAssembler.CreateOSGridReferenceDT((initialLlocationChoiceList[0] as LocationChoice).OSGridReference);
                        easting = osGridReference.Easting;
                        northing = osGridReference.Northing;

                    }
                    else
                    {
                    
                        // use eastings and northings instead
                        if (easting + northing == 0) // there arent any E/N supplied
                        {
                        
                            OKToProceed = false;
                            Exception e = new Exception("Please provide a valid postcode or easting/northing");
                            throw e;
                        
                        }
                    }



                    // Call the stored procedure to populate the record


                    //CommonV1.FuelRetailer[] fuelRetailerRecordArray = new FuelRetailer[20];
                    List<FuelRetailer> fuelRetailerRecordArray = new List<FuelRetailer>();
                    

                    //Execute sql and populate the record here

                    SqlHelper helper = new SqlHelper();

                    try
                    {
                        // Initialise a SqlHelper and connect to the database.
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.TransientPortalDB.ToString()));

                        helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                                TDTraceLevel.Verbose, "Populating FuelRetailer object"));
                        }

                        // Build the Hash tables for parameters and types
                        Hashtable parameter = new Hashtable(1);
                        parameter.Add("@searchEasting", easting);
                        parameter.Add("@searchNorthing", northing);
                        parameter.Add("@searchType", searchType);
                        parameter.Add("@searchFlag", searchFlag);
                        parameter.Add("@searchRadius", searchRadius);

                        Hashtable parameters = new Hashtable(1);
                        parameters.Add("@searchEasting", SqlDbType.Int);
                        parameters.Add("@searchNorthing", SqlDbType.Int);
                        parameters.Add("@searchType", SqlDbType.VarChar);
                        parameters.Add("@searchFlag", SqlDbType.VarChar);
                        parameters.Add("@searchRadius", SqlDbType.Float);

                        // Execute the GetNearestFuelRetailer stored procedure.
                        // This returns the nearest fuel site based on the input parameters
                        DataSet ds = helper.GetDataSet("GetFuelRetailerSites", parameter, parameters);
                        int noRecs = ds.Tables[0].Rows.Count;
                        //Populate operator
                        for (int rec = 0; rec < noRecs; rec++)
                        {
                            FuelRetailer fr = new FuelRetailer();
                            fr.PostCode = (ds.Tables[0].Rows[rec] as DataRow)[0].ToString(); ;
                            fr.FuelGenieSite  = (ds.Tables[0].Rows[rec] as DataRow)[1].ToString();
                            fr.Easting =   (int)(ds.Tables[0].Rows[rec] as DataRow)[2];
                            fr.Northing =  (int)(ds.Tables[0].Rows[rec] as DataRow)[3];
                            fr.Brand          = (ds.Tables[0].Rows[rec] as DataRow)[4].ToString();
                            fr.SiteReference1 = (ds.Tables[0].Rows[rec] as DataRow)[5].ToString();
                            fr.SiteReference2 = (ds.Tables[0].Rows[rec] as DataRow)[6].ToString();
                            fr.SiteReference2 = (ds.Tables[0].Rows[rec] as DataRow)[7].ToString();
                            fr.SiteReference2 = (ds.Tables[0].Rows[rec] as DataRow)[8].ToString();
                            // Find hypoteneuse distance from search co-ordinates
                            fr.Miles = (float)(Math.Sqrt((Math.Pow(Math.Abs((
                                fr.Easting - easting)), 2.0) +
                                Math.Pow(Math.Abs((fr.Northing - northing)), 2.0))
                                ) / 1609.344);


                            fuelRetailerRecordArray.Add(fr);

                        }

                        // Log the fact that the data was loaded.
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                                TDTraceLevel.Verbose, "Fuel Retailer object successfully populated"));
                        }

                    }
                    catch (SqlException sqle)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An SQL exception occurred in TransportDirect.UserPortal.CostSearchCoachRoutes.CoachOperator.Fetch method", sqle));
                    }
                    finally
                    {
                        //close the database connection
                        helper.ConnClose();
                    }



                    // log the finish event 		
                    LogFinishEvent(true);

                    return fuelRetailerRecordArray.ToArray();
                }
                else
                {
                    //should never go here but all code paths need to return a value
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
            }
            catch (SoapException soapException)
            {
                // log the finish event 		
                LogFinishEvent(false);
                //log the error 				
                LogError(soapException.Message);
                // throw the exception without wrapping it
                throw;
            }
            catch (TDException tdException)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(tdException.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);

            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
            }
        }
        /// <summary>
        /// Provides Nearest Fuel Station functionality
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="language">EN(English) or CY(Welsh)</param>
        /// <param name="postcode">Full UK postcode</param>
        /// <param name="easting">Easting</param>
        /// <param name="northing">Northing</param>
        /// <param name="northing">SearchType</param>
        /// <param name="northing">SearchFlag</param>
        /// <returns>FuelRetailer</returns>
        [WebMethod(EnableSession = false), Validation]
        public CommonV1.FuelRetailer FindNearestFuelGenie(string transactionId, string language, string postcode, int easting, int northing, String searchType, String searchFlag)
        {
            try
            {
                bool OKToProceed = false;
                if (String.Compare(transactionId, "TransactionInjector", true) != 0)
                {
                    int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                    if (tokenCount < 1)
                    {
                        OKToProceed = false;
                        Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                        throw e;
                    }
                    else
                    {
                        OKToProceed = true;
                    }
                }
                else
                {
                    //this is a TWS request from the local machine so should proceed
                    OKToProceed = true;
                }

                if (OKToProceed)
                {
                    CreateExposedServiceContext(transactionId, language);

                    // logging the start event
                    LogStartEvent(true);


                    //is the correct searchType input
                    if (searchType != "F" && searchType != "N" && searchType != "A")
                    {
                        //throw TDException using 
                        throw new TDException("Invalid Search Type - use F,N or A", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                    }
                    //is the correct searchFlag input
                    if (searchFlag != "I" && searchFlag != "E")
                    {
                        //throw TDException using 
                        throw new TDException("Invalid Search Flag - use I or E", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                    }
                    //formulate sql query based on parameters supplied
                    if (postcode.Length >= 5)
                    {
                        //Post code supplied
                        // is this one in the fuel retailer list? If so return it


                        //Perform operation to return grid reference for postcode
                        LocationChoiceList initialLlocationChoiceList = LocationSearch.FindPostcode(postcode, transactionId, false);

                        //If the locationChoiceList has 0 or more than 1 result throw a TDException
                        //We excepted 1 result otherwise the postcode is ambiguous 
                        if (initialLlocationChoiceList.Count != 1)
                        {
                            //throw TDException using TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode code
                            throw new TDException("Postcode not found", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                        }

                        CommonV1.OSGridReference osGridReference
                            = CommonAssembler.CreateOSGridReferenceDT((initialLlocationChoiceList[0] as LocationChoice).OSGridReference);
                        easting = osGridReference.Easting;
                        northing = osGridReference.Northing;

                    }
                    else
                    {

                        // use eastings and northings instead
                        if (easting + northing == 0) // there arent any E/N supplied
                        {

                            OKToProceed = false;
                            Exception e = new Exception("Please provide a valid postcode or easting/northing");
                            throw e;

                        }
                    }



                    // Call the stored procedure to populate the record


                    CommonV1.FuelRetailer fuelRetailerRecord = new FuelRetailer();


                    //Execute sql and populate the record here

                    SqlHelper helper = new SqlHelper();

                    try
                    {
                        // Initialise a SqlHelper and connect to the database.
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.TransientPortalDB.ToString()));

                        helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                                TDTraceLevel.Verbose, "Populating FuelRetailer object"));
                        }

                        // Build the Hash tables for parameters and types
                        Hashtable parameter = new Hashtable(1);
                        parameter.Add("@searchEasting", easting);
                        parameter.Add("@searchNorthing", northing);
                        parameter.Add("@searchType", searchType);
                        parameter.Add("@searchFlag", searchFlag);

                        Hashtable parameters = new Hashtable(1);
                        parameters.Add("@searchEasting", SqlDbType.Int);
                        parameters.Add("@searchNorthing", SqlDbType.Int);
                        parameters.Add("@searchType", SqlDbType.VarChar);
                        parameters.Add("@searchFlag", SqlDbType.VarChar);

                        // Execute the GetNearestFuelRetailer stored procedure.
                        // This returns the nearest fuel site based on the input parameters
                        DataSet ds = helper.GetDataSet("GetNearestFuelRetailer", parameter, parameters);

                        //Populate operator
                        fuelRetailerRecord.PostCode = (ds.Tables[0].Rows[0] as DataRow)[0].ToString();
                        fuelRetailerRecord.FuelGenieSite = (ds.Tables[0].Rows[0] as DataRow)[1].ToString(); ;
                        fuelRetailerRecord.Easting = (int)(ds.Tables[0].Rows[0] as DataRow)[2];
                        fuelRetailerRecord.Northing = (int)(ds.Tables[0].Rows[0] as DataRow)[3];
                        fuelRetailerRecord.Brand = (ds.Tables[0].Rows[0] as DataRow)[4].ToString();
                        fuelRetailerRecord.SiteReference1 = (ds.Tables[0].Rows[0] as DataRow)[5].ToString();
                        fuelRetailerRecord.SiteReference2 = (ds.Tables[0].Rows[0] as DataRow)[6].ToString();
                        fuelRetailerRecord.SiteReference2 = (ds.Tables[0].Rows[0] as DataRow)[7].ToString();
                        fuelRetailerRecord.SiteReference2 = (ds.Tables[0].Rows[0] as DataRow)[8].ToString();
                        
                        // Find hypoteneuse distance from search co-ordinates
                        fuelRetailerRecord.Miles = (float)(Math.Sqrt((Math.Pow(Math.Abs((fuelRetailerRecord.Easting - easting)), 2.0) + Math.Pow(Math.Abs((fuelRetailerRecord.Northing - northing)), 2.0))) / 1609.344);

                        // Log the fact that the data was loaded.
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                                TDTraceLevel.Verbose, "Fuel Retailer object successfully populated"));
                        }

                    }
                    catch (SqlException sqle)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An SQL exception occurred in TransportDirect.UserPortal.CostSearchCoachRoutes.CoachOperator.Fetch method", sqle));
                    }
                    finally
                    {
                        //close the database connection
                        helper.ConnClose();
                    }





                    // log the finish event 		
                    LogFinishEvent(true);

                    return fuelRetailerRecord;
                }
                else
                {
                    //should never go here but all code paths need to return a value
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
            }
            catch (SoapException soapException)
            {
                // log the finish event 		
                LogFinishEvent(false);
                //log the error 				
                LogError(soapException.Message);
                // throw the exception without wrapping it
                throw;
            }
            catch (TDException tdException)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(tdException.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, tdException);

            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.FindNearestServiceError, exception);
            }
        }
    }
}
