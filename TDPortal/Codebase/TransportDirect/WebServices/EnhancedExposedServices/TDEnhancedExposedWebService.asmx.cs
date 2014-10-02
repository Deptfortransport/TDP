// *********************************************** 
// NAME                 : TDEnhancedExposedWebServices.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 22/11/2005 
// DESCRIPTION  		: Base class for any web service of  EnhancedExposedServices.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/TDEnhancedExposedWebService.asmx.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:19:10   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:51:48   mturner
//Initial revision.
//
//   Rev 1.21   Jan 31 2006 16:10:10   mdambrine
//Fix fo the welsh language
//
//   Rev 1.20   Jan 23 2006 19:33:32   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.19   Jan 20 2006 13:29:18   mtillett
//Move all code requiring HttpContent e.g. for username and partner id lookup to single class. This is so that the test switch is in a seperate place to ensure refactoring later
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.18   Jan 20 2006 09:28:04   mtillett
//Remove SetForTest property and add test code to allow unit testing
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.17   Jan 19 2006 17:31:32   mtillett
//Add property to allow web service methods to be tested via NUNIT
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.16   Jan 16 2006 11:59:44   halkatib
//Fixed bug for LogFinishEvent
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.15   Jan 13 2006 13:30:52   mtillett
//Delete obsolete method
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.14   Jan 11 2006 16:43:12   mtillett
//Update Log method to log the InternalTransactionId as the sessionId
//
//   Rev 1.13   Jan 11 2006 16:31:24   schand
//Parsing extracted ServiceType and handling the error
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.12   Jan 10 2006 16:05:58   schand
//Passing contect soap action to ExposedServiceContext
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.11   Jan 05 2006 14:52:18   mtillett
//Prevent logging where no context available
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.10   Jan 04 2006 12:37:48   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.9   Jan 04 2006 10:08:46   halkatib
//Fixed error on page.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.8   Dec 23 2005 11:10:38   halkatib
//Removed setter that is not required by the context object
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.7   Dec 23 2005 10:42:06   halkatib
//Added second overload for the Finish event the takes a Context object as a parameter.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.6   Dec 22 2005 17:24:40   halkatib
//Applied changes reqested by Chris O
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.5   Dec 22 2005 14:23:10   halkatib
//Changes requested by Chris O made.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.4   Dec 22 2005 11:27:24   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Dec 14 2005 15:56:46   schand
//Added more namespace at the top
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Dec 05 2005 17:57:32   schand
//Added overloaded method for LgStartEvent and CreateExposedServiceContext()
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 30 2005 16:02:24   schand
//pre- Code review changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Xml; 
using System.Web.Services.Protocols;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security; 
using Microsoft.Web.Services3.Security.Tokens; 
using TransportDirect.Common;
using TransportDirect.Common.Logging;   
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;
using TransportDirect.EnhancedExposedServices.Common; 
using TransportDirect.EnhancedExposedServices.Helpers; 

namespace TransportDirect.EnhancedExposedServices
{
	/// <summary>
	/// Base class for any web service of  EnhancedExposedServices.
	/// </summary>
	public class TDEnhancedExposedWebService : System.Web.Services.WebService
	{
		#region Class Members
		private const string EN = "en-GB";
		private const string CY = "cy-GB";
		private ExposedServiceContext exposedServiceContext;
		#endregion
		
		#region Constructor
		public TDEnhancedExposedWebService()
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

		#region Protected Members
		/// <summary>
		/// Protected read only property for getting 
		/// </summary>
		protected ExposedServiceContext RequestContext
		{
			get { return exposedServiceContext; }			
		}
		/// <summary>
		/// Protected method to log the start of the request with the given Exposed Service Context
		/// </summary>
		/// <param name="exposedServiceContext">Enhanced Exposed Service Context object</param>		
		/// <param name="callSucessful">Indicates whether call was sucessfull or not</param>
		protected void LogStartEvent(bool callSuccessful)
		{   	
			if (exposedServiceContext != null)
			{
				Logger.Write(new EnhancedExposedServiceStartEvent(callSuccessful, exposedServiceContext));						
			}
		}

		/// <summary>
		/// Protected method to log the end of the request
		/// </summary>
		/// <param name="enhanceExpServiceType">Enhanced Exposed Service Type Request</param>
		/// <param name="externalTransactionId">Reference transaction Id provided by client</param>
		/// <param name="callSucessful">Indicates whether call was sucessfull or not</param>
		protected void LogFinishEvent(bool callSuccessful)
		{
			if (exposedServiceContext != null)
			{
				Logger.Write(new EnhancedExposedServiceFinishEvent(callSuccessful, exposedServiceContext));						
			}
		}
		
		/// <summary>
		/// Protected method to return new instance of Exposed Service context.
		/// </summary>
		/// <param name="enhanceExpServiceType"></param>
		/// <param name="externalTransactionId"></param>
		/// <returns>ExposedServiceContext object</returns>
		protected void CreateExposedServiceContext(string externalTransactionId, string language)
		{	  
			if (CheckLanguage(language))
			{
				EnhancedExposedServicesHelper helper = new EnhancedExposedServicesHelper();
				exposedServiceContext = helper.CreateExposedServiceContext(externalTransactionId, language);
			}
			else
			{
				//throw exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Incorrect language string received - " + language));
				throw new TDException(EnhancedExposedServicesMessages.LanguageNotSupported, true, TDExceptionIdentifier.EESGeneralErrorCode);
			}
		}	

		/// <summary>
		/// Protected method to log errors
		/// </summary>
		/// <param name="message"></param>
		protected void LogError(string message)
		{
			string transactionId = String.Empty;
			if (exposedServiceContext != null)
			{
				transactionId = exposedServiceContext.InternalTransactionId;
			}
			// log the message			
			Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message, null, transactionId)) ;				
		}
		
		
		#endregion
		
		#region Private Members
		/// <summary>
		/// private method to check if the language is a english or welsh
		/// </summary>
		/// <param name="language"></param>
		/// <returns></returns>
		private static bool CheckLanguage(string language)
		{
			if ((language.ToLower().StartsWith("en")) || (language.ToLower().StartsWith("cy")))
			{
				return true;
			}
			else 
				return false;
		}		
		#endregion	
	}
}
