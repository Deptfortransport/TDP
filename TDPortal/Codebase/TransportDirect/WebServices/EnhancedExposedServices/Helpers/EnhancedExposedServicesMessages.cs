// *********************************************** 
// NAME                 : EnhancedExposedServicesMessages.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 24/11/2005 
// DESCRIPTION  		: Definition of all messages used withing the  EnhancedExposedServices.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Helpers/EnhancedExposedServicesMessages.cs-arc  $ 
//
//   Rev 1.2   Sep 29 2010 11:27:56   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.1   Aug 04 2009 14:29:08   mmodi
//Updated for Car journey planner exposed service
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 13:52:00   mturner
//Initial revision.
//
//   Rev 1.13   Apr 11 2006 11:55:24   build
//Automatically merged from branch for stream0036
//
//   Rev 1.12.1.0   Apr 07 2006 12:09:04   asinclair
//Add string constant for open journey planner service error
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.13   Apr 06 2006 15:24:00   COwczarek
//Add string constant for open journey planner service error
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.12   Jan 23 2006 19:33:34   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.11   Jan 19 2006 14:51:28   RWilby
//Corrected FindNearestServiceError constant to public scope
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.10   Jan 19 2006 14:47:04   COwczarek
//Added JourneyPlannerServiceError constant
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Jan 19 2006 09:49:44   RWilby
//Added FindNearestServiceError constant
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.8   Jan 17 2006 15:15:34   schand
//Added message for Taxi info
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.7   Jan 13 2006 15:24:04   schand
//Added message for travel news and code service
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.6   Jan 12 2006 20:03:22   schand
//Added more messages for travel news
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Jan 12 2006 18:12:56   mtillett
//Update MessageValidation to ensure that the SOAP response is validated aginast the WSDL in schema folder
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Jan 10 2006 17:48:00   schand
//added message for incorrect servicetype or operation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 05 2006 14:52:46   mtillett
//Add new error message for invalid language
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Jan 04 2006 12:37:48   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Dec 22 2005 11:24:30   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Nov 25 2005 18:46:48   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.Helpers
{
	/// <summary>
	///  Message class for Enhanced ExposedServices.
	/// </summary>
	public sealed class EnhancedExposedServicesMessages
	{
		// Message for WSDL validation failure
		public const string WSDLRequestValidationError = "The SOAP request message is invalid against the WSDL specification for this service."; 
		// Message for WSDL validation failure
		public const string WSDLResponseValidationError = "The SOAP response message is invalid against the WSDL specification for this service. The response has not been sent."; 
		// Message if Soap Context is not found
		public const string SoapContextRequired = "Soap context is required for this web service."; 
		// Message for not support language request
		public const string LanguageNotSupported = "The supplied language is incorrect or not supported."; 
		// Message for general departure error
		public const string DepartureBoardEnquiryError = "Departure Board Service encountered a problem while processing your request."; 
		// Message for general travel news error
		public const string TravelNewsEnquiryError = "Travel Service encountered a problem while processing your request."; 		
		// Message for general code service error
		public const string CodeServiceEnquiryError = "Code Service encountered a problem while processing your request."; 		
		// Message for general taxi information error
		public const string TaxiInformationEnquiryError = "Taxi Information Service encountered a problem while processing your request."; 		
        // Message for general journey planner service error
        public const string JourneyPlannerServiceError = "Journey Planner Service encountered a problem while processing your request."; 		
        // Message for general open journey planner service error
        public const string OpenJourneyPlannerServiceError = "Open Journey Planner Service encountered a problem while processing your request.";
        // Message for general car journey planner service error
        public const string CarJourneyPlannerServiceError = "Car Journey Planner Service encountered a problem while processing your request."; 		

		// Message for invalid servicetype or operation type
		public const string InvalidServiceOrOperation = "Invalid service or operation has been passed."; 
        // Message for invalid travel news region 
		public const string InvalidTravelNewsRegion = "Region Requested is not a standard region defined in EnhancedExposedServices"; 
		// Message for travel news invalid request
		public const string TravelNewsServiceInvalidRequest = "Invalid Travel news service request has been passed."; 
		// Message for code service invalid request
		public const string CodeServiceInvalidRequest = "Invalid code service request has been passed."; 
		// Message for invalid taxi information request
		public const string TaxiInformationInvalidRequest = "Invalid request or no naptan id has been passed."; 
		// Message for general FindNearest service error
		public const string FindNearestServiceError = "Find Nearest Service encountered a problem while processing your request.";

        // Message for general cycle journey planner service error
        public const string CycleJourneyPlannerServiceError = "Cycle Journey Planner Service encountered a problem while processing your request.";

        // Message for general gradient profile service error
        public const string GradientProfileServiceError = "Gradient Profile Service encountered a problem while processing your request.";

	}
}
