// *********************************************** 
// NAME                 : JourneyPlannerService.asmx.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 19/01/2006
// DESCRIPTION  		: This class implements the asynchronous Journey Planning web service. 
//                        The class provides methods for performing journey planning that are invoked
//                        by the External System as web services.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlanner/V1/JourneyPlannerService.asmx.cs-arc  $
//
//   Rev 1.2   Feb 11 2010 14:51:58   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:16   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.0   Nov 08 2007 13:52:04   mturner
//Initial revision.
//
//   Rev 1.4   Feb 21 2006 16:12:16   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 27 2006 16:29:16   COwczarek
//Remove handling of SOAPException - this case will never occur
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 20 2006 11:47:10   COwczarek
//Initial version
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

using TransportDirect.Common;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.UserPortal.JourneyPlannerService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;
using System.Web.Services.Protocols;

namespace TransportDirect.EnhancedExposedServices.JourneyPlanner.V1
{
	/// <summary>
    /// This class implements the asynchronous Journey Planning web service. The class provides methods
    /// for performing journey planning that are invoked by the External System as web services.
    /// Currently this class contains a single method for planning multi modal public transport journeys.
    /// In the future this may be extended to offer further journey planning methods, e.g. for private 
    /// journeys, quick planners and so on.
    /// </summary>
    [WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")
	,ValidationSchemaCache("./schemas")]	
	public class JourneyPlannerService : TDEnhancedExposedWebService
	{

        /// <summary>
        /// Default constructor.
        /// </summary>
		public JourneyPlannerService()
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
        /// This method commences planning of a multi modal public transport journey. 
        /// Parameters for journey planning are passed in the journeyRequest parameter. 
        /// The transactionId parameter is used for logging purposes and should be unique
        /// (although this is not mandatory). The method returns immediately and returns no value.
        ///  A SoapException is thrown for unexpected error cases.
        /// </summary>
		/// <param name="transactionId">An external reference</param>
		/// <param name="language">Language of text in response</param>
		/// <param name="request">Journey request parameters</param>
		[WebMethod,Validation]
		public void PlanPublicJourney(string transactionId, string language, PublicJourneyRequest request)
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
                    LogStartEvent(true);

                    // invoke journey planner service
                    IJourneyPlanner journeyPlanner = (IJourneyPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
                    journeyPlanner.PlanPublicJourney(RequestContext, request);

                    // Finish event will be logged asynchronously
                }
            }
            catch(TDException tdException)
            {
                // log the finish event 		
                LogFinishEvent(false);     
                // log the error but only if not already logged
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