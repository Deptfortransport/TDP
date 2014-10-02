// *********************************************** 
// NAME                 : TestOpenJourneyPlannerServiceProxyReference.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 05.04.2006
// DESCRIPTION  		: Proxy test class for OpenJourneyPlannerService.asmx. Generated using Visual Studio.
//                        Original class name modified and "Test..." prefix added.
//                        Note that non-WSE proxy class has been removed.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/OpenJourneyPlanner/V1/Test/TestOpenJourneyPlannerServiceProxyReference.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:16:40   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:18   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:15:24   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
namespace TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1.Test 
{
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OpenJourneyPlannerServiceSoap", Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class TestOpenJourneyPlannerServiceProxyReference : Microsoft.Web.Services3.WebServicesClientProtocol 
    {
        
        /// <remarks/>
        public TestOpenJourneyPlannerServiceProxyReference() 
        {
            this.Url = "http://localhost/EnhancedExposedServices/OpenJourneyPlanner/V1/OpenJourneyPlanner" +
                "Service.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
             "eyPlanner.V1/PlanJourney", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
             "eyPlanner.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
             "eyPlanner.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Result PlanJourney(string transactionId, string language, Request request) 
        {
            object[] results = this.Invoke("PlanJourney", new object[] {
                                                                           transactionId,
                                                                           language,
                                                                           request});
            return ((Result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginPlanJourney(string transactionId, string language, Request request, System.AsyncCallback callback, object asyncState) 
        {
            return this.BeginInvoke("PlanJourney", new object[] {
                                                                    transactionId,
                                                                    language,
                                                                    request}, callback, asyncState);
        }
        
        /// <remarks/>
        public Result EndPlanJourney(System.IAsyncResult asyncResult) 
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Result)(results[0]));
        }
    }
        
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Request 
    {
        
        /// <remarks/>
        public TransportModes ModeFilter;
        
        /// <remarks/>
        public Operators OperatorFilter;
        
        /// <remarks/>
        public ServiceFilter ServiceFilter;
        
        /// <remarks/>
        public bool Depart;
        
        /// <remarks/>
        public RequestPlace Destination;
        
        /// <remarks/>
        public RequestPlace Origin;
        
        /// <remarks/>
        public PublicParameters PublicParameters;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class TransportModes 
    {
        
        /// <remarks/>
        public bool Include;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public TransportMode[] Modes;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class TransportMode 
    {
        
        /// <remarks/>
        public TransportModeType Mode;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum TransportModeType 
    {
        
        /// <remarks/>
        Air,
        
        /// <remarks/>
        Bus,
        
        /// <remarks/>
        Car,
        
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
        Underground,
        
        /// <remarks/>
        Walk,
        
        /// <remarks/>
        CheckIn,
        
        /// <remarks/>
        CheckOut,
        
        /// <remarks/>
        Transfer,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class WindowOfOpportunity 
    {
        
        /// <remarks/>
        public System.DateTime End;
        
        /// <remarks/>
        public System.DateTime Start;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Service 
    {
        
        /// <remarks/>
        public Days[] DaysOfOperation;
        
        /// <remarks/>
        public string DestinationBoard;
        
        /// <remarks/>
        public System.DateTime ExpiryDate;
        
        /// <remarks/>
        public System.DateTime FirstDateOfOperation;
        
        /// <remarks/>
        public bool OpenEnded;
        
        /// <remarks/>
        public string OperatorCode;
        
        /// <remarks/>
        public string OperatorName;
        
        /// <remarks/>
        public string PrivateID;
        
        /// <remarks/>
        public string RetailId;
        
        /// <remarks/>
        public string ServiceNumber;
        
        /// <remarks/>
        public string TimetableLinkURL;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum Days 
    {
        
        /// <remarks/>
        Monday,
        
        /// <remarks/>
        Tuesday,
        
        /// <remarks/>
        Wednesday,
        
        /// <remarks/>
        Thursday,
        
        /// <remarks/>
        Friday,
        
        /// <remarks/>
        Saturday,
        
        /// <remarks/>
        Sunday,
        
        /// <remarks/>
        MondayToFriday,
        
        /// <remarks/>
        MondayToSaturday,
        
        /// <remarks/>
        BankHoliday,
        
        /// <remarks/>
        NotBankHoliday,
        
        /// <remarks/>
        SchoolHoliday,
        
        /// <remarks/>
        NotSchoolHoliday,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Stop 
    {
        
        /// <remarks/>
        public string Bay;
        
        /// <remarks/>
        public Coordinate Coordinate;
        
        /// <remarks/>
        public string Name;
        
        /// <remarks/>
        public string NaPTANID;
        
        /// <remarks/>
        public bool TimingPoint;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Coordinate 
    {
        
        /// <remarks/>
        public int Easting;
        
        /// <remarks/>
        public int Northing;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Event 
    {
        
        /// <remarks/>
        public ActivityType Activity;
        
        /// <remarks/>
        public System.DateTime ArriveTime;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ArriveTimeSpecified;
        
        /// <remarks/>
        public bool ConfirmedVia;
        
        /// <remarks/>
        public System.DateTime DepartTime;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DepartTimeSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Coordinate[] Geometry;
        
        /// <remarks/>
        public Stop Stop;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum ActivityType 
    {
        
        /// <remarks/>
        Arrive,
        
        /// <remarks/>
        Depart,
        
        /// <remarks/>
        ArriveDepart,
        
        /// <remarks/>
        Frequency,
        
        /// <remarks/>
        Request,
        
        /// <remarks/>
        Pass,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Leg 
    {
        
        /// <remarks/>
        public Event Alight;
        
        /// <remarks/>
        public Event Board;
        
        /// <remarks/>
        public string Description;
        
        /// <remarks/>
        public Event Destination;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Event[] IntermediatesA;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Event[] IntermediatesB;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Event[] IntermediatesC;
        
        /// <remarks/>
        public TransportModeType Mode;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public string[] Notes;
        
        /// <remarks/>
        public Event Origin;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Service[] Services;
        
        /// <remarks/>
        public bool Validated;
        
        /// <remarks/>
        public int[] VehicleFeatures;
        
        /// <remarks/>
        public WindowOfOpportunity WindowOfOpportunity;
        
        /// <remarks/>
        public int TypicalDuration;
        
        /// <remarks/>
        public int Frequency;
        
        /// <remarks/>
        public int MaxDuration;
        
        /// <remarks/>
        public int MaxFrequency;
        
        /// <remarks/>
        public int MinFrequency;
        
        /// <remarks/>
        public LegType Type;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum LegType 
    {
        
        /// <remarks/>
        TimedLeg,
        
        /// <remarks/>
        FrequencyLeg,
        
        /// <remarks/>
        ContinuousLeg,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class PublicJourney 
    {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Leg[] Legs;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Message 
    {
        
        /// <remarks/>
        public int Code;
        
        /// <remarks/>
        public string Description;
        
        /// <remarks/>
        public int SubCode;
        
        /// <remarks/>
        public string Region;
        
        /// <remarks/>
        public int SubClass;
        
        /// <remarks/>
        public MessageType Type;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum MessageType 
    {
        
        /// <remarks/>
        JourneyPlannerMessage,
        
        /// <remarks/>
        RailEngineMessage,
        
        /// <remarks/>
        JourneyWebMessage,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Result 
    {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Message[] Messages;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public PublicJourney[] PublicJourneys;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class PublicParameters 
    {
        
        /// <remarks/>
        public PublicAlgorithmType Algorithm;
        
        /// <remarks/>
        public System.DateTime ExtraCheckInTime;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExtraCheckInTimeSpecified;
        
        /// <remarks/>
        public int InterchangeSpeed;
        
        /// <remarks/>
        public IntermediateStopsType IntermediateStops;
        
        /// <remarks/>
        public System.DateTime Interval;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IntervalSpecified;
        
        /// <remarks/>
        public int MaxWalkDistance;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestPlace[] NotVias;
        
        /// <remarks/>
        public RangeType RangeType;
        
        /// <remarks/>
        public int Sequence;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SequenceSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestPlace[] SoftVias;
        
        /// <remarks/>
        public bool TrunkPlan;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestPlace[] Vias;
        
        /// <remarks/>
        public int WalkSpeed;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum PublicAlgorithmType 
    {
        
        /// <remarks/>
        Default,
        
        /// <remarks/>
        Fastest,
        
        /// <remarks/>
        NoChanges,
        
        /// <remarks/>
        Max1Change,
        
        /// <remarks/>
        Max2Changes,
        
        /// <remarks/>
        Max3Changes,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum IntermediateStopsType 
    {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Before,
        
        /// <remarks/>
        BeforeAndLeg,
        
        /// <remarks/>
        Leg,
        
        /// <remarks/>
        LegAndAfter,
        
        /// <remarks/>
        After,
        
        /// <remarks/>
        All,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class RequestPlace 
    {
        
        /// <remarks/>
        public Coordinate Coordinate;
        
        /// <remarks/>
        public string GivenName;
        
        /// <remarks/>
        public string Locality;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestStop[] Stops;
        
        /// <remarks/>
        public RequestPlaceType Type;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class RequestStop 
    {
        
        /// <remarks/>
        public Coordinate Coordinate;
        
        /// <remarks/>
        public string NaPTANID;
        
        /// <remarks/>
        public System.DateTime TimeDate;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum RequestPlaceType 
    {
        
        /// <remarks/>
        NaPTAN,
        
        /// <remarks/>
        Coordinate,
        
        /// <remarks/>
        Locality,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum RangeType 
    {
        
        /// <remarks/>
        Sequence,
        
        /// <remarks/>
        Interval,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class RequestService 
    {
        
        /// <remarks/>
        public string OperatorCode;
        
        /// <remarks/>
        public string ServiceNumber;
        
        /// <remarks/>
        public string PrivateID;
        
        /// <remarks/>
        public RequestServiceType Type;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public enum RequestServiceType 
    {
        
        /// <remarks/>
        RequestServicePrivate,
        
        /// <remarks/>
        RequestServiceNumber,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class ServiceFilter 
    {
        
        /// <remarks/>
        public bool Include;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public RequestService[] Services;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class OperatorCode 
    {
        
        /// <remarks/>
        public string Code;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.OpenJourn" +
         "eyPlanner.V1")]
    public class Operators 
    {
        
        /// <remarks/>
        public bool Include;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public OperatorCode[] OperatorCodes;
    }
}