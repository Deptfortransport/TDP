// ***********************************************************************************
// NAME 		: CacheEngine
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: CacheEngine for the testtool, encapsulates some cache key values
// ************************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/CacheEngine.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:14   mturner
//Initial revision.
//
//   Rev 1.7   Feb 07 2006 10:50:34   mdambrine
//added pvcs log

using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// CacheEngine for the testtool, encapsulates some cache key values
	/// </summary>
	public class CacheEngine
	{		
		private const string cacheKey = "TestToolTransactions";

		#region properties
		/// <summary>
		/// read-only property returing the lifetime of the cache 
		/// </summary>
		public static int AbsoluteExpiryMinutes
		{
			get{ return Convert.ToInt32(HelperClass.GetConfigSetting("CacheLifeTime"), HelperClass.Provider);}			
		}
		#endregion

		/// <summary>
		/// This method returns the Soaptransactions present in the cache
		/// If it doesn't exist a new one will be instaciated
		/// </summary>
		/// <returns>The transactions in the cache</returns>
		public static SoapTransactions RetrieveTransactions()
		{			
			SoapTransactions soapTransactions = (SoapTransactions) System.Web.HttpContext.Current.Application[cacheKey];		

			if (soapTransactions == null)
			{
				soapTransactions = new SoapTransactions();

				System.Web.HttpContext.Current.Application[cacheKey] = soapTransactions;

			}

			return soapTransactions;

		}

		/// <summary>
		/// This method returns the Soaptransactions present in the cache
		/// If it doesn't exist a new one will be instaciated
		/// use this method if the cache needs to be retrieved in an async process
		/// </summary>
		/// <param name="context">the Page.Context in which the application is running</param>
		/// <returns>The transactions for this context</returns>
		public static SoapTransactions RetrieveTransactions(System.Web.HttpContext context)
		{
			

			SoapTransactions soapTransactions = (SoapTransactions) context.Cache[cacheKey];						

			if (soapTransactions == null)
			{
				soapTransactions = new SoapTransactions();				

				context.Cache.Insert(cacheKey, 
									 soapTransactions,
									 null,
									 //DateTime.Now.AddMinutes(AbsoluteExpiryMinutes),
									 DateTime.MaxValue,
									 //TimeSpan.FromMinutes(AbsoluteExpiryMinutes),
									TimeSpan.Zero,
									 System.Web.Caching.CacheItemPriority.Default,
									 null);
			}

			return soapTransactions;

		}

		/// <summary>
		/// This method reads in the xml webservices configurations file and build an arraylist 
		/// note the values will be read when they are present in the system's cache
		/// </summary>
		/// <returns>arraylist of webservice objects</returns>
		public static ArrayList RetrieveWebServices()
		{
			//get the configuration filepath
			string webserviceConfigFilePath = HttpRuntime.AppDomainAppPath
											  + HelperClass.GetConfigSetting("WebserviceConfigFileName");

			ArrayList webServicesObjectArray = HelperClass.GetParameters(HelperClass.ReadXmlFromFile(webserviceConfigFilePath), "webservice", false, string.Empty);
			ArrayList webServicesArray = new ArrayList();

			//build the webservice object arrays
			foreach( object[] webServiceParams in webServicesObjectArray)
			{
				Webservice webService = new Webservice();

				//fill in the webservice properties
				webService.Name	= webServiceParams[0].ToString();					
				if (webServiceParams[1].ToString().Length > 0)
					webService.SoapHeaderPath = HttpRuntime.AppDomainAppPath + webServiceParams[1].ToString();
				else
					webService.SoapHeaderPath = string.Empty;

				webService.Uri = webServiceParams[2].ToString();

				//now get the methods in the webservice
				ArrayList webServicesMethodsObjectArray = HelperClass.GetParameters(webServiceParams[3].ToString(), "method", false, string.Empty);
				WebServiceMethod[] webServicesMethodsArray = new WebServiceMethod[webServicesMethodsObjectArray.Count];
				int count = 0;

				//build the webservuce method arrays
				foreach( object[] webServiceMethodParams in webServicesMethodsObjectArray)
				{
					WebServiceMethod webServiceMethod = new WebServiceMethod();

					//fill in the webserviceMethod properties
					webServiceMethod.Name = webServiceMethodParams[0].ToString();						
					webServiceMethod.SoapAction = webServiceMethodParams[1].ToString();
					webServiceMethod.MethodNamespace = webServiceMethodParams[2].ToString();
					webServiceMethod.OutputPage = webServiceMethodParams[3].ToString();
					webServiceMethod.IsAsync = Convert.ToBoolean(webServiceMethodParams[4].ToString(), HelperClass.Provider);						
					webServiceMethod.XsltPath = webServiceMethodParams[5].ToString();
					

					webServicesMethodsArray[count] = webServiceMethod;

					count++;
				}

				//add the method array to the webservice
				webService.WebServiceMethods = webServicesMethodsArray;

				//add the webservice to the array
				webServicesArray.Add(webService);
				
			}

		
		
			return webServicesArray;

			
		}
		
	}
}
