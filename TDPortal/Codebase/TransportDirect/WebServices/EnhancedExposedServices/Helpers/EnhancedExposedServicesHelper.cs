// *********************************************** 
// NAME                 : EnhancedExposedServicesHelper.cs
// AUTHOR               : Sanjeev Chand.
// DATE CREATED         : 22/11/2005 
// DESCRIPTION  		: Enhanced Exposed ServicesHelper class contains some function/routines to perform common tasks.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Helpers/EnhancedExposedServicesHelper.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:05:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:00   mturner
//Initial revision.
//
//   Rev 1.12   Feb 20 2006 15:36:36   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.11   Jan 23 2006 19:33:34   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.10   Jan 20 2006 13:29:18   mtillett
//Move all code requiring HttpContent e.g. for username and partner id lookup to single class. This is so that the test switch is in a seperate place to ensure refactoring later
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.9   Jan 11 2006 09:17:12   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.8   Jan 05 2006 17:10:56   mtillett
//Fix FxCop errors with Helper and Validation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.7   Jan 05 2006 15:57:36   mtillett
//Add method level comments
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.6   Jan 05 2006 14:54:04   mtillett
//Fix up SoapException creation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Jan 04 2006 12:37:48   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Dec 22 2005 11:24:04   asinclair
//Check in of Sanjeev's Work In Progress Code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Dec 14 2005 15:57:40   schand
//Added more helper method
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Nov 28 2005 16:46:18   schand
//Code review changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 28 2005 12:04:14   schand
//FxCop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Nov 25 2005 18:46:22   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Web;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security; 
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common;
using TransportDirect.Common.Logging;   
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.Partners;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.EnhancedExposedServices.Helpers
{	
	/// <summary>
	/// Enhanced Exposed ServicesHelper class contains some function/routines to perform common tasks.
	/// </summary>
	public sealed class EnhancedExposedServicesHelper
	{
		#region	Default Constructor 
		/// <summary>
		/// Public contructor
		/// </summary>
		public EnhancedExposedServicesHelper()
		{
		}
		#endregion

		#region Private Members
		/// <summary>
		/// Read-Only property to return soap user name
		/// </summary>
		private string SoapUsername
		{
			get 
			{
				string username=string.Empty;
				SoapContext requestContext = RequestSoapContext.Current;
				if (requestContext == null || requestContext.Security == null )
					throw new SoapFormatException(EnhancedExposedServicesMessages.SoapContextRequired); 
				
				// Look through all the tokens in the Tokens collection 
				// for a UsernameToken.
				foreach (SecurityToken tok in requestContext.Security.Tokens)
				{
					UsernameToken token = tok as UsernameToken;
					if (token != null)
					{	
						username =  token.Username;
						break;           						
					}
				}

				return username; 
			}
		}
		/// <summary>
		/// Private method to get partner id from the soap username
		/// </summary>
		/// <returns></returns>
		private string CalculatePartnerId()
		{	
			string username;			
			username = SoapUsername; 
			PartnerCatalogue partnerCatalogue =  (PartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue];  
			return partnerCatalogue.GetPartnerIdFromHostName(username).ToString(CultureInfo.InvariantCulture);  							  
		}	
		#endregion

		/// <summary>
		/// Creates an ExposedServiceContext class using real data
		/// or fake context for testing purposes
		/// </summary>
		/// <param name="externalTransactionId">External Transaction Id</param>
		/// <param name="language">Language</param>
		/// <returns>ExposedServiceContext object</returns>
		public ExposedServiceContext CreateExposedServiceContext(string externalTransactionId, string language)
		{
			try
			{
				ExposedServiceContext exposedServiceContext;
				if (RequestSoapContext.Current != null)
				{
					//create context using real data
					string partnerId = CalculatePartnerId();
					exposedServiceContext = new ExposedServiceContext(partnerId, externalTransactionId, language, HttpContext.Current.Request.Headers["SoapAction"]);			
				}
				else
				{
					//create fake context for testing ONLY
					exposedServiceContext = new ExposedServiceContext("101", externalTransactionId, language, "TransportDirect.EnhancedExposedServices.TestWebService/RequestContextData");			
				}
				return exposedServiceContext;
			}
			catch(ArgumentNullException aNEx)
			{
				//throw exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, aNEx.Message));
				throw new TDException(EnhancedExposedServicesMessages.InvalidServiceOrOperation, true, TDExceptionIdentifier.EESGeneralErrorCode);
			}
			catch (ArgumentException aEx)
			{
				//throw exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, aEx.Message));
				throw new TDException(EnhancedExposedServicesMessages.InvalidServiceOrOperation, true, TDExceptionIdentifier.EESGeneralErrorCode);
			}
		}
	}
}
