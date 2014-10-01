// *********************************************** 
// NAME                 : TDAuthManager.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 22/11/2005 
// DESCRIPTION  		: Enhanced ExposedServices Custom UserToken Manager class for checking WSE username/password against database.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesSecurity/TDAuthManager.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:22:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 12:22:56   mturner
//Initial revision.
//
//   Rev 1.6   Feb 20 2006 15:36:40   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.5   Dec 16 2005 11:39:36   schand
//Added constructor to take config data
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.4   Nov 29 2005 13:31:38   schand
//Creating the instance of PartnerCatlogue from ServiceDiscovery instead of creating a new instance each time.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Nov 28 2005 18:32:20   schand
//Code review changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Nov 28 2005 14:36:04   schand
//FxCop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 28 2005 12:05:14   schand
//FxCop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Nov 25 2005 18:55:10   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements


using System;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security; 
using Microsoft.Web.Services3.Security.Tokens; 
using TransportDirect.Partners; 
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using System.Xml; 
using TransportDirect.EnhancedExposedServices.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
   

namespace TransportDirect.EnhancedExposedServices.Security
{
	/// <summary>
	/// Enhanced ExposedServices Custom UserToken Manager class for checking WSE username/password against database.
	/// </summary>
	public class TDAuthManager : UsernameTokenManager 
	{
		
		#region Declarations
		private PartnerCatalogue partnerCatalogue;
		#endregion

		#region constructor
		public TDAuthManager(): base()
		{
		}

		public TDAuthManager(XmlNodeList xmlNodeList): base(xmlNodeList)
		{
		}
		#endregion

		#region Protected Members
		/// <summary>
		/// Protected overriden method for authenticating WSE users
		/// </summary>
		/// <param name="token">Username token</param>
		/// <returns>Password in clear text</returns>     
		protected override string AuthenticateToken(UsernameToken token)
		{
			string username = token.Username;   
						
			//get the partnercatalogue. This is in here and not in the constructor because datanotification could change
			//the value of the partnercatalogue
			partnerCatalogue = (PartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue];

			// Lookup the user's password based on their username ...
			string password = partnerCatalogue.GetClearPassword(username);  

			//check if the partner is allowed to access the webservice
			CheckAccessRestriction(username);

			return password;						
		}         
		#endregion					

		#region Private Methods	
		/// <summary>
		/// This method will check if a partner has got the rights to proceed with this web service. 
		/// </summary>
		/// <param name="username">The passed username</param>
		private void CheckAccessRestriction(string username)
		{			
			//get the soapaction from the WSE2 soapcontext
			string SoapAction = SoapContext.Current.Addressing.Action.ToString();

			string serviceName = ExposedServiceContext.GetServiceType(SoapAction);
			
			bool IsPartnerAllowed = partnerCatalogue.IsPartnerAllowedToService(username, serviceName);

			//if the if the partner is not allowed then throw an error and log it
			if (IsPartnerAllowed == false)
			{		
				//log error and throw exception
				string message = "Access to this service has been restricted for this user account, username={0}, servicename={1}";
				message = message.Replace("{0}", username);
				message = message.Replace("{1}", serviceName);

				OperationalEvent oe = new  OperationalEvent(TDEventCategory.Business,
															TDTraceLevel.Error,
															message);

				Logger.Write(oe);										

				throw new TDException(message, false, TDExceptionIdentifier.EESAccessDenied);
			}
		}
		#endregion
	}
}
