// *********************************************** 
// NAME                 : TravelNewsServiceHeadlineItem.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO class implementation of HeadlineItem. For Information refer to HeadlineItem.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TravelNews/V1/TravelNewsServiceHeadlineItem.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:52   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 19:38:44   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
{
	/// <summary>
	/// DTO class implementation of HeadlineItem. For Information refer to HeadlineItem.
	/// </summary>
	[System.Serializable]
	public class TravelNewsServiceHeadlineItem
	{
		#region Private members
		private  string uid;
		private  string headlineText;
		private  TravelNewsServiceSeverityLevel severityLevel;
		private  TravelNewsServiceTransportType transportType;
		private  string regions;                               
		private  TravelNewsServiceDelayType[]	 travelNewsServiceDelayTypes;
		#endregion
		
		#region Constructor
		public TravelNewsServiceHeadlineItem()
		{
			uid   = string.Empty; 
			headlineText = string.Empty;
			regions = string.Empty;
			travelNewsServiceDelayTypes = new TravelNewsServiceDelayType[0];
		}
		#endregion
		
		#region Public properties		
		
		/// <summary>
		/// Read-Write property for Uid
		/// </summary>
		public  string Uid
		{  
			get {return uid;}
			set {uid=value;}
		}

		/// <summary>
		/// Read-Write property for HeadlineText
		/// </summary>
		public  string HeadlineText
		{  
			get {return headlineText;}
			set {headlineText=value;}
		}

		/// <summary>
		/// Read-Write property for DelayTypes
		/// </summary>
		public  TravelNewsServiceDelayType[] DelayTypes
		{  
			get {return travelNewsServiceDelayTypes;}
			set {travelNewsServiceDelayTypes=value;}
		}

		/// <summary>
		/// Read-Write property for SeverityLevel 
		/// </summary>
		public  TravelNewsServiceSeverityLevel SeverityLevel
		{  
			get {return severityLevel;}
			set {severityLevel=value;}
		}

		/// <summary>
		/// Read-Write property for TransportType
		/// </summary>
		public  TravelNewsServiceTransportType TransportType
		{  
			get {return transportType;}
			set {transportType=value;}
		}

		/// <summary>
		/// Read-Write property for Regions
		/// </summary>
		public  string Regions
		{  
			get {return regions;}
			set {regions=value;}
		}
		#endregion


	}// END CLASS DEFINITION TravelNewsServiceHeadlineItem

} // TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
