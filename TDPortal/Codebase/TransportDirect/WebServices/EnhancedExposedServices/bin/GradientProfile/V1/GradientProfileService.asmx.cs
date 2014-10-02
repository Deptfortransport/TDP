// *********************************************** 
// NAME             : GradientProfileService.asmx.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: GradientProfileService will take an array of coordinates 
//                  : (specified as OSGRs) as an input and will respond with a
//                  : series of height points for those coordinates. 
//                  : This web service will utilise the gradient
//                  : profile sub-system used to deliver the gradient profile graph 
//                  : on TransportDirect.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/GradientProfile/V1/GradientProfileService.asmx.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:52:52   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyPlannerService;
using TransportDirect.Common.ServiceDiscovery;
using System.Web.Services.Protocols;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using TransportDirect.EnhancedExposedServices.Helpers;

namespace TransportDirect.EnhancedExposedServices.GradientProfile.V1
{
    /// <summary>
    /// GradientProfileService will take an array of coordinates (specified as OSGRs) as an input and will
    /// respond with a series of height points for those coordinates. This web service will utilise the gradient
    /// profile sub-system used to deliver the gradient profile graph on TransportDirect.
    /// </summary>
    [WebService(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.GradientProfile.V1"), ValidationSchemaCache("./schemas")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class GradientProfileService : TDEnhancedExposedWebService
    {
        private const string DEFAULT_LANGUAGE = "en-GB";

        #region Web Methods
        /// <summary>
        /// The GetGradientProfile webservice will service an array of polyline groups.The polylines being specified as OSGRs
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = false), Validation]
        public GradientProfileResult GetGradientProfile(string transactionId, GradientProfileRequest request)
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

                    CreateExposedServiceContext(transactionId, DEFAULT_LANGUAGE);

                    LogStartEvent(true);

                    // invoke the journey planner service
                    IJourneyPlannerSynchronous journeyPlanner = (IJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                    GradientProfileResult gradientProfileResult = journeyPlanner.GetGradientProfile(RequestContext, request);

                    LogFinishEvent(true);

                    return gradientProfileResult;
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
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.GradientProfileServiceError, tdException);
            }
            catch (Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);

                // log the error 
                LogError(exception.Message);

                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.GradientProfileServiceError, exception);
            }
        }
        #endregion
    }
}
