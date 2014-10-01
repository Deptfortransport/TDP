// *********************************************** 
// NAME                 : ExposedServiceContext.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 07/02/2005
// DESCRIPTION  :  ExposedServiceContext used to initailise internal properties and
//				   to provide static methods to retrieve ServiceType and OperationType
//                 from the soapAction 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesCommon/Common/ExposedServiceContext.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:04   mturner
//Initial revision.
//
//   Rev 1.16   Mar 07 2006 13:34:56   build
//Not allowing CLSCompliant to be set to true as the DefaultAssemblyInfo file does not have this set to true.
//
//   Rev 1.15   Feb 22 2006 16:05:30   RWilby
//Added descriptive XML summary and file header description.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.14   Feb 21 2006 15:48:18   RWilby
//Updated GetServiceType and GetOperationType method to removed the TransportDirect domain from the soapAction.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.13   Feb 20 2006 15:36:38   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.12   Feb 02 2006 11:55:54   schand
//Code REview Changes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.11   Jan 31 2006 16:10:10   mdambrine
//Fix fo the welsh language
//
//   Rev 1.10   Jan 27 2006 16:24:52   COwczarek
//Initialise ServiceType and OperationType to empty strings to
//allow logging to work in cases where these two properties are
//not set (i.e. during NUnit testing where no Soap action is
//available)
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Jan 20 2006 19:38:16   schand
//Added more comments and serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.8   Jan 12 2006 19:59:00   schand
//Changes for operation type
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.7   Jan 10 2006 15:19:30   schand
//Passing Context Soap Action as a parameter instead of deriving it with the class
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.6   Jan 05 2006 13:57:12   mtillett
//Ensure that the correct namespace and method names are obtained form the SOAPAction
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Dec 23 2005 10:40:46   halkatib
//Added error handling to detect of the httpcontext is empty
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.4   Dec 22 2005 14:58:24   halkatib
//Changes requested by Chris o made
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Nov 25 2005 14:36:02   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon
//
//   Rev 1.2   Nov 22 2005 16:21:32   rgreenwood
//Changed namespace
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 22 2005 16:00:36   rgreenwood
//Changed namespace
//
//   Rev 1.0   Nov 21 2005 13:16:24   rgreenwood
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
using System.Web;
using System.Threading;
using System.Globalization;
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;


namespace TransportDirect.EnhancedExposedServices.Common
{
	/// <summary>
	/// ExposedServiceContext used to initailise internal properties and
	/// to provide static methods to retrieve ServiceType and OperationType from the soapAction 
	/// </summary>
	[Serializable]
	public class ExposedServiceContext
	{
		#region Private Methods
		private string partnerId  = String.Empty;
		private string internalTransactionId;
		private string externalTransactionId = String.Empty;
		private string language = String.Empty;
		private string serviceType = String.Empty;
		private string operationType = String.Empty;		
		private const char SEPARATOR = '/';
		private const string TRANSPORTDIRECTDOMAIN =  @"http://www.transportdirect.info/";
		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public ExposedServiceContext() : this(String.Empty, String.Empty, String.Empty, String.Empty)
		{
		}

		/// <summary>
		/// Overloaded Contructor which initailise the class members
		/// </summary>
		/// <param name="partner">Partner's username</param>
		/// <param name="externalTransaction">COnsumer transaction Id</param>
		/// <param name="languageIdentifier">Language</param>
		/// <param name="contextRequestString">Soap context string</param>
		public ExposedServiceContext(string partner, string externalTransaction, string languageIdentifier, string contextRequestString)
		{
			partnerId = partner;
			internalTransactionId = System.Guid.NewGuid().ToString();
			externalTransactionId = externalTransaction;
			language = languageIdentifier;
			serviceType = GetServiceType(contextRequestString);
			operationType = GetOperationType(contextRequestString);
			SetThreadCurrentCulture();			
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Read-Only property to return partner Id
		/// </summary>
		public string PartnerId
		{
			get { return partnerId; }			
		}

		/// <summary>
		/// Read-Only property to return Internal TransactionId
		/// </summary>
		public string InternalTransactionId
		{
			get { return internalTransactionId; }
		}

		/// <summary>
		/// Read-Only property to return external TransactionId
		/// </summary>
		public string ExternalTransactionId
		{
			get { return externalTransactionId; }
		}
		
		/// <summary>
		/// Read-Only property to return lanaguge
		/// </summary>
		public string Language
		{
			get { return language; }
		}

		/// <summary>
		/// Read-Only property to return service Type
		/// </summary>
		public string ServiceType
		{
			get { return serviceType;}
		}
		
		/// <summary>
		/// Read-Only property to return operation Type
		/// </summary>
		public string OperationType
		{
			get { return operationType;}
		}
		
		#endregion

		#region Public Methods
		public void SetThreadCurrentCulture()
		{
			//new TDCultureIno object
			TDCultureInfo cultureInfo;

			//if the language is CY than default the culture to Welsh United Kingdom
			//else default it to english United Kingdom
			if (language.ToLower().StartsWith("cy"))
			{
				cultureInfo = new TDCultureInfo( "en-GB", "cy-GB", 1106 );							
			}
			else
			{
				CultureInfo ci = new CultureInfo("en-GB");
				cultureInfo = new TDCultureInfo( ci.Name, ci.Name, ci.LCID );
			}	

			//set culture to use according to the cultureInfo settings			
			Thread.CurrentThread.CurrentUICulture = cultureInfo;			
		}
		#endregion				

		#region Static Methods
		/// <summary>
		/// Gets the servicetype from a soapaction string
		/// </summary>
		/// <param name="soapAction">Soapaction</param>
		/// <returns>the servicetype in a transport direct </returns>
		public static string GetServiceType(string soapAction)
		{
			try
			{
				//Removed the TransportDirect domain from the soapAction
				soapAction = soapAction.Replace(TRANSPORTDIRECTDOMAIN,string.Empty);

				string[] ContextRequest = soapAction.Split(SEPARATOR);

				string serviceType = ContextRequest[0].Trim('"');
				serviceType = serviceType.Replace(".", "_");

				// Checking if extracted service type is one of the defined enum value 
				Enum.Parse(typeof(EnhancedExposedServiceTypes), serviceType, true);  

				return serviceType;
			}
			catch
			{				
				//log error and throw exception
				string message = "An error occurred while resolving the SoapAction:" + soapAction;

				OperationalEvent oe = new  OperationalEvent(TDEventCategory.Business,
															TDTraceLevel.Error,
															message);

				Logger.Write(oe);										

				throw new TDException(message, false, TDExceptionIdentifier.EESGeneralErrorCode);
			}
		}

		/// <summary>
		/// Gets the operationType from a soapaction string
		/// </summary>
		/// <param name="soapAction">Soapaction</param>
		/// <returns>the servicetype in a transport direct </returns>
		public static string GetOperationType(string soapAction)
		{
			try
			{
				//Removed the TransportDirect domain from the soapAction
				soapAction = soapAction.Replace(TRANSPORTDIRECTDOMAIN,string.Empty);

				string[] ContextRequest = soapAction.Split(SEPARATOR);

				string operationType = ContextRequest[1].Trim('"');				

				// Checking if extracted operation type is one of the defined enum value 
				Enum.Parse(typeof(EnhancedExposedOperationType), operationType, true);  

				return operationType;
			}
			catch
			{				
				//log error and throw exception
				string message = "An error occurred while resolving the SoapAction:" + soapAction;

				OperationalEvent oe = new  OperationalEvent(TDEventCategory.Business,
															TDTraceLevel.Error,
															message);

				Logger.Write(oe);										

				throw new TDException(message, false, TDExceptionIdentifier.EESGeneralErrorCode);
			}
		}
		#endregion
	}
}
