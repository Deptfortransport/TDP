// *********************************************** 
// NAME                 : TaxiInformationStopDetail.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006 
// DESCRIPTION  		: DTO class implementation o for StopTaxiInformation domain object. For detail information refer to StopTaxiInformation
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TaxiInformation/V1/TaxiInformationStopDetail.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:50   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 19:38:42   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 16 2006 13:48:32   schand
//Added constructor
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111



namespace TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1
{
	/// <summary>
	/// DTO class implementation o for StopTaxiInformation domain object. For detail information refer to StopTaxiInformation
	/// </summary>
	[System.Serializable]
	public class TaxiInformationStopDetail
	{
		#region Private members
		private string stopName;  
		private string stopNaptan;
		private string comment;   
		private TaxiInformationOperator[]  operators;
		private TaxiInformationStopDetail[] alternativeStops;
		private bool accessibleOperatorPresent;               
		private string accessibleText;                        
		private bool informationAvailable;    
        #endregion
        
		#region Constructor
		public TaxiInformationStopDetail()
		{
			stopName = string.Empty;  
			stopNaptan = string.Empty;
			comment = string.Empty;   					
			accessibleOperatorPresent = false;               
			accessibleText = string.Empty;                        
			informationAvailable = false;    
		}
		#endregion

		#region Public properties
		
		/// <summary>
		/// Read-Write property for StopName
		/// </summary>
		public string StopName
		{
			get {return stopName;}
			set {stopName=value;}
		}

		/// <summary>
		/// Read-Write property for StopNaptan
		/// </summary>
		public string StopNaptan
		{
			get {return stopNaptan;}
			set {stopNaptan=value;}
		}

		/// <summary>
		/// Read-Write property for Comment
		/// </summary>
		public string Comment
		{
			get {return comment;}
			set {comment=value;}
		}
		
		/// <summary>
		/// Read-Write property for Operators
		/// </summary>
		public TaxiInformationOperator[]  Operators
		{
			get {return operators;}
			set {operators=value;}
		}
		
		/// <summary>
		/// Read-Write property for AlternativeStops
		/// </summary>
		public TaxiInformationStopDetail[] AlternativeStops
		{
			get {return alternativeStops;}
			set {alternativeStops=value;}
		}
		
		/// <summary>
		/// Read-Write property for AccessibleOperatorPresent
		/// </summary>
		public bool AccessibleOperatorPresent
		{
			get {return accessibleOperatorPresent;}
			set {accessibleOperatorPresent=value;}
		}   
		
		/// <summary>
		/// Read-Write property for AccessibleText
		/// </summary>
		public string AccessibleText
		{
			get {return accessibleText;}
			set {accessibleText=value;}
		}            
		
		/// <summary>
		/// Read-Write property for InformationAvailable
		/// </summary>
		public bool InformationAvailable
		{
			get {return informationAvailable;}
			set {informationAvailable=value;}
		}
		#endregion
	}

} // TransportDirect.EnhancedExposedServices.DataTransfer.TaxiInformation.V1
