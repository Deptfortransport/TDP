// *********************************************** 
// NAME                 : TestJourneyPlannerSyncServiceProxyReference.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 23/01/2006
// DESCRIPTION  		: Proxy test class for JourneyPlannerSynchronousService.asmx. Generated using Visual Studio.
//                        Original Class name modified and "Test..." prefix added.
//                        Note that non-WSE proxy class has been removed.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlannerSynchronous/V1/Test/TestJourneyPlannerSyncServiceProxyReference.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:14:00   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:08   mturner
//Initial revision.
//
//   Rev 1.1   Feb 22 2006 10:12:54   mdambrine
//fixed the unit tests after namespace change
//
//   Rev 1.0   Jan 27 2006 16:31:44   COwczarek
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//

namespace TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1.Test
{
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    // CODEGEN: The optional WSDL extension element 'validation' from namespace 'http://www.develop.com/web/services/' was not handled.
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="JourneyPlannerSynchronousServiceSoap", Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class TestJourneyPlannerSyncServiceProxyReference : Microsoft.Web.Services3.WebServicesClientProtocol 
    {
        
        /// <remarks/>
        public TestJourneyPlannerSyncServiceProxyReference() 
        {
            this.Url = "http://localhost/EnhancedExposedServices/JourneyPlannerSynchronous" +
                    "/V1/JourneyPlannerSynchronousService.asmx";        
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1/PlanPublicJo" +
             "urney", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PublicJourneyResult PlanPublicJourney(string transactionId, string language, PublicJourneyRequest request) 
        {
            object[] results = this.Invoke("PlanPublicJourney", new object[] {
                                                                                 transactionId,
                                                                                 language,
                                                                                 request});
            return ((PublicJourneyResult)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginPlanPublicJourney(string transactionId, string language, PublicJourneyRequest request, System.AsyncCallback callback, object asyncState) 
        {
            return this.BeginInvoke("PlanPublicJourney", new object[] {
                                                                          transactionId,
                                                                          language,
                                                                          request}, callback, asyncState);
        }
        
        /// <remarks/>
        public PublicJourneyResult EndPlanPublicJourney(System.IAsyncResult asyncResult) 
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PublicJourneyResult)(results[0]));
        }
    }
        
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class PublicJourneyRequest 
    {
        
        /// <remarks/>
        public bool IsReturnRequired;
        
        /// <remarks/>
        public bool OutwardArriveBefore;
        
        /// <remarks/>
        public bool ReturnArriveBefore;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReturnArriveBeforeSpecified;
        
        /// <remarks/>
        public System.DateTime OutwardDateTime;
        
        /// <remarks/>
        public System.DateTime ReturnDateTime;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReturnDateTimeSpecified;
        
        /// <remarks/>
        public int InterchangeSpeed;
        
        /// <remarks/>
        public int WalkingSpeed;
        
        /// <remarks/>
        public int MaxWalkingTime;
        
        /// <remarks/>
        public RequestLocation OriginLocation;
        
        /// <remarks/>
        public RequestLocation DestinationLocation;
        
        /// <remarks/>
        public int Sequence;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class RequestLocation 
    {
        
        /// <remarks/>
        public OSGridReference GridReference;
        
        /// <remarks/>
        public Naptan[] NaPTANs;
        
        /// <remarks/>
        public LocationType Type;
        
        /// <remarks/>
        public string Postcode;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class OSGridReference 
    {
        
        /// <remarks/>
        public int Easting;
        
        /// <remarks/>
        public int Northing;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class JourneySummary 
    {
        
        /// <remarks/>
        public string OriginDescription;
        
        /// <remarks/>
        public string DestinationDescription;
        
        /// <remarks/>
        public ModeType[] Modes;
        
        /// <remarks/>
        public string[] ModesText;
        
        /// <remarks/>
        public int InterchangeCount;
        
        /// <remarks/>
        public System.DateTime DepartureDateTime;
        
        /// <remarks/>
        public System.DateTime ArrivalDateTime;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public enum ModeType 
    {
        
        /// <remarks/>
        Air,
        
        /// <remarks/>
        Bus,
        
        /// <remarks/>
        Car,
        
        /// <remarks/>
        CheckIn,
        
        /// <remarks/>
        CheckOut,
        
        /// <remarks/>
        Coach,
        
        /// <remarks/>
        Cycle,
        
        /// <remarks/>
        Drt,
        
        /// <remarks/>
        Ferry,
        
        /// <remarks/>
        Metro,
        
        /// <remarks/>
        Rail,
        
        /// <remarks/>
        RailReplacementBus,
        
        /// <remarks/>
        Taxi,
        
        /// <remarks/>
        Tram,
        
        /// <remarks/>
        Transfer,
        
        /// <remarks/>
        Underground,
        
        /// <remarks/>
        Walk,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class ServiceDetails 
    {
        
        /// <remarks/>
        public string OperatorCode;
        
        /// <remarks/>
        public string OperatorName;
        
        /// <remarks/>
        public string ServiceNumber;
        
        /// <remarks/>
        public string DestinationBoard;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class ResponseLocation 
    {
        
        /// <remarks/>
        public OSGridReference GridReference;
        
        /// <remarks/>
        public Naptan Naptan;
        
        /// <remarks/>
        public string Description;
        
        /// <remarks/>
        public string Locality;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class Naptan 
    {
        
        /// <remarks/>
        public OSGridReference GridReference;
        
        /// <remarks/>
        public string NaptanId;
        
        /// <remarks/>
        public string Name;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class PublicJourneyCallingPoint 
    {
        
        /// <remarks/>
        public ResponseLocation Location;
        
        /// <remarks/>
        public System.DateTime ArrivalDateTime;
        
        /// <remarks/>
        public System.DateTime DepartureDateTime;
        
        /// <remarks/>
        public PublicJourneyCallingPointType Type;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public enum PublicJourneyCallingPointType 
    {
        
        /// <remarks/>
        Origin,
        
        /// <remarks/>
        Destination,
        
        /// <remarks/>
        Board,
        
        /// <remarks/>
        Alight,
        
        /// <remarks/>
        CallingPoint,
        
        /// <remarks/>
        PassingPoint,
        
        /// <remarks/>
        OriginAndBoard,
        
        /// <remarks/>
        DestinationAndAlight,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class PublicJourneyDetail 
    {
        
        /// <remarks/>
        public LegType Type;
        
        /// <remarks/>
        public ModeType Mode;
        
        /// <remarks/>
        public string ModeText;
        
        /// <remarks/>
        public int Duration;
        
        /// <remarks/>
        public string DurationText;
        
        /// <remarks/>
        public string InstructionText;
        
        /// <remarks/>
        public PublicJourneyCallingPoint LegStart;
        
        /// <remarks/>
        public PublicJourneyCallingPoint LegEnd;
        
        /// <remarks/>
        public PublicJourneyCallingPoint Origin;
        
        /// <remarks/>
        public PublicJourneyCallingPoint Destination;
        
        /// <remarks/>
        public PublicJourneyCallingPoint[] IntermediatesBefore;
        
        /// <remarks/>
        public PublicJourneyCallingPoint[] IntermediatesLeg;
        
        /// <remarks/>
        public PublicJourneyCallingPoint[] IntermediatesAfter;
        
        /// <remarks/>
        public ServiceDetails[] Services;
        
        /// <remarks/>
        public int[] VehicleFeatures;
        
        /// <remarks/>
        public string[] VehicleFeaturesText;
        
        /// <remarks/>
        public string[] DisplayNotes;
        
        /// <remarks/>
        public int MinFrequency;
        
        /// <remarks/>
        public int MaxFrequency;
        
        /// <remarks/>
        public string FrequencyText;
        
        /// <remarks/>
        public int MaxDuration;
        
        /// <remarks/>
        public string MaxDurationText;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public enum LegType 
    {
        
        /// <remarks/>
        Timed,
        
        /// <remarks/>
        Frequency,
        
        /// <remarks/>
        Continuous,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class PublicJourney 
    {
        
        /// <remarks/>
        public PublicJourneyDetail[] Details;
        
        /// <remarks/>
        public JourneySummary Summary;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public class PublicJourneyResult 
    {
        
        /// <remarks/>
        public string[] UserWarnings;
        
        /// <remarks/>
        public PublicJourney[] OutwardPublicJourneys;
        
        /// <remarks/>
        public PublicJourney[] ReturnPublicJourneys;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1")]
    public enum LocationType 
    {
        
        /// <remarks/>
        Coordinate,
        
        /// <remarks/>
        Locality,
        
        /// <remarks/>
        NaPTAN,
        
        /// <remarks/>
        Postcode,
    }
}
