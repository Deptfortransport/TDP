// *********************************************** 
// NAME                 : TestFindNearestServiceProxyReference.cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 17/01/2006 
// DESCRIPTION  		: Proxy test class for FindNearestService.asmx
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/FindNearest/V1/Test/TestFindNearestServiceProxyReference.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:04:52   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:00   mturner
//Initial revision.
//
//   Rev 1.2   Mar 13 2006 13:54:06   RWilby
//FindNearestLocality web method
//
//   Rev 1.1   Feb 22 2006 10:12:52   mdambrine
//fixed the unit tests after namespace change
//
//   Rev 1.0   Jan 19 2006 10:41:12   RWilby
//Initial revision.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110

namespace TransportDirect.EnhancedExposedServices.FindNearest.V1.Test
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
	[System.Web.Services.WebServiceBindingAttribute(Name="FindNearestServiceSoap", Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1")]
	public class TestFindNearestServiceProxyReference : Microsoft.Web.Services3.WebServicesClientProtocol  
	{
        
		/// <remarks/>
		public TestFindNearestServiceProxyReference() 
		{
			this.Url = "http://localhost/EnhancedExposedServices/FindNearest/V1/FindNearestService.asmx";
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1/GetGridReference", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public OSGridReference GetGridReference(string transactionId, string language, string postcode) 
		{
			object[] results = this.Invoke("GetGridReference", new object[] {
																				transactionId,
																				language,
																				postcode});
			return ((OSGridReference)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BeginGetGridReference(string transactionId, string language, string postcode, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("GetGridReference", new object[] {
																		 transactionId,
																		 language,
																		 postcode}, callback, asyncState);
		}
        
		/// <remarks/>
		public OSGridReference EndGetGridReference(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((OSGridReference)(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1/FindNearestAirports", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public NaptanProximity[] FindNearestAirports(string transactionId, string language, OSGridReference gridReference, int maxResults) 
		{
			object[] results = this.Invoke("FindNearestAirports", new object[] {
																				   transactionId,
																				   language,
																				   gridReference,
																				   maxResults});
			return ((NaptanProximity[])(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BeginFindNearestAirports(string transactionId, string language, OSGridReference gridReference, int maxResults, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("FindNearestAirports", new object[] {
																			transactionId,
																			language,
																			gridReference,
																			maxResults}, callback, asyncState);
		}
        
		/// <remarks/>
		public NaptanProximity[] EndFindNearestAirports(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((NaptanProximity[])(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1/FindNearestStations", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public NaptanProximity[] FindNearestStations(string transactionId, string language, OSGridReference gridReference, int maxResults) 
		{
			object[] results = this.Invoke("FindNearestStations", new object[] {
																				   transactionId,
																				   language,
																				   gridReference,
																				   maxResults});
			return ((NaptanProximity[])(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BeginFindNearestStations(string transactionId, string language, OSGridReference gridReference, int maxResults, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("FindNearestStations", new object[] {
																			transactionId,
																			language,
																			gridReference,
																			maxResults}, callback, asyncState);
		}
        
		/// <remarks/>
		public NaptanProximity[] EndFindNearestStations(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((NaptanProximity[])(results[0]));
		}
        
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1/FindNearestBusStops", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public NaptanProximity[] FindNearestBusStops(string transactionId, string language, OSGridReference gridReference, int maxResults) 
		{
			object[] results = this.Invoke("FindNearestBusStops", new object[] {
																				   transactionId,
																				   language,
																				   gridReference,
																				   maxResults});
			return ((NaptanProximity[])(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BeginFindNearestBusStops(string transactionId, string language, OSGridReference gridReference, int maxResults, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("FindNearestBusStops", new object[] {
																			transactionId,
																			language,
																			gridReference,
																			maxResults}, callback, asyncState);
		}
        
		/// <remarks/>
		public NaptanProximity[] EndFindNearestBusStops(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((NaptanProximity[])(results[0]));
		}

		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
			 "st.V1/FindNearestLocality", RequestNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
			 "st.V1", ResponseNamespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNeare" +
			 "st.V1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
		public string FindNearestLocality(string transactionId, string language, OSGridReference gridReference) 
		{
			object[] results = this.Invoke("FindNearestLocality", new object[] {
																				   transactionId,
																				   language,
																				   gridReference});
			return ((string)(results[0]));
		}
        
		/// <remarks/>
		public System.IAsyncResult BeginFindNearestLocality(string transactionId, string language, OSGridReference gridReference, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("FindNearestLocality", new object[] {
																			transactionId,
																			language,
																			gridReference}, callback, asyncState);
		}
        
		/// <remarks/>
		public string EndFindNearestLocality(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
	}
    
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1")]
	public class OSGridReference 
	{
        
		/// <remarks/>
		public int Easting;
        
		/// <remarks/>
		public int Northing;
	}
    
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1")]
	public class NaptanProximity 
	{
        
		/// <remarks/>
		public string NaptanId;
        
		/// <remarks/>
		public string Name;
        
		/// <remarks/>
		public OSGridReference GridReference;
        
		/// <remarks/>
		public int Distance;
        
		/// <remarks/>
		public NaptanType Type;
        
		/// <remarks/>
		public string SMSCode;
        
		/// <remarks/>
		public string CRSCode;
        
		/// <remarks/>
		public string IATA;
	}
    
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info/TransportDirect.EnhancedExposedServices.FindNearest.V1")]
	public enum NaptanType 
	{
        
		/// <remarks/>
		Airport,
        
		/// <remarks/>
		Station,
        
		/// <remarks/>
		BusStop,
	}
}
