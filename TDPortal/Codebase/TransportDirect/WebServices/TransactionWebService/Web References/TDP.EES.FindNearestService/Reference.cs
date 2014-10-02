﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.296.
// 
#pragma warning disable 1591

namespace TransportDirect.ReportDataProvider.TransactionWebService.TDP.EES.FindNearestService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="FindNearestSoap", Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
        "st.V1")]
    public partial class FindNearest : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetGridReferenceOperationCompleted;
        
        private System.Threading.SendOrPostCallback FindNearestAirportsOperationCompleted;
        
        private System.Threading.SendOrPostCallback FindNearestStationsOperationCompleted;
        
        private System.Threading.SendOrPostCallback FindNearestBusStopsOperationCompleted;
        
        private System.Threading.SendOrPostCallback FindNearestLocalityOperationCompleted;
        
        private System.Threading.SendOrPostCallback FindNearestFuelGenieOperationCompleted;
        
        private System.Threading.SendOrPostCallback FindFuelGenieSitesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public FindNearest() {
            this.Url = global::TransportDirect.ReportDataProvider.TransactionWebService.Properties.Settings.Default.td_reportdataprovider_transactionwebservice_TDP_EES_FindNearestService_FindNearest;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetGridReferenceCompletedEventHandler GetGridReferenceCompleted;
        
        /// <remarks/>
        public event FindNearestAirportsCompletedEventHandler FindNearestAirportsCompleted;
        
        /// <remarks/>
        public event FindNearestStationsCompletedEventHandler FindNearestStationsCompleted;
        
        /// <remarks/>
        public event FindNearestBusStopsCompletedEventHandler FindNearestBusStopsCompleted;
        
        /// <remarks/>
        public event FindNearestLocalityCompletedEventHandler FindNearestLocalityCompleted;
        
        /// <remarks/>
        public event FindNearestFuelGenieCompletedEventHandler FindNearestFuelGenieCompleted;
        
        /// <remarks/>
        public event FindFuelGenieSitesCompletedEventHandler FindFuelGenieSitesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/GetGridReference", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public OSGridReference GetGridReference(string transactionId, string language, string postcode) {
            object[] results = this.Invoke("GetGridReference", new object[] {
                        transactionId,
                        language,
                        postcode});
            return ((OSGridReference)(results[0]));
        }
        
        /// <remarks/>
        public void GetGridReferenceAsync(string transactionId, string language, string postcode) {
            this.GetGridReferenceAsync(transactionId, language, postcode, null);
        }
        
        /// <remarks/>
        public void GetGridReferenceAsync(string transactionId, string language, string postcode, object userState) {
            if ((this.GetGridReferenceOperationCompleted == null)) {
                this.GetGridReferenceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGridReferenceOperationCompleted);
            }
            this.InvokeAsync("GetGridReference", new object[] {
                        transactionId,
                        language,
                        postcode}, this.GetGridReferenceOperationCompleted, userState);
        }
        
        private void OnGetGridReferenceOperationCompleted(object arg) {
            if ((this.GetGridReferenceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGridReferenceCompleted(this, new GetGridReferenceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/FindNearestAirports", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public NaptanProximity[] FindNearestAirports(string transactionId, string language, OSGridReference gridReference, int maxResults) {
            object[] results = this.Invoke("FindNearestAirports", new object[] {
                        transactionId,
                        language,
                        gridReference,
                        maxResults});
            return ((NaptanProximity[])(results[0]));
        }
        
        /// <remarks/>
        public void FindNearestAirportsAsync(string transactionId, string language, OSGridReference gridReference, int maxResults) {
            this.FindNearestAirportsAsync(transactionId, language, gridReference, maxResults, null);
        }
        
        /// <remarks/>
        public void FindNearestAirportsAsync(string transactionId, string language, OSGridReference gridReference, int maxResults, object userState) {
            if ((this.FindNearestAirportsOperationCompleted == null)) {
                this.FindNearestAirportsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFindNearestAirportsOperationCompleted);
            }
            this.InvokeAsync("FindNearestAirports", new object[] {
                        transactionId,
                        language,
                        gridReference,
                        maxResults}, this.FindNearestAirportsOperationCompleted, userState);
        }
        
        private void OnFindNearestAirportsOperationCompleted(object arg) {
            if ((this.FindNearestAirportsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FindNearestAirportsCompleted(this, new FindNearestAirportsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/FindNearestStations", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public NaptanProximity[] FindNearestStations(string transactionId, string language, OSGridReference gridReference, int maxResults) {
            object[] results = this.Invoke("FindNearestStations", new object[] {
                        transactionId,
                        language,
                        gridReference,
                        maxResults});
            return ((NaptanProximity[])(results[0]));
        }
        
        /// <remarks/>
        public void FindNearestStationsAsync(string transactionId, string language, OSGridReference gridReference, int maxResults) {
            this.FindNearestStationsAsync(transactionId, language, gridReference, maxResults, null);
        }
        
        /// <remarks/>
        public void FindNearestStationsAsync(string transactionId, string language, OSGridReference gridReference, int maxResults, object userState) {
            if ((this.FindNearestStationsOperationCompleted == null)) {
                this.FindNearestStationsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFindNearestStationsOperationCompleted);
            }
            this.InvokeAsync("FindNearestStations", new object[] {
                        transactionId,
                        language,
                        gridReference,
                        maxResults}, this.FindNearestStationsOperationCompleted, userState);
        }
        
        private void OnFindNearestStationsOperationCompleted(object arg) {
            if ((this.FindNearestStationsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FindNearestStationsCompleted(this, new FindNearestStationsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/FindNearestBusStops", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public NaptanProximity[] FindNearestBusStops(string transactionId, string language, OSGridReference gridReference, int maxResults) {
            object[] results = this.Invoke("FindNearestBusStops", new object[] {
                        transactionId,
                        language,
                        gridReference,
                        maxResults});
            return ((NaptanProximity[])(results[0]));
        }
        
        /// <remarks/>
        public void FindNearestBusStopsAsync(string transactionId, string language, OSGridReference gridReference, int maxResults) {
            this.FindNearestBusStopsAsync(transactionId, language, gridReference, maxResults, null);
        }
        
        /// <remarks/>
        public void FindNearestBusStopsAsync(string transactionId, string language, OSGridReference gridReference, int maxResults, object userState) {
            if ((this.FindNearestBusStopsOperationCompleted == null)) {
                this.FindNearestBusStopsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFindNearestBusStopsOperationCompleted);
            }
            this.InvokeAsync("FindNearestBusStops", new object[] {
                        transactionId,
                        language,
                        gridReference,
                        maxResults}, this.FindNearestBusStopsOperationCompleted, userState);
        }
        
        private void OnFindNearestBusStopsOperationCompleted(object arg) {
            if ((this.FindNearestBusStopsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FindNearestBusStopsCompleted(this, new FindNearestBusStopsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/FindNearestLocality", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FindNearestLocality(string transactionId, string language, OSGridReference gridReference) {
            object[] results = this.Invoke("FindNearestLocality", new object[] {
                        transactionId,
                        language,
                        gridReference});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FindNearestLocalityAsync(string transactionId, string language, OSGridReference gridReference) {
            this.FindNearestLocalityAsync(transactionId, language, gridReference, null);
        }
        
        /// <remarks/>
        public void FindNearestLocalityAsync(string transactionId, string language, OSGridReference gridReference, object userState) {
            if ((this.FindNearestLocalityOperationCompleted == null)) {
                this.FindNearestLocalityOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFindNearestLocalityOperationCompleted);
            }
            this.InvokeAsync("FindNearestLocality", new object[] {
                        transactionId,
                        language,
                        gridReference}, this.FindNearestLocalityOperationCompleted, userState);
        }
        
        private void OnFindNearestLocalityOperationCompleted(object arg) {
            if ((this.FindNearestLocalityCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FindNearestLocalityCompleted(this, new FindNearestLocalityCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/FindNearestFuelGenie", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public FuelRetailer FindNearestFuelGenie(string transactionId, string language, string postcode, int easting, int northing, string searchType, string searchFlag) {
            object[] results = this.Invoke("FindNearestFuelGenie", new object[] {
                        transactionId,
                        language,
                        postcode,
                        easting,
                        northing,
                        searchType,
                        searchFlag});
            return ((FuelRetailer)(results[0]));
        }
        
        /// <remarks/>
        public void FindNearestFuelGenieAsync(string transactionId, string language, string postcode, int easting, int northing, string searchType, string searchFlag) {
            this.FindNearestFuelGenieAsync(transactionId, language, postcode, easting, northing, searchType, searchFlag, null);
        }
        
        /// <remarks/>
        public void FindNearestFuelGenieAsync(string transactionId, string language, string postcode, int easting, int northing, string searchType, string searchFlag, object userState) {
            if ((this.FindNearestFuelGenieOperationCompleted == null)) {
                this.FindNearestFuelGenieOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFindNearestFuelGenieOperationCompleted);
            }
            this.InvokeAsync("FindNearestFuelGenie", new object[] {
                        transactionId,
                        language,
                        postcode,
                        easting,
                        northing,
                        searchType,
                        searchFlag}, this.FindNearestFuelGenieOperationCompleted, userState);
        }
        
        private void OnFindNearestFuelGenieOperationCompleted(object arg) {
            if ((this.FindNearestFuelGenieCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FindNearestFuelGenieCompleted(this, new FindNearestFuelGenieCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1/FindFuelGenieSites", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
            "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("FindFuelGenieSitesResult")]
        public FuelRetailer[] FindFuelGenieSites(string transactionId, string language, string postcode, int easting, int northing, string searchType, string searchFlag, float searchRadius) {
            object[] results = this.Invoke("FindFuelGenieSites", new object[] {
                        transactionId,
                        language,
                        postcode,
                        easting,
                        northing,
                        searchType,
                        searchFlag,
                        searchRadius});
            return ((FuelRetailer[])(results[0]));
        }
        
        /// <remarks/>
        public void FindFuelGenieSitesAsync(string transactionId, string language, string postcode, int easting, int northing, string searchType, string searchFlag, float searchRadius) {
            this.FindFuelGenieSitesAsync(transactionId, language, postcode, easting, northing, searchType, searchFlag, searchRadius, null);
        }
        
        /// <remarks/>
        public void FindFuelGenieSitesAsync(string transactionId, string language, string postcode, int easting, int northing, string searchType, string searchFlag, float searchRadius, object userState) {
            if ((this.FindFuelGenieSitesOperationCompleted == null)) {
                this.FindFuelGenieSitesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFindFuelGenieSitesOperationCompleted);
            }
            this.InvokeAsync("FindFuelGenieSites", new object[] {
                        transactionId,
                        language,
                        postcode,
                        easting,
                        northing,
                        searchType,
                        searchFlag,
                        searchRadius}, this.FindFuelGenieSitesOperationCompleted, userState);
        }
        
        private void OnFindFuelGenieSitesOperationCompleted(object arg) {
            if ((this.FindFuelGenieSitesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FindFuelGenieSitesCompleted(this, new FindFuelGenieSitesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
        "st.V1")]
    public partial class OSGridReference {
        
        private int eastingField;
        
        private int northingField;
        
        /// <remarks/>
        public int Easting {
            get {
                return this.eastingField;
            }
            set {
                this.eastingField = value;
            }
        }
        
        /// <remarks/>
        public int Northing {
            get {
                return this.northingField;
            }
            set {
                this.northingField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
        "st.V1")]
    public partial class FuelRetailer {
        
        private string postCodeField;
        
        private string fuelGenieSiteField;
        
        private int eastingField;
        
        private int northingField;
        
        private string brandField;
        
        private string siteReference1Field;
        
        private string siteReference2Field;
        
        private string siteReference3Field;
        
        private string siteReference4Field;
        
        private float milesField;
        
        /// <remarks/>
        public string PostCode {
            get {
                return this.postCodeField;
            }
            set {
                this.postCodeField = value;
            }
        }
        
        /// <remarks/>
        public string FuelGenieSite {
            get {
                return this.fuelGenieSiteField;
            }
            set {
                this.fuelGenieSiteField = value;
            }
        }
        
        /// <remarks/>
        public int Easting {
            get {
                return this.eastingField;
            }
            set {
                this.eastingField = value;
            }
        }
        
        /// <remarks/>
        public int Northing {
            get {
                return this.northingField;
            }
            set {
                this.northingField = value;
            }
        }
        
        /// <remarks/>
        public string Brand {
            get {
                return this.brandField;
            }
            set {
                this.brandField = value;
            }
        }
        
        /// <remarks/>
        public string SiteReference1 {
            get {
                return this.siteReference1Field;
            }
            set {
                this.siteReference1Field = value;
            }
        }
        
        /// <remarks/>
        public string SiteReference2 {
            get {
                return this.siteReference2Field;
            }
            set {
                this.siteReference2Field = value;
            }
        }
        
        /// <remarks/>
        public string SiteReference3 {
            get {
                return this.siteReference3Field;
            }
            set {
                this.siteReference3Field = value;
            }
        }
        
        /// <remarks/>
        public string SiteReference4 {
            get {
                return this.siteReference4Field;
            }
            set {
                this.siteReference4Field = value;
            }
        }
        
        /// <remarks/>
        public float Miles {
            get {
                return this.milesField;
            }
            set {
                this.milesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
        "st.V1")]
    public partial class NaptanProximity {
        
        private string naptanIdField;
        
        private string nameField;
        
        private OSGridReference gridReferenceField;
        
        private int distanceField;
        
        private NaptanType typeField;
        
        private string sMSCodeField;
        
        private string cRSCodeField;
        
        private string iATAField;
        
        /// <remarks/>
        public string NaptanId {
            get {
                return this.naptanIdField;
            }
            set {
                this.naptanIdField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public OSGridReference GridReference {
            get {
                return this.gridReferenceField;
            }
            set {
                this.gridReferenceField = value;
            }
        }
        
        /// <remarks/>
        public int Distance {
            get {
                return this.distanceField;
            }
            set {
                this.distanceField = value;
            }
        }
        
        /// <remarks/>
        public NaptanType Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        public string SMSCode {
            get {
                return this.sMSCodeField;
            }
            set {
                this.sMSCodeField = value;
            }
        }
        
        /// <remarks/>
        public string CRSCode {
            get {
                return this.cRSCodeField;
            }
            set {
                this.cRSCodeField = value;
            }
        }
        
        /// <remarks/>
        public string IATA {
            get {
                return this.iATAField;
            }
            set {
                this.iATAField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
        "st.V1")]
    public enum NaptanType {
        
        /// <remarks/>
        Airport,
        
        /// <remarks/>
        Station,
        
        /// <remarks/>
        BusStop,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetGridReferenceCompletedEventHandler(object sender, GetGridReferenceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGridReferenceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGridReferenceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public OSGridReference Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((OSGridReference)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FindNearestAirportsCompletedEventHandler(object sender, FindNearestAirportsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FindNearestAirportsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FindNearestAirportsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public NaptanProximity[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((NaptanProximity[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FindNearestStationsCompletedEventHandler(object sender, FindNearestStationsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FindNearestStationsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FindNearestStationsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public NaptanProximity[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((NaptanProximity[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FindNearestBusStopsCompletedEventHandler(object sender, FindNearestBusStopsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FindNearestBusStopsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FindNearestBusStopsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public NaptanProximity[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((NaptanProximity[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FindNearestLocalityCompletedEventHandler(object sender, FindNearestLocalityCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FindNearestLocalityCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FindNearestLocalityCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FindNearestFuelGenieCompletedEventHandler(object sender, FindNearestFuelGenieCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FindNearestFuelGenieCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FindNearestFuelGenieCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public FuelRetailer Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((FuelRetailer)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void FindFuelGenieSitesCompletedEventHandler(object sender, FindFuelGenieSitesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FindFuelGenieSitesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FindFuelGenieSitesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public FuelRetailer[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((FuelRetailer[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591