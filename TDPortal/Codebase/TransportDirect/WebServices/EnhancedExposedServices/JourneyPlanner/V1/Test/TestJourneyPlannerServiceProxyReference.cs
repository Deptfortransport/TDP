// *********************************************** 
// NAME                 : TestJourneyPlannerServiceProxyReference.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 23/01/2006
// DESCRIPTION  		: Proxy test class for JourneyPlannerService.asmx. Generated using Visual Studio.
//                        Original class name modified and "Test..." prefix added.
//                        Note that non-WSE proxy class has been removed.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlanner/V1/Test/TestJourneyPlannerServiceProxyReference.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:07:30   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:04   mturner
//Initial revision.
//
//   Rev 1.1   Feb 22 2006 10:12:54   mdambrine
//fixed the unit tests after namespace change
//
//   Rev 1.0   Jan 27 2006 16:31:06   COwczarek
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
namespace TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test 
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
    [System.Web.Services.WebServiceBindingAttribute(Name="JourneyPlannerServiceSoap", Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")]
    public class TestJourneyPlannerServiceProxyReference : Microsoft.Web.Services3.WebServicesClientProtocol 
    {
        
        /// <remarks/>
        public TestJourneyPlannerServiceProxyReference() 
        {
            this.Url = "http://localhost/EnhancedExposedServices/JourneyPlanner/V1/JourneyPlannerService.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1/PlanPublicJourney", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void PlanPublicJourney(string transactionId, string language, PublicJourneyRequest request) 
        {
            this.Invoke("PlanPublicJourney", new object[] {
                                                              transactionId,
                                                              language,
                                                              request});
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
        public void EndPlanPublicJourney(System.IAsyncResult asyncResult) 
        {
            this.EndInvoke(asyncResult);
        }
    }
        
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")]
    public class OSGridReference 
    {
        
        /// <remarks/>
        public int Easting;
        
        /// <remarks/>
        public int Northing;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.JourneyPlanner.V1")]
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