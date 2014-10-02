// *********************************************** 
// NAME                 : DepartureBoardService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 02/12/2005 
// DESCRIPTION  		: Departure board Web Service Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/DepartureBoard/V1/DepartureBoardService.asmx.cs-arc  $ 
//
//   Rev 1.5   Mar 22 2013 16:40:52   rbroddle
//Updated to convert incorrect MDV operator codes for service info requests
//Resolution for 5906: MDV Issue - Stop Events Not Being Displayed in MDV Regions Except London
//
//   Rev 1.4   Feb 24 2010 14:37:54   MTurner
//Changes to allow TI to call this service.
//Resolution for 5411: EES Security improvements led to TI injections failing
//
//   Rev 1.3   Feb 18 2010 12:37:58   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.2   Feb 11 2010 14:52:02   MTurner
//Updated new exceptions to be of type System.Exception.  As this results in them being displayed clearly to the caller.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.1   Feb 10 2010 16:28:18   MTurner
//Fixed vulnerability that was allowing callers to bypass the username/password checking by not supplying a security token.
//Resolution for 5400: EES security not preventing empty security token
//
//   Rev 1.0   Nov 08 2007 13:51:54   mturner
//Initial revision.
//
//   Rev 1.10   Jul 02 2007 15:20:56   mturner
//Change for counting buses
//
//   Rev 1.9   Feb 21 2006 16:20:30   RWilby
//Added TransportDirect domain to web service namespace.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.8   Jan 20 2006 19:40:32   schand
//Updated schema location
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.7   Jan 17 2006 15:33:56   schand
//Using correct assembler method now
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.6   Jan 12 2006 20:02:52   schand
//completed version
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Jan 11 2006 09:17:12   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Jan 05 2006 14:54:04   mtillett
//Fix up SoapException creation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 04 2006 12:37:48   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Dec 22 2005 17:24:40   halkatib
//Applied changes reqested by Chris O
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Dec 22 2005 11:25:50   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Dec 14 2005 16:14:06   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements


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
using TransportDirect.Common.ServiceDiscovery;   
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;   
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.EnhancedExposedServices.Common;      
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1;
using TransportDirect.EnhancedExposedServices.Helpers;      
using TransportDirect.EnhancedExposedServices.SoapFault.V1;        
using TransportDirect.EnhancedExposedServices.Validation;  
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
#endregion

namespace TransportDirect.EnhancedExposedServices.DepartureBoard.V1
{
	/// <summary>
	/// This service will be responsible for providing departure board information.
	/// </summary>
	[WebService(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.DepartureBoard.V1"), 
	XmlInclude(typeof(DepartureBoardServiceRequest)),
	XmlInclude(typeof(DepartureBoardServiceTrainRealTime)),
	XmlInclude(typeof(DepartureBoardServiceStopInformation)), 
	XmlInclude(typeof(DepartureBoardServiceCallingStopStatus)),
	XmlInclude(typeof(DepartureBoardServiceInformation)), 
	XmlInclude(typeof(DepartureBoardServiceItinerary)),
	ValidationSchemaCache("./schemas")]	
	public class DepartureBoardService : TDEnhancedExposedWebService
	{
		#region Constructor
		public DepartureBoardService()
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
		///  DepartureBoard Enquiry Request Web Service method 
		/// </summary>
		/// <param name="transactionId">A transaction Id provided by the client</param>
		/// <param name="language">The language passed by the client</param>
		/// <param name="departureBoardRequest">Departure Board Service request parameter which contains like origin, destination, time etc.</param>
		/// <returns>An array of DepartureBoardServiceStopInformation</returns>
		[WebMethod(EnableSession=false), Validation] 
		public DepartureBoardServiceStopInformation[] GetDepartureBoard(string transactionId, string language, DepartureBoardServiceRequest departureBoardRequest)
		{
			try
			{
                bool OKToProceed = false;
                if (String.Compare(transactionId, "TransactionInjector", true) != 0)
                {
                    int tokenCount = Microsoft.Web.Services3.RequestSoapContext.Current.Security.Tokens.Count;
                    if (tokenCount < 1)
                    {
                        OKToProceed = false;
                        Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                        throw e;
                    }
                    else
                    {
                        OKToProceed = true;
                    }
                }
                else
                {
                    //this is a TWS request from the local machine so should proceed
                    OKToProceed = true;
                }

                if (OKToProceed)
                {
                    if (String.Compare(transactionId, "CountingBuses", true) != 0)
                    {
                        CreateExposedServiceContext(transactionId, language);
                    }
                    // logging the start event
                    LogStartEvent(true);

                    // Getting departure Infterface
                    IDepartureBoardService dbs = (IDepartureBoardService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DepartureBoardService];

                    IOperatorCatalogue operatorCatalogue = OperatorCatalogue.Current;

                    // Converting External Object to Domain object using data translation assembler
                    DepartureBoardServiceInternalRequest internalRequest = DepartureBoardAssembler.CreateDepartureBoardServiceInternalRequest(departureBoardRequest);

                    // Gettting the information for the internal component
                    DBSResult dbsResult = new DBSResult();
                    dbsResult = dbs.GetDepartureBoardTrip
                        (transactionId,
                        internalRequest.OriginLocation,
                        internalRequest.DestinationLocation,
                        operatorCatalogue.TranslateOperator(internalRequest.OperatorCode),
                        internalRequest.ServiceNumber,
                        internalRequest.JourneyTimeInformation,
                        internalRequest.RangeType,
                        internalRequest.Range,
                        internalRequest.ShowDepartures,
                        internalRequest.ShowCallingStops
                        );

                    // Check if any error meesage exists, if yes then throw soap exeption
                    if (dbsResult.Messages.Length > 0)
                    {
                        // If the code indicates its only no trip data (i.e. no services exist for the station),
                        // then do not throw an exception
                        if (dbsResult.Messages[0].Code != RTTIUtilities.GetErrorCodeForMessage(UserPortal.DepartureBoardService.Messages.RTTIUnableToExtractTripData))
                        {
                            throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.DepartureBoardEnquiryError, dbsResult.Messages);
                        }
                    }

                    // Converting Domain object to External Object using data translation assembler			
                    DepartureBoardServiceStopInformation[] result = DepartureBoardAssembler.CreateDepartureBoardServiceStopInformationArrayDT(dbsResult.StopEvents);

                    // log the finish event 		
                    LogFinishEvent(true);

                    return result;
                }
                else
                {
                    //should never go here but all code paths need to return a value
                    Exception e = new Exception("This is a restricted web service, please provide a valid username and password");
                    throw e;
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
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.DepartureBoardEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.DepartureBoardEnquiryError, exception);
			}
		}

		/// <summary>
		/// Deparure Board Web Method to return DepartureBoardServiceTimeRequestTypes
		/// </summary>
		/// <param name="transactionId">A transaction Id provided by the client</param>
		/// <param name="language">The language passed by the client</param>
		/// <returns>An array of DepartureBoardServiceTimeRequestType</returns>
		[WebMethod(EnableSession=false), Validation]
		public DepartureBoardServiceTimeRequestType[] GetDepartureBoardTimeRequestTypes(string transactionId, string language)
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

                    // getting the types from the dbs component 
                    IDepartureBoardService dbs = (IDepartureBoardService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DepartureBoardService];
                    TimeRequestType[] timeRequestType = dbs.GetTimeRequestTypesToDisplay();

                    DepartureBoardServiceTimeRequestType[] timeRequestTypes = DepartureBoardAssembler.CreateDepartureBoardServiceTimeRequestTypeArrayDT(timeRequestType);

                    // log the finish event 		
                    LogFinishEvent(true);

                    return timeRequestTypes;
                }
			}
			catch(TDException tdException)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(tdException.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.DepartureBoardEnquiryError, tdException);
			}
			catch(Exception exception)
			{
				// log the finish event 		
				LogFinishEvent(false);     
				// log the error 
				LogError(exception.Message);
				// wrap the error and throw the exception
				throw SoapExceptionHelper.ThrowSoapException(EnhancedExposedServicesMessages.DepartureBoardEnquiryError, exception);
			}
		}
		#endregion
	}
}
