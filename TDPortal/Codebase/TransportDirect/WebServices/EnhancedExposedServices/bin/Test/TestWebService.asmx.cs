// *********************************************** 
// NAME                 : TestWebService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 22/11/2005 
// DESCRIPTION  		: External test Web service class  
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestWebService.asmx.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:26   mturner
//Initial revision.
//
//   Rev 1.16   Feb 21 2006 17:00:42   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.15   Jan 20 2006 13:29:18   mtillett
//Move all code requiring HttpContent e.g. for username and partner id lookup to single class. This is so that the test switch is in a seperate place to ensure refactoring later
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.14   Jan 19 2006 17:29:50   mtillett
//Add new test method to test langauge validation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.13   Jan 12 2006 18:12:56   mtillett
//Update MessageValidation to ensure that the SOAP response is validated aginast the WSDL in schema folder
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.12   Jan 11 2006 09:17:14   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.11   Jan 10 2006 17:26:42   schand
//Removed TestMethod()
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.10   Jan 10 2006 16:58:10   schand
//Added TestMethod()
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.9   Jan 05 2006 14:56:28   mtillett
//Update test classes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.8   Jan 04 2006 15:47:00   mtillett
//Add new test web service
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.7   Jan 04 2006 12:57:14   mtillett
//Move Test web service into the correct namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.6   Jan 04 2006 12:37:50   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Dec 22 2005 17:24:42   halkatib
//Applied changes reqested by Chris O
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.4   Dec 22 2005 14:59:34   halkatib
//Changes requested by Chris o made
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Nov 30 2005 15:59:30   schand
//Code review Changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Nov 29 2005 13:34:24   schand
//Pre-Code review changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 28 2005 12:04:34   schand
//FxCop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Nov 25 2005 18:48:02   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text; 
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using TransportDirect.Common;  
using TransportDirect.EnhancedExposedServices.Common;  
using TransportDirect.EnhancedExposedServices.Helpers;  
using TransportDirect.EnhancedExposedServices.SoapFault.V1;  
using TransportDirect.EnhancedExposedServices.Validation;  

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// External test Web service class. 
	/// </summary>
	[WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.TestWebService")
	,ValidationSchemaCache("./schemas")]
	public class TestWebService : TDEnhancedExposedWebService
	{
		public TestWebService()
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
		/// Mock method to allow valid and invalid language to be tested 
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language"></param>
		/// <returns></returns>
		public string LanguageTest(string transactionId, string language)
		{
			try			
			{
				CreateExposedServiceContext(transactionId, language);
				return RequestContext.Language;
			}
			catch(TDException tdex)
			{   
				SoapException soapex = SoapExceptionHelper.ThrowSoapException(
					"Test message",	tdex);
				throw soapex;
			}
			catch(Exception ex)
			{   
				throw SoapExceptionHelper.ThrowSoapException(
					"Test message",	ex);
			}		
		}
		/// <summary>
		/// Web service method to test Enhances Exposed services framework 
		/// security and request context
		/// </summary>
		/// <returns>It returns the username and partner id for the valid soap username</returns>
		[WebMethod]		
		public string[] RequestContextData(string transactionId, string language)
		{	
			try			
			{
				CreateExposedServiceContext(transactionId, language);				
				LogStartEvent(true);   

				string[] returndata = new string[7];
				returndata[1] = RequestContext.ExternalTransactionId;
				returndata[2] = RequestContext.InternalTransactionId;
				returndata[3] = RequestContext.Language;
				returndata[4] = RequestContext.OperationType;
				returndata[5] = RequestContext.PartnerId;
				returndata[6] = RequestContext.ServiceType;
				
				LogFinishEvent(true);

				return returndata;
			}
			catch(TDException tdex)
			{   
				LogFinishEvent(false);
				SoapException soapex = SoapExceptionHelper.ThrowSoapException(
					"Test message",	tdex);
				throw soapex;
			}
			catch(Exception ex)
			{   
				LogFinishEvent(false);
				throw SoapExceptionHelper.ThrowSoapException(
					"Test message",	ex);
			}
		}

		/// <summary>
		/// Mock web method used to test the MessageValidation
		/// </summary>
		/// <param name="transactionId"></param>
		/// <param name="language"></param>
		/// <param name="param1"></param>
		/// <param name="param2"></param>
		[WebMethod,Validation]
		public string WSDLValidation(string transactionId, string language, int param1, string param2)
		{
			//return param2 to allow testing for valid and invalid response data
			return param2;
		}
	}
}
