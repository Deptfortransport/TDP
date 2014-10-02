// *********************************************** 
// NAME                 : JourneyPlannerSynchronousService.asmx.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 19/01/2006
// DESCRIPTION  		: This class implements the synchronous Journey Planning web service. 
//                        It offers identical functionality to its asynchronous counterpart, 
//                        the JourneyPlannerService class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlannerSynchronous/V1/JourneyPlannerSynchronousService.asmx.cs-arc  $
//
//   Rev 1.3   Feb 24 2010 14:39:28   MTurner
//Changes to allow TI to call this service.
//Resolution for 5411: EES Security improvements led to TI injections failing
//
//   Rev 1.2   Feb 11 2010 14:51:58   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:16   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.0   Nov 08 2007 13:52:06   mturner
//Initial revision.
//
//   Rev 1.5   Feb 21 2006 16:09:26   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Jan 27 2006 16:29:18   COwczarek
//Remove handling of SOAPException - this case will never occur
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 20 2006 12:16:08   COwczarek
//Initial version
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Xml;

using TransportDirect.Common;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.UserPortal.JourneyPlannerService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using System.Web.Services.Protocols;

namespace TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1
{
    /// <summary>
    /// This class implements the synchronous Journey Planning web service. It offers identical 
    /// functionality to its asynchronous counterpart, the JourneyPlanner class. 
    /// </summary>
    [WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")
    ,ValidationSchemaCache("./schemas")]	
    public class JourneyPlannerSynchronousService : TDEnhancedExposedWebService
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JourneyPlannerSynchronousService()
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
        /// This method performs planning of a multi modal public transport journey. Parameters for journey
        /// planning are passed in the journeyRequest parameter. The transactionId parameter is used for
        /// logging purposes and should be unique (although this is not mandatory). When journey planning 
        /// is complete, the journey plans are returned in the PublicJourneyResult object. The method 
        /// will throw a SoapException if the request is invalid, if errors occur during journey 
        /// planning or if no journeys were found.
        /// </summary>
        /// <param name="transactionId">An external reference</param>
        /// <param name="language">Language of text in response</param>
        /// <param name="request">Journey request parameters</param>
        /// <returns>Journey plan result</returns>
        [WebMethod,Validation]
        public PublicJourneyResult PlanPublicJourney(string transactionId, string language, PublicJourneyRequest request)
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
                    LogStartEvent(true);

                    // invoke journey planner service
                    IJourneyPlannerSynchronous journeyPlanner = (IJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                    PublicJourneyResult journeyResult = journeyPlanner.PlanPublicJourney(RequestContext, request);

                    LogFinishEvent(true);

                    return journeyResult;
                }
                else
                {
                    //should never go here but all code paths need to return a value
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
                }
            }
            catch(TDException tdException)
            {
                // log the finish event 		
                LogFinishEvent(false);     
                // log the error 
                if (!tdException.Logged) 
                {
                    LogError(tdException.Message);
                }
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.JourneyPlannerServiceError, tdException);
            }
            catch(Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);     
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.JourneyPlannerServiceError, exception);
            }

        }		
    }
}