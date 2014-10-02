// *********************************************** 
// NAME                 : CarJourneyPlannerSynchronousService.asmx.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: This class implements the synchronous Car Journey Planning web service. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CarJourneyPlannerSynchronous/V1/CarJourneyPlannerSynchronousService.asmx.cs-arc  $
//
//   Rev 1.3   Feb 11 2010 14:52:00   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.2   Feb 10 2010 16:28:20   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Sep 08 2009 13:25:14   mmodi
//Updated for a max number of journeys in request value
//Resolution for 5318: Car exposed service - Multiple journey limit property
//
//   Rev 1.0   Aug 04 2009 15:02:20   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Xml;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.UserPortal.JourneyPlannerService;
using TransportDirect.UserPortal.LocationService;

using CommonV1 = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.EnhancedExposedServices.CarJourneyPlannerSynchronous.V1
{
    /// <summary>
    /// This class implements the synchronous Car Journey Planning web service. 
    /// </summary>
    [WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourneyPlannerSynchronous.V1")
    ,ValidationSchemaCache("./schemas")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class CarJourneyPlannerSynchronousService : TDEnhancedExposedWebService
    {
        #region Web Methods

        /// <summary>
        /// This method performs planning of a car journey. Parameters for journey
        /// planning are passed in the CarJourneyRequest parameter. The transactionId parameter is used for
        /// logging purposes and should be unique (although this is not mandatory). When journey planning 
        /// is complete, the journey plans are returned in the CarJourneyResult object. The method 
        /// will throw a SoapException if the request is invalid, if errors occur during journey 
        /// planning or if no journeys were found.
        /// </summary>
        /// <param name="transactionId">An external reference</param>
        /// <param name="request">Car journey request parameters</param>
        /// <returns>Car journey result</returns>
        [WebMethod(EnableSession = false), Validation]
        public CarJourneyResult PlanCarJourney(string transactionId, CarJourneyRequest request)
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

                    // Determine the limit on the number of journeys in a request
                    int maxNumberOfJourneys = GetMaxNumberOfJourneysAllowed();

                    // invoke the journey planner service
                    IJourneyPlannerSynchronous journeyPlanner = (IJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                    CarJourneyResult journeyResult = journeyPlanner.PlanPrivateJourney(RequestContext, request, maxNumberOfJourneys);

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
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CarJourneyPlannerServiceError, tdException);
            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);

                // log the error 
                LogError(exception.Message);
                
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CarJourneyPlannerServiceError, exception);
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
                    CreateExposedServiceContext(transactionId, CarJourneyPlannerAssembler.DEFAULT_LANGUAGE);

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
        private string GetLanguage(CarJourneyRequest request)
        {
            if (request != null)
            {
                // Validate the result settings before extracting the language. This creates a default settings if null or invalid
                request.ResultSettings = CarJourneyPlannerAssembler.ValidateResultSettingsDT(request.ResultSettings);

                return request.ResultSettings.Language;
            }

            // If any of the above is null/missing, then return english by default. 
            // No need to fail transaction because the language is missing 
            return CarJourneyPlannerAssembler.DEFAULT_LANGUAGE;
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

        /// <summary>
        /// Returns the max number of journeys allowed in the request.
        /// A value of -1 indicates any number of journeys are allowed
        /// </summary>
        /// <returns></returns>
        private int GetMaxNumberOfJourneysAllowed()
        {
            int maxNumberOfJourneys = 1;
            bool useMaxNumberOfJourneys = true;

            try
            {
                // Determine if the max number of journeys value should be read
                useMaxNumberOfJourneys = Boolean.Parse(Properties.Current["CarExposedService.JourneysInRequest.UseMaxAllowed"]);
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning,
                    "Failure reading property [CarExposedService.JourneysInRequest.UseMaxAllowed], defaulting value to True"));
            }

            
            if (useMaxNumberOfJourneys)
            {
                try
                {
                    maxNumberOfJourneys = int.Parse(Properties.Current["CarExposedService.JourneysInRequest.MaxAllowed"]);
                }
                catch
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning,
                        "Failure reading property [CarExposedService.JourneysInRequest.MaxAllowed], defaulting value to 1"));
                }
            }
            else
            {
                // -1 Indicates to ignore max number of journeys value
                maxNumberOfJourneys = -1;
            }

            return maxNumberOfJourneys;
        }

        #endregion
    }
}
