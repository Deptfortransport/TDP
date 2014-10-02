// *********************************************** 
// NAME                 : TravelNewsService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 12/01/2006 
// DESCRIPTION  		: Travel News Web Service Class 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/TravelNews/V1/TravelNewsService.asmx.cs-arc  $ 
//
//   Rev 1.2   Feb 11 2010 14:51:56   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:14   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.0   Nov 08 2007 13:52:28   mturner
//Initial revision.
//
//   Rev 1.6   Apr 10 2006 11:25:22   mtillett
//Add note for clarification of requirements
//Resolution for 3810: Mobile: Travel News Service for South East errors
//
//   Rev 1.5   Feb 21 2006 15:49:20   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Feb 21 2006 10:13:58   mguney
//ITravelNewsHandler is used when getting the travel news handler so that it can be stubbed out.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 23 2006 19:33:38   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 19:40:34   schand
//Updated schema location
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 17 2006 15:17:36   schand
//Added comments and validation
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


#region Used Namespaces
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text; 
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using TransportDirect.Common;
using TransportDirect.UserPortal.DataServices;     
using TransportDirect.EnhancedExposedServices.Common;      
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1; 
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface;
using Logger = System.Diagnostics.Trace;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.SoapFault.V1; 
using TransportDirect.Common.Logging;
using TransportDirect.EnhancedExposedServices.Validation; 
#endregion 


namespace TransportDirect.EnhancedExposedServices.TravelNews.V1
{
	/// <summary>
	/// This web service will be resposible for provding travel news related information.
	/// </summary> 	
	[WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.TravelNews.V1"), 
	XmlInclude(typeof(TravelNewsServiceNewsItem)), XmlInclude(typeof(TravelNewsServiceHeadlineItem)),
	XmlInclude(typeof(TravelNewsServiceRequest)),
	ValidationSchemaCache("./schemas")]	
	public class TravelNewsService : TDEnhancedExposedWebService
	{
		#region Constructor
		public TravelNewsService()
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
		/// <summary>
		///  Web method for returning available Travel news details according to filtering requirements
		/// </summary>
		/// <param name="transactionId">A transaction Id provided by the client</param>
		/// <param name="language">The language passed by the client</param>
		/// <param name="travelNewsRequest">Travel news request parameter which contains request for region or transport type or delay type</param>
		/// <returns>TravelNewsServiceNewsItem response object</returns>
		[WebMethod(EnableSession=false), Validation]
		public TravelNewsServiceNewsItem[] GetTravelNewsDetails
			(string transactionId, string language,TravelNewsServiceRequest travelNewsRequest)
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

                    if (!IsRegionValid(travelNewsRequest.Region))
                    {
                        // throw SoapException									
                        throw new TDException(EnhancedExposedServicesMessages.InvalidTravelNewsRegion, true, TDExceptionIdentifier.EESTravelNewsServiceInValidRegion);
                    }

                    if (travelNewsRequest == null)
                        throw new TDException(EnhancedExposedServicesMessages.InvalidTravelNewsRegion, true, TDExceptionIdentifier.EESTravelNewsServiceInValidRequest);

                    // Getting travel news Infterface
                    ITravelNewsHandler travelNewsHandler =
                        (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];
                    TravelNewsState state = new TravelNewsState();

                    state.SelectedTransport = TravelNewsAssembler.CreateTransportType(travelNewsRequest.TransportType);
                    state.SelectedDelays = TravelNewsAssembler.CreateDelayType(travelNewsRequest.DelayType);
                    state.SelectedRegion = travelNewsRequest.Region;

                    TravelNewsItem[] items = travelNewsHandler.GetDetails(state);
                    TravelNewsServiceNewsItem[] travelNewsServiceNewsItems = TravelNewsAssembler.CreateTravelNewsServiceNewsItemArrayDT(items);

                    // log the finish event 		
                    LogFinishEvent(true);
                    return travelNewsServiceNewsItems;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, exception);
			}

		}


		/// <summary>
		/// Web method for returning filtered headlines items using the given filtering parameters.
		/// </summary>
		/// <param name="transactionId">A transaction Id provided by the client</param>
		/// <param name="language">The language passed by the client</param>
		/// <param name="travelNewsRequest">Travel News service Request object which contains filtering requirement for region or transport type or delay type </param>
		/// <returns>TravelNewsServiceHeadlineItem response object</returns>
		[WebMethod(EnableSession=false), Validation]
		public TravelNewsServiceHeadlineItem[]  GetTravelNewsHeadlines
			(string transactionId,string language,TravelNewsServiceRequest travelNewsRequest)
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


                    if (!IsRegionValid(travelNewsRequest.Region))
                    {
                        // throw SoapException									
                        throw new TDException(EnhancedExposedServicesMessages.InvalidTravelNewsRegion, true, TDExceptionIdentifier.EESTravelNewsServiceInValidRegion);
                    }

                    if (travelNewsRequest == null)
                        throw new TDException(EnhancedExposedServicesMessages.InvalidTravelNewsRegion, true, TDExceptionIdentifier.EESTravelNewsServiceInValidRequest);

                    // Getting travel news Infterface
                    ITravelNewsHandler travelNewsHandler =
                        (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];
                    TravelNewsState state = new TravelNewsState();

                    state.SelectedTransport = TravelNewsAssembler.CreateTransportType(travelNewsRequest.TransportType);
                    state.SelectedDelays = TravelNewsAssembler.CreateDelayType(travelNewsRequest.DelayType);
                    state.SelectedRegion = travelNewsRequest.Region;

                    //Use overloaded GetHeadlines method to ensure critical indicents NOT returned
                    HeadlineItem[] headlineItems = travelNewsHandler.GetHeadlines(state);
                    TravelNewsServiceHeadlineItem[] travelNewsServiceHeadlineItems = TravelNewsAssembler.CreateTravelNewsServiceHeadlineItemArrayDT(headlineItems);

                    // log the finish event 		
                    LogFinishEvent(true);
                    return travelNewsServiceHeadlineItems;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, exception);
			}

		}

		
		
		[WebMethod(EnableSession=false), Validation]
		public TravelNewsServiceNewsItem GetTravelNewsDetailsByUid
			(string transactionId,string language, string uid)
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

                    // Getting travel news Infterface				
                    ITravelNewsHandler travelNewsHandler =
                        (ITravelNewsHandler)TDServiceDiscovery.Current[ServiceDiscoveryKey.TravelNews];
                    TravelNewsItem item = travelNewsHandler.GetDetailsByUid(uid);
                    TravelNewsServiceNewsItem travelNewsServiceNewsItem = TravelNewsAssembler.CreateTravelNewsServiceNewsItemDT(item);

                    // log the finish event 		
                    LogFinishEvent(true);
                    return travelNewsServiceNewsItem;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, exception);
			}

		}

		
		[WebMethod(EnableSession=false), Validation]
		public string[] GetTravelNewsAvailableRegions(string transactionId, string language)
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

                    DataServices ds = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

                    ArrayList list = ds.GetList(DataServiceType.NewsRegionDrop);
                    string[] regions = new string[list.Count];
                    for (int i = 0; i < list.Count; i++)
                    {
                        regions[i] = ((DSDropItem)list[i]).ResourceID;
                    }

                    // log the finish event 		
                    LogFinishEvent(true);
                    return regions;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, exception);
			}

		}

		
		[WebMethod(EnableSession=false), Validation]
		public string GetTravelNewsRegion(string transactionId, string language,string travelLineRegion)
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

                    DataServices ds = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
                    string travelNewsRegion = ds.GetValue(DataServiceType.Lookup_Travelline_TravelNews_Region, travelLineRegion);
                    // log the finish event 		
                    LogFinishEvent(true);
                    return travelNewsRegion;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.TravelNewsEnquiryError, exception);
			}
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Uses the DataServices to determin if region param is valid according to the regions entries stored in the DB. Accepts also empty string.
		/// </summary>
		/// <param name="region">given region to validate</param>
		/// <returns>true if region valid or empty, false otherwise</returns>
		private static bool IsRegionValid(string region)
		{
			
			DataServices ds = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			
			if (	region.Length == 0
				||
				ds.GetValue(DataServiceType.NewsRegionDrop, region).Length > 0) // region matches one of DS entries
				return true;
			else
				return false;
		}

		#endregion
	}
}




