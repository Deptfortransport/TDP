// *********************************************** 
// NAME                 : CodeServiceCodeDetail.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO class implementation of TDCodeDetail. For Information refer to TDCodeDetail.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CodeHandler/V1/CodeServiceCodeDetail.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:14   mturner
//Initial revision.
//
//   Rev 1.2   Jan 20 2006 16:28:24   schand
//Added more comments to the properties
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:25:30   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;   
namespace TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
{
	/// <summary>
	/// DTO class implementation of TDCodeDetail. For Information refer to TDCodeDetail.
	/// </summary>
	[System.Serializable]  
	public class CodeServiceCodeDetail
	{
		#region Private Members
		private string naptanId;
		private CodeServiceCodeType codeType;
		private string code;                 
		private string description;          
		private string locality;             
		private string region;               
		private  OSGridReference gridReference;                  
		private CodeServiceModeType modeType;
		#endregion

		#region Constructor
		public CodeServiceCodeDetail()
		{
			naptanId = string.Empty;		
			code = string.Empty;                 
			description = string.Empty;          
			locality = string.Empty;             
			region = string.Empty;               
			gridReference = new OSGridReference();                  
		}
		#endregion

		#region Public properties
		/// <summary>
		/// Read-Write property for NaptanId
		/// </summary>
		public string NaptanId
		{
			get {return naptanId;}
			set {naptanId=value;}
		}
		
		/// <summary>
		/// Read-Write property for CodeType
		/// </summary>
		public CodeServiceCodeType CodeType
		{
			get {return codeType;}
			set {codeType=value;}
		}
		
		/// <summary>
		/// Read-Write property for Code
		/// </summary>
		public string Code
		{
			get {return code;}
			set {code=value;}
		}     

		/// <summary>
		/// Read-Write property for Description
		/// </summary>
		public string Description
		{
			get {return description;}
			set {description=value;}
		}
		
		/// <summary>
		/// Read-Write property for Locality
		/// </summary>
		public string Locality
		{
			get {return locality;}
			set {locality=value;}
		} 
		
		/// <summary>
		/// Read-Write property for Region
		/// </summary>
		public string Region
		{
			get {return region;}
			set {region=value;}
		}   
		
		/// <summary>
		/// Read-Write property for GridReference
		/// </summary>
		public  OSGridReference GridReference
		{
			get {return gridReference;}
			set {gridReference=value;}
		}   

		/// <summary>
		/// Read-Write property for ModeType
		/// </summary>
		public CodeServiceModeType ModeType
		{
			get {return modeType;}
			set {modeType=value;}
		}  
		#endregion


	}

} // TransportDirect.EnhancedExposedServices.DataTransfer.CodeHandler.V1
