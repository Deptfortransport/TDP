// *********************************************** 
// NAME                 : CodeService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 12/01/2006 
// DESCRIPTION  		: Code Web Service class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CodeHandler/V1/CodeService.asmx.cs-arc  $ 
//
//   Rev 1.3   Feb 11 2010 14:52:00   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.2   Feb 10 2010 16:28:18   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Jul 16 2008 15:44:42   mturner
//Added try catch to handle previously unhandled exception.
//Resolution for 5070: Invalid CRS code submission causes EES to throw Exception
//
//   Rev 1.0   Nov 08 2007 13:51:52   mturner
//Initial revision.
//
//   Rev 1.6   May 14 2007 15:40:34   mturner
//Fixed bug that resulted in locality ID's being inserted into the column that should hold NaPTAN ID's
//
//   Rev 1.5   May 14 2007 12:47:24   mturner
//Added code to attempt to retrieve location information from the NaPTAN cache if the initial FindCode call to the CodeGaz returns no results.
//
//   Rev 1.4   Feb 21 2006 16:27:26   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 23 2006 19:33:34   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 10:28:58   mtillett
//Correct ValidationSchemaCache location attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 17 2006 15:14:50   schand
//Added validation
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


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
using TransportDirect.Common;  
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.Validation;
using TransportDirect.EnhancedExposedServices.SoapFault.V1;  


namespace TransportDirect.EnhancedExposedServices.CodeHandler.V1
{
	/// <summary>
	/// This web service will be resposible for provding code related information.
	/// </summary>
	[WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CodeHandler.V1"), 
	XmlInclude(typeof(CodeServiceCodeDetail)), XmlInclude(typeof(CodeServiceRequest)),
	ValidationSchemaCache("./schemas")]
	public class CodeService : TDEnhancedExposedWebService
	{
		#region Constructor
		public CodeService()
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
		public CodeServiceCodeDetail[] FindCode(string transactionId,	string language,	string code)
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

                    // get the Code Gazetter reference
                    ITDCodeGazetteer cg = (ITDCodeGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.CodeGazetteer];
                    TDCodeDetail[] codedetail;
                    codedetail = cg.FindCode(code);
                    try
                    {
                        if (codedetail.Length == 0)
                        {
                            // No data has been found so try to find data from the NaPTAN cache
                            NaptanCacheEntry nce = NaptanLookup.Get(code, "");
                            if (nce.Found == true)
                            {
                                codedetail = new TDCodeDetail[1];
                                codedetail[0] = new TDCodeDetail();
                                codedetail[0].Code = nce.Naptan;
                                codedetail[0].CodeType = TDCodeType.NAPTAN;
                                codedetail[0].Description = nce.Description;
                                codedetail[0].Easting = nce.OSGR.Easting;
                                codedetail[0].Locality = nce.Locality;
                                codedetail[0].NaptanId = nce.Naptan;
                                codedetail[0].Northing = nce.OSGR.Northing;
                                switch (nce.Naptan.Substring(0, 4))
                                {
                                    case ("9000"):
                                        {
                                            codedetail[0].ModeType = TDModeType.Coach;
                                            break;
                                        }
                                    case ("9100"):
                                        {
                                            codedetail[0].ModeType = TDModeType.Rail;
                                            break;
                                        }
                                    case ("9200"):
                                        {
                                            codedetail[0].ModeType = TDModeType.Air;
                                            break;
                                        }
                                    case ("9300"):
                                        {
                                            codedetail[0].ModeType = TDModeType.Ferry;
                                            break;
                                        }
                                    case ("9400"):
                                        {
                                            codedetail[0].ModeType = TDModeType.Metro;
                                            break;
                                        }
                                    default:
                                        {
                                            codedetail[0].ModeType = TDModeType.Bus;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        // log the finish event 		
                        LogFinishEvent(false);
                        //log the error 				
                        LogError(exception.Message);
                    }
                    // translating to exposed object 
                    CodeServiceCodeDetail[] codeServiceCodeDetail = CodeServiceAssembler.CreateCodeServiceCodeDetailArrayDT(codedetail);

                    // log the finish event 		
                    LogFinishEvent(true);
                    return codeServiceCodeDetail;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CodeServiceEnquiryError , tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CodeServiceEnquiryError, exception);
			}

		}

		[WebMethod(EnableSession=false), Validation] 		 
		public CodeServiceCodeDetail[]  FindText(string transactionId, string language,CodeServiceRequest codeRequest)
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

                    // checking request object 
                    if (codeRequest == null)
                        throw new TDException(EnhancedExposedServicesMessages.CodeServiceInvalidRequest, true, TDExceptionIdentifier.EESCodeServiceInValidRequest);

                    // get the Code Gazetter reference
                    ITDCodeGazetteer cg = (ITDCodeGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.CodeGazetteer];
                    TDCodeDetail[] codedetails;
                    codedetails = cg.FindText(codeRequest.PlaceText, codeRequest.Fuzzy, CodeServiceAssembler.CreateTDModeTypeArray(codeRequest.ModeTypes));

                    // translating to exposed object 
                    CodeServiceCodeDetail[] codeServiceCodeDetails = CodeServiceAssembler.CreateCodeServiceCodeDetailArrayDT(codedetails);

                    // log the finish event 		
                    LogFinishEvent(true);
                    return codeServiceCodeDetails;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CodeServiceEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.CodeServiceEnquiryError, exception);
			}
		}
	   #endregion

	}
}



