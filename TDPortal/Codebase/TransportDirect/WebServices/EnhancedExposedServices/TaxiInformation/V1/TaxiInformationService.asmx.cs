// *********************************************** 
// NAME                 : TaxiInformationService.asmx.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : ??/??/2006
// DESCRIPTION  		: This class implements the Taxi Information web service. The class provides 
//                        methods for performing Taxi Information requests that are invoked by the External 
//                        System as web services.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/TaxiInformation/V1/TaxiInformationService.asmx.cs-arc  $
//
//   Rev 1.2   Feb 11 2010 14:51:56   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:14   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.Common;  
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;  

namespace TransportDirect.EnhancedExposedServices.TaxiInformation.V1
{
	/// <summary>
	/// Summary description for TaxiInformationService.
	/// </summary>
	[WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.TaxiInformation.V1"), 
	XmlInclude(typeof(TaxiInformationStopDetail)), XmlInclude(typeof(TaxiInformationOperator)),
	ValidationSchemaCache("./schemas")]
	public class TaxiInformationService : TDEnhancedExposedWebService
	{
		#region Constructor
		public TaxiInformationService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}
	     #endregion

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

		#region Web Methods
		[WebMethod(EnableSession=false), Validation] 		
		public TaxiInformationStopDetail GetTaxiInfo(string transactionId, string language,	string naptanId)
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

                    // checking naptanId

                    if (naptanId == null || naptanId.Length < 1)
                    {
                        // throw td exception 
                        throw new TDException(EnhancedExposedServicesMessages.TaxiInformationInvalidRequest, true, TDExceptionIdentifier.EESTaxiInformationInValidRequest);
                    }

                    // Getting the instance of StopTaxiInformation
                    StopTaxiInformation requestedTaxiInfo = new StopTaxiInformation(naptanId, true);

                    // translate domain object to dto object 
                    TaxiInformationStopDetail txResult = new TaxiInformationStopDetail();
                    txResult = TaxiInformationAssembler.CreateTaxiInformationStopDetailDT(requestedTaxiInfo);

                    // log the finish event 		
                    LogFinishEvent(true);
                    return txResult;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TaxiInformationEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TaxiInformationEnquiryError, exception);
			}
		}
		 #endregion
	}
}



