// *********************************************** 
// NAME                 : TestJourneyPlannerSyncServiceProxyReference.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 03/08/2009
// DESCRIPTION  		: Proxy test class for JourneyPlannerSynchronousService.asmx. Generated using Visual Studio.
//                        Original Class name modified and "Test..." prefix added.
//                        Note that non-WSE proxy class has been removed.
//                      : wsdl /out:myProxyClass.cs http://localhost/enhancedexposedservices/CarJourneyPlannerSynchronous/v1/CarJourneyPlannerSynchronousService.asmx?WSDL
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CarJourneyPlannerSynchronous/V1/Test/TestCarJourneyPlannerSyncServiceProxyReference.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 15:02:20   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace TransportDirect.EnhancedExposedServices.CarJourneyPlannerSynchronous.V1.Test
{
    /// <remarks/>
    // CODEGEN: The optional WSDL extension element 'validation' from namespace 'http://www.develop.com/web/services/' was not handled.
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "CarJourneyPlannerSynchronousServiceSoap", Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
    "yPlannerSynchronous.V1")]
    public class TestCarJourneyPlannerSyncServiceProxyReference : Microsoft.Web.Services3.WebServicesClientProtocol
    {
        private System.Threading.SendOrPostCallback PlanCarJourneyOperationCompleted;

        private System.Threading.SendOrPostCallback GetGridReferenceOperationCompleted;

        /// <remarks/>
        public TestCarJourneyPlannerSyncServiceProxyReference()
        {
            this.Url = "http://localhost/enhancedexposedservices/CarJourneyPlannerSynchronous/v1/CarJourn" +
                "eyPlannerSynchronousService.asmx";
        }

        /// <remarks/>
        public event PlanCarJourneyCompletedEventHandler PlanCarJourneyCompleted;

        /// <remarks/>
        public event GetGridReferenceCompletedEventHandler GetGridReferenceCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
            "yPlannerSynchronous.V1/PlanCarJourney", RequestNamespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
            "yPlannerSynchronous.V1", ResponseNamespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
            "yPlannerSynchronous.V1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CarJourneyResult PlanCarJourney(string transactionId, CarJourneyRequest request)
        {
            object[] results = this.Invoke("PlanCarJourney", new object[] {
                    transactionId,
                    request});
            return ((CarJourneyResult)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginPlanCarJourney(string transactionId, CarJourneyRequest request, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("PlanCarJourney", new object[] {
                    transactionId,
                    request}, callback, asyncState);
        }

        /// <remarks/>
        public CarJourneyResult EndPlanCarJourney(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CarJourneyResult)(results[0]));
        }

        /// <remarks/>
        public void PlanCarJourneyAsync(string transactionId, CarJourneyRequest request)
        {
            this.PlanCarJourneyAsync(transactionId, request, null);
        }

        /// <remarks/>
        public void PlanCarJourneyAsync(string transactionId, CarJourneyRequest request, object userState)
        {
            if ((this.PlanCarJourneyOperationCompleted == null))
            {
                this.PlanCarJourneyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPlanCarJourneyOperationCompleted);
            }
            this.InvokeAsync("PlanCarJourney", new object[] {
                    transactionId,
                    request}, this.PlanCarJourneyOperationCompleted, userState);
        }

        private void OnPlanCarJourneyOperationCompleted(object arg)
        {
            if ((this.PlanCarJourneyCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PlanCarJourneyCompleted(this, new PlanCarJourneyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
            "yPlannerSynchronous.V1/GetGridReference", RequestNamespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
            "yPlannerSynchronous.V1", ResponseNamespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
            "yPlannerSynchronous.V1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public OSGridReference GetGridReference(string transactionId, LocationType locationType, string locationValue)
        {
            object[] results = this.Invoke("GetGridReference", new object[] {
                    transactionId,
                    locationType,
                    locationValue});
            return ((OSGridReference)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetGridReference(string transactionId, LocationType locationType, string locationValue, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetGridReference", new object[] {
                    transactionId,
                    locationType,
                    locationValue}, callback, asyncState);
        }

        /// <remarks/>
        public OSGridReference EndGetGridReference(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((OSGridReference)(results[0]));
        }

        /// <remarks/>
        public void GetGridReferenceAsync(string transactionId, LocationType locationType, string locationValue)
        {
            this.GetGridReferenceAsync(transactionId, locationType, locationValue, null);
        }

        /// <remarks/>
        public void GetGridReferenceAsync(string transactionId, LocationType locationType, string locationValue, object userState)
        {
            if ((this.GetGridReferenceOperationCompleted == null))
            {
                this.GetGridReferenceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGridReferenceOperationCompleted);
            }
            this.InvokeAsync("GetGridReference", new object[] {
                    transactionId,
                    locationType,
                    locationValue}, this.GetGridReferenceOperationCompleted, userState);
        }

        private void OnGetGridReferenceOperationCompleted(object arg)
        {
            if ((this.GetGridReferenceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGridReferenceCompleted(this, new GetGridReferenceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }


    #region Class type definitions

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarJourneyRequest
    {

        private JourneyRequest[] journeyRequestsField;

        private CarParameters carParametersField;

        private ResultSettings resultSettingsField;

        /// <remarks/>
        public JourneyRequest[] JourneyRequests
        {
            get
            {
                return this.journeyRequestsField;
            }
            set
            {
                this.journeyRequestsField = value;
            }
        }

        /// <remarks/>
        public CarParameters CarParameters
        {
            get
            {
                return this.carParametersField;
            }
            set
            {
                this.carParametersField = value;
            }
        }

        /// <remarks/>
        public ResultSettings ResultSettings
        {
            get
            {
                return this.resultSettingsField;
            }
            set
            {
                this.resultSettingsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class JourneyRequest
    {

        private int journeyRequestIdField;

        private RequestLocation originLocationField;

        private RequestLocation destinationLocationField;

        private RequestLocation viaLocationField;

        private System.DateTime outwardDateTimeField;

        private System.DateTime returnDateTimeField;

        private bool returnDateTimeFieldSpecified;

        private bool outwardArriveBeforeField;

        private bool outwardArriveBeforeFieldSpecified;

        private bool returnArriveBeforeField;

        private bool returnArriveBeforeFieldSpecified;

        private bool isReturnRequiredField;

        /// <remarks/>
        public int JourneyRequestId
        {
            get
            {
                return this.journeyRequestIdField;
            }
            set
            {
                this.journeyRequestIdField = value;
            }
        }

        /// <remarks/>
        public RequestLocation OriginLocation
        {
            get
            {
                return this.originLocationField;
            }
            set
            {
                this.originLocationField = value;
            }
        }

        /// <remarks/>
        public RequestLocation DestinationLocation
        {
            get
            {
                return this.destinationLocationField;
            }
            set
            {
                this.destinationLocationField = value;
            }
        }

        /// <remarks/>
        public RequestLocation ViaLocation
        {
            get
            {
                return this.viaLocationField;
            }
            set
            {
                this.viaLocationField = value;
            }
        }

        /// <remarks/>
        public System.DateTime OutwardDateTime
        {
            get
            {
                return this.outwardDateTimeField;
            }
            set
            {
                this.outwardDateTimeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ReturnDateTime
        {
            get
            {
                return this.returnDateTimeField;
            }
            set
            {
                this.returnDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReturnDateTimeSpecified
        {
            get
            {
                return this.returnDateTimeFieldSpecified;
            }
            set
            {
                this.returnDateTimeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool OutwardArriveBefore
        {
            get
            {
                return this.outwardArriveBeforeField;
            }
            set
            {
                this.outwardArriveBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OutwardArriveBeforeSpecified
        {
            get
            {
                return this.outwardArriveBeforeFieldSpecified;
            }
            set
            {
                this.outwardArriveBeforeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool ReturnArriveBefore
        {
            get
            {
                return this.returnArriveBeforeField;
            }
            set
            {
                this.returnArriveBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReturnArriveBeforeSpecified
        {
            get
            {
                return this.returnArriveBeforeFieldSpecified;
            }
            set
            {
                this.returnArriveBeforeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool IsReturnRequired
        {
            get
            {
                return this.isReturnRequiredField;
            }
            set
            {
                this.isReturnRequiredField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class RequestLocation
    {

        private string descriptionField;

        private LocationType typeField;

        private OSGridReference gridReferenceField;

        private Naptan[] naPTANsField;

        private string postcodeField;

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public LocationType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public OSGridReference GridReference
        {
            get
            {
                return this.gridReferenceField;
            }
            set
            {
                this.gridReferenceField = value;
            }
        }

        /// <remarks/>
        public Naptan[] NaPTANs
        {
            get
            {
                return this.naPTANsField;
            }
            set
            {
                this.naPTANsField = value;
            }
        }

        /// <remarks/>
        public string Postcode
        {
            get
            {
                return this.postcodeField;
            }
            set
            {
                this.postcodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum LocationType
    {

        /// <remarks/>
        Postcode,

        /// <remarks/>
        NaPTAN,

        /// <remarks/>
        Coordinate,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class OSGridReference
    {

        private int eastingField;

        private int northingField;

        /// <remarks/>
        public int Easting
        {
            get
            {
                return this.eastingField;
            }
            set
            {
                this.eastingField = value;
            }
        }

        /// <remarks/>
        public int Northing
        {
            get
            {
                return this.northingField;
            }
            set
            {
                this.northingField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarJourneyDetail
    {

        private int instructionNumberField;

        private string instructionTextField;

        private string cumulativeDistanceField;

        private string arrivalTimeField;

        private CarCost costField;

        /// <remarks/>
        public int InstructionNumber
        {
            get
            {
                return this.instructionNumberField;
            }
            set
            {
                this.instructionNumberField = value;
            }
        }

        /// <remarks/>
        public string InstructionText
        {
            get
            {
                return this.instructionTextField;
            }
            set
            {
                this.instructionTextField = value;
            }
        }

        /// <remarks/>
        public string CumulativeDistance
        {
            get
            {
                return this.cumulativeDistanceField;
            }
            set
            {
                this.cumulativeDistanceField = value;
            }
        }

        /// <remarks/>
        public string ArrivalTime
        {
            get
            {
                return this.arrivalTimeField;
            }
            set
            {
                this.arrivalTimeField = value;
            }
        }

        /// <remarks/>
        public CarCost Cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                this.costField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarCost
    {

        private CarCostType costTypeField;

        private double costField;

        private string descriptionField;

        private string companyNameField;

        private string companyURLField;

        /// <remarks/>
        public CarCostType CostType
        {
            get
            {
                return this.costTypeField;
            }
            set
            {
                this.costTypeField = value;
            }
        }

        /// <remarks/>
        public double Cost
        {
            get
            {
                return this.costField;
            }
            set
            {
                this.costField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string CompanyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                this.companyNameField = value;
            }
        }

        /// <remarks/>
        public string CompanyURL
        {
            get
            {
                return this.companyURLField;
            }
            set
            {
                this.companyURLField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum CarCostType
    {

        /// <remarks/>
        TotalCost,

        /// <remarks/>
        TotalOtherCosts,

        /// <remarks/>
        FuelCost,

        /// <remarks/>
        RunningCost,

        /// <remarks/>
        OtherCost,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class Emissions
    {

        private VehicleType vehicleTypeField;

        private double cO2EmissionsField;

        /// <remarks/>
        public VehicleType VehicleType
        {
            get
            {
                return this.vehicleTypeField;
            }
            set
            {
                this.vehicleTypeField = value;
            }
        }

        /// <remarks/>
        public double CO2Emissions
        {
            get
            {
                return this.cO2EmissionsField;
            }
            set
            {
                this.cO2EmissionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum VehicleType
    {

        /// <remarks/>
        Car,

        /// <remarks/>
        SmallCar,

        /// <remarks/>
        LargeCar,

        /// <remarks/>
        Train,

        /// <remarks/>
        Bus,

        /// <remarks/>
        Coach,

        /// <remarks/>
        Plane,

        /// <remarks/>
        Bicycle,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class TimeSpan
    {

        private int daysField;

        private int hoursField;

        private int minutesField;

        private int secondsField;

        /// <remarks/>
        public int Days
        {
            get
            {
                return this.daysField;
            }
            set
            {
                this.daysField = value;
            }
        }

        /// <remarks/>
        public int Hours
        {
            get
            {
                return this.hoursField;
            }
            set
            {
                this.hoursField = value;
            }
        }

        /// <remarks/>
        public int Minutes
        {
            get
            {
                return this.minutesField;
            }
            set
            {
                this.minutesField = value;
            }
        }

        /// <remarks/>
        public int Seconds
        {
            get
            {
                return this.secondsField;
            }
            set
            {
                this.secondsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class ResponseLocation
    {

        private string descriptionField;

        private LocationType typeField;

        private OSGridReference gridReferenceField;

        private Naptan[] naPTANsField;

        private string postcodeField;

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public LocationType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public OSGridReference GridReference
        {
            get
            {
                return this.gridReferenceField;
            }
            set
            {
                this.gridReferenceField = value;
            }
        }

        /// <remarks/>
        public Naptan[] NaPTANs
        {
            get
            {
                return this.naPTANsField;
            }
            set
            {
                this.naPTANsField = value;
            }
        }

        /// <remarks/>
        public string Postcode
        {
            get
            {
                return this.postcodeField;
            }
            set
            {
                this.postcodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class Naptan
    {

        private OSGridReference gridReferenceField;

        private string naptanIdField;

        private string nameField;

        /// <remarks/>
        public OSGridReference GridReference
        {
            get
            {
                return this.gridReferenceField;
            }
            set
            {
                this.gridReferenceField = value;
            }
        }

        /// <remarks/>
        public string NaptanId
        {
            get
            {
                return this.naptanIdField;
            }
            set
            {
                this.naptanIdField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarJourneySummary
    {

        private ResponseLocation originLocationField;

        private ResponseLocation destinationLocationField;

        private ResponseLocation viaLocationField;

        private System.DateTime departureDateTimeField;

        private System.DateTime arrivalDateTimeField;

        private TimeSpan durationField;

        private double distanceField;

        private DistanceUnit distanceUnitField;

        private CarCost[] costsField;

        private Emissions[] emissionsField;

        private CarParameters carParametersField;

        private string summaryDirectionsField;

        /// <remarks/>
        public ResponseLocation OriginLocation
        {
            get
            {
                return this.originLocationField;
            }
            set
            {
                this.originLocationField = value;
            }
        }

        /// <remarks/>
        public ResponseLocation DestinationLocation
        {
            get
            {
                return this.destinationLocationField;
            }
            set
            {
                this.destinationLocationField = value;
            }
        }

        /// <remarks/>
        public ResponseLocation ViaLocation
        {
            get
            {
                return this.viaLocationField;
            }
            set
            {
                this.viaLocationField = value;
            }
        }

        /// <remarks/>
        public System.DateTime DepartureDateTime
        {
            get
            {
                return this.departureDateTimeField;
            }
            set
            {
                this.departureDateTimeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ArrivalDateTime
        {
            get
            {
                return this.arrivalDateTimeField;
            }
            set
            {
                this.arrivalDateTimeField = value;
            }
        }

        /// <remarks/>
        public TimeSpan Duration
        {
            get
            {
                return this.durationField;
            }
            set
            {
                this.durationField = value;
            }
        }

        /// <remarks/>
        public double Distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }

        /// <remarks/>
        public DistanceUnit DistanceUnit
        {
            get
            {
                return this.distanceUnitField;
            }
            set
            {
                this.distanceUnitField = value;
            }
        }

        /// <remarks/>
        public CarCost[] Costs
        {
            get
            {
                return this.costsField;
            }
            set
            {
                this.costsField = value;
            }
        }

        /// <remarks/>
        public Emissions[] Emissions
        {
            get
            {
                return this.emissionsField;
            }
            set
            {
                this.emissionsField = value;
            }
        }

        /// <remarks/>
        public CarParameters CarParameters
        {
            get
            {
                return this.carParametersField;
            }
            set
            {
                this.carParametersField = value;
            }
        }

        /// <remarks/>
        public string SummaryDirections
        {
            get
            {
                return this.summaryDirectionsField;
            }
            set
            {
                this.summaryDirectionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum DistanceUnit
    {

        /// <remarks/>
        Miles,

        /// <remarks/>
        Kms,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarParameters
    {

        private CarAlgorithmType algorithmField;

        private bool algorithmFieldSpecified;

        private CarSizeType carSizeTypeField;

        private bool carSizeTypeFieldSpecified;

        private FuelType fuelTypeField;

        private bool fuelTypeFieldSpecified;

        private int maxSpeedField;

        private bool maxSpeedFieldSpecified;

        private int fuelConsumptionField;

        private bool fuelConsumptionFieldSpecified;

        private FuelConsumptionUnit fuelConsumptionUnitField;

        private bool fuelConsumptionUnitFieldSpecified;

        private double fuelCostField;

        private bool fuelCostFieldSpecified;

        private bool banMotorwayField;

        private bool banMotorwayFieldSpecified;

        private bool avoidTollField;

        private bool avoidTollFieldSpecified;

        private bool avoidFerriesField;

        private bool avoidFerriesFieldSpecified;

        private bool avoidMotorwayField;

        private bool avoidMotorwayFieldSpecified;

        private string[] avoidRoadsField;

        private string[] useRoadsField;

        /// <remarks/>
        public CarAlgorithmType Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AlgorithmSpecified
        {
            get
            {
                return this.algorithmFieldSpecified;
            }
            set
            {
                this.algorithmFieldSpecified = value;
            }
        }

        /// <remarks/>
        public CarSizeType CarSizeType
        {
            get
            {
                return this.carSizeTypeField;
            }
            set
            {
                this.carSizeTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CarSizeTypeSpecified
        {
            get
            {
                return this.carSizeTypeFieldSpecified;
            }
            set
            {
                this.carSizeTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public FuelType FuelType
        {
            get
            {
                return this.fuelTypeField;
            }
            set
            {
                this.fuelTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelTypeSpecified
        {
            get
            {
                return this.fuelTypeFieldSpecified;
            }
            set
            {
                this.fuelTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int MaxSpeed
        {
            get
            {
                return this.maxSpeedField;
            }
            set
            {
                this.maxSpeedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MaxSpeedSpecified
        {
            get
            {
                return this.maxSpeedFieldSpecified;
            }
            set
            {
                this.maxSpeedFieldSpecified = value;
            }
        }

        /// <remarks/>
        public int FuelConsumption
        {
            get
            {
                return this.fuelConsumptionField;
            }
            set
            {
                this.fuelConsumptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelConsumptionSpecified
        {
            get
            {
                return this.fuelConsumptionFieldSpecified;
            }
            set
            {
                this.fuelConsumptionFieldSpecified = value;
            }
        }

        /// <remarks/>
        public FuelConsumptionUnit FuelConsumptionUnit
        {
            get
            {
                return this.fuelConsumptionUnitField;
            }
            set
            {
                this.fuelConsumptionUnitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelConsumptionUnitSpecified
        {
            get
            {
                return this.fuelConsumptionUnitFieldSpecified;
            }
            set
            {
                this.fuelConsumptionUnitFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double FuelCost
        {
            get
            {
                return this.fuelCostField;
            }
            set
            {
                this.fuelCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelCostSpecified
        {
            get
            {
                return this.fuelCostFieldSpecified;
            }
            set
            {
                this.fuelCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool BanMotorway
        {
            get
            {
                return this.banMotorwayField;
            }
            set
            {
                this.banMotorwayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BanMotorwaySpecified
        {
            get
            {
                return this.banMotorwayFieldSpecified;
            }
            set
            {
                this.banMotorwayFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool AvoidToll
        {
            get
            {
                return this.avoidTollField;
            }
            set
            {
                this.avoidTollField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidTollSpecified
        {
            get
            {
                return this.avoidTollFieldSpecified;
            }
            set
            {
                this.avoidTollFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool AvoidFerries
        {
            get
            {
                return this.avoidFerriesField;
            }
            set
            {
                this.avoidFerriesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidFerriesSpecified
        {
            get
            {
                return this.avoidFerriesFieldSpecified;
            }
            set
            {
                this.avoidFerriesFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool AvoidMotorway
        {
            get
            {
                return this.avoidMotorwayField;
            }
            set
            {
                this.avoidMotorwayField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidMotorwaySpecified
        {
            get
            {
                return this.avoidMotorwayFieldSpecified;
            }
            set
            {
                this.avoidMotorwayFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string[] AvoidRoads
        {
            get
            {
                return this.avoidRoadsField;
            }
            set
            {
                this.avoidRoadsField = value;
            }
        }

        /// <remarks/>
        public string[] UseRoads
        {
            get
            {
                return this.useRoadsField;
            }
            set
            {
                this.useRoadsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum CarAlgorithmType
    {

        /// <remarks/>
        Fastest,

        /// <remarks/>
        Shortest,

        /// <remarks/>
        MostEconomical,

        /// <remarks/>
        Cheapest,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum CarSizeType
    {

        /// <remarks/>
        Small,

        /// <remarks/>
        Medium,

        /// <remarks/>
        Large,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum FuelType
    {

        /// <remarks/>
        Petrol,

        /// <remarks/>
        Diesel,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum FuelConsumptionUnit
    {

        /// <remarks/>
        MilesPerGallon,

        /// <remarks/>
        LitresPer100Km,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarJourney
    {

        private CarJourneySummary summaryField;

        private CarJourneyDetail[] detailsField;

        /// <remarks/>
        public CarJourneySummary Summary
        {
            get
            {
                return this.summaryField;
            }
            set
            {
                this.summaryField = value;
            }
        }

        /// <remarks/>
        public CarJourneyDetail[] Details
        {
            get
            {
                return this.detailsField;
            }
            set
            {
                this.detailsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class JourneyResult
    {

        private int journeyIdField;

        private Message[] errorMessagesField;

        private Message[] userWarningsField;

        private CarJourney outwardCarJourneyField;

        private CarJourney returnCarJourneyField;

        /// <remarks/>
        public int JourneyId
        {
            get
            {
                return this.journeyIdField;
            }
            set
            {
                this.journeyIdField = value;
            }
        }

        /// <remarks/>
        public Message[] ErrorMessages
        {
            get
            {
                return this.errorMessagesField;
            }
            set
            {
                this.errorMessagesField = value;
            }
        }

        /// <remarks/>
        public Message[] UserWarnings
        {
            get
            {
                return this.userWarningsField;
            }
            set
            {
                this.userWarningsField = value;
            }
        }

        /// <remarks/>
        public CarJourney OutwardCarJourney
        {
            get
            {
                return this.outwardCarJourneyField;
            }
            set
            {
                this.outwardCarJourneyField = value;
            }
        }

        /// <remarks/>
        public CarJourney ReturnCarJourney
        {
            get
            {
                return this.returnCarJourneyField;
            }
            set
            {
                this.returnCarJourneyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class Message
    {

        private string textField;

        private int codeField;

        /// <remarks/>
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        public int Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CompletionStatus
    {

        private StatusType statusField;

        private Message[] messagesField;

        /// <remarks/>
        public StatusType Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public Message[] Messages
        {
            get
            {
                return this.messagesField;
            }
            set
            {
                this.messagesField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum StatusType
    {

        /// <remarks/>
        Success,

        /// <remarks/>
        Failed,

        /// <remarks/>
        ValidationError,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class CarJourneyResult
    {

        private CompletionStatus completionStatusField;

        private JourneyResult[] journeyResultsField;

        /// <remarks/>
        public CompletionStatus CompletionStatus
        {
            get
            {
                return this.completionStatusField;
            }
            set
            {
                this.completionStatusField = value;
            }
        }

        /// <remarks/>
        public JourneyResult[] JourneyResults
        {
            get
            {
                return this.journeyResultsField;
            }
            set
            {
                this.journeyResultsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public partial class ResultSettings
    {

        private ResultType resultTypeField;

        private bool resultTypeFieldSpecified;

        private DistanceUnit distanceUnitField;

        private bool distanceUnitFieldSpecified;

        private string languageField;

        /// <remarks/>
        public ResultType ResultType
        {
            get
            {
                return this.resultTypeField;
            }
            set
            {
                this.resultTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ResultTypeSpecified
        {
            get
            {
                return this.resultTypeFieldSpecified;
            }
            set
            {
                this.resultTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public DistanceUnit DistanceUnit
        {
            get
            {
                return this.distanceUnitField;
            }
            set
            {
                this.distanceUnitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DistanceUnitSpecified
        {
            get
            {
                return this.distanceUnitFieldSpecified;
            }
            set
            {
                this.distanceUnitFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.CarJourne" +
        "yPlannerSynchronous.V1")]
    public enum ResultType
    {

        /// <remarks/>
        Summary,

        /// <remarks/>
        Detailed,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void PlanCarJourneyCompletedEventHandler(object sender, PlanCarJourneyCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PlanCarJourneyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal PlanCarJourneyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CarJourneyResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CarJourneyResult)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void GetGridReferenceCompletedEventHandler(object sender, GetGridReferenceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGridReferenceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetGridReferenceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public OSGridReference Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((OSGridReference)(this.results[0]));
            }
        }
    }
    
    #endregion

}
