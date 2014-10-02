// *********************************************** 
// NAME                 : OpenJourneyPlanner.asmx.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 31/03/2006
// DESCRIPTION  		: This class implements the Open Journey Planning web service. The class provides 
//                        methods for performing journey planning that are invoked by the External 
//                        System as web services.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/OpenJourneyPlanner/V1/OpenJourneyPlannerService.asmx.cs-arc  $
//
//   Rev 1.2   Feb 11 2010 14:51:56   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:14   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.0   Nov 08 2007 13:52:16   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:16:24   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using TransportDirect.Common;  
using TransportDirect.Common.ServiceDiscovery;   
using TransportDirect.EnhancedExposedServices.Common;      
using TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Helpers;      
using TransportDirect.EnhancedExposedServices.SoapFault.V1;        
using TransportDirect.EnhancedExposedServices.Validation;  
using Logger = System.Diagnostics.Trace;
using CJPInterface = TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.JourneyPlanning.CJP;

namespace TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1
{
	/// <summary>
	/// This class implements the Open Journey Planning web service. The class provides methods for 
	/// performing journey planning that are invoked by the External System as web services.
	/// </summary>
    [WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1"), 
    ValidationSchemaCache("./schemas")]
    public class OpenJourneyPlannerService : TDEnhancedExposedWebService
	{
		public OpenJourneyPlannerService()
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
        /// This method performs planning of a public transport journey. Parameters for journey planning are
        /// passed in the request parameter. The transactionId parameter is used for logging purposes and 
        /// should be unique (although this is not mandatory). When journey planning is complete, the 
        /// journey plans are returned in the Result object. The method will throw a SoapException if the 
        /// request is invalid or if errors occur during journey planning.
        /// </summary>
        /// <param name="transactionId">An external reference</param>
        /// <param name="language">Language of text in response</param>
        /// <param name="request">Journey request parameters</param>
        /// <returns>Journey plan result</returns>
        [WebMethod(EnableSession=false), Validation] 
        public Result PlanJourney(string transactionId, string language, Request request)
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

                    CJPInterface.JourneyRequest journeyRequest = OpenJourneyPlannerAssembler.CreateJourneyRequest(
                        request, RequestContext.Language, RequestContext.InternalTransactionId);

                    ICJP journeyPlanner = (ICJP)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];

                    CJPInterface.CJPResult journeyResult = journeyPlanner.JourneyPlan(journeyRequest);

                    Result result = OpenJourneyPlannerAssembler.CreateResultDT(
                        (CJPInterface.JourneyResult)journeyResult);

                    LogFinishEvent(true);

                    return result;
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
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.OpenJourneyPlannerServiceError, tdException);
            }
            catch(Exception exception)
            {
                // log the finish event 		
                LogFinishEvent(false);     
                // log the error 
                LogError(exception.Message);
                // wrap the error and throw the exception
                throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.OpenJourneyPlannerServiceError, exception);
            }
        }
	}
}
