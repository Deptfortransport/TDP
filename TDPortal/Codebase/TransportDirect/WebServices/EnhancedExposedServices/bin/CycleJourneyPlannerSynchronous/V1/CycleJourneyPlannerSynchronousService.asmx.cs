// *********************************************** 
// NAME             : CycleJourneyPlannerSynchronousService.asmx.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: This class implements the synchronous Cycle Journey Planning web service
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CycleJourneyPlannerSynchronous/V1/CycleJourneyPlannerSynchronousService.asmx.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:50:36   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.Common;
using System.Web.Services.Protocols;
using TransportDirect.Common.PropertyService.Properties;

using CommonV1 = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyPlannerService;

namespace TransportDirect.EnhancedExposedServices.CycleJourneyPlannerSynchronous.V1
{
    /// <summary>
    /// This class implements the synchronous Cycle Journey Planning web service. 
    /// </summary>
    [WebService(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CycleJourneyPlannerSynchronous.V1"),ValidationSchemaCache("./schemas")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CycleJourneyPlannerSynchronousService : TDEnhancedExposedWebService
    {

        #region Web Methods
        /// <summary>
        /// The PlanCycleJourney web method invokes the planning of cycle journeys between the specified
        /// locations (OSGR coordinates), arriving at or leaving by the specified time. A
        /// return journey cannot be requested, a separate request from the external system would be required.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = false), Validation]
        public CycleJourneyResult PlanCycleJourney(string transactionId, CycleJourneyRequest request)
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
                    // Check for transactionId, throw error if null. This should have been trapped by the wsdl
                    // validation, but included here for certainty
                    if (string.IsNullOrEmpty(transactionId))
                    {
                        throw new TDException("TransactionId was null or empty", false, TDExceptionIdentifier.EESWSDLRequestValidationFailed);
                    }
                    // Check for request, throw error if null.
                    if (request == null)
                    {
                        throw new TDException("Request was null", false, TDExceptionIdentifier.EESWSDLRequestValidationFailed);
                    }

                    // trim transactionId if needed)
                    if (transactionId.Length > 100)
                    {
                        transactionId = transactionId.Substring(0, 100);
                    }

                    CreateExposedServiceContext(transactionId, GetLanguage(request));

                    LogStartEvent(true);

                    // invoke the journey planner service
                    IJourneyPlannerSynchronous journeyPlanner = (IJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                    CycleJourneyResult journeyResult = journeyPlanner.PlanCycleJourney(RequestContext, request);
                    
                    LogFinishEvent(true);

                    return journeyResult;
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
                if (!tdException.Logged)
                {
                    LogError(tdException.Message);
                }

                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, tdException);
            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);

                // log the error 
                LogError(exception.Message);

                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, exception);
            }
        }

        /// <summary>
        /// The GetCycleAlgorithms method returns the available cycle journey planner penalty functions
        /// and their associated name which is supplied into the request. 
        /// The penalty function affects the cycle journey generated, e.g. Quickest journey, Quietest 
        /// journey
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = false), Validation]
        public CycleAlgorithm[] GetCycleAlgorithms(string transactionId)
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
                    // Check for transactionId, throw error if null. This should have been trapped by the wsdl
                    // validation, but included here for certainty
                    if (string.IsNullOrEmpty(transactionId))
                    {
                        throw new TDException("TransactionId was null or empty", false, TDExceptionIdentifier.EESWSDLRequestValidationFailed);
                    }

                    CreateExposedServiceContext(transactionId, CycleJourneyPlannerAssembler.DEFAULT_LANGUAGE);
                    // logging the start event
                    LogStartEvent(true);

                    CycleAlgorithmAssembler cycleAlgorithmAssembler = new CycleAlgorithmAssembler();

                    CycleAlgorithm[] algorithms = cycleAlgorithmAssembler.GetCycleAlgorithms();

                    // log the finish event 		
                    LogFinishEvent(true);

                    return algorithms;
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
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, tdException);
            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, exception);
            }

        }

        /// <summary>
        /// The GetCycleAttributes web method returns the cycle attribute numbers with attribute
        /// name/description values which can be returned in the CycleJourneyResult
        /// e.g. id: 48, description: Shared Use Footpath.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = false), Validation]
        public CycleAttribute[] GetCycleAttributes(string transactionId)
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
                    // Check for transactionId, throw error if null. This should have been trapped by the wsdl
                    // validation, but included here for certainty
                    if (string.IsNullOrEmpty(transactionId))
                    {
                        throw new TDException("TransactionId was null or empty", false, TDExceptionIdentifier.EESWSDLRequestValidationFailed);
                    }

                    CreateExposedServiceContext(transactionId, CycleJourneyPlannerAssembler.DEFAULT_LANGUAGE);
                    // logging the start event
                    LogStartEvent(true);

                    CycleAttributeAssembler cycleAttributeHelper = new CycleAttributeAssembler();

                    CycleAttribute[] cycleAttributes = cycleAttributeHelper.GetCycleAttributes();

                    // log the finish event 		
                    LogFinishEvent(true);

                    return cycleAttributes;
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
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, tdException);
            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, exception);
            }
        }

        /// <summary>
        /// The GetCycleRequestPreferences web method returns the available cycle journey planner request
        /// preferences(user preferences) which can be specified into the CycleJourneyRequest.
        /// 
        /// The preferences provide additional weightings that will be used to influence the journey
        /// e.g. the user's average cycling speed: id: 5, description: Maximum speed, value : 19
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = false), Validation]
        public CycleRequestPreference[] GetCycleRequestPreferences(string transactionId)
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
                    // Check for transactionId, throw error if null. This should have been trapped by the wsdl
                    // validation, but included here for certainty
                    if (string.IsNullOrEmpty(transactionId))
                    {
                        throw new TDException("TransactionId was null or empty", false, TDExceptionIdentifier.EESWSDLRequestValidationFailed);
                    }

                    CreateExposedServiceContext(transactionId, CycleJourneyPlannerAssembler.DEFAULT_LANGUAGE);
                    // logging the start event
                    LogStartEvent(true);

                    CycleRequestPreferenceAssembler cycleRequestPreferenceAssembler = new CycleRequestPreferenceAssembler();

                    CycleRequestPreference[] requestPreferences = cycleRequestPreferenceAssembler.GetCycleRequestPreferences();

                    // log the finish event 		
                    LogFinishEvent(true);

                    return requestPreferences;
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
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, tdException);
            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError, exception);
            }
        }


        /// <summary>
        /// Provides OS grid reference coordinate for the Location specified
        /// </summary>
        /// <returns>OSGridReference</returns>
        [WebMethod(EnableSession = false), Validation]
        public CommonV1.OSGridReference GetGridReference(string transactionId, LocationType locationType, string locationValue)
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
                    CreateExposedServiceContext(transactionId, CycleJourneyPlannerAssembler.DEFAULT_LANGUAGE);

                    // logging the start event
                    LogStartEvent(true);

                    OSGridReference osgr = new OSGridReference();

                    // Get grid reference dependent on LocationType
                    switch (locationType)
                    {
                        case LocationType.Postcode:
                            osgr = GetGridReferenceForPostcode(transactionId, locationValue);
                            break;
                        case LocationType.NaPTAN:
                            osgr = GetGridReferenceForNaPTAN(transactionId, locationValue);
                            break;
                        default:
                            throw new TDException("LocationType is not supported", false, TDExceptionIdentifier.EESCarJourneyPlannerOSGridReferenceForLocationNotSupported);
                    }

                    // Convert into a response dto object
                    CommonV1.OSGridReference osGridReference = CommonV1.CommonAssembler.CreateOSGridReferenceDT(osgr);

                    // log the finish event 		
                    LogFinishEvent(true);

                    return osGridReference;
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



        #endregion

        #region Private methods

        /// <summary>
        /// Gets the language value from the request. Returns "en-GB" as a default.
        /// </summary>
        private string GetLanguage(CycleJourneyRequest request)
        {
            if (request != null)
            {
                // If no Result Settings are provided initialise Result Settings with default values
                if (request.ResultSettings == null)
                    request.ResultSettings = new ResultSettings();

                return request.ResultSettings.Language;
            }

            // If any of the above is null/missing, then return english by default. 
            // No need to fail transaction because the language is missing 
            return CycleJourneyPlannerAssembler.DEFAULT_LANGUAGE;
        }

        /// <summary>
        /// Returns the OSGR for a postcode.
        /// </summary>
        private OSGridReference GetGridReferenceForPostcode(string transactionId, string postcode)
        {
            //Perform operation to return postcode details
            LocationSearch search = new LocationSearch();
            LocationChoiceList locationChoiceList = search.StartSearch(postcode, SearchType.AddressPostCode,
                false, 1000, transactionId, false);

            //We excepted 1 result otherwise the postcode is ambiguous 
            if (locationChoiceList.Count != 1)
            {
                throw new TDException("Postcode not found", false, TDExceptionIdentifier.ESSCarJourneyPlannerUniqueOSGridReferenceNotFoundForPostcode);
            }

            // Get a populated location, so we can then call the PopulatePoint() to 
            // use the find nearest point on toid logic
            TDLocation location = new TDLocation();
            search.GetLocationDetails(ref location, (locationChoiceList[0] as LocationChoice));

            location.PopulatePoint();

            return location.PointAsOSGR();
        }

        /// <summary>
        /// Returns the OSGR for a naptan.
        /// </summary>
        private OSGridReference GetGridReferenceForNaPTAN(string transactionId, string naptan)
        {
            try
            {
                // If its a valid naptan, it will exist in the naptan cache
                NaptanCacheEntry nce = NaptanLookup.Get(naptan.Trim().ToUpper(), "Naptan");

                if (nce.Found)
                {
                    return nce.OSGR;
                }
                else
                {
                    //If Naptan is not found, throw an exception
                    throw (new TDException("Naptan code not found", false, TDExceptionIdentifier.ESSCarJourneyPlannerUniqueOSGridReferenceNotFoundForNaPTAN));
                }
            }
            catch
            {
                // Any errors, its an invalid naptan, throw exception
                throw (new TDException("Naptan code not found", false, TDExceptionIdentifier.ESSCarJourneyPlannerUniqueOSGridReferenceNotFoundForNaPTAN));
            }
        }


        #endregion
    }
}
